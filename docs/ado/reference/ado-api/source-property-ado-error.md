---
title: "Source Property (ADO Error)"
description: "Source Property (ADO Error)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Error::get_Source"
  - "Error::Source"
  - "Error::GetSource"
helpviewer_keywords:
  - "Source property [ADO Error]"
apitype: "COM"
---
# Source Property (ADO Error)
Indicates the name of the object or application that originally generated an error.  
  
## Return Value  
 Returns a **String** value that indicates the name of an object or application.  
  
## Remarks  
 Use the **Source** property on an [Error](./error-object.md) object to determine the name of the object or application that originally generated an error. This could be the object's class name or programmatic ID. For errors in ADO, the property value will be **ADODB.**_ObjectName_, where *ObjectName* is the name of the object that triggered the error. For ADOX and ADO MD, the value will be **ADOX.**_ObjectName_ and **ADOMD.**_ObjectName_, respectively.  
  
 Based on the error documentation from the **Source**, [Number](./number-property-ado.md), and [Description](./description-property.md) properties of **Error** objects, you can write code that will handle the error appropriately.  
  
 The **Source** property is read-only for **Error** objects.  
  
## Applies To  
 [Error Object](./error-object.md)  
  
## See Also  
 [Description, HelpContext, HelpFile, NativeError, Number, Source, and SQLState Properties Example (VB)](./description-helpcontext-helpfile-nativeerror-number-source-example-vb.md)   
 [Description, HelpContext, HelpFile, NativeError, Number, Source, and SQLState Properties Example (VC++)](./description-helpcontext-helpfile-nativeerror-number-source-example-vc.md)   
 [Description Property](./description-property.md)   
 [HelpContext, HelpFile Properties](./helpcontext-helpfile-properties.md)   
 [Number Property (ADO)](./number-property-ado.md)   
 [Source Property (ADO Record)](./source-property-ado-record.md)   
 [Source Property (ADO Recordset)](./source-property-ado-recordset.md)