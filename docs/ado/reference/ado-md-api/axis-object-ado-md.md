---
description: "Axis Object (ADO MD)"
title: "Axis Object (ADO MD) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: ado
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Axis"
helpviewer_keywords: 
  - "Axis object [ADO MD]"
ms.assetid: 5f498c9a-b1e7-4e6e-9ae6-71eadaf9aada
author: rothja
ms.author: jroth
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