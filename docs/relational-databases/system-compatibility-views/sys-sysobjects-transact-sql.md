---
title: "sys.sysobjects (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.sysobjects_TSQL"
  - "sysobjects"
  - "sysobjects_TSQL"
  - "sys.sysobjects"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.sysobjects compatibility view"
  - "sysobjects system table"
ms.assetid: 44fdc387-67b0-4139-8bf5-ed26cf640cd1
author: "rothja"
ms.author: "jroth"
manager: craigg
monikerRange: ">=aps-pdw-2016||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.sysobjects (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-asdw-pdw-md](../../includes/tsql-appliesto-ss2008-xxxx-asdw-pdw-md.md)]

  Contains one row for each object that is created within a database, such as a constraint, default, log, rule, and stored procedure.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssnoteCompView](../../includes/ssnotecompview-md.md)]  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|name|**sysname**|Object name|  
|id|**int**|Object identification number|  
|xtype|**char(2)**|Object type. Can be one of the following object types:<br /><br /> AF = Aggregate function (CLR)<br /><br /> C = CHECK constraint<br /><br /> D = Default or DEFAULT constraint<br /><br /> F = FOREIGN KEY constraint<br /><br /> L = Log<br /><br /> FN = Scalar function<br /><br /> FS = Assembly (CLR) scalar-function<br /><br /> FT = Assembly (CLR) table-valued function<br /><br /> IF = In-lined table-function<br /><br /> IT = Internal table<br /><br /> P = Stored procedure<br /><br /> PC = Assembly (CLR) stored-procedure<br /><br /> PK = PRIMARY KEY constraint (type is K)<br /><br /> RF = Replication filter stored procedure<br /><br /> S = System table<br /><br /> SN = Synonym<br /><br /> SQ = Service queue<br /><br /> TA = Assembly (CLR) DML trigger<br /><br /> TF = Table function<br /><br /> TR = SQL DML Trigger<br /><br /> TT = Table type<br /><br /> U = User table<br /><br /> UQ = UNIQUE constraint (type is K)<br /><br /> V = View<br /><br /> X = Extended stored procedure|  
|uid|**smallint**|Schema ID of the owner of the object. For databases upgraded from an earlier version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the schema ID is equal to the user ID of the owner. Overflows or returns NULL if the number of users and roles exceeds 32,767.<br /><br /> **\*\* Important \*\*** If you use any of the following [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] DDL statements, you must use the [sys.objects](../../relational-databases/system-catalog-views/sys-objects-transact-sql.md) catalog view instead of sys.sysobjects.<br /><br /> CREATE &#124; ALTER &#124; DROP USER<br /><br /> CREATE &#124; ALTER &#124; DROP ROLE<br /><br /> CREATE &#124; ALTER &#124; DROP APPLICATION ROLE<br /><br /> CREATE SCHEMA<br /><br /> ALTER AUTHORIZATION ON OBJECT|  
|info|**smallint**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|status|**int**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|base_schema_ver|**int**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|replinfo|**int**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|parent_obj|**int**|Object identification number of the parent object. For example, the table ID if it is a trigger or constraint.|  
|crdate|**datetime**|Date the object was created.|  
|ftcatid|**smallint**|Identifier of the full-text catalog for all user tables registered for full-text indexing, and 0 for all user tables that are not registered.|  
|schema_ver|**int**|Version number that is incremented every time the schema for a table changes. Always returns 0.|  
|stats_schema_ver|**int**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|type|**char(2)**|Object type. Can be one of the following values:<br /><br /> AF = Aggregate function (CLR)<br /><br /> C = CHECK constraint<br /><br /> D = Default or DEFAULT constraint<br /><br /> F = FOREIGN KEY constraint<br /><br /> FN = Scalar function<br /><br /> FS = Assembly (CLR) scalar-function<br /><br /> FT = Assembly (CLR) table-valued functionIF = In-lined table-function<br /><br /> IT - Internal table<br /><br /> K = PRIMARY KEY or UNIQUE constraint<br /><br /> L = Log<br /><br /> P = Stored procedure<br /><br /> PC = Assembly (CLR) stored-procedure<br /><br /> R = Rule<br /><br /> RF = Replication filter stored procedure<br /><br /> S = System table<br /><br /> SN = Synonym<br /><br /> SQ = Service queue<br /><br /> TA = Assembly (CLR) DML trigger<br /><br /> TF = Table function<br /><br /> TR = SQL DML Trigger<br /><br /> TT = Table type<br /><br /> U = User table<br /><br /> V = View<br /><br /> X = Extended stored procedure|  
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
  
## See Also  
 [Mapping System Tables to System Views &#40;Transact-SQL&#41;](../../relational-databases/system-tables/mapping-system-tables-to-system-views-transact-sql.md)   
 [Compatibility Views &#40;Transact-SQL&#41;](~/relational-databases/system-compatibility-views/system-compatibility-views-transact-sql.md)  
  
  
