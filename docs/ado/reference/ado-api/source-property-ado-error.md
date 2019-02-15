---
title: "Source Property (ADO Error) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Error::get_Source"
  - "Error::Source"
  - "Error::GetSource"
helpviewer_keywords: 
  - "Source property [ADO Error]"
ms.assetid: 4044ba15-f013-4c4c-9fe1-b4410fe9a778
author: MightyPen
ms.author: genemi
manager: craigg
---
# Source Property (ADO Error)
Indicates the name of the object or application that originally generated an error.  
  
## Return Value  
 Returns a **String** value that indicates the name of an object or application.  
  
## Remarks  
 Use the **Source** property on an [Error](../../../ado/reference/ado-api/error-object.md) object to determine the name of the object or application that originally generated an error. This could be the object's class name or programmatic ID. For errors in ADO, the property value will be **ADODB.**_ObjectName_, where *ObjectName* is the name of the object that triggered the error. For ADOX and ADO MD, the value will be **ADOX.**_ObjectName_ and **ADOMD.**_ObjectName_, respectively.  
  
 Based on the error documentation from the **Source**, [Number](../../../ado/reference/ado-api/number-property-ado.md), and [Description](../../../ado/reference/ado-api/description-property.md) properties of **Error** objects, you can write code that will handle the error appropriately.  
  
 The **Source** property is read-only for **Error** objects.  
  
## Applies To  
 [Error Object](../../../ado/reference/ado-api/error-object.md)  
  
## See Also  
 [Description, HelpContext, HelpFile, NativeError, Number, Source, and SQLState Properties Example (VB)](../../../ado/reference/ado-api/description-helpcontext-helpfile-nativeerror-number-source-example-vb.md)   
 [Description, HelpContext, HelpFile, NativeError, Number, Source, and SQLState Properties Example (VC++)](../../../ado/reference/ado-api/description-helpcontext-helpfile-nativeerror-number-source-example-vc.md)   
 [Description Property](../../../ado/reference/ado-api/description-property.md)   
 [HelpContext, HelpFile Properties](../../../ado/reference/ado-api/helpcontext-helpfile-properties.md)   
 [Number Property (ADO)](../../../ado/reference/ado-api/number-property-ado.md)   
 [Source Property (ADO Record)](../../../ado/reference/ado-api/source-property-ado-record.md)   
 [Source Property (ADO Recordset)](../../../ado/reference/ado-api/source-property-ado-recordset.md)
