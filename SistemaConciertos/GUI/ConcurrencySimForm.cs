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
using System.Threading;
using BLL.Utils; 

namespace GUI
{
    public partial class ConcurrencySimForm : Form
    {
        
        private ColaDinamica<Transaccion> _colaTransaccionesNormal;
        private ColaDinamica<Transaccion> _colaTransaccionesVIP;
        private List<Transaccion> _transaccionesProcesadas; 

        

        public ConcurrencySimForm(ColaDinamica<Transaccion> colaNormal, ColaDinamica<Transaccion> colaVIP, List<Transaccion> transaccionesProcesadas)
        {
            InitializeComponent();
            _colaTransaccionesNormal = colaNormal;
            _colaTransaccionesVIP = colaVIP;
            _transaccionesProcesadas = transaccionesProcesadas;

            
            this.Load += ConcurrencySimForm_Load;
        }

        private void ConcurrencySimForm_Load(object sender, EventArgs e)
        {
            
            ActualizarContadoresColas();
           
        }

      

        
        private async Task ProcesarSiguienteTransaccion()
        {
            Transaccion transaccionAProcesar = null;
            string tipoCola = "Normal";

            if (_colaTransaccionesVIP.Desencolar(out transaccionAProcesar))
            {
                tipoCola = "VIP";
            }
            else if (_colaTransaccionesNormal.Desencolar(out transaccionAProcesar))
            {
                tipoCola = "Normal";
            }

            if (transaccionAProcesar != null)
            {
                try
                {
                    await transaccionAProcesar.ProcesarTransaccion();
                    _transaccionesProcesadas.Add(transaccionAProcesar);

                    // ⬇️ Enviar correo después de procesar
                    await EmailSender.SendTicketConfirmationWithQrEmailAsync(new List<Transaccion> { transaccionAProcesar });

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

            ActualizarContadoresColas();
        }

        // Método para procesar todas las transacciones pendientes
        private async Task ProcesarTodasLasTransacciones()
        {
            int transaccionesProcesadasCount = 0;
            List<Transaccion> transaccionesAEnviar = new();
            Transaccion transaccionAProcesar;

            while (_colaTransaccionesVIP.Desencolar(out transaccionAProcesar))
            {
                try
                {
                    await transaccionAProcesar.ProcesarTransaccion();
                    _transaccionesProcesadas.Add(transaccionAProcesar);
                    transaccionesAEnviar.Add(transaccionAProcesar);
                    transaccionesProcesadasCount++;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al procesar transacción VIP {transaccionAProcesar.IdTransaccion}: {ex.Message}", "Error de Procesamiento", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            while (_colaTransaccionesNormal.Desencolar(out transaccionAProcesar))
            {
                try
                {
                    await transaccionAProcesar.ProcesarTransaccion();
                    _transaccionesProcesadas.Add(transaccionAProcesar);
                    transaccionesAEnviar.Add(transaccionAProcesar);
                    transaccionesProcesadasCount++;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al procesar transacción Normal {transaccionAProcesar.IdTransaccion}: {ex.Message}", "Error de Procesamiento", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            // ⬇️ Enviar el correo con todos los boletos procesados
            if (transaccionesAEnviar.Count > 0)
            {
                await EmailSender.SendTicketConfirmationWithQrEmailAsync(transaccionesAEnviar);
            }

            MessageBox.Show($"Se procesaron {transaccionesProcesadasCount} transacciones.", "Procesamiento Completado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ActualizarContadoresColas();
        }


        
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

        
    }
}