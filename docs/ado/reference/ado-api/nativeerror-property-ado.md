---
description: "NativeError Property (ADO)"
title: "NativeError Property (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: ado
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Error::GetNativeError"
  - "Error::get_NativeError"
  - "Error::NativeError"
helpviewer_keywords: 
  - "NativeError property [ADO]"
ms.assetid: b9b47e57-18a4-4186-aef5-5bd18d7b1d74
author: rothja
ms.author: jroth
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