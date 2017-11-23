$peersnum = 5
$startport = 5000
$nodesdir = "c:\blockchain-nodes\"
$nodeexecutables = "MyBlockchain.Server\bin\Debug"
$nodeterminal = "MyBlockchain.Terminal\bin\Debug"

$cmdargs = @(
    "--peersnum=$peersnum",
    "--startport=$startport"
)



$currentPath = Get-Location

Try {
    $date = (Get-Date).AddMinutes(0).DateTime

    for($i=0; $i -lt $peersnum; $i++) {
        $nodepath = "$nodesdir\node500$i"
        Get-Process | Where-Object {$_.Path -like "*node500$i*"} | Stop-Process | Out-Null
        if(Test-Path $nodepath){
            [system.io.directory]::Delete($nodepath, 1);
        }
        [system.io.directory]::CreateDirectory($nodepath)
        for($j=0; $j -lt $peersnum; $j++) {
            if ($i -ne $j){
                Add-Content -Value "http://127.0.0.1:500$j/blockchain.json" -Path "$nodepath\nodes.txt"        
            }
        }
        xcopy "$nodeexecutables\*.*" "$nodepath\*"  /F /R /Y /EXCLUDE:"xcopy-excluded-files.txt"
        xcopy "$nodeterminal\*.*" "$nodepath\*"  /F /R /Y /EXCLUDE:"xcopy-excluded-files.txt"
        start-process "$nodepath\MyBlockchain.Server.exe" -ArgumentList "500$i" 
    }


    $now1 = (Get-Date).AddMinutes(0).DateTime
	Write-Host ($date + " - " + $now1 )
}
Finally {
	Push-Location -Path $currentPath
}