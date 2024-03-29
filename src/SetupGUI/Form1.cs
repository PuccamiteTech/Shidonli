﻿using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Windows.Forms;

namespace Shidonli
{
    public partial class frmSetup : Form
    {
        public frmSetup()
        {
            InitializeComponent();
        }

        private void frmSetup_Load(object sender, EventArgs e)
        {
            txtStatus.Text = "Initializing...";

            // Check OS version
            if (Environment.OSVersion.ToString().Contains("6.0"))
            {
                txtStatus.Text += Environment.NewLine + "WARNING: Windows Vista will fail to download the WAMP stack.";
                txtStatus.Text += Environment.NewLine + "WARNING: During testing, Windows Vista failed to run Apache.";
            }
            else if (Environment.OSVersion.ToString().Contains("6.1"))
            {
                txtStatus.Text += Environment.NewLine + "WARNING: If TLS 1.2 is disabled, Shidonli may fail on Windows 7.";
            }

            // Discourage Tampering
            if (!File.Exists("Shidonli.exe.config"))
            {
                txtStatus.Text += Environment.NewLine + "WARNING: The app may misbehave without its intended config.";
            }

            // Restrict to 64-bit OSes
            if (!Environment.Is64BitOperatingSystem)
            {
                txtStatus.Text += Environment.NewLine + "This tool is not for 32-bit systems!";
                btnInstall.Enabled = false;
                btnReset.Enabled = false;
                grpVersion.Enabled = false;
                grpModifications.Enabled = false;
            }

            // Check for supplementary files
            if (!File.Exists(@"Resources\Silverlight_Developer_x64.exe"))
            {
                txtStatus.Text += Environment.NewLine + "Silverlight_Developer_x64.exe is missing!";
                btnInstall.Enabled = false;
            }

            if (!File.Exists(@"Resources\RegisterLib.xap"))
            {
                txtStatus.Text += Environment.NewLine + "RegisterLib.xap is missing!";
                chkRegister.Enabled = false;
                chkRegister.Checked = false;
            }

            if (!File.Exists(@"Resources\htdocs.v1.zip"))
            {
                radV1.Enabled = false;
                radV1.Checked = false;
            }
            else
            {
                txtStatus.Text += Environment.NewLine + "htdocs.v1.zip detected...";
            }

            if (!File.Exists(@"Resources\htdocs.v2.zip"))
            {
                radV2.Enabled = false;
                radV2.Checked = false;
            }
            else
            {
                txtStatus.Text += Environment.NewLine + "htdocs.v2.zip detected...";
            }


            if (!File.Exists(@"Resources\htdocs.v1.zip") && !File.Exists(@"Resources\htdocs.v2.zip"))
            {
                txtStatus.Text += Environment.NewLine + "No htdocs zip files are present!";
                btnInstall.Enabled = false;
            }
        }

        private async void btnInstall_Click(object sender, EventArgs e)
        {
            try
            {
                string wampLnk = "https://sourceforge.net/projects/xampp/files/XAMPP%20Windows/8.2.4/xampp-windows-x64-8.2.4-0-VS16-installer.exe";
                string wampExe = @"C:\xampp\xampp-control.exe";
                string htdocsDir = @"C:\xampp\htdocs";
                string silverlightDir = @"C:\Program Files\Microsoft Silverlight\5.1.50918.0";
                string registerFile = htdocsDir + @"\v2\ClientBin\RegisterLib.xap";
                string hostsFile = @"C:\Windows\System32\drivers\etc\hosts";
                string ip = "\n127.0.0.1 ";
                string host = "shidonni.com";

                if (radV1.Checked)
                {
                    chkRegister.Checked = false;
                }

                btnInstall.Enabled = false;
                btnReset.Enabled = false;
                grpVersion.Enabled = false;
                grpModifications.Enabled = false;

                // Update hosts
                File.Copy(hostsFile, @"Resources\hosts.bak", true);

                if (!File.ReadAllText(hostsFile).Contains("shidonni"))
                {
                    txtStatus.Text += Environment.NewLine + "Adding hosts...";
                    File.AppendAllText(hostsFile, ip + host + ip + "www." + host + ip + "www2." + host);
                }

                // Download software
                if (!File.Exists(@"Resources\xampp.exe") || chkFresh.Checked)
                {
                    WebClient client = new WebClient();
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    txtStatus.Text += Environment.NewLine + "Downloading WAMP stack...";
                    await client.DownloadFileTaskAsync(new Uri(wampLnk), @"Resources\xampp.exe");
                }

                // Install Silverlight
                if (!Directory.Exists(silverlightDir) || chkFresh.Checked)
                {
                    txtStatus.Text += Environment.NewLine + "Installing Silverlight...";
                    Process.Start(@"Resources\Silverlight_Developer_x64.exe", "/q /doNotRequireDRMPrompt /noupdate");
                }

                // Install WAMP (wait until done)
                if (!File.Exists(wampExe) || chkFresh.Checked)
                {
                    txtStatus.Text += Environment.NewLine + "Installing WAMP stack...";
                    var process = Process.Start(@"Resources\xampp.exe",
                    "--mode unattended --unattendedmodeui minimal --disable-components xampp_filezilla," +
                    "xampp_mercury,xampp_tomcat,xampp_perl,xampp_webalizer,xampp_sendmail");
                    process.WaitForExit();
                }

                // Clean htdocs
                txtStatus.Text += Environment.NewLine + "Deleting old htdocs...";
                if (Directory.Exists(htdocsDir))
                {
                    Directory.Delete(htdocsDir, true);
                }

                Directory.CreateDirectory(htdocsDir);

                // Fill htdocs
                if (radV1.Checked)
                {
                    txtStatus.Text += Environment.NewLine + "Extracting v1 htdocs...";
                    ZipFile.ExtractToDirectory(@"Resources\htdocs.v1.zip", htdocsDir);
                }
                else
                {
                    txtStatus.Text += Environment.NewLine + "Extracting v2 htdocs...";
                    ZipFile.ExtractToDirectory(@"Resources\htdocs.v2.zip", htdocsDir);
                }

                // Apply modifications
                if (chkRegister.Checked && radV2.Checked)
                {
                    txtStatus.Text += Environment.NewLine + "Replacing RegisterLib.xap...";
                    File.Copy(@"Resources\RegisterLib.xap", registerFile, true);
                }

                txtStatus.Text += Environment.NewLine + "Installation is complete!";

                btnInstall.Enabled = true;
                btnReset.Enabled = true;
                grpVersion.Enabled = true;
                grpModifications.Enabled = true;

                if (File.Exists(wampExe))
                {
                    Process.Start(wampExe);
                }
            }
            catch (Exception err)
            {
                txtStatus.Text += Environment.NewLine + "ERROR: " + err.Message;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtStatus.Text = "Resetting selections...";
            radV1.Checked = false;
            radV2.Checked = true;
            chkRegister.Checked = true;
            chkFresh.Checked = false;
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
