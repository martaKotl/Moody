# Moody
Desktop application where you can store your mood and emotions during the day

Downloading:
1. Download 'MoodyApp.application' file
2. Go to Database folder and download 'Moody.mdf' file
3. Go to Database -> SQL instalation file and download 'sql2022-ssei-dev.exe' file

Installation and configuration:
1. Run 'sql2022-ssei-dev.exe' file and go through installation process
   - choose 'Basic' installation type
   - don't change file path unless you know what you are doing
2. Copy 'Moody.mdf' file and paste it in 'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA' in File Explorer
3. Open command prompt as administrator and run this command: sqlcmd -S . -E -Q "CREATE DATABASE Moody ON (FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Moody.mdf') FOR ATTACH"
4. Run 'MoodyApp.application' file to install the application
