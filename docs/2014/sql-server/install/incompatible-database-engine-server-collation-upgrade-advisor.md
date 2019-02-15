---
title: "Incompatible Database Engine Server Collation (Upgrade Advisor) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
ms.assetid: 80f499d6-2c90-49eb-a5b3-0bb5b7faaa3b
author: markingmyname
ms.author: maghan
manager: craigg
---
# Incompatible Database Engine Server Collation (Upgrade Advisor)
  Upgrade Advisor detected [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] is using an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] that is configured to use an incompatible server collation.  
  
||  
|-|  
|**[!INCLUDE[applies](../../includes/applies-md.md)]**  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint mode.|  
  
## Component  
 [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]  
  
## Description  
 Upgrade Advisor detected [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] is using an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] that is configured to use an incompatible server collation.  
  
 [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint mode utilizes the SharePoint shared services architecture. SharePoint does not support [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] configured for case sensitive or server collations or binary server collations. Incompatible collations include collations that are by default case sensitive or binary and a base collation that by default is compatible but has been configured with any of the following collation designators:  
  
-   **Binary**  
  
-   **Case-sensitive**  
  
-   **Binary-codepoint**  
  
 Because the current [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] server collation is incompatible, upgrade is blocked.  
  
## Corrective Action  
 The [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] server collation property cannot be changed. You will not be able to complete an upgrade of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. You will need to migrate your [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] installation to a new server which is using a compatible server collation. For more information, see the following:  
  
-   [Upgrade and Migrate Reporting Services](https://go.microsoft.com/fwlink/?LinkId=233227)  
  
-   [Selecting a SQL Server Collation](https://go.microsoft.com/fwlink/?LinkId=233226)  
  
  
