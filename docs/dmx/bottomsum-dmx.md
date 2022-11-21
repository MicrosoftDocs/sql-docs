---
title: "BottomSum (DMX)"
description: "BottomSum (DMX)"
author: minewiskan
ms.author: owend
ms.reviewer: owend
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: dmx
---
# BottomSum (DMX)
[!INCLUDE[ssas](../includes/applies-to-version/ssas.md)]

  Returns, in order of increasing rank, the bottom-most rows of a table whose cumulative total is at least a specified value.  
  
## Syntax  
  
```  
  
BottomSum(<table expression>, <rank expression>, <sum>)  
```  
  
## Applies To  
 An expression that returns a table, such as a \<table column reference>, or a function that returns a table.  
  
## Return Type  
 \<table expression>  
  
## Remarks  
 The **BottomSum** function returns the bottom-most rows in increasing order of rank. The rank is based on the evaluated value of the \<rank expression> argument for each row, such that the sum of the \<rank expression> values is at least the given total that is specified by the \<sum> argument. **BottomSum** returns the smallest number of elements possible while still meeting the specified sum value.  
  
## Examples  
 The following example creates a prediction query against the Association model that you build by using the [Basic Data Mining Tutorial](/previous-versions/sql/sql-server-2016/ms167167(v=sql.130)).  
  
 To understand how BottomSum works, it might be helpful to first execute a prediction query that returns only the nested table.  
  
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
  
 The BottomSum function takes the results of this query and returns the rows with the lowest values that sum to the specified count.  
  
```  
SELECT   
BottomSum  
    (  
    Predict([Association].[v Assoc Seq Line Items],INCLUDE_STATISTICS,10),  
    $PROBABILITY,  
    .1)  
FROM   
     [Association]  
NATURAL PREDICTION JOIN  
(SELECT (SELECT 'Women''s Mountain Shorts' as [Model]) AS [v Assoc Seq Line Items]) AS t  
```  
  
 The first argument to the BottomSum function is the name of a table column. In this example, the nested table is returned by calling the Predict function and using the INCLUDE_STATISTICS argument.  
  
 The second argument to the BottomSum function is the column in the nested table that you use to order the results. In this example, the INCLUDE_STATISTICS option returns the columns $SUPPORT, $PROBABILTY, and $ADJUSTED PROBABILITY. This example uses $PROBABILITY to return rows that sum to at least 50% probability.  
  
 The third argument to the BottomSum function specifies the target sum, as a double. To get the rows for the lowest-count products that sum to 10 percent probability, you type .1.  
  
 Example results:  
  
|Model|$SUPPORT|$PROBABILITY|$ADJUSTEDPROBABILITY|  
|-----------|--------------|------------------|--------------------------|  
|Road Bottle Cage|1195|0.08...|0.07...|  
|Mountain Bottle Cage|1367|0.09...|0.08...|  
  
 **Note** This example is provided only to illustrate the usage of BottomSum. Depending on the size of your data set, this query might take a long time to run.  
  
## See Also  
 [Functions &#40;DMX&#41;](../dmx/functions-dmx.md)   
 [General Prediction Functions &#40;DMX&#41;](../dmx/general-prediction-functions-dmx.md)   
 [BottomPercent &#40;DMX&#41;](../dmx/bottompercent-dmx.md)  
  
