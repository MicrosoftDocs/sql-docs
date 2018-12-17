---
title: "Upgrade Integration Services | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "Integration Services, upgrading"
  - "SSIS, upgrading"
  - "SQL Server Integration Services, upgrading"
  - "upgrading Integration Services"
ms.assetid: 04f9863c-ba0b-47c5-af91-f2d41b078a23
author: "MikeRayMSFT"
ms.author: "mikeray"
manager: "erikre"
---
# Upgrade Integration Services
  If [!INCLUDE[ssISversion10](../../includes/ssisversion10-md.md)] or later is currently installed on your computer, you can upgrade to [!INCLUDE[ssISCurrent](../../includes/ssiscurrent-md.md)].  
  
 When you upgrade to [!INCLUDE[ssISCurrent](../../includes/ssiscurrent-md.md)] on a machine that has one of these earlier versions of [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] installed, [!INCLUDE[ssISCurrent](../../includes/ssiscurrent-md.md)] is installed side-by-side with the earlier version.  
  
 With this side-by-side install, multiple versions of dtexec utility are installed. To ensure that you run the correct version of the utility, at the command prompt run the utility by entering the full path (\<drive>:\Program Files\Microsoft SQL Server\\<version\>\DTS\Binn). For more information about dtexec, see [dtexec Utility](../../integration-services/packages/dtexec-utility.md).  
  
> [!NOTE]  
>  In previous versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], by default when you installed [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] all users in the Users group had access to the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service. When you install [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], users do not have access to the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service. The service is secure by default. After [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] is installed, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] administrator must run the DCOM Configuration tool (Dcomcnfg.exe) to grant specific users access to the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service. For more information, see [Integration Services Service &#40;SSIS Service&#41;](../../integration-services/service/integration-services-service-ssis-service.md).  
  
## Before Upgrading Integration Services  
 We recommended that you run Upgrade Advisor before you upgrade to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. Upgrade Advisor reports issues that you might encounter if you migrate existing [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] packages to the new package format that [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] uses.  
  
> [!NOTE]
>  Support for migrating or running Data Transformation Services (DTS) packages has been discontinued in SQL Server 2012. The following DTS functionality has been discontinued.  
> 
>  -   DTS runtime  
> -   DTS API  
> -   Package Migration Wizard for migrating DTS packages to the next version of [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]  
> -   Support for DTS package maintenance in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]  
> -   Execute DTS 2000 Package task  
> -   Upgrade Advisor scan of DTS packages.  
> 
>  For information about other discontinued features, see [Discontinued Integration Services Functionality in SQL Server 2016](https://msdn.microsoft.com/library/5ee40ceb-37b9-47a9-b90d-ce1de74b10f7).  
  
## Upgrading Integration Services  
 You can upgrade by using one of the following methods:  
  
-   Run [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] Setup and select the option to **Upgrade from SQL Server 2008, SQL Server 2008 R2, [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], or [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]**.  
  
-   Run **setup.exe** at the command prompt and specify the **/ACTION=upgrade** option. For more information, see the section, "Installation Scripts for [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]," in [Install SQL Server 2016 from the Command Prompt](../../database-engine/install-windows/install-sql-server-2016-from-the-command-prompt.md).  
  
 You cannot use upgrade to perform the following actions:  
  
-   Reconfigure an existing installation of [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)].  
  
-   Move from a 32-bit to a 64-bit version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or from a 64-bit version to a 32-bit version.  
  
-   Move from one localized version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to another localized version.  
  
 When you upgrade, you can upgrade both [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] and the [!INCLUDE[ssDE](../../includes/ssde-md.md)], or just upgrade the [!INCLUDE[ssDE](../../includes/ssde-md.md)], or just upgrade [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]. If you upgrade only the [!INCLUDE[ssDE](../../includes/ssde-md.md)], [!INCLUDE[ssISversion10](../../includes/ssisversion10-md.md)] or later remains functional, but you do not have the functionality of [!INCLUDE[ssISCurrent](../../includes/ssiscurrent-md.md)]. If you upgrade only [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)], [!INCLUDE[ssISCurrent](../../includes/ssiscurrent-md.md)] is fully functional, but can only store packages in the file system, unless an instance of the [!INCLUDE[ssDECurrent](../../includes/ssdecurrent-md.md)] is available on another computer.  
  
## Upgrading Both Integration Services and the Database Engine to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]  
 This section describes the effects of performing an upgrade that has the following criteria:  
  
-   You upgrade both [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] and an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
-   Both [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] and the instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] are on the same computer.  
  
### What the Upgrade Process Does  
 The upgrade process does the following tasks:  
  
-   Installs the [!INCLUDE[ssISCurrent](../../includes/ssiscurrent-md.md)] files, service, and tools ([!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] and [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)]). When there are multiple instances of [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)], [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], or [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] on the same computer, the first time you upgrade any of the instances to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], the [!INCLUDE[ssISCurrent](../../includes/ssiscurrent-md.md)] files, service, and tools are installed.  
  
-   Upgrades the instance of the [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)], [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], or [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)][!INCLUDE[ssDE](../../includes/ssde-md.md)] to the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] version.  
  
-   Moves data from the [!INCLUDE[ssISversion10](../../includes/ssisversion10-md.md)] or later system tables to the [!INCLUDE[ssISCurrent](../../includes/ssiscurrent-md.md)] system tables, as follows:  
  
    -   Moves packages without change from the msdb.dbo.sysdtspackages90 system table to the msdb.dbo.sysssispackages system table.  
  
        > [!NOTE]  
        >  Although the data moves to a different system table, the upgrade process does not migrate packages to the new format.  
  
    -   Moves folder metadata from the msdb.sysdtsfolders90 system table to the msdb.sysssisfolders system table.  
  
    -   Moves log data from the msdb.sysdtslog90 system table to the msdb.sysssislog system table.  
  
