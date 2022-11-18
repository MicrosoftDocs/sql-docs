---
description: "catalog.set_folder_description (SSISDB Database)"
title: "catalog.set_folder_description (SSISDB Database) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: "language-reference"
ms.assetid: 802416f6-5177-4db5-bca5-976dec5faf53
author: chugugrace
ms.author: chugu
---
# catalog.set_folder_description (SSISDB Database)

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Sets the description of a folder in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
## Syntax  
  
```sql  
catalog.set_folder_description [ @folder_name = ] folder_name  
    , [ @folder_description = ] folder_description  
```  
  
## Arguments  
 [ @folder_name = ] *folder_name*  
 The name of the folder. The *folder_name* is **nvarchar(128)**.  
  
 [ @folder_description = ] *folder_description*  
 The description of the folder. The *folder_description* is **nvarchar(MAX)**.  
  
## Return Code Value  
 None  
  
## Result Sets  
 None  
  
## Permissions  
 This stored procedure requires one of the following permissions:  
  
-   Membership to the **ssis_admin** database role  
  
-   Membership to the **sysadmin** server role  
  
## Errors and Warnings  
 The stored procedure returns a message to confirm the setting of new folder description.  
  
  
