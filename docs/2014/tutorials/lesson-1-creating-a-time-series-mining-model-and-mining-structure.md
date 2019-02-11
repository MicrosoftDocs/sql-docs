---
title: "Lesson 1: Creating a Time Series Mining Model and Mining Structure | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: analysis-services
ms.topic: conceptual
ms.assetid: b201f2b8-9ab5-425b-9ff3-fe321a60a7b7
author: minewiskan
ms.author: owend
manager: kfile
---
# Lesson 1: Creating a Time Series Mining Model and Mining Structure
  In this lesson, you will create a mining model that allows you to predict values over time, based on historical data. When you create the model, the underlying structure will be generated automatically and can be used as the basis for additional mining models.  
  
 This lesson assumes that you are familiar with forecasting models and with the requirements of the Microsoft Time Series algorithm. For more information, see [Microsoft Time Series Algorithm](../../2014/analysis-services/data-mining/microsoft-time-series-algorithm.md).  
  
## CREATE MINING MODEL Statement  
 In order to create a mining model directly and automatically generate the underlying mining structure, you use the [CREATE MINING MODEL &#40;DMX&#41;](/sql/dmx/create-mining-model-dmx) statement. The code in the statement can be broken into the following parts:  
  
-   Naming the model  
  
-   Defining the time stamp  
  
-   Defining the optional series key column  
  
-   Defining the predictable attribute or attributes  
  
 The following is a generic example of the CREATE MINING MODEL statement:  
  
```  
CREATE MINING MODEL [<Mining Structure Name>]  
(  
   <key columns>,  
   <predictable attribute columns>  
)  
USING <algorithm name>([parameter list])  
WITH DRILLTHROUGH  
```  
  
 The first line of the code defines the name of the mining model:  
  
```  
CREATE MINING MODEL [Mining Model Name]  
```  
  
 Analysis Services automatically generates a name for the underlying structure, by appending "_structure" to the model name, which ensures that the structure name is unique from the model name. For information about naming an object in DMX, see [Identifiers &#40;DMX&#41;](/sql/dmx/identifiers-dmx).  
  
 The next line of the code defines the key column for the mining model, which in the case of a time series model uniquely identifies a time step in the source data. The time step is identified with the `KEY TIME` keywords after the column name and data types. If the time series model has a separate series key, it is identified by using the `KEY` keyword.  
  
```  
<key columns>  
```  
  
 The next line of the code is used to define the columns in the model that will be predicted. You can have multiple predictable attributes in a single mining model. When there are multiple predictable attributes, the Microsoft Time Series algorithm generates a separate analysis for each series:  
  
```  
<predictable attribute columns>  
```  
  
## Lesson Tasks  
 You will perform the following tasks in this lesson:  
  
-   Create a new blank query  
  
-   Alter the query to create the mining model  
  
-   Execute the query  
  
## Creating the Query  
 The first step is to connect to an instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] and create a new DMX query in [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)].  
  
#### To create a new DMX query in SQL Server Management Studio  
  
1.  Open [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)].  
  
2.  In the **Connect to Server** dialog box, for **Server type**, select **Analysis Services**. In **Server name**, type `LocalHost`, or the name of the instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] that you want to connect to for this lesson. Click **Connect**.  
  
3.  In **Object Explorer**, right-click the instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)], point to **New Query**, and then click **DMX**.  
  
     Query Editor opens and contains a new, blank query.  
  
## Altering the Query  
 The next step is to modify the CREATE MINING MODEL statement to create the mining model used for forecasting, together with its underlying mining structure.  
  
#### To customize the CREATE MINING MODEL statement  
  
1.  In Query Editor, copy the generic example of the CREATE MINING MODEL statement into the blank query.  
  
2.  Replace the following:  
  
    ```  
    [mining model name]   
    ```  
  
     with:  
  
    ```  
    [Forecasting_MIXED]  
    ```  
  
