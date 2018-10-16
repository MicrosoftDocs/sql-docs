---
title: "catalog.packages (SSISDB Database) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "language-reference"
helpviewer_keywords: 
  - "packages view [Integration Services]"
  - "catalog.packages view [Integration Services]"
ms.assetid: a634e94d-f492-4dfd-9611-a35f545106a1
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# catalog.packages (SSISDB Database)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Displays the details for all packages that appear in the **SSISDB** catalog.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|package_id|**bigint**|The unique identifier (ID) of the package.|  
|name|**nvarchar(256)**|The unique name of the package.|  
|package_guid|**uniqueidentifier**|The globally unique identifier (GUID) that identifies the package.|  
|description|**nvarchar(1024)**|An optional description of the package.|  
|package_format_version|**int**|The version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that was used to develop the package.|  
|version_major|**int**|The major version of the package.|  
|version_minor|**int**|The minor version of the package.|  
|version_build|**int**|The build version of the package.|  
|version_comments|**nvarchar(1024)**|Optional comments on the version of the package.|  
|version_guid|**uniqueidentifier**|The GUID that uniquely identifies the version of the package.|  
|project_id|**bigint**|The unique ID of the project.|  
|entry_point|**bit**|The value of `1` signifies that the package is meant to be started directly. The value of `0` signifies that the package is meant to be started by another package with the Execute Package task. The default value is `1`.|  
|validation_status|**char(1)**|The status of the validation.|  
|last_validation_time|**datetimeoffset(7)**|The time of the last validation.|  
  
## Remarks  
 This view displays a row for each package in the catalog.  
  
## Permissions  
 This view requires one of the following permissions:  
  
-   READ permission on the corresponding project  
  
-   Membership to the **ssis_admin** database role  
  
-   Membership to the **sysadmin** server role.  
  
> [!NOTE]  
>  If you have READ permission on a project, you also have READ permission on all of the packages and environment references associated with that project. Row-level security is enforced; only rows that you have permission to view are displayed.  
  
  
