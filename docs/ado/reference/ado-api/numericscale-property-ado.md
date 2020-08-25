---
description: "NumericScale Property (ADO)"
title: "NumericScale Property (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "_Parameter::NumericScale"
  - "Field20::NumericScale"
helpviewer_keywords: 
  - "NumericScale property [ADO]"
ms.assetid: 29a02992-64be-4fcd-be13-445cba205893
author: rothja
ms.author: jroth
---
# NumericScale Property (ADO)
Indicates the scale of numeric values in a [Parameter](./parameter-object.md) or [Field](./field-object.md) object.  
  
## Settings and Return Values  
 Sets or returns a **Byte** value that indicates the number of decimal places to which numeric values will be resolved.  
  
## Remarks  
 Use the **NumericScale** property to determine how many digits to the right of the decimal point will be used to represent values for a numeric **Parameter** or **Field** object.  
  
 For **Parameter** objects, the **NumericScale** property is read/write.  
  
 For a **Field**object, **NumericScale** is normally read-only. However, for new **Field** objects that have been appended to the [Fields](./fields-collection-ado.md) collection of a [Record](./record-object-ado.md), **NumericScale** is read/write only after the [Value](./value-property-ado.md) property for the **Field** has been specified and the data provider has successfully added the new **Field** by calling the [Update](./update-method.md) method of the [Fields](./fields-collection-ado.md) collection.  
  
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
 [Precision Property (ADO)](./precision-property-ado.md)