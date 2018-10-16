---
title: "Query Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Query Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "Query element"
ms.assetid: 832c3337-de6d-43b2-8f1c-75bdba76539b
author: minewiskan
ms.author: owend
manager: craigg
---
# Query Element (ASSL)
  Contains the text of the query to execute for the notification.  
  
## Syntax  
  
```xml  
  
<QueryNotification>  
   <Query>...</Query>  
</QueryNotification>  
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
|Parent element|[QueryNotification](../objects/querynotification-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The element that corresponds to the parent of `Query` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.QueryNotification>.  
  
## See Also  
 [ProactiveCachingQueryBinding Data Type &#40;ASSL&#41;](../data-type/binding-data-type-assl.md)   
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
