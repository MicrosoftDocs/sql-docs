---
title: "Lesson 4: Executing Market Basket Predictions | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: analysis-services
ms.topic: conceptual
ms.assetid: b3238f1b-ea04-4253-ade2-838a806b62fe
author: minewiskan
ms.author: owend
manager: kfile
---
# Lesson 4: Executing Market Basket Predictions
  In this lesson, you will use the DMX `SELECT` statement to create predictions based on the association models you created in [Lesson 2: Adding Mining Models to the Market Basket Mining Structure](../../2014/tutorials/lesson-2-adding-mining-models-to-the-market-basket-mining-structure.md). A prediction query is created by using the DMX `SELECT` statement and adding a `PREDICTION JOIN` clause. For more information about the syntax of a prediction join, see [SELECT FROM &#60;model&#62; PREDICTION JOIN &#40;DMX&#41;](/sql/dmx/select-from-model-cases-dmx).  
  
 The **SELECT FROM \<model> PREDICTION JOIN** form of the `SELECT` statement contains three parts:  
  
-   A list of the mining model columns and prediction functions that are returned in the result set. This list can also contain input columns from the source data.  
  
-   A source query that defines the data that is being used to create a prediction. For example, if you are creating many predictions in a batch, the source query could retrieve a list of customers.  
  
-   A mapping between the mining model columns and the source data. If the columns names match, you can use the `NATURAL PREDICTION JOIN` syntax and omit the column mappings.  
  
 You can enhance the query by using prediction functions. Prediction functions provide additional information, such as the probability of a prediction occurring, or the support for a prediction in the training dataset. For more information about prediction functions, see [Functions &#40;DMX&#41;](/sql/dmx/functions-dmx).  
  
 You can also use the prediction query builder in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] to create prediction queries.  
  
## Singleton PREDICTION JOIN Statement  
 The first step is to create a singleton query, by using the **SELECT FROM \<model> PREDICTION JOIN** syntax and supplying a single set of values as input. The following is a generic example of the singleton statement:  
  
```  
SELECT <select list>  
    FROM [<mining model>]   
[NATURAL] PREDICTION JOIN  
(SELECT '<value>' AS [<column>],   
    (SELECT 'value' AS [<nested column>] UNION  
        SELECT 'value' AS [<nested column>] ...)   
    AS [<nested table>])  
AS [<input alias>]  
```  
  
 The first line of the code defines the columns from the mining model that the query returns, and specifies the name of the mining model used to generate the prediction:  
  
```  
SELECT <select list> FROM [<mining model>]   
```  
  
 The next line of the code indicates the operation to perform. Because you will specify values for each of the columns and type the column names exactly so as to match the model, you can use the `NATURAL PREDICTION JOIN` syntax. However, if the column names were different, you would have to specify mappings between the columns in the model and the columns in the new data by adding an `ON` clause.  
  
```  
[NATURAL] PREDICTION JOIN  
```  
  
 The next lines of the code define the products in the shopping cart that will be used to predict additional products that a customer will add:  
  
```  
(SELECT '<value>' AS [<column>],   
    (SELECT 'value' AS [<nested column>] UNION  
        SELECT 'value' AS [<nested column>] ...)   
    AS [<nested table>])  
```  
  
## Lesson Tasks  
 You will perform the following tasks in this lesson:  
  
-   Create a query that predicts what other items a customer will likely purchase, based on items already existing in their shopping cart. You will create this query by using the mining model with the default *MINIMUM_PROBABILITY*.  
  
-   Create a query that predicts what other items a customer will likely purchase based on items already existing in their shopping cart. This query is based on a different model, in which *MINIMUM_PROBABILITY* has been set to 0.01. Because the default value for *MINIMUM_PROBABILITY* in association models is 0.3, the query on this model should return more possible items than the query on the default model.  
  
## Create a Prediction by Using a Model with the Default MINIMUM_PROBABILITY  
  
#### To create an association query  
  
1.  In **Object Explorer**, right-click the instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)], point to **New Query**, and then click **DMX** to open the Query Editor.  
  
2.  Copy the generic example of the `PREDICTION JOIN` statement into the blank query.  
  
