---
title: "Understanding Database Engine Errors | Microsoft Docs"
description: Learn about the attributes of errors raised by the SQL Server Database Engine and how to access all system and user-defined error messages from sys.messages.
ms.custom: ""
ms.date: "03/16/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "errors [SQL Server], about errors"
  - "errors [SQL Server], Database Engine"
  - "errors [SQL Server]"
  - "Database Engine [SQL Server], errors"
ms.assetid: ddaca9d3-956f-46a5-8cd3-a7a15ec75878
author: MashaMSFT
ms.author: mathoma
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Understanding Database Engine Errors
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]
  Errors raised by the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] have the attributes described in the following table.  
  
|Attribute|Description|  
|---------------|-----------------|  
|Error number|Each error message has a unique error number.|  
|Error message string|The error message contains diagnostic information about the cause of the error. Many error messages have substitution variables in which information, such as the name of the object generating the error, is inserted.|  
|Severity|The severity indicates how serious the error is. Errors that have a low severity, such as 1 or 2, are information messages or low-level warnings. Errors that have a high severity indicate problems that should be addressed as soon as possible. For more information about severities, see [Database Engine Error Severities](../../relational-databases/errors-events/database-engine-error-severities.md).|  
|State|Some error messages can be raised at multiple points in the code for the [!INCLUDE[ssDE](../../includes/ssde-md.md)]. For example, an 1105 error can be raised for several different conditions. Each specific condition that raises an error assigns a unique state code.<br /><br /> When you are viewing databases that contain information about known issues, such as the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Knowledge Base, you can use the state number to determine whether the recorded issue is the same as the error you have encountered. For example, if a Knowledge Base Article describes an 1105 error that has a state of 2 and the 1105 error message you received had a state of 3, the error probably has a different cause than the one reported in the article.<br /><br /> A [!INCLUDE[msCoName](../../includes/msconame-md.md)] support engineer can also use the state code from an error to find the location in the source code where that error code is being raised. This information might provide additional ideas on how to diagnose the problem.|  
|Procedure name|Is the name of the stored procedure or trigger in which the error has occurred.|  
|Line number|Indicates which statement in a batch, stored procedure, trigger, or function generated the error.|  
  
 All system and user-defined error messages in an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] are contained in the **sys.messages** catalog view. You can use the RAISERROR statement to return user-defined errors to an application.  
  
 All database APIs, such as the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] **SQLClient** namespace, ActiveX Data Objects (ADO), OLE DB, and Open Database Connectivity (ODBC), report the basic error attributes. This information includes the error number and message string. However, not all the APIs report all the other error attributes.  
  
 Information about an error that occurs in the scope of the TRY block of a TRY...CATCH construct can be obtained in [!INCLUDE[tsql](../../includes/tsql-md.md)] code by using functions such as ERROR_LINE, ERROR_MESSAGE, ERROR_NUMBER, ERROR_PROCEDURE, ERROR_SEVERITY, and ERROR_STATE in the scope of the associated CATCH block. For more information, see [TRY...CATCH &#40;Transact-SQL&#41;](../../t-sql/language-elements/try-catch-transact-sql.md).  
  
## Examples  
 The following example queries the `sys.messages` catalog view to return a list of all system and user-defined error messages in the [!INCLUDE[ssDE](../../includes/ssde-md.md)] that have English text (`1033`).  
  
```  
SELECT  
    message_id,  
    language_id,  
    severity,  
    is_event_logged,  
    text  
  FROM sys.messages  
  WHERE language_id = 1033;  
```  
  
 For more information, see [sys.messages &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/messages-for-errors-catalog-views-sys-messages.md).  
  
## See Also  
 [sys.messages &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/messages-for-errors-catalog-views-sys-messages.md)   
 [RAISERROR &#40;Transact-SQL&#41;](../../t-sql/language-elements/raiserror-transact-sql.md)   
 [@@ERROR &#40;Transact-SQL&#41;](../../t-sql/functions/error-transact-sql.md)   
 [TRY...CATCH &#40;Transact-SQL&#41;](../../t-sql/language-elements/try-catch-transact-sql.md)   
 [ERROR_LINE &#40;Transact-SQL&#41;](../../t-sql/functions/error-line-transact-sql.md)   
 [ERROR_MESSAGE &#40;Transact-SQL&#41;](../../t-sql/functions/error-message-transact-sql.md)   
 [ERROR_NUMBER &#40;Transact-SQL&#41;](../../t-sql/functions/error-number-transact-sql.md)   
 [ERROR_PROCEDURE &#40;Transact-SQL&#41;](../../t-sql/functions/error-procedure-transact-sql.md)   
 [ERROR_SEVERITY &#40;Transact-SQL&#41;](../../t-sql/functions/error-severity-transact-sql.md)   
 [ERROR_STATE &#40;Transact-SQL&#41;](../../t-sql/functions/error-state-transact-sql.md)  
  
  
