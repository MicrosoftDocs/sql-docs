---
title: "dta Utility | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: tools-other
ms.topic: conceptual
helpviewer_keywords: 
  - "physical design structures [SQL Server]"
  - "command prompt utilities [SQL Server], dta"
  - "dta utility"
  - "tuning databases [SQL Server], Database Engine Tuning Advisor"
  - "workloads [SQL Server], analyzing"
  - "dta utility, about dta utility"
  - "performance [SQL Server], Database Engine Tuning Advisor"
  - "Database Engine Tuning Advisor [SQL Server], command prompt"
  - "optimizing databases [SQL Server]"
ms.assetid: a0b210ce-9b58-4709-80cb-9363b68a1f5a
author: stevestein
ms.author: sstein
manager: craigg
---
# dta Utility
  The **dta** utility is the command prompt version of Database Engine Tuning Advisor. The **dta** utility is designed to allow you to use Database Engine Tuning Advisor functionality in applications and scripts.  
  
 Like Database Engine Tuning Advisor, the **dta** utility analyzes a workload and recommends physical design structures to improve server performance for that workload. The workload can be a plan cache, a [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] trace file or table, or a [!INCLUDE[tsql](../../includes/tsql-md.md)] script. Physical design structures include indexes, indexed views, and partitioning. After analyzing a workload, the **dta** utility produces a recommendation for the physical design of databases and can generate the necessary script to implement the recommendation. Workloads can be specified from the command prompt with the **-if** or the **-it** argument. You can also specify an XML input file from the command prompt with the **-ix** argument. In that case, the workload is specified in the XML input file.  
  
## Syntax  
  
```  
  
      dta  
[ -? ] |  
[  
      [ -S server_name[ \instance ] ]  
      { { -U login_id [-P password ] } | -E  }  
      { -D database_name [ ,...n ] }  
      [ -ddatabase_name ]   
      [ -Tltable_list | -Tf table_list_file ]  
      { -if workload_file | -it workload_trace_table_name  |   
        -ip | -ipf }  
      { -ssession_name | -IDsession_ID }  
      [ -F ]  
      [ -of output_script_file_name ]  
      [ -oroutput_xml_report_file_name ]  
      [ -ox output_XML_file_name ]  
      [ -rl analysis_report_list [ ,...n ] ]  
      [ -ix input_XML_file_name ]  
      [ -A time_for_tuning_in_minutes ]  
      [ -nnumber_of_events ]  
      [ -m minimum_improvement ]  
      [ -fa physical_design_structures_to_add ]  
      [ -fi ]  
      [ -fppartitioning_strategy ]  
      [ -fk keep_existing_option ]  
      [ -fxdrop_only_mode ]  
      [ -B storage_size ]  
      [ -cmax_key_columns_in_index ]  
      [ -C max_columns_in_index ]  
      [ -e | -e tuning_log_name ]  
      [ -N online_option]  
      [ -q ]  
      [ -u ]  
      [ -x ]  
      [ -a ]  
]  
```  
  
## Arguments  
 **-?**  
 Displays usage information.  
  
 **-A** _time_for_tuning_in_minutes_  
 Specifies the tuning time limit in minutes. **dta** uses the specified amount of time to tune the workload and generate a script with the recommended physical design changes. By default **dta** assumes a tuning time of 8 hours. Specifying 0allows unlimited tuning time. **dta** might finish tuning the entire workload before the time limit expires. However, to make sure that the entire workload is tuned, we recommend that you specify unlimited tuning time (-A 0).  
  
 **-a**  
 Tunes workload and applies the recommendation without prompting you.  
  
 **-B** _storage_size_  
 Specifies the maximum space in megabytes that can be consumed by the recommended index and partitioning. When multiple databases are tuned, recommendations for all databases are considered for the space calculation. By default, **dta** assumes the smaller of the following storage sizes:  
  
-   Three times the current raw data size, which includes the total size of heaps and clustered indexes on tables in the database.  
  
