---
title: "catalog.set_customized_logging_level_value | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "language-reference"
ms.assetid: d83fb763-c7c6-4e20-bd10-0f995598b198
author: janinezhang
ms.author: janinez
manager: craigg
---
# catalog.set_customized_logging_level_value 

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


[!INCLUDE[tsql-appliesto-ss2016-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2016-xxxx-xxxx-xxx-md.md)]

  Changes the statistics or the events logged by an existing customized logging level. For more info about customized logging levels, see [Integration Services &#40;SSIS&#41; Logging](../../integration-services/performance/integration-services-ssis-logging.md).  
  
## Syntax  
  
```sql  
catalog.set_customized_logging_level_value [ @level_name = ] level_name  
    , [ @property_name = ] property_name  
    , [ @property_value = ] property_value  
```  
  
## Arguments  
 [ @level_name = ] *level_name*  
 The name of an existing customized logging level.  
  
 The *level_name* is **nvarchar(128)**.  
  
 [ @property_name = ] *property_name*  
 The name of the property to change. Valid values are **PROFILE** and **EVENTS**.  
  
 The *property_name* is **nvarchar(128)**.  
  
 [ @property_value = ] *property_value*  
 The new value for the specified property of the specified customized logging level.  
  
 For a list of valid values for profile and events, see [catalog.create_customized_logging_level](../../integration-services/system-stored-procedures/catalog-create-customized-logging-level.md).  
  
 The *property_value* is a **bigint**.  
  
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
  
  
