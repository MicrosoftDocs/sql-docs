---
title: "Back up multiple databases: Azure Blob Storage"
description: This article provides sample scripts that can be used to automate backups in SQL Server to Azure Blob Storage using PowerShell cmdlets.
titleSuffix: "PowerShell"
ms.custom: seo-lt-2019
ms.date: "12/17/2019"
ms.service: sql
ms.reviewer: ""
ms.subservice: backup-restore
ms.topic: conceptual
ms.assetid: f7008339-e69d-4e20-9265-d649da670460
author: MashaMSFT
ms.author: mathoma
---
# Back up Multiple Databases to Azure Blob Storage - PowerShell

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This topic provides sample scripts that can be used to automate backups to Azure Blob Storage using PowerShell cmdlets.  
  
## Overview of PowerShell cmdlets for Backup and Restore

The **Backup-SqlDatabase** and **Restore-SqlDatabase** are the two main cmdlets available to do backup and restore operations.

In addition, there are other cmdlets that may be required to automate backups to Windows Azure Blob storage, like the set of **SqlCredential** cmdlets.

For a list of all available cmdlets, see [SqlServer cmdlets](/powershell/module/sqlserver).
  
> [!TIP]  
> The **SqlCredential** cmdlets are used in Backup and Restore to Azure Blob storage scenarios. For more information on credentials and their use, see [SQL Server Backup and Restore with Microsoft Azure Blob Storage](../../relational-databases/backup-restore/sql-server-backup-and-restore-with-microsoft-azure-blob-storage-service.md).
  
### PowerShell for Multi-Database, Multi-Instance Backup Operations

The following sections include scripts for various operations like creating a SQL credential on multiple instance of SQL Server, backing up all user databases in an instance of SQL Server, and such. You can use these scripts to automate or schedule backup operations according to the requirements of your environment. The scripts provided here are examples, and may be modified or extended for your environment.  
  
The following are considerations for the sample scripts:  
  
- SQL Server PowerShell implements cmdlets to navigate the path structure that represents the hierarchy of objects supported by a PowerShell provider. When you have navigated to a node in the path, you can use other cmdlets to perform basic operations on the current object.

  For more information, see [Navigate SQL Server PowerShell Paths](../../powershell/navigate-sql-server-powershell-paths.md).

- **Get-ChildItem** cmdlet: The information returned by the **Get-ChildItem** depends on the location in a SQL Server PowerShell path. For example, if the location is at the computer level, this cmdlet returns all the SQL Server database engine instances installed on the computer. Or, if the location is at the object level such as databases, then it returns a list of database objects. By default the **Get-ChildItem** cmdlet does not return system objects. Use the `–Force` parameter to see the system objects.

- An Azure storage account and SQL credential are required prerequisites and for all backup and restore operations to Azure Blob Storage.
  
### Create a SQL credential on all instances of SQL Server

The following script can be used to create a generic SQL credential on all the instances of SQL Server on a computer. If there's already an existing credential with the same name on one of the instances of the computer, the script shows the error and continues.  
  
```powershell
# load the sqlps module
import-module sqlps  
  
# set parameters
$sqlPath = "sqlserver:\sql\$($env:COMPUTERNAME)"
$storageAccount = "<myStorageAccount>"  
$storageKey = "<myStorageAccessKey>"  
$secureString = ConvertTo-SecureString $storageKey -AsPlainText -Force  
$credentialName = "myCredential-$(Get-Random)"

Write-Host "Generate credential: " $credentialName
  
#cd to sql server and get instances  
cd $sqlPath
$instances = Get-ChildItem

#loop through instances and create a SQL credential, output any errors
foreach ($instance in $instances)  {
    try {
        $path = "$($sqlPath)\$($instance.DisplayName)\credentials"
        New-SqlCredential -Name $credentialName -Identity $storageAccount -Secret $secureString -Path $path -ea Stop | Out-Null
        Write-Host "...generated credential $($path)\$($credentialName)."  }
    catch { Write-Host $_.Exception.Message } }
```

