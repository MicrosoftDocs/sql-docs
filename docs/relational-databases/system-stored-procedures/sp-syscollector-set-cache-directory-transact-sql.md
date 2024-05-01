---
title: "sp_syscollector_set_cache_directory (Transact-SQL)"
description: Specifies the directory where collected data is stored before it is uploaded to the management data warehouse.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/04/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_syscollector_set_cache_directory_TSQL"
  - "sp_syscollector_set_cache_directory"
helpviewer_keywords:
  - "data collector [SQL Server], stored procedures"
  - "sp_syscollector_set_cache_directory stored procedure"
dev_langs:
  - "TSQL"
---
# sp_syscollector_set_cache_directory (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Specifies the directory where collected data is stored before it is uploaded to the management data warehouse.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_syscollector_set_cache_directory [ [ @cache_directory = ] N'cache_directory' ]
[ ; ]
```

The directory in the file system where collected data is stored temporarily. *@cache_directory* is **nvarchar(255)**, with a default of an empty string. If no value is specified, the default temporary [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] directory is used.

## Return code values

`0` (success) or `1` (failure).

## Remarks

You must disable the data collector before changing the cache directory configuration. This stored procedure fails if the data collector is enabled. For more information, see [Enable or Disable Data Collection](../data-collection/enable-or-disable-data-collection.md), and [Manage Data Collection](../data-collection/manage-data-collection.md).

The specified directory doesn't need to exist at the time the `sp_syscollector_set_cache_directory` is executed; however, data can't be successfully cached and uploaded until the directory is created. We recommend creating the directory before executing this stored procedure.

## Permissions

Requires membership in the **dc_admin** (with EXECUTE permission) fixed database role to execute this procedure.

## Examples

The following example disables the data collector, sets the cache directory for the data collector to `D:\tempdata`, and then enables the data collector.

```sql
USE msdb;
GO
EXECUTE dbo.sp_syscollector_disable_collector;
GO
EXEC dbo.sp_syscollector_set_cache_directory
    @cache_directory = N'D:\tempdata';
GO
EXECUTE dbo.sp_syscollector_enable_collector;
GO
```

## Related content

- [Data Collector stored procedures (Transact-SQL)](data-collector-stored-procedures-transact-sql.md)
- [sp_syscollector_set_cache_window (Transact-SQL)](sp-syscollector-set-cache-window-transact-sql.md)
