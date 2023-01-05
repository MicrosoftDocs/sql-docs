---
title: "sysssispackagefolders (Transact-SQL)"
description: sysssispackagefolders (Transact-SQL)
author: lrtoyou1223
ms.author: lle
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sysdtspackagefolders90"
  - "sysdtspackagefolders90_TSQL"
helpviewer_keywords:
  - "sysssispackagefolders system table"
dev_langs:
  - "TSQL"
ms.assetid: ddc4833f-27bf-4610-b739-d257961d17ac
---
# sysssispackagefolders (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Contains one row for each logical folder in the folder hierarchy that [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] uses. These folders are listed in Object Explorer of [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] when you connect to [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]. A folder lists packages that are saved to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or to the file system.  
  
 The **parentfolderid** column describes the folder hierarchy. The folder at the top of the folder hierarchy contains a null value in **parentfolderid**.  
  
 The **foldername** column contains the name of folders that appear in Object Explorer.  
  
 This table is stored in the **msdb** database.  

  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**folderid**|**uniqueidentifier**|The GUID of the folder.|  
|**parentfolderid**|**uniqueidentifier**|The GUID of the folder that is the parent folder.|  
|**foldername**|**sysname**|The name of the folder. This name appears in the folder hierarchy in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].|  
  
  
