---
title: "QueryNotifications Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "QueryNotifications Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "QueryNotifications element"
ms.assetid: 0e7e951f-c8b9-4492-bb01-e4b5d16edde6
caps.latest.revision: 15
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# QueryNotifications Element (ASSL)
  Contains the collection of [QueryNotification](../../../analysis-services/scripting/objects/querynotification-element-assl.md) elements that provide information to the [ProactiveCaching](../../../analysis-services/scripting/objects/proactivecaching-element-assl.md) element about queries to execute to determine whether a data source has been modified.  
  
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
|Parent elements|[ProactiveCachingQueryBinding](../../../analysis-services/scripting/data-type/proactivecachingquerybinding-data-type-assl.md)|  
|Child elements|[QueryNotification](../../../analysis-services/scripting/objects/querynotification-element-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.QueryNotificationCollection>.  
  
## See Also  
 [Collections &#40;ASSL&#41;](../../../analysis-services/scripting/collections/collections-assl.md)  
  
  