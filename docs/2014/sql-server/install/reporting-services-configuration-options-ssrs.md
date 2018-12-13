---
title: "Reporting Services Configuration Options (SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
f1_keywords: 
  - "sql12.ins.instwizard.reportserverinstoptions.f1"
helpviewer_keywords: 
  - "Report Server Installation Options page [SQL Server Installation Wizard]"
  - "report servers [Reporting Services], installing"
  - "SQL Server Installation Wizard, Report Server Installation Options page"
ms.assetid: e4561f6c-bc7f-467e-821a-cde8e5cd7391
author: markingmyname
ms.author: maghan
manager: craigg
---
# Reporting Services Configuration Options (SSRS)
  Use the **Reporting Services Configuration** page of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Installation Wizard to specify how a report server is installed and configured. The availability of an installation option depends on options you chose previously on the **Feature Selection** page and whether you are also installing a local instance of [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] at the same time you are installing the report server.  
  
 In some cases, if a Secure Sockets Layer (SSL) certificate is installed on the computer and is bound to a strong wildcard, Setup will create the Reporting Services URLs using the HTTPS prefix. For more information about how certificates are mapped to Reporting Services URLs, see [Configuring a Report Server for Secure Sockets Layer (SSL) Connections](https://go.microsoft.com/fwlink/?LinkId=199089) (https://go.microsoft.com/fwlink/?LinkId=199089) in SQL Server Books Online.  
  
 For the most recent information regarding [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] and the installation and configuration of this release, see [Additional installation information](https://go.microsoft.com/fwlink/?LinkId=207425) (https://go.microsoft.com/fwlink/?LinkId=207425).  
  
## Options  
  
### Reporting Services Native Mode  
 If Setup cannot perform a default report server configuration because one or more requirements are not met, the Installation Wizard allows only the minimal installation option; copying the files you need, but requiring you to use the Reporting Services Configuration Manager to configure a native mode report server after setup is finished.  
  
> [!NOTE]  
>  An existing report server database file can cause setup to fail if you choose one of the default installation options. When you choose a default installation option, setup attempts to create a report server database using the default name. If a database with that name already exists, setup will fail and you will have to roll back the installation. To avoid this problem, you can rename the existing database before you run setup or choose the **Install only** option so that you can specify custom database settings after setup is finished.  
  
#### Install and configure  
 Installs a report server instance in Native Mode using the default values for the report server databases, service account, and URL reservations. When you choose this option, the report server instance is ready to use when Setup is finished. Setup creates the report server database using a local [!INCLUDE[ssDE](../../includes/ssde-md.md)] instance, and configures a report server to use default values.  
  
 This option is available only if the default values used in a report server installation are valid for your system. This option is recommended for developers who want to install all components locally, and for users who are evaluating the software.  
  
 To view information about the default Settings that Setup uses, or to find out why the default configuration cannot be installed, click **Details**. For more information about the default configuration for a native mode report server, see [Default Configuration for a Native Mode Installation (Reporting Services)](https://go.microsoft.com/fwlink/?LinkId=199091) (https://go.microsoft.com/fwlink/?LinkId=199091).  
  
#### Install only  
 Installs the report server program files, creates the Report Server service account, and registers the report server Windows Management Instrumentation (WMI) provider. This installation option is referred to as a "files-only" installation. Select this option if you do not want to use the default configuration. If the default configuration cannot be installed, or if you are installing a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] failover cluster that includes [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], this is the only option available. For more information about a files-only installation, see [Files-Only Installation (Reporting Services)](https://go.microsoft.com/fwlink/?LinkId=199093) (https://go.microsoft.com/fwlink/?LinkId=199093).  
  
 After Setup completes, you must create the report server database and configure the report server before it can be used. To configure a report server and create the database, use the Reporting Services Configuration Manager. For more information, see [How to: Create a Report Server Database (Reporting Services Configuration)](https://go.microsoft.com/fwlink/?LinkId=199094) (https://go.microsoft.com/fwlink/?LinkId=199094) and [Configuring a Report Server Database Connection](https://go.microsoft.com/fwlink/?LinkId=199095) (https://go.microsoft.com/fwlink/?LinkId=199095).  
  
### Reporting Services SharePoint Mode  
  
#### Install only  
 Installs the report server program files and PowerShell cmdlets. After installation is complete, you will need to start the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint services and create an [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service application. For more information, see, the following:  
  
-   [Installing Reporting Services SharePoint Mode Report Server for Power View and Data Alerting](https://go.microsoft.com/fwlink/?LinkId=207543) (https://go.microsoft.com/fwlink/?LinkId=207543).  
  
-   [Install Reporting Services SharePoint Mode as a Single Server Farm](https://go.microsoft.com/fwlink/?LinkId=207544) (https://go.microsoft.com/fwlink/?LinkId=207544).  
  
-   [Reporting Services Report Server (SSRS)](https://go.microsoft.com/fwlink/?LinkID=207244) (https://go.microsoft.com/fwlink/?LinkID=207244).  
  
## Installing the Reporting Services Add-in for SharePoint Technologies  
 Starting with the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] release, the add-in can be installed as part of the SQL Server installation, in the feature selection page of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation wizard.  
  
 However you can install the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Add-in for SharePoint 2010 the by using one of the following methods:  
  
-   Run the SharePoint 2010 Products Preparation tool **PreRequisiteInstaller.exe**.  
  
-   Install from the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation media. Click the **rsSharePoint.msi** file in the Setup folder on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation media after [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup is complete.  
  
-   Download and install the add-in. For more information, see [Where to find the Reporting Services add-in for SharePoint Products](https://go.microsoft.com/fwlink/?LinkID=208634) (https://go.microsoft.com/fwlink/?LinkID=208634).  
  
## See Also  
 [Start Reporting Services Configuration Manager](https://go.microsoft.com/fwlink/?LinkId=199096)   
 [Create a Report Server Database (Reporting Services Configuration)](https://go.microsoft.com/fwlink/?LinkId=199094)   
 [Upgrade and Migrate Reporting Services](https://go.microsoft.com/fwlink/?LinkID=245628)   
 [Command Prompt Installation of Reporting Services SharePoint Mode and Native Mode](https://go.microsoft.com/fwlink/?LinkId=217620)  
  
  
