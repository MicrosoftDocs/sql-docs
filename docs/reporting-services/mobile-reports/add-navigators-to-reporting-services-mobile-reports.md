---
title: "Add navigators to Reporting Services mobile reports | Microsoft Docs"
description: In Mobile Report Publisher, add navigators to filter data by time or by selection. Navigators are similar to slicers in Power BI and Excel PivotTables.
ms.date: 07/21/2022
ms.service: reporting-services
ms.subservice: mobile-reports

ms.topic: conceptual
ms.assetid: e141f50e-49a9-46c6-983c-f656013aa07c
author: maggiesMSFT
ms.author: maggies
---
# Add navigators to Reporting Services mobile reports

[!INCLUDE [ssrs-mobile-report-deprecated](../../includes/ssrs-mobile-report-deprecated.md)]

In [!INCLUDE[SS_MobileReptPub_Long](../../includes/ss-mobilereptpub-long.md)], you add *navigators* to filter the data in visualizations by time or by selection. 

Navigators are similar to slicers in Power BI and Excel PivotTables, but navigators have some unique traits, too.

**Time-based navigators** filter tables by selecting rows that fall in a specific time range. 

**Selection-based navigators** filter tables by selecting rows where a particular column value matches the selected key value; or, in cases of hierarchical trees, where a particular column value belongs to the sub-tree of the selected key value. There are two types of selection navigators:
* Selection lists are single-column tables you can use to filter your mobile report, similar to slicers in Power BI and Excel.
* Scorecard grids filter the mobile report, too, and can also contain 
  
## Time navigators   
  
As its name implies, you use the time navigator to filter a range of data bounded by a time range.   
  
![SSMRP_TimeNav](../../reporting-services/mobile-reports/media/ssmrp-timenav.png)  
*The four line charts on the left are set in the Time Range Presets. The line chart on the right is the filter.*  
  
When you view the report in Preview or in the [!INCLUDE[ssRSnoversion_md](../../includes/ssrsnoversion-md.md)] web portal, you drag the arrows in the time navigator to filter the rest of the report.  
  
![SSMRP_TimeNavPreview](../../reporting-services/mobile-reports/media/ssmrp-timenavpreview.png)  
  
By default, the time navigator filters all the visuals in the report that are connected to time-based data. If a table contains more than one time-based column, only the first is used for filtering. The series table drives the embedded visualization and determines the overall date range of the mobile report.  
  
You can disconnect a visualization from the time navigator.   
1. Select the visualization, then select the **Data** tab.  
2. In **Data Properties**, select **Options**.  
3. Clear the **Filtered by** check box.  
  
   ![SSMRP_ClearTimeFilter](../../reporting-services/mobile-reports/media/ssmrp-cleartimefilter.png)  
  
## Selection lists   
  
The selection list filters data in a mobile report by matching the value you select in the list to the value of a specified column for each row of a filtered table. 

1. From the **Layout** tab, drag **Selection list** to the design surface and resize it the way you want it.

2. Select the **Data** tab, and in the **Data properties** pane under **Keys**, select the table and column that will be the filter. 

3. Under **Labels**, select the column with the label that will be displayed. The key column and label column can be the same.  
  
   In the case of hierarchical tree data, select a parent key column.  
  
4. After you've set the data properties, under **Tables FIltered by Selection List**, select the tables to be filtered and the column to filter by. This column needs to match values in the selection list's key column. 

For each visualization in the mobile report that you want the selection list to filter:

1. Select the visualization, select the **Data** tab, and in the **Data properties** pane, select **Options** next to the field name.

   ![Screenshot of the Category Column's Options expanded with the Filter by State / Regions option selected.](../../reporting-services/mobile-reports/media/mobile-report-set-selection-list.png)

2. Under **Filtered by**, select the selection list.

When you view the mobile report in Preview or in the [!INCLUDE[ssRSnoversion_md](../../includes/ssrsnoversion-md.md)] web portal and select a value in the selection list, it filters the other visualizations in the mobile report.

![Screenshot showing the mobile report with Washington selected from the States / Regions list and cities in Washington represented in the returned results.](../../reporting-services/mobile-reports/media/mobile-report-selection-list-filtering.png) 
     
## Scorecard grid  
  
The scorecard grid filter functions much like the selection list filter, but it also displays value columns and score indicators, which are the same as the indicators in an indicator data grid. After selecting the key, label, and optional parent key columns, you select an input table and column to provide the scorecard with data. The scorecard data column should be filterable by the key column.  

1. From the **Layout** tab, drag **Scorecard grid** to the design surface and resize it the way you want it.

2. Select the **Data** tab, and in the **Data properties** pane under **Keys**, select the table and column that will be the filter. 

3. Under **Labels**, select the column with the label that will be displayed. The key column and label column can be the same.  
  
4. To add a score indicator, in the **Data Columns** pane, select **Add score**.   
  
5. Name the score indicator and select **Options** to set the same properties you'd set for an indicator in a data grid:  
  
   * Gauge type
   * Value field
   * Comparison field
   * Value direction
  
6. To add a value indicator, in the **Data Columns** pane, select **Add value**.

7. Name the value indicator as desired, choose its source column from the table, and select how it will be formatted.  

   ![Screenshot showing the options that are available for a new score.](../../reporting-services/mobile-reports/media/mobile-report-scorecard-grid-data-properties.png)

8. After you've set the data properties, under **Tables FIltered by Selection List**, select the tables to be filtered and the column to filter by. This column needs to match values in the selection list's key column. 

When you view the mobile report in Preview or in the [!INCLUDE[ssRSnoversion_md](../../includes/ssrsnoversion-md.md)] web portal and select a value in the scorecard grid, it filters the other visualizations in the mobile report.

![Screenshot of a mobile report scorecard grid.](../../reporting-services/mobile-reports/media/mobile-report-scorecard-grid.png)
    
## Set which visualizations are filtered  
  
Gallery elements are configured to use filters by clicking the options button for a particular input in data view. Filters are enabled by toggling the appropriate checkbox.  

You can decide which visualizations in the mobile report that a navigator will filter:

1. Select the visualization, select the **Data** tab, and in the **Data properties** pane, select **Options** next to the field name.

   ![Another screenshot of the Category Column's Options expanded with the Filter by State / Regions option selected.](../../reporting-services/mobile-reports/media/mobile-report-set-selection-list.png)

2. Under **Filtered by**, select the navigator. Each visualization can be filtered by multiple navigators.
  
## Cascading filters   
  
Filters can also be cascaded together so that the selected value of one will filter the available values in a second. To cascade filters, apply the filters to the Key column as you would a regular gallery element.  

### See also 
  
* [Maps in Reporting Services mobile reports](../../reporting-services/mobile-reports/maps-in-reporting-services-mobile-reports.md)
* [Visualizations in Reporting Services mobile reports](../../reporting-services/mobile-reports/add-visualizations-to-reporting-services-mobile-reports.md)
* [Gauges in Reporting Services mobile reports](../../reporting-services/mobile-reports/add-gauges-to-mobile-reports-reporting-services.md)
* [Data grids in Reporting Services mobile reports](../../reporting-services/mobile-reports/add-data-grids-to-mobile-reports-reporting-services.md)  
