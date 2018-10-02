---
title: "MDSCHEMA_MEASUREGROUPS Rowset | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "MDSCHEMA_MEASUREGROUPS"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "MDSCHEMA_MEASUREGROUPS rowset"
ms.assetid: bab1bbd0-421b-4fad-9aee-e6511e0e1f1b
author: minewiskan
ms.author: owend
manager: craigg
---
# MDSCHEMA_MEASUREGROUPS Rowset
  Describes the measure groups within a database.  
  
## Rowset Columns  
 The `MDSCHEMA_MEASUREGROUPS` rowset contains the following columns.  
  
|Column name|Type indicator|Length|Description|  
|-----------------|--------------------|------------|-----------------|  
|`CATALOG_NAME`|`DBTYPE_WSTR`||The name of the catalog to which this measure group belongs. `NULL` if the provider does not support catalogs.|  
|`SCHEMA_NAME`|`DBTYPE_WSTR`||Not supported.|  
|`CUBE_NAME`|`DBTYPE_WSTR`||The name of the cube to which this measure group belongs.|  
|`MEASUREGROUP_NAME`|`DBTYPE_WSTR`||The name of the measure group.|  
|`DESCRIPTION`|`DBTYPE_WSTR`||A human-readable description of the member.|  
|`IS_WRITE_ENABLED`|`DBTYPE_BOOL`||A Boolean that indicates whether the measure group is write-enabled.<br /><br /> Returns `true` if the measure group is write enabled.|  
|`MEASUREGROUP_CAPTION`|`DBTYPE_WSTR`||The display caption for the measure group.|  
  
 This schema rowset is not sorted.  
  
## Restriction Columns  
 The `MDSCHEMA_MEASUREGROUPS` rowset can be restricted on the columns in the following table.  
  
|Column name|Type indicator|Restriction State|  
|-----------------|--------------------|-----------------------|  
|`CATALOG_NAME`|`DBTYPE_WSTR`|Optional.|  
|`SCHEMA_NAME`|`DBTYPE_WSTR`|Optional.|  
|`CUBE_NAME`|`DBTYPE_WSTR`|Optional.|  
|`MEASUREGROUP_NAME`|`DBTYPE_WSTR`|Optional.|  
  
## See Also  
 [OLE DB for OLAP Schema Rowsets](ole-db-for-olap-schema-rowsets.md)  
  
  
