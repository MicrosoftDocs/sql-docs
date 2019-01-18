---
title: "Attributes Property (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Connection15::Attributes"
  - "Field20::Attributes"
  - "_Parameter::Attributes"
helpviewer_keywords: 
  - "Attributes property [ADO]"
ms.assetid: acc15d40-68a6-4ba9-85bd-12d331aecaa6
author: MightyPen
ms.author: genemi
manager: craigg
---
# Attributes Property (ADO)
Indicates one or more characteristics of an object.  
  
## Settings and Return Values  
 Sets or returns a **Long** value.  
  
 For a [Connection](../../../ado/reference/ado-api/connection-object-ado.md) object, the **Attributes** property is read/write, and its value can be the sum of one or more [XactAttributeEnum](../../../ado/reference/ado-api/xactattributeenum.md) values. The default is zero (0).  
  
 For a [Parameter](../../../ado/reference/ado-api/parameter-object.md) object, the **Attributes** property is read/write, and its value can be the sum of any one or more [ParameterAttributesEnum](../../../ado/reference/ado-api/parameterattributesenum.md) values. The default is **adParamSigned**.  
  
 For a [Field](../../../ado/reference/ado-api/field-object.md) object, the **Attributes** property can be the sum of one or more [FieldAttributeEnum](../../../ado/reference/ado-api/fieldattributeenum.md) values. It is normally read-only. However, for new **Field** objects that have been appended to the [Fields](../../../ado/reference/ado-api/fields-collection-ado.md) collection of a [Record](../../../ado/reference/ado-api/record-object-ado.md), **Attributes** is read/write only after the [Value](../../../ado/reference/ado-api/value-property-ado.md) property for the **Field** has been specified and the new **Field** has been successfully added by the data provider by calling the [Update](../../../ado/reference/ado-api/update-method.md) method of the **Fields** collection.  
  
 For a [Property](../../../ado/reference/ado-api/property-object-ado.md) object, the **Attributes** property is read-only, and its value can be the sum of any one or more [PropertyAttributesEnum](../../../ado/reference/ado-api/propertyattributesenum.md) values.  
  
## Remarks  
 Use the **Attributes** property to set or return characteristics of **Connection** objects, **Parameter** objects, **Field** objects, or **Property** objects.  
  
 When you set multiple attributes, you can sum the appropriate constants. If you set the property value to a sum including incompatible constants, an error occurs.  
  
> [!NOTE]
>  **Remote Data Service Usage** This property is not available on a client-side **Connection** object.  
  
## Applies To  
  
|||  
|-|-|  
|[Connection Object (ADO)](../../../ado/reference/ado-api/connection-object-ado.md)|[Field Object](../../../ado/reference/ado-api/field-object.md)|  
|[Parameter Object](../../../ado/reference/ado-api/parameter-object.md)|[Property Object (ADO)](../../../ado/reference/ado-api/property-object-ado.md)|  
  
## See Also  
 [Attributes and Name Properties Example (VB)](../../../ado/reference/ado-api/attributes-and-name-properties-example-vb.md)   
 [Attributes and Name Properties Example (VC++)](../../../ado/reference/ado-api/attributes-and-name-properties-example-vc.md)   
 [AppendChunk Method (ADO)](../../../ado/reference/ado-api/appendchunk-method-ado.md)   
 [BeginTrans, CommitTrans, and RollbackTrans Methods (ADO)](../../../ado/reference/ado-api/begintrans-committrans-and-rollbacktrans-methods-ado.md)   
 [GetChunk Method (ADO)](../../../ado/reference/ado-api/getchunk-method-ado.md)
