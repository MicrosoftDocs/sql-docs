---
title: "SQLError (Visual FoxPro ODBC Driver) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLError function [ODBC], Visual FoxPro ODBC Driver"
ms.assetid: 8315ec16-1c22-447a-a577-39bd94f61070
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLError (Visual FoxPro ODBC Driver)
> [!NOTE]  
>  This topic contains Visual FoxPro ODBC Driver-specific information. For general information about this function, see the appropriate topic under [ODBC API Reference](../../odbc/reference/syntax/odbc-api-reference.md).  
  
 Support: Full  
  
 ODBC API Conformance: Core Level  
  
 Returns error or status information about the last error. The driver maintains a stack or list of errors that can be returned for the *hstmt*, *hdbc*, and *henv* arguments, depending on how the call to **SQLError** is made. The error queue is flushed after each statement.  
  
 The following table describes the **SQLError** arguments and return values used by the driver.  
  
|SQLError argument|Return value description|  
|-----------------------|------------------------------|  
|*szSQLState*|The value for the SQLSTATE represented by the error.|  
|*pfNativeError*|A nonzero value indicates a [Visual FoxPro ODBC Driver Native Error Message](../../odbc/microsoft/visual-foxpro-odbc-driver-native-error-messages.md). A value of zero indicates the error has been detected by the driver and mapped to the appropriate [ODBC Error Code](../../odbc/microsoft/odbc-error-codes-visual-foxpro-odbc-driver.md).|  
|*szErrorMsg*|The text for the native error or ODBC error.|  
|*pcbErrorMsg*|The length of the message text plus the length of the identifiers.|  
  
 For more information on driver error messages, see [Error Messages Overview](../../odbc/microsoft/error-messages-visual-foxpro-odbc-driver.md). For more information about this function, see [SQLError](../../odbc/reference/syntax/sqlerror-function.md) in the *ODBC Programmer's Reference*.