-   The free space on all attached disk drives plus the raw data size.  
  
 The default storage size does not include nonclustered indexes and indexed views.  
  
 **-C** _max_columns_in_index_  
 Specifies the maximum number of columns in indexes that **dta** proposes. The maximum value  is 1024. By default, this argument is set to 16.  
  
 **-c** _max_key_columns_in_index_  
 Specifies the maximum number of key columns in indexes that **dta** proposes. The default value is 16, the maximum value allowed. **dta** also considers creating indexes with included columns. Indexes recommended with included columns may exceed the number of columns specified in this argument.  
  
 **-D** _database_name_  
 Specifies the name of each database that is to be tuned. The first database is the default database. You can specify multiple databases by separating the database names with commas, for example:  
  
```  
dta -D database_name1, database_name2...  
```  
  
 Alternatively, you can specify multiple databases by using the **-D** argument for each database name, for example:  
  
```  
dta -D database_name1 -D database_name2... n  
```  
  
 The **-D** argument is mandatory. If the **-d** argument has not been specified, **dta** initially connects to the database that is specified with the first `USE database_name` clause in the workload. If there is not explicit `USE database_name` clause in the workload, you must use the **-d** argument.  
  
 For example, if you have a workload that contains no explicit `USE database_name` clause, and you use the following **dta** command, a recommendation will not be generated:  
  
```  
dta -D db_name1, db_name2...  
```  
  
 But if you use the same workload, and use the following **dta** command that uses the **-d** argument, a recommendation will be generated:  
  
```  
dta -D db_name1, db_name2 -d db_name1  
```  
  
 **-d** _database_name_  
 Specifies the first database to which **dta** connects when tuning a workload. Only one database can be specified for this argument. For example:  
  
```  
dta -d AdventureWorks2012 ...  
```  
  
 If multiple database names are specified, then **dta** returns an error. The **-d** argument is optional.  
  
 If you are using an XML input file, you can specify the first database to which **dta** connects by using the `DatabaseToConnect` element that is located under the `TuningOptions` element. For more information, see [Database Engine Tuning Advisor](../../relational-databases/performance/database-engine-tuning-advisor.md).  
  
 If you are tuning only one database, the **-d** argument provides functionality that is similar to the **-d** argument in the **sqlcmd** utility, but it does not execute the USE *database_name* statement. For more information, see [sqlcmd Utility](../sqlcmd-utility.md).  
  
 **-E**  
 Uses a trusted connection instead of requesting a password. Either the **-E** argument or the **-U** argument, which specifies a login ID, must be used.  
  
 **-e** _tuning_log_name_  
 Specifies the name of the table or file where **dta** records events that it could not tune. The table is created on the server where the tuning is performed.  
  
 If a table is used, specify its name in the format: *[database_name].[owner_name].table_name*. The following table shows the default values for each parameter:  
  
|Parameter|Default value|  
|---------------|-------------------|  
|*database_name*|*database_name* specified with the **-D** option|  
|*owner_name*|**dbo**<br /><br /> Note: *owner_name* must be **dbo**. If any other value is specified, then **dta** execution fails and it returns an error.|  
|*table_name*|None|  
  
 If a file is used, specify .xml as its extension. For example, TuningLog.xml.  
  
> [!NOTE]  
>  The **dta** utility does not delete the contents of user-specified tuning log tables if the session is deleted. When tuning very large workloads, we recommend that a table be specified for the tuning log. Since tuning large workloads can result in large tuning logs, the sessions can be deleted much faster when a table is used.  
  
 **-F**  
 Permits **dta** to overwrite an existing output file. If an output file with the same name already exists and **-F** is not specified, **dta**returns an error. You can use **-F** with **-of**, **-or**, or **-ox**.  
  
 **-fa** _physical_design_structures_to_add_  
 Specifies what types of physical design structures **dta** should include in the recommendation. The following table lists and describes the values that can be specified for this argument. When no value is specified, **dta** uses the default **-fa**`IDX`.  
  
|Value|Description|  
|-----------|-----------------|  
|IDX_IV|Indexes and indexed views.|  
|IDX|Indexes only.|  
|IV|Indexed views only.|  
|NCL_IDX|Nonclustered indexes only.|  
  
 **-fi**  
 Specifies that filtered indexes be considered for new recommendations. For more information, see [Create Filtered Indexes](../../relational-databases/indexes/create-filtered-indexes.md).  
  
 **-fk** _keep_existing_option_  
 Specifies what existing physical design structures **dta** must retain when generating its recommendation. The following table lists and describes the values that can be specified for this argument:  
  
