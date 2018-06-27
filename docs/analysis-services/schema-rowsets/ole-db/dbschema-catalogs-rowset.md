---
title: "DBSCHEMA_CATALOGS Rowset | Microsoft Docs"
ms.date: 05/03/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: schema-rowsets
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# DBSCHEMA_CATALOGS Rowset
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
  Identifies the physical attributes associated with catalogs accessible from the database management system (DBMS).  
  
## Rowset Columns  
 The **DBSCHEMA_CATALOGS** rowset contains the following columns.  
  
|Column name|Type indicator|Length|Description|  
|-----------------|--------------------|------------|-----------------|  
|**CATALOG_NAME**|**DBTYPE_WSTR**|255|The catalog name. Cannot be null.|  
|**DESCRIPTION**|**DBTYPE_WSTR**||A human-readable description of the table.|  
|**ROLES**|**DBTYPE_WSTR**||A comma delimited list of roles to which the current user belongs.<br /><br /> An asterisk (\*) is included as a role if the current user is a server or database administrator.<br /><br /> **Username** is appended to **ROLES** if one of the roles uses dynamic security.|  
|**DATE_MODIFIED**|**DBTYPE_DBTIMESTAMP**||The date that the catalog was last modified.|  
  
 The rowset is sorted on **CATALOG_NAME**.  
  
## Restriction Columns  
 The **DBSCHEMA_CATALOGS** rowset can be restricted on the columns listed in the following table.  
  
|Column name|Type indicator|Restriction State|  
|-----------------|--------------------|-----------------------|  
|**CATALOG_NAME**|**DBTYPE_WSTR**|Optional|  
  
## See Also  
 [OLE DB Schema Rowsets](../../../analysis-services/schema-rowsets/ole-db/ole-db-schema-rowsets.md)  
  
  
