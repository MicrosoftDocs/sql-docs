---
description: "catalog.validate_package (SSISDB Database)"
title: "catalog.validate_package (SSISDB Database) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: "language-reference"
helpviewer_keywords: 
  - "validate_package stored procedure [Integration Services]"
  - "catalog.validate_package stored procedure [Integration Services]"
ms.assetid: 0dc03df1-b793-408f-af4c-c11188729abf
author: chugugrace
ms.author: chugu
---
# catalog.validate_package (SSISDB Database)

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Asynchronously validates a package in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
## Syntax  
  
```sql
catalog.validate_package [ @folder_name = ] folder_name  
    , [ @project_name = ] project_name  
    , [ @package_name = ] package_name  
    , [ @validation_id = ] validation_id OUTPUT  
 [  , [ @use32bitruntime = ] use32bitruntime ]  
 [  , [ @environment_scope = ] environment_scope ]  
 [  , [ @reference_id = ] reference_id ]  
```  
  
## Arguments  
 [ @folder_name = ] *folder_name*  
 The name of the folder that contains the package. The *folder_name* is **nvarchar(128)**.  
  
 [ @project_name = ] *project_name*  
 The name of the project that contains the package. The *project_name* is **nvarchar(128)**.  
  
 [ @package_name = ] *package_name*  
 The name of the package. The *package_name* is **nvarchar(260)**.  
  
 [ @validation_id = ] *validation_id*  
 Returns the unique identifier (ID) of the validation. The *validation_id* is **bigint**.  
  
 [ @use32bitruntime = ] *use32bitruntime*  
 Indicates if the 32-bit runtime should be used to run the package on a 64-bit operating system. Use the value of `1` to execute the package with the 32-bit runtime when running on a 64-bit operating system. Use the value of `0` to execute the package with the 64-bit runtime when running on a 64-bit operating system. This parameter is optional. The *use32bitruntime* is **bit**.  
  
 [ @environment_scope = ] *environment_scope*  
 Indicates the environment references that are considered by the validation. When the value is `A`, all environment references associated with the project are included in the validation. When the value is `S`, only a single environment reference is included. When the value is `D`, no environment references are included and each parameter must have a literal default value in order to pass validation. This parameter is optional. The character `D` is used by default. The *environment_scope* is **char(1)**.  
  
 [ @reference_id = ] *reference_id*  
 The unique ID of the environment reference. This parameter is required only when a single environment reference is included in the validation, when *environment_scope* is `S`. The *reference_id* is **bigint**.  
  
## Return Code Values  
 0 (success)  
  
## Result Sets  
 None  
  
## Permissions  
 This stored procedure requires one of the following permissions:  
  
-   READ permissions on the project and, if applicable, READ permissions on the referenced environments  
  
-   Membership to the **ssis_admin** database role  
  
-   Membership to the **sysadmin** server role  
  
## Errors and Warnings  
 The following list describes some conditions that may raise an error or warning:  
  
-   The project name or package name is not valid  
  
-   The user does not have the appropriate permissions  
  
-   One or more of the referenced environments included in the validation do not contain referenced variables  
  
-   The validation of the package fails  
  
-   The environment that is referenced does not exist  
  
-   Referenced variables cannot be found in the referenced environments included in the validation  
  
-   Variables are referenced in the package parameters, but no referenced environments have been included in the validation  
  
## Remarks  
 Validation helps identify issues that may prevent the package from running successfully. Use the [catalog.validations](../../integration-services/system-views/catalog-validations-ssisdb-database.md) or [catalog.operations](../../integration-services/system-views/catalog-operations-ssisdb-database.md) views to monitor for validation status.  
  
  