|Value|Description|  
|-----------|-----------------|  
|NONE|No existing structures|  
|ALL|All existing structures|  
|ALIGNED|All partition-aligned structures.|  
|CL_IDX|All clustered indexes on tables|  
|IDX|All clustered and nonclustered indexes on tables|  
  
 **-fp** _partitioning_strategy_  
 Specifies whether new physical design structures (indexes and indexed views) that **dta** proposes should be partitioned, and how they should be partitioned. The following table lists and describes the values that can be specified for this argument:  
  
|Value|Description|  
|-----------|-----------------|  
|NONE|No partitioning|  
|FULL|Full partitioning (choose to enhance performance)|  
|ALIGNED|Aligned partitioning only (choose to enhance manageability)|  
  
 ALIGNED means that in the recommendation generated by **dta** every proposed index is partitioned in exactly the same way as the underlying table for which the index is defined. Nonclustered indexes on an indexed view are aligned with the indexed view. Only one value can be specified for this argument. The default is **-fp**`NONE`.  
  
 **-fx** _drop_only_mode_  
 Specifies that **dta** only considers dropping existing physical design structures. No new physical design structures are considered. When this option is specified, **dta** evaluates the usefulness of existing physical design structures and recommends dropping seldom used structures. This argument takes no values. It cannot be used with the **-fa**, **-fp**, or **-fk ALL** arguments  
  
 **-ID** _session_ID_  
 Specifies a numerical identifier for the tuning session. If not specified, then **dta** generates an ID number. You can use this identifier to view information for existing tuning sessions. If you do not specify a value for **-ID**, then a session name must be specified with **-s**.  
  
 **-ip**  
 Specifies that the plan cache be used as the workload. The top 1,000 plan cache events for explicitly selected databases are analyzed. This value can be changed using the **-n** option.  
  
 **-ipf**  
 Specifies that the plan cache be used as the workload. The top 1,000 plan cache events for all databases are analyzed. This value can be changed using the **-n** option.  
  
 **-if** _workload_file_  
 Specifies the path and name of the workload file to use as input for tuning. The file must be in one of these formats: .trc (SQL Server Profiler trace file), .sql (SQL file), or .log ([!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] trace file). Either one workload file or one workload table must be specified.  
  
 **-it** _workload_trace_table_name_  
 Specifies the name of a table containing the workload trace for tuning. The name is specified in the format: [*database_name*]**.**[*owner_name*]**.**_table_name_.  
  
 The following table shows the default values for each:  
  
|Parameter|Default value|  
|---------------|-------------------|  
|*database_name*|*database_name* specified with **-D** option.|  
|*owner_name*|**dbo**.|  
|*table_name*|None.|  
  
> [!NOTE]  
>  *owner_name* must be **dbo**. If any other value is specified, execution of **dta** fails and an error is returned. Also note that either one workload table or one workload file must be specified.  
  
 **-ix** _input_XML_file_name_  
 Specifies the name of the XML file containing **dta** input information. This must be a valid XML document conforming to DTASchema.xsd. Conflicting arguments specified from the command prompt for tuning options override the corresponding value in this XML file. The only exception is if a user-specified configuration is entered in the evaluate mode in the XML input file. For example, if a configuration is entered in the **Configuration** element of the XML input file and the **EvaluateConfiguration** element is also specified as one of the tuning options, the tuning options specified in the XML input file will override any tuning options entered from the command prompt.  
  
 **-m** _minimum_improvement_  
 Specifies the minimum percentage of improvement that the recommended configuration must satisfy.  
  
 **-N** _online_option_  
 Specifies whether physical design structures are created online. The following table lists and describes the values you can specify for this argument:  
  
|Value|Description|  
|-----------|-----------------|  
|OFF|No recommended physical design structures can be created online.|  
|ON|All recommended physical design structures can be created online.|  
|MIXED|Database Engine Tuning Advisor attempts to recommend physical design structures that can be created online when possible.|  
  
 If indexes are created online, ONLINE = ON is appended to its object definition.  
  
 **-n** _number_of_events_  
 Specifies the number of events in the workload that **dta** should tune. If this argument is specified and the workload is a trace file that contains duration information, then **dta** tunes events in decreasing order of duration. This argument is useful to compare two configurations of physical design structures. To compare two configurations, specify the same number of events to be tuned for both configurations and then specify an unlimited tuning time for both also as follows:  
  
