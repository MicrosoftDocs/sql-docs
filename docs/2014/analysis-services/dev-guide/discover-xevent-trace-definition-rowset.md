---
title: "DISCOVER_XEVENT_TRACE_DEFINITION Rowset | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: "analysis-services"
ms.topic: "reference"
ms.assetid: e1ce2d2d-f994-4318-801a-ee0385aecd84
author: minewiskan
ms.author: owend
manager: craigg
---
# DISCOVER_XEVENT_TRACE_DEFINITION Rowset
  Provides information about XEvent traces that are currently active on the server.  
  
 **Applies to:** tabular models, multidimensional models  
  
## Rowset Columns  
 The `DISCOVER_XEVENT_TRACE_DEFINITION` rowset contains the following columns.  
  
|Column name|Type indicator|Length|Description|  
|-----------------|--------------------|------------|-----------------|  
|`Data`|`DBTYPE_WSTR`||The XML definition of the XEvent trace.|  
  
 This schema rowset is not sorted.  
  
## Using ADOMD.NET to return the rowset  
 When using ADOMD.NET and the schema rowset to retrieve metadata, you can use either the GUID or string to reference a schema rowset object in the GetSchemaDataSet method. For more information, see [Working with Schema Rowsets in ADOMD.NET](https://docs.microsoft.com/bi-reference/adomd/multidimensional-models-adomd-net-client/retrieving-metadata-working-with-schema-rowsets).  
  
 The following table provides the GUID and string values that identify this rowset.  
  
|Argument|Value|  
|--------------|-----------|  
|GUID|a07ccd1c-8148-11d0-87bb-00c04fc33942|  
|String|DISCOVER_XEVENT_TRACE_DEFINITION|  
  
## See Also  
 [XML for Analysis Schema Rowsets](https://docs.microsoft.com/bi-reference/schema-rowsets/xml/xml-for-analysis-schema-rowsets)   
 [Use SQL Server Extended Events &#40;XEvents&#41; to Monitor Analysis Services](../instances/monitor-analysis-services-with-sql-server-extended-events.md)   
 [Use Dynamic Management Views &#40;DMVs&#41; to Monitor Analysis Services](../instances/use-dynamic-management-views-dmvs-to-monitor-analysis-services.md)  
  
  
