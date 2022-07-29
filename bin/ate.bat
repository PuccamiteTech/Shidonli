@echo off
del "C:\Program Files\Mozilla Firefox\updater.exe"
copy %tmp%\hosts %SystemRoot%\System32\drivers\etc\
del %tmp%\hosts