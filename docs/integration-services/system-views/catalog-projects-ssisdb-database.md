---
title: "catalog.projects (SSISDB Database) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "language-reference"
ms.assetid: a6b595e1-5227-47ce-8ee2-a28c1e1d5645
author: janinezhang
ms.author: janinez
manager: craigg
---
# catalog.projects (SSISDB Database)

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Displays the details for all projects that appear in the **SSISDB** catalog.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|project_id|**bigint**|The unique identifier (ID) of the project.|  
|folder_id|**bigint**|The unique ID of the folder where the project resides.|  
|name|**sysname**|The name of the project.|  
|description|**nvarchar(1024)**|The optional description of the project.|  
|project_format_version|**int**|The version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that was used to develop the project.|  
|deployed_by_sid|**varbinary(85)**|The security identifier (SID) of the user who installed the project.|  
|deployed_by_name|**nvarchar(128)**|The name of the user who installed the project.|  
|last_deployed_time|**datetimeoffset(7)**|The date and time at which the project was deployed or redeployed.|  
|created_time|**datetimeoffset(7)**|The date and time at which the project was created.|  
|object_version_lsn|**bigint**|The version of the project. This number is not guaranteed to be sequential.|  
|validation_status|**char(1)**|The validation status.|  
|last_validation_time|**datetimeoffset(7)**|The time of the last validation.|  
  
## Remarks  
 This view displays a row for each project in the catalog.  
  
## Permissions  
 This view requires one of the following permissions:  
  
-   READ permission on the project  
  
-   Membership to the **ssis_admin** database role  
  
-   Membership to the **sysadmin** server role.  
  
> [!NOTE]  
>  If you have READ permission on a project, you also have READ permission on all of the packages and environment references associated with that project. Row-level security is enforced; only rows that you have permission to view are displayed.  
  
  
