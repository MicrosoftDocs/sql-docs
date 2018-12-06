---
title: "Database maintenance plans superseded | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "suspended database maintenance plans"
  - "database maintenance plans [SQL Server Agent]"
  - "maintenance plans [SQL Server Agent]"
ms.assetid: efac127c-6c81-4c7a-a6c4-9aae5d15545d
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Database maintenance plans superseded
    
## Component  
 [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent  
  
## Description  
 Existing database maintenance plans are upgraded and continue to work. However, you will not be able to create new database maintenance plans by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. To view the maintenance plans in Object Explorer, expand Management, and then expand Legacy. You can migrate existing database maintenance plans to the new format by selecting **Migrate** from the context menu for any database maintenance plan. Because the new maintenance plan feature is not a direct replacement of database maintenance plans, some functionality might be lost after migration. Migrating a database maintenance plan does not delete the old plan, so you can test its functionality as a maintenance plan before removing the old plan.  
  
 The following features are no longer supported within database maintenance plans:  
  
-   Log shipping  
  
-   The **Attempt to repair any minor problems** option of the **Database Integrity Check** task  
  
## Corrective Action  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] prevents log shipping from being included in a database maintenance plan. For more information, see "Log Shipping" in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
  
