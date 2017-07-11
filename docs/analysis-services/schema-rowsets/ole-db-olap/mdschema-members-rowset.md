---
title: "MDSCHEMA_MEMBERS Rowset | Microsoft Docs"
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
  - "MDSCHEMA_MEMBERS"
apitype: "NA"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "MDSCHEMA_MEMBERS rowset"
ms.assetid: 0b1aada0-67f8-4ef6-81b2-0100b65e0c2f
caps.latest.revision: 36
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# MDSCHEMA_MEMBERS Rowset
  Describes the members within a database.  
  
## Rowset Columns  
 The **MDSCHEMA_MEMBERS** rowset contains the following columns.  
  
|Column name|Type indicator|Length|Description|  
|-----------------|--------------------|------------|-----------------|  
|**CATALOG_NAME**|**DBTYPE_WSTR**||The name of the database to which this member belongs.|  
|**SCHEMA_NAME**|**DBTYPE_WSTR**||The name of the schema to which this member belongs.|  
|**CUBE_NAME**|**DBTYPE_WSTR**||The name of the cube to which this member belongs.|  
|**DIMENSION_UNIQUE_NAME**|**DBTYPE_WSTR**||The unique name of the dimension to which this member belongs.|  
|**HIERARCHY_UNIQUE_NAME**|**DBTYPE_WSTR**||The unique name of the hierarchy to which this member belongs.|  
|**LEVEL_UNIQUE_NAME**|**DBTYPE_WSTR**||The unique name of the level to which this member belongs.|  
|**LEVEL_NUMBER**|**DBTYPE_UI4**||The distance of the member from the root of the hierarchy. The root level is zero (0).|  
|**MEMBER_ORDINAL**|**DBTYPE_UI4**||(Deprecated) Always returns **0**.|  
|**MEMBER_NAME**|**DBTYPE_WSTR**||The name of the member.|  
|**MEMBER_UNIQUE_NAME**|**DBTYPE_WSTR**||The unique name of the member.|  
|**MEMBER_TYPE**|**DBTYPE_I4**||The type of the member:<br /><br /> **MDMEMBER_TYPE_UNKNOWN** (**0**)<br /><br /> **MDMEMBER_TYPE_REGULAR** (**1**)<br /><br /> **MDMEMBER_TYPE_ALL** (**2**)<br /><br /> **MDMEMBER_TYPE_MEASURE** (**3**)<br /><br /> **MDMEMBER_TYPE_FORMULA** (**4**)<br /><br /> <br /><br /> Note that <br />                    **MDMEMBER_TYPE_FORMULA**takes precedence over **MDMEMBER_TYPE_MEASURE**. For example, if there is a formula (calculated) member on the Measures dimension, it is listed as **MDMEMBER_TYPE_FORMULA**.|  
|**MEMBER_GUID**|**DBTYPE_GUID**||The GUID of the member. **NULL** if no GUID exists.|  
|**MEMBER_CAPTION**|**DBTYPE_WSTR**||A label or caption associated with the member. Used primarily for display purposes. If a caption does not exist, **MEMBER_NAME** is returned.|  
|**CHILDREN_CARDINALITY**|**DBTYPE_UI4**||The number of children that the member has. This can be an estimate, so consumers should not rely on this to be the exact count. Providers should return the best estimate possible.|  
|**PARENT_LEVEL**|**DBTYPE_UI4**||The distance of the member's parent from the root level of the hierarchy. The root level is zero (0).|  
|**PARENT_UNIQUE_NAME**|**DBTYPE_WSTR**||The unique name of the member's parent. **NULL** is returned for any members at the root level.|  
|**PARENT_COUNT**|**DBTYPE_UI4**||The number of parents that this member has.|  
|**DESCRIPTION**|**DBTYPE_WSTR**||This column always returns a **NULL** value.<br /><br /> This column exists for backwards compatibility|  
|**EXPRESSION**|**DBTYPE_WSTR**||The expression for calculations, if the member is of type **MDMEMBER_TYPE_FORMULA**.|  
|**MEMBER_KEY**|**DBTYPE_WSTR**||The value of the member's key column. Returns **NULL** if the member has a composite key.|  
|**IS_PLACEHOLDERMEMBER**|**DBTYPE_BOOL**||A Boolean that indicates whether a member is a placeholder member for an empty position in a dimension hierarchy.<br /><br /> It is valid only if the **MDX Compatibility** property has been set to 2.|  
|**IS_DATAMEMBER**|**DBTYPE_BOOL**||A Boolean that indicates whether the member is a data member.<br /><br /> Returns True if the member is a data member.|  
|**SCOPE**|**DBTYPE_I4**||The scope of the member. The member can be a session calculated member or global calculated member. The column returns **NULL** for non-calculated members. This column can have one of the following values:<br /><br /> MDMEMBER_SCOPE_GLOBAL=1<br /><br /> MDMEMBER_SCOPE_SESSION=2|  
|**Zero or more additional columns**|**DBTYPE_UI2**||No properties are returned if the members could be returned from multiple levels. For example, if the Tree operator is **PARENT** and **SELF** for a non-parent child hierarchy, no member properties are returned.<br /><br /> This applies to ragged hierarchies where tree operators could return members from different levels (for example, if the prior level contains holes and parent on members is requested).|  
  
 The rowset is sorted on **CATALOG_NAME**, **SCHEMA_NAME**, **CUBE_NAME**, **DIMENSION_UNIQUE_NAME**, **HIERARCHY_UNIQUE_NAME**, **LEVEL_UNIQUE_NAME**, **LEVEL_NUMBER**, **MEMBER_ORDINAL**.  
  
