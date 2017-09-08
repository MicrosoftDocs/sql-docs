---
title: "dbo.slo_service_objectives (Azure SQL Database) | Microsoft Docs"
ms.custom: 
  - "MSDN content"
  - "MSDN - SQL DB"
ms.date: "03/04/2017"
ms.prod: 
ms.reviewer: ""
ms.service: "sql-database"
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
applies_to: 
  - "Azure SQL Database"
f1_keywords: 
  - "dbo.slo_service_objectives"
  - "dbo.slo_service_objectives_TSQL"
  - "slo_service_objectives"
  - "slo_service_objectives_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "dbo.slo_service_objectives"
  - "slo_service_objectives"
ms.assetid: d5dd7ed9-440a-4432-ad45-644e4e72318f
caps.latest.revision: 10
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# dbo.slo_service_objectives (Azure SQL Database)
[!INCLUDE[tsql-appliesto-ss2016-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2016-asdb-xxxx-xxx-md.md)]

    
> [!IMPORTANT]  
>  This feature is in a preview state, and has been deprecated in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] V12. Do not take a dependency on the specific implementation of this feature because the feature might be changed or removed in a future release.  
  
 Returns Service Level Objective (SLO) information on the current server.  
  
||  
|-|  
|**Applies to**: [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] V11.|  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|objective_id|**uniqueidentifier**|ID of the service level objective.|  
|name|**sysname**|Name of the service level objective.|  
|description|**nvarchar**|Description of the service level objective.|  
|create_date|**datetimeoffset(7)**|creation date of the service level object on the server.|  
|is_system|**bit**|1 = system service level objective|  
|is_default|**bit**|1 = service level objective is the default SLO.|  
|state|**tinyint**|1 = service level objective is enable.<br /><br /> 2 = service level objective is disabled.|  
|state_desc|**nvarchar**|Description of the service level objective.|  
|metadata_version|**decimal**|Version of the service level objective.|  
  
## Permissions  
 This view is available to all user roles with permissions to connect to the virtual **master** database.  
  
## See Also  
 [Managing Premium Databases](http://go.microsoft.com/fwlink/?LinkID=311927)  
  
  