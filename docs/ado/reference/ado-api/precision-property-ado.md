---
title: "Precision Property (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "_Parameter::Precision"
  - "Field20::Precision"
helpviewer_keywords: 
  - "Precision property [ADO]"
ms.assetid: 1fa38e78-6b5b-414d-ba0a-3dd26b29b766
author: MightyPen
ms.author: genemi
manager: craigg
---
# Precision Property (ADO)
Indicates the degree of precision for numeric values in a [Parameter](../../../ado/reference/ado-api/parameter-object.md) object or for numeric [Field](../../../ado/reference/ado-api/field-object.md) objects.  
  
## Settings and Return Values  
 Sets or returns a **Byte** value that indicates the maximum number of digits used to represent values.  
  
## Remarks  
 Use the **Precision** property to determine the maximum number of digits used to represent values for a numeric **Parameter** or **Field** object.  
  
 The value is read/write on a **Parameter** object.  
  
 For a **Field**object, **Precision** is normally read-only. However, for new **Field** objects that have been appended to the [Fields](../../../ado/reference/ado-api/fields-collection-ado.md) collection of a [Record](../../../ado/reference/ado-api/record-object-ado.md), **Precision** is read/write only after the [Value](../../../ado/reference/ado-api/value-property-ado.md) property for the **Field** has been specified and the data provider has successfully added the new **Field** by calling the [Update](../../../ado/reference/ado-api/update-method.md) method of the **Fields** collection.  
  
## Applies To  
  
|||  
|-|-|  
|[Field Object](../../../ado/reference/ado-api/field-object.md)|[Parameter Object](../../../ado/reference/ado-api/parameter-object.md)|  
  
## See Also  
 [NumericScale and Precision Properties Example (VB)](../../../ado/reference/ado-api/numericscale-and-precision-properties-example-vb.md)   
 [NumericScale and Precision Properties Example (VC++)](../../../ado/reference/ado-api/numericscale-and-precision-properties-example-vc.md)   
 [NumericScale Property (ADO)](../../../ado/reference/ado-api/numericscale-property-ado.md)
