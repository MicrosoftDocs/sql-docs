---
title: "Sort Data in a Table (SSAS Tabular) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "analysis-services/multidimensional-tabular"
  - "analysis-services/data-mining"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 5fa6ad56-bf68-4aac-a226-52556173b7e2
caps.latest.revision: 10
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Sort Data in a Table (SSAS Tabular)
  You can sort data by text (A to Z or Z to A) and numbers (smallest to largest or largest to smallest) in one or more columns.  
  
### To sort the data in a table based on a text column  
  
1.  In the model designer, select a column of alphanumeric data, a range of cells in a column, or make sure that the active cell is in a table column that contains alphanumeric data, and then click the arrow in the header of the column that you want to filter by.  
  
2.  In the AutoFilter menu, do one of the following:  
  
    -   To sort in ascending alphanumeric order, click **Sort A to Z**.  
  
    -   To sort in descending alphanumeric order, click **Sort Z to A**.  
  
    > [!NOTE]  
    >  In some cases, data imported from another application might have leading spaces inserted before data. You must remove the leading spaces in order to correctly sort the data.  
  
### To sort the data in a table based on a numeric column  
  
1.  In the model designer, select a column of alphanumeric data, a range of cells in a column, or make sure that the active cell is in a table column that contains alphanumeric data, and then click the arrow in the header of the column that you want to filter by.  
  
2.  In the AutoFilter menu, do one of the following:  
  
    -   To sort from low numbers to high numbers, click **Sort Smallest to Largest**.  
  
    -   To sort from high numbers to low numbers, click **Sort Largest to Smallest**.  
  
    > [!NOTE]  
    >  If the results are not what you expected, the column might contain numbers stored as text and not as numbers. For example, negative numbers imported from some accounting systems, or a number entered with a leading ' (apostrophe), are stored as text.  
  
## See Also  
 [Filter and Sort Data &#40;SSAS Tabular&#41;](http://msdn.microsoft.com/library/55ebd7a6-2458-4398-911f-fcfeb2413f1b)   
 [Perspectives &#40;SSAS Tabular&#41;](../../analysis-services/tabular-models/perspectives-ssas-tabular.md)   
 [Roles &#40;SSAS Tabular&#41;](../../analysis-services/tabular-models/roles-ssas-tabular.md)  
  
  