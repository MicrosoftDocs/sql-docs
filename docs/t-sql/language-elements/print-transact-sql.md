---
title: "PRINT (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "PRINT_TSQL"
  - "PRINT"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "PRINT statement"
  - "user-defined messages [SQL Server]"
  - "messages [SQL Server], PRINT statement"
  - "displaying user-defined messages"
  - "viewing user-defined messages"
  - "conditionally returning messages [SQL Server]"
ms.assetid: 32ba0729-c4b5-4cfb-a5aa-e8b9402be028
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---

# PRINT (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Returns a user-defined message to the client.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
PRINT msg_str | @local_variable | string_expr  
```  
  
## Arguments  
 *msg_str*  
 Is a character string or Unicode string constant. For more information, see [Constants &#40;Transact-SQL&#41;](../../t-sql/data-types/constants-transact-sql.md).  
  
 **@** *local_variable*  
 Is a variable of any valid character data type. **@**_local\_variable_ must be **char**, **nchar**, **varchar**, or **nvarchar**, or it must be able to be implicitly converted to those data types.  
  
 *string_expr*  
 Is an expression that returns a string. Can include concatenated literal values, functions, and variables. For more information, see [Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md).  
  
## Remarks  
 A message string can be up to 8,000 characters long if it is a non-Unicode string, and 4,000 characters long if it is a Unicode string. Longer strings are truncated. The **varchar(max)** and **nvarchar(max)** data types are truncated to data types that are no larger than **varchar(8000)** and **nvarchar(4000)**.  
  
 RAISERROR can also be used to return messages. RAISERROR has these advantages over PRINT:  
  
-   RAISERROR supports substituting arguments into an error message string using a mechanism modeled on the printf function of the C language standard library.  
  
-   RAISERROR can specify a unique error number, a severity, and a state code in addition to the text message.  
  
-   RAISERROR can be used to return user-defined messages created using the sp_addmessage system stored procedure.  
  
## Examples  
  
### A. Conditionally executing print (IF EXISTS)  
 The following example uses the `PRINT` statement to conditionally return a message.  
  
```  
IF @@OPTIONS & 512 <> 0  
    PRINT N'This user has SET NOCOUNT turned ON.';  
ELSE  
    PRINT N'This user has SET NOCOUNT turned OFF.';  
GO  
```  
  
### B. Building and displaying a string  
 The following example converts the results of the `GETDATE` function to a `nvarchar` data type and concatenates it with literal text to be returned by `PRINT`.  
  
```  
-- Build the message text by concatenating  
-- strings and expressions.  
PRINT N'This message was printed on '  
    + RTRIM(CAST(GETDATE() AS nvarchar(30)))  
    + N'.';  
GO  
-- This example shows building the message text  
-- in a variable and then passing it to PRINT.  
-- This was required in SQL Server 7.0 or earlier.  
DECLARE @PrintMessage nvarchar(50);  
SET @PrintMessage = N'This message was printed on '  
    + RTRIM(CAST(GETDATE() AS nvarchar(30)))  
    + N'.';  
PRINT @PrintMessage;  
GO  
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
### C. Conditionally executing print  
 The following example uses the `PRINT` statement to conditionally return a message.  
  
```  
IF DB_ID() = 1  
    PRINT N'The current database is master.';  
ELSE  
    PRINT N'The current database is not master.';  
GO  
```  
  
## See Also  
 [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)   
 [DECLARE @local_variable &#40;Transact-SQL&#41;](../../t-sql/language-elements/declare-local-variable-transact-sql.md)   
 [RAISERROR &#40;Transact-SQL&#41;](../../t-sql/language-elements/raiserror-transact-sql.md)  
  
  

