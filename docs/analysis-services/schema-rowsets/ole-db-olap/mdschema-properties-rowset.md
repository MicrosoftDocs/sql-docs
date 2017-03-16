---
title: "MDSCHEMA_PROPERTIES Rowset | Microsoft Docs"
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
  - "MDSCHEMA_PROPERTIES"
apitype: "NA"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "MDSCHEMA_PROPERTIES rowset"
ms.assetid: 95c480f7-c525-44ba-a59b-cd36f5855a4f
caps.latest.revision: 31
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# MDSCHEMA_PROPERTIES Rowset
  Describes the properties of members within a database.  
  
## Rowset Columns  
 The **MDSCHEMA_PROPERTIES** rowset contains the following columns.  
  
|Column name|Type indicator|Length|Description|  
|-----------------|--------------------|------------|-----------------|  
|**CATALOG_NAME**|**DBTYPE_WSTR**||The name of the database.|  
|**SCHEMA_NAME**|**DBTYPE_WSTR**||The name of the schema to which this property belongs. **NULL** if the provider does not support schemas.|  
|**CUBE_NAME**|**DBTYPE_WSTR**||The name of the cube.|  
|**DIMENSION_UNIQUE_NAME**|**DBTYPE_WSTR**||The unique name of the dimension. For providers that generate unique names by qualification, each component of this name is delimited.|  
|**HIERARCHY_UNIQUE_NAME**|**DBTYPE_WSTR**||The unique name of the hierarchy. For providers that generate unique names by qualification, each component of this name is delimited.|  
|**LEVEL_UNIQUE_NAME**|**DBTYPE_WSTR**||The unique name of the level to which this property belongs. If the provider does not support named levels, it should return the **DIMENSION_UNIQUE_NAME** value for this field. For providers that generate unique names by qualification, each component of this name is delimited.|  
|**MEMBER_UNIQUE_NAME**|**DBTYPE_WSTR**||The unique name of the member to which the property belongs. Used for data stores that do not support named levels or have properties on a member-by-member basis. If the property applies to all members in a level, this column is **NULL**. For providers that generate unique names by qualification, each component of this name is delimited.|  
|**PROPERTY_TYPE**|**DBTYPE_I2**||A bitmap that specifies the type of the property:<br /><br /> **MDPROP_MEMBER** (**1**) identifies a property of a member. This property can be used in the DIMENSION PROPERTIES clause of the SELECT statement.<br /><br /> **MDPROP_CELL** (**2**) identifies a property of a cell. This property can be used in the CELL PROPERTIES clause that occurs at the end of the SELECT statement.<br /><br /> **MDPROP_SYSTEM** (**4**) identifies an internal property.<br /><br /> **MDPROP_BLOB** (**8**) identifies a property which contains a binary large object (blob).|  
|**PROPERTY_NAME**|**DBTYPE_WSTR**||The name of the property. If the key for the property is the same as the name for the property, **PROPERTY_NAME** will be blank.|  
|**PROPERTY_CAPTION**|**DBTYPE_WSTR**||A label or caption associated with the property, used primarily for display purposes. Returns **PROPERTY_NAME** if a caption does not exist.|  
|**DATA_TYPE**|**DBTYPE_UI2**||The data type of the property.|  
|**CHARACTER_MAXIMUM_LENGTH**|**DBTYPE_UI4**||The maximum possible length of the property, if it is a character, binary, or bit type.<br /><br /> Zero indicates there is no defined maximum length.<br /><br /> Returns **NULL** for all other data types.|  
|**CHARACTER_OCTET_LENGTH**|**DBTYPE_UI4**||The maximum possible length (in bytes) of the property, if it is a character or binary type.<br /><br /> Zero indicates there is no defined maximum length.<br /><br /> Returns **NULL** for all other data types.|  
|**NUMERIC_PRECISION**|**DBTYPE_UI2**||The maximum precision of the property, if it is a numeric data type.<br /><br /> Returns **NULL** for all other data types.|  
|**NUMERIC_SCALE**|**DBTYPE_I2**||The number of digits to the right of the decimal point, if it is a **DBTYPE_NUMERIC** or **DBTYPE_DECIMAL** type.<br /><br /> Returns **NULL** for all other data types.|  
|**DESCRIPTION**|**DBTYPE_WSTR**||A human readable description of the property. **NULL** if no description exists.|  
|**PROPERTY_CONTENT_TYPE**|**DBTYPE_I2**||The type of the property. Can be one of the following enumerations:<br /><br /> **MD_PROPTYPE_REGULAR** (**0x00**)<br /><br /> **MD_PROPTYPE_ID** (**0x01**)<br /><br /> **MD_PROPTYPE_RELATION_TO_PARENT** (**0x02**)<br /><br /> **MD_PROPTYPE_ROLLUP_OPERATOR** (**0x03**)<br /><br /> **MD_PROPTYPE_ORG_TITLE** (**0x11**)<br /><br /> **MD_PROPTYPE_CAPTION** (**0x21**)<br /><br /> **MD_PROPTYPE_CAPTION_SHORT** (**0x22**)<br /><br /> **MD_PROPTYPE_CAPTION_DESCRIPTION** (**0x23**)<br /><br /> **MD_PROPTYPE_CAPTION_ABREVIATION** (**0x24**)<br /><br /> **MD_PROPTYPE_WEB_URL** (**0x31**)<br /><br /> **MD_PROPTYPE_WEB_HTML** (**0x32**)<br /><br /> **MD_PROPTYPE_WEB_XML_OR_XSL** (**0x33**)<br /><br /> **MD_PROPTYPE_WEB_MAIL_ALIAS** (**0x34**)<br /><br /> **MD_PROPTYPE_ADDRESS** (**0x41**)<br /><br /> **MD_PROPTYPE_ADDRESS_STREET** (**0x42**)<br /><br /> **MD_PROPTYPE_ADDRESS_HOUSE** (**0x43**)<br /><br /> **MD_PROPTYPE_ADDRESS_CITY** (**0x44**)<br /><br /> **MD_PROPTYPE_ADDRESS_STATE_OR_PROVINCE** (**0x45**)<br /><br /> **MD_PROPTYPE_ADDRESS_ZIP** (**0x46**)<br /><br /> **MD_PROPTYPE_ADDRESS_QUARTER** (**0x47**)<br /><br /> **MD_PROPTYPE_ADDRESS_COUNTRY** (**0x48**)<br /><br /> **MD_PROPTYPE_ADDRESS_BUILDING** (**0x49**)<br /><br /> **MD_PROPTYPE_ADDRESS_ROOM** (**0x4A**)<br /><br /> **MD_PROPTYPE_ADDRESS_FLOOR** (**0x4B**)<br /><br /> **MD_PROPTYPE_ADDRESS_FAX** (**0x4C**)<br /><br /> **MD_PROPTYPE_ADDRESS_PHONE** (**0x4D**)<br /><br /> **MD_PROPTYPE_GEO_CENTROID_X** (**0x61**)<br /><br /> **MD_PROPTYPE_GEO_CENTROID_Y** (**0x62**)<br /><br /> **MD_PROPTYPE_GEO_CENTROID_Z** (**0x63**)<br /><br /> **MD_PROPTYPE_GEO_BOUNDARY_TOP** (**0x64**)<br /><br /> **MD_PROPTYPE_GEO_BOUNDARY_LEFT** (**0x65**)<br /><br /> **MD_PROPTYPE_GEO_BOUNDARY_BOTTOM** (**0x66**)<br /><br /> **MD_PROPTYPE_GEO_BOUNDARY_RIGHT** (**0x67**)<br /><br /> **MD_PROPTYPE_GEO_BOUNDARY_FRONT** (**0x68**)<br /><br /> **MD_PROPTYPE_GEO_BOUNDARY_REAR** (**0x69**)<br /><br /> **MD_PROPTYPE_GEO_BOUNDARY_POLYGON** (**0x6A**)<br /><br /> **MD_PROPTYPE_PHYSICAL_SIZE** (**0x71**)<br /><br /> **MD_PROPTYPE_PHYSICAL_COLOR** (**0x72**)<br /><br /> **MD_PROPTYPE_PHYSICAL_WEIGHT** (**0x73**)<br /><br /> **MD_PROPTYPE_PHYSICAL_HEIGHT** (**0x74**)<br /><br /> **MD_PROPTYPE_PHYSICAL_WIDTH** (**0x75**)<br /><br /> **MD_PROPTYPE_PHYSICAL_DEPTH** (**0x76**)<br /><br /> **MD_PROPTYPE_PHYSICAL_VOLUME** (**0x77**)<br /><br /> **MD_PROPTYPE_PHYSICAL_DENSITY** (**0x78**)<br /><br /> **MD_PROPTYPE_PERSON_FULL_NAME** (**0x82**)<br /><br /> **MD_PROPTYPE_PERSON_FIRST_NAME** (**0x83**)<br /><br /> **MD_PROPTYPE_PERSON_LAST_NAME** (**0x84**)<br /><br /> **MD_PROPTYPE_PERSON_MIDDLE_NAME** (**0x85**)<br /><br /> **MD_PROPTYPE_PERSON_DEMOGRAPHIC** (**0x86**)<br /><br /> **MD_PROPTYPE_PERSON_CONTACT** (**0x87**)<br /><br /> **MD_PROPTYPE_QTY_RANGE_LOW** (**0x91**)<br /><br /> **MD_PROPTYPE_QTY_RANGE_HIGH** (**0x92**)<br /><br /> **MD_PROPTYPE_FORMATTING_COLOR** (**0xA1**)<br /><br /> **MD_PROPTYPE_FORMATTING_ORDER** (**0xA2**)<br /><br /> **MD_PROPTYPE_FORMATTING_FONT** (**0xA3**)<br /><br /> **MD_PROPTYPE_FORMATTING_FONT_EFFECTS** (**0xA4**)<br /><br /> **MD_PROPTYPE_FORMATTING_FONT_SIZE** (**0xA5**)<br /><br /> **MD_PROPTYPE_FORMATTING_SUB_TOTAL** (**0xA6**)<br /><br /> **MD_PROPTYPE_DATE** (**0xB1**)<br /><br /> **MD_PROPTYPE_DATE_START** (**0xB2**)<br /><br /> **MD_PROPTYPE_DATE_ENDED** (**0xB3**)<br /><br /> **MD_PROPTYPE_DATE_CANCELED** (**0xB4**)<br /><br /> **MD_PROPTYPE_DATE_MODIFIED** (**0xB5**)<br /><br /> **MD_PROPTYPE_DATE_DURATION** (**0xB6**)<br /><br /> **MD_PROPTYPE_VERSION** (**0xC1**)|  
|**SQL_COLUMN_NAME**|**DBTYPE_WSTR**||The name of the property used in SQL queries from the cube dimension or database dDimension.|  
|**LANGUAGE**|**DBTYPE_UI2**||The translation expressed as an **LCID**. Only valid for property translations.|  
|**PROPERTY_ORIGIN**|**DBTYPE_UI2**||Identifies the type of hierarchy that the property applies to:<br /><br /> **MD_USER_DEFINED** (**1**) indicates the property is on a user defined hierarchy<br /><br /> **MD_SYSTEM_ENABLED** (**2**) indicates the property is on an attribute hierarchy<br /><br /> **MD_SYSTEM_DISABLED** (**4**) indicates the property is on an attribute hierarchy that is not enabled.|  
|**PROPERTY_ATTRIBUTE_HIERARCHY_NAME**|**DBTYPE_WSTR**||The name of the attribute hierarchy sourcing this property.|  
|**PROPERTY_CARDINALITY**|**DBTYPE_WSTR**||The cardinality of the property. Possible values include the following strings:<br /><br /> **ONE**<br /><br /> **MANY**|  
|**MIME_TYPE**|**DBTYPE_WSTR**||The mime type for binary large objects (BLOBs).|  
|**PROPERTY_IS_VISIBLE**|**DBTYPE_BOOL**||A Boolean that indicates whether the property is visible.<br /><br /> **TRUE** if the property is visible; otherwise, **FALSE**.|  
  
 This schema rowset is not sorted.  
  
