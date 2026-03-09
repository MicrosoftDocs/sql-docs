---
title: Place tempdb on Ephemeral Storage
description: Learn how to configure your SQL Server tempdb database to use ephemeral storage for SQL Server on Azure VMs, and how to extend the buffer pool to the local SSD drive.
author: dplessMSFT
ms.author: dpless
ms.reviewer: mathoma, randolphwest
ms.date: 09/07/2025
ms.service: azure-vm-sql-server
ms.subservice: management
ms.topic: how-to
tags: azure-resource-manager
---
# Place tempdb on ephemeral storage for SQL Server on Azure VMs

[!INCLUDE [appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

In this article, learn how to improve the performance of your workloads on SQL Server on Azure Virtual Machines (VMs) by utilizing the local SSD ephemeral storage available to some Azure VMs; such as by moving the `tempdb` system database to the local SSD drive, or using the local SSD drive to extend the buffer pool.

## Overview

The local SSD drive attached to certain Azure Virtual Machines (VMs) provides optimized ephemeral storage: a high-performance disk physically connected to the host machine. This ephemeral storage is recreated whenever the VM is deallocated or moved (such as during maintenance or resizing). Regardless, placing the SQL Server `tempdb` system database on ephemeral storage poses no risk, since the database is recreated every time SQL Server restarts.

Placing `tempdb` on the ephemeral drive is a recommended best practice, as its optimized low latency and high IOPS can significantly boost performance for workloads that heavily rely on temporary objects, including:

- Queries processing large record sets
- Index creation and maintenance
- Row versioning isolation levels
- Temporary tables
- Triggers

However, because the local SSD drive is non-persistent, its contents and permissions are lost whenever the VM is stopped, deallocated, or relocated to a new host. This necessitates careful planning, with the following considerations:

- **Reconfiguration on restart**: `tempdb` must be reconfigured to use the ephemeral disk (typically D:\) each time the VM restarts. For SQL Server on Azure VM images from Azure Marketplace, this process is automated with the SQL IaaS Agent extension, simplifies management by creating folders and handling permissions automatically when the VM starts. However, if you manually installed SQL Server, you need to configure `tempdb` to use the ephemeral disk manually every time the VM restarts. This process can be [automated](#automate-tempdb-configuration-on-startup) with, for example, PowerShell and Task Scheduler.

- **Exclusive use**: `tempdb` should be the only data stored on the local SSD drive. Persistent data (such as data files, log files, or backups) must not be placed on ephemeral storage, as they are lost each time the VM restarts or is deallocated.

## Prerequisites

Before you can configure your `tempdb` to use ephemeral storage, you need the following prerequisites:

- An Azure subscription. If you don't have an Azure subscription, create a [free account](https://azure.microsoft.com/pricing/purchase-options/azure-account?cid=msft_learn).

- SQL Server manually installed to an [Azure VM](/azure/virtual-machines/windows/quick-create-portal).

- An [initialized ephemeral disk](/azure/virtual-machines/enable-nvme-temp-faqs#how-can-i-format-and-initialize-temp-nvme-disks-in-windows-when-i-create-a-vm-). On Azure VMs, the ephemeral disk is typically mounted as the `D:` drive. If you have a different configuration, adjust the instructions accordingly.

> [!NOTE]  
> This article assumes you've installed SQL Server manually because `tempdb` is automatically configured to use ephemeral storage when you deploy a SQL Server on Azure VM image from Azure Marketplace.

## Configure tempdb to use ephemeral storage

During a maintenance window, you can configure your SQL Server `tempdb` to use the ephemeral disk by using Transact-SQL. Consider:

- The `tempb` database can have multiple data files, such as `tempdb.mdf`, `tempdb2.md`, and `tempdb3.md`, depending on your SQL Server configuration. You must run the `MODIFY FILE` command for *each data file* to reconfigure `tempdb` to use the ephemeral disk, such as `D:\SQLTemp`.

- You can query [sys.master_files](/sql/relational-databases/system-catalog-views/sys-master-files-transact-sql) (`WHERE database_id = 2`) to identify all `tempdb` data files.

1. Create a folder on the local SSD drive, such as `D:\SQLTemp`.

1. Open SQL Server Management Studio (SSMS) and connect to your SQL Server instance.

1. Run the following T-SQL commands to configure `tempdb` to use the ephemeral disk:

   ```sql
   USE master;
   GO

   ALTER DATABASE tempdb
       MODIFY FILE (NAME = tempdev, FILENAME = 'D:\SQLTemp\tempdb.mdf'); -- to move data files
   GO

   ALTER DATABASE tempdb
       MODIFY FILE (NAME = templog, FILENAME = 'D:\SQLTemp\templog.ldf'); -- to move log files
   GO
   ```

1. Restart the SQL Server instance to apply your changes.

1. Verify that `tempdb` is using the ephemeral disk by running the following T-SQL command:

   ```sql
   USE tempdb;
   GO

   EXECUTE sp_helpfile;
   GO
   ```

1. Check the folder on the ephemeral disk to verify that `tempdb` files are created in the `D:\SQLTemp` folder.

## Automate tempdb configuration on startup

Since contents on the ephemeral drive are lost when the VM is restarted, SQL Server fails to start if the `tempdb` folder isn't created before SQL Server starts. You can use PowerShell to automate creating the folder before the SQL Server service starts.

To automate `tempdb` configuration on startup, follow these steps:

1. **Configure Services**: Set both the SQL Server and SQL Server Agent services to manual startup. This prevents them from launching automatically before the folder is created.

1. **Create a PowerShell script** that creates the folder on the ephemeral disk and starts the SQL Server and SQL Agent services.

1. **Schedule the script** to run at system startup using a Windows scheduled task. Configure the task to run whether the user is logged on or not, using an account

The following sections provide detailed instructions for each step.

### Configure start mode

Since you want the script to create the folder to run before SQL Server starts, you need to set the SQL Server and SQL Agent services to start manually. To do this, follow these steps:

1. Open [SQL Server Configuration Manager](/sql/tools/configuration-manager/sql-server-configuration-manager).

1. Select **SQL Server Services** in the left pane.

1. Right-click on the **SQL Server** service and select **Properties** to open the **Properties** window.

1. On the **Properties** window, select the **Service** tab.

1. On the **Service** tab, use the dropdown list to change the **Start Mode** to **Manual**:

   :::image type="content" source="media/tempdb-ephemeral-storage/sql-server-properties.png" alt-text="Screenshot of the SQL Server Configuration Manager, SQL Server service properties, showing where to change the start mode.":::

1. Use **Apply** to save your changes and then **OK** to close the window.

1. Repeat these steps for the **SQL Server Agent** service.

### Create PowerShell script

Create a PowerShell script that:

1. Creates the folder on the ephemeral disk.
1. Starts the SQL Server service.
1. Starts the SQL Agent service.

Copy and paste the following script, modify it as needed, and save it as a PowerShell file on the OS drive, such as `C:\Scripts\SQLStartup.ps1`:

```powershell
$SQLService = "SQL Server (MSSQLSERVER)"
$SQLAgentService = "SQL Server Agent (MSSQLSERVER)"
$tempfolder = "D:\SQLTEMP"
if (!(test-path -path $tempfolder)) {
    New-Item -ItemType directory -Path $tempfolder
}
Start-Service $SQLService
Start-Service $SQLAgentService
```

> [!NOTE]  
> The script assumes that the SQL Server instance is the default instance. If you're using a named instance, replace `MSSQLSERVER` with the name of your SQL Server instance.

### Create a scheduled task to run the script

Create a scheduled task to run the PowerShell script at startup. To do this, follow these steps:

1. Open **Task Scheduler** from the Start menu.

1. Under **Actions**, select **Create Basic Task** to open the **Create Task** window.

1. On the **Create a Basic Task** tab, enter a name for the task, such as `SQL-startup`, and provide a description. Select **Next**.

1. On the **Triggers** tab, check **When the computer starts** and select **Next**.

1. On the **Actions** tab, select **Start a program** and select **Next**.

1. On the **Start a Program** tab, n the **Program/script** box, enter `powershell.exe` and in the **Add arguments (optional)** box, enter the path of the script, such as: `-ExecutionPolicy Bypass -File C:\Scripts\SQLStartup.ps1`.

1. Review the summary on the **Finish** tab and select **Finish** to create the task:

   :::image type="content" source="media/tempdb-ephemeral-storage/create-sql-start-task.png" alt-text="Screenshot of the Task Scheduler, Create a Basic Task window, showing where to enter the script path.":::

### Test the script

Restart the VM to test the script. After the VM restarts, check that the `tempdb` data files are located on the ephemeral disk and that the SQL Server and SQL Agent services are running.

## Configure buffer pool extension

You can further enhance SQL Server performance by configuring the [buffer pool extension](/sql/database-engine/configure-windows/buffer-pool-extension) to use the local SSD drive on Azure VMs. This feature extends the in-memory buffer pool by using a file on disk to boost I/O throughput for memory-intensive workloads that exceed available RAM. Since the local SSD (ephemeral storage) offers low latency and high performance, it's an ideal location for this extension.

When configuring the buffer pool extension, specify the size of the file in kilobytes (KB), megabytes (MB), or gigabytes (GB). The recommended size is typically 4 to 8 times the [max server memory (MB)](/sql/database-engine/configure-windows/server-memory-server-configuration-options) setting configured for SQL Server, though for Standard edition, the maximum is capped at 4 times this value (Enterprise edition allows up to 32 times). For example, if `max server memory (MB)` is set to 16 GB, aim for a buffer pool extension size of 64-128 GB, adjusted to your SQL Server edition and workload needs.

Assuming the specified path exists on the ephemeral drive (such as `D:\SQLTEMP\`), to enable the buffer pool extension, execute the following T-SQL command in SQL Server Management Studio (SSMS) after connecting to your instance:

```syntaxsql
ALTER SERVER CONFIGURATION
SET BUFFER POOL EXTENSION ON
( FILENAME = 'D:\SQLTEMP\ExtensionFile.BPE' , SIZE = <size> [ KB | MB | GB ] )
```

## Related content

- [What is SQL Server on Azure Windows Virtual Machines?](sql-server-on-azure-vm-iaas-what-is-overview.md)
- [FAQ for SQL Server on Windows VMs](frequently-asked-questions-faq.yml)
- [Pricing guidance for SQL Server on Azure VMs](pricing-guidance.md)
- [What's new with SQL Server on Azure Virtual Machines?](doc-changes-updates-release-notes-whats-new.md)
- [Checklist: Best practices for SQL Server on Azure VMs](performance-guidelines-best-practices-checklist.md)
- [Introduction to Azure Managed Disks](/azure/virtual-machines/managed-disks-overview)
