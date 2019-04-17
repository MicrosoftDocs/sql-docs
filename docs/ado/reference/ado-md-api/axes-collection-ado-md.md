---
title: "Axes Collection (ADO MD) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Axes"
  - "Cellset::Axes"
helpviewer_keywords: 
  - "Axes collection [ADO MD]"
ms.assetid: 072fb21a-ec0f-4b02-9022-1cef3ad4bfff
author: MightyPen
ms.author: genemi
manager: craigg
---
# Axes Collection (ADO MD)
Contains the [Axis](../../../ado/reference/ado-md-api/axis-object-ado-md.md) objects that define a cellset.  
  
## Remarks  
 A [Cellset](../../../ado/reference/ado-md-api/cellset-object-ado-md.md) object contains an **Axes** collection. Once the **Cellset** is opened, this collection will contain at least one **Axis**. See the [Axis](../../../ado/reference/ado-md-api/axis-object-ado-md.md) object for a more detailed explanation of how to use **Axis** objects.  
  
> [!NOTE]
>  The filter axis of a **Cellset** is not contained in the **Axes** collection. See the [FilterAxis](../../../ado/reference/ado-md-api/filteraxis-property-ado-md.md) property for more information.  
  
 **Axes** is a standard ADO collection. With the properties and methods of a collection, you can do the following:  
  
-   Obtain the number of objects in the collection with the [Count](../../../ado/reference/ado-api/count-property-ado.md) property.  
  
-   Return an object from the collection with the default [Item](../../../ado/reference/ado-api/item-property-ado.md) property.  
  
-   Update the objects in the collection from the provider with the [Refresh](../../../ado/reference/ado-api/refresh-method-ado.md) method.  
  
 This section contains the following topic.  
  
-   [Properties, Methods, and Events](../../../ado/reference/ado-md-api/axes-collection-properties-methods-and-events.md)  
  
## See Also  
 [Cellset Example (VB)](../../../ado/reference/ado-md-api/cellset-example-vb.md)   
 [Axis Object (ADO MD)](../../../ado/reference/ado-md-api/axis-object-ado-md.md)