## Restriction Columns  
 The **MDSCHEMA_PROPERTIES** rowset can be restricted on the columns listed in the following table.  
  
|Column name|Type indicator|Restriction State|  
|-----------------|--------------------|-----------------------|  
|**CATALOG_NAME**|**DBTYPE_WSTR**|Mandatory|  
|**SCHEMA_NAME**|**DBTYPE_WSTR**|Optional|  
|**CUBE_NAME**|**DBTYPE_WSTR**|Optional|  
|**DIMENSION_UNIQUE_NAME**|**DBTYPE_WSTR**|Optional|  
|**HIERARCHY_UNIQUE_NAME**|**DBTYPE_WSTR**|Optional|  
|**LEVEL_UNIQUE_NAME**|**DBTYPE_WSTR**|Optional|  
|**MEMBER_UNIQUE_NAME**|**DBTYPE_WSTR**|Optional|  
|**PROPERTY_TYPE**|**DBTYPE_I2**|Optional|  
|**PROPERTY_NAME**|**DBTYPE_WSTR**|Optional|  
|**PROPERTY_CONTENT_TYPE**|**DBTYPE_I2**|(Optional) A default restriction is in place on **MDPROP_MEMBER** OR **MDPROP_CELL**.|  
|**PROPERTY_ORIGIN**|**DBTYPE_UI2**|(Optional) A default restriction is in place on **MD_USER_DEFINED** OR **MD_SYSTEM_ENABLED**.|  
|**CUBE_SOURCE**|**DBTYPE_UI2**|(Optional) Default restriction is a value of 1.  A bitmap with one of the following valid values:<br /><br /> 1 CUBE<br /><br /> 2 DIMENSION|  
|**PROPERTY_VISIBILITY**|**DBTYPE_UI2**|(Optional) Default restriction is a value of 1. A bitmap with one of the following valid values:<br /><br /> 1 Visible<br /><br /> 2 Not visible|  
  
## See Also  
 [OLE DB for OLAP Schema Rowsets](../../../analysis-services/schema-rowsets/ole-db-olap/ole-db-for-olap-schema-rowsets.md)  
  
  