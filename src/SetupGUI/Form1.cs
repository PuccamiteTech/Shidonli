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

        private void ToggleControls(bool doDisable)
        {
            btnInstall.Enabled = doDisable;
            btnReset.Enabled = doDisable;
            grpVersions.Enabled = doDisable;
            grpSettings.Enabled = doDisable;
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

            // Discourage tampering
            if (!File.Exists("Shidonli.exe.config"))
            {
                txtStatus.Text += Environment.NewLine + "WARNING: The app may misbehave without its intended config.";
            }

            // Check for supplementary files
            if (!File.Exists(@"Resources\htdocs.v1.zip"))
            {
                chkV1.Enabled = false;
                chkV1.Checked = false;
            }
            else
            {
                txtStatus.Text += Environment.NewLine + "V1 htdocs detected...";
            }

            if (!File.Exists(@"Resources\htdocs.v2.zip"))
            {
                chkV2.Enabled = false;
                chkV2.Checked = false;
                chkRegister.Enabled = false;
            }
            else
            {
                txtStatus.Text += Environment.NewLine + "V2 htdocs detected...";
            }

            if (!chkV1.Enabled && !chkV2.Enabled) // If this check is misplaced, it can falsely trigger.
            {
                txtStatus.Text += Environment.NewLine + "No Shidonni archives are present!";
                ToggleControls(false);
            }

            if (!File.Exists(@"Resources\httpd-vhosts.conf"))
            {
                txtStatus.Text += Environment.NewLine + "The vhosts config file is missing!";
                ToggleControls(false);
            }

            if (!File.Exists(@"Resources\Silverlight_Developer_x64.exe"))
            {
                txtStatus.Text += Environment.NewLine + "The Silverlight installer is missing!";
                ToggleControls(false);
            }

            if (!File.Exists(@"Resources\RegisterLib.xap"))
            {
                txtStatus.Text += Environment.NewLine + "The custom registration library is missing!";
                chkRegister.Enabled = false;
                chkRegister.Checked = false;
            }

            // Restrict to 64-bit OSes
            if (!Environment.Is64BitOperatingSystem)
            {
                txtStatus.Text += Environment.NewLine + "This tool is not for 32-bit systems!";
                ToggleControls(false);
            }
        }

        private async void btnInstall_Click(object sender, EventArgs e)
        {
            string wampLnk = "https://sourceforge.net/projects/xampp/files/XAMPP%20Windows/8.2.12/xampp-windows-x64-8.2.12-0-VS16-installer.exe";
            string wampExe = @"C:\xampp\xampp-control.exe";
            string htdocsDir = @"C:\xampp\htdocs";
            string shidonniDir = htdocsDir + @"\shidonni";
            string silverlightDir = @"C:\Program Files\Microsoft Silverlight\5.1.50918.0";
            string registerFile = shidonniDir + @"\islands\v2\ClientBin\RegisterLib.xap";
            string hostsFile = @"C:\Windows\System32\drivers\etc\hosts";
            string vhostsFile = @"C:\xampp\apache\conf\extra\httpd-vhosts.conf";
            string apacheFile = @"C:\xampp\apache\bin\httpd.exe";
            string mysqlFile = @"C:\xampp\mysql\bin\mysqld.exe";
            string ip = "\n127.0.0.1 ";
            string host = "shidonni.com";
            bool doFullClean = !Directory.Exists(htdocsDir);

            try
            {
                ToggleControls(false);

                // Uncheck invalid modifications
                if (!chkV2.Checked)
                {
                    chkRegister.Checked = false;
                }

                // Update hosts
                if (!File.ReadAllText(hostsFile).Contains(host))
                {
                    txtStatus.Text += Environment.NewLine + "Adding hosts...";
                    File.Copy(hostsFile, @"Resources\hosts.bak", true);
                    File.AppendAllText(hostsFile, ip + host + ip + "www." + host + ip + "www2." + host);
                }

                // Download software
                if (!File.Exists(wampExe) && !File.Exists(@"Resources\xampp.exe") || chkRepair.Checked)
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    txtStatus.Text += Environment.NewLine + "Downloading WAMP stack...";
                    await new WebClient().DownloadFileTaskAsync(new Uri(wampLnk), @"Resources\xampp.exe");
                }

                // Install Silverlight
                if (!Directory.Exists(silverlightDir) || chkRepair.Checked)
                {
                    txtStatus.Text += Environment.NewLine + "Installing Silverlight...";
                    Process.Start(@"Resources\Silverlight_Developer_x64.exe", "/q /doNotRequireDRMPrompt /noupdate");
                }

                // Install WAMP (wait until done)
                if (!File.Exists(wampExe) || chkRepair.Checked)
                {
                    txtStatus.Text += Environment.NewLine + "Installing WAMP stack...";
                    Process.Start(@"Resources\xampp.exe",
                    "--mode unattended --unattendedmodeui minimal --disable-components xampp_filezilla," +
                    "xampp_mercury,xampp_tomcat,xampp_perl,xampp_webalizer,xampp_sendmail").WaitForExit();
                }

                // Update vhosts
                if (!File.ReadAllText(vhostsFile).Contains(host))
                {
                    txtStatus.Text += Environment.NewLine + "Adding vhosts...";
                    File.AppendAllText(vhostsFile, File.ReadAllText(@"Resources\httpd-vhosts.conf"));
                }

                // Clean htdocs                
                if (doFullClean)
                {
                    txtStatus.Text += Environment.NewLine + "Preparing htdocs directory...";
                    Directory.Delete(htdocsDir, true);
                    Directory.CreateDirectory(htdocsDir);
                }
                else if (Directory.Exists(shidonniDir))
                {
                    txtStatus.Text += Environment.NewLine + "Deleting old htdocs...";
                    Directory.Delete(shidonniDir, true);
                }

                Directory.CreateDirectory(shidonniDir);

                // Fill htdocs
                if (chkV1.Checked)
                {
                    txtStatus.Text += Environment.NewLine + "Extracting V1 htdocs...";
                    ZipFile.ExtractToDirectory(@"Resources\htdocs.v1.zip", shidonniDir + @"\planets");
                }
                
                if (chkV2.Checked)
                {
                    txtStatus.Text += Environment.NewLine + "Extracting V2 htdocs...";
                    ZipFile.ExtractToDirectory(@"Resources\htdocs.v2.zip", shidonniDir + @"\islands");
                }

                // Apply modifications
                if (chkRegister.Checked && chkV2.Checked)
                {
                    txtStatus.Text += Environment.NewLine + "Replacing RegisterLib.xap...";
                    File.Copy(@"Resources\RegisterLib.xap", registerFile, true);
                }
                
                // Prepare Apache
                if (File.Exists(apacheFile))
                {
                    txtStatus.Text += Environment.NewLine + "Installing Apache service...";
                    Process.Start(apacheFile, "-k stop").WaitForExit();
                    Process.Start(apacheFile, "-k install").WaitForExit();
                    Process.Start(apacheFile, "-k start").WaitForExit();
                }

                // Prepare MySQL
                if (File.Exists(mysqlFile))
                {
                    txtStatus.Text += Environment.NewLine + "Installing MySQL Service...";
                    Process.Start("net.exe", "stop mysql").WaitForExit();
                    Process.Start(mysqlFile, "--install").WaitForExit();
                    Process.Start("net.exe", "start mysql").WaitForExit();
                }

                txtStatus.Text += Environment.NewLine + "Installation is complete!";

                ToggleControls(true);
            }
            catch (Exception err)
            {
                txtStatus.Text += Environment.NewLine + "ERROR: " + err.Message;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtStatus.Text = "Resetting selections...";

            if (chkV1.Enabled)
            {
                chkV1.Checked = true;
            }
            
            if (chkV2.Enabled)
            {
                chkV2.Checked = true;
            }

            if (chkRegister.Enabled)
            {
                chkRegister.Checked = true;
            }

            chkRepair.Checked = false;
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