> [!NOTE]
> The statement `-ea Stop | Out-Null` is used for user-defined exception output. If you prefer default PowerShell error messages, this statement can be removed. 

### Remove a SQL credential from all instances of SQL Server

The following script can be used to remove a specific credential from all the instances of SQL Server installed on the computer. If the credential does not exist on a specific instance, an error message is displayed, and the script continues until all instances are checked.  
  
```powershell
import-module sqlps

$sqlPath = "sqlserver:\sql\$($env:COMPUTERNAME)"
$credentialName = "<myCredential>"

Write-Host "Delete credential: " $credentialName

cd $sqlPath
$instances = Get-ChildItem

#loop through instances and delete a SQL credential
foreach ($instance in $instances)  {
    try {
        $path = "$($sqlPath)\$($instance.DisplayName)\credentials\$($credentialName)"
        Remove-SqlCredential -Path $path -ea Stop | Out-Null
        Write-Host "...deleted credential $($path)."  }
    catch { Write-Host $_.Exception.Message } }
```  
  
### Full backup for all databases

The following script creates backups of all databases on the computer. This includes both user databases and **msdb** system database. The script filters out **tempdb** and **model** system databases.  
  
```powershell
import-module sqlps  

$sqlPath = "sqlserver:\sql\$($env:COMPUTERNAME)"
$storageAccount = "<myStorageAccount>"  
$blobContainer = "<myBlobContainer>"  
$backupUrlContainer = "https://$storageAccount.blob.core.windows.net/$blobContainer/"  
$credentialName = "<myCredential>"

Write-Host "Backup database: " $backupUrlContainer
  
cd $sqlPath
$instances = Get-ChildItem

#loop through instances and backup all databases (excluding tempdb and model)
foreach ($instance in $instances)  {
    $path = "$($sqlPath)\$($instance.DisplayName)\databases"
    $databases = Get-ChildItem -Force -Path $path | Where-object {$_.name -ne "tempdb" -and $_.name -ne "model"}

    foreach ($database in $databases) {
        try {
            $databasePath = "$($path)\$($database.Name)"
            Write-Host "...starting backup: " $databasePath
            Backup-SqlDatabase -Database $database.Name -Path $path -BackupContainer $backupUrlContainer -SqlCredential $credentialName -Compression On
            Write-Host "...backup complete."  }
        catch { Write-Host $_.Exception.Message } } }
```  
  
### Full backup for system databases only on a specific instance of SQL Server

The full script can be used to back up the **master** and the **msdb** databases on a named instance of SQL Server. The same script can be used for any instance by changing the instance parameter value. SQL Server's default instance is named `DEFAULT`.
  
```powershell
import-module sqlps  

$sqlPath = "sqlserver:\sql\$($env:COMPUTERNAME)"
$instanceName = "DEFAULT"
$storageAccount = "<myStorageAccount>"  
$blobContainer = "<myBlobContainer>"  
$backupUrlContainer = "https://$storageAccount.blob.core.windows.net/$blobContainer/"  
$credentialName = "<myCredential>"

Write-Host "Backup database: " $instanceName " to " $backupUrlContainer
  
cd "$($sqlPath)\$($instanceName)"

#loop through instance and backup specific databases
$databases = "master", "msdb"  
foreach ($database in $databases) {
    try {
        Write-Host "...starting backup: " $database
        Backup-SqlDatabase -Database $database -BackupContainer $backupUrlContainer -SqlCredential $credentialName -Compression On
        Write-Host "...backup complete." }
    catch { Write-Host $_.Exception.Message } }
```  
  
## See also

[SQL Server Backup and Restore with Azure Blob Storage](../../relational-databases/backup-restore/sql-server-backup-and-restore-with-microsoft-azure-blob-storage-service.md)

[SQL Server Backup to URL Best Practices and Troubleshooting](../../relational-databases/backup-restore/sql-server-backup-to-url-best-practices-and-troubleshooting.md)
