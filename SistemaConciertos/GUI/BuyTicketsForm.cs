using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL.Clases;
using BLL.EstructuraDatos;

namespace GUI
{
    public partial class BuyTicketsForm : Form
    {
        private Estadio _estadio;
        private ColaDinamica<Transaccion> _colaTransaccionesNormal;
        private ColaDinamica<Transaccion> _colaTransaccionesVIP;

        public BuyTicketsForm(Estadio estadio, ColaDinamica<Transaccion> colaNormal, ColaDinamica<Transaccion> colaVIP)
        {
            InitializeComponent();
            _estadio = estadio;
            _colaTransaccionesNormal = colaNormal;
            _colaTransaccionesVIP = colaVIP;

            CargarZonasEnComboBox();
            ActualizarUIInicial();
        }

        private void CargarZonasEnComboBox()
        {
            cmbZonas.Items.Clear();
            foreach (string zonaNombre in _estadio.ObtenerNombresZonas())
            {
                cmbZonas.Items.Add(zonaNombre);
            }
            if (cmbZonas.Items.Count > 0)
            {
                cmbZonas.SelectedIndex = 0; // Selecciona la primera zona por defecto
            }
        }

        private void ActualizarUIInicial()
        {
            lblAsientoAsignado.Text = "N/A";
            // Deshabilitar botón de compra hasta que se seleccione una zona y haya disponibilidad
            btnComprarBoleto.Enabled = false;
            // *** ELIMINAR O OCULTAR chkEsVIP del diseñador ***
            // chkEsVIP.Visible = false; // Puedes hacer esto si no lo borras del diseñador
        }