```  
dta -n number_of_events -A 0  
```  
  
 In this case, it is important to specify an unlimited tuning time (`-A 0`). Otherwise, Database Engine Tuning Advisor assumes an 8 hour tuning time by default.  
  
 **-of** _output_script_file_name_  
 Specifies that **dta** writes the recommendation as a [!INCLUDE[tsql](../../includes/tsql-md.md)] script to the file name and destination specified.  
  
 You can use **-F** with this option. Make sure that the file name is unique, especially if you are also using **-or** and **-ox**.  
  
 **-or** _output_xml_report_file_name_  
 Specifies that **dta** writes the recommendation to an output report in XML. If a file name is supplied, then the recommendations are written to that destination. Otherwise, **dta** uses the session name to generate the file name and writes it to the current directory.  
  
 You can use **-F** with this option. Make sure that the file name is unique, especially if you are also using **-of** and **-ox**.  
  
 **-ox** _output_XML_file_name_  
 Specifies that **dta** writes the recommendation as an XML file to the file name and destination supplied. Ensure that Database Engine Tuning Advisor has permissions to write to the destination directory.  
  
 You can use **-F** with this option. Make sure that the file name is unique, especially if you are also using **-of** and **-or**.  
  
 **-P** _password_  
 Specifies the password for the login ID. If this option is not used, **dta** prompts for a password.  
  
 **-q**  
 Sets quiet mode. No information is written to the console, including progress and header information.  
  
 **-rl** _analysis_report_list_  
 Specifies the list of analysis reports to generate. The following table lists the values that can be specified for this argument:  
  
|Value|Report|  
|-----------|------------|  
|ALL|All analysis reports|  
|STMT_COST|Statement cost report|  
|EVT_FREQ|Event frequency report|  
|STMT_DET|Statement detail report|  
|CUR_STMT_IDX|Statement-index relations report (current configuration)|  
|REC_STMT_IDX|Statement-index relations report (recommended configuration)|  
|STMT_COSTRANGE|Statement cost range report|  
|CUR_IDX_USAGE|Index usage report (current configuration)|  
|REC_IDX_USAGE|Index usage report (recommended configuration)|  
|CUR_IDX_DET|Index detail report (current configuration)|  
|REC_IDX_DET|Index detail report (recommended configuration)|  
|VIW_TAB|View-table relations report|  
|WKLD_ANL|Workload analysis report|  
|DB_ACCESS|Database access report|  
|TAB_ACCESS|Table access report|  
|COL_ACCESS|Column access report|  
  
 Specify multiple reports by separating the values with commas, for example:  
  
```  
... -rl EVT_FREQ, VIW_TAB, WKLD_ANL ...  
```  
  
 **-S** _server_name_[ *\instance*]  
 Specifies the name of the computer and instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to connect to. If no *server_name* is specified, **dta** connects to the default instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on the local computer. This option is required when connecting to a named instance or when executing **dta** from a remote computer on the network.  
  
 **-s** _session_name_  
 Specifies the name of the tuning session. This is required if **-ID** is not specified.  
  
 **-Tf** _table_list_file_  
 Specifies the name of a file containing a list of tables to be tuned. Each table listed within the file should begin on a new line. Table names should be qualified with three-part naming, for example, **AdventureWorks2012.HumanResources.Department**. Optionally, to invoke the table-scaling feature, the name of an existing table can be followed by a number indicating the projected number of rows in the table. Database Engine Tuning Advisor takes into consideration the projected number of rows while tuning or evaluating statements in the workload that reference these tables. Note that there can be one or more spaces between the *number_of_rows* count and the *table_name*.  
  
 This is the file format for *table_list_file*:  
  
 *database_name*.[*schema_name*].*table_name* [*number_of_rows*]  
  
 *database_name*.[*schema_name*].*table_name* [*number_of_rows*]  
  
 *database_name*.[*schema_name*].*table_name* [*number_of_rows*]  
  
 This argument is an alternative to entering a table list at the command prompt (**-Tl**). Do not use a table list file (**-Tf**) if you are using **-Tl**. If both arguments are used, **dta** fails and returns an error.  
  
 If the **-Tf** and **-Tl** arguments are omitted, all user tables in the specified databases are considered for tuning.  
  
 **-Tl** _table_list_  
 Specifies at the command prompt a list of tables to be tuned. Place commas between table names to separate them. If only one database is specified with the **-D** argument, then table names do not need to be qualified with a database name. Otherwise, the fully qualified name in the format: *database_name.schema_name.table_name* is required for each table.  
  
 This argument is an alternative to using a table list file (**-Tf**). If both **-Tl** and **-Tf** are used, **dta** fails and returns an error.  
  
 **-U** _login_id_  
 Specifies the login ID used to connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 **-u**  
 Launches the Database Engine Tuning Advisor GUI. All parameters are treated as the initial settings for the user interface.  
  
 **-x**  
 Starts tuning session and exits.  
  
