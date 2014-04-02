using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using AwesomeRedis.API;
using AwesomeRedis.API.Connect;
using AwesomeRedis.API.Reader;

namespace AwesomeRedis.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private RedisConnectionHandler _connection;
        private readonly BackgroundWorker _worker;

        public MainWindow()
        {
            InitializeComponent();
            _worker = new BackgroundWorker();
            _worker.DoWork += ConnectToInstance;
        }

        private void ConnectToInstance(object sender, DoWorkEventArgs e)
        {
            var parameters = (AsyncWorkerParams) e.Argument;

            _connection = new RedisConnectionHandler(parameters.IpAddress, parameters.PortAddress);
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            progressRing.IsActive = true;
            var parameters = new AsyncWorkerParams() { IpAddress = ipTextBox.Text, PortAddress = Int32.Parse(portTextBox.Text) };

            try
            {
                _worker.RunWorkerAsync(parameters);
                LogToTextBox("Connected");
            }
            catch (Exception exception)
            {
                LogToTextBox(exception.Message);
            }
            progressRing.IsActive = false;
        }

        private void DisconnectButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _connection.Close();
                LogToTextBox("Disconnected");
            }
            catch (Exception exception)
            {
                LogToTextBox(exception.Message);
            }
        }

        private void CommandButton_Click(object sender, RoutedEventArgs e)
        {
            var responseParsers = new IResponseReader[] {new BulkResponseReader(), new IntegerResponseReader(), new MultiBulkResponseReader(), new StatusCodeResponseReader()};

            var messageSender = new CommandSender(_connection);
            var responseGetter = new ResponseGetter(_connection, responseParsers);

            var genericCommand = new GenericCommand(messageSender, responseGetter);

            try
            {
                var responses = genericCommand.ExecuteCommand(commandBox.Text);
                foreach (var response in responses)
                {
                    LogToTextBox(response);
                }
            }
            catch (Exception exception)
            {
                LogToTextBox(exception.Message);
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            try
            {
                _connection.Close();
            } catch(NullReferenceException)
            {
                //Do nothing if user closes w/o creating connection
            }
        }

        private void Enter_Handler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                submitButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
        }

        private void LogToTextBox(string message)
        {
            responseBlock.Text += String.Format("{0}{1}", message, Environment.NewLine);
        }

        private struct AsyncWorkerParams
        {
            public string IpAddress { get; set; }

            public int PortAddress { get; set; }
        }
    }
}
