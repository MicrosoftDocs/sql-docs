---
title: "catalog.operations (SSISDB Database) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "language-reference"
helpviewer_keywords: 
  - "operations view [Integration Services]"
  - "catalog.operations view [Integration Services]"
ms.assetid: 9455c5b1-60ff-45fc-8599-cc3abbd6daf5
author: janinezhang
ms.author: janinez
manager: craigg
---
# catalog.operations (SSISDB Database)

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Displays the details of all operations in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|operation_id|**bigint**|The unique identifier (ID) of the operation.|  
|operation_type|**smallint**|The type of operation.|  
|created_time|**datetimeoffset**|The time when the operation was created.|  
|object_type|**smallint**|The type of object affected by the operation. The object may be a folder (`10`), project (`20`), package (`30`), environment (`40`), or instance of execution (`50`).|  
|object_id|**bigint**|The ID of the object affected by the operation.|  
|object_name|**nvarchar(260)**|The name of the object.|  
|status|**int**|The status of the operation. The possible values are created (`1`), running (`2`), canceled (`3`), failed (`4`), pending (`5`), ended unexpectedly (`6`), succeeded (`7`), stopping (`8`), and completed (`9`).|  
|start_time|**datetimeoffset**|The time when the operation started.|  
|end_time|**datetimeoffsset**|The time when the operation ended.|  
|caller_sid|**varbinary(85)**|The security ID (SID) of the user if Windows Authentication was used to log on.|  
|caller_name|**nvarchar(128)**|The name of the account that performed the operation.|  
|process_id|**int**|The process ID of the external process, if applicable.|  
|stopped_by_sid|**varbinary(85)**|The SID of the user who stopped the operation.|  
|stopped_by_name|**nvarchar(128)**|The name of the user who stopped the operation.|  
|server_name|**nvarchar(128)**|The Windows server and instance information for a specified instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|machine_name|**nvarchar(128)**|The computer name on which the server instance is running.|  
  
## Remarks  
 This view displays one row for each operation in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog. It allows the administrator to enumerate all the logical operations that were performed on the server, such as deploying a project or executing a package.  
  
 This view displays the following operation types, as listed in the **operation_type** column:  
  
|**operation_type** Value|**operation_type** Description|**object_id** Description|**object_name** Description|  
|-------------------------------|-------------------------------------|--------------------------------|----------------------------------|  
|`1`|[!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] initialization|**NULL**|**NULL**|  
|`2`|Retention window<br /><br /> (SQL Agent job)|**NULL**|**NULL**|  
|`3`|MaxProjectVersion<br /><br /> (SQL Agent job)|**NULL**|**NULL**|  
|`101`|**deploy_project**<br /><br /> (Stored procedure)|Project ID|Project name|  
|`106`|**restore_project**<br /><br /> (Stored procedure)|Project ID|Project name|  
|`200`|**create_execution** and **start_execution**<br /><br /> (Stored procedures)|Project ID|**NULL**|  
|`202`|**stop_operation**<br /><br /> (Stored procedure)|Project ID|**NULL**|  
|`300`|**validate_project**<br /><br /> (Stored procedure)|Project ID|Project name|  
|`301`|**validate_package**<br /><br /> (Stored procedure)|Project ID|Package name|  
|`1000`|**configure_catalog**<br /><br /> (Stored procedure)|**NULL**|**NULL**||  
  
## Permissions  
 This view requires one of the following permissions:  
  
-   READ permission on the operation  
  
-   Membership to the **ssis_admin** database role  
  
-   Membership to the **sysadmin** server role  
  
> [!NOTE]  
>  When you have permission to perform an operation on the server, you also have permission to view information about the operation. Row-level security is enforced; only rows that you have permission to view are displayed.  
  
  
