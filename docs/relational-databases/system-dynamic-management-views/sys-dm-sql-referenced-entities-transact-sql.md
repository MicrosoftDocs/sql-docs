---
title: "sys.dm_sql_referenced_entities (Transact-SQL)"
description: sys.dm_sql_referenced_entities (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "05/01/2019"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dm_sql_referenced_entities_TSQL"
  - "dm_sql_referenced_entities"
  - "sys.dm_sql_referenced_entities"
  - "sys.dm_sql_referenced_entities_TSQL"
helpviewer_keywords:
  - "sys.dm_sql_referenced_entities dynamic management function"
dev_langs:
  - "TSQL"
ms.assetid: 077111cb-b860-4d61-916f-bac5d532912f
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_sql_referenced_entities (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns one row for each user-defined entity that is referenced by name in the definition of the specified referencing entity in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. A dependency between two entities is created when one user-defined entity, called the *referenced entity*, appears by name in a persisted SQL expression of another user-defined entity, called the *referencing entity*. For example, if a stored procedure is the specified referencing entity, this function returns all user-defined entities that are referenced in the stored procedure such as tables, views, user-defined types (UDTs), or other stored procedures.  
  
 You can use this dynamic management function to report on the following types of entities referenced by the specified referencing entity:  
  
-   Schema-bound entities  
  
-   Non-schema-bound entities  
  
-   Cross-database and cross-server entities  
  
-   Column-level dependencies on schema-bound and non-schema-bound entities  
  
-   User-defined types (alias and CLR UDT)  
  
-   XML schema collections  
  
-   Partition functions  

## Syntax  
  
```  
sys.dm_sql_referenced_entities (  
    ' [ schema_name. ] referencing_entity_name ' ,
    ' <referencing_class> ' )  
  
<referencing_class> ::=  
{  
    OBJECT  
  | DATABASE_DDL_TRIGGER  
  | SERVER_DDL_TRIGGER  
}  
```  
  
## Arguments  
 [ *schema_name*. ] *referencing_entity_name*  
 Is the name of the referencing entity. *schema_name* is required when the referencing class is OBJECT.  
  
 *schema_name.referencing_entity_name* is **nvarchar(517)**.  
  
 *<referencing_class>* ::=  { OBJECT | DATABASE_DDL_TRIGGER   | SERVER_DDL_TRIGGER }  
 Is the class of the specified referencing entity. Only one class can be specified per statement.  
  
 *<referencing_class>* is **nvarchar(60)**.  
  
## Table Returned  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|referencing_minor_id|**int**|Column ID when the referencing entity is a column; otherwise 0. Is not nullable.|  
|referenced_server_name|**sysname**|Name of the server of the referenced entity.<br /><br /> This column is populated for cross-server dependencies that are made by specifying a valid four-part name. For information about multipart names, see [Transact-SQL Syntax Conventions &#40;Transact-SQL&#41;](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md).<br /><br /> NULL for non-schema-bound dependencies for which the entity was referenced without specifying a four-part name.<br /><br /> NULL for schema-bound entities because they must be in the same database and therefore can only be defined using a two-part (*schema.object*) name.|  
|referenced_database_name|**sysname**|Name of the database of the referenced entity.<br /><br /> This column is populated for cross-database or cross-server references that are made by specifying a valid three-part or four-part name.<br /><br /> NULL for non-schema-bound references when specified using a one-part or two-part name.<br /><br /> NULL for schema-bound entities because they must be in the same database and therefore can only be defined using a two-part (*schema.object*) name.|  
|referenced_schema_name|**sysname**|Schema in which the referenced entity belongs.<br /><br /> NULL for non-schema-bound references in which the entity was referenced without specifying the schema name.<br /><br /> Never NULL for schema-bound references.|  
|referenced_entity_name|**sysname**|Name of the referenced entity. Is not nullable.|  
|referenced_minor_name|**sysname**|Column name when the referenced entity is a column; otherwise NULL. For example, referenced_minor_name is NULL in the row that lists the referenced entity itself.<br /><br /> A referenced entity is a column when a column is identified by name in the referencing entity, or when the parent entity is used in a SELECT * statement.|  
|referenced_id|**int**|ID of the referenced entity. When referenced_minor_id is not 0, referenced_id is the entity in which the column is defined.<br /><br /> Always NULL for cross-server references.<br /><br /> NULL for cross-database references when the ID cannot be determined because the database is offline or the entity cannot be bound.<br /><br /> NULL for references within the database if the ID cannot be determined. For non-schema-bound references, the ID cannot be resolved when the  referenced entity does not exist in the database or when the name resolution is caller dependent.  In the latter case, is_caller_dependent is set to 1.<br /><br /> Never NULL for schema-bound references.|  
|referenced_minor_id|**int**|Column ID when the referenced entity is a column; otherwise, 0. For example, referenced_minor_is is 0 in the row that lists the referenced entity itself.<br /><br /> For non-schema-bound references, column dependencies are reported only when all referenced entities can be bound. If any referenced entity cannot be bound, no column-level dependencies are reported and referenced_minor_id is 0. See Example D.|  
|referenced_class|**tinyint**|Class of the referenced entity.<br /><br /> 1 = Object or column<br /><br /> 6 = Type<br /><br /> 10 = XML schema collection<br /><br /> 21 = Partition function|  
|referenced_class_desc|**nvarchar(60)**|Description of class of referenced entity.<br /><br /> OBJECT_OR_COLUMN<br /><br /> TYPE<br /><br /> XML_SCHEMA_COLLECTION<br /><br /> PARTITION_FUNCTION|  
|is_caller_dependent|**bit**|Indicates schema binding for the referenced entity occurs at run time; therefore, resolution of the entity ID depends on the schema of the caller. This occurs when the referenced entity is a stored procedure, extended stored procedure, or user-defined function called within an EXECUTE statement.<br /><br /> 1 = The referenced entity is caller dependent and is resolved at run time. In this case, referenced_id is NULL.<br /><br /> 0 = The referenced entity ID is not caller dependent. Always 0 for schema-bound references and for cross-database and cross-server references that explicitly specify a schema name. For example, a reference to an entity in the format `EXEC MyDatabase.MySchema.MyProc` is not caller dependent. However, a reference in the format `EXEC MyDatabase..MyProc` is caller dependent.|  
|is_ambiguous|**bit**|Indicates the reference is ambiguous and can resolve at run time to a user-defined function, a user-defined type (UDT), or an xquery reference to a column of type **xml**. For example, assume the statement `SELECT Sales.GetOrder() FROM Sales.MySales` is defined in a stored procedure. Until the stored procedure is executed, it is not known whether `Sales.GetOrder()` is a user-defined function in the `Sales` schema or column named `Sales` of type UDT with a method named `GetOrder()`.<br /><br /> 1 = Reference to a user-defined function or column user-defined type (UDT) method is ambiguous.<br /><br /> 0 = Reference is unambiguous or the entity can be successfully bound when the function is called.<br /><br /> Always 0 for schema-bound references.|  
|is_selected|**bit**|1 = The object or column is selected.|  
|is_updated|**bit**|1 = The object or column is modified.|  
|is_select_all|**bit**|1 = The object is used in a SELECT * clause (object-level only).|  
|is_all_columns_found|**bit**|1 = All column dependencies for the object could be found.<br /><br /> 0 = Column dependencies for the object could not be found.|
|is_insert_all|**bit**|1 = The object is used in an INSERT statement without a column list (object-level only).<br /><br />This column was added in SQL Server 2016.|  
|is_incomplete|**bit**|1 = The object or column has a binding error and is incomplete.<br /><br />This column was added in SQL Server 2016 SP2.|

## Exceptions  
 Returns an empty result set under any of the following conditions:  
  
-   A system object is specified.  
  
-   The specified entity does not exist in the current database.  
  
-   The specified entity does not reference any entities.  
  
-   An invalid parameter is passed.  
  
 Returns an error when the specified referencing entity is a numbered stored procedure.  
  
 Returns error 2020 when column dependencies cannot be resolved. This error does not prevent the query from returning object-level dependencies.  
  
## Remarks  
 This function can be executed in the context of the any database to return the entities that reference a server-level DDL trigger.  
  
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
 Requires SELECT permission on sys.dm_sql_referenced_entities and VIEW DEFINITION permission on the referencing entity. By default, SELECT permission is granted to public. Requires VIEW DEFINITION permission on the database or ALTER DATABASE DDL TRIGGER permission on the database when the referencing entity is a database-level DDL trigger. Requires VIEW ANY DEFINITION permission on the server when the referencing entity is a server-level DDL trigger.  
  
## Examples  
  
### A. Return entities that are referenced by a database-level DDL trigger  
 The following example returns the entities (tables and columns) that are referenced by the database-level DDL trigger `ddlDatabaseTriggerLog`.  
  
```sql  
USE AdventureWorks2012;  
GO  
SELECT
        referenced_schema_name,
        referenced_entity_name,
        referenced_minor_name,
        referenced_minor_id,
        referenced_class_desc
    FROM
        sys.dm_sql_referenced_entities (
            'ddlDatabaseTriggerLog',
            'DATABASE_DDL_TRIGGER')
;
GO  
```  
  
### B. Return entities that are referenced by an object  
 The following example returns the entities that are referenced by the user-defined function `dbo.ufnGetContactInformation`.  
  
```sql  
USE AdventureWorks2012;  
GO  
SELECT
        referenced_schema_name,
        referenced_entity_name,
        referenced_minor_name,
        referenced_minor_id,
        referenced_class_desc,
        is_caller_dependent,
        is_ambiguous
    FROM
        sys.dm_sql_referenced_entities (
            'dbo.ufnGetContactInformation',
            'OBJECT')
;
GO  
```  
  
### C. Return column dependencies  
 The following example creates the table `Table1` with the computed column `c` defined as the sum of columns `a` and `b`. The `sys.dm_sql_referenced_entities` view is then called. The view returns two rows, one for each column defined in the computed column.  
  
```sql  
CREATE TABLE dbo.Table1 (a int, b int, c AS a + b);  
GO  
SELECT
        referenced_schema_name AS schema_name,  
        referenced_entity_name AS table_name,  
        referenced_minor_name  AS referenced_column,  
        COALESCE(
            COL_NAME(OBJECT_ID(N'dbo.Table1'),
            referencing_minor_id),
            'N/A') AS referencing_column_name  
    FROM
        sys.dm_sql_referenced_entities ('dbo.Table1', 'OBJECT')
;
GO

-- Remove the table.  
DROP TABLE dbo.Table1;  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 ```console
 schema_name table_name referenced_column referencing_column  
 ----------- ---------- ----------------- ------------------  
 dbo         Table1     a                 c  
 dbo         Table1     b                 c  
```

### D. Returning non-schema-bound column dependencies  
 The following example drops `Table1` and creates `Table2` and stored procedure `Proc1`. The procedure references `Table2` and the nonexistent table `Table1`. The view `sys.dm_sql_referenced_entities` is run with the stored procedure specified as the referencing entity. The result set shows one row for `Table1` and 3 rows for `Table2`. Because `Table1` does not exist, the column dependencies cannot be resolved and error 2020 is returned. The `is_all_columns_found` column returns 0 for `Table1` indicating that there were columns that could not be discovered.  
  
```sql  
DROP TABLE IF EXISTS dbo.Table1;
GO  
CREATE TABLE dbo.Table2 (c1 int, c2 int);  
GO  
CREATE PROCEDURE dbo.Proc1 AS  
    SELECT a, b, c FROM Table1;  
    SELECT c1, c2 FROM Table2;  
GO  
SELECT
        referenced_id,
        referenced_entity_name AS table_name,
        referenced_minor_name  AS referenced_column_name,
        is_all_columns_found
    FROM
        sys.dm_sql_referenced_entities ('dbo.Proc1', 'OBJECT');
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 ```console
 referenced_id table_name   referenced_column_name  is_all_columns_found  
 ------------- ------------ ----------------------- --------------------  
 935674381     Table2       NULL                    1  
 935674381     Table2       C1                      1  
 935674381     Table2       C2                      1  
 NULL          Table1       NULL                    0  

Msg 2020, Level 16, State 1, Line 1
The dependencies reported for entity "dbo.Proc1" might not include
  references to all columns. This is either because the entity
  references an object that does not exist or because of an error
  in one or more statements in the entity.  Before rerunning the
  query, ensure that there are no errors in the entity and that
  all objects referenced by the entity exist.
 ```
  
### E. Demonstrating dynamic dependency maintenance  

This Example E assumes that Example D has been run. Example E shows that dependencies are maintained dynamically. The example does the following things:

1. Re-creates `Table1`, which was dropped in Example D.
2. Run Then `sys.dm_sql_referenced_entities` is run again with the stored procedure specified as the referencing entity.

The result set shows that both tables, and their respective columns defined in the stored procedure, are returned. In addition, the `is_all_columns_found` column returns a 1 for all objects and columns.

```sql  
CREATE TABLE Table1 (a int, b int, c AS a + b);  
GO   
SELECT
        referenced_id,
        referenced_entity_name AS table_name,
        referenced_minor_name  AS column_name,
        is_all_columns_found
    FROM
        sys.dm_sql_referenced_entities ('dbo.Proc1', 'OBJECT');
GO  
DROP TABLE Table1, Table2;  
DROP PROC Proc1;  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 ```console
 referenced_id table_name   referenced_column_name  is_all_columns_found  
 ------------- ------------ ----------------------- --------------------  
 935674381     Table2       NULL                    1 
 935674381     Table2       c1                      1 
 935674381     Table2       c2                      1 
 967674495     Table1       NULL                    1 
 967674495     Table1       a                       1  
 967674495     Table1       b                       1  
 967674495     Table1       c                       1  
 ```
 
### F. Returning object or column usage  
 The following example returns the objects and column dependencies of the stored procedure `HumanResources.uspUpdateEmployeePersonalInfo`. This procedure updates the columns `NationalIDNumber`, `BirthDate,``MaritalStatus`, and `Gender` of the `Employee` table based on a specified `BusinessEntityID` value. Another stored procedure, `upsLogError` is defined in a TRY...CATCH block to capture any execution errors. The `is_selected`, `is_updated`, and `is_select_all` columns return information about how these objects and columns are used within the referencing object. The table and columns that are modified are indicated by a 1 in the is_updated column. The `BusinessEntityID` column is only selected and the stored procedure `uspLogError` is neither selected nor modified.  

```sql  
USE AdventureWorks2012;
GO
SELECT
        referenced_entity_name AS table_name,
        referenced_minor_name  AS column_name,
        is_selected,  is_updated,  is_select_all
    FROM
        sys.dm_sql_referenced_entities(
            'HumanResources.uspUpdateEmployeePersonalInfo',
            'OBJECT')
;
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 ```console
 table_name    column_name         is_selected is_updated is_select_all  
 ------------- ------------------- ----------- ---------- -------------  
 uspLogError   NULL                0           0          0  
 Employee      NULL                0           1          0  
 Employee      BusinessEntityID    1           0          0  
 Employee      NationalIDNumber    0           1          0  
 Employee      BirthDate           0           1          0  
 Employee      MaritalStatus       0           1          0  
 Employee      Gender              0           1          0
 ```
  
## See Also  
 [sys.dm_sql_referencing_entities &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-sql-referencing-entities-transact-sql.md)   
 [sys.sql_expression_dependencies &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-sql-expression-dependencies-transact-sql.md)  
  
  
