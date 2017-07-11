---
title: "DISCOVER_DIMENSION_STAT Rowset | Microsoft Docs"
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
ms.assetid: 639f8cd7-3b43-40d5-8b84-552daf60d484
caps.latest.revision: 7
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# DISCOVER_DIMENSION_STAT Rowset
  Provides information about a dimension, including the name of the database that contains it, the dimension name, its attributes, and a count of the members for each attribute. In a tabular model, this corresponds to the columns in a table, and the number of values in each column.  
  
 **Applies to:** tabular models, multidimensional models  
  
## Rowset Columns  
 The **DISCOVER_DIMENSION_STAT** rowset contains the following columns.  
  
|Column name|Type indicator|Restriction|Description|  
|-----------------|--------------------|-----------------|-----------------|  
|**DATABASE_NAME**|**DBTYPE_WSTR**|Required|The name of the database that contains the dimension.<br /><br /> This column is required in the restriction list.|  
|**DIMENSION_NAME**|**DBTYPE_WSTR**|Required|The name of the dimension.<br /><br /> This column is required in the restriction list.|  
|**ATTRIBUTE_NAME**|**DBTYPE_WSTR**||The name of an attribute in the dimension.|  
|**ATTRIBUTE_COUNT**|**DBTYPE_I8**||The count of values in the named attribute. For a tabular model, the value is always the same as the number of rows in the table.|  
  
 This schema rowset is not sorted.  
  
## Using ADOMD.NET to return the rowset  
 When using ADOMD.NET and the schema rowset to retrieve metadata, you can use either the GUID or string to reference a schema rowset object in the GetSchemaDataSet method. For more information, see [Working with Schema Rowsets in ADOMD.NET](../../../analysis-services/multidimensional-models-adomd-net-client/retrieving-metadata-working-with-schema-rowsets.md).  
  
 The following table provides the GUID and string values that identify this rowset.  
  
|Argument|Value|  
|--------------|-----------|  
|GUID|a07ccd90-8148-11d0-87bb-00c04fc33942|  
|ADOMDNAME|PartitionDimensionStat|  
  
## See Also  
 [XML for Analysis Schema Rowsets](../../../analysis-services/schema-rowsets/xml/xml-for-analysis-schema-rowsets.md)  
  
  