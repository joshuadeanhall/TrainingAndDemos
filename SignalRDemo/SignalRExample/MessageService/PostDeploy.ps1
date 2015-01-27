sc.exe delete MessageService | Write-Host

"Service is deleted." | Write-Host

Start-Sleep -s 3

# install is done via TopShelf's command line to install the proper type of server.

& ".\MessageService.exe" "install" | Write-Host

Start-Sleep -s 3

Start-Service MessageService | Write-Host