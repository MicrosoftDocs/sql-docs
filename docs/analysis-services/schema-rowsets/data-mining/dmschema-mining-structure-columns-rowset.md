---
title: "DMSCHEMA_MINING_STRUCTURE_COLUMNS Rowset | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "analysis-services/data-mining"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "DMSCHEMA_MINING_STRUCTURE_COLUMNS"
apitype: "NA"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "DMSCHEMA_MINING_STRUCTURE_COLUMNS rowset"
ms.assetid: 81f25502-ac90-42f1-8ddf-7b0f9752ebfd
caps.latest.revision: 35
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# DMSCHEMA_MINING_STRUCTURE_COLUMNS Rowset
  Describes the individual columns of all mining structures deployed on a server that is running [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)].  
  
## Rowset Columns  
 The **DMSCHEMA_MINING_STRUCTURE_COLUMNS** rowset contains the following columns.  
  
|Column name|Type indicator|Length|Description|  
|-----------------|--------------------|------------|-----------------|  
|**STRUCTURE_CATALOG**|**DBTYPE_WSTR**||The catalog name.|  
|**STRUCTURE_SCHEMA**|**DBTYPE_WSTR**||The unqualified schema name. [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] does not support schemas, so this column is always **NULL**.|  
|**STRUCTURE_NAME**|**DBTYPE_WSTR**||The structure name. This column cannot contain a **NULL**.|  
|**COLUMN_NAME**|**DBTYPE_WSTR**||The name of the column. Uniqueness is only guaranteed among columns that share the same pattern. For example, two nested columns may have the same name if they belong to two different nested tables inside the same structure.|  
|**COLUMN_GUID**|**DBTYPE_GUID**||The column GUID. Providers that do not use GUIDs to identify columns should return **NULL** in this column.|  
|**COLUMN_PROPID**|**DBTYPE_UI4**||The column property ID. Providers that do not associate property IDs with columns should return **NULL** in this column. [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] returns **NULL** for this column.|  
|**ORDINAL_POSITION**|**DBTYPE_UI4**||The ordinal of the column. Columns are numbered starting from 1. **NULL** if there is no stable ordinal value for the column.|  
|**COLUMN_HASDEFAULT**|**DBTYPE_BOOL**||A Boolean that indicates whether this column has a default value.<br /><br /> **TRUE** if the column has a default value.<br /><br /> **FALSE** if the column does not have a default value or if it is unknown whether the column has a default value.|  
|**COLUMN_DEFAULT**|**DBTYPE_WSTR**||The default value of the column. A provider may expose **DBCOLUMN_DEFAULTVALUE** but not **DBCOLUMN_HASDEFAULT** (for ISO tables) in the rowset returned by **IColumnsRowset::GetColumnsRowset**.<br /><br /> If the default value is **NULL**, **COLUMN_HASDEFAULT** is **TRUE** and the **COLUMN_DEFAULT** column is a **NULL** value.|  
|**COLUMN_FLAGS**|**DBTYPE_UI4**||A bitmask that describes column characteristics. The **DBCOLUMNFLAGS** enumerated type specifies the bits in the bitmask. This column cannot contain a **NULL** value. Valid values include:<br /><br /> **DBCOLUMNFLAGS_ISNULLABLE** (**0x20**)<br /><br /> **DBCOLUMNFLAGS_MAYBENULL** (**0x40**)<br /><br /> **DBCOLUMNFLAGS_ISLONG** (**0x80**)|  
|**IS_NULLABLE**|**DBTYPE_BOOL**||A Boolean that indicates whether this column has a default value.<br /><br /> **TRUE** if the column can contain **NULL**; **FALSE**, otherwise.|  
|**DATA_TYPE**|**DBTYPE_UI2**||The indicator of the column's data type. For example:<br /><br /> "**TABLE**" = **DBTYPE_HCHAPTER**<br /><br /> "**TEXT**" = **DBTYPE_WCHAR**<br /><br /> "**LONG**" = **DBTYPE_I8**<br /><br /> "**DOUBLE**" = **DBTYPE_R8**<br /><br /> "**DATE**" = **DBTYPE_DATE**|  
|**TYPE_GUID**|**DBTYPE_GUID**||The GUID of the column's data type. Providers that do not use GUIDs to identify data types should return **NULL** in this column.|  
|**CHARACTER_MAXIMUM_LENGTH**|**DBTYPE_UI4**||The maximum possible length of a value in the column. For character, binary, or bit columns, this is one of the following:<br /><br /> The maximum length of the column in characters, bytes, or bits, respectively, if the length is defined. For example, a `CHAR(5)` column in an SQL table has a maximum length of 5.<br /><br /> The maximum length of the data type in characters, bytes, or bits, respectively, if the column does not have a defined length.<br /><br /> Zero (0) if neither the column nor the data type has a defined maximum length.<br /><br /> **NULL** for all other types of columns.|  
|**CHARACTER_OCTET_LENGTH**|**DBTYPE_UI4**||The maximum length in octets (bytes) of the column, if the type of the column is character or binary. A value of zero (0) means the column has no maximum length. **NULL** for all other types of columns.|  
|**NUMERIC_PRECISION**|**DBTYPE_UI2**||The maximum precision of the column if the column's data type is of a numeric data type other than **VARNUMERIC**; **NULL** if the column's data type is not numeric or is **VARNUMERIC**.<br /><br /> The precision of columns with a data type of **DBTYPE_DECIMAL** or **DBTYPE_NUM**ERIC depends on the definition of the column.|  
|**NUMERIC_SCALE**|**DBTYPE_I2**||The number of digits to the right of the decimal point if the column's type indicator is **DBTYPE_DECIMAL**, **DBTYPE_NUMERIC**, or **DBTYPE_VARNUMERIC**. Otherwise, this is **NULL**.|  
|**DATETIME_PRECISION**|**DBTYPE_UI4**||The DateTime precision (the number of digits in the fractional seconds portion) of the column if the column is a datetime or interval type. If the column's data type is not datetime, this is **NULL**.|  
|**CHARACTER_SET_CATALOG**|**DBTYPE_WSTR**||The catalog name in which the character set is defined. **NULL** if the provider does not support catalogs or different character sets.|  
|**CHARACTER_SET_SCHEMA**|**DBTYPE_WSTR**||The unqualified schema name in which the character set is defined. **NULL** if the provider does not support schemas or different character sets.|  
|**CHARACTER_SET_NAME**|**DBTYPE_WSTR**||The character set name. **NULL** if the provider does not support different character sets.|  
|**COLLATION_CATALOG**|**DBTYPE_WSTR**||The catalog name in which the collation is defined. **NULL** if the provider does not support catalogs or different collations.|  
|**COLLATION_SCHEMA**|**DBTYPE_WSTR**||The unqualified schema name in which the collation is defined. **NULL** if the provider does not support schemas or different collations.|  
|**COLLATION_NAME**|**DBTYPE_WSTR**||The collation name. **NULL** if the provider does not support different collations.|  
|**DOMAIN_CATALOG**|**DBTYPE_WSTR**||The catalog name in which the domain is defined. **NULL** if the provider does not support catalogs or domains.|  
|**DOMAIN_SCHEMA**|**DBTYPE_WSTR**||The unqualified schema name in which the domain is defined. **NULL** if the provider does not support schemas or domains.|  
|**DOMAIN_NAME**|**DBTYPE_WSTR**||The domain name. **NULL** if the provider does not support domains.|  
|**DESCRIPTION**|**DBTYPE_WSTR**||A human-readable description of the column. **NULL** if there is no description associated with the column.|  
|**DISTRIBUTION_FLAG**|**DBTYPE_WSTR**||The distribution of the mining structure column:<br /><br /> "**NORMAL**"<br /><br /> "**LOG_NORM**AL"<br /><br /> "**UNIFORM**"|  
|**CONTENT_TYPE**|**DBTYPE_WSTR**||The content type of the mining structure column:<br /><br /> "**KEY**"<br /><br /> "**DISCRETE**"<br /><br /> "**CONTINUOUS**"<br /><br /> "**DISCRETIZED(**[args]**)**"<br /><br /> "**ORDERED**"<br /><br /> "**SEQUENCE_TIME**"<br /><br /> "**CYCLICAL**"<br /><br /> "**PROBABILITY**"<br /><br /> "**VARIANCE**"<br /><br /> "**STDEV**"<br /><br /> "**SUPPORT**"<br /><br /> "**PROBABILITY_VARIANCE**"<br /><br /> "**PROBABILITY_STDEV**"|  
|**MODELING_FLAG**|**DBTYPE_WSTR**||A comma-delimited list of modeling flags. The only supported flag for a structure column is "**NOT NULL**".|  
|**IS_RELATED_TO_KEY**|**DBTYPE_BOOL**||A Boolean that indicates whether this column is related to the key.<br /><br /> **VARIANT_TRUE** if this column is related to the key; **VARIANT_FALSE** otherwise. If the key is a single column, the **RELATED_ATTRIBUTE** field optionally may contain its column name.|  
|**RELATED_ATTRIBUTE**|**DBTYPE_WSTR**||The name of the target column that the current column relates to, or is a special property of.|  
|**CONTAINING_COLUMN**|**DBTYPE_WSTR**||The name of the **TABLE** column containing this column. **NULL** if no table contains the column.|  
|**IS_POPULATED**|**DBTYPE_BOOL**||A Boolean that indicates whether this column has learned a set of possible values.<br /><br /> **TRUE** if the column has learned a set of possible values; **FALSE**, otherwise.|  
  
## Restriction Columns  
 The **DMSCHEMA_MINING_STRUCTURE_COLUMNS** rowset can be restricted on the columns in the following table.  
  
|Column name|Type indicator|Restriction State|  
|-----------------|--------------------|-----------------------|  
|**STRUCTURE_CATALOG**|**DBTYPE_WSTR**|Optional.|  
|**STRUCTURE_SCHEMA**|**DBTYPE_WSTR**|Optional.|  
|**STRUCTURE_NAME**|**DBTYPE_WSTR**|Optional.|  
|**COLUMN_NAME**|**DBTYPE_WSTR**|Optional.|  
  
## See Also  
 [Data Mining Schema Rowsets](../../../analysis-services/schema-rowsets/data-mining/data-mining-schema-rowsets.md)  
  
  