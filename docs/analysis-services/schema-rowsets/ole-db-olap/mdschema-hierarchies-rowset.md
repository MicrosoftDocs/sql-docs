---
title: "MDSCHEMA_HIERARCHIES Rowset | Microsoft Docs"
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
  - "MDSCHEMA_HIERARCHIES"
apitype: "NA"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "MDSCHEMA_HIERARCHIES rowset"
ms.assetid: 2e5b2a81-366e-4d5b-af1e-1d372bf596d9
caps.latest.revision: 34
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# MDSCHEMA_HIERARCHIES Rowset
  Describes each hierarchy within a particular dimension.  
  
## Rowset Columns  
 The **MDSCHEMA_HIERARCHIES** rowset contains the following columns.  
  
|Column name|Type indicator|Description|  
|-----------------|--------------------|-----------------|  
|**CATALOG_NAME**|**DBTYPE_WSTR**|The name of the catalog to which this hierarchy belongs. **NULL** if the provider does not support catalogs.|  
|**SCHEMA_NAME**|**DBTYPE_WSTR**|Not supported|  
|**CUBE_NAME**|**DBTYPE_WSTR**|(Required) The name of the cube to which this hierarchy belongs.|  
|**DIMENSION_UNIQUE_NAME**|**DBTYPE_WSTR**|The unique name of the dimension to which this hierarchy belongs. For providers that generate unique names by qualification, each component of this name is delimited.|  
|**HIERARCHY_NAME**|**DBTYPE_WSTR**|The name of the hierarchy. Blank if there is only a single hierarchy in the dimension. This will always have a value in [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)].|  
|**HIERARCHY_UNIQUE_NAME**|**DBTYPE_WSTR**|The unique name of the hierarchy.|  
|**HIERARCHY_GUID**|**DBTYPE_GUID**|Not supported|  
|**HIERARCHY_CAPTION**|**DBTYPE_WSTR**|A label or a caption associated with the hierarchy. Used primarily for display purposes. If a caption does not exist, **HIERARCHY_NAME** is returned. If the dimension either does not contain a hierarchy or has just one hierarchy, this column will contain the name of the dimension.|  
|**DIMENSION_TYPE**|**DBTYPE_I2**|The type of the dimension. Valid values include the following values:<br /><br /> **MD_DIMTYPE_UNKNOWN** (**0**)<br /><br /> **MD_DIMTYPE_TIME** (**1**)<br /><br /> **MD_DIMTYPE_MEASURE** (**2**)<br /><br /> **MD_DIMTYPE_OTHER** (**3**)<br /><br /> **MD_DIMTYPE_QUANTITATIVE** (**5**)<br /><br /> **MD_DIMTYPE_ACCOUNTS** (**6**)<br /><br /> **MD_DIMTYPE_CUSTOMERS** (**7**)<br /><br /> **MD_DIMTYPE_PRODUCTS** (**8**)<br /><br /> **MD_DIMTYPE_SCENARIO** (**9**)<br /><br /> **MD_DIMTYPE_UTILIY** (**10**)<br /><br /> **MD_DIMTYPE_CURRENCY** (**11**)<br /><br /> **MD_DIMTYPE_RATES** (**12**)<br /><br /> **MD_DIMTYPE_CHANNEL** (**13**)<br /><br /> **MD_DIMTYPE_PROMOTION** (**14**)<br /><br /> **MD_DIMTYPE_ORGANIZATION** (**15**)<br /><br /> **MD_DIMTYPE_BILL_OF_MATERIALS** (**16**)<br /><br /> **MD_DIMTYPE_GEOGRAPHY** (**17**)|  
|**HIERARCHY_CARDINALITY**|**DBTYPE_UI4**|The number of members in the hierarchy.|  
|**DEFAULT_MEMBER**|**DBTYPE_WSTR**|The default member for this hierarchy. This is a unique name. Every hierarchy must have a default member.|  
|**ALL_MEMBER**|**DBTYPE_WSTR**|The member at the highest level of the rollup.|  
|**DESCRIPTION**|**DBTYPE_WSTR**|A human-readable description of the hierarchy. **NULL** if no description exists.|  
|**STRUCTURE**|**DBTYPE_I2**|The structure of the hierarchy. Valid values include the following values:<br /><br /> **MD_STRUCTURE_FULLYBALANCED** (**0**)<br /><br /> **MD_STRUCTURE_RAGGEDBALANCED** (**1**)<br /><br /> **MD_STRUCTURE_UNBALANCED** (**2**)<br /><br /> **MD_STRUCTURE_NETWORK** (**3**)|  
|**IS_VIRTUAL**|**DBTYPE_BOOL**|Always returns **False**.|  
|**IS_READWRITE**|**DBTYPE_BOOL**|A Boolean that indicates whether the Write Back to dimension column is enabled.<br /><br /> Returns **TRUE** if the **Write Back to dimension** column that represents this hierarchy is enabled.|  
|**DIMENSION_UNIQUE_SETTINGS**|**DBTYPE_I4**|Always returns **MDDIMENSIONS_MEMBER_KEY_UNIQUE** (**1**).|  
|**DIMENSION_MASTER_UNIQUE_NAME**|**DBTYPE_WSTR**|Always returns **NULL**.|  
|**DIMENSION_IS_VISIBLE**|**DBTYPE_BOOL**|Always returns **true**. If the dimension is not visible, it will not appear in the schema rowset.|  
|**HIERARCHY_ORDINAL**|**DBTYPE_UI4**|The ordinal number of the hierarchy across all hierarchies of the cube.|  
|**DIMENSION_IS_SHARED**|**DBTYPE_BOOL**|Always returns **TRUE**.|  
|**HIERARCHY_IS_VISIBLE**|**DBTYPE_BOOL**|A Boolean that indicates whether the hieararchy is visible.<br /><br /> Returns **TRUE** if the hierarchy is visible; otherwise, **FALSE**.|  
|**HIERARCHY_ORIGIN**|**DBTYPE_UI2**|A bit mask that determines the source of the hierarchy:<br /><br /> **MD_USER_DEFINED** identifies user defined hierarchies, and has a value of **0x0000001**.<br /><br /> **MD_SYSTEM_ENABLED** identifies attribute hierarchies, and has a value of **0x0000002**.<br /><br /> **MD_SYSTEM_INTERNAL** identifies attributes with no attribute hierarchies, and has a value of **0x0000004**.<br /><br /> <br /><br /> Note that a parent/child attribute hierarchy is both **MD_USER_DEFINED** and **MD_SYSTEM_ENABLED**.|  
|**HIERARCHY_DISPLAY_FOLDER**|**DBTYPE_WSTR**|The path to be used when displaying the hierarchy in the user interface. Folder names will be separated by a semicolon (;). Nested folders are indicated by a backslash (\\).|  
|**INSTANCE_SELECTION**|**DBTYPE_UI2**|A hint to the client application on how to show the hierarchy. Valid values include the following values:<br /><br /> **MD_INSTANCE_SELECTION_NONE**<br /><br /> **MD_INSTANCE_SELECTION_DROPDOWN**<br /><br /> **MD_INSTANCE_SELECTION_LIST**<br /><br /> **MD_INSTANCE_SELECTION_FILTEREDLIST**<br /><br /> **MD_INSTANCE_SELECTION_MANDATORYFILTER**|  
|**GROUPING_BEHAVIOR**|**DBTYPE_I2**|An enumeration that specifies the expected grouping behavior of clients for this hierarchy. Possible values are the following:<br /><br /> **EncourageGrouping**  (1)<br /><br /> **DiscourageGrouping**  (2)|  
|**STRUCTURE_TYPE**|**DBTYPE_WSTR**|Indicates the type of hierarchy. Valid values include the following values:<br /><br /> **Natural**<br /><br /> **Unnatural**<br /><br /> **Unknown**|  
  
 The rowset is sorted on **CATALOG_NAME**, **SCHEMA_NAME**, **CUBE_NAME**, **DIMENSION_UNIQUE_NAME**, **HIERARCHY_NAME**.  
  
