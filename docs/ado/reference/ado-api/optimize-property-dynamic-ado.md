---
title: "Optimize Property-Dynamic (ADO)"
description: "Optimize Property-Dynamic (ADO)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "Optimize property [ADO]"
apitype: "COM"
---
# Optimize Property-Dynamic (ADO)
Specifies whether an index should be created on a [field](./field-object.md).  
  
## Settings and Return Values  
 Sets or returns a **Boolean** value that indicates whether an index should be created.  
  
## Remarks  
 An index can improve the performance of operations that find or sort values in a [Recordset](./recordset-object-ado.md). The index is internal to ADO; you cannot explicitly access or use it in your application.  
  
 To create an index on a field, set the **Optimize** property to **True**. To delete the index, set this property to **False**.  
  
 **Optimize** is a dynamic property appended to the [Field](./field-object.md) object [Properties](./properties-collection-ado.md) collection when the [CursorLocation](./cursorlocation-property-ado.md) property is set to **adUseClient**.  
  
## Usage  
  
```  
Dim rs As New Recordset  
Dim fld As Field  
rs.CursorLocation = adUseClient      'Enable index creation  
rs.Fields.Append "Field1", adChar, 35, adFldIsNullable  
rs.Open  
Set fld = rs.Fields(0)  
fld.Properties("Optimize") = True    'Create an index  
fld.Properties("Optimize") = False   'Delete an index  
```  
  
## Applies To  
 [Field Object](./field-object.md)  
  
## See Also  
 [Optimize Property Example (VB)](./optimize-property-example-vb.md)   
 [Optimize Property Example (VC++)](./optimize-property-example-vc.md)   
 [Filter Property](./filter-property.md)   
 [Find Method (ADO)](./find-method-ado.md)   
 [Sort Property](./sort-property.md)