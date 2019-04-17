---
title: "USER (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "USER"
  - "USER_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "usernames [SQL Server]"
  - "system-supplied usernames [SQL Server]"
  - "USER function"
  - "users [SQL Server], database names"
  - "names [SQL Server], database users"
  - "database usernames [SQL Server]"
ms.assetid: 82bbbd94-870c-4c43-9ed9-d9abc767a6be
author: MashaMSFT
ms.author: mathoma
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# USER (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Allows a system-supplied value for the database user name of the current user to be inserted into a table when no default value is specified.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
USER  
```  
  
## Return Types  
 **char**  
  
## Remarks  
 USER provides the same functionality as the USER_NAME system function.  
  
 Use USER with DEFAULT constraints in either the CREATE TABLE or ALTER TABLE statements, or use as any standard function.  
  
 USER always returns the name of the current context. When called after an EXECUTE AS statement, USER returns the name of the impersonated context.  
  
 If a Windows principal accesses the database by way of membership in a group, USER returns the name of the Windows principal instead of the name of the group.  
  
## Examples  
  
### A. Using USER to return the database user name  
 The following example declares a variable as `char`, assigns the current value of USER to it, and then prints the variable with a text description.  
  
```  
DECLARE @usr char(30)  
SET @usr = user  
SELECT 'The current user''s database username is: '+ @usr  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 ```
-----------------------------------------------------------------------  
The current user's database username is: dbo  
  
(1 row(s) affected)
```  
  
### B. Using USER with DEFAULT constraints  
 The following example creates a table by using `USER` as a `DEFAULT` constraint for the salesperson of a sales row.  
  
```  
USE AdventureWorks2012;  
GO  
CREATE TABLE inventory22  
(  
 part_id int IDENTITY(100, 1) NOT NULL,  
 description varchar(30) NOT NULL,  
 entry_person varchar(30) NOT NULL DEFAULT USER   
)  
GO  
INSERT inventory22 (description)  
VALUES ('Red pencil')  
INSERT inventory22 (description)  
VALUES ('Blue pencil')  
INSERT inventory22 (description)  
VALUES ('Green pencil')  
INSERT inventory22 (description)  
VALUES ('Black pencil')  
INSERT inventory22 (description)  
VALUES ('Yellow pencil')  
GO  
```  
  
 This is the query to select all information from the `inventory22` table:  
  
```  
SELECT * FROM inventory22 ORDER BY part_id;  
GO  
```  
  
 Here is the result set (note the `entry-person` value):  
  
 ```
part_id     description                    entry_person
----------- ------------------------------ -------------------------
100         Red pencil                     dbo
101         Blue pencil                    dbo
102         Green pencil                   dbo
103         Black pencil                   dbo
104         Yellow pencil                  dbo
  
(5 row(s) affected)
```  
  
### C. Using USER in combination with EXECUTE AS  
 The following example illustrates the behavior of `USER` when called inside an impersonated session.  
  
```  
SELECT USER;  
GO  
EXECUTE AS USER = 'Mario';  
GO  
SELECT USER;  
GO  
REVERT;  
GO  
SELECT USER;  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 ```
DBO
Mario
DBO
```  
  
## See Also  
 [ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md)   
 [CREATE TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-transact-sql.md)   
 [CURRENT_TIMESTAMP &#40;Transact-SQL&#41;](../../t-sql/functions/current-timestamp-transact-sql.md)   
 [CURRENT_USER &#40;Transact-SQL&#41;](../../t-sql/functions/current-user-transact-sql.md)   
 [Security Functions &#40;Transact-SQL&#41;](../../t-sql/functions/security-functions-transact-sql.md)   
 [SESSION_USER &#40;Transact-SQL&#41;](../../t-sql/functions/session-user-transact-sql.md)   
 [SYSTEM_USER &#40;Transact-SQL&#41;](../../t-sql/functions/system-user-transact-sql.md)   
 [USER_NAME &#40;Transact-SQL&#41;](../../t-sql/functions/user-name-transact-sql.md)  
  
  

