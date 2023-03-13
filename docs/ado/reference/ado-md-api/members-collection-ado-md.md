---
title: "Members Collection (ADO MD)"
description: "Members Collection (ADO MD)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Level::Members"
  - "Members"
  - "Position::Members"
helpviewer_keywords:
  - "Members collection [ADO MD]"
apitype: "COM"
---
# Members Collection (ADO MD)
Contains the [Member](./member-object-ado-md.md) objects from a level or a position along an axis.  
  
## Remarks  
 A **Members** collection is used to contain the following types of members:  
  
-   The members that make up a level in a cube. These are contained in the **Members** collection of a [Level](./level-object-ado-md.md) object. For example, using the sample from [Overview of Multidimensional Schemas and Data](../../guide/multidimensional/overview-of-multidimensional-schemas-and-data.md), the four members of the Countries level are Canada, USA, UK, and Germany.  
  
-   The members that are the children of a specific member within a hierarchy. These members are returned by the [Children](./children-property-ado-md.md) property of the parent **Member** object. For example, again using the same sample, the two children of the Canada member are Canada-East and Canada-West.  
  
-   The members that define a specific position along an axis of a [cellset](./cellset-object-ado-md.md). Using the cellset from [Working with Multidimensional Data](../../guide/multidimensional/working-with-multidimensional-data.md) as an example, the two members of the first position on the x-axis are Valentine and Seattle. These members are contained by the **Members** collection of a [Position](./position-object-ado-md.md) object.  
  
 **Members** is a standard ADO collection. With the properties and methods of a collection, you can do the following:  
  
-   Obtain the number of objects in the collection with the [Count](../ado-api/count-property-ado.md) property.  
  
-   Return an object from the collection with the default [Item](../ado-api/item-property-ado.md) property.  
  
-   Update the objects in the collection from the provider with the [Refresh](../ado-api/refresh-method-ado.md) method.  
  
 This section contains the following topic.  
  
-   [Properties, Methods, and Events](./members-collection-properties-methods-and-events.md)  
  
## See Also  
 [Members Example (VBScript)](./members-example-vbscript.md)   
 [Member Object (ADO MD)](./member-object-ado-md.md)