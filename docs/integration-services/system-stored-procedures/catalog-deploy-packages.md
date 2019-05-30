---
title: "catalog.deploy_packages | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "language-reference"
ms.assetid: 8e861df6-d103-4d84-8438-e822533f6849
author: janinezhang
ms.author: janinez
manager: craigg
---
# catalog.deploy_packages 

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Deploys one or more packages to a folder in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog or updates an existing package that has been deployed previously.  
  
## Syntax  
  
```sql  
[catalog].[deploy_packages]     [ @folder_name = ] folder_name,    [ @project_name = ] project_name,    [ @packages_table = ] packages_table,     [ @operation_id OUTPUT ] operation_id OUTPUT ]  
```  
  
## Arguments  
 [ @folder_name = ] *folder_name*  
 The name of the folder. The *folder_name* is **nvarchar(128)**.  
  
 [ @project_name = ] *project_name*  
 The name of the project in the folder. The *project_name* is **nvarchar(128)**.  
  
 [ @packages_table = ] *packages_table*  
 The binary contents of [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package (.dtsx) file(s). The *packages_table* is **[catalog].[Package_Table_Type]**  
  
 [ @operation_id = ] *operation_id*  
 Returns the unique identifier for the deployment operation. The *operation_id* is **bigint**.  
  
## Return Code Value  
 0 (success)  
  
## Result Sets  
 None  
  
## Permissions  
 This stored procedure requires one of the following permissions:  
  
-   CREATE_OBJECTS permissions on the project or MODIFY permissions on the package to update a package.  
  
-   Membership to the **ssis_admin** database role  
  
-   Membership to the **sysadmin** server role  
  
## Errors and Warnings  
 The following list describes some conditions that may cause this stored procedure to raise an error:  
  
-   A parameter refers to an object that does not exist, a parameter tries to create an object that already exists, or a parameter is invalid in some other way.  
  
-   The user does not have sufficient permissions  
  
  
