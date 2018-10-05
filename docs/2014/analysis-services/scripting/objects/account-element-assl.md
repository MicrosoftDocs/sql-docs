---
title: "Account Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Account Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "Account"
helpviewer_keywords: 
  - "Account element"
ms.assetid: 0bb7d06c-0158-4ab2-b2b1-cb50ba24f7c0
author: minewiskan
ms.author: owend
manager: craigg
---
# Account Element (ASSL)
  Contains details about an account type within a [Database](database-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<Accounts>  
   <Account>  
      <AccountType>...</AccountType>  
      <AggregationFunction>...</AggregationFunction>  
            <Aliases>...</Aliases>  
            <Annotations>...</Annotations>  
   </Account>  
</Accounts>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-n: Optional element that can occur more than once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Accounts](../collections/accounts-element-assl.md)|  
|Child elements|[AccountType](../properties/accounttype-element-assl.md), [AggregationFunction](../properties/aggregationfunction-element-assl.md), [Aliases](../collections/aliases-element-assl.md), [Annotations](../collections/annotations-element-assl.md)|  
  
## Remarks  
 Dimensions, whose [Type](../properties/type-element-dimension-assl.md) element is set to *Accounts,* can have an attribute that specifies the account type, such as Income, Expense, and so on, represented by members in the dimension. The account type is then used by [Measure](measure-element-assl.md) elements, whose [AggregationFunction](../properties/aggregatefunction-element-assl.md) element is set to *ByAccount*, to determine the aggregation function to use when aggregating the members of that dimension. The `Account` element represents a single account type and the aggregation function that should be used by such measures.  
  
 An account type must be listed if the aggregate function is different from the default used by [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] for each account type.  
  
 The set of valid account types is fixed.  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Account>.  
  
## See Also  
 [Database Element &#40;ASSL&#41;](database-element-assl.md)   
 [Objects &#40;ASSL&#41;](objects-assl.md)  
  
  
