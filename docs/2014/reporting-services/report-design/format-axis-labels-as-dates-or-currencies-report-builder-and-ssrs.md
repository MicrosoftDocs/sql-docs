---
title: "Format Axis Labels as Dates or Currencies (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: e9a01a74-2f51-4b35-be3a-a6138568f6cf
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Format Axis Labels as Dates or Currencies (Report Builder and SSRS)
  When you show properly formatted DateTime values on an axis, a chart will automatically display these values as days. To specify a date/time interval for the x-axis, such as an interval of months or an interval of hours, you must format the axis labels and set the type of axis interval to a valid date or time interval.  
  
> [!NOTE]  
>  In column and scatter charts, the horizontal, or x-axis, is the category axis. In bar charts, the vertical, or y-axis, is the category axis.  
  
 In order to format time intervals correctly, the values displayed on the x-axis must evaluate to a <xref:System.DateTime> data type. If your field has a data type of <xref:System.String>, the chart will not calculate intervals as dates or times. For more information, see [Charts &#40;Report Builder and SSRS&#41;](charts-report-builder-and-ssrs.md).  
  
 When a numeric value is added to the y-axis, by default, the chart does not format the number before displaying it. If your numeric field is a sales figure, consider formatting the numbers as currencies to increase the readability of the chart.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
### To format x-axis labels as monthly intervals  
  
1.  Right-click the horizontal, or x-axis, of the chart, and select **HorizontalAxis Properties**.  
  
2.  In the **HorizontalAxis Properties** dialog box, select **Number**.  
  
3.  From the **Category** list, select **Date**. From the **Type** list, select a date format to apply to the x-axis labels.  
  
4.  Select **Axis Options.**  
  
5.  In **Interval**, type **1**. In **Interval type** property, select **Months**.  
  
    > [!NOTE]  
    >  If you do not specify an interval type, the chart will calculate intervals in terms of days.  
  
6.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
### To format y-axis labels using a currency format  
  
1.  Right-click the vertical, or y-axis, of the chart, and select **VerticalAxis Properties**.  
  
2.  In the **VerticalAxis Properties** dialog box, select **Number**.  
  
3.  From the **Category** list, select **Currency**. From the **Symbol** list, select a currency format to apply to the y-axis labels.  
  
4.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
## See Also  
 [Formatting Axis Labels on a Chart &#40;Report Builder and SSRS&#41;](formatting-axis-labels-on-a-chart-report-builder-and-ssrs.md)   
 [Formatting a Chart &#40;Report Builder and SSRS&#41;](formatting-a-chart-report-builder-and-ssrs.md)   
 [Specify a Logarithmic Scale &#40;Report Builder and SSRS&#41;](specify-a-logarithmic-scale-report-builder-and-ssrs.md)   
 [Specify an Axis Interval &#40;Report Builder and SSRS&#41;](specify-an-axis-interval-report-builder-and-ssrs.md)  
  
  
