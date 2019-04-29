---
title: "Data Source View (Cube Structure Tab, Cube Designer) (Analysis Services - Multidimensional Data) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.cubeeditor.cubebuilder.datasourcepane.f1"
ms.assetid: 1e39c910-5c10-4624-be27-ca02a461b46b
author: minewiskan
ms.author: owend
manager: craigg
---
# Data Source View (Cube Structure Tab, Cube Designer) (Analysis Services - Multidimensional Data)
  Use the **Data Source View** pane to view tables and columns from the data source view associated with the selected cube. This pane is used to create measure groups and measures by dragging columns from the **Data Source View** pane to the **Measures** pane.  
  
## Options  
 **Data Source View**  
 Displays the data source view associated with the selected cube.  
  
 **(Move viewpoint)**  
 Click the lower right corner of the pane, between the scrollbars, to select an area of the **Data Source View** pane to view.  
  
## Diagram Context Menu  
 The following options are available in the context menu displayed by right-clicking the diagram background of the **Data Source View** pane:  
  
 **Show Tables**  
 Displays the **Show Table** dialog box. For more information about the **Show Table** dialog box, see [Show Table Dialog Box &#40;Analysis Services - Multidimensional Data&#41;](show-table-dialog-box-analysis-services-multidimensional-data.md).  
  
 **Show All Tables**  
 Displays in the pane all tables included in the data source view associated with the cube.  
  
 **Show Only Used Tables**  
 Displays in the pane only those tables used by the cube from the associated data source view.  
  
 **Show Friendly Names**  
 Selects to show friendly names for the objects in the pane.  
  
 **Select All**  
 Selects all of the objects in the pane.  
  
 **Find Table**  
 Displays the **Find Table** dialog box. For more information about the **Find Table** dialog box, see [Find Table Dialog Box &#40;Analysis Services - Multidimensional Data&#41;](find-table-dialog-box-analysis-services-multidimensional-data.md).  
  
 **Arrange Tables**  
 Arranges the objects in the pane according to the layout specified by selecting either **Switch to Diagonal Layout** or **Switch to Rectangular Layout**.  
  
 **Switch to Diagonal Layout**  
 Select to arrange objects in a diagonal pattern.  
  
> [!NOTE]  
>  This option is displayed only if **Switch to Rectangular Layout** is selected.  
  
 **Switch to Rectangular Layout**  
 Select to arrange objects in a rectangular pattern.  
  
> [!NOTE]  
>  This option is displayed only if **Switch to Diagonal Layout** is selected.  
  
 **Edit Data Source View**  
 Displays Data Source View Designer for the data source view associated with the object. For more information about Data Source View Designer, see [Data Source View Designer &#40;Analysis Services - Multidimensional Data&#41;](data-source-view-designer-analysis-services-multidimensional-data.md).  
  
 **Show Data Source View In**  
 Select one of the following options to toggle between viewing the **Data Source View** pane in the following modes:  
  
-   Diagram  
  
     Displays a diagram of the tables and columns associated with the current cube.  
  
-   Tree  
  
     Displays a tree view containing the tables and columns associated with the current cube.  
  
 **Copy Diagram From**  
 Select one of the diagrams included in the data source view associated with the cube to display it in the **Data Source View** pane.  
  
 **Zoom**  
 Select an available zoom option.  
  
 **Properties**  
 Displays the **Properties** window in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] for the data source view associated with the cube.  
  
## Table Context Menu  
 The following options are available in the context menu displayed by right-clicking the name of a table or view in the **Data Source View** pane:  
  
 **Show Related Tables**  
 Displays in the pane the tables related to the selected table in the data source view.  
  
 **Hide Table**  
 Removes the table from the pane.  
  
 **Explore Data**  
 Display the **Explore Data** dialog box for the selected table.  
  
 **Edit Data Source View**  
 Displays Data Source View Designer for the data source view that contains the selected table. For more information about Data Source View Designer, see [Data Source View Designer &#40;Analysis Services - Multidimensional Data&#41;](data-source-view-designer-analysis-services-multidimensional-data.md).  
  
 **New Measure Group from Table**  
 Defines a new measure group in the **Measures** pane based on the selected table.  
  
> [!NOTE]  
>  This option is enabled only if the table is not yet referenced by a measure group in the **Measures** pane.  
  
 **Properties**  
 Displays the **Properties** window in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] for the selected table.  
  
## Column Context Menu  
 The following options are available in the context menu displayed by right-clicking the name of a column in a table or view in the **Data Source View** pane:  
  
 **New Measure from Column**  
 Defines a new measure in the **Measures** pane based on the selected column.  
  
 **Explore Data**  
 Display the **Explore Data** dialog box for the table that contains the selected column.  
  
 **Edit Data Source View**  
 Displays Data Source View Designer for the data source view that contains the selected column. For more information about Data Source View Designer, see [Data Source View Designer &#40;Analysis Services - Multidimensional Data&#41;](data-source-view-designer-analysis-services-multidimensional-data.md).  
  
 **Properties**  
 Displays the **Properties** window in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] for the selected column.  
  
## Relationship Context Menu  
 The following options are available in the context menu displayed by right-clicking a relationship in the **Data Source View** pane:  
  
 **Edit Data Source View**  
 Displays Data Source View Designer for the data source view that contains the selected relationship. For more information about Data Source View Designer, see [Data Source View Designer &#40;Analysis Services - Multidimensional Data&#41;](data-source-view-designer-analysis-services-multidimensional-data.md).  
  
 **Properties**  
 Displays the **Properties** window in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] for the selected relationship.  
  
## See Also  
 [Toolbar &#40;Cube Structure Tab, Cube Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](toolbar-cube-structure-cube-designer-analysis-services-multidimensional-data.md)   
 [Measures &#40;Cube Structure Tab, Cube Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](measures-cube-structure-cube-designer-analysis-services-multidimensional-data.md)   
 [Dimensions &#40;Cube Structure Tab, Cube Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](dimensions-cube-structure-cube-designer-analysis-services-multidimensional-data.md)   
 [Cube Structure &#40;Cube Designer&#41; &#40;Analysis Services - Multidimensional Data&#41;](cube-structure-cube-designer-analysis-services-multidimensional-data.md)  
  
  
