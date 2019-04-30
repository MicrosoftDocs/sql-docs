---
title: "catalog.update_master_address (SSISDB Database) | Microsoft Docs"
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
# catalog.update_master_address (SSISDB Database)

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


[!INCLUDE[tsql-appliesto-ss2017-xxxx-xxxx-xxx-md.md](../../includes/tsql-appliesto-ss2017-xxxx-xxxx-xxx-md.md)]

Update the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] Scale Out Master endpoint.

## Syntax

```sql
catalog.update_master_address [@MasterAddress = ] masterAddress
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
 
