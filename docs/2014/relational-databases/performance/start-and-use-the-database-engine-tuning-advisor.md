---
title: "Start and Use the Database Engine Tuning Advisor | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
f1_keywords: 
  - "sql12.dta.advancedtuningoptions.f1"
  - "sql12.dta.general.f1"
  - "sql12.dta.tuningoptions.f1"
  - "sql12.dta.workload.f1"
  - "sql12.dta.progress.f1"
  - "sql12.dta.options.f1"
helpviewer_keywords: 
  - "Database Engine Tuning Advisor [SQL Server], starting"
ms.assetid: a4e3226a-3917-4ec8-bdf0-472879d231c9
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Start and Use the Database Engine Tuning Advisor
  This topic describes how to start and use Database Engine Tuning Advisor in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. For information about how to view and work with the results after you tune a database, see [View and Work with the Output from the Database Engine Tuning Advisor](database-engine-tuning-advisor.md).  
  
##  <a name="Initialize"></a> Initialize the Database Engine Tuning Advisor  
 On first use, a user who is member of the **sysadmin** fixed server role must initialize the Database Engine Tuning Advisor. This is because several system tables must be created in the `msdb` database to support tuning operations. Initialization also enables users that are members of the **db_owner** fixed database role to tune workloads on tables in databases that they own.  
  
 A user that has system administrator permissions must perform either of the following actions:  
  
-   Use the Database Engine Tuning Advisor graphical user interface to connect to an instance of [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. For more information, see [Start the Database Engine Tuning Advisor](#Start) later in this topic.  
  
-   Use the **dta** utility to tune the first workload. For more information, see [Use the dta Utility](#dta) later in this topic.  
  
##  <a name="Start"></a> Start the Database Engine Tuning Advisor  
 You can start the Database Engine Tuning Advisor graphical user interface (GUI) in several different ways to support database tuning in a variety of scenarios. The different ways to start Database Engine Tuning Advisor include: from the **Start** menu, from the **Tools** menu in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], from the Query Editor in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], and from the **Tools** menu in [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)]. When you first start Database Engine Tuning Advisor, the application displays a **Connect to Server** dialog box where you can specify the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance to which you want to connect.  
  
