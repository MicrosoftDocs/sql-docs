---
title: "TableNotification Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
api_name: 
  - "TableNotification Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "TableNotification element"
ms.assetid: 3afd075a-74f9-428c-b527-ee497cbe71e7
caps.latest.revision: 14
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# TableNotification Element (ASSL)
  Contains information for the [ProactiveCaching](../../../2014/analysis-services/dev-guide/proactivecaching-element-assl.md) element about a table or view in a data source that has been modified.  
  
## Syntax  
  
```xml  
  
<TableNotifications>  
   <TableNotification>  
      <DbTableName>...</DbTableName>  
      <DbSchemaName>...</DbSchemaName>  
...</TableNotification>  
</TableNotifications>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|1-n: Required element that can occur more than once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[TableNotifications](../../../2014/analysis-services/dev-guide/tablenotifications-element-assl.md)|  
|Child elements|[DbSchemaName](../../../2014/analysis-services/dev-guide/dbschemaname-element-assl.md), [DbTableName](../../../2014/analysis-services/dev-guide/dbtablename-element-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.TableNotification>.  
  
## See Also  
 [ProactiveCachingTablesBinding Data Type &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/proactivecachingtablesbinding-data-type-assl.md)   
 [ProactiveCachingObjectNotificationBinding Data Type &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/proactivecachingobjectnotificationbinding-data-type-assl.md)   
 [ProactiveCachingBinding Data Type &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/proactivecachingbinding-data-type-assl.md)   
 [Objects &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/objects-assl.md)  
  
  