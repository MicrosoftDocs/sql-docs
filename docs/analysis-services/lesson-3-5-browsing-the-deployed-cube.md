---
title: "Browsing the Deployed Cube | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
ms.tgt_pltfrm: ""
ms.topic: "get-started-article"
applies_to: 
  - "SQL Server 2016"
ms.assetid: 849c6109-1453-4fe4-a892-c49a982cfadb
caps.latest.revision: 24
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Lesson 3-5 - Browsing the Deployed Cube
In the following task, you browse the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] Tutorial cube. Because our analysis compares measure across multiple dimensions, you will use an Excel PivotTable to browse your data. Using a PivotTable lets you place customer, date, and product information on different axes so that you can see how Internet Sales change when viewed across specific time periods, customer demographics, and product lines.  
  
### To browse the deployed cube  
  
1.  To switch to Cube Designer in [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)], double-click the **[!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] Tutorial** cube in the **Cubes** folder of the Solution Explorer.  
  
2.  Open the **Browser** tab, and then click the **Reconnect** button on the toolbar of the designer.  
  
3.  Click the Excel icon to launch Excel using the workspace database as the data source. When prompted to enable connections, click **Enable**.  
  
4.  In the PivotTable Field List, expand **Internet Sales**, and then drag the **Sales Amount** measure to the **Values** area.  
  
5.  In the PivotTable Field List, expand **Product**.  
  
6.  Drag the **Product Model Lines** user hierarchy to the **Columns** area.  
  
7.  In the PivotTable Field List, expand **Customer**, expand **Location**, and then drag the **Customer Geography** hierarchy from the Location display folder in the Customer dimension to the **Rows** area.  
  
8.  In the PivotTable Field List, expand **Order Date**, and then drag the **Order Date.Calendar Date** hierarchy to the **Report Filter** area.  
  
9. Click the arrow to the right of the **Order Date.Calendar Date** filter in the data pane, clear the check box for the **(All)** level, expand **2006**, expand **H1 CY 2006**, expand **Q1 CY 2006**, select the check box for **February 2006**, and then click **OK**.  
  
    Internet sales by region and product line for the month of February, 2006 appear as shown in the following image.  
  
    ![Internet sales by region and product line](../analysis-services/media/l3-cube-browser-finish.gif "Internet sales by region and product line")  
  
## Next Lesson  
[Lesson 4: Defining Advanced Attribute and Dimension Properties](../analysis-services/lesson-4-defining-advanced-attribute-and-dimension-properties.md)  
  
  
  
