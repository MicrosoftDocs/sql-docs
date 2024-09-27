---
title: "Design view"
description: Learn how to use the Design view in the Reporting Services Report Designer to arrange report items in a report.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: tools
ms.topic: conceptual
ms.custom:
  - updatefrequency5
f1_keywords:
  - "sql13.rtp.rptdesigner.layoutview.f1"
helpviewer_keywords:
  - "Layout View dialog box"
---
# Design view
In [!INCLUDE[ssRSnoversion_md](../../includes/ssrsnoversion-md.md)] Report Designer, use Design view to arrange report items in the report. Design view is sometimes called the design surface or layout view.  
  :::image type="content" source="../../reporting-services/media/ssrs-ssdt-preview.png" alt-text="Screenshot that shows the Preview button.":::

## Report design surface  
The design surface consists of three sections: 
+ report body
+ page header
+ page footer 

Use the Toolbox to select items to place in any of these three sections. Use the Report Data pane to view images, parameters, data sources, and datasets, including dataset queries and field lists. After you add report items to the design surface, drag dataset fields from the **Report Data** pane onto data regions such as table, matrix, or list. Each item on the report design surface contains properties that can be managed by using a properties dialog box or the Properties pane.  
  
## Toolbox  
 The Toolbox lists data regions and other report items that are available for your report. To add report items from the Toolbox, double-click the item or drag it to the design surface. You can then change the shape and size by using the object handles.  
  
## Report Data pane  
 To view the Report Data pane, on the **View** menu, select **Report Data**. Use this pane to define parameters, images, data sources, and datasets, and to reference built-in fields such as ReportName. To add a new item, select the **New** menu and choose an item. To add calculated fields to an existing dataset, select **Dataset**, and in the **Dataset Properties** dialog box, choose **Fields**. Choose an item and select **Edit** to open the **Properties** dialog box. You can also right-click items in the Report Data pane to add items or update their properties.  
  
 Drag items from the Report Data pane to data regions and text boxes on the design surface to add data and images to a report.  
  
 For more information, see [Report Data pane](../../reporting-services/report-data/report-data-pane.md).  
  
## Grouping pane  
 Groups are used to organize your report data into a visual hierarchy and to calculate totals. Use the Grouping pane to view the groups defined for a table, matrix, or list data region. By default, the Grouping pane displays all the groups for the selected data region as a flattened list. The Grouping pane is disabled for Chart and Gauge data regions.  
  
 To see the groups in relationship to one another, toggle the Grouping pane to Advanced mode. This mode displays the hierarchy of group members, a visual display of cells in the data region that correspond to each group.  
  
 For more information, see [Grouping pane](../../reporting-services/tools/grouping-pane.md).  
  
## Page header and page footer  
 A page header and page footer run along the top and bottom of each page, respectively. Headers and footers can contain static text, images, lines, rectangles, borders, background color, and background images. To add report items to the header or footer, right-click the design surface and select Header or Footer. Header and footer sections appear on the design surface.  
  
## Properties pane  
 Use the Properties pane to view properties for the currently selected report item on the design surface or the currently selected group in the Grouping pane. Alternatively, you can right-click on a selected report item or group and then select **Properties** to open the corresponding **Properties** dialog box for the report item or group.  
  
## Related content

- [Page headers and footers &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/page-headers-and-footers-report-builder-and-ssrs.md)
- [Report design tips &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/report-design-tips-report-builder-and-ssrs.md)
