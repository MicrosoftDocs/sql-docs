---
title: "View Spatial Data in Object Explorer | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
ms.assetid: 59cca562-e3f5-4257-b868-adcbcc0142cc
author: MightyPen
ms.author: genemi
manager: craigg
---
# View Spatial Data in Object Explorer
  The **Spatial results** window in Query Editor provides visual mapping tools for viewing spatial data results in addition to the data displayed in grid format in the **Results** window. To display spatial data in the **Spatial Results** window, your query results must contain at least one spatial data column with either geometry or geography data.  
  
### To view spatial data in the Spatial results window  
  
1.  In Query Editor, click the **Spatial results** tab.  
  
    > [!NOTE]  
    >  This window is not available if your query results do not contain spatial data or if you specify that your results are returned as text.  
  
2.  Select the column you want to view from the **Select spatial column** list. If there is only one spatial column in your table, this column is the default option in the list.  
  
3.  Select the non-spatial column you want to use as data labels from the **Select label columns** list.  
  
4.  Select the projection you want for geography data from the **Select projection** list. The default projection is Equirectangular; other projections available are Mercator, Robinson, or Bonne.  
  
    > [!NOTE]  
    >  **Select projection** is not available if your spatial column contains geometry data.  
  
5.  Adjust the **Zoom** slider to increase the visual size of mapped elements. For polygon shapes, the label is visible only when the shape is large enough to hold the label text.  
  
## See Also  
 [Spatial Results Window](spatial-results-window.md)   
 [Database Engine Query Editor &#40;SQL Server Management Studio&#41;](database-engine-query-editor-sql-server-management-studio.md)   
 [Query and Text Editors &#40;SQL Server Management Studio&#41;](query-and-text-editors-sql-server-management-studio.md)  
  
  
