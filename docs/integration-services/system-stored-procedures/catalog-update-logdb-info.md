---
title: "catalog.update_logdb_info (SSISDB Database) | Microsoft Docs"
ms.custom: ""
ms.date: "07/18/2017"
ms.prod: "sql-server-2017"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "integration-services"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
caps.latest.revision: 1
author: "haoqian"
ms.author: "haoqian"
manager: "jhubbard"
---
# catalog.update_logdb_info (SSISDB Database)
[!INCLUDE[tsql-appliesto-ssvnxt-xxxx-xxxx-xxx.md](../../includes/tsql-appliesto-ssvnxt-xxxx-xxxx-xxx.md)]

Update the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] Scale Out Logging information.

## Syntax

```tsql
update_logdb_info [@server_name = ] server_name, [@connection_string = ] connection_string
```

## Arguments
[ @server_name = ] *server_name*  
 The Sql Server used for Scale Out logging. The *server_name* is **nvarchar**.  

 [ @connection_string = ] *connection_string*  
 The connection string used for Scale Out logging. The *connection_string* is **nvarchar**.

 ## Return Code Value  
 0 (success)  
  
## Result Sets  
 None  

## Permissions  
 This stored procedure requires one of the following permissions:  
   
-   Membership to the **ssis_admin** database role  
  
-   Membership to the **sysadmin** server role  
 