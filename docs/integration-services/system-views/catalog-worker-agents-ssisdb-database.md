---
description: "catalog.worker_agents (SSISDB Database)"
title: "catalog.worker_agents (SSISDB Database) | Microsoft Docs"
ms.custom: ""
ms.date: "12/16/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
ms.assetid: 0bd0d827-e2f1-44fe-aa90-6bf922d68d16
author: chugugrace
ms.author: chugu
---
# catalog.worker_agents (SSISDB Database)

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]

Displays the information for the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] Scale Out Worker.

|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|WorkerAgentId|**uniqueidentifier**|The worker agent ID of Scale Out Worker.|
|IsEnabled|**bit**|Whether the Scale Out Worker is enabled.|
|DisplayName|**nvarchar(256)**|The display name of Scale Out Worker.|
|Description|**nvarchar(256)**|The description of Scale Out Worker.|
|MachineName|**nvarchar(256)**|The machine name for Scale Out Worker.|
|Tags|**nvarchar(max)**|The tags of Scale Out Worker.|
|UserAccount|**nvarchar(256)**|The user account running the Scale Out Worker service.|
|LastOnlineTime|**datetimeoffset(7)**|The last time that the Scale Out Worker is online.|

## Remarks
This view displays a row for each Scale Out Worker connecting to the Scale Out Master working with the SSISDB catalog.

## Permissions
This view requires one of the following permissions:

- Membership to the **ssis_admin** database role

- Membership to the **ssis_cluster_executor** database role

- Membership to the **sysadmin** server role
