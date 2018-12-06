---
title: "Adding a Data Source View for Forecasting (Intermediate Data Mining Tutorial) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 2665040a-1291-4064-ba01-f458637dda57
author: minewiskan
ms.author: owend
manager: craigg
---
# Adding a Data Source View for Forecasting (Intermediate Data Mining Tutorial)
  In this task, you add a data source view that will be used for the forecasting scenario. A forecasting model requires that the data contains a column that can be used to identify steps in a time series. If you plan to analyze multiple series of data, all series must end on the same date or time step.  
  
### To add a data source view  
  
1.  In Solution Explorer, right-click **Data Source Views**, and then select **New Data Source View**.  
  
2.  On the **Welcome to the Data Source View Wizard** page, click **Next**.  
  
3.  On the **Select a Data Source** page, under **Relational data sources**, select the [!INCLUDE[ssSampleDBDWobject](../includes/sssampledbdwobject-md.md)] data source. Click **Next**.  
  
    > [!NOTE]  
    >  If you do not have this data source, you can find the steps to create the data source in the [Basic Data Mining Tutorial](../../2014/tutorials/basic-data-mining-tutorial.md).  
  
4.  On the **Select Tables and Views** page, select the table, vTimeSeries (dbo), and then click the right arrow to add it to the data source view.  
  
5.  Click **Next**.  
  
6.  On the **Completing the Wizard** page, by default the data source view is named [!INCLUDE[ssAWDWsp](../includes/ssawdwsp-md.md)]. Change the name to **SalesByRegion**, and then click **Finish**.  
  
     Data Source View Designer opens and the **SalesByRegion** data source view appears.  
  
## Working with the Data Source View  
 After you have created the data source view, you can explore the data in the following ways:  
  
-   Right-click the table vTimeSeries in the designer, and select **Explore Data** to open the selected table in a grid.  
  
-   Click **Sampling options** and then use the **Data Exploration Options** dialog box to change the sampling method. Click **Refresh** to load data in the table using the new option settings. For example, you could specify the number of rows to output in the sample, or choose the top n rows.  
  
-   Right-click the table vTimeSeries and select **Properties** to assign a new name to the table. You can also select individual columns from the data source view, and the modify the column properties.  
  
-   Click anywhere in the data source view design area to create a new query and assign a name to it, to create relationships between tables, or to change the layout of the design area.  
  
-   Right-click a table and select **New Named Calculation** to create derived columns, including aggregations. You can also add new tables and views from the data source in this view.  
  
 In the next task, you will explore the time series data and determine the best column to use as the time series identifier. You will also learn how to handle gaps in time series data.  
  
## Next Task in Lesson  
 [Understanding the Requirements for a Time Series Model &#40;Intermediate Data Mining Tutorial&#41;](../../2014/tutorials/time-series-model-requirements-intermediate-data-mining-tutorial.md)  
  
## See Also  
 [Microsoft Time Series Algorithm](../../2014/analysis-services/data-mining/microsoft-time-series-algorithm.md)  
  
  