## Restriction Columns  
 The **MDSCHEMA_HIERARCHIES** rowset can be restricted on the columns listed in the following table.  
  
|Column name|Type indicator|Restriction State|  
|-----------------|--------------------|-----------------------|  
|**CATALOG_NAME**|**DBTYPE_WSTR**|Optional.|  
|**SCHEMA_NAME**|**DBTYPE_WSTR**|Optional.|  
|**CUBE_NAME**|**DBTYPE_WSTR**|Optional.|  
|**DIMENSION_UNIQUE_NAME**|**DBTYPE_WSTR**|Optional.|  
|**HIERARCHY_NAME**|**DBTYPE_WSTR**|Optional.|  
|**HIERARCHY_UNIQUE_NAME**|**DBTYPE_WSTR**|Optional.|  
|**HIERARCHY_ORIGIN**|**DBTYPE_UI2**|(Optional) A default restriction is in effect on MD_USER_DEFINED and MD_SYSTEM_ENABLED.|  
|**CUBE_SOURCE**|**DBTYPE_UI2**|(Optional) Default restriction is a value of 1. A bitmap with one of the following valid values:<br /><br /> 1 CUBE<br /><br /> 2 DIMENSION|  
|**HIERARCHY_VISIBILITY**|**DBTYPE_UI2**|(Optional) Default restriction is a value of 1. A bitmap with one of the following valid values:<br /><br /> 1 Visible<br /><br /> 2 Not visible|  
  
## See Also  
 [OLE DB for OLAP Schema Rowsets](../../../analysis-services/schema-rowsets/ole-db-olap/ole-db-for-olap-schema-rowsets.md)  
  
  