> [!WARNING]  
>  Do not start Database Engine Tuning Advisor when [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is running in single-user mode. If you attempt to start it while the server is in single-user mode, an error will be returned and Database Engine Tuning Advisor will not start. For more information about single-user mode, see [Start SQL Server in Single-User Mode](../../database-engine/configure-windows/start-sql-server-in-single-user-mode.md).  
  
#### To start Database Engine Tuning Advisor from the Windows Start menu  
  
1.  On the **Start** menu, point to **All Programs**, point to **Microsoft SQL Server**, point to **Performance Tools**, and then click **Database Engine Tuning Advisor**.  
  
#### To start the Database Engine Tuning Advisor in SQL Server Management Studio  
  
1.  On the [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] **Tools** menu, click **Database Engine Tuning Advisor**.  
  
#### To start the Database Engine Tuning Advisor from the SQL Server Management Studio Query Editor  
  
1.  Open a [!INCLUDE[tsql](../../includes/tsql-md.md)] script file in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. For more information, see [Query and Text Editors &#40;SQL Server Management Studio&#41;](../scripting/query-and-text-editors-sql-server-management-studio.md).  
  
2.  Select a query in the [!INCLUDE[tsql](../../includes/tsql-md.md)] script, or select the entire script, right-click the selection, and choose **Analyze Query in Database Engine Tuning Advisor**. The Database Engine Tuning Advisor GUI opens and imports the script as an XML file workload. You can specify a session name and tuning options to tune the selected [!INCLUDE[tsql](../../includes/tsql-md.md)] queries as your workload.  
  
#### To start the Database Engine Tuning Advisor in SQL Server Profiler  
  
1.  On the SQL Server Profiler **Tools** menu, click **Database Engine Tuning Advisor**.  
  
##  <a name="Create"></a> Create a Workload  
 A workload is a set of [!INCLUDE[tsql](../../includes/tsql-md.md)] statements that execute against a database or databases that you want to tune. Database Engine Tuning Advisor analyzes these workloads to recommend indexes or partitioning strategies that will improve your server's query performance.  
  
 You can create a workload by using one of the following methods.  
  
-   Use the plan cache as a workload. By doing this, you can avoid having to manually create a workload. For more information, see [Tune a Database](#Tune) later in this topic.  
  
-   Use the Query Editor in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or your favorite text editor to manually create [!INCLUDE[tsql](../../includes/tsql-md.md)] script workloads.  
  
-   Use [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] to create trace file or trace table workloads  
  
    > [!NOTE]  
    >  When using a trace table as a workload, that table must exist on the same server where Database Engine Tuning Advisor is tuning. If you create the trace table on a different server, then move it to the server where Database Engine Tuning Advisor is tuning.  
  
-   Workloads can also be embedded in an XML input file, where you can also specify a weight for each event. For more information about specifying embedded workloads, see [Create an XML Input File](#XMLInput) later in this topic.  
  
###  <a name="SSMS"></a> To create Transact-SQL script workloads  
  
1.  Launch the Query Editor in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. For more information, see [Query and Text Editors &#40;SQL Server Management Studio&#41;](../scripting/query-and-text-editors-sql-server-management-studio.md).  
  
2.  Type your [!INCLUDE[tsql](../../includes/tsql-md.md)] script into the Query Editor. This script should contain a set of [!INCLUDE[tsql](../../includes/tsql-md.md)] statements that execute against the database or databases that you want to tune.  
  
3.  Save the file with a **.sql** extension. The Database Engine Tuning Advisor GUI and the command-line **dta** utility can use this [!INCLUDE[tsql](../../includes/tsql-md.md)] script as a workload.  
  
###  <a name="Profiler"></a> To create trace file and trace table workloads  
  
1.  Launch [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] by using one of the following methods:  
  
    -   On the **Start** menu, point to **All Programs**, **Microsoft SQL Server**, **Performance Tools**, and then click **SQL Server Profiler**.  
  
    -   In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], click the **Tools** menu, and then click **SQL Server Profiler**.  
  
2.  Create a trace file or table as described in the following procedures that uses the [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] **Tuning** template:  
  
    -   [Create a Trace &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/create-a-trace-sql-server-profiler.md)  
  
    -   [Save Trace Results to a File &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/save-trace-results-to-a-file-sql-server-profiler.md)  
  
         Database Engine Tuning Advisor assumes that the workload trace file is a rollover file. For more information about rollover files, see [Limit Trace File and Table Sizes](../sql-trace/limit-trace-file-and-table-sizes.md).  
  
    -   [Save Trace Results to a Table &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/save-trace-results-to-a-table-sql-server-profiler.md)  
  
         Make sure that tracing has stopped before using a trace table as a workload.  
  
 We recommend that you use the SQL Server Profiler Tuning template for capturing workloads for Database Engine Tuning Advisor.  
  
 If you want to use your own template, ensure that the following trace events are captured:  
  
-   **RPC:Completed**  
  
-   **SQL:BatchCompleted**  
  
-   **SP:StmtCompleted**  
  
 You can also use the **Starting** versions of these trace events. For example, **SQL:BatchStarting**. However, the **Completed** versions of these trace events include the **Duration** column, which allows Database Engine Tuning Advisor to more effectively tune the workload. Database Engine Tuning Advisor does not tune other types of trace events. For more information about these trace events, see [Stored Procedures Event Category](../event-classes/stored-procedures-event-category.md) and [TSQL Event Category](../event-classes/tsql-event-category.md). For information about using the SQL Trace stored procedures to create a trace file workload, see [Create a Trace &#40;Transact-SQL&#41;](../sql-trace/create-a-trace-transact-sql.md).  
  
### Trace File or Trace Table Workloads That Contain the LoginName Data Column  
 Database Engine Tuning Advisor submits Showplan requests as part of the tuning process. When a trace table or file that contains the **LoginName** data column is consumed as a workload, Database Engine Tuning Advisor impersonates the user specified in **LoginName**. If this user has not been granted the SHOWPLAN permission, which enables the user to execute and produce Showplans for the statements contained in the trace, Database Engine Tuning Advisor will not tune those statements.  
  
##### To avoid granting the SHOWPLAN permission to each user specified in the LoginName column of the trace  
  
1.  Tune the trace file or table workload. For more information, see [Tune a Database](#Tune) later in this topic.  
  
2.  Check the tuning log for statements that were not tuned due to inadequate permissions. For more information, see [View and Work with the Output from the Database Engine Tuning Advisor](database-engine-tuning-advisor.md).  
  
3.  Create a new workload by deleting the **LoginName** column from the events that were not tuned, and then save only the untuned events in a new trace file or table. For more information about deleting data columns from a trace, see [Specify Events and Data Columns for a Trace File &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/specify-events-and-data-columns-for-a-trace-file-sql-server-profiler.md) or [Modify an Existing Trace &#40;Transact-SQL&#41;](../sql-trace/modify-an-existing-trace-transact-sql.md).  
  
4.  Resubmit the new workload without the **LoginName** column to Database Engine Tuning Advisor.  
  
 Database Engine Tuning Advisor will tune the new workload because login information is not specified in the trace. If the **LoginName** does not exist for a statement, Database Engine Tuning Advisor tunes that statement by impersonating the user who started the tuning session (a member of either the **sysadmin** fixed server role or the **db_owner** fixed database role).  
  
##  <a name="Tune"></a> Tune a Database  
 To tune a database, you can use the Database Engine Tuning Advisor GUI or the **dta** utility.  
  
> [!NOTE]  
>  Make sure that tracing has stopped before using a trace table as a workload for Database Engine Tuning Advisor. Database Engine Tuning Advisor does not support using a trace table to which trace events are still being written as a workload.  
  
### Use the Database Engine Tuning Advisor Graphical User Interface  
 On the Database Engine Tuning Advisor GUI, you can tune a database by using the plan cache, workload files, or workload tables. You can use the Database Engine Tuning Advisor GUI to easily view the results of your current tuning session and results of previous tuning sessions. For information about user interface options, see [User Interface Descriptions](#UI) later in this topic. For more information about working with the output after you tune a database, see [View and Work with the Output from the Database Engine Tuning Advisor](database-engine-tuning-advisor.md).  
  
####  <a name="PlanCache"></a> To tune a database by using the plan cache  
  
1.  Launch Database Engine Tuning Advisor, and log into an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [Start the Database Engine Tuning Advisor](#Start) earlier in this topic.  
  
2.  On the **General** tab, type a name in **Session name** to create a new tuning session. You must configure the fields in the **General** tab before starting a tuning session. It is not necessary to modify the settings of the **Tuning Options** tab before starting a tuning session.  
  
3.  Select **Plan Cache** as the workload option. Database Engine Tuning Advisor selects the top 1,000 events from the plan cache to use for analysis.  
  
4.  Select the database or databases that you want to tune, and optionally from **Selected Tables**, choose one or more tables from each database. To include cache entries for all databases, from **Tuning Options**, click **Advanced Options** and then check **Include plan cache events from all databases**.  
  
5.  Check **Save tuning log** to save a copy of the tuning log. Clear the check box if you do not want to save a copy of the tuning log.  
  
     You can view the tuning log after analysis by opening the session and selecting the **Progress** tab.  
  
6.  Click the **Tuning Options** tab and select from the options listed there.  
  
7.  Click **Start Analysis**.  
  
     If you want to stop the tuning session after it has started, choose one of the following options on the **Actions** menu:  
  
    -   **Stop Analysis (With Recommendations)** stops the tuning session and prompts you to decide whether you want Database Engine Tuning Advisor to generate recommendations based on the analysis done up to this point.  
  
    -   **Stop Analysis** stops the tuning session without generating any recommendations.  
  
> [!NOTE]  
>  Pausing Database Engine Tuning Advisor is not supported. If you click the **Start Analysis** toolbar button after clicking either the **Stop Analysis** or **Stop Analysis (With Recommendations)** toolbar buttons, Database Engine Tuning Advisor starts a new tuning session.  
  
##### To tune a database using a workload file or table as input  
  
1.  Determine the database features (indexes, indexed views, partitioning) you want Database Engine Tuning Advisor to consider adding, removing, or retaining during analysis.  
  
2.  Create a workload. For more information, see [Create a Workload](#Create) earlier in this topic.  
  
3.  Launch Database Engine Tuning Advisor, and log into an instance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [Start the Database Engine Tuning Advisor](#Start) earlier in this topic.  
  
4.  On the **General** tab, type a name in **Session name** to create a new tuning session.  
  
5.  Choose either a **Workload File** or **Table** and type either the path to the file, or the name of the table in the adjacent text box.  
  
     The format for specifying a table is  
  
    ```  
  
    database_name.schema_name.table_name  
    ```  
  
     To search for a workload file or table, click **Browse**. Database Engine Tuning Advisor assumes that workload files are rollover files. For more information about rollover files, see [Limit Trace File and Table Sizes](../sql-trace/limit-trace-file-and-table-sizes.md).  
  
     When using a trace table as a workload, that table must exist on the same server that Database Engine Tuning Advisor is tuning. If you create the trace table on a different server, move it to the server that Database Engine Tuning Advisor is tuning before using it as your workload.  
  
6.  Select the databases and tables against which you wish to run the workload that you selected in step 5. To select the tables, click the **Selected Tables** arrow.  
  
7.  Check **Save tuning log** to save a copy of the tuning log. Clear the check box if you do not want to save a copy of the tuning log.  
  
     You can view the tuning log after analysis by opening the session and selecting the **Progress** tab.  
  
8.  Click the **Tuning Options** tab and select from the options listed there.  
  
9. Click the **Start Analysis** button in the toolbar.  
  
     If you want to stop the tuning session after it has started, choose one of the following options on the **Actions** menu:  
  
    -   **Stop Analysis (With Recommendations)** stops the tuning session and prompts you to decide whether you want Database Engine Tuning Advisor to generate recommendations based on the analysis done up to this point.  
  
    -   **Stop Analysis** stops the tuning session without generating any recommendations.  
  
> [!NOTE]  
>  Pausing Database Engine Tuning Advisor is not supported. If you click the **Start Analysis** toolbar button after clicking either the **Stop Analysis** or **Stop Analysis (With Recommendations)** toolbar buttons, Database Engine Tuning Advisor starts a new tuning session.  
  
###  <a name="dta"></a> Use the dta Utility  
 The [dta utility](../../tools/dta/dta-utility.md) provides a command prompt executable file that you can use to tune databases. It enables you to use Database Engine Tuning Advisor functionality in batch files and scripts. The **dta** utility takes plan cache entries, trace files, trace tables, and [!INCLUDE[tsql](../../includes/tsql-md.md)] scripts as workloads. It also takes XML input that conforms to the Database Engine Tuning Advisor XML schema, which is available at this [Microsoft Web site](https://go.microsoft.com/fwlink/?linkid=43100).  
  
 Consider the following before you begin tuning a workload with the **dta** utility:  
  
-   When using a trace table as a workload, that table must exist on the same server that Database Engine Tuning Advisor is tuning. If you create the trace table on a different server, then move it to the server that Database Engine Tuning Advisor is tuning.  
  
-   Make sure that tracing has stopped before using a trace table as a workload for Database Engine Tuning Advisor. Database Engine Tuning Advisor does not support using a trace table to which trace events are still being written as a workload.  
  
-   If a tuning session continues running longer than you had anticipated it would run, you can press CTRL+C to stop the tuning session and generate recommendations based on the analysis **dta** has completed up to this point. You will be prompted to decide whether you want to generate recommendations or not. Press CTRL+C again to stop the tuning session without generating recommendations.  
  
 For more information about **dta** utility syntax and examples, see [dta Utility](../../tools/dta/dta-utility.md).  
  
##### To tune a database by using the plan cache  
  
1.  Specify the **-ip** option. The top 1,000 plan cache events for the selected databases are analyzed.  
  
     From a command prompt, enter the following:  
  
    ```  
    dta -E -D DatabaseName -ip -s SessionName  
    ```  
  
2.  To modify the number of events to use for analysis, specify the **-n** option. The following example increases the number of cache entries to 2,000.  
  
    ```  
    dta -E -D DatabaseName -ip -n 2000-s SessionName1  
    ```  
  
3.  To analyze events for all databases in the instance, specify the **-ipf** option.  
  
    ```  
    dta -E -D DatabaseName -ip -ipf -n 2000 -s SessionName2  
    ```  
  
##### To tune a database by using a workload and dta utility default settings  
  
1.  Determine the database features (indexes, indexed views, partitioning) you want Database Engine Tuning Advisor to consider adding, removing, or retaining during analysis.  
  
2.  Create a workload. For more information, see [Create a Workload](#Create) earlier in this topic.  
  
3.  From a command prompt, enter the following:  
  
    ```  
    dta -E -D DatabaseName -if WorkloadFile -s SessionName  
    ```  
  
     where `-E` specifies that your tuning session uses a trusted connection (instead of a login ID and password), `-D` specifies the name of the database you want to tune. By default, the utility connects to the default instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on the local computer. (Use the `-S` option to specify a remote database as shown in the following procedure, or to specify a named instance.) The `-if` option specifies the name and path to a workload file (which can be a [!INCLUDE[tsql](../../includes/tsql-md.md)] script or a trace file), and `-s` specifies a name for your tuning session.  
  
     The four options shown here (database name, workload, connection type, and session name) are mandatory.  
  
##### To tune a remote database or a named instance for a specific duration  
  
1.  Determine the database features (indexes, indexed views, partitioning) you want Database Engine Tuning Advisor to consider adding, removing, or retaining during analysis.  
  
2.  Create a workload. For more information, see [Create a Workload](#Create) earlier in this topic.  
  
3.  From a command prompt, enter the following:  
  
    ```  
    dta -S ServerName\Instance -D DatabaseName -it WorkloadTableName   
    -U LoginID -P Password -s SessionName -A TuningTimeInMinutes  
    ```  
  
     where `-S` specifies a remote server name and instance (or a named instance on the local server) and `-D` specifies the name of the database you want to tune. The `-it` option specifies the name of the workload table, `-U` and `-P` specify the login ID and password to the remote database, `-s` specifies the tuning session name, and `-A` specifies the tuning session duration in minutes. By default, the **dta** utility uses an 8-hour tuning duration. If you would like Database Engine Tuning Advisor to tune a workload for an unlimited amount of time, specify **0** (zero) with the `-A` option.  
  
##### To tune a database using an XML input file  
  
1.  Determine the database features (indexes, indexed views, partitioning) you want Database Engine Tuning Advisor to consider adding, removing, or retaining during analysis.  
  
2.  Create a workload. For more information, see [Create a Workload](#Create) earlier in this topic.  
  
3.  Create an XML input file. For more information, see [Create XML Input Files](#XMLInput) later in this topic.  
  
4.  From a command prompt, enter the following:  
  
    ```  
    dta -E -S ServerName\Instance -s SessionName -ix PathToXMLInputFile  
    ```  
  
     where `-E` specifies a trusted connection, `-S` specifies a remote server and instance, or a named instance on the local server, `-s` specifies a tuning session name, and `-ix` specifies the XML input file to use for the tuning session.  
  
5.  After the utility finishes tuning the workload, you can view the results of tuning sessions with the Database Engine Tuning Advisor GUI. As an alternative, you can also specify that the tuning recommendations be written to an XML file with the **-ox** option. For more information, see [dta Utility](../../tools/dta/dta-utility.md).  
  
##  <a name="XMLInput"></a> Create an XML Input File  
 If you are an experienced XML developer, you can create XML-formatted files that [!INCLUDE[ssDE](../../includes/ssde-md.md)] Tuning Advisor can use to tune workloads. To create these XML files, use your favorite XML tools to edit a sample file or to generate an instance from the [!INCLUDE[ssDE](../../includes/ssde-md.md)] Tuning Advisor XML schema.  
  
 The [!INCLUDE[ssDE](../../includes/ssde-md.md)] Tuning Advisor XML schema is available in your [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation in the following location:  
  
 C:\Program Files\Microsoft SQL Server\100\Tools\Binn\schemas\sqlserver\2004\07\dta\dtaschema.xsd  
  
 The [!INCLUDE[ssDE](../../includes/ssde-md.md)] Tuning Advisor XML schema is also available online at this [Microsoft Web site](https://go.microsoft.com/fwlink/?linkid=43100&clcid=0x409).  
  
 This URL takes you to a page where many [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] XML schemas are available. Scroll down the page until you reach the row for [!INCLUDE[ssDE](../../includes/ssde-md.md)] Tuning Advisor.  
  
#### To create an XML input file to tune workloads  
  
1.  Create a workload. You can use a trace file or table by using the tuning template in [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)], or create a [!INCLUDE[tsql](../../includes/tsql-md.md)] script that reproduces a representative workload for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [Create a Workload](#Create) earlier in this topic.  
  
2.  Create an XML input file by one of the following methods:  
  
    -   Copy and paste one of the [XML Input File Samples &#40;DTA&#41;](../../tools/dta/xml-input-file-samples-dta.md) into your favorite XML editor. Change the values to specify the appropriate arguments for your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation, and save the XML file.  
  
    -   Using your favorite XML tool, generate an instance from the [!INCLUDE[ssDE](../../includes/ssde-md.md)] Tuning Advisor XML schema.  
  
3.  After creating the XML input file, use it as input to the **dta** command-line utility to tune the workload. For information about using XML input files with this utility, see the section [Use the dta Utililty](#dta) earlier in this topic.  
  
> [!NOTE]  
>  If you want to use an inline workload, which is a workload that is specified directly in the XML input file, use the sample [XML Input File Sample with Inline Workload &#40;DTA&#41;](../../tools/dta/xml-input-file-sample-with-inline-workload-dta.md).  
  
##  <a name="UI"></a> User Interface Descriptions  
  
### Tools Menu/Options Page  
 Use this dialog box to specify general configuration parameters for the Database Engine Tuning Advisor.  
  
 **On startup**  
 Specify what Database Engine Tuning Advisor should do when it is started: open without a database connection, show a **New Connection** dialog box, show a new session, or load the last loaded session.  
  
 **Change font**  
 Specify the display font used by Database Engine Tuning Advisor tables.  
  
 **Number of items in most recently used lists**  
 Specify the number of sessions or files to display under **Recent Sessions** or **Recent Files** in the **File** menu.  
  
 **Remember my last tuning options**  
 Retain tuning options between sessions. Selected by default. Clear this check box to always start with the Database Engine Tuning Advisor defaults.  
  
 **Ask before permanently deleting sessions**  
 Display a confirmation dialog box before deleting sessions.  
  
 **Ask before stopping session analysis**  
 Display a confirmation dialog box before stopping analysis of a workload.  
  
#### General Tab Options  
 You must configure the fields in the **General** tab before starting a tuning session. You do not have to modify the settings of the **Tuning Options** tab before starting a tuning session.  
  
 **Session name**  
 Specify a name for the session. The session name associates a name with a tuning session. You can refer to this name to review the tuning session later.  
  
 **File**  
 Specify a .sql script or trace file for a workload. Specify the path and filename in the associated text box. Database Engine Tuning Advisor assumes that the workload trace file is a rollover file. For more information about rollover files, see [Limit Trace File and Table Sizes](../sql-trace/limit-trace-file-and-table-sizes.md).  
  
 **Table**  
 Specify a trace table for a workload. Specify the fully qualified name of the trace table in the associated text box as follows:  
  
```  
database_name.owner_name.table_name  
```  
  
-   Make sure that tracing has stopped before using a trace table as a workload.  
  
-   The trace table must exist on the same server that Database Engine Tuning Advisor is tuning. If you create the trace table on a different server, then move it to the server that Database Engine Tuning Advisor is tuning.  
  
 **Plan Cache**  
 Specify the plan cache as a workload. By doing this, you can avoid having to manually create a workload. Database Engine Tuning Advisor selects the top 1,000 events to use for analysis.  
  
 **Xml**  
 This does not appear unless you import a workload query from [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
 To import a workload query from [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]:  
  
1.  Type a query into Query Editor and highlight it.  
  
2.  Right-click the highlighted query and click **Analyze Query in Database Engine Tuning Advisor**.  
  
 **Browse for a workload [file or table]**  
 When **File** or **Table** is selected as the workload source, use this browse button to select the target.  
  
 **Preview the XML workload**  
 View an XML-formatted workload that has been imported from [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
 **Database for workload analysis**  
 Specify the first database to which Database Engine Tuning Advisor connects when tuning a workload. After tuning begins, Database Engine Tuning Advisor connects to the databases specified by the `USE DATABASE` statements contained in the workload.  
  
 **Select databases and tables to tune**  
 Specify the databases and tables to be tuned. To specify all of the databases, select the check box in the **Name** column heading. To specify certain databases, select the check box next to the database name. By default, all of the tables for selected databases are automatically included in the tuning session. To exclude tables, click the arrow in the **Selected Tables** column, and then clear the check boxes next to the tables that you do not want to tune.  
  
 **Selected Tables** down arrow  
 Expand the tables list to allow selecting individual tables for tuning.  
  
 **Save tuning log**  
 Create a log and record errors during the session.  
  
> [!NOTE]  
>  Database Engine Tuning Advisor does not automatically update the rows information for the tables displayed on the **General** tab. Instead it relies upon the metadata in the database. If you suspect that the rows information is outdated, run the DBCC UPDATEUSAGE command for the relevant objects.  
  
##### Tuning Tab Options  
 Use the **Tuning Options** tab to modify default settings of general tuning options. You do not have to modify the settings of the **Tuning Options** tab before starting a tuning session.  
  
 **Limit tuning time**  
 Limits the time for the current tuning session. Providing more time for turning improves the quality of the recommendations. To ensure the best recommendations, do not select this option.  
  
> [!NOTE]  
>  [!INCLUDE[ssDE](../../includes/ssde-md.md)] Tuning Advisor consumes system resources during analysis. Use **Limit tuning time** to stop tuning before periods of anticipated heavy workload on the server being tuned.  
  
 **Advanced Options**  
 Use the **Advanced Tuning Options** dialog box to configure the maximum space, maximum key columns, and online index recommendations.  
  
 **Define max. space for recommendations (MB)**  
 Type the maximum amount of space to be used by physical design structures recommended by Database Engine Tuning Advisor.  
  
 If no value is entered here, Database Engine Tuning Advisor assumes the smaller of the following space limits:  
  
-   Three times the current raw data size, which includes the total size of heaps and clustered indexes on tables in the database.  
  
-   The free space on all attached disk drives plus the raw data size.  
  
 **Include plan cache events from all databases**  
 Specify that plan cache events from all databases are analyzed.  
  
 **Max. columns per index**  
 Specify the maximum number of columns to include in any index. The default is 1023.  
  
 **All recommendations are offline**  
 Generate the best recommendations possible, but do not recommend that any physical design structures be created online.  
  
 **Generate online recommendations where possible**  
 When creating [!INCLUDE[tsql](../../includes/tsql-md.md)] statements to implement the recommendations, choose methods that can be implemented with the server online, even if a faster offline method is available.  
  
 **Generate only online recommendations**  
 Only make recommendations that allow the server to stay online.  
  
 **Stop at**  
 Provide the date and time when [!INCLUDE[ssDE](../../includes/ssde-md.md)] Tuning Advisor should stop.  
  
 **Indexes and indexed views**  
 Check this box to include recommendations for adding clustered indexes, nonclustered indexes, and indexed views.  
  
 **Indexed views**  
 Only include recommendations for adding indexed views. Clustered and nonclustered indexes will not be recommended.  
  
 **Include filtered indexes**  
 Include recommendations for adding filtered indexes. This option is available if you select one of these physical design structures: **Indexes and indexed views**, **Indexes**, or **Nonclustered indexes**.  
  
 **Indexes**  
 Only include recommendations for adding clustered and nonclustered indexes. Indexed views will not be recommended.  
  
 **Nonclustered indexes**  
 Include recommendations for only nonclustered indexes. Clustered indexes and indexed views will not be recommended.  
  
 **Evaluate utilization of existing PDS only**  
 Evaluate the effectiveness of the current indexes but do not recommend additional indexes or indexed views.  
  
 **No partitioning**  
 Do not recommend partitioning.  
  
 **Full partitioning**  
 Include recommendations for partitioning.  
  
 **Aligned partitioning**  
 New recommended partitions will be aligned to make partitions easy to maintain.  
  
 **Do not keep any existing PDS**  
 Recommend dropping unnecessary existing indexes, views, and partitioning. If an existing physical design structure (PDS) is useful to the workload, [!INCLUDE[ssDE](../../includes/ssde-md.md)] Tuning Advisor does not recommend dropping it.  
  
 **Keep indexes only**  
 Keep all existing indexes but recommend dropping unnecessary indexed views, and partitioning.  
  
 **Keep all existing PDS**  
 Keep all existing indexes, indexed views, and partitioning.  
  
 **Keep clustered indexes only**  
 Keep all existing clustered indexes but recommend dropping unnecessary indexed views, partitions, and nonclustered indexes.  
  
 **Keep aligned partitioning**  
 Keep partitioning structures that are currently aligned, but recommend dropping unnecessary indexed views, indexes, and non-aligned partitioning. Any additional partitioning recommended will align with the current partitioning scheme.  
  
###### Progress Tab Options  
 The **Progress** tab of Database Engine Tuning Advisor appears after Database Engine Tuning Advisor begins analyzing a workload.  
  
 If you want to stop the tuning session after it has started, choose one of the following options on the **Actions** menu:  
  
-   **Stop Analysis (With Recommendations)** stops the tuning session and prompts you to decide whether you want Database Engine Tuning Advisor to generate recommendations based on the analysis done up to this point.  
  
-   **Stop Analysis** stops the tuning session without generating any recommendations.  
  
 **Tuning Progress**  
 Indicates the current status of the progress. Contains the number of actions performed, and the number of error, success, and warning messages received.  
  
 **Details**  
 Contains an icon indicating status.  
  
 **Action**  
 Displays the steps being performed.  
  
 **Status**  
 Displays the status of the action step.  
  
 **Message**  
 Contains any messages returned by the action steps.  
  
 **Tuning Log**  
 Contains information regarding this tuning session. To print this log, right-click the log, and then click **Print**.  
  
## See Also  
 [View and Work with the Output from the Database Engine Tuning Advisor](database-engine-tuning-advisor.md)   
 [dta Utility](../../tools/dta/dta-utility.md)  
  
  
