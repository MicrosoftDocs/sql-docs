---
title: "IDENT_CURRENT (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "IDENT_CURRENT"
  - "IDENT_CURRENT_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "last identity value generated for table"
  - "identity values [SQL Server], last generated"
  - "identity columns, current value"
  - "IDENT_CURRENT function"
ms.assetid: 21517ced-39f5-4cd8-8d9c-0a0b8aff554a
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# IDENT_CURRENT (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Returns the last identity value generated for a specified table or view. The last identity value generated can be for any session and any scope.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
IDENT_CURRENT( 'table_name' )  
```  
  
## Arguments  
 *table_name*  
 Is the name of the table whose identity value is returned. *table_name* is **varchar**, with no default.  
  
## Return Types  
 **numeric(38,0)**  
  
## Exceptions  
 Returns NULL on error or if a caller does not have permission to view the object.  
  
 In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], a user can only view the metadata of securables that the user owns or on which the user has been granted permission. This means that metadata-emitting, built-in functions such as IDENT_CURRENT may return NULL if the user does not have any permission on the object. For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## Remarks  
 IDENT_CURRENT is similar to the [!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)] identity functions SCOPE_IDENTITY and @@IDENTITY. All three functions return last-generated identity values. However, the scope and session on which *last* is defined in each of these functions differ:  
  
-   IDENT_CURRENT returns the last identity value generated for a specific table in any session and any scope.  
  
-   @@IDENTITY returns the last identity value generated for any table in the current session, across all scopes.  
  
-   SCOPE_IDENTITY returns the last identity value generated for any table in the current session and the current scope.  
  
 When the IDENT_CURRENT value is NULL (because the table has never contained rows or has been truncated), the IDENT_CURRENT function returns the seed value.  
  
 Failed statements and transactions can change the current identity for a table and create gaps in the identity column values. The identity value is never rolled back even though the transaction that tried to insert the value into the table is not committed. For example, if an INSERT statement fails because of an IGNORE_DUP_KEY violation, the current identity value for the table is still incremented.  
  
 Be cautious about using IDENT_CURRENT to predict the next generated identity value. The actual generated value may be different from IDENT_CURRENT plus IDENT_INCR because of insertions performed by other sessions.  
  
## Examples  
  
### A. Returning the last identity value generated for a specified table  
 The following example returns the last identity value generated for the `Person.Address` table in the `AdventureWorks2012` database.  
  
```  
USE AdventureWorks2012;  
GO  
SELECT IDENT_CURRENT ('Person.Address') AS Current_Identity;  
GO  
```  
  
### B. Comparing identity values returned by IDENT_CURRENT, @@IDENTITY and SCOPE_IDENTITY  
 The following example shows the different identity values that are returned by `IDENT_CURRENT`, `@@IDENTITY`, and `SCOPE_IDENTITY`.  
  
```  
USE AdventureWorks2012;  
GO  
IF OBJECT_ID(N't6', N'U') IS NOT NULL   
    DROP TABLE t6;  
GO  
IF OBJECT_ID(N't7', N'U') IS NOT NULL   
    DROP TABLE t7;  
GO  
CREATE TABLE t6(id int IDENTITY);  
CREATE TABLE t7(id int IDENTITY(100,1));  
GO  
CREATE TRIGGER t6ins ON t6 FOR INSERT   
AS  
BEGIN  
   INSERT t7 DEFAULT VALUES  
END;  
GO  
--End of trigger definition  
  
SELECT id FROM t6;  
--IDs empty.  
  
SELECT id FROM t7;  
--ID is empty.  
  
--Do the following in Session 1  
INSERT t6 DEFAULT VALUES;  
SELECT @@IDENTITY;  
/*Returns the value 100. This was inserted by the trigger.*/  
  
SELECT SCOPE_IDENTITY();  
/* Returns the value 1. This was inserted by the   
INSERT statement two statements before this query.*/  
  
SELECT IDENT_CURRENT('t7');  
/* Returns value inserted into t7, that is in the trigger.*/  
  
SELECT IDENT_CURRENT('t6');  
/* Returns value inserted into t6. This was the INSERT statement four statements before this query.*/  
  
-- Do the following in Session 2.  
SELECT @@IDENTITY;  
/* Returns NULL because there has been no INSERT action   
up to this point in this session.*/  
  
SELECT SCOPE_IDENTITY();  
/* Returns NULL because there has been no INSERT action   
up to this point in this scope in this session.*/  
  
SELECT IDENT_CURRENT('t7');  
/* Returns the last value inserted into t7.*/  
```  
  
## See Also  
 [@@IDENTITY &#40;Transact-SQL&#41;](../../t-sql/functions/identity-transact-sql.md)   
 [SCOPE_IDENTITY &#40;Transact-SQL&#41;](../../t-sql/functions/scope-identity-transact-sql.md)   
 [IDENT_INCR &#40;Transact-SQL&#41;](../../t-sql/functions/ident-incr-transact-sql.md)   
 [IDENT_SEED &#40;Transact-SQL&#41;](../../t-sql/functions/ident-seed-transact-sql.md)   
 [Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md)   
 [System Functions &#40;Transact-SQL&#41;](../../relational-databases/system-functions/system-functions-for-transact-sql.md)  
  
  
