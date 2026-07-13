---
title: "Database Engine: Breaking Changes"
titleSuffix: "SQL Server 2016"
description: Learn about Database Engine changes in SQL Server 2016 (13.x) and earlier that might break previous-version functionality when you upgrade.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: randolphwest
ms.date: 09/08/2025
ms.service: sql
ms.subservice: release-landing
ms.topic: release-notes
helpviewer_keywords:
  - "Database Engine [SQL Server], what's new"
  - "breaking changes [SQL Server]"
monikerRange: "=sql-server-2016"
---
# Breaking changes to Database Engine features in SQL Server 2016

[!INCLUDE [SQL Server 2016](../includes/applies-to-version/sqlserver2016.md)]

This article describes breaking changes in the [!INCLUDE [sssql15-md](../includes/sssql16-md.md)] [!INCLUDE [ssDE](../includes/ssde-md.md)] and earlier versions of [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)]. These changes might break applications, scripts, or functionalities that are based on earlier versions of [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)]. You might encounter these issues when you upgrade.

<a id="SQL15"></a>

## Breaking changes in SQL Server 2016

- The `sample_ms` column of `sys.dm_io_virtual_file_stats` has expanded from an **int** to a **bigint** data type.

- The `timestamp` column of `sys.fn_virtualfilestats` has expanded from an **int** to a **bigint** data type.

- Under database compatibility level 130, implicit conversions from **datetime** to **datetime2** data types show improved accuracy by accounting for the fractional milliseconds, resulting in different converted values. Use explicit casting to **datetime2** data type whenever a mixed comparison scenario between **datetime** and **datetime2** datatypes exists. For more information, see this [SQL Server and Azure SQL Database improvements in handling some data types and uncommon operations](/troubleshoot/sql/database-engine/general/sql-server-azure-sql-database-improvements).

- Under database compatibility level 130, operations that perform implicit conversions between certain numeric and **datetime** data types show improved accuracy and can result in different converted values. This includes usage of functions that require calculations such as `DATEDIFF` and `ROUND`. For more information, see this [SQL Server and Azure SQL Database improvements in handling some data types and uncommon operations](/troubleshoot/sql/database-engine/general/sql-server-azure-sql-database-improvements).

## Previous versions

For information about breaking changes in [!INCLUDE [ssSQL14](../includes/sssql14-md.md)], and in some earlier versions, see [Breaking Changes to Database Engine Features in SQL Server 2014](/previous-versions/sql/2014/database-engine/breaking-changes-to-database-engine-features-in-sql-server-2016).

### Archived documentation for old versions of SQL Server

[!INCLUDE [previous-versions-archive-documentation-sql-server](includes/previous-versions-archive-documentation-sql-server.md)]

## Related content

- [Deprecated Database Engine features in SQL Server 2016 (13.x)](deprecated-database-engine-features-in-sql-server-2016.md)
- [Discontinued Database Engine functionality in SQL Server](discontinued-database-engine-functionality-in-sql-server.md)
- [ALTER DATABASE (Transact-SQL) compatibility level](../t-sql/statements/alter-database-transact-sql-compatibility-level.md)
- [SQL Server and Azure SQL Database improvements in handling some data types and uncommon operations](/troubleshoot/sql/database-engine/general/sql-server-azure-sql-database-improvements)
