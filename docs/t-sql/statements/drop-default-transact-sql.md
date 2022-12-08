---
title: "DROP DEFAULT (Transact-SQL)"
description: DROP DEFAULT (Transact-SQL)
author: markingmyname
ms.author: maghan
ms.date: "05/10/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DROP_DEFAULT_TSQL"
  - "DROP DEFAULT"
helpviewer_keywords:
  - "DROP DEFAULT statement"
  - "defaults [SQL Server], removing"
dev_langs:
  - "TSQL"
---
# DROP DEFAULT (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Removes one or more user-defined defaults from the current database.  
  
> [!IMPORTANT]
>  DROP DEFAULT will be removed in the next version of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Do not use DROP DEFAULT in new development work, and plan to modify applications that currently use them. Instead, use default definitions that you can create by using the DEFAULT keyword of [ALTER TABLE](../../t-sql/statements/alter-table-transact-sql.md) or [CREATE TABLE](../../t-sql/statements/create-table-transact-sql.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
DROP DEFAULT [ IF EXISTS ] { [ schema_name . ] default_name } [ ,...n ] [ ; ]  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *IF EXISTS*  
 **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ( [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] through [current version](/troubleshoot/sql/general/determine-version-edition-update-level)).  
  
 Conditionally drops the default only if it already exists.  
  
 *schema_name*  
 Is the name of the schema to which the default belongs.  
  
 *default_name*  
 Is the name of an existing default. To see a list of defaults that exist, execute **sp_help**. Defaults must comply with the rules for [identifiers](../../relational-databases/databases/database-identifiers.md). Specifying the default schema name is optional.  
  
## Remarks  
 Before dropping a default, unbind the default by executing **sp_unbindefault** if the default is currently bound to a column or an alias data type.  
  
 After a default is dropped from a column that allows for null values, NULL is inserted in that position when rows are added and no value is explicitly supplied. After a default is dropped from a NOT NULL column, an error message is returned when rows are added and no value is explicitly supplied. These rows are added later as part of the typical INSERT statement behavior.  
  
## Permissions  
 To execute DROP DEFAULT, at a minimum, a user must have ALTER permission on the schema to which the default belongs.  
  
## Examples  
  
### A. Dropping a default  
 If a default has not been bound to a column or to an alias data type, it can just be dropped using DROP DEFAULT. The following example removes the user-created default named `datedflt`.  
  
```sql  
USE AdventureWorks2012;  
GO  
IF EXISTS (SELECT name FROM sys.objects  
         WHERE name = 'datedflt'   
            AND type = 'D')  
   DROP DEFAULT datedflt;  
GO  
```  
  
 Beginning with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] you can use the following syntax.  
  
```sql  
DROP DEFAULT IF EXISTS datedflt;  
GO  
```  
  
### B. Dropping a default that has been bound to a column  
 The following example unbinds the default associated with the `EmergencyContactPhone` column of the `Contact` table and then drops the default named `phonedflt`.  
  
```sql  
USE AdventureWorks2012;  
GO  
   BEGIN   
      EXEC sp_unbindefault 'Person.Contact.Phone'  
      DROP DEFAULT phonedflt  
   END;  
GO  
```  
  
## See Also  
 [CREATE DEFAULT &#40;Transact-SQL&#41;](../../t-sql/statements/create-default-transact-sql.md)   
 [sp_helptext &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helptext-transact-sql.md)   
 [sp_help &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-transact-sql.md)   
 [sp_unbindefault &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-unbindefault-transact-sql.md)  
