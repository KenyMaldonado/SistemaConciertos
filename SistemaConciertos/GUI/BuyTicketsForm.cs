using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL.Clases;
using BLL.EstructuraDatos;
using BLL.Utils;

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
                cmbZonas.SelectedIndex = 0;
            }
        }

        private void ActualizarUIInicial()
        {
            lblAsientoAsignado.Text = "N/A";
            btnComprarBoleto.Enabled = false;
        }

        private void cmbZonas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbZonas.SelectedItem != null)
            {
                string zonaSeleccionada = cmbZonas.SelectedItem.ToString();
                DibujarMapaDeAsientos(zonaSeleccionada);
                Zona zonaObj = _estadio.ObtenerZonaPorNombre(zonaSeleccionada);

                int disponibilidad = _estadio.VerificarDisponibilidadZona(zonaSeleccionada);
                lblDisponibilidad.Text = $"{disponibilidad} boletos";

                btnComprarBoleto.Enabled = disponibilidad > 0;
            }
            else
            {
                lblDisponibilidad.Text = "0 boletos";
                btnComprarBoleto.Enabled = false;
            }
        }

        private async void btnComprarBoleto_Click(object sender, EventArgs e)
        {
            string nombreComprador = txtNombreComprador.Text.Trim();
            string apellidoComprador = txtApellidoComprador.Text.Trim();
            string direccionComprador = txtDireccion.Text.Trim();
            string telefono = txtTelefono.Text.Trim();
            string correo = txtCorreo.Text.Trim();
            int cantidadBoletos = (int)numCantidadBoletos.Value;

            // Validaciones básicas
            if (string.IsNullOrEmpty(nombreComprador))
            {
                MessageBox.Show("Por favor, ingrese el nombre del comprador.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(apellidoComprador))
            {
                MessageBox.Show("Por favor, ingrese el apellido del comprador.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(direccionComprador))
            {
                MessageBox.Show("Por favor, ingrese la dirección del comprador.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!EsTelefonoValido(telefono))
            {
                MessageBox.Show("Por favor, ingrese un número de teléfono válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!EsCorreoValido(correo))
            {
                MessageBox.Show("Por favor, ingrese un correo electrónico válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cantidadBoletos < 1)
            {
                MessageBox.Show("Debe seleccionar al menos un boleto para comprar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbZonas.SelectedItem == null)
            {
                MessageBox.Show("Por favor, seleccione una zona.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string zonaSeleccionada = cmbZonas.SelectedItem.ToString();
            Zona zonaObj = _estadio.ObtenerZonaPorNombre(zonaSeleccionada);
            if (zonaObj == null)
            {
                MessageBox.Show("Error: La zona seleccionada no se encontró en el estadio.", "Error Interno", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int disponibilidad = _estadio.VerificarDisponibilidadZona(zonaSeleccionada);
            if (cantidadBoletos > disponibilidad)
            {
                MessageBox.Show($"Solo hay {disponibilidad} boletos disponibles en esta zona. Por favor, reduzca la cantidad.", "Boletos insuficientes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool esVIP = zonaObj.EsVIP;
            List<Transaccion> transaccionesCompradas = new List<Transaccion>();
            List<int> asientosAsignados = new List<int>();

            for (int i = 0; i < cantidadBoletos; i++)
            {
                int asientoAsignado = _estadio.ObtenerAsientoDisponible(zonaSeleccionada);
                if (asientoAsignado == -1) break;

                string prefijo = esVIP ? "VIP" : "GEN";
                string correlativo = GeneradorCorrelativo.ObtenerSiguiente(prefijo);

                Transaccion nuevaTransaccion = Transaccion.CrearNuevaTransaccion(
                    correlativo,
                    zonaSeleccionada,
                    asientoAsignado,
                    nombreComprador,
                    apellidoComprador,
                    direccionComprador,
                    telefono,
                    correo,
                    DateTime.Now,
                    "",  // Código QR se puede generar después
                    esVIP
                );

                if (esVIP)
                    _colaTransaccionesVIP.Encolar(nuevaTransaccion);
                else
                    _colaTransaccionesNormal.Encolar(nuevaTransaccion);

                transaccionesCompradas.Add(nuevaTransaccion);
                asientosAsignados.Add(asientoAsignado);
            }

            if (asientosAsignados.Count > 0)
            {
                lblAsientoAsignado.Text = string.Join(", ", asientosAsignados);
                string tipo = esVIP ? "VIP" : "General";
                MessageBox.Show(
                    $"{asientosAsignados.Count} boletos tipo {tipo} encolados para {nombreComprador}.\nAsientos: {string.Join(", ", asientosAsignados)}",
                    "Compra Exitosa",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // Intentar enviar email con tickets
                bool emailEnviado = await EmailSender.SendOrderNotificationEmailAsync(transaccionesCompradas);

                if (emailEnviado)
                {
                    MessageBox.Show("Los boletos se enviaron por correo electrónico correctamente.", "Correo enviado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se pudo enviar el correo con los boletos. Por favor, verifica tu conexión o los datos del correo.", "Error al enviar correo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No se pudo asignar ningún asiento.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            cmbZonas_SelectedIndexChanged(null, null); // Actualizar disponibilidad

            // Limpiar formulario
            txtNombreComprador.Clear();
            txtApellidoComprador.Clear();
            txtDireccion.Clear();
            txtTelefono.Clear();
            txtCorreo.Clear();
            numCantidadBoletos.Value = 1;
        }

        // Métodos de validación

        private bool EsCorreoValido(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Regex básico para validar email
                string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
            }
            catch
            {
                return false;
            }
        }

        private bool EsTelefonoValido(string telefono)
        {
            if (string.IsNullOrWhiteSpace(telefono))
                return false;

            // Validar que tenga solo números, espacio, + o -
            string pattern = @"^[\d\s\+\-]{7,15}$";
            return Regex.IsMatch(telefono, pattern);
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

            int maxFilas = 10;
            int filas = Math.Min(maxFilas, totalAsientos);
            int columnas = (int)Math.Ceiling((double)totalAsientos / filas);

            int margin = 5;
            int btnWidth = (panelWidth - (margin * (columnas + 1))) / columnas;
            int btnHeight = (panelHeight - (margin * (filas + 1))) / filas;

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
