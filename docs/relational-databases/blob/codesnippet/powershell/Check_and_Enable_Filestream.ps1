

function Get-SQLInstancesInfo {
# Get Name, Edition and Version of all instances
    $inst = (get-itemproperty 'HKLM:\SOFTWARE\Microsoft\Microsoft SQL Server').InstalledInstances
    $table = foreach ($i in $inst)
    {
        @(
            $p = (Get-ItemProperty 'HKLM:\SOFTWARE\Microsoft\Microsoft SQL Server\Instance Names\SQL').$i
            @{
                InstanceName = $p.Substring($p.IndexOf('.')+1)
                Edition = (Get-ItemProperty "HKLM:\SOFTWARE\Microsoft\Microsoft SQL Server\$p\Setup").Edition
                Version = (Get-ItemProperty "HKLM:\SOFTWARE\Microsoft\Microsoft SQL Server\$p\Setup").Version
            }
        ) | % { New-Object object | Add-Member -NotePropertyMembers $_ -PassThru }
    }
    Write-Host 'INSTANCES FOUND:' -BackgroundColor DarkGreen -ForegroundColor White -NoNewline
    $table | Format-Table InstanceName, Edition, Version -AutoSize
}

function Get-SQLInstanceFileStreamInfo {
    param ($instance)
    try {
        $wmiName = 'root\Microsoft\SqlServer\ComputerManagement' + $instance.version
        $wmi = Get-WmiObject -Namespace $wmiName -Class FilestreamSettings | where {$_.InstanceName -eq $instance.name} 

        Write-Host 'INSTANCE FILESTREAM CONFIGURATION:' -BackgroundColor DarkGreen -ForegroundColor White -NoNewline
        $wmi | Format-Table PSComputerName, InstanceName, ShareName, AccessLevel -AutoSize

        Write-FSAccessLevel
    }
    catch {
        Write-Host 'Error: No instance selected' -BackgroundColor Red -ForegroundColor Yellow
    }
}

function Set-InstanceObject {
    Write-Host 'Select instance to work with.'
    $name = Read-Host 'Enter instance name'
    $version = Read-Host -Prompt 'Enter Major Version Number (ex. for 14.0.1000.169, enter: 14)'
    
    $name = $name.ToUpper()
    # Full instance name is required to restart the DB Engine
    $def_inst = if($name -eq 'MSSQLSERVER'){$true} else{$false}
    $fullname = if($def_inst){$instance} else{'MSSQL$'+$name}

    if ($name.Substring(0,6) -eq 'MSSQL$') {
        $fullname = $name
        $name = $name.Substring(7, [System.Math]::$name.length-7)
    }

    $instance = New-Object -TypeName psobject
    $instance | Add-Member -NotePropertyName 'Name' -NotePropertyValue $name
    $instance | Add-Member -NotePropertyName 'FullName' -NotePropertyValue $fullname
    $instance | Add-Member -NotePropertyName 'Version' -NotePropertyValue $version

    Clear-Host

    Return $instance
}

function Write-FSAccessLevel {
# prints options for access level
    $accsLvl = @(
        @{AccessLevel='0';FILESTREAM='disabled';FileIO='disabled';RemoteClientAccess='disabled'},
        @{AccessLevel='1';FILESTREAM='enabled';FileIO='disabled';RemoteClientAccess='disabled'},
        @{AccessLevel='2';FILESTREAM='enabled';FileIO='enabled';RemoteClientAccess='disabled'},
        @{AccessLevel='3';FILESTREAM='enabled';FileIO='enabled';RemoteClientAccess='enabled'}
    ) | % { New-Object object | Add-Member -NotePropertyMembers $_ -PassThru }

    Write-Host 'ACCESS LEVEL OPTIONS:' -BackgroundColor DarkGreen -ForegroundColor White -NoNewline
    $accsLvl | Format-Table AccessLevel, FILESTREAM, FileIO, RemoteClientAccess
}

