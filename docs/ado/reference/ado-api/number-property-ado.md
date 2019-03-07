---
title: "Number Property (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Error::Number"
  - "Error::GetNumber"
  - "Error::get_Number"
helpviewer_keywords: 
  - "number property [ADO]"
ms.assetid: f92323c5-dd11-4a63-a505-d9014a0f067f
author: MightyPen
ms.author: genemi
manager: craigg
---
# Number Property (ADO)
Indicates the number that uniquely identifies an [Error](../../../ado/reference/ado-api/error-object.md) object.  
  
## Return Value  
 Returns a **Long** value that may correspond to one of the [ErrorValueEnum](../../../ado/reference/ado-api/errorvalueenum.md) constants.  
  
## Remarks  
 Use the **Number** property to determine which error occurred. The value of the property is a unique number that corresponds to the error condition.  
  
 The [Errors](../../../ado/reference/ado-api/errors-collection-ado.md) collection returns an HRESULT in either hexadecimal format (for example, 0x80004005) or long value (for example, 2147467259). These HRESULTs can be raised by underlying components, such as OLE DB or even OLE itself. For more information about these numbers, see [Errors (OLE DB)](https://msdn.microsoft.com/ed74e62d-4948-4eeb-a7c9-fd7ad46af7fd) in the [OLE DB Programmer's Reference](https://msdn.microsoft.com/3c5e2dd5-35e5-4a93-ac3a-3818bb43bbf8)*.*  
  
## Applies To  
 [Error Object](../../../ado/reference/ado-api/error-object.md)  
  
## See Also  
 [Description, HelpContext, HelpFile, NativeError, Number, Source, and SQLState Properties Example (VB)](../../../ado/reference/ado-api/description-helpcontext-helpfile-nativeerror-number-source-example-vb.md)   
 [Description, HelpContext, HelpFile, NativeError, Number, Source, and SQLState Properties Example (VC++)](../../../ado/reference/ado-api/description-helpcontext-helpfile-nativeerror-number-source-example-vc.md)   
 [Description Property](../../../ado/reference/ado-api/description-property.md)   
 [HelpContext, HelpFile Properties](../../../ado/reference/ado-api/helpcontext-helpfile-properties.md)   
 [Source Property (ADO Error)](../../../ado/reference/ado-api/source-property-ado-error.md)
