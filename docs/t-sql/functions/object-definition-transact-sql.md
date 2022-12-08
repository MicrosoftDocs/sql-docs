---
title: "OBJECT_DEFINITION (Transact-SQL)"
description: "OBJECT_DEFINITION (Transact-SQL)"
author: VanMSFT
ms.author: vanto
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "OBJECT_DEFINITION_TSQL"
  - "OBJECT_DEFINITION"
helpviewer_keywords:
  - "viewing source text"
  - "source text [SQL Server]"
  - "displaying source text"
  - "OBJECT_DEFINITION function"
dev_langs:
  - "TSQL"
---
# OBJECT_DEFINITION (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns the [!INCLUDE[tsql](../../includes/tsql-md.md)] source text of the definition of a specified object.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
OBJECT_DEFINITION ( object_id )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *object_id*  
 Is the ID of the object to be used. *object_id* is **int**, and assumed to represent an object in the current database context.  
  
## Return Types  
 **nvarchar(max)**  
  
## Exceptions  
 Returns NULL on error or if a caller does not have permission to view the object.  
  
 A user can only view the metadata of securables that the user owns or on which the user has been granted permission. This means that metadata-emitting, built-in functions such as OBJECT_DEFINITION may return NULL if the user does not have any permission on the object. For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## Remarks  
 The [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] assumes that *object_id* is in the current database context. The collation of the object definition always matches that of the calling database context.  
  
 OBJECT_DEFINITION applies to the following object types:  
  
-   C = Check constraint  
  
-   D = Default (constraint or stand-alone)  
  
-   P = SQL stored procedure  
  
-   FN = SQL scalar function  
  
-   R = Rule  
  
-   RF = Replication filter procedure  
  
-   TR = SQL trigger (schema-scoped DML trigger, or DDL trigger at either the database or server scope)  
  
-   IF = SQL inline table-valued function  
  
-   TF = SQL table-valued function  
  
-   V = View  
  
## Permissions  
 System object definitions are publicly visible. The definition of user objects is visible to the object owner or grantees that have any one of the following permissions: ALTER, CONTROL, TAKE OWNERSHIP, or VIEW DEFINITION. These permissions are implicitly held by members of the **db_owner**, **db_ddladmin**, and **db_securityadmin** fixed database roles.  
  
## Examples  
  
### A. Returning the source text of a user-defined object  
 The following example returns the definition of a user-defined trigger, `uAddress`, in the `Person` schema. The built-in function `OBJECT_ID` is used to return the object ID of the trigger to the `OBJECT_DEFINITION` statement.  
  
```sql  
USE AdventureWorks2012;  
GO  
SELECT OBJECT_DEFINITION (OBJECT_ID(N'Person.uAddress')) AS [Trigger Definition];   
GO  
```  
  
### B. Returning the source text of a system object  
 The following example returns the definition of the system stored procedure `sys.sp_columns`.  
  
```sql  
USE AdventureWorks2012;  
GO  
SELECT OBJECT_DEFINITION (OBJECT_ID(N'sys.sp_columns')) AS [Object Definition];  
GO  
```  
  
## See Also  
 [Metadata Functions &#40;Transact-SQL&#41;](../../t-sql/functions/metadata-functions-transact-sql.md)   
 [OBJECT_NAME &#40;Transact-SQL&#41;](../../t-sql/functions/object-name-transact-sql.md)   
 [OBJECT_ID &#40;Transact-SQL&#41;](../../t-sql/functions/object-id-transact-sql.md)   
 [sp_helptext &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helptext-transact-sql.md)   
 [sys.sql_modules &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-sql-modules-transact-sql.md)   
 [sys.server_sql_modules &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-sql-modules-transact-sql.md)  
  
  
