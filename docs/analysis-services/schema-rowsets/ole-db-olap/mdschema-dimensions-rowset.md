---
title: "MDSCHEMA_DIMENSIONS Rowset | Microsoft Docs"
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
  - "MDSCHEMA_DIMENSIONS"
apitype: "NA"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "MDSCHEMA_DIMENSIONS rowset"
ms.assetid: a0fd94bb-359a-4df6-93a6-d60d50223944
caps.latest.revision: 30
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# MDSCHEMA_DIMENSIONS Rowset
  Describes the shared and private dimensions within a database.  
  
## Rowset Columns  
 The **MDSCHEMA_DIMENSIONS** rowset contains the following columns:  
  
|Column name|Type indicator|Description|  
|-----------------|--------------------|-----------------|  
|**CATALOG_NAME**|**DBTYPE_WSTR**|The name of the database.|  
|**SCHEMA_NAME**|**DBTYPE_WSTR**|Not supported.|  
|**CUBE_NAME**|**DBTYPE_WSTR**|The name of the cube.|  
|**DIMENSION_NAME**|**DBTYPE_WSTR**|The name of the dimension. If a dimension is part of more than one cube or measure group, then there is one row for each unique combination of dimension, measure group, and cube.|  
|**DIMENSION_UNIQUE_NAME**|**DBTYPE_WSTR**|The unique name of the dimension.|  
|**DIMENSION_GUID**|**DBTYPE_GUID**|Not supported.|  
|**DIMENSION_CAPTION**|**DBTYPE_WSTR**|The caption of the dimension. This should be used when displaying the name of the dimension to the user, such as in the user interface or reports.|  
|**DIMENSION_ORDINAL**|**DBTYPE_UI4**|The position of the dimension within the cube.|  
|**DIMENSION_TYPE**|**DBTYPE_I2**|The type of the dimension. Valid values include:<br /><br /> **MD_DIMTYPE_UNKNOWN** (**0**)<br /><br /> **MD_DIMTYPE_TIME** (**1**)<br /><br /> **MD_DIMTYPE_MEASURE** (**2**)<br /><br /> **MD_DIMTYPE_OTHER** (**3**)<br /><br /> **MD_DIMTYPE_QUANTITATIVE** (**5**)<br /><br /> **MD_DIMTYPE_ACCOUNTS** (**6**)<br /><br /> **MD_DIMTYPE_CUSTOMERS** (**7**)<br /><br /> **MD_DIMTYPE_PRODUCTS** (**8**)<br /><br /> **MD_DIMTYPE_SCENARIO** (**9**)<br /><br /> **MD_DIMTYPE_UTILIY** (**10**)<br /><br /> **MD_DIMTYPE_CURRENCY** (**11**)<br /><br /> **MD_DIMTYPE_RATES** (**12**)<br /><br /> **MD_DIMTYPE_CHANNEL** (**13**)<br /><br /> **MD_DIMTYPE_PROMOTION** (**14**)<br /><br /> **MD_DIMTYPE_ORGANIZATION** (**15**)<br /><br /> **MD_DIMTYPE_BILL_OF_MATERIALS** (**16**)<br /><br /> **MD_DIMTYPE_GEOGRAPHY** (**17**)|  
|**DIMENSION_CARDINALITY**|**DBTYPE_UI4**|The number of members in the key attribute.|  
|**DEFAULT_HIERARCHY**|**DBTYPE_WSTR**|A hierarchy from the dimension. Preserved for backwards compatibility.|  
|**DESCRIPTION**|**DBTYPE_WSTR**|A user-friendly description of the dimension.|  
|**IS_VIRTUAL**|**DBTYPE_BOOL**|Always **FALSE**.|  
|**IS_READWRITE**|**DBTYPE_BOOL**|A Boolean that indicates whether the dimension is write-enabled.<br /><br /> **TRUE** if the dimension is write-enabled.|  
|**DIMENSION_UNIQUE_SETTINGS**|**DBTYPE_I4**|A bitmap that specifies which columns contain unique values if the dimension contains only members with unique names. The following bit value constants are defined in Msmd.h for this bitmap:<br /><br /> **MDDIMENSIONS_MEMBER_KEY_UNIQUE**|  
|**DIMENSION_MASTER_UNIQUE_NAME**|**DBTYPE_WSTR**|Always **NULL**.|  
|**DIMENSION_IS_VISIBLE**|**DBTYPE_BOOL**|Always **TRUE**.<br /><br /> Note: A dimension is not visible unless one or more hierarchies in the dimension are visible.|  
  
 The rowset is sorted on **CATALOG_NAME**, **SCHEMA_NAME**, **CUBE_NAME**, **DIMENSION_NAME**.  
  
## Restriction Columns  
 The **MDSCHEMA_DIMENSIONS** rowset can be restricted on the columns listed in the following table.  
  
|Column name|Type indicator|Restriction State|  
|-----------------|--------------------|-----------------------|  
|**CATALOG_NAME**|**DBTYPE_WSTR**|Optional.|  
|**SCHEMA_NAME**|**DBTYPE_WSTR**|Optional.|  
|**CUBE_NAME**|**DBTYPE_WSTR**|Optional.|  
|**DIMENSION_NAME**|**DBTYPE_WSTR**|Optional.|  
|**DIMENSION_UNIQUE_NAME**|**DBTYPE_WSTR**|Optional.|  
|**CUBE_SOURCE**|**DBTYPE_UI2**|(Optional) Default restriction is a value of 1. A bitmap with one of the following valid values:<br /><br /> 1 CUBE<br /><br /> 2 DIMENSION|  
|**DIMENSION_VISIBILITY**|**DBTYPE_UI2**|(Optional) Default restriction is a value of 1. A bitmap with one of the following valid values:<br /><br /> 1 Visible<br /><br /> 2 Not visible|  
  
## See Also  
 [OLE DB for OLAP Schema Rowsets](../../../analysis-services/schema-rowsets/ole-db-olap/ole-db-for-olap-schema-rowsets.md)  
  
  