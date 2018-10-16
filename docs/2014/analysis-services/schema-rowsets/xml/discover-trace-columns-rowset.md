---
title: "DISCOVER_TRACE_COLUMNS Rowset | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
ms.assetid: 02baf401-52b0-4a73-8a7b-3b5b5e568626
author: minewiskan
ms.author: owend
manager: craigg
---
# DISCOVER_TRACE_COLUMNS Rowset
  Returns an XML document that describes the columns available in a trace.  
  
 **Applies to:** tabular models, multidimensional models  
  
## Rowset Columns  
 The `DISCOVER_TRACE_COLUMNS` rowset contains the following columns.  
  
|Column name|Type indicator|Restriction|Description|  
|-----------------|--------------------|-----------------|-----------------|  
|`Data`|`DBTYPE_WSTR`|Yes|Contains encoded XML string describing information about the trace columns provided by the trace provider.|  
  
 This schema rowset is not sorted.  
  
## Using ADOMD.NET to return the rowset  
 When using ADOMD.NET and the schema rowset to retrieve metadata, you can use either the GUID or string to reference a schema rowset object in the GetSchemaDataSet method. For more information, see [Working with Schema Rowsets in ADOMD.NET](../../../relational-databases/native-client-ole-db-rowsets/rowsets.md).  
  
 The following table provides the GUID and string values that identify this rowset.  
  
|Argument|Value|  
|--------------|-----------|  
|GUID|a07ccd18-8148-11d0-87bb-00c04fc33942|  
|ADOMDNAME|TraceColumns|  
  
## See Also  
 [XML for Analysis Schema Rowsets](xml-for-analysis-schema-rowsets.md)  
  
  
