---
title: "sys.objects (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: 05/30/2017
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.objects_TSQL"
  - "objects"
  - "sys.objects"
  - "objects_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.objects catalog view"
  - "table-valued parameters, sys.objects catalog view"
  - "user-defined table types [SQL Server]"
  - "table types [SQL Server]"
ms.assetid: f8d6163a-2474-410c-a794-997639f31b3b
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.objects (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Contains a row for each user-defined, schema-scoped object that is created within a database, including natively compiled scalar user-defined function.  
  
 For more information, see [Scalar User-Defined Functions for In-Memory OLTP](../../relational-databases/in-memory-oltp/scalar-user-defined-functions-for-in-memory-oltp.md).  
  
> [!NOTE]  
>  sys.objects does not show DDL triggers, because they are not schema-scoped. All triggers, both DML and DDL, are found in [sys.triggers](../../relational-databases/system-catalog-views/sys-triggers-transact-sql.md). sys.triggers supports a mixture of name-scoping rules for the various kinds of triggers.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|name|**sysname**|Object name.|  
|object_id|**int**|Object identification number. Is unique within a database.|  
|principal_id|**int**|ID of the individual owner, if different from the schema owner. By default, schema-contained objects are owned by the schema owner. However, an alternate owner can be specified by using the ALTER AUTHORIZATION statement to change ownership.<br /><br /> Is NULL if there is no alternate individual owner.<br /><br /> Is NULL if the object type is one of the following:<br /><br /> C = CHECK constraint<br /><br /> D = DEFAULT (constraint or stand-alone)<br /><br /> F = FOREIGN KEY constraint<br /><br /> PK = PRIMARY KEY constraint<br /><br /> R = Rule (old-style, stand-alone)<br /><br /> TA = Assembly (CLR-integration) trigger<br /><br /> TR = SQL trigger<br /><br /> UQ = UNIQUE constraint<br /><br /> EC = Edge constraint |  
|schema_id|**int**|ID of the schema that the object is contained in.<br /><br /> Schema-scoped system objects are always contained in the sys or INFORMATION_SCHEMA schemas.|  
|parent_object_id|**int**|ID of the object to which this object belongs.<br /><br /> 0 = Not a child object.|  
|type|**char(2)**|Object type:<br /><br /> AF = Aggregate function (CLR)<br /><br /> C = CHECK constraint<br /><br /> D = DEFAULT (constraint or stand-alone)<br /><br /> F = FOREIGN KEY constraint<br /><br /> FN = SQL scalar function<br /><br /> FS = Assembly (CLR) scalar-function<br /><br /> FT = Assembly (CLR) table-valued function<br /><br /> IF = SQL inline table-valued function<br /><br /> IT = Internal table<br /><br /> P = SQL Stored Procedure<br /><br /> PC = Assembly (CLR) stored-procedure<br /><br /> PG = Plan guide<br /><br /> PK = PRIMARY KEY constraint<br /><br /> R = Rule (old-style, stand-alone)<br /><br /> RF = Replication-filter-procedure<br /><br /> S = System base table<br /><br /> SN = Synonym<br /><br /> SO = Sequence object<br /><br /> U = Table (user-defined)<br /><br /> V = View<br /><br /> EC = Edge constraint <br /><br /> <br /><br /> **Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> <br /><br /> SQ = Service queue<br /><br /> TA = Assembly (CLR) DML trigger<br /><br /> TF = SQL table-valued-function<br /><br /> TR = SQL DML trigger<br /><br /> TT = Table type<br /><br /> UQ = UNIQUE constraint<br /><br /> X = Extended stored procedure<br /><br /> <br /><br /> **Applies to**: [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)], [!INCLUDE[ssPDW](../../includes/sspdw-md.md)].<br /><br /> <br /><br /> ET = External Table|  
|type_desc|**nvarchar(60)**|Description of the object type:<br /><br /> AGGREGATE_FUNCTION<br /><br /> CHECK_CONSTRAINT<br /><br /> CLR_SCALAR_FUNCTION<br /><br /> CLR_STORED_PROCEDURE<br /><br /> CLR_TABLE_VALUED_FUNCTION<br /><br /> CLR_TRIGGER<br /><br /> DEFAULT_CONSTRAINT<br /><br /> EXTENDED_STORED_PROCEDURE<br /><br /> FOREIGN_KEY_CONSTRAINT<br /><br /> INTERNAL_TABLE<br /><br /> PLAN_GUIDE<br /><br /> PRIMARY_KEY_CONSTRAINT<br /><br /> REPLICATION_FILTER_PROCEDURE<br /><br /> RULE<br /><br /> SEQUENCE_OBJECT<br /><br /> <br /><br /> **Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> <br /><br /> SERVICE_QUEUE<br /><br /> SQL_INLINE_TABLE_VALUED_FUNCTION<br /><br /> SQL_SCALAR_FUNCTION<br /><br /> SQL_STORED_PROCEDURE<br /><br /> SQL_TABLE_VALUED_FUNCTION<br /><br /> SQL_TRIGGER<br /><br /> SYNONYM<br /><br /> SYSTEM_TABLE<br /><br /> TABLE_TYPE<br /><br /> UNIQUE_CONSTRAINT<br /><br /> USER_TABLE<br /><br /> VIEW|  
|create_date|**datetime**|Date the object was created.|  
|modify_date|**datetime**|Date the object was last modified by using an ALTER statement. If the object is a table or a view, modify_date also changes when a clustered index on the table or view is created or altered.|  
|is_ms_shipped|**bit**|Object is created by an internal [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] component.|  
|is_published|**bit**|Object is published.|  
|is_schema_published|**bit**|Only the schema of the object is published.|  
  
