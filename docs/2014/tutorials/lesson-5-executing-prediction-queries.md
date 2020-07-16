---
title: "Lesson 5: Executing Prediction Queries | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: analysis-services
ms.topic: conceptual
ms.assetid: 0037bd2f-aa2d-464b-bf86-b0210f0438b1
author: minewiskan
ms.author: owend
manager: kfile
---
# Lesson 5: Executing Prediction Queries
  In this lesson, you will use the [SELECT FROM \<model> PREDICTION JOIN (DMX)](/sql/dmx/select-from-model-cases-dmx) form of the SELECT statement to create two different types of predictions based on the decision tree model you created in [Lesson 2: Adding Mining Models to the Association Mining Structure](../../2014/tutorials/lesson-2-adding-mining-models-to-the-market-basket-mining-structure.md). These prediction types are defined below.  
  
 Singleton Query  
 Use a singleton query to provide ad hoc values when making predictions. For example, you can determine whether a single customer is likely to be a bike buyer, by passing inputs to the query such as the commute distance, the area code, or the number of children of the customer. The singleton query returns a value that indicates how likely the person is to purchase a bicycle based on those inputs.  
  
 Batch Query  
 Use a batch query to determine who in a table of potential customers is likely to purchase a bicycle. For example, if your marketing department provides you with a list of customers and customer attributes, then you can use a batch prediction to determine who from the table is likely to purchase a bicycle.  
  
 The [SELECT FROM \<model> PREDICTION JOIN (DMX)](/sql/dmx/select-from-model-cases-dmx) form of the SELECT statement contains three parts:  
  
-   A list of the mining model columns and prediction functions that are returned in the results. The results can also contain input columns from the source data.  
  
-   The source query defining the data that is being used to create a prediction. For example, in a batch query this could be a list of customers.  
  
-   A mapping between the mining model columns and the source data. If these names match, then you can use NATURAL syntax and leave out the column mappings.  
  
 You can further enhance the query by using prediction functions. Prediction functions provide additional information, such as the probability of a prediction occurring, and provide support for the prediction in the training dataset. For more information about prediction functions, see [Functions &#40;DMX&#41;](/sql/dmx/functions-dmx).  
  
 The predictions in this tutorial are based on the ProspectiveBuyer table in the [!INCLUDE[ssSampleDBDWobject](../includes/sssampledbdwobject-md.md)] sample database. The ProspectiveBuyer table contains a list of potential customers and their associated characteristics. The customers in this table are independent of the customers that were used to create the decision tree mining model.  
  
 You can also create predictions by using the prediction query builder in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)].  
  
## Lesson Tasks  
 You will perform the following tasks in this lesson:  
  
-   Create a singleton query to determine whether a specific customer is likely to purchase a bicycle.  
  
-   Create a batch query to determine which customers, listed in a table of customers, are likely to purchase a bicycle.  
  
## Singleton Query  
 The first step is to use the [SELECT FROM &#60;model&#62; PREDICTION JOIN &#40;DMX&#41;](/sql/dmx/select-from-model-cases-dmx) in a singleton prediction query. The following is a generic example of the singleton statement:  
  
```  
SELECT <select list> FROM [<mining model name>]   
NATURAL PREDICTION JOIN  
(SELECT '<value>' AS [<column>], ...)  
AS [<input alias>]  
```  
  
 The first line of the code defines the columns from the mining model that the query should return, and specifies the mining model that is used to generate the prediction:  
  
```  
SELECT <select list> FROM [<mining model name>]   
```  
  
 The next lines of the code define the characteristics of the customer that you use to create a prediction:  
  
```  
NATURAL PREDICTION JOIN  
(SELECT '<value>' AS [<column>], ...)  
AS [<input alias>]  
ORDER BY <expression>  
```  
  
 If you specify NATURAL PREDICTION JOIN, the server matches each column from the model to a column from the input, based on column names. If column names do not match, the columns are ignored.  
  
#### To create a singleton prediction query  
  
1.  In **Object Explorer**, right-click the instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)], point to **New Query**, and then click **DMX**.  
  
     Query Editor opens and contains a new, blank query.  
  
2.  Copy the generic example of the singleton statement into the blank query.  
  
