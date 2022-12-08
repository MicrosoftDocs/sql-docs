---
title: "THROW (Transact-SQL)"
description: "THROW (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: ""
ms.date: "04/21/2022"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
f1_keywords:
  - "THROW_TSQL"
  - "THROW"
helpviewer_keywords:
  - "THROW statement"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# THROW (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Raises an exception and transfers execution to a CATCH block of a [TRY...CATCH](try-catch-transact-sql.md) construct.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
THROW [ { error_number | @local_variable },  
        { message | @local_variable },  
        { state | @local_variable } ]   
[ ; ]  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *error_number*  
 Is a constant or variable that represents the exception. *error_number* is **int** and must be greater than or equal to 50000 and less than or equal to 2147483647.  
  
 *message*  
 Is a string or variable that describes the exception. *message* is **nvarchar(2048)**.  
  
 *state*  
 Is a constant or variable between 0 and 255 that indicates the state to associate with the message. *state* is **tinyint**.  
  
## Remarks  

The statement before the THROW statement must be followed by the semicolon (;) statement terminator.  
  
If a TRY...CATCH construct is not available, the statement batch is terminated. The line number and procedure where the exception is raised are set. The severity is set to 16.  
  
If the THROW statement is specified without parameters, it must appear inside a CATCH block. This causes the caught exception to be raised. Any error that occurs in a THROW statement causes the statement batch to be terminated.  
  
% is a reserved character in the message text of a THROW statement and must be escaped. Double the % character to return % as part of the message text, for example 'The increase exceeded 15%% of the original value.'  
  
## Differences Between RAISERROR and THROW  

The following table lists differences between the [RAISERROR](raiserror-transact-sql.md) and THROW statements.  
  
|RAISERROR statement|THROW statement|  
|-------------------------|---------------------|  
|If a *msg_id* is passed to RAISERROR, the ID must be defined in sys.messages.|The *error_number* parameter does not have to be defined in sys.messages.|  
|The *msg_str* parameter can contain **printf** formatting styles.|The *message* parameter does not accept **printf** style formatting.|  
|The *severity* parameter specifies the severity of the exception.|There is no *severity* parameter. When THROW is used to initiate the exception, the severity is always set to 16. However, when THROW is used to re-throw an existing exception, the severity is set to that exception's severity level.|  
| Does not honor [SET XACT_ABORT](../statements/set-xact-abort-transact-sql.md). | Transactions _will_ be rolled back if [SET XACT_ABORT](../statements/set-xact-abort-transact-sql.md) is ON. |

  
## Examples  
  
### A. Use THROW to raise an exception  

The following example shows how to use the `THROW` statement to raise an exception.  
  
```sql  
THROW 51000, 'The record does not exist.', 1;  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```
Msg 51000, Level 16, State 1, Line 1  
 
The record does not exist.
```  
  
### B. Use THROW to raise an exception again  

The following example shows how to use the `THROW` statement to raise the last thrown exception again.  
  
```sql  
USE tempdb;  
GO  
CREATE TABLE dbo.TestRethrow  
(    ID INT PRIMARY KEY  
);  
BEGIN TRY  
    INSERT dbo.TestRethrow(ID) VALUES(1);  
--  Force error 2627, Violation of PRIMARY KEY constraint to be raised.  
    INSERT dbo.TestRethrow(ID) VALUES(1);  
END TRY  
BEGIN CATCH  
  
    PRINT 'In catch block.';  
    THROW;  
END CATCH;  
  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```
In catch block. 
Msg 2627, Level 14, State 1, Line 1  
Violation of PRIMARY KEY constraint 'PK__TestReth__3214EC272E3BD7D3'. Cannot insert duplicate key in object 'dbo.TestRethrow'.  
The statement has been terminated.
```  
  
### C. Use FORMATMESSAGE with THROW  

The following example shows how to use the `FORMATMESSAGE` function with `THROW` to throw a customized error message. The example first creates a user-defined error message by using `sp_addmessage`. Because the THROW statement does not allow for substitution parameters in the *message* parameter in the way that RAISERROR does, the FORMATMESSAGE function is used to pass the three parameter values expected by error message 60000.  
  
```sql  
EXEC sys.sp_addmessage  
     @msgnum   = 60000  
    ,@severity = 16  
    ,@msgtext  = N'This is a test message with one numeric parameter (%d), one string parameter (%s), and another string parameter (%s).'  
    ,@lang = 'us_english';   
GO  
  
DECLARE @msg NVARCHAR(2048) = FORMATMESSAGE(60000, 500, N'First string', N'second string');   
  
THROW 60000, @msg, 1;  
```  
  
[!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```
Msg 60000, Level 16, State 1, Line 2  
This is a test message with one numeric parameter (500), one string parameter (First string), and another string parameter (second string).
```  
  
## Next steps

Learn more about related concepts in the following articles:

- [RAISERROR &#40;Transact-SQL&#41;](../../t-sql/language-elements/raiserror-transact-sql.md)
- [FORMATMESSAGE &#40;Transact-SQL&#41;](../../t-sql/functions/formatmessage-transact-sql.md)
- [ERROR_MESSAGE &#40;Transact-SQL&#41;](../../t-sql/functions/error-message-transact-sql.md)
- [ERROR_NUMBER &#40;Transact-SQL&#41;](../../t-sql/functions/error-number-transact-sql.md)
- [@@ERROR &#40;Transact-SQL&#41;](../../t-sql/functions/error-transact-sql.md)
- [XACT_STATE &#40;Transact-SQL&#41;](../../t-sql/functions/xact-state-transact-sql.md)
- [SET XACT_ABORT &#40;Transact-SQL&#41;](../../t-sql/statements/set-xact-abort-transact-sql.md)
