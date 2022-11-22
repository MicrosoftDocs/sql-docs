---
description: "catalog.create_environment (SSISDB Database)"
title: "catalog.create_environment (SSISDB Database) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: "language-reference"
ms.assetid: 66367092-9f6e-40e6-90bd-81efb078ab70
author: chugugrace
ms.author: chugu
---
# catalog.create_environment (SSISDB Database)

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Creates an environment in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
## Syntax  
  
```sql  
catalog.create_environment [ @folder_name = ] folder_name  
     , [ @environment_name = ] environment_name  
  [  , [ @environment_description = ] environment_description ]  
```  
  
## Arguments  
 [@folder_name =] *folder_name*  
 The name of the folder to contain the environment. The *folder_name* is **nvarchar(128)**.  
  
 [@environment_name =] *environment_name*  
 The name of the environment. The *environment_name* is **nvarchar(128)**.  
  
 [@environment_description=] *environment_description*  
 An optional description of the environment. The *environment_description* is **nvarchar(1024)**.  
  
## Return Code Value  
 0 (success)  
  
## Result Sets  
 None  
  
## Permissions  
 This stored procedure requires one of the following permissions:  
  
-   READ and MODIFY permissions on the folder  
  
-   Membership to the **ssis_admin** database role  
  
-   database role  
  
-   Membership to the **sysadmin** server role  
  
## Errors and Warnings  
 The following list describes some conditions that may raise an error or warning:  
  
-   The folder name cannot be found  
  
-   An environment that has the same name already exists in the specified folder  
  
## Remarks  
 The environment name must be unique within the folder.  
  
  
