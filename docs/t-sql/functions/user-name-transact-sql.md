---
title: "USER_NAME (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "USER_NAME"
  - "USER_NAME_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "usernames [SQL Server]"
  - "IDs [SQL Server], databases"
  - "USER_NAME function"
  - "users [SQL Server], database username"
  - "names [SQL Server], database users"
  - "identification numbers [SQL Server], databases"
  - "database usernames [SQL Server]"
ms.assetid: ab32d644-4228-449a-9ef0-5a975c305775
author: VanMSFT
ms.author: vanto
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# USER_NAME (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Returns a database user name from a specified identification number.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
USER_NAME ( [ id ] )  
```  
  
## Arguments  
 *id*  
 Is the identification number associated with a database user. *id* is **int**. The parentheses are required.  
  
## Return Types  
 **nvarchar(256)**  
  
## Remarks  
 When *id* is omitted, the current user in the current context is assumed. If the parameter contains the word NULL will return NULL. When USER_NAME is called without specifying an *id* after an EXECUTE AS statement, USER_NAME returns the name of the impersonated user. If a Windows principal accesses the database by way of membership in a group, USER_NAME returns the name of the Windows principal instead of the group.  
  
## Examples  
  
### A. Using USER_NAME  
 The following example returns the user name for user ID `13`.  
  
```  
SELECT USER_NAME(13);  
GO  
```  
  
### B. Using USER_NAME without an ID  
 The following example finds the name of the current user without specifying an ID.  
  
```  
SELECT USER_NAME();  
GO  
```  
  
 Here is the result set for a user that is a member of the sysadmin fixed server role.  
  
 ```
------------------------------  
dbo  
  
(1 row(s) affected)
```  
  
### C. Using USER_NAME in the WHERE clause  
 The following example finds the row in `sysusers` in which the name is equal to the result of applying the system function `USER_NAME` to user identification number `1`.  
  
```  
SELECT name FROM sysusers WHERE name = USER_NAME(1);  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 ```
name  
------------------------------  
dbo  
  
(1 row(s) affected)
```  
  
### D. Calling USER_NAME during impersonation with EXECUTE AS  
 The following example shows how `USER_NAME` behaves during impersonation.  
  
```  
SELECT USER_NAME();  
GO  
EXECUTE AS USER = 'Zelig';  
GO  
SELECT USER_NAME();  
GO  
REVERT;  
GO  
SELECT USER_NAME();  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 ```
DBO  
Zelig  
DBO
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
### E. Using USER_NAME without an ID  
 The following example finds the name of the current user without specifying an ID.  
  
```  
SELECT USER_NAME();  
```  
  
 Here is the result set for a currently logged-in user.  
  
```  
------------------------------   
User7                              
```  
  
### F. Using USER_NAME in the WHERE clause  
 The following example finds the row in `sysusers` in which the name is equal to the result of applying the system function `USER_NAME` to user identification number `1`.  
  
```  
SELECT name FROM sysusers WHERE name = USER_NAME(1);  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
name                             
------------------------------   
User7                              
```  
  
## See Also  
 [ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md)   
 [CREATE TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-transact-sql.md)   
 [CURRENT_TIMESTAMP &#40;Transact-SQL&#41;](../../t-sql/functions/current-timestamp-transact-sql.md)   
 [CURRENT_USER &#40;Transact-SQL&#41;](../../t-sql/functions/current-user-transact-sql.md)   
 [SESSION_USER &#40;Transact-SQL&#41;](../../t-sql/functions/session-user-transact-sql.md)   
 [System Functions &#40;Transact-SQL&#41;](../../relational-databases/system-functions/system-functions-for-transact-sql.md)   
 [SYSTEM_USER &#40;Transact-SQL&#41;](../../t-sql/functions/system-user-transact-sql.md)  
  
  

