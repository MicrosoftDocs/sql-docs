---
title: "Enable and configure FILESTREAM | Microsoft Docs"
description: To use FILESTREAM, first enable it on the SQL Server Database Engine instance. Learn how to enable FILESTREAM by using SQL Server Configuration Manager.
ms.custom: ""
ms.date: "08/23/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: filestream
ms.topic: conceptual
helpviewer_keywords: 
  - "FILESTREAM [SQL Server], enabling"
ms.assetid: 78737e19-c65b-48d9-8fa9-aa6f1e1bce73
author: MikeRayMSFT
ms.author: mikeray
---
# Enable and configure FILESTREAM

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  Before you can start to use FILESTREAM, you must enable FILESTREAM on the instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]. This topic describes how to enable FILESTREAM by using SQL Server Configuration Manager.  
  
##  <a name="enabling"></a> Enabling FILESTREAM  
  
#### To enable and change FILESTREAM settings  
  
1.  On the **Start** menu, point to **All Programs**, point to [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)], point to **Configuration Tools**, and then click **SQL Server Configuration Manager**.  
  
2.  In the list of services, right-click **SQL Server Services**, and then click **Open**.  
  
3.  In the **SQL Server Configuration Manager** snap-in, locate the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on which you want to enable FILESTREAM.  
  
4.  Right-click the instance, and then click **Properties**.  
  
5.  In the **SQL Server Properties** dialog box, click the **FILESTREAM** tab.  
  
6.  Select the **Enable FILESTREAM for Transact-SQL access** check box.  
  
7.  If you want to read and write FILESTREAM data from Windows, click **Enable FILESTREAM for file I/O streaming access**. Enter the name of the Windows share in the **Windows Share Name** box.  
  
8.  If remote clients must access the FILESTREAM data that is stored on this share, select **Allow remote clients to have streaming access to FILESTREAM data**.  
  
9. Click **Apply**.  
  
10. In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], click **New Query** to display the Query Editor.  
  
11. In Query Editor, enter the following [!INCLUDE[tsql](../../includes/tsql-md.md)] code:  
  
    ```sql  
    EXEC sp_configure filestream_access_level, 2  
    RECONFIGURE  
    ```  
  
12. Click **Execute**.  
  
13. Restart the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service.  

## <a name="ps_check_enable"></a> Check and enable FILESTREAM using PowerShell  
The following script allows configuring FILESTREAM from PowerShell. ([script file](./codesnippet/powershell/Check_and_Enable_Filestream.ps1))
1. Run PowerShell as Administrator
2. Paste script into the PS window and hit enter
3. The script provides a few options to choose from:  
   ![script_options](./media/ps_enable_fs_script_options.png)  
   1. Option 1 - Shows all instances available on machine where scipt is being executed
   2. Option 2 - Allows to select instance for configuration
   3. Option 3 - Displays current configuration of selected instance
   4. Option 4 - Modifies the instance with settings entered by the user
   5. Option 0 - Terminates the script  
   

 
```powershell
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

```

##  <a name="best"></a> Best practices  
  
###  <a name="config"></a> Physical configuration and maintenance  
 When you set up FILESTREAM storage volumes, consider the following guidelines:  
  
-   Turn off short file names on FILESTREAM computer systems. Short file names take significantly longer to create. To disable short file names, use the Windows **fsutil** utility.  
  
-   Regularly defragment FILESTREAM computer systems.  
  
-   Use 64-KB NTFS clusters. Compressed volumes must be set to 4-KB NTFS clusters.  
  
-   Disable indexing on FILESTREAM volumes and set **disablelastaccess**. To set **disablelastaccess**, use the Windows **fsutil** utility.  
  
-   Disable antivirus scanning of FILESTREAM volumes when it is not necessary. If antivirus scanning is necessary, avoid setting policies that will automatically delete offending files.  
  
-   Set up and tune the RAID level for fault tolerance and the performance that is required by an application.  
  
|RAID level|Write performance|Read performance|Fault tolerance|Remarks|  
|-|-|-|-|-|   
|RAID 5|Normal|Normal|Excellent|Performance is better than one disk or JBOD; and less than RAID 0 or RAID 5 with striping.|  
|RAID 0|Excellent|Excellent|None||  
|RAID 5 + striping|Excellent|Excellent|Excellent|Most expensive option.|  
| &nbsp; | &nbsp; | &nbsp; | &nbsp; | &nbsp; |
  
  
###  <a name="database"></a> Physical database design  
 When you design a FILESTREAM database, consider the following guidelines:  
  
-   FILESTREAM columns must be accompanied by a corresponding **uniqueidentifier**ROWGUID column. These kinds of tables must also be accompanied by a unique index. Typically this index is not a clustered index. If the databases business logic requires a clustered index, you have to make sure that the values stored in the index are not random. Random values will cause the index to be reordered every time that a row is added or removed from the table.  
  
-   For performance reasons, FILESTREAM filegroups and containers should reside on volumes other than the operating system, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] log, tempdb, or paging file.  
  
-   Space management and policies are not directly supported by FILESTREAM. However, you can manage space and apply policies indirectly by assigning each FILESTREAM filegroup to a separate volume and using the volume's management features.  
