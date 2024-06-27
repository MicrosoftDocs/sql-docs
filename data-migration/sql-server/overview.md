---
title: "Migrate from SQL Server overview"
description: "Follow the five steps to a successful SQL Server migration to Azure SQL: Discovery, Assessment, Business case, Conversion, and Migration."
author: abhims14
ms.author: abhishekum
ms.reviewer: randolphwest
ms.date: 06/26/2024
ms.service: sql-managed-instance
ms.subservice: migration-guide
ms.topic: how-to
ms.custom:
  - sql-migration-content
---
# Migration overview: From SQL Server

[!INCLUDE [appliesto-sqlvm-sqldb-sqlmi](../includes/appliesto-sqlvm-sqldb-sqlmi.md)]

This article describes the five phases for a successful cloud migration from SQL Server to Azure SQL.

- [Discovery](#discovery)
- [Business case](#business-case)
- [Assessment](#assessment)
- [Conversion](#conversion)
- [Migration](#migration)

:::image type="content" source="media/migration-process-flow-small.png" alt-text="Diagram that shows a migration process flow.":::

## Discovery

When you start your cloud migration journey, it's critical to discover installed software inventory, web apps, and SQL Server instances and databases on servers running in your on-premises environment. This discovery helps you tailor a migration path to Azure SQL.

The Azure Migrate appliance performs this discovery using the Windows OS domain or non-domain credentials, or SQL Server authentication credentials that have access to your SQL Server instances and databases. This discovery process is agentless, meaning that nothing is installed on the servers. The Azure Migrate appliance supports discovery on various virtualization platforms like VMware, Microsoft Hyper-V, and physical environments.

To learn how to discover your on-premises SQL Servers, see the [Discover](pre-migration.md#discover) section later in this article.

## Business case

The business case capability helps you build a business proposal to understand how Azure can bring the most value to your business. It highlights:

- Total cost of ownership (TCO) between on-premises and Azure SQL.

- Year on year cashflow analysis.

- Resource utilization based insights to identify servers and workloads that are ideal for the cloud.

- Quick wins for migration and modernization, including end of support Windows OS and SQL versions.

- Long term cost savings by moving from a *capital* expenditure model to an *operating* expenditure model, by paying for only what you use.

## Assessment

Assessment is the analysis of configuration, utilization, and performance data collected during discovery. This analysis is used to measure the readiness, and estimate the effect, of migrating on-premises SQL Server instances to different Azure SQL targets. Assessments on SQL Server instances can be run using Azure Arc for SQL Server, or using Azure Migrate, or the Azure Database Migration Service extension in Azure Data Studio.

An Azure SQL assessment provides two sizing criteria:

- **As on-premises**: Assessments that make recommendations based on the on-premises SQL Server configuration alone.

- **Performance-based**: Assessments that make recommendations based on collected performance data.

After the assessment determines the readiness and the recommended Azure SQL deployment type, it computes a specific service tier and Azure SQL configuration (SKU size) that can meet or exceed the on-premises SQL Server performance. This calculation depends on whether you're using As on-premises or performance-based sizing criteria.

## Conversion

In heterogenous migrations, while you migrate data from one database to another, it's important to convert database schema and objects into equivalent Transact-SQL syntax, as the source and target databases engines are different. These database objects include tables, indexes, views, data types, Transact-SQL statements, stored procedures, and functions.

> [!NOTE]  
> The conversion phase isn't needed for SQL Server to Azure SQL migrations. SQL Server Migration Assistant (SSMA) performs the conversion while migrating to any of the Azure SQL targets. SSMA supports multiple sources, such as Oracle, MySQL, DB2, Sybase, and Microsoft Access, for both conversion and data migration.

## Migration

Migration is the last stage of of this process, in which the data is migrated from source database to the target database. Azure Database Migration Service (DMS) is a fully managed service designed to enable seamless migrations from multiple database sources to Azure data platforms. DMS offers minimal downtime, high reliability, and resiliency. DMS is available via various clients, including the Azure SQL migration extension for Azure Data Studio, Azure portal, PowerShell, and Azure CLI. To learn more about Azure Database Migration service, see [What is Azure Database Migration Service?](/azure/dms/dms-overview)

## Next step

> [!div class="nextstepaction"]
> [Migrate from SQL Server: Pre-migration](pre-migration.md)
