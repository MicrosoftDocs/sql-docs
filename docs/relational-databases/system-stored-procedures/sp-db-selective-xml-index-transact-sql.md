---
title: "sp_db_selective_xml_index (Transact-SQL)"
description: sp_db_selective_xml_index enables and disables selective XML index (SXI) functionality on a SQL Server database.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/04/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_db_selective_xml_index_TSQL"
  - "sp_db_selective_xml_index"
helpviewer_keywords:
  - "sp_db_selective_xml_index procedure"
dev_langs:
  - "TSQL"
---
# sp_db_selective_xml_index (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Enables and disables selective XML index (SXI) functionality on a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database. If called without any parameters, the stored procedure returns `1` if SXI is enabled on a particular database.

> [!NOTE]  
> In [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)] and later versions, the SXI functionality can't be disabled. [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)]

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_db_selective_xml_index
    [ [ @dbname = ] N'dbname' ]
    [ , [ @selective_xml_index = ] 'selective_xml_index' ]
[ ; ]
```

## Arguments

#### [ @dbname = ] N'*dbname*'

The name of the database on which to to enable or disable selective XML index. *@dbname* is **sysname**, with a default of `NULL`.

If *@dbname* is `NULL`, the current database is assumed.

#### [ @selective_xml_index = ] '*selective_xml_index*'

Determines whether to enable or disable the index. *@selective_xml_index* is **varchar(6)**, with a default of `NULL`, and can be one of the following values: `ON`, `OFF`, `TRUE`, or `FALSE`. Any other value raises an error.

## Return code values

`1` if the SXI is enabled on a particular database, `0` if disabled.

## Examples

### A. Enable selective XML index functionality

The following example enables SXI on the current database.

```sql
EXEC sys.sp_db_selective_xml_index
    @dbname = NULL
  , @selective_xml_index = N'on';
GO
```

The following example enables SXI on the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database.

```sql
EXECUTE sys.sp_db_selective_xml_index
    @dbname = N'AdventureWorks2022'
  , @selective_xml_index = N'true';
GO
```

### B. Disable selective XML index functionality

The following example disables SXI on the current database.

```sql
EXECUTE sys.sp_db_selective_xml_index
    @dbname = NULL
  , @selective_xml_index = N'off';
GO
```

The following example disables SXI on the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database.

```sql
EXECUTE sys.sp_db_selective_xml_index
    @dbname = N'AdventureWorks2022'
  , @selective_xml_index = N'false';
GO
```

### C. Detect if selective XML index is enabled

The following example detects if SXI is enabled, and returns `1` if SXI is enabled.

```sql
EXECUTE sys.sp_db_selective_xml_index;
GO
```

## Related content

- [Selective XML indexes (SXI)](../xml/selective-xml-indexes-sxi.md)
