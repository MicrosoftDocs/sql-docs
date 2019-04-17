---
title: "Defining a Data Source View | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: af00938a-5a06-4fae-b2fc-f3fb0ca3cea5
author: minewiskan
ms.author: owend
manager: craigg
---
# Defining a Data Source View
  After you define the data sources that you will use in an [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] project, the next step is generally to define a data source view for the project. A data source view is a single, unified view of the metadata from the specified tables and views that the data source defines in the project. Storing the metadata in the data source view enables you to work with the metadata during development without an open connection to any underlying data source. For more information, see [Data Source Views in Multidimensional Models](multidimensional-models/data-source-views-in-multidimensional-models.md).  
  
 In the following task, you define a data source view that includes five tables from the **AdventureWorksDW2012** data source.  
  
### To define a new data source view  
  
1.  In Solution Explorer (on the right of the Microsoft Visual Studio window), right-click **Data Source Views**, and then click **New Data Source View**.  
  
2.  On the **Welcome to the Data Source View Wizard** page, click **Next**. The **Select a Data Source** page appears.  
  
3.  Under **Relational data sources**, the **Adventure Works DW 2012** data source is selected. Click **Next**.  
  
    > [!NOTE]  
    >  To create a data source view that is based on multiple data sources, first define a data source view that is based on a single data source. This data source is then called the primary data source. You can then add tables and views from a secondary data source. When designing dimensions that contain attributes based on related tables in multiple data sources, you might need to define a [!INCLUDE[msCoName](../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] data source as the primary data source to use its distributed query engine capabilities.  
  
4.  On the **Select Tables and Views** page, select tables and views from the list of objects that are available from the selected data source. You can filter this list to help you select tables and views.  
  
    > [!NOTE]  
    >  Click the maximize button in the upper-right corner so that the window covers the full screen. This makes it easier to see the complete list of available objects.  
  
     In the **Available objects** list, select the following objects. You can select multiple tables by clicking each while holding down the CTRL key:  
  
    -   **DimCustomer (dbo)**  
  
    -   **DimDate (dbo)**  
  
    -   **DimGeography (dbo)**  
  
    -   **DimProduct (dbo)**  
  
    -   **FactInternetSales (dbo)**  
  
5.  Click **>** to add the selected tables to the **Included objects** list.  
  
6.  Click **Next.**  
  
7.  In the Name field, make sure **Adventure Works DW 2012** displays, and then click **Finish**.  
  
     The **Adventure Works DW 2012** data source view appears in the **Data Source Views** folder in Solution Explorer. The content of the data source view is also displayed in Data Source View Designer in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)]. This designer contains the following elements:  
  
    -   A **Diagram** pane in which the tables and their relationships are represented graphically.  
  
    -   A **Tables** pane in which the tables and their schema elements are displayed in a tree view.  
  
    -   A **Diagram Organizer** pane in which you can create subdiagrams so that you can view subsets of the data source view.  
  
    -   A toolbar that is specific to Data Source View Designer.  
  
8.  To maximize the [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[vsprvs](../includes/vsprvs-md.md)] development environment, click the **Maximize** button.  
  
9. To view the tables in the **Diagram** pane at 50 percent, click the **Zoom** icon on the Data Source View Designer toolbar. This will hide the column details of each table.  
  
10. To hide Solution Explorer, click the **Auto Hide** button, which is the pushpin icon on the title bar. To view Solution Explorer again, position your pointer over the Solution Explorer tab along the right side of the development environment. To unhide Solution Explorer, click the **Auto Hide** button again.  
  
11. If the windows are not hidden by default, click **Auto Hide** on the title bar of the Properties and Solution Explorer windows.  
  
     You can now view all the tables and their relationships in the **Diagram** pane. Notice that there are three relationships between the FactInternetSales table and the DimDate table. Each sale has three dates associated with the sale: an order date, a due date, and a ship date. To view the details of any relationship, double-click the relationship arrow in the **Diagram** pane.  
  
## Next Task in Lesson  
 [Modifying Default Table Names](lesson-1-4-modifying-default-table-names.md)  
  
## See Also  
 [Data Source Views in Multidimensional Models](multidimensional-models/data-source-views-in-multidimensional-models.md)  
  
  
