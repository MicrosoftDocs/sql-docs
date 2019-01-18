---
title: "catalog.set_worker_agent_property (SSISDB Database) | Microsoft Docs"
ms.custom: ""
ms.date: "03/02/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: ddd2a534-6925-4d66-90e7-541c14f41de7
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# catalog.set_worker_agent_property (SSISDB Database)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

Sets the property of a [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] Scale Out Worker.

## Syntax

```sql
catalog.set_worker_agent_property [@WorkerAgentId =] WorkerAgentId, [@PropertyName =] PropertyName, [@PropertyValue =] PropertyValue 
```

## Arguments
[@WorkerAgentId =] *WorkerAgentId*  
The worker agent ID of Scale Out Worker. The *WorkerAgentId* is **uniqueidentifier**.

[@PropertyName =] *PropertyName*  
The name of the property. The *PropertyName* is **nvarchar(256)**.

[@PropertyValue =] *PropertyValue*  
The value of the property. The *PropertyValue* is **nvarchar(max)**.

## Remarks
The valid property names are **DisplayName**, **Description**, **Tags**.

## Return Code Value  
 0 (success)  
  
## Result Sets  
 None  

## Permissions  
 This stored procedure requires one of the following permissions:  
  
-   Membership to the **ssis_admin** database role  
  
-   Membership to the **sysadmin** server role

## Errors and Warnings
  The following list describes some conditions that may raise an error or warning:  
  
-   The user does not have the appropriate permissions 

-   The worker agent ID is not valid.

-   The property name is not valid.

-   The property value is not valid.  
