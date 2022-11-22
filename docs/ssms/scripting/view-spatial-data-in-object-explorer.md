---
title: "View Spatial Data in Object Explorer"
description: Learn how to use the visual mapping tools of the Query Editor Spatial results window to view spatial data results (either geometric or geographic).
ms.custom: seo-lt-2019
ms.date: "03/01/2017"
ms.service: sql
ms.subservice: ssms
ms.reviewer: ""
ms.topic: conceptual
ms.assetid: 59cca562-e3f5-4257-b868-adcbcc0142cc
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# View Spatial Data in Object Explorer
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]
  The **Spatial results** window in Query Editor provides visual mapping tools for viewing spatial data results in addition to the data displayed in grid format in the **Results** window. To display spatial data in the **Spatial results** window, your query results must contain at least one spatial data column with either geometry or geography data.  
  
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
 [Spatial Results Window](./spatial-results-window.md)   
 [Database Engine Query Editor &#40;SQL Server Management Studio&#41;](../f1-help/database-engine-query-editor-sql-server-management-studio.md)   
 [Query and Text Editors &#40;SQL Server Management Studio&#41;](../f1-help/database-engine-query-editor-sql-server-management-studio.md)  
  
