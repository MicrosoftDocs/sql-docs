---
title: "Lesson 2: Adding Mining Models to the Time Series Mining Structure | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 75c2a74b-21ce-44fb-a26b-68be4c685c12
author: minewiskan
ms.author: owend
manager: craigg
---
# Lesson 2: Adding Mining Models to the Time Series Mining Structure
  In this lesson, you will add a new mining model to the mining structure that you just created in [Lesson 1: Creating a Time Series Mining Model and Mining Structure](../../2014/tutorials/lesson-1-creating-a-time-series-mining-model-and-mining-structure.md).  
  
## ALTER MINING STRUCTURE Statement  
 In order to add a new mining model to an existing mining structure, you use the [ALTER MINING STRUCTURE &#40;DMX&#41;](/sql/dmx/alter-mining-structure-dmx?view=sql-server-2016) statement. The code in the statement can be broken into the following parts:  
  
-   Identifying the mining structure  
  
-   Naming the mining model  
  
-   Defining the key column  
  
-   Defining the predictable columns  
  
-   Specifying the algorithm and any parameter changes  
  
 The following is a generic example of the ALTER MINING STRUCTURE statement:  
  
```  
ALTER MINING STRUCTURE [<mining structure name>]  
ADD MINING MODEL [<mining model name>]  
   ([<key columns>],  
    <mining model columns>  
   )  
USING <algorithm name>([<algorithm parameters>])  
[WITH DRILLTHROUGH]  
```  
  
 The first line of the code identifies the existing mining structure to which the mining models will be added:  
  
```  
ALTER MINING STRUCTURE [<mining structure name>]  
```  
  
 The next line of the code names the mining model that will be added to the mining structure:  
  
```  
ADD MINING MODEL [<mining model name>]  
```  
  
 For information about naming an object in DMX, see [Identifiers &#40;DMX&#41;](/sql/dmx/identifiers-dmx).  
  
 The next lines of the code define columns from the mining structure that will be used by the mining model:  
  
```  
[<key columns>],  
<mining model columns>  
```  
  
 You can only use columns that already exist in the mining structure, and the first column in the list must be the key column from the mining structure.  
  
 The next lines of the code defines the mining algorithm that generates the mining model and the algorithm parameters that you can set on the algorithm, and specify whether you can drill down from the mining model into view detailed data in the training cases:  
  
```  
USING <algorithm name>([<algorithm parameters>])  
WITH DRILLTHROUGH  
```  
  
 For more information about the algorithm parameters that you can adjust, see [Microsoft Time Series Algorithm Technical Reference](../../2014/analysis-services/data-mining/microsoft-time-series-algorithm-technical-reference.md).  
  
 You can specify that a column in the mining model be used for prediction by using the following syntax:  
  
```  
<mining model column> PREDICT  
```  
  
## Lesson Tasks  
 You will perform the following tasks in this lesson:  
  
-   Add a new time series mining model to the structure.  
  
-   Change the algorithm parameters to use a different method of analysis and prediction  
  
## Adding an ARIMA Time Series Model to the Structure  
 The first step is to add a new forecasting mining model to the existing structure. By default, the [!INCLUDE[msCoName](../includes/msconame-md.md)] Time Series algorithm creates time series mining models by using two algorithms, ARIMA and ARTXP, and blending the results. However, you can specify a single algorithm to use, or you can specify the exact blend of algorithms. In this step, you will add a new model that uses only the ARIMA algorithm. This algorithm is optimized for long-term prediction.  
  
#### To add an ARIMA time series mining model  
  
1.  In **Object Explorer**, right-click the instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)], point to **New Query**, and then click **DMX** to open Query Editor and a new, blank query.  
  
2.  Copy the generic example of the ALTER MINING STRUCTURE statement into the blank query.  
  
3.  Replace the following:  
  
    ```  
    <mining structure name>   
    ```  
  
     with:  
  
    ```  
    [Forecasting_MIXED_Structure]  
    ```  
  
4.  Replace the following:  
  
    ```  
    <mining model name>   
    ```  
  
     with:  
  
    ```  
    Forecasting_ARIMA  
    ```  
  
5.  Replace the following:  
  
    ```  
    <key columns>,  
    ```  
  
     with:  
  
    ```  
    [ReportingDate],  
    [ModelRegion]  
    ```  
  
     Note that you do not need to repeat any of the date type or content type information that you provided in the CREATE MINING MODEL statement, because this information is already stored in the mining structure.  
  
6.  Replace the following:  
  
    ```  
    <mining model columns>  
    ```  
  
     with:  
  
    ```  
    ([Quantity] PREDICT,  
    [Amount] PREDICT  
    )  
    ```  
  
7.  Replace the following:  
  
    ```  
    USING <algorithm name>([<algorithm parameters>])   
    [WITH DRILLTHROUGH]  
    ```  
  
     with:  
  
    ```  
    USING Microsoft_Time_Series (AUTO_DETECT_PERIODICITY = .08, FORECAST_METHOD = 'ARIMA')  
    WITH DRILLTHROUGH  
    ```  
  
     The resulting statement should now be as follows:  
  
    ```  
    ALTER MINING STRUCTURE [Forecasting_MIXED_Structure]  
    ADD MINING MODEL [Forecasting_ARIMA]  
       (  
       ([ReportingDate],  
        [ModelRegion],  
        ([Quantity] PREDICT,  
        [Amount] PREDICT  
       )   
    USING Microsoft_Time_Series (AUTO_DETECT_PERIODICITY = .08, FORECAST_METHOD = 'ARIMA')  
    WITH DRILLTHROUGH  
    ```  
  
8.  On the **File** menu, click **Save DMXQuery1.dmx As**.  
  
9. In the **Save As** dialog box, browse to the appropriate folder, and name the file `Forecasting_ARIMA.dmx`.  
  
10. On the toolbar, click the **Execute** button.  
  
## Adding an ARTXP Time Series Model to the Structure  
 The ARTXP algorithm was the default time series algorithm in SQL Server 2005 and is optimized for short-term prediction. To compare predictions by using all three time series algorithms, you will add one more model that is based on the ARTXP algorithm.  
  
#### To add an ARTXP time series mining model  
  
1.  Copy the following code into a blank query window.  
  
     Note that you do not need to change anything except the name of the new mining model, and the value of the FORECAST_METHOD parameter.  
  
    ```  
    ALTER MINING STRUCTURE [Forecasting_MIXED_Structure]  
    ADD MINING MODEL [Forecasting_ARTXP]  
       (  
       ([ReportingDate],  
        [ModelRegion],  
        ([Quantity] PREDICT,  
        [Amount] PREDICT  
       )   
    USING Microsoft_Time_Series (AUTO_DETECT_PERIODICITY = .08, FORECAST_METHOD = 'ARTXP')  
    WITH DRILLTHROUGH  
    ```  
  
2.  On the **File** menu, click **Save DMXQuery1.dmx As**.  
  
3.  In the **Save As** dialog box, browse to the appropriate folder, and name the file `Forecasting_ARTXP.dmx`.  
  
4.  On the toolbar, click the **Execute** button.  
  
 In the next lesson, you will process all of the models and the mining structure.  
  
## Next Lesson  
 [Lesson 3: Processing the Time Series Structure and Models](../../2014/tutorials/lesson-3-processing-the-time-series-structure-and-models.md)  
  
## See Also  
 [Microsoft Time Series Algorithm](../../2014/analysis-services/data-mining/microsoft-time-series-algorithm.md)   
 [Microsoft Time Series Algorithm Technical Reference](../../2014/analysis-services/data-mining/microsoft-time-series-algorithm-technical-reference.md)  
  
  
