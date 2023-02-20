@echo off
copy %SystemRoot%\System32\drivers\etc\hosts hosts.bak
copy /b hosts.bak+hosts.add %tmp%\hosts
cls
cd bin
wget "https://ftp.mozilla.org/pub/firefox/releases/52.2.1esr/win64/en-US/Firefox Setup 52.2.1esr.exe"
wget https://downloads.bitnami.com/files/stacks/wampstack/8.1.12-0/bitnami-wampstack-8.1.12-0-windows-x64-installer.exe
title Local Shidonni Server AutoTool
Silverlight_x64.exe /q /doNotRequireDRMPrompt /noupdate
"Firefox Setup 52.2.1esr.exe" -ms -ma
ates.lnk
cls
echo Uncheck all boxes as you manually install the following application. The password is your choice, but leave the stack's directory unchanged!
pause
bitnami-wampstack-8.1.12-0-windows-x64-installer.exe
del /Q C:\Bitnami\wampstack-8.1.12-0\apache2\htdocs\*.*
del /Q C:\Bitnami\wampstack-8.1.12-0\apache2\htdocs\img\*.*
copy htdocs.zip C:\Bitnami\wampstack-8.1.12-0\apache2\
cls
cd C:\Bitnami\wampstack-8.1.12-0\apache2\
rd htdocs\img
powershell Expand-Archive htdocs.v2.zip
del htdocs.zip
echo All finished!
pause