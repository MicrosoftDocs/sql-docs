---
title: "Catalog Object (ADO MD)"
description: "Catalog Object (ADO MD)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Catalog object [ADO MD]"
helpviewer_keywords:
  - "Catalog object [ADO MD]"
apitype: "COM"
---

# Catalog Object (ADO MD)

Contains multidimensional schema information (that is, cubes and underlying dimensions, hierarchies, levels, and members) specific to a multidimensional data provider (MDP).
  
## Remarks

With the collections and properties of a **Catalog** object, you can do the following:
  
- Open the catalog by setting the [ActiveConnection](./activeconnection-property-ado-md.md) property to a standard ADO [Connection](../ado-api/connection-object-ado.md) object or to a valid connection string.
  
- Identify the **Catalog** with the [Name](./name-property-ado-md.md) property.
  
- Iterate through the cubes in a catalog using the [CubeDefs](./cubedefs-collection-ado-md.md) collection.
  
This section contains the following topic.  
  
- [Properties, Methods, and Events](./catalog-object-properties-methods-and-events-ado-md.md)  
  
## See Also  

- [Catalog Example (VB)](./catalog-example-vb.md)
- [Connection Object (ADO)](../ado-api/connection-object-ado.md)
- [CubeDefs Collection (ADO MD)](./cubedefs-collection-ado-md.md)