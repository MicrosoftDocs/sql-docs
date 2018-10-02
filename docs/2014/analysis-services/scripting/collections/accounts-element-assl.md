---
title: "Accounts Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Accounts Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "Accounts"
helpviewer_keywords: 
  - "Accounts element"
ms.assetid: 3ec62f58-c19b-4b15-b040-8941521a389b
author: minewiskan
ms.author: owend
manager: craigg
---
# Accounts Element (ASSL)
  Contains the collection of account types that are defined in a [Database](../objects/database-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<Database>  
   ...  
   <Accounts>  
      <Account>...</Account>  
   </Accounts>  
   ...  
</Database>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None (collection)|  
|Default value|None (collection)|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Database](../objects/database-element-assl.md)|  
|Child elements|[Account](../objects/account-element-assl.md)|  
  
## Remarks  
 Dimensions, whose [Type](../properties/type-element-dimension-assl.md) element is set to *Accounts*, can have an attribute that specifies the account type, such as Income, Expense, and so on, represented by members in the dimension. The account type is then used by [Measure](../objects/measure-element-assl.md) elements, whose [AggregationFunction](../properties/aggregatefunction-element-assl.md) element is set to *ByAccount*, to determine the aggregation function to use when aggregating the members of that dimension. The `Accounts` element holds a collection of `Account` elements, which represent account types and the aggregation function that should be used for each account type.  
  
 An account type must be listed if the aggregate function is different from the default used by [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] for each account type.  
  
 The set of valid account types is fixed.  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.AccountCollection>.  
  
## See Also  
 [AccountType Element &#40;ASSL&#41;](../properties/accounttype-element-assl.md)   
 [Collections &#40;ASSL&#41;](collections-assl.md)  
  
  
