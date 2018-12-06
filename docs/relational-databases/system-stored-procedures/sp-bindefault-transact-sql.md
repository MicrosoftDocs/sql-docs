---
title: "sp_bindefault (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "11/25/2015"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_bindefault"
  - "sp_bindefault_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_bindefault"
ms.assetid: 3da70c10-68d0-4c16-94a5-9e84c4a520f6
author: "stevestein"
ms.author: "sstein"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sp_bindefault (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Binds a default to a column or to an alias data type.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureDontUse](../../includes/ssnotedepfuturedontuse-md.md)] We recommend that you create default definitions by using the DEFAULT keyword of the [ALTER TABLE](../../t-sql/statements/alter-table-transact-sql.md) or [CREATE TABLE](../../t-sql/statements/create-table-transact-sql.md) statements instead.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_bindefault [ @defname = ] 'default' ,   
    [ @objname = ] 'object_name'   
    [ , [ @futureonly = ] 'futureonly_flag' ]   
```  
  
## Arguments  
 [ **@defname=** ] **'***default***'**  
 Is the name of the default that is created by CREATE DEFAULT. *default* is **nvarchar(776)**, with no default.  
  
 [ **@objname=** ] **'***object_name***'**  
 Is the name of table and column or the alias data type to which the default is to be bound. *object_name* is **nvarchar(776)** with no default. *object_name* cannot be defined with the **varchar(max)**, **nvarchar(max)**, **varbinary(max)**, **xml**, or CLR user-defined types.  
  
 If *object_name* is a one-part name, it is resolved as an alias data type. If it is a two- or three-part name, it is first resolved as a table and column; and if this resolution fails, it is resolved as an alias data type. By default, existing columns of the alias data type inherit *default*, unless a default has been bound directly to the column. A default cannot be bound to a **text**, **ntext**, **image**, **varchar(max)**, **nvarchar(max)**, **varbinary(max)**, **xml**, **timestamp**, or CLR user-defined type column, a column with the IDENTITY property, a computed column, or a column that already has a DEFAULT constraint.  
  
> [!NOTE]  
>  *object_name* can contain brackets **[]** as delimited identifiers. For more information, see [Database Identifiers](../../relational-databases/databases/database-identifiers.md).  
  
 [ **@futureonly=** ] **'***futureonly_flag***'**  
 Is used only when binding a default to an alias data type. *futureonly_flag* is **varchar(15)** with a default of NULL. When this parameter is set to **futureonly**, existing columns of that data type cannot inherit the new default. This parameter is never used when binding a default to a column. If *futureonly_flag* is NULL, the new default is bound to any columns of the alias data type that currently have no default or that are using the existing default of the alias data type.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Remarks  
 You can use **sp_bindefault** to bind a new default to a column, although using the DEFAULT constraint is preferred, or to an alias data type without unbinding an existing default. The old default is overridden. You cannot bind a default to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] system data type or a CLR user-defined type. If the default is not compatible with the column to which you have bound it, the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] returns an error message when it tries to insert the default value, not when you bind it.  
  
 Existing columns of the alias data type inherit the new default, unless either a default is bound directly to them or *futureonly_flag* is specified as **futureonly**. New columns of the alias data type always inherit the default.  
  
 When you bind a default to a column, related information is added to the **sys.columns** catalog view. When you bind a default to an alias data type, related information is added to the **sys.types** catalog view.  
  
## Permissions  
 User must own the table, or be a member of the **sysadmin** fixed server role, or the **db_owner** and **db_ddladmin** fixed database roles.  
  
## Examples  
  
### A. Binding a default to a column  
 A default named `today` has been defined in the current database by using CREATE DEFAULT, the following example binds the default to the `HireDate` column of the `Employee` table. Whenever a row is added to the `Employee` table and data for the `HireDate` column is not supplied, the column gets the value of the default `today`.  
  
```  
USE master;  
GO  
EXEC sp_bindefault 'today', 'HumanResources.Employee.HireDate';  
```  
  
### B. Binding a default to an alias data type  
 A default named `def_ssn` and an alias data type named `ssn` already exists. The following example binds the default `def_ssn` to `ssn`. When a table is created, the default is inherited by all columns that are assigned the alias data type `ssn`. Existing columns of type **ssn** also inherit the default **def_ssn**, unless **futureonly** is specified for *futureonly_flag* value, or unless the column has a default bound directly to it. Defaults bound to columns always take precedence over those bound to data types.  
  
```  
USE master;  
GO  
EXEC sp_bindefault 'def_ssn', 'ssn';  
```  
  
### C. Using the futureonly_flag  
 The following example binds the default `def_ssn` to the alias data type `ssn`. Because **futureonly** is specified, no existing columns of type `ssn` are affected.  
  
```  
USE master;  
GO  
EXEC sp_bindefault 'def_ssn', 'ssn', 'futureonly';  
```  
  
### D. Using delimited identifiers  
 The following example shows using delimited identifiers, `[t.1]`, in *object_name*.  
  
```  
USE master;  
GO  
CREATE TABLE [t.1] (c1 int);   
-- Notice the period as part of the table name.  
EXEC sp_bindefault 'default1', '[t.1].c1' ;  
-- The object contains two periods;   
-- the first is part of the table name,   
-- and the second distinguishes the table name from the column name.  
```  
  
## See Also  
 [Database Engine Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/database-engine-stored-procedures-transact-sql.md)   
 [CREATE DEFAULT &#40;Transact-SQL&#41;](../../t-sql/statements/create-default-transact-sql.md)   
 [DROP DEFAULT &#40;Transact-SQL&#41;](../../t-sql/statements/drop-default-transact-sql.md)   
 [sp_unbindefault &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-unbindefault-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
