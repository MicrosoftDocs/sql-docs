---
title: "GetChunk Method (ADO)"
description: "GetChunk Method (ADO)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Field20::raw_GetChunk"
  - "Field20::GetChunk"
helpviewer_keywords:
  - "GetChunk method [ADO]"
apitype: "COM"
---
# GetChunk Method (ADO)
Returns all, or a portion, of the contents of a large text or binary data [Field](./field-object.md) object.  
  
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
  
 Each subsequent **GetChunk** call retrieves data starting from where the previous **GetChunk** call left off. However, if you are retrieving data from one field and then you set or read the value of another field in the current record, ADO assumes you have finished retrieving data from the first field. If you call the **GetChunk** method on the first field again, ADO interprets the call as a new **GetChunk** operation and starts reading from the beginning of the data. Accessing fields in other [Recordset](./recordset-object-ado.md) objects that are not clones of the first **Recordset** object will not disrupt **GetChunk** operations.  
  
 If the **adFldLong** bit in the [Attributes](./attributes-property-ado.md) property of a **Field** object is set to **True**, you can use the **GetChunk** method for that field.  
  
 If there is no current record when you use the **GetChunk** method on a **Field** object, error 3021 (no current record) occurs.  
  
> [!NOTE]
>  The **GetChunk** method does not operate on **Field** objects of a [Record](./record-object-ado.md) object. It does not perform any operation and will produce a run-time error.  
  
## Applies To  
 [Field Object](./field-object.md)  
  
## See Also  
 [AppendChunk and GetChunk Methods Example (VB)](./appendchunk-and-getchunk-methods-example-vb.md)   
 [AppendChunk and GetChunk Methods Example (VC++)](./appendchunk-and-getchunk-methods-example-vc.md)   
 [AppendChunk Method (ADO)](./appendchunk-method-ado.md)   
 [Attributes Property (ADO)](./attributes-property-ado.md)