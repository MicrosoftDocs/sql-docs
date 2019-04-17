---
title: "catalog.rename_customized_logging_level | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "language-reference"
ms.assetid: b1a57d5e-3f03-4901-8b2b-bb8b371b595b
author: janinezhang
ms.author: janinez
manager: craigg
---
# catalog.rename_customized_logging_level
[!INCLUDE[tsql-appliesto-ss2016-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2016-xxxx-xxxx-xxx-md.md)]

  Renames an existing customized logging level. For more info about customized logging levels, see [Integration Services &#40;SSIS&#41; Logging](../../integration-services/performance/integration-services-ssis-logging.md).  
  
## Syntax  
  
```sql  
catalog.rename_customized_logging_level [ @old_name = ] old_name  
    , [ @new_name = ] new_name  
```  
  
## Arguments  
 [ @old_name = ] *old_name*  
 The name of the existing customized logging level to rename.  
  
 The *old_name* is **nvarchar(128)**.  
  
 [ @new_name = ] *new_name*  
 The new name for the  specified customized logging level.  
  
 The *new_name* is **nvarchar(128)**.  
  
## Remarks  
  
## Return Codes  
 0 (success)  
  
 When the stored procedure fails, it throws an error.  
  
## Result Set  
 None  
  
## Permissions  
 This stored procedure requires one of the following permissions:  
  
-   Membership in the **ssis_admin** database role  
  
-   Membership in the **sysadmin** server role  
  
## Errors and Warnings  
 The following list describes conditions that cause the stored procedure to fail.  
  
-   The user does not have the required permissions.  
  
  
