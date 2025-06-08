namespace Client
{
    public partial class ServerUI : Form
    {
        public IServer server;
        public ServerUI()
        {
            InitializeComponent();
            UDPmode.Checked = true;
            TCPmode.Checked = false;
        }

        private void Start_Click(object sender, EventArgs e)
        {
            if (TCPmode.Checked) server = new TCPAsyncServer();
            else server = new UDPAsyncServer();
            server.ServerLogEvent += Print;
            server.Boot();
            Start.Enabled = false;
            Stop.Enabled = true;
            Reboot.Enabled = true;
            TCPmode.Enabled = false;
            UDPmode.Enabled = false;
        }

        private void Print(object? sender, LogEventArgs e)
        {
            listBox1.Items.Add(e.Message);
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            server.Close();
            Start.Enabled = true;
            Stop.Enabled = false;
            Reboot.Enabled = false;
            TCPmode.Enabled = true;
            UDPmode.Enabled = true;
            listBox1.Items.Add("\nServer closed.");
        }

        private void Reboot_Click(object sender, EventArgs e)
        {
            Stop_Click(sender, e);
            Start_Click(sender, e);
        }

        private void TCPmode_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
