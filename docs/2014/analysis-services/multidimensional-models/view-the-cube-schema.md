---
title: "View the Cube Schema | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 82fc715c-e08e-447d-8fc8-9c9005f145f0
author: minewiskan
ms.author: owend
manager: craigg
---
# View the Cube Schema
  The **Data Source View** pane of the **Cube Structure** tab in **Cube Designer** displays the cube schema. The schema is the set of tables from which the measures and dimensions for a cube are derived. Every cube schema consists of one or more fact tables and one or more dimension tables on which the measures and dimensions in the cube are based.  
  
 The **Data Source View** pane of the **Cube Structure** tab displays a diagram of the data source view on which the cube is based. This diagram is a subset of the main diagram of the data source view. You can hide and show tables in the **Data Source View** pane and view any existing diagrams. However, you cannot make changes (such as adding new relationships or named queries) to the underlying schema. To make changes to the schema, use Data Source View Designer.  
  
 When you create a cube, the diagram displayed in the **Data Source View** pane of the **Cube Structure** tab is initially the same as the **Show All Tables** diagram in the data source view for the project or database. You can replace this diagram with any existing diagram in the data source view and make adjustments in the **Data Source View** pane.  
  
 While you work with the diagram in **Cube Designer**, commands that act on the tab or on any selected object in the tab are available on the **Data Source View** menu. You can also right-click the background of the diagram or any object in the diagram to use commands that act on the diagram or selected object. You can:  
  
-   Switch between diagram and tree formats.  
  
-   Arrange, find, show, and hide tables.  
  
-   Show friendly names.  
  
-   Switch layouts.  
  
-   Change the magnification.  
  
-   View properties.  
  
 Additionally, you can perform the actions listed in the following table:  
  
|To|Do this|  
|--------|-------------|  
|Use a diagram from the data source view of the cube|On the **Data Source View** menu, point to **Copy Diagram from**, and then click the data source view diagram you want to use.<br /><br /> - or -<br /><br /> Right-click the background of the **Data Source View** pane, point to **Copy Diagram from**, and then click the diagram in the data source view that you want. This method creates an independent copy of the diagram, so any changes you make on the **Cube Builder** tab do not appear in the original diagram.|  
|Show only the tables that are used in the cube|On the **Data Source View** menu, click **Show Only Used Tables**.<br /><br /> - or -<br /><br /> Right-click the background of the **Data Source View** pane, and then click **Show Only Used Tables**.|  
|Edit the data source view schema|On the **Data Source View** menu, click **Edit Data Source View**.<br /><br /> - or -<br /><br /> Right-click the background of the **Data Source View** pane, and then click **Edit Data Source View**.|  
  
  
