---
title: "Back up Multiple Databases to Azure Blob Storage - PowerShell | Microsoft Docs"
ms.custom: ""
ms.date: "05/20/2016"
ms.prod: sql
ms.prod_service: backup-restore
ms.reviewer: ""
ms.technology: backup-restore
ms.topic: conceptual
ms.assetid: f7008339-e69d-4e20-9265-d649da670460
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Back up Multiple Databases to Azure Blob Storage - PowerShell
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

  This topic provides sample scripts that can be used to automate backups to Windows Azure Blob storage service using PowerShell cmdlets.  
  
## Overview of PowerShell cmdlets for Backup and Restore  
 The **Backup-SqlDatabase** and **Restore-SqlDatabase** are the two main cmdlets available to do backup and restore operations. In addition, there are other cmdlets that may be required to automate backups to Windows Azure Blob storage like the set of **SqlCredential** cmdlets  Following is a list of PowerShell cmdlets available in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] that are used in backup and restore operations:  
  
 Backup-SqlDatabase  
 This cmdlet is used to create a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Backup.  
  
 Restore-SqlDatabase  
 Used to restore a database.  
  
 New-SqlCredential  
 This cmdlet is used to create a SQL Credential to use for SQL Server Backup to Windows Azure Storage. For more information on credentials and their use in SQL Server Backup and Restore, see [SQL Server Backup and Restore with Microsoft Azure Blob Storage Service](../../relational-databases/backup-restore/sql-server-backup-and-restore-with-microsoft-azure-blob-storage-service.md).  
  
 Get-SqlCredential  
 This cmdlet is used to retrieve the Credential object and its properties.  
  
 Remove-SqlCredential  
 Delete a SQL Credential object  
  
 Set-SqlCredential  
 This cmdlet is used to change or set the properties of the SQL Credential Object.  
  
> [!TIP]  
>  The Credential cmdlets are used in Backup and Restore to Windows Azure Blob storage scenarios.  
  
### PowerShell for Multi-Database, Multi-Instance Backup Operations  
 The following sections include scripts for various operations like creating a SQL Credential on multiple instance of SQL Server, backing up all user databases in an instance of SQL Server, and such. You can use these scripts to automate or schedule backup operations according to the requirements of your environment. The scripts provided here are examples, and may be modified or extended for your environment.  
  
 The following are considerations for the sample scripts:  
  
1.  **Navigating SQL Server PowerShell paths:** Windows PowerShell implements cmdlets to navigate the path structure that represents the hierarchy of objects supported by a PowerShell provider. When you have navigated to a node in the path, you can use other cmdlets to perform basic operations on the current object.  
  
2.  **Get-ChildItem** cmdlet: The information returned by the **Get-ChildItem** depends on the location in a SQL Server PowerShell path. For example, if the location is at the computer level, this cmdlet returns all the SQL Server database engine instances installed on the computer. As another example, if the location is at the object level such as databases, then this cmdlet returns a list of database objects.  By default the **Get-ChildItem** cmdlet does not return system objects.  Using the -Force parameter you can see the system objects.  
  
     For more information, see [Navigate SQL Server PowerShell Paths](../../relational-databases/scripting/navigate-sql-server-powershell-paths.md).  
  
3.  Although each code sample can be tried independently by changing the variable values, creating a Windows Azure Storage Account and a SQL Credential are prerequisites and required for all backup and restore operations to Windows Azure Blob storage service.  
  
### Create a SQL Credential on All the Instances of SQL Server  
 There are two sample scripts, and both create a SQL Credential "mybackupToURL" on all the instances of SQL Server on a computer. The first example creates is simple and creates the credential and does not trap exceptions.  For example, if there was already an existing credential with the same name on one of the instances of the computer, the script would fail. The second example traps errors and allows the script to continue.  
  
