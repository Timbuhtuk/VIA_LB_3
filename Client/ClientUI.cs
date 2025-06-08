using Client;

namespace Server
{
    public partial class ClientUI : Form
    {


        private IClient client = new TCPAsyncClient();
        private bool BootRequired = true;

        public ClientUI()
        {
            InitializeComponent();
        }

        private async void Send_Click(object sender, EventArgs e)
        {

        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Send_Click(sender, e);
                textBox2.Focus();
            }
        }

        private async void TCPmode_CheckedChanged(object sender, EventArgs e)
        {
            if (TCPmode.Checked)
            {
                //if (client != null) client.Stop();
                client = new TCPAsyncClient();
                BootRequired = true;
                textBox2.Text = "swapped to TCP";
            }

        }

        private async void UDPmode_CheckedChanged(object sender, EventArgs e)
        {
            if (UDPmode.Checked)
            {
                //if (client != null) client.Stop();
                client = new UDPAsyncClient();
                BootRequired = true;
                textBox2.Text = "swapped to UDP";
            }
        }
    }
}
