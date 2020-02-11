---
title: "Install SQL Server 2014 Servicing Updates | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: install
ms.topic: conceptual
ms.assetid: 7d6c962b-c8d0-49f7-a2ac-00ad8dca930a
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Install SQL Server 2014 Servicing Updates
  This topic provides information about installing updates for [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. This section provides information about the following:  
  
-   Installing updates for [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] during a new installation  
  
-   Installing updates for [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] after it has already been installed.  
  
 We recommend that customers evaluate and install latest [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] updates in a timely manner to make sure that systems are up-to-date with the most recent security updates.  
  
## Installing Updates for [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] During a New Installation  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup integrates the latest product updates with the main product installation so that the main product and its applicable updates are installed at the same time. Product Update can search for the applicable updates from:  
  
-   [!INCLUDE[msCoName](../../includes/msconame-md.md)] Update  
  
-   Windows Server Update Services (WSUS)  
  
-   A local folder  
  
-   A network share  
  
 After Setup finds the latest versions of the applicable updates, it downloads and integrates them with the current [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup process. Product Update can include a cumulative update, service pack, or service pack plus cumulative update. Product Update functionality is an extension of the [Slipstream Functionality](https://go.microsoft.com/fwlink/?LinkId=219945) that was available in [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] PCU1.  
  
## Installing Updates for [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] after it has already been installed  
 On an installed instance of [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], we recommend that you apply all available updates: General Distribution Releases (GDR - security/critical updates), Service Packs (SP), as well as the latest available Cumulative Update (CU).  
  
 Depending on the type of servicing release, SQL Server updates are available via Microsoft Update (MU), the Microsoft Download Center, and/or the Customer Support Services hotfix Server. Security and Critical updates for SQL Server are automatically provided by Microsoft Update (to be able to see these updates you need to opt-in to MU through Windows Update in Control panel). Service Packs are available on MU as Optional/Important downloads as well as the Download Center. Cumulative updates are available on the Microsoft hotfix download server provided in CU Knowledge Base articles.  
  
## See Also  
 [Install SQL Server 2014 from the Installation Wizard &#40;Setup&#41;](install-sql-server-from-the-installation-wizard-setup.md)   
 [Installing updates from the command prompt](installing-updates-from-the-command-prompt.md)
 [Add Features to an Instance of SQL Server 2014 &#40;Setup&#41;](add-features-to-an-instance-of-sql-server-setup.md)   
 [Drop a SQL Server 2014 Installation](repair-a-failed-sql-server-installation.md)  
  
  
