---
title: "Create Invoices and Forms with Lists (Report Builder and SSRS) | Microsoft Docs"
ms.date: 03/07/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-design


ms.topic: conceptual
ms.assetid: c33231a5-b3a8-42e4-95bc-d05bdf2222f5
author: markingmyname
ms.author: maghan
---
# Create Invoices and Forms with Lists (Report Builder and SSRS)
  A list data region repeats with each group or row in the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] paginated report dataset. A list can be used to create free-form reports or forms, such as invoices, or in conjunction with other data regions. You can define lists that contain any number of report items. A list can be nested wit  
  
 To quickly get started with lists, see [Tutorial: Creating a Free Form Report &#40;Report Builder&#41;](../../reporting-services/tutorial-creating-a-free-form-report-report-builder.md).  
  
> [!NOTE]  
>  You can publish lists separately from a report as report parts. Read more about [Report Parts (Report Builder and SSRS)](../../reporting-services/report-design/report-parts-report-builder-and-ssrs.md).  
  
##  <a name="AddingList"></a> Adding a List to Your Report  
 Add a list to the design surface from the Insert tab on ribbon. By default, the list initially has a single cell in a row associated with the detail group.  
  
 ![New List report item on the design surface](../../reporting-services/report-design/media/rs-listtemplatenew.gif "New List report item on the design surface")  
  
 When you select a list on the design surface, row and column handles appear, as shown in the following figure.  
  
 ![New List added from Toolbox, selected](../../reporting-services/report-design/media/rs-listtemplatenewselected.gif "New List added from Toolbox, selected")  
  
 The list you start with is a template based on the tablix data region. After you add a list, you can continue to enhance the design by changing the content or appearance of the list by specifying filter, sort, or group expressions, or changing the way the list displays across report pages. For more information, see [Controlling the Tablix Data Region Display on a Report Page &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/controlling-the-tablix-data-region-display-on-a-report-page.md). Although the list starts with a single column and row, you can further continue to develop your list design by adding nested or adjacent row groups or column groups, or adding additional detail rows. For more information, see [Exploring the Flexibility of a Tablix Data Region &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/exploring-the-flexibility-of-a-tablix-data-region-report-builder-and-ssrs.md).  
  
  
##  <a name="DisplayingLayout"></a> Displaying Data in a Free-form Layout  
 To organize report data in a free-form layout instead of a grid, you can add a list to the design surface. Drag fields from the Report Data pane to the cell. By default, the cell contains a rectangle that acts as a container. Move each field in the container until you have the design you want. Use the snaplines that appear when you drag text boxes in the rectangle container to help you align edges vertically and horizontally. Remove unwanted white space by adjusting the size of the cell. For more information, see [Change Row Height or Column Width &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/change-row-height-or-column-width-report-builder-and-ssrs.md).  
  
 The following figure shows a list that displays information about an order, including these fields: Date, Order, Qty, Product, LineTotal, and an image.  
  
 ![List in design view, 4 fields and an image](../../reporting-services/report-design/media/rs-basiclistformdesign.gif "List in design view, 4 fields and an image")  
  
 In Preview, the list repeats to display the field data in the free-form format, as shown in the following figure:  
  
 ![Preview for List with 4 fields and one image](../../reporting-services/report-design/media/rs-basiclistformpreview.gif "Preview for List with 4 fields and one image")  
  
> [!NOTE]  
>  The dotted lines displays in these figures are included to show the free-form layout for each field value. Typically, you would not use dotted lines in a production report.  
  
  
##  <a name="DisplayingGrouping"></a> Displaying Data with One Level of Grouping  
 Because a list automatically provides a container, you can use a list to display grouped data with multiple views. To change the default list to specify a group, edit the Details group, specify a new name, and specify a group expression.  
  
 For example, you can embed a table and a chart that show different views of the same dataset. You can add a group to the list so that the nested report items will repeat once for every group value. The following figure shows a list grouped by product category. Notice that there is no detail row. Two tables are nested side by side in the list. The first table displays the subcategories with total sales. The second table displays the category grouped by geographical area, with a chart that shows the distribution of subcategories.  
  
 ![A list with 2 tables, one with nested chart](../../reporting-services/report-design/media/rs-basiclistgroupdesign.gif "A list with 2 tables, one with nested chart")  
  
 In Preview, the table displays total sales for all subcategories of bicycles, and the table beside it displays the breakdown of sales per geographical area. By using an expression to specify the background color for the table and a custom palette for the chart, the first table also provides the legend for the chart colors.  
  
 ![Preview, 2 tables, one with nested chart](../../reporting-services/report-design/media/rs-basiclistgrouppreview.gif "Preview, 2 tables, one with nested chart")  
  
  
## See Also  
 [Aggregate Functions Reference &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/report-builder-functions-aggregate-functions-reference.md)   
 [Expression Examples &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/expression-examples-report-builder-and-ssrs.md)  
  
  
