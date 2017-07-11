---
title: "Replace a Table or a Named Query in a Data Source View (Analysis Services) | Microsoft Docs"
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
helpviewer_keywords: 
  - "replacing tables"
  - "data source views [Analysis Services], tables"
  - "named queries [Analysis Services], replacing tables"
  - "tables [Analysis Services], data source views"
  - "partitions [Analysis Services], named queries"
ms.assetid: 60c2a018-1299-4915-b60e-e73316524def
caps.latest.revision: 33
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Replace a Table or a Named Query in a Data Source View (Analysis Services)
  In Data Source View Designer, you can replace a table, view, or named query in a data source view (DSV) with a different table or view from the same or a different data source, or with a named query defined in the DSV. When you replace a table, all other objects in an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database or project that have references to the table continue to reference the table because the object ID for the table in the DSV does not change. Any relationships that are still relevant (based on name and column-type matching) are retained. In contrast, if you delete and then add a table, references and relationships are lost and must be recreated.  
  
 To replace a table with another table, you must have an active connection to the source data in Data Source View Designer in project mode.  
  
 You most frequently replace a table in the data source view with another table in the data source. However, you can also replace a named query with a table. For example, you previously replaced a table with a named query, and now want to revert to a table.  
  
> [!IMPORTANT]  
>  If you rename a table in a data source, follow the steps for replacing a table and specify the renamed table as the source of the corresponding table in the DSV before you refresh a DSV. Completing the replacement and renaming process preserves the table, the table's references, and the table's relationships in the DSV. Otherwise, when you refresh the DSV, a renamed table in data source is interpreted as being deleted. For more information, see [Refresh the Schema in a Data Source View &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/refresh-the-schema-in-a-data-source-view-analysis-services.md).  
  
##  <a name="bkmk_nq"></a> Replace a table with a named query  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the project or connect to the database that contains the data source view in which you want to replace a table or a named query.  
  
2.  In Solution Explorer, expand the **Data Source Views** folder, and then double-click the data source view.  
  
3.  Open the **Create Named Query** dialog box. In either **Tables** or **Diagram** pane, right-click the table that you wish to replace, point to **Replace Table** and then click **With New Named Query**.  
  
4.  In the **Create Named Query** dialog box, define the named query and then click **OK**.  
  
5.  Save the modified data source view.  
  
## Replace a table or named query with a table  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the project or connect to the database that contains the data source view in which you want to replace a table or a named query.  
  
2.  In Solution Explorer, expand the **Data Source Views** folder, and then double-click the data source view.  
  
3.  Open the **Replace Table with Other Table** dialog box. In either **Tables** or **Diagram** pane, right-click the table or named query that you wish to replace, point to **Replace Table** and then click **With Other Table**.  
  
4.  In the **Replace Table with Other Table** dialog box:  
  
    1.  In the **DataSource** drop-down list box, select the desired data source  
  
    2.  Select the table with which you wish to replace the table or named query  
  
5.  Click **OK**.  
  
6.  Save the modified data source view.  
  
## See Also  
 [Data Source Views in Multidimensional Models](../../analysis-services/multidimensional-models/data-source-views-in-multidimensional-models.md)  
  
  