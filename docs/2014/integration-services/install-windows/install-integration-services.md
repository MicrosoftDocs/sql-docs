---
title: "Install Integration Services | Microsoft Docs"
ms.custom: ""
ms.date: "05/24/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "Integration Services, installing"
  - "SSIS, installing"
  - "installing Integration Services, about installing Integration Services"
  - "SQL Server Integration Services, installing"
  - "Setup [Integration Services], about installing Integration Services"
  - "installing Integration Services"
  - "Setup [Integration Services]"
ms.assetid: bd20fd3a-414b-4581-959d-ebba4ddf5a55
author: janinezhang
ms.author: janinez
manager: craigg
---
# Install Integration Services
  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides a single Setup program to install any or all of its components, including [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]. Through Setup, you can install [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] with or without other [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] components on a single computer.  
  
 This topic highlights important considerations that you should know before you install [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]. Information in this topic will help you evaluate the installation options so that you can make selections that result in a successful installation.  
  
 This topic does not include instructions for starting Setup, using the Setup Wizard, or running Setup from the command line. For step-by-step instructions on how to start Setup and select components to install, see [Quick-Start Installation of SQL Server 2014](../../getting-started/quick-start-installation-of-sql-server-2014.md). For information about command-line options for installing [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)], see [Install SQL Server 2014 from the Command Prompt](../../database-engine/install-windows/install-sql-server-from-the-command-prompt.md).  
  
## Preparing to Install Integration Services  
 Before you install [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)], review the following requirements:  
  
-   [Hardware and Software Requirements for Installing SQL Server 2014](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md)  
  
-   [Check Parameters for the System Configuration Checker](../../database-engine/install-windows/check-parameters-for-the-system-configuration-checker.md)  
  
-   [Security Considerations for a SQL Server Installation](../../sql-server/install/security-considerations-for-a-sql-server-installation.md)  
  
## Selecting an Integration Services Configuration  
 You can install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)][!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] in the following configurations:  
  
-   You can install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)][!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] on a computer that has no previous instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
-   You can install [!INCLUDE[ssISCurrent](../../includes/ssiscurrent-md.md)] side-by-side with an existing instance of [!INCLUDE[ssISversion10](../../includes/ssisversion10-md.md)] and [!INCLUDE[ssISversion11](../../includes/ssisversion11-md.md)].  
  
     When you upgrade to [!INCLUDE[ssISCurrent](../../includes/ssiscurrent-md.md)] on a machine that has one of these earlier versions of [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] already installed, [!INCLUDE[ssISCurrent](../../includes/ssiscurrent-md.md)] is installed side-by-side with the earlier version.  
  
     For more information on upgrading [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)], see [Upgrade Integration Services](upgrade-integration-services.md). For information about backward compatibility with earlier versions of [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)], see [Integration Services Backward Compatibility](../integration-services-backward-compatibility.md).  
  
## Installing Integration Services  
 After you review the installation requirements for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and ensure that your computer meets those requirements, you are ready to install [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)].  
  
> [!NOTE]  
>  In previous versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], by default when you installed [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] all users in the Users group had access to the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service. When you install [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], users do not have access to the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service. The service is secure by default. After [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is installed, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] administrator must run the DCOM Configuration tool (Dcomcnfg.exe) to grant specific users access to **SQL Server Integration Services 12.0**.  
>   
>  For instructions on how to grant permissions, see [Grant Permissions to Integration Services Service](../grant-permissions-to-integration-services-service.md).  
  
 If you are using the Setup Wizard to install [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)], you will use a series of pages to specify components and options. The following are pages in the Setup Wizard where the options that you select affect your installation of [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] with selection recommendations:  
  
