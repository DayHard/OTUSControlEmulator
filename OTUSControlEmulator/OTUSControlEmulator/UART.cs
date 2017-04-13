using System;
using System.Windows.Forms;
using System.IO.Ports;

namespace OTUSControlEmulator
{
    // Делегад для события приема данных
    public delegate void DataResived();

    public class Uart
    {
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private SerialPort _port;

        // Обявление события, с сигнатурой делегата DataResive
        public event DataResived DataResived;

        #region  Property     
        public string PortName { get; set; }
        public int BaudRate { get; set; }
        public Parity Parity { get; set; }
        public StopBits StopBits { get; set; }
        public int DataBits { get; set; }
        #endregion
        public Uart()
        {
            _port = new SerialPort();
            _port.DataReceived += Port_DataReceived;
           
        }
        // Возвращает значение открыт ли SerialPort
        public bool IsOpen => _port.IsOpen;
        // Событие приема данных с COM
        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            DataResived?.Invoke();
        }

        //Открыть порт, с параметрами объявленных полей
        public bool Open()
        {
            // Проверить что будет при изменении настроек с открытым COM
            // Вроде работает =_=
            try
            {
                if (!_port.IsOpen)
                {
                    _port.PortName = PortName;
                    _port.BaudRate = BaudRate;
                    _port.Parity = Parity;
                    _port.StopBits = StopBits;
                    _port.DataBits = DataBits;
                    _port.Open();
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }
        // Закрыть порт
        public bool Close()
        {
            try
            {
                if (_port.IsOpen)
                {
                    _port.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        // Послать данные в паорт
        public void Send(object package)
        {
            try
            {
                var data = (Package)package;



                /*
                    int argumentLenght;
                    if (data.Argument == null)
                       argumentLenght = 0;
                    else argumentLenght = data.Argument.Length;
                 */

                var argumentLenght = data.Argument == null ? 0 : data.Argument.Length;

                byte[] buffer = new byte[7 + argumentLenght];

                buffer[0] = data.IdentityOfResiver;
                buffer[1] = data.IdentityOfTransmitter;
                buffer[2] = data.ClassCode;
                buffer[3] = data.MessageCode;

                int j = 0;
                for (int i = 4; i < argumentLenght + 4; i++)
                {
                    if (data.Argument != null) buffer[i] = data.Argument[j];
                    j++;
                }


                buffer[4 + argumentLenght] = data.CheckSumMsb;
                buffer[5 + argumentLenght] = data.CheckSumLsb;
                buffer[6 + argumentLenght] = data.Flag;

                if (_port.IsOpen)
                    _port.Write(buffer, 0, buffer.Length);
                else
                    MessageBox.Show(@"Port is not open. Сommand is not sent.", @"Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            catch (TimeoutException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        // НЕ РЕАЛИЗОВАН Прием данных
        public byte[] Resive()
        {
            byte[] buffer = new byte[_port.BytesToRead];
            _port.Read(buffer, 0, buffer.Length);
            return buffer;
        }
    }
}