function Set-InstanceFileStream {
    param ($instance, $accessLevel, $shareName)

    try {
        $mcn = (get-item env:\computername).Value
        $fullInstance = $mcn + '\' + $instance.Name

        $wmiName = 'root\Microsoft\SqlServer\ComputerManagement' + $instance.Version
        $wmi = Get-WmiObject -Namespace $wmiName -Class FilestreamSettings | where {$_.InstanceName -eq $instance.Name}
        
        $wmi.EnableFilestream($accessLevel, $shareName)
        Write-Host 'Restaring SQL Server Instance to apply settings' -BackgroundColor DarkYellow -ForegroundColor Black
        Get-Service -Name $instance.FullName | Restart-Service -Force
        Start-Sleep -Seconds 3
        
        Set-ExecutionPolicy RemoteSigned
        Import-Module "sqlps" -DisableNameChecking        

        Write-Host 'Checking if current user is sysadmin...' -BackgroundColor DarkYellow -ForegroundColor Black

        $sysadmin = Invoke-Sqlcmd "SELECT IS_SRVROLEMEMBER ('sysadmin') AS IsSysAdmin" -ServerInstance $fullInstance
        if ($sysadmin.IsSysAdmin -eq 0) {
            Write-Host 'The current user is not a sysadmin. Provide sysadmin credentials!' -BackgroundColor DarkYellow -ForegroundColor Black
            $user = Read-Host -Prompt 'UserName'
            $securePwd = Read-Host -Prompt 'Password' -AsSecureString
            $plainPwd =[Runtime.InteropServices.Marshal]::PtrToStringAuto([Runtime.InteropServices.Marshal]::SecureStringToBSTR($securePwd))

            Write-Host 'Configuring Server Instance' -BackgroundColor DarkYellow -ForegroundColor Black
            Invoke-Sqlcmd "EXEC sp_configure filestream_access_level, 2" -ServerInstance $fullInstance -Username $user -Password $plainPwd
            Invoke-Sqlcmd "RECONFIGURE" -ServerInstance $fullInstance -Username $user -Password $plainPwd
        }
        else {
            Write-Host 'Configuring Server Instance' -BackgroundColor DarkYellow -ForegroundColor Black

            Invoke-Sqlcmd "EXEC sp_configure filestream_access_level, 2" -ServerInstance $fullInstance
            Invoke-Sqlcmd "RECONFIGURE" -ServerInstance $fullInstance
        }

        Write-Host 'SQL Server Instance' $instance.name 'has been configured for FILESTREAM.' -BackgroundColor DarkGreen -ForegroundColor White
    }
    catch {
        Write-Host 'Error occured. Make sure you run PowerShell as Administrator.' -BackgroundColor DarkRed -Foreground White
        Read-Host 'Press ENTER to continue...'
    }    
}

# Main
$instance = $null
$option = 1
$options = @(
    @{ Option='1'; Description='Show all installed instances' },
    @{ Option='2'; Description='Select/Change instance' },
    @{ Option='3'; Description='Show instance FILESTREAM configuration' },
    @{ Option='4'; Description='Setup instance FILESTREAM options' },
    @{ Option='0'; Description='Exit' }
) | % { New-Object System.Object | Add-Member -NotePropertyMembers $_ -PassThru }

Clear-Host

while ($option -ne 0) {
    if ($null -eq $instance) {
        Write-Host 'NO INSTANCE SELECTED.' -BackgroundColor DarkRed -ForegroundColor White -NoNewline
    } else {
        Write-Host 'SELECTED INSTANCE:' -BackgroundColor DarkGreen -ForegroundColor White -NoNewline
        $instance | Format-List
    }
    
    # show option and ask user for input
    $options | Format-Table -AutoSize

    $option = Read-Host -Prompt 'Select option'

    # run selected option
    switch ($option) {
        1 { Get-SQLInstancesInfo }
        2 { $instance = Set-InstanceObject }
        3 { 
            if ($null -eq $instance) {
                $instance = Set-InstanceObject
            }
            Clear-Host; Get-SQLInstanceFileStreamInfo -instance $instance 
        }
        4 { 
            if ($null -eq $instance) {
                $instance = Set-InstanceObject
            }
            Write-FSAccessLevel
            $lvl = Read-Host -Prompt 'Select access level to setup'
            if ($lvl -gt 1) {
                $shn = Read-Host -Prompt 'Enter share name'    
            }            
            Set-InstanceFileStream -instance $instance -accessLevel $lvl -shareName $shn
        }
        0 { Return }
    }
}
