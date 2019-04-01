---
title: "catalog.restore_project (SSISDB Database) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "language-reference"
ms.assetid: 8adee525-579b-4d2f-b807-e2ecc07fb2e9
author: janinezhang
ms.author: janinez
manager: craigg
---
# catalog.restore_project (SSISDB Database)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Restores a project in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog to a previous version.  
  
## Syntax  
  
```sql  
catalog.restore_project [ @folder_name = ] folder_name  
    , [ @project_name = ] project _name  
    , [ @object_version_lsn = ] object_version_lsn  
  
```  
  
## Arguments  
 [ @folder_name = ] *folder_name*  
 The name of the folder that contains the project. The *folder_name* is **nvarchar(128)**.  
  
 [ @project _name = ] *project_name*  
 The name of the project. The *project_name* is **nvarchar(128)**.  
  
 [ @object_version_lsn = ] *object_version_lsn*  
 The version of the project. The *object_version_lsn* is **bigint**.  
  
## Return Code Value  
 0 (success)  
  
## Result Sets  
 Project details are returned as **varbinary(MAX)** as part of the result set if the *project_name* is found.  
  
 **NO RESULT SET** is returned if the project cannot be restored to the specified folder.  
  
## Permissions  
 This stored procedure requires one of the following permissions:  
  
-   READ and MODIFY permissions on the project  
  
-   Membership to the **ssis_admin** database role  
  
-   Membership to the **sysadmin** server role  
  
## Errors and Warnings  
 The following list describes some conditions that may raise an error or warning:  
  
-   The project version does not exist or does not match the project name  
  
-   The project does not exist  
  
-   The user does not have the appropriate permissions  
  
## Remarks  
 When a project is restored, all parameters are assigned default values and all environment references remain unchanged. The maximum number of project versions that are retained in the catalog is determined by the catalog property **MAX_VERSIONS_PER_PROJECT**, as shown in the [catalog_property](../../integration-services/system-views/catalog-catalog-properties-ssisdb-database.md) view.  
  
> [!WARNING]  
>  Environment references may no longer be valid after a project has been restored.  
  
  
