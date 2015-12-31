using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;

namespace LegionLauncher
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.mods = new System.Windows.Forms.TabPage();
            this.armaPath = new System.Windows.Forms.Label();
            this.currentModsViewBox = new System.Windows.Forms.ListBox();
            this.mainTabControl.SuspendLayout();
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
            this.servers.BackColor = System.Drawing.Color.Black;
            this.servers.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.servers.Location = new System.Drawing.Point(4, 22);
            this.servers.Name = "servers";
            this.servers.Padding = new System.Windows.Forms.Padding(3);
            this.servers.Size = new System.Drawing.Size(513, 521);
            this.servers.TabIndex = 0;
            this.servers.Text = "Legion Servers";
            // 
            // mods
            // 
            this.mods.BackColor = System.Drawing.Color.Black;
            this.mods.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.mods.Controls.Add(this.currentModsViewBox);
            this.mods.Controls.Add(this.armaPath);
            this.mods.Location = new System.Drawing.Point(4, 22);
            this.mods.Name = "mods";
            this.mods.Padding = new System.Windows.Forms.Padding(3);
            this.mods.Size = new System.Drawing.Size(513, 521);
            this.mods.TabIndex = 1;
            this.mods.Text = "Mods";
            this.mods.Click += new System.EventHandler(this.mods_Click);
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
            // currentModsViewBox
            // 
            this.currentModsViewBox.FormattingEnabled = true;
            this.currentModsViewBox.Location = new System.Drawing.Point(0, 204);
            this.currentModsViewBox.Name = "currentModsViewBox";
            this.currentModsViewBox.Size = new System.Drawing.Size(513, 316);
            this.currentModsViewBox.TabIndex = 1;
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(521, 547);
            this.Controls.Add(this.mainTabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Legion ARMA Launcher";
            this.mainTabControl.ResumeLayout(false);
            this.mods.ResumeLayout(false);
            this.mods.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion
        
        
        private System.Windows.Forms.TabPage servers;
        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage mods;
        private System.Windows.Forms.Label armaPath;
        private System.Windows.Forms.ListBox currentModsViewBox;
    }
}

