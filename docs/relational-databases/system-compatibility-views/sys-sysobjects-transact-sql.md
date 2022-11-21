---
title: "sys.sysobjects (Transact-SQL)"
description: "Contains one row for each object that is created within a database, such as a constraint, default, log, rule, and stored procedure."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: randolphwest
ms.date: 09/10/2022
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.sysobjects_TSQL"
  - "sysobjects"
  - "sysobjects_TSQL"
  - "sys.sysobjects"
helpviewer_keywords:
  - "sys.sysobjects compatibility view"
  - "sysobjects system table"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.sysobjects (Transact-SQL)

[!INCLUDE [sql-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdbmi-asa-pdw.md)]

Contains one row for each object that is created within a database, such as a constraint, default, log, rule, and stored procedure.

> [!IMPORTANT]  
> [!INCLUDE[ssnoteCompView](../../includes/ssnotecompview-md.md)]

|Column name|Data type|Description|
|-----------------|---------------|-----------------|
|name|**sysname**|Object name|
|id|**int**|Object identification number|
|xtype|**char(2)**|Object type. Can be one of the following object types:<br /><br />AF = Aggregate function (CLR)<br />C = CHECK constraint<br />D = Default or DEFAULT constraint<br />F = FOREIGN KEY constraint<br />L = Log<br />FN = Scalar function<br />FS = Assembly (CLR) scalar-function<br />FT = Assembly (CLR) table-valued function<br />IF = In-lined table-function<br />IT = Internal table<br />P = Stored procedure<br />PC = Assembly (CLR) stored-procedure<br />PK = PRIMARY KEY constraint (type is K)<br />RF = Replication filter stored procedure<br />S = System table<br />SN = Synonym<br />SO = Sequence<br />SQ = Service queue<br />TA = Assembly (CLR) DML trigger<br />TF = Table function<br />TR = SQL DML Trigger<br />TT = Table type<br />U = User table<br />UQ = UNIQUE constraint (type is K)<br />V = View<br />X = Extended stored procedure|
|uid|**smallint**|Schema ID of the owner of the object. For databases upgraded from an earlier version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the schema ID is equal to the user ID of the owner. Overflows or returns NULL if the number of users and roles exceeds 32,767.<br /><br />**Important:** If you use any of the following [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] DDL statements, you must use the [sys.objects](../../relational-databases/system-catalog-views/sys-objects-transact-sql.md) catalog view instead of `sys.sysobjects`.<br /><br />CREATE \| ALTER \| DROP USER<br /><br />CREATE \| ALTER \| DROP ROLE<br /><br />CREATE \| ALTER \| DROP APPLICATION ROLE<br /><br />CREATE SCHEMA<br /><br />ALTER AUTHORIZATION ON OBJECT|
|info|**smallint**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|
|status|**int**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|
|base_schema_ver|**int**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|
|replinfo|**int**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|
|parent_obj|**int**|Object identification number of the parent object. For example, the table ID if it is a trigger or constraint.|
|crdate|**datetime**|Date the object was created.|
|ftcatid|**smallint**|Identifier of the full-text catalog for all user tables registered for full-text indexing, and 0 for all user tables that are not registered.|
|schema_ver|**int**|Version number that is incremented every time the schema for a table changes. Always returns 0.|
|stats_schema_ver|**int**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|
|type|**char(2)**|Object type. Can be one of the following values:<br /><br />AF = Aggregate function (CLR)<br />C = CHECK constraint<br />D = Default or DEFAULT constraint<br />F = FOREIGN KEY constraint<br />FN = Scalar function<br />FS = Assembly (CLR) scalar-function<br />FT = Assembly (CLR) table-valued functionIF = In-lined table-function<br />IT - Internal table<br />K = PRIMARY KEY or UNIQUE constraint<br />L = Log<br />P = Stored procedure<br />PC = Assembly (CLR) stored-procedure<br />R = Rule<br />RF = Replication filter stored procedure<br />S = System table<br />SN = Synonym<br />SQ = Service queue<br />TA = Assembly (CLR) DML trigger<br />TF = Table function<br />TR = SQL DML Trigger<br />TT = Table type<br />U = User table<br />V = View<br />X = Extended stored procedure|
|userstat|**smallint**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|
|sysstat|**smallint**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|
|indexdel|**smallint**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|
|refdate|**datetime**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|
|version|**int**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|
|deltrig|**int**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|
|instrig|**int**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|
|updtrig|**int**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|
|seltrig|**int**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|
|category|**int**|Used for publication, constraints, and identity.|
|cache|**smallint**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|

## See also

- [Mapping System Tables to System Views (Transact-SQL)](../../relational-databases/system-tables/mapping-system-tables-to-system-views-transact-sql.md)
- [Compatibility Views (Transact-SQL)](~/relational-databases/system-compatibility-views/system-compatibility-views-transact-sql.md)
