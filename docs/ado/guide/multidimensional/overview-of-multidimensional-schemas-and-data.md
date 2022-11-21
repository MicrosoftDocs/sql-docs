---
title: "Overview of Multidimensional Schemas and Data"
description: "Overview of Multidimensional Schemas and Data"
author: rothja
ms.author: jroth
ms.date: 11/08/2018
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "multidimensional schemas and data"
---
# Overview of Multidimensional Schemas and Data
## Understanding Multidimensional Schemas  
 The central metadata object in ADO MD is the *cube*, which consists of a structured set of related dimensions, hierarchies, levels, and members.  
  
 A *dimension* is an independent category of data from your multidimensional database, derived from your business entities. A dimension typically contains items to be used as query criteria for the measures of the database.  
  
 A *hierarchy* is a path of aggregation of a dimension. A dimension may have multiple levels of granularity, which have parent-child relationships. A hierarchy defines how these levels are related.  
  
 A *level* is a step of aggregation in a hierarchy. For dimensions with multiple layers of information, each layer is a level.  
  
 A *member* is a data item in a dimension. Typically, you create a caption or describe a measure of the database using members.  
  
 Cubes are represented by [CubeDef](../../reference/ado-md-api/cubedef-object-ado-md.md) objects in ADO MD. Dimensions, hierarchies, levels, and members are also represented by their corresponding ADO MD objects: [Dimension](../../reference/ado-md-api/dimension-object-ado-md.md), [Hierarchy](../../reference/ado-md-api/hierarchy-object-ado-md.md), [Level](../../reference/ado-md-api/level-object-ado-md.md), and [Member](../../reference/ado-md-api/member-object-ado-md.md).  
  
### Dimensions  
 The dimensions of a cube depend on your business entities and types of data to be modeled in the database. Typically, each dimension is an independent entry point or mechanism for selecting data.  
  
 For example, a cube containing sales data has the following five dimensions: Salesperson, Geography, Time, Products, and Measures. The Measures dimension contains actual sales data values, while the other dimensions represent ways to categorize and group the sales data values.  
  
 The Geography dimension has the following set of members:  
  
```console
{All, North America, Europe, Canada, USA, UK, Germany, Canada-West,  
Canada-East, USA-NW, USA-SW, USA-NE, USA-SE, England, Scotland,   
Wales,Ireland, Germany-North, Germany-South, Ottawa, Toronto,   
Vancouver, Calgary, Seattle, Boise, Los Angeles, Houston,   
Shreveport, Miami, Boston, New York, London, Dover, Glasgow,   
Edinburgh, Cardiff, Pembroke, Belfast, Derry, Berlin,   
Hamburg, Munich, Stuttgart}  
```  
  
### Hierarchies  
 Hierarchies define the ways in which the levels of a dimension can be "rolled up" or grouped. A dimension can have more than one hierarchy. A natural hierarchy exists in the Geography dimension:  
  
### Levels  
 In the example Geography dimension pictured in the previous figure, each box represents a level in the hierarchy.  
  
 Each level has a set of members, as follows:  
  
-   The World `= {All}`  
  
-   Continents `= {North America, Europe}`  
  
-   Countries `= {Canada, USA, UK, Germany}`  
  
-   Regions `= {Canada-East, Canada-West, USA-NE, USA-NW, USA-SE, USA-SW, England, Ireland, Scotland, Wales, Germany-North, Germany-South}`  
  
-   Cities `= {Ottawa, Toronto, Vancouver, Calgary, Seattle, Boise, Los Angeles, Houston, Shreveport, Miami, Boston, New York, London, Dover, Glasgow, Edinburgh, Cardiff, Pembroke, Belfast, Derry, Berlin, Hamburg, Munich, Stuttgart}`  
  
### Members  
 Members at the leaf level of a hierarchy have no children, and members at the root level have no parent. All other members have at least one parent and at least one child. For example, a partial traversal of the hierarchy tree in the Geography dimension yields the following parent-child relationships:  
  
-   `{All} (parent of) {Europe, North America}`  
  
-   `{North America} (parent of) {Canada, USA}`  
  
-   `{USA} (parent of) {USA-NE, USA-NW, USA-SE, USA-SW}`  
  
-   `{USA-NW} (parent of) {Boise, Seattle}`  
  
 Members can be consolidated along one or more hierarchies per dimension. Consider a Time dimension where there are two ways to roll up to the Year level from the Days level:  
  
 This example also illustrates another characteristic: Some members of the Week level of the Year-Week hierarchy do not appear in any level of the Year-Quarter hierarchy. Thus, a hierarchy need not include all members of a dimension.  
  
## See Also  
 [ADO MD Object Model](../../reference/ado-md-api/ado-md-object-model.md)   
 [ADO (Multidimensional) (ADO MD)](./ado-multidimensional-ado-md.md)   
 [Programming with ADO MD](./programming-with-ado-md.md)   
 [Using ADO with ADO MD](./using-ado-with-ado-md.md)   
 [Working with Multidimensional Data](./working-with-multidimensional-data.md)