## Remarks  
 You can apply the [OBJECT_ID](../../t-sql/functions/object-id-transact-sql.md), [OBJECT_NAME](../../t-sql/functions/object-name-transact-sql.md), and [OBJECTPROPERTY](../../t-sql/functions/objectproperty-transact-sql.md)() built-in functions to the objects shown in sys.objects.  
  
 There is a version of this view with the same schema, called [sys.system_objects](../../relational-databases/system-catalog-views/sys-system-objects-transact-sql.md), that shows system objects. There is another view called [sys.all_objects](../../relational-databases/system-catalog-views/sys-all-objects-transact-sql.md) that shows both system and user objects. All three catalog views have the same structure.  
  
 In this version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], an extended index, such as an XML index or spatial index, is considered an internal table in sys.objects (type = IT and type_desc = INTERNAL_TABLE). For an extended index:  
  
-   name is the internal name of the index table.  
  
-   parent_object_id is the object_id of the base table.  
  
-   is_ms_shipped, is_published and is_schema_published columns are set to 0.  

**Related useful system views**  
Subsets of the objects can be viewed by using system views for a specific type of object, such as:  
- [sys.tables](sys-tables-transact-sql.md)  
- [sys.views](sys-views-transact-sql.md)  
- [sys.procedures](sys-procedures-transact-sql.md)  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## Examples  
  
### A. Returning all the objects that have been modified in the last N days  
 Before you run the following query, replace `<database_name>` and `<n_days>` with valid values.  
  
```sql  
USE <database_name>;  
GO  
SELECT name AS object_name   
  ,SCHEMA_NAME(schema_id) AS schema_name  
  ,type_desc  
  ,create_date  
  ,modify_date  
FROM sys.objects  
WHERE modify_date > GETDATE() - <n_days>  
ORDER BY modify_date;  
GO  
```  
  
### B. Returning the parameters for a specified stored procedure or function  
 Before you run the following query, replace `<database_name>` and `<schema_name.object_name>` with valid names.  
  
```sql  
USE <database_name>;  
GO  
SELECT SCHEMA_NAME(schema_id) AS schema_name  
    ,o.name AS object_name  
    ,o.type_desc  
    ,p.parameter_id  
    ,p.name AS parameter_name  
    ,TYPE_NAME(p.user_type_id) AS parameter_type  
    ,p.max_length  
    ,p.precision  
    ,p.scale  
    ,p.is_output  
FROM sys.objects AS o  
INNER JOIN sys.parameters AS p ON o.object_id = p.object_id  
WHERE o.object_id = OBJECT_ID('<schema_name.object_name>')  
ORDER BY schema_name, object_name, p.parameter_id;  
GO  
```  
  
### C. Returning all the user-defined functions in a database  
 Before you run the following query, replace `<database_name>` with a valid database name.  
  
```sql  
USE <database_name>;  
GO  
SELECT name AS function_name   
  ,SCHEMA_NAME(schema_id) AS schema_name  
  ,type_desc  
  ,create_date  
  ,modify_date  
FROM sys.objects  
WHERE type_desc LIKE '%FUNCTION%';  
GO  
```  
  
### D. Returning the owner of each object in a schema.  
 Before you run the following query, replace all occurrences of `<database_name>` and `<schema_name>` with valid names.  
  
```sql  
USE <database_name>;  
GO  
SELECT 'OBJECT' AS entity_type  
    ,USER_NAME(OBJECTPROPERTY(object_id, 'OwnerId')) AS owner_name  
    ,name   
FROM sys.objects WHERE SCHEMA_NAME(schema_id) = '<schema_name>'  
UNION   
SELECT 'TYPE' AS entity_type  
    ,USER_NAME(TYPEPROPERTY(SCHEMA_NAME(schema_id) + '.' + name, 'OwnerId')) AS owner_name  
    ,name   
FROM sys.types WHERE SCHEMA_NAME(schema_id) = '<schema_name>'   
UNION  
SELECT 'XML SCHEMA COLLECTION' AS entity_type   
    ,COALESCE(USER_NAME(xsc.principal_id),USER_NAME(s.principal_id)) AS owner_name  
    ,xsc.name   
FROM sys.xml_schema_collections AS xsc JOIN sys.schemas AS s  
    ON s.schema_id = xsc.schema_id  
WHERE s.name = '<schema_name>';  
GO  
```  
  
## See Also  
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [sys.all_objects &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-all-objects-transact-sql.md)   
 [sys.system_objects &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-system-objects-transact-sql.md)   
 [sys.triggers &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-triggers-transact-sql.md)   
 [Object Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/object-catalog-views-transact-sql.md)   
 [Querying the SQL Server System Catalog FAQ](../../relational-databases/system-catalog-views/querying-the-sql-server-system-catalog-faq.md)   
 [sys.internal_tables &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-internal-tables-transact-sql.md)  
  
  
