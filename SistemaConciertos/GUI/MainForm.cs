using System; // Agrega este using si no lo tienes
using System.Collections.Generic; // Agrega este using si no lo tienes
using System.Linq; // Agrega este using para usar .Max() y .Any()
using System.Windows.Forms; // Ya deber�a estar
using System.Collections.Concurrent; // Para ConcurrentQueue
using BLL.EstructuraDatos; // Para ColaDinamica
using BLL.Clases; // Para Estadio, Transaccion, Boleto, Zona
using BLL.Utils; // Para FileManager

namespace GUI
{
    public partial class MainForm : Form
    {
        // Instancias centrales que ser�n accesibles desde otros formularios
        public Estadio Estadio { get; private set; }
        // Se recomienda usar ConcurrentQueue para hilos seguros, si no, mant�n ColaDinamica
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

        // --- NUEVO M�TODO: Carga los datos persistentes al iniciar la aplicaci�n ---
        private void CargarDatosAlInicio()
        {
            try
            {
                // 1. Cargar el estado del estadio primero
                // Esto restaurar� la disponibilidad de las zonas y los asientos ocupados.
                FileManager.CargarEstadoEstadio(Estadio);

                // 2. Cargar las transacciones hist�ricas procesadas
                // Estas transacciones ya se consideran "cerradas" o "completadas".
                List<Transaccion> transaccionesCargadas = FileManager.CargarTransacciones();

                if (transaccionesCargadas.Any())
                {
                    // Agrega las transacciones cargadas a tu lista de transacciones procesadas
                    TransaccionesProcesadas.AddRange(transaccionesCargadas);

                    // *** MUY IMPORTANTE: Actualizar el pr�ximo n�mero correlativo ***
                    // Esto asegura que los nuevos IDs de boletos/transacciones no se dupliquen con los ya guardados.
                    // Si tu Transaccion es la que lleva el ID principal:
                    // Necesitar�s un m�todo est�tico en Transaccion para establecer el pr�ximo ID.
                   
                    // Si Boleto.NumeroCorrelativo es el que garantiza unicidad a nivel de transacci�n:
                    // Boleto.SetProximoNumeroCorrelativo(transaccionesCargadas.Max(t => t.NumeroCorrelativo));
                }

                MessageBox.Show("Datos cargados exitosamente.", "Carga de Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}", "Error de Carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Aqu� podr�as loggear el error para depuraci�n
            }
        }

        // --- NUEVO M�TODO: Guarda los datos persistentes al cerrar la aplicaci�n ---
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // 1. Guardar el estado actual del estadio (disponibilidad de asientos)
                // Esto asegura que la pr�xima vez que inicie el programa, sepa cu�ntos boletos quedan.
                FileManager.GuardarEstadoEstadio(Estadio);

                // 2. Las transacciones individuales ya se guardan en el archivo de transacciones
                // cuando se procesan (probablemente en el m�todo ProcesarTransaccion de la clase Transaccion).
                // Por lo tanto, no es necesario iterar sobre TransaccionesProcesadas y volver a guardarlas aqu�,
                // a menos que tu l�gica de "guardar transacci�n" sea diferente y solo se haga al cerrar.
                // Si la l�gica de GuardarTransaccion ya est� bien, aqu� no hacemos nada con la lista.

                // Opcional: Si quieres guardar las transacciones que quedaron en cola (pendientes)
                // en un archivo separado para ser procesadas en la pr�xima ejecuci�n, lo har�as aqu�.
                // Por simplicidad, asumimos que las transacciones en cola no se persisten autom�ticamente
                // y se perder�n si no se procesan antes de cerrar.

                MessageBox.Show("Datos guardados exitosamente.", "Guardar Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar datos: {ex.Message}", "Error al Guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Aqu� podr�as loggear el error para depuraci�n
            }
            // Application.Exit() ya est� en menuItemSalir_Click.
            // Si quieres que la aplicaci�n se cierre sin preguntar despu�s de guardar,
            // no llames a Application.Exit() aqu�, deja que el sistema cierre el formulario.
        }


        // M�todo para cargar un formulario hijo en el panel contenedor
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
            pnlContenedor.Tag = formHijo; // Guarda una referencia al formulario actual (opcional, pero �til)

            formHijo.Show(); // Muestra el formulario hijo
            formHijo.BringToFront(); // Asegura que est� al frente
        }

        private void menuItemComprarBoleto_Click(object sender, EventArgs e)
        {
            // Pasa las instancias del Estadio y las colas al formulario de compra
            BuyTicketsForm buyForm = new BuyTicketsForm(Estadio, ColaTransacciones, ColaTransaccionesVIP);
            CargarFormularioEnPanel(buyForm);
        }

        private void menuItemVerTransacciones_Click(object sender, EventArgs e)
        {
            // Pasa las instancias del Estadio y las colas al formulario de visualizaci�n
            ViewTransactionsForm viewForm = new ViewTransactionsForm(Estadio, ColaTransacciones, ColaTransaccionesVIP, TransaccionesProcesadas);
            CargarFormularioEnPanel(viewForm);
        }

        private void menuItemSimulacion_Click(object sender, EventArgs e)
        {
            // Cierra el formulario anterior si est� abierto para evitar m�ltiples instancias
            if (_concurrencySimForm != null && !_concurrencySimForm.IsDisposed)
            {
                _concurrencySimForm.Close();
            }

            // Instancia el ConcurrencySimForm y le pasa las referencias
            _concurrencySimForm = new ConcurrencySimForm(ColaTransacciones, ColaTransaccionesVIP, TransaccionesProcesadas);

            // Si tienes un panel en MainForm para cargar formularios dentro:
            // CargarFormularioEnPanel(_concurrencySimForm); 

            // Si lo abres como una ventana separada (lo m�s com�n para este tipo de simulaci�n):
            _concurrencySimForm.Show();
        }

        private void menuItemSalir_Click(object sender, EventArgs e)
        {
            Application.Exit(); // Esto disparar� el evento FormClosing antes de salir.
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Puedes cargar el formulario de compra por defecto al iniciar
            menuItemComprarBoleto_Click(sender, e);
        }
    }
}