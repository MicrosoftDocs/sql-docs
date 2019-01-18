---
title: "Query Pane (Mining Model Prediction View) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dm.miningmodeleditor.prediction.query.f1"
ms.assetid: fdeec72e-d0bd-4453-9eaa-46436e4d6edc
author: minewiskan
ms.author: owend
manager: craigg
---
# Query Pane (Mining Model Prediction View)
  The **Query** pane displays the Data Mining Expressions (DMX) statements created by Prediction Query Builder. You can modify the statements and then click the **Switch to query result view** button to return the results. If you switch back to the **Design** view, any changes you made to the statement will be lost.  
  
 **For More Information:** [Data Mining Extensions &#40;DMX&#41; Data Manipulation Statements](/sql/dmx/dmx-statements-data-manipulation), [Create a DMX Query in SQL Server Management Studio](data-mining/create-a-dmx-query-in-sql-server-management-studio.md)  
  
## Options  
 **Switch to query result view**  
 Click to switch between the **Design**, **Query**, and **Result** panes. Switching to the **Result** pane runs the query.  
  
 **Save the query result**  
 Opens the **Save Data Mining Query Result** dialog box.  
  
 **Singleton query**  
 Lets you create a singleton query, in which you provide the input data directly in your query instead of providing a reference to a table of input values. The **Select Input Table(s)** table is replaced by a **Singleton Query Input** table. The current prediction query will be lost if you switch to a singleton query.  
  
 **Refresh query results**  
 Reprocesses the prediction query. This is enabled only in the **Result** pane.  
  
## See Also  
 [Data Mining Query Interfaces](data-mining/data-mining-query-tools.md)   
 [Data Mining Extensions &#40;DMX&#41; Statement Reference](/sql/dmx/data-mining-extensions-dmx-statements)   
 [Prediction Query Builder &#40;Data Mining&#41;](prediction-query-builder-data-mining.md)  
  
  
