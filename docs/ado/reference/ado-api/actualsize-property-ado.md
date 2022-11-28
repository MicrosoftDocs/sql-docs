---
title: "ActualSize Property (ADO)"
description: "ActualSize Property (ADO)"
author: rothja
ms.author: jroth
ms.date: "03/20/2018"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Field20::ActualSize"
helpviewer_keywords:
  - "ActualSize property [ADO]"
apitype: "COM"
---
# ActualSize Property (ADO)
Indicates the actual length of a field's value in bytes.  
  
## Settings and Return Values  
 Returns a **Long** value.  
  
## Remarks  
 Use the **ActualSize** property to return the actual length of a [Field](./field-object.md) object's value. For all fields, the **ActualSize** property is read-only. If ADO cannot determine the length of the **Field** object's value, the **ActualSize** property returns **adUnknown**.  
  
 The **ActualSize** and [DefinedSize](./definedsize-property.md) properties are different, as shown in the following example. A **Field** object with a declared type of **adVarChar** and a maximum length of 50 characters returns a **DefinedSize** property value of 50, but the **ActualSize** property value it returns is the length of the data stored in the field for the current record. **Fields** with a **DefinedSize** greater than 255 bytes are treated as variable length columns.  
  
## Applies To  
 [Field Object](./field-object.md)  
  
## See Also  
 [ActualSize and DefinedSize Properties Example (VB)](./actualsize-and-definedsize-properties-example-vb.md)   
 [ActualSize and DefinedSize Properties Example (VC++)](./actualsize-and-definedsize-properties-example-vc.md)   
 [DefinedSize Property](./definedsize-property.md)