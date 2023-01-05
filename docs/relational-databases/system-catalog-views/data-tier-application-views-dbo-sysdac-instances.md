---
title: "dbo.sysdac_instances (Transact-SQL)"
description: Data-tier Application Views - dbo.sysdac_instances
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/04/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dbo.sysdac_instances_TSQL"
  - "sysdac_instances"
  - "sysdac_instances_TSQL"
  - "dbo.sysdac_instances"
helpviewer_keywords:
  - "dbo.sysdac_instances"
  - "sysdac_instances"
dev_langs:
  - "TSQL"
ms.assetid: 28285f3d-3889-439f-8b24-3bdef08e46b4
---
# Data-tier Application Views - dbo.sysdac_instances
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Displays one row for each data-tier application (DAC) instance deployed to an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)]. sysdac_instances belongs to the dbo schema in the msdb database. The following table describes the columns in the sysdac_instances view.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|instance_id|**uniqueidentifier**|Identifier of the DAC instance.|  
|instance_name|**sysname**|Name of the DAC instance specified when the DAC was deployed.|  
|type_name|**sysname**|Name of the DAC specified when the DAC package was created.|  
|type_version|**nvarchar(64)**|Version of the DAC specified when the DAC package was created.|  
|description|**nvarchar(4000)**|A description of the DAC written when the DAC package was created.|  
|type_stream|**varbinary(max)**|A bit stream that contains an encoded representation of the logical objects, such as tables and views, contained in the DAC.|  
|date_created|**datetime**|Date and time the DAC instance was created.|  
|created_by|**sysname**|Login that created the DAC instance.|  
|database_name|**sysname**|Name of the database created for the DAC isntance.|  
  
## Remarks  
 A DAC includes a DAC type, which is a definition of the logical data-tier objects used by an application, such as tables and views. A DAC package is a file used to deploy a DAC. The DAC package contains a representation of all the logical objects contained in the DAC type. The DAC package can be used to deploy one or more copies, or instances, of the DAC to an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)]. Each DAC instance deployed from the same DAC package shares the same type, but is assigned a unique instance name and identifier.  
  
## Permissions  
 Requires membership in the sysadmin fixed server role to view all of the columns. Members of the public role can view the instance_name, description, and type_version columns.  
  
## See Also  
 [Data-tier Applications](../../relational-databases/data-tier-applications/data-tier-applications.md)   
 [Data-tier Application Views &#40;Transact-SQL&#41;]()  
  
