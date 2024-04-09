---
title: "sp_setdefaultdatatypemapping (Transact-SQL)"
description: Marks an existing data type mapping between SQL Server and a non-SQL Server database management system (DBMS) as the default.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 04/08/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_setdefaultdatatypemapping"
  - "sp_setdefaultdatatypemapping_TSQL"
helpviewer_keywords:
  - "sp_setdefaultdatatypemapping"
dev_langs:
  - "TSQL"
---
# sp_setdefaultdatatypemapping (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Marks an existing data type mapping between [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and a non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database management system (DBMS) as the default. This stored procedure is executed at the Distributor on any database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_setdefaultdatatypemapping
    [ [ @mapping_id = ] mapping_id ]
    [ , [ @source_dbms = ] N'source_dbms' ]
    [ , [ @source_version = ] 'source_version' ]
    [ , [ @source_type = ] N'source_type' ]
    [ , [ @source_length_min = ] source_length_min ]
    [ , [ @source_length_max = ] source_length_max ]
    [ , [ @source_precision_min = ] source_precision_min ]
    [ , [ @source_precision_max = ] source_precision_max ]
    [ , [ @source_scale_min = ] source_scale_min ]
    [ , [ @source_scale_max = ] source_scale_max ]
    [ , [ @source_nullable = ] source_nullable ]
    [ , [ @destination_dbms = ] N'destination_dbms' ]
    [ , [ @destination_version = ] 'destination_version' ]
    [ , [ @destination_type = ] N'destination_type' ]
    [ , [ @destination_length = ] destination_length ]
    [ , [ @destination_precision = ] destination_precision ]
    [ , [ @destination_scale = ] destination_scale ]
    [ , [ @destination_nullable = ] destination_nullable ]
[ ; ]
```

## Arguments

#### [ @mapping_id = ] *mapping_id*

Identifies an existing data type mapping. *@mapping_id* is **int**, with a default of `NULL`. If you specify *@mapping_id*, then the remaining parameters aren't required.

#### [ @source_dbms = ] N'*source_dbms*'

The name of the DBMS from which the data types are mapped. *@source_dbms* is **sysname**, and can be one of the following values.

| Value | Description |
| --- | --- |
| `MSSQLSERVER` | The source is a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database. |
| `ORACLE` | The source is an Oracle database. |
| `NULL` (default) | |

You must specify this parameter if *@mapping_id* is `NULL`.

#### [ @source_version = ] '*source_version*'

The version number of the source DBMS. *@source_version* is **varchar(10)**, with a default of `NULL`.

#### [ @source_type = ] N'*source_type*'

The data type in the source DBMS. *@source_type* is **sysname**, with a default of `NULL`. You must specify this parameter if *mapping_id* is `NULL`.

#### [ @source_length_min = ] *source_length_min*

The minimum length of the data type in the source DBMS. *@source_length_min* is **bigint**, with a default of `NULL`.

#### [ @source_length_max = ] *source_length_max*

The maximum length of the data type in the source DBMS. *@source_length_max* is **bigint**, with a default of `NULL`.

#### [ @source_precision_min = ] *source_precision_min*

The minimum precision of the data type in the source DBMS. *@source_precision_min* is **bigint**, with a default of `NULL`.

#### [ @source_precision_max = ] *source_precision_max*

The maximum precision of the data type in the source DBMS. *@source_precision_max* is **bigint**, with a default of `NULL`.

#### [ @source_scale_min = ] *source_scale_min*

The minimum scale of the data type in the source DBMS. *@source_scale_min* is **int**, with a default of `NULL`.

#### [ @source_scale_max = ] *source_scale_max*

The maximum scale of the data type in the source DBMS. *@source_scale_max* is **int**, with a default of `NULL`.

#### [ @source_nullable = ] *source_nullable*

Specifies whether the data type in the source DBMS supports a value of `NULL`. *@source_nullable* is **bit**, with a default of `NULL`. `1` means that `NULL` values are supported.

#### [ @destination_dbms = ] N'*destination_dbms*'

The name of the destination DBMS. *@destination_dbms* is **sysname**, and can be one of the following values.

| Value | Description |
| --- | --- |
| `MSSQLSERVER` | The destination is a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database. |
| `ORACLE` | The destination is an Oracle database. |
| `DB2` | The destination is an IBM DB2 database. |
| `SYBASE` | The destination is a Sybase database. |
| `NULL` (default) | |

#### [ @destination_version = ] '*destination_version*'

The product version of the destination DBMS. *@destination_version* is **varchar(10)**, with a default of `NULL`.

#### [ @destination_type = ] N'*destination_type*'

The data type listed in the destination DBMS. *@destination_type* is **sysname**, with a default of `NULL`.

#### [ @destination_length = ] *destination_length*

The length of the data type in the destination DBMS. *@destination_length* is **bigint**, with a default of `NULL`.

#### [ @destination_precision = ] *destination_precision*

The precision of the data type in the destination DBMS. *@destination_precision* is **bigint**, with a default of `NULL`.

#### [ @destination_scale = ] *destination_scale*

The scale of the data type in the destination DBMS. *@destination_scale* is **int**, with a default of `NULL`.

#### [ @destination_nullable = ] *destination_nullable*

Specifies whether the data type in the destination DBMS supports a value of `NULL`. *@destination_nullable* is **bit**, with a default of `NULL`. `1` means that `NULL` values are supported.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_setdefaultdatatypemapping` is used in all types of replication between [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and a non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] DBMS.

The default data type mappings apply to all replication topologies that include the specified DBMS.

## Permissions

Only members of the **sysadmin** fixed server role can execute `sp_setdefaultdatatypemapping`.

## Related content

- [Specify Data Type Mappings for an Oracle Publisher](../replication/publish/specify-data-type-mappings-for-an-oracle-publisher.md)
- [sp_getdefaultdatatypemapping (Transact-SQL)](sp-getdefaultdatatypemapping-transact-sql.md)
- [sp_helpdatatypemap (Transact-SQL)](sp-helpdatatypemap-transact-sql.md)
