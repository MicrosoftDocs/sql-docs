---
title: "Debugging Data Flow | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "progress reporting [Integration Services]"
  - "data viewers [Integration Services]"
  - "data flow [Integration Services], debugging"
  - "debugging [Integration Services], data flow"
  - "counting rows"
ms.assetid: 1c574f1b-54f7-4c05-8e42-8620e2c1df0f
author: janinezhang
ms.author: janinez
manager: craigg
---
# Debugging Data Flow
  [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] and the [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer include features and tools that you can use to troubleshoot the data flows in an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package.  
  
-   [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer provides data viewers.  
  
-   [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer and [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] transformations provide row counts.  
  
-   [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer provides progress reporting at run time.  
  
## Data Viewers  
 Data viewers display data between two components in a data flow. Data viewers can display data when the data is extracted from a data source and first enters a data flow, before and after a transformation updates the data, and before the data is loaded into its destination.  
  
 To view the data, you attach data viewers to the path that connects two data flow components. The ability to view data between data flow components makes it easier to identify unexpected data values, view the way a transformation changes column values, and discover the reason that a transformation fails. For example, you may find that a lookup in a reference table fails, and to correct this you may want to add a transformation that provides default data for blank columns.  
  
 A data viewer can display data in a grid. Using a grid, you select the columns to display. The values for the selected columns display in a tabular format.  
  
 You can also include multiple data viewers on a path. You can display the same data in different formats-for example, create a chart view and a grid view of the data-or create different data viewers for different columns of data.  
  
 When you add a data viewer to a path, [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer adds a data viewer icon to the design surface of the **Data Flow** tab, next to the path. Transformations that can have multiple outputs, such as the Conditional Split transformation, can include a data viewer on each path.  
  
 At run time, a **Data Viewer** window opens and displays the information specified by the data viewer format. For example, a data viewer that uses the grid format shows data for the selected columns, the number of output rows passed to the data flow component, and the number of rows displayed. The information displays buffer by buffer and, depending on the width of the rows in the data flow, a buffer may contain more or fewer rows.  
  
 In the **Data Viewer** dialog box, you can copy the data to the Clipboard, clear all data from the table, reconfigure the data viewer, resume the flow of data, and detach or attach the data viewer.  
  
#### To add a data viewer  
  
-   [Add a Data Viewer to a Data Flow](../add-a-data-viewer-to-a-data-flow.md)  
  
## Row Counts  
 The number of rows that have passed through a path is displayed on the design surface of the **Data Flow** tab in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer next to the path. The number is updated periodically while the data moves through the path.  
  
 You can also add a Row Count transformation to the data flow to capture the final row count in a variable. For more information, see [Row Count Transformation](../data-flow/transformations/row-count-transformation.md).  
  
## Progress Reporting  
 When you run a package, [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer depicts progress on the design surface of the **Data Flow** tab by displaying each data flow component in a color that indicates status. When each component starts to perform its work, it changes from no color to yellow, and when it finishes successfully, it changes to green. A red color indicates that the component failed.  
  
 The following table describes the color-coding.  
  
|Color|Description|  
|-----------|-----------------|  
|No color|Waiting to be called by the data flow engine.|  
|Yellow|Performing a transformation, extracting data, or loading data.|  
|Green|Ran successfully.|  
|red|Ran with errors.|  
  
## See Also  
 [Troubleshooting Tools for Package Development](troubleshooting-tools-for-package-development.md)  
  
  
