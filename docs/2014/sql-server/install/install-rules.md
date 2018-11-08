---
title: "Install Rules | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
f1_keywords: 
  - "SCC"
  - "System Configuration Check, Setup"
helpviewer_keywords: 
  - "System Configuration Check page [SQL Server Installation Wizard]"
  - "SQL Server Installation Wizard, System Configuration Check page"
  - "SCC [SQL Server]"
ms.assetid: 168c0445-5651-42fc-91f4-d9f27d9e2281
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Install Rules
  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup validates your computer configuration before the Setup operation completes. During [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup, the System Configuration Checker (SCC) scans the computer where [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will be installed. The SCC checks for conditions that prevent a successful [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup operation. Before Setup starts the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Installation Wizard, the SCC retrieves the status of each item. It then compares the result with required conditions and provides guidance for removal of blocking issues.  
  
 The system configuration check generates a report which contains a short description for each executed rule, and the execution status. The system configuration check report is located at %programfiles%\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\120\Setup Bootstrap\Log\\<YYYYMMDD_HHMM>\\.  
  
 Before you run the setup operation, review the following topics:  
  
1.  [Hardware and Software Requirements for Installing SQL Server 2014](hardware-and-software-requirements-for-installing-sql-server.md)  
  
2.  [Features Supported by the Editions of SQL Server 2014](../../../2014/getting-started/features-supported-by-the-editions-of-sql-server-2014.md)  
  
3.  [Security Considerations for a SQL Server Installation](../../../2014/sql-server/install/security-considerations-for-a-sql-server-installation.md)  
  
4.  [Local Language Versions in SQL Server](../../../2014/sql-server/install/local-language-versions-in-sql-server.md)  
  
 Other references:  
  
1.  [Supported Version and Edition Upgrades](../../database-engine/install-windows/supported-version-and-edition-upgrades.md)  
  
2.  [Before Installing Failover Clustering](../failover-clusters/install/before-installing-failover-clustering.md)  
  
## Additional Rule topics  
 See the following topics for scenario-specific Setup rules:  
  
-   [Installation Rules](../../../2014/sql-server/install/installation-rules.md)  
  
-   [Feature Rules &#40;Upgrade&#41;](../../../2014/sql-server/install/feature-rules-upgrade.md)  
  
-   [Edition Upgrade Rules](../../../2014/sql-server/install/edition-upgrade-rules.md)  
  
-   [Uninstallation rules](../../../2014/sql-server/install/uninstallation-rules.md)  
  
-   [Prepare Image Rules](../../../2014/sql-server/install/prepare-image-rules.md)  
  
-   [Complete Image Rules](../../../2014/sql-server/install/complete-image-rules.md)  
  
  
