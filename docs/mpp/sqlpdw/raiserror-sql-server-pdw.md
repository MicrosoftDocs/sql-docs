---
title: "RAISERROR (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: f3af4ea8-9b4b-417d-89ed-a6a930af28df
caps.latest.revision: 10
author: BarbKess
---
# RAISERROR (SQL Server PDW)
Generates an error message and initiates error processing for the session. The error number will always be 50000. The message text is built dynamically. The message is returned as a server error message to the calling application or to an associated CATCH block of a TRY…CATCH construct. New applications should use [THROW](../../mpp/sqlpdw/throw-sql-server-pdw.md) instead.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
RAISERROR ( { msg_str | @local_variable }  
    { ,severity ,state }  
    [ ,argument [ ,...n ] ] )  
    [ WITH option [ ,...n ] ]  
```  
  
## Arguments  
*msg_str*  
Is a user-defined message with formatting similar to the **printf** function in the C standard library. The error message can have a maximum of 2,047 characters. If the message contains 2,048 or more characters, only the first 2,044 are displayed and an ellipsis is added to indicate that the message has been truncated. Note that substitution parameters consume more characters than the output shows because of internal storage behavior. For example, the substitution parameter of *%d* with an assigned value of 2 actually produces one character in the message string but also internally takes up three additional characters of storage. This storage requirement decreases the number of available characters for message output.  
  
RAISERROR raises an error message with an error number of 50000.  
  
*msg_str* is a string of characters with optional embedded conversion specifications. Each conversion specification defines how a value in the argument list is formatted and placed into a field at the location of the conversion specification in *msg_str*. Conversion specifications have this format:  
  
% [[*flag*] [*width*] [. *precision*] [{h | l}]] *type*  
  
The parameters that can be used in *msg_str* are:  
  
*flag*  
Is a code that determines the spacing and justification of the substituted value.  
  
|Code|Prefix or justification|Description|  
|--------|---------------------------|---------------|  
|- (minus)|Left-justified|Left-justify the argument value within the given field width.|  
|+ (plus)|Sign prefix|Preface the argument value with a plus (+) or minus (-) if the value is of a signed type.|  
|0 (zero)|Zero padding|Preface the output with zeros until the minimum width is reached. When 0 and the minus sign (-) appear, 0 is ignored.|  
|# (number)|0x prefix for hexadecimal type of x or X|When used with the o, x, or X format, the number sign (#) flag prefaces any nonzero value with 0, 0x, or 0X, respectively. When d, i, or u are prefaced by the number sign (#) flag, the flag is ignored.|  
|' ' (blank)|Space padding|Preface the output value with blank spaces if the value is signed and positive. This is ignored when included with the plus sign (+) flag.|  
  
*width*  
Is an integer that defines the minimum width for the field into which the argument value is placed. If the length of the argument value is equal to or longer than *width*, the value is printed with no padding. If the value is shorter than *width*, the value is padded to the length specified in *width*.  
  
An asterisk (*) means that the width is specified by the associated argument in the argument list, which must be an integer value.  
  
*precision*  
Is the maximum number of characters taken from the argument value for string values. For example, if a string has five characters and precision is 3, only the first three characters of the string value are used.  
  
For integer values, *precision* is the minimum number of digits printed.  
  
An asterisk (*) means that the precision is specified by the associated argument in the argument list, which must be an integer value.  
  
{h | l} *type*  
Is used with character types d, i, o, s, x, X, or u, and creates **shortint** (h) or **longint** (l) values.  
  
|Type specification|Represents|  
|----------------------|--------------|  
|d or i|Signed integer|  
|o|Unsigned octal|  
|s|String|  
|u|Unsigned integer|  
|x or X|Unsigned hexadecimal|  
  
> [!NOTE]  
> These type specifications are based on the ones originally defined for the **printf** function in the C standard library. The type specifications used in RAISERROR message strings map to Transact\-SQL data types, while the specifications used in **printf** map to C language data types. Type specifications used in **printf** are not supported by RAISERROR when Transact\-SQL does not have a data type similar to the associated C data type. For example, the *%p* specification for pointers is not supported in RAISERROR because Transact\-SQL does not have a pointer data type.  
  
> [!NOTE]  
> To convert a value to the Transact\-SQL**bigint** data type, specify **%I64d**.  
  
**@***local_variable*  
Is a variable of any valid character data type that contains a string formatted in the same manner as *msg_str*. **@***local_variable* must be **char** or **varchar**, or be able to be implicitly converted to these data types.  
  
*severity*  
Is the user-defined severity level associated with this message.  
  
Severity levels from 0 through 18 can be specified by any user. Severity levels less than 0 are interpreted as 0.  
  
*state*  
Is an integer from 0 through 255. Negative values produce the state 0. Values larger than 255 generate an error.  
  
*argument*  
Are the parameters used in the substitution for variables defined in *msg_str*. There can be 0 or more substitution parameters, but the total number of substitution parameters cannot exceed 20. Each substitution parameter can be a local variable or any of these data types: **tinyint**, **smallint**, **int**, **char**, **varchar**, **nchar**, **nvarchar**, **binary**, or **varbinary**. No other data types are supported.  
  
*option*  
Is a custom option for the error. Currently NOWAIT is the only option  
  
|Value|Description|  
|---------|---------------|  
|NOWAIT|Sends messages immediately to the client.|  
  
## Remarks  
The errors generated by RAISERROR operate the same as errors generated by the PDW engine. The values specified by RAISERROR are reported by the ERROR_MESSAGE, ERROR_NUMBER, ERROR_PROCEDURE, ERROR_SEVERITY, ERROR_STATE, and @@ERROR system functions. When RAISERROR is run with a severity of 11 or higher in a TRY block, it transfers control to the associated CATCH block. The error is returned to the caller if RAISERROR is run outside the scope of any TRY block.  
  
CATCH blocks can use RAISERROR to rethrow the error that invoked the CATCH block by using system functions such as ERROR_NUMBER and ERROR_MESSAGE to retrieve the original error information. @@ERROR is set to 0 by default for messages with a severity from 1 through 10.  
  
RAISERROR can be used as an alternative to PRINT to return messages to calling applications. RAISERROR supports character substitution similar to the functionality of the **printf** function in the C standard library, while the Transact\-SQL PRINT statement does not. The PRINT statement is not affected by TRY blocks, while a RAISERROR run with a severity of 11 to 19 in a TRY block transfers control to the associated CATCH block. Specify a severity of 10 or lower to use RAISERROR to return a message from a TRY block without invoking the CATCH block.  
  
Typically, successive arguments replace successive conversion specifications; the first argument replaces the first conversion specification, the second argument replaces the second conversion specification, and so on. For example, in the following `RAISERROR` statement, the first argument of `N'number'` replaces the first conversion specification of `%s`; and the second argument of `5` replaces the second conversion specification of `%d.`  
  
```  
RAISERROR (N'This is message %s %d.', -- Message text.  
           10, -- Severity,  
           1, -- State,  
           N'number', -- First argument.  
           5); -- Second argument.  
