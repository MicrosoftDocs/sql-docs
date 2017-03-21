---
title: "DBSCHEMA_COLUMNS Rowset | Microsoft Docs"
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
apiname: 
  - "DBSCHEMA_COLUMNS"
apitype: "NA"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "DBSCHEMA_COLUMNS rowset"
ms.assetid: 653bdd07-a533-4a99-8b6a-6e5c7322e1f3
caps.latest.revision: 40
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# DBSCHEMA_COLUMNS Rowset
  Provides column information for all columns meeting the provided restriction criteria.  
  
## Rowset Columns  
 The **DBSCHEMA_COLUMNS** rowset contains the following columns.  
  
|Column name|Type indicator|Length|Description|  
|-----------------|--------------------|------------|-----------------|  
|**TABLE_CATALOG**|**DBTYPE_WSTR**||The name of the Database.|  
|**TABLE_SCHEMA**|**DBTYPE_WSTR**||Not supported.|  
|**TABLE_NAME**|**DBTYPE_WSTR**||The name of the cube.|  
|**COLUMN_NAME**|**DBTYPE_WSTR**||The name of the attribute hierarchy or measure.|  
|**COLUMN_GUID**|**DBTYPE_GUID**||Not supported.|  
|**COLUMN_PROPID**|**DBTYPE_UI4**||Not supported.|  
|**ORDINAL_POSITION**|**DBTYPE_UI4**||The position of the column, beginning with 1.|  
|**COLUMN_HAS_DEFAULT**|**DBTYPE_BOOL**||Not supported.|  
|**COLUMN_DEFAULT**|**DBTYPE_WSTR**||Not supported.|  
|**COLUMN_FLAGS**|**DBTYPE_UI4**||A **DBCOLUMNFLAGS** bitmask indicating column properties. See 'DBCOLUMNFLAGS Enumerated Type' in [IColumnsInfo::GetColumnInfo](http://msdn2.microsoft.com/library/ms722704.aspx)|  
|**IS_NULLABLE**|**DBTYPE_BOOL**||Always returns **false**.|  
|**DATA_TYPE**|**DBTYPE_WSTR**<br /><br /> **DBTYPE_VARIANT**||The data type of the column. Returns a string for dimension columns and a variant for measures.|  
|**TYPE_GUID**|**DBTYPE_GUID**||Not supported.|  
|**CHARACTER_MAXIMUM_LENGTH**|**DBTYPE_UI4**||The maximum possible length of a value within the column.<br /><br /> This is retrieved from the **DataSize** property in the **DataItem**.|  
|**CHARACTER_OCTET_LENGTH**|**DBTYPE_UI4**||The maximum possible length of a value within the column, in bytes, for character or binary columns.<br /><br /> A value of zero (0) indicates the column has no maximum length.<br /><br /> **NULL** will be returned for columns that do not return binary or character data types.|  
|**NUMERIC_PRECISION**|**DBTYPE_UI2**||The maximum precision of the column for numeric data types other than **DBTYPE_VARNUMERIC**.|  
|**NUMERIC_SCALE**|**DBTYPE_I2**||The number of digits to the right of the decimal point for **DBTYPE_DECIMAL**, **DBTYPE_NUMERIC**, **DBTYPE_VARNUMERIC**. Otherwise, this is **NULL**.|  
|**DATETIME_PRECISION**|**DBTYPE_UI4**||Not supported.|  
|**CHARACTER_SET_CATALOG**|**DBTYPE_WSTR**||Not supported.|  
|**CHARACTER_SET_SCHEMA**|**DBTYPE_WSTR**||Not supported.|  
|**CHARACTER_SET_NAME**|**DBTYPE_WSTR**||Not supported.|  
|**COLLATION_CATALOG**|**DBTYPE_WSTR**||Not supported.|  
|**COLLATION_SCHEMA**|**DBTYPE_WSTR**||Not supported.|  
|**COLLATION_NAME**|**DBTYPE_WSTR**||Not supported.|  
|**DOMAIN_CATALOG**|**DBTYPE_WSTR**||Not supported.|  
|**DOMAIN_SCHEMA**|**DBTYPE_WSTR**||Not supported.|  
|**DOMAIN_NAME**|**DBTYPE_WSTR**||Not supported.|  
|**DESCRIPTION**|**DBTYPE_WSTR**||Not supported.|  
|**COLUMN_OLAP_TYPE**|**DBTYPE_WSTR**||The OLAP type of the object.<br /><br /> **MEASURE** indicates the object is a measure.<br /><br /> **ATTRIBUTE** indicates the object is a dimension attribute.<br /><br /> **SCHEMA** indicates the object is a column in a schema.|  
  
 The rowset is sorted on **TABLE_CATALOG**, **TABLE_SCHEMA**, **TABLE_NAME**.  
  
## Restriction Columns  
 The **DBSCHEMA_COLUMNS** rowset can be restricted on the columns listed in the following table.  
  
|Column name|Type indicator|Restriction State|  
|-----------------|--------------------|-----------------------|  
|**TABLE_CATALOG**|**DBTYPE_WSTR**|Optional|  
|**TABLE_SCHEMA**|**DBTYPE_WSTR**|Optional|  
|**TABLE_NAME**|**DBTYPE_WSTR**|Optional|  
|**COLUMN_NAME**|**DBTYPE_WSTR**|Optional|  
|**COLUMN_OLAP_TYPE**|**DBTYPE_WSTR**|Optional|  
  
## See Also  
 [OLE DB Schema Rowsets](../../../analysis-services/schema-rowsets/ole-db/ole-db-schema-rowsets.md)  
  
  