namespace Scheduler_NEA
{
    partial class SignUp
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
            panel1 = new Panel();
            SignInSign = new Label();
            panel2 = new Panel();
            Show = new CheckBox();
            passwordRequirements = new Label();
            label1 = new Label();
            ConfirmPassword = new TextBox();
            BackButton = new Button();
            PText = new Label();
            NewPassword = new TextBox();
            UText = new Label();
            SignUpButton = new Button();
            NewUsername = new TextBox();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ActiveCaption;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(SignInSign);
            panel1.Controls.Add(panel2);
            panel1.Location = new Point(194, 110);
            panel1.Name = "panel1";
            panel1.Size = new Size(1554, 772);
            panel1.TabIndex = 0;
            // 
            // SignInSign
            // 
            SignInSign.AutoSize = true;
            SignInSign.Font = new Font("Kristen ITC", 40.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            SignInSign.Location = new Point(657, 40);
            SignInSign.Name = "SignInSign";
            SignInSign.Size = new Size(277, 92);
            SignInSign.TabIndex = 1;
            SignInSign.Text = "Sign-up";
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.GradientInactiveCaption;
            panel2.Controls.Add(Show);
            panel2.Controls.Add(passwordRequirements);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(ConfirmPassword);
            panel2.Controls.Add(BackButton);
            panel2.Controls.Add(PText);
            panel2.Controls.Add(NewPassword);
            panel2.Controls.Add(UText);
            panel2.Controls.Add(SignUpButton);
            panel2.Controls.Add(NewUsername);
            panel2.Location = new Point(395, 150);
            panel2.Name = "panel2";
            panel2.RightToLeft = RightToLeft.No;
            panel2.Size = new Size(802, 580);
            panel2.TabIndex = 0;
            // 
            // Show
            // 
            Show.AutoSize = true;
            Show.Location = new Point(23, 449);
            Show.Name = "Show";
            Show.Size = new Size(67, 24);
            Show.TabIndex = 16;
            Show.Text = "Show";
            Show.UseVisualStyleBackColor = true;
            Show.CheckedChanged += Show_CheckedChanged;
            // 
            // passwordRequirements
            // 
            passwordRequirements.ForeColor = Color.Red;
            passwordRequirements.Location = new Point(23, 298);
            passwordRequirements.Name = "passwordRequirements";
            passwordRequirements.Size = new Size(779, 25);
            passwordRequirements.TabIndex = 15;
            passwordRequirements.Text = "Password must be at least ten letters long and contain: a capital letter, a special character and a number.";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.ActiveCaption;
            label1.Font = new Font("Segoe UI", 20F);
            label1.ForeColor = SystemColors.ActiveCaptionText;
            label1.ImageAlign = ContentAlignment.TopCenter;
            label1.Location = new Point(23, 328);
            label1.Name = "label1";
            label1.Size = new Size(296, 46);
            label1.TabIndex = 14;
            label1.Text = "Confirm Password:";
            // 
            // ConfirmPassword
            // 
            ConfirmPassword.Font = new Font("Segoe UI", 20F);
            ConfirmPassword.Location = new Point(23, 391);
            ConfirmPassword.Name = "ConfirmPassword";
            ConfirmPassword.Size = new Size(440, 52);
            ConfirmPassword.TabIndex = 13;
            ConfirmPassword.UseSystemPasswordChar = true;
            // 
            // BackButton
            // 
            BackButton.ForeColor = SystemColors.HotTrack;
            BackButton.Location = new Point(369, 501);
            BackButton.Name = "BackButton";
            BackButton.Size = new Size(94, 29);
            BackButton.TabIndex = 12;
            BackButton.Text = "Back";
            BackButton.UseVisualStyleBackColor = true;
            BackButton.Click += BackButton_Click;
            // 
            // PText
            // 
            PText.AutoSize = true;
            PText.BackColor = SystemColors.ActiveCaption;
            PText.Font = new Font("Segoe UI", 20F);
            PText.ForeColor = SystemColors.ActiveCaptionText;
            PText.ImageAlign = ContentAlignment.TopCenter;
            PText.Location = new Point(23, 180);
            PText.Name = "PText";
            PText.Size = new Size(167, 46);
            PText.TabIndex = 11;
            PText.Text = "Password:";
            // 
            // NewPassword
            // 
            NewPassword.Font = new Font("Segoe UI", 20F);
            NewPassword.Location = new Point(23, 243);
            NewPassword.Name = "NewPassword";
            NewPassword.Size = new Size(440, 52);
            NewPassword.TabIndex = 10;
            NewPassword.UseSystemPasswordChar = true;
            // 
            // UText
            // 
            UText.AutoSize = true;
            UText.BackColor = SystemColors.ActiveCaption;
            UText.Font = new Font("Segoe UI", 20F);
            UText.ForeColor = SystemColors.ActiveCaptionText;
            UText.ImageAlign = ContentAlignment.TopCenter;
            UText.Location = new Point(23, 25);
            UText.Name = "UText";
            UText.Size = new Size(177, 46);
            UText.TabIndex = 9;
            UText.Text = "Username:";
            // 
            // SignUpButton
            // 
            SignUpButton.ForeColor = SystemColors.ActiveCaptionText;
            SignUpButton.Location = new Point(369, 466);
            SignUpButton.Name = "SignUpButton";
            SignUpButton.Size = new Size(94, 29);
            SignUpButton.TabIndex = 8;
            SignUpButton.Text = "Sign-up";
            SignUpButton.UseVisualStyleBackColor = true;
            SignUpButton.Click += SignUpButton_Click;
            // 
            // NewUsername
            // 
            NewUsername.Font = new Font("Segoe UI", 20F);
            NewUsername.Location = new Point(23, 89);
            NewUsername.Name = "NewUsername";
            NewUsername.Size = new Size(440, 52);
            NewUsername.TabIndex = 7;
            // 
            // SignUp
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.AlbedoBase_XL_A_mesmerizing_abstract_composition_featuring_a_v_0;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1924, 1055);
            Controls.Add(panel1);
            Name = "SignUp";
            Text = "SignUp";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private Button BackButton;
        private Label PText;
        private TextBox NewPassword;
        private Label UText;
        private Button SignUpButton;
        private TextBox NewUsername;
        private Label label1;
        private TextBox ConfirmPassword;
        private Label SignInSign;
        private Label passwordRequirements;
        private CheckBox Show;
    }
}