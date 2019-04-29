---
title: "catalog.disable_worker_agent (SSISDB Database) | Microsoft Docs"
ms.custom: ""
ms.date: "12/16/2016"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: 3f19dc4c-a000-4318-8fe1-e80d56720e66
author: janinezhang
ms.author: janinez
manager: craigg
---
# catalog.disable_worker_agent (SSISDB Database)
[!INCLUDE[tsql-appliesto-ss2016-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2016-xxxx-xxxx-xxx-md.md)]

Disable a Scale Out Worker for Scale Out Master working with this [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.

## Syntax

```sql
catalog.disable_worker_agent [@WorkerAgentId =] WorkerAgentId
```
## Arguments
[@WorkerAgentId =] *WorkerAgentId*
The worker agent ID of Scale Out Worker. The *WorkerAgentId* is **uniqueidentifier**.

## Example
This example disables the Scale Out Worker on MachineA.

```sql
SELECT WorkerAgentId, MachineName FROM [catalog].[worker_agents]
GO
-- Result: --
-- WorkerAgentId                           MachineName --
-- 6583054A-E915-4C2A-80E4-C765E79EF61D    MachineA    --

EXEC [catalog].[disable_worker_agent] '6583054A-E915-4C2A-80E4-C765E79EF61D'
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
If the worker agent ID is not valid, the stored procedure returns an error.
