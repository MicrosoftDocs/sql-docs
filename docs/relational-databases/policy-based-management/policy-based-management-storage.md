---
description: "Policy-Based Management Storage"
title: "Policy-Based Management Storage | Microsoft Docs"
ms.custom: ""
ms.date: "08/09/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: security
ms.topic: conceptual
helpviewer_keywords: 
  - "Policy-Based Management, storage"
ms.assetid: d0cbf214-fc2e-4917-8d31-1d71c9ffa61d
author: VanMSFT
ms.author: vanto
---
# Policy-Based Management Storage
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  Policies are stored in the msdb database. After a policy or condition is changed, msdb should be backed up. For more information, see [Back Up and Restore of System Databases &#40;SQL Server&#41;](../../relational-databases/backup-restore/back-up-and-restore-of-system-databases-sql-server.md).  
  
## Storing Policies  
 [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] includes policies that can be used to monitor an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. By default, these policies are not installed on the [!INCLUDE[ssDE](../../includes/ssde-md.md)]. However, you can import them from their default installation location. SQL Server Policies are stored in their respective version folder. For example, for SQL Server 2019, SQL Server Policies are stored in C:\Program Files (x86)\Microsoft SQL Server\150\Tools\Policies\DatabaseEngine\1033. 
  
 You can directly create policies by using the **File/New** menu, and then saving them to a file. This enables you to create policies when you are not connected to an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
 Policy history for policies evaluated in the current instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] is maintained in msdb system tables. Policy history for policies applied to other instances of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] or applied to [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] or [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] is not retained.  
  
  
