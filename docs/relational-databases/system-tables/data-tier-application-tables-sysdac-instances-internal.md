---
title: "sysdac_instances_internal (Transact-SQL)"
description: Data-tier Application Tables - sysdac_instances_internal
author: VanMSFT
ms.author: vanto
ms.date: "03/03/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sysdac_instances_internal_TSQL"
  - "sysdac_instances_internal"
helpviewer_keywords:
  - "sysdac_instances_internal"
dev_langs:
  - "TSQL"
ms.assetid: d2d52cc4-3463-431a-b779-6fbfdeee1dfc
---
# Data-tier Application Tables - sysdac_instances_internal
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Displays one row for each data-tier application (DAC) instance deployed to an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)]. This table is stored in the dbo schema in the msdb database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|instance_id|**uniqueidentifier**|Identifier of the DAC instance.|  
|instance_name|**sysname**|Name of the DAC instance specified when the instance was deployed.|  
|type_name|**sysname**|Name of the DAC specified when the DAC package was created.|  
|type_version|**nvarchar(64)**|Version of the DAC specified when the DAC package was created.|  
|description|**nvarchar(4000)**|A description of the DAC written when the DAC package was created.|  
|type_stream|**varbinary(max)**|A bit stream that contains the encoded representation of the logical objects, such as tables and views, contained in the DAC.|  
|date_created|**datetime**|Date and time the DAC instance was created.|  
|created_by|**sysname**|Login that created the DAC instance|  
  
## Remarks  
 Read-only access to this view is available to all users with permissions to connect to the master database.  
  
## Permissions  
 Requires membership in the sysadmin fixed server role.  
  
## See Also  
 [Data-tier Applications](../../relational-databases/data-tier-applications/data-tier-applications.md)   
 [dbo.sysdac_instances &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/data-tier-application-views-dbo-sysdac-instances.md)  
  
  
