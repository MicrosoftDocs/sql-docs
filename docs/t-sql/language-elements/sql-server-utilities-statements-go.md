---
title: "SQL Server Utilities Statements - GO"
description: "SQL Server Utilities Statements - GO"
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: ""
ms.date: "07/27/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
f1_keywords:
  - "GO"
  - "GO_TSQL"
helpviewer_keywords:
  - "batches [SQL Server], ending"
  - "ending batches [SQL Server]"
  - "GO command"
dev_langs:
  - "TSQL"
---
# SQL Server Utilities Statements - GO
[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides commands that are not [!INCLUDE[tsql](../../includes/tsql-md.md)] statements, but are recognized by the **sqlcmd** and **osql** utilities and [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] Code Editor. These commands can be used to facilitate the readability and execution of batches and scripts.  
  
  GO signals the end of a batch of [!INCLUDE[tsql](../../includes/tsql-md.md)] statements to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] utilities.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql  
GO [count]  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *count*  
 Is a positive integer. The batch preceding GO will execute the specified number of times.  
  
## Remarks  
 GO is not a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement; it is a command recognized by the **sqlcmd** and **osql** utilities and [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] Code editor.  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] utilities interpret GO as a signal that they should send the current batch of [!INCLUDE[tsql](../../includes/tsql-md.md)] statements to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The current batch of statements is composed of all statements entered since the last GO, or since the start of the ad hoc session or script if this is the first GO.  
  
 A [!INCLUDE[tsql](../../includes/tsql-md.md)] statement cannot occupy the same line as a GO command. However, the line can contain comments.  
  
 Users must follow the rules for batches. For example, any execution of a stored procedure after the first statement in a batch must include the EXECUTE keyword. The scope of local (user-defined) variables is limited to a batch, and cannot be referenced after a GO command.  
  
```sql  
USE AdventureWorks2012;  
GO  
DECLARE @MyMsg VARCHAR(50)  
SELECT @MyMsg = 'Hello, World.'  
GO -- @MyMsg is not valid after this GO ends the batch.  
  
-- Yields an error because @MyMsg not declared in this batch.  
PRINT @MyMsg  
GO  
  
SELECT @@VERSION;  
-- Yields an error: Must be EXEC sp_who if not first statement in   
-- batch.  
sp_who  
GO  
```  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] applications can send multiple [!INCLUDE[tsql](../../includes/tsql-md.md)] statements to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] for execution as a batch. The statements in the batch are then compiled into a single execution plan. Programmers executing ad hoc statements in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] utilities, or building scripts of [!INCLUDE[tsql](../../includes/tsql-md.md)] statements to run through the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] utilities, use GO to signal the end of a batch.  
  
 Applications based on the ODBC or OLE DB APIs receive a syntax error if they try to execute a GO command. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] utilities never send a GO command to the server.  
  
 Do not use a semicolon as a statement terminator after GO.
 
```sql
-- Yields an error because ; is not permitted after GO  
SELECT @@VERSION;  
GO;  
```
  
## Permissions  
 GO is a utility command that requires no permissions. It can be executed by any user.    
  
## Examples  
 The following example creates two batches. The first batch contains only a `USE AdventureWorks2012` statement to set the database context. The remaining statements use a local variable. Therefore, all local variable declarations must be grouped in a single batch. This is done by not having a `GO` command until after the last statement that references the variable.  
  
```sql  
USE AdventureWorks2012;  
GO  
DECLARE @NmbrPeople INT  
SELECT @NmbrPeople = COUNT(*)  
FROM Person.Person;  
PRINT 'The number of people as of ' +  
      CAST(GETDATE() AS CHAR(20)) + ' is ' +  
      CAST(@NmbrPeople AS CHAR(10));  
GO  
```  
  
 The following example executes the statements in the batch twice.  
  
```sql  
SELECT DB_NAME();  
SELECT USER_NAME();  
GO 2  
```  
  
  
