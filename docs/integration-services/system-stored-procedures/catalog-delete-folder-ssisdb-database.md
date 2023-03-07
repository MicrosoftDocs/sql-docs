---
title: "catalog.delete_folder (SSISDB Database)"
description: "catalog.delete_folder (SSISDB Database)"
author: chugugrace
ms.author: chugu
ms.date: "03/03/2017"
ms.service: sql
ms.subservice: integration-services
ms.topic: "language-reference"
---
# catalog.delete_folder (SSISDB Database)

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Deletes a folder from the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
## Syntax  
  
```sql  
catalog.delete_folder [ @folder_name = ] folder_name  
```  
  
## Arguments  
 [ @folder_name = ] *folder_name*  
 The name of the folder that is to be deleted. The *folder_name* is **nvarchar(128)**.  
  
## Return Code Value  
 None  
  
## Result Sets  
 None  
  
## Permissions  
 This stored procedure requires one of the following permissions:  
  
-   Membership to the **ssis_admin** database role  
  
-   Membership to the **sysadmin** server role  
  
## Errors and Warnings  
 Returns a message to confirm the deletion of the folder.  
  
  
