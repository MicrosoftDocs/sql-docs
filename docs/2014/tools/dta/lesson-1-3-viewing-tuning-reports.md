---
title: "Viewing Tuning Reports | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "tuning reports [SQL Server]"
ms.assetid: daee6143-269f-428b-8458-9a3e726d586c
author: stevestein
ms.author: sstein
manager: craigg
---
# Viewing Tuning Reports
  In the previous practice of this lesson, you viewed the [!INCLUDE[tsql](../../includes/tsql-md.md)] scripts that create or drop database objects in the Database Engine Tuning Advisor recommendations that were generated as a result of the MySession tuning session. The MySession tuning session was created in [Tuning a Workload](lesson-1-1-tuning-a-workload.md).  
  
 Although it is very useful to view the scripts that can be used to implement the tuning results, Database Engine Tuning Advisor also provides many useful reports that you can view. These reports provide information about the existing physical design structures in the database you are tuning, and about the recommended structures. The tuning reports can be viewed by clicking the **Reports** tab as described in the following practice. This practice uses the MySession and the EvaluateMySession tuning sessions that you created in [Tuning a Workload](lesson-1-1-tuning-a-workload.md) and in [Viewing Tuning Recommendations](lesson-1-2-viewing-tuning-recommendations.md).  
  
### View tuning reports  
  
1.  Start Database Engine Tuning Advisor. See [Launching Database Engine Tuning Advisor](../../relational-databases/performance/database-engine-tuning-advisor.md). Make sure that you connect to the same [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance that you used in previous practices of this lesson.  
  
     Double-click **MySession** in the **Session Monitor** pane. Database Engine Tuning Advisor loads the session information from this session.  
  
2.  Click the **Reports** tab.  
  
3.  In the **Tuning Summary** pane, you can view information about this tuning session. Use the scroll bar to view all of the pane contents. Note the **Expected percentage improvement** and the **Space used by recommendation**. It is possible to limit the space used by the recommendation when you set the tuning options. On the **Tuning Options** tab, select **Advanced Options**. Check **Define max. space for recommendtaions** and specify in megabytes the maximum space a recommendation configuration can use. Use the **Back** button in your help browser to return to this tutorial.  
  
4.  In the **Tuning Reports** pane, click **Statement cost report** in the **Select report** list. If you need more space to view the report, drag the **Session Monitor** pane border to the left. Each [!INCLUDE[tsql](../../includes/tsql-md.md)] statement that executes against a table in your database has a performance cost associated with it. This performance cost can be reduced by creating effective indexes on frequently accessed columns in a table. This report shows the estimated percentage improvement between the original cost of executing a statement in the workload and the cost if the tuning recommendation is implemented. Note that the amount of information contained in the report is based on the length and complexity of the workload.  
  
5.  Right-click the **Statement cost report** pane in the grid area, and click **Export to File**. Save the report as `MyReport`. An .xml extension is automatically appended to the file name. You can open MyReport.xml in your favorite XML editor or in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to view the report contents.  
  
6.  Return to the **Reports** tab of Database Engine Tuning Advisor, and right-click the **Statement cost report** again. Review the other options that are available. Note that you can change the font for the report you are viewing. Changing the font here also changes it on the other tabbed pages.  
  
7.  Click other reports in the **Select report** list to familiarize yourself with them.  
  
## Summary  
 You have now explored the **Reports** tab of the Database Engine Tuning Advisor GUI for the MySession tuning session. You can use these same steps to explore the reports that were generated for the EvaluateMySession tuning session. Double-click **EvaluateMySession** in the **Session Monitor** pane to begin.  
  
## Next Lesson  
 [Lesson 3: Using the dta Command Prompt Utility](lesson-3-using-the-dta-command-prompt-utility.md)  
  
  