        private void cmbZonas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbZonas.SelectedItem != null)
            {
                string zonaSeleccionada = cmbZonas.SelectedItem.ToString();
                DibujarMapaDeAsientos(zonaSeleccionada);
                Zona zonaObj = _estadio.ObtenerZonaPorNombre(zonaSeleccionada); // Obtener el objeto Zona

                int disponibilidad = _estadio.VerificarDisponibilidadZona(zonaSeleccionada);
                lblDisponibilidad.Text = $"{disponibilidad} boletos";

                // Habilitar el botón de compra si hay disponibilidad
                btnComprarBoleto.Enabled = disponibilidad > 0;

                // Puedes añadir aquí un label para mostrar si la zona es VIP o Normal
                // Ejemplo: if (zonaObj != null) lblTipoZona.Text = zonaObj.EsVIP ? "Tipo: VIP" : "Tipo: Normal";
            }
            else
            {
                lblDisponibilidad.Text = "0 boletos";
                btnComprarBoleto.Enabled = false;
                // Ejemplo: lblTipoZona.Text = "Tipo: N/A";
            }
        }

        private void btnComprarBoleto_Click(object sender, EventArgs e)
        {
            string nombreComprador = txtNombreComprador.Text.Trim();
            if (string.IsNullOrEmpty(nombreComprador))
            {
                MessageBox.Show("Por favor, ingrese el nombre del comprador.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbZonas.SelectedItem == null)
            {
                MessageBox.Show("Por favor, seleccione una zona.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string zonaSeleccionada = cmbZonas.SelectedItem.ToString();
            Zona zonaObj = _estadio.ObtenerZonaPorNombre(zonaSeleccionada);
            bool esVIP = false;
            if (zonaObj == null)
            {
                MessageBox.Show("Error: La zona seleccionada no se encontró en el estadio.", "Error Interno", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            esVIP = zonaObj.EsVIP; // Determinamos si es VIP basado en la zona


            // Paso 1: Intentar asignar un asiento
            int asientoAsignado = _estadio.ObtenerAsientoDisponible(zonaSeleccionada);

            if (asientoAsignado == -1)
            {
                MessageBox.Show($"Lo sentimos, no hay asientos disponibles en la zona {zonaSeleccionada}.", "Sin Boletos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            lblAsientoAsignado.Text = asientoAsignado.ToString();

            // Paso 2: Crear la transacción y encolarla
            Transaccion nuevaTransaccion = Transaccion.CrearNuevaTransaccion(nombreComprador, zonaSeleccionada, asientoAsignado, esVIP);

            if (esVIP) // Se encola en la cola VIP si la ZONA es VIP
            {
                // *** CAMBIO AQUÍ: Ahora se usa Encolar() en lugar de EncolarConPrioridad() ***
                _colaTransaccionesVIP.Encolar(nuevaTransaccion);
                MessageBox.Show($"Transacción VIP para {nombreComprador} encolada para procesamiento en zona '{zonaSeleccionada}'. Asiento: {asientoAsignado}", "Compra Encolada VIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else // Se encola en la cola Normal si la ZONA NO es VIP
            {
                _colaTransaccionesNormal.Encolar(nuevaTransaccion);
                MessageBox.Show($"Transacción para {nombreComprador} encolada para procesamiento en zona '{zonaSeleccionada}'. Asiento: {asientoAsignado}", "Compra Encolada", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // Actualizar la disponibilidad mostrada
            cmbZonas_SelectedIndexChanged(null, null); // Forzar actualización de disponibilidad
            txtNombreComprador.Clear(); // Limpiar campo de nombre
        }

        private void DibujarMapaDeAsientos(string zonaSeleccionada)
        {
            pnlAsientos.Controls.Clear();

            Zona zona = _estadio.ObtenerZonaPorNombre(zonaSeleccionada);
            if (zona == null) return;

            int totalAsientos = zona.CapacidadMaxima;

            int panelWidth = pnlAsientos.Width;
            int panelHeight = pnlAsientos.Height;

            pnlAsientos.AutoScroll = true;

            // Máximo filas permitidas para mejor visual
            int maxFilas = 10;

            // Calculamos filas y columnas de forma dinámica
            int filas = Math.Min(maxFilas, totalAsientos);
            int columnas = (int)Math.Ceiling((double)totalAsientos / filas);

            // Margen entre botones
            int margin = 5;

            // Calculamos tamaño del botón en función del panel y filas/columnas
            int btnWidth = (panelWidth - (margin * (columnas + 1))) / columnas;
            int btnHeight = (panelHeight - (margin * (filas + 1))) / filas;

            // Limitamos tamaño mínimo y máximo
            btnWidth = Math.Max(30, Math.Min(50, btnWidth));
            btnHeight = Math.Max(30, Math.Min(50, btnHeight));

            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    int numeroAsiento = i * columnas + j + 1;
                    if (numeroAsiento > totalAsientos)
                        break;

                    Button btnAsiento = new Button
                    {
                        Width = btnWidth,
                        Height = btnHeight,
                        Left = margin + j * (btnWidth + margin),
                        Top = margin + i * (btnHeight + margin),
                        Text = numeroAsiento.ToString(),
                        Font = new Font("Segoe UI", 10, FontStyle.Bold),
                        TextAlign = ContentAlignment.MiddleCenter,
                        UseCompatibleTextRendering = true,
                        Enabled = false,
                        FlatStyle = FlatStyle.Flat,
                        TabStop = false
                    };

                    // Diferenciar VIP o no
                    if (zona.EsVIP)
                    {
                        btnAsiento.FlatAppearance.BorderColor = Color.SteelBlue;
                        btnAsiento.FlatAppearance.BorderSize = 1;

                        if (zona.ObtenerAsientosOcupados().Contains(numeroAsiento))
                        {
                            btnAsiento.BackColor = Color.LightCoral;
                            btnAsiento.ForeColor = Color.DarkRed;
                        }
                        else
                        {
                            btnAsiento.BackColor = Color.LightSkyBlue;
                            btnAsiento.ForeColor = Color.DarkBlue;
                        }
                    }
                    else
                    {
                        btnAsiento.FlatAppearance.BorderColor = Color.DarkGreen;
                        btnAsiento.FlatAppearance.BorderSize = 1;

                        if (zona.ObtenerAsientosOcupados().Contains(numeroAsiento))
                        {
                            btnAsiento.BackColor = Color.IndianRed;
                            btnAsiento.ForeColor = Color.DarkRed;
                        }
                        else
                        {
                            btnAsiento.BackColor = Color.LightGreen;
                            btnAsiento.ForeColor = Color.DarkGreen;
                        }
                    }

                    pnlAsientos.Controls.Add(btnAsiento);
                }
            }
        }

    }
}