-   **Feature Selection**  
  
     Select **Integration Services** to install the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service and to run packages outside the design environment.  
  
     For a complete installation of [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)], together with the tools and documentation for developing and managing packages, select both **Integration Services** and the following **Shared Features**:  
  
    -   **SQL Server Data Tools** to install the tools for designing packages.  
  
    -   **Management Tools - Complete** to install [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] for managing packages.  
  
    -   **Client Tools SDK** to install managed assemblies for [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] programming.  
  
     Many data warehousing solutions also require the installation of additional [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] components, such as the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].  
  
    > [!NOTE]  
    >  Some [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] components that you can select for installation on the **Feature Selection** page of the Setup Wizard install a partial subset of [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] components. These components are useful for specific tasks, but the functionality of [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] will be limited. For example, the **Database Engine Services** option installs the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] components required for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Import and Export Wizard. The **SQL Server Data Tools** option installs the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] components required to design a package, but the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service is not installed and you cannot run packages outside of [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)]. To ensure a complete installation of [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)], you must select **Integration Services** on the **Feature Selection** page.  
  
     **Installing on a 64-bit Computer** On a 64-bit computer, selecting **Integration Services** installs only the 64-bit runtime and tools. If you have to run packages in 32-bit mode, you must also select an additional option to install the 32-bit runtime and tools:  
  
    -   If the 64-bit computer is running the x86 operating system, select **SQL Server Data Tools** or **Management Tools - Complete**.  
  
    -   If the 64-bit computer is running the [!INCLUDE[vcpritanium](../../includes/vcpritanium-md.md)] operating system, select **Management Tools - Complete**.  
  
     **Installing on a Dedicated Server for ETL** To use a dedicated server for extraction, transformation, and loading (ETL) processes, we recommend that you install a local instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] when you install [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]. [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] typically stores packages in an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] and relies on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent for scheduling those packages. If the ETL server does not have an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)], you will have to schedule or run packages from a server that does have an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)]. This means that the packages will not be running on the ETL server, but instead on the server from which they were started. As a result, the resources of the dedicated ETL server are not being used as intended. Furthermore, the resources of other servers may be strained by the running ETL processes  
  
-   **Instance Configuration**  
  
     Any selection that you make on the **Instance Configuration** page does not affect [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] or the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service.  
  
     You can only install one instance of the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service on a computer. You connect to the service by using the computer name.  
  
     By default, the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service is configured to manage packages that are stored in the **msdb** database in the instance of the Database Engine that is installed at the same time as [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]. If an instance of the Database Engine is not installed at the same time as [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)], the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service is configured to manage packages that are stored in the **msdb** database of the local, default instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)]. To manage packages that are stored in a named instance or a remote instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)], or in multiple instances of the [!INCLUDE[ssDE](../../includes/ssde-md.md)], you have to modify the configuration file. For more information about how to modify this configuration file, see [Configuring the Integration Services Service &#40;SSIS Service&#41;](../service/integration-services-service-ssis-service.md).  
  
-   **Server Configuration**  
  
     Review the settings for the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service on the **Service Accounts** tab of the **Server Configuration** page.  
  
     If Windows 7 or Windows Server 2008 R2 is installed, the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service is registered to run under the NT Services\MsDtsServer120 virtual account, and the **Startup Type** is **Automatic**.  You do not have to enter a password for the virtual account. If Microsoft Vista or Windows Server 2008 is installed, the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service is registered to run under the built-in Network Service account, and the **Startup Type** is **Automatic**. You do not have to enter a password for the built-in Network Service account.  
  
 By default, in a new installation, [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] is configured not to log events that are related to the running of packages to the Application event log. This setting prevents too many event log entries when you use the Data Collector feature of [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. The events that are not logged are EventID 12288, "Package started," and EventID 12289, "Package finished successfully." To log these events to the Application event log, open the registry for editing. Then, in the registry, locate the HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\120\SSIS node, and change the DWORD value of the LogPackageExecutionToEventLog setting from 0 to 1.  
  
## Understanding the Integration Services Service  
 [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] installs the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service.  
  
 The [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service is installed when you select the **Integration Services** option on the **Feature Selection** page. When you accept the default settings on the **Server Configuration** page, the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service is enabled and its **Startup Type** is **Automatic**.  
  
 You can only install a single instance of the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service on a computer. The service is not specific to a particular instance of the Database Engine. You connect to the service by using the name of the computer on which the service is running.  
  
## Installing Integration Services on 64-bit Computers  
  
### Integration Services Features Installed on 64-bit Computers  
 Setup installs various [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] features based on the setup options that you select:  
  
-   When you install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and select [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] for installation, Setup installs all available 64-bit [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] features and tools.  
  
-   If you require [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] design-time features, you must also install [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)].  
  
-   If you require 32-bit versions of the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] runtime and tools to run certain packages in 32-bit mode, you must also install [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)].  
  
 64-bit features are installed under the **Program Files** directory, and 32-bit features are installed separately under the **Program Files (x86)** directory. (This behavior is not specific to [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] or to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].)  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], the 32-bit development environment for [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] packages, is not supported on the [!INCLUDE[vcpritanium](../../includes/vcpritanium-md.md)] 64-bit operating system and is not installed on [!INCLUDE[vcpritanium](../../includes/vcpritanium-md.md)] servers.  
  
  
