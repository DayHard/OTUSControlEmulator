using System;
using System.Windows.Forms;
using System.IO.Ports;
// ReSharper disable CoVariantArrayConversion

namespace OTUSControlEmulator
{
    public partial class FrmUartProperties : Form
    {
        public FrmMain FrmMain;
        public Uart Uart;

        public FrmUartProperties()
        {
            InitializeComponent();
        }

        private void FrmUART_Load(object sender, EventArgs e)
        {
            TopMost = true; 

        }

        private void FrmUART_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        // Применение настроеr Uart
        // ПОСМОТРЕТЬ  насчет опенпорт в конце
        private void btnApply_Click(object sender, EventArgs e)
        {
            if (Uart.IsOpen)
                if(!Uart.Close())
                    MessageBox.Show(@"Port is not closed.", @"Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);

            Uart.PortName = cbPortName.Text;
            Uart.BaudRate = int.Parse(cbBaudRate.Text);
            Uart.Parity = (Parity)Enum.Parse(typeof(Parity), cbParity.Text);
            Uart.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cbStopBits.Text);
            Uart.DataBits = int.Parse(cbDataBits.Text);
            MessageBox.Show(@"Properties saved.",@"Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        // Настройка SelectedIndex согласно файлу config
        public void SetConfigProperties(string portName, int baudRate, Parity parity, StopBits stopBits, int dataBits)
        {
            // Получение списка всех доступных SerialPort (Com)
            cbPortName.Items.AddRange(SerialPort.GetPortNames());

           // получение доступным паритетов SerialPort
            Array parities = Enum.GetValues(typeof(Parity));
            foreach (Parity p in parities) cbParity.Items.Add(p);

            // Проверка, существует ли в списке параментров, параметр находящийся в файле config
            // при отсутсвии данного параметра (значение -1), значение устанавливается по умолчанию

            cbPortName.SelectedIndex = cbPortName.FindString(portName) != -1 
                ? cbPortName.FindString(portName) 
                : 0;

            cbBaudRate.SelectedIndex = cbBaudRate.FindString(baudRate.ToString()) != -1
                ? cbBaudRate.FindString(baudRate.ToString())
                : 3;

            cbBaudRate.SelectedIndex = cbBaudRate.FindString(baudRate.ToString()) != -1 
                ? cbBaudRate.FindString(baudRate.ToString()) 
                : 3;

            cbParity.SelectedIndex = cbParity.FindString(parity.ToString()) != -1 
                ? cbParity.FindString(parity.ToString()) 
                : 2;

            cbStopBits.SelectedIndex = cbStopBits.FindString(stopBits.ToString()) != -1 
                ? cbStopBits.FindString(stopBits.ToString()) 
                : 0;

            cbDataBits.SelectedIndex = cbDataBits.FindString(dataBits.ToString()) != -1 
                ? cbDataBits.FindString(dataBits.ToString()) 
                : 3;

            //if (cbDataBits.FindString(dataBits.ToString()) != -1)
            //    cbDataBits.SelectedIndex = cbDataBits.FindString(dataBits.ToString());
            //else cbDataBits.SelectedIndex = 3;

        }
    }
}
