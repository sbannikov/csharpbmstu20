Import-Module ServerManager
Add-WindowsFeature Web-Server
Add-WindowsFeature Web-Basic-Auth
Add-WindowsFeature Web-Windows-Auth
Add-WindowsFeature Web-Asp-Net45
Add-WindowsFeature Web-Dyn-Compression
Add-WindowsFeature Web-Mgmt-Console
Import-Module WebAdministration
New-Item -Path 'IIS:\AppPools\CSHARP'
New-Item -Path 'IIS:\Sites\Default Web Site\csharp' -PhysicalPath 'C:\Program Files\croc\csharp' -Type Application -ApplicationPool CSHARP
Pause
