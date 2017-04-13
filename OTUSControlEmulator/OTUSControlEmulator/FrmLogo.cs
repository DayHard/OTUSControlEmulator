using System;
using System.Windows.Forms;

namespace OTUSControlEmulator
{
    public partial class FrmLogo : Form
    {
        private int _clock;
        public FrmLogo()
        {
            InitializeComponent();
        }

        private void FrmLogo_Load(object sender, EventArgs e)
        {
            labVersion.Text = @"ver: " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
        }

        private void timerLogo_Tick(object sender, EventArgs e)
        {
            if (_clock == 4)
            {
                Close();
                Dispose();
            }
            else
                _clock++;
        }

        private void timerOpacity_Tick(object sender, EventArgs e)
        {
            Opacity += .01;
        }

    }
}