-   Removes the msdb.sysdts*90 system tables and the stored procedures that are used to access them after moving the data to the new msdb.sysssis\* tables. However, upgrade replaces the sysdtslog90 table with a view that is also named sysdtslog90. This new sysdtslog90 view exposes the new msdb.sysssislog system table. This ensures that reports based on the log table continue to run without interruption.  
  
-   To control access to packages, creates three new fixed database-level roles: db_ssisadmin, db_ssisltduser, and db_ssisoperator. The [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)][!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] roles of db_dtsadmin, db_dtsltduser, and db_dtsoperator are not removed, but are made members of the corresponding new roles.  
  
-   If the [!INCLUDE[ssIS](../../includes/ssis-md.md)] package store (that is, the file system location managed by the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service) is the default location under **\SQL Server\90**, **\SQL Server\100**, **\SQL Server\110**, or **\SQL Server\120** moves those packages to the new default location under **\SQL Server\130**.  
  
-   Updates the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service configuration file to point to the upgraded instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
### What the Upgrade Process Does Not Do  
 The upgrade process does not do the following tasks:  
  
-   **Does not** remove the [!INCLUDE[ssISversion10](../../includes/ssisversion10-md.md)] or later service.  
  
-   Does not migrate existing [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] packages to the new package format that [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] uses. For information about how to migrate packages, see [Upgrade Integration Services Packages](../../integration-services/install-windows/upgrade-integration-services-packages.md).  
  
-   Does not move packages from file system locations, other than the default location, that have been added to the service configuration file. If you have previously edited the service configuration file to add more file system folders, packages that are stored in those folders will not be moved to a new location.  
  
-   In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job steps that call the **dtexec** utility (dtexec.exe) directly, does not update the file system path for the **dtexec** utility. You have to edit these job steps manually to update the file system path to specify the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] location for the **dtexec** utility.  
  
### What You Can Do After Upgrading  
 After the upgrade process finishes, you can do the following tasks:  
  
-   Run [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent jobs that run packages.  
  
-   Use [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] to manage [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] packages that are stored in an instance of [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)], [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], or [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]. You need to modify the service configuration file to add the instance of [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)], [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], or [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] to the list of locations managed by the service.  
  
    > [!NOTE]  
    >  Early versions of [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] cannot connect to [!INCLUDE[ssISCurrent](../../includes/ssiscurrent-md.md)] Service.  
  
-   Identify the version of packages in the msdb.dbo.sysssispackages system table by checking the value in the packageformat column. The table has a packageformat column that identifies the version of each package. A value of 3 indicates a [!INCLUDE[ssISversion10](../../includes/ssisversion10-md.md)] package. Until you migrate packages to the new package format, the value in the packageformat column does not change.  
  
-   You cannot use the [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)], [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], or [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] tools to design, run, or manage [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] packages. The [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)], [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], or [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] tools include the respective versions of [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Import and Export Wizard, and the Package Execution Utility (dtexecui.exe). The upgrade process does not remove the [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)], [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], or [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]tools. However, you will not able to use these tools to continue to work with [!INCLUDE[ssISversion10](../../includes/ssisversion10-md.md)] or later packages on a server that has been upgraded.  
  
-   By default, in an upgrade installation, [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] is configured to log events that are related to the running of packages to the Application event log. This setting might generate too many event log entries when you use the Data Collector feature of [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. The events that are logged include EventID 12288, "Package started," and EventID 12289, "Package finished successfully." To stop logging these two events to the Application event log, open the registry for editing. Then in the registry, locate the HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\130\SSIS node, and change the DWORD value of the LogPackageExecutionToEventLog setting from 1 to 0.  
  
## Upgrading only the Database Engine to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]  
 This section describes the effects of performing an upgrade that has the following criteria:  
  
-   You upgrade only an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)]. That is, the instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] is now an instance of [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], but the instance of [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] and the client tools are from [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)], [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], or [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)].  
  
-   The instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] is on one computer, and [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] and the client tools are on another computer.  
  
### What You Can Do After Upgrading  
 The system tables that store packages in the upgraded instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] are not the same as those used in [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)]. Therefore, the [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] versions of [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] and [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)] cannot discover the packages in the system tables on the upgraded instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)]. Because these packages cannot be discovered, there are limitations on what you can do with those packages:  
  
-   You cannot use the [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] tools, [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] and [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)], on other computers to load or manage packages from the upgraded instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
    > [!NOTE]  
    >  Although the packages in the upgraded instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] have not yet been migrated to the new package format, they are not discoverable by the [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] tools. Therefore, the packages cannot be used by the [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] tools.  
  
-   You cannot use [!INCLUDE[ssISversion10](../../includes/ssisversion10-md.md)] on other computers to run packages that are stored in msdb on the upgraded instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
-   You cannot use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent jobs on [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] computers to run [!INCLUDE[ssISversion10](../../includes/ssisversion10-md.md)] packages that are stored in the upgraded instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
## External Resources  
 Blog entry, [Making your Existing Custom SSIS Extensions and Applications Work in Denali](https://go.microsoft.com/fwlink/?LinkId=238157), on blogs.msdn.com.  
  
  
