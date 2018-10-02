---
title: "ProcessingQuery Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "ProcessingQuery Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "ProcessingQuery element"
ms.assetid: d18e6f4b-c24c-4f73-8b85-4b6e8a82a695
author: minewiskan
ms.author: owend
manager: craigg
---
# ProcessingQuery Element (ASSL)
  Contains the parameterized text of the query to execute for notification of incremental processing status.  
  
## Syntax  
  
```xml  
  
<IncrementalProcessingNotification>  
   ...  
   <ProcessingQuery>...</ProcessingQuery>  
   ...  
</IncrementalProcessingNotification>  
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
|Parent element|[IncrementalProcessingNotification](../objects/incrementalprocessingnotification-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The table in the [DataSourceView](../objects/datasourceview-element-assl.md) that is referenced by the `ProcessingQuery` is identified by the sibling element, [TableID](id-element-assl.md).  
  
 The element that corresponds to the parent of `ProcessingQuery` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.IncrementalProcessingNotification>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
