namespace Scheduler_NEA
{
    partial class ToastMessage
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
            components = new System.ComponentModel.Container();
            toastBorder = new Panel();
            ToastMessageText = new Label();
            UpTimer = new System.Windows.Forms.Timer(components);
            DownTimer = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // toastBorder
            // 
            toastBorder.BackColor = SystemColors.ActiveCaptionText;
            toastBorder.ForeColor = SystemColors.ButtonFace;
            toastBorder.Location = new Point(0, 0);
            toastBorder.Name = "toastBorder";
            toastBorder.Size = new Size(20, 59);
            toastBorder.TabIndex = 0;
            toastBorder.Paint += panel1_Paint;
            // 
            // ToastMessageText
            // 
            ToastMessageText.Font = new Font("Verdana", 9F);
            ToastMessageText.Location = new Point(26, 0);
            ToastMessageText.Name = "ToastMessageText";
            ToastMessageText.Size = new Size(270, 59);
            ToastMessageText.TabIndex = 1;
            ToastMessageText.Text = "Type";
            ToastMessageText.Click += ToastMessageText_Click;
            // 
            // UpTimer
            // 
            UpTimer.Enabled = true;
            UpTimer.Interval = 10;
            UpTimer.Tick += UpTimer_Tick;
            // 
            // DownTimer
            // 
            DownTimer.Interval = 10;
            DownTimer.Tick += DownTimer_Tick;
            // 
            // ToastMessage
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLightLight;
            ClientSize = new Size(306, 59);
            Controls.Add(ToastMessageText);
            Controls.Add(toastBorder);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ToastMessage";
            Text = "ToastMessage";
            Load += ToastMessage_Load;
            ResumeLayout(false);
        }

        #endregion

        private Panel toastBorder;
        private Label ToastMessageText;
        private System.Windows.Forms.Timer UpTimer;
        private System.Windows.Forms.Timer DownTimer;
    }
}