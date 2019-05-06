---
title: "catalog.move_project (SSISDB Database) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "language-reference"
ms.assetid: ef3b0325-d8e9-472b-bf11-7d3efa6312ff
author: janinezhang
ms.author: janinez
manager: craigg
---
# catalog.move_project - SSISDB Database
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Moves a project from one folder to another within the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
## Syntax  
  
```sql  
catalog.move_project [ @source_folder = ] source_folder  
    , [ @project_name = ] project_name  
    , [ @destination_folder = ] destination_folder  
```  
  
## Arguments  
 [ @source_folder = ] *source_folder*  
 The name of the source folder, where the project resides before the move. The *source_folder* is **nvarchar(128)**.  
  
 [ @project_name = ] *project_name*  
 The name of the project that is to be moved. The *project_name* is **nvarchar(128)**.  
  
 [ @destination_folder = ] *destination_folder*  
 The name of the destionation folder, where the project resides after the move. The *destination_folder* is **nvarchar(128)**.  
  
## Return Code Value  
 0 (success)  
  
## Result Sets  
 None  
  
## Permissions  
 This stored procedure requires one of the following permissions:  
  
-   READ and MODIFY permissions on the project that you want to move and CREATE_OBJECTS permission on the destination folder  
  
-   Membership to the **ssis_admin** database role  
  
-   Membership to the **sysadmin** server role  
  
## Errors and Warnings  
 The following list describes some conditions that may cause this stored procedure to raise an error:  
  
-   The project does not exist  
  
-   The source folder does not exist  
  
-   The destination folder does not exist or the destination folder already contains a project with the same name  
  
-   The user does not have the appropriate permissions  
  
## Remarks  
 When a project is moved from a source folder to a destination folder, the project in the source folder and corresponding environment references are deleted. In the destination folder, an identical project and environment references are created. Relative environment references will resolve to a different folder after the move. Absolute references will resolve to the same folder after the move.  
  
> [!NOTE]  
>  A project can have relative or absolute environment references. Relative references refer to the environment by name, and these references require that the environment reside in the same folder as the project. Absolute references refer to the environment by name and folder, and these references refer to environments that reside in a different folder from that of the project.  
  
  
