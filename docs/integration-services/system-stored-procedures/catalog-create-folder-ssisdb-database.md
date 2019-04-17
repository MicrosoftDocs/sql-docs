---
title: "catalog.create_folder (SSISDB Database) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "language-reference"
ms.assetid: 06fb3549-e970-4ca2-a61f-59affb9c6dcc
author: janinezhang
ms.author: janinez
manager: craigg
---
# catalog.create_folder (SSISDB Database)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Creates a folder in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
## Syntax  
  
```sql  
catalog.create_folder [@folder_name =] folder_name, [@folder_id =] folder_id OUTPUT  
```  
  
## Arguments  
 [@folder_name =] *folder_name*  
 The name of the new folder. The *folder_name* is **nvarchar(128)**.  
  
 [@folder_name =] *folder_id*  
 The unique identifier (ID) of the folder. The *folder_id* is **bigint**.  
  
## Return Code Value  
 The folder identifier is returned.  
  
## Result Sets  
 None  
  
## Permissions  
 This stored procedure requires one of the following permissions:  
  
-   Membership to the **ssis_admin** database role  
  
-   Membership to the **sysadmin** server role  
  
## Errors and Warnings  
If a folder with the same name already exists, the stored procedure returns an error .  
  
  
