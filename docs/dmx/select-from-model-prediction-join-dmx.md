---
title: "SELECT FROM &lt;model&gt; PREDICTION JOIN (DMX)"
description: "SELECT FROM &lt;model&gt; PREDICTION JOIN (DMX)"
author: minewiskan
ms.author: owend
ms.reviewer: owend
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: dmx
---
# SELECT FROM &lt;model&gt; PREDICTION JOIN (DMX)
[!INCLUDE[ssas](../includes/applies-to-version/ssas.md)]

  Uses a mining model to predict the states of columns in an external data source. The **PREDICTION JOIN** statement matches each case from the source query to the model.  
  
## Syntax  
  
```  
  
SELECT [FLATTENED] [TOP <n>] <select expression list>   
FROM <model> | <sub select> [NATURAL] PREDICTION JOIN   
<source data query> [ON <join mapping list>]   
[WHERE <condition expression>]  
[ORDER BY <expression> [DESC|ASC]]  
```  
  
## Arguments  
 *n*  
 Optional. An integer that specifies how many rows to return.  
  
 *select expression list*  
 A comma-separated list of column identifiers and expressions that are derived from the mining model.  
  
 *model*  
 A model identifier.  
  
 *sub select*  
 An embedded select statement.  
  
 *source data query*  
 The source query.  
  
 *join mapping list*  
 Optional. A logical expression that compares columns from the model to columns from the source query.  
  
 *condition expression*  
 Optional. A condition to restrict the values that are returned from the column list.  
  
 *expression*  
 Optional. An expression that returns a scalar value.  
  
## Remarks  
 The ON clause defines the mapping between the columns from the source query and the columns from the mining model. This mapping is used to direct columns from the source query to columns in the mining model so that the columns can be used as inputs to create the predictions. Columns in the \<*join mapping list*> are related by using an equal sign (=), as shown in the following example:  
  
```  
[MiningModel].ColumnA = [source data query].Column1 AND   
[MiningModel].ColumnB = [source data query].Column2 AND  
...  
```  
  
 If you are binding a nested table in the ON clause, ensure that you bind the key column with any non-key columns so that the algorithm can correctly identify which case the record of the nested column belongs to.  
  
 The source query for the prediction join can either be a table or a singleton query.  
  
 You can specify prediction functions that do not return a table expression in the \<*select expression list*> and the \<*condition expression*>.  
  
 **NATURAL PREDICTION JOIN** automatically maps together column names from the source query that match column names in the model. If you use **NATURAL PREDICTION**, you can omit the ON clause.  
  
 The WHERE condition can be applied only to predictable columns or related columns.  
  
 The ORDER by clause can accept only a single column as an argument; that is, you cannot sort on more than one column.  
  
## Example 1: Singleton Query  
 The following example shows how to create a query to predict whether a specific person will buy a bicycle in real time. In this query the data is not stored in a table or other data source, but instead is entered directly into the query. The person in the query has the following traits:  
  
-   35 years old  
  
-   Owns a house  
  
-   Owns two cars  
  
-   Has two children living at home  
  
 Using the TM Decision Tree mining model and the known characteristics about the subject, the query returns a Boolean value that describes whether the person bought the bike and a set of tabular values, returned by the [PredictHistogram &#40;DMX&#41;](../dmx/predicthistogram-dmx.md) function, that describe how the prediction was made.  
  
```  
SELECT  
  [TM Decision Tree].[Bike Buyer],  
  PredictHistogram([Bike Buyer])  
FROM  
  [TM Decision Tree]  
NATURAL PREDICTION JOIN  
(SELECT 35 AS [Age],  
  '5-10 Miles' AS [Commute Distance],  
  '1' AS [House Owner Flag],  
  2 AS [Number Cars Owned],  
  2 AS [Total Children]) AS t  
```  
  
## Example 2: Using OPENQUERY  
 The following example shows how to create a batch prediction query by using a list of potential customers stored in an external dataset. Because the table is part of a data source view that has been defined on an instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)], the query can use [OPENQUERY](../dmx/source-data-query-openquery.md) to retrieve the data. Because the names of the columns in the table are different from those in the mining model, the **ON** clause must be used to map the columns in the table to the columns in the model.  
  
 The query returns the first and last name of each person in the table, together with a Boolean column that indicates whether each person is likely to buy a bike, where 0 means "probably will not buy a bike" and 1 means "probably will buy a bike". The last column contains the probability for the predicted result.  
  
```  
SELECT  
  t.[LastName],  
  t.[FirstName],  
  [TM Decision Tree].[Bike Buyer],  
  PredictProbability([Bike Buyer])  
From  
  [TM Decision Tree]  
PREDICTION JOIN  
  OPENQUERY([Adventure Works DW Multidimensional 2012],  
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
  [TM Decision Tree].[Marital Status] = t.[MaritalStatus] AND  
  [TM Decision Tree].[Gender] = t.[Gender] AND  
  [TM Decision Tree].[Yearly Income] = t.[YearlyIncome] AND  
  [TM Decision Tree].[Total Children] = t.[TotalChildren] AND  
  [TM Decision Tree].[Number Children At Home] = t.[NumberChildrenAtHome] AND  
  [TM Decision Tree].[Education] = t.[Education] AND  
  [TM Decision Tree].[Occupation] = t.[Occupation] AND  
  [TM Decision Tree].[House Owner Flag] = t.[HouseOwnerFlag] AND  
  [TM Decision Tree].[Number Cars Owned] = t.[NumberCarsOwned]  
```  
  
 To restrict the data set to only the customers who are predicted to buy a bike, and then sort the list by customer name, you can add a WHERE clause and an ORDER BY clause to the previous example:  
  
```  
WHERE [BIKE Buyer]  
ORDER BY [LastName] ASC  
```  
  
## Example 3: Predicting Associations  
 The following example shows how to create a prediction by using a model that is built from the [!INCLUDE[msCoName](../includes/msconame-md.md)] Association algorithm. Predictions on an association model can be used to recommend related products. For example, the following query returns the three products that are most likely to be purchased together:  
  
-   Mountain Bottle Cage  
  
-   Mountain Tire Tube  
  
-   Mountain-200  
  
 The [Predict &#40;DMX&#41;](../dmx/predict-dmx.md) function is polymorphic and can be used with all model types. You use the value3 as an argument to the function to limit the number of items that are returned by the query. The **SELECT** list that follows the NATURAL PREDICTION JOIN clause supplies the values to use as input for prediction.  
  
```  
SELECT FLATTENED  
  PREDICT([Association].[v Assoc Seq Line Items], 3)  
FROM  
  [Association]  
NATURAL PREDICTION JOIN  
(SELECT (SELECT 'Mountain Bottle Cage' AS [Model]  
  UNION SELECT 'Mountain Tire Tube' AS [Model]  
  UNION SELECT 'Mountain-200' AS [Model]) AS [v Assoc Seq Line Items ]) AS t  
```  
  
 Example results:  
  
|Expression.Model|  
|----------------------|  
|HL Mountain Tire|  
|Water Bottle|  
|Fender Set - Mountain|  
  
 Because the column that contains the predictable attribute, `[v Assoc Seq Line Items]`, is a table column, the query returns a single column that contains a nested table. By default the nested table column is named `Expression`. If your provider does not support hierarchical rowsets, you can use the **FLATTENED** keyword as shown in this example to make the results easier to view.  
  
## See Also  
 [SELECT &#40;DMX&#41;](../dmx/select-dmx.md)   
 [Data Mining Extensions &#40;DMX&#41; Data Definition Statements](../dmx/dmx-statements-data-definition.md)   
 [Data Mining Extensions &#40;DMX&#41; Data Manipulation Statements](../dmx/dmx-statements-data-manipulation.md)   
 [Data Mining Extensions &#40;DMX&#41; Statement Reference](../dmx/data-mining-extensions-dmx-statements.md)  
  
  
