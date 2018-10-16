---
title: "DISCOVER_PERFORMANCE_COUNTERS Rowset | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
ms.assetid: 62b1e967-af67-4915-a305-727bffd61fe4
author: minewiskan
ms.author: owend
manager: craigg
---
# DISCOVER_PERFORMANCE_COUNTERS Rowset
  Returns the value of one or more performance counters. It does not support counters that return information about usage over time (such as disk reads per second and percentage of CPU usage).  
  
 **Applies to:** tabular models, multidimensional models  
  
## Rowset Columns  
 The `DISCOVER_PERFORMANCE_COUNTERS` rowset contains the following columns.  
  
|Column name|Type indicator|Restriction|Description|  
|-----------------|--------------------|-----------------|-----------------|  
|`PERF_COUNTER_NAME`|`DBTYPE_WSTR`|Required|The name of the performance counter.|  
|`PERF_COUNTER_VALUE`|`DBTYPE_DOUBLE`||The value of the performance counter.|  
  
 This schema rowset is not sorted.  
  
## Using ADOMD.NET to return the rowset  
 When using ADOMD.NET and the schema rowset to retrieve metadata, you can use either the GUID or string to reference a schema rowset object in the GetSchemaDataSet method. For more information, see [Working with Schema Rowsets in ADOMD.NET](../../../relational-databases/native-client-ole-db-rowsets/rowsets.md).  
  
 The following table provides the GUID and string values that identify this rowset.  
  
|Argument|Value|  
|--------------|-----------|  
|GUID|a07ccd2e-8148-11d0-87bb-00c04fc33942|  
|ADOMDNAME|PerformanceCounters|  
  
## See Also  
 [XML for Analysis Schema Rowsets](xml-for-analysis-schema-rowsets.md)  
  
  
