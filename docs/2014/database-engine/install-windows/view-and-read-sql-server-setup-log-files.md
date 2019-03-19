---
title: "View and Read SQL Server Setup Log Files | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: install
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
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# View and Read SQL Server Setup Log Files
  Each execution of Setup creates log files are created with a new timestamped log folder at %programfiles%\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\120\Setup Bootstrap\Log\\. The time-stamped log folder name format is YYYYMMDD_hhmmss. When Setup is run in an unattended mode, the logs are created at % temp%\sqlsetup*.log. All files in the logs folder are archived into the Log\*.cab file in their respective log folder.  
  
 A typical Setup request goes through three execution phases:  
  
1.  Global rules text  
  
2.  Component update  
  
3.  User-requested action  
  
 In each phase, Setup generates detail and summary logs with additional logs created as appropriate. Setup is called at least three times per user-requested Setup action.  
  
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
  
### Location  
 It is located at %programfiles%\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\120\Setup Bootstrap\Log\\.  
  
 To find errors in the summary text file, search the file by using the "error" or "failed" keywords.  
  
## Summary_engine-base_YYYYMMDD_HHMMss.txt  
  
### Overview  
 The summary_engine base file is similar to the summary file and is generated during the main workflow.  
  
### Location  
 It is located at %programfiles%\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\120\Setup Bootstrap\Log\\<YYYYMMDD_HHMM>\\.  
  
## Summary_engine-base_YYYYMMDD_HHMMss_ComponentUpdate.txt  
  
### Overview  
 The component update summary log file is similar to the summary file and is generated during the component update workflow.  
  
### Location  
 It is located at %programfiles%\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\120\Setup Bootstrap\Log\\<YYYYMMDD_HHMM>\\.  
  
## Summary_engine-base_\<VersionNumber>MMDD_HHMMss_GlobalRules.txt  
  
### Overview  
 The global rules summary log file is similar to the summary file generated during the global rules workflow.  
  
### Location  
 It is located at %programfiles%\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\120\Setup Bootstrap\Log\\<YYYYMMDD_HHMM>\\.  
  
## Detail.txt  
  
### Overview  
 Detail.txt is generated for the main workflow such as install or upgrade, and provides the details of the execution. The logs in the file are generated based on the time when each action for the installation was invoked, and show the order in which the actions were executed, and their dependencies.  
  
### Location  
 It is located at %programfiles%\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\120\Setup  
  
 Bootstrap\Log\\<YYYYMMDD_HHMM>\Detail.txt.  
  
 If an error occurs during the Setup process, the exception or error are logged at the end of this file. To find the errors in this file, first examine the end of the file followed by a search of the file for the "error" or "exception" keywords.  
  
## Detail_ComponentUpdate.txt  
  
### Overview  
 The Detail_ComponentUpdate.txt file is generated for the component update workflow and is similar to Detail.txt.  
  
### Location  
 It is located at %programfiles%\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\120\Setup Bootstrap\Log\\<YYYYMMDD_HHMM>\\.  
  
## Detail_GlobalRules.txt  
  
### Overview  
 Detail_GlobalRules.txt is generated for the global rules execution and is similar to Detail.txt.  
  
### Location  
 It is located at %programfiles%\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\120\Setup Bootstrap\Log\\<YYYYMMDD_HHMM>\\.  
  
## MSI log files  
  
### Overview  
 The MSI log files provide details of the installation package process. They are generated by the MSIEXEC during the installation of the specified package.  
  
 Types of MSI log files:  
  
-   \<Feature>_\<Architecture>\_\<Interaction>.log  
  
-   \<Feature>_\<Architecture>\_\<Language>\_\<Interaction>.log  
  
-   \<Feature>_\<Architecture>\_\<Interaction>\_\<workflow>.log  
  
### Location  
 The MSI log files are located at %programfiles%\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\120\Setup Bootstrap\Log\\<YYYYMMDD_HHMM>\\<Name\>.log.  
  
 At the end of the file is a summary of the execution which includes the success or failure status and properties. To find the error in the MSI file, search for "value 3" and usually the errors can be found close to the string.  
  
## ConfigurationFile.ini  
  
### Overview  
 The configuration file contains the input settings that are provided during installation. It can be used to restart the installation without having to enter the settings manually. However, passwords for the accounts, PID, and some parameters are not saved in the configuration file. The settings can be either added to the file or provided by using the command line or the Setup user interface. For more information, see [Install SQL Server 2014 Using a Configuration File](install-sql-server-using-a-configuration-file.md).  
  
### Location  
 It is located at %programfiles%\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\120\Setup Bootstrap\Log\\<YYYYMMDD_HHMM>\\.  
  
## SystemConfigurationCheck_Report.htm  
  
### Overview  
 The system configuration check report contains a short description for each executed rule, and the execution status.  
  
### Location  
 It is located at %programfiles%\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\120\Setup Bootstrap\Log\\<YYYYMMDD_HHMM>\\.  
  
## See Also  
 [Installation How-to Topics](../../sql-server/install/installation-how-to-topics.md)   
 [Install SQL Server 2014](install-sql-server.md)  
  
  
