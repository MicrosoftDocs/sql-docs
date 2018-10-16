---
title: "Alias Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Alias Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "Alias"
helpviewer_keywords: 
  - "Alias element"
ms.assetid: 674fdb06-e33c-4f35-bd6a-d9bbb13ececa
author: minewiskan
ms.author: owend
manager: craigg
---
# Alias Element (ASSL)
  Defines an alias for an [Account](../objects/account-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<Aliases>  
      <Alias>...</Alias>  
</Aliases>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality|0-n: Optional element that can occur more than once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Aliases](../collections/aliases-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of the `Alias` element is used as an alternate name for the account defined by the `Account` parent element, and helps to identify the combination of account type and aggregation function in a user interface.  
  
 The element that corresponds to the parent of the `Aliases` collection in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Account>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
