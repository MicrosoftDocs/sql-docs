---
title: "catalog.update_master_address (SSISDB Database)"
description: "catalog.update_master_address (SSISDB Database)"
author: "haoqian"
ms.author: "haoqian"
ms.date: "07/18/2017"
ms.service: sql
ms.subservice: integration-services
ms.topic: "language-reference"
monikerRange: ">= sql-server-2017"
---
# catalog.update_master_address (SSISDB Database)

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


[!INCLUDE[sqlserver2017](../../includes/applies-to-version/sqlserver2017.md)]

Update the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] Scale Out Master endpoint.

## Syntax

```sql
catalog.update_master_address [ @MasterAddress = ] masterAddress
```

## Arguments
[ @MasterAddress = ] *masterAddress*  
The Scale Out Master endpoint. The *masterAddress* is **nvarchar**.  

 ## Return Code Value  
 0 (success)  
  
## Result Sets  
 None  

## Permissions  
 This stored procedure requires one of the following permissions:  
   
-   Membership to the **ssis_admin** database role  
  
-   Membership to the **sysadmin** server role  
 
