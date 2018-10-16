---
title: "QueryDefinition Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "QueryDefinition Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "QueryDefinition"
helpviewer_keywords: 
  - "QueryDefinition element"
ms.assetid: 25bf0e93-d5c5-41df-b310-a253a4ace80e
author: minewiskan
ms.author: owend
manager: craigg
---
# QueryDefinition Element (ASSL)
  Contains an opaque expression for a query associated with a [DataSource](../objects/datasource-element-assl.md) element in a [QueryBinding](../data-type/binding-data-type-assl.md) element.  
  
## Syntax  
  
```xml  
  
<QueryBinding>  
   ...  
   <QueryDefinition>...</QueryDefinition>  
   ...  
</QueryBinding>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality|1-1: Required element that occurs once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[QueryBinding](../data-type/binding-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The element that corresponds to the parent of `QueryDefinition` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.QueryBinding>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
