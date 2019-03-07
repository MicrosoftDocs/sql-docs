---
title: "Upgrade Advisor Overview | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "Upgrade Advisor Report Viewer"
  - "SQL Server Upgrade Advisor, components"
  - "tools [Upgrade Advisor]"
  - "Upgrade Advisor [SQL Server], components"
  - "components [Upgrade Advisor]"
  - "Upgrade Advisor Analysis Wizard"
  - "limitations [Upgrade Advisor]"
  - "analyzing system [Upgrade Advisor]"
  - "analyzing system [Upgrade Advisor], about analysis"
ms.assetid: f5c56f63-4478-40af-abb9-642f58a0026c
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Upgrade Advisor Overview
  Upgrade Advisor provides a central console to analyze [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)], and [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] components, and to view reports that contain information about the results of the analysis.  
  
## How Upgrade Advisor Works  
 When you run Upgrade Advisor, the Upgrade Advisor start page appears. The Upgrade Advisor start page is the launching point for the following:  
  
-   Upgrade Advisor Analysis Wizard  
  
-   Upgrade Advisor Report Viewer  
  
-   Upgrade Advisor Help  
  
 The first time that you use Upgrade Advisor, run the Upgrade Advisor Analysis Wizard to analyze a server. When the wizard completes the analysis, click **Launch Report** from the wizard or return to the Upgrade Advisor start page. From there, run the Upgrade Advisor Report Viewer to view the report. The report provides links to information that will help you resolve known issues.  
  
## Upgrade Advisor Analysis Wizard  
 To perform an analysis, click **Launch Upgrade Advisor Analysis Wizard** on the Upgrade Advisor start page. The Upgrade Advisor Analysis Wizard gathers information about the computer, instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] components, and trace files that you want to analyze. After all the information has been gathered and confirmed, the Upgrade Advisor Analysis Wizard analyzes [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] components.  
  
> [!NOTE]  
>  Each time you run the Upgrade Advisor Analysis Wizard, a separate report is generated, and existing reports for the selected components are not overwritten. However, the report viewer displays only the five latest reports.  
  
 When the Upgrade Advisor Analysis Wizard finishes its analysis, it creates an XML file for each component that you have included in the analysis. The XML files contain the items and issues discovered in each component.  
  
### What Upgrade Advisor Analyzes  
 A dedicated analyzer runs in the context of Upgrade Advisor for each [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] component. The output of each analyzer is an XML report for that component.  
  
 Upgrade Advisor queries the following [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] components:  
  
-   Database Server (also referred to as the [!INCLUDE[ssDE](../../includes/ssde-md.md)] in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online), which includes Replication, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent, Full-Text Search, and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client  
  
-   [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]  
  
-   [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]  
  
-   [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]  
  
> [!NOTE]  
>  During analysis, each analyzer creates a log file. You can use these log files to troubleshoot the analysis. For example, if Upgrade Advisor is running slowly, you can view the log files to determine the cause of the delay.  
  
### Upgrade Advisor Limitations  
 Upgrade Advisor cannot detect every issue that might affect an upgrade. For example, if you have embedded SQL code in a client application that runs on end-user desktops, it will not be possible for Upgrade Advisor to analyze the applications. For these items, you still must consider the issues and upgrade, migrate, or modify the information as required in your installation.  
  
 Upgrade Advisor does not analyze encrypted stored procedures, code in extended stored procedures, and source code in languages other than [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
## Upgrade Advisor Report Viewer  
 To view an Upgrade Advisor report, click **Launch Upgrade Advisor Report Viewer** on the Upgrade Advisor start page. When the Upgrade Advisor Report Viewer starts, the reports in the default directory are loaded. Reports are not displayed if the Upgrade Advisor Report Viewer does not find any reports in the default directory. If there are no reports in the default directory, you can either run the Upgrade Advisor Analysis Wizard to create a report or load an existing report from another server or from a subdirectory.  
  
 [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] Upgrade Advisor does not overwrite existing reports. However, the report viewer displays only the five latest reports. To view an earlier report, select the report from the **Report** drop-down list box. The timestamp indicates the date and time the report was generated.  
  
 When XML files from the Upgrade Advisor Analysis Wizard are loaded into the Upgrade Advisor Report Viewer, a report for each component is displayed. The report contains all the known issues, both detectable and undetectable, that you need to address. For each issue there is an icon indicating importance, a label informing you when the issue must be fixed, and a short description. When you expand an issue, you will see a longer description, a link to issue details, and a link to the Help file. The information for each issue is designed to provide enough information for you to fix the issue.  
  
 Most components have issues that cannot be detected. To view these issues, expand the **Other Upgrade Issues** item for the component, and then click the link to view additional information about the issues in the documentation. For more information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] backward compatibility issues, see [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
## See Also  
 [Working with Upgrade Advisor](../../../2014/sql-server/install/working-with-upgrade-advisor.md)  
  
  
