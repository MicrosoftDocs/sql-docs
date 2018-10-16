---
title: "Type Property (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "_Parameter::Type"
  - "Field20::Type"
helpviewer_keywords: 
  - "Type property [ADO]"
ms.assetid: 8a4c079f-9f4f-4545-801d-85983b8db71e
author: MightyPen
ms.author: genemi
manager: craigg
---
# Type Property (ADO)
Indicates the operational type or data type of a [Parameter](../../../ado/reference/ado-api/parameter-object.md), [Field](../../../ado/reference/ado-api/field-object.md), or [Property](../../../ado/reference/ado-api/property-object-ado.md) object.  
  
## Settings and Return Values  
 Sets or returns a [DataTypeEnum](../../../ado/reference/ado-api/datatypeenum.md) value.  
  
## Remarks  
 For **Parameter** objects, the **Type** property is read/write. For new **Field** objects that have been appended to the [Fields](../../../ado/reference/ado-api/fields-collection-ado.md) collection of a [Record](../../../ado/reference/ado-api/record-object-ado.md), **Type** is read/write only after the [Value](../../../ado/reference/ado-api/value-property-ado.md) property for the **Field** has been specified and the data provider has successfully added the new **Field** by calling the [Update](../../../ado/reference/ado-api/update-method.md) method of the **Fields** collection.  
  
 For all other objects, the **Type** property is read-only.  
  
## Applies To  
  
||||  
|-|-|-|  
|[Field Object](../../../ado/reference/ado-api/field-object.md)|[Parameter Object](../../../ado/reference/ado-api/parameter-object.md)|[Property Object (ADO)](../../../ado/reference/ado-api/property-object-ado.md)|  
  
## See Also  
 [Type Property Example (Field) (VB)](../../../ado/reference/ado-api/type-property-example-field-vb.md)   
 [Type Property Example (Property) (VC++)](../../../ado/reference/ado-api/type-property-example-property-vc.md)   
 [RecordType Property (ADO)](../../../ado/reference/ado-api/recordtype-property-ado.md)   
 [Type Property (ADO Stream)](../../../ado/reference/ado-api/type-property-ado-stream.md)
