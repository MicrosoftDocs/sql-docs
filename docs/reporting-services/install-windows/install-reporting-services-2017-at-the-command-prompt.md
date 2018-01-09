---
title: "Install Reporting Services 2017 and Power BI Report Server at the Command Prompt | Microsoft Docs"
ms.custom: ""
ms.date: "01/09/2018"
ms.prod: reporting-services
ms.prod_service: "reporting-services-native"
ms.service: ""
ms.component: "install-windows"
ms.reviewer: ""
ms.suite: "pro-bi"
ms.technology: 

ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "command line"
author: "guyinacube"
ms.author: "maghan"
manager: "kfile"
ms.workload: "Active"
---
# Install Reporting Services 2017 and Power BI Report Server at the Command Prompt

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE[ssrs-appliesto-2017-and-later](../../includes/ssrs-appliesto-2017-and-later.md)] [!INCLUDE[ssrs-appliesto-pbirsi](../includes/ssrs-appliesto-pbirs.md)]

SQL Server Reporting Services and Power BI Report Server support a command-line installation from the Reporting Services and Power BI Report Server setup programs. Installation involves server components for storing report items, rendering reports, and processing of subscription and other report services. 

To download SQL Server 2017 Reporting Services, go to the [Microsoft Download Center](https://www.microsoft.com/download/details.aspx?id=55252).

To download Power BI Report Server, go to [On-premises reporting with Power BI Report Server](https://powerbi.microsoft.com/report-server/).

## Before you begin

Before you install Reporting Services, review the [Hardware and software requirements for installing SQL Server](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md).

## Run setup from the command line

For local installations, you must run setup as an administrator. If you install Reporting Services from a remote share, you must use a domain account that has read and execute permissions on the remote share. For failover cluster installations, you must be a local administrator with permissions to login as a service, and to act as part of the operating system on all failover cluster nodes.

1. Open the command prompt as Administrator.
2. For help on the installation, typing /HELP, /H, or /? will show help.

## Setup parameters

Use the following guidelines to develop installation commands that have correct syntax:

- /repair | /uninstall - Repairs or uninstalls the product. By default, an install or upgrade is performed.
- /passive | /quiet - Displays minimal UI with no prompts, or displays no UI and no prompts.
- /norestart - Supporesses any attempts to restart.
- /log `<file>` - Specifies the setup log file location. By default, log files are created under %TEMP%.

    Example: /log log.txt
- /InstallFolder - Sets the install folder. Example: /InstallFolder="c:\Program Files\SSRS".
- /IAcceptLicenseTerms - Required for install and upgrade.
- /PID - Sets the custom license key. 

   Example: /PID=12345-12345-12345-12345-12345
- /Edition - Sts the custom free edition. Options are Dev, Eval, ExprAdv. 

    Example /Edition=Eval
- /EditionUpgrade - Upgrades the edition of the installed product. Requires /PID or /Edition flag.

### Examples

Here are a few examples:
- Install Evaluation edition:

    SQLServerReportingServices.exe /quiet /IAcceptLicenseTerms
- Install Developer edition:

    SQLServerReportingServices.exe /quiet /Edition=Dev /IAcceptLicenseTerms
- Install an edition that requires a product key (e.g., Enterprise) (substitute actual product key):

     SQLServerReportingServices.exe /quiet /PID=12345-12345-12345-12345-12345 /IAcceptLicenseTerms




## Next steps

[Install SQL Server from the Command Prompt](../../database-engine/install-windows/install-sql-server-from-the-command-prompt.md)   
[SysPrep Parameters](../../database-engine/install-windows/install-sql-server-from-the-command-prompt.md#SysPrep)   

More questions? [Try asking the Reporting Services forum](http://go.microsoft.com/fwlink/?LinkId=620231)
