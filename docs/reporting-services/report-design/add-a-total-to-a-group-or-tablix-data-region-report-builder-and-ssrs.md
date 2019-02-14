---
title: "Add a Total to a Group or Tablix Data Region (Report Builder and SSRS) | Microsoft Docs"
ms.date: 03/07/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-design


ms.topic: conceptual
ms.assetid: cf1b96c3-7f0f-4c94-ad08-5239c77ccfe4
author: markingmyname
ms.author: maghan
---
# Add a Total to a Group or Tablix Data Region (Report Builder and SSRS)
 In a [!INCLUDE[ssRSnoversion_md](../../includes/ssrsnoversion-md.md)] paginated report, you can add totals in a tablix data region for a group or for the entire data region. By default, a total is the sum of the numeric, non-null data in a group or in the data region, after filters are applied. To add totals for a group, click **Add Total** on the shortcut menu for the group in the Grouping pane. To add totals for an individual cell in the tablix body area, click **Add Total** on the shortcut menu for the cell. The **Add Total** command is context-sensitive and enabled only for numeric fields. Depending on the tablix cell that you select, you can add a total for a single cell by selecting a cell in the tablix body area or for the entire group by selecting a cell in the tablix row group area or the tablix column group area. For more information about tablix areas, see [Tablix Data Region &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/tablix-data-region-report-builder-and-ssrs.md).  
  
 After you add a total, you can change the default function Sum to a different aggregate function from the list of built-in report functions. For more information, see [Aggregate Functions Reference &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/report-builder-functions-aggregate-functions-reference.md). [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## To add a total for an individual value in the tablix body area  
  
-   In the tablix data region body area, right-click the cell where you want to add the total. The cell must contain a numeric field. Point to **Add Total**, and then click **Row** or **Column**.  
  
     A new row or column outside the current group is added to the data region, with a default total for the field in the cell you clicked.  
  
     If the tablix data region is a table, a row is automatically added.  
  
## To add totals for a row group  
  
-   In the tablix data region row group area, right-click a cell in the row group area for which you want totals, point to **Add Total**, and then click **Before** or **After**.  
  
     A new row outside the current group is added to the data region, and then a default total is added for each numeric field in the row.  
  
## To add totals for a column group  
  
-   In the tablix data region row group area, right-click a cell in the column group area for which you want totals, then point to **Add Total**, and click **Before** or **After**.  
  
     A new column outside the current group is added to the data region, and then a default total is added for each numeric field in the column.  
  
## See Also  
 [Expression Scope for Totals, Aggregates, and Built-in Collections &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/expression-scope-for-totals-aggregates-and-built-in-collections.md)   
 [Tablix Data Region &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/tablix-data-region-report-builder-and-ssrs.md)   
 [Tables, Matrices, and Lists &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/tables-matrices-and-lists-report-builder-and-ssrs.md)  
  
  
