---
title: "sp_rename (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "01/09/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_rename_TSQL"
  - "sp_rename"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "renaming indexes"
  - "renaming columns"
  - "sp_rename"
  - "renaming tables"
ms.assetid: bc3548f0-143f-404e-a2e9-0a15960fc8ed
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sp_rename (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Changes the name of a user-created object in the current database. This object can be a table, index, column, alias data type, or [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] common language runtime (CLR) user-defined type.  
  
> [!CAUTION]  
>  Changing any part of an object name can break scripts and stored procedures. We recommend you do not use this statement to rename stored procedures, triggers, user-defined functions, or views; instead, drop the object and re-create it with the new name.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_rename [ @objname = ] 'object_name' , [ @newname = ] 'new_name'   
    [ , [ @objtype = ] 'object_type' ]   
```  
  
## Arguments  
 [ @objname = ] '*object_name*'  
 Is the current qualified or nonqualified name of the user object or data type. If the object to be renamed is a column in a table, *object_name* must be in the form *table.column* or *schema.table.column*. If the object to be renamed is an index, *object_name* must be in the form *table.index* or *schema.table.index*. If the object to be renamed is a constraint, *object_name* must be in the form *schema.constraint*.  
  
 Quotation marks are only necessary if a qualified object is specified. If a fully qualified name, including a database name, is provided, the database name must be the name of the current database. *object_name* is **nvarchar(776)**, with no default.  
  
 [ @newname = ] '*new_name*'  
 Is the new name for the specified object. *new_name* must be a one-part name and must follow the rules for identifiers. *newname* is **sysname**, with no default.  
  
> [!NOTE]  
>  Trigger names cannot start with # or ##.  
  
 [ @objtype = ] '*object_type*'  
 Is the type of object being renamed. *object_type* is **varchar(13)**, with a default of NULL, and can be one of these values.  
  
|Value|Description|  
|-----------|-----------------|  
|COLUMN|A column to be renamed.|  
|DATABASE|A user-defined database. This object type is required when renaming a database.|  
|INDEX|A user-defined index. Renaming an index with statistics, also automatically renames the statistics.|  
|OBJECT|An item of a type tracked in [sys.objects](../../relational-databases/system-catalog-views/sys-objects-transact-sql.md). For example, OBJECT could be used to rename objects including constraints (CHECK, FOREIGN KEY, PRIMARY/UNIQUE KEY), user tables, and rules.|  
|STATISTICS|**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].<br /><br /> Statistics created explicitly by a user or created implicitly with an index. Renaming the statistics of an index automatically renames the index as well.|  
|USERDATATYPE|A [CLR User-defined Types](../../relational-databases/clr-integration-database-objects-user-defined-types/clr-user-defined-types.md) added by executing [CREATE TYPE](../../t-sql/statements/create-type-transact-sql.md) or [sp_addtype](../../relational-databases/system-stored-procedures/sp-addtype-transact-sql.md).|  
  
## Return Code Values  
 0 (success) or a nonzero number (failure)  
  
## Remarks  
 You can change the name of an object or data type in the current database only. The names of most system data types and system objects cannot be changed.  
  
 sp_rename automatically renames the associated index whenever a PRIMARY KEY or UNIQUE constraint is renamed. If a renamed index is tied to a PRIMARY KEY constraint, the PRIMARY KEY constraint is also automatically renamed by sp_rename.  
  
 sp_rename can be used to rename primary and secondary XML indexes.  
  
 Renaming a stored procedure, function, view, or trigger will not change the name of the corresponding object either in the definition column of the [sys.sql_modules](../../relational-databases/system-catalog-views/sys-sql-modules-transact-sql.md) catalog view or obtained using the [OBJECT_DEFINITION](../../t-sql/functions/object-definition-transact-sql.md) built-in function. Therefore, we recommend that sp_rename not be used to rename these object types. Instead, drop and re-create the object with its new name.  
  
 Renaming an object such as a table or column will not automatically rename references to that object. You must modify any objects that reference the renamed object manually. For example, if you rename a table column and that column is referenced in a trigger, you must modify the trigger to reflect the new column name. Use [sys.sql_expression_dependencies](../../relational-databases/system-catalog-views/sys-sql-expression-dependencies-transact-sql.md) to list dependencies on the object before renaming it.  
  
## Permissions  
 To rename objects, columns, and indexes, requires ALTER permission on the object. To rename user types, requires CONTROL permission on the type. To rename a database, requires membership in the sysadmin or dbcreator fixed server roles  
  
## Examples  
  
### A. Renaming a table  
 The following example renames the `SalesTerritory` table to `SalesTerr` in the `Sales` schema.  
  
```  
USE AdventureWorks2012;  
GO  
EXEC sp_rename 'Sales.SalesTerritory', 'SalesTerr';  
GO  
```  
  
### B. Renaming a column  
 The following example renames the `TerritoryID` column in the `SalesTerritory` table to `TerrID`.  
  
```  
USE AdventureWorks2012;  
GO  
EXEC sp_rename 'Sales.SalesTerritory.TerritoryID', 'TerrID', 'COLUMN';  
GO  
```  
  
### C. Renaming an index  
 The following example renames the `IX_ProductVendor_VendorID` index to `IX_VendorID`.  
  
```  
USE AdventureWorks2012;  
GO  
EXEC sp_rename N'Purchasing.ProductVendor.IX_ProductVendor_VendorID', N'IX_VendorID', N'INDEX';  
GO  
```  
  
### D. Renaming an alias data type  
 The following example renames the `Phone` alias data type to `Telephone`.  
  
```  
USE AdventureWorks2012;  
GO  
EXEC sp_rename N'Phone', N'Telephone', N'USERDATATYPE';  
GO  
```  
  
### E. Renaming constraints  
 The following examples rename a PRIMARY KEY constraint, a CHECK constraint and a FOREIGN KEY constraint. When renaming a constraint, the schema to which the constraint belongs must be specified.  
  
```  
USE AdventureWorks2012;   
GO  
-- Return the current Primary Key, Foreign Key and Check constraints for the Employee table.  
SELECT name, SCHEMA_NAME(schema_id) AS schema_name, type_desc  
FROM sys.objects  
WHERE parent_object_id = (OBJECT_ID('HumanResources.Employee'))   
AND type IN ('C','F', 'PK');   
GO  
  
-- Rename the primary key constraint.  
sp_rename 'HumanResources.PK_Employee_BusinessEntityID', 'PK_EmployeeID';  
GO  
  
-- Rename a check constraint.  
sp_rename 'HumanResources.CK_Employee_BirthDate', 'CK_BirthDate';  
GO  
  
-- Rename a foreign key constraint.  
sp_rename 'HumanResources.FK_Employee_Person_BusinessEntityID', 'FK_EmployeeID';  
  
-- Return the current Primary Key, Foreign Key and Check constraints for the Employee table.  
SELECT name, SCHEMA_NAME(schema_id) AS schema_name, type_desc  
FROM sys.objects  
WHERE parent_object_id = (OBJECT_ID('HumanResources.Employee'))   
AND type IN ('C','F', 'PK');  
  
```  
  
```  
  
name                                  schema_name        type_desc  
------------------------------------- ------------------ ----------------------  
FK_Employee_Person_BusinessEntityID   HumanResources     FOREIGN_KEY_CONSTRAINT  
PK_Employee_BusinessEntityID          HumanResources     PRIMARY_KEY_CONSTRAINT  
CK_Employee_BirthDate                 HumanResources     CHECK_CONSTRAINT  
CK_Employee_MaritalStatus             HumanResources     CHECK_CONSTRAINT  
CK_Employee_HireDate                  HumanResources     CHECK_CONSTRAINT  
CK_Employee_Gender                    HumanResources     CHECK_CONSTRAINT  
CK_Employee_VacationHours             HumanResources     CHECK_CONSTRAINT  
CK_Employee_SickLeaveHours            HumanResources     CHECK_CONSTRAINT  
  
(7 row(s) affected)  
  
name                                  schema_name        type_desc  
------------------------------------- ------------------ ----------------------  
FK_Employee_ID                        HumanResources     FOREIGN_KEY_CONSTRAINT  
PK_Employee_ID                        HumanResources     PRIMARY_KEY_CONSTRAINT  
CK_BirthDate                          HumanResources     CHECK_CONSTRAINT  
CK_Employee_MaritalStatus             HumanResources     CHECK_CONSTRAINT  
CK_Employee_HireDate                  HumanResources     CHECK_CONSTRAINT  
CK_Employee_Gender                    HumanResources     CHECK_CONSTRAINT  
CK_Employee_VacationHours             HumanResources     CHECK_CONSTRAINT  
CK_Employee_SickLeaveHours            HumanResources     CHECK_CONSTRAINT  
  
(7 row(s) affected)  
  
```  
  
### F. Renaming statistics  
 The following example creates a statistics object named contactMail1 and then renames the statistic to NewContact by using sp_rename. When renaming statistics, the object must be specified in the format schema.table.statistics_name.  
  
```  
CREATE STATISTICS ContactMail1  
    ON Person.Person (BusinessEntityID, EmailPromotion)  
    WITH SAMPLE 5 PERCENT;  
  
sp_rename 'Person.Person.ContactMail1', 'NewContact','Statistics';  
  
```  
  
## See Also  
 [sys.sql_expression_dependencies &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-sql-expression-dependencies-transact-sql.md)   
 [sys.sql_modules &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-sql-modules-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)   
 [Database Engine Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/database-engine-stored-procedures-transact-sql.md)  
  
  
