---
title: "Working with Multidimensional Data"
description: "Working with Multidimensional Data"
author: rothja
ms.author: jroth
ms.date: 11/08/2018
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "multidimensional data [ADO]"
---
# Working with Multidimensional Data
A *cellset* is the result of a query on multidimensional data. It consists of a collection of axes, usually no more than four axes and typically only two or three. An *axis* is a collection of members from one or more dimensions, which is used to locate or filter specific values in a cube.  
  
 A *position* is a point along an axis. For an axis consisting of a single dimension, these positions are a subset of the dimension members. If an axis consists of more than one dimension, then each position is a compound entity, which has *n* parts where *n* is the number of dimensions oriented along that axis. Each part of the position is a member from one constituent dimension.  
  
 For example, if the Geography and Product dimensions from a cube containing sales data are oriented along the x-axis of a cellset, a position along this axis may contain the members "USA" and "Computers." In this example, determining a position along the x-axis requires that members from each dimension are oriented along the axis.  
  
 A *cell* is an object positioned at the intersection of axis coordinates. Each cell has multiple pieces of information associated with it, including the data itself, a formatted string (the displayable form of cell data), and the cell ordinal value. (Each cell is a unique ordinal value in the cellset. The ordinal value of the first cell in the cellset is zero, while the leftmost cell in the second row of a cellset with eight columns would have an ordinal value of eight.)  
  
 For example, a cube has the following six dimensions (note that this cube schema differs slightly from the example given in [Overview of Multidimensional Schemas and Data](./overview-of-multidimensional-schemas-and-data.md)):  
  
-   Salesperson  
  
-   Geography (natural hierarchy) - Continents, Countries/Regions, States, and so on  
  
-   Quarters - Quarters, Months, Days  
  
-   Years  
  
-   Measures - Sales, PercentChange, BudgetedSales  
  
-   Products  
  
 The following cellset represents sales for 1991 for all products:  
  
> [!NOTE]
>  The cell values in the example can be viewed as ordered pairs of axis position ordinals where the first digit represents the x-axis position and the second digit the y-axis position.  
  
 The characteristics of this cellset are as follows:  
  
-   Axis dimensions: Quarters, Salesperson, Geography  
  
-   Filter dimensions: Measures, Years, Products  
  
-   Two axes: COLUMN (x, or Axis 0) and ROW (y, or Axis 1)  
  
-   x-axis: two nested dimensions, Salesperson and Geography  
  
-   y-axis: Quarters dimension  
  
 The x-axis has two nested dimensions: Salesperson and Geography. From Geography, four members are selected: Seattle, Boston, USA-South, and Japan. Two members are selected from Salesperson: Valentine and Nash. This yields a total of eight positions on this axis (8 = 4*2).  
  
 Each coordinate is represented as a position with two members - one from the Salesperson dimension and another from the Geography dimension:  
  
```console
(Valentine, Seattle), (Valentine, Boston), (Valentine, USA_North),  
(Valentine, Japan), (Nash, Seattle), (Nash, Boston), (Nash, USA_North),  
(Nash, Japan)  
```  
  
 The y-axis has only one dimension, containing the following eight positions:  
  
```console
Jan, Feb, Mar, Qtr2, Qtr3, Oct, Nov, Dec  
```  
  
 Cellsets, cells, axes, and positions are all represented in ADO MD by corresponding objects: [Cellset](../../reference/ado-md-api/cellset-object-ado-md.md), [Cell](../../reference/ado-md-api/cell-object-ado-md.md), [Axis](../../reference/ado-md-api/axis-object-ado-md.md), and [Position](../../reference/ado-md-api/position-object-ado-md.md).  
  
## See Also  
 [ADO MD Object Model](../../reference/ado-md-api/ado-md-object-model.md)   
 [ADO (Multidimensional) (ADO MD)](./ado-multidimensional-ado-md.md)   
 [Overview of Multidimensional Schemas and Data](./overview-of-multidimensional-schemas-and-data.md)   
 [Programming with ADO MD](./programming-with-ado-md.md)   
 [Using ADO with ADO MD](./using-ado-with-ado-md.md)