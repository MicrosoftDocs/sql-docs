---
title: "Add a Data Viewer to a Data Flow | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "data viewers [Integration Services]"
  - "data flow [Integration Services], data viewers"
  - "adding data viewers"
ms.assetid: 5e573274-a170-4132-bfc8-a8ff3a8411e4
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Add a Data Viewer to a Data Flow
  This topic describes how to add and configure a data viewer in a data flow. A data viewer displays data that is moving between two data flow components. For example, a data viewer can display the data that is extracted from a data source before a transformation in the data flow modifies the data.  
  
 A path connects components in a data flow by connecting the output of one data flow component to the input of another component.  
  
 Before you can add data viewers to a package, the package must include a Data Flow task and at least two data flow components that are connected.  
  
### To add a data viewer to a data flow  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  In Solution Explorer, double-click the package to open it.  
  
3.  Click the **Control Flow** tab, if it is not already active.  
  
4.  Click the Data Flow task to whose data flow you want to attach a data viewer and then click the **Data Flow** tab.  
  
5.  Right-click a path between two data flow components, and click **Edit**.  
  
6.  On the **General** page, you can view and edit path properties. For example, from the **PathAnnotation** drop-down list you can select the annotation that appears next to the path.  
  
7.  On the **Metadata** page, you can view the column metadata and copy the metadata to the Clipboard.  
  
8.  On the **Data Viewer** page, click **Enable data viewer**.  
  
9. In the Columns to display area, select the columns you want to display in the data viewer. By default, all the available columns are selected and listed in the **Displayed Columns** list. Move columns that you do not want to use to the **Unused Column** list by selecting them and then clicking the left arrow.  
  
    > [!NOTE]  
    >  In the grid, values that represent the DT_DATE, DT_DBTIME2, DT_FILETIME, DT_DBTIMESTAMP, DT_DBTIMESTAMP2, and DT_DBTIMESTAMPOFFSET data types appear as ISO 8601 formatted strings and a space separator replaces the `T` separator. Values that represent the DT_DATE and DT_FILETIME data types include seven digits for fractional seconds. Because the DT_FILETIME data type stores only three digits of fractional seconds, the grid displays zeros for the remaining four digits. Values that represent the DT_DBTIMESTAMP data type include three digits for fractional seconds. For values that represent the DT_DBTIME2, DT_DBTIMESTAMP2, and DT_DBTIMESTAMPOFFSET data types, the number of digits for fractional seconds corresponds to the scale specified for the column's data type. For more information about ISO 8601 formats, see [Date and Time Formats](../../2014/integration-services/date-and-time-formats.md). For more information about data types, see [Integration Services Data Types](data-flow/integration-services-data-types.md).  
  
10. Click **OK**.  
  
## See Also  
 [Integration Services Transformations](data-flow/transformations/integration-services-transformations.md)   
 [Integration Services Paths](data-flow/integration-services-paths.md)   
 [Data Flow](data-flow/data-flow.md)   
 [Debugging Data Flow](troubleshooting/debugging-data-flow.md)  
  
  
