---
description: "catalog.set_environment_reference_type (SSISDB Database)"
title: "catalog.set_environment_reference_type (SSISDB Database) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: "language-reference"
ms.assetid: b79e3a06-22c0-40e5-8933-1b3414db3329
author: chugugrace
ms.author: chugu
---
# catalog.set_environment_reference_type (SSISDB Database)

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Sets the reference type and environment name associated with an existing environment reference for a project in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
## Syntax  
  
```sql  
catalog.set_environment_reference_location [ @reference_id = reference_id  
    , [ @reference_type = ] reference_type  
 [  , [ @environment_folder_name = ] environment_folder_name ]  
```  
  
## Arguments  
 [ @reference_id = ] *reference_id*  
 The unique identifier of the environment reference that is to be updated. The *reference_id* is **bigint**.  
  
 [ @reference_type = ] *reference_type*  
 Indicates whether the environment can be located in the same folder as the project (relative reference) or in a different folder (absolute reference). Use the value `R` to indicate a relative reference. Use the value `A` to indicate an absolute reference. The *reference_type* is **char(1)**.  
  
 [ @environment_folder_name = ] *environment_folder_name*  
 The folder in which the environment is located. This value is required for absolute references. The *environment_folder_name* is **nvarchar(128)**.  
  
## Return Code Value  
 0 (success)  
  
## Result Sets  
 None  
  
## Permissions  
 This stored procedure requires one of the following permissions:  
  
-   READ and MODIFY permissions on the project, and READ permission on the environment  
  
-   Membership to the **ssis_admin** database role  
  
-   Membership to the **sysadmin** server role  
  
## Errors and Warnings  
 The following list describes some conditions that may raise an error or warning:  
  
-   The folder name, environment name, or reference ID is not valid  
  
-   The user does not appropriate permissions  
  
-   An absolute reference is specified by using the `A` character in the *reference_location* parameter, but the name of the folder was not specified with the *environment_folder_name* parameter.  
  
## Remarks  
 A project can have relative or absolute environment references. Relative references refer to the environment by name and require that it resides in the same folder as the project. Absolute references refer to the environment by name and folder, and may refer to environments that reside in a different folder than the project. A project can reference multiple environments.  
  
> [!IMPORTANT]  
>  If a relative reference is specified, the *environment_folder_name* parameter value is not used, and the environment folder name is automatically set to **NULL**. If an absolute reference is specified, the environment folder name must be provided in the *environment_folder_name* parameter.  
  
  
