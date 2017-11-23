$peersnum = 5
$startport = 5000
$nodesdir = "c:\blockchain-nodes\"
$nodeexecutables = "MyBlockchain.Server\bin\Debug"

$cmdargs = @(
    "--peersnum=$peersnum",
    "--startport=$startport"
)



$currentPath = Get-Location

Try {
    $date = (Get-Date).AddMinutes(0).DateTime

    for($i=0; $i -lt $peersnum; $i++) {
        $nodepath = "$nodesdir\node500$i"
        Get-Process | Where-Object {$_.Path -like "*node500$i*"} | Stop-Process
        if(Test-Path $nodepath){
            [system.io.directory]::Delete($nodepath, 1);
        }
        [system.io.directory]::CreateDirectory($nodepath)
        xcopy "$nodeexecutables\*.*" "$nodepath\*"  /F /R /Y /EXCLUDE:"xcopy-excluded-files.txt"
        start-process "$nodepath\MyBlockchain.Server.exe"+" $i"   
    }


    $now1 = (Get-Date).AddMinutes(0).DateTime
	Write-Host ($date + " - " + $now1 )
}
Finally {
	Push-Location -Path $currentPath
}