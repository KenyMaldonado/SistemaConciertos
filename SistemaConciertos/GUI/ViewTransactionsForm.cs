using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BLL.Clases;
using BLL.EstructuraDatos;
using BLL.Utils;

namespace GUI
{
    public partial class ViewTransactionsForm : Form
    {
        private Estadio _estadio;
        private ColaDinamica<Transaccion> _colaTransaccionesNormal;
        private ColaDinamica<Transaccion> _colaTransaccionesVIP;
        private List<Transaccion> _transaccionesProcesadas;

        public ViewTransactionsForm(Estadio estadio, ColaDinamica<Transaccion> colaNormal, ColaDinamica<Transaccion> colaVIP, List<Transaccion> transaccionesProcesadas)
        {
            InitializeComponent();
            _estadio = estadio;
            _colaTransaccionesNormal = colaNormal;
            _colaTransaccionesVIP = colaVIP;
            _transaccionesProcesadas = transaccionesProcesadas;
            this.Load += ViewTransactionsForm_Load;
        }

        private void ViewTransactionsForm_Load(object sender, EventArgs e)
        {
            ConfigurarDataGridViewColumns();
            CargarTransaccionesEnDataGridView(); // Cargar al inicio
        }

        private void ConfigurarDataGridViewColumns()
        {
            dgvTransacciones.Columns.Clear();
            dgvTransacciones.AutoGenerateColumns = false;

            dgvTransacciones.Columns.Add(new DataGridViewTextBoxColumn() { Name = "IdTransaccion", HeaderText = "ID", DataPropertyName = "IdTransaccion", Width = 50 });
            dgvTransacciones.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Comprador", HeaderText = "Comprador", DataPropertyName = "NombreComprador", Width = 150 });
            dgvTransacciones.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Zona", HeaderText = "Zona", DataPropertyName = "ZonaAsignada", Width = 100 });
            dgvTransacciones.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Asiento", HeaderText = "Asiento", DataPropertyName = "NumeroAsiento", Width = 70 });
            dgvTransacciones.Columns.Add(new DataGridViewTextBoxColumn() { Name = "TipoBoleto", HeaderText = "Tipo", DataPropertyName = "TipoBoleto", Width = 80 });
            dgvTransacciones.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Estado", HeaderText = "Estado", DataPropertyName = "Estado", Width = 90 });
            dgvTransacciones.Columns.Add(new DataGridViewTextBoxColumn() { Name = "FechaProcesamiento", HeaderText = "Fecha Proceso", DataPropertyName = "FechaProcesamiento", Width = 150 });
            dgvTransacciones.Columns.Add(new DataGridViewTextBoxColumn() { Name = "CodigoQR", HeaderText = "Código QR", DataPropertyName = "CodigoQR", Width = 180 });

            foreach (DataGridViewColumn col in dgvTransacciones.Columns)
            {
                col.ReadOnly = true;
            }
        }

        public void AgregarTransaccionProcesada(Transaccion transaccion)
        {
            if (transaccion.Estado == EstadoTransaccion.Procesada)
            {
                _transaccionesProcesadas.Add(transaccion);
                CargarTransaccionesEnDataGridView(); // Recargar la vista
            }
        }

        public void CargarTransaccionesEnDataGridView()
        {
            // Cargar transacciones desde archivo
            List<Transaccion> transaccionesDesdeArchivo = FileManager.CargarTransacciones();

            _transaccionesProcesadas.Clear();
            _transaccionesProcesadas.AddRange(transaccionesDesdeArchivo);

            // Asignar la fuente de datos
            dgvTransacciones.DataSource = null; // Limpiar para evitar conflictos
            dgvTransacciones.DataSource = _transaccionesProcesadas;

            // Ajustar columnas automáticamente
            dgvTransacciones.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void dgvTransacciones_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTransacciones.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvTransacciones.SelectedRows[0];
                if (row.Cells["IdTransaccion"].Value is int idTransaccion)
                {
                    Transaccion transaccion = _transaccionesProcesadas.Find(t => t.IdTransaccion == idTransaccion);
                    if (transaccion != null)
                    {
                        txtDetalleBoleto.Text = transaccion.BoletoComprado.MostrarInformacion();

                        string rutaQR = transaccion.BoletoComprado.CodigoQR;
                        if (!string.IsNullOrEmpty(rutaQR) && File.Exists(rutaQR))
                        {
                            try
                            {
                                using (var fs = new FileStream(rutaQR, FileMode.Open, FileAccess.Read))
                                using (var ms = new MemoryStream())
                                {
                                    fs.CopyTo(ms);
                                    pbQRCode.Image = Image.FromStream(ms);
                                }
                            }
                            catch (Exception ex)
                            {
                                pbQRCode.Image = null;
                                MessageBox.Show($"Error al cargar imagen QR: {ex.Message}", "Error QR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            pbQRCode.Image = null;
                        }
                    }
                }
            }
            else
            {
                txtDetalleBoleto.Clear();
                pbQRCode.Image = null;
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarTransaccionesEnDataGridView();
            MessageBox.Show("Lista de transacciones actualizada.", "Actualización", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
