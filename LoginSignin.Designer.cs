namespace Scheduler_NEA
{
    partial class LoginSignin
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
            BackgroundSegment = new Panel();
            panel1 = new Panel();
            SignUpLinkButton = new Button();
            PText = new Label();
            PasswordInput = new TextBox();
            UText = new Label();
            LoginButton = new Button();
            UsernameInput = new TextBox();
            label1 = new Label();
            Show = new CheckBox();
            BackgroundSegment.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // BackgroundSegment
            // 
            BackgroundSegment.BackColor = SystemColors.ActiveCaption;
            BackgroundSegment.BorderStyle = BorderStyle.Fixed3D;
            BackgroundSegment.Controls.Add(panel1);
            BackgroundSegment.Controls.Add(label1);
            BackgroundSegment.ForeColor = SystemColors.AppWorkspace;
            BackgroundSegment.Location = new Point(194, 110);
            BackgroundSegment.Name = "BackgroundSegment";
            BackgroundSegment.Size = new Size(1554, 772);
            BackgroundSegment.TabIndex = 0;
            BackgroundSegment.Paint += BackgroundSegment_Paint;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.GradientInactiveCaption;
            panel1.Controls.Add(Show);
            panel1.Controls.Add(SignUpLinkButton);
            panel1.Controls.Add(PText);
            panel1.Controls.Add(PasswordInput);
            panel1.Controls.Add(UText);
            panel1.Controls.Add(LoginButton);
            panel1.Controls.Add(UsernameInput);
            panel1.Location = new Point(395, 150);
            panel1.Name = "panel1";
            panel1.Size = new Size(802, 580);
            panel1.TabIndex = 1;
            panel1.Paint += panel1_Paint;
            // 
            // SignUpLinkButton
            // 
            SignUpLinkButton.ForeColor = SystemColors.HotTrack;
            SignUpLinkButton.Location = new Point(362, 382);
            SignUpLinkButton.Name = "SignUpLinkButton";
            SignUpLinkButton.Size = new Size(94, 29);
            SignUpLinkButton.TabIndex = 6;
            SignUpLinkButton.Text = "Sign-up";
            SignUpLinkButton.UseVisualStyleBackColor = true;
            SignUpLinkButton.Click += SignUpButton_Click;
            // 
            // PText
            // 
            PText.AutoSize = true;
            PText.BackColor = SystemColors.ActiveCaption;
            PText.Font = new Font("Segoe UI", 20F);
            PText.ForeColor = SystemColors.ActiveCaptionText;
            PText.ImageAlign = ContentAlignment.TopCenter;
            PText.Location = new Point(16, 171);
            PText.Name = "PText";
            PText.Size = new Size(167, 46);
            PText.TabIndex = 4;
            PText.Text = "Password:";
            PText.Click += label2_Click;
            // 
            // PasswordInput
            // 
            PasswordInput.Font = new Font("Segoe UI", 20F);
            PasswordInput.Location = new Point(16, 234);
            PasswordInput.Name = "PasswordInput";
            PasswordInput.Size = new Size(440, 52);
            PasswordInput.TabIndex = 3;
            PasswordInput.UseSystemPasswordChar = true;
            // 
            // UText
            // 
            UText.AutoSize = true;
            UText.BackColor = SystemColors.ActiveCaption;
            UText.Font = new Font("Segoe UI", 20F);
            UText.ForeColor = SystemColors.ActiveCaptionText;
            UText.ImageAlign = ContentAlignment.TopCenter;
            UText.Location = new Point(16, 16);
            UText.Name = "UText";
            UText.Size = new Size(177, 46);
            UText.TabIndex = 2;
            UText.Text = "Username:";
            // 
            // LoginButton
            // 
            LoginButton.ForeColor = SystemColors.ActiveCaptionText;
            LoginButton.Location = new Point(362, 347);
            LoginButton.Name = "LoginButton";
            LoginButton.Size = new Size(94, 29);
            LoginButton.TabIndex = 1;
            LoginButton.Text = "Login";
            LoginButton.UseVisualStyleBackColor = true;
            LoginButton.Click += LoginButton_Click;
            // 
            // UsernameInput
            // 
            UsernameInput.Font = new Font("Segoe UI", 20F);
            UsernameInput.Location = new Point(16, 80);
            UsernameInput.Name = "UsernameInput";
            UsernameInput.Size = new Size(440, 52);
            UsernameInput.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Kristen ITC", 40.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ActiveCaptionText;
            label1.Location = new Point(395, 44);
            label1.Name = "label1";
            label1.Size = new Size(802, 92);
            label1.TabIndex = 0;
            label1.Text = "Welcome to FriendSync";
            // 
            // Show
            // 
            Show.AutoSize = true;
            Show.ForeColor = SystemColors.ActiveCaptionText;
            Show.Location = new Point(16, 292);
            Show.Name = "Show";
            Show.Size = new Size(67, 24);
            Show.TabIndex = 7;
            Show.Text = "Show";
            Show.UseVisualStyleBackColor = true;
            Show.CheckedChanged += Show_CheckedChanged;
            // 
            // LoginSignin
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.AlbedoBase_XL_A_mesmerizing_abstract_composition_featuring_a_v_0;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1924, 1055);
            Controls.Add(BackgroundSegment);
            Name = "LoginSignin";
            Text = "LoginSignin";
            BackgroundSegment.ResumeLayout(false);
            BackgroundSegment.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel BackgroundSegment;
        private Label label1;
        private Panel panel1;
        private Button LoginButton;
        private TextBox UsernameInput;
        private Label UText;
        private Label PText;
        private TextBox PasswordInput;
        private Button SignUpLinkButton;
        private CheckBox Show;
    }
}