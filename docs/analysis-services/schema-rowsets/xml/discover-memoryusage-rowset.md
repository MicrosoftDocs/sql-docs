---
title: "DISCOVER_MEMORYUSAGE Rowset | Microsoft Docs"
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
ms.assetid: e416ea61-9615-468c-a96f-bbf731f803b1
caps.latest.revision: 7
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# DISCOVER_MEMORYUSAGE Rowset
  Returns the DISCOVER_MEMORYUSAGE statistics for various objects allocated by the server.  
  
> [!WARNING]  
>  This rowset can produce very large result sets. If the results cannot be displayed because they require more display memory than SQL Server Management Studio allows, the results are written to a temporary file, in the following default location:  
>   
>  '\<drive>:\Users\\<username\>\AppData\Local\Temp\\<fileID\>.xml'.  
  
 **Applies to:** tabular models, multidimensional models  
  
## Rowset Columns  
 The **DISCOVER_MEMORYUSAGE** rowset contains the following columns.  
  
|Column name|Type indicator|Restriction|Description|  
|-----------------|--------------------|-----------------|-----------------|  
|**MemoryID**|**DBTYPE_UI8**||A number identifying the memory.|  
|**MemoryName**|**DBTYPE_WSTR**||The name of the object owning the memory.|  
|**SPID**|**DBTYPE_UI4**|Yes|The session that allocated the memory. Zero indicates memory not tied to a specific session.|  
|**CreationTime**|**DBTYPE_DBTIMESTAMP**||Either "the time the object was created" or "the time the memory was allocated."|  
|**BaseObjectType**|**DBTYPE_UI4**|Yes|This is a number describing the type of the object. Objects with the same BaseObjectType have the same type.|  
|**MemoryUsed**|**DBTYPE_UI8**|Yes|This is the current size of the object, which may be less than the memory allocated for use by the object.|  
|**MemoryAllocated**|**DBTYPE_UI8**||The amount of memory allocated for use by the object, which may be greater than the amount of memory actually used by the object.|  
|**MemoryAllocBase**|**DBTYPE_UI8**||The bytes initially allocated for the object itself (excluding additional allocations for object contents).|  
|**MemoryAllocFromAlloc**|**DBTYPE_UI8**||The memory allocated for the contents of this object.|  
|**ElementCount**|**DBTYPE_UI4**||For a container object, this is the number of objects contained by that object.|  
|**Shrinkable**|**DBTYPE_BOOL**|Yes|A Boolean that indicates if the memory is shrinkable (can be evicted due to memory pressure). If true, the memory is shrinkable; if false, the memory is not shrinkable.|  
|**ObjectParentPath**|**DBTYPE_WSTR**||A string identifying the full path of this object.|  
|**ObjectID**|**DBTYPE_WSTR**||A string identifying the object. The full path of this object is represented by the string: (ObjectParentPath + '.' + ObjectId).|  
  
 This schema rowset is not sorted.  
  
## Using ADOMD.NET to return the rowset  
 When using ADOMD.NET and the schema rowset to retrieve metadata, you can use either the GUID or string to reference a schema rowset object in the GetSchemaDataSet method. For more information, see [Working with Schema Rowsets in ADOMD.NET](../../../analysis-services/multidimensional-models-adomd-net-client/retrieving-metadata-working-with-schema-rowsets.md).  
  
 The following table provides the GUID and string values that identify this rowset.  
  
|Argument|Value|  
|--------------|-----------|  
|GUID|A07CCD21-8148-11D0-87BB-00C04FC33942|  
|ADOMDNAME|MemoryUsage|  
  
## See Also  
 [XML for Analysis Schema Rowsets](../../../analysis-services/schema-rowsets/xml/xml-for-analysis-schema-rowsets.md)  
  
  