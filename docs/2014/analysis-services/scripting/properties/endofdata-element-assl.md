---
title: "EndOfData Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "EndOfData Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "EndOfData"
helpviewer_keywords: 
  - "EndOfData element"
ms.assetid: 4cee48bc-d486-4125-9d65-f323c6ec9d09
author: minewiskan
ms.author: owend
manager: craigg
---
# EndOfData Element (ASSL)
  Indicates the end of data received from a [PushedDataSource](../data-type/datasource-data-type-assl.md) element.  
  
## Syntax  
  
```xml  
  
<PushedDataSource>  
   ...  
   <EndOfData>...</EndOfData>  
   ...  
</PushedDataSource  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|Boolean|  
|Default value|None|  
|Cardinality|1-1: Required element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[PushedDataSource](../data-type/datasource-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The last data packet from the `PushedDataSource` must set the `EndOfData` element to `True`.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
