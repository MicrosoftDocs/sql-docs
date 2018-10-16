---
title: "Setting Tool Options and Layout | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "Database Engine [SQL Server], tutorials"
ms.assetid: 43e97ce0-97bc-4a27-9485-5bbeb7190b85
author: stevestein
ms.author: sstein
manager: craigg
---
# Setting Tool Options and Layout
  You can set options that configure what the Database Engine Tuning Advisor graphical user interface (GUI) displays on startup, the font it uses, and other tool functionality to best support how you use it. The practices on this page familiarize you with the options you can set, and how to set them.  
  
### Set the tool options  
  
1.  Start the Database Engine Tuning Advisor. On the Windows **Start** menu, point to **All Programs**, point to [!INCLUDE[ssCurrentUI](../../includes/sscurrentui-md.md)], point to **Performance Tools**, and click **Database Engine Tuning Advisor**.  
  
2.  On the **Tools** menu, click **Options**.  
  
3.  In the **Options** dialog box, view the following options:  
  
    -   Expand the **On startup** list to view what Database Engine Tuning Advisor can display when it is started. By default, **Show a new session** is selected.  
  
    -   Click **Change Font** to see what fonts you can choose for the lists of databases and tables on the **General** tab. The fonts you choose for this option also are used in Database Engine Tuning Advisor recommendation grids and reports after you have performed tuning. By default, Database Engine Tuning Advisor uses system fonts.  
  
    -   The **Number of items in most recently used lists** can be set between **1** and **10**. This sets the maximum number of items in the lists displayed by clicking **Recent Sessions** or **Recent Files** on the **File** menu. By default, this option is set to **4**.  
  
    -   When **Remember my last tuning options** is checked, by default Database Engine Tuning Advisor uses the tuning options you specified for your last tuning session for the next tuning session. Clear this check box to use Database Engine Tuning Advisor tuning option defaults. By default, this option is selected.  
  
    -   By default, **Ask before permanently deleting sessions** is checked to avoid accidentally deleting tuning sessions.  
  
    -   By default, **Ask before stopping session analysis** is checked to avoid accidentally stopping a tuning session before Database Engine Tuning Advisor has finished analyzing a workload.  
  
## Next Lesson  
 [Lesson 2: Using Database Engine Tuning Advisor](../../relational-databases/performance/database-engine-tuning-advisor.md)  
  
  
