---
title: "Include indicators and gauges in a gauge panel in paginated report | Microsoft Docs"
description: Find out about using gauges and indicators in the gauge panel, a top-level container, in your paginated reports in Report Builder. 
ms.date: 03/01/2017
ms.service: reporting-services
ms.subservice: report-design


ms.topic: conceptual
ms.assetid: 4dff9b67-b483-4c51-a822-6dbe706a6840
author: maggiesMSFT
ms.author: maggies
---
# Include indicators and gauges in a gauge panel in paginated report (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

  The gauge panel is the top-level container that holds one or more gauges and indicators in a paginated report. Indicators can be embedded in gauges or placed next to them in the gauge panel.  
  
 If the indicator and gauge are adjacent in the gauge panel and show data from different fields, you might want to add labels to make it clear what data the gauge and indicator convey.  
  
 Gauge and indicator options can be set by using expressions. For more information, see [Expressions &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/expressions-report-builder-and-ssrs.md).  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
### To embed an indicator in a gauge  
  
1.  Open an existing report or create a new report that contains a table and matrix with the data you want to display.   
  
2.  Insert a column in your table or matrix. For more information, see [Insert or Delete a Column &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/insert-or-delete-a-column-report-builder-and-ssrs.md).  
  
3.  On the **Insert** tab, in the **Data Regions** group, click **Gauge**, and then, and then click a cell in the new column. The **Select Gauge Type** dialog box appears.  
  
4.  Click **Radial**. The first radial gauge is selected.  
  
5.  Select **OK**.
  
6.  Click the gauge. The **Gauge Data** pane opens.  
  
7.  In the **Values** area, in the **(Unspecified)** list box, click the field whose values you want to display in the gauge. Alternatively, drag the field to use from the report dataset.  
  
8.  Right-click the gauge, click **Add Indicator**, and then click **Child**. The **Select Indicator Style** dialog box opens.  
  
9. In the **Select Indicator Style** dialog box, in the left pane, click the indicator type you want, and then click the indicator set.  
  
10. Select **OK**.
  
11. Click the indicator. The **Gauge Data** pane opens.  
  
12. In the **Values** area, in the **(Unspecified)** list box, click the field whose values you want to display as an indicator. Alternatively, drag the field to use from the report dataset.  
  
     The field can be the same or a different field from the one you use in the gauge.  
  
13. Select **OK**.
  
### To show an indicator and gauge side by side  
  
1.  Open an existing report or create a new report that contains a table and matrix with the data you want to display.  
  
2.  Insert a column in your table or matrix. For more information, see [Insert or Delete a Column &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/insert-or-delete-a-column-report-builder-and-ssrs.md).  
  
3.  On the **Insert** tab, in the **Data Regions** group, click **Gauge**, and then click a cell in the column you inserted. The **Select Gauge Type** dialog appears.  
  
4.  Click **Radial**. The first radial gauge is selected.  
  
5.  Select **OK**.
  
6.  Click the gauge. The **Gauge Data** pane opens.  
  
7.  In the **Values** area, in the **(Unspecified)** list box, click the field whose values you want to display in the gauge. Alternatively, drag the field to use from the report dataset.  
  
8.  Right-click the gauge, click **Add Indicator**, and then click **Adjacent**. The **Select Indicator Style** dialog box opens.  
  
9. In the **Select Indicator Style** dialog box, in the left pane, click the indicator type you want, and then click the indicator set.  
  
10. Select **OK**.
  
11. Click the indicator. The **Gauge Data** pane opens.  
  
12. In the **Values** area, in the **(Unspecified)** list box, click the field whose values you want to display as an indicator. Alternatively, drag the field to use from the report dataset.  
  
     The field can be the same or a different field from the one you use in the gauge.  
  
13. Select **OK**.
  
14. Right-click the gauge panel and click **Add Label**. A label is added to the gauge panel. Repeat one more time.  
  
     The gauge panel has two labels.  
  
15. Drag each label to a location near the gauge or indicator.  
  
16. Right-click the label near the gauge, click **Label Properties**,and type the text you want in the **Text** box.  
  
17. Right-click the label near the indicator, click **Label Properties**,and type the text you want in the **Text** box.  
  
18. Select **OK**.
  
## See Also  
 [Indicators &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/indicators-report-builder-and-ssrs.md)  
  
  