-- The message text returned is: This is message number 5.  
GO  
```  
  
If an asterisk (*) is specified for either the width or precision of a conversion specification, the value to be used for the width or precision is specified as an integer argument value. In this case, one conversion specification can use up to three arguments, one each for the width, precision, and substitution value.  
  
For example, both of the following `RAISERROR` statements return the same string. One specifies the width and precision values in the argument list; the other specifies them in the conversion specification.  
  
```  
RAISERROR (N'<<%*.*s>>', -- Message text.  
           10, -- Severity,  
           1, -- State,  
           7, -- First argument used for width.  
           3, -- Second argument used for precision.  
           N'abcde'); -- Third argument supplies the string.  
-- The message text returned is: <<    abc>>.  
GO  
RAISERROR (N'<<%7.3s>>', -- Message text.  
           10, -- Severity,  
           1, -- State,  
           N'abcde'); -- First argument supplies the string.  
-- The message text returned is: <<    abc>>.  
GO  
```  
  
## Examples  
  
### A. Returning error information from a CATCH block  
The following code example shows how to use `RAISERROR` inside a `TRY` block to cause execution to jump to the associated `CATCH` block. It also shows how to use `RAISERROR` to return information about the error that invoked the `CATCH` block.  
  
> [!NOTE]  
> RAISERROR only generates errors with state from 1 through 18. Because the PDW engine may raise errors with state 0, we recommend that you check the error state returned by ERROR_STATE before passing it as a value to the state parameter of RAISERROR.  
  
```  
BEGIN TRY  
    -- RAISERROR with severity 11-18 will cause execution to   
    -- jump to the CATCH block.  
    RAISERROR ('Error raised in TRY block.', -- Message text.  
               16, -- Severity.  
               1 -- State.  
               );  
END TRY  
BEGIN CATCH  
    DECLARE @ErrorMessage NVARCHAR(4000);  
    DECLARE @ErrorSeverity INT;  
    DECLARE @ErrorState INT;  
  
    SET @ErrorMessage = ERROR_MESSAGE();  
    SET @ErrorSeverity = ERROR_SEVERITY();  
    SET @ErrorState = ERROR_STATE();  
  
    -- Use RAISERROR inside the CATCH block to return error  
    -- information about the original error that caused  
    -- execution to jump to the CATCH block.  
    RAISERROR (@ErrorMessage, -- Message text.  
               @ErrorSeverity, -- Severity.  
               @ErrorState -- State.  
               );  
END CATCH;  
```  
  
### B. Using a local variable to supply the message text  
The following code example shows how to use a local variable to supply the message text for a `RAISERROR` statement.  
  
```  
DECLARE @StringVariable NVARCHAR(50);  
SET @StringVariable = N'<<%7.3s>>';  
  
RAISERROR (@StringVariable, -- Message text.  
           10, -- Severity,  
           1, -- State,  
           N'abcde'); -- First argument supplies the string.  
-- The message text returned is: <<    abc>>.  
GO  
```  
  
## See Also  
[PRINT &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/print-sql-server-pdw.md)  
[@@ERROR &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/error-sql-server-pdw.md)  
[TRY...CATCH &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/try-catch-sql-server-pdw.md)  
[ERROR_MESSAGE &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/error-message-sql-server-pdw.md)  
[ERROR_NUMBER &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/error-number-sql-server-pdw.md)  
[ERROR_PROCEDURE &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/error-procedure-sql-server-pdw.md)  
[ERROR_SEVERITY &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/error-severity-sql-server-pdw.md)  
[ERROR_STATE &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/error-state-sql-server-pdw.md)  
[THROW &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/throw-sql-server-pdw.md)  
  
