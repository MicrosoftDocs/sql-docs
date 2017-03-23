---
title: "catalog.set_worker_agent_property (SSISDB Database) | Microsoft Docs"
ms.custom: ""
ms.date: "03/02/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "integration-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: ddd2a534-6925-4d66-90e7-541c14f41de7
caps.latest.revision: 2
author: "sabotta"
ms.author: "carlasab"
manager: "jhubbard"
---
# catalog.set_worker_agent_property (SSISDB Database)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx_md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

Sets the property of a [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] Scale Out Worker.

## Syntax

```tsql
set_worker_agent_property [ @WorkerAgentId = ] WorkerAgentId, [ @PropertyName = ] PropertyName, [ @PropertyValue = ] PropertyValue 
```

## Arguments
[ @WorkerAgentId = ] *WorkerAgentId*  
The worker agent id of Scale Out Worker. The *WorkerAgentId* is **uniqueidentifier**.

[ @PropertyName = ] *PropertyName*  
The name of the property. The *PropertyName* is **nvarchar(256)**.

[ @PropertyValue = ] *PropertyValue*  
The value of the propery. The *PropertyValue* is **nvarchar(max)**.

## Remarks
The valid propery names are **DisplayName**, **Description**, **Tags**.

## Return Code Value  
 0 (success)  
  
## Result Sets  
 None  

## Permissions  
 This stored procedure requires one of the following permissions:  
  
-   Membership to the **ssis_admin** database role  
  
-   Membership to the **sysadmin** server role

## Erros and Warnings
  The following list describes some conditions that may raise an error or warning:  
  
-   The user does not have the appropriate permissions 

-   The worker agent id is not valid.

-   The property name is not valid.

-   The property value is not vilid.  