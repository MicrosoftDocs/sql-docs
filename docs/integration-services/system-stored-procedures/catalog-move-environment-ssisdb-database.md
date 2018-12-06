---
title: "catalog.move_environment (SSISDB Database) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "language-reference"
ms.assetid: b3fb5242-3c4c-4a87-b3e5-beb22fbab053
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# catalog.move_environment (SSISDB Database)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Moves an environment from one folder to another within the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
## Syntax  
  
```sql  
catalog.move_environment [ @source_folder = ] source_folder  
    , [ @environment_name = ] environment_name  
    , [ @destination_folder = ] destination_folder  
```  
  
## Arguments  
 [ @source_folder = ] *source_folder*  
 The name of the source folder, where the environment resides before the move. The *source_folder* is **nvarchar(128)**.  
  
 [ @environment_name = ] *environment_name*  
 The name of the environment that is to be moved. The *environment_name* is **nvarchar(128)**.  
  
 [ @destination_folder = ] *destination_folder*  
 The name of the destination folder, where the environment resides after the move. The *destination_folder* is **nvarchar(128)**.  
  
## Return Code Value  
 0 (success)  
  
## Result Sets  
 None  
  
## Permissions  
 This stored procedure requires one of the following permissions:  
  
-   READ and MODIFY permissions on the environment  
  
-   Membership to the **ssis_admin** database role  
  
-   Membership to the **sysadmin** server role  
  
## Errors and Warnings  
 The following list describes some conditions that may raise an error or warning:  
  
-   The environment does not exist in the source folder  
  
-   The destination folder already has an environment with the same name  
  
-   The user does not have the appropriate permissions  
  
## Remarks  
 Environment references from projects do not follow the environment during the move. Environment references must be updated accordingly. This stored procedure will succeed even if environment references are broken by moving an environment. Environment references must be updated after this stored procedure completes.  
  
> [!NOTE]  
>  A project can have relative or absolute environment references. Relative references refer to the environment by name, and these references require that the environment reside in the same folder as the project. Absolute references refer to the environment by name and folder, and these references refer to environments that reside in a different folder from that of the project.  
  
  
