---
title: "Sort Data in a Table (SSAS Tabular) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 5fa6ad56-bf68-4aac-a226-52556173b7e2
author: minewiskan
ms.author: owend
manager: craigg
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
 [Filter and Sort Data &#40;SSAS Tabular&#41;](../filter-and-sort-data-ssas-tabular.md)   
 [Perspectives &#40;SSAS Tabular&#41;](perspectives-ssas-tabular.md)   
 [Roles &#40;SSAS Tabular&#41;](roles-ssas-tabular.md)  
  
  
