using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Windows.Forms; 
using System.Collections.Concurrent; 
using BLL.EstructuraDatos; 
using BLL.Clases; 
using BLL.Utils; 

namespace GUI
{
    public partial class MainForm : Form
    {
        
        public Estadio Estadio { get; private set; }
        
        public ColaDinamica<Transaccion> ColaTransacciones { get; private set; }
        public ColaDinamica<Transaccion> ColaTransaccionesVIP { get; private set; } // Para prioridades VIP
        public List<Transaccion> TransaccionesProcesadas { get; private set; }
        private ConcurrencySimForm _concurrencySimForm;
        public MainForm()
        {
            InitializeComponent();
            Estadio = new Estadio(); // Inicializa tu estadio con sus zonas
            ColaTransacciones = new ColaDinamica<Transaccion>();
            ColaTransaccionesVIP = new ColaDinamica<Transaccion>(); // Cola para transacciones VIP
            TransaccionesProcesadas = new List<Transaccion>();

            // Configurar el formulario al iniciar
            this.Text = "Sistema de Venta de Boletos de Concierto";
            this.WindowState = FormWindowState.Normal; // Muestra la ventana normal
            this.StartPosition = FormStartPosition.CenterScreen; // Centra la ventana

            // --- PASO CLAVE 1: Cargar datos al inicio del programa ---
            CargarDatosAlInicio();

            // --- PASO CLAVE 2: Suscribirse al evento FormClosing para guardar datos al cerrar ---
            this.FormClosing += MainForm_FormClosing;
        }

        // --- NUEVO MÉTODO: Carga los datos persistentes al iniciar la aplicación ---
        private void CargarDatosAlInicio()
        {
            try
            {
                // 1. Cargar el estado del estadio primero
                // Esto restaurará la disponibilidad de las zonas y los asientos ocupados.
                FileManager.CargarEstadoEstadio(Estadio);

                // 2. Cargar las transacciones históricas procesadas
                // Estas transacciones ya se consideran "cerradas" o "completadas".
                List<Transaccion> transaccionesCargadas = FileManager.CargarTransacciones();

                if (transaccionesCargadas.Any())
                {
                    
                    TransaccionesProcesadas.AddRange(transaccionesCargadas);

                    
                }

                MessageBox.Show("Datos cargados exitosamente.", "Carga de Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}", "Error de Carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
        }

        // --- NUEVO MÉTODO: Guarda los datos persistentes al cerrar la aplicación ---
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                
                FileManager.GuardarEstadoEstadio(Estadio);

                
                MessageBox.Show("Datos guardados exitosamente.", "Guardar Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar datos: {ex.Message}", "Error al Guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Aquí podrías loggear el error para depuración
            }
            
        }


        // Método para cargar un formulario hijo en el panel contenedor
        private void CargarFormularioEnPanel(Form formHijo)
        {
            // Cierra cualquier formulario existente en el panel
            if (pnlContenedor.Controls.Count > 0)
            {
                pnlContenedor.Controls.RemoveAt(0);
            }

            formHijo.TopLevel = false; // Importante: indica que no es un formulario de nivel superior
            formHijo.FormBorderStyle = FormBorderStyle.None; // Elimina el borde del formulario hijo
            formHijo.Dock = DockStyle.Fill; // Hace que el formulario hijo llene el panel

            pnlContenedor.Controls.Add(formHijo); // Agrega el formulario hijo al panel
            pnlContenedor.Tag = formHijo; // Guarda una referencia al formulario actual (opcional, pero útil)

            formHijo.Show(); // Muestra el formulario hijo
            formHijo.BringToFront(); // Asegura que esté al frente
        }

        private void menuItemComprarBoleto_Click(object sender, EventArgs e)
        {
            // Pasa las instancias del Estadio y las colas al formulario de compra
            BuyTicketsForm buyForm = new BuyTicketsForm(Estadio, ColaTransacciones, ColaTransaccionesVIP);
            CargarFormularioEnPanel(buyForm);
        }

        private void menuItemVerTransacciones_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            // Pasa las instancias del Estadio y las colas al formulario de visualización
            ViewTransactionsForm viewForm = new ViewTransactionsForm(Estadio, ColaTransacciones, ColaTransaccionesVIP, TransaccionesProcesadas);
            CargarFormularioEnPanel(viewForm);
        }

        private void menuItemSimulacion_Click(object sender, EventArgs e)
        {
            // Cierra el formulario anterior si está abierto para evitar múltiples instancias
            if (_concurrencySimForm != null && !_concurrencySimForm.IsDisposed)
            {
                _concurrencySimForm.Close();
            }

            // Instancia el ConcurrencySimForm y le pasa las referencias
            _concurrencySimForm = new ConcurrencySimForm(ColaTransacciones, ColaTransaccionesVIP, TransaccionesProcesadas);

            
            _concurrencySimForm.Show();
        }

        private void menuItemSalir_Click(object sender, EventArgs e)
        {
            Application.Exit(); // Esto disparará el evento FormClosing antes de salir.
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Puedes cargar el formulario de compra por defecto al iniciar
            menuItemComprarBoleto_Click(sender, e);
        }
    }
}