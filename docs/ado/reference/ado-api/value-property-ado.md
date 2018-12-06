---
title: "Value Property (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "03/20/2018"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Field20::Value"
  - "_Parameter::Value"
helpviewer_keywords: 
  - "Value property [ADO]"
ms.assetid: 48919c74-86d4-462e-99b9-8854ceb8d683
author: MightyPen
ms.author: genemi
manager: craigg
---
# Value Property (ADO)

Indicates the value assigned to a [Field](../../../ado/reference/ado-api/field-object.md), [Parameter](../../../ado/reference/ado-api/parameter-object.md), or [Property](../../../ado/reference/ado-api/property-object-ado.md) object.
  
## Settings and Return Values

Sets or returns a **Variant** value that indicates the value of the object. Default value depends on the [Type](../../../ado/reference/ado-api/type-property-ado.md) property.
  
## Remarks

Use the **Value** property to set or return data from **Field** objects, to set or return parameter values with **Parameter** objects, or to set or return property settings with **Property** objects. Whether the **Value** property is read/write or read-only depends upon numerous factors. See the respective object topics for more information.

ADO allows setting and returning long binary data with the **Value** property.
  
> [!NOTE]
> For **Parameter** objects, ADO reads the **Value** property only once from the provider. If a command contains a **Parameter** whose **Value** property is empty, and you create a [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) from the command, ensure that you first close the **Recordset** before retrieving the **Value** property. Otherwise, for some providers, the **Value** property may be empty, and won't contain the correct value.
> 
> For new **Field** objects that have been appended to the [Fields](../../../ado/reference/ado-api/fields-collection-ado.md) collection of a [Record](../../../ado/reference/ado-api/record-object-ado.md) object, the **Value** property must be set before any other **Field** properties can be specified. First, a specific value for the **Value** property must have been assigned and [Update](../../../ado/reference/ado-api/update-method.md) on the **Fields** collection called. Then, other properties such as [Type](../../../ado/reference/ado-api/type-property-ado.md) or [Attributes](../../../ado/reference/ado-api/attributes-property-ado.md) can be accessed.
  
## Applies To
  
||||  
|-|-|-|  
|[Field Object](../../../ado/reference/ado-api/field-object.md)|[Parameter Object](../../../ado/reference/ado-api/parameter-object.md)|[Property Object (ADO)](../../../ado/reference/ado-api/property-object-ado.md)|
  
## See Also

[Value Property Example (VB)](../../../ado/reference/ado-api/value-property-example-vb.md)
[Value Property Example (VC++)](../../../ado/reference/ado-api/value-property-example-vc.md) 