3.  Replace the following:  
  
    ```  
    <select list>   
    ```  
  
     with:  
  
    ```  
    [Bike Buyer] AS Buyer, PredictHistogram([Bike Buyer]) AS Statistics  
    ```  
  
     The AS statement is used to alias columns returned by the query. The [PredictHistogram](/sql/dmx/predicthistogram-dmx) function returns statistics about the prediction, including the probability and the support. For more information about the functions that can be used in a prediction statement, see [Functions &#40;DMX&#41;](/sql/dmx/functions-dmx).  
  
4.  Replace the following:  
  
    ```  
    [<mining model>]   
    ```  
  
     with:  
  
    ```  
    [Decision Tree]  
    ```  
  
5.  Replace the following:  
  
    ```  
    (SELECT '<value>' AS [<column name>], ...)  AS t  
    ```  
  
     with:  
  
    ```  
    (SELECT 35 AS [Age],  
      '5-10 Miles' AS [Commute Distance],  
      '1' AS [House Owner Flag],  
      2 AS [Number Cars Owned],  
      2 AS [Total Children]) AS t  
    ```  
  
     The complete statement should now be as follows:  
  
    ```  
    SELECT  
       [Bike Buyer] AS Buyer,  
       PredictHistogram([Bike Buyer]) AS Statistics  
    FROM  
       [Decision Tree]  
    NATURAL PREDICTION JOIN  
    (SELECT 35 AS [Age],  
       '5-10 Miles' AS [Commute Distance],  
       '1' AS [House Owner Flag],  
       2 AS [Number Cars Owned],  
       2 AS [Total Children]) AS t  
    ```  
  
6.  On the **File** menu, click **Save DMXQuery1.dmx As**.  
  
7.  In the **Save As** dialog box, browse to the appropriate folder, and name the file `Singleton_Query.dmx`.  
  
8.  On the toolbar, click the **Execute** button.  
  
     The query returns a prediction about whether a customer with the specified characteristics will purchase a bicycle, as well as statistics about that prediction.  
  
## Batch Query  
 The next step is to use the [SELECT FROM &#60;model&#62; PREDICTION JOIN &#40;DMX&#41;](/sql/dmx/select-from-model-cases-dmx) in a batch prediction query. The following is a generic example of a batch statement:  
  
```  
SELECT TOP <number> <select list>   
FROM [<mining model name>]  
PREDICTION JOIN  
OPENQUERY([<datasource>],'<SELECT statement>')  
  AS [<input alias>]  
ON <on clause, mapping,>  
WHERE <where clause, boolean expression,>  
ORDER BY <expression>  
```  
  
 As in the singleton query, the first two lines of the code define the columns from mining model that the query returns, as well as the name of the mining model that is used to generate the prediction. The TOP \<number> statement specifies that the query will only return the number or the results specified by \<number>.  
  
 The next lines of the code define the source data that the predictions are based on:  
  
```  
OPENQUERY([<datasource>],'<SELECT statement>')  
  AS [<input alias>]  
```  
  
 You have several options for the method of retrieving the source data, but in this tutorial, you will use OPENQUERY. For more information about the options available, see [&#60;source data query&#62;](/sql/dmx/source-data-query).  
  
 The next line defines the mapping between the source columns in the mining model and the columns in the source data:  
  
```  
ON <column mappings>  
```  
  
 The WHERE clause filters the results returned by the prediction query:  
  
```  
WHERE <where clause, boolean expression,>  
```  
  
 The last and optional line of the code specifies the column that the results will be ordered by:  
  
```  
ORDER BY <expression> [DESC|ASC]  
```  
  
 Use ORDER BY in combination with the TOP \<number> statement, to filter the results that are returned. For example, in this prediction you will return the top ten bike buyers, ordered by the probability of the prediction being correct. You can use [DESC|ASC] syntax to control the order in which the results are displayed.  
  
#### To create a batch prediction query  
  
1.  In **Object Explorer**, right-click the instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)], point to **New Query**, and then click **DMX**.  
  
     Query Editor opens and contains a new, blank query.  
  
2.  Copy the generic example of the batch statement into the blank query.  
  
3.  Replace the following:  
  
    ```  
    <select list>   
    ```  
  
     with:  
  
    ```  
    SELECT  
      TOP 10  
      t.[LastName],  
      t.[FirstName],  
      [Decision Tree].[Bike Buyer],  
      PredictProbability([Bike Buyer])  
    ```  
  
     The TOP 10 clause specifies that only the top ten results will be returned by the query. The ORDER BY statement in this query orders the results by the probability of the prediction being correct, so only the ten most likely results will be returned.  
  
4.  Replace the following placeholder:  
  
    ```  
    [<mining model>]   
    ```  
  
     With the name of the model:  
  
    ```  
    [Decision Tree]  
    ```  
  
5.  Replace the following generic OPENQUERY statement:  
  
    ```  
    OPENQUERY([<datasource>],'<SELECT statement>')  
    ```  
  
     With a statement that references the current Adventureworks data warehouse, such as:  
  
    ```  
    OPENQUERY([Adventure Works DW 2014],  
      'SELECT  
        [LastName],  
        [FirstName],  
        [MaritalStatus],  
        [Gender],  
        [YearlyIncome],  
        [TotalChildren],  
        [NumberChildrenAtHome],  
        [Education],  
        [Occupation],  
        [HouseOwnerFlag],  
        [NumberCarsOwned]  
      FROM  
        [dbo].[ProspectiveBuyer]  
      ') AS t  
    ```  
  
6.  Replace the following generic syntax:  
  
    ```  
    <ON clause, mapping,>   
    WHERE <where clause, boolean expression,>  
    ORDER BY <expression>  
    ```  
  
     With the column mappings needed for this model and input data set:  
  
    ```  
    [Decision Tree].[Marital Status] = t.[MaritalStatus] AND  
      [Decision Tree].[Gender] = t.[Gender] AND  
      [Decision Tree].[Yearly Income] = t.[YearlyIncome] AND  
      [Decision Tree].[Total Children] = t.[TotalChildren] AND  
      [Decision Tree].[Number Children At Home] = t.[NumberChildrenAtHome] AND  
      [Decision Tree].[Education] = t.[Education] AND  
      [Decision Tree].[Occupation] = t.[Occupation] AND  
      [Decision Tree].[House Owner Flag] = t.[HouseOwnerFlag] AND  
      [Decision Tree].[Number Cars Owned] = t.[NumberCarsOwned]  
    WHERE [Decision Tree].[Bike Buyer] =1  
    ORDER BY PredictProbability([Bike Buyer]) DESC  
    ```  
  
     Specify `DESC` in order to list the results with the highest probability first.  
  
     The complete statement should now be as follows:  
  
    ```  
    SELECT  
      TOP 10  
      t.[LastName],  
      t.[FirstName],  
      [Decision Tree].[Bike Buyer],  
      PredictProbability([Bike Buyer])  
    FROM  
      [Decision Tree]  
    PREDICTION JOIN  
      OPENQUERY([Adventure Works DW 2014],  
        'SELECT  
          [LastName],  
          [FirstName],  
          [MaritalStatus],  
          [Gender],  
          [YearlyIncome],  
          [TotalChildren],  
          [NumberChildrenAtHome],  
          [Education],  
          [Occupation],  
          [HouseOwnerFlag],  
          [NumberCarsOwned]  
        FROM  
          [dbo].[ProspectiveBuyer]  
        ') AS t  
    ON  
      [Decision Tree].[Marital Status] = t.[MaritalStatus] AND  
      [Decision Tree].[Gender] = t.[Gender] AND  
      [Decision Tree].[Yearly Income] = t.[YearlyIncome] AND  
      [Decision Tree].[Total Children] = t.[TotalChildren] AND  
      [Decision Tree].[Number Children At Home] = t.[NumberChildrenAtHome] AND  
      [Decision Tree].[Education] = t.[Education] AND  
      [Decision Tree].[Occupation] = t.[Occupation] AND  
      [Decision Tree].[House Owner Flag] = t.[HouseOwnerFlag] AND  
      [Decision Tree].[Number Cars Owned] = t.[NumberCarsOwned]  
    WHERE [Decision Tree].[Bike Buyer] =1  
    ORDER BY PredictProbability([Bike Buyer]) DESC  
    ```  
  
7.  On the **File** menu, click **Save DMXQuery1.dmx As**.  
  
8.  In the **Save As** dialog box, browse to the appropriate folder, and name the file `Batch_Prediction.dmx`.  
  
9. On the toolbar, click the **Execute** button.  
  
     The query returns a table containing customer names, a prediction of whether each customer will purchase a bicycle, and the probability of the prediction.  
  
 This is the last step in the Bike Buyer tutorial. You now have a set of mining models that you can use to explore similarities between you customers and predict whether potential customers will purchase a bicycle.  
  
 To learn how to use DMX in a Market Basket scenario, see [Market Basket DMX Tutorial](../../2014/tutorials/market-basket-dmx-tutorial.md).  
  
  
