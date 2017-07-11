---
title: "DISCOVER_COMMAND_OBJECTS Rowset | Microsoft Docs"
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
  - "DISCOVER_COMMAND_OBJECTS rowset"
ms.assetid: 325114ee-3a50-4504-9782-dbf7c1a44778
caps.latest.revision: 21
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# DISCOVER_COMMAND_OBJECTS Rowset
  Provides resource usage and activity information about the objects in use by the referenced command.  
  
 **Applies to:** tabular models, multidimensional models  
  
## Rowset Columns  
 The **DISCOVER_COMMAND_OBJECTS** rowset contains the following columns.  
  
|Column name|Type indicator|Restriction|Description|  
|-----------------|--------------------|-----------------|-----------------|  
|**SESSION_SPID**|**DBTYPE_I4**|Yes|The session ID.|  
|**SESSION_ID**|**DBTYPE_WSTR**|Yes|The session unique identifier, as a GUID.|  
|**SESSION_COMMAND_COUNT**|**DBTYPE_I4**||The command sequence number.|  
|**OBJECT_PARENT_PATH**|**DBTYPE_WSTR**|Yes|The path to the parent of the current object.|  
|**OBJECT_ID**|**DBTYPE_WSTR**|Yes|The ID of the object as defined when it was created.|  
|**OBJECT_VERSION**|**DBTYPE_I4**||The metadata version number of the object; this number changes every time the object is altered.|  
|**OBJECT_DATA_VERSION**|**DBTYPE_I4**||The lineage number of the data in the object. This number increments each time the object is processed.|  
|**OBJECT_CPU_TIME_MS**|**DBTYPE_I8**||The CPU time, in milliseconds, consumed by the object since the start of the command.|  
|**OBJECT_READ_KB**|**DBTYPE_I8**||The accumulated value of data, in kilobytes, read by the object since the start of the command.|  
|**OBJECT_READS**|**DBTYPE_I8**||The accumulated number of read operations by the object since the start of the command.|  
|**OBJECT_WRITE_KB**|**DBTYPE_I8**||The accumulated value of data, in kilobytes,written by the object since the start of the command.|  
|**OBJECT_WRITES**|**DBTYPE_I8**||The accumulated number of write operations by the object since the start of the command.|  
|**OBJECT_ROWS_RETURNED**|**DBTYPE_I8**||The number of rows returned by the object to the caller since the start of the command.|  
|**OBJECT_ROWS_SCANNED**|**DBTYPE_I8**||The number of rows scanned by the object since the start of the command.|  
  
 This schema rowset is not sorted.  
  
> [!IMPORTANT]  
>  The DISCOVER_COMMAND_OBJECTS schema rowset does not report activity through DMV queries.  
  
## Using ADOMD.NET to return the rowset  
 When using ADOMD.NET and the schema rowset to retrieve metadata, you can use either the GUID or string to reference a schema rowset object in the GetSchemaDataSet method. For more information, see [Working with Schema Rowsets in ADOMD.NET](../../../analysis-services/multidimensional-models-adomd-net-client/retrieving-metadata-working-with-schema-rowsets.md).  
  
 The following table provides the GUID and string values that identify this rowset.  
  
|Argument|Value|  
|--------------|-----------|  
|GUID|a07ccd35-8148-11d0-87bb-00c04fc33942|  
|ADOMDNAME|CommandObjects|  
  
## See Also  
 [XML for Analysis Schema Rowsets](../../../analysis-services/schema-rowsets/xml/xml-for-analysis-schema-rowsets.md)  
  
  