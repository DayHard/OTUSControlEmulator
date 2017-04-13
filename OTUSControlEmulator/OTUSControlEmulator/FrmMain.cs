using System;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.Xml;
using System.Xml.Linq;
// ReSharper disable PossibleNullReferenceException
// ReSharper disable AssignNullToNotNullAttribute

namespace OTUSControlEmulator
{
    // Структура посылки (протокол Otus)
    public struct Package
    {
        public byte IdentityOfResiver;
        public byte IdentityOfTransmitter;
        public byte ClassCode;
        public byte MessageCode;
        public byte[] Argument;
        public byte CheckSumMsb;
        public byte CheckSumLsb;
        public byte Flag;
    }

    public partial class FrmMain : Form
    {
        // Объявление ссылок на экземпляры классов
        private FrmUartProperties frmUart;
        private Package _package;
        private Crc16Ccitt crc;
        public Uart Uart;
        // ReSharper disable once NotAccessedField.Local
        byte[] _data;
        public FrmMain()
        {
            InitializeComponent();

            //Инициализация экземпляров классов
            frmUart = new FrmUartProperties();
            Uart = new Uart();
            _package = new Package();
            crc = new Crc16Ccitt(InitialCrcValue.NonZero1);

            frmUart.FrmMain = this;
            frmUart.Uart = Uart;
            //Подпись на событие приема данных
            Uart.DataResived += Uart_dataResived;

        }
        // Проверка наличия файла конфигурации COM
        private void FrmMain_Load(object sender, EventArgs e)
        {
            if (File.Exists("config.xml"))
            {
                LoadConfig();
             }
            else
            {
                frmUart.ShowDialog();
            }

        }
        // Сохранение файла конфигурации COM, закрытие COM если открыт
        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Uart.Close();
            SaveConfig();
        }
        //Обработчик события приема данных
        private void Uart_dataResived()
        {
            MessageBox.Show(@"Данные получены!");
            if (Uart != null) _data = Uart.Resive();
        }
        // Загрузка парсинг файла конфигурации Uart
        public void LoadConfig()
        {
            XDocument xDoc = XDocument.Load("config.xml");

            foreach (XElement uartElement in xDoc.Element("xml")?.Elements("UART"))
            {
                XElement portNameElement = uartElement.Element("PortName");
                XElement baudRateElement = uartElement.Element("BaudRate");
                XElement parityElement = uartElement.Element("Parity");
                XElement stopBitsElement = uartElement.Element("StopBits");
                XElement dataBitsElement = uartElement.Element("DataBits");

                Uart.PortName = portNameElement?.Value;
                Uart.BaudRate = int.Parse(baudRateElement?.Value);
                Uart.Parity = (Parity)Enum.Parse(typeof(Parity), parityElement?.Value);
                Uart.StopBits = (StopBits)Enum.Parse(typeof(StopBits), stopBitsElement?.Value);
                Uart.DataBits = int.Parse(dataBitsElement?.Value);
            }

            // Установка комбо боксов FrmProperties на значения из файла config
            frmUart.SetConfigProperties(Uart.PortName, Uart.BaudRate, Uart.Parity, Uart.StopBits, Uart.DataBits);

        }
        // Сохранение файла конфигурации Uart
        public void SaveConfig()
        {
            XmlTextWriter writer = new XmlTextWriter("config.xml", Encoding.UTF8);
            writer.WriteStartDocument();
            writer.WriteStartElement("xml");
            writer.WriteEndElement();
            writer.Close();

            XmlDocument doc = new XmlDocument();
            doc.Load("config.xml");
            XmlNode element = doc.CreateElement("UART");
            doc.DocumentElement?.AppendChild(element);

            XmlNode subElement1 = doc.CreateElement("PortName"); // даём имя
            subElement1.InnerText = Uart.PortName; // и значение
            element.AppendChild(subElement1); // и указываем кому принадлежит

            XmlNode subElement2 = doc.CreateElement("BaudRate"); // даём имя
            subElement2.InnerText = Uart.BaudRate.ToString(); // и значение
            element.AppendChild(subElement2); // и указываем кому принадлежит

            XmlNode subElement3 = doc.CreateElement("Parity"); // даём имя
            subElement3.InnerText = Uart.Parity.ToString(); // и значение
            element.AppendChild(subElement3); // и указываем кому принадлежит

            XmlNode subElement4 = doc.CreateElement("StopBits"); // даём имя
            subElement4.InnerText = Uart.StopBits.ToString(); // и значение
            element.AppendChild(subElement4); // и указываем кому принадлежит

            XmlNode subElement5 = doc.CreateElement("DataBits"); // даём имя
            subElement5.InnerText = Uart.DataBits.ToString(); // и значение
            element.AppendChild(subElement5); // и указываем кому принадлежит

            doc.Save("config.xml");
        }
        // Кнопка настройки свойств Uart
        private void btnUARTProp_Click(object sender, EventArgs e)
        {
            frmUart.ShowDialog();
        }
        // Подключение/Отключение от Uart
        private void btnOpenUART_Click(object sender, EventArgs e)
        {
            if (!Uart.IsOpen)
            {
                if (!Uart.Open())
                    frmUart.ShowDialog();
                else
                    btnOpenUART.Text = @"Disconnect";
            }
            else
            {
                if (!Uart.Close())
                    MessageBox.Show(@"Port is not closed.", @"Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                btnOpenUART.Text = @"Connect";
            }

        }
        // Отправка тестовой последовательности в COM 
        private void btnTestSend_Click(object sender, EventArgs e)
        {
            _package.IdentityOfResiver = 0x31;
            _package.IdentityOfTransmitter = 0x32;
            _package.ClassCode = 0x33;
            _package.MessageCode = 0x34;
            _package.Argument = new byte[] { 0x35, 0x36, 0x37, 0x38, 0x39 };

            //package.Argument = null;  
            //package.Argument = new byte[] 
            //{ 0xAA, 0xA1, 0xA2, 0xA3, 0xA4, 0xA5, 0xA6, 0xA7, 0xA8, 0xA9,
            // 0xBB, 0xB1, 0xB2, 0xB3, 0xB4, 0xB5, 0xB6, 0xB7, 0xB8, 0xB9};

            //Формирование массива для расчета контрольной суммы
            byte[] buffer = crc.CreateCheckSumBuffer(_package);
            //Расчет контрольной суммы
            var checkSum = crc.ComputeChecksumBytes(buffer);

            _package.CheckSumMsb = checkSum[1];
            _package.CheckSumLsb = checkSum[0];
            _package.Flag = 0x7E;

            // Отправка данных по UART
            Uart.Send(_package);
        }
    }
}
