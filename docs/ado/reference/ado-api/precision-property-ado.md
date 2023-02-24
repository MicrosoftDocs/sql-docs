---
title: "Precision Property (ADO)"
description: "Precision Property (ADO)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "_Parameter::Precision"
  - "Field20::Precision"
helpviewer_keywords:
  - "Precision property [ADO]"
apitype: "COM"
---
# Precision Property (ADO)
Indicates the degree of precision for numeric values in a [Parameter](./parameter-object.md) object or for numeric [Field](./field-object.md) objects.  
  
## Settings and Return Values  
 Sets or returns a **Byte** value that indicates the maximum number of digits used to represent values.  
  
## Remarks  
 Use the **Precision** property to determine the maximum number of digits used to represent values for a numeric **Parameter** or **Field** object.  
  
 The value is read/write on a **Parameter** object.  
  
 For a **Field**object, **Precision** is normally read-only. However, for new **Field** objects that have been appended to the [Fields](./fields-collection-ado.md) collection of a [Record](./record-object-ado.md), **Precision** is read/write only after the [Value](./value-property-ado.md) property for the **Field** has been specified and the data provider has successfully added the new **Field** by calling the [Update](./update-method.md) method of the **Fields** collection.  
  
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
 [NumericScale and Precision Properties Example (VB)](./numericscale-and-precision-properties-example-vb.md)   
 [NumericScale and Precision Properties Example (VC++)](./numericscale-and-precision-properties-example-vc.md)   
 [NumericScale Property (ADO)](./numericscale-property-ado.md)