---
title: "Axes Collection (ADO MD)"
description: "Axes Collection (ADO MD)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Axes"
  - "Cellset::Axes"
helpviewer_keywords:
  - "Axes collection [ADO MD]"
apitype: "COM"
---
# Axes Collection (ADO MD)
Contains the [Axis](./axis-object-ado-md.md) objects that define a cellset.  
  
## Remarks  
 A [Cellset](./cellset-object-ado-md.md) object contains an **Axes** collection. Once the **Cellset** is opened, this collection will contain at least one **Axis**. See the [Axis](./axis-object-ado-md.md) object for a more detailed explanation of how to use **Axis** objects.  
  
> [!NOTE]
>  The filter axis of a **Cellset** is not contained in the **Axes** collection. See the [FilterAxis](./filteraxis-property-ado-md.md) property for more information.  
  
 **Axes** is a standard ADO collection. With the properties and methods of a collection, you can do the following:  
  
-   Obtain the number of objects in the collection with the [Count](../ado-api/count-property-ado.md) property.  
  
-   Return an object from the collection with the default [Item](../ado-api/item-property-ado.md) property.  
  
-   Update the objects in the collection from the provider with the [Refresh](../ado-api/refresh-method-ado.md) method.  
  
 This section contains the following topic.  
  
-   [Properties, Methods, and Events](./axes-collection-properties-methods-and-events.md)  
  
## See Also  
 [Cellset Example (VB)](./cellset-example-vb.md)   
 [Axis Object (ADO MD)](./axis-object-ado-md.md)