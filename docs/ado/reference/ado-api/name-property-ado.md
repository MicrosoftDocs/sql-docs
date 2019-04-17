---
title: "Name Property (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "_Parameter::Name"
  - "Field20::Name"
helpviewer_keywords: 
  - "Name property [ADO]"
ms.assetid: cfd0e29c-8310-44ab-85c3-5761184b865d
author: MightyPen
ms.author: genemi
manager: craigg
---
# Name Property (ADO)
Indicates the name of an object.  
  
## Settings and Return Values  
 Sets or returns a **String** value that indicates the name of an object.  
  
## Remarks  
 Use the **Name** property to assign a name to or retrieve the name of a **Command**, **Property**, **Field**, or **Parameter** object.  
  
 The value is read/write on a **Command** object and read-only on a **Property** object.  
  
 For a **Field** object, **Name** is normally read-only. However, for new **Field** objects that have been appended to the [Fields](../../../ado/reference/ado-api/fields-collection-ado.md) collection of a [Record](../../../ado/reference/ado-api/record-object-ado.md), **Name** is read/write only after the [Value](../../../ado/reference/ado-api/value-property-ado.md) property for the **Field** has been specified and the data provider has successfully added the new **Field** by calling the [Update](../../../ado/reference/ado-api/update-method.md) method of the **Fields** collection.  
  
 For **Parameter** objects not yet appended to the [Parameters](../../../ado/reference/ado-api/parameters-collection-ado.md) collection, the **Name** property is read/write. For appended **Parameter** objects and all other objects, the **Name** property is read-only. Names do not have to be unique within a collection.  
  
 You can retrieve the **Name** property of an object by an ordinal reference, after which you can refer to the object directly by name. For example, if `rstMain.Properties(20).Name` yields `Updatability`, you can subsequently refer to this property as `rstMain.Properties("Updatability")`.  
  
## Applies To  
  
|||  
|-|-|  
|[Command Object (ADO)](../../../ado/reference/ado-api/command-object-ado.md)|[Field Object](../../../ado/reference/ado-api/field-object.md)|  
|[Parameter Object](../../../ado/reference/ado-api/parameter-object.md)|[Property Object (ADO)](../../../ado/reference/ado-api/property-object-ado.md)|  
  
## See Also  
 [Attributes and Name Properties Example (VB)](../../../ado/reference/ado-api/attributes-and-name-properties-example-vb.md)   
 [Attributes and Name Properties Example (VC++)](../../../ado/reference/ado-api/attributes-and-name-properties-example-vc.md)   
