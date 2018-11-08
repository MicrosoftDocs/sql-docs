---
title: "Tuning a Workload | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "workloads [SQL Server], tuning"
ms.assetid: 6229bf3f-1182-4bc6-8451-cedc37f4b62e
author: stevestein
ms.author: sstein
manager: craigg
---
# Tuning a Workload
  The Database Engine Tuning Advisor can be used to find the best physical database design for query performance on the databases and tables that you select for tuning.  
  
 This task uses the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] sample database. To enhance security, the sample databases are not installed by default. To install the sample databases, see [Installing SQL Server Samples and Sample Databases](http://sqlserversamples.codeplex.com).  
  
### Tune a workload Transact-SQL script file  
  
1.  Copy a sample SELECT statement or statements from "A. Using SELECT to retrieve rows and columns" in [SELECT Examples &#40;Transact-SQL&#41;](/sql/t-sql/queries/select-examples-transact-sql) and paste the statements into the Query Editor of [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. Save the file as **MyScript.sql** in a directory where you can easily find it.  
  
2.  Start Database Engine Tuning Advisor. See [Launching Database Engine Tuning Advisor](../../relational-databases/performance/database-engine-tuning-advisor.md).  
  
3.  In the right pane of the Database Engine Tuning Advisor GUI, type **MySession** in **Session name**.  
  
4.  Select **File** for your **Workload**, and click the **Browse for a workload file** button to locate the **MyScript.sql** file that you saved in Step 1.  
  
5.  Select [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] in the **Database for workload analysis** list, select [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] in the **Select databases and tables to tune** grid, and leave **Save tuning log** selected. **Database for workload analysis** specifies the first database to which Database Engine Tuning Advisor connects when tuning a workload. After tuning begins, Database Engine Tuning Advisor connects to the databases specified by the `USE DATABASE` statements contained in the workload.  
  
6.  Click the **Tuning Options** tab. You will not set any tuning options for this practice, but take a moment to review the default tuning options. Press F1 to view the Help for this tabbed page. Click **Advanced Options** to view additional tuning options. Click **Help** in the **Advanced Tuning Options** dialog box for information about the tuning options that are displayed there. Click **Cancel** to close the **Advanced Tuning Options** dialog box, leaving the default options selected.  
  
7.  Click the **Start Analysis** button on the toolbar. While Database Engine Tuning Advisor is analyzing the workload, you can monitor the status on the **Progress** tab. When tuning is complete, the **Recommendations** tab is displayed.  
  
     If you receive an error about the tuning stop date and time, check the **Stop at** time on the main **Tuning Options** tab. Make sure the **Stop at** date and time are greater than the current date and time, and if necessary, change them.  
  
8.  After the analysis completes, save your recommendation as a [!INCLUDE[tsql](../../includes/tsql-md.md)] script by clicking **Save Recommendations** on the **Actions** menu. In the **Save As** dialog box, navigate to the directory where you want to save the recommendations script, and type the file name **MyRecommendations**.  
  
## Summary  
 You have completed tuning a simple SELECT statement workload on the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database. The Database Engine Tuning Advisor can also take [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] trace files and tables as tuning workloads. The next task shows you how to view and interpret the tuning recommendations that you received as a result of the practice tuning.  
  
## Next Task in Lesson  
 [Viewing Tuning Recommendations](lesson-1-2-viewing-tuning-recommendations.md)  
  
  
