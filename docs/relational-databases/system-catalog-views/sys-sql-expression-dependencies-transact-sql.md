---
title: "sys.sql_expression_dependencies (Transact-SQL)"
description: sys.sql_expression_dependencies (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.sql_expression_dependencies"
  - "sql_expression_dependencies_TSQL"
  - "sql_expression_dependencies"
  - "sys.sql_expression_dependencies_TSQL"
helpviewer_keywords:
  - "sys.sql_expression_dependencies catalog view"
dev_langs:
  - "TSQL"
ms.assetid: 78a218e4-bf99-4a6a-acbf-ff82425a5946
monikerRange: ">=aps-pdw-2016||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.sql_expression_dependencies (Transact-SQL)
[!INCLUDE [sql-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdbmi-asa-pdw.md)]

  Contains one row for each by-name dependency on a user-defined entity in the current database. This includes dependences between natively compiled, scalar user-defined functions and other [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] modules. A dependency between two entities is created when one entity, called the *referenced entity*, appears by name in a persisted SQL expression of another entity, called the *referencing entity*. For example, when a table is referenced in the definition of a view, the view, as the referencing entity, depends on the table, the referenced entity. If the table is dropped, the view is unusable.  
  
 For more information, see [Scalar User-Defined Functions for In-Memory OLTP](../../relational-databases/in-memory-oltp/scalar-user-defined-functions-for-in-memory-oltp.md).  
  
 You can use this catalog view to report dependency information for the following entities:  
  
-   Schema-bound entities.  
  
-   Non-schema-bound entities.  
  
-   Cross-database and cross-server entities. Entity names are reported; however, entity IDs are not resolved.  
  
-   Column-level dependencies on schema-bound entities. Column-level dependencies for non-schema-bound objects can be returned by using [sys.dm_sql_referenced_entities](../../relational-databases/system-dynamic-management-views/sys-dm-sql-referenced-entities-transact-sql.md).  
  
-   Server-level DDL triggers when in the context of the master database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|referencing_id|**int**|ID of the referencing entity. Is not nullable.|  
|referencing_minor_id|**int**|Column ID when the referencing entity is a column; otherwise 0. Is not nullable.|  
|referencing_class|**tinyint**|Class of the referencing entity.<br /><br /> 1 = Object or column<br /><br /> 12 = Database DDL trigger<br /><br /> 13 = Server DDL trigger<br /><br /> Is not nullable.|  
|referencing_class_desc|**nvarchar(60)**|Description of the class of referencing entity.<br /><br /> OBJECT_OR_COLUMN<br /><br /> DATABASE_DDL_TRIGGER<br /><br /> SERVER_DDL_TRIGGER<br /><br /> Is not nullable.|  
|is_schema_bound_reference|**bit**|1 = Referenced entity is schema-bound.<br /><br /> 0 = Referenced entity is non-schema-bound.<br /><br /> Is not nullable.|  
|referenced_class|**tinyint**|Class of the referenced entity.<br /><br /> 1 = Object or column<br /><br /> 6 = Type<br /><br /> 10 = XML schema collection<br /><br /> 21 = Partition function<br /><br /> Is not nullable.|  
|referenced_class_desc|**nvarchar(60)**|Description of class of referenced entity.<br /><br /> OBJECT_OR_COLUMN<br /><br /> TYPE<br /><br /> XML_SCHEMA_COLLECTION<br /><br /> PARTITION_FUNCTION<br /><br /> Is not nullable.|  
|referenced_server_name|**sysname**|Name of the server of the referenced entity.<br /><br /> This column is populated for cross-server dependencies that are made by specifying a valid four-part name. For information about multipart names, see [Transact-SQL Syntax Conventions &#40;Transact-SQL&#41;](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md).<br /><br /> NULL for non-schema-bound entities for which the entity was referenced without specifying a four-part name.<br /><br /> NULL for schema-bound entities because they must be in the same database and therefore can only be defined using a two-part (*schema.object*) name.|  
|referenced_database_name|**sysname**|Name of the database of the referenced entity.<br /><br /> This column is populated for cross-database or cross-server references that are made by specifying a valid three-part or four-part name.<br /><br /> NULL for non-schema-bound references when specified using a one-part or two-part name.<br /><br /> NULL for schema-bound entities because they must be in the same database and therefore can only be defined using a two-part (*schema.object*) name.|  
|referenced_schema_name|**sysname**|Schema in which the referenced entity belongs.<br /><br /> NULL for non-schema-bound references in which the entity was referenced without specifying the schema name.<br /><br /> Never NULL for schema-bound references because schema-bound entities must be defined and referenced by using a two-part name.|  
|referenced_entity_name|**sysname**|Name of the referenced entity. Is not nullable.|  
|referenced_id|**int**|ID of the referenced entity. The value of this column is never NULL for schema-bound references. The value of this column is always NULL for cross-server and cross-database references.<br /><br /> NULL for references within the database if the ID cannot be determined. For non-schema-bound references, the ID cannot be resolved in the following cases:<br /><br /> The referenced entity does not exist in the database.<br /><br /> The schema of the referenced entity depends on the schema of the caller and is resolved at run time. In this case, is_caller_dependent is set to 1.|  
|referenced_minor_id|**int**|ID of the referenced column when the referencing entity is a column; otherwise 0. Is not nullable.<br /><br /> A referenced entity is a column when a column is identified by name in the referencing entity, or when the parent entity is used in a SELECT * statement.|  
|is_caller_dependent|**bit**|Indicates that schema binding for the referenced entity occurs at runtime; therefore, resolution of the entity ID depends on the schema of the caller. This occurs when the referenced entity is a stored procedure, extended stored procedure, or a non-schema-bound user-defined function called in an EXECUTE statement.<br /><br /> 1 = The referenced entity is caller dependent and is resolved at runtime. In this case, referenced_id is NULL.<br /><br /> 0 = The referenced entity ID is not caller dependent.<br /><br /> Always 0 for schema-bound references and for cross-database and cross-server references that explicitly specify a schema name. For example, a reference to an entity in the format `EXEC MyDatabase.MySchema.MyProc` is not caller dependent. However, a reference in the format `EXEC MyDatabase..MyProc` is caller dependent.|  
|is_ambiguous|**bit**|Indicates the reference is ambiguous and can resolve at run time to a user-defined function, a user-defined type (UDT), or an xquery reference to a column of type **xml**.<br /><br /> For example, assume that the statement `SELECT Sales.GetOrder() FROM Sales.MySales` is defined in a stored procedure. Until the stored procedure is executed, it is not known whether `Sales.GetOrder()` is a user-defined function in the `Sales` schema or column named `Sales` of type UDT with a method named `GetOrder()`.<br /><br /> 1 = Reference is ambiguous.<br /><br /> 0 = Reference is unambiguous or the entity can be successfully bound when the view is called.<br /><br /> Always 0 for schema bound references.|  
  
## Remarks  
 The following table lists the types of entities for which dependency information is created and maintained. Dependency information is not created or maintained for rules, defaults, temporary tables, temporary stored procedures, or system objects.  

> [!NOTE]
> Azure Synapse Analytics and Parallel Data Warehouse support tables, views, filtered statistics, and Transact-SQL stored procedures entity types from this list.  Dependency information is created and maintained for tables, views, and filtered statistics only.  
  
|Entity type|Referencing entity|Referenced entity|  
|-----------------|------------------------|-----------------------|  
|Table|Yes*|Yes|  
|View|Yes|Yes|  
|Filtered index|Yes**|No|  
|Filtered statistics|Yes**|No|  
|[!INCLUDE[tsql](../../includes/tsql-md.md)] stored procedure***|Yes|Yes|  
|CLR stored procedure|No|Yes|  
|[!INCLUDE[tsql](../../includes/tsql-md.md)] user-defined function|Yes|Yes|  
|CLR user-defined function|No|Yes|  
|CLR trigger (DML and DDL)|No|No|  
|[!INCLUDE[tsql](../../includes/tsql-md.md)] DML trigger|Yes|No|  
|[!INCLUDE[tsql](../../includes/tsql-md.md)] database-level DDL trigger|Yes|No|  
|[!INCLUDE[tsql](../../includes/tsql-md.md)] server-level DDL trigger|Yes|No|  
|Extended stored procedures|No|Yes|  
|Queue|No|Yes|  
|Synonym|No|Yes|  
|Type (alias and CLR user-defined type)|No|Yes|  
|XML schema collection|No|Yes|  
|Partition function|No|Yes|  
  
 \* A table is tracked as a referencing entity only when it references a [!INCLUDE[tsql](../../includes/tsql-md.md)] module, user-defined type, or XML schema collection in the definition of a computed column, CHECK constraint, or DEFAULT constraint.  
  
 ** Each column used in the filter predicate is tracked as a referencing entity.  
  
 *** Numbered stored procedures with an integer value greater than 1 are not tracked as either a referencing or referenced entity.  
  
## Permissions  
 Requires VIEW DEFINITION permission on the database and SELECT permission on sys.sql_expression_dependencies for the database. By default, SELECT permission is granted only to members of the db_owner fixed database role. When SELECT and VIEW DEFINITION permissions are granted to another user, the grantee can view all dependencies in the database.  
  
## Examples  
  
### A. Returning entities that are referenced by another entity  
 The following example returns the tables and columns referenced in the view `Production.vProductAndDescription`. The view depends on the entities (tables and columns) returned in the `referenced_entity_name` and `referenced_column_name` columns.  
  
```  
USE AdventureWorks2012;  
GO  
SELECT OBJECT_NAME(referencing_id) AS referencing_entity_name,   
    o.type_desc AS referencing_desciption,   
    COALESCE(COL_NAME(referencing_id, referencing_minor_id), '(n/a)') AS referencing_minor_id,   
    referencing_class_desc,  
    referenced_server_name, referenced_database_name, referenced_schema_name,  
    referenced_entity_name,   
    COALESCE(COL_NAME(referenced_id, referenced_minor_id), '(n/a)') AS referenced_column_name,  
    is_caller_dependent, is_ambiguous  
FROM sys.sql_expression_dependencies AS sed  
INNER JOIN sys.objects AS o ON sed.referencing_id = o.object_id  
WHERE referencing_id = OBJECT_ID(N'Production.vProductAndDescription');  
GO  
  
```  
  
### B. Returning entities that reference another entity  
 The following example returns the entities that reference the table `Production.Product`. The entities returned in the `referencing_entity_name` column depend on the `Product` table.  
  
```  
USE AdventureWorks2012;  
GO  
SELECT OBJECT_SCHEMA_NAME ( referencing_id ) AS referencing_schema_name,  
    OBJECT_NAME(referencing_id) AS referencing_entity_name,   
    o.type_desc AS referencing_desciption,   
    COALESCE(COL_NAME(referencing_id, referencing_minor_id), '(n/a)') AS referencing_minor_id,   
    referencing_class_desc, referenced_class_desc,  
    referenced_server_name, referenced_database_name, referenced_schema_name,  
    referenced_entity_name,   
    COALESCE(COL_NAME(referenced_id, referenced_minor_id), '(n/a)') AS referenced_column_name,  
    is_caller_dependent, is_ambiguous  
FROM sys.sql_expression_dependencies AS sed  
INNER JOIN sys.objects AS o ON sed.referencing_id = o.object_id  
WHERE referenced_id = OBJECT_ID(N'Production.Product');  
GO  
  
```  
  
### C. Returning cross-database dependencies  
 The following example returns all cross-database dependencies. The example first creates the database `db1` and two stored procedures that reference tables in the databases `db2` and `db3`. The `sys.sql_expression_dependencies` table is then queried to report the cross-database dependencies between the procedures and the tables. Notice that NULL is returned in the `referenced_schema_name` column for the referenced entity `t3` because a schema name was not specified for that entity in the definition of the procedure.  
  
```  
CREATE DATABASE db1;  
GO  
USE db1;  
GO  
CREATE PROCEDURE p1 AS SELECT * FROM db2.s1.t1;  
GO  
CREATE PROCEDURE p2 AS  
    UPDATE db3..t3  
    SET c1 = c1 + 1;  
GO  
SELECT OBJECT_NAME (referencing_id),referenced_database_name,   
    referenced_schema_name, referenced_entity_name  
FROM sys.sql_expression_dependencies  
WHERE referenced_database_name IS NOT NULL;  
GO  
USE master;  
GO  
DROP DATABASE db1;  
GO  
  
```  
  
## See Also  
 [sys.dm_sql_referenced_entities &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-sql-referenced-entities-transact-sql.md)   
 [sys.dm_sql_referencing_entities &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-sql-referencing-entities-transact-sql.md)  
  
  
