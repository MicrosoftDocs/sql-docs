---
title: "Use Upgrade Advisor to Prepare for Upgrades | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "Upgrade Advisor [SQL Server]"
  - "upgrading SQL Server, Upgrade Advisor"
  - "upgrading SQL Server, preparing to upgrade"
  - "SQL Server Upgrade Advisor"
  - "analyzing installations for upgrading [SQL Server]"
ms.assetid: d85b0833-ddeb-42e3-9397-97ea60d521b7
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Use Upgrade Advisor to Prepare for Upgrades
  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Upgrade Advisor helps you prepare for upgrades to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. Upgrade Advisor analyzes installed components from earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and then generates a report that identifies issues to fix either before or after you upgrade.  
  
## How Upgrade Advisor Works  
 When you run Upgrade Advisor, the Upgrade Advisor Home page appears. From the Home page, you can run the following tools:  
  
-   Upgrade Advisor Analysis Wizard  
  
-   Upgrade Advisor Report Viewer  
  
-   Upgrade Advisor Help  
  
 The first time that you use Upgrade Advisor, run the Upgrade Advisor Analysis Wizard to analyze [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] components. When the wizard finishes the analysis, view the resulting reports in the Upgrade Advisor Report Viewer. Each report provides links to information in Upgrade Advisor Help that will help you fix or reduce the effect of the known issues.  
  
## Upgrade Advisor Analysis  
 Upgrade Advisor analyzes the following [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] components:  
  
-   [!INCLUDE[ssDE](../../includes/ssde-md.md)]  
  
-   [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]  
  
-   [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]  
  
-   [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]  
  
 The analysis examines objects that can be accessed, such as scripts, stored procedures, triggers, and trace files. Upgrade Advisor cannot analyze desktop applications or encrypted stored procedures.  
  
 Output is in the form of an XML report. View the XML report by using the Upgrade Advisor report viewer.  
  
> [!NOTE]  
>  Reports might contain an "other upgrade issues" item. This item links to a list of issues that are not detected by Upgrade Advisor, but might exist on your server or in your applications. You should review the list of undetectable issues and determine whether you must change your server or applications because of the undetectable issues.  
  
## How to Install and Run Upgrade Advisor  
 The location where you install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Upgrade Advisor depends on what you will be analyzing. Upgrade Advisor supports remote analysis of all supported components except [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. If you are not scanning instances of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], you can install Upgrade Advisor on any computer that can connect to your instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and that meets the Upgrade Advisor prerequisites. For more information, see [Supported Version and Edition Upgrades](../../database-engine/install-windows/supported-version-and-edition-upgrades.md). If you are scanning instances of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], you must install Upgrade Advisor on the report server.  
  
 Upgrade Advisor is available in a feature pack.  
  
 Prerequisites for installing and running Upgrade Advisor are as follows:  
  
-   [!INCLUDE[firstref_longhorn](../../includes/firstref-longhorn-md.md)] SP2, Windows 7 SP1, and [!INCLUDE[winserver2008r2](../../includes/winserver2008r2-md.md)] SP1.  
  
-   Windows Installer beginning with version 4.5. You can install Windows Installer from the [Windows Installer Web site](https://go.microsoft.com/fwlink/?LinkId=49112).  
  
-   Microsoft .NET Framework 4. .NET Framework 4 is available on the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] product media, and from the [.NET Framework 4 download page](https://go.microsoft.com/fwlink/?LinkId=209895).  
  
    -   To install the .NET Framework 4 from the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] media, locate the root of the disk drive. Then, double-click the \redist folder, double-click the DotNetFrameworks folder, and run dotNetFx40_Full_x86_x64.exe (for 32-bit operating systems or for 64-bit operating systems).  
  
 To install Upgrade Advisor from the Web, click the download button on the download page. You can then run installation immediately, or save the SQLUA.msi file to run later. If you are installing from the product disc, run SQLUA.msi directly from the product disk.  
  
 After you install Upgrade Advisor, you can open it from the **Start** menu:  
  
-   Click **Start**, point to **All Programs**, point to [!INCLUDE[ssCurrentUI](../../includes/sscurrentui-md.md)], and then click **[!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] Upgrade Advisor**.  
  
 For more information, see the Upgrade Advisor documentation included in the Upgrade Advisor download and the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] Release Notes.  
  
## See Also  
 [Work with Multiple Versions and Instances of SQL Server](../../../2014/sql-server/install/work-with-multiple-versions-and-instances-of-sql-server.md)   
 [Supported Version and Edition Upgrades](../../database-engine/install-windows/supported-version-and-edition-upgrades.md)   
 [Backward Compatibility](../../../2014/getting-started/backward-compatibility.md)  
  
  
