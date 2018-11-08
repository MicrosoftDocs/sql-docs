---
title: "Resolving Upgrade Issues | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "installing Upgrade Advisor"
  - "Upgrade Advisor [SQL Server], reference"
  - "component issue resolution [Upgrade Advisor]"
  - "resolving upgrade issues"
  - "upgrade blocks [Upgrade Advisor]"
  - "detecting upgrade issues"
  - "finding upgrade issues"
  - "Upgrade Advisor [SQL Server], blocking issues"
  - "configurations preventing upgrading [Upgrade Advisor]"
  - "locating upgrade issues"
  - "blocking issues [Upgrade Advisor]"
  - "issues preventing upgrading [Upgrade Advisor]"
  - "Setup [Upgrade Advisor]"
  - "SQL Server Upgrade Advisor, reference"
  - "analyzing system [Upgrade Advisor], resolving issues"
  - "settings preventing upgrading [Upgrade Advisor]"
  - "upgrade issue reference [Upgrade Advisor]"
  - "identifying upgrade issues"
  - "fixing upgrade issues [Upgrade Advisor]"
ms.assetid: 113eb435-8d36-4ed6-9790-b5e4c14809c8
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Resolving Upgrade Issues
  The topics in this section describe upgrade issues that can be detected as well as those that cannot be detected, but that might affect the upgrade or post-upgrade experience. The issues are organized by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] component.  
  
## In This Section  
  
-   [Late-Breaking Upgrade Issues](../../../2014/sql-server/install/late-breaking-upgrade-issues.md)  
  
-   [Database Engine Upgrade Issues](../../../2014/sql-server/install/database-engine-upgrade-issues.md)  
  
-   [Full-Text Search Upgrade Issues](../../../2014/sql-server/install/full-text-search-upgrade-issues.md)  
  
-   [Replication Upgrade Issues](../../../2014/sql-server/install/replication-upgrade-issues.md)  
  
-   [Reporting Services Upgrade Issues &#40;Upgrade Advisor&#41;](../../../2014/sql-server/install/reporting-services-upgrade-issues-upgrade-advisor.md)  
  
-   [SQL Server Agent Upgrade Issues](../../../2014/sql-server/install/sql-server-agent-upgrade-issues.md)  
  
## Issues That Prevent Upgrading  
 A few configurations or settings in a previous version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can prevent you from upgrading to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. If Setup detects these issues when you install [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], Setup stops the upgrade process and requests that you run Upgrade Advisor and fix any blocking issues.  
  
### [!INCLUDE[ssDE](../../includes/ssde-md.md)]  
 If the following tasks are listed on the [!INCLUDE[ssDE](../../includes/ssde-md.md)] upgrade report, you must perform the required actions before you upgrade to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]:  
  
-   [Detach database ID 32767](../../../2014/sql-server/install/detach-database-id-32767.md)  
  
-   [Rename logins matching fixed server role names](../../../2014/sql-server/install/rename-logins-matching-fixed-server-role-names.md)  
  
-   [Rename user sys](../../../2014/sql-server/install/rename-user-sys.md)  
  
-   [Use sp_rename to rename duplicate index name](../../../2014/sql-server/install/use-sp-rename-to-rename-duplicate-index-name.md)  
  
## See Also  
 [SQL Server 2014 Upgrade Advisor &#91;new&#93;](/sql/2014/sql-server/install/sql-server-2014-upgrade-advisor)  
  
  
