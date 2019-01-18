---
title: "Add or Delete an Indicator (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: a8b1aac1-53ef-47a4-afc0-8fa866c6c480
author: maggiesMSFT
ms.author: maggies
manager: craigg
---
# Add or Delete an Indicator (Report Builder and SSRS)
  Indicators are minimal gauges that convey the state of a single data value at a glance. For more information about them, see [Indicators &#40;Report Builder and SSRS&#41;](indicators-report-builder-and-ssrs.md).  
  
 Indicators are commonly placed in cells in a table or matrix, but you can also use indicators by themselves, side-by-side with gauges, or embedded in gauges.  
  
 When you first add an indicator, it is by default configured to use percentages as measurement units. The percentage ranges are evenly distributed across members of the indicator set, and the scope of values shown by the indicator is the parent of the indicator such as a table or matrix.  
  
 You can update the values and states of indicators. For more information, see the following topics:  
  
-   [Change Indicator Icons and Indicator Sets &#40;Report Builder and SSRS&#41;](change-indicator-icons-and-indicator-sets-report-builder-and-ssrs.md)  
  
-   [Set and Configure Measurement Units &#40;Report Builder and SSRS&#41;](set-and-configure-measurement-units-report-builder-and-ssrs.md)  
  
-   [Set Synchronization Scope &#40;Report Builder and SSRS&#41;](set-synchronization-scope-report-builder-and-ssrs.md)  
  
 Because an indicator is positioned inside the gauge panel, you need to select the indicator instead of the panel when you want to configure the indicator by using the **Indicators Properties** dialog box or the **Properties** pane. The following picture shows a selected indicator in its gauge panel.  
  
 ![rs_GaugePanelWithIndicator](../media/rs-gaugepanelwithindicator.gif "rs_GaugePanelWithIndicator")  
  
> [!NOTE]  
>  Depending on column width and the length of data values, the text in table or matrix cells might wrap and display text on multiple lines. When this occurs, the indicator icon might be stretched and change shape. This can make the indicator icon less readable. Place the indicator inside a rectangle to ensure that the icon is never stretched.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
### To add an indicator to a table or matrix  
  
1.  Open an existing report or create a new report that contains a table and matrix with the data you want to display. For more information, see [Tables &#40;Report Builder  and SSRS&#41;](tables-report-builder-and-ssrs.md) or [Matrices &#40;Report Builder and SSRS&#41;](create-a-matrix-report-builder-and-ssrs.md).  
  
2.  Insert a column in your table or matrix. For more information, see [Insert or Delete a Column &#40;Report Builder and SSRS&#41;](insert-or-delete-a-column-report-builder-and-ssrs.md).  
  
3.  Optionally, on the **Insert** tab, click **Rectangle**, and then click a cell in the new column.  
  
4.  On the **Insert** tab, click **Indicator**, and then click a cell in the new column.  
  
     If you added a rectangle to a cell, click that cell.  
  
5.  In the **Select Indicator Style** dialog box, in the left pane, click the indicator type you want, and then click the indicator set.  
  
6.  Click **OK**.  
  
7.  Click the indicator. The **Gauge Data** pane opens.  
  
8.  In the **Values** area, in the **(Unspecified)** drop-down list, click the field whose values you want to display as an indicator.  
  
     The indicator is configured to use default values. By default, indicators are configured use percentages as measurement units and the percentage ranges are evenly distributed across the members of the indicator and the value that the indicator conveys uses the scope of the nearest group.  
  
### To delete an indicator to a table or matrix  
  
1.  Right-click the indicator to delete and click **Delete**.  
  
    > [!NOTE]  
    >  An indicator might be positioned inside a gauge panel that contains other indicators or gauges. If the gauge panels contain multiple items, be sure to click the indicator to delete it, not the gauge panel. If you click and then delete the gauge panel, the gauge panels and all the items in it are deleted.  
  
2.  Click **Delete**.  
  
## See Also  
 [Indicators &#40;Report Builder and SSRS&#41;](indicators-report-builder-and-ssrs.md)  
  
  