## Remarks  
 Press CTRL+C once to stop the tuning session and generate recommendations based on the analysis **dta** has completed up to this point. You will be prompted to decide whether you want to generate recommendations or not. Press CTRL+C again to stop the tuning session without generating recommendations.  
  
## Examples  
 **A. Tune a workload that includes indexes and indexed views in its recommendation**  
  
 This example uses a secure connection (`-E`) to connect to the **tpcd1G** database on MyServer to analyze a workload and create recommendations. It writes the output to a script file named script.sql. If script.sql already exists, then **dta** will overwrite the file because the `-F` argument has been specified. The tuning session runs for an unlimited length of time to ensure a complete analysis of the workload (`-A 0`). The recommendation must provide a minimum improvement of 5% (`-m 5`). **dta** should include indexes and indexed views in its final recommendation (`-fa IDX_IV`).  
  
```  
dta -S MyServer -E -D tpcd1G -if tpcd_22.sql -F -of script.sql -A 0 -m 5 -fa IDX_IV  
```  
  
 **B. Limit disk use**  
  
 This example limits the total database size, which includes the raw data and the additional indexes, to 3 gigabytes (GB) (`-B 3000`) and directs the output to d:\result_dir\script1.sql. It runs for no more than 1 hour (`-A 60`).  
  
```  
dta -D tpcd1G -if tpcd_22.sql -B 3000 -of "d:\result_dir\script1.sql" -A 60  
```  
  
 **C. Limit the number of tuned queries**  
  
 This example limits the number of queries read from the file orders_wkld.sql to a maximum of 10 (`-n 10`) and runs for 15 minutes (`-A 15`), whichever comes first. To make sure that all 10 queries are tuned, specify an unlimited tuning time with `-A 0`. If time is important, specify an appropriate time limit by specifying the number of minutes that are available for tuning with the `-A` argument as shown in this example.  
  
```  
dta -D orders -if orders_wkld.sql -of script.sql -A 15 -n 10  
```  
  
 **D. Tune specific tables listed in a file**  
  
 This example demonstrates the use of *table_list_file* (the **-Tf** argument). The contents of the file table_list.txt are as follows:  
  
```  
AdventureWorks2012.Sales.Customer  100000  
AdventureWorks2012.Sales.Store  
AdventureWorks2012.Production.Product  2000000  
```  
  
 The contents of table_list.txt specifies that:  
  
-   Only the **Customer**, **Store**, and **Product** tables in the database should be tuned.  
  
-   The number of rows in the **Customer** and **Product** tables are assumed to be 100,000 and 2,000,000, respectively.  
  
-   The number of rows in **Store** are assumed to be the current number of rows in the table.  
  
 Note that there can be one or more spaces between the number of rows count and the preceding table name in the *table_list_file*.  
  
 The tuning time is 2 hours (`-A 120`) and the output is written to an XML file (`-ox XMLTune.xml`).  
  
```  
dta -D pubs -if pubs_wkld.sql -ox XMLTune.xml -A 120 -Tf table_list.txt  
```  
  
## See Also  
 [Command Prompt Utility Reference &#40;Database Engine&#41;](../command-prompt-utility-reference-database-engine.md)   
 [Database Engine Tuning Advisor](../../relational-databases/performance/database-engine-tuning-advisor.md)  
  
  
