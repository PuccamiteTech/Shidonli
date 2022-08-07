@echo off
echo You must run this script as an administrator for it to work.
echo If you don't know what this script does, close it now!
pause
set /p ip=Enter the mirror's public IPv4 address: 
cd drivers\etc
copy hosts hosts.%random%.bak
echo %ip% shidonni.com >> hosts
echo %ip% www2.shidonni.com >> hosts
echo %ip% www.shidonni.com >> hosts
echo All finished!
pause