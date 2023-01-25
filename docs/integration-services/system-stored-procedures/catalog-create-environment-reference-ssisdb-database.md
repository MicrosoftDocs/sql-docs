---
description: "catalog.create_environment_reference (SSISDB Database)"
title: "catalog.create_environment_reference (SSISDB Database) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: "language-reference"
ms.assetid: 48069bea-31cb-4a0e-9849-a07edc94088f
author: chugugrace
ms.author: chugu
---
# catalog.create_environment_reference (SSISDB Database)

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Creates an environment reference for a project in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
## Syntax  
  
```sql  
catalog.create_environment_reference [ @folder_name = ] folder_name  
     , [ @project_name = ] project_name  
     , [ @environment_name = ] environment_name  
     , [ @reference_type = ] reference_type  
  [  , [ @environment_folder_name = ] environment_folder_name ]  
  [  , [ @reference_id = ] reference_id OUTPUT ]  
```  
  
## Arguments  
 [ @folder_name = ] *folder_name*  
 The name of the folder of the project that is referencing the environment. The *folder_name* is **nvarchar(128)**.  
  
 [ @project_name = ] *project_name*  
 The name of the project that is referencing the environment. The *project_name* is **nvarchar(128)**.  
  
 [ @environment_name = ] *environment_name*  
 The name of the environment being referenced. The *environment_name* is **nvarchar(128)**.  
  
 [ @reference_type = ] *reference_type*  
 Indicates whether the environment can be located in the same folder as the project (relative reference) or in a different folder (absolute reference). Use the value `R` to indicate a relative reference. Use the value `A` to indicate an absolute reference. The *reference_type* is **char(1)**.  
  
 [ @environment_folder_name = ] *environment_folder_name*  
 The name of the folder in which the environment that being referenced is located. This value is required for absolute references. The *environment_folder_name* is **nvarchar(128)**.  
  
 [ @reference_id = ] *reference_id*  
 Returns the unique identifier for the new reference. This parameter is optional. The *reference_id* is **bigint**.  
  
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
  
-   The folder name is not valid  
  
-   The project name is not valid  
  
-   The user does not appropriate permissions  
  
-   An absolute reference is specified by using the `A` character in the *reference_location* parameter, but the name of the folder was not specified with the *environment_folder_name* parameter.  
  
## Remarks  
 A project can have relative or absolute environment references. Relative references refer to the environment by name and require that it resides in the same folder as the project. Absolute references refer to the environment by name and folder, and may refer to environments that reside in a different folder than the project. A project can reference multiple environments.  
  
  
