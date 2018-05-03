---
title: "View and Read SQL Server Setup Log Files | Microsoft Docs"
ms.custom: ""
ms.date: "09/08/2016"
ms.prod: sql
ms.prod_service: install
ms.reviewer: ""
ms.suite: "sql"
ms.technology: install
ms.tgt_pltfrm: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "viewing logs"
  - "displaying log files"
  - "Setup [SQL Server], logs"
  - "installation log files [SQL Server]"
  - "installing SQL Server, logs"
  - "errors [SQL Server], Setup"
  - "logs [SQL Server], Setup"
ms.assetid: 9d77af64-9084-4375-908a-d90f99535062
caps.latest.revision: 54
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# View and Read SQL Server Setup Log Files

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

Each execution of Setup creates log files are created with a new timestamped log folder at %programfiles%\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\\*nnn*\Setup Bootstrap\Log\\. The time-stamped log folder name format is YYYYMMDD_hhmmss. When Setup is run in an unattended mode, the logs are created at % temp%\sqlsetup*.log. All files in the logs folder are archived into the Log\*.cab file in their respective log folder.  
  
 A typical Setup request goes through a single execution phase that accomplishes three things: 
  
1.  Global rules text
  
2.  Component update
  
3.  User-requested action
  
 This workflow produces a single summary log, and at most two Detail logs, though typically there is onl one detail log. The second detail log would be present if additional media is slipstreamed along with the original media. 
  
 Datastore files contain a snapshot of the state of all configuration objects being tracked by the Setup process, and are useful for troubleshooting configuration errors. XML file dumps are created for datastore objects for each execution phase. They are saved in their own log subfolder under the time-stamped log folder, as follows:  
  
-   Datastore_GlobalRules  
  
-   Datastore_ComponentUpdated  
  
-   Datastore  
  
 The following sections describe [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup log files.  
  
## Summary Text  
  
### Overview  
 This file shows the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] components that were detected during Setup, the operating system environment, command-line parameter values if they are specified, and the overall status of each MSI/MSP that was executed.
  
 The log is organized into the following sections:
  
-   An overall summary of the execution
  
-   Properties and the configuration of the computer where [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup was run
  
-   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] product features previously installed on the computer
  
-   Description of the installation version and installation package properties  
  
-   Runtime input settings that are provided during install
  
-   Location of the configuration file
  
-   Details of the execution results
  
-   Global rules
  
-   Rules specific to the installation scenario
  
-   Failed rules
  
-   Location of the rules report file

  >[!NOTE]
  > Note that when patching there can be a number of sub folders (one for each instance being patched, and one for shared features) which contain a similiar set of files (i.e. %programfiles%\MicrosoftSQL Server\130\Setup Bootstrap\Log\<YYYYMMDD_HHMM>\MSSQLSERVER). 
  
### Location  
 It is located at %programfiles%\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\\*nnn*\Setup Bootstrap\Log\\.
  
 To find errors in the summary text file, search the file by using the "error" or "failed" keywords.
  
## Summary_\<MachineName>_YYYYMMDD_HHMMss.txt
  
### Overview  
 The summary_engine base file is similar to the summary file and is generated during the main workflow.
  
### Location  
 It is located at %programfiles%\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\\*nnn*\Setup Bootstrap\Log\\<YYYYMMDD_HHMM>\\.
  
## Summary_\<MachineName>_YYYYMMDD_HHMMss_ComponentUpdate.txt
  
### Overview
 The component update summary log file is similar to the summary file and is generated during the component update workflow.
  
### Location
 It is located at %programfiles%\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\\*nnn*\Setup Bootstrap\Log\\<YYYYMMDD_HHMM>\\.
  
## Summary_\<MachineName>_\<VersionNumber>MMDD_HHMMss_GlobalRules.txt
  
### Overview
 The global rules summary log file is similar to the summary file generated during the global rules workflow.
  
### Location  
 It is located at %programfiles%\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\\*nnn*\Setup Bootstrap\Log\\<YYYYMMDD_HHMM>\\.
  
## Detail.txt
  
### Overview
 Detail.txt is generated for the main workflow such as install or upgrade, and provides the details of the execution. The logs in the file are generated based on the time when each action for the installation was invoked, and show the order in which the actions were executed, and their dependencies.  
  
### Location  
 It is located at %programfiles%\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\\*nnn*\Setup  
  
 Bootstrap\Log\\<YYYYMMDD_HHMM>\Detail.txt.  
  
 If an error occurs during the Setup process, the exception or error are logged at the end of this file. To find the errors in this file, first examine the end of the file followed by a search of the file for the "error" or "exception" keywords.  
  
## Detail_ComponentUpdate.txt  
  
### Overview  
 The Detail_ComponentUpdate.txt file is generated for the component update workflow and is similar to Detail.txt.  
  
### Location  
 It is located at %programfiles%\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\\*nnn*\Setup Bootstrap\Log\\<YYYYMMDD_HHMM>\\.  
  
## Detail_GlobalRules.txt  
  
### Overview  
 Detail_GlobalRules.txt is generated for the global rules execution and is similar to Detail.txt.  
  
### Location  
 It is located at %programfiles%\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\\*nnn*\Setup Bootstrap\Log\\<YYYYMMDD_HHMM>\\.  
  
## MSI log files  
  
### Overview  
 The MSI log files provide details of the installation package process. They are generated by the MSIEXEC during the installation of the specified package.  
  
 Types of MSI log files:  
  
-   \<Feature>_\<Architecture>\_\<Interaction>.log  
  
-   \<Feature>_\<Architecture>\_\<Language>\_\<Interaction>.log  
  
-   \<Feature>_\<Architecture>\_\<Interaction>\_\<workflow>.log  
  
### Location  
 The MSI log files are located at %programfiles%\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\\*nnn*\Setup Bootstrap\Log\\<YYYYMMDD_HHMM>\\<Name\>.log.  
  
 At the end of the file is a summary of the execution which includes the success or failure status and properties. To find the error in the MSI file, search for "value 3" and usually the errors can be found close to the string.  
  
## ConfigurationFile.ini  
  
### Overview  
 The configuration file contains the input settings that are provided during installation. It can be used to restart the installation without having to enter the settings manually. However, passwords for the accounts, PID, and some parameters are not saved in the configuration file. The settings can be either added to the file or provided by using the command line or the Setup user interface. For more information, see [Install SQL Server 2016 Using a Configuration File](../../database-engine/install-windows/install-sql-server-2016-using-a-configuration-file.md).  
  
### Location  
 It is located at %programfiles%\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\\*nnn*\Setup Bootstrap\Log\\<YYYYMMDD_HHMM>\\.  
  
## SystemConfigurationCheck_Report.htm  
  
### Overview  
 The system configuration check report contains a short description for each executed rule, and the execution status.  
  
### Location  
 It is located at %programfiles%\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\\*nnn*\Setup Bootstrap\Log\\<YYYYMMDD_HHMM>\\.  
  
## See also  
 [Install SQL Server 2016](../../database-engine/install-windows/install-sql-server.md)  
  
  
