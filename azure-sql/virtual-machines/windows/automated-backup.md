---
title: Automated Backup for SQL Server 2016 and later
description: This article explains the Automated Backup feature for SQL Server 2016 and later installed to SQL Server on Azure VMs. This article is specific to VMs using the Resource Manager.
author: AbdullahMSFT
ms.author: amamun
ms.reviewer: mathoma
ms.date: 09/27/2024
ms.service: azure-vm-sql-server
ms.subservice: backup
ms.topic: how-to
ms.custom: devx-track-azurepowershell
tags: azure-resource-manager
---

# Automated Backup for SQL Server on Azure VMs
[!INCLUDE[appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

> [!div class="op_single_selector"]
> * [SQL Server 2014](automated-backup-sql-2014.md)
> * [SQL Server 2016 and later](automated-backup.md)

Automated Backup automatically configures [Managed Backup to Microsoft Azure](/sql/relational-databases/backup-restore/sql-server-managed-backup-to-microsoft-azure) for all existing and new databases on SQL Server on Azure VMs starting with SQL Server 2016 or later Standard, Enterprise, or Developer editions. This enables you to configure regular database backups that utilize durable Azure Blob Storage. 


## Prerequisites

To use Automated Backups for your SQL Server on Azure VM, you need: 

 - A SQL Server on Azure VMs registered with the [SQL IaaS Agent extension](sql-server-iaas-agent-extension-automate-management.md).
 - Windows Server 2012 R2 or later.
 - SQL Server 2016 and later. For SQL Server 2014, see [Automated Backup for SQL Server 2014](automated-backup-sql-2014.md).
- **Database configuration**:
   - Target _user_ databases must use the full recovery model. System databases don't have to use the full recovery model. However, if you require log backups to be taken for `model` or `msdb`, you must use the full recovery model. For more information about the impact of the full recovery model on backups, see [Backup under the full recovery model](/previous-versions/sql/sql-server-2008-r2/ms190217(v=sql.105)). 
   - The SQL Server VM has been registered with the [SQL IaaS Agent extension](sql-server-iaas-agent-extension-automate-management.md) and the **Automated Backup** feature is enabled. Since Automated Backup relies on the extension, Automated Backup is only supported on target databases from the default instance, or a single named instance. If there's no default instance, and multiple named instances, the SQL IaaS Agent extension fails and Automated Backup won't work. 
   - If you're running automated backups on a secondary Always On availability group replica, the replica must be **Readable** for the backups to succeed. 

## Settings

The following table describes the options that can be configured for Automated Backup. The actual configuration steps vary depending on whether you use the Azure portal or Azure Windows PowerShell commands. Automated Backup uses [backup compression](/sql/database-engine/configure-windows/view-or-configure-the-backup-compression-default-server-configuration-option) by default and it can't be disabled.

### Basic Settings

| Setting | Range (Default) | Description |
| --- | --- | --- |
| **Automated Backup** | Enable/Disable (Disabled) | Enables or disables Automated Backup for an Azure VM running SQL Server 2016 or later Developer, Standard, or Enterprise. |
| **Retention Period** | 1-90 days (90 days) | The number of days the service retains backup metadata in `msdb`. After the retention period expires for a backup, the metadata is deleted from `msdb`, but files aren't deleted from the storage container. You can use a [lifecycle management policy](/azure/storage/blobs/lifecycle-management-policy-configure) for your storage account to balance backup retention with cost management according to your business needs.    |
| **Storage Account** | Azure storage account | An Azure storage account to use for storing Automated Backup files in blob storage. A container is created at this location to store all backup files. The backup file naming convention includes the date, time, and database GUID. |
| **Encryption** |Enable/Disable (Disabled) | Enables or disables backup encryption. When backup encryption is enabled, the certificates used to restore the backup are located in the specified storage account in the same `automaticbackup` container using the same naming convention. If the password changes, a new certificate is generated with that password, but the old certificate remains to restore prior backups. |
| **Password** |Password text | A password for encryption keys. This password is only required if encryption is enabled. In order to restore an encrypted backup, you must have the correct password and related certificate that was used at the time the backup was taken. |

### Advanced Settings

| Setting | Range (Default) | Description |
| --- | --- | --- |
| **System Database Backups** | Enable/Disable (Disabled) | When enabled, this feature also backs up the system databases: `master`, `msdb`, and `model`. For the `msdb` and `model` databases, verify that they are in the full recovery model if you want log backups to be taken. Log backups are never taken for `master`, and no backups are taken for `tempdb`. |
| **Backup Schedule** | Manual/Automated (Automated) | By default, the backup schedule is automatically determined based on the log growth. Manual backup schedule allows the user to specify the time window for backups. In this case, backups only take place at the specified frequency and during the specified time window of a given day. |
| **Full backup frequency** | Daily/Weekly | Frequency of full backups. In both cases, full backups begin during the next scheduled time window. When weekly is selected, backups could span multiple days until all databases have successfully backed up. |
| **Full backup start time** | 00:00 – 23:00 (01:00) | Start time of a given day during which full backups can take place. |
| **Full backup time window** | 1 – 23 hours (1 hour) | Duration of the time window of a given day during which full backups can take place. |
| **Log backup frequency** | 5 – 60 minutes (60 minutes) | Frequency of log backups. |

> [!NOTE]
> The Automated backup schedule takes backups based on log growth. Using Automated Backups when your databases are in [simple recovery mode](/sql/relational-databases/backup-restore/recovery-models-sql-server) is not recommended. 

## Understanding full backup frequency

It's important to understand the difference between daily and weekly full backups. Consider the following two example scenarios.

### Scenario 1: Weekly backups

You have a SQL Server VM that contains a number of large databases.

On Monday, you enable Automated Backup with the following settings:

- Backup schedule: **Manual**
- Full backup frequency: **Weekly**
- Full backup start time: **01:00**
- Full backup time window: **1 hour**

This means that the next available backup window is Tuesday at 1 AM for 1 hour. At that time, Automated Backup begins backing up your databases one at a time. In this scenario, your databases are large enough that full backups complete for the first couple databases. However, after one hour not all of the databases have been backed up.

When this happens, Automated Backup begins backing up the remaining databases the next day, Wednesday at 1 AM for one hour. If not all databases have been backed up in that time, it tries again the next day at the same time. This continues until all databases have been successfully backed up.

After it reaches Tuesday again, Automated Backup begins backing up all databases again.

This scenario shows that Automated Backup only operates within the specified time window, and each database is backed up once per week. This also shows that it's possible for backups to span multiple days in the case where it isn't possible to complete all backups in a single day.

### Scenario 2: Daily backups

You have a SQL Server VM that contains a number of large databases.

On Monday, you enable Automated Backup with the following settings:

- Backup schedule: Manual
- Full backup frequency: Daily
- Full backup start time: 22:00
- Full backup time window: 6 hours

This means that the next available backup window is Monday at 10 PM for 6 hours. At that time, Automated Backup begins backing up your databases one at a time.

Then, on Tuesday at 10 for 6 hours, full backups of all databases start again.


> [!IMPORTANT]
> Backups happen sequentially during each interval. For instances with a large number of databases, schedule your backup interval with enough time to accommodate all backups. If backups cannot complete within the given interval, some backups might be skipped, and your time between backups for a single database might be higher than the configured backup interval time, which could negatively impact your restore point objective (RPO). 

## Configure new VMs

Use the Azure portal to configure Automated Backup when you create a new SQL Server 2016 or later machine in the Resource Manager deployment model.

In the **SQL Server settings** tab, select **Enable** under **Automated Backup**. 
When you enable Automated Backup, you can configure the following settings:

* Retention period for backups (up to 90 days)
* Storage account, and storage container, to use for backups
* Encryption option and password for backups
* Backup system databases
* Configure backup schedule

To encrypt the backup, select **Enable**. Then specify the **Password**. Azure creates a certificate to encrypt the backups and uses the specified password to protect that certificate. 

Choose **Select Storage Container** to specify the container where you want to store your backups. 

By default the schedule is set automatically, but you can create your own schedule by selecting **Manual**, which allows you to configure the backup frequency, backup time window, and the log backup frequency in minutes.

The following Azure portal screenshot shows the **Automated Backup** settings when you create a new SQL Server VM:

:::image type="content" source="./media/automated-backup/automated-backup-blade.png" alt-text="Screenshot of Automated Backup configuration in the Azure portal.":::


## Configure existing VMs

For existing SQL Server virtual machines, go to the [SQL virtual machines resource](manage-sql-vm-portal.md#access-the-resource) and then select **Backups** to configure your Automated Backups. 

Select **Enable** to configure your Automated Backup settings. 

You can configure the retention period (up to 90 days), the container for the storage account where you want to store your backups, as well as the encryption, and the backup schedule. By default, the schedule is automated.

:::image type="content" source="./media/automated-backup/sql-server-configuration.png" alt-text="Screenshot of Automated Backup for existing VMs in the portal.":::


If you want to set your own backup schedule, choose **Manual** and configure the backup frequency, whether or not you want system databases backed up, and the transaction log backup interval in minutes. 

:::image type="content" source="./media/automated-backup/configure-manual-backup-schedule.png" alt-text="Screenshot of selecting manual to configure your own backup schedule.":::

When finished, select the **Apply** button on the bottom of the **Backups** settings page to save your changes.

If you're enabling Automated Backup for the first time, Azure configures the SQL Server IaaS Agent in the background. During this time, the Azure portal might not show that Automated Backup is configured. Wait several minutes for the agent to be installed, configured. After that, the Azure portal will reflect the new settings.

## Configure with PowerShell

You can use PowerShell to configure Automated Backup. Before you begin, you must:

- [Download and install the latest Azure PowerShell](https://aka.ms/webpi-azps).
- Open Windows PowerShell and associate it with your account with the **Connect-AzAccount** command.

[!INCLUDE [updated-for-az.md](../../includes/updated-for-az.md)]

### Install the SQL Server IaaS Extension
If you provisioned a SQL Server virtual machine from the Azure portal, the SQL Server IaaS Extension should already be installed. You can determine whether it's installed for your VM by calling **Get-AzVM** command and examining the **Extensions** property.

```powershell
$vmname = "yourvmname"
$resourcegroupname = "yourresourcegroupname"

(Get-AzVM -Name $vmname -ResourceGroupName $resourcegroupname).Extensions 
```

If the SQL Server IaaS Agent extension is installed, you should see it listed as "SqlIaaSAgent" or "SQLIaaSExtension." **ProvisioningState** for the extension should also show "Succeeded." 

If it isn't installed or it has failed to be provisioned, you can install it with the following command. In addition to the VM name and resource group, you must also specify the region (**$region**) that your VM is located in.

```powershell
$region = "EASTUS2"
Set-AzVMSqlServerExtension -VMName $vmname `
    -ResourceGroupName $resourcegroupname -Name "SQLIaasExtension" `
    -Version "2.0" -Location $region 
```

### <a id="verifysettings"></a> Verify current settings
If you enabled Automated Backup during provisioning, you can use PowerShell to check your current configuration. Run the **Get-AzVMSqlServerExtension** command and examine the **AutoBackupSettings** property:

```powershell
(Get-AzVMSqlServerExtension -VMName $vmname -ResourceGroupName $resourcegroupname).AutoBackupSettings
```

You should get output similar to the following:

```
Enable                      : True
EnableEncryption            : False
RetentionPeriod             : 30
StorageUrl                  : https://test.blob.core.windows.net/
StorageAccessKey            :  
Password                    : 
BackupSystemDbs             : False
BackupScheduleType          : Manual
FullBackupFrequency         : WEEKLY
FullBackupStartTime         : 2
FullBackupWindowHours       : 2
LogBackupFrequency          : 60
```

If your output shows that **Enable** is set to **False**, then you have to enable Automated Backup. The good news is that you enable and configure Automated Backup in the same way. See the next section for this information.

> [!NOTE] 
> If you check the settings immediately after making a change, it is possible that you will get back the old configuration values. Wait a few minutes and check the settings again to make sure that your changes were applied.

### Configure Automated Backup
You can use PowerShell to enable Automated Backup as well as to modify its configuration and behavior at any time. 

First, select, or create a storage account for the backup files. The following script selects a storage account or creates it if it doesn't exist.

```powershell
$vmname = "yourvmname"
$resourcegroupname = "yourresourcegroupname"
$storage_accountname = "yourstorageaccount"
$storage_url = "https://yourstorageaccount.blob.core.windows.net/"

$storage = Get-AzStorageAccount -ResourceGroupName $resourcegroupname `
    -Name $storage_accountname -ErrorAction SilentlyContinue
If (-Not $storage)
    { $storage = New-AzStorageAccount -ResourceGroupName $resourcegroupname `
    -Name $storage_accountname -SkuName Standard_GRS -Location $region }
```

> [!NOTE]
> Automated Backup does not support storing backups in premium storage, but it can take backups from VM disks which use Premium Storage.

If you want to use a custom container in the storage account for the backups, use the following script to check for the container or create it if it doesn't exist. 

```powershell
$storage_container = "backupcontainer"

New-AzStorageContainer -Name $storage_container -Context $storage.Context

if (!(Get-AzStorageAccount -StorageAccountName $storage_accountname -ResourceGroupName $resourcegroupname | Get-AzStorageContainer | Where-Object { $_.Name -eq $storage_container })){ `
	New-AzStorageContainer -Name $storage_container -Context $storage.Context `
} `
 else `
{ `
	Write-Warning "Container $storage_container already exists." `
}
```

Next, use the following script to get the Access key for the storage account:

```powershell
$accesskey = (Get-AzStorageAccountKey -ResourceGroupName $resourcegroupname  -Name $storage_accountname)[0].value
```

Then use the **Update-AzSqlVM** command to enable and configure the Automated Backup settings to store backups in the Azure storage account. In this example, the backups are set to be retained for 10 days. System database backups are enabled. Full backups are scheduled for every Saturday (weekly) with a time window starting at 20:00 for two hours. Log backups are scheduled for every 30 minutes. 

```powershell
Update-AzSqlVM -ResourceGroupName $resourcegroupname -Name $vmname -AutoBackupSettingEnable `
-AutoBackupSettingBackupScheduleType Manual `
-AutoBackupSettingFullBackupFrequency Weekly 
-AutoBackupSettingDaysOfWeek Saturday `
-AutoBackupSettingFullBackupStartTime 20 `
-AutoBackupSettingFullBackupWindowHour 2 `
-AutoBackupSettingStorageAccessKey $accesskey `
-AutoBackupSettingStorageAccountUrl $storage_url `
-AutoBackupSettingRetentionPeriod 10 `
-AutoBackupSettingLogBackupFrequency 30 `
-AutoBackupSettingStorageContainerName $storage_container `
-AutoBackupSettingBackupSystemDb
```

It could take several minutes to install and configure the SQL Server IaaS Agent. 

To enable encryption, modify the previous script to pass the **-AutoBackupSettingEnableEncryption** parameter along with a password (secure string) for the **-AutoBackupSettingPassword** parameter. The following script enables the Automated Backup settings in the previous example and adds encryption.

```powershell
$password = "r@ndom Va1ue"
$encryptionpassword = $password | ConvertTo-SecureString -AsPlainText -Force  

Update-AzSqlVM -ResourceGroupName $resourcegroupname -Name $vmname -AutoBackupSettingEnable `
-AutoBackupSettingBackupScheduleType Manual `
-AutoBackupSettingFullBackupFrequency Weekly `
-AutoBackupSettingDaysOfWeek Saturday `
-AutoBackupSettingFullBackupStartTime 20 `
-AutoBackupSettingFullBackupWindowHour 2 `
-AutoBackupSettingStorageAccessKey $accesskey `
-AutoBackupSettingStorageAccountUrl $storage_url `
-AutoBackupSettingRetentionPeriod 10 `
-AutoBackupSettingLogBackupFrequency 30 `
-AutoBackupSettingEnableEncryption `
-AutoBackupSettingPassword $encryptionpassword `
-AutoBackupSettingStorageContainerName $storage_container `
-AutoBackupSettingBackupSystemDb
```

To confirm your settings are applied, [verify the Automated Backup configuration](#verifysettings).

### Disable Automated Backup
To disable Automated Backup, run the same script with the **-AutoBackupSettingEnable** parameter set to **$false** in the **Update-AzSqlVM** command. By setting the value to **$false** the feature is disabled. As with installation, it can take several minutes to disable Automated Backup.

```powershell
Update-AzSqlVM -ResourceGroupName $resourcegroupname -Name $vmname -AutoBackupSettingEnable:$false
```

### Example script
The following script provides a set of variables that you can customize to enable and configure Automated Backup for your VM. In your case, you might need to customize the script based on your requirements. For example, you would have to make changes if you wanted to disable the backup of system databases or enable encryption.

```powershell
$vmname = "yourvmname"
$resourcegroupname = "yourresourcegroupname"
$region = "Azure region name such as EASTUS2"
$storage_accountname = "yourstorageaccount"
$storage_url = "https://yourstorageaccount.blob.core.windows.net/"
$retentionperiod = 10
$backupscheduletype = "Manual"
$fullbackupfrequency = "Weekly"
$fullbackupdayofweek = "Saturday"
$fullbackupstarthour = "20"
$fullbackupwindow = "2"
$logbackupfrequency = "30"

# ResourceGroupName is the resource group which is hosting the VM where you are deploying the SQL Server IaaS Extension 

Set-AzVMSqlServerExtension -VMName $vmname `
    -ResourceGroupName $resourcegroupname -Name "SQLIaasExtension" `
    -Version "2.0" -Location $region

# Creates/use a storage account to store the backups

$storage = Get-AzStorageAccount -ResourceGroupName $resourcegroupname `
    -Name $storage_accountname -ErrorAction SilentlyContinue
If (-Not $storage)
    { $storage = New-AzStorageAccount -ResourceGroupName $resourcegroupname `
    -Name $storage_accountname -SkuName Standard_GRS -Location $region }

# Creates/uses a custom storage account container

$storage_container = "yourbackupcontainer"

if (!(Get-AzStorageAccount -StorageAccountName $storage_accountname -ResourceGroupName $resourcegroupname | Get-AzStorageContainer | Where-Object { $_.Name -eq $storage_container })){ `
	New-AzStorageContainer -Name $storage_container -Context $storage.Context `
} `
 else `
{ `
	Write-Warning "Container $storage_container already exists." `
}

# Get storage account access key
$accesskey = (Get-AzStorageAccountKey -ResourceGroupName $resourcegroupname  -Name $storage_accountname)[0].value

# Configure Automated Backup settings

Update-AzSqlVM -ResourceGroupName $resourcegroupname -Name $vmname -AutoBackupSettingEnable `
-AutoBackupSettingBackupScheduleType $backupscheduletype `
-AutoBackupSettingFullBackupFrequency $fullbackupfrequency `
-AutoBackupSettingDaysOfWeek $fullbackupdayofweek `
-AutoBackupSettingFullBackupStartTime $fullbackupstarthour `
-AutoBackupSettingFullBackupWindowHour $fullbackupwindow `
-AutoBackupSettingStorageAccessKey $accesskey `
-AutoBackupSettingStorageAccountUrl $storage_url `
-AutoBackupSettingRetentionPeriod $retentionperiod `
-AutoBackupSettingLogBackupFrequency $logbackupfrequency `
-AutoBackupSettingStorageContainerName $storage_container `
-AutoBackupSettingBackupSystemDb
```

## Backup with encryption certificates

If you decide to encrypt your backups, an encryption certificate will be generated and saved in the same storage account as the backups. In this scenario, you will also need to enter a password which will be used to protect the encryption certificates used for encrypting and decrypting your backups. This allows you to not worry about your backups beyond the configuration of this feature, and also ensures you can trust that your backups are secure.

When backup encryption is enabled, we strongly recommend that you ascertain whether the encryption certificate has been successfully created and uploaded to ensure restorability of your databases. You can do so by creating a database right away and checking the encryption certificates and data were backed up to the newly created container properly. This will show that everything was configured correctly and no anomalies took place.

If the certificate failed to upload for some reason, you can use the certificate manager to export the certificate and save it. You do not want to save it on the same VM, however, as this does not ensure you have access to the certificate when the VM is down. To know if the certificate was backed up properly after changing or creating the Automated Backup configuration, you can check the event logs in the VM,  and if it failed you will see this error message:

:::image type="content" source="./media/automated-backup/automated-backup-event-log.png" alt-text="Screenshot of the error message shown in the Event Log in VM.":::

If the certificates were backed up correctly, you will see this message in the Event Logs:

:::image type="content" source="./media/automated-backup/automated-backup-success.png" alt-text="Screenshot of the successful backup of encryption certificate in event logs.":::

As a general practice, it is recommended to check on the health of your backups from time to time. In order to be able to restore your backups, you should do the following:

1. Confirm that your encryption certificates have been backed up and you remember your password. If you do not do this, you will not be able to decrypt and restore your backups. If for some reason your certificates were not properly backed up, you can accomplish this manually by executing the following T-SQL query:

   ```sql
   BACKUP MASTER KEY TO FILE = <file_path> ENCRYPTION BY PASSWORD = <password>
   BACKUP CERTIFICATE [AutoBackup_Certificate] TO FILE = <file_path> WITH PRIVATE KEY (FILE = <file_path>, ENCRYPTION BY PASSWORD = <password>)
   ```

1. Confirm that your backup files are uploaded with at least 1 full backup. Because mistakes happen, you should be sure you always have at least one full backup before deleting your VM, or in case your VM gets corrupted, so you know you can still access your data. You should make sure the backup in storage is safe and recoverable before deleting your VM’s data disks.


## Monitoring

To monitor Automated Backup on SQL Server 2016 and later, you have two main options. Because Automated Backup uses the SQL Server Managed Backup feature, the same monitoring techniques apply to both.

First, you can poll the status by calling [msdb.managed_backup.sp_get_backup_diagnostics](/sql/relational-databases/system-stored-procedures/managed-backup-sp-get-backup-diagnostics-transact-sql). Or query the [msdb.managed_backup.fn_get_health_status](/sql/relational-databases/system-functions/managed-backup-fn-get-health-status-transact-sql) table-valued function.

Another option is to take advantage of the built-in Database Mail feature for notifications.

1. Call the [msdb.managed_backup.sp_set_parameter](/sql/relational-databases/system-stored-procedures/managed-backup-sp-set-parameter-transact-sql) stored procedure to assign an email address to the **SSMBackup2WANotificationEmailIds** parameter. 
1. Enable [SendGrid](https://docs.sendgrid.com/for-developers/partners/microsoft-azure-2021#create-a-twilio-sendgrid-accountcreate-a-twilio-sendgrid-account) to send the emails from the Azure VM.
1. Use the SMTP server and user name to configure Database Mail. You can configure Database Mail in SQL Server Management Studio or with Transact-SQL commands. For more information, see [Database Mail](/sql/relational-databases/database-mail/database-mail).
1. [Configure SQL Server Agent to use Database Mail](/sql/relational-databases/database-mail/configure-sql-server-agent-mail-to-use-database-mail).
1. Verify that the SMTP port is allowed both through the local VM firewall and the network security group for the VM.

## Known issues

Consider these known issues when working with the Automated Backup feature. 

### Can't enable Automated Backup in the Azure portal


The following table lists the possible solutions if you're having issues enabling Automated Backup from the Azure portal: 

| Symptom | Solution |
|--|--|
| **Enabling Automated Backups will fail if your IaaS extension is in a failed state** | Repair the [SQL IaaS Agent extension](sql-agent-extension-troubleshoot-known-issues.md#repair-extension) if it's in a failed state. | 
| **Enabling Automated Backup fails if you have hundreds of databases** | This is a known limitation with the SQL IaaS Agent extension. To work around this issue, you can enable [Managed Backup](/sql/relational-databases/backup-restore/enable-sql-server-managed-backup-to-microsoft-azure#enable-managed-backup-to-azure) directly instead of using the SQL IaaS Agent extension to configure Automated Backup.  | 
| **Enabling Automated Backup fails due to metadata issues** | Stop the SQL IaaS Agent service.  Run the T-SQL command: `use msdb exec autoadmin_metadata_delete`. Start the SQL IaaS Agent service and try to re-enable Automated Backup from Azure portal.  |
| **Enabling Automated Backups for FCI** | Back ups using private endpoints are unsupported. Use the full storage account URI for your backup.  |
| **Backup Multiple SQL instances using Automated Backup**   | Automated Backup currently only supports one SQL Server instance. If you have multiple named instances, and the default instance, Automated Backup works with the default instance. If you have multiple named instances and no default instance, turning on Automated Backup will fail. |
| **Automated Backup cannot be enabled due to account and permissions** | Check the following:  <br /> - The SQL Server Agent is running. <br /> - The  **NT Service\SqlIaaSExtensionQuery**  account has proper [permissions](sql-server-iaas-agent-extension-automate-management.md#permissions-models) for the Automated Backup feature both within SQL Server, and also for the [SQL virtual machines](manage-sql-vm-portal.md) resource in the Azure portal. <br /> - The **SA** account hasn't been renamed, though disabling it is acceptable.   | 
| **Automated Backup fails for SQL 2016 +**| **Allow Blob Public Access** is enabled on the storage Account. This provides a temporary workaround to a known issue. |




### Common issues with Automated or Managed Backup

The following table lists possible errors and solutions when working with Automated Backups: 

| Symptom | Solution |
|--|--|
| **Automated/Managed Backup fails due to connectivity to storage account/Timeout errors** | Check that the Network Security Group (NSG) for the virtual network, and the Windows Firewall aren't blocking outbound connections from the virtual machine (VM) to the storage account on port 443. |
| **Automated/Managed Backup fails due to Memory/IO Pressure** | See if you can increase the Max Server memory and/or resize the disk/VM if you're running out of [IO/VM limits](https://devblogs.microsoft.com/premier-developer/azure-vm-and-disk-throttling/). If you're using  an availability group, consider offloading your backups to the secondary replica. | 
| **Automated Backup fails after Server Rename** | If you've renamed your machine hostname, you need to also [rename the hostname inside SQL Server](/sql/database-engine/install-windows/rename-a-computer-that-hosts-a-stand-alone-instance-of-sql-server#rename-a-computer-that-hosts-a-stand-alone-instance-of-).  |
| **Error: The operation failed because of an internal error. The argument must not be empty string.\\r\\nParameter name: sas Token Please retry later** | This is likely caused by the SQL Server Agent service not having correct impersonation permissions. Change the SQL Server Agent service to use a different account to fix this issue. | 
| **Error: SQL Server Managed Backup to Microsoft Azure cannot configure the default backup settings for the SQLServer instance because the container URL was invalid. It is also possible that your SAS credential is invalid** | You might see this error if you have a large number of databases. Use [Managed backup](/sql/relational-databases/backup-restore/enable-sql-server-managed-backup-to-microsoft-azure) instead of Automated Backup. | 
| **Automated Backup job failed after VM Restart**| Check that the SQL Agent service is up and running. | 
| **Managed backup fails intermittently/Error:Execution timeout Expired** | This is a known issue fixed in [CU18](https://support.microsoft.com/topic/kb5017593-cumulative-update-18-for-sql-server-2019-5fa00c36-edeb-446c-94e3-c4882b7526bc#bkmk_14913295) for SQL Server 2019 and [KB4040376] for SQL Server 2014-2017.|
| **Error: The remote server returned an error: (403) Forbidden**  | Repair the [SQL IaaS Agent extension](sql-agent-extension-troubleshoot-known-issues.md#repair-extension). | 
| **Error 3202: Write on Storage account failed 13 (The data is invalid)** | Remove the immutable blob policy on the storage container and make sure the storage account is using, at minimum, TLS 1.0.  | 
| **Error 3063: Write to backup block blob device. Device has reached its limit of allowed blocks.** | This can happen if you're running automated backups from a secondary Always On availability group replica that has the `Readable` configuration set to `NO`. For automated backups to work on a secondary replica, the replica must be readable. | 
| **Unable to schedule a backup for a particular day** | If you're using [Automated backups for SQL Server 2014](automated-backup-sql-2014.md), this is expected. You can configure a backup schedule starting with SQL Server 2016. |


### Disabling Automated Backup or Managed Backup fails

The following table lists the possible solutions if you're having issues disabling Automated Backup from the Azure portal: 


| Symptom | Solution |
|--|--|
| **Disabling Auto backups will fail if your IaaS extension is in a failed state** | Repair the [SQL IaaS Agent extension](sql-agent-extension-troubleshoot-known-issues.md#repair-extension) if it's in a failed state. | 
| **Disabling Automated Backup fails due to metadata issues** | Stop the SQL IaaS Agent service. Run the T-SQL command: `use msdb exec autoadmin_metadata_delete`. Start SQL Iaas Agent service and try to disable Automated Backup from Azure portal.  |
| **Automated Backup cannot be disabled due to account and permissions** | Check the following: <br /> - The SQL Server Agent is running. <br /> - The  **NT Service\SqlIaaSExtensionQuery**  account has proper [permissions](sql-server-iaas-agent-extension-automate-management.md#permissions-models) for the Automated Backup feature both within SQL Server, and also for the [SQL virtual machines](manage-sql-vm-portal.md) resource in the Azure portal. <br /> - The **SA** account hasn't been renamed, though disabling it is acceptable.  | 


### I want to find out what service/application is taking SQL Server backups

- In SQL Server Management Studio (SSMS) **Object Explorer**, right-click the database > Select **Reports** > **Standard Reports** > **Backup and Restore Events**. In the report, you can expand the **Successful Backup Operations** section to see the backup history.
- If you see multiple backups on Azure or to a virtual device, check if you're using [Azure Backup](/azure/backup/backup-azure-sql-database) to back up individual SQL databases or taking a virtual machine snapshot to a virtual device, which uses the `NT Authority/SYSTEM` account. If you're not, check the Windows **Services** console (services.msc) to identify any third-party applications which might be taking backups. 


## Next steps

Automated Backup configures Managed Backup on Azure VMs. So it's important to [review the documentation for Managed Backup](/sql/relational-databases/backup-restore/sql-server-managed-backup-to-microsoft-azure) to understand the behavior and implications.

You can find additional backup and restore guidance for SQL Server on Azure VMs in the following article: [Backup and restore for SQL Server on Azure virtual machines](backup-restore.md).

For information about other available automation tasks, see [SQL Server IaaS Agent Extension](sql-server-iaas-agent-extension-automate-management.md).

For more information about running SQL Server on Azure VMs, see [SQL Server on Azure virtual machines overview](sql-server-on-azure-vm-iaas-what-is-overview.md).
