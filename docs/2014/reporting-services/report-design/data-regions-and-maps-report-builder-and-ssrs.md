---
title: "Data Regions and Maps (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "data regions"
ms.assetid: 3afb8874-b36c-4e44-a0d8-80d2f7135fb1
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Data Regions and Maps (Report Builder and SSRS)
  A data region is an object in a report that displays data from a report dataset. Report data can be displayed as numbers and text in a table, matrix, or list; graphically in a chart or gauge; and against a geographic background in a map. Tables, matrices, and lists are all based on the *tablix* data region, which expands as needed to display all the data from the dataset. A tablix data region supports multiple row and column groups and both static and dynamic rows and columns. A chart displays multiple series and category groups in a variety of chart formats. A gauge displays a single value or an aggregated value for a dataset. A map displays spatial data as map elements that can vary in appearance based on aggregated data from a dataset.  
  
 You can save a data region or map as a report part. [!INCLUDE[ssRBrptparts](../../includes/ssrbrptparts-md.md)]  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## Table  
 A table is a data region that presents data row by row. Table columns are static: you determine the number of columns when you design your report. Table rows are dynamic: they expand downwards to accommodate the data. You can add groups to tables, which organize data by selected fields or expressions. For information about adding a table to a report, see [Tables &#40;Report Builder  and SSRS&#41;](tables-report-builder-and-ssrs.md).  
  
## Matrix  
 A matrix is also known as a crosstab. A matrix data region contains both dynamic columns and rows: they expand to accommodate the data. A matrix can have dynamic columns and rows and static columns and rows. Columns or rows can contain other columns or rows, and can be used to group data. For information about adding a matrix to a report, see [Matrices &#40;Report Builder and SSRS&#41;](create-a-matrix-report-builder-and-ssrs.md)  
  
## List  
 A list is a data region that presents data arranged in a freeform fashion. You can arrange report items to create a form with text boxes, images, and other data regions placed anywhere within the list. For information about adding a list to a report, see [Lists &#40;Report Builder and SSRS&#41;](create-invoices-and-forms-with-lists-report-builder-and-ssrs.md).  
  
## Chart  
 A chart presents data graphically. Examples of charts include bar, pie, and line charts, but many more styles are supported. For information about adding a chart to a report, see [Charts &#40;Report Builder and SSRS&#41;](charts-report-builder-and-ssrs.md).  
  
## Gauge  
 A gauge presents data as a range with an indicator pointing to a specific value within the range. Gauges are used to display key performance indicators (KPIs) and other metrics. Examples of gauges include linear and circular. For more information about adding a gauge to a report, see [Gauges &#40;Report Builder and SSRS&#41;](gauges-report-builder-and-ssrs.md).  
  
## Map  
 A map enables you to present data against a geographical background. Map data can be spatial data from a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] query, an ESRI shapefile, or [!INCLUDE[msCoName](../../includes/msconame-md.md)] Bing map tiles. Spatial data consists of sets of coordinates that define polygons that represent shapes or areas, lines that represent routes or paths, and points represented by markers. You can associate aggregate data with map elements to automatically vary their color and size. For example, you can vary the marker type for a store based on sales amount or the color for a road based on speed limit. For more information, see [Maps &#40;Report Builder and SSRS&#41;](maps-report-builder-and-ssrs.md).  
  
## Data Regions in the Report Layout  
 You can add multiple data regions to a report. Data regions grow to accommodate the data from the report dataset that they are linked to. For example, a matrix that displays sales for each product by year has a row group based on product names and a column group based on years. When you run the report, the matrix expands down the page for each product and across the page for each year. A chart that is placed next to the matrix on the report design surface displays next to the expanded matrix in the rendered report. The way data regions render on a page follows a set of rules based on the output format of a report. For example, to help control how a chart and matrix render on a page, you might use a rectangle as a container or nest both data regions in a list. For more information, see [Page Layout and Rendering &#40;Report Builder and SSRS&#41;](page-layout-and-rendering-report-builder-and-ssrs.md).  
  
## Nested Data Regions  
 You can nest data regions within other data regions. For example, if you want to create a sales record for each sales person in a database, you can create a list with text boxes and an image to display information about the employee, and then add table and chart data regions to the list to show the employee's sales record. For more information, see [Nested Data Regions &#40;Report Builder and SSRS&#41;](nested-data-regions-report-builder-and-ssrs.md).  
  
## Multiple Data Regions Linked to the Same Dataset  
 You can link more than one data region to the same dataset to provide different views of the same data. For example, you can show the same data in a table and in a chart. You can author the report to provide interactive sort buttons on the table, so that when you sort the table, the chart is also automatically sorted. For more information, see [Linking Multiple Data Regions to the Same Dataset &#40;Report Builder and SSRS&#41;](linking-multiple-data-regions-to-the-same-dataset-report-builder-and-ssrs.md).  
  
## Data for a Data Region  
 Each tablix, chart, and gauge is designed to display data from a single dataset. A map displays spatial data and analytical data from the same or different datasets. You can also include values from datasets that are not linked to the data region in the following ways:  
  
-   Aggregate values that do not depend on sort order or grouping that are scoped to a different dataset.  
  
-   Lookup values from name/value pairs in a different dataset.  
  
 For more information, see [Expressions &#40;Report Builder and SSRS&#41;](expressions-report-builder-and-ssrs.md).  
  
## See Also  
 [Report Authoring Concepts &#40;Report Builder and SSRS&#41;](report-authoring-concepts-report-builder-and-ssrs.md)   
 [Reports, Report Parts, and Report Definitions &#40;Report Builder and SSRS&#41;](reports-report-parts-and-report-definitions-report-builder-and-ssrs.md)   
 [Page Layout and Rendering &#40;Report Builder and SSRS&#41;](page-layout-and-rendering-report-builder-and-ssrs.md)   
 [Tutorials &#40;Report Builder&#41;](../report-builder-tutorials.md)   
 [Reporting Services Tutorials &#40;SSRS&#41;](../reporting-services-tutorials-ssrs.md)  
  
  
