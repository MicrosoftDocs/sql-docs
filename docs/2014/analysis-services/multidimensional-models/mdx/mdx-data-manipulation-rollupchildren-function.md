---
title: "Working with the RollupChildren Function (MDX) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "queries [MDX], RollupChildren function"
  - "RollupChildren function"
  - "custom member properties [MDX]"
  - "IIf function"
ms.assetid: 03c624d4-f277-451d-9995-623a07ea2f86
author: minewiskan
ms.author: owend
manager: craigg
---
# Working with the RollupChildren Function (MDX)
  The Multidimensional Expressions (MDX) [RollupChildren](/sql/mdx/rollupchildren-mdx)
[Script for Search and Replace] function rolls up the children of a member, applying a different unary operator to each child, and returns the value of this rollup as a number. The unary operator can be supplied by a member property associated with the child member, or the operator can be a string expression provided directly to the function.  
  
## RollupChildren Function Examples  
 The use of the `RollupChildren` function in Multidimensional Expressions (MDX) statements is simple to explain, but the effect of this function on MDX queries can be wide-ranging.  
  
 The effect of the `RollupChildren` function occurs in MDX queries designed to perform selective analysis on existing cube data. For example, the following table contains a list of child members for the Net Sales parent member, with their unary operators (represented by the `UNARY_OPERATOR` member property) shown in parentheses.  
  
|Parent member|Child member|  
|-------------------|------------------|  
|Net Sales|Domestic Sales (+)<br /><br /> Domestic Returns (-)<br /><br /> Foreign Sales (+)<br /><br /> Foreign Returns (-)|  
  
 The Net Sales parent member currently provides a total of net sales minus the gross domestic and foreign sales values, with the domestic and foreign returns subtracted as part of the rollup.  
  
 However, you want to provide a quick and easy forecast of domestic and foreign gross sales plus 10%, ignoring the domestic and foreign returns. To calculate this value, you can use the `RollupChildren` function in one of two ways: with a custom member property or with the `IIf` function.  
  
### Using a Custom Member Property  
 If the rollup calculation is to be a frequently performed operation, one method is to create a member property that stores the operator that will be used for each child for a specific function. The following table displays valid unary operators and describes the expected result.  
  
|Operator|Result|  
|--------------|------------|  
|+|total = total + current child|  
|-|total = total - current child|  
|*|total = total * current child|  
|/|total = total / current child|  
|~|Child is not used in the rollup. The child's value is ignored.|  
  
 For example, a member property called `SALES_OPERATOR` could be created, and the following unary operators would be assigned to that member property, as shown in the following table.  
  
|Parent member|Child member|  
|-------------------|------------------|  
|Net Sales|Domestic Sales (+)<br /><br /> Domestic Returns (~)<br /><br /> Foreign Sales (+)<br /><br /> Foreign Returns (~)|  
  
 With this new member property, the following MDX statement would perform the gross sales estimate operation quickly and efficiently (ignoring Foreign and Domestic returns):  
  
```  
RollupChildren([Net Sales], [Net Sales].CurrentMember.Properties("SALES_OPERATOR")) * 1.1  
```  
  
 When the function is called, the value of each child is applied to a total using the operator stored in the member property. The members for domestic and foreign returns are ignored, and the rollup total returned by the `RollupChildren` function is multiplied by 1.1.  
  
### Using the IIf Function  
 If the example operation is not commonplace or if the operation applies only to one MDX query, the [IIf](/sql/mdx/iif-mdx) function can be used with the `RollupChildren` function to provide the same result. The following MDX query provides the same result as the earlier MDX example, but does so without resorting to the use of a custom member property:  
  
```  
RollupChildren([Net Sales], IIf([Net Sales].CurrentMember.Properties("UNARY_OPERATOR") = "-", "~", [Net Sales].CurrentMember.Properties("UNARY_OPERATOR))) * 1.1  
```  
  
 The MDX statement examines the unary operator of the child member. If the unary operator is used for subtraction (as in the case with the domestic and foreign returns members), the `IIf` function substitutes the tilde (~) unary operator. Otherwise, the `IIf` function uses the unary operator of the child member. Finally, the returned rollup total is then multiplied by 1.1 to provide the domestic and foreign gross sales forecast value.  
  
## See Also  
 [Manipulating Data &#40;MDX&#41;](mdx-data-manipulation-manipulating-data.md)  
  
  
