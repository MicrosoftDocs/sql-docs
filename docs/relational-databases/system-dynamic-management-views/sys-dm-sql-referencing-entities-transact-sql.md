---
title: "sys.dm_sql_referencing_entities (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.dm_sql_referencing_entities"
  - "dm_sql_referencing_entities_TSQL"
  - "sys.dm_sql_referencing_entities_TSQL"
  - "dm_sql_referencing_entities"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_sql_referencing_entities dynamic management function"
ms.assetid: c16f8f0a-483f-4feb-842e-da90426045ae
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_sql_referencing_entities (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Returns one row for each entity in the current database that references another user-defined entity by name. A dependency between two entities is created when one entity, called the *referenced entity*, appears by name in a persisted SQL expression of another entity, called the *referencing entity*. For example, if a user-defined type (UDT) is specified as the referenced entity, this function returns each user-defined entity that reference that type by name in its definition. The function does not return entities in other databases that may reference the specified entity. This function must be executed in the context of the master database to return a server-level DDL trigger as a referencing entity.  
  
 You can use this dynamic management function to report on the following types of entities in the current database that reference the specified entity:  
  
-   Schema-bound or non-schema-bound entities  
  
-   Database-level DDL triggers  
  
-   Server-level DDL triggers  
  
**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ( [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]), [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)].  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
sys.dm_sql_referencing_entities (  
    ' schema_name.referenced_entity_name ' , ' <referenced_class> ' )  
  
<referenced_class> ::=  
{  
    OBJECT  
  | TYPE  
  | XML_SCHEMA_COLLECTION  
  | PARTITION_FUNCTION  
}  
```  
  
## Arguments  
 *schema_name.referenced*_*entity_name*  
 Is the name of the referenced entity.  
  
 *schema_name* is required except when the referenced class is PARTITION_FUNCTION.  
  
 *schema_name.referenced_entity_name* is **nvarchar(517)**.  
  
 *<referenced_class>* ::= { OBJECT  | TYPE | XML_SCHEMA_COLLECTION | PARTITION_FUNCTION }  
 Is the class of the referenced entity. Only one class can be specified per statement.  
  
 *<referenced_class>* is **nvarchar**(60).  
  
## Table Returned  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|referencing_schema_name|**sysname**|Schema in which the referencing entity belongs. Is nullable.<br /><br /> NULL for database-level and server-level DDL triggers.|  
|referencing_entity_name|**sysname**|Name of the referencing entity. Is not nullable.|  
|referencing_id|**int**|ID of the referencing entity. Is not nullable.|  
|referencing_class|**tinyint**|Class of the referencing entity. Is not nullable.<br /><br /> 1 = Object<br /><br /> 12 = Database-level DDL trigger<br /><br /> 13 = Server-level DDL trigger|  
|referencing_class_desc|**nvarchar(60)**|Description of class of referencing entity.<br /><br /> OBJECT<br /><br /> DATABASE_DDL_TRIGGER<br /><br /> SERVER_DDL_TRIGGER|  
|is_caller_dependent|**bit**|Indicates the resolution of the referenced entity ID occurs at run time because it depends on the schema of the caller.<br /><br /> 1 = The referencing entity has the potential to reference the entity; however, resolution of the referenced entity ID is caller dependent and cannot be determined. This occurs only for non-schema-bound references to a stored procedure, extended stored procedure, or user-defined function called in an EXECUTE statement.<br /><br /> 0 = Referenced entity is not caller dependent.|  
  
## Exceptions  
 Returns an empty result set under any of the following conditions:  
  
-   A system object is specified.  
  
-   The specified entity does not exist in the current database.  
  
-   The specified entity does not reference any entities.  
  
-   An invalid parameter is passed.  
  
 Returns an error when the specified referenced entity is a numbered stored procedure.  
  
## Remarks  
 The following table lists the types of entities for which dependency information is created and maintained. Dependency information is not created or maintained for rules, defaults, temporary tables, temporary stored procedures, or system objects.  
  
|Entity type|Referencing entity|Referenced entity|  
|-----------------|------------------------|-----------------------|  
|Table|Yes*|Yes|  
|View|Yes|Yes|  
|[!INCLUDE[tsql](../../includes/tsql-md.md)] stored procedure**|Yes|Yes|  
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
  
 ** Numbered stored procedures with an integer value greater than 1 are not tracked as either a referencing or referenced entity.  
  
## Permissions  
  
### [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] - [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]  
  
-   Requires CONTROL permission on the referenced object. When the referenced entity is a partition function, CONTROL permission on the database is required.  
  
-   Requires SELECT permission on sys.dm_sql_referencing_entities. By default, SELECT permission is granted to public.  
  
### [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] - [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]  
  
-   Requires no permissions on the referenced object. Partial results can be returned if the user has VIEW DEFINITION on only some of the referencing entities.  
  
-   Requires VIEW DEFINITION on the object when the referencing entity is an object.  
  
-   Requires VIEW DEFINITION on the database when the referencing entity is a database-level DDL trigger.  
  
-   Requires VIEW ANY DEFINITION on the server when the referencing entity is a server-level DDL trigger.  
  
## Examples  
  
### A. Returning the entities that refer to a given entity  
 The following example returns the entities in the current database that refer to the specified table.  
  
```sql  
USE AdventureWorks2012;  
GO  
SELECT referencing_schema_name, referencing_entity_name, referencing_id, referencing_class_desc, is_caller_dependent  
FROM sys.dm_sql_referencing_entities ('Production.Product', 'OBJECT');  
GO  
```  
  
### B. Returning the entities that refer to a given type  
 The following example returns the entities that reference the alias type `dbo.Flag`. The result set shows that two stored procedures use this type. The `dbo.Flag` type is also used in the definition of several columns in the `HumanResources.Employee` table; however, because the type is not in the definition of a computed column, CHECK constraint, or DEFAULT constraint in the table, no rows are returned for the `HumanResources.Employee` table.  
  
```sql  
USE AdventureWorks2012;  
GO  
SELECT referencing_schema_name, referencing_entity_name, referencing_id, referencing_class_desc, is_caller_dependent  
FROM sys.dm_sql_referencing_entities ('dbo.Flag', 'TYPE');  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 ```
 referencing_schema_name referencing_entity_name   referencing_id referencing_class_desc is_caller_dependent  
 ----------------------- -------------------------  ------------- ---------------------- -------------------  
 HumanResources          uspUpdateEmployeeHireInfo  1803153469    OBJECT_OR_COLUMN       0  
 HumanResources          uspUpdateEmployeeLogin     1819153526    OBJECT_OR_COLUMN       0  
 (2 row(s) affected)`  
 ``` 
 
## See Also  
 [sys.dm_sql_referenced_entities &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-sql-referenced-entities-transact-sql.md)   
 [sys.sql_expression_dependencies &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-sql-expression-dependencies-transact-sql.md)  
  
  
