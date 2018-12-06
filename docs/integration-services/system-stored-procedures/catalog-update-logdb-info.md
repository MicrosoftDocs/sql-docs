---
title: "catalog.update_logdb_info (SSISDB Database) | Microsoft Docs"
ms.custom: ""
ms.date: "07/18/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "language-reference"
author: "haoqian"
ms.author: "haoqian"
manager: craigg
monikerRange: ">= sql-server-2017 || = sqlallproducts-allversions"
---
# catalog.update_logdb_info (SSISDB Database)
[!INCLUDE[tsql-appliesto-ss2017-xxxx-xxxx-xxx-md.md](../../includes/tsql-appliesto-ss2017-xxxx-xxxx-xxx-md.md)]

Update the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] Scale Out Logging information.

## Syntax

```sql
catalog.update_logdb_info [@server_name = ] server_name, [@connection_string = ] connection_string
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
 
