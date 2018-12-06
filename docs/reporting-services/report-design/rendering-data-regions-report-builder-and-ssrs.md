---
title: "Rendering Data Regions (Report Builder and SSRS) | Microsoft Docs"
ms.date: 03/07/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-design


ms.topic: conceptual
ms.assetid: 4f3b2c7d-3669-457f-899b-b758d1db3426
author: maggiesMSFT
ms.author: maggies
---
# Rendering Data Regions (Report Builder and SSRS)
  In addition to the general rendering behaviors that apply to all report items, data regions have additional pagination and rendering behaviors that they follow. Data region-specific rendering rules include how a data region grows, how special cells such as the corner cell or header cells are rendered, and how a data region for right-to-left reading is rendered. This topic discusses how the various parts of a data region are rendered.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## Tablix Data Regions  
 The tablix data region, which enables you to create tables, matrices, and lists, is rendered as a grid comprised of columns and rows. The intersection of a row and a column is a cell. When rendered, this cell can contain data or other report items, such as images, rectangles, text boxes, or subreports. A tablix data region can grow vertically and/or horizontally. In addition, the corner cell, the data region header cells, and the data region body cells may grow based on their contents. If the data region spans multiple pages, report items that are set to repeat with the data region are rendered on every page on which the data region is displayed. For more information, see [Tables, Matrices, and Lists &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/tables-matrices-and-lists-report-builder-and-ssrs.md).  
  
### Right to Left  
 A tablix data region set to display from right to left is rendered with its structure as a mirror image of the data region if it were rendered left to right. The corner of the data region appears in the upper right corner. If dynamic columns exist in the report, they expand to the left. Right-to-left settings do not affect the order of the data in the data region; your columns are simply ordered differently.  
  
### Tablix Headers  
 Tablix headers are rendered as a row header or a column header depending on where the header cell appears in the row group hierarchy or the column group hierarchy. If a logical page break exists within the cell contents of a header, it is ignored. Logical page breaks on column groups are ignored.  
  
 Logical page breaks on groups do not cause outer group headers to break. For example, suppose your report has an outer group of country and an inner group of country region. If there is a logical page break between instances of the country region group, the outer group, the country, will appear on both pages of the report.  
  
#### Repeated Tablix Headers  
 When the RepeatWith property is set in the **Properties** pane, items that do not change within the data region, such as column headers, repeat on each page where that part of the data region is rendered. For example, if a row of data appears on the next page and the Repeat With property is set, the column headers appear on the rendered page as well.  
  
### Tablix Corner  
 The upper left corner is called the tablix corner. The Tablix corner can contain other report items within it but, if logical page breaks are inserted in the corner, they are ignored when the Tablix data region is rendered.  
  
### Tablix Body  
 The Tablix body is made up of Tablix cells. The Tablix body is rendered based on pagination rules and the rendering behaviors of report items. For more information, see [Rendering Report Items &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/rendering-report-items-report-builder-and-ssrs.md).  
  
## Chart, Gauge, and Map Data Regions  
 Chart, Gauge, and Map data regions behave like images when they are rendered and displayed in the report body. Values within the data region can have associated actions, such as linking to another report or going to a bookmark, and these actions can be rendered as well, if the renderer supports it.  
  
## See Also  
 [Pagination in Reporting Services &#40;Report Builder  and SSRS&#41;](../../reporting-services/report-design/pagination-in-reporting-services-report-builder-and-ssrs.md)   
 [Rendering Behaviors &#40;Report Builder  and SSRS&#41;](../../reporting-services/report-design/rendering-behaviors-report-builder-and-ssrs.md)   
 [Interactive Functionality for Different Report Rendering Extensions &#40;Report Builder and SSRS&#41;](../../reporting-services/report-builder/interactive-functionality-different-report-rendering-extensions.md)   
 [Rendering Report Items &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/rendering-report-items-report-builder-and-ssrs.md)   
 [Tables, Matrices, and Lists &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/tables-matrices-and-lists-report-builder-and-ssrs.md)   
 [Charts &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/charts-report-builder-and-ssrs.md)   
 [Gauges &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/gauges-report-builder-and-ssrs.md)  
  
  
