---
title: "DISCOVER_OBJECT_MEMORY_USAGE Rowset | Microsoft Docs"
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
  - "DISCOVER_OBJECT_MEMORY_USAGE rowset"
ms.assetid: 211cfa04-7bd6-43fe-8bd5-bfbff78bdafb
caps.latest.revision: 13
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# DISCOVER_OBJECT_MEMORY_USAGE Rowset
  Provides information about memory resources used by objects.  
  
## Rowset Columns  
 The **DISCOVER_OBJECT_MEMORY_USAGE** rowset contains the following columns.  
  
|Column name|Type indicator|Length|Description|  
|-----------------|--------------------|------------|-----------------|  
|**OBJECT_PARENT_PATH**|**DBTYPE_WSTR**||The path to the parent of the current object.|  
|**OBJECT_ID**|**DBTYPE_WSTR**||The ID of the object as defined at creation time.|  
|**OBJECT_MEMORY_SHRINKABLE**|**DBTYPE_I8**||The total amount of memory (bytes) used by all the shrinkable objects that are directly owned by the current object. The current value does not include memory from objects owned by named objects that are owned by the current object.|  
|**OBJECT_MEMORY_NONSHRINKABLE**|**DBTYPE_I8**||The amount of memory (bytes) of all non-shrinkable objects directly owned by current object. The current value does not include memory from objects owned by named objects that are owned by current object.|  
|**OBJECT_VERSION**|**DBTYPE_I4**||The metadata version number of the object. This number changes each time the object is altered.|  
|**OBJECT_DATA_VERSION**|**DBTYPE_I4**||The lineage number of the data in the object. This number increments each time the object is processed.|  
|**OBJECT_TYPE_ID**|**DBTYPE_I4**||Reserved for internal use.|  
|**OBJECT_TIME_CREATED**|**DBTYPE_DBTIMESTAMP**||The UTC server time at the moment the object was created.|  
  
 This schema rowset is not sorted.  
  
## Restriction Columns  
 The **DISCOVER_OBJECT_MEMORY_USAGE** rowset can be restricted on the columns listed in the following table.  
  
|Column name|Type indicator|Restriction State|  
|-----------------|--------------------|-----------------------|  
|**OBJECT_PARENT_PATH**|**DBTYPE_WSTR**|Optional.|  
|**OBJECT_ID**|**DBTYPE_WSTR**|Optional|  
  
## See Also  
 [XML for Analysis Schema Rowsets](../../../analysis-services/schema-rowsets/xml/xml-for-analysis-schema-rowsets.md)  
  
  