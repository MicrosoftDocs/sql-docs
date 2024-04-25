---
title: "Add sparklines and data bars in a paginated report"
description: "Learn how to add sparklines and data bars in a paginated report."
author: maggiesMSFT
ms.author: maggies
ms.date: 03/03/2017
ms.service: reporting-services
ms.subservice: report-design
ms.topic: conceptual
ms.custom: updatefrequency5
---
# Add sparklines and data bars in a paginated report (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

  Sparklines and data bars are small, spare charts that convey information with little extraneous detail in a paginated report. For more information about them, see [Sparklines and data bars &#40;Report Builder&#41;](../../reporting-services/report-design/sparklines-and-data-bars-report-builder-and-ssrs.md).  
  
 In paginated reports, sparklines and data bars are most commonly placed in cells in a table or matrix. Sparklines usually display only one series each. Data bars can contain one or more data points. Both sparklines and data bars derive their impact from repeating the series information for each row in the table or matrix.  
  
## Add a sparkline or data bar to a table or matrix  
  
1.  If you didn't do so already, create a [table](../../reporting-services/report-design/tables-report-builder-and-ssrs.md) or [matrix](../../reporting-services/report-design/create-a-matrix-report-builder-and-ssrs.md) with the data you want to display.  
  
1.  Insert a column in your table or matrix. For more information, see [Insert or delete a column &#40;Report Builder&#41;](../../reporting-services/report-design/insert-or-delete-a-column-report-builder-and-ssrs.md).  
  
1.  On the **Insert** tab, select **Sparkline** or **Data Bar**, and then choose in a cell in the new column.  
  
    > [!NOTE]  
    >  You can't place sparklines in a detail group in a table. They must go in a cell associated with a group.  
  
1.  In the **Change Sparkline/Data Bar Type** dialog, select the kind of sparkline or data bar you want, and then select **OK**.  
  
1.  Select the sparkline or data bar.  
  
     The **Chart Data** pane opens.  
  
1.  In the **Values** area, select the **Add Fields** plus sign (**+**), and then choose the field whose values you want charted.  
  
1.  In the **Category Groups** area, select the **Add Fields** plus sign (**+**), and then choose the field whose values you want to group by.  
  
     Typically for sparklines and data bars, you don't add a field to the **Series Group** area because you only want one series for each row.  
  
## Related content  
 [Charts &#40;Report Builder&#41;](../../reporting-services/report-design/charts-report-builder-and-ssrs.md)   
 [Align the data in a chart in a table or matrix &#40;Report Builder&#41;](../../reporting-services/report-design/align-the-data-in-a-chart-in-a-table-or-matrix-report-builder-and-ssrs.md)  
  
  
