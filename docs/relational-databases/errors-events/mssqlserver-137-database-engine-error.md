---
description: "MSSQLSERVER_137"
title: "MSSQLSERVER_137 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "137 (Database Engine error)"
ms.assetid: 47fb4212-2165-4fec-bc41-6d548465d7be
author: MashaMSFT
ms.author: mathoma
---
# MSSQLSERVER_137
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|137|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|P_SCALAR_VAR_NOTFOUND|  
|Message Text|Must declare the scalar variable "%.*ls".|  
  
## Explanation  
This error occurs when a variable is used in a SQL script without first declaring the variable. The following example returns error 137 for both the SET and SELECT statements because **\@mycol** is not declared.  
  
```sql
SET @mycol = 'ContactName';  
  
SELECT @mycol; 
```
  
One of the more complicated causes of this error includes the use of a variable that is declared outside the EXECUTE statement. For example, the variable **\@mycol** specified in the SELECT statement is local to the SELECT statement; thus it is outside the EXECUTE statement.  
  
```sql
USE AdventureWorks2012;  
  
GO  
  
DECLARE @mycol nvarchar(20);  
  
SET @mycol = 'Name';  
  
EXECUTE ('SELECT @mycol FROM Production.Product;'); 
```
  
## User Action  
Verify that any variables used in a SQL script are declared before being used elsewhere in the script.  
  
Rewrite the script so that it does not reference variables in the EXECUTE statement that are declared outside of it. For example:  
  
```sql
USE AdventureWorks2012;  
  
GO  
  
DECLARE @mycol nvarchar(20) ;  
  
SET @mycol = 'Name';  
  
EXECUTE ('SELECT ' + @mycol + ' FROM Production.Product;') ;
```
  
## See Also  
[EXECUTE &#40;Transact-SQL&#41;](~/t-sql/language-elements/execute-transact-sql.md)  
[SET Statements &#40;Transact-SQL&#41;](~/t-sql/statements/set-statements-transact-sql.md)  
[DECLARE @local_variable &#40;Transact-SQL&#41;](~/t-sql/language-elements/declare-local-variable-transact-sql.md)  
  
