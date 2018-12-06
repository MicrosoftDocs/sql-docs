---
title: "catalog.validations (SSISDB Database) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "language-reference"
ms.assetid: dbafe110-b480-48f3-b45f-31d71ca68f62
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# catalog.validations (SSISDB Database)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Displays the details of all project and package validations in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|validation_id|**bigint**|The unique identifier (ID) of the validation.|  
|environment_scope|**Char(1)**|Indicates the environment references that are considered by the validation. When the value is `A`, all environment references associated with the project are included in the validation. When the value is `S`, only a single environment reference is included. When the value is `D`, no environment references are included and each parameter must have a literal default value in order to pass validation.|  
|validate_type|**Char(1)**|The type of validation to perform. The possible types of validation are dependency validation (`D`) or full validation (`F`). Package validation is always full validation.|  
|folder_name|**nvarchar(128)**|The name of the folder that contains the corresponding project.|  
|project_name|**nvarchar(128)**|The name of the project.|  
|project_lsn|**bigint**|The version of the project that is validated against.|  
|use32bitruntime|**bit**|Indicates if the 32-bit runtime is used to run the package on a 64-bit operating system. When the value is `1`, the execution is performed with the 32-bit runtime. When the value is `0`, the execution is performed with the 64-bit runtime.|  
|reference_id|**bigint**|Unique ID of the project-environment reference that is used by the project to reference an environment.|  
|operation_type|**smallint**|The type of operation. The operations displayed in this view include project validation (`300`) and package validation (`301`).|  
|object_name|**nvarhcar(260)**|The name of the object.|  
|object_type|**smallint**|The type of object. The object may be a project (`20`) or a package (`30`).|  
|object_id|**bigint**|The ID of the object affected by the operation.|  
|start_time|**datetimeoffset(7)**|The time when the operation started.|  
|end_time|**datetimeoffsset(7)**|The time when the operation ended.|  
|status|**int**|The status of the operation. The possible values are created (`1`), running (`2`), canceled (`3`), failed (`4`), pending (`5`), ended unexpectedly (`6`), succeeded (`7`), stopping (`8`), and completed (`9`).|  
|caller_sid|**varbinary(85)**|The security ID (SID) of the user if Windows Authentication was used to log on.|  
|caller_name|**nvarchar(128)**|The name of the account that performed the operation.|  
|process_id|**int**|The process ID of the external process, if applicable.|  
|stopped_by_sid|**varbinary(85)**|The SID of the user who stopped the operation.|  
|stopped_by_name|**nvarchar(128)**|The name of the user who stopped the operation.|  
|server_name|**nvarchar(128)**|The Windows server and instance information for a specified instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|machine_name|**nvarchar(128)**|The computer name on which the server instance is running.|  
|dump_id|**uniqueidentifier**|The ID of the execution dump.|  
  
## Remarks  
 This view displays one row for each validation in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
## Permissions  
 This view requires one of the following permissions:  
  
-   READ permission on the corresponding operation  
  
-   Membership to the **ssis_admin** database role  
  
-   Membership to the **sysadmin** server role  
  
> [!NOTE]  
>  When you have permission to perform an operation on the server, you also have permission to view information about the operation. Row-level security is enforced; only rows that you have permission to view are displayed.  
  
  
