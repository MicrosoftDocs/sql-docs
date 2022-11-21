---
description: "catalog.update_logdb_info (SSISDB Database)"
title: "catalog.update_logdb_info (SSISDB Database) | Microsoft Docs"
ms.custom: ""
ms.date: "07/18/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: "language-reference"
author: "haoqian"
ms.author: "haoqian"
monikerRange: ">= sql-server-2017"
---
# catalog.update_logdb_info (SSISDB Database)

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


[!INCLUDE[sqlserver2017](../../includes/applies-to-version/sqlserver2017.md)]

Update the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] Scale Out Logging information.

## Syntax

```sql
catalog.update_logdb_info [ @server_name = ] server_name, [ @connection_string = ] connection_string
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
 
