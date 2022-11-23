---
description: "catalog.executables"
title: "catalog.executables | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: "language-reference"
ms.assetid: bae22d0c-e190-426f-a074-c1d1170e8dd8
author: chugugrace
ms.author: chugu
---
# catalog.executables 

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]

  This view displays a row for each executable in the specified execution.  
  
 An executable is a task or container that you add to the control flow of a package.  
  
|Column name|**Data type**|Description|  
|-----------------|-------------------|-----------------|  
|executable_id|**bigint**|The unique identifier for the executable.|  
|execution_id|**bigint**|The unique identifier for the instance of execution.|  
|executable_name|**nvarchar(4000)**|The name of the executable.|  
|executable_guid|**nvarchar(38)**|The GUID of the executable.|  
|package_name|**nvarchar(260)**|The name of the package.|  
|package_path|**nvarchar(max)**|The path of the package.|  
  
## Permissions  
 This view requires one of the following permissions:  
  
-   READ permission on the instance of execution  
  
-   Membership to the **ssis_admin** database role  
  
-   Membership to the **sysadmin** server role  
  
> [!NOTE]  
>  When you have permission to perform an operation on the server, you also have permission to view information about the operation. Row-level security is enforced; only rows that you have permission to view are displayed.  
  
## Remarks  
  
