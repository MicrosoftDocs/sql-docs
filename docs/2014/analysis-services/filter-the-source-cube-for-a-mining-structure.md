---
title: "Filter the Source Cube for a Mining Structure | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "slice cubes [Analysis Services]"
  - "mining structures [Analysis Services], filtering source cube"
  - "cubes [Analysis Services], slicing"
  - "filtering data [Analysis Services]"
ms.assetid: 05dce7e1-2fe5-4500-bacf-c1a8a76e1424
author: minewiskan
ms.author: owend
manager: craigg
---
# Filter the Source Cube for a Mining Structure
  When you create a mining structure that is based on data in a multidimensional model (an OLAP cube), you can *slice* the cube that the mining structure is based on. Slicing lets you create subsets of data, as a kind of filter on the data that is used to train the mining model.  
  
### To slice a cube  
  
1.  In Data Mining Designer in [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)], select the **Mining Structure** tab or the **Mining Models** tab.  
  
2.  On the **Mining Model** menu, select **Define Mining Structure Cube Slice**.  
  
     The **Slice Cube** dialog box opens.  
  
3.  In the **Dimension** column of the **Slice Cube** dialog box, select the dimension that you want to filter.  
  
4.  Select a level of a hierarchy, using the list in the **Hierarchy** column.  
  
5.  Select an operator from the list in the **Operator** column, to use in building the filter condition.  
  
6.  Click the box in the **Filter** column.  
  
     A dialog box opens that contains all the members at the specified level of the hierarchy.  
  
7.  Select the member or members on which you want to filter.  
  
8.  Click **OK** in the member dialog box.  
  
9. Click **OK** in the **Slice Cube** dialog box.  
  
     The source cube is now filtered as defined by the cube slice.  
  
## See Also  
 [Mining Structure Tasks and How-tos](data-mining/mining-structure-tasks-and-how-tos.md)   
 [Create a New OLAP Mining Structure](data-mining/create-a-new-olap-mining-structure.md)  
  
  
