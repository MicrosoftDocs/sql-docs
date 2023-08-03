---
title: "Axis Object (ADO MD)"
description: "Axis Object (ADO MD)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Axis"
helpviewer_keywords:
  - "Axis object [ADO MD]"
apitype: "COM"
---
# Axis Object (ADO MD)
Represents a positional or filter axis of a cellset, containing selected members of one or more dimensions.  
  
## Remarks  
 An **Axis** object can be contained by an [Axes](./axes-collection-ado-md.md) collection, or returned by the [FilterAxis](./filteraxis-property-ado-md.md) property of a [Cellset](./cellset-object-ado-md.md).  
  
 With the collections and properties of an **Axis** object, you can do the following:  
  
-   Identify the **Axis** with the [Name](./name-property-ado-md.md) property.  
  
-   Iterate through each position along an **Axis** using the [Positions](./positions-collection-ado-md.md) collection.  
  
-   Obtain the number of dimensions on the **Axis** with the [DimensionCount](./dimensioncount-property-ado-md.md) property.  
  
-   Obtain provider-specific attributes of the **Axis** with the standard ADO [Properties](../ado-api/properties-collection-ado.md) collection.  
  
 This section contains the following topic.  
  
-   [Properties, Methods, and Events](./axis-object-properties-methods-and-events.md)  
  
## See Also  
 [Axis Example (VBScript)](./axis-example-vbscript.md)   
 [Axes Collection (ADO MD)](./axes-collection-ado-md.md)   
 [Positions Collection (ADO MD)](./positions-collection-ado-md.md)   
 [Properties Collection (ADO)](../ado-api/properties-collection-ado.md)