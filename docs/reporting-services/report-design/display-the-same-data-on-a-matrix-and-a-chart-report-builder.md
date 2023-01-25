---
title: "Display the same data on a matrix and a chart in paginated report | Microsoft Docs"
description: Show the same data in a paginated report in a matrix and in a chart in Report Builder. Set properties on these data regions to include the specifics.
ms.date: 03/07/2017
ms.service: reporting-services
ms.subservice: report-design


ms.topic: conceptual
ms.assetid: 1262f283-9fc2-4bc1-9c79-457f7642abc7
author: maggiesMSFT
ms.author: maggies
---
# Display the same data on a matrix and a chart in paginated report (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

  When you want to show the same data in a matrix and a chart in a paginated report, you must set properties on both data regions to specify the same dataset, and also the same expressions for filters, groups, sorts, and data.  
  
 Because both data regions will have the same ancestor for data (the report dataset), you can add an interactive sort button to the matrix that, when the user clicks it, changes the sort order for both the matrix and the chart. For more information, see [Add Interactive Sort to a Table or Matrix &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/add-interactive-sort-to-a-table-or-matrix-report-builder-and-ssrs.md).  
  
 To use the matrix column group values as a legend for the chart, you must specify the colors for the series data on the chart, and then use the same colors as the fill colors for the background of the text boxes in the matrix cell that displays the group values. For more information, see [Specify Consistent Colors across Multiple Shape Charts &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/specify-consistent-colors-across-multiple-shape-charts-report-builder-and-ssrs.md).  
  
 At run-time, your report may appear cluttered if there are too many group values for your group definitions. You might need to filter values, combine groups, or adjust the threshold for the chart to combine groups for you. For more information, see [Linking Multiple Data Regions to the Same Dataset &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/linking-multiple-data-regions-to-the-same-dataset-report-builder-and-ssrs.md)  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
### To add a matrix and chart to display the same data  
  
1.  Open a report in design view.  
  
2.  From the **Insert** tab, in the **Data Regions** group, click **Matrix**, and then click the report body or in a rectangle in the report body. A matrix is added to the report.  
  
3.  From the **Insert** tab, in the **Data Regions** group, click **Chart**, and then select the chart type. A chart is added to the report.  
  
4.  (Optional) From the **Insert** tab, in the **Report Items** group, click **Rectangle**, and then click the report. A rectangle is added to the report. Drag the matrix and chart from steps 2 and 3 into the rectangle.  
  
     By placing multiple data regions in the rectangle container, you help control how the matrix and chart render when you view the report.  
  
     In the next few steps, you will choose the same dataset field to display in the matrix and to display in the chart.  
  
5.  From the Report Data pane, drag a numeric dataset field to the Data cell in the matrix.  
  
     By default, the aggregate function Sum is used for calculating the group value. If you change the aggregate function in the matrix, you must change in the chart also.  
  
6.  In the matrix, right-click the cell with data, click **Text Box Properties**, and then click **Number**. Choose an appropriate format for the dataset field value.  
  
7.  Select **OK**.
  
8.  Drag the same dataset field you chose in step 3 to the **Values** area on the chart.  
  
9. In the chart, right-click the Y axis, click **Axis Properties**, and then click **Number**. Choose the same format for the data that you chose in step 4.  
  
10. Select **OK**.
  
     In the next few steps, you will set the matrix row group and the chart series group to the same expression, and also set the sort order for the chart series group.  
  
11. From the Report Data pane, drag the dataset field that you want to group by for matrix rows to the Row Groups pane.  
  
     By default, the matrix row group adds a sort expression that is the same as the group expression.  
  
12. Drag the same dataset field that you used in step 9 to the **Series Groups** area for the chart.  
  
13. Right-click the group in the **Series Groups** area, and then click **Series Group Properties**.  
  
14. Click **Sorting**.  
  
15. Click **Add**. A new row appears in the sort expressions grid.  
  
16. In **Sort by**, from the drop-down list, choose the dataset field that you chose to group by in step 9.  
  
17. Select **OK**.
  
     In the next few steps, you will set the matrix column group and the chart category group to the same expression, and also set the sort order for the chart category group.  
  
18. From the Report Data pane, drag the dataset field that you want to group by for matrix columns to the Column Groups pane.  
  
     By default, the matrix column group adds a sort expression that is the same as the group expression.  
  
19. Drag the same dataset field that you used in step 16 to the **Category Groups** area for the chart.  
  
20. Right-click the group in the **CategoryGroups** area, and then click **Category Group Properties**.  
  
21. Click **Sorting**.  
  
22. Click **Add**. A new row appears in the sort expressions grid.  
  
23. In **Sort by**, from the drop-down list, choose the dataset field that you chose to group by in step 16.  
  
24. Select **OK**.
  
25. Preview the result. The matrix row and column groups display the same data as the chart series and category groups.  
  
## See Also  
 [Linking Multiple Data Regions to the Same Dataset &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/linking-multiple-data-regions-to-the-same-dataset-report-builder-and-ssrs.md)   
 [Add Dataset Filters, Data Region Filters, and Group Filters &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/add-dataset-filters-data-region-filters-and-group-filters.md)   
 [Tables, Matrices, and Lists &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/tables-matrices-and-lists-report-builder-and-ssrs.md)   
 [Charts &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/charts-report-builder-and-ssrs.md)  
  
  
