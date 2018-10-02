---
title: "DISCOVER_TRACES Rowset | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
ms.assetid: 1be6a035-ffc9-489e-9d4d-7391b3e384e2
author: minewiskan
ms.author: owend
manager: craigg
---
# DISCOVER_TRACES Rowset
  Provides information about traces that are currently active on the server.  
  
 **Applies to:** tabular models, multidimensional models  
  
## Rowset Columns  
 The `DISCOVER_TRACES` rowset contains the following columns.  
  
|Column name|Type indicator|Description|  
|-----------------|--------------------|-----------------|  
|`TraceID`|`DBTYPE_WSTR`|The trace ID.|  
|`TraceName`|`DBTYPE_WSTR`|The trace name.|  
|`LogFileName`|`DBTYPE_WSTR`|The trace log file name.|  
|`LogFileSize`|`DBTYPE_I4`|The trace log file size.|  
|`LogFileRollover`|`DBTYPE_BOOL`|When true, indicates that the log file should be rolled over; otherwise false.|  
|`AutoRestart`|`DBTYPE_BOOL`|When true, indicates that the auto restart option is enabled; otherwise false.|  
|`CreationTime`|`DBTYPE_TIME`|The date and time that the trace was created.|  
|`StopTime`|`DBTYPE_TIME`|The stop time of a trace.|  
|`Type`|`PF_DBTYPE_WSTR`|The type of trace.|  
  
 This schema rowset is not sorted.  
  
## Restriction Columns  
 The `DISCOVER_TRACES` rowset can be restricted on the columns listed in the following table.  
  
|Column name|Type indicator|Restriction State|  
|-----------------|--------------------|-----------------------|  
|**TraceId**|`DBTYPE_I4`|Optional.|  
|`Type`|WSTR|Optional.|  
  
## Using ADOMD.NET to return the rowset  
 When using ADOMD.NET and the schema rowset to retrieve metadata, you can use either the GUID or string to reference a schema rowset object in the GetSchemaDataSet method. For more information, see [Working with Schema Rowsets in ADOMD.NET](../../../relational-databases/native-client-ole-db-rowsets/rowsets.md).  
  
 The following table provides the GUID and string values that identify this rowset.  
  
|Argument|Value|  
|--------------|-----------|  
|GUID|a07ccd1a-8148-11d0-87bb-00c04fc33942|  
|String|DISCOVER_TRACES|  
  
## See Also  
 [XML for Analysis Schema Rowsets](xml-for-analysis-schema-rowsets.md)  
  
  
