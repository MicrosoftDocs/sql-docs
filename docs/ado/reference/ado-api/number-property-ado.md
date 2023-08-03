---
title: "Number Property (ADO)"
description: "Number Property (ADO)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Error::Number"
  - "Error::GetNumber"
  - "Error::get_Number"
helpviewer_keywords:
  - "number property [ADO]"
apitype: "COM"
---
# Number Property (ADO)
Indicates the number that uniquely identifies an [Error](./error-object.md) object.  
  
## Return Value  
 Returns a **Long** value that may correspond to one of the [ErrorValueEnum](./errorvalueenum.md) constants.  
  
## Remarks  
 Use the **Number** property to determine which error occurred. The value of the property is a unique number that corresponds to the error condition.  
  
 The [Errors](./errors-collection-ado.md) collection returns an HRESULT in either hexadecimal format (for example, 0x80004005) or long value (for example, 2147467259). These HRESULTs can be raised by underlying components, such as OLE DB or even OLE itself. For more information about these numbers, see [Errors (OLE DB)](/previous-versions/windows/desktop/ms724533(v=vs.85)) in the [OLE DB Programmer's Reference](/previous-versions/windows/desktop/ms713643(v=vs.85))*.*  
  
## Applies To  
 [Error Object](./error-object.md)  
  
## See Also  
 [Description, HelpContext, HelpFile, NativeError, Number, Source, and SQLState Properties Example (VB)](./description-helpcontext-helpfile-nativeerror-number-source-example-vb.md)   
 [Description, HelpContext, HelpFile, NativeError, Number, Source, and SQLState Properties Example (VC++)](./description-helpcontext-helpfile-nativeerror-number-source-example-vc.md)   
 [Description Property](./description-property.md)   
 [HelpContext, HelpFile Properties](./helpcontext-helpfile-properties.md)   
 [Source Property (ADO Error)](./source-property-ado-error.md)