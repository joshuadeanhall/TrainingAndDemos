sc.exe delete MyService-service1 | Write-Host

Start-Sleep -Second 3

# install is done via TopShelf's command line to install the proper type of server.

& "MessageService.exe" "install" | Write-Host