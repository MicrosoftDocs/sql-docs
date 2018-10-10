---
title: "Type Element (MeasureGroup) (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Type Element (MeasureGroup)"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "TYPE"
helpviewer_keywords: 
  - "Type element"
ms.assetid: 3a584baf-36bb-4e1d-9128-c4758c0b8f06
author: minewiskan
ms.author: owend
manager: craigg
---
# Type Element (MeasureGroup) (ASSL)
  Specifies the type of the [MeasureGroup](../objects/group-element-assl.md).  
  
## Syntax  
  
```xml  
  
<MeasureGroup>  
   ...  
   <Type>...</Type>  
   ...  
</MeasureGroup>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|*Regular*|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[MeasureGroup](../objects/group-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of this element is limited to one of the strings listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*Regular*|Contains regular measures.|  
|*ExchangeRate*|Contains foreign exchange rate measures.|  
|*Sales*|Contains sales measures.|  
|*Budget*|Contains budget measures.|  
|*FinancialReporting*|Contains financial reporting measures.|  
|*Marketing*|Contains marketing measures.|  
|*Inventory*|Contains inventory measures.|  
  
 The enumeration that corresponds to the allowed values for `Type` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.MeasureGroupType>.  
  
 The element that corresponds to the parent of `Type` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.MeasureGroup>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
