---
title: "catalog.add_execution_worker (SSISDB Database) | Microsoft Docs"
ms.custom: ""
ms.date: "12/16/2016"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "integration-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: d587cedd-6402-4d5c-9526-7cd25627a037
caps.latest.revision: 4
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# catalog.add_execution_worker (SSISDB Database)
[!INCLUDE[tsql-appliesto-ssvnxt-xxxx-xxxx-xxx.md](../../includes/tsql-appliesto-ssvnxt-xxxx-xxxx-xxx.md)]

Adds a [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] Scale Out Worker to an instance of execution in Scale Out.

## Syntax

```tsql
add_execution_worker [@execution_id = ] execution_id, [@workeragent_id = ] workeragent_id
```

## Arguments
[ @execution_id = ] *execution_id*  
 The unique identifier for the instance of execution. The *execution_id* is **bigint**.  
 
[@workeragent_id = ] *workeragent_id*  
The worker agent id of a Scale Out Worker. The *workeragent_id* is **uniqueIdentifier**.

## Return Code Value  
 0 (success)  
  
## Result Sets  
 None  

## Permissions  
 This stored procedure requires one of the following permissions:  
  
-   READ and MODIFY permissions on the instance of execution  
  
-   Membership to the **ssis_admin** database role  
  
-   Membership to the **sysadmin** server role  
 
## Errors and Warnings  
 The following list describes some conditions that may raise an error or warning:  
 
- The user does not have the appropriate permissions.

- The execution identifier is not valid.

- The worker agent id is not valid.

- The execution is not in Scale Out.

## See Also
[Execute packages in Scale Out](~/integration-services/scale-out/run-packages-in-integration-services-ssis-scale-out.md).

