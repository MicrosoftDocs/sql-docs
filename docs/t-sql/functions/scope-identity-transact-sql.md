---
title: "SCOPE_IDENTITY (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/06/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "SCOPE_IDENTITY"
  - "SCOPE_IDENTITY_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "identity columns [SQL Server], SCOPE_IDENTITY function"
  - "SCOPE_IDENTITY function"
  - "last-inserted identity values"
  - "identity values [SQL Server], last-inserted"
ms.assetid: eef24670-059b-4f10-91d4-a67bc1ed12ab
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# SCOPE_IDENTITY (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Returns the last identity value inserted into an identity column in the same scope. A scope is a module: a stored procedure, trigger, function, or batch. Therefore, if two statements are in the same stored procedure, function, or batch, they are in the same scope.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
SCOPE_IDENTITY()  
```  
  
## Return Types  
 **numeric(38,0)**  
  
## Remarks  
 SCOPE_IDENTITY, IDENT_CURRENT, and @@IDENTITY are similar functions because they return values that are inserted into identity columns.  
  
 IDENT_CURRENT is not limited by scope and session; it is limited to a specified table. IDENT_CURRENT returns the value generated for a specific table in any session and any scope. For more information, see [IDENT_CURRENT &#40;Transact-SQL&#41;](../../t-sql/functions/ident-current-transact-sql.md).  
  
 SCOPE_IDENTITY and @@IDENTITY return the last identity values that are generated in any table in the current session. However, SCOPE_IDENTITY returns values inserted only within the current scope; @@IDENTITY is not limited to a specific scope.  
  
 For example, there are two tables, T1 and T2, and an INSERT trigger is defined on T1. When a row is inserted to T1, the trigger fires and inserts a row in T2. This scenario illustrates two scopes: the insert on T1, and the insert on T2 by the trigger.  
  
 Assuming that both T1 and T2 have identity columns, @@IDENTITY and SCOPE_IDENTITY return different values at the end of an INSERT statement on T1. @@IDENTITY returns the last identity column value inserted across any scope in the current session. This is the value inserted in T2. SCOPE_IDENTITY() returns the IDENTITY value inserted in T1. This was the last insert that occurred in the same scope. The SCOPE_IDENTITY() function returns the null value if the function is invoked before any INSERT statements into an identity column occur in the scope.  
  
 Failed statements and transactions can change the current identity for a table and create gaps in the identity column values. The identity value is never rolled back even though the transaction that tried to insert the value into the table is not committed. For example, if an INSERT statement fails because of an IGNORE_DUP_KEY violation, the current identity value for the table is still incremented.  
  
## Examples  
  
### A. Using @@IDENTITY and SCOPE_IDENTITY with triggers  
 The following example creates two tables, `TZ` and `TY`, and an INSERT trigger on `TZ`. When a row is inserted to table `TZ`, the trigger (`Ztrig`) fires and inserts a row in `TY`.  
  
```sql  
USE tempdb;  
GO  
CREATE TABLE TZ (  
   Z_id  int IDENTITY(1,1)PRIMARY KEY,  
   Z_name varchar(20) NOT NULL);  
  
INSERT TZ  
   VALUES ('Lisa'),('Mike'),('Carla');  
  
SELECT * FROM TZ;  
```     
Result set: This is how table TZ looks.  
  
```  
Z_id   Z_name  
-------------  
1      Lisa  
2      Mike  
3      Carla  
```  
```sql 
CREATE TABLE TY (  
   Y_id  int IDENTITY(100,5)PRIMARY KEY,  
   Y_name varchar(20) NULL);  
  
INSERT TY (Y_name)  
   VALUES ('boathouse'), ('rocks'), ('elevator');  
  
SELECT * FROM TY;  
```   
Result set: This is how TY looks:  
```  
Y_id  Y_name  
---------------  
100   boathouse  
105   rocks  
110   elevator  
```  

Create the trigger that inserts a row in table TY when a row is inserted in table TZ.  
```sql  
CREATE TRIGGER Ztrig  
ON TZ  
FOR INSERT AS   
   BEGIN  
   INSERT TY VALUES ('')  
   END;  
```  
FIRE the trigger and determine what identity values you obtain with the @@IDENTITY and SCOPE_IDENTITY functions.   
```sql
INSERT TZ VALUES ('Rosalie');  
  
SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY];  
GO  
SELECT @@IDENTITY AS [@@IDENTITY];  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
```
/*SCOPE_IDENTITY returns the last identity value in the same scope. This was the insert on table TZ.*/`  
SCOPE_IDENTITY  
4  

/*@@IDENTITY returns the last identity value inserted to TY by the trigger. 
  This fired because of an earlier insert on TZ.*/
@@IDENTITY  
115  
```  
  
### B. Using @@IDENTITY and SCOPE_IDENTITY() with replication  
 The following examples show how to use `@@IDENTITY` and `SCOPE_IDENTITY()` for inserts in a database that is published for merge replication. Both tables in the examples are in the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] sample database: `Person.ContactType` is not published, and `Sales.Customer` is published. Merge replication adds triggers to tables that are published. Therefore, `@@IDENTITY` can return the value from the insert into a replication system table instead of the insert into a user table.  
  
 The `Person.ContactType` table has a maximum identity value of 20. If you insert a row into the table, `@@IDENTITY` and `SCOPE_IDENTITY()` return the same value.  
  
```sql  
USE AdventureWorks2012;  
GO  
INSERT INTO Person.ContactType ([Name]) VALUES ('Assistant to the Manager');  
GO  
SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY];  
GO  
SELECT @@IDENTITY AS [@@IDENTITY];  
GO  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
```  
SCOPE_IDENTITY  
21  
@@IDENTITY  
21
```  
  
 The `Sales.Customer` table has a maximum identity value of 29483. If you insert a row into the table, `@@IDENTITY` and `SCOPE_IDENTITY()` return different values. `SCOPE_IDENTITY()` returns the value from the insert into the user table, whereas `@@IDENTITY` returns the value from the insert into the replication system table. Use `SCOPE_IDENTITY()` for applications that require access to the inserted identity value.  
  
```sql  
INSERT INTO Sales.Customer ([TerritoryID],[PersonID]) VALUES (8,NULL);  
GO  
SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY];  
GO  
SELECT @@IDENTITY AS [@@IDENTITY];  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
 ```
 SCOPE_IDENTITY  
 29484  
 @@IDENTITY  
 89
 ```  
  
## See Also  
 [@@IDENTITY &#40;Transact-SQL&#41;](../../t-sql/functions/identity-transact-sql.md)  
  
  
