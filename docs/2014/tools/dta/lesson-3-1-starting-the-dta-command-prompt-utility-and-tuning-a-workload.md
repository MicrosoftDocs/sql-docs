---
title: "Starting the dta Command Prompt Utility and Tuning a Workload | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "Database Engine [SQL Server], tutorials"
ms.assetid: f34a5acf-1f3b-4484-a770-6470cb925ab0
author: stevestein
ms.author: sstein
manager: craigg
---
# Starting the dta Command Prompt Utility and Tuning a Workload
  This task guides you through starting the **dta** utility, viewing its Help, and then using it to tune a workload from the command prompt. It uses the workload, MyScript.sql, which you created for the Database Engine Tuning Advisor graphical user interface (GUI) practice [Tuning a Workload](lesson-1-1-tuning-a-workload.md).  
  
 The tutorial uses the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] sample database. For security reasons, the sample databases are not installed by default. To install the sample databases, see [Installing SQL Server Samples and Sample Databases](http://sqlserversamples.codeplex.com).  
  
 The following tasks guide you through opening a command prompt, starting the **dta** command prompt utility, viewing its syntax Help, and tuning a simple workload, MyScript.sql, which you created in [Tuning a Workload](lesson-1-1-tuning-a-workload.md).  
  
### To start the dta command prompt utility and view Help  
  
1.  On the **Start** menu, point to **All Programs**, point to **Accessories**, and then click **Command Prompt**.  
  
2.  At the command prompt, type the following, and press ENTER:  
  
    ```  
    dta -? | more  
    ```  
  
     The `| more` part of this command is optional. However, using it enables you to page through the syntax help for the utility. Press ENTER to advance the help text by the line, or press the SPACEBAR to advance it by the page.  
  
### To tune a simple workload by using the dta command prompt utility  
  
1.  At the command prompt, navigate to the directory where you have stored the MyScript.sql file.  
  
2.  At the command prompt, type the following, and press ENTER to run the command and start the tuning session (note that the utility is case-sensitive when it parses commands):  
  
    ```  
    dta -S YourServerName\YourSQLServerInstanceName -E -D AdventureWorks2012 -if MyScript.sql -s MySession2 -of MySession2OutputScript.sql -ox MySession2Output.xml -fa IDX_IV -fp NONE -fk NONE  
    ```  
  
     where `-S` specifies the name of your server and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance where the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database is installed. The setting `-E` specifies that you want to use a trusted connection to the instance, which is appropriate if you are connecting with a Windows domain account. The setting `-D` specifies the database that you want to tune, `-if` specifies the workload file, `-s` specifies the session name, `-of` specifies the file to which you want the tool to write the [!INCLUDE[tsql](../../includes/tsql-md.md)] recommendations script, and `-ox` specifies the file to which you want the tool to write the recommendations in XML format. The last three switches specify tuning options as follows: `-fa IDX_IV` specifies that Database Engine Tuning Advisor should only consider adding indexes (both clustered and nonclustered) and indexed views; `-fp NONE` specifies that no partition strategy should be considered during analysis; and `-fk NONE` specifies that no existing physical design structures in the database must be kept when Database Engine Tuning Advisor makes its recommendations.  
  
3.  After Database Engine Tuning Advisor finishes tuning the workload, it displays a message indicating that your tuning session completed successfully. You can view the tuning results, by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to open the files MySession2OutputScript.sql and MySession2Output.xml. Alternatively, you can also open the MySession2 tuning session in the Database Engine Tuning Advisor GUI and view its recommendations and reports in the same way that you did in [Viewing Tuning Recommendations](lesson-1-2-viewing-tuning-recommendations.md) and [Viewing Tuning Reports](lesson-1-3-viewing-tuning-reports.md).  
  
## Summary  
 You have completed tuning a simple workload from the command prompt by using the **dta** utility. This tool provides many other tuning options. Refer to the tool Help (**dta -?**) and the reference topic [dta Utility](dta-utility.md) for more information.  
  
## After You Finish This Tutorial  
 After you finish the lessons in this tutorial, refer to the following topics for more information about Database Engine Tuning Advisor:  
  
-   [Database Engine Tuning Advisor](../../relational-databases/performance/database-engine-tuning-advisor.md) for descriptions of how to perform tasks with this tool.  
  
-   [dta Utility](dta-utility.md) for reference material on the command prompt utility and the optional XML file you can use to control the operation of the utility.  
  
 To return to the start of the tutorial, see [Tutorial: Database Engine Tuning Advisor](tutorial-database-engine-tuning-advisor.md).  
  
## See Also  
 [Database Engine Tutorials](../../relational-databases/database-engine-tutorials.md)  
  
  
