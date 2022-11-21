---
title: "SELECT FROM &lt;structure&gt;.CASES"
description: "SELECT FROM &lt;structure&gt;.CASES"
author: minewiskan
ms.author: owend
ms.reviewer: owend
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: dmx
---
# SELECT FROM &lt;structure&gt;.CASES
[!INCLUDE[ssas](../includes/applies-to-version/ssas.md)]

  Returns the cases that were used to create the mining structure.  
  
 If drillthrough is not enabled on the structure, the statement will fail. Also, the statement will fail if the user does not have drillthrough permissions on the mining structure.  
  
 In [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)], drillthrough on new mining structures is enabled by default. To verify whether drillthrough is enabled for a particular structure, check whether the value of the **CacheMode** property is set to **KeepTrainingCases**.  
  
 If the value of **CacheMode** is changed to **ClearAfterProcessing**, the structure cases are cleared from the cache and you cannot use drillthrough.  
  
> [!NOTE]  
>  You cannot enable or disable drillthrough on the mining structure by using Data Mining Extensions (DMX).  
  
## Syntax  
  
```  
  
SELECT [TOP n] <expression list> FROM <structure>.CASES  
[WHERE <condition expression>][ORDER BY <expression> [DESC|ASC]]  
```  
  
## Arguments  
 *n*  
 Optional. An integer that specifies how many rows to return.  
  
 *expression list*  
 A comma-separated list of expressions.  
  
 An expression can include column identifiers, user-defined functions, and VBA functions.  
  
 *structure*  
 The name of the structure.  
  
 *condition expression*  
 A condition to restrict the values that are returned from the column list.  
  
 *expression*  
 Optional. An expression that returns a scalar value.  
  
## Remarks  
 If drillthrough is enabled on both the model and the structure, any member of a role that has drillthrough permissions on the mining structure and the model can return structure columns that were not included in the model, by using the following syntax:  
  
```  
SELECT StructureColumn('<column name>') FROM <model>.CASES  
```  
  
 Therefore, to protect sensitive data or personal information, you should construct your data source view to mask personal information, and grant **AllowDrillthrough** permission on a mining structure or mining model only when necessary.  
  
## Examples  
 The following examples are based on the mining structure, Targeted Mailing, which is based on the [!INCLUDE[ssAWDWsp](../includes/ssawdwsp-md.md)] database, and the associated mining models. For more information, see [Basic Data Mining Tutorial](/previous-versions/sql/sql-server-2016/ms167167(v=sql.130)).  
  
### Example 1: Drill through to Structure Cases  
 The following example returns a list of the 500 oldest customers in the mining structure, Targeted Mailing. The query returns all the columns in the mining model, but restricts the rows to those who purchased a bike, and orders them by age. You can also edit the expression list to return only the columns that you need.  
  
```  
SELECT TOP 500 *  
FROM [Targeted Mailing].Cases  
WHERE [Bike Buyer] = 1  
ORDER BY Age DESC;  
```  
  
### Example 2: Drillthrough to Test or Training Cases Only  
 The following example returns a list of the structure cases for Targeted Mailing that are reserved for testing. If the mining structure does not contain a holdout test set, by default all cases are treated as training cases, and this query would return 0 cases.  
  
```  
SELECT [Customer Key], Gender, Age  
FROM [Targeted Mailing].Cases  
WHERE IsTestCase();  
```  
  
 To return the training cases, substitute the function `IsTrainingCase()`.  
  
## See Also  
 [SELECT &#40;DMX&#41;](../dmx/select-dmx.md)   
 [Data Mining Extensions &#40;DMX&#41; Data Definition Statements](../dmx/dmx-statements-data-definition.md)   
 [Data Mining Extensions &#40;DMX&#41; Data Manipulation Statements](../dmx/dmx-statements-data-manipulation.md)   
 [Data Mining Extensions &#40;DMX&#41; Statement Reference](../dmx/data-mining-extensions-dmx-statements.md)  
  
