---
title: "Display the Estimated Execution Plan | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "zoom [SQL Server]"
  - "estimated execution plan [SQL Server]"
  - "displaying execution plans"
  - "viewing execution plans"
  - "execution plans [SQL Server], displaying"
  - "customizing execution plan display [SQL Server]"
  - "modifying execution plan display"
  - "custom zoom [SQL Server]"
ms.assetid: e94aa576-4c0c-4c54-ad05-6c3432cc615b
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Display the Estimated Execution Plan
  This topic describes how to generate graphical estimated execution plans by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. When estimated execution plans are generated, the [!INCLUDE[tsql](../../includes/tsql-md.md)] queries or batches do not execute. Instead, the execution plan that is generated displays the query execution plan that [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] would most probably use if the queries were actually executed.  
  
 To use this feature, users must have the appropriate permissions to execute the [!INCLUDE[tsql](../../includes/tsql-md.md)] query for which a graphical execution plan is being generated, and they must be granted the SHOWPLAN permission for all databases referenced by the query.  
  
### To display the estimated execution plan for a query  
  
1.  On the toolbar, click **Database Engine Query**. You can also open an existing query and display the estimated execution plan by clicking the **Open File** toolbar button and locating the existing query.  
  
2.  Enter the query for which you would like to display the estimated execution plan.  
  
3.  On the **Query** menu, click **Display Estimated Execution Plan** or click the **Display Estimated Execution Plan** toolbar button. The estimated execution plan is displayed on the **Execution Plan** tab in the results pane. To view additional information, pause the mouse over the logical and physical operator icons and view the description and properties of the operator in the displayed ToolTip. Alternatively, you can view operator properties in the Properties window. If Properties is not visible, right-click an operator and click **Properties**. Select an operator to view its properties.  
  
4.  To alter the display of the execution plan, right-click the execution plan and select **Zoom In**, **Zoom Out**, **Custom Zoom**, or **Zoom to Fit**. **Zoom In** and **Zoom Out** allow you to magnify or reduce the execution plan by fixed amounts. **Custom Zoom** allows you to define your own display magnification, such as zooming at 80 percent. **Zoom to Fit** magnifies the execution plan to fit the result pane.  
  
  
