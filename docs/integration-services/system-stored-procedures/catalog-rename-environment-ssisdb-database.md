---
description: "catalog.rename_environment (SSISDB Database)"
title: "catalog.rename_environment (SSISDB Database) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: "language-reference"
ms.assetid: c73d7452-31c5-4f4e-afcc-e9eca760c826
author: chugugrace
ms.author: chugu
---
# catalog.rename_environment (SSISDB Database)

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Renames an environment in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
## Syntax  
  
```sql  
catalog.rename_environment [ @folder_name = ] folder_name  
    , [ @environment_name = ] environment_name  
    , [ @new_environment_name= ] new_environment_name  
```  
  
## Arguments  
 [ @folder_name = ] *folder_name*  
 The name of the folder that contains the environment. The *folder_name* is **nvarchar(128)**.  
  
 [ @environment_name = ] *environment_name*  
 The original name of the environment. The *environment_name* is **nvarchar(128)**.  
  
 [ @new_environment_name = ] *new_environment_name*  
 The new name of the environment. The *new_environment_name* is **nvarchar(128)**.  
  
## Return Code Value  
 0 (success)  
  
## Result Sets  
 None  
  
## Permissions  
 This stored procedure requires one of the following permissions:  
  
-   MODIFY permissions on the environment  
  
-   Membership to the **ssis_admin** database role  
  
-   Membership to the **sysadmin** server role  
  
## Errors and Warnings  
 The following list describes some conditions that may raise an error or warning:  
  
-   The original environment name is not valid  
  
-   The new name has already been used on an existing environment  
  
## Remarks  
 Environment references from projects are not automatically updated when you rename the environment. Environment references must be updated accordingly. This stored procedure will succeed even if environment references are broken by changing the environment name. Environment references must be updated after this stored procedure completes.  
  
> [!NOTE]  
>  When an environment reference is not valid, validation and execution of the corresponding packages that use those references will fail.  
  
  
