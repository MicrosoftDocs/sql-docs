---
title: "Value Property (ADO)"
description: "Value Property (ADO)"
author: rothja
ms.author: jroth
ms.date: "03/20/2018"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Field20::Value"
  - "_Parameter::Value"
helpviewer_keywords:
  - "Value property [ADO]"
apitype: "COM"
---
# Value Property (ADO)

Indicates the value assigned to a [Field](./field-object.md), [Parameter](./parameter-object.md), or [Property](./property-object-ado.md) object.
  
## Settings and Return Values

Sets or returns a **Variant** value that indicates the value of the object. Default value depends on the [Type](./type-property-ado.md) property.
  
## Remarks

Use the **Value** property to set or return data from **Field** objects, to set or return parameter values with **Parameter** objects, or to set or return property settings with **Property** objects. Whether the **Value** property is read/write or read-only depends upon numerous factors. See the respective object topics for more information.

ADO allows setting and returning long binary data with the **Value** property.
  
> [!NOTE]
> For **Parameter** objects, ADO reads the **Value** property only once from the provider. If a command contains a **Parameter** whose **Value** property is empty, and you create a [Recordset](./recordset-object-ado.md) from the command, ensure that you first close the **Recordset** before retrieving the **Value** property. Otherwise, for some providers, the **Value** property may be empty, and won't contain the correct value.
> 
> For new **Field** objects that have been appended to the [Fields](./fields-collection-ado.md) collection of a [Record](./record-object-ado.md) object, the **Value** property must be set before any other **Field** properties can be specified. First, a specific value for the **Value** property must have been assigned and [Update](./update-method.md) on the **Fields** collection called. Then, other properties such as [Type](./type-property-ado.md) or [Attributes](./attributes-property-ado.md) can be accessed.
  
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

[Value Property Example (VB)](./value-property-example-vb.md)
[Value Property Example (VC++)](./value-property-example-vc.md)