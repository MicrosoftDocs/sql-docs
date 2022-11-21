---
title: "DefinedSize Property"
description: "DefinedSize Property"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Field20::DefinedSize"
helpviewer_keywords:
  - "DefinedSize property [ADO]"
apitype: "COM"
---
# DefinedSize Property
Indicates the data capacity of a [Field](../../../ado/reference/ado-api/field-object.md) object.  
  
## Return Value  
 Returns a **Long** value that reflects the defined size of a field, which depends on the data type of the field object; see [Type](../../../ado/reference/ado-api/type-property-ado.md) for more information. For a field that uses a fixed-length data type, the return value is the size of the data type in bytes. For a field that uses a variable-length data type, this is one of the following:  
  
1.  The maximum length of the field in characters (for **adVarChar** and **adVarWChar**) or in bytes (for **adVarBinary**, and **adVarNumeric**) if the field has a defined length. For example, **adVarChar(5)** field has a maximum length of 5.  
  
2.  The maximum length of the data type in characters (for **adChar** and **adWChar**) or in bytes (for **adBinary** and **adNumeric**) if the field does not have a defined length.  
  
3.  ~0 (bitwise, the value is not 0; all bits are set to 1) if neither the field nor the data type has a defined maximum length.  
  
4.  For data types that do not have a length, this is set to ~0 (bitwise, the value is not 0; all bits are set to 1).  
  
## Remarks  
 Use the **DefinedSize** property to determine the data capacity of a **Field** object.  
  
 The **DefinedSize** and [ActualSize](../../../ado/reference/ado-api/actualsize-property-ado.md) properties are different. For example, consider a **Field** object with a declared type of **adVarChar** and a **DefinedSize** property value of 50, containing a single character. The **ActualSize** property value it returns is the length in bytes of the single character.  
  
## Applies To  
 [Field Object](../../../ado/reference/ado-api/field-object.md)  
  
## See Also  
 [ActualSize and DefinedSize Properties Example (VB)](../../../ado/reference/ado-api/actualsize-and-definedsize-properties-example-vb.md)   
 [ActualSize and DefinedSize Properties Example (VC++)](../../../ado/reference/ado-api/actualsize-and-definedsize-properties-example-vc.md)   
 [ActualSize Property (ADO)](../../../ado/reference/ado-api/actualsize-property-ado.md)
