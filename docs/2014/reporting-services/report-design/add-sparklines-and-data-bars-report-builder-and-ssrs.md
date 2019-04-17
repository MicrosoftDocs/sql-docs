---
title: "Add Sparklines and Data Bars (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 0b297c2e-d48b-41b0-aabd-29680cdcdb05
author: markingmyname
ms.author: maghan
manager: kfile
---
# Add Sparklines and Data Bars (Report Builder and SSRS)
  Sparklines and data bars are small, spare charts that convey a lot of information with little extraneous detail. For more information about them, see [Sparklines and Data Bars &#40;Report Builder and SSRS&#41;](sparklines-and-data-bars-report-builder-and-ssrs.md).  
  
 Sparklines and data bars are most commonly placed in cells in a table or matrix. Sparklines usually display only one series each. Data bars can contain one or more data points. Both sparklines and data bars derive their impact from repeating the series information for each row in the table or matrix.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
### To add a sparkline or data bar to a table or matrix  
  
1.  If you have not done so already, create a table or matrix with the data you want to display. For more information, see [Tables &#40;Report Builder  and SSRS&#41;](tables-report-builder-and-ssrs.md) or [Matrices &#40;Report Builder and SSRS&#41;](create-a-matrix-report-builder-and-ssrs.md).  
  
2.  Insert a column in your table or matrix. For more information, see [Insert or Delete a Column &#40;Report Builder and SSRS&#41;](insert-or-delete-a-column-report-builder-and-ssrs.md).  
  
3.  On the **Insert** tab, click **Sparkline** or **Data Bar**, and then click in a cell in the new column.  
  
    > [!NOTE]  
    >  You cannot place sparklines in a detail group in a table. They must go in a cell associated with a group.  
  
4.  In the **Change Sparkline/Data Bar Type** dialog box, click the kind of sparkline or data bar you want, and then click **OK**.  
  
5.  Click the sparkline or data bar.  
  
     The **Chart Data** pane opens.  
  
6.  In the **Values** area, click the **Add Fields** plus sign (**+**), and then click the field whose values you want charted.  
  
7.  In the **Category Groups** area, click the **Add Fields** plus sign (**+**), and then click the field whose values you want to group by.  
  
     Typically for sparklines and data bars, you will not add a field to the **Series Group** area because you only want one series for each row.  
  
## See Also  
 [Charts &#40;Report Builder and SSRS&#41;](charts-report-builder-and-ssrs.md)   
 [Align the Data in a Chart in a Table or Matrix &#40;Report Builder and SSRS&#41;](align-the-data-in-a-chart-in-a-table-or-matrix-report-builder-and-ssrs.md)  
  
  
