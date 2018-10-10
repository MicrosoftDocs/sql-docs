---
title: "Members Collection (ADO MD) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Level::Members"
  - "Members"
  - "Position::Members"
helpviewer_keywords: 
  - "Members collection [ADO MD]"
ms.assetid: 3a647cde-efdc-4394-b1b9-8cbb1b9d689f
author: MightyPen
ms.author: genemi
manager: craigg
---
# Members Collection (ADO MD)
Contains the [Member](../../../ado/reference/ado-md-api/member-object-ado-md.md) objects from a level or a position along an axis.  
  
## Remarks  
 A **Members** collection is used to contain the following types of members:  
  
-   The members that make up a level in a cube. These are contained in the **Members** collection of a [Level](../../../ado/reference/ado-md-api/level-object-ado-md.md) object. For example, using the sample from [Overview of Multidimensional Schemas and Data](../../../ado/guide/multidimensional/overview-of-multidimensional-schemas-and-data.md), the four members of the Countries level are Canada, USA, UK, and Germany.  
  
-   The members that are the children of a specific member within a hierarchy. These members are returned by the [Children](../../../ado/reference/ado-md-api/children-property-ado-md.md) property of the parent **Member** object. For example, again using the same sample, the two children of the Canada member are Canada-East and Canada-West.  
  
-   The members that define a specific position along an axis of a [cellset](../../../ado/reference/ado-md-api/cellset-object-ado-md.md). Using the cellset from [Working with Multidimensional Data](../../../ado/guide/multidimensional/working-with-multidimensional-data.md) as an example, the two members of the first position on the x-axis are Valentine and Seattle. These members are contained by the **Members** collection of a [Position](../../../ado/reference/ado-md-api/position-object-ado-md.md) object.  
  
 **Members** is a standard ADO collection. With the properties and methods of a collection, you can do the following:  
  
-   Obtain the number of objects in the collection with the [Count](../../../ado/reference/ado-api/count-property-ado.md) property.  
  
-   Return an object from the collection with the default [Item](../../../ado/reference/ado-api/item-property-ado.md) property.  
  
-   Update the objects in the collection from the provider with the [Refresh](../../../ado/reference/ado-api/refresh-method-ado.md) method.  
  
 This section contains the following topic.  
  
-   [Properties, Methods, and Events](../../../ado/reference/ado-md-api/members-collection-properties-methods-and-events.md)  
  
## See Also  
 [Members Example (VBScript)](../../../ado/reference/ado-md-api/members-example-vbscript.md)   
 [Member Object (ADO MD)](../../../ado/reference/ado-md-api/member-object-ado-md.md)