```  
  
import-module sqlps  
  
# create variables  
$storageAccount = "mystorageaccount"  
$storageKey = "<storageaccesskeyvalue>"  
$secureString = convertto-securestring $storageKey  -asplaintext -force  
$credentialName = "mybackuptoURL"  
  
#cd to computer level  
cd SQLServer:\SQL\$env:COMPUTERNAME  
# get the list of instances  
$instances = Get-childitem  
#pipe the instances to new-sqlcredentail cmdlet to create SQL credential  
$instances | new-sqlcredential -Name $credentialName  -Identity $storageAccount -Secret $secureString  
```  
  
```  
  
import-module sqlps  
  
# set the parameter values  
  
$storageAccount = "mystorageaccount"  
$storageKey = "<storageaccesskeyvalue>"  
$secureString = convertto-securestring $storageKey  -asplaintext -force  
$credentialName = "mybackuptoURL"  
  
#cd to computer level  
cd SQLServer:\SQL\$env:COMPUTERNAME  
# get the list of instances  
$instances = Get-childitem  
#loop through instances and create a SQL credential, output any errors  
foreach ($instance in $instances)  
{  
    Try  
{  
     new-sqlcredential -Name $credentialName -path "SQLServer:\SQL\$($instance.name)" -Identity $storageAccount -Secret $secureString -ea Stop   
}  
Catch [Exception]  
    {  
  
            write-host "instance - $($instance.name): "$_.Exception.Message  
  
    }  
  
 }  
  
```  
  
### Remove A SQL Credential from All the Instances of SQL Server  
 This script can be used to remove a specific credential from all the instances of SQL Server installed on the computer. If the Credential object does not exist on a specific instance, an error message is displayed, and the script continues until all instances are checked.  
  
```  
  
import-module sqlps  
  
cd SQLServer:\SQL\$env:COMPUTERNAME  
$credentialName = "mybackuptoURL"  
  
$instances = Get-childitem  
  
foreach ($instance in $instances)  
   {   
    try  
        {  
            $path = "SQLServer:\SQL\$($instance.name)\credentials\$credentialName"   
            Remove-sqlCredential -path $path -ea stop   
         }  
         catch [Exception]  
         {  
            write-host $_.Exception.Message  
  
        }  
  
    }  
```  
  
### Full Database Backup for all Databases (INCLUDING SYSTEM DATABASES)  
 The following script creates backups of all databases on the computer. This includes both user databases and **msdb** system database. The script filters out **tempdb** and **model** system databases.  
  
```  
  
import-module sqlps  
# set the parameter values  
$storageAccount = "mystorageaccount"  
$blobContainer = "privatecontainertest"  
$backupUrlContainer = "https://$storageAccount.blob.core.windows.net/$blobContainer/"  
$credentialName = "mybackuptoURL"  
  
# cd to computer level  
  
cd SQLServer:\SQL\$env:COMPUTERNAME  
$instances = Get-childitem   
# loop through each instances and backup up all the  databases -filter out tempdb and model databases  
  
 foreach ($instance in $instances)  
 {  
   $path = "sqlserver:\sql\$($instance.name)\databases"  
   $alldatabases = get-childitem -Force -path $path |Where-object {$_.name -ne "tempdb" -and $_.name -ne "model"}   
  
   $alldatabases | Backup-SqlDatabase -BackupContainer $backupUrlContainer -SqlCredential $credentialName -Compression On -script   
 }  
  
```  
  
### Full Database Backup for ALL User Databases  
 The following script can be used to back up all the user databases on all the instances of SQL Server on the computer.  
  
```  
  
import-module sqlps   
  
$storageAccount = "mystorageaccount"  
$blobContainer = "privatecontainertest"  
$backupUrlContainer = "https://$storageAccount.blob.core.windows.net/$blobContainer/"  
$credentialName = "mybackuptoURL"  
  
# cd to computer level  
  
cd SQLServer:\SQL\$env:COMPUTERNAME  
$instances = Get-childitem   
# loop through each instances and backup up all the user databases  
 foreach ($instance in $instances)  
 {  
    $databases = dir "sqlserver:\sql\$($instance.name)\databases"  
   $databases | Backup-SqlDatabase -BackupContainer $backupUrlContainer -SqlCredential $credentialName -Compression On   
 }  
```  
  
