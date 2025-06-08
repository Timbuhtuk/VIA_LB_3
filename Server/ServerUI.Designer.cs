namespace Client
{
    partial class ServerUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Start = new Button();
            TCPmode = new RadioButton();
            UDPmode = new RadioButton();
            Stop = new Button();
            Reboot = new Button();
            listBox1 = new ListBox();
            panel1 = new Panel();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // Start
            // 
            Start.Location = new Point(10, 12);
            Start.Name = "Start";
            Start.Size = new Size(75, 23);
            Start.TabIndex = 0;
            Start.Text = "START";
            Start.UseVisualStyleBackColor = true;
            Start.Click += Start_Click;
            // 
            // TCPmode
            // 
            TCPmode.AutoSize = true;
            TCPmode.Checked = true;
            TCPmode.Location = new Point(258, 16);
            TCPmode.Name = "TCPmode";
            TCPmode.Size = new Size(46, 19);
            TCPmode.TabIndex = 1;
            TCPmode.TabStop = true;
            TCPmode.Text = "TCP";
            TCPmode.UseVisualStyleBackColor = true;
            TCPmode.CheckedChanged += TCPmode_CheckedChanged;
            // 
            // UDPmode
            // 
            UDPmode.AutoSize = true;
            UDPmode.Location = new Point(310, 14);
            UDPmode.Name = "UDPmode";
            UDPmode.Size = new Size(48, 19);
            UDPmode.TabIndex = 2;
            UDPmode.Text = "UDP";
            UDPmode.UseVisualStyleBackColor = true;
            // 
            // Stop
            // 
            Stop.Enabled = false;
            Stop.Location = new Point(91, 12);
            Stop.Name = "Stop";
            Stop.Size = new Size(75, 23);
            Stop.TabIndex = 3;
            Stop.Text = "STOP";
            Stop.UseVisualStyleBackColor = true;
            Stop.Click += Stop_Click;
            // 
            // Reboot
            // 
            Reboot.Enabled = false;
            Reboot.Location = new Point(172, 12);
            Reboot.Name = "Reboot";
            Reboot.Size = new Size(75, 23);
            Reboot.TabIndex = 4;
            Reboot.Text = "REBOOT";
            Reboot.UseVisualStyleBackColor = true;
            Reboot.Click += Reboot_Click;
            // 
            // listBox1
            // 
            listBox1.Dock = DockStyle.Fill;
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(0, 0);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(367, 450);
            listBox1.TabIndex = 5;
            // 
            // panel1
            // 
            panel1.Controls.Add(Reboot);
            panel1.Controls.Add(Start);
            panel1.Controls.Add(TCPmode);
            panel1.Controls.Add(Stop);
            panel1.Controls.Add(UDPmode);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 400);
            panel1.Name = "panel1";
            panel1.Size = new Size(367, 50);
            panel1.TabIndex = 6;
            // 
            // ServerUI
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(367, 450);
            Controls.Add(panel1);
            Controls.Add(listBox1);
            Name = "ServerUI";
            Text = "UI";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button Start;
        private RadioButton TCPmode;
        private RadioButton UDPmode;
        private Button Stop;
        private Button Reboot;
        private ListBox listBox1;
        private Panel panel1;
    }
}