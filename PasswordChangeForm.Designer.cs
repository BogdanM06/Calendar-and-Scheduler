namespace Scheduler_NEA
{
    partial class PasswordChangeForm
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
            label1 = new Label();
            label2 = new Label();
            PasswordTextBox = new TextBox();
            CPasswordTextBox = new TextBox();
            SubmitButton = new Button();
            Show = new CheckBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F);
            label1.Location = new Point(48, 31);
            label1.Name = "label1";
            label1.Size = new Size(97, 28);
            label1.TabIndex = 0;
            label1.Text = "Password:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.Location = new Point(48, 91);
            label2.Name = "label2";
            label2.Size = new Size(172, 28);
            label2.TabIndex = 1;
            label2.Text = "Confirm Password:";
            // 
            // PasswordTextBox
            // 
            PasswordTextBox.Location = new Point(225, 35);
            PasswordTextBox.Name = "PasswordTextBox";
            PasswordTextBox.Size = new Size(563, 27);
            PasswordTextBox.TabIndex = 2;
            PasswordTextBox.UseSystemPasswordChar = true;
            // 
            // CPasswordTextBox
            // 
            CPasswordTextBox.Location = new Point(225, 95);
            CPasswordTextBox.Name = "CPasswordTextBox";
            CPasswordTextBox.Size = new Size(563, 27);
            CPasswordTextBox.TabIndex = 3;
            CPasswordTextBox.UseSystemPasswordChar = true;
            // 
            // SubmitButton
            // 
            SubmitButton.Location = new Point(357, 168);
            SubmitButton.Name = "SubmitButton";
            SubmitButton.Size = new Size(84, 30);
            SubmitButton.TabIndex = 4;
            SubmitButton.Text = "Submit";
            SubmitButton.UseVisualStyleBackColor = true;
            SubmitButton.Click += SubmitButton_Click;
            // 
            // Show
            // 
            Show.AutoSize = true;
            Show.Location = new Point(58, 134);
            Show.Name = "Show";
            Show.Size = new Size(67, 24);
            Show.TabIndex = 5;
            Show.Text = "Show";
            Show.UseVisualStyleBackColor = true;
            Show.CheckedChanged += Show_CheckedChanged;
            // 
            // PasswordChangeForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 201);
            Controls.Add(Show);
            Controls.Add(SubmitButton);
            Controls.Add(CPasswordTextBox);
            Controls.Add(PasswordTextBox);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "PasswordChangeForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox PasswordTextBox;
        private TextBox CPasswordTextBox;
        private Button SubmitButton;
        private CheckBox Show;
    }
}