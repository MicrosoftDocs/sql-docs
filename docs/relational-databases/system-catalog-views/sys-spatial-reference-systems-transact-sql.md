---
title: "sys.spatial_reference_systems (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "spatial_reference_systems_TSQL"
  - "sys.spatial_reference_systems_TSQL"
  - "sys.spatial_reference_systems"
  - "spatial_reference_systems"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.spatial_reference_systems catalog view"
  - "spatial_reference_systems"
ms.assetid: 3c9bc120-67c3-463f-9e24-29fd623f25a0
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.spatial_reference_systems (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Lists the spatial reference systems (SRIDs) supported by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  

  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|spatial_reference_id|**int**|The SRID supported by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|authority_name|**nvarchar(128)**|The authority of the SRID.|  
|authorized_spatial_reference_id|**int**|The SRID given by the authority named in **authority_name**.|  
|well_known_text|**nvarchar(4000)**|The WKT representation of the SRID.|  
|unit_of_measure|**nvarchar(128)**|The name of the unit of measure.|  
|unit_conversion_factor|**float**|The length of the unit of measure in meters.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)]  
  
  
