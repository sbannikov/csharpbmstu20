using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.ServiceModel;

namespace BotControlPanel
{
    /// <summary>
    /// Главная форма
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Клиент управляющего сервиса
        /// </summary>
        private ControlReference.ControlServiceClient client;

        private UdpClient udp;

        private System.Collections.Concurrent.ConcurrentQueue<string> queue;

        /// <summary>
        /// Конструктор
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            client = new ControlReference.ControlServiceClient();
            udp = new UdpClient(9999);
            udp.BeginReceive(new AsyncCallback(OnUdpUpdate), udp);
            queue = new System.Collections.Concurrent.ConcurrentQueue<string>();
        }

        /// <summary>
        /// Асинхронный прием сообщений по UDP
        /// </summary>
        /// <param name="result"></param>
        private void OnUdpUpdate(IAsyncResult result)
        {
            UdpClient socket = result.AsyncState as UdpClient;
            System.Net.IPEndPoint source = null;
            byte[] message = socket.EndReceive(result, ref source);
            string s = Encoding.UTF8.GetString(message);
            queue.Enqueue(s);
            udp.BeginReceive(new AsyncCallback(OnUdpUpdate), udp);
        }

        /// <summary>
        /// Обновление состояния кнопок
        /// </summary>
        private void UpdateButtons()
        {
            startButton.Enabled = true;
            stopButton.Enabled = true;

            // Обновление состояния контроллера службы
            service.Refresh();

            // Установка состояния кнопок в зависимости от состояния сервиса
            switch (service.Status)
            {
                case System.ServiceProcess.ServiceControllerStatus.Running:
                    startButton.Enabled = false;
                    break;

                case System.ServiceProcess.ServiceControllerStatus.Stopped:
                    stopButton.Enabled = false;
                    break;

                default:
                    startButton.Enabled = false;
                    stopButton.Enabled = false;
                    break;
            }
        }

        /// <summary>
        /// Запуск сервиса
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startButton_Click(object sender, EventArgs e)
        {
            try
            {
                service.Start();
                UpdateButtons();
                list.Items.Add("Сервис запущен");
            }
            catch (Exception ex)
            {
                Message(ex);
            }
        }

        /// <summary>
        /// Останов сервиса
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stopButton_Click(object sender, EventArgs e)
        {
            try
            {
                service.Stop();
                UpdateButtons();
                list.Items.Add("Сервис остановлен");
            }
            catch (Exception ex)
            {
                Message(ex);
            }
        }

        /// <summary>
        /// Загрузка формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                timer.Interval = Properties.Settings.Default.TimerIntervalInMilliseconds;
                timer.Start();
            }
            catch (Exception ex)
            {
                Message(ex);
            }
        }

        /// <summary>
        /// Такт таймера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Tick(object sender, EventArgs e)
        {
            try
            {
                timer.Stop();
                UpdateButtons();

                string s;
                while (queue.TryDequeue(out s))
                {
                    list.Items.Add(s);
                }
            }
            catch (Exception ex)
            {
                Message(ex);
            }
            finally
            {
                timer.Start();
            }
        }

        /// <summary>
        /// Протоколирование ошибки
        /// </summary>
        /// <param name="ex">Исключение</param>
        private void Message(Exception ex)
        {
            MessageBox.Show(ex.Message, "Панель управления", MessageBoxButtons.OK, MessageBoxIcon.Error);
            list.Items.Add(ex.Message);
        }

        /// <summary>
        /// Запрос состояния сервиса
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void queryButton_Click(object sender, EventArgs e)
        {
            try
            {
                string s = client.Query();
                list.Items.Add(s);
            }
            catch (Exception ex)
            {
                Message(ex);
            }
        }

        private void startTraceButton_Click(object sender, EventArgs e)
        {
            try
            {
                client.StartTrace("127.0.0.1");
                list.Items.Add("Трассировка включена");
            }
            catch (Exception ex)
            {
                Message(ex);
            }
        }

        private void stopTraceButton_Click(object sender, EventArgs e)
        {
            try
            {
                client.StopTrace();
                list.Items.Add("Трассировка выключена");
            }
            catch (Exception ex)
            {
                Message(ex);
            }
        }
    }
}
