---
title: Getting Started Content Reference
titleSuffix: Azure SQL Managed Instance
description: "A reference for content that helps you get started with Azure SQL Managed Instance. "
author: MashaMSFT
ms.author: mathoma
ms.reviewer: vanto, mlandzic, randolphwest
ms.date: 01/26/2026
ms.service: azure-sql-managed-instance
ms.subservice: deployment-configuration
ms.topic: quickstart
ms.custom:
  - sqldbrb=1
  - mode-other
---
# Get started with Azure SQL Managed Instance

[!INCLUDE [appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

[Azure SQL Managed Instance](sql-managed-instance-paas-overview.md) creates a database with near 100% compatibility with the latest SQL Server (Enterprise Edition) database engine, providing a native [virtual network](/azure/virtual-network/virtual-networks-overview) implementation that addresses common security concerns, and a [business model](https://azure.microsoft.com/pricing/details/sql-database/) favorable for existing SQL Server customers.

In this article, you find references to content that teach you how to quickly configure and create a SQL Managed Instance and migrate your databases.

## Quickstart overview

The following quickstarts enable you to quickly create a SQL Managed Instance, configure a virtual machine or point-to-site VPN connection for client application, and restore a database to your new SQL Managed Instance using a `.bak` file.

### Configure environment

As a first step, you need to create your first SQL Managed Instance with the network environment where it will be placed. You must enable connection from the computer or virtual machine where you're executing queries to SQL Managed Instance. You can use the following guides:

- [Create a SQL Managed Instance using the Azure portal](instance-create-quickstart.md). In the Azure portal, you configure the necessary parameters (username and password, number of cores, and max storage amount), and automatically create the Azure network environment without the need to know about networking details and infrastructure requirements. You just make sure that you have a [subscription type](resource-limits.md#supported-subscription-types) that is currently allowed to create a SQL Managed Instance. If you have your own network that you want to use or you want to customize the network, see [configure an existing virtual network for Azure SQL Managed Instance](vnet-existing-add-subnet.md) or [create a virtual network for Azure SQL Managed Instance](virtual-network-subnet-create-arm-template.md).
- A SQL Managed Instance is created in its own virtual network with no public endpoint. For client application access, you can either **create a VM in the same VNet (different subnet)** or **create a point-to-site VPN connection to the VNet from your client computer** using one of these quickstarts:
  - Enable [public endpoint](public-endpoint-configure.md) on your SQL Managed Instance to access your data directly from your environment.
  - Create [Azure Virtual Machine in the SQL Managed Instance virtual network](connect-vm-instance-configure.md) for client application connectivity, including SQL Server Management Studio.
  - Set up [point-to-site VPN connection to your SQL Managed Instance](point-to-site-p2s-configure.md) from your client computer on which you have SQL Server Management Studio and other client connectivity applications. This is other of two options for connectivity to your SQL Managed Instance and to its virtual network.

  > [!NOTE]  
  > - You can also use express route or site-to-site connection from your local network, but these approaches are out of the scope of these quickstarts.
  > - If you change retention period from `0` (unlimited retention) to any other value, retention only applies to logs written after retention value was changed (logs written during the period when retention was set to *unlimited* are preserved, even after retention is enabled).

As an alternative to manual creation of SQL Managed Instance, you can use [PowerShell](scripts/create-configure-managed-instance-powershell.md), [PowerShell with Resource Manager template](create-template-quickstart.md), or [Azure CLI](/cli/azure/sql/mi#az-sql-mi-create) to script and automate this process.

### Migrate your databases

After you create a SQL Managed Instance and configure access, you can start migrating your SQL Server databases. Migration can fail if the source database uses unsupported features. To avoid failures, check for any issues that could block migration to a SQL Managed Instance, such as the existence of [FILESTREAM](/sql/relational-databases/blob/filestream-sql-server) or multiple log files. If you resolve these issues, your databases are ready to migrate to SQL Managed Instance.

When you're sure that you can migrate your database to a SQL Managed Instance, use the native SQL Server restore capabilities to restore a database into a SQL Managed Instance from a `.bak` file. You can use this method to migrate databases from SQL Server database engine installed on-premises or Azure Virtual Machines. For a quickstart, see [Quickstart: Restore a database to Azure SQL Managed Instance with SSMS](restore-sample-database-quickstart.md). In this quickstart, you restore from a `.bak` file stored in Azure Blob storage using the `RESTORE` Transact-SQL command.

> [!TIP]  
> To use the `BACKUP` Transact-SQL command to create a backup of your database in Azure Blob storage, see [SQL Server backup to URL](/sql/relational-databases/backup-restore/sql-server-backup-to-url).

These quickstarts enable you to quickly create, configure, and restore database backup to a SQL Managed Instance. In some scenarios, you need to customize or automate deployment of SQL Managed Instance and the required networking environment. These scenarios are described in the following sections.

## Customize network environment

Although the virtual network and subnet can be automatically configured when the instance is [created using the Azure portal](instance-create-quickstart.md), you might want to create them before you start creating instances in SQL Managed Instance. By creating the virtual network and subnet, you can configure the parameters of virtual network and subnet. The easiest way to create and configure the network environment is to use the [Azure Resource deployment](virtual-network-subnet-create-arm-template.md) template that creates and configures your network and subnet where the instance will be placed. You just need to press the Azure Resource Manager deploy button and populate the form with parameters.

As an alternative, you can also use this [PowerShell script](https://powershellmagazine.com/2018/07/23/configuring-azure-environment-to-set-up-azure-sql-database-managed-instance-preview) to automate creation of the network.

If you already have a virtual network and subnet where you want to deploy your SQL Managed Instance, make sure that your virtual network and subnet satisfy the [networking requirements](connectivity-architecture-overview.md#network-requirements). Use this [PowerShell script to verify that your subnet is properly configured](vnet-existing-add-subnet.md). This script validates your network and reports any issues. It tells you what should be changed and then offers to make the necessary changes in your virtual network and subnet. Run this script if you don't want to configure your virtual network and subnet manually. You can also run it after any major reconfiguration of your network infrastructure. If you want to create and configure your own network, see [Connectivity architecture for Azure SQL Managed Instance](connectivity-architecture-overview.md).

## Migrate to a SQL managed instance

The previously mentioned quickstarts enable you to quickly set up a SQL managed instance and move your databases using the native `RESTORE` capability. This approach is a good starting point if you want to complete quick proof-of concepts and verify that your solution can work on SQL Managed Instance.

However, to migrate your production database or even development or test databases that you want to use for some performance test, consider using other techniques:

- **Performance testing**: Measure baseline performance metrics on your source SQL Server instance and compare them with the performance metrics on the destination SQL Managed Instance where you migrated the database. Learn more about the [best practices for performance comparison](https://techcommunity.microsoft.com/blog/azuresqlblog/the-best-practices-for-performance-comparison-between-azure-sql-managed-instance/683210).
- **Online migration**: With the native `RESTORE` described in this article, you have to wait for the databases to be restored (and copied to Azure Blob storage if not already stored there). This process causes some downtime of your application, especially for larger databases. To move your production database, use the [Data Migration service (DMS)](/azure/dms/tutorial-sql-server-to-managed-instance?toc=%2fazure%2fsql-database%2ftoc.json) to migrate your database with minimal downtime. DMS accomplishes this goal by incrementally pushing the changes made in your source database to the SQL Managed Instance database being restored. This way, you can quickly switch your application from source to target database with minimal downtime.

Learn more about the [recommended migration process](../migration-guides/managed-instance/sql-server-to-managed-instance-guide.md).

## Related content

- [Features comparison: Azure SQL Database and Azure SQL Managed Instance](../database/features-comparison.md)
- [T-SQL differences between SQL Server and Azure SQL Managed Instance](transact-sql-tsql-differences-sql-server.md)
- [Technical characteristics of SQL Managed Instance](resource-limits.md#service-tier-characteristics)
- [Azure SQL Managed Instance content reference](how-to-content-reference-guide.md)
- [Modifiable configuration reference for Azure SQL Managed Instance](modifiable-configuration-reference.md)
