---
description: "catalog.executable_statistics"
title: "catalog.executable_statistics | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: "language-reference"
ms.assetid: 3dda28d6-10d8-4294-9b5e-a6048c07faf9
author: chugugrace
ms.author: chugu
---
# catalog.executable_statistics 

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]

  Displays a row for each executable that is run, including each iteration of an executable.  
  
 An executable is a task or container that you add to the control flow of a package.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|Statistics_id|bigint|Unique ID of the data.|  
|Execution_id|bigint|Unique ID for the instance of the execution.<br /><br /> The catalog.executions view provides additional information about executions. For more information, see [catalog.executions &#40;SSISDB Database&#41;](../../integration-services/system-views/catalog-executions-ssisdb-database.md).|  
|Executable_id|bigint|Unique ID for the package component.<br /><br /> The catalog.executables view provides additional information about executables. For more information, see [catalog.executables](../../integration-services/system-views/catalog-executables.md).|  
|Execution_path|nvarchar(max)|The full execution path of the package component, including each iteration of the component.|  
|Start_time|datetimeoffset(7)|The time when the executable enters the pre-execute phase.|  
|End_time|datetimeoffset(7)|The time when the executable enters the post-execute phase.|  
|Execution_duration|int|The length of time the executable spent in execution. The value is in milliseconds.|  
|Execution_result|smallint|The following are the possible values:<br /><br /> 0 (Success)<br /><br /> 1 (Failure)<br /><br /> 2 (Completion)<br /><br /> 3 (Cancelled)|  
|Execution_value|sql_variant|The value that is returned by the execution. This is a user-defined value.|  
  
## Permissions  
 The view requires one of the following permissions:  
  
-   READ permission on the instance of the execution.  
  
-   Membership to the **ssis_admin** database role.  
  
-   Membership to the **sysadmin** server role.  
  
  
