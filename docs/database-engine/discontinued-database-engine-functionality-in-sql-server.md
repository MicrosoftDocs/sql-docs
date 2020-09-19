---
title: Discontinued database engine functionality
description: Learn which database engine functionality and features were discontinued in SQL Server 2019 (15.x), SQL Server 2016 (13.x), and previous versions.
ms.custom: "seo-lt-2019"
ms.date: 07/22/2020
ms.prod: sql
ms.prod_service: high-availability
ms.reviewer: ""
ms.technology: release-landing
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
ms.assetid: d686cdf0-d11d-4dba-9ec8-de1a5f189f25
author: MikeRayMSFT
ms.author: mikeray
monikerRange: ">= sql-server-linux-2017  || >= sql-server-2016 || = sqlallproducts-allversions"
---
# Discontinued database engine functionality in SQL Server
[!INCLUDE[sqlserver](../includes/applies-to-version/sqlserver.md)]

  This topic describes the [!INCLUDE[ssDE](../includes/ssde-md.md)] features that are no longer available in [!INCLUDE[ssCurrent](../includes/ssnoversion-md.md)].  

## Discontinued features in [!INCLUDE[ssSQLv15](../includes/sssqlv15-md.md)]  

- The following database scoped configuration options are discontinued:

  - `DISABLE_BATCH_MODE_ADAPTIVE_JOIN`
  - `DISABLE_BATCH_MODE_MEMORY_GRANT_FEEDBACK`
  - `DISABLE_INTERLEAVED_EXECUTION_TVF`

For current configuration options, see [ALTER DATABASE SCOPED CONFIGURATION (Transact-SQL)](../t-sql/statements/alter-database-scoped-configuration-transact-sql.md).

>[!NOTE]
>No features were discontinued in [!INCLUDE[ssSQLv14](../includes/sssqlv14-md.md)].

## Discontinued features in [!INCLUDE[ssSQL15](../includes/sssql15-md.md)]

- [!INCLUDE[ssSQL15](../includes/sssql15-md.md)] is a 64-bit application. 32-bit installation is discontinued, though some elements run as 32-bit components.  

- Compatibility level 90 is discontinued. For more information, see [ALTER DATABASE Compatibility Level &#40;Transact-SQL&#41;](../t-sql/statements/alter-database-transact-sql-compatibility-level.md).  

- ActiveX subsystem is discontinued. Use command line or PowerShell scripts instead.

- Startup parameters **-h** and **-g**. For more information, see [Database Engine Service Startup Options](/previous-versions/sql/2014/database-engine/configure-windows/database-engine-service-startup-options?view=sql-server-2014).

- Secure Sockets Layer (SSL) encryption is discontinued. Use Transport Layer Security (TLS) instead. For more information, see [Enable Encrypted Connections to the Database Engine](../database-engine/configure-windows/enable-encrypted-connections-to-the-database-engine.md).

## Previous Versions

- [Discontinued Database Engine Functionality in SQL Server 2014](/previous-versions/sql/2014/database-engine/discontinued-database-engine-functionality-in-sql-server-2016?view=sql-server-2014)

### See also

- [Deprecated database engine features in SQL Server 2019](deprecated-database-engine-features-in-sql-server-version-15.md)
- [Deprecated database engine features in SQL Server 2017](deprecated-database-engine-features-in-sql-server-2017.md)
- [Deprecated database engine features in SQL Server 2016](../database-engine/deprecated-database-engine-features-in-sql-server-2016.md)
- [Breaking changes to database engine features in SQL Server 2019](breaking-changes-to-database-engine-features-in-sql-server-version-15.md)
- [Breaking changes to database engine features in SQL Server 2017](breaking-changes-to-database-engine-features-in-sql-server-2017.md)
- [Breaking changes to database engine features in SQL Server 2016](breaking-changes-to-database-engine-features-in-sql-server-2016.md)
- [Deprecated features in SQL Server replication](../relational-databases/replication/deprecated-features-in-sql-server-replication.md)