---
title: "Overview of Data Migration Assistant (SQL Server)"
description: Learn how to use Data Migration Assistant to migrate SQL Server databases to other instances of SQL Server, Azure SQL Database, or Azure SQL Managed Instance.
author: ajithkr-ms
ms.author: ajithkr
ms.reviewer: randolphwest
ms.date: 06/28/2024
ms.service: sql
ms.subservice: dma
ms.topic: overview
ms.custom:
  - UpdateFrequency5
  - sql-migration-content
helpviewer_keywords:
  - "Data Migration Assistant, overview"
---

# Overview of Data Migration Assistant

[!INCLUDE [deprecation-notice](includes/deprecation-notice.md)]

The Data Migration Assistant (DMA) helps you upgrade to a modern data platform by detecting compatibility issues that can affect database functionality when you:

- Upgrade to a new version of SQL Server
- Migrate to [Azure SQL Database](/azure/azure-sql/database/sql-database-paas-overview)
- Migrate to [Azure SQL Managed Instance](/azure/azure-sql/managed-instance/sql-managed-instance-paas-overview)

DMA recommends performance and reliability improvements for your target environment and allows you to move your schema, data, and uncontained objects from your source server to your target server.

For SQL Server large migrations (in terms of number and size of databases) to Azure, we recommend that you use the [Azure Database Migration Service](/azure/dms/dms-overview), which can migrate databases at scale.

DMA doesn't support database migrations to [Azure SQL Managed Instance](/azure/azure-sql/managed-instance/sql-managed-instance-paas-overview). Use the [Azure SQL Migration extension for Azure Data Studio](/azure/dms/migration-using-azure-data-studio) instead, which supports both online and offline database migrations to Azure SQL Managed Instance.

## Get Data Migration Assistant

To install DMA, download the latest version of the tool from the [Microsoft Download Center](https://www.microsoft.com/download/details.aspx?id=53595), and then run the `DataMigrationAssistant.msi` file.

## Capabilities

DMA brings you the following capabilities:

#### Assess on-premises SQL Server Instances migrating to Azure

Assess on-premises SQL Server instances migrating to Azure SQL Database or Azure SQL Managed Instance. The assessment workflow helps you to detect the following issues, which might affect your Azure SQL migration and provides detailed guidance on how to resolve them.

- Migration blocking issues: Discovers the compatibility issues that block migrating on-premises SQL Server database to Azure SQL Database or Azure SQL Managed Instance. DMA provides recommendations to help you address those issues.

- Partially supported or unsupported features: Detects partially supported or unsupported features that are currently in use by the source SQL Server instance. DMA provides a comprehensive set of recommendations, alternative approaches available in Azure, and mitigating steps so that you can incorporate them into your migration projects.

#### Discover issues that affect an upgrade

Discover issues that can affect an upgrade to an on-premises SQL Server. These are described as compatibility issues and are organized in the following categories:

- Breaking changes
- Behavior changes
- Deprecated features

#### Discover new features

Discover new features in the target SQL Server platform that the database can benefit from after an upgrade. These are described as feature recommendations and are organized in the following categories:

- Performance
- Security
- Storage

#### Migrate on-premises instances to SQL Server on Azure VMs

Migrate an on-premises SQL Server instance to a modern SQL Server instance hosted on-premises or on an Azure virtual machine (VM) that is accessible from your on-premises network. The Azure VM can be accessed using VPN or other technologies. The migration workflow helps you to migrate the following components:

- Schema of databases
- Data and users
- Server roles
- SQL Server and Windows logins

#### Assess on-premises SSIS packages migration to Azure

Assess on-premises SQL Server Integration Services (SSIS) packages migrating to Azure SQL Database or Azure SQL Managed Instance. The assessment helps to discover issues that can affect the migration. These are described as compatibility issues and are organized in the following categories:

- Migration blockers: discovers the compatibility issues that block migrating source packages to Azure. DMA provides recommendations to help you address those issues.

- Information issues: detects partially supported or deprecated features that are used in source packages.

#### Connect to databases after migration

After a successful migration, applications can connect to the target SQL databases seamlessly.

## Permissions

To run an assessment, you have to be a member of the SQL Server **sysadmin** role. The recommended display resolution is 1024x756.

## Supported source and target versions

DMA replaces all previous versions of SQL Server Upgrade Advisor and should be used for upgrades for most SQL Server versions. The following list shows the supported source and target versions for assessment:

:::row:::
    :::column:::
    **Supported sources**
    - SQL Server 2005 (deprecated)
    - SQL Server 2008
    - SQL Server 2008 R2
    - SQL Server 2012
    - SQL Server 2014
    - SQL Server 2016
    - SQL Server 2017
    - SQL Server 2019
    - SQL Server 2022
    - Amazon RDS for SQL Server
    :::column-end:::
    :::column:::
    **Supported targets**
    - SQL Server 2012
    - SQL Server 2014
    - SQL Server 2016
    - SQL Server 2017 on Windows and Linux
    - SQL Server 2019 on Windows and Linux
    - SQL Server 2022 on Windows and Linux
    - [Azure SQL Database](/azure/azure-sql/database/sql-database-paas-overview)
    - [Azure SQL Managed Instance](/azure/azure-sql/managed-instance/sql-managed-instance-paas-overview) (assessment only)
    - [SQL Server on Azure Virtual Machines](/azure/azure-sql/virtual-machines/windows/sql-server-on-azure-vm-iaas-what-is-overview)
    :::column-end:::
:::row-end:::

## Related content

- [Assess your SQL Server Migration](dma-assesssqlonprem.md)
- [Data Migration Assistant: Configuration settings](dma-configurationsettings.md)
- [Migrate on-premises SQL Server using Data Migration Assistant](dma-migrateonpremsql.md)
- [Data Migration Assistant: Best Practices](dma-bestpractices.md)
