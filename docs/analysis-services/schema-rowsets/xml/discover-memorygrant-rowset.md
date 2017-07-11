---
title: "DISCOVER_MEMORYGRANT Rowset | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
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
ms.assetid: d254e42d-9918-47ce-b6df-47f1f0b432dd
caps.latest.revision: 6
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# DISCOVER_MEMORYGRANT Rowset
  Returns a list of internal memory quota grants that are taken by jobs that are currently running on the server. To find out whether a job is running on the server, use `Select * from $System.Discover_Jobs`.  
  
 **Applies to:** tabular models, multidimensional models  
  
## Rowset Columns  
 The **DISCOVER_MEMORYGRANT** rowset contains the following columns.  
  
|Column name|Type indicator|Restriction|Description|  
|-----------------|--------------------|-----------------|-----------------|  
|**MEMORY_ID**|**DBTYPE_I8**||A number that identifies the memory quota grant. Unique within the context of a single DISCOVER_MEMORYGRANT request.|  
|**SPID**|**DBTYPE_I4**|Required|The SPID, which you can obtain by running `Select * from $System.Discover_Sessions`.|  
|**CreationTime**|**DBTYPE_I8 DBTYPE_DBTIMESTAMP**||The time the quota was granted.|  
|**LastRequestTime**|**DBTYPE_DBTIMESTAMP**||The time the quota request was last modified.|  
|**MemoryUsed**|**DBTYPE_I4**||The amount of memory used in association with the quota.|  
|**MemoryGranted**|**DBTYPE_I4**||The amount of memory granted for use by the job that is obtaining the memory quota.|  
|**Blocked**|**DBTYPE_BOOL**||A Boolean that indicates the block status of the job. True indicates that the job is blocked waiting for another job to release sufficient quota to grant its quota request. False indicates that the job has received its quota and can execute.|  
  
 This schema rowset is not sorted.  
  
## Using ADOMD.NET to return the rowset  
 When using ADOMD.NET and the schema rowset to retrieve metadata, you can use either the GUID or string to reference a schema rowset object in the GetSchemaDataSet method. For more information, see [Working with Schema Rowsets in ADOMD.NET](../../../analysis-services/multidimensional-models-adomd-net-client/retrieving-metadata-working-with-schema-rowsets.md).  
  
 The following table provides the GUID and string values that identify this rowset.  
  
|Argument|Value|  
|--------------|-----------|  
|GUID|a07ccd23-8148-11d0-87bb-00c04fc33942|  
|ADOMDNAME|MemoryGrant|  
  
## See Also  
 [XML for Analysis Schema Rowsets](../../../analysis-services/schema-rowsets/xml/xml-for-analysis-schema-rowsets.md)  
  
  