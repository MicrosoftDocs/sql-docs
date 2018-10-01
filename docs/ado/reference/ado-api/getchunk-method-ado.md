---
title: "GetChunk Method (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Field20::raw_GetChunk"
  - "Field20::GetChunk"
helpviewer_keywords: 
  - "GetChunk method [ADO]"
ms.assetid: fc268e22-205b-44a3-9038-ffed51e23e10
author: MightyPen
ms.author: genemi
manager: craigg
---
# GetChunk Method (ADO)
Returns all, or a portion, of the contents of a large text or binary data [Field](../../../ado/reference/ado-api/field-object.md) object.  
  
## Syntax  
  
```  
  
variable = field.GetChunk(Size)  
```  
  
## Return Value  
 Returns a **Variant**.  
  
#### Parameters  
 *Size*  
 A **Long** expression that is equal to the number of bytes or characters that you want to retrieve.  
  
## Remarks  
 Use the **GetChunk** method on a **Field** object to retrieve part or all of its long binary or character data. In situations where system memory is limited, you can use the **GetChunk** method to manipulate long values in portions, rather than in their entirety.  
  
 The data that a **GetChunk** call returns is assigned to *variable*. If *Size* is greater than the remaining data, the **GetChunk** method returns only the remaining data without padding *variable* with empty spaces. If the field is empty, the **GetChunk** method returns a null value.  
  
 Each subsequent **GetChunk** call retrieves data starting from where the previous **GetChunk** call left off. However, if you are retrieving data from one field and then you set or read the value of another field in the current record, ADO assumes you have finished retrieving data from the first field. If you call the **GetChunk** method on the first field again, ADO interprets the call as a new **GetChunk** operation and starts reading from the beginning of the data. Accessing fields in other [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) objects that are not clones of the first **Recordset** object will not disrupt **GetChunk** operations.  
  
 If the **adFldLong** bit in the [Attributes](../../../ado/reference/ado-api/attributes-property-ado.md) property of a **Field** object is set to **True**, you can use the **GetChunk** method for that field.  
  
 If there is no current record when you use the **GetChunk** method on a **Field** object, error 3021 (no current record) occurs.  
  
> [!NOTE]
>  The **GetChunk** method does not operate on **Field** objects of a [Record](../../../ado/reference/ado-api/record-object-ado.md) object. It does not perform any operation and will produce a run-time error.  
  
## Applies To  
 [Field Object](../../../ado/reference/ado-api/field-object.md)  
  
## See Also  
 [AppendChunk and GetChunk Methods Example (VB)](../../../ado/reference/ado-api/appendchunk-and-getchunk-methods-example-vb.md)   
 [AppendChunk and GetChunk Methods Example (VC++)](../../../ado/reference/ado-api/appendchunk-and-getchunk-methods-example-vc.md)   
 [AppendChunk Method (ADO)](../../../ado/reference/ado-api/appendchunk-method-ado.md)   
 [Attributes Property (ADO)](../../../ado/reference/ado-api/attributes-property-ado.md)
