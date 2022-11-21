---
description: "LocalDBFormatMessage Function"
title: "LocalDBFormatMessage Function | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: 
ms.topic: "reference"
apiname: 
  - "LocalDBFormatMessage"
apilocation: 
  - "sqluserinstance.dll"
apitype: "DLLExport"
ms.assetid: 31b3152a-94cf-4f75-a31b-296d7dd16dbe
author: markingmyname
ms.author: maghan
---
# LocalDBFormatMessage Function
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  Returns the localized textual description for the specified SQL Server Express LocalDB error.  
  
 **Header file:** msoledbsql.h  
  
## Syntax  
  
```  
HRESULT LocalDBFormatMessage(  
           HRESULT hrLocalDB,  
           DWORD dwFlags,   
           DWORD dwLanguageId,   
           LPWSTR wszMessage,   
           LPDWORD lpcchMessage   
);  
```  
  
## Parameters  
 *hrLocalDB*  
 [Input] The LocalDB error code.  
  
 *dwFlags*  
 [Input] The flags specifying the behavior of this function.  
  
 Available flags:  
  
 LOCALDB_TRUNCATE_ERR_MESSAGE  
 If the input buffer is too short, the error message will be truncated to fit the buffer.  
  
 *dwLanguageId*  
 [Input] The language desired (LANGID) or 0, in which case the Win32 FormatMessage language order is used.  
  
 *wszMessage*  
 [Output] The buffer to store the LocalDB error message.  
  
 *lpcchMessage*  
 [Input/Output] On input contains the size of the *wszMessage* buffer in characters. On output, if the given buffer size is too small, contains the buffer size required in characters, including any trailing nulls. If the function succeeds, contains the number of characters in the message, excluding any trailing nulls.  
  
## Returns  
 S_OK  
 The function succeeded.  
  
 [LOCALDB_ERROR_NOT_INSTALLED](../../relational-databases/express-localdb-error-messages/localdb-error-not-installed.md)  
 SQL Server Express LocalDB is not installed on the computer.  
  
 [LOCALDB_ERROR_INVALID_PARAMETER](../../relational-databases/express-localdb-error-messages/localdb-error-invalid-parameter.md)  
 One or more specified input parameters are invalid.  
  
 [LOCALDB_ERROR_UNKNOWN_ERROR_CODE](../../relational-databases/express-localdb-error-messages/localdb-error-unknown-error-code.md)  
 The requested message does not exist.  
  
 [LOCALDB_ERROR_UNKNOWN_LANGUAGE_ID](../../relational-databases/express-localdb-error-messages/localdb-error-unknown-language-id.md)  
 The message is not available in the requested language.  
  
 [LOCALDB_ERROR_INSUFFICIENT_BUFFER](../../relational-databases/express-localdb-error-messages/localdb-error-insufficient-buffer.md)  
 The input buffer *wszMessage* is too short, and truncation is not requested.  
  
 [LOCALDB_ERROR_INTERNAL_ERROR](../../relational-databases/express-localdb-error-messages/localdb-error-internal-error.md)  
 An unexpected error occurred. See the event log for details.  
  
## Remarks  
 For a code sample that uses LocalDB API, see [SQL Server Express LocalDB Reference](../../relational-databases/sql-server-express-localdb-reference.md).  
  
## See Also  
 [SQL Server Express LocalDB Header and Version Information](../../relational-databases/express-localdb-instance-apis/sql-server-express-localdb-header-and-version-information.md)  
  
  
