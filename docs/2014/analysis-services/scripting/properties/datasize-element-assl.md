---
title: "DataSize Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "04/27/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "DataSize Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "DataSize"
helpviewer_keywords: 
  - "DataSize element"
ms.assetid: 4be79dbb-304e-4a65-9198-89fad407f775
author: minewiskan
ms.author: owend
manager: craigg
---
# DataSize Element (ASSL)
  Contains the size in bytes of a [DataItem](../data-type/dataitem-data-type-assl.md) element.  
  
## Syntax  
  
```xml  
  
<DataItem>  
   ...  
   <DataSize>...</DataSize>  
   ...  
</DataItem  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|Integer|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[DataItem](../data-type/dataitem-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The element that corresponds to the parent of `DataSize` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.DataItem>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
