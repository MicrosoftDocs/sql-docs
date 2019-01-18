---
title: "BottomPercent (DMX) | Microsoft Docs"
ms.date: 06/07/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: dmx
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# BottomPercent (DMX)
[!INCLUDE[ssas-appliesto-sqlas](../includes/ssas-appliesto-sqlas.md)]

  Returns, in order of increasing rank, the bottom-most rows of a table whose cumulative total is at least a specified percentage.  
  
## Syntax  
  
```  
  
BottomPercent(<table expression>, <rank expression>, <percent>)  
```  
  
## Arguments  
 *\<Table expression>*  
 The name of a nested table column or table-valued expression.  
  
 *\<rank expression>*  
 A column in the nested table, or expression that evaluates to a column.  
  
 *\<percent>*  
 A double that indicates the total target percentage.  
  
## Result Type  
 A table.  
  
## Remarks  
 The **BottomPercent** function returns the bottom-most rows in increasing order of rank. The rank is based on the evaluated value of the \<rank expression> argument for each row, such that the sum of the \<rank expression> values is at least the given percentage that is specified by the \<percent> argument. **BottomPercent** returns the smallest number of elements possible while still meeting the specified percent value.  
  
## Examples  
 The following example creates a prediction query against the Association model that you built in the [Basic Data Mining Tutorial](https://msdn.microsoft.com/library/6602edb6-d160-43fb-83c8-9df5dddfeb9c).  
  
 To understand how BottomPercent works, it may be helpful to first execute a prediction query that returns only the nested table.  
  
```  
SELECT Predict ([Association].[v Assoc Seq Line Items], INCLUDE_STATISTICS, 10)  
FROM   
     [Association]  
NATURAL PREDICTION JOIN  
SELECT (SELECT 'Women''s Mountain Shorts' as [Model]) AS [v Assoc Seq Line Items]) AS t  
```  
  
> [!NOTE]  
>  In this example, the value supplied as input contains a single quotation mark, and therefore must be escaped by prefacing it with another single quotation mark. If you are not sure of the syntax for inserting an escape character, you can use the Prediction Query Builder to create the query. When you select the value from the dropdown list, the required escape character is inserted for you. For more information, see [Create a Singleton Query in the Data Mining Designer](../analysis-services/data-mining/create-a-singleton-query-in-the-data-mining-designer.md).  
  
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
  
 The BottomPercent function takes the results of this query and returns the smallest-valued rows that sum to the specified percentage.  
  
```  
SELECT   
BottomPercent  
    (  
    Predict ([Association].[v Assoc Seq Line Items],INCLUDE_STATISTICS,10),  
    $SUPPORT,  
    50)  
FROM   
     [Association]  
NATURAL PREDICTION JOIN  
(SELECT (SELECT 'Women''s Mountain Shorts' as [Model]) AS [v Assoc Seq Line Items]) AS t  
```  
  
 The first argument to the BottomPercent function is the name of a table column. In this example, the nested table is returned by calling the Predict function and using the INCLUDE_STATISTICS argument.  
  
 The second argument to the BottomPercent function is the column in the nested table that you use to order the results. In this example, the INCLUDE_STATISTICS option returns the columns $SUPPORT, $PROBABILTY, and $ADJUSTED PROBABILITY. This example uses $SUPPORT because support values are not fractional and therefore are easier to verify.  
  
 The third argument to the BottomPercent function specifies the percentage, as a double. To get the rows that represent the bottom 50 percent of the support, you type 50.  
  
 Example results:  
  
|Model|$SUPPORT|$PROBABILITY|$ADJUSTEDPROBABILITY|  
|-----------|--------------|------------------|--------------------------|  
|Road Bottle Cage|1195|0.080314537|0.077173962|  
|Mountain Bottle Cage|1367|0.091874454|0.087780332|  
|Fender Set - Mountain|1415|0.095100477|0.090718432|  
|Cycling Cap|1473|0.098998589|0.094256014|  
|Road Tire Tube|1588|0.106727603|0.101229538|  
|Mountain-200|1755|0.117951475|0.111260823|  
|Mountain Tire Tube|1992|0.133879965|0.125304948|  
  
 **Note** This example is provided only to illustrate the usage of BottomPercent. Depending on the size of your data set, this query might take a long time to run.  
  
> [!WARNING]  
>  The MDX functions for TOPPERCENT and BOTTOMPERCENT can generate unexpected results when the values used to calculate the percentage include negative numbers. This behavior does not affect the DMX functions. For more information, see [BottomPercent &#40;MDX&#41;](../mdx/bottompercent-mdx.md).  
  
## See Also  
 [Data Mining Extensions &#40;DMX&#41; Function Reference](../dmx/data-mining-extensions-dmx-function-reference.md)   
 [Functions &#40;DMX&#41;](../dmx/functions-dmx.md)  
  
  
