---
title: "SESSION_USER (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "SESSION_USER_TSQL"
  - "SESSION_USER"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "usernames [SQL Server]"
  - "current user names"
  - "sessions [SQL Server], user names"
  - "displaying user names"
  - "viewing user names"
  - "SESSION_USER function"
ms.assetid: 3dbe8532-31b6-4862-8b2a-e58b00b964de
author: MashaMSFT
ms.author: mathoma
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SESSION_USER (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  SESSION_USER returns the user name of the current context in the current database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
SESSION_USER  
```  
  
## Return Types  
 **nvarchar(128)**  
  
## Remarks  
 Use SESSION_USER with DEFAULT constraints in either the CREATE TABLE or ALTER TABLE statements, or use it as any standard function. SESSION_USER can be inserted into a table when no default value is specified. This function takes no arguments. SESSION_USER can be used in queries.  
  
 If SESSION_USER is called after a context switch, SESSION_USER will return the user name of the impersonated context.  
  
## Examples  
  
### A. Using SESSION_USER to return the user name of the current session  
 The following example declares a variable as `nchar`, assigns the current value of `SESSION_USER` to that variable, and then prints the variable with a text description.  
  
```  
DECLARE @session_usr nchar(30);  
SET @session_usr = SESSION_USER;  
SELECT 'This session''s current user is: '+ @session_usr;  
GO  
```  
  
 This is the result set when the session user is `Surya`:  
  
 ```
--------------------------------------------------------------
This session's current user is: Surya

(1 row(s) affected)
```  
  
### B. Using SESSION_USER with DEFAULT constraints  
 The following example creates a table that uses `SESSION_USER` as a `DEFAULT` constraint for the name of the person who records receipt of a shipment.  
  
```  
USE AdventureWorks2012;  
GO  
CREATE TABLE deliveries3  
(  
 order_id int IDENTITY(5000, 1) NOT NULL,  
 cust_id  int NOT NULL,  
 order_date smalldatetime NOT NULL DEFAULT GETDATE(),  
 delivery_date smalldatetime NOT NULL DEFAULT   
    DATEADD(dd, 10, GETDATE()),  
 received_shipment nchar(30) NOT NULL DEFAULT SESSION_USER  
);  
GO  
```  
  
 Records added to the table will be stamped with the user name of the current user. In this example, `Wanida`, `Sylvester`, and `Alejandro` verify receipt of shipments. This can be emulated by switching user context by using `EXECUTE AS`.  
  
```  
EXECUTE AS USER = 'Wanida'  
INSERT deliveries3 (cust_id)  
VALUES (7510);  
INSERT deliveries3 (cust_id)  
VALUES (7231);  
REVERT;  
EXECUTE AS USER = 'Sylvester'  
INSERT deliveries3 (cust_id)  
VALUES (7028);  
REVERT;  
EXECUTE AS USER = 'Alejandro'  
INSERT deliveries3 (cust_id)  
VALUES (7392);  
INSERT deliveries3 (cust_id)  
VALUES (7452);  
REVERT;  
GO  
```  
  
 The following query selects all information from the `deliveries3` table.  
  
```  
SELECT order_id AS 'Order #', cust_id AS 'Customer #',   
   delivery_date AS 'When Delivered', received_shipment   
   AS 'Received By'  
FROM deliveries3  
ORDER BY order_id;  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 ```
Order #   Customer #  When Delivered       Received By
--------  ----------  -------------------  -----------
5000      7510        2005-03-16 12:02:14  Wanida
5001      7231        2005-03-16 12:02:14  Wanida
5002      7028        2005-03-16 12:02:14  Sylvester
5003      7392        2005-03-16 12:02:14  Alejandro
5004      7452        2005-03-16 12:02:14  Alejandro

(5 row(s) affected)
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
### C: Using SESSION_USER to return the user name of the current session  
 The following example returns the session user for the current session.  
  
```  
SELECT SESSION_USER;  
```  
  
## See Also  
 [ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md)   
 [CREATE TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-transact-sql.md)   
 [CURRENT_TIMESTAMP &#40;Transact-SQL&#41;](../../t-sql/functions/current-timestamp-transact-sql.md)   
 [CURRENT_USER &#40;Transact-SQL&#41;](../../t-sql/functions/current-user-transact-sql.md)   
 [SYSTEM_USER &#40;Transact-SQL&#41;](../../t-sql/functions/system-user-transact-sql.md)   
 [System Functions &#40;Transact-SQL&#41;](../../relational-databases/system-functions/system-functions-for-transact-sql.md)   
 [USER &#40;Transact-SQL&#41;](../../t-sql/functions/user-transact-sql.md)   
 [USER_NAME &#40;Transact-SQL&#41;](../../t-sql/functions/user-name-transact-sql.md)  
  
  

