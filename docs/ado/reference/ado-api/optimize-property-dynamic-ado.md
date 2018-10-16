---
title: "Optimize Property-Dynamic (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
helpviewer_keywords: 
  - "Optimize property [ADO]"
ms.assetid: a491c4ce-2b04-4c84-be83-3846bde8d16b
author: MightyPen
ms.author: genemi
manager: craigg
---
# Optimize Property-Dynamic (ADO)
Specifies whether an index should be created on a [field](../../../ado/reference/ado-api/field-object.md).  
  
## Settings and Return Values  
 Sets or returns a **Boolean** value that indicates whether an index should be created.  
  
## Remarks  
 An index can improve the performance of operations that find or sort values in a [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md). The index is internal to ADO; you cannot explicitly access or use it in your application.  
  
 To create an index on a field, set the **Optimize** property to **True**. To delete the index, set this property to **False**.  
  
 **Optimize** is a dynamic property appended to the [Field](../../../ado/reference/ado-api/field-object.md) object [Properties](../../../ado/reference/ado-api/properties-collection-ado.md) collection when the [CursorLocation](../../../ado/reference/ado-api/cursorlocation-property-ado.md) property is set to **adUseClient**.  
  
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
 [Field Object](../../../ado/reference/ado-api/field-object.md)  
  
## See Also  
 [Optimize Property Example (VB)](../../../ado/reference/ado-api/optimize-property-example-vb.md)   
 [Optimize Property Example (VC++)](../../../ado/reference/ado-api/optimize-property-example-vc.md)   
 [Filter Property](../../../ado/reference/ado-api/filter-property.md)   
 [Find Method (ADO)](../../../ado/reference/ado-api/find-method-ado.md)   
 [Sort Property](../../../ado/reference/ado-api/sort-property.md)
