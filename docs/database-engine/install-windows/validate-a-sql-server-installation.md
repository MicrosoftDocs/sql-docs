---
title: "Validate a SQL Server Installation"
description: The SQL Server discovery report can be used to verify the version of SQL Server and the SQL Server features installed on the computer.
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: install
ms.topic: conceptual
helpviewer_keywords:
  - "validating installations [SQL Server]"
monikerRange: ">=sql-server-2016"
---
# Validate a SQL Server Installation

[!INCLUDE [SQL Server -Windows Only](../../includes/applies-to-version/sql-windows-only.md)]
  
  The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] discovery report can be used to verify the version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] features installed on the computer. The **Installed [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] features discovery report** displays a report of all [!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)], [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)], [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)], [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)], and [!INCLUDE[sssql17-md](../../includes/sssql17-md.md)] products and features that are installed on the local server. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] features discovery report is available on the **Tools** page on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Installation center.  
  
 ## Run [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] features discovery report  
  
 Launch the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Installation center, using the **Start** menu, point to **All Programs**, point to **[!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] \<Version Name>**, point to **Configuration Tools**, and click **[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Installation Center**. To run the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] features discovery report, click **Tools** in the left-hand navigation area of **[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Installation Center**, and then click **Installed [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] features discovery report**.  
  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] discovery report is saved to %ProgramFiles%\\[!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\\*nnn*\Setup Bootstrap\Log\\<last Setup Session\>.  
  
 You can also generate the discovery report through the command line. Run "Setup.exe /Action=RunDiscovery" from a command prompt If you add "/q" to the command line above no UI will be shown, but the report will still be created in %ProgramFiles%\\[!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\\*nnn*\Setup Bootstrap\Log\\<last Setup Session\>.  
  
## See also  
 [View and Read SQL Server Setup Log Files](../../database-engine/install-windows/view-and-read-sql-server-setup-log-files.md)  
  
  
