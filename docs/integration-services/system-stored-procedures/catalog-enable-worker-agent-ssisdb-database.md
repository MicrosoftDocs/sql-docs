---
title: "catalog.enable_worker_agent (SSISDB Database) | Microsoft Docs"
ms.custom: ""
ms.date: "12/16/2016"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "integration-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: c6e5266b-c32d-49ff-aa69-f09664009fb4
caps.latest.revision: 3
author: "sabotta"
ms.author: "carlasab"
manager: "jhubbard"
---
# catalog.enable_worker_agent (SSISDB Database)
[!INCLUDE[tsql-appliesto-ss2016-xxxx-xxxx-xxx_md](../../includes/tsql-appliesto-ss2016-xxxx-xxxx-xxx-md.md)]

Enable a Scale Out Worker for Scale Out Master working with this [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.

## Syntax

```tsql
enable_worker_agent [@WorkerAgentId = ] WorkerAgentId
```
## Arguments
[ @WorkerAgentId = ] *WorkerAgentId*
The worker agent id of Scale Out Worker. The *WorkerAgentId* is **uniqueidentifier**.

## Example
This example enables the Scale Out Worker on MachineA.
```tsql
SELECT WorkerAgentId, MachineName FROM [catalog].[worker_agents]
GO
-- Result: --
-- WorkerAgentId                           MachineName --
-- 6583054A-E915-4C2A-80E4-C765E79EF61D    MachineA    --

EXEC [catalog].[enable_worker_agent] '6583054A-E915-4C2A-80E4-C765E79EF61D'
GO 
```

## Return Code Value  
 0 (success)  
  
## Result Sets  
 None  

## Permissions  
 This stored procedure requires one of the following permissions:  
  
-   Membership to the **ssis_admin** database role  
  
-   Membership to the **sysadmin** server role 

## Errors and Warnings
The stored procedure returns an error if the worker agent ID is not valid.
