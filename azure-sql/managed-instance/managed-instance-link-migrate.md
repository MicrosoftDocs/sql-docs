---
title: Migrate with the link
titleSuffix: Azure SQL Managed Instance
description: Learn how to use the Managed Instance link to migrate your SQL Server data to Azure SQL Managed Instance.
author: djordje-jeremic
ms.author: djjeremi
ms.reviewer: mathoma
ms.date: 06/15/2024
ms.service: azure-sql-managed-instance
ms.subservice: data-movement
ms.custom: ignite-2023
ms.topic: how-to
---

# Migrate with the link - Azure SQL Managed Instance
[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This article teaches you to migrate your SQL Server database to Azure SQL Managed Instance by using the [Managed Instance link](managed-instance-link-feature-overview.md). 

For a detailed migration guide, review [Migrate to Azure SQL Managed Instance](../migration-guides/managed-instance/sql-server-to-managed-instance-guide.md)

## Overview

The Managed Instance link enables migration from SQL Server hosted anywhere, to Azure SQL Managed Instance. The link uses Always On availability group technology to replicate changes nearly in real time from the primary SQL Server instance to the secondary SQL Managed Instance. The link provides the only truly online migration option between SQL Server and Azure SQL Managed Instance, since the only downtime is cutting over to the target SQL managed instance. 

Migrating with the link gives you: 

- The ability to test read only workloads on SQL Managed Instance before you finalize the migration to Azure
- The ability to keep the link and migration running for as long as you need, weeks and even months at a time
- Near real-time replication of data that provides the fastest available data replication to Azure
- The most minimum downtime migration compared to all other solutions available today
- Instantaneous cutover to the target SQL Managed Instance 
- The ability to migrate anytime you're ready
- The ability to migrate single or multiple databases from a single or multiple SQL Server instances to the same or multiple SQL managed instances in Azure
- The only true online migration to the Business Critical service tier


> [!NOTE]
> While you can only migrate one database per link, you can establish multiple links from the same SQL Server instance to the same SQL Managed Instance. 


## Prerequisites 

To use the link with Azure SQL Managed Instance for migration, you need the following prerequisites: 

- An active Azure subscription. If you don't have one, [create a free account](https://azure.microsoft.com/free/).
- [Supported version of SQL Server](managed-instance-link-feature-overview.md#prerequisites) with the required service update installed.

## Assess and discover

After you've verified that your source environment is supported, start with the pre-migration stage. Discover all of the existing data sources, assess migration feasibility, and identify any blocking issues that might prevent your migration. In the Discover phase, scan the network to identify all SQL Server instances and features used by your organization. 

You can use the following tools to discover SQL sources in your environment: 
- [Azure Migrate](/azure/migrate/migrate-services-overview) to assess migration suitability of on-premises servers, perform performance-based sizing, and provide cost estimations for running them in Azure. 
- [Microsoft Assessment and Planning Toolkit (the "MAP Toolkit")](https://www.microsoft.com/download/details.aspx?id=7826) to assess your current IT infrastructure. The toolkit provides a powerful inventory, assessment, and reporting tool to simplify the migration planning process.

After data sources have been discovered, assess any on-premises SQL Server instances that can be migrated to Azure SQL Managed Instance to identify migration blockers or compatibility issues. 

You can use the following tools to assess your source SQL Server instance: 
- [Azure SQL migration extension for Azure Data Studio](/azure/dms/migration-using-azure-data-studio)
- [Azure right-sized recommendations](/azure/dms/ads-sku-recommend)

For detailed guidance, review [pre-migration](../migration-guides/managed-instance/sql-server-to-managed-instance-guide.md). 

## Create target instance

After you've assessed your existing environment, and determined the appropriate service tier and hardware configuration for your target SQL managed instance, deploy your target instance by using the [Azure portal](instance-create-quickstart.md), [PowerShell](scripts/create-configure-managed-instance-powershell.md) or the [Azure CLI](scripts/create-configure-managed-instance-cli.md)

## Configure link

After your target SQL managed instance is created, configure a link between the database on your SQL Server instance and Azure SQL Managed Instance. First, [prepare your environment](managed-instance-link-preparation.md) and then configure a link by using [SQL Server Management Studio (SSMS)](managed-instance-link-configure-how-to-ssms.md) or [scripts](managed-instance-link-configure-how-to-scripts.md). 

## Data sync and cutover

After your link is established, and you're ready to migrate, follow these steps (typically during a maintenance window): 

1. Stop workload on the primary SQL Server database so the secondary database on SQL Managed Instance catches up. 
1. Validate all data has made it over to the secondary SQL managed instance database. 
1. [Fail over the link](managed-instance-link-failover-how-to.md) to the secondary SQL managed instance by choosing **Planned failover**. 
1. Cut over the application to connect to the SQL Managed Instance endpoint. 

## Validate migration

After you've cut over to the SQL managed instance target, monitor your application, test performance and remediate any issues. 

For details, review [post-migration](../migration-guides/managed-instance/sql-server-to-managed-instance-guide.md#post-migration). 

## Related content

For more information, see the following resources:

- [Migration overview](../migration-guides/managed-instance/sql-server-to-managed-instance-overview.md)
- [Compare migration options](../migration-guides/managed-instance//sql-server-to-managed-instance-overview.md#compare-migration-options)
- [Managed Instance link overview](managed-instance-link-feature-overview.md)