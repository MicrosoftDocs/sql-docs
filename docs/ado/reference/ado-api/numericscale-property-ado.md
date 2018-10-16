---
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
author: MightyPen
ms.author: genemi
manager: craigg
---
# NumericScale Property (ADO)
Indicates the scale of numeric values in a [Parameter](../../../ado/reference/ado-api/parameter-object.md) or [Field](../../../ado/reference/ado-api/field-object.md) object.  
  
## Settings and Return Values  
 Sets or returns a **Byte** value that indicates the number of decimal places to which numeric values will be resolved.  
  
## Remarks  
 Use the **NumericScale** property to determine how many digits to the right of the decimal point will be used to represent values for a numeric **Parameter** or **Field** object.  
  
 For **Parameter** objects, the **NumericScale** property is read/write.  
  
 For a **Field**object, **NumericScale** is normally read-only. However, for new **Field** objects that have been appended to the [Fields](../../../ado/reference/ado-api/fields-collection-ado.md) collection of a [Record](../../../ado/reference/ado-api/record-object-ado.md), **NumericScale** is read/write only after the [Value](../../../ado/reference/ado-api/value-property-ado.md) property for the **Field** has been specified and the data provider has successfully added the new **Field** by calling the [Update](../../../ado/reference/ado-api/update-method.md) method of the [Fields](../../../ado/reference/ado-api/fields-collection-ado.md) collection.  
  
## Applies To  
  
|||  
|-|-|  
|[Parameter Object](../../../ado/reference/ado-api/parameter-object.md)|[Field Object](../../../ado/reference/ado-api/field-object.md)|  
  
## See Also  
 [NumericScale and Precision Properties Example (VB)](../../../ado/reference/ado-api/numericscale-and-precision-properties-example-vb.md)   
 [NumericScale and Precision Properties Example (VC++)](../../../ado/reference/ado-api/numericscale-and-precision-properties-example-vc.md)   
 [Precision Property (ADO)](../../../ado/reference/ado-api/precision-property-ado.md)
