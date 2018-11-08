---
title: "How to: View an Upgrade Advisor Report | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "displaying reports"
  - "viewing reports"
  - "Upgrade Advisor [SQL Server], reports"
  - "SQL Server Upgrade Advisor, reports"
  - "reports [Upgrade Advisor], viewing"
ms.assetid: d13b38af-0ac3-4030-83cd-e7d7825dd09f
author: mashamsft
ms.author: mathoma
manager: craigg
---
# How to: View an Upgrade Advisor Report
  Upgrade Advisor creates reports for each component that you select to analyze. This topic describes how to view an Upgrade Advisor report from the Upgrade Advisor start page.  
  
> [!IMPORTANT]  
>  The report viewer loads files based on standard file names. If the files have been renamed, the report viewer will not load them.  
  
### To view a report  
  
1.  Click **Start**, click **All Programs**, click **[!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]**, and then click **[!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] Upgrade Advisor**.  
  
2.  On the Upgrade Advisor start page, click **Launch Upgrade Advisor Report Viewer**.  
  
3.  To select a report in the default location on your computer:  
  
    1.  In the **Server** list, select a server.  
  
    2.  In the **Instance or component** list, select a component or component/instance combination.  
  
     To select a report at another location:  
  
    1.  Click the **Open Report** link.  
  
    2.  Browse to the report location and then double-click the XML file.  
  
     Upgrade Advisor stores up to five reports from previous analysis as history. To view these reports, click the **Report** drop-down list box, and select a report. The reports are listed by the timestamp from when they were generated.  
  
     The report contains the following details for all detected issues:  
  
    -   **Importance**, which indicates how important it is to fix the issue.  
  
    -   **When to fix**, which indicates if you should (or must) fix the issue before or after upgrading to [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], before or after migrating the application or data, or anytime.  
  
    -   A brief description of the issue.  
  
4.  If the report contains more than 20 items, click the green forward arrow at the top or bottom of the report to view the next set of items. Click the green back button to view the previous 20 items.  
  
5.  To sort the report, click a column heading.  
  
6.  To view details for a specific item, click the item. A description of the issue appears, along with additional options:  
  
    -   To view the objects where this issue was found, click **Show affected objects**.  
  
    -   To view help for the issue, click **Tell me more about this issue and how to resolve it**.  
  
    -   To mark the issue as resolved, which hides the issue when you view the report again, select **This issue has been resolved**.  
  
> [!NOTE]  
>  The report may contain an item for undetectable issues. These are issues that cannot be detected or that would generate too many false-positive results. Click the **Tell me more about this issue and how to resolve it** link to see a list of undetectable issues for the component.  
  
## See Also  
 [How to: Export Reports](../../../2014/sql-server/install/how-to-export-reports.md)   
 [How to: Run the Upgrade Advisor Analysis Wizard](../../../2014/sql-server/install/how-to-run-the-upgrade-advisor-analysis-wizard.md)   
 [Resolving Upgrade Issues](../../../2014/sql-server/install/resolving-upgrade-issues.md)   
 [Upgrade Advisor How-to Topics](../../../2014/sql-server/install/upgrade-advisor-how-to-topics.md)   
 [Working with Upgrade Advisor](../../../2014/sql-server/install/working-with-upgrade-advisor.md)  
  
  
