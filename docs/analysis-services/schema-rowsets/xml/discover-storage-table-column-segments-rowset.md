---
title: "DISCOVER_STORAGE_TABLE_COLUMN_SEGMENTS Rowset | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
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
ms.assetid: 3e514715-9fe6-4e6a-accb-4149ffd7e0bf
caps.latest.revision: 15
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# DISCOVER_STORAGE_TABLE_COLUMN_SEGMENTS Rowset
  Provides information at the column and segment level about storage tables used by an Analysis Services database running in tabular or [!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] mode. This rowset is primarily used for troubleshooting and analysis.  
  
 **Applies to:** tabular models  
  
## Rowset Columns  
 The **DISCOVER_STORAGE_TABLE_COLUMN_SEGMENTS** rowset contains the following columns.  
  
|**Column name**|**Type indicator**|**Restriction**|**Description**|  
|---------------------|------------------------|---------------------|---------------------|  
|**DATABASE_NAME**|**DBTYPE_WSTR**|Yes|Specifies the tabular database.<br /><br /> The **DISCOVER_STORAGE_TABLE_COLUMN_SEGMENTS** rowset can be restricted by using this column. If omitted the current database is used.|  
|**CUBE_NAME**|**DBTYPE_WSTR**|Yes|The name of the model.<br /><br /> The **DISCOVER_STORAGE_TABLES** rowset can be restricted by using this column.|  
|**MEASURE_GROUP_NAME**|**DBTYPE_WSTR**|Yes|The name of the measure group.|  
|**PARTITION_NAME**|**DBTYPE_WSTR**|Yes|The name of the partition.|  
|**DIMENSION_NAME**|**DBTYPE_WSTR**||The name of the dimension.|  
|**TABLE_ID**|**DBTYPE_WSTR**||The internal ID of the table segment.|  
|**COLUMN_ID**|**DBTYPE_WSTR**||The internal ID of the column.|  
|**SEGMENT _NUMBER**|**DBTYPE_I8**||The ordinal number of the table segment.|  
|**TABLE_PARTTION_NUMBER**|**DBTYPE_I8**||The ordinal number of the partition.|  
|**RECORDS_COUNT**|**DBTYPE_I8**||The number of records in the partition.|  
|**ALLOCATED_SIZE**|**DBTYPE_UI8**||Size in bytes allocated to the column segment.|  
|**USED_SIZE**|**DBTYPE_UI8**||Size in bytes used by the column segment.|  
|**COMPRESSION_TYPE**|**DBTYPE_WSTR**||Type of compression applied to the column segment. This value is intended for internal use and customer support use only. Microsoft does not publish valid values or descriptions for this column.|  
|**BITS_COUNT**|**DBTYPE_I8**||The count of bits.|  
|**BOOKMARK_BITS_COUNT**|**DBTYPE_I8**||The count of bookmark bits.|  
|**VERTIPAQ_STATE**|**DBTYPE_WSTR**||The state of the VertiPaq compression for this column segment. The value is one of the following:<br /><br /> SKIPPED – The VertiPaq compression was skipped.<br /><br /> COMPLETED – The VertiPaq compression completed successfully.<br /><br /> TIMEBOXED – The VertiPaq compression was timeboxed.|  
  
## Using ADOMD.NET to return the rowset  
 When using ADOMD.NET and the schema rowset to retrieve metadata, you can use either the GUID or string to reference a schema rowset object in the GetSchemaDataSet method. For more information, see [Working with Schema Rowsets in ADOMD.NET](../../../analysis-services/multidimensional-models-adomd-net-client/retrieving-metadata-working-with-schema-rowsets.md).  
  
 The following table provides the GUID and string values that identify this rowset.  
  
|Argument|Value|  
|--------------|-----------|  
|GUID|a07ccd45-8148-11d0-87bb-00c04fc33942|  
|ADOMDNAME|StorageSegments|  
  
## Example  
 The following query returns the storage table segments associated with the model attribute LastName, in the current database.  
  
```  
SELECT DISTINCT TABLE_ID, COLUMN_ID   
FROM $system.DISCOVER_STORAGE_TABLE_COLUMN_SEGMENTS  
WHERE COLUMN_ID = 'LastName'  
ORDER BY TABLE_ID  
  
```  
  
## See Also  
 [Analysis Services Schema Rowsets](../../../analysis-services/schema-rowsets/analysis-services-schema-rowsets.md)  
  
  