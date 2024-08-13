---
title: Import or export database without allowing Azure services to access the server
description: Import or export an Azure SQL Database without allowing Azure services to access the server.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: mathoma, hudequei
ms.date: 07/24/2024
ms.service: azure-sql-database
ms.subservice: migration
ms.topic: how-to
ms.custom:
  - sqldbrb=1
  - sql-migration-content
---
# Import or export an Azure SQL Database without allowing Azure services to access the server
[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

This article shows you how to import or export an Azure SQL Database when **Allow Azure services and resources to access this server** is set to *OFF*. The how-to article uses an Azure virtual machine to run SqlPackage to perform the import or export operation.

The **Allow Azure services and resources to access this server** setting is visible in the Azure portal under the **Security** menu in the resource menu, **Networking**, in the **Exceptions** section. For more information on this setting, see [Azure SQL Database network access controls](network-access-controls-overview.md).

## Sign in to the Azure portal

Sign in to the [Azure portal](https://portal.azure.com/).

## Create the Azure virtual machine

Create an Azure virtual machine by selecting the **Deploy to Azure** button.

This template allows you to deploy a simple Windows virtual machine using a few different options for the Windows version, using the latest patched version. This deploys an A2 size VM in the resource group location and returns the fully qualified domain name of the VM.
<br><br>

[![Image showing a button labeled "Deploy to Azure".](https://raw.githubusercontent.com/Azure/azure-quickstart-templates/master/1-CONTRIBUTION-GUIDE/images/deploytoazure.svg)](
https://portal.azure.com/#create/Microsoft.Template/uri/https%3A%2F%2Fraw.githubusercontent.com%2FAzure%2Fazure-quickstart-templates%2Fmaster%2Fquickstarts%2Fmicrosoft.compute%2Fvm-simple-windows%2Fazuredeploy.json)

For more information including an Azure Quickstart Template, see [Deploy a simple Windows VM](https://github.com/Azure/azure-quickstart-templates/tree/master/quickstarts/microsoft.compute/vm-simple-windows).

## Connect to the virtual machine

The following steps show you how to connect to your virtual machine using a remote desktop connection.

1. After deployment completes, go to the virtual machine resource.

1. Select **Connect**.

   A Remote Desktop Protocol file (.rdp file) form appears with the public IP address and port number for the virtual machine.

   :::image type="content" source="media/database-import-export-azure-services-off/rdp.png" alt-text="Screenshot of Azure portal, connect to VM, with download RDP highlighted.":::

    > [!NOTE]
    > There are multiple ways to connect to a VM. This tutorial uses Remote Desktop Protocol (RDP) to connect to the VM, but a newer solution to use Azure Bastion is an alternative that would work well, if you have [deployed Bastion in your environment](/azure/bastion/tutorial-create-host-portal). You can also [use SSH to connect to your VM](/azure/virtual-machines/windows/connect-ssh).

1. Select **Download RDP File**.
1. Close the **Connect to virtual machine** form.
1. To connect to your VM, open the downloaded RDP file.
1. When prompted, select **Connect**.
    - On a Mac, you need an RDP client such as this [Remote Desktop Client](https://apps.apple.com/app/microsoft-remote-desktop-10/id1295203466?mt=12) from the Mac App Store.

1. Enter the username and password you specified when creating the virtual machine, then choose **OK**.

1. You might receive a certificate warning during the sign-in process. Choose **Yes** or **Continue** to proceed with the connection.

## Install SqlPackage

[Download and install the latest version of SqlPackage](/sql/tools/sqlpackage-download). For more information, see [SqlPackage](/sql/tools/sqlpackage).

## Create a firewall rule to allow the VM access to the database

First, add the virtual machine's public IP address to the server's firewall. The following steps create a server-level IP firewall rule for your virtual machine's public IP address, and enable connectivity from the virtual machine.

1. Select **SQL databases** from the left-hand menu and then select your database on the **SQL databases** page. The overview page for your database opens, showing you the fully qualified server name (for example: `sql-svr.database.windows.net`) and provides options for further configuration.

   :::image type="content" source="media/database-import-export-azure-services-off/server-name.png" alt-text="Screenshot of the Azure portal, database overview page, with the server name highlighted." lightbox="media/database-import-export-azure-services-off/server-name.png":::

1. Copy this fully qualified server name to use when connecting to your server and its databases.

1. Select **Set server firewall** on the toolbar.

1. On the **Networking** page, in the **Public access** tab, in the **Firewall settings** section, select **Add your client IPv4 address**. This adds your virtual machine's public IP address to a new server-level IP firewall rule. A server-level IP firewall rule can open port 1433 for a single IP address or a range of IP addresses.

1. Select **Save**. A server-level IP firewall rule is created for your virtual machine's public IP address opening port 1433 on the server.

## Export a database using SqlPackage

To export an Azure SQL Database using the [SqlPackage](/sql/tools/sqlpackage) command-line utility, see [Export parameters and properties](/sql/tools/sqlpackage#export-parameters-and-properties). The SqlPackage utility ships with the latest versions of [SQL Server Management Studio](/sql/ssms/download-sql-server-management-studio-ssms) and [SQL Server Data Tools](/sql/ssdt/download-sql-server-data-tools-ssdt), or you can download the latest version of [SqlPackage](/sql/tools/sqlpackage-download).

For more information and step to create a BACPAC file, see [Export to a BACPAC file](database-export.md).

We recommend the use of the SqlPackage utility for scale and performance in most production environments. For a SQL Server Customer Advisory Team blog about migrating using BACPAC files, see [Migrating from SQL Server to Azure SQL Database using BACPAC Files](/archive/blogs/sqlcat/migrating-from-sql-server-to-azure-sql-database-using-bacpac-files).

This example shows how to export a database using SqlPackage with Active Directory Universal Authentication. Replace with values that are specific to your environment.

```cmd
SqlPackage /a:Export /tf:testExport.bacpac /scs:"Data Source=<servername>.database.windows.net;Initial Catalog=MyDB;" /ua:True /tid:"apptest.onmicrosoft.com"
```

## Import a database using SqlPackage

To import a SQL Server database using the [SqlPackage](/sql/tools/sqlpackage) command-line utility, see [import parameters and properties](/sql/tools/sqlpackage#import-parameters-and-properties). SqlPackage has the latest [SQL Server Management Studio](/sql/ssms/download-sql-server-management-studio-ssms) and [SQL Server Data Tools](/sql/ssdt/download-sql-server-data-tools-ssdt). You can also download the latest version of [SqlPackage](/sql/tools/sqlpackage-download). 

For scale and performance, we recommend using SqlPackage in most production environments rather than using the Azure portal. For a SQL Server Customer Advisory Team blog about migrating using `BACPAC` files, see [migrating from SQL Server to Azure SQL Database using BACPAC Files](/archive/blogs/sqlcat/migrating-from-sql-server-to-azure-sql-database-using-bacpac-files).

The following SqlPackage command imports the `AdventureWorks2022` database from local storage to an Azure SQL Database. It creates a new database called `myMigratedDatabase` with a **Premium** service tier and a **P6** Service Objective. Change these values as appropriate for your environment.

```cmd
SqlPackage /a:import /tcs:"Data Source=<serverName>.database.windows.net;Initial Catalog=myMigratedDatabase>;User Id=<userId>;Password=<password>" /sf:AdventureWorks2022.bacpac /p:DatabaseEdition=Premium /p:DatabaseServiceObjective=P6
```

> [!IMPORTANT]
> To connect to Azure SQL Database from behind a corporate firewall, the firewall must have port 1433 open.

This example shows how to import a database using SqlPackage with Active Directory Universal Authentication.

```cmd
SqlPackage /a:Import /sf:testExport.bacpac /tdn:NewDacFX /tsn:apptestserver.database.windows.net /ua:True /tid:"apptest.onmicrosoft.com"
```

## Performance considerations

Export speeds vary due to many factors (for example, data shape) so it's impossible to predict what speed should be expected. SqlPackage can take considerable time, particularly for large databases.

For best performance, try the following strategies:

1. Make sure no other workload is running on the database. Create a copy before export might be the best solution to ensure no other workloads are running.
1. Increase database service level objective (SLO) to better handle the export workload (primarily read I/O). If the database is currently GP_Gen5_4, perhaps a Business Critical tier would help with read workload.
1. Make sure there are clustered indexes particularly for large tables.
1. Virtual machines (VMs) should be in the same region as the database to help avoid network constraints.
1. VMs should have SSD with adequate size for generating temp artifacts before uploading to blob storage.
1. VMs should have adequate core and memory configuration for the specific database.

## Store the imported or exported BACPAC file

The BACPAC file can be stored in [Azure Blobs](/azure/storage/blobs/storage-blobs-overview), or [Azure Files](/azure/storage/files/storage-files-introduction).

To achieve the best performance, use Azure Files. SqlPackage operates with the filesystem so it can access Azure Files directly.

To reduce cost, use Azure Blobs, which cost less than a premium Azure file share. However, it requires you to copy the [BACPAC file](/sql/relational-databases/data-tier-applications/data-tier-applications#bacpac) between the blob and the local file system before the import or export operation. As a result, the process takes longer.

To upload or download BACPAC files, see [Transfer data with AzCopy and Blob storage](/azure/storage/common/storage-use-azcopy-v10#transfer-data), and [Transfer data with AzCopy and file storage](/azure/storage/common/storage-use-azcopy-files).

Depending on your environment, you might need to [Configure Azure Storage firewalls and virtual networks](/azure/storage/common/storage-network-security).

## Related content

- To learn how to connect to and query an imported SQL Database, see [Quickstart: Azure SQL Database: Use SQL Server Management Studio to connect and query data](connect-query-ssms.md).
- For a SQL Server Customer Advisory Team blog about migrating using BACPAC files, see [Migrating from SQL Server to Azure SQL Database using BACPAC Files](https://techcommunity.microsoft.com/t5/DataCAT/Migrating-from-SQL-Server-to-Azure-SQL-Database-using-Bacpac/ba-p/305407).
- For a discussion of the entire SQL Server database migration process, including performance recommendations, see [SQL Server database migration to Azure SQL Database](migrate-to-database-from-sql-server.md).
- To learn how to manage and share storage keys and shared access signatures securely, see [Azure Storage Security Guide](/azure/storage/blobs/security-recommendations).
