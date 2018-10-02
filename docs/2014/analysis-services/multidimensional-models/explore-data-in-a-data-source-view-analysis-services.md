---
title: "Explore Data in a Data Source View (Analysis Services) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "exploring data [Analysis Services]"
  - "data source views [Analysis Services], exploring data"
  - "viewing source data"
ms.assetid: 2c922c35-fbcb-45b2-96b1-c7a846d8b419
author: minewiskan
ms.author: owend
manager: craigg
---
# Explore Data in a Data Source View (Analysis Services)
  You can use the **Explore Data** dialog box in Data Source View Designer in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] to browse data for a table, view, or named query in a data source view (DSV). When you explore the data in Data Source View Designer, you can view the contents of each column of data in a selected table, view, or named query. Viewing the actual contents assists you in determining whether all columns are needed, if named calculations are required to increase user friendliness and usability, and whether existing named calculations or named queries return the anticipated values.  
  
 To view data, you must have an active connection to the data source or sources for the selected object in the DSV. Any named calculations in a table are also sent in the query.  
  
 The data returned in a tabular format that you can sort and copy. Click on the column headers to re-sort the rows by that column. You can also highlight data in the grid and press Ctrl-C to copy the selection to the clipboard.  
  
 You can also control the sample method and the sample count. By default, the top 5000 rows are returned.  
  
## To browse data or change sampling options  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the project or connect to the database that contains the data source view in which you want to browse data.  
  
2.  In Solution Explorer, expand the **Data Source Views** folder, and then double-click the data source view.  
  
3.  Right-click the table, view, or named query that contains the data you want to view, and then click **Explore Data**.  
  
     The data source underlying the table, view, or named query in the data source view are queries, and the results appear in the **Explore \<object name> Table** tab.  
  
4.  On the **Explore \<object name> Table** toolbar, click the **Sampling options** icon.  
  
     The **Data Exploration Options** dialog box opens. In this dialog box you can specify the sampling method (fewer or more records than the default sampling size of 5000 rows), or sample count.  
  
5.  Click **OK** or **Cancel** as appropriate.  
  
6.  To resample the data, click **Resample Data** on the **Explore \<object name> Table** toolbar.  
  
## See Also  
 [Data Source Views in Multidimensional Models](data-source-views-in-multidimensional-models.md)  
  
  
