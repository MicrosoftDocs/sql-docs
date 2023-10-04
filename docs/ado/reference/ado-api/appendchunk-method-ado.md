---
title: "AppendChunk Method (ADO)"
description: "AppendChunk Method (ADO)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "_Parameter::AppendChunk"
  - "Field20::AppendChunk"
helpviewer_keywords:
  - "AppendChunk method [ADO]"
apitype: "COM"
---
# AppendChunk Method (ADO)
Appends data to a large text or binary data [Field](./field-object.md), or to a [Parameter](./parameter-object.md) object.  
  
## Syntax  
  
```  
  
object.AppendChunk Data  
```  
  
#### Parameters  
 *object*  
 A **Field** or **Parameter** object.  
  
 *Data*  
 A **Variant** that contains the data to append to the object.  
  
## Remarks  
 Use the **AppendChunk** method on a **Field** or **Parameter** object to fill it with long binary or character data. In situations where system memory is limited, you can use the **AppendChunk** method to manipulate long values in portions rather than in their entirety.  
  
## Field  
 If the **adFldLong** bit in the [Attributes](./attributes-property-ado.md) property of a **Field** object is set to **true**, you can use the **AppendChunk** method for that field.  
  
 The first **AppendChunk** call on a **Field** object writes data to the field, overwriting any existing data. Subsequent **AppendChunk** calls add to existing data. If you are appending data to one field and then you set or read the value of another field in the current record, ADO assumes that you are finished appending data to the first field. If you call the **AppendChunk** method on the first field again, ADO interprets the call as a new **AppendChunk** operation and overwrites the existing data. Accessing fields in other [Recordset](./recordset-object-ado.md) objects that are not clones of the first **Recordset** object will not disrupt **AppendChunk** operations.  
  
 If there is no current record when you call **AppendChunk** on a **Field** object, an error occurs.  
  
> [!NOTE]
>  The **AppendChunk** method does not operate on **Field** objects of a [Record Object (ADO)](./record-object-ado.md) object. It does not perform any operation and will produce a run-time error.  
  
## Parameter  
 If the **adParamLong** bit in the **Attributes** property of a **Parameter** object is set to **true**, you can use the **AppendChunk** method for that parameter.  
  
 The first **AppendChunk** call on a **Parameter** object writes data to the parameter, overwriting any existing data. Subsequent **AppendChunk** calls on a **Parameter** object add to existing parameter data. An **AppendChunk** call that passes a null value discards all of the parameter data.  
  
## Applies To  

:::row:::
    :::column:::
        [Field Object](./field-object.md)  
    :::column-end:::
    :::column:::
        [Parameter Object](./parameter-object.md)  
    :::column-end:::
:::row-end:::

## See Also  
 [AppendChunk and GetChunk Methods Example (VB)](./appendchunk-and-getchunk-methods-example-vb.md)   
 [AppendChunk and GetChunk Methods Example (VC++)](./appendchunk-and-getchunk-methods-example-vc.md)   
 [Attributes Property (ADO)](./attributes-property-ado.md)   
 [GetChunk Method (ADO)](./getchunk-method-ado.md)