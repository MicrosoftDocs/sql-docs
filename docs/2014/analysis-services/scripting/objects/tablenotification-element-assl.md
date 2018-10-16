---
title: "TableNotification Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
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
author: minewiskan
ms.author: owend
manager: craigg
---
# TableNotification Element (ASSL)
  Contains information for the [ProactiveCaching](proactivecaching-element-assl.md) element about a table or view in a data source that has been modified.  
  
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
|Parent elements|[TableNotifications](../collections/tablenotifications-element-assl.md)|  
|Child elements|[DbSchemaName](../properties/name-element-assl.md), [DbTableName](../properties/dbtablename-element-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.TableNotification>.  
  
## See Also  
 [ProactiveCachingTablesBinding Data Type &#40;ASSL&#41;](../data-type/binding-data-type-assl.md)   
 [ProactiveCachingObjectNotificationBinding Data Type &#40;ASSL&#41;](../data-type/proactivecachingobjectnotificationbinding-data-type-assl.md)   
 [ProactiveCachingBinding Data Type &#40;ASSL&#41;](../data-type/proactivecachingbinding-data-type-assl.md)   
 [Objects &#40;ASSL&#41;](objects-assl.md)  
  
  
