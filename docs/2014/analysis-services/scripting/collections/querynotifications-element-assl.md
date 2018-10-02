---
title: "QueryNotifications Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "QueryNotifications Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "QueryNotifications element"
ms.assetid: 0e7e951f-c8b9-4492-bb01-e4b5d16edde6
author: minewiskan
ms.author: owend
manager: craigg
---
# QueryNotifications Element (ASSL)
  Contains the collection of [QueryNotification](../objects/querynotification-element-assl.md) elements that provide information to the [ProactiveCaching](../objects/proactivecaching-element-assl.md) element about queries to execute to determine whether a data source has been modified.  
  
## Syntax  
  
```xml  
  
<ProactiveCachingQueryBinding>  
   ...  
   <QueryNotifications>  
      <QueryNotification>...</QueryNotification>  
...</QueryNotifications>  
   ...  
</ProactiveCachingQueryBinding>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|1-1: Required element that occurs once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[ProactiveCachingQueryBinding](../data-type/binding-data-type-assl.md)|  
|Child elements|[QueryNotification](../objects/querynotification-element-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.QueryNotificationCollection>.  
  
## See Also  
 [Collections &#40;ASSL&#41;](collections-assl.md)  
  
  
