---
title: "Overview of Data Migration Assistant (SQL Server)"
description: Learn how to use Data Migration Assistant to migrate SQL Server databases to other SQL Server or Azure databases
author: rajeshsetlem
ms.author: rajpo
ms.reviewer: randolphwest
ms.date: 05/24/2022
ms.service: sql
ms.subservice: dma
ms.topic: conceptual
helpviewer_keywords:
  - "Data Migration Assistant, overview"
---

# Overview of Data Migration Assistant

The Data Migration Assistant (DMA) helps you upgrade to a modern data platform by detecting compatibility issues that can impact database functionality in your new version of SQL Server or Azure SQL Database. DMA recommends performance and reliability improvements for your target environment and allows you to move your schema, data, and uncontained objects from your source server to your target server.

> [!NOTE]
> For large migrations (in terms of number and size of databases), we recommend that you use the [Azure Database Migration Service](/azure/dms/dms-overview), which can migrate databases at scale.
  
## Get Data Migration Assistant

To install DMA, download the latest version of the tool from the [Microsoft Download Center](https://www.microsoft.com/download/details.aspx?id=53595), and then run the **DataMigrationAssistant.msi** file.

## Capabilities

- Assess on-premises SQL Server instance(s) migrating to Azure SQL database(s). The assessment workflow helps you to detect the following issues that can affect Azure SQL database migration and provides detailed guidance on how to resolve them.

  - Migration blocking issues: Discovers the compatibility issues that block migrating on-premises SQL Server database(s) to Azure SQL Database(s). DMA provides recommendations to help you address those issues.

  - Partially supported or unsupported features: Detects partially supported or unsupported features that are currently in use on the source SQL Server instance. DMA provides a comprehensive set of recommendations, alternative approaches available in Azure, and mitigating steps so that you can incorporate them into your migration projects.

- Discover issues that can affect an upgrade to an on-premises SQL Server. These are described as compatibility issues and are organized in the following categories:

  - Breaking changes
  - Behavior changes
  - Deprecated features

- Discover new features in the target SQL Server platform that the database can benefit from after an upgrade. These are described as feature recommendations and are organized in the following categories:

  - Performance
  - Security
  - Storage

- Migrate an on-premises SQL Server instance to a modern SQL Server instance hosted on-premises or on an Azure virtual machine (VM) that is accessible from your on-premises network. The Azure VM can be accessed using VPN or other technologies. The migration workflow helps you to migrate the following components:

  - Schema of databases
  - Data and users
  - Server roles
  - SQL Server and Windows logins

- After a successful migration, applications can connect to the target SQL Server databases seamlessly.

- Assess on-premises SQL Server Integration Services (SSIS) package(s) migrating to Azure SQL Database or Azure SQL Managed Instance. The assessment helps to discover issues that can affect the migration. These are described as compatibility issues and are organized in the following categories:

  - Migration blockers: discovers the compatibility issues that block migrating source package(s) to Azure. DMA provides recommendations to help you address those issues.

  - Information issues: detects partially supported or deprecated features that are used in source package(s).

> [!NOTE]  
> DMA does not support database migrations to Azure SQL Managed Instance. Recommendation is to use the [Azure SQL migration extension for Azure Data Studio](/azure/dms/migration-using-azure-data-studio), which supports both online and offline database migrations to Azure SQL Managed Instance.

## Prerequisites

To run an assessment, you have to be a member of the SQL Server **sysadmin** role. The recommended display resolution is 1024x756.

## Supported source and target versions

DMA replaces all previous versions of SQL Server Upgrade Advisor and should be used for upgrades for most SQL Server versions. Supported source and target versions for assessment are:

### Sources

- SQL Server 2005 (deprecated)
- SQL Server 2008
- SQL Server 2008 R2
- SQL Server 2012
- SQL Server 2014
- SQL Server 2016
- SQL Server 2017
- SQL Server 2019
- Amazon RDS for SQL Server

### Targets

- SQL Server 2012
- SQL Server 2014
- SQL Server 2016
- SQL Server 2017 on Windows and Linux
- SQL Server 2019
- Azure SQL Database single database
- Azure SQL Managed Instance
- SQL server running on an Azure Virtual Machine

## See also

- [Assess your SQL Server Migration](../dma/dma-assesssqlonprem.md)
- [Data Migration Assistant: Configuration settings](../dma/dma-configurationsettings.md)
- [Migrate On-Premises SQL Server using Data Migration Assistant](../dma/dma-migrateonpremsql.md)
- [Data Migration Assistant: Best Practices](../dma/dma-bestpractices.md)
