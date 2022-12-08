---
title: "NativeError Property (ADO)"
description: "NativeError Property (ADO)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Error::GetNativeError"
  - "Error::get_NativeError"
  - "Error::NativeError"
helpviewer_keywords:
  - "NativeError property [ADO]"
apitype: "COM"
---
# NativeError Property (ADO)
Indicates the provider-specific error code for a given [Error](./error-object.md) object.  
  
## Return Value  
 Returns a **Long** value that indicates the error code.  
  
## Remarks  
 Use the **NativeError** property to retrieve the database-specific error information for a particular **Error** object. For example, when using the Microsoft ODBC Provider for OLE DB with a Microsoft SQL Server database, native error codes that originate from SQL Server pass through ODBC and the ODBC Provider to the ADO **NativeError** property.  
  
## Applies To  
 [Error Object](./error-object.md)  
  
## See Also  
 [Description, HelpContext, HelpFile, NativeError, Number, Source, and SQLState Properties Example (VB)](./description-helpcontext-helpfile-nativeerror-number-source-example-vb.md)   
 [Description, HelpContext, HelpFile, NativeError, Number, Source, and SQLState Properties Example (VC++)](./description-helpcontext-helpfile-nativeerror-number-source-example-vc.md)