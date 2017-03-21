---
title: "DBSCHEMA_CATALOGS Rowset | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "DBSCHEMA_CATALOGS"
apitype: "NA"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "DBSCHEMA_CATALOGS rowset"
ms.assetid: f02dc75d-5442-4eea-b33a-567dc816be7a
caps.latest.revision: 31
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# DBSCHEMA_CATALOGS Rowset
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
  
  