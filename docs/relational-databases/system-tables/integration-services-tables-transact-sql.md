---
title: "Integration Services Tables (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "SQL Server Integration Services system tables"
  - "system tables [SQL Server], Integration Services"
  - "system tables [Integration Services]"
  - "SSIS, system tables"
ms.assetid: 683b181b-0091-4a9c-86db-bc577af43cec
author: leolimsft
ms.author: lle
manager: craigg
---
# Integration Services Tables (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  The topics in this section describe the system tables in the msdb database that store information used by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)].  
  
## In This Section  
 [sysssislog](../../relational-databases/system-tables/sysssislog-transact-sql.md)  
 Contains one row for each log entry that an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package generates at run time.  
  
 This table is used only when packages use the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] log provider.  
  
 [sysssispackagefolders](../../relational-databases/system-tables/sysssispackagefolders-transact-sql.md)  
 Contains one row for each logical folder that the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service uses to organize packages. Column values define the parent/child relationships between nested folders.  
  
> [!NOTE]  
>  [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] displays stored packages in a hierarchical view when you connect to the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service.  
  
 [sysssispackages](../../relational-databases/system-tables/sysssispackages-transact-sql.md)  
 Contains one row for each [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package.  
  
 This table is used only when you store packages in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
  
