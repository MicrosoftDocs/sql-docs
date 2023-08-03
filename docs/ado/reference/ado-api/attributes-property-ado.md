---
title: "Attributes Property (ADO)"
description: "Attributes Property (ADO)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Connection15::Attributes"
  - "Field20::Attributes"
  - "_Parameter::Attributes"
helpviewer_keywords:
  - "Attributes property [ADO]"
apitype: "COM"
---
# Attributes Property (ADO)
Indicates one or more characteristics of an object.  
  
## Settings and Return Values  
 Sets or returns a **Long** value.  
  
 For a [Connection](./connection-object-ado.md) object, the **Attributes** property is read/write, and its value can be the sum of one or more [XactAttributeEnum](./xactattributeenum.md) values. The default is zero (0).  
  
 For a [Parameter](./parameter-object.md) object, the **Attributes** property is read/write, and its value can be the sum of any one or more [ParameterAttributesEnum](./parameterattributesenum.md) values. The default is **adParamSigned**.  
  
 For a [Field](./field-object.md) object, the **Attributes** property can be the sum of one or more [FieldAttributeEnum](./fieldattributeenum.md) values. It is normally read-only. However, for new **Field** objects that have been appended to the [Fields](./fields-collection-ado.md) collection of a [Record](./record-object-ado.md), **Attributes** is read/write only after the [Value](./value-property-ado.md) property for the **Field** has been specified and the new **Field** has been successfully added by the data provider by calling the [Update](./update-method.md) method of the **Fields** collection.  
  
 For a [Property](./property-object-ado.md) object, the **Attributes** property is read-only, and its value can be the sum of any one or more [PropertyAttributesEnum](./propertyattributesenum.md) values.  
  
## Remarks  
 Use the **Attributes** property to set or return characteristics of **Connection** objects, **Parameter** objects, **Field** objects, or **Property** objects.  
  
 When you set multiple attributes, you can sum the appropriate constants. If you set the property value to a sum including incompatible constants, an error occurs.  
  
> [!NOTE]
>  **Remote Data Service Usage** This property is not available on a client-side **Connection** object.  
  
## Applies To  

:::row:::
    :::column:::
        [Connection Object (ADO)](./connection-object-ado.md)  
        [Field Object](./field-object.md)  
    :::column-end:::
    :::column:::
        [Parameter Object](./parameter-object.md)  
        [Property Object (ADO)](./property-object-ado.md)  
    :::column-end:::
:::row-end:::

## See Also  
 [Attributes and Name Properties Example (VB)](./attributes-and-name-properties-example-vb.md)   
 [Attributes and Name Properties Example (VC++)](./attributes-and-name-properties-example-vc.md)   
 [AppendChunk Method (ADO)](./appendchunk-method-ado.md)   
 [BeginTrans, CommitTrans, and RollbackTrans Methods (ADO)](./begintrans-committrans-and-rollbacktrans-methods-ado.md)   
 [GetChunk Method (ADO)](./getchunk-method-ado.md)