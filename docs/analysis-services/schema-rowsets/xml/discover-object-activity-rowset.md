---
title: "DISCOVER_OBJECT_ACTIVITY Rowset | Microsoft Docs"
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
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "DISCOVER_OBJECT_ACTIVITY rowset"
ms.assetid: 100f7de1-ad5c-4973-b863-3c10df1245c4
caps.latest.revision: 14
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# DISCOVER_OBJECT_ACTIVITY Rowset
  Provides resource usage per object since the start of the service.  
  
## Rowset Columns  
 The **DISCOVER_OBJECT_ACTIVITY** rowset contains the following columns.  
  
|Column name|Type indicator|Length|Description|  
|-----------------|--------------------|------------|-----------------|  
|**OBJECT_AGGREGATION_HIT**|**DBTYPE_I8**||The number of times an aggregation of the object has been hit since the start of the service.|  
|**OBJECT_AGGREGATION_MISS**|**DBTYPE_I8**||The number of times an exisiting aggregation, of the object, has not been missed (that is, has not been used) since the start of the service.|  
|**OBJECT_CPU_TIME_MS**|**DBTYPE_I8**||The CPU time, in milliseconds,  consumed by the object since the beginning of the service.|  
|**OBJECT_DATA_VERSION**|**DBTYPE_I4**||The lineage number of the data in the object; this number increments each time the object is processed.|  
|**OBJECT_HIT**|**DBTYPE_I8**||The number of times the object has been hit in the cache since the start of the service.|  
|**OBJECT_ID**|**DBTYPE_WSTR**||The ID of the object as defined at creation time|  
|**OBJECT_MISS**|**DBTYPE_I8**||The number of times the object has been missed in the cache since the start of the service.|  
|**OBJECT_PARENT_PATH**|**DBTYPE_WSTR**||The path to the parent of current object.|  
|**OBJECT_READ_KB**|**DBTYPE_I8**||The accumulated value of data read by the object since the start of the service, in kilobytes.|  
|**OBJECT_READS**|**DBTYPE_I8**||The accumulated number of read operations by the object since the start of the service.|  
|**OBJECT_ROWS_RETURNED**|**DBTYPE_I8**||The number of rows returned by the object to the caller since the start of the service.|  
|**OBJECT_ROWS_SCANNED**|**DBTYPE_I8**||The number of rows scanned by the object since the start of the service.|  
|**OBJECT_VERSION**|**DBTYPE_I4**||The metadata version number of the object; this number changes every time the object is altered.|  
|**OBJECT_WRITE_KB**|**DBTYPE_I8**||The accumulated value of data written by the object since the start of the service, in kilobytes.|  
|**OBJECT_WRITES**|**DBTYPE_I8**||The accumulated number of write operations by the object since the start of the service.|  
  
 This schema rowset is not sorted.  
  
## Restriction Columns  
 The **DISCOVER_OBJECT_ACTIVTY** rowset can be restricted on the columns listed in the following table.  
  
|Column name|Type indicator|Restriction State|  
|-----------------|--------------------|-----------------------|  
|OBJECT_PARENT_PATH|DBTYPE_WSTR|Optional.|  
|OBJECT_ID|DBTYPE_WSTR|Optional.|  
  
## See Also  
 [XML for Analysis Schema Rowsets](../../../analysis-services/schema-rowsets/xml/xml-for-analysis-schema-rowsets.md)  
  
  