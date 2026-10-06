namespace Scheduler_NEA
{
    partial class InvitationMessage
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DescBox = new Label();
            panel1 = new Panel();
            RejectButton = new Button();
            AcceptButton = new Button();
            Status = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // DescBox
            // 
            DescBox.BorderStyle = BorderStyle.FixedSingle;
            DescBox.Location = new Point(153, -1);
            DescBox.Name = "DescBox";
            DescBox.Size = new Size(264, 102);
            DescBox.TabIndex = 0;
            DescBox.Text = "label1";
            DescBox.Click += label1_Click;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ControlLightLight;
            panel1.Controls.Add(RejectButton);
            panel1.Controls.Add(AcceptButton);
            panel1.Location = new Point(416, -1);
            panel1.Name = "panel1";
            panel1.Size = new Size(100, 102);
            panel1.TabIndex = 1;
            // 
            // RejectButton
            // 
            RejectButton.BackColor = Color.Red;
            RejectButton.Location = new Point(55, 28);
            RejectButton.Name = "RejectButton";
            RejectButton.Size = new Size(42, 42);
            RejectButton.TabIndex = 1;
            RejectButton.Text = "X";
            RejectButton.UseVisualStyleBackColor = false;
            RejectButton.Click += RejectButton_Click;
            // 
            // AcceptButton
            // 
            AcceptButton.BackColor = Color.Lime;
            AcceptButton.Location = new Point(7, 28);
            AcceptButton.Name = "AcceptButton";
            AcceptButton.Size = new Size(42, 42);
            AcceptButton.TabIndex = 0;
            AcceptButton.Text = "✓";
            AcceptButton.UseVisualStyleBackColor = false;
            AcceptButton.Click += AcceptButton_Click;
            // 
            // Status
            // 
            Status.Location = new Point(-1, -1);
            Status.Name = "Status";
            Status.Size = new Size(148, 102);
            Status.TabIndex = 2;
            Status.Text = "label1";
            // 
            // InvitationMessage
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(Status);
            Controls.Add(panel1);
            Controls.Add(DescBox);
            Name = "InvitationMessage";
            Size = new Size(515, 100);
            Load += InvitationMessage_Load;
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Label DescBox;
        private Panel panel1;
        private Button RejectButton;
        private Button AcceptButton;
        private Label Status;
    }
}