3.  Replace the following:  
  
    ```  
    <select list>   
    ```  
  
     with:  
  
    ```  
    PREDICT([Default Association].[Products],INCLUDE_STATISTICS,3)  
    ```  
  
     You could just include the column name [Products], but by using the [Predict &#40;DMX&#41;](/sql/dmx/predict-dmx) function, you can limit the number of products that are returned by the algorithm to three. You can also use `INCLUDE_STATISTICS`, which returns the support, probability, and adjusted probability for each product. These statistics help you rate the accuracy of the prediction.  
  
4.  Replace the following:  
  
    ```  
    [<mining model>]   
    ```  
  
     with:  
  
    ```  
    [Default Association]  
    ```  
  
5.  Replace the following:  
  
    ```  
    (SELECT '<value>' AS [<column>],   
        (SELECT 'value' AS [<nested column>] UNION  
            SELECT 'value' AS [<nested column>] ...)   
        AS [<nested table>])  
    ```  
  
     with:  
  
    ```  
    (SELECT (SELECT 'Mountain Bottle Cage' AS [Model]  
      UNION SELECT 'Mountain Tire Tube' AS [Model]  
      UNION SELECT 'Mountain-200' AS [Model]) AS [Products]) AS t  
    ```  
  
     This statement uses the `UNION` statement to specify three products that must be included in the shopping cart together with the predicted products. The Model column in the `SELECT` statement corresponds to the model column that is contained in the nested products table.  
  
     The complete statement should now be as follows:  
  
    ```  
    SELECT  
      PREDICT([Default Association].[Products],INCLUDE_STATISTICS,3)  
    From  
      [Default Association]  
    NATURAL PREDICTION JOIN  
    (SELECT (SELECT 'Mountain Bottle Cage' AS [Model]  
      UNION SELECT 'Mountain Tire Tube' AS [Model]  
      UNION SELECT 'Mountain-200' AS [Model]) AS [Products]) AS t  
    ```  
  
6.  On the **File** menu, click **Save DMXQuery1.dmx As**.  
  
7.  In the **Save As** dialog box, browse to the appropriate folder, and name the file `Association Prediction.dmx`.  
  
8.  On the toolbar, click the **Execute** button.  
  
     The query returns a table that contains three products: HL Mountain Tire, Fender Set - Mountain, and ML Mountain Tire. The table lists these returned products in order of probability. The returned product that is most likely to be included in the same shopping cart as the three products specified in the query appears at the top of the table. The two products that follow are the next most likely to be included in the shopping cart. The table also contains statistics describing the accuracy of the prediction.  
  
## Create a Prediction by Using a Model with a MINIMUM_PROBABILITY of 0.01  
  
#### To create an association query  
  
1.  In **Object Explorer**, right-click the instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)], point to **New Query**, and then click **DMX** to open the Query Editor.  
  
2.  Copy the generic example of the `PREDICTION JOIN` statement into the blank query.  
  
3.  Replace the following:  
  
    ```  
    <select list>   
    ```  
  
     with:  
  
    ```  
    PREDICT([Modified Association].[Products],INCLUDE_STATISTICS,3)  
    ```  
  
4.  Replace the following:  
  
    ```  
    [<mining model>]   
    ```  
  
     with:  
  
    ```  
    [Modified Association]  
    ```  
  
5.  Replace the following:  
  
    ```  
    (SELECT '<value>' AS [<column>],   
        (SELECT 'value' AS [<nested column>] UNION  
            SELECT 'value' AS [<nested column>] ...)   
        AS [<nested table>])  
    ```  
  
     with:  
  
    ```  
    (SELECT (SELECT 'Mountain Bottle Cage' AS [Model]  
      UNION SELECT 'Mountain Tire Tube' AS [Model]  
      UNION SELECT 'Mountain-200' AS [Model]) AS [Products]) AS t  
    ```  
  
     This statement uses the `UNION` statement to specify three products that must be included in the shopping cart together with the predicted products. The `[Model]` column in the `SELECT` statement corresponds to the column in the nested products table.  
  
     The complete statement should now be as follows:  
  
    ```  
    SELECT  
      PREDICT([Modified Association].[Products],INCLUDE_STATISTICS,3)  
    From  
      [Modified Association]  
    NATURAL PREDICTION JOIN  
    (SELECT (SELECT 'Mountain Bottle Cage' AS [Model]  
      UNION SELECT 'Mountain Tire Tube' AS [Model]  
      UNION SELECT 'Mountain-200' AS [Model]) AS [Products]) AS t  
    ```  
  
6.  On the **File** menu, click **Save DMXQuery1.dmx As**.  
  
7.  In the **Save As** dialog box, browse to the appropriate folder, and name the file `Modified Association Prediction.dmx`.  
  
8.  On the toolbar, click the **Execute** button.  
  
     The query returns a table that contains three products: HL Mountain Tire, Water Bottle, and Fender Set - Mountain. The table lists these products in order of probability. The product that appears at the top of the table is the product that is most likely to be included in the same shopping cart as the three products specified in the query. The remaining products are the next most likely to be included in the shopping cart. The table also contains statistics that describe the accuracy of the prediction.  
  
     You can see from the results of this query that the value of the *MINIMUM_PROBABILITY* parameter affects the results returned by the query.  
  
 This is the last step in the Market Basket tutorial. You now have a set of models that you can use to predict the products that customers might purchase at the same time.  
  
 To learn how to use DMX in another predictive scenario, see [Bike Buyer DMX Tutorial](../../2014/tutorials/bike-buyer-dmx-tutorial.md).  
  
## See Also  
 [Association Model Query Examples](../../2014/analysis-services/data-mining/association-model-query-examples.md)   
 [Data Mining Query Interfaces](../../2014/analysis-services/data-mining/data-mining-query-tools.md)  
  
  
