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
            _estadio = estadio; // Asigna la instancia del estadio pasada desde el formulario principal
            _colaTransaccionesNormal = colaNormal;
            _colaTransaccionesVIP = colaVIP;
            // IMPORTANTE: Aquí inicializamos _transaccionesProcesadas con la lista pasada desde el formulario principal.
            // En el Load y en CargarTransaccionesEnDataGridView() la volveremos a cargar desde el archivo para asegurar consistencia.
            _transaccionesProcesadas = transaccionesProcesadas ?? new List<Transaccion>();

            this.Load += ViewTransactionsForm_Load;
            dgvTransacciones.SelectionChanged += dgvTransacciones_SelectionChanged;
            dgvTransacciones.RowPrePaint += dgvTransacciones_RowPrePaint;

            this.AutoSize = false;
            this.AutoSizeMode = AutoSizeMode.GrowOnly;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void ViewTransactionsForm_Load(object sender, EventArgs e)
        {
            ConfigurarDataGridViewColumns();
            // Al cargar el formulario, siempre recargamos las transacciones del archivo
            // para tener los datos más actualizados, incluyendo estados de cancelación.
            CargarTransaccionesEnDataGridView();
        }

        // En GUI\ViewTransactionsForm.cs

        private void ConfigurarDataGridViewColumns()
        {
            dgvTransacciones.Columns.Clear();
            dgvTransacciones.AutoGenerateColumns = false;
            dgvTransacciones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvTransacciones.Columns.Add(new DataGridViewTextBoxColumn() { Name = "IdTransaccion", HeaderText = "ID", DataPropertyName = "IdTransaccion", Width = 50 });

            // ¡Aquí está el cambio clave! Usa las propiedades proxy directamente.
            dgvTransacciones.Columns.Add(new DataGridViewTextBoxColumn() { Name = "NombreComprador", HeaderText = "Nombre del Comprador", DataPropertyName = "NombreComprador", Width = 150 });
            dgvTransacciones.Columns.Add(new DataGridViewTextBoxColumn() { Name = "ApellidoComprador", HeaderText = "Apellido del Comprador", DataPropertyName = "ApellidoComprador", Width = 150 });
            dgvTransacciones.Columns.Add(new DataGridViewTextBoxColumn() { Name = "DireccionComprador", HeaderText = "Dirección", DataPropertyName = "DireccionComprador", Width = 200 });
            dgvTransacciones.Columns.Add(new DataGridViewTextBoxColumn() { Name = "TelefonoComprador", HeaderText = "Teléfono", DataPropertyName = "TelefonoComprador", Width = 120 });
            dgvTransacciones.Columns.Add(new DataGridViewTextBoxColumn() { Name = "EmailComprador", HeaderText = "Correo", DataPropertyName = "EmailComprador", Width = 180 });
            dgvTransacciones.Columns.Add(new DataGridViewTextBoxColumn() { Name = "ZonaAsignada", HeaderText = "Zona", DataPropertyName = "ZonaAsignada", Width = 100 });
            dgvTransacciones.Columns.Add(new DataGridViewTextBoxColumn() { Name = "NumeroAsiento", HeaderText = "Asiento", DataPropertyName = "NumeroAsiento", Width = 70 });
            dgvTransacciones.Columns.Add(new DataGridViewTextBoxColumn() { Name = "TipoBoleto", HeaderText = "Tipo", DataPropertyName = "TipoBoleto", Width = 80 });
            // Si tienes beneficios VIP, también asegúrate de añadir esta columna y su proxy
            // dgvTransacciones.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Beneficios", HeaderText = "Beneficios VIP", DataPropertyName = "BeneficiosAdicionales", Width = 150 }); // Asegúrate de tener esta proxy en Transaccion

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
            // Este método se llama desde el formulario principal cuando se procesa una transacción.
            // Para mantener la consistencia, simplemente recargamos todo el DataGridView desde el archivo.
            CargarTransaccionesEnDataGridView();
        }

        public void CargarTransaccionesEnDataGridView()
        {
            // Siempre recarga la lista _transaccionesProcesadas desde el archivo para asegurar la consistencia.
            _transaccionesProcesadas = FileManager.CargarTransacciones();

            // Re-enlaza el DataSource para refrescar la visualización.
            dgvTransacciones.DataSource = null;
            dgvTransacciones.DataSource = _transaccionesProcesadas;
        }

        // Este evento se dispara cuando cambia la selección de fila en el DataGridView.
        // Su propósito principal es mostrar detalles y habilitar/deshabilitar el botón de cancelar.
        private void dgvTransacciones_SelectionChanged(object sender, EventArgs e)
        {
            // Inicialmente deshabilitamos el botón de cancelar y limpiamos los detalles.
            btnCancelar.Enabled = false;
            txtDetalleBoleto.Clear();
            pbQRCode.Image = null;

            if (dgvTransacciones.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvTransacciones.SelectedRows[0];
                Transaccion transaccion = row.DataBoundItem as Transaccion; // Obtén la transacción de la fila seleccionada

                if (transaccion != null)
                {
                    // Habilitar el botón de cancelar solo si la transacción está pendiente.
                    btnCancelar.Enabled = (transaccion.Estado == EstadoTransaccion.Pendiente);

                    // Mostrar detalles del boleto
                    txtDetalleBoleto.Text = transaccion.BoletoComprado.MostrarInformacion();

                    // Cargar imagen QR
                    string rutaQR = transaccion.BoletoComprado.CodigoQR;
                    if (!string.IsNullOrEmpty(rutaQR) && File.Exists(rutaQR))
                    {
                        try
                        {
                            // Usar FileStream con FileShare.ReadWrite para evitar problemas de archivo bloqueado
                            using (var fs = new FileStream(rutaQR, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                            {
                                pbQRCode.Image = Image.FromStream(fs);
                            }
                        }
                        catch (Exception ex)
                        {
                            pbQRCode.Image = null;
                            MessageBox.Show($"Error al cargar imagen QR: {ex.Message}", "Error QR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        // --- MÉTODO btnCancelar_Click - LÓGICA PRINCIPAL DE CANCELACIÓN ---
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Primero, verifica si hay alguna fila seleccionada.
            if (dgvTransacciones.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvTransacciones.SelectedRows[0];
                Transaccion transaccion = row.DataBoundItem as Transaccion; // Obtén la transacción de la fila seleccionada

                if (transaccion != null)
                {
                    // Validar que solo se puedan cancelar transacciones en estado "Pendiente".
                    if (transaccion.Estado == EstadoTransaccion.Pendiente)
                    {
                        DialogResult result = MessageBox.Show("¿Está seguro que desea cancelar este ticket?", "Confirmar cancelación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.Yes)
                        {
                            // 1. Cambiar el estado de la transacción a "Cancelada" en el objeto en memoria
                            //    y persistir este cambio en el archivo de transacciones.
                            //    Asumimos que tu método Transaccion.CancelarTransaccion() ya hace esto internamente:
                            //      - transaccion.Estado = EstadoTransaccion.Cancelada;
                            //      - transaccion.FechaProcesamiento = DateTime.Now;
                            //      - FileManager.ActualizarEstadoTransaccion(transaccion);
                            transaccion.CancelarTransaccion();

                            // 2. Liberar el asiento en el modelo del Estadio en memoria.
                            //    Se obtiene la zona y el asiento del boleto asociado a la transacción.
                            string zonaNombre = transaccion.BoletoComprado.Zona;
                            int asientoNumero = transaccion.BoletoComprado.Asiento;

                            // Llama al método LiberarAsiento de tu instancia de Estadio.
                            // Este método debe retornar true si el asiento se liberó con éxito.
                            _estadio.LiberarAsiento(zonaNombre, asientoNumero);
                            
                                // Si el asiento se liberó con éxito en el modelo del estadio,
                                // se guarda el estado actualizado del estadio en el archivo.
                                FileManager.GuardarEstadoEstadio(_estadio);
                                MessageBox.Show("Ticket cancelado con éxito y asiento liberado.", "Cancelación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            
                            

                            // 3. Recargar el DataGridView para reflejar todos los cambios (transacción cancelada y estado del estadio).
                            CargarTransaccionesEnDataGridView();
                            // También deshabilitar el botón de cancelar después de una cancelación exitosa
                            btnCancelar.Enabled = false;
                        }
                    }
                    else if (transaccion.Estado == EstadoTransaccion.Cancelada)
                    {
                        MessageBox.Show("Este ticket ya ha sido cancelado y su asiento liberado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (transaccion.Estado == EstadoTransaccion.Procesada)
                    {
                        MessageBox.Show("No se puede cancelar un ticket ya procesado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una fila para cancelar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarTransaccionesEnDataGridView();
            MessageBox.Show("Lista de transacciones actualizada.", "Actualización", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Este evento pinta las filas del DataGridView según el estado de la transacción.
        private void dgvTransacciones_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            // Asegúrate de que no estamos en el encabezado de la fila o en una fila nueva
            if (e.RowIndex >= 0 && e.RowIndex < dgvTransacciones.Rows.Count)
            {
                var row = dgvTransacciones.Rows[e.RowIndex];
                var transaccion = row.DataBoundItem as Transaccion;

                if (transaccion != null && transaccion.Estado == EstadoTransaccion.Cancelada)
                {
                    row.DefaultCellStyle.BackColor = Color.LightGray;
                    row.DefaultCellStyle.ForeColor = Color.DarkRed;
                }
                else
                {
                    // Restaura el estilo predeterminado para las filas no canceladas
                    // Esto es importante para que las filas no canceladas no hereden el color gris de una fila anterior.
                    row.DefaultCellStyle.BackColor = dgvTransacciones.DefaultCellStyle.BackColor;
                    row.DefaultCellStyle.ForeColor = dgvTransacciones.DefaultCellStyle.ForeColor;
                }
            }
        }
    }
}