﻿using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace RealNews
{
    public partial class frmSettings : Form
    {
        public frmSettings()
        {
            InitializeComponent();
        }
        bool _isdirty = false;


        private void frmSettings_Load(object sender, EventArgs e)
        {
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            // setup form
            chkUseSystemProxy.Checked = Settings.UseSytemProxy;
            numUpdate.Value = Settings.GlobalUpdateEveryMin;
            numWebPort.Value = Settings.webport;
            numDownloadSize.Value = Settings.DownloadImagesUnderKB;
            txtStart.Text = Settings.StartDownloadImgTime;
            txtEnd.Text = Settings.EndDownloadImgTime;
            lblLastUpdated.Text = Settings.LastUpdateTime.ToLongTimeString();
            numCleaupDays.Value = Settings.CleanupItemAfterDays;
            _isdirty = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cancelclose();
        }

        private void cancelclose()
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Settings.UseSytemProxy = chkUseSystemProxy.Checked;
                Settings.GlobalUpdateEveryMin = (int)numUpdate.Value;
                Settings.webport = (int)numWebPort.Value;
                Settings.DownloadImagesUnderKB = (int)numDownloadSize.Value;
                Settings.CleanupItemAfterDays = (int)numCleaupDays.Value;

                var st = TimeSpan.Parse(txtStart.Text);
                var ed = TimeSpan.Parse(txtEnd.Text);
                // validate start end times
                if (st < ed)
                {
                    Settings.StartDownloadImgTime = txtStart.Text;
                    Settings.EndDownloadImgTime = txtEnd.Text;

                    DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                    MessageBox.Show("Start Time must be less than End Time");
            }
            catch
            {
                MessageBox.Show("Invalid settings");
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("notepad.exe","configs/style.css");
        }

        private void frmSettings_KeyUp(object sender, KeyEventArgs e)
        {
            if (_isdirty == false && e.KeyCode == Keys.Escape)
                cancelclose();
        }

        private void chkUseSystemProxy_CheckedChanged(object sender, EventArgs e)
        {
            _isdirty = true;
        }
    }
}
