---
title: "View and Read SQL Server Setup Log Files"
description: This article describes the log files that SQL Server Setup creates. Log files are placed in a dated and time-stamped folder.
author: rwestMSFT
ms.author: randolphwest
ms.date: 05/29/2025
ms.service: sql
ms.subservice: install
ms.topic: install-set-up-deploy
helpviewer_keywords:
  - "viewing logs"
  - "displaying log files"
  - "Setup [SQL Server], logs"
  - "installation log files [SQL Server]"
  - "installing SQL Server, logs"
  - "errors [SQL Server], Setup"
  - "logs [SQL Server], Setup"
monikerRange: ">=sql-server-2017"
---
# View and read SQL Server Setup log files

[!INCLUDE [SQL Server -Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

SQL Server Setup creates log files in a dated and time-stamped folder within `%programfiles%\Microsoft SQL Server\<nnn>\Setup Bootstrap\Log` by default, where `<nnn>` are numbers that correspond to the version of SQL that's being installed. The time-stamped log folder name format is yyyyMMdd_HHmmss. When Setup is executed in unattended mode, the logs are created within `%temp%\sqlsetup*.log`. All files in the log folder are archived into the Log\*.cab file in their respective log folder.

   | File | Path |
   | --- | --- |
   | **Summary.txt** | `%programfiles%\Microsoft SQL Server\<nnn>\Setup Bootstrap\Log` |
   | **Summary_\<MachineName>\_Date.txt** | `%programfiles%\Microsoft SQL Server\<nnn>\Setup Bootstrap\Log\<yyyyMMdd_HHmmss>` |
   | **Detail.txt** | `%programfiles%\Microsoft SQL Server\<nnn>\Setup Bootstrap\Log\<yyyyMMdd_HHmmss>` |
   | **Datastore** | `%programfiles%\Microsoft SQL Server\<nnn>\Setup Bootstrap\Log\<yyyyMMdd_HHmmss>\Datastore` |
   | **MSI Log Files** | `%programfiles%\Microsoft SQL Server\<nnn>\Setup Bootstrap\Log\<yyyyMMdd_HHmmss>\<Name>.log` |
   | **ConfigurationFile.ini** | `%programfiles%\Microsoft SQL Server\<nnn>\Setup Bootstrap\Log\<yyyyMMdd_HHmmss>` |
   | **SystemConfigurationCheck_Report.htm** | `%programfiles%\Microsoft SQL Server\<nnn>\Setup Bootstrap\Log\<yyyyMMdd_HHmmss>` |
   | **For unattended installations** | `%temp%\sqlsetup*.log` |

:::image type="content" source="media/view-and-read-sql-server-setup-log-files/setup-bootstrap-example.png" alt-text="Screenshot showing where to find the ConfigurationFiles.ini file in the Setup Bootstrap folder." lightbox="media/view-and-read-sql-server-setup-log-files/setup-bootstrap-example.png":::

> [!NOTE]  
> The numbers in the path `<nnn>` correspond to the version of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] being installed. In the previous screenshot, [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] was installed, so the folder is `140`. For [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)], the folder would be `130`, and so on.

[!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Setup completes three basic phases:

1. Global Rules verification: validates basic system requirements
1. Component update: checks to see if there are any updates available for the media being installed
1. User-requested action: allows the user to select and customize features

This workflow produces a single summary log, and either a single detail log for a base SQL Server installation, or two detail logs for when update, such as a service pack, is installed along with the base installation.

Additionally, there are datastore files that contain a snapshot of the state of all the configuration objects that are being tracked by the setup process, and are useful for troubleshooting configuration errors. XML dump files are created for each execution phase and are saved in the Datastore log subfolder under the time-stamped log folder.

The following sections describe [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Setup log files.

## The Summary.txt file

### Overview

This file shows the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] components that were detected during Setup, the operating system environment, command-line parameter values if they're specified, and the overall status of each MSI/MSP that was executed.

The log is organized into the following sections:

- An overall summary of the execution
- Properties and the configuration of the computer where [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Setup was run
- [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] product features previously installed on the computer
- Description of the installation version and installation package properties
- Runtime input settings that are provided during install
- Location of the configuration file
- Details of the execution results
- Global rules
- Rules specific to the installation scenario
- Failed rules
- Location of the rules report file

  > [!NOTE]  
  > When patching there can be several sub folders (one for each instance being patched, and one for shared features) which contain a similar set of files (that is, `%programfiles%\Microsoft SQL Server\130\Setup Bootstrap\Log\<yyyyMMdd_HHMM>\MSSQLSERVER`).

### Location

The summary file is located within `%programfiles%\Microsoft SQL Server\<nnn>\Setup Bootstrap\Log\`.

To find errors in the summary text file, search the file by using the "error" or "failed" keywords.

## *Summary_\<MachineName>_yyyyMMdd_HHmmss.txt* file

### Overview

The summary_engine base file is similar to the summary file and is generated during the main workflow.

### Location

The *Summary_\<MachineName>_yyyyMMdd_HHmmss.txt* file is located at `%programfiles%\Microsoft SQL Server\<nnn>\Setup Bootstrap\Log\<yyyyMMdd_HHMM>\`.

## The Detail.txt file

### Overview

The `Detail.txt` file is generated for the main workflow such as install or upgrade, and provides the details of the execution. The logs in the file are generated based on the time when each action for the installation was invoked. The text file shows the order in which the actions were executed, and their dependencies.

### Location

The `Detail.txt` file is located within `%programfiles%\Microsoft SQL Server\<nnn>\Setup Bootstrap\Log\<yyyyMMdd_HHMM>\detail.txt`.

If an error occurs during the Setup process, the exception or error is logged at the end of this file. To find the errors in this file, first examine the end of the file followed by a search of the file for the "error" or "exception" keywords

## MSI log files

### Overview

The MSI log files provide details of the installation package process. They are generated by the MSIEXEC during the installation of the specified package.

Types of MSI log files:

- \<Feature>_\<Architecture>\_\<Interaction>.log
- \<Feature>_\<Architecture>\_\<Language>\_\<Interaction>.log
- \<Feature>_\<Architecture>\_\<Interaction>\_\<workflow>.log

### Location

The MSI log files are located at `%programfiles%\Microsoft SQL Server\<nnn>\Setup Bootstrap\Log\<yyyyMMdd_HHMM>\<Name>.log`.

At the end of the file is a summary of the execution, which includes the success or failure status and properties. To find the error in the MSI file, search for "value 3" and review the text before and after.

## The ConfigurationFile.ini file

### Overview

The configuration file contains the input settings that are provided during installation. It can be used to restart the installation without having to enter the settings manually. However, passwords for the accounts, PID, and some parameters aren't saved in the configuration file. The settings can be either added to the file or provided by using the command line or the Setup user interface. For more information, see [Install SQL Server using a configuration file](install-sql-server-using-a-configuration-file.md).

### Location

The `ConfigurationFile.ini` is located at `%programfiles%\Microsoft SQL Server\<nnn>\Setup Bootstrap\Log\<yyyyMMdd_HHMM>\`.

## The SystemConfigurationCheck_Report.htm file

### Overview

The system configuration check report contains a short description for each executed rule, and the execution status.

### Location

The `SystemConfigurationCheck_Report.htm` file is located at `%programfiles%\Microsoft SQL Server\<nnn>\Setup Bootstrap\Log\<yyyyMMdd_HHMM>\`.

## Related content

- [SQL Server installation guide](install-sql-server.md)

[!INCLUDE [get-help-options](../../includes/paragraph-content/get-help-options.md)]
