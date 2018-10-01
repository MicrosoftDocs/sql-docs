---
title: "catalog.deploy_project (SSISDB Database) | Microsoft Docs"
ms.custom: ""
ms.date: "03/09/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "language-reference"
ms.assetid: 2e3439b4-7226-4b61-a993-7a1d161eac7e
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# catalog.deploy_project (SSISDB Database)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Deploys a project to a folder in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog or updates an existing project that has been deployed previously.  
  
## Syntax  
  
```sql  
catalog.deploy_project [@folder_name =] folder_name   
      , [@project_name =] project_name   
      , [@project_stream =] projectstream   
    [ , [@operation_id ] = operation_id OUTPUT ]   
```  
  
## Arguments  
 [@folder_name =] *folder_name*  
 The name of the folder where the project is deployed. The *folder_name* is **nvarchar(128)**.  
  
 [@project_name =] *project_name*  
 The name of the new or updated project in the folder. The *project_name* is **nvarchar(128)**.  
  
 [@projectstream =] *projectstream*  
 The binary contents of an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project deployment file (.ispac extension).  
  
 You can use a SELECT statement with the OPENROWSET function and the BULK rowset provider to retrieve the binary contents of the file. For an example, see [Deploy Integration Services (SSIS) Projects and Packages](../../integration-services/packages/deploy-integration-services-ssis-projects-and-packages.md). For more information about OPENROWSET, see [OPENROWSET &#40;Transact-SQL&#41;](../../t-sql/functions/openrowset-transact-sql.md).  
  
 The *projectstream* is **varbinary(MAX)**  
  
 [@operation_id =] *operation_id*  
 Returns the unique identifier for the deployment operation. The *operation_id* is **bigint**.  
  
## Return Code Value  
 0 (success)  
  
## Result Sets  
 None  
  
## Permissions  
 This stored procedure requires one of the following permissions:  
  
-   CREATE_OBJECTS permissions on the folder to deploy a new project or MODIFY permissions on the project to update a project  
  
-   Membership to the **ssis_admin** database role  
  
-   Membership to the **sysadmin** server role  
  
## Errors and Warnings  
 The following list describes some conditions that may cause this stored procedure to raise an error:  
  
-   A parameter refers to an object that does not exist, a parameter tries to create an object that already exists, or a parameter is invalid in some other way  
  
-   The value of the parameter *@project_name* does not match the name of the project in the deployment file  
  
-   The user does not have sufficient permissions  
  
## Remarks  
 During a project deployment or update, the stored procedure does not check the protection level of individual packages in the project.  
  
  
