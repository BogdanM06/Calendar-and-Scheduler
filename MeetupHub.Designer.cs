namespace Scheduler_NEA
{
    partial class MeetupHub
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
            FriendSelectCheckBox = new CheckedListBox();
            label2 = new Label();
            ChooseTimeBox = new ComboBox();
            label3 = new Label();
            SendInv = new Button();
            FindFreeTimeButton = new Button();
            AvailableTimeBox = new Label();
            InvitationOutputBox = new FlowLayoutPanel();
            label4 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 12F);
            label1.Location = new Point(12, 31);
            label1.Name = "label1";
            label1.Size = new Size(251, 61);
            label1.TabIndex = 0;
            label1.Text = "Select the friends you would like to meet up with:";
            // 
            // FriendSelectCheckBox
            // 
            FriendSelectCheckBox.FormattingEnabled = true;
            FriendSelectCheckBox.Location = new Point(12, 95);
            FriendSelectCheckBox.Name = "FriendSelectCheckBox";
            FriendSelectCheckBox.Size = new Size(448, 246);
            FriendSelectCheckBox.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.Location = new Point(12, 409);
            label2.Name = "label2";
            label2.Size = new Size(252, 28);
            label2.TabIndex = 3;
            label2.Text = "Available times to meet-up:";
            // 
            // ChooseTimeBox
            // 
            ChooseTimeBox.DropDownStyle = ComboBoxStyle.DropDownList;
            ChooseTimeBox.FormattingEnabled = true;
            ChooseTimeBox.Location = new Point(695, 150);
            ChooseTimeBox.Name = "ChooseTimeBox";
            ChooseTimeBox.Size = new Size(221, 28);
            ChooseTimeBox.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = SystemColors.ButtonHighlight;
            label3.Font = new Font("Segoe UI", 12F);
            label3.Location = new Point(562, 150);
            label3.Name = "label3";
            label3.Size = new Size(112, 28);
            label3.TabIndex = 5;
            label3.Text = "Select time:";
            // 
            // SendInv
            // 
            SendInv.Location = new Point(664, 216);
            SendInv.Name = "SendInv";
            SendInv.Size = new Size(131, 43);
            SendInv.TabIndex = 6;
            SendInv.Text = "Send invite";
            SendInv.UseVisualStyleBackColor = true;
            SendInv.Click += SendInv_Click;
            // 
            // FindFreeTimeButton
            // 
            FindFreeTimeButton.Location = new Point(170, 347);
            FindFreeTimeButton.Name = "FindFreeTimeButton";
            FindFreeTimeButton.Size = new Size(117, 42);
            FindFreeTimeButton.TabIndex = 7;
            FindFreeTimeButton.Text = "Find free time";
            FindFreeTimeButton.UseVisualStyleBackColor = true;
            FindFreeTimeButton.Click += FindFreeTimeButton_Click;
            // 
            // AvailableTimeBox
            // 
            AvailableTimeBox.BackColor = SystemColors.ButtonHighlight;
            AvailableTimeBox.BorderStyle = BorderStyle.FixedSingle;
            AvailableTimeBox.Font = new Font("Segoe UI", 8F);
            AvailableTimeBox.Location = new Point(12, 437);
            AvailableTimeBox.Name = "AvailableTimeBox";
            AvailableTimeBox.Size = new Size(448, 147);
            AvailableTimeBox.TabIndex = 8;
            // 
            // InvitationOutputBox
            // 
            InvitationOutputBox.AutoScroll = true;
            InvitationOutputBox.BackColor = SystemColors.ButtonHighlight;
            InvitationOutputBox.BorderStyle = BorderStyle.FixedSingle;
            InvitationOutputBox.Location = new Point(487, 302);
            InvitationOutputBox.Name = "InvitationOutputBox";
            InvitationOutputBox.Size = new Size(515, 279);
            InvitationOutputBox.TabIndex = 9;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F);
            label4.Location = new Point(487, 271);
            label4.Name = "label4";
            label4.Size = new Size(176, 28);
            label4.TabIndex = 10;
            label4.Text = "Incoming requests:";
            // 
            // MeetupHub
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1014, 593);
            Controls.Add(label4);
            Controls.Add(InvitationOutputBox);
            Controls.Add(AvailableTimeBox);
            Controls.Add(FindFreeTimeButton);
            Controls.Add(SendInv);
            Controls.Add(label3);
            Controls.Add(ChooseTimeBox);
            Controls.Add(label2);
            Controls.Add(FriendSelectCheckBox);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "MeetupHub";
            Text = "MeetupHub";
            Load += MeetupHub_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private CheckedListBox FriendSelectCheckBox;
        private Label label2;
        private ComboBox ChooseTimeBox;
        private Label label3;
        private Button SendInv;
        private Button FindFreeTimeButton;
        private Label AvailableTimeBox;
        private FlowLayoutPanel InvitationOutputBox;
        private Label label4;
    }
}