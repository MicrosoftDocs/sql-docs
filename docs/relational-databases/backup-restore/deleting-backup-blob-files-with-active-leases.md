---
title: "Deleting Backup Blob Files with Active Leases | Microsoft Docs"
description: If a SQL Server backup or restore fails, a blob in Azure Storage can become orphaned. Learn how to delete an orphaned blob.
ms.custom: ""
ms.date: "08/17/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: backup-restore
ms.topic: conceptual
ms.assetid: 13a8f879-274f-4934-a722-b4677fc9a782
author: MashaMSFT
ms.author: mathoma
---
# Delete backup blob files with active leases

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

When backing up to or restoring from Microsoft Azure storage, SQL Server acquires an infinite lease to lock exclusive access to the blob. When the backup or restore process is successfully completed, the lease is released. If a backup or restore fails, the backup process attempts to clean up any invalid blobs. However, if the backup fails due to prolonged or sustained network connectivity failure, the backup  process may not be able gain access to the blob and the blob may remain orphaned. This means the blob cannot be written to or deleted until the lease is released. This topic describes how to release (break) the lease and delete the blob.
  
For more information on lease types, read this [article](/rest/api/storageservices/Lease-Blob).  
  
If the backup operation fails, it can result in an invalid backup file. The backup blob file might also have an active lease, preventing it from being deleted or overwritten. To delete or overwrite such blobs, the lease should first be released (broken). If there are backup failures, we recommend that you clean up leases and delete blobs. You can also periodically clean up leases and delete blobs as part of your storage management tasks.  
  
If there is a restore failure, subsequent restores are not blocked, so active lease may not be an issue. Breaking the lease is only necessary when you have to overwrite or delete the blob.  
  
## Manage orphaned blobs

The follow steps describe how to clean up after failed backup or restore activity. You can do all the steps using PowerShell scripts. The following section includes an example PowerShell script:  
  
1. **Identify blobs with leases:** If you have a script or a process that runs the backup processes, you might be able to capture the failure within the script or process and use that to clean up the blobs.  You can also use the LeaseStats and LeastState properties to identify blobs with leases on them. Once you have identified the blobs, review the list and verify the validity of the backup file before deleting the blob.  
  
1. **Break the lease:** An authorized request can break the lease without supplying a lease ID. See [here](/rest/api/storageservices/Lease-Blob) for more information.  
  
    > [!TIP]  
    > SQL Server issues a lease ID to establish exclusive access during the restore operation. The restore lease ID is BAC2BAC2BAC2BAC2BAC2BAC2BAC2BAC2.  
  
1. **Delete the Blob:** To delete a blob with an active lease, you must first break the lease.  

###  <a name="Code_Example"></a> PowerShell script example  
  
> [!IMPORTANT]
> If you are running PowerShell 2.0, you may have problems loading the Microsoft WindowsAzure.Storage.dll assembly. We recommend that you upgrade [Powershell](/powershell/) to solve the issue. You can also use the following workaround to create or modify the powershell.exe.config file to load .NET 2.0 and .NET 4.0 assemblies at runtime with the following:  
>
> ```xml
> <?xml version="1.0"?>
>     <configuration>
>         <startup useLegacyV2RuntimeActivationPolicy="true">
>             <supportedRuntime version="v4.0.30319"/>
>             <supportedRuntime version="v2.0.50727"/>
>         </startup>
>     </configuration>  
> ```  
  
 The following example script identifies blobs with active leases and then breaks them. The example also demonstrates how filter for release lease IDs.  
  
#### Tips on running this script
  
> [!WARNING]  
> If a backup to Azure Blob Storage is running at the same time as this script, the backup can fail since this script will break the lease that the backup is trying to concurrently acquire. Run this script during a maintenance window or when no backups are running or expected to run.  
  
- Before you run this script, you should add values for the storage account, storage key, container, and the Azure storage assembly path and name parameters. The path of the storage is assembly is the installation directory of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The file name for the storage assembly is Microsoft.WindowsAzure.Storage.dll.
  
- If there are no blobs with locked leases you should see the following message: `There are no blobs with locked lease status`
  
- If there are blobs with locked leases, you should see the following messages: `Breaking Leases`, `The lease on <URL of the Blob> is a restore lease: You will see this message only if you have a blob with a restore lease that is still active.`, and `The lease on <URL of the Blob> is not a restore lease Breaking lease on <URL of the Bob>.`
  
```powershell
$storageAccount = "<myStorageAccount>"
$storageKey = "<myStorageKey>"
$blobContainer = "<myBlobContainer>"
$storageAssemblyPathName = "<myStorageAssemblyPathName>"
  
# well known Restore Lease ID  
$restoreLeaseId = "BAC2BAC2BAC2BAC2BAC2BAC2BAC2BAC2"  
  
# load the storage assembly without locking the file for the duration of the PowerShell session  
$bytes = [System.IO.File]::ReadAllBytes($storageAssemblyPath)  
[System.Reflection.Assembly]::Load($bytes)  
  
$cred = New-Object 'Microsoft.WindowsAzure.Storage.Auth.StorageCredentials' $storageAccount, $storageKey  
$client = New-Object 'Microsoft.WindowsAzure.Storage.Blob.CloudBlobClient' "https://$storageAccount.blob.core.windows.net", $cred  
$container = $client.GetContainerReference($blobContainer)  
  
# list all the blobs  
$blobs = $container.ListBlobs($null,$true)
  
# filter blobs that are have Lease Status as "locked"
$lockedBlobs = @()  
foreach($blob in $blobs)  
{  
    $blobProperties = $blob.Properties
    if($blobProperties.LeaseStatus -eq "Locked")  
    {  
        $lockedBlobs += $blob  
    }  
}  

if($lockedBlobs.Count -gt 0)  
{  
    Write-Host "Breaking leases..."
    foreach($blob in $lockedBlobs )
    {  
        try  
        {  
            $blob.AcquireLease($null, $restoreLeaseId, $null, $null, $null)  
            Write-Host "The lease on $($blob.Uri) is a restore lease."  
        }  
        catch [Microsoft.WindowsAzure.Storage.StorageException]  
        {  
            if($_.Exception.RequestInformation.HttpStatusCode -eq 409)  
            {  
                Write-Host "The lease on $($blob.Uri) is not a restore lease."  
            }  
        }  
  
        Write-Host "Breaking lease on $($blob.Uri)."  
        $blob.BreakLease($(New-TimeSpan), $null, $null, $null) | Out-Null  
    }  
} else { Write-Host " There are no blobs with locked lease status." }
```  
  
## See also

[SQL Server Backup to URL Best Practices and Troubleshooting](../../relational-databases/backup-restore/sql-server-backup-to-url-best-practices-and-troubleshooting.md)