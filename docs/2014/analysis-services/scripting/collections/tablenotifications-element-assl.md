---
title: "TableNotifications Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "TableNotifications Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "TableNotifications element"
ms.assetid: 4cecdfea-0d4d-4bd6-bbb3-4d0d2284c665
author: minewiskan
ms.author: owend
manager: craigg
---
# TableNotifications Element (ASSL)
  Contains the collection of [TableNotification](../objects/tablenotification-element-assl.md) elements that provide information for the [ProactiveCaching](../objects/proactivecaching-element-assl.md) element about tables or views in a data source that have been modified.  
  
## Syntax  
  
```xml  
  
<ProactiveCachingTablesBinding>  
   <TableNotifications>  
      <TableNotification>...</TableNotification>  
...</TableNotifications>  
</ProactiveCachingTablesBinding>  
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
|Parent elements|[ProactiveCachingTablesBinding](../data-type/binding-data-type-assl.md)|  
|Child elements|[TableNotification](../objects/tablenotification-element-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.TableNotificationCollection>.  
  
## See Also  
 [ProactiveCachingBinding Data Type &#40;ASSL&#41;](../data-type/proactivecachingbinding-data-type-assl.md)   
 [ProactiveCachingObjectNotificationBinding Data Type &#40;ASSL&#41;](../data-type/proactivecachingobjectnotificationbinding-data-type-assl.md)   
 [Collections &#40;ASSL&#41;](collections-assl.md)  
  
  
