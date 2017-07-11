---
title: "MDSCHEMA_MEASUREGROUPS Rowset | Microsoft Docs"
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
  - "MDSCHEMA_MEASUREGROUPS"
apitype: "NA"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "MDSCHEMA_MEASUREGROUPS rowset"
ms.assetid: bab1bbd0-421b-4fad-9aee-e6511e0e1f1b
caps.latest.revision: 28
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# MDSCHEMA_MEASUREGROUPS Rowset
  Describes the measure groups within a database.  
  
## Rowset Columns  
 The **MDSCHEMA_MEASUREGROUPS** rowset contains the following columns.  
  
|Column name|Type indicator|Length|Description|  
|-----------------|--------------------|------------|-----------------|  
|**CATALOG_NAME**|**DBTYPE_WSTR**||The name of the catalog to which this measure group belongs. **NULL** if the provider does not support catalogs.|  
|**SCHEMA_NAME**|**DBTYPE_WSTR**||Not supported.|  
|**CUBE_NAME**|**DBTYPE_WSTR**||The name of the cube to which this measure group belongs.|  
|**MEASUREGROUP_NAME**|**DBTYPE_WSTR**||The name of the measure group.|  
|**DESCRIPTION**|**DBTYPE_WSTR**||A human-readable description of the member.|  
|**IS_WRITE_ENABLED**|**DBTYPE_BOOL**||A Boolean that indicates whether the measure group is write-enabled.<br /><br /> Returns **true** if the measure group is write enabled.|  
|**MEASUREGROUP_CAPTION**|**DBTYPE_WSTR**||The display caption for the measure group.|  
  
 This schema rowset is not sorted.  
  
## Restriction Columns  
 The **MDSCHEMA_MEASUREGROUPS** rowset can be restricted on the columns in the following table.  
  
|Column name|Type indicator|Restriction State|  
|-----------------|--------------------|-----------------------|  
|**CATALOG_NAME**|**DBTYPE_WSTR**|Optional.|  
|**SCHEMA_NAME**|**DBTYPE_WSTR**|Optional.|  
|**CUBE_NAME**|**DBTYPE_WSTR**|Optional.|  
|**MEASUREGROUP_NAME**|**DBTYPE_WSTR**|Optional.|  
  
## See Also  
 [OLE DB for OLAP Schema Rowsets](../../../analysis-services/schema-rowsets/ole-db-olap/ole-db-for-olap-schema-rowsets.md)  
  
  