3.  Replace the following:  
  
    ```  
    <key columns>  
    ```  
  
     with:  
  
    ```  
    [Reporting Date] DATE KEY TIME,  
    [Model Region] TEXT KEY  
    ```  
  
     The `TIME KEY` keyword indicates that the ReportingDate column contains the time step values used to order the values. Time steps can be dates and times, integers, or any ordered data type, so long as the values are unique and the data is sorted.  
  
     The `TEXT` and `KEY` keywords indicate that the ModelRegion column contains an additional series key. You can have only one series key, and the values in the column must be distinct.  
  
4.  Replace the following:  
  
    ```  
    < predictable attribute columns> )  
    ```  
  
     with:  
  
    ```  
    [Quantity] LONG CONTINUOUS PREDICT,  
    [Amount] DOUBLE CONTINUOUS PREDICT  
    )  
    ```  
  
5.  Replace the following:  
  
    ```  
    USING <algorithm name>([parameter list])  
    WITH DRILLTHROUGH  
    ```  
  
     with:  
  
    ```  
    USING Microsoft_Time_Series(AUTO_DETECT_PERIODICITY = 0.8, FORECAST_METHOD = 'MIXED')  
    WITH DRILLTHROUGH  
    ```  
  
     The algorithm parameter, `AUTO_DETECT_PERIODICITY` = 0.8, indicates that you want the algorithm to detect cycles in the data. Setting this value closer to 1 favors the discovery of many patterns but can slow processing.  
  
     The algorithm parameter, `FORECAST_METHOD`, indicates whether you want the data to be analyzed using ARTXP, ARIMA, or a mixture of both.  
  
     The keyword, `WITH DRILLTHROUGH`, specify that you want to be able to view detailed statistics in the source data after the model is complete. You must add this clause if you want to browse the model by using the Microsoft Time Series Viewer. It is not required for prediction.  
  
     The complete statement should now be as follows:  
  
    ```  
    CREATE MINING MODEL [Forecasting_MIXED]  
         (  
        [Reporting Date] DATE KEY TIME,  
        [Model Region] TEXT KEY,  
        [Quantity] LONG CONTINUOUS PREDICT,  
        [Amount] DOUBLE CONTINUOUS PREDICT  
        )  
    USING Microsoft_Time_Series (AUTO_DETECT_PERIODICITY = 0.8, FORECAST_METHOD = 'MIXED')  
    WITH DRILLTHROUGH  
  
    ```  
  
6.  On the **File** menu, click **Save DMXQuery1.dmx As**.  
  
7.  In the **Save As** dialog box, browse to the appropriate folder, and name the file `Forecasting_MIXED.dmx`.  
  
## Executing the Query  
 The final step is to execute the query. After a query is created and saved, it needs to be executed to create the mining model and its mining structure on the server. For more information about executing queries in Query Editor, see [Database Engine Query Editor &#40;SQL Server Management Studio&#41;](../relational-databases/scripting/database-engine-query-editor-sql-server-management-studio.md).  
  
#### To execute the query  
  
-   In Query Editor, on the toolbar, click **Execute**.  
  
     The status of the query is displayed in the **Messages** tab at the bottom of Query Editor after the statement finishes executing. Messages should display:  
  
    ```  
    Executing the query   
    Execution complete  
    ```  
  
     A new structure named **Forecasting_MIXED_Structure** now exists on the server, together with the related mining model **Forecasting_MIXED**.  
  
 In the next lesson, you will add a mining model to the **Forecasting_MIXED** mining structure that you just created.  
  
## Next Lesson  
 [Lesson 2: Adding Mining Models to the Time Series Mining Structure](../../2014/tutorials/lesson-2-adding-mining-models-to-the-time-series-mining-structure.md)  
  
## See Also  
 [Mining Model Content for Time Series Models &#40;Analysis Services - Data Mining&#41;](../../2014/analysis-services/data-mining/mining-model-content-for-time-series-models-analysis-services-data-mining.md)   
 [Microsoft Time Series Algorithm Technical Reference](../../2014/analysis-services/data-mining/microsoft-time-series-algorithm-technical-reference.md)  
  
  
