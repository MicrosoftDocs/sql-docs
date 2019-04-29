---
title: "Define Logical Relationships in a Data Source View (Analysis Services) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "adding relationships"
  - "relationships [Analysis Services], data source views"
  - "data source views [Analysis Services], relationships"
ms.assetid: a20d6dae-e769-4131-8a59-7ef56f174220
author: minewiskan
ms.author: owend
manager: craigg
---
# Define Logical Relationships in a Data Source View (Analysis Services)
  The Data Source View Wizard and Data Source View Designer automatically define relationships between tables added to a data source view (DSV) based on underlying database relationships or name matching criteria you specify.  
  
 In cases where you are working with data from multiple data sources, you may need to manually define logical relationships in the DSV to supplement those relationships that are defined automatically. Relationships are required in [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] to identify fact and dimension tables, to construct queries for retrieving data and metadata from underlying data sources, and to take advantage of advanced business intelligence features.  
  
 You can define the following types of relationships in Data Source View Designer:  
  
-   A relationship from one table to another table in the same data source.  
  
-   A relationship from one table to itself, as in a parent-child relationship.  
  
-   A relationship from one table in a data source to another table in a different data source.  
  
> [!NOTE]  
>  The relationships defined in a DSV are logical and may not reflect the actual relationships defined in the underlying data source. You can create relationships in Data Source View Designer that do not exist in the underlying data source, and remove relationships created by Data Source View Designer from existing foreign key relationships in the underlying data source.  
  
 Relationships are directed. For every value in the source column, there is a corresponding value in the destination column. In a data source view diagram, such as the diagrams displayed in the **Diagram** pane, an arrow on the line between two tables indicates the direction of the relationship.  
  
 This topic includes the following sections:  
  
 [To add a relationship between tables, named queries, or views](#bkmk_addRel)  
  
 [To view or modify a relationship in the Diagram pane](#bkmk_diagrampane)  
  
 [To view or modify a relationship in the Tables pane](#bkmk_tablespane)  
  
##  <a name="bkmk_addRel"></a> To add a relationship between tables, named queries, or views  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the project or connect to the database that contains the data source view in which you wish to add a logical relationship.  
  
2.  In Solution Explorer, expand the **Data Source Views** folder, then double-click the data source view to open it in **Data Source View Designer**.  
  
3.  Right-click the table, named query or view to which you want to add a relationship in either the **Tables** pane and then click **New Relationship**.  
  
    > [!NOTE]  
    >  To locate a table, view or named query, you can use the **Find Table** option by either clicking the **Data Source View** menu or right-clicking in an open area of the **Tables** or **Diagram** panes.  
  
4.  In the **Specify Relationship** dialog box, do the following:  
  
    1.  Select the appropriate table, named query, or view in the **Source (foreign key) table** list.  
  
    2.  Select the appropriate table, named query, or view in the **Destination (primary key) table** lists.  
  
    3.  Select columns from the **Source Columns** and **Destination Columns** lists to create a relationship between the two tables.  
  
         If [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] detects, by sampling the data in the underlying table, view, or named query, that you have defined the relationship in the wrong direction (from the primary key to the foreign key rather than from the foreign key to the primary key), you will be prompted to reverse the order. To quickly reverse the order, click **Reverse**.  
  
         If [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] detects that a relationship already exists for the columns you have selected, you will be prompted. You cannot define duplicate relationships.  
  
    4.  Optionally, in the **Description** box, type a description for the relationship.  
  
##  <a name="bkmk_diagrampane"></a> To view or modify a relationship in the Diagram pane  
  
-   In the **Diagram** pane in **Data Source View Designer**, right-click the relationship that you want to view and click **Edit Relationship** (or simply double-click the relationship arrow).  Use the **Edit Relationship** dialog box to modify the relationship.  
  
##  <a name="bkmk_tablespane"></a> To view or modify a relationship in the Tables pane  
  
1.  In the **Tables** pane in **Data Source View Designer**, locate and then expand the table, view or named query containing the relationship that you wish to view or modify.  
  
2.  Expand the **Relationships** folder.  The relationships between the selected table, view or named query and other tables, views and named queries appear with the relationship column listed.  
  
3.  Right-click the relationship you want to modify, and then click **Edit Relationship**.  
  
## See Also  
 [Data Source Views in Multidimensional Models](data-source-views-in-multidimensional-models.md)  
  
  
