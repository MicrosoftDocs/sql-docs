---
title: "DISCOVER_TRACE_EVENT_CATEGORIES Rowset | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
ms.assetid: 1ad74fd2-4740-469d-85b5-abf0171737fd
author: minewiskan
ms.author: owend
manager: craigg
---
# DISCOVER_TRACE_EVENT_CATEGORIES Rowset
  Shows the list of event categories that are supported by the trace provider.  
  
 **Applies to:** tabular models, multidimensional models  
  
## Rowset Columns  
 The `DISCOVER_TRACE_EVENT_CATEGORIES` rowset contains the following columns.  
  
|Column name|Type indicator|Length|Description|  
|-----------------|--------------------|------------|-----------------|  
|`Data`|`DBTYPE_WSTR`||Contains an encoded XML string describing event category information about the trace provider, including the category name, type, and description. Type is a string indicating the type of event category. The enumeration values are as follows:<br /><br /> -   0=Normal<br />-   1=Significant<br />-   2=Error|  
  
 This schema rowset is not sorted.  
  
## Using ADOMD.NET to return the rowset  
 When using ADOMD.NET and the schema rowset to retrieve metadata, you can use either the GUID or string to reference a schema rowset object in the GetSchemaDataSet method. For more information, see [Working with Schema Rowsets in ADOMD.NET](../../../relational-databases/native-client-ole-db-rowsets/rowsets.md).  
  
 The following table provides the GUID and string values that identify this rowset.  
  
|Argument|Value|  
|--------------|-----------|  
|GUID|a07ccd19-8148-11d0-87bb-00c04fc33942|  
|String|DISCOVER_TRACE_EVENT_CATEGORIES|  
  
## See Also  
 [XML for Analysis Schema Rowsets](xml-for-analysis-schema-rowsets.md)  
  
  
