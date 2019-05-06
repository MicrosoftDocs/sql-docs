---
title: "Using Reports | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "displaying reports"
  - "overriding reports"
  - "Upgrade Advisor Report Viewer"
  - "reports [Upgrade Advisor], about reports"
  - "formatting reports"
  - "resolved issues [Upgrade Advisor]"
  - "distributing reports [Reporting Services]"
  - "filtering reports [Reporting Services]"
  - "removing report items"
  - "viewing reports"
  - "rerunning analysis"
  - "information issues [Upgrade Advisor]"
  - "deleting report items"
  - "icons [Upgrade Advisor]"
  - "Upgrade Advisor [SQL Server], reports"
  - "sending reports"
  - "blocking issues [Upgrade Advisor]"
  - "sharing reports"
  - "reports [Upgrade Advisor]"
  - "SQL Server Upgrade Advisor, reports"
  - "modifying reports"
  - "distributing reports [Reporting Services], about report distribution"
  - "warnings [Upgrade Advisor]"
  - "analyzing system [Upgrade Advisor], reports"
ms.assetid: 4a3cb94a-a7ac-4cec-94c7-db26fcf6d161
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Using Reports
  A separate report is generated for each component, and, where necessary, each instance, that the Upgrade Advisor Analysis Wizard analyzes on a server. The report provides details about known issues that affect an upgrade. It also provides links to information and suggested actions for addressing the identified issues.  
  
> [!NOTE]  
>  If the Upgrade Advisor Report Viewer does not find any reports in the default reports directory, you can load a report from a different directory by using the **Open Report** link.  
  
## Viewing Reports  
 You use the Upgrade Advisor Report Viewer to view Upgrade Advisor reports. To view reports, on the Upgrade Advisor start page, click **Launch Upgrade Advisor Report Viewer**.  
  
 After you load a report for a server, you can select a component for which you want to see upgrade issues. You can apply a filter from the **Filter By** box to see the following:  
  
-   All issues  
  
-   All upgrade issues  
  
-   Pre-upgrade issues  
  
-   All migration issues  
  
-   Resolved issues  
  
-   Unresolved issues  
  
 If your report has more than 20 issues, you can move to the next or previous group of issues by clicking **Next 20** or **Previous 20** at the top or bottom of the issues list.  
  
 You can view up to five saved reports by selecting the reports from the **Report** drop-down list box. The reports are listed by the timestamp from when they were generated.  
  
## Report Format  
 The report viewer displays report issues in three columns. Each issue is collapsible so that you can hide the description and view only the key information.  
  
 The first column is the **Importance** column. Icons indicate the importance for each issue by displaying either a red circle with an X for blocking or important issues or a yellow triangle with an exclamation mark for Warning and Information issues. The second column, **When to fix**, indicates when you must resolve the issue. You can sort the report on either the **Importance** or **When to fix** column. The third column, **Description**, lists the title of the issue.  
  
 You can expand an issue to display additional information, a link to detailed information about resolving the issue, and a link to show issue details. When you click the link to get detailed information for the issue, a Help topic with information about the issue and instructions for addressing the issue is displayed. After you have fixed an issue, or to manage your action items, you can mark issues as complete by selecting the **This issue has been resolved** check box. If you want to remove the resolved issues from the list of upgrade issues, click **Refresh**. The issue is not displayed again until you either run the Upgrade Advisor Analysis Wizard against the same component or apply the **Resolved Issues** filter from the **Filter By** option.  
  
## Report Files  
 The Upgrade Advisor Analysis Wizard creates reports in the My Documents\\[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Upgrade Advisor\110\Reports directory and creates a subdirectory for each server that you analyze. The report files are XML files that follow a specific naming convention. When you launch the Upgrade Advisor Report Viewer, the report files in the default directory are displayed. If you copy report files into this folder, they must adhere to the naming convention or the report viewer will not display them automatically.  
  
 If you want to share the information with other people, you can send them the XML report. Or, if you want to use another application, you can export the report into a comma-separated value file that you can use to create a spreadsheet, text file, or e-mail message.  
  
## See Also  
 [How to: View an Upgrade Advisor Report](../../../2014/sql-server/install/how-to-view-an-upgrade-advisor-report.md)   
 [How to: Export Reports](../../../2014/sql-server/install/how-to-export-reports.md)   
 [How to: Filter Reports](../../../2014/sql-server/install/how-to-filter-reports.md)   
 [Resolving Upgrade Issues](../../../2014/sql-server/install/resolving-upgrade-issues.md)   
 [SQL Server 2014 Upgrade Advisor &#91;new&#93;](sql-server-2014-upgrade-advisor.md)  
  
  
