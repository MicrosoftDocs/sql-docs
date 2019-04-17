---
title: "catalog.environments (SSISDB Database) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "language-reference"
ms.assetid: 7014c0e3-65dc-4a46-842e-4decf3737748
author: janinezhang
ms.author: janinez
manager: craigg
---
# catalog.environments (SSISDB Database)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Displays the environment details for all environments in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog. Environments contain variables that can be referenced by [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] projects.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|environment_id|**bigint**|The unique identifier (ID) of the environment.|  
|name|**sysname**|The name of the environment.|  
|folder_id|**bigint**|The unique ID of the folder in which the environment resides.|  
|description|**nvarchar(1024)**|The description of the environment. This value is optional.|  
|created_by_sid|**varbinary(85)**|The security identifier (SID) of the user who created the environment.|  
|created_by_name|**nvarchar(128)**|The name of the user who created the environment.|  
|created_time|**datetimeoffset**|The date and time at which the environment was created.|  
  
## Remarks  
 This view displays a row for each environment in the catalog. Environment names are only unique with respect to the folder in which they are located. For example, an environment named `E1` can exist in more than one folder in the catalog, but each folder can have only one environment named `E1`.  
  
## Permissions  
 This view requires one of the following permissions:  
  
-   READ permission on the environment  
  
-   Membership to the **ssis_admin** database role  
  
-   Membership to the **sysadmin** server role  
  
> [!NOTE]  
>  Row-level security is enforced; only rows that you have permission to view are displayed.  
  
  
