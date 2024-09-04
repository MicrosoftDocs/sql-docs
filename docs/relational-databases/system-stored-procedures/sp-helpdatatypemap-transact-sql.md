---
title: "sp_helpdatatypemap (Transact-SQL)"
description: sp_helpdatatypemap returns information on the defined data type mappings between SQL Server and non-SQL Server database management systems (DBMS).
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/14/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_helpdatatypemap"
  - "sp_helpdatatypemap_TSQL"
helpviewer_keywords:
  - "sp_helpdatatypemap"
dev_langs:
  - "TSQL"
---
# sp_helpdatatypemap (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Returns information on the defined data type mappings between [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database management systems (DBMS). This stored procedure is executed at the Distributor on any database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_helpdatatypemap
    [ @source_dbms = ] N'source_dbms'
    [ , [ @source_version = ] 'source_version' ]
    [ , [ @source_type = ] N'source_type' ]
    [ , [ @destination_dbms = ] N'destination_dbms' ]
    [ , [ @destination_version = ] 'destination_version' ]
    [ , [ @destination_type = ] N'destination_type' ]
    [ , [ @defaults_only = ] defaults_only ]
[ ; ]
```

## Arguments

#### [ @source_dbms = ] N'*source_dbms*'

The name of the DBMS from which the data types are mapped. *@source_dbms* is **sysname**, and can be one of the following values.

| Value | Description |
| --- | --- |
| `MSSQLSERVER` | The source is a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database. |
| `ORACLE` | The source is an Oracle database. |

#### [ @source_version = ] '*source_version*'

The product version of the source DBMS. *@source_version* is **varchar(10)**, with a default of `%`. If not specified, the data type mappings for all versions of the source DBMS are returned. Enables filtering the result set by the source version of the DBMS.

#### [ @source_type = ] N'*source_type*'

The data type listed in the source DBMS. *@source_type* is **sysname**, with a default of `%`. If not specified, mappings for all data types in the source DBMS are returned. Enables filtering the result set by data type in the source DBMS.

#### [ @destination_dbms = ] N'*destination_dbms*'

The name of the destination DBMS. *@destination_dbms* is **sysname**, with a default of `%`, and can be one of the following values.

| Value | Description |
| --- | --- |
| `MSSQLSERVER` | The destination is a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database. |
| `ORACLE` | The destination is an Oracle database. |
| `DB2` | The destination is an IBM Db2 database. |
| `SYBASE` | The destination is a Sybase database. |

#### [ @destination_version = ] '*destination_version*'

The product version of the destination DBMS. *@destination_version* is **varchar(10)**, with a default of `%`. If not specified, mappings for all versions of the destination DBMS are returned. Enables filtering the result set by the destination version of the DBMS.

#### [ @destination_type = ] N'*destination_type*'

The data type listed in the destination DBMS. *@destination_type* is **sysname**, with a default of `%`. If not specified, mappings for all data types in the destination DBMS are returned. Enables filtering the result set by data type in the destination DBMS.

#### [ @defaults_only = ] *defaults_only*

If only the default data type mappings are returned. *@defaults_only* is **bit**, with a default of `0`.

- `1` means that only the default data type mappings are returned.
- `0` means that the default and any user-defined data type mappings are returned.

## Result set

| Column name | Description |
| --- | --- |
| `mapping_id` | Identifies a data type mapping. |
| `source_dbms` | The name and version number of the source DBMS. |
| `source_type` | The data type in the source DBMS. |
| `destination_dbms` | The name of the destination DBMS. |
| `destination_type` | The data type in the destination DBMS. |
| `is_default` | Specifies whether the mapping is a default or an alternative mapping. A value of `0` indicates that this mapping is user-defined. |

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_helpdatatypemap` defines data type mappings both from non-SQL Server Publishers and from [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publishers to non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Subscribers.

When the specified combination of source and destination DBMS isn't supported, `sp_helpdatatypemap` returns an empty result set.

## Permissions

Only members of the **sysadmin** fixed server role at the Distributor or members of the **db_owner** fixed database role on the distribution database can execute `sp_helpdatatypemap`.

## Related content

- [sp_getdefaultdatatypemapping (Transact-SQL)](sp-getdefaultdatatypemapping-transact-sql.md)
- [sp_setdefaultdatatypemapping (Transact-SQL)](sp-setdefaultdatatypemapping-transact-sql.md)
