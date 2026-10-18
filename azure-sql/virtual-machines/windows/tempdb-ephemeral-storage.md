---
title: Place tempdb on Ephemeral Storage
description: Learn how to configure your SQL Server tempdb database to use ephemeral storage for SQL Server on Azure VMs, and how to extend the buffer pool to the local SSD drive.
author: dplessMSFT
ms.author: dpless
ms.reviewer: vanto
ms.date: 04/24/2026
ms.service: azure-vm-sql-server
ms.subservice: management
ms.topic: how-to
tags: azure-resource-manager
---
# Place tempdb on ephemeral storage for SQL Server on Azure VMs

[!INCLUDE [appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

In this article, learn how to improve the performance of your workloads on SQL Server on Azure Virtual Machines (VMs) by using the local SSD ephemeral storage available on some Azure VMs. For example, you can move the `tempdb` system database to the local SSD drive or use the local SSD drive to extend the buffer pool.

## Overview

The local SSD drive attached to certain Azure Virtual Machines (VMs) provides optimized ephemeral storage. It's a high-performance disk that's physically connected to the host machine. The ephemeral storage is recreated whenever the VM is deallocated or moved (such as during maintenance or resizing). Regardless, placing the SQL Server `tempdb` system database on ephemeral storage poses no risk of data loss, since the database is recreated every time SQL Server restarts.

Place `tempdb` on the ephemeral drive as a best practice. For many VM sizes, its optimized low latency and high IOPS can significantly boost performance for workloads that heavily rely on temporary objects, including:

- Queries that process large record sets
- Index creation and maintenance
- Row versioning isolation levels
- Temporary tables
- Triggers

However, because the local SSD drive is non-persistent, it loses its contents and permissions whenever the VM stops, deallocates, or relocates to a new host. This limitation requires careful planning. Consider the following points:

- **Reconfiguration on restart**: You must reconfigure `tempdb` to use the ephemeral disk (typically `D:\`) each time the VM restarts. For SQL Server on Azure VM images from Azure Marketplace, the SQL IaaS Agent extension automates this process. It simplifies management by creating folders and handling permissions automatically when the VM starts. However, if you manually installed SQL Server, you need to configure `tempdb` to use the ephemeral disk manually every time the VM restarts. You can [automate](#automate-tempdb-configuration-on-startup) this process by using, for example, PowerShell and Task Scheduler.

- **Exclusive use**: `tempdb` should be the only data stored on the local SSD drive. Don't place persistent data (such as data files, log files, or backups) on ephemeral storage, as the data is lost each time the VM restarts or is deallocated.

> [!TIP]
> Local SSD performance varies by VM size. After you apply changes from this article, compare your workload performance against your baseline.

## Prerequisites

Before you can configure your `tempdb` to use ephemeral storage, you need the following prerequisites:

- An Azure subscription. If you don't have an Azure subscription, create a [free account](https://azure.microsoft.com/pricing/purchase-options/azure-account?cid=msft_learn).

- SQL Server manually installed to an [Azure VM](/azure/virtual-machines/windows/quick-create-portal).

- An [initialized ephemeral disk](/azure/virtual-machines/enable-nvme-temp-faqs#how-can-i-format-and-initialize-temp-nvme-disks-in-windows-when-i-create-a-vm-). On Azure VMs, the ephemeral disk is typically mounted as the `D:` drive. If you have a different configuration, adjust the instructions accordingly.

> [!NOTE]  
> This article assumes you installed SQL Server manually. When you deploy a SQL Server on Azure VM image from Azure Marketplace, `tempdb` is automatically configured to use ephemeral storage.

## Configure tempdb to use ephemeral storage

During a maintenance window, you can configure your SQL Server `tempdb` to use the ephemeral disk by using Transact-SQL. Consider the following information:

- The `tempdb` database can have multiple data files, such as `tempdb.mdf`, `tempdb2.ndf`, and `tempdb3.ndf`, depending on your SQL Server configuration. You must run the `MODIFY FILE` command for *each data file* to reconfigure `tempdb` to use the ephemeral disk, such as `D:\SQLTemp`.

- You can query [sys.master_files](/sql/relational-databases/system-catalog-views/sys-master-files-transact-sql) (`WHERE database_id = 2`) to identify all `tempdb` data files.

1. Create a folder on the local SSD drive, such as `D:\SQLTemp`.

1. Open SQL Server Management Studio (SSMS) and connect to your SQL Server instance.

1. Run the following T-SQL commands to configure `tempdb` to use the ephemeral disk:

   ```sql
   USE master;
   GO

    -- To move data files
   ALTER DATABASE tempdb
   MODIFY FILE (
       NAME = tempdev,
       FILENAME = 'D:\SQLTemp\tempdb.mdf'
   );
   GO

   -- To move log files
   ALTER DATABASE tempdb
   MODIFY FILE (
       NAME = templog,
       FILENAME = 'D:\SQLTemp\templog.ldf'
   );
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

Because the contents on the ephemeral drive are lost when the VM restarts, SQL Server can't start if the `tempdb` folder doesn't exist before SQL Server starts. Use PowerShell to automate creating the folder before the SQL Server service starts.

To automate `tempdb` configuration on startup, follow these steps:

1. **Configure Services**: Set both the SQL Server and SQL Server Agent services to manual startup. This setting prevents the services from launching automatically before the folder is created.

1. **Create a PowerShell script** that creates the folder on the ephemeral disk and starts the SQL Server and SQL Agent services.

1. **Schedule the script** to run at system startup by using a Windows scheduled task. Configure the task to run whether the user is logged on or not, by using an account with [sufficient privileges](/sql/database-engine/configure-windows/configure-windows-service-accounts-and-permissions) to start SQL Server services.

The following sections provide detailed instructions for each step.

### Configure start mode

To create the folder for the script to run before SQL Server starts, set the SQL Server and SQL Agent services to start manually. To do this, follow these steps:

1. Open [SQL Server Configuration Manager](/sql/tools/configuration-manager/sql-server-configuration-manager).

1. Select **SQL Server Services** in the left pane.

1. Right-click the **SQL Server** service and select **Properties** to open the **Properties** window.

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
$SQLService = "MSSQLSERVER"
$SQLAgentService = "SQLSERVERAGENT"
$tempfolder = "D:\SQLTemp"
if (!(test-path -path $tempfolder)) {
    New-Item -ItemType directory -Path $tempfolder
}
Start-Service $SQLService
Start-Service $SQLAgentService
```

> [!NOTE]  
> The script assumes a default SQL Server instance. For a named instance, replace `MSSQLSERVER` with `MSSQL$<InstanceName>` and `SQLSERVERAGENT` with `SQLAgent$<InstanceName>`, where `<InstanceName>` is the name of your instance.

### Create a scheduled task to run the script

Create a scheduled task to run the PowerShell script at startup. To do this, follow these steps:

1. Open **Task Scheduler** from the Start menu.

1. Under **Actions**, select **Create Basic Task** to open the **Create Task** window.

1. On the **Create a Basic Task** tab, enter a name for the task, such as `SQL-startup`, and provide a description. Select **Next**.

1. On the **Triggers** tab, check **When the computer starts** and select **Next**.

1. On the **Actions** tab, select **Start a program** and select **Next**.

1. On the **Start a Program** tab, in the **Program/script** box, enter `powershell.exe` and in the **Add arguments (optional)** box, enter the path of the script, such as: `-ExecutionPolicy Bypass -File C:\Scripts\SQLStartup.ps1`.

   > [!NOTE]  
   > The `-ExecutionPolicy Bypass` parameter allows the script to run without being blocked by the system [execution policy](/powershell/module/microsoft.powershell.core/about/about_execution_policies). If your organization requires a stricter policy, consider signing the script and using `-ExecutionPolicy AllSigned` instead.

1. Review the summary on the **Finish** tab and select **Finish** to create the task:

   :::image type="content" source="media/tempdb-ephemeral-storage/create-sql-start-task.png" alt-text="Screenshot of the Task Scheduler, Create a Basic Task window, showing where to enter the script path.":::

### Test the script

Restart the VM to test the script. After the VM restarts, check that the `tempdb` data files are located on the ephemeral disk and that the SQL Server and SQL Agent services are running.

## Configure buffer pool extension

You can further enhance SQL Server performance by configuring the [buffer pool extension](/sql/database-engine/configure-windows/buffer-pool-extension) to use the local SSD drive on Azure VMs. This feature extends the in-memory buffer pool by using a file on disk to boost I/O throughput for memory-intensive workloads that exceed available RAM. Since the local SSD (ephemeral storage) offers low latency and high performance, it's an ideal location for this extension.

When configuring the buffer pool extension, specify the size of the file in kilobytes (KB), megabytes (MB), or gigabytes (GB). The recommended size is typically 4 to 8 times the [max server memory (MB)](/sql/database-engine/configure-windows/server-memory-server-configuration-options) setting configured for SQL Server, though for Standard edition, the maximum is capped at 4 times this value (Enterprise edition allows up to 32 times). For example, if `max server memory (MB)` is set to 16 GB, aim for a buffer pool extension size of 64-128 GB, adjusted to your SQL Server edition and workload needs.

### Example buffer pool extension configuration

Assuming the specified path exists on the ephemeral drive (such as `D:\SQLTemp\`), enable the buffer pool extension by using the following Transact-SQL command after connecting to your instance. In this example, the buffer pool extension is set to 64 GB.

```sql
ALTER SERVER CONFIGURATION
SET BUFFER POOL EXTENSION
ON (
    FILENAME = 'D:\SQLTemp\ExtensionFile.BPE',
    SIZE = 64 GB
);
```

## Related content

- [What is SQL Server on Azure Windows Virtual Machines?](sql-server-on-azure-vm-iaas-what-is-overview.md)
- [FAQ for SQL Server on Windows VMs](frequently-asked-questions-faq.yml)
- [Pricing guidance for SQL Server on Azure VMs](pricing-guidance.md)
- [What's new with SQL Server on Azure Virtual Machines?](doc-changes-updates-release-notes-whats-new.md)
- [Checklist: Best practices for SQL Server on Azure VMs](performance-guidelines-best-practices-checklist.md)
- [Introduction to Azure managed disks](/azure/virtual-machines/managed-disks-overview)
