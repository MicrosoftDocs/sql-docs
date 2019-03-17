---
title: "sp_unbindefault (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_unbindefault_TSQL"
  - "sp_unbindefault"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_unbindefault"
ms.assetid: c96a6c5e-f3ca-4c1e-b64b-0d8ef6986af8
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_unbindefault (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Unbinds, or removes, a default from a column or from an alias data type in the current database.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepNextDontUse](../../includes/ssnotedepnextdontuse-md.md)] We recommend that you create default definitions by using the DEFAULT keyword in the [ALTER TABLE](../../t-sql/statements/alter-table-transact-sql.md) or [CREATE TABLE](../../t-sql/statements/create-table-transact-sql.md) statements instead.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_unbindefault [ @objname = ] 'object_name'   
     [ , [ @futureonly = ] 'futureonly_flag' ]  
```  
  
## Arguments  
 [ **@objname=** ] **'***object_name***'**  
 Is the name of the table and column or the alias data type from which the default is to be unbound. *object_name* is **nvarchar(776)**, with no default. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] attempts to resolve two-part identifiers to column names first, then to alias data types.  
  
 When unbinding a default from an alias data type, any columns of that data type that have the same default are also unbound. Columns of that data type with defaults bound directly to them are unaffected.  
  
> [!NOTE]  
>  *object_name* can contain brackets **[]** as delimited identifier characters. For more information, see [Database Identifiers](../../relational-databases/databases/database-identifiers.md).  
  
 [ **@futureonly=** ] **'***futureonly_flag***'**  
 Is used only when unbinding a default from an alias data type. *futureonly_flag* is **varchar(15)**, with a default of NULL. When *futureonly_flag* is **futureonly**, existing columns of the data type do not lose the specified default.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Remarks  
 To display the text of a default, execute **sp_helptext** with the name of the default as the parameter.  
  
## Permissions  
 To unbind a default from a table column requires ALTER permission on the table. To unbind a default from an alias data type requires CONTROL permission on the type or ALTER permission on the schema to which the type belongs.  
  
## Examples  
  
### A. Unbinding a default from a column  
 The following example unbinds the default from the `hiredate` column of an `employees` table.  
  
```  
EXEC sp_unbindefault 'employees.hiredate';  
```  
  
### B. Unbinding a default from an alias data type  
 The following example unbinds the default from the alias data type `ssn`. It unbinds existing and future columns of that type.  
  
```  
EXEC sp_unbindefault 'ssn';  
```  
  
### C. Using the futureonly_flag  
 The following example unbinds future uses of the alias data type `ssn` without affecting existing `ssn` columns.  
  
```  
EXEC sp_unbindefault 'ssn', 'futureonly';  
```  
  
### D. Using delimited identifiers  
 The following example shows using delimited identifiers in *object_name* parameter.  
  
```  
CREATE TABLE [t.3] (c1 int); -- Notice the period as part of the table   
-- name.  
CREATE DEFAULT default2 AS 0;  
GO  
EXEC sp_bindefault 'default2', '[t.3].c1' ;  
-- The object contains two periods;  
-- the first is part of the table name and the second   
-- distinguishes the table name from the column name.  
EXEC sp_unbindefault '[t.3].c1';  
```  
  
## See Also  
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)   
 [Database Engine Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/database-engine-stored-procedures-transact-sql.md)   
 [CREATE DEFAULT &#40;Transact-SQL&#41;](../../t-sql/statements/create-default-transact-sql.md)   
 [DROP DEFAULT &#40;Transact-SQL&#41;](../../t-sql/statements/drop-default-transact-sql.md)   
 [sp_bindefault &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-bindefault-transact-sql.md)   
 [sp_helptext &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helptext-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
