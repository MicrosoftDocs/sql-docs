---
title: "MDSCHEMA_CUBES Rowset | Microsoft Docs"
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
apiname: 
  - "MDSCHEMA_CUBES"
apitype: "NA"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "MDSCHEMA_CUBES rowset"
ms.assetid: 5f1b63d4-aa3f-48c6-b866-7ffd91675044
caps.latest.revision: 32
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# MDSCHEMA_CUBES Rowset
  Describes the structure of cubes within a database.  
  
## Rowset Columns  
 The **MDSCHEMA_CUBES** rowset contains the following columns.  
  
|Column name|Type indicator|Description|  
|-----------------|--------------------|-----------------|  
|**CATALOG_NAME**|**DBTYPE_WSTR**|The name of the database.|  
|**SCHEMA_NAME**|**DBTYPE_WSTR**|Not supported.|  
|**CUBE_NAME**|**DBTYPE_WSTR**|The name of the cube or dimension. Dimension names are prefaced by a dollar sign ($) symbol.<br /><br /> Note: Only server and database administrators have permissions to see cubes created from a dimension.|  
|**CUBE_TYPE**|**DBTYPE_WSTR**|The type of the cube. Valid values are:<br /><br /> **CUBE**<br /><br /> **DIMENSION**|  
|**CUBE_GUID**|**DBTYPE_GUID**|Not supported.|  
|**CREATED_ON**|**DBTYPE_DBTIMESTAMP**|Not supported.|  
|**LAST_SCHEMA_UPDATE**|**DBTYPE_DBTIMESTAMP**|The time that the cube was last processed.|  
|**SCHEMA_UPDATED_BY**|**DBTYPE_WSTR**|Not supported.|  
|**LAST_DATA_UPDATE**|**DBTYPE_DBTIMESTAMP**|The time that the cube was last processed.|  
|**DATA_UPDATED_BY**|**DBTYPE_WSTR**|Not supported.|  
|**DESCRIPTION**|**DBTYPE_WSTR**|A user-friendly description of the cube.|  
|**IS_DRILLTHROUGH_ENABLED**|**DBTYPE_BOOL**|A Boolean that always returns true.|  
|**IS_LINKABLE**|**DBTYPE_BOOL**|A Boolean that indicates whether a cube can be used in a linked cube.|  
|**IS_WRITE_ENABLED**|**DBTYPE_BOOL**|A Boolean that indicates whether a cube is write-enabled.|  
|**IS_SQL_ENABLED**|**DBTYPE_BOOL**|A Boolean that indicates whether SQL can be used on the cube.|  
|**CUBE_CAPTION**|**DBTYPE_WSTR**|The caption of the cube.|  
|**BASE_CUBE_NAME**|**DBTYPE_WSTR**|The name of the source cube if this cube is a perspective cube.|  
|**ANNOTATIONS**|**DBTYPE_WSTR**|(Optional) A set of notes, in XML format.|  
  
 The rowset is sorted on **CATALOG_NAME**, **SCHEMA_NAME**, **CUBE_NAME**.  
  
## Restriction Columns  
 The **MDSCHEMA_CUBES** rowset can be restricted on the columns listed in the following table.  
  
|Column name|Type indicator|Restriction State|  
|-----------------|--------------------|-----------------------|  
|**CATALOG_NAME**|**DBTYPE_WSTR**|Optional.|  
|**SCHEMA_NAME**|**DBTYPE_WSTR**|Optional.|  
|**CUBE_NAME**|**DBTYPE_WSTR**|Optional.|  
|**CUBE_SOURCE**|**DBTYPE_UI2**|(Optional) Default restriction is a value of 1. A bitmap with one of these valid values:<br /><br /> 1 CUBE<br /><br /> 2 DIMENSION|  
|**Base Cube_Name**|**DBTYPE_WSTR**|Optional.|  
  
## See Also  
 [OLE DB for OLAP Schema Rowsets](../../../analysis-services/schema-rowsets/ole-db-olap/ole-db-for-olap-schema-rowsets.md)  
  
  