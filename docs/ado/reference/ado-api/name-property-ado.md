---
title: "Name Property (ADO)"
description: "Name Property (ADO)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "_Parameter::Name"
  - "Field20::Name"
helpviewer_keywords:
  - "Name property [ADO]"
apitype: "COM"
---
# Name Property (ADO)
Indicates the name of an object.  
  
## Settings and Return Values  
 Sets or returns a **String** value that indicates the name of an object.  
  
## Remarks  
 Use the **Name** property to assign a name to or retrieve the name of a **Command**, **Property**, **Field**, or **Parameter** object.  
  
 The value is read/write on a **Command** object and read-only on a **Property** object.  
  
 For a **Field** object, **Name** is normally read-only. However, for new **Field** objects that have been appended to the [Fields](./fields-collection-ado.md) collection of a [Record](./record-object-ado.md), **Name** is read/write only after the [Value](./value-property-ado.md) property for the **Field** has been specified and the data provider has successfully added the new **Field** by calling the [Update](./update-method.md) method of the **Fields** collection.  
  
 For **Parameter** objects not yet appended to the [Parameters](./parameters-collection-ado.md) collection, the **Name** property is read/write. For appended **Parameter** objects and all other objects, the **Name** property is read-only. Names do not have to be unique within a collection.  
  
 You can retrieve the **Name** property of an object by an ordinal reference, after which you can refer to the object directly by name. For example, if `rstMain.Properties(20).Name` yields `Updatability`, you can subsequently refer to this property as `rstMain.Properties("Updatability")`.  
  
## Applies To  

:::row:::
    :::column:::
        [Command Object (ADO)](./command-object-ado.md)  
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