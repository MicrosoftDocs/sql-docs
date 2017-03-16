---
title: "MDSCHEMA_LEVELS Rowset | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "MDSCHEMA_LEVELS"
apitype: "NA"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "MDSCHEMA_LEVELS rowset"
ms.assetid: 4313e268-33f4-4e99-96d7-2ec26775c580
caps.latest.revision: 33
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# MDSCHEMA_LEVELS Rowset
  Describes each level within a particular hierarchy.  
  
## Rowset Columns  
 The **MDSCHEMA_LEVELS** rowset contains the following columns.  
  
|Column name|Type indicator|Description|  
|-----------------|--------------------|-----------------|  
|**CATALOG_NAME**|**DBTYPE_WSTR**|The name of the catalog to which this level belongs. **NULL** if the provider does not support catalogs.|  
|**SCHEMA_NAME**|**DBTYPE_WSTR**|The name of the schema to which this level belongs. **NULL** if the provider does not support schemas.|  
|**CUBE_NAME**|**DBTYPE_WSTR**|The name of the cube to which this level belongs.|  
|**DIMENSION_UNIQUE_NAME**|**DBTYPE_WSTR**|The unique name of the dimension to which this level belongs. For providers that generate unique names by qualification, each component of this name is delimited.|  
|**HIERARCHY_UNIQUE_NAME**|**DBTYPE_WSTR**|The unique name of the hierarchy. If the level belongs to more than one hierarchy, there is one row for each hierarchy to which it belongs. For providers that generate unique names by qualification, each component of this name is delimited.|  
|**LEVEL_NAME**|**DBTYPE_WSTR**|The name of the level.|  
|**LEVEL_UNIQUE_NAME**|**DBTYPE_WSTR**|The properly escaped unique name of the level.|  
|**LEVEL_GUID**|**DBTYPE_GUID**|Not supported.|  
|**LEVEL_CAPTION**|**DBTYPE_WSTR**|A label or caption associated with the hierarchy. Used primarily for display purposes. If a caption does not exist, **LEVEL_NAME** is returned.|  
|**LEVEL_NUMBER**|**DBTYPE_UI4**|The distance of the level from the root of the hierarchy. Root level is zero (**0)**.|  
|**LEVEL_CARDINALITY**|**DBTYPE_UI4**|The number of members in the level.|  
|**LEVEL_TYPE**|**DBTYPE_I4**|Type of the level:<br /><br /> **MDLEVEL_TYPE_GEO_CONTINENT** (**0x2001**)<br /><br /> **MDLEVEL_TYPE_GEO_REGION** (**0x2002**)<br /><br /> **MDLEVEL_TYPE_GEO_COUNTRY** (**0x2003**)<br /><br /> **MDLEVEL_TYPE_GEO_STATE_OR_PROVINCE** (**0x2004**)<br /><br /> **MDLEVEL_TYPE_GEO_COUNTY** (**0x2005**)<br /><br /> **MDLEVEL_TYPE_GEO_CITY** (**0x2006**)<br /><br /> **MDLEVEL_TYPE_GEO_POSTALCODE** (**0x2007**)<br /><br /> **MDLEVEL_TYPE_GEO_POINT** (**0x2008**)<br /><br /> **MDLEVEL_TYPE_ORG_UNIT** (**0x1011**)<br /><br /> **MDLEVEL_TYPE_BOM_RESOURCE** (**0x1012**)<br /><br /> **MDLEVEL_TYPE_QUANTITATIVE** (**0x1013**)<br /><br /> **MDLEVEL_TYPE_ACCOUNT** (**0x1014**)<br /><br /> **MDLEVEL_TYPE_CUSTOMER** (**0x1021**)<br /><br /> **MDLEVEL_TYPE_CUSTOMER_GROUP** (**0x1022**)<br /><br /> **MDLEVEL_TYPE_CUSTOMER_HOUSEHOLD** (**0x1023**)<br /><br /> **MDLEVEL_TYPE_PRODUCT** (**0x1031**)<br /><br /> **MDLEVEL_TYPE_PRODUCT_GROUP** (**0x1032**)<br /><br /> **MDLEVEL_TYPE_SCENARIO** (**0x1015**)<br /><br /> **MDLEVEL_TYPE_UTILITY** (**0x1016**)<br /><br /> **MDLEVEL_TYPE_PERSON** (**0x1041**)<br /><br /> **MDLEVEL_TYPE_COMPANY** (**0x1042**)<br /><br /> **MDLEVEL_TYPE_CURRENCY_SOURCE** (**0x1051**)<br /><br /> **MDLEVEL_TYPE_CURRENCY_DESTINATION** (**0x1052**)<br /><br /> **MDLEVEL_TYPE_CHANNEL** (**0x1061**)<br /><br /> **MDLEVEL_TYPE_REPRESENTATIVE** (**0x1062**)<br /><br /> **MDLEVEL_TYPE_PROMOTION** (**0x1071**)|  
|**DESCRIPTION**|**DBTYPE_WSTR**|A human-readable description of the level. NULL if no description exists.|  
|**CUSTOM_ROLLUP_SETTINGS**|**DBTYPE_I4**|A bitmap that specifies the custom rollup options:<br /><br /> **MDLEVELS_CUSTOM_ROLLUP_EXPRESSION** (**0x01**) indicates an expression exists for this level. (Deprecated)<br /><br /> **MDLEVELS_CUSTOM_ROLLUP_COLUMN** (**0x02**) indicates that there is a custom rollup column for this level.<br /><br /> **MDLEVELS_SKIPPED_LEVELS** (**0x04**) indicates that there is a skipped level associated with members of this level.<br /><br /> **MDLEVELS_CUSTOM_MEMBER_PROPERTIES** (**0x08**) indicates that members of the level have custom member properties.<br /><br /> **MDLEVELS_UNARY_OPERATOR** (**0x10**) indicates that members on the level have unary operators.|  
|**LEVEL_UNIQUE_SETTINGS**|**DBTYPE_I4**|A bitmap that specifies which columns contain unique values, if the level only has members with unique names or keys. The Msmd.h file defines the following bit value constants for this bitmap:<br /><br /> **MDDIMENSIONS_MEMBER_KEY_UNIQUE** (**1**)<br /><br /> **MDDIMENSIONS_MEMBER_NAME_UNIQUE** (**2**)<br /><br /> <br /><br /> Note that the key is always unique in [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)]. The name will be unique if the setting on the attribute is **UniqueInDimension** or **UniqueInAttribute**|  
|**LEVEL_IS_VISIBLE**|**DBTYPE_BOOL**|A Boolean that indicates whether the level is visible.<br /><br /> Always returns True. If the level is not visible, it will not be included in the schema rowset.|  
|**LEVEL_ORDERING_PROPERTY**|**DBTYPE_WSTR**|The ID of the attribute that the level is sorted on.|  
|**LEVEL_DBTYPE**|**DBTYPE_I4**|The **DBTYPE** enumeration of the member key column that is used for the level attribute.<br /><br /> Null if concatenated keys are used as the member key column.|  
|**LEVEL_MASTER_UNIQUE_NAME**|**DBTYPE_WSTR**|Always returns NULL.|  
|**LEVEL_NAME_SQL_COLUMN_NAME**|**DBTYPE_WSTR**|The SQL representation of the level member names.|  
|**LEVEL_KEY_SQL_COLUMN_NAME**|**DBTYPE_WSTR**|The SQL representation of the level member key values.|  
|**LEVEL_UNIQUE_NAME_SQL_COLUMN_NAME**|**DBTYPE_WSTR**|The SQL representation of the member unique names.|  
|**LEVEL_ATTRIBUTE_HIERARCHY_NAME**|**DBTYPE_WSTR**|The name of the attribute hierarchy providing the source of the level.|  
|**LEVEL_KEY_CARDINALITY**|**DBTYPE_UI2**|The number of columns in the level key.|  
|**LEVEL_ORIGIN**|**DBTYPE_UI2**|A bit map that defines how the level was sourced:<br /><br /> **MD_ORIGIN_USER_DEFINED** identifies levels in a user defined hierarchy.<br /><br /> **MD_ORIGIN_ATTRIBUTE** identifies levels in an attribute hierarchy.<br /><br /> **MD_ORIGIN_KEY_ATTRIBUTE** identifies levels in a key attribute hierarchy.<br /><br /> **MD_ORIGIN_INTERNAL** identifies levels in attribute hierarchies that are not enabled.|  
  
 The rowset is sorted on **CATALOG_NAME**, **SCHEMA_NAME**, **CUBE_NAME**, **DIMENSION_UNIQUE_NAME**, **HIERARCHY_UNIQUE_NAME**, **LEVEL_NUMBER**.  
  
## Restriction Columns  
 The **MDSCHEMA_LEVELS** rowset can be restricted on the columns listed in the following table.  
  
|Column name|Type indicator|Restriction State|  
|-----------------|--------------------|-----------------------|  
|**CATALOG_NAME**|**DBTYPE_WSTR**|Optional.|  
|**SCHEMA_NAME**|**DBTYPE_WSTR**|Optional.|  
|**CUBE_NAME**|**DBTYPE_WSTR**|Optional.|  
|**DIMENSION_UNIQUE_NAME**|**DBTYPE_WSTR**|Optional.|  
|**HIERARCHY_UNIQUE_NAME**|**DBTYPE_WSTR**|Optional.|  
|**LEVEL_NAME**|**DBTYPE_WSTR**|Optional.|  
|**LEVEL_UNIQUE_NAME**|**DBTYPE_WSTR**|Optional.|  
|**LEVEL_ORIGIN**|**DBTYPE_UI2**|(Optional) A default restriction is in effect on **MD_USER_DEFINED** and **MD_SYSTEM_ENABLED**|  
|**CUBE_SOURCE**|**DBTYPE_UI2**|(Optional) Default restriction is a value of 1. A bitmap with one of the following valid values:<br /><br /> 1 CUBE<br /><br /> 2 DIMENSION|  
|**LEVEL_VISIBILITY**|**DBTYPE_UI2**|(Optional) Default restriction is a value of 1. A bitmap with one of the following values:<br /><br /> 1 Visible<br /><br /> 2 Not visible|  
  
## See Also  
 [OLE DB for OLAP Schema Rowsets](../../../analysis-services/schema-rowsets/ole-db-olap/ole-db-for-olap-schema-rowsets.md)  
  
  