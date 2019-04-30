---
title: "catalog.extended_operation_info (SSISDB Database) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "language-reference"
ms.assetid: db299b45-557d-4c62-8e14-355cdb051f63
author: janinezhang
ms.author: janinez
manager: craigg
---
# catalog.extended_operation_info (SSISDB Database)

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Displays extended information for all operations in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|info_id|**bigint**|The unique identifier (ID) of the extended information.|  
|operation_id|**bigint**|The unique ID of the operation that corresponds to the extended information.|  
|object_name|**nvarchar(260)**|The name of the object.|  
|object_type|**smallint**|The type of object affected by the operation. The object may be a folder (`10`), project (`20`), package (`30`), environment (`40`), or instance of execution (`50`).|  
|reference_id|**bigint**|The unique ID of the reference that is used in the operation.|  
|status|**int**|The status of the operation. The possible values are created (`1`), running (`2`), canceled (`3`), failed (`4`), pending (`5`), ended unexpectedly (`6`), succeeded (`7`), stopping (`8`), and completed (`9`).|  
|start_time|**datetimeoffset(7)**|The date and time at which the operation started.|  
|end_time|**datetimeoffset(7)**|The date and time at which the operation ended.|  
  
## Remarks  
 A single operation can have multiple extended information rows.  
  
## Permissions  
 This view requires one of the following permissions:  
  
-   READ permission on the operation  
  
-   Membership to the **ssis_admin** database role  
  
-   Membership to the **sysadmin** server role  
  
> [!NOTE]  
>  When you have permission to perform an operation on the server, you also have permission to view information about the operation. Row-level security is enforced; only rows that you have permission to view are displayed.  
  
  
