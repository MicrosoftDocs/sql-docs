---
title: "Upgrade Advisor Progress | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "Upgrade Advisor [SQL Server], analysis progress status"
  - "analyzing system [Upgrade Advisor], progress information"
  - "SQL Server Upgrade Advisor, analysis progress status"
  - "monitoring analysis progress"
  - "progress information [Upgrade Advisor]"
  - "status information [Upgrade Advisor]"
ms.assetid: a9e3d1c8-d492-4beb-93c7-f1bc40d4a910
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Upgrade Advisor Progress
  Upgrade Advisor analysis starts with a dedicated analyzer that performs an analysis of each [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] component that you selected. As components are analyzed, progress is reported in the **Progress** dialog box.  
  
## Options  
 **Action**  
 Specifies the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] component that is selected for analysis.  
  
 **Status**  
 Displays the status value returned from the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] component progress interface.  
  
 **Message**  
 Displays error, failure, or success messages returned from the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] individual analyzer.  
  
> [!NOTE]  
>  If the analysis is taking too long, you can stop the analysis, exit the Upgrade Advisor Analysis Wizard, and then rerun the wizard. To reduce analysis time, select fewer components to scan.  
  
 When analysis is complete, the report is written to a file. You can view the report by clicking **Launch Report** to launch the report viewer from this page. If you want to view the report later, you can open the **Upgrade Advisor Report Viewer** from the Upgrade Advisor start page.  
  
> [!NOTE]  
>  Previous reports are saved every time you analyze a server. The reports are saved using the timestamp for the file name. The report viewer displays the latest five saved reports.  
  
## See Also  
 [How to: Launch Upgrade Advisor](../../../2014/sql-server/install/how-to-launch-upgrade-advisor.md)   
 [How to: Run the Upgrade Advisor Analysis Wizard](../../../2014/sql-server/install/how-to-run-the-upgrade-advisor-analysis-wizard.md)   
 [SQL Server Components](../../../2014/sql-server/install/sql-server-components.md)   
 [Upgrade Advisor User Interface Reference](../../../2014/sql-server/install/upgrade-advisor-user-interface-reference.md)   
 [Working with Upgrade Advisor](../../../2014/sql-server/install/working-with-upgrade-advisor.md)  
  
  
