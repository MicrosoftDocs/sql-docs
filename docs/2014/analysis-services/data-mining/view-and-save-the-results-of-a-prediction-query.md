---
title: "View and Save the Results of a Prediction Query | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "prediction queries [Analysis Services]"
  - "viewing prediction query results"
  - "displaying prediction query results"
  - "Mining Model Prediction [Analysis Services], viewing results"
ms.assetid: abba4d24-3619-44c1-8279-88f27ad627d3
author: minewiskan
ms.author: owend
manager: craigg
---
# View and Save the Results of a Prediction Query
  After you have defined a query in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] by using Prediction Query Builder, you can run the query and view the results by switching to the query result view.  
  
 You can save the results of a prediction query to a table in any data source that is defined in a [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project. You can either create a new table or save the query results to an existing table. If you save the results to an existing table, you can choose to overwrite the data that is currently stored in the table; otherwise, the query results are appended to the existing data in the table.  
  
### Run a query and view the results  
  
1.  On the toolbar of the **Mining Model Prediction** tab of Data Mining Designer in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], click **Result** .  
  
     The query results view opens and runs the query. The results are displayed in a grid in the viewer.  
  
### Save the results of a prediction query to a table  
  
1.  On the toolbar of the **Mining Model Prediction** tab in Data Mining Designer, click **Save query result**.  
  
     The **Save Data Mining Query Result** dialog box opens.  
  
2.  Select a data source from the **Data Source** list, or click **New** to create a new data source.  
  
3.  In the **Table Name** box, enter the name of the table. If the table already exists, select **Overwrite if exists** to replace the contents of the table with the query results. If you do not want to overwrite the contents of the table, do not select this check box. The new query results will be appended to the existing data in the table.  
  
4.  Select a data source view from **Add to DSV** if you want to add the table to a data source view.  
  
5.  Click **Save**.  
  
    > [!WARNING]  
    >  If the destination does not support hierarchical rowsets, you can add the FALTTENED keyword to the results to save as a flat table.  
  
  
