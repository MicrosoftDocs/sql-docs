---
title: Discontinued database engine functionality
description: Learn which database engine functionality and features were discontinued in SQL Server.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 10/18/2022
ms.service: sql
ms.subservice: release-landing
ms.topic: conceptual
ms.custom:
  - seo-lt-2019
  - event-tier1-build-2022
helpviewer_keywords:
  - "VIA protocol"
  - "unsupported features [SQL Server]"
  - "SQL Mail"
  - "discontinued functionality [SQL Server]"
  - "RESTORE WITH DBO_ONLY"
  - "BACKUP WITH PASSWORD"
  - "user instances enabled"
  - "BACKUP WITH MEDIAPASSWORD"
  - "AWE"
  - "SQL-DMO"
  - "*= and =*"
  - "80 compatibility levels"
  - "COMPUTE BY"
  - "user instance timeout"
  - "sp_dropalias"
  - "COMPUTE"
  - "SSL"
  - "WITH APPEND"
  - "sys.database_principal_aliases"
  - "sp_dboption"
  - "DATABASEPROPERTY"
  - "FASTFIRSTROW hint"
  - "SET DISABLE_DEF_CNST_CHK"
monikerRange: ">= sql-server-linux-2017 || >= sql-server-2016"
---
# Discontinued Database Engine functionality in SQL Server

[!INCLUDE[sqlserver](../includes/applies-to-version/sqlserver.md)]

This article describes the [!INCLUDE[ssDE](../includes/ssde-md.md)] features that are no longer available in [!INCLUDE[ssnoversion](../includes/ssnoversion-md.md)].

## Discontinued features in [!INCLUDE[sssql22](../includes/sssql22-md.md)]

- The following Machine Learning Services packages are no longer included with installation of [!INCLUDE [sssql22-md](../includes/sssql22-md.md)]. Instead, you can install any custom packages as desired. For more information, see [What's new in SQL Server Machine Learning Services?](../machine-learning/what-s-new-in-sql-server-machine-learning-services.md)

    | **Language** | **Package** |
    | :-- | :-- |
    | Python | [microsoftml](../machine-learning/python/ref-py-microsoftml.md)|
    | R | [olapR](../machine-learning/r/ref-r-olapr.md) |
    | R | [sqlrutils](../machine-learning/r/ref-r-sqlrutils.md) |
    | R | [MicrosoftML (R)](../machine-learning/r/ref-r-microsoftml.md) |

- SQL Server Big Data Clusters will be retired. See information in [[!INCLUDE[big-data-cluster](../includes/ssbigdataclusters-ss-nover.md)] overview](../big-data-cluster/big-data-cluster-overview.md).

- SQL Server PolyBase scale-out groups will be retired. Scale out group functionality is removed from the product in [!INCLUDE [sssql22-md](../includes/sssql22-md.md)]. PolyBase data virtualization continues to be fully supported as a scale-up feature in SQL Server. 

- Support for Hadoop (HDFS) external data sources will be retired for SQL Server PolyBase.

- See [Changes to PolyBase support in SQL Server](../big-data-cluster/big-data-options.md#changes-to-polybase-support-in-sql-server).


- Starting in [!INCLUDE [sssql22-md](../includes/sssql22-md.md)], Hadoop external data sources are no longer supported. It is required to manually recreate external data sources previously created with `TYPE = HADOOP`, and any external table that uses this external data source. Users will also need to configure their external data sources to use new connectors when connecting to Azure Storage.

| External Data Source | From | To |
|:--|:--|:--|
| Azure Blob Storage | wasb[s] | abs |
| ADLS Gen 2 | abfs[s] | adls |


## Discontinued features in [!INCLUDE[sssql19](../includes/sssql19-md.md)]

- The following database scoped configuration options are discontinued:

  - `DISABLE_BATCH_MODE_ADAPTIVE_JOIN`
  - `DISABLE_BATCH_MODE_MEMORY_GRANT_FEEDBACK`
  - `DISABLE_INTERLEAVED_EXECUTION_TVF`

For current configuration options, see [ALTER DATABASE SCOPED CONFIGURATION (Transact-SQL)](../t-sql/statements/alter-database-scoped-configuration-transact-sql.md).

> [!NOTE]  
> No features were discontinued in [!INCLUDE[sssql14](../includes/sssql17-md.md)].

## Discontinued features in [!INCLUDE[sssql15-md](../includes/sssql16-md.md)]

- [!INCLUDE[sssql15-md](../includes/sssql16-md.md)] is a 64-bit application. 32-bit installation is discontinued, though some elements run as 32-bit components.

- Compatibility level 90 is discontinued. For more information, see [ALTER DATABASE Compatibility Level &#40;Transact-SQL&#41;](../t-sql/statements/alter-database-transact-sql-compatibility-level.md).

- ActiveX subsystem is discontinued. Use command line or PowerShell scripts instead.

- Startup parameters **-h** and **-g**. For more information, see [Database Engine Service Startup Options](/previous-versions/sql/2014/database-engine/configure-windows/database-engine-service-startup-options?view=sql-server-2014&preserve-view=true).

- Secure Sockets Layer (SSL) encryption is discontinued. Use Transport Layer Security (TLS) instead. For more information, see [Enable Encrypted Connections to the Database Engine](../database-engine/configure-windows/enable-encrypted-connections-to-the-database-engine.md).

- `precompute rank` Server configuration option was discontinued beginning with SQL Server 2008. The article has been removed from documentation.

## Previous versions

- [Discontinued Database Engine Functionality in SQL Server 2014](/previous-versions/sql/2014/database-engine/discontinued-database-engine-functionality-in-sql-server-2016?view=sql-server-2014&preserve-view=true)

## See also

- [Deprecated database engine features in SQL Server 2019](./deprecated-database-engine-features-in-sql-server-2019.md)
- [Deprecated database engine features in SQL Server 2017](deprecated-database-engine-features-in-sql-server-2017.md)
- [Deprecated database engine features in SQL Server 2016](../database-engine/deprecated-database-engine-features-in-sql-server-2016.md)
- [Breaking changes to database engine features in SQL Server 2019](./breaking-changes-to-database-engine-features-in-sql-server-2019.md)
- [Breaking changes to database engine features in SQL Server 2017](breaking-changes-to-database-engine-features-in-sql-server-2017.md)
- [Breaking changes to database engine features in SQL Server 2016](breaking-changes-to-database-engine-features-in-sql-server-2016.md)
- [Deprecated features in SQL Server replication](../relational-databases/replication/deprecated-features-in-sql-server-replication.md)
