---
title: "MDSCHEMA_MEASURES Rowset | Microsoft Docs"
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
  - "MDSCHEMA_MEASURES"
apitype: "NA"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "MDSCHEMA_MEASURES rowset"
ms.assetid: 6ff5bd1a-aad0-49b8-9f8d-7df2637caacf
caps.latest.revision: 31
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# MDSCHEMA_MEASURES Rowset
  Describes each measure within a cube.  
  
## Rowset Columns  
 The **MDSCHEMA_MEASURES** rowset contains the following columns.  
  
|Column name|Type indicator|Length|Description|  
|-----------------|--------------------|------------|-----------------|  
|**CATALOG_NAME**|**DBTYPE_WSTR**||The name of the catalog to which this measure belongs. **NULL** if the provider does not support catalogs.|  
|**SCHEMA_NAME**|**DBTYPE_WSTR**||The name of the schema to which this measure belongs. **NULL** if the provider does not support schemas.|  
|**CUBE_NAME**|**DBTYPE_WSTR**||The name of the cube to which this measure belongs.|  
|**MEASURE_NAME**|**DBTYPE_WSTR**||The name of the measure.|  
|**MEASURE_UNIQUE_NAME**|**DBTYPE_WSTR**||The Unique name of the measure. For providers that generate unique names by qualification, each component of this name is delimited.|  
|**MEASURE_CAPTION**|**DBTYPE_WSTR**||A label or caption associated with the measure. Used primarily for display purposes. If a caption does not exist, **MEASURE_NAME** is returned.|  
|**MEASURE_GUID**|**DBTYPE_GUID**||Not supported.|  
|**MEASURE_AGGREGATOR**|**DBTYPE_I4**||An enumeration that identifies how a measure was derived. Can be one of the following values:<br /><br /> **MDMEASURE_AGGR_SUM** (**1**) identifies that the measure aggregates from **SUM**.<br /><br /> **MDMEASURE_AGGR_COUNT** (**2**) identifies that the measure aggregates from **COUNT**.<br /><br /> **MDMEASURE_AGGR_MIN** (**3**) identifies that the measure aggregates from **MIN**.<br /><br /> **MDMEASURE_AGGR_MAX** (**4**) identifies that the measure aggregates from **MAX**.<br /><br /> **MDMEASURE_AGGR_AVG** (**5**) identifies that the measure aggregates from **AVG**.<br /><br /> **MDMEASURE_AGGR_VAR** (**6**) identifies that the measure aggregates from **VAR**.<br /><br /> **MDMEASURE_AGGR_STD** (**7**) identifies that the measure aggregates from **STDEV**.<br /><br /> **MDMEASURE_AGGR_DST** (**8**) identifies that the measure aggregates from **DISTINCT COUNT**.<br /><br /> **MDMEASURE_AGGR_NONE** (**9**) identifies that the measure aggregates from **NONE**.<br /><br /> **MDMEASURE_AGGR_AVGCHILDREN** (**10**) identifies that the measure aggregates from **AVERAGEOFCHILDREN**.<br /><br /> **MDMEASURE_AGGR_FIRSTCHILD** (**11**) identifies that the measure aggregates from **FIRSTCHILD**.<br /><br /> **MDMEASURE_AGGR_LASTCHILD** (**12**) identifies that the measure aggregates from **LASTCHILD**.<br /><br /> **MDMEASURE_AGGR_FIRSTNONEMPTY** (**13**) identifies that the measure aggregates from **FIRSTNONEMPTY**,<br /><br /> **MDMEASURE_AGGR_LASTNONEMPTY** (**14**) identifies that the measure aggregates from **LASTNONEMPTY**.<br /><br /> **MDMEASURE_AGGR_BYACCOUNT** (**15**) identifies that the measure aggregates from **BYACCOUNT**.<br /><br /> **MDMEASURE_AGGR_CALCULATED** (**127**) identifies that the measure was derived from a formula that was not any single function above.<br /><br /> **MDMEASURE_AGGR_UNKNOWN** (**0**) identifies that the measure was derived from an unknown aggregation function or formula.|  
|**DATA_TYPE**|**DBTYPE_UI2**||The data type of the measure.|  
|**NUMERIC_PRECISION**|**DBTYPE_UI2**||The maximum precision of the property if the measure object's data type is exact numeric. **NULL** for all other property types.|  
|**NUMERIC_SCALE**|**DBTYPE_I2**||The number of digits to the right of the decimal point if the measure object's type indicator is **DBTYPE_NUMERIC** or **DBTYPE_DECIMAL**. Otherwise, this value is **NULL**.|  
|**MEASURE_UNITS**|**DBTYPE_WSTR**||Not supported|  
|**DESCRIPTION**|**DBTYPE_WSTR**||A human-readable description of the measure. **NULL** if no description exists.|  
|**EXPRESSION**|**DBTYPE_WSTR**||An expression for the member.|  
|**MEASURE_IS_VISIBLE**|**DBTYPE_BOOL**||A Boolean that always returns True. If the measure is not visible, it will not be included in the schema rowset.|  
|**LEVELS_LIST**|**DBTYPE_WSTR**||A string that always returns **NULL**.|  
|**MEASURE_NAME_SQL_COLUMN_NAME**|**DBTYPE_WSTR**||The name of the column in the SQL query that corresponds to the measure's name.|  
|**MEASURE_UNQUALIFIED_CAPTION**|**DBTYPE_WSTR**||The name of the measure, not qualified with the measure group name.|  
|**MEASUREGROUP_NAME**|**DBTYPE_WSTR**||The name of the measure group to which the measure belongs.|  
|**MEASURE_DISPLAY_FOLDER**|**DBTYPE_WSTR**||The path to be used when displaying the measure in the user interface. Folder names will be separated by a semicolon. Nested folders are indicated by a backslash (\\).|  
|**DEFAULT_FORMAT_STRING**|**DBTYPE_WSTR**||The default format string for the measure.|  
  
 The rowset is sorted on **CATALOG_NAME**, **SCHEMA_NAME**, **CUBE_NAME**, **MEASURE_NAME**.  
  
## Restriction Columns  
 The **MDSCHEMA_MEASURES** rowset can be restricted on the columns listed in the following table.  
  
|Column name|Type indicator|Restriction State|  
|-----------------|--------------------|-----------------------|  
|**CATALOG_NAME**|**DBTYPE_WSTR**|Optional.|  
|**SCHEMA_NAME**|**DBTYPE_WSTR**|Optional.|  
|**CUBE_NAME**|**DBTYPE_WSTR**|Optional.|  
|**MEASURE_NAME**|**DBTYPE_WSTR**|Optional.|  
|**MEASURE_UNIQUE_NAME**|**DBTYPE_WSTR**|Optional.|  
|**MEASUREGROUP_NAME**|**DBTYPE_WSTR**|Optional.|  
|**CUBE_SOURCE**|**DBTYPE_UI2**|(Optional) Default restriction is a value of 1. A bitmap with one of the following valid values:<br /><br /> 1 CUBE<br /><br /> 2 DIMENSION|  
|**MEASURE_VISIBILITY**|**DBTYPE_UI2**|(Optional) Default restriction is a value of 1. A bitmap with one of the following valid values:<br /><br /> 1 Visible<br /><br /> 2 Not Visible|  
  
## See Also  
 [OLE DB for OLAP Schema Rowsets](../../../analysis-services/schema-rowsets/ole-db-olap/ole-db-for-olap-schema-rowsets.md)  
  
  