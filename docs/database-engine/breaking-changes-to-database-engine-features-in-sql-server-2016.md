---
title: "Database Engine: Breaking changes"
titleSuffix: "SQL Server 2016"
description: Learn about Database Engine changes in SQL Server 2016 (13.x) and earlier that might break previous-version functionality when you upgrade.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 07/25/2022
ms.service: sql
ms.subservice: release-landing
ms.topic: conceptual
ms.custom: seo-lt-2019
helpviewer_keywords:
  - "Database Engine [SQL Server], what's new"
  - "breaking changes [SQL Server]"
---
# Breaking changes to Database Engine features in SQL Server 2016

[!INCLUDE [SQL Server 2016](../includes/applies-to-version/sqlserver2016.md)]

This article describes breaking changes in the [!INCLUDE[sssql15-md](../includes/sssql16-md.md)] [!INCLUDE[ssDE](../includes/ssde-md.md)] and earlier versions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. These changes might break applications, scripts, or functionalities that are based on earlier versions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. You might encounter these issues when you upgrade.

## <a id="SQL15"></a> Breaking Changes in [!INCLUDE[sssql15-md](../includes/sssql16-md.md)]

- The *sample_ms* column of `sys.dm_io_virtual_file_stats` has expanded from an **int** to a **bigint** data type.

- The *TimeStamp* column of `sys.fn_virtualfilestats` has expanded from an **int** to a **bigint** data type.

- Under database compatibility level 130, implicit conversions from **datetime** to **datetime2** data types show improved accuracy by accounting for the fractional milliseconds, resulting in different converted values. Use explicit casting to datetime2 datatype whenever a mixed comparison scenario between datetime and datetime2 datatypes exists. For more information, see this [Microsoft Support Article](https://support.microsoft.com/help/4010261).

- Under database compatibility level 130, operations that perform implicit conversions between certain numeric and datetime data types show improved accuracy and can result in different converted values. This includes usage of functions that require calculations such as, for example, `DATEDIFF` and `ROUND`. For more information, see this [Microsoft Support Article](https://support.microsoft.com/help/4010261).

## <a id="previous-versions"></a> Previous Versions

For information about breaking changes in [!INCLUDE[ssSQL14](../includes/sssql14-md.md)], and in some earlier versions, see [Breaking Changes to Database Engine Features in SQL Server 2014](/previous-versions/sql/2014/database-engine/breaking-changes-to-database-engine-features-in-sql-server-2016).

#### Archived Documentation for Very Old Versions of SQL Server

[!INCLUDE[Archived documentation for very old versions of SQL Server](../includes/paragraph-content/previous-versions-archive-documentation-sql-server.md)]

## See also

- [Deprecated Database Engine Features in SQL Server 2016](../database-engine/deprecated-database-engine-features-in-sql-server-2016.md)
- [Discontinued Database Engine Functionality in SQL Server 2016](./discontinued-database-engine-functionality-in-sql-server.md)
- [SQL Server Database Engine Backward Compatibility](./discontinued-database-engine-functionality-in-sql-server.md)
- [ALTER DATABASE Compatibility Level &#40;Transact-SQL&#41;](../t-sql/statements/alter-database-transact-sql-compatibility-level.md)
- [SQL Server 2016 or SQL Server 2017 on Windows improvements in handling some data types and uncommon operations](https://support.microsoft.com/help/4010261)
