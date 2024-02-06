---
title: "Configure Log Shipping for SQL Server on Azure VMs"
description: This article teaches you to configure Log Shipping for your SQL Server on Azure VM.
author: tarynpratt
ms.author: tarynpratt
ms.reviewer: mathoma
ms.date: 02/06/2024
ms.service: virtual-machines-sql
ms.subservice: hadr
ms.topic: how-to
---

# Configure Log Shipping for SQL Server on Azure VMs
[!INCLUDE[appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

This article teaches you to configure [log shipping](/sql/database-engine/log-shipping/about-log-shipping-sql-server) for SQL Server on Azure VM. 

## Overview

Log shipping allows you to automatically send transaction log backups from a *primary database* on a *primary server* instance to one or more *secondary databases* on separate *secondary server* instances. The transaction log backups are applied to each of the secondary databases individually. An optional third server instance, known as the *monitor server*, records the history and status of backup and restore operations and, optionally, raises alerts if these operations fail to occur as scheduled.

Log shipping is a primarily used as a disaster recovery solution and it can be combined with other high availability and disaster recovery options, including Always On availability groups.

## Prerequisites

To configure log shipping for SQL Server on Azure VM, you must have the following prerequisites:

- At least two domain-joined Azure virtual machines with SQL Server
- The primary database must use the full or bulk-logged recovery mode; switching the database to siple recovery will cause log shipping to stop functioning
- The account executing the log-shipping stored procedures require membership in the **sysadmin** fixed server role

## Create Azure File Share

Before you configure log shipping, you must create an [Azure File Share](/azure/storage/files/storage-files-introduction) inside of an Azure storage account. The file share will be used to make the transaction log backups available to the secondary server. 

In the [Azure portal](https://portal.azure.com), go to your resource group and select the [storage account](/azure/storage/blobs/storage-blobs-overview) for the file share. 

1. Under Data Storage, select File Shares.

1. Choose **+File share** to create a new File share. 

   :::image type="content" source="media/configure-log-shipping/create-azure-file-share.png" alt-text="Screenshot of the File share creation option in the Azure portal.":::

1. On the **Basics** tab, provide the name of the file share, such as **log-shipping**. 

1. On the **Backup** tab, enable backups of your file share to [Azure Backup](/azure/backup/azure-file-share-backup-overview), if needed. 

1. Choose **Review + create**, and then **Create** to create your new file share. 

### Create Backup Directories

After Azure creates the file share, the portal returns you to file share **Overview** page. Within the file share, it is recommended to create two directories:

   - A directory for the primary to write the log backups to
   - A directory for the secondary to copy and restore the log backup

To create the directories:

1. Choose **Browse** from your file share, then **+ Add directory**. Provide the name of the new directory, for example **log-backups**, then select **OK**. 

   :::image type="content" source="media/configure-log-shipping/create-file-share-directory.png" alt-text="Screenshot of the add directory creation option in the Azure portal.":::

1. Repeat the process to add a second directory, for example **restore-backups**, then select **OK**.

### File Share Connection Details

Once the directories are created, you will need to connect the virtual machines to the file share. The connection details are located by selecting **Connect** on the file share menu.

   :::image type="content" source="media/configure-log-shipping/file-share-connect.png" alt-text="Screenshot of the Connect option for the file share in the Azure portal.":::

The connect screen provides a PowerShell script that needs to be executed on each virtual machine to be included in the log shipping. This script allows the virtual machine to connect to the file share. For this walkthrough, it uses a storage account key and the script for Windows virtual machines. If needed, change the Drive letter to mount the file share to the virtual machine, then to view the PowerShell script, select Show Script.

   :::image type="content" source="media/configure-log-shipping/file-share-connection-script.png" alt-text="Screenshot of the connection details for the file share in the Azure portal.":::

After executing the PowerShell script, you can verify connectivity to port 445 by executing the following PowerShell command:

```powershell
Test-NetConnection -ComputerName yourstorageaccount.file.core.windows.net -Port 445
```

If the test connection was successful, you should see an output of **TcpTestSucceeded : True**. 

### Provide Access to the primary and secondary SQL Server Engine account

The next step is to create the credential within SQL Server that will have access to the Azure File Share. This credential matches the one used in the PowerShell script. **xp_cmdshell** will be used to execute the command that stores the credential.

1. Connect to each SQL Server instance that will take part in the log shipping with an account that is a part of the **sysadmin** role. 
1. Open SQL Server Management Studio and connect to the instance. 
1. Create a new query window and paste the following code containing the storage key details obtained from the Azure Portal. 

   ```sql
   EXEC sp_configure 'show advanced options', 1;
   RECONFIGURE;
   exec sp_configure 'xp_cmdshell', 1;
   RECONFIGURE;
   GO
   EXEC xp_cmdshell 'cmdkey /add:"yourstorageaccount.file.core.windows.net" /user:"localhost\yourstorageaccount" /pass:"<yourpasskey>"';
   GO
   EXEC sp_configure 'xp_cmdshell', 0;
   RECONFIGURE;
   GO
   ```

   After executing the command, you should see a confirmation.

   :::image type="content" source="media/configure-log-shipping/credential-confirmation.png" alt-text="Screenshot of the confirmation the credential was successfully created in SSMS.":::


## Configure log shipping using SQL Server Management Studio  
  
1.  Right-click the database you want to use as your primary database in the log shipping configuration, and then click **Properties**.  
  
1.  Under **Select a page**, click **Transaction Log Shipping**.  
  
1.  Select the **Enable this as a primary database in a log shipping configuration** check box.  
  
1.  Under **Transaction log backups**, click **Backup Settings**.  
  
1.  In the **Network path to the backup folder** box, type the network path to the share you created for the transaction log backup folder. The network path will be the path to your log backup folder, for example in this article it would look like **\\\yourstorageaccount.file.core.windows.net\log-shipping\log-backups**.
  
1.  Configure the **Delete files older than** and **Alert if no backup occurs within** parameters.  
  
1.  Note the backup schedule listed in the **Schedule** box under **Backup job**. If you want to customize the schedule for your installation, then click **Schedule** and adjust the SQL Server Agent schedule as needed.  
  
1. SQL Server supports [backup compression](/sql/relational-databases/backup-restore/backup-compression-sql-server). When creating a log shipping configuration, you can control the backup compression behavior of log backups by choosing one of the following options: **Use the default server setting**, **Compress backup**, or **Do not compress backup**. For more information, see [Log Shipping Transaction Log Backup Settings](/sql/relational-databases/databases/log-shipping-transaction-log-backup-settings).  
  
1. Click **OK**.  
  
1. Under **Secondary server instances and databases**, click **Add**.  
  
1. Click **Connect** and connect to the instance of SQL Server that you want to use as your secondary server.  
  
1. In the **Secondary Database** box, choose a database from the list or type the name of the database you want to create.  
  
1. On the **Initialize Secondary database** tab, choose the option that you want to use to initialize the secondary database.  
  
    > [!NOTE]  
    >  If you choose to have Management Studio initialize the secondary database from a database backup, the data and log files of the secondary database are placed in the same location as the data and log files of the **master** database. This location is likely to be different than the location of the data and log files of the primary database.  
  
1. On the **Copy Files** tab, in the **Destination folder for copied files** box, type the path of the folder into which the transaction logs backups should be copied. This folder is often located on the secondary server. This will be the netwrok path of the restore directory in the file share, for this article the path is **\\\yourstorageaccount.file.core.windows.net\log-shipping\restore-backups**
  
1. Note the copy schedule listed in the **Schedule** box under **Copy job**. If you want to customize the schedule for your installation, click **Schedule** and then adjust the SQL Server Agent schedule as needed. This schedule should approximate the backup schedule.  
  
1. On the **Restore** tab, under **Database state when restoring backups**, choose the **No recovery mode** or **Standby mode** option.  
    > [!IMPORTANT]  
    > **Standby mode** is only an option when the version of the primary and secondary server are the same. When the major version of the secondary server is higher than the primary, only **No recovery mode** is allowed
  
1. If you chose the **Standby mode** option, choose if you want to disconnect users from the secondary database while the restore operation is underway.  
  
1. If you want to delay the restore process on the secondary server, choose a delay time under **Delay restoring backups at least**.  
  
1. Choose an alert threshold under **Alert if no restore occurs within**.  
  
1. Note the restore schedule listed in the **Schedule** box under **Restore job**. If you want to customize the schedule for your installation, click **Schedule** and then adjust the SQL Server Agent schedule as needed. This schedule should approximate the backup schedule.  
  
1. Click **OK**.  
  
1. Under **Monitor server instance**, select the **Use a monitor server instance** check box, and then click **Settings**.  
  
    > [!IMPORTANT]  
    >  To monitor this log shipping configuration, you must add the monitor server now. To add the monitor server later, you would need to remove this log shipping configuration and then replace it with a new configuration that includes a monitor server.  
  
1. Click **Connect** and connect to the instance of SQL Server that you want to use as your monitor server.  
  
1. Under **Monitor connections**, choose the connection method to be used by the backup, copy, and restore jobs to connect to the monitor server.  
  
1. Under **History retention**, choose the length of time you want to retain a record of your log shipping history.  
  
1. Click **OK**.  
  
1. On the **Database Properties** dialog box, click **OK** to begin the configuration process.  



## Related Tasks

- [Upgrading Log Shipping to SQL Server 2016 (Transact-SQL)](/sql/database-engine/log-shipping/upgrading-log-shipping-to-sql-server-2016-transact-sql)
- [Add a Secondary Database to a Log Shipping Configuration (SQL Server)](/sql/database-engine/log-shipping/add-a-secondary-database-to-a-log-shipping-configuration-sql-server)
- [Remove a Secondary Database from a Log Shipping Configuration (SQL Server)](/sql/database-engine/log-shipping/remove-a-secondary-database-from-a-log-shipping-configuration-sql-server)
- [Remove Log Shipping (SQL Server)](/sql/database-engine/log-shipping/remove-log-shipping-sql-server)
- [View the Log Shipping Report (SQL Server Management Studio)](/sql/database-engine/log-shipping/view-the-log-shipping-report-sql-server-management-studio)
- [Monitor Log Shipping (Transact-SQL)](/sql/database-engine/log-shipping/monitor-log-shipping-transact-sql)
- [Fail Over to a Log Shipping Secondary (SQL Server)](/sql/database-engine/log-shipping/fail-over-to-a-log-shipping-secondary-sql-server)
