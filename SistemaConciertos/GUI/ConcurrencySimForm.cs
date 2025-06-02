using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL.Clases; // Para Transaccion, Boleto, EstadoTransaccion
using BLL.EstructuraDatos; // Para ColaDinamica
using System.Threading; // Para el Timer y el manejo de hilos (opcionalmente)

namespace GUI
{
    public partial class ConcurrencySimForm : Form
    {
        // Referencias a las colas y a la lista de transacciones procesadas
        private ColaDinamica<Transaccion> _colaTransaccionesNormal;
        private ColaDinamica<Transaccion> _colaTransaccionesVIP;
        private List<Transaccion> _transaccionesProcesadas; // Esta es la lista compartida de MainForm

        // Opcional: Un Timer para procesamiento automático (lo haremos manual por ahora)
        // private System.Windows.Forms.Timer _processingTimer;

        public ConcurrencySimForm(ColaDinamica<Transaccion> colaNormal, ColaDinamica<Transaccion> colaVIP, List<Transaccion> transaccionesProcesadas)
        {
            InitializeComponent();
            _colaTransaccionesNormal = colaNormal;
            _colaTransaccionesVIP = colaVIP;
            _transaccionesProcesadas = transaccionesProcesadas;

            // Configura el formulario al cargar
            this.Load += ConcurrencySimForm_Load;
        }

        private void ConcurrencySimForm_Load(object sender, EventArgs e)
        {
            // Inicializar la UI
            ActualizarContadoresColas();
            // Si quieres auto-procesamiento, aquí podrías iniciar el timer:
            // ConfigurarTimerProcesamiento();
        }

        // --- Métodos de Procesamiento de Transacciones ---

        // Método para procesar una sola transacción (manual)
        private void ProcesarSiguienteTransaccion()
        {
            Transaccion transaccionAProcesar = null;
            string tipoCola = "Normal"; // Para el mensaje

            // Priorizar la cola VIP si tiene elementos
            if (_colaTransaccionesVIP.Desencolar(out transaccionAProcesar))
            {
                tipoCola = "VIP";
            }
            // Si la cola VIP está vacía, intentar con la cola Normal
            else if (_colaTransaccionesNormal.Desencolar(out transaccionAProcesar))
            {
                tipoCola = "Normal";
            }

            if (transaccionAProcesar != null)
            {
                try
                {
                    // Llamar al método de procesamiento de la transacción
                    transaccionAProcesar.ProcesarTransaccion(); // Esto genera el QR y guarda en transactions.csv

                    // Añadir la transacción procesada a la lista compartida de MainForm
                    // (Esta lista es la misma referencia que en MainForm, por lo que se actualiza allí también)
                    _transaccionesProcesadas.Add(transaccionAProcesar);

                    MessageBox.Show($"Transacción {transaccionAProcesar.IdTransaccion} ({tipoCola}) de {transaccionAProcesar.BoletoComprado.NombreComprador} procesada exitosamente.", "Transacción Procesada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al procesar transacción {transaccionAProcesar.IdTransaccion}: {ex.Message}", "Error de Procesamiento", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No hay transacciones pendientes en las colas.", "Sin Transacciones", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            ActualizarContadoresColas(); // Actualizar la UI después de cada procesamiento
        }

        // Método para procesar todas las transacciones pendientes
        private void ProcesarTodasLasTransacciones()
        {
            int transaccionesProcesadasCount = 0;
            Transaccion transaccionAProcesar;

            // Procesar todas las VIP primero
            while (_colaTransaccionesVIP.Desencolar(out transaccionAProcesar))
            {
                try
                {
                    transaccionAProcesar.ProcesarTransaccion();
                    _transaccionesProcesadas.Add(transaccionAProcesar);
                    transaccionesProcesadasCount++;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al procesar transacción VIP {transaccionAProcesar.IdTransaccion}: {ex.Message}", "Error de Procesamiento", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            // Luego procesar todas las normales
            while (_colaTransaccionesNormal.Desencolar(out transaccionAProcesar))
            {
                try
                {
                    transaccionAProcesar.ProcesarTransaccion();
                    _transaccionesProcesadas.Add(transaccionAProcesar);
                    transaccionesProcesadasCount++;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al procesar transacción Normal {transaccionAProcesar.IdTransaccion}: {ex.Message}", "Error de Procesamiento", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            MessageBox.Show($"Se procesaron {transaccionesProcesadasCount} transacciones.", "Procesamiento Completado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ActualizarContadoresColas(); // Actualizar la UI al finalizar
        }


        // --- Métodos de Actualización de UI ---

        // Método para actualizar los labels que muestran la cantidad de transacciones en cola
        private void ActualizarContadoresColas()
        {
            lblNormalQueueCount.Text = $"Normal: {_colaTransaccionesNormal.Count}";
            lblVIPQueueCount.Text = $"VIP: {_colaTransaccionesVIP.Count}";
        }



        private void btnProcessOne_Click_1(object sender, EventArgs e)
        {
            ProcesarSiguienteTransaccion();
        }

        private void btnProcessAll_Click_1(object sender, EventArgs e)
        {
            ProcesarTodasLasTransacciones();
        }

        // --- Configuración Opcional para Auto-Procesamiento (si lo deseas en el futuro) ---
        /*
        private void ConfigurarTimerProcesamiento()
        {
            _processingTimer = new System.Windows.Forms.Timer();
            _processingTimer.Interval = 2000; // Procesar cada 2 segundos
            _processingTimer.Tick += ProcessingTimer_Tick;
            // No lo iniciamos automáticamente, un botón para iniciar/detener sería mejor
        }

        private void ProcessingTimer_Tick(object sender, EventArgs e)
        {
            // Este método se llamará automáticamente cada 'Interval' ms
            ProcesarSiguienteTransaccion(); // Procesa una a la vez
        }

        private void btnStartAutoProcess_Click(object sender, EventArgs e)
        {
            _processingTimer.Start();
            MessageBox.Show("Procesamiento automático iniciado.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnStopAutoProcess_Click(object sender, EventArgs e)
        {
            _processingTimer.Stop();
            MessageBox.Show("Procesamiento automático detenido.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        */
    }
}