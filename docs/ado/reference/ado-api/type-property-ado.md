---
title: "Type Property (ADO)"
description: "Type Property (ADO)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "_Parameter::Type"
  - "Field20::Type"
helpviewer_keywords:
  - "Type property [ADO]"
apitype: "COM"
---
# Type Property (ADO)
Indicates the operational type or data type of a [Parameter](./parameter-object.md), [Field](./field-object.md), or [Property](./property-object-ado.md) object.  
  
## Settings and Return Values  
 Sets or returns a [DataTypeEnum](./datatypeenum.md) value.  
  
## Remarks  
 For **Parameter** objects, the **Type** property is read/write. For new **Field** objects that have been appended to the [Fields](./fields-collection-ado.md) collection of a [Record](./record-object-ado.md), **Type** is read/write only after the [Value](./value-property-ado.md) property for the **Field** has been specified and the data provider has successfully added the new **Field** by calling the [Update](./update-method.md) method of the **Fields** collection.  
  
 For all other objects, the **Type** property is read-only.  
  
## Applies To  

:::row:::
    :::column:::
        [Field Object](./field-object.md)  
    :::column-end:::
    :::column:::
        [Parameter Object](./parameter-object.md)  
    :::column-end:::
    :::column:::
        [Property Object (ADO)](./property-object-ado.md)  
    :::column-end:::
:::row-end:::

## See Also  
 [Type Property Example (Field) (VB)](./type-property-example-field-vb.md)   
 [Type Property Example (Property) (VC++)](./type-property-example-property-vc.md)   
 [RecordType Property (ADO)](./recordtype-property-ado.md)   
 [Type Property (ADO Stream)](./type-property-ado-stream.md)