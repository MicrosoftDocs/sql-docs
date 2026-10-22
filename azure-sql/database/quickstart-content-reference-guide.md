---
title: Single Database Quickstart Content Reference
description: "Find a content reference of all the quickstarts that help you quickly get started with Azure SQL Database."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: vanto
ms.date: 01/26/2026
ms.service: azure-sql-database
ms.subservice: service-overview
ms.topic: get-started
ms.custom:
  - sqldbrb=1
monikerRange: "=azuresql || =azuresql-db"
---
# Getting started with single databases in Azure SQL Database

[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

[A single database](single-database-overview.md) is fully managed platform as a service (PaaS) database as a service (DbaaS) that is ideal storage engine for the modern cloud-born applications. In this section, you'll learn how to quickly configure and create a single database in Azure SQL Database.

## Quickstart overview

In this section, you'll see an overview of available articles that can help you to quickly get started with single databases. The following quickstarts enable you to quickly create a single database, configure a server-level firewall rule, and then import a database into the new single database using a `.bacpac` file:

- [Create a single database using the Azure portal](single-database-create-quickstart.md).
- After creating the database, you would need to [secure your database by configuring firewall rules](firewall-create-server-level-portal-quickstart.md).

## Automate management operations

You can use PowerShell or the Azure CLI to create, configure, and scale your database.

- [Create and configure a single database using PowerShell](scripts/create-and-configure-database-powershell.md) or [Azure CLI](scripts/create-and-configure-database-cli.md)
- [Update your single database and scale resources using PowerShell](scripts/monitor-and-scale-database-powershell.md) or [Azure CLI](scripts/monitor-and-scale-database-cli.md)

<a id="migrating-to-a-single-database-with-minimal-downtime"></a>

## Migrate to a single database with minimal downtime

These quickstarts enable you to quickly create or import your database to Azure using a `.bacpac` file. However, `.bacpac` and `.dacpac` files are designed to quickly move databases across different versions of SQL Server and within Azure SQL, or to implement continuous integration in your DevOps pipeline. However, this method isn't designed for migration of your production databases with minimal downtime, because you would need to stop adding new data, wait for the export of the source database to a `.bacpac` file to complete, and then wait for the import into Azure SQL Database to complete. All of this waiting results in downtime of your application, especially for large databases. To move your production database, you need a better way to migrate that guarantees minimal downtime of migration. For this, use the [Data Migration Service (DMS)](/azure/dms/tutorial-sql-server-to-azure-sql?toc=/azure/sql-database/toc.json) to migrate your database with the minimal downtime. DMS accomplishes this by incrementally pushing the changes made in your source database to the single database being restored. This way, you can quickly switch your application from source to target database with the minimal downtime.

## Hands-on learning modules

The following free Learn modules help you learn about Azure SQL Database.

- [Provision a database in SQL Database to store application data](/training/modules/provision-azure-sql-db/)
- [Develop and configure an ASP.NET application that queries a database in Azure SQL Database](/training/modules/develop-app-that-queries-azure-sql/)
- [Secure your database in Azure SQL Database](/training/modules/secure-your-azure-sql-database/)

## Related content

- [Features comparison: Azure SQL Database and Azure SQL Managed Instance](features-comparison.md)
- [Tutorial: Secure a database in Azure SQL Database](secure-database-tutorial.md)
- [Configure and manage content reference - Azure SQL Database](how-to-content-reference-guide.md)
- [Azure PowerShell samples for Azure SQL Database](powershell-script-content-guide.md)
- [the Azure CLI](az-cli-script-samples-content-guide.md)
- [Create and manage servers and single databases in Azure SQL Database](single-database-manage.md)
