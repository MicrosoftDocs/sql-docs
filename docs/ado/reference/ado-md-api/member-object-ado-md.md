---
title: "Member Object (ADO MD)"
description: "Member Object (ADO MD)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Member"
helpviewer_keywords:
  - "Member object [ADO MD], members"
apitype: "COM"
---
# Member Object (ADO MD)
Represents a member of a level in a cube, the children of a member of a level, or a member of a position along an axis of a cellset.  
  
## Remarks  
 The properties of a **Member** differ depending on the context in which it is used. A **Member** of a [Level](./level-object-ado-md.md) in a [CubeDef](./cubedef-object-ado-md.md) has a [Children](./children-property-ado-md.md) property that returns the **Members** on the next lower level in the hierarchy from the current **Member**. For a **Member** of a [Position](./position-object-ado-md.md), the **Children** collection is always empty. Also, the [Type](./type-property-ado-md.md) property applies only to **Members** of a **Level**.  
  
 A **Member** of **Position** has two properties that are useful when displaying the [Cellset](./cellset-object-ado-md.md): [DrilledDown](./drilleddown-property-ado-md.md) and [ParentSameAsPrev](./parentsameasprev-property-ado-md.md). An error will occur if these properties are accessed on a **Member** of a **Level**.  
  
 With the collections and properties of a **Member** object of a **Level**, you can do the following:  
  
-   Identify the **Member** with the [Name](./name-property-ado-md.md) and [UniqueName](./uniquename-property-ado-md.md) properties.  
  
-   Return a string to use when displaying the **Member** with the [Caption](./caption-property-ado-md.md) property.  
  
-   Return a meaningful string that describes a measure or formula **Member** with the [Description](./description-property-ado-md.md) property.  
  
-   Determine the nature of the **Member** with the [Type](./type-property-ado-md.md) property.  
  
-   Obtain information about the **Level** of the **Member** with the [LevelDepth](./leveldepth-property-ado-md.md) and [LevelName](./levelname-property-ado-md.md) properties.  
  
-   Obtain related **Members** in a [Hierarchy](./hierarchy-object-ado-md.md) with the [Parent](./parent-property-ado-md.md) and [Children](./children-property-ado-md.md) properties.  
  
-   Count the children of a **Member** with the [ChildCount](./childcount-property-ado-md.md) property.  
  
-   Use the standard ADO [Properties](../ado-api/properties-collection-ado.md) collection to obtain additional information about the **Level** object.  
  
 With the collections and properties of a **Member** of a **Position** along an [Axis](./axis-object-ado-md.md), you can do the following:  
  
-   Identify the **Member** with the [Name](./name-property-ado-md.md) and [UniqueName](./uniquename-property-ado-md.md) properties.  
  
-   Return a string to use when displaying the **Member** with the [Caption](./caption-property-ado-md.md) property.  
  
-   Return a meaningful string that describes a measure or formula **Member** with the [Description](./description-property-ado-md.md) property.  
  
-   Obtain information about the **Level** of the **Member** with the [LevelDepth](./leveldepth-property-ado-md.md) and [LevelName](./levelname-property-ado-md.md) properties.  
  
-   Count the children of a **Member** with the [ChildCount](./childcount-property-ado-md.md) property.  
  
-   Use the [DrilledDown](./drilleddown-property-ado-md.md) property to determine whether there is at least one child on the **Axis** immediately following this **Member**.  
  
-   Use the [ParentSameAsPrev](./parentsameasprev-property-ado-md.md) property to determine whether the parent of this **Member** is the same as the parent of the immediately preceding **Member**.  
  
-   Use the standard ADO [Properties](../ado-api/properties-collection-ado.md) collection to obtain additional information about the **Level** object.  
  
 The **Properties** collection contains provider-supplied properties. The following table lists properties that might be available. The actual property list may differ depending upon the implementation of the provider. See the documentation for your provider for a more complete list of available properties.  
  
|Name|Description|  
|----------|-----------------|  
|CatalogName|The name of the catalog to which this cube belongs.|  
|ChildrenCardinality|The number of children that the member has.|  
|CubeName|The name of the cube.|  
|Description|A meaningful description of the member.|  
|DimensionUniqueName|The unambiguous name of the [dimension](./dimension-object-ado-md.md).|  
|HierarchyUniqueName|The unambiguous name of the hierarchy.|  
|LevelNumber|The distance between the level and the root of the hierarchy.|  
|LevelUniqueName|The unambiguous name of the level.|  
|MemberCaption|A label or caption associated with the member.|  
|MemberGUID|The GUID of the member.|  
|MemberName|The name of the member.|  
|MemberOrdinal|The ordinal number of the member.|  
|MemberType|The type of the member.|  
|MemberUniqueName|The unambiguous name of the member.|  
|ParentCount|The count of the number of parents that this member has.|  
|ParentLevel|The level number of the member's parent.|  
|ParentUniqueName|The unambiguous name of the member's parent.|  
|SchemaName|The name of the schema to which this cube belongs.|  
  
 This section contains the following topic.  
  
-   [Properties, Methods, and Events](./member-object-properties-methods-and-events.md)  
  
## See Also  
 [Catalog Example (VB)](./catalog-example-vb.md)   
 [Members Collection (ADO MD)](./members-collection-ado-md.md)   
 [Properties Collection (ADO)](../ado-api/properties-collection-ado.md)