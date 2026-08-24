---
title: "RETURN (Transact-SQL)"
description: "RETURN (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/16/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "RETURN_TSQL"
  - "RETURN"
helpviewer_keywords:
  - "unconditionally exiting program"
  - "stored procedures [SQL Server], exiting"
  - "batches [SQL Server], exiting"
  - "statement blocks [SQL Server]"
  - "queries [SQL Server], exiting"
  - "exiting queries [SQL Server]"
  - "exiting procedures [SQL Server]"
  - "RETURN statement"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# RETURN (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance Fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

  Exits unconditionally from a query or procedure. RETURN is immediate and complete and can be used at any point to exit from a procedure, batch, or statement block. Statements that follow RETURN are not executed.  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
RETURN [ integer_expression ]   
```  
  
## Arguments  
 *integer_expression*  
 Is the integer value that is returned. Stored procedures can return an integer value to a calling procedure or an application.  
  
## Return Types
 Optionally returns **int**.  
  
> [!NOTE]  
>  Unless documented otherwise, all system stored procedures return a value of 0. This indicates success and a nonzero value indicates failure.  
  
## Remarks  
 When used with a stored procedure, RETURN cannot return a null value. If a procedure tries to return a null value (for example, using RETURN @status when @status is NULL), a warning message is generated and a value of 0 is returned.  
  
 The return status value can be included in subsequent [!INCLUDE[tsql](../../includes/tsql-md.md)] statements in the batch or procedure that executed the current procedure, but it must be entered in the following form: `EXECUTE @return_status = <procedure_name>`.  
  
## Examples  
  
### A. Returning from a procedure  
 The following example shows if no user name is specified as a parameter when `findjobs` is executed, `RETURN` causes the procedure to exit after a message has been sent to the user's screen. If a user name is specified, the names of all objects created by this user in the current database are retrieved from the appropriate system tables.  
  
```sql  
CREATE PROCEDURE findjobs @nm sysname = NULL  
AS   
IF @nm IS NULL  
    BEGIN  
        PRINT 'You must give a user name'  
        RETURN  
    END  
ELSE  
    BEGIN  
        SELECT o.name, o.id, o.uid  
        FROM sysobjects o INNER JOIN master..syslogins l  
            ON o.uid = l.sid  
        WHERE l.name = @nm  
    END;  
```  
  
### B. Returning status codes  
 The following example checks the state for the ID of a specified contact. If the state is Washington (`WA`), a status of `1` is returned. Otherwise, `2` is returned for any other condition (a value other than `WA` for `StateProvince` or `ContactID` that did not match a row).  
  
```sql  
USE AdventureWorks2022;  
GO  
CREATE PROCEDURE checkstate @param VARCHAR(11)  
AS  
IF (SELECT StateProvince FROM Person.vAdditionalContactInfo WHERE ContactID = @param) = 'WA'  
    RETURN 1  
ELSE  
    RETURN 2;  
GO  
```  
  
 The following examples show the return status from executing `checkstate`. The first shows a contact in Washington; the second, contact not in Washington; and the third, a contact that is not valid. The `@return_status` local variable must be declared before it can be used.  
  
```sql  
DECLARE @return_status INT;  
EXEC @return_status = checkstate '2';  
SELECT 'Return Status' = @return_status;  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 ```
 Return Status 
  
 ------------- 
  
 1
 ```  
  
 Execute the query again, specifying a different contact number.  
  
```sql  
DECLARE @return_status INT;  
EXEC @return_status = checkstate '6';  
SELECT 'Return Status' = @return_status;  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 ```
 Return Status  
 -------------  
  
 2
 ```  
  
 Execute the query again, specifying another contact number.  
  
```sql  
DECLARE @return_status INT  
EXEC @return_status = checkstate '12345678901';  
SELECT 'Return Status' = @return_status;  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 ```
 Return Status  
 -------------  
  
 2
 ```  
  
## Related content

- [ALTER PROCEDURE (Transact-SQL)](../statements/alter-procedure-transact-sql.md)
- [CREATE PROCEDURE (Transact-SQL)](../statements/create-procedure-transact-sql.md)
- [DECLARE @local_variable (Transact-SQL)](declare-local-variable-transact-sql.md)
- [EXECUTE (Transact-SQL)](execute-transact-sql.md)
- [SET @local_variable (Transact-SQL)](set-local-variable-transact-sql.md)
- [THROW (Transact-SQL)](throw-transact-sql.md)
