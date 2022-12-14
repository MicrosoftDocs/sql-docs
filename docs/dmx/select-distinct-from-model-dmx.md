---
title: "SELECT DISTINCT FROM &lt;model &gt; (DMX)"
description: "SELECT DISTINCT FROM &lt;model &gt; (DMX)"
author: minewiskan
ms.author: owend
ms.reviewer: owend
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: dmx
---
# SELECT DISTINCT FROM &lt;model &gt; (DMX)
[!INCLUDE[ssas](../includes/applies-to-version/ssas.md)]

  Returns all possible states for the selected column in the model. The values that are returned vary depending on whether the specified column contains discrete values, discretized numeric values, or continuous numeric values.  
  
## Syntax  
  
```  
SELECT [FLATTENED] DISTINCT [TOP <n>] <expression list> FROM <model>   
[WHERE <condition list>][ORDER BY <expression>]  
```  
  
## Arguments  
 *n*  
 Optional. An integer specifying how many rows to return.  
  
 *expression list*  
 A comma-separated list of related column identifiers (derived from the model) or expressions.  
  
 *model*  
 A model identifier.  
  
 *condition list*  
 A condition to restrict the values that are returned from the column list.  
  
 *expression*  
 Optional. An expression that returns a scalar value.  
  
## Remarks  
 The **SELECT DISTINCT FROM** statement only works with a single column or with a set of related columns. This clause does not work with a set of unrelated columns.  
  
 The **SELECT DISTINCT FROM** statement allows you to directly reference a column inside of a nested table. For example:  
  
```  
<model>.<table column reference>.<column reference>  
```  
  
 The results of the **SELECT DISTINCT FROM \<model>** statement vary, depending on the column type. The following table describes the supported column types and the output from the statement.  
  
|Column type|Output|  
|-----------------|------------|  
|Discrete|The unique values in the column.|  
|Discretized|The midpoint for each discretized bucket in the column.|  
|Continuous|The midpoint for the values in the column.|  
  
## Discrete Column Example  
 The following code sample is based on the `[TM Decision Tree]` model that you create in the [Basic Data Mining Tutorial](/previous-versions/sql/sql-server-2016/ms167167(v=sql.130)). The query returns the unique values that exist in the discrete column, `Gender`.  
  
```  
SELECT DISTINCT [Gender]  
FROM [TM Decision Tree]  
```  
  
 Example results:  
  
|Gender|  
|------------|  
|F|  
|M|  
  
 For columns that contain discrete values, the results always include the Missing state, shown as a null value.  
  
## Continuous Column Example  
 The following code sample returns the midpoint, minimum age, and maximum age for all of the values in the column.  
  
```  
SELECT DISTINCT [Age] AS [Midpoint Age],   
    RangeMin([Age]) AS [Minimum Age],   
    RangeMax([Age]) AS [Maximum Age]  
FROM [TM Decision Tree]  
```  
  
 Example results:  
  
|Midpoint Age|Minimum Age|Maximum Age|  
|------------------|-----------------|-----------------|  
|62|26|97|  
  
 The query also returns a single row of null values, to represent missing values.  
  
## Discretized Column Example  
 The following code sample returns the midpoint, maximum, and minimum values for each bucket that has been created by the algorithm for the column, `[Yearly Income]`. To reproduce the results for this example, you must create a new mining structure that is the same as `[Targeted Mailing]`. In the wizard, change the content type of the `Yearly Income` column from **Continuous** to **Discretized**.  
  
> [!NOTE]  
>  You can also change the mining model created in the Basic Mining Tutorial to discretize the mining structure column, `[Yearly Income]`. For information about how to do this, see [Change the Discretization of a Column in a Mining Model](/analysis-services/data-mining/change-the-discretization-of-a-column-in-a-mining-model). However, when you change the discretization of the column, it will force the mining structure to be reprocessed, which will change the results of other models that you have built using that structure.  
  
```  
SELECT DISTINCT [Yearly Income] AS [Bucket Average],   
    RangeMin([Yearly Income]) AS [Bucket Minimum],   
    RangeMax([Yearly Income]) AS [Bucket Maximum]  
FROM [TM Decision Tree]  
```  
  
 Example results:  
  
|Bucket Average|Bucket Minimum|Bucket Maximum|  
|--------------------|--------------------|--------------------|  
|24610.7|10000|39221.41|  
|55115.73|39221.41|71010.05|  
|84821.54|71010.05|98633.04|  
|111633.9|98633.04|124634.7|  
|147317.4|124634.7|170000|  
  
 You can see that the values of the `[Yearly Income]` column have been discretized into five buckets, plus an additional row of null values, to represent missing values.  
  
 The number of decimal places in the results depends on the client that you use for querying. Here they have been rounded to two decimal places, both for simplicity and to reflect the values that are displayed in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)].  
  
 For example, if you browse the model by using the Decision Tree viewer and click a node that contains customers grouped by income, the following node properties are displayed in the Tooltip:  
  
 Age >=69 AND Yearly Income < 39221.41  
  
> [!NOTE]  
>  The minimum value of the minimum bucket and the maximum value of the maximum bucket are just the highest and lowest observed values. Any values that fall outside this observed range are assumed to belong to the minimum and maximum buckets.  
  
## See Also  
 [SELECT &#40;DMX&#41;](../dmx/select-dmx.md)   
 [Data Mining Extensions &#40;DMX&#41; Data Manipulation Statements](../dmx/dmx-statements-data-manipulation.md)   
 [Data Mining Extensions &#40;DMX&#41; Statement Reference](../dmx/data-mining-extensions-dmx-statements.md)  
  