### Full Database Backup for MASTER and MSDB (SYSTEM DATABASES) On All the Instances of SQL Server  
 The following script can be used to back up **master** and **msdb** databases on all the instances of SQL Server installed on the computer.  
  
```  
  
import-module sqlps  
  
$storageAccount = "mystorageaccount"  
$blobContainer = "privatecontainertest"  
$backupUrlContainer = "https://$storageAccount.blob.core.windows.net/$blobContainer/"  
$credentialName = "mybackupToUrl"  
$sysDbs = "master", "msdb"  
  
#cd to computer level  
cd SQLServer:\SQL\$env:COMPUTERNAME  
$instances = Get-childitem   
foreach ($instance in $instances)  
 {  
      foreach ($s in $sysdbs)  
     {  
Backup-SqlDatabase -Database $s -path "sqlserver:\sql\$($instance.name)" -BackupContainer  $backupUrlContainer -SqlCredential $credentialName -Compression On   
}    
 }  
  
```  
  
### Full Database Backup for All User Databases on an Instance of SQL Server  
 The following script is used to back up only the user databases available on a named instance of SQL Server. The same script can be used for the default instance on the computer by changing the $srvPath parameter value.  
  
```  
  
import-module sqlps  
  
$storageAccount = "mystorageaccount"  
$blobContainer = "privatecontainertest"  
$backupUrlContainer = "https://$storageAccount.blob.core.windows.net/$blobContainer/"  
$srvPath = "SQLServer:\SQL\$env:COMPUTERNAME\INSTANCENAME"  # for default instance, the $srvpath variable is "SQLServer:\SQL\$env:COMPUTERNAME\DEFAULT"  
$credentialName = "mybackupToUrl"  
  
#cd to computer level  
cd SQLServer:\SQL\$env:COMPUTERNAME  
  
#retrieves the database objects for a specific instance  
$databases = dir $srvPath\databases  
  
#backup all the user databases  
$databases | Backup-SqlDatabase -BackupContainer $backupUrlContainer -SqlCredential $credentialName -Compression On  
```  
  
### Full Database Backup for Only System Databases (MASTER AND MSDB) On an Instance of SQL Server  
 The full script can be used to back up the **master** and the **msdb** databases on a named instance of SQL Server. The same script can be used for the default instance on the computer by changing the $srvpath parameter value.  
  
```  
  
import-module sqlps  
  
$storageAccount = "mystorageaccount"  
$blobContainer = "privatecontainertest"  
$backupUrlContainer = "https://$storageAccount.blob.core.windows.net/$blobContainer/"  
$srvPath = "SQLServer:\SQL\$env:COMPUTERNAME\INSTANCENAME" # for default instance, the $srvpath variable is "SQLServer:\SQL\$env:COMPUTERNAME\DEFAULT"  
$credentialName = "mybackupToUrl"  
  
#navigate to instance level  
cd $srvPath  
  
$sysDbs = "master", "msdb"  
foreach ($s in $sysDbs)   
{  
Backup-SqlDatabase -Database $s -BackupContainer $backupUrlContainer -SqlCredential $credentialName -Compression On  
}  
  
```  
  
## See Also  
 [SQL Server Backup and Restore with Microsoft Azure Blob Storage Service](../../relational-databases/backup-restore/sql-server-backup-and-restore-with-microsoft-azure-blob-storage-service.md)   
 [SQL Server Backup to URL Best Practices and Troubleshooting](../../relational-databases/backup-restore/sql-server-backup-to-url-best-practices-and-troubleshooting.md)  
  
  
