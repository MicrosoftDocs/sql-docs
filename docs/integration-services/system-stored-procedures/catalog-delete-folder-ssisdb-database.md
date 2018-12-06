---
title: "catalog.delete_folder (SSISDB Database) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "language-reference"
ms.assetid: b9c08992-500c-447e-bc19-1eb13c9b0293
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# catalog.delete_folder (SSISDB Database)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Deletes a folder from the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog.  
  
## Syntax  
  
```sql  
delete_folder [ @folder_name = ] folder_name  
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
  
  