## Restriction Columns  
 The **MDSCHEMA_MEMBERS** rowset can be restricted on the columns listed in the following table.  
  
|Column name|Type indicator|Restriction State|  
|-----------------|--------------------|-----------------------|  
|**CATALOG_NAME**|**DBTYPE_WSTR**|Optional.|  
|**SCHEMA_NAME**|**DBTYPE_WSTR**|Optional.|  
|**CUBE_NAME**|**DBTYPE_WSTR**|Optional.|  
|**DIMENSION_UNIQUE_NAME**|**DBTYPE_WSTR**|Optional.|  
|**HIERARCHY_UNIQUE_NAME**|**DBTYPE_WSTR**|Optional.|  
|**LEVEL_UNIQUE_NAME**|**DBTYPE_WSTR**|Optional.|  
|**LEVEL_NUMBER**|**DBTYPE_UI4**|Optional.|  
|**MEMBER_NAME**|**DBTYPE_WSTR**|Optional.|  
|**MEMBER_UNIQUE_NAME**|**DBTYPE_WSTR**|Optional.|  
|**MEMBER_CAPTION**|**DBTYPE_WSTR**|Optional.|  
|**MEMBER_TYPE**|**DBTYPE_I4**|Optional.|  
|**TREE_OP**|**DBTYPE_I4**|(Optional) Only applies to a single member:<br /><br /> **MDTREEOP_ANCESTORS** (**0x20**) returns all of the ancestors.<br /><br /> **MDTREEOP_CHILDREN** (**0x01**) returns only the immediate children.<br /><br /> **MDTREEOP_SIBLINGS** (**0x02**) returns members on the same level.<br /><br /> **MDTREEOP_PARENT** (**0x04**) returns only the immediate parent.<br /><br /> **MDTREEOP_SELF** (**0x08**) returns itself in the list of returned rows.<br /><br /> **MDTREEOP_DESCENDANTS** (**0x10**) returns all of the descendants.|  
|**CUBE_SOURCE**|**DBTYPE_UI2**|(Optional) Default restriction is a value of 1. A bitmap with one of the following valid values:<br /><br /> 1 CUBE<br /><br /> 2 DIMENSION|  
  
## See Also  
 [OLE DB for OLAP Schema Rowsets](../../../analysis-services/schema-rowsets/ole-db-olap/ole-db-for-olap-schema-rowsets.md)  
  
  