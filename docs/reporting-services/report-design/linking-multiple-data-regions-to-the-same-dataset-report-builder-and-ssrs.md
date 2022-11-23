---
title: "Linking multiple data regions to the same dataset in a paginated report | Microsoft Docs"
description: Find out how to add multiple data regions to a paginated report to provide different views of data from the same report dataset in Report Builder.
ms.date: 05/30/2017
ms.service: reporting-services
ms.subservice: report-design


ms.topic: conceptual
ms.assetid: 90c94a91-8fb2-42cb-b998-563691f30d2d
author: maggiesMSFT
ms.author: maggies
---

# Linking multiple data regions to the same dataset in a paginated report (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

You can add multiple data regions to a paginated report to provide different views of data from the same report dataset. For example, you might want to display data in a table and also display it visually in a chart. To do so, you must use identical expressions and scopes for the appropriate filter expressions, sort expressions, and group expressions.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
 To use a chart and a table or matrix to display the same data, it helps to understand the similarities between a table and shape charts, and a matrix and area, bar, and column charts. A table with a single row group can easily be displayed as a pie chart. As you add multiple row groups, you can choose different types of charts to best display the nested groups. Adding nested row groups to a pie chart increases the number of slices in the pie. You must decide if the number of group instances for the parent group and child group combined is too many to display in a single pie chart. For multiple group values that display as small slices on a pie chart, you can set a property so that all values below a certain threshold display as one pie slice. For more information, see [Collect Small Slices on a Pie Chart](../../reporting-services/report-design/collect-small-slices-on-a-pie-chart-report-builder-and-ssrs.md).  
  
 A table with multiple row groups can be shown as a column chart with multiple category groups. For more information, see [Display the Same Data on a Matrix and a Chart](../../reporting-services/report-design/display-the-same-data-on-a-matrix-and-a-chart-report-builder.md). For an example of a table and chart that present different views of the same report dataset, see the Product Line Sales report in AdventureWorks Report Samples. Because both the table and the chart are linked to the same dataset in this report, when you click the interactive sort button for Employee Name in the sort the Top Employees table, the Top Employees chart also automatically shows the new sort order. For more information about downloading this sample report and others, see [Report Builder and Report Designer sample reports](https://go.microsoft.com/fwlink/?LinkId=198283).  
  
 A matrix with multiple row and column groups can best be displayed by using an area, bar, or column chart with both category and series groups. Use the same group expressions for column groups on the matrix and category groups on the chart, and the same group expressions for row groups on the matrix and series groups on the chart. You must keep in mind that the number of group instances affects the readability of the chart. You can define groups based on range values to reduce the number of group instances in a report. For more information, see [Group Expression Examples](../../reporting-services/report-design/group-expression-examples-report-builder-and-ssrs.md).  
  
## Next steps

[Charts](../../reporting-services/report-design/charts-report-builder-and-ssrs.md)   
[Tables, Matrices, and Lists](../../reporting-services/report-design/tables-matrices-and-lists-report-builder-and-ssrs.md)   
[Nested Data Regions](../../reporting-services/report-design/nested-data-regions-report-builder-and-ssrs.md)  

More questions? [Try asking the Reporting Services forum](/answers/search.html?c=&f=&includeChildren=&q=ssrs+OR+reporting+services&redirect=search%2fsearch&sort=relevance&type=question+OR+idea+OR+kbentry+OR+answer+OR+topic+OR+user)