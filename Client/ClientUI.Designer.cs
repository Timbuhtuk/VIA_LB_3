namespace Server
{
    partial class ClientUI
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
            Send = new Button();
            TCPmode = new RadioButton();
            UDPmode = new RadioButton();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            panel1 = new Panel();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // Send
            // 
            Send.Location = new Point(11, 46);
            Send.Name = "Send";
            Send.Size = new Size(75, 23);
            Send.TabIndex = 0;
            Send.Text = "SEND";
            Send.UseVisualStyleBackColor = true;
            Send.Click += Send_Click;
            // 
            // TCPmode
            // 
            TCPmode.AutoSize = true;
            TCPmode.Checked = true;
            TCPmode.Location = new Point(259, 50);
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
            UDPmode.Location = new Point(311, 48);
            UDPmode.Name = "UDPmode";
            UDPmode.Size = new Size(48, 19);
            UDPmode.TabIndex = 2;
            UDPmode.Text = "UDP";
            UDPmode.UseVisualStyleBackColor = true;
            UDPmode.CheckedChanged += UDPmode_CheckedChanged;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(11, 13);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(343, 23);
            textBox1.TabIndex = 6;
            textBox1.KeyDown += textBox1_KeyDown;
            // 
            // textBox2
            // 
            textBox2.Dock = DockStyle.Fill;
            textBox2.Location = new Point(0, 0);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(367, 450);
            textBox2.TabIndex = 7;
            // 
            // panel1
            // 
            panel1.Controls.Add(textBox1);
            panel1.Controls.Add(Send);
            panel1.Controls.Add(TCPmode);
            panel1.Controls.Add(UDPmode);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 375);
            panel1.Name = "panel1";
            panel1.Size = new Size(367, 75);
            panel1.TabIndex = 8;
            // 
            // ClientUI
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(367, 450);
            Controls.Add(panel1);
            Controls.Add(textBox2);
            Name = "ClientUI";
            Text = "UI";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button Send;
        private RadioButton TCPmode;
        private RadioButton UDPmode;
        private TextBox textBox1;
        private TextBox textBox2;
        private Panel panel1;
    }
}