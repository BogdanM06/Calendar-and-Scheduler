namespace Scheduler_NEA
{
    partial class MainPage
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
            sqlCommand1 = new Microsoft.Data.SqlClient.SqlCommand();
            UserProfilePicture = new PictureBox();
            CalUser = new Label();
            SettingsButton = new PictureBox();
            SuggestionBox = new Label();
            panel1 = new Panel();
            label9 = new Label();
            AddPanel = new Panel();
            AddEventButton = new Button();
            PriorityCheck = new CheckBox();
            AllDayCheck = new CheckBox();
            label7 = new Label();
            label6 = new Label();
            RepeatFrequency = new ComboBox();
            label5 = new Label();
            EndDate = new DateTimePicker();
            label4 = new Label();
            label3 = new Label();
            EventName = new TextBox();
            StartDate = new DateTimePicker();
            label2 = new Label();
            WeekCalendar = new Button();
            MonthCalendar = new Button();
            panel2 = new Panel();
            DeleteButton = new Button();
            DEvent = new TextBox();
            label8 = new Label();
            panel3 = new Panel();
            label1 = new Label();
            SettingsPanel = new Panel();
            MeetUpButton = new Button();
            FriendAddButton = new Button();
            ChangePfpButton = new Button();
            ChangePasswordButton = new Button();
            LogOutButton = new Button();
            ((System.ComponentModel.ISupportInitialize)UserProfilePicture).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SettingsButton).BeginInit();
            panel1.SuspendLayout();
            AddPanel.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            SettingsPanel.SuspendLayout();
            SuspendLayout();
            // 
            // sqlCommand1
            // 
            sqlCommand1.CommandTimeout = 30;
            sqlCommand1.EnableOptimizedParameterBinding = false;
            // 
            // UserProfilePicture
            // 
            UserProfilePicture.BackColor = SystemColors.ControlDark;
            UserProfilePicture.BackgroundImageLayout = ImageLayout.None;
            UserProfilePicture.InitialImage = null;
            UserProfilePicture.Location = new Point(12, 12);
            UserProfilePicture.Name = "UserProfilePicture";
            UserProfilePicture.Size = new Size(75, 75);
            UserProfilePicture.SizeMode = PictureBoxSizeMode.Zoom;
            UserProfilePicture.TabIndex = 0;
            UserProfilePicture.TabStop = false;
            UserProfilePicture.Click += UserProfilePicture_Click;
            // 
            // CalUser
            // 
            CalUser.BackColor = SystemColors.MenuBar;
            CalUser.Font = new Font("Copperplate Gothic Bold", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            CalUser.Location = new Point(12, 90);
            CalUser.Name = "CalUser";
            CalUser.Size = new Size(224, 22);
            CalUser.TabIndex = 1;
            CalUser.Text = "label1";
            // 
            // SettingsButton
            // 
            SettingsButton.BackColor = SystemColors.ControlLight;
            SettingsButton.Image = Properties.Resources.settings_960;
            SettingsButton.Location = new Point(1841, 12);
            SettingsButton.Name = "SettingsButton";
            SettingsButton.Size = new Size(50, 50);
            SettingsButton.SizeMode = PictureBoxSizeMode.StretchImage;
            SettingsButton.TabIndex = 2;
            SettingsButton.TabStop = false;
            SettingsButton.Click += SettingsButton_Click;
            // 
            // SuggestionBox
            // 
            SuggestionBox.AllowDrop = true;
            SuggestionBox.BackColor = SystemColors.ActiveCaption;
            SuggestionBox.Font = new Font("Segoe UI", 9F);
            SuggestionBox.ForeColor = SystemColors.ButtonHighlight;
            SuggestionBox.Location = new Point(-1, 33);
            SuggestionBox.Name = "SuggestionBox";
            SuggestionBox.Size = new Size(500, 190);
            SuggestionBox.TabIndex = 0;
            SuggestionBox.TextAlign = ContentAlignment.TopCenter;
            SuggestionBox.Click += label1_Click;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ActiveCaption;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(label9);
            panel1.Controls.Add(SuggestionBox);
            panel1.Location = new Point(12, 115);
            panel1.Name = "panel1";
            panel1.Size = new Size(500, 224);
            panel1.TabIndex = 3;
            // 
            // label9
            // 
            label9.BackColor = SystemColors.InactiveCaption;
            label9.Font = new Font("Segoe UI", 12F);
            label9.Location = new Point(0, 0);
            label9.Name = "label9";
            label9.Size = new Size(500, 33);
            label9.TabIndex = 1;
            label9.Text = "IN ORDER TO MAXIMISE FREE TIME WITH FRIENDS...";
            // 
            // AddPanel
            // 
            AddPanel.BackColor = SystemColors.ActiveCaption;
            AddPanel.BorderStyle = BorderStyle.FixedSingle;
            AddPanel.Controls.Add(AddEventButton);
            AddPanel.Controls.Add(PriorityCheck);
            AddPanel.Controls.Add(AllDayCheck);
            AddPanel.Controls.Add(label7);
            AddPanel.Controls.Add(label6);
            AddPanel.Controls.Add(RepeatFrequency);
            AddPanel.Controls.Add(label5);
            AddPanel.Controls.Add(EndDate);
            AddPanel.Controls.Add(label4);
            AddPanel.Controls.Add(label3);
            AddPanel.Controls.Add(EventName);
            AddPanel.Controls.Add(StartDate);
            AddPanel.Controls.Add(label2);
            AddPanel.Location = new Point(12, 535);
            AddPanel.Name = "AddPanel";
            AddPanel.Size = new Size(500, 474);
            AddPanel.TabIndex = 4;
            // 
            // AddEventButton
            // 
            AddEventButton.Location = new Point(178, 386);
            AddEventButton.Name = "AddEventButton";
            AddEventButton.Size = new Size(120, 29);
            AddEventButton.TabIndex = 11;
            AddEventButton.Text = "Add Event (+)";
            AddEventButton.UseVisualStyleBackColor = true;
            AddEventButton.Click += AddEventButton_Click;
            // 
            // PriorityCheck
            // 
            PriorityCheck.AutoSize = true;
            PriorityCheck.Font = new Font("Segoe UI", 11F);
            PriorityCheck.Location = new Point(35, 256);
            PriorityCheck.Name = "PriorityCheck";
            PriorityCheck.Size = new Size(95, 29);
            PriorityCheck.TabIndex = 10;
            PriorityCheck.Text = "Priority";
            PriorityCheck.UseVisualStyleBackColor = true;
            // 
            // AllDayCheck
            // 
            AllDayCheck.AutoSize = true;
            AllDayCheck.Font = new Font("Segoe UI", 11F);
            AllDayCheck.Location = new Point(185, 256);
            AllDayCheck.Name = "AllDayCheck";
            AllDayCheck.Size = new Size(94, 29);
            AllDayCheck.TabIndex = 9;
            AllDayCheck.Text = "All-day";
            AllDayCheck.UseVisualStyleBackColor = true;
            AllDayCheck.CheckedChanged += AllDayCheck_CheckedChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 11F);
            label7.Location = new Point(463, 5);
            label7.Name = "label7";
            label7.Size = new Size(25, 25);
            label7.TabIndex = 8;
            label7.Text = "+";
            // 
            // label6
            // 
            label6.Font = new Font("Segoe UI", 11F);
            label6.Location = new Point(3, 321);
            label6.Name = "label6";
            label6.Size = new Size(80, 25);
            label6.TabIndex = 7;
            label6.Text = "Repeat:";
            // 
            // RepeatFrequency
            // 
            RepeatFrequency.DropDownStyle = ComboBoxStyle.DropDownList;
            RepeatFrequency.FormattingEnabled = true;
            RepeatFrequency.Items.AddRange(new object[] { "None", "Daily", "Weekly", "Monthly", "Annually" });
            RepeatFrequency.Location = new Point(119, 321);
            RepeatFrequency.Name = "RepeatFrequency";
            RepeatFrequency.Size = new Size(160, 28);
            RepeatFrequency.TabIndex = 5;
            // 
            // label5
            // 
            label5.Font = new Font("Segoe UI", 11F);
            label5.Location = new Point(-1, 192);
            label5.Name = "label5";
            label5.Size = new Size(93, 27);
            label5.TabIndex = 6;
            label5.Text = "End date:";
            // 
            // EndDate
            // 
            EndDate.CustomFormat = "dd/MM/yyyy HH:mm";
            EndDate.Format = DateTimePickerFormat.Custom;
            EndDate.Location = new Point(119, 192);
            EndDate.Name = "EndDate";
            EndDate.Size = new Size(160, 27);
            EndDate.TabIndex = 5;
            // 
            // label4
            // 
            label4.Font = new Font("Segoe UI", 11F);
            label4.Location = new Point(-1, 146);
            label4.Name = "label4";
            label4.Size = new Size(104, 27);
            label4.TabIndex = 4;
            label4.Text = "Start date:";
            // 
            // label3
            // 
            label3.Font = new Font("Segoe UI", 11F);
            label3.Location = new Point(-1, 69);
            label3.Name = "label3";
            label3.Size = new Size(114, 27);
            label3.TabIndex = 3;
            label3.Text = "Event name:";
            // 
            // EventName
            // 
            EventName.Font = new Font("Segoe UI", 11F);
            EventName.Location = new Point(119, 69);
            EventName.Name = "EventName";
            EventName.Size = new Size(369, 32);
            EventName.TabIndex = 2;
            // 
            // StartDate
            // 
            StartDate.CustomFormat = "dd/MM/yyyy HH:mm";
            StartDate.Font = new Font("Segoe UI", 9F);
            StartDate.Format = DateTimePickerFormat.Custom;
            StartDate.Location = new Point(119, 146);
            StartDate.Name = "StartDate";
            StartDate.Size = new Size(160, 27);
            StartDate.TabIndex = 1;
            // 
            // label2
            // 
            label2.BackColor = SystemColors.InactiveCaption;
            label2.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(-1, 0);
            label2.Name = "label2";
            label2.Size = new Size(500, 37);
            label2.TabIndex = 0;
            label2.Text = "ADD ";
            // 
            // WeekCalendar
            // 
            WeekCalendar.Location = new Point(1684, 85);
            WeekCalendar.Name = "WeekCalendar";
            WeekCalendar.RightToLeft = RightToLeft.Yes;
            WeekCalendar.Size = new Size(94, 29);
            WeekCalendar.TabIndex = 6;
            WeekCalendar.Text = "Week";
            WeekCalendar.UseVisualStyleBackColor = true;
            WeekCalendar.Click += WeekCalendar_Click;
            // 
            // MonthCalendar
            // 
            MonthCalendar.Location = new Point(1784, 85);
            MonthCalendar.Name = "MonthCalendar";
            MonthCalendar.Size = new Size(94, 29);
            MonthCalendar.TabIndex = 7;
            MonthCalendar.Text = "Month";
            MonthCalendar.UseVisualStyleBackColor = true;
            MonthCalendar.Click += MonthCalendar_Click;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ActiveCaption;
            panel2.Controls.Add(DeleteButton);
            panel2.Controls.Add(DEvent);
            panel2.Controls.Add(label8);
            panel2.Location = new Point(12, 345);
            panel2.Name = "panel2";
            panel2.Size = new Size(500, 184);
            panel2.TabIndex = 9;
            // 
            // DeleteButton
            // 
            DeleteButton.Location = new Point(179, 133);
            DeleteButton.Name = "DeleteButton";
            DeleteButton.Size = new Size(120, 29);
            DeleteButton.TabIndex = 2;
            DeleteButton.Text = "Delete";
            DeleteButton.UseVisualStyleBackColor = true;
            DeleteButton.Click += DeleteButton_Click;
            // 
            // DEvent
            // 
            DEvent.Location = new Point(124, 61);
            DEvent.Name = "DEvent";
            DEvent.Size = new Size(365, 27);
            DEvent.TabIndex = 1;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 11F);
            label8.Location = new Point(4, 60);
            label8.Name = "label8";
            label8.Size = new Size(114, 25);
            label8.TabIndex = 0;
            label8.Text = "Event name:";
            // 
            // panel3
            // 
            panel3.BackColor = SystemColors.InactiveCaption;
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel3.Controls.Add(label1);
            panel3.Location = new Point(12, 345);
            panel3.Name = "panel3";
            panel3.Size = new Size(500, 38);
            panel3.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(-1, -1);
            label1.Name = "label1";
            label1.Size = new Size(89, 31);
            label1.TabIndex = 0;
            label1.Text = "DELETE";
            // 
            // SettingsPanel
            // 
            SettingsPanel.BackColor = SystemColors.ActiveCaption;
            SettingsPanel.Controls.Add(MeetUpButton);
            SettingsPanel.Controls.Add(FriendAddButton);
            SettingsPanel.Controls.Add(ChangePfpButton);
            SettingsPanel.Controls.Add(ChangePasswordButton);
            SettingsPanel.Controls.Add(LogOutButton);
            SettingsPanel.Location = new Point(731, 12);
            SettingsPanel.Name = "SettingsPanel";
            SettingsPanel.Size = new Size(1084, 50);
            SettingsPanel.TabIndex = 10;
            SettingsPanel.Visible = false;
            // 
            // MeetUpButton
            // 
            MeetUpButton.Location = new Point(885, 7);
            MeetUpButton.Name = "MeetUpButton";
            MeetUpButton.Size = new Size(180, 40);
            MeetUpButton.TabIndex = 12;
            MeetUpButton.Text = "Manage meetups";
            MeetUpButton.UseVisualStyleBackColor = true;
            MeetUpButton.Click += MeetUpButton_Click;
            // 
            // FriendAddButton
            // 
            FriendAddButton.Location = new Point(667, 7);
            FriendAddButton.Name = "FriendAddButton";
            FriendAddButton.Size = new Size(180, 40);
            FriendAddButton.TabIndex = 11;
            FriendAddButton.Text = "Add friend";
            FriendAddButton.UseVisualStyleBackColor = true;
            FriendAddButton.Click += FriendAddButton_Click;
            // 
            // ChangePfpButton
            // 
            ChangePfpButton.Location = new Point(239, 7);
            ChangePfpButton.Name = "ChangePfpButton";
            ChangePfpButton.Size = new Size(180, 40);
            ChangePfpButton.TabIndex = 2;
            ChangePfpButton.Text = "Change profile picture";
            ChangePfpButton.UseVisualStyleBackColor = true;
            ChangePfpButton.Click += ChangePfpButton_Click_1;
            // 
            // ChangePasswordButton
            // 
            ChangePasswordButton.Location = new Point(456, 7);
            ChangePasswordButton.Name = "ChangePasswordButton";
            ChangePasswordButton.Size = new Size(180, 40);
            ChangePasswordButton.TabIndex = 1;
            ChangePasswordButton.Text = "Change password";
            ChangePasswordButton.UseVisualStyleBackColor = true;
            ChangePasswordButton.Click += ChangePasswordButton_Click;
            // 
            // LogOutButton
            // 
            LogOutButton.Location = new Point(16, 7);
            LogOutButton.Name = "LogOutButton";
            LogOutButton.Size = new Size(180, 40);
            LogOutButton.TabIndex = 0;
            LogOutButton.Text = "Log out";
            LogOutButton.UseVisualStyleBackColor = true;
            LogOutButton.Click += LogOutButton_Click;
            // 
            // MainPage
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.AlbedoBase_XL_A_mesmerizing_abstract_composition_featuring_a_v_3;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1924, 1055);
            Controls.Add(SettingsPanel);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(MonthCalendar);
            Controls.Add(WeekCalendar);
            Controls.Add(AddPanel);
            Controls.Add(panel1);
            Controls.Add(SettingsButton);
            Controls.Add(CalUser);
            Controls.Add(UserProfilePicture);
            Name = "MainPage";
            Load += MainPage_Load;
            ((System.ComponentModel.ISupportInitialize)UserProfilePicture).EndInit();
            ((System.ComponentModel.ISupportInitialize)SettingsButton).EndInit();
            panel1.ResumeLayout(false);
            AddPanel.ResumeLayout(false);
            AddPanel.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            SettingsPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Microsoft.Data.SqlClient.SqlCommand sqlCommand1;
        private PictureBox UserProfilePicture;
        private Label CalUser;
        private PictureBox SettingsButton;
        private Label SuggestionBox;
        private Panel panel1;
        private Panel AddPanel;
        private Label label2;
        private DateTimePicker StartDate;
        private Label label3;
        private TextBox EventName;
        private Label label4;
        private DateTimePicker EndDate;
        private Label label5;
        private ComboBox RepeatFrequency;
        private Label label6;
        private Label label7;
        private CheckBox AllDayCheck;
        private CheckBox PriorityCheck;
        private Button AddEventButton;
        private Button WeekCalendar;
        private Button MonthCalendar;
        private Panel panel2;
        private Panel panel3;
        private Label label1;
        private Button DeleteButton;
        private TextBox DEvent;
        private Label label8;
        private Panel SettingsPanel;
        private Button ChangePasswordButton;
        private Button LogOutButton;
        private Button ChangePfpButton;
        private Button FriendAddButton;
        private Label label9;
        private Button MeetUpButton;
    }
}