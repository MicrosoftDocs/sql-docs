---
title: "TopCount (DMX)"
description: "TopCount (DMX)"
author: minewiskan
ms.author: owend
ms.reviewer: owend
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: dmx
---
# TopCount (DMX)
[!INCLUDE[ssas](../includes/applies-to-version/ssas.md)]

  Returns the specified number of top-most rows in decreasing order of rank as specified by an expression.  
  
## Syntax  
  
```  
  
TopCount(<table expression>, <rank expression>, <count>)  
```  
  
## Applies To  
 An expression that returns a table, such as a \<table column reference>, or a function that returns a table.  
  
## Return Type  
 \<table expression>  
  
## Remarks  
 The value that is supplied by the \<rank expression> argument determines the decreasing order of rank for the rows that are supplied in the \<table expression> argument, and the number of top-most rows that is specified in the \<count> argument is returned.  
  
 The TopCount function was originally introduced to enable associative predictions and in general, produces the same results as a statement that includes **SELECT TOP** and **ORDER BY** clauses. You will obtain better performance for associative predictions if you use the **Predict (DMX)** function, which supports specification of a number of predictions to return.  
  
 However, there are situations where you might still need to use TopCount. For example, DMX does not support the **TOP** qualifier in a sub-select statement. The [PredictHistogram &#40;DMX&#41;](../dmx/predicthistogram-dmx.md) function also does not support the addition of **TOP**.  
  
## Examples  
 The following examples are prediction queries against the Association model that you build by using the [Basic Data Mining Tutorial](/previous-versions/sql/sql-server-2016/ms167167(v=sql.130)). The queries return the same results, but the first example uses TopCount, and the second example uses the Predict function.  
  
 To understand how TopCount works, it may be helpful to first execute a prediction query that returns only the nested table.  
  
```  
SELECT Predict ([Association].[v Assoc Seq Line Items], INCLUDE_STATISTICS, 10)  
FROM   
     [Association]  
NATURAL PREDICTION JOIN  
SELECT (SELECT 'Women''s Mountain Shorts' as [Model]) AS [v Assoc Seq Line Items]) AS t  
```  
  
> [!NOTE]  
>  In this example, the value supplied as input contains a single quotation mark, and therefore must be escaped by prefacing it with another single quotation mark. If you are not sure of the syntax for inserting an escape character, you can use the Prediction Query Builder to create the query. When you select the value from the dropdown list, the required escape character is inserted for you. For more information, see [Create a Singleton Query in the Data Mining Designer](/analysis-services/data-mining/create-a-singleton-query-in-the-data-mining-designer).  
  
 Example results:  
  
|Model|$SUPPORT|$PROBABILITY|$ADJUSTEDPROBABILITY|  
|-----------|--------------|------------------|--------------------------|  
|Sport-100|4334|0.291283016|0.252695851|  
|Water Bottle|2866|0.192620472|0.175205052|  
|Patch kit|2113|0.142012232|0.132389356|  
|Mountain Tire Tube|1992|0.133879965|0.125304948|  
|Mountain-200|1755|0.117951475|0.111260823|  
|Road Tire Tube|1588|0.106727603|0.101229538|  
|Cycling Cap|1473|0.098998589|0.094256014|  
|Fender Set - Mountain|1415|0.095100477|0.090718432|  
|Mountain Bottle Cage|1367|0.091874454|0.087780332|  
|Road Bottle Cage|1195|0.080314537|0.077173962|  
  
 The TopCount function takes the results of this query and returns the specified number of the smallest-valued rows.  
  
```  
SELECT   
TopCount  
    (  
    Predict ([Association].[v Assoc Seq Line Items],INCLUDE_STATISTICS,10),  
    $SUPPORT,  
    3)  
FROM   
     [Association]  
NATURAL PREDICTION JOIN  
(SELECT (SELECT 'Women''s Mountain Shorts' as [Model]) AS [v Assoc Seq Line Items]) AS t  
```  
  
 The first argument to the TopCount function is the name of a table column. In this example, the nested table is returned by calling the Predict function and using the INCLUDE_STATISTICS argument.  
  
 The second argument to the TopCount function is the column in the nested table that you use to order the results. In this example, the INCLUDE_STATISTICS option returns the columns $SUPPORT, $PROBABILTY, and $ADJUSTED PROBABILITY. This example uses $SUPPORT to rank the results.  
  
 The third argument to the TopCount function specifies the number of rows to return, as an integer. To get the top three products, as ordered by $SUPPORT, you type 3.  
  
 Example results:  
  
|Model|$SUPPORT|$PROBABILITY|$ADJUSTEDPROBABILITY|  
|-----------|--------------|------------------|--------------------------|  
|Sport-100|4334|0.29...|0.25...|  
|Water Bottle|2866|0.19...|0.17...|  
|Patch kit|2113|0.14...|0.13...|  
  
 However, this type of query might affect performance in a production setting. This is because the query returns a set of all predictions from the algorithm, sorts these predictions, and returns the top 3.  
  
 The following example provides an alternative statement that returns the same results but executes significantly faster. This example replaces TopCount with the Predict function, which accepts a number of predictions as an argument. This example also uses the **$SUPPORT** keyword to directly retrieve the nested table column.  
  
```  
SELECT Predict ([Association].[v Assoc Seq Line Items], INCLUDE_STATISTICS, 3, $SUPPORT)  
```  
  
 The results contain the top 3 predictions sorted by the support value. You can replace $SUPPORT with $PROBABILITY or $ADJUSTED_PROBABILITY to return predictions ranked by probability or adjusted probability. For more information, see **Predict (DMX)**.  
  
## See Also  
 [Functions &#40;DMX&#41;](../dmx/functions-dmx.md)   
 [General Prediction Functions &#40;DMX&#41;](../dmx/general-prediction-functions-dmx.md)   
 [BottomCount &#40;DMX&#41;](../dmx/bottomcount-dmx.md)   
 [TopPercent &#40;DMX&#41;](../dmx/toppercent-dmx.md)   
 [TopSum &#40;DMX&#41;](../dmx/topsum-dmx.md)  
  
