---
title: Discontinued database engine functionality
description: Learn which database engine functionality and features were discontinued in SQL Server.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 07/09/2024
ms.service: sql
ms.subservice: release-landing
ms.topic: conceptual
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
monikerRange: ">=sql-server-linux-2017 || >=sql-server-2016"
---
# Discontinued Database Engine functionality in SQL Server

[!INCLUDE [sqlserver](../includes/applies-to-version/sqlserver.md)]

This article describes the [!INCLUDE [ssDE](../includes/ssde-md.md)] features that are no longer available in [!INCLUDE [ssnoversion](../includes/ssnoversion-md.md)].

## Discontinued features in SQL Server 2022 (16.x)

- The following Machine Learning Services packages are no longer included with installation of [!INCLUDE [sssql22-md](../includes/sssql22-md.md)]. Instead, you can install any custom packages as desired. For more information, see [What's new in SQL Server Machine Learning Services?](../machine-learning/what-s-new-in-sql-server-machine-learning-services.md)

  | Language | Package |
  | --- | --- |
  | Python | [microsoftml](../machine-learning/python/ref-py-microsoftml.md) (Python package in SQL Server Machine Learning Services) |
  | R | [olapR](../machine-learning/r/ref-r-olapr.md) (R package in SQL Server Machine Learning Services) |
  | R | [sqlrutils](../machine-learning/r/ref-r-sqlrutils.md) (R package in SQL Server Machine Learning Services) |
  | R | [MicrosoftML](../machine-learning/r/ref-r-microsoftml.md) (R package in SQL Server Machine Learning Services) |

- SQL Server Big Data Clusters will be retired. See information in [Introducing SQL Server Big Data Clusters](../big-data-cluster/big-data-cluster-overview.md).

- [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] PolyBase scale-out groups will be retired. Scale out group functionality is removed from the product in [!INCLUDE [sssql22-md](../includes/sssql22-md.md)]. PolyBase data virtualization continues to be fully supported as a scale-up feature in [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)].

- Support for Hadoop (HDFS) external data sources will be retired for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] PolyBase.

- See [Changes to PolyBase support in SQL Server](../big-data-cluster/big-data-options.md#changes-to-polybase-support-in-sql-server).

- In [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] and later versions, Hadoop external data sources are no longer supported. You must manually recreate external data sources previously created with `TYPE = HADOOP`, and any external table that uses this external data source. You must also configure your external data sources to use new connectors when connecting to Azure Storage.

  | External Data Source | From | To |
  | --- | --- | --- |
  | Azure Blob Storage | `wasb[s]` | `abs` |
  | ADLS Gen 2 | `abfs[s]` | `adls` |

- In July 2024, Stretch Database was discontinued in all supported versions of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)].

## Discontinued features in SQL Server 2019 (15.x)

The following database scoped configuration options are discontinued:

- `DISABLE_BATCH_MODE_ADAPTIVE_JOIN`
- `DISABLE_BATCH_MODE_MEMORY_GRANT_FEEDBACK`
- `DISABLE_INTERLEAVED_EXECUTION_TVF`

For current configuration options, see [ALTER DATABASE SCOPED CONFIGURATION](../t-sql/statements/alter-database-scoped-configuration-transact-sql.md).

In July 2024, Stretch Database was discontinued in all supported versions of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)].

## Discontinued features in SQL Server 2017 (14.x)

In July 2024, Stretch Database was discontinued in all supported versions of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)].

No other features were discontinued in [!INCLUDE [sssql14](../includes/sssql17-md.md)].

## Discontinued features in SQL Server 2016 (13.x)

- [!INCLUDE [sssql15-md](../includes/sssql16-md.md)] is a 64-bit application. 32-bit installation is discontinued, though some elements run as 32-bit components.

- Compatibility level 90 is discontinued. For more information, see [ALTER DATABASE (Transact-SQL) compatibility level](../t-sql/statements/alter-database-transact-sql-compatibility-level.md).

- ActiveX subsystem is discontinued. Use command line or PowerShell scripts instead.

- Startup parameters `-h` and `-g`. For more information, see [Database Engine Service Startup Options](/previous-versions/sql/2014/database-engine/configure-windows/database-engine-service-startup-options?view=sql-server-2014&preserve-view=true).

- Secure Sockets Layer (SSL) encryption is discontinued. Use Transport Layer Security (TLS) instead. For more information, see [Configure SQL Server Database Engine for encrypting connections](configure-windows/configure-sql-server-encryption.md).

- The **precompute rank** server configuration option was discontinued beginning with [!INCLUDE [sql2008-md](../includes/sql2008-md.md)]. The article was removed from documentation.

- In July 2024, Stretch Database was discontinued in all supported versions of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)].

## Previous versions

- [Discontinued Database Engine Functionality in SQL Server 2014](/previous-versions/sql/2014/database-engine/discontinued-database-engine-functionality-in-sql-server-2016?view=sql-server-2014&preserve-view=true)

## Related content

- [Deprecated Database Engine features in SQL Server 2022 (16.x)](deprecated-database-engine-features-in-sql-server-2022.md)
- [Deprecated Database Engine features in SQL Server 2019 (15.x)](deprecated-database-engine-features-in-sql-server-2019.md)
- [Deprecated Database Engine features in SQL Server 2017 (14.x)](deprecated-database-engine-features-in-sql-server-2017.md)
- [Deprecated Database Engine features in SQL Server 2016 (13.x)](deprecated-database-engine-features-in-sql-server-2016.md)
- [Breaking changes to Database Engine features in SQL Server 2019](breaking-changes-to-database-engine-features-in-sql-server-2019.md)
- [Breaking changes to Database Engine features in SQL Server 2017](breaking-changes-to-database-engine-features-in-sql-server-2017.md)
- [Breaking changes to Database Engine features in SQL Server 2016](breaking-changes-to-database-engine-features-in-sql-server-2016.md)
- [Deprecated Features in SQL Server Replication](../relational-databases/replication/deprecated-features-in-sql-server-replication.md)
