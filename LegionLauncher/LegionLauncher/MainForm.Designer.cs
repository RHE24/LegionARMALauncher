using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;

namespace LegionLauncher
{
    partial class MainForm
    {

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.servers = new System.Windows.Forms.TabPage();
            this.serverOneLaunchButton = new System.Windows.Forms.Button();
            this.playersPlaceholder = new System.Windows.Forms.Label();
            this.serverNamePlaceholder = new System.Windows.Forms.Label();
            this.mods = new System.Windows.Forms.TabPage();
            this.currentModsListBox = new System.Windows.Forms.ListView();
            this.verifyModsButton = new System.Windows.Forms.Button();
            this.armaPath = new System.Windows.Forms.Label();
            this.mainTabControl.SuspendLayout();
            this.servers.SuspendLayout();
            this.mods.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTabControl
            // 
            this.mainTabControl.Controls.Add(this.servers);
            this.mainTabControl.Controls.Add(this.mods);
            this.mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabControl.Location = new System.Drawing.Point(0, 0);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(521, 547);
            this.mainTabControl.TabIndex = 0;
            // 
            // servers
            // 
            this.servers.BackColor = System.Drawing.Color.Transparent;
            this.servers.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.servers.Controls.Add(this.serverOneLaunchButton);
            this.servers.Controls.Add(this.playersPlaceholder);
            this.servers.Controls.Add(this.serverNamePlaceholder);
            this.servers.Location = new System.Drawing.Point(4, 22);
            this.servers.Name = "servers";
            this.servers.Padding = new System.Windows.Forms.Padding(3);
            this.servers.Size = new System.Drawing.Size(513, 521);
            this.servers.TabIndex = 0;
            this.servers.Text = "Legion Servers";
            // 
            // serverOneLaunchButton
            // 
            this.serverOneLaunchButton.Location = new System.Drawing.Point(366, 42);
            this.serverOneLaunchButton.Name = "serverOneLaunchButton";
            this.serverOneLaunchButton.Size = new System.Drawing.Size(122, 56);
            this.serverOneLaunchButton.TabIndex = 3;
            this.serverOneLaunchButton.Text = "PLAY!";
            this.serverOneLaunchButton.UseVisualStyleBackColor = true;
            this.serverOneLaunchButton.Click += new System.EventHandler(this.serverOneLaunchButton_Click);
            // 
            // playersPlaceholder
            // 
            this.playersPlaceholder.AutoSize = true;
            this.playersPlaceholder.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playersPlaceholder.Location = new System.Drawing.Point(8, 73);
            this.playersPlaceholder.Name = "playersPlaceholder";
            this.playersPlaceholder.Size = new System.Drawing.Size(202, 25);
            this.playersPlaceholder.TabIndex = 2;
            this.playersPlaceholder.Text = "players Placeholder";
            this.playersPlaceholder.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // serverNamePlaceholder
            // 
            this.serverNamePlaceholder.AutoSize = true;
            this.serverNamePlaceholder.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.serverNamePlaceholder.Location = new System.Drawing.Point(3, 42);
            this.serverNamePlaceholder.Name = "serverNamePlaceholder";
            this.serverNamePlaceholder.Size = new System.Drawing.Size(323, 31);
            this.serverNamePlaceholder.TabIndex = 0;
            this.serverNamePlaceholder.Text = "Server Name Placeholder";
            this.serverNamePlaceholder.Click += new System.EventHandler(this.label1_Click);
            // 
            // mods
            // 
            this.mods.BackColor = System.Drawing.Color.Black;
            this.mods.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.mods.Controls.Add(this.currentModsListBox);
            this.mods.Controls.Add(this.verifyModsButton);
            this.mods.Controls.Add(this.armaPath);
            this.mods.Location = new System.Drawing.Point(4, 22);
            this.mods.Name = "mods";
            this.mods.Padding = new System.Windows.Forms.Padding(3);
            this.mods.Size = new System.Drawing.Size(513, 521);
            this.mods.TabIndex = 1;
            this.mods.Text = "Mods";
            this.mods.Click += new System.EventHandler(this.mods_Click);
            // 
            // currentModsListBox
            // 
            this.currentModsListBox.Location = new System.Drawing.Point(0, 203);
            this.currentModsListBox.Name = "currentModsListBox";
            this.currentModsListBox.Size = new System.Drawing.Size(517, 322);
            this.currentModsListBox.TabIndex = 3;
            this.currentModsListBox.UseCompatibleStateImageBehavior = false;
            // 
            // verifyModsButton
            // 
            this.verifyModsButton.Location = new System.Drawing.Point(196, 113);
            this.verifyModsButton.Name = "verifyModsButton";
            this.verifyModsButton.Size = new System.Drawing.Size(112, 38);
            this.verifyModsButton.TabIndex = 2;
            this.verifyModsButton.Text = "Verify your mods";
            this.verifyModsButton.UseVisualStyleBackColor = true;
            this.verifyModsButton.Click += new System.EventHandler(this.verifyModsButton_Click);
            // 
            // armaPath
            // 
            this.armaPath.AutoSize = true;
            this.armaPath.BackColor = System.Drawing.Color.Green;
            this.armaPath.ForeColor = System.Drawing.Color.White;
            this.armaPath.Location = new System.Drawing.Point(193, 37);
            this.armaPath.Name = "armaPath";
            this.armaPath.Size = new System.Drawing.Size(115, 13);
            this.armaPath.TabIndex = 0;
            this.armaPath.Text = "Arma Path Placeholder";
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(521, 547);
            this.Controls.Add(this.mainTabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Legion ARMA Launcher";
            this.mainTabControl.ResumeLayout(false);
            this.servers.ResumeLayout(false);
            this.servers.PerformLayout();
            this.mods.ResumeLayout(false);
            this.mods.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion
        
        
        private System.Windows.Forms.TabPage servers;
        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage mods;
        private System.Windows.Forms.Label armaPath;
        private System.Windows.Forms.Label serverNamePlaceholder;
        private System.Windows.Forms.Label playersPlaceholder;
        private System.Windows.Forms.Button serverOneLaunchButton;
        private System.Windows.Forms.Button verifyModsButton;
        private System.Windows.Forms.ListView currentModsListBox;
    }
}

