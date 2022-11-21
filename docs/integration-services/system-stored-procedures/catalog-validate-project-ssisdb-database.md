---
description: "catalog.validate_project (SSISDB Database)"
title: "catalog.validate_project (SSISDB Database) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: "language-reference"
ms.assetid: 5270689a-46d4-4847-b41f-3bed1899e955
author: chugugrace
ms.author: chugu
---
# catalog.validate_project (SSISDB Database)

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Asynchronously validates a project in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
## Syntax  
  
```sql
catalog.validate_project [ @folder_name = ] folder_name  
    , [ @project_name = ] project_name  
    , [ @validate_type = ] validate_type  
    , [ @validation_id = ] validation_id OUTPUT  
 [  , [ @use32bitruntime = ] use32bitruntime ]  
 [  , [ @environment_scope = ] environment_scope ]  
 [  , [ @reference_id = ] reference_id ]  
```  
  
## Arguments  
 [ @folder_name = ] *folder_name*  
 The name of a folder that contains the project. The *folder_name* is **nvarchar(128)**.  
  
 [ @project_name = ] *project_name*  
 The name of the project. The *project_name* is **nvarchar(128)**.  
  
 [ @validate_type = ] *validate_type*  
 Indicates the type of validation to perform. Use the character `F` to perform a full validation. This parameter is optional, the character `F` will be used by default. The *validate_type* is **char(1)**.  
  
 [ @validation_id = ] *validation_id*  
 Returns the unique identifier (ID) of the validation. The *validation_id* is **bigint**.  
  
 [ @use32bitruntime = ] *use32bitruntime*  
 Indicates if the 32-bit runtime should be used to run the package on a 64-bit operating system. Use the value of `1` to execute the package with the 32-bit runtime when running on a 64-bit operating system. Use the value of `0` to execute the package with the 64-bit runtime when running on a 64-bit operating system. This parameter is optional. The *use32bitruntime* is **bit**.  
  
 [ @environment_scope = ] *environment_scope*  
 Indicates the environment references that are considered by the validation. When the value is `A`, all environment references associated with the project are included in the validation. When the value is `S`, only a single environment reference is included. When the value is `D`, no environment references are included and each parameter must have a literal default value in order to pass validation. This parameter is optional, the character `D` will be used by default. The *environment_scope* is **char(1)**.  
  
 [ @reference_id = ] *reference_id*  
 The unique ID of the environment reference. This parameter is required only when a single environment reference is included in the validation, when *environment_scope* is `S`. The *reference_id* is **bigint**.  
  
## Return Code Values  
 0 (success)  
  
## Result Sets  
 Output from the validation steps is returned as different sections of the result set.  
  
## Permissions  
 This stored procedure requires one of the following permissions:  
  
-   READ permissions on the project and, if applicable, READ permissions on the referenced environments  
  
-   Membership to the **ssis_admin** database role  
  
-   Membership to the **sysadmin** server role  
  
## Errors and Warnings  
 The following list provides some conditions that may raise an error or warning:  
  
-   Validation fails for one or more packages in the project  
  
-   Validation fails if one or more of the referenced environments included in the validation do not contain referenced variables  
  
-   The specified validate type is not valid  
  
-   The project name or environment reference ID is not valid  
  
-   The user does not have the appropriate permissions  
  
## Remarks  
 Validation helps identify issues that will prevent the packages in the project from running successfully. Use the [catalog.validations](../../integration-services/system-views/catalog-validations-ssisdb-database.md) or [catalog.operations](../../integration-services/system-views/catalog-operations-ssisdb-database.md) views to monitor for validation status.  
  
 Only environments that are accessible by the user can be used in the validation. Validation output is sent to the client as a result set.  
  
 In this release, project validation does not support dependency validation.  
  
 Full validation confirms that all referenced environment variables are found within the referenced environments that were included in the validation. Full validation results list environment references that are not valid and referenced environment variables that could not be found in any of the referenced environments that were included in the validation.  
  
  
