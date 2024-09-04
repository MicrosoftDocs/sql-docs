---
title: "sp_getdefaultdatatypemapping (Transact-SQL)"
description: "Returns information on the default mapping for data types between SQL Server and a non-SQL Server DBMS."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_getdefaultdatatypemapping_TSQL"
  - "sp_getdefaultdatatypemapping"
helpviewer_keywords:
  - "sp_getdefaultdatatypemapping"
dev_langs:
  - "TSQL"
---
# sp_getdefaultdatatypemapping (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns information on the default mapping for the specified data type between [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and a non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database management system (DBMS). This stored procedure is executed at the Distributor on any database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_getdefaultdatatypemapping [ @source_dbms = ] 'source_dbms'
    [ , [ @source_version = ] 'source_version' ]
      , [ @source_type = ] 'source_type'
    [ , [ @source_length = ] source_length ]
    [ , [ @source_precision = ] source_precision ]
    [ , [ @source_scale = ] source_scale ]
    [ , [ @source_nullable = ] source_nullable ]
      , [ @destination_dbms = ] 'destination_dbms'
    [ , [ @destination_version = ] 'destination_version' ]
    [ , [ @destination_type = ] 'destination_type' OUTPUT ]
    [ , [ @destination_length = ] destination_length OUTPUT ]
    [ , [ @destination_precision = ] destination_precision OUTPUT ]
    [ , [ @destination_scale = ] destination_scale OUTPUT ]
    [ , [ @destination_nullable = ] source_nullable OUTPUT ]
    [ , [ @dataloss = ] dataloss OUTPUT ]
[ ; ]
```

## Arguments

#### [ @source_dbms = ] '*source_dbms*'

The name of the DBMS from which the data types are mapped. *@source_dbms* is **sysname**, and can be one of the following values:

| Value | Description |
| --- | --- |
| `MSSQLSERVER` | The source is a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database. |
| `ORACLE` | The source is an Oracle database. |

You must specify this parameter.

#### [ @source_version = ] '*source_version*'

The version number of the source DBMS. *@source_version* is **varchar(10)**, with a default value of `NULL`.

#### [ @source_type = ] '*source_type*'

The data type in the source DBMS. *@source_type* is **sysname**, with no default.

#### [ @source_length = ] *source_length*

The length of the data type in the source DBMS. *@source_length* is **bigint**, with a default value of `NULL`.

#### [ @source_precision = ] *source_precision*

The precision of the data type in the source DBMS. *@source_precision* is **bigint**, with a default value of `NULL`.

#### [ @source_scale = ] *source_scale*

The scale of the data type in the source DBMS. *@source_scale* is **int**, with a default value of `NULL`.

#### [ @source_nullable = ] *source_nullable*

Specifies if the data type in the source DBMS supports a value of `NULL`. *@source_nullable* is **bit**, with a default value of `1`, which means that `NULL` values are supported.

#### [ @destination_dbms = ] '*destination_dbms*'

The name of the destination DBMS. *@destination_dbms* is **sysname**, and can be one of the following values:

| Value | Description |
| --- | --- |
| `MSSQLSERVER` | The destination is a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database. |
| `ORACLE` | The destination is an Oracle database. |
| `DB2` | The destination is an IBM Db2 database. |
| `SYBASE` | The destination is a Sybase database. |

You must specify this parameter.

#### [ @destination_version = ] '*destination_version*'

The product version of the destination DBMS. *@destination_version* is **varchar(10)**, with a default value of `NULL`.

#### [ @destination_type = ] '*destination_type*' OUTPUT

The data type listed in the destination DBMS. *@destination_type* is **sysname**, with a default value of `NULL`.

#### [ @destination_length = ] *destination_length* OUTPUT

The length of the data type in the destination DBMS. *@destination_length* is **bigint**, with a default value of `NULL`.

#### [ @destination_precision = ] *destination_precision* OUTPUT

The precision of the data type in the destination DBMS. *@destination_precision* is **bigint**, with a default value of `NULL`.

#### [ @destination_scale = ] *destination_scale* OUTPUT

The scale of the data type in the destination DBMS. *@destination_scale* is **int**, with a default value of `NULL`.

#### [ @destination_nullable = ] *destination_nullable* OUTPUT

Specifies if the data type in the destination DBMS supports a value of `NULL`. *@destination_nullable* is **bit**, with a default value of `NULL`. `1` means that `NULL` values are supported.

#### [ @dataloss = ] *dataloss* OUTPUT

Specifies if the mapping has the potential for data loss. *@dataloss* is **bit**, with a default value of `NULL`. `1` means that there's a potential for data loss.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_getdefaultdatatypemapping` is used in all types of replication between [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and a non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] DBMS.

`sp_getdefaultdatatypemapping` returns the default destination data type that is the closest match to the specified source data type.

## Permissions

Only members of the **sysadmin** fixed server role can execute `sp_getdefaultdatatypemapping`.

## Related content

- [sp_helpdatatypemap (Transact-SQL)](sp-helpdatatypemap-transact-sql.md)
- [sp_setdefaultdatatypemapping (Transact-SQL)](sp-setdefaultdatatypemapping-transact-sql.md)
- [Data Type Mapping for Oracle Publishers](../replication/non-sql/data-type-mapping-for-oracle-publishers.md)
- [IBM Db2 Subscribers](../replication/non-sql/ibm-db2-subscribers.md)
- [Oracle Subscribers](../replication/non-sql/oracle-subscribers.md)
