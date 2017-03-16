---
title: "DMSCHEMA_MINING_COLUMNS Rowset | Microsoft Docs"
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
  - "DMSCHEMA_MINING_COLUMNS"
apitype: "NA"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "DMSCHEMA_MINING_COLUMNS rowset"
ms.assetid: ae35ccde-4438-46f4-8611-40b2b1a42fce
caps.latest.revision: 36
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# DMSCHEMA_MINING_COLUMNS Rowset
  Describes the individual columns of all data mining models in [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)]. This rowset is restricted to the current catalog.  
  
## Rowset Columns  
 The **DMSCHEMA_MINING_COLUMNS** rowset contains the following columns.  
  
|Column name|Type indicator|Description|  
|-----------------|--------------------|-----------------|  
|**MODEL_CATALOG**|**DBTYPE_WSTR**|The catalog name. Populated with the name of the database of which the model is a member.|  
|**MODEL_SCHEMA**|**DBTYPE_WSTR**|The unqualified schema name. This column is not supported by [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)]; it always contains **NULL**.|  
|**MODEL_NAME**|**DBTYPE_WSTR**|The mining model name. This column contains the name of the mining model with which a column is associated, and it is never empty.|  
|**COLUMN_NAME**|**DBTYPE_WSTR**|The name of the column.|  
|**COLUMN_GUID**|**DBTYPE_GUID**|The column GUID. This column is not supported by [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)]; it always contains **NULL**.|  
|**COLUMN_PROPID**|**DBTYPE_UI4**|The column property ID. This column is not supported by [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)]; it always contains **NULL**.|  
|**ORDINAL_POSITION**|**DBTYPE_UI4**|The ordinal position of the column. Columns are numbered starting from 1. This column contains **NULL** if there is no stable ordinal value for the column.|  
|**COLUMN_HAS_DEFAULT**|**DBTYPE_BOOL**|A Boolean that indicates whether the column has a default value.<br /><br /> **TRUE** if the column has a default value, otherwise **FALSE**.|  
|**COLUMN_DEFAULT**|**DBTYPE_WSTR**|The default value of the column.<br /><br /> If the default value is the **NULL** value, **COLUMN_HASDEFAULT** contains **TRUE**, and this column contains **NULL**.|  
|**COLUMN_FLAGS**|**DBTYPE_UI4**|A bitmask that describes characteristics of the column. The **DBCOLUMNFLAGS** enumerated type specifies the bits in the bitmask. This column is never empty.|  
|**IS_NULLABLE**|**DBTYPE_BOOL**|A Boolean that indicates whether the column is nullable.<br /><br /> **FALSE** if the column is known not to be nullable; otherwise, **TRUE**.|  
|**DATA_TYPE**|**DBTYPE_UI2**|The indicator of the column's data type. The following list shows examples of the types of indicator returned:<br /><br /> "**TABLE**" would return **DBTYPE_HCHAPTER**.<br /><br /> "**TEXT**" would return **DBTYPE_WCHAR**.<br /><br /> "**LONG**" would return **DBTYPE_I8**.<br /><br /> "**DOUBLE**" would return **DBTYPE_R8**.<br /><br /> "**DATE**" would return **DBTYPE_DATE**.|  
|**TYPE_GUID**|**DBTYPE_GUID**|The GUID of the column's data type. This column is not supported by [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)]; it always contains **VT_NULL**.|  
|**CHARACTER_MAXIMUM_LENGTH**|**DBTYPE_UI4**|The maximum possible length of a value in the column. For character, binary, or bit columns, this is one of the following:<br /><br /> The maximum length of the column in characters, bytes, or bits, respective to the column type, if a length is defined. For example, a **CHAR(5)** column in an SQL table has a maximum length of 5.<br /><br /> The maximum length of the data type in characters, bytes, or bits, respective to the column type, if the column does not have a defined length.<br /><br /> Zero (0) if neither the column nor the data type has a defined maximum length.<br /><br /> **NULL** for all other types of columns|  
|**CHARACTER_OCTET_LENGTH**|**DBTYPE_UI4**|The maximum length in octets (bytes) of the column, if the type of the column is character or binary. A value of zero (0) means the column has no maximum length. This column contains **NULL** for all other types of columns.|  
|**NUMERIC_PRECISION**|**DBTYPE_UI2**|The maximum precision of the column if the column's data type is of a numeric data type other than **VARNUMERIC**.<br /><br /> **NULL** if the column's data type is not numeric or is **VARNUMERIC**.<br /><br /> The precision of columns with a data type of **DBTYPE_DECIMAL** or **DBTYPE_NUMERIC** depends on the column definition.|  
|**NUMERIC_SCALE**|**DBTYPE_I2**|The number of digits to the right of the decimal point if the column's type indicator is **DBTYPE_DECIMAL**, **DBTYPE_NUMERIC**, or **DBTYPE_VARNUMERIC**. Otherwise, this column contains **VT_NULL**.|  
|**DATETIME_PRECISION**|**DBTYPE_UI4**|The date/time precision (number of digits in the fractional seconds portion) of the column if the column data type is a DateTime or interval type; otherwise, **NULL**.|  
|**CHARACTER_SET_CATALOG**|**DBTYPE_WSTR**|The catalog name in which the character set is defined. This column is not supported by [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)]; it always contains **NULL**.|  
|**CHARACTER_SET_SCHEMA**|**DBTYPE_WSTR**|An unqualified schema name in which the character set is defined. This column is not supported by [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)]; it always contains **NULL**.|  
|**CHARACTER_SET_NAME**|**DBTYPE_WSTR**|The character set name. This column is not supported by [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)]; it always contains **NULL**.|  
|**COLLATION_CATALOG**|**DBTYPE_WSTR**|The catalog name in which the collation is defined. This column is not supported by [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)]; it always contains **NULL**.|  
|**COLLATION_SCHEMA**|**DBTYPE_WSTR**|An unqualified schema name in which the collation is defined. This column is not supported by [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)]; it always contains **NULL**.|  
|**COLLATION_NAME**|**DBTYPE_WSTR**|The collation name. This column is not supported by [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)]; it always contains **NULL**.|  
|**DOMAIN_CATALOG**|**DBTYPE_WSTR**|The catalog name in which the domain is defined. This column is not supported by [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)]; it always contains **NULL**.|  
|**DOMAIN_SCHEMA**|**DBTYPE_WSTR**|The unqualified schema name in which the domain is defined. This column is not supported by [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)]; it always contains **NULL**.|  
|**DOMAIN_NAME**|**DBTYPE_WSTR**|The domain name. This column is not supported by [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)]; it always contains **NULL**.|  
|**DESCRIPTION**|**DBTYPE_WSTR**|A user-friendly description of the column This column is not supported by [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)]; it always contains **NULL**.|  
|**DISTRIBUTION_FLAG**|**DBTYPE_WSTR**|A description of the statistical distribution of the column. This column contains one of the following:<br /><br /> "**NORMAL**"<br /><br /> "**LOG_NORMAL**"<br /><br /> "**UNIFORM**"|  
|**CONTENT_TYPE**|**DBTYPE_WSTR**|A description of the content of the column. This column contains one of the following:<br /><br /> "**KEY**"<br /><br /> "**DISCRETE**"<br /><br /> "**CONTINUOUS**"<br /><br /> "**DISCRETIZED(**[arguments]**)**"<br /><br /> "**ORDERED**"<br /><br /> "**KEY TIME**"<br /><br /> "**CYCLICAL**"<br /><br /> "**PROBABILITY**"<br /><br /> "**VARIANCE**"<br /><br /> "**STDEV**"<br /><br /> "**SUPPORT**"<br /><br /> "**PROBABILITY_VARIANCE**"<br /><br /> "**PROBABILITY_STDEV**"<br /><br /> **"KEY SEQUENCE**"|  
|**MODELING_FLAG**|**DBTYPE_WSTR**|A comma-delimited list of flags. The defined flags are:<br /><br /> "**MODEL_EXISTENCE_ONLY**"<br /><br /> "**REGRESSOR**"<br /><br /> Algorithm-specific modeling flags can also be contained in this column.|  
|**IS_RELATED_TO_KEY**|**DBTYPE_BOOL**|A Boolean that indicates whether the column is related to the key.<br /><br /> **TRUE** if this column is related to the key. If the key is a single column, the **RELATED_ATTRIBUTE** field can optionally contain its column name.|  
|**RELATED_ATTRIBUTE**|**DBTYPE_WSTR**|The name of the target column to which the current column either relates or is a special property.|  
|**IS_INPUT**|**DBTYPE_BOOL**|A Boolean that indicates whether the column is an input column.<br /><br /> **VARIANT_TRUE** if this is an input column.|  
|**IS_PREDICTABLE**|**DBTYPE_BOOL**|A Boolean that indicates whether the column is predictable.<br /><br /> **TRUE** if the column is predictable.|  
|**CONTAINING_COLUMN**|**DBTYPE_WSTR**|The name of the **TABLE** column that contains this column. This column contains **NULL** if the column is not contained in another column.|  
|**PREDICTION_SCALAR_FUNCTIONS**|**DBTYPE_WSTR**|A comma-delimited list of scalar functions that can be performed on the column.|  
|**PREDICTION_TABLE_FUNCTIONS**|**DBTYPE_WSTR**|A comma-delimited list of functions that can be applied to the column. The functions should return a table. The list has the following format:<br /><br /> `<function name>(<column1> [, <column2>], ...)`<br /><br /> The format allows the client application to determine the signature (list of parameters) for the respective function.|  
|**IS_POPULATED**|**DBTYPE_BOOL**|A Boolean that indicates whether the column has been trained with a set of possible values.<br /><br /> **TRUE** if the column has been trained with a set of possible values.<br /><br /> Contains **FALSE** if the column is not populated.|  
|**PREDICTION_SCORE**|**DBTYPE_R8**|The score of the model on predicting the column. Score is used to measure the accuracy of a model.|  
|**SOURCE_COLUMN**|**DBTYPE_WSTR**|The name of the source mining structure column for the current mining column.|  
  
## Restriction Columns  
 The **DMSCHEMA_MINING_COLUMNS** rowset can be restricted on the columns listed in the following table.  
  
|Column name|Type indicator|Restriction State|  
|-----------------|--------------------|-----------------------|  
|**MODEL_CATALOG**|**DBTYPE_WSTR**|Optional.|  
|**MODEL_SCHEMA**|**DBTYPE_WSTR**|Optional.|  
|**MODEL_NAME**|**DBTYPE_WSTR**|Optional.|  
|**COLUMN_NAME**|**DBTYPE_WSTR**|Optional.|  
  
## See Also  
 [Data Mining Schema Rowsets](../../../analysis-services/schema-rowsets/data-mining/data-mining-schema-rowsets.md)  
  
  