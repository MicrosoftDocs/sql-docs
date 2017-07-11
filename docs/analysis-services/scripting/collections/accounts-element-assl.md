---
title: "Accounts Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "Accounts Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "Accounts"
helpviewer_keywords: 
  - "Accounts element"
ms.assetid: 3ec62f58-c19b-4b15-b040-8941521a389b
caps.latest.revision: 44
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Accounts Element (ASSL)
  Contains the collection of account types that are defined in a [Database](../../../analysis-services/scripting/objects/database-element-assl.md) element.  
  
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
|Parent elements|[Database](../../../analysis-services/scripting/objects/database-element-assl.md)|  
|Child elements|[Account](../../../analysis-services/scripting/objects/account-element-assl.md)|  
  
## Remarks  
 Dimensions, whose [Type](../../../analysis-services/scripting/properties/type-element-dimension-assl.md) element is set to *Accounts*, can have an attribute that specifies the account type, such as Income, Expense, and so on, represented by members in the dimension. The account type is then used by [Measure](../../../analysis-services/scripting/objects/measure-element-assl.md) elements, whose [AggregationFunction](../../../analysis-services/scripting/properties/aggregatefunction-element-assl.md) element is set to *ByAccount*, to determine the aggregation function to use when aggregating the members of that dimension. The **Accounts** element holds a collection of **Account** elements, which represent account types and the aggregation function that should be used for each account type.  
  
 An account type must be listed if the aggregate function is different from the default used by [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] for each account type.  
  
 The set of valid account types is fixed.  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.AccountCollection>.  
  
## See Also  
 [AccountType Element &#40;ASSL&#41;](../../../analysis-services/scripting/properties/accounttype-element-assl.md)   
 [Collections &#40;ASSL&#41;](../../../analysis-services/scripting/collections/collections-assl.md)  
  
  