---
title: "Account Element (ASSL) | Microsoft Docs"
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
  - "Account Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "Account"
helpviewer_keywords: 
  - "Account element"
ms.assetid: 0bb7d06c-0158-4ab2-b2b1-cb50ba24f7c0
caps.latest.revision: 41
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Account Element (ASSL)
  Contains details about an account type within a [Database](../../../analysis-services/scripting/objects/database-element-assl.md) element.  
  
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
|Parent elements|[Accounts](../../../analysis-services/scripting/collections/accounts-element-assl.md)|  
|Child elements|[AccountType](../../../analysis-services/scripting/properties/accounttype-element-assl.md), [AggregationFunction](../../../analysis-services/scripting/properties/aggregationfunction-element-assl.md), [Aliases](../../../analysis-services/scripting/collections/aliases-element-assl.md), [Annotations](../../../analysis-services/scripting/collections/annotations-element-assl.md)|  
  
## Remarks  
 Dimensions, whose [Type](../../../analysis-services/scripting/properties/type-element-dimension-assl.md) element is set to *Accounts,* can have an attribute that specifies the account type, such as Income, Expense, and so on, represented by members in the dimension. The account type is then used by [Measure](../../../analysis-services/scripting/objects/measure-element-assl.md) elements, whose [AggregationFunction](../../../analysis-services/scripting/properties/aggregatefunction-element-assl.md) element is set to *ByAccount*, to determine the aggregation function to use when aggregating the members of that dimension. The **Account** element represents a single account type and the aggregation function that should be used by such measures.  
  
 An account type must be listed if the aggregate function is different from the default used by [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] for each account type.  
  
 The set of valid account types is fixed.  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Account>.  
  
## See Also  
 [Database Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/database-element-assl.md)   
 [Objects &#40;ASSL&#41;](../../../analysis-services/scripting/objects/objects-assl.md)  
  
  