---
title: "Lesson 3: Processing the Time Series Structure and Models | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: analysis-services
ms.topic: conceptual
ms.assetid: 16e27b57-eae1-47a7-a02c-47b6ed487d87
author: minewiskan
ms.author: owend
manager: kfile
---
# Lesson 3: Processing the Time Series Structure and Models
  In this lesson, you will use the [INSERT INTO &#40;DMX&#41;](/sql/dmx/insert-into-dmx) statement to process the time series mining structures and mining models that you created.  
  
 When you process a mining structure, [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] reads the source data and builds the structures that support mining models. You always have to process a mining model and structure when you first create it. If you specify the mining structure when using INSERT INTO, the statement processes the mining structure and all its associated mining models.  
  
 When you add a mining model to a mining structure that has already been processed, you can use the `INSERT INTO MINING MODEL` statement to process just the new mining model by using the existing data.  
  
 For more information about processing mining models, see [Processing Requirements and Considerations &#40;Data Mining&#41;](../../2014/analysis-services/data-mining/processing-requirements-and-considerations-data-mining.md).  
  
## INSERT INTO Statement  
 In order to train the time series mining structure and all its associated mining models, use the [INSERT INTO &#40;DMX&#41;](/sql/dmx/insert-into-dmx) statement. The code in the statement can be broken into the following parts.  
  
-   Identifying the mining structure  
  
-   Listing the columns in the mining structure  
  
-   Defining the training data  
  
 The following is a generic example of the `INSERT INTO` statement:  
  
```  
INSERT INTO MINING STRUCTURE [<mining structure name>]  
(  
   <mining structure columns>  
)  
OPENQUERY (<source data definition>)  
```  
  
 The first line of the code identifies the mining structure that you will train:  
  
```  
INSERT INTO MINING STRUCTURE [<mining structure name>]  
```  
  
 The next lines of the code specify the columns that are defined by the mining structure. You must list each column in the mining structure, and each column must map to a column contained within the source query data.  
  
```  
(  
   <mining structure columns>  
)  
```  
  
 The final lines of the code define the data that will be used to train the mining structure.  
  
```  
OPENQUERY (<source data definition>)  
```  
  
 In this lesson, you use `OPENQUERY` to define the source data. For more information about other methods of defining a query on the source data, see [&#60;source data query&#62;](/sql/dmx/source-data-query).  
  
## Lesson Tasks  
 You will perform the following task in this lesson:  
  
-   Process the mining structure Forecasting_MIXED_Structure  
  
-   Process the related mining models Forecasting_MIXED, Forecasting_ARIMA, and Forecasting_ARTXP  
  
## Processing the Time Series Mining Structure  
  
#### To process the mining structure and related mining models by using INSERT INTO  
  
1.  In **Object Explorer**, right-click the instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)], point to **New Query**, and then click **DMX**.  
  
     Query Editor opens and contains a new, blank query.  
  
2.  Copy the generic example of the INSERT INTO statement into the blank query.  
  
3.  Replace the following:  
  
    ```  
    [<mining structure>]  
    ```  
  
     with:  
  
    ```  
    Forecasting_MIXED_Structure  
    ```  
  
4.  Replace the following:  
  
    ```  
    <mining structure columns>  
    ```  
  
     with:  
  
    ```  
    [ReportingDate],  
    [ModelRegion]   
    ```  
  
5.  Replace the following:  
  
    ```  
    OPENQUERY(<source data definition>)  
    ```  
  
     with:  
  
    ```  
    OPENQUERY([Adventure Works DW 2008R2],'SELECT [ReportingDate], [ModelRegion], [Quantity], [Amount]  
    FROM vTimeSeries ORDER BY [ReportingDate]')  
    ```  
  
     The source query references the  [!INCLUDE[ssSampleDBDWobject](../includes/sssampledbdwobject-md.md)] data source defined in the IntermediateTutorial sample project. It uses this data source to access the view vTimeSeries. This view contains the source data that will be used to train the mining model. If you are not familiar with this project or this views, see[Lesson 2: Building a Forecasting Scenario &#40;Intermediate Data Mining Tutorial&#41;](../../2014/tutorials/lesson-2-building-a-forecasting-scenario-intermediate-data-mining-tutorial.md).  
  
     The complete statement should now be as follows:  
  
    ```  
    INSERT INTO MINING STRUCTURE [Forecasting_MIXED_Structure]  
    (  
       [ReportingDate],[ModelRegion],[Quantity],[Amount])  
    )  
    OPENQUERY(  
    [Adventure Works DW 2008R2],  
    'SELECT [ReportingDate],[ModelRegion],[Quantity],[Amount] FROM vTimeSeries ORDER BY [ReportingDate]'  
    )   
    ```  
  
6.  On the **File** menu, click **Save DMXQuery1.dmx As**.  
  
7.  In the **Save As** dialog box, browse to the appropriate folder, and name the file `ProcessForecastingAll.dmx`.  
  
8.  On the toolbar, click the **Execute** button.  
  
 After the query has finished running, you can create predictions by using the processed mining models. In the next lesson, you will create several predictions based on the mining models that you created.  
  
## Next Lesson  
 [Lesson 4: Creating Time Series Predictions Using DMX](../../2014/tutorials/lesson-4-creating-time-series-predictions-using-dmx.md)  
  
## See Also  
 [Processing Requirements and Considerations &#40;Data Mining&#41;](../../2014/analysis-services/data-mining/processing-requirements-and-considerations-data-mining.md)   
 [&#60;source data query&#62;](/sql/dmx/source-data-query)   
 [OPENQUERY &#40;DMX&#41;](/sql/dmx/source-data-query-openquery)  
  
  
