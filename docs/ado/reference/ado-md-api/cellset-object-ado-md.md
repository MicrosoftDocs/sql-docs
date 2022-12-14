---
title: "Cellset Object (ADO MD)"
description: "Cellset Object (ADO MD)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Cellset"
helpviewer_keywords:
  - "Cellset object [ADO MD]"
apitype: "COM"
---
# Cellset Object (ADO MD)
Represents the results of a multidimensional query. It is a collection of cells selected from cubes or other cellsets.  
  
## Remarks  
 Data within a **Cellset** is retrieved using direct, array-like access. You can drill down to a specific member to obtain data about that member. For example, the following code returns the caption of the first member in the first position on the first axis of a cellset named `cst`:  
  
```  
cst.Axes(0).Positions(0).Members(0).Caption  
```  
  
 There is no notion of a current cell within a cellset. Instead, the [Item](./item-property-ado-md-cellset.md) property retrieves a specific [Cell](./cell-object-ado-md.md) object from the cellset. The arguments of the **Item** property determine which cell is retrieved. You can specify the unique ordinal value of a cell. You can also retrieve cells by using their position numbers along each axis of the cellset. For more information about retrieving cells, see the [Item](./item-property-ado-md-cellset.md) property.  
  
 With the collections, methods, and properties of a **Cellset** object, you can do the following:  
  
-   Associate an open connection with a **Cellset** object by setting its [ActiveConnection](./activeconnection-property-ado-md.md) property.  
  
-   Execute and retrieve the results of a multidimensional query with the [Open](./open-method-ado-md.md) method.  
  
-   Retrieve a **Cell** from the **Cellset** with the [Item](./item-property-ado-md-cellset.md) property.  
  
-   Return the [Axis](./axis-object-ado-md.md) objects that define the **Cellset** with the [Axes](./axes-collection-ado-md.md) collection.  
  
-   Retrieve information about the dimensions used to filter the data in the **Cellset** with the [FilterAxis](./filteraxis-property-ado-md.md) property.  
  
-   Return or specify the query used to define the **Cellset** with the [Source](./source-property-ado-md.md) property.  
  
-   Return the current state of the **Cellset** (open, closed, executing, or connecting) with the [State](./state-property-ado-md.md) property.  
  
-   Close an open **Cellset** with the [Close](./close-method-ado-md.md) method.  
  
-   Retrieve provider-specific information about the **Cellset** with the standard ADO [Properties](../ado-api/properties-collection-ado.md) collection.  
  
 This section contains the following topic.  
  
-   [Properties, Methods, and Events](./cellset-object-properties-methods-and-events.md)  
  
## See Also  
 [Cellset Example (VB)](./cellset-example-vb.md)   
 [Axes Collection (ADO MD)](./axes-collection-ado-md.md)   
 [Cell Object (ADO MD)](./cell-object-ado-md.md)   
 [Connection Object (ADO)](../ado-api/connection-object-ado.md)   
 [Properties Collection (ADO)](../ado-api/properties-collection-ado.md)