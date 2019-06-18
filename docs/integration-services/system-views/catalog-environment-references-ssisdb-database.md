---
title: "catalog.environment_references (SSISDB Database) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "language-reference"
ms.assetid: efec53ef-3e5a-4b76-b71d-a0cf9e11ac00
author: janinezhang
ms.author: janinez
manager: craigg
---
# catalog.environment_references (SSISDB Database)

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Displays the environment references for all projects in the **SSISDB** catalog.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|reference_id|**bigint**|The unique identifier (ID) of the reference.|  
|project_id|**bigint**|The unique ID of the project.|  
|reference_type|**char(1)**|Indicates whether the environment can be located in the same folder as the project (relative reference) or in a different folder (absolute reference). When the value is `R`, the environment is located by using a relative reference. When the value is `A`, the environment is located by using an absolute reference.|  
|environment_folder_name|**sysname**|The name of the folder if the environment is located by using an absolute reference.|  
|environment_name|**sysname**|The name of the environment that is referenced by the project.|  
|validation_status|**char(1)**|The status of the validation.|  
|last_validation_time|**datatimeoffset(7)**|The time of the last validation.|  
  
## Remarks  
 This view displays a row for each environment reference in the catalog.  
  
## Permissions  
 This view requires one of the following permissions:  
  
-   READ permission on the corresponding project  
  
-   Membership to the **ssis_admin** database role  
  
-   Membership to the **sysadmin** server role.  
  
> [!NOTE]  
>  If you have READ permission on a project, you also have READ permission on all of the packages and environment references associated with that project. Row-level security is enforced; only rows that you have permission to view are displayed.  
  
## Remarks  
 A project can have relative or absolute environment references. Relative references refer to the environment by name and require that it resides in the same folder as the project. Absolute references refer to the environment by name and folder, and may refer to environments that reside in a different folder than the project. A project can reference multiple environments.  
  
  
