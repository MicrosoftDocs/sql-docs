---
title: "SQLdiag Utility | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: tools-other
ms.topic: conceptual
helpviewer_keywords: 
  - "command prompt utilities [SQL Server], SQLdiag"
  - "stopping diagnostic collection"
  - "storing diagnostic information"
  - "performance [SQL Server], diagnostic collection"
  - "diagnostic records [SQL Server]"
  - "scripts [SQL Server], diagnostic collection"
  - "logs [SQL Server], diagnostic collection"
  - "starting diagnostic collection"
  - "clustered instance of SQL Server"
  - "monitoring performance [SQL Server], diagnostic collection"
  - "security [SQL Server], diagnostic collection"
  - "SQLDIAG service"
  - "space [SQL Server], diagnostic collection"
  - "SQLdiag utility"
  - "disk space [SQL Server], diagnostic collection"
  - "configuration files [SQL Server]"
  - "automatic diagnostic collection"
  - "clusters [SQL Server], diagnostic collection"
ms.assetid: 45ba1307-33d1-431e-872c-a6e4556f5ff2
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# SQLdiag Utility
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  The **SQLdiag** utility is a general purpose diagnostics collection utility that can be run as a console application or as a service. You can use **SQLdiag** to collect logs and data files from [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] and other types of servers, and use it to monitor your servers over time or troubleshoot specific problems with your servers. **SQLdiag** is intended to expedite and simplify diagnostic information gathering for [!INCLUDE[msCoName](../includes/msconame-md.md)] Customer Support Services.  
  
> [!NOTE]  
>  This utility may be changed, and applications or scripts that rely on its command line arguments or behavior may not work correctly in future releases.  
  
 **SQLdiag** can collect the following types of diagnostic information:  
  
-   Windows performance logs  
  
-   Windows event logs  
  
-   [!INCLUDE[ssSqlProfiler](../includes/sssqlprofiler-md.md)] traces  
  
-   [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] blocking information  
  
-   [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] configuration information  
  
 You can specify what types of information you want **SQLdiag** to collect by editing the configuration file SQLDiag.xml, which is described in a following section.  
  
## Syntax  
  
```  
  
sqldiag   
     { [/?] }  
     |  
     { [/I configuration_file]  
       [/O output_folder_path]  
       [/P support_folder_path]  
       [/N output_folder_management_option]  
       [/M machine1 [ machine2 machineN]| @machinelistfile]  
       [/C file_compression_type]  
       [/B [+]start_time]  
       [/E [+]stop_time]  
       [/A SQLdiag_application_name]  
       [/T { tcp [ ,port ] | np | lpc } ]  
       [/Q] [/G] [/R] [/U] [/L] [/X] }  
     |  
     { [START | STOP | STOP_ABORT] }  
     |  
     { [START | STOP | STOP_ABORT] /A SQLdiag_application_name }  
```  
  
## Arguments  
 **/?**  
 Displays usage information.  
  
 **/I** _configuration_file_  
 Sets the configuration file for **SQLdiag** to use. By default, **/I** is set to SQLDiag.Xml.  
  
 **/O** _output_folder_path_  
 Redirects **SQLdiag** output to the specified folder. If the **/O** option is not specified, **SQLdiag** output is written to a subfolder named SQLDIAG under the **SQLdiag** startup folder. If the SQLDIAG folder does not exist, **SQLdiag** attempts to create it.  
  
> [!NOTE]  
>  The output folder location is relative to the support folder location that can be specified with **/P**. To set an entirely different location for the output folder, specify the full directory path for **/O**.  
  
 **/P** _support_folder_path_  
 Sets the support folder path. By default, **/P** is set to the folder where the **SQLdiag** executable resides. The support folder contains **SQLdiag** support files, such as the XML configuration file, Transact-SQL scripts, and other files that the utility uses during diagnostics collection. If you use this option to specify an alternate support files path, **SQLdiag** will automatically copy the support files it requires to the specified folder if they do not already exist.  
  
> [!NOTE]  
>  To set your current folder as the support path, specify **%cd%** on the command line as follows:  
>   
>  **SQLDIAG /P %cd%**  
  
 **/N** _output_folder_management_option_  
 Sets whether **SQLdiag** overwrites or renames the output folder when it starts up. Available options:  
  
 1 = Overwrites the output folder (default)  
  
 2 = When **SQLdiag** starts up, it renames the output folder to SQLDIAG_00001, SQLDIAG_00002, and so on. After renaming the current output folder, **SQLdiag** writes output to the default output folder SQLDIAG.  
  
> [!NOTE]  
>  **SQLdiag** does not append output to the current output folder when it starts up. It can only overwrite the default output folder (option 1) or rename the folder (option 2), and then it writes output to the new default output folder named SQLDIAG.  
  
 **/M** _machine1_ [ *machine2* *machineN*] | *@machinelistfile*  
 Overrides the machines specified in the configuration file. By default the configuration file is SQLDiag.Xml, or is set with the **/I** parameter. When specifying more than one machine, separate each machine name with a space.  
  
 Using *@machinelistfile* specifies a machine list filename to be stored in the configuration file.  
  
 **/C** _file_compression_type_  
 Sets the type of file compression used on the **SQLdiag** output folder files. Available options:  
  
 0 = none (default)  
  
 1 = uses NTFS compression  
  
 **/B** [**+**]*start_time*  
 Specifies the date and time to start collecting diagnostic data in the following format:  
  
 YYYYMMDD_HH:MM:SS  
  
 The time is specified using 24-hour notation. For example, 2:00 P.M. should be specified as **14:00:00**.  
  
 Use **+** without the date (HH:MM:SS only) to specify a time that is relative to the current date and time. For example, if you specify **/B +02:00:00**, **SQLdiag** will wait 2 hours before it starts collecting information.  
  
 Do not insert a space between **+** and the specified *start_time*.  
  
 If you specify a start time that is in the past, **SQLdiag** forcibly changes the start date so the start date and time are in the future. For example, if you specify **/B 01:00:00** and the current time is 08:00:00, **SQLdiag** forcibly changes the start date so that the start date is the next day.  
  
 Note that **SQLdiag** uses the local time on the computer where the utility is running.  
  
 **/E** [**+**]*stop_time*  
 Specifies the date and time to stop collecting diagnostic data in the following format:  
  
 YYYYMMDD_HH:MM:SS  
  
 The time is specified using 24-hour notation. For example, 2:00 P.M. should be specified as **14:00:00**.  
  
 Use **+** without the date (HH:MM:SS only) to specify a time that is relative to the current date and time. For example, if you specify a start time and end time by using **/B +02:00:00 /E +03:00:00**, **SQLdiag** waits 2 hours before it starts collecting information, then collects information for 3 hours before it stops and exits. If **/B** is not specified, **SQLdiag** starts collecting diagnostics immediately and ends at the date and time specified by **/E**.  
  
 Do not insert a space between **+** and the specified *start_time* or *end_time*.  
  
 Note that **SQLdiag** uses the local time on the computer where the utility is running.  
  
 **/A**  _SQLdiag_application_name_  
 Enables running multiple instances of the **SQLdiag** utility against the same [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] instance.  
  
 Each *SQLdiag_application_name* identifies a different instance of **SQLdiag**. No relationship exists between a *SQLdiag_application_name* instance and a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] instance name.  
  
 *SQLdiag_application_name* can be used to start or stop a specific instance of the **SQLdiag** service.  
  
 For example:  
  
 **SQLDIAG START /A**  _SQLdiag_application_name_  
  
 It can also be used with the **/R** option to register a specific instance of **SQLdiag** as a service. For example:  
  
 **SQLDIAG /R /A** _SQLdiag_application_name_  
  
> [!NOTE]  
>  **SQLdiag** automatically prefixes DIAG$ to the instance name specified for *SQLdiag_application_name*. This provides a sensible service name if you register **SQLdiag** as a service.  
  
 /T { tcp [ ,*port* ] | np | lpc }  
 Connects to an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] using the specified protocol.  
  
 tcp [,*port*]  
 Transmission Control Protocol/Internet Protocol (TCP/IP). You can optionally specify a port number for the connection.  
  
 np  
 Named pipes. By default, the default instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] listens on named pipe `\\.\pipe\sql\query` and `\\.\pipe\MSSQL$<instancename>\sql\query` for a named instance. You cannot connect to an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] by using an alternate pipe name.  
  
 lpc  
 Local procedure call. This shared memory protocol is available if the client is connecting to an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] on the same computer.  
  
 **/Q**  
 Runs **SQLdiag** in quiet mode. **/Q** suppresses all prompts, such as password prompts.  
  
 **/G**  
 Runs **SQLdiag** in generic mode. When **/G** is specified, on startup **SQLdiag** does not enforce [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] connectivity checks or verify that the user is a member of the **sysadmin** fixed server role. Instead, **SQLdiag** defers to Windows to determine whether a user has the appropriate rights to gather each requested diagnostic.  
  
 If **/G** is not specified, **SQLdiag** checks to determine whether the user is a member of the Windows **Administrators** group, and will not collect [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] diagnostics if the user is not an **Administrators** group member.  
  
 **/R**  
 Registers **SQLdiag** as a service. Any command line arguments that are specified when you register **SQLdiag** as a service are preserved for future runs of the service.  
  
 When **SQLdiag** is registered as a service, the default service name is SQLDIAG. You can change the service name by using the **/A** argument.  
  
 Use the **START** command line argument to start the service:  
  
 **SQLDIAG START**  
  
 You can also use the **net start** command to start the service:  
  
 **net  start SQLDIAG**  
  
 **/U**  
 Unregisters **SQLdiag** as a service.  
  
 Use the **/A** argument also if unregistering a named **SQLdiag** instance.  
  
 **/L**  
 Runs **SQLdiag** in continuous mode when a start time or end time is also specified with the **/B** or **/E** arguments, respectively. **SQLdiag** automatically restarts after diagnostics collection stops due to a scheduled shutdown. For example, by using the **/E** or the **/X** arguments.  
  
> [!NOTE]  
>  **SQLdiag** ignores the **/L** argument if a start time or end time is not specified by using the **/B** and **/E** command line arguments.  
  
 Using **/L** does not imply the service mode. To use **/L** when running **SQLdiag** as a service, specify it on the command line when you register the service.  
  
 **/X**  
 Runs **SQLdiag** in snapshot mode. **SQLdiag** takes a snapshot of all configured diagnostics and then shuts down automatically.  
  
 **START** | **STOP** | **STOP_ABORT**  
 Starts or stops the **SQLdiag** service. **STOP_ABORT** forces the service to shut down as quickly as possible without finishing collection of diagnostics it is currently collecting.  
  
 When these service control arguments are used, they must be the first argument used on the command line. For example:  
  
 **SQLDIAG START**  
  
 Only the **/A** argument, which specifies a named instance of **SQLdiag**, can be used with **START**, **STOP**, or **STOP_ABORT** to control a specific instance of the **SQLdiag** service. For example:  
  
 **SQLDIAG START /A** _SQLdiag_application_name_  
  
## Security Requirements  
 Unless **SQLdiag** is run in generic mode (by specifying the **/G** command line argument), the user who runs **SQLdiag** must be a member of the Windows **Administrators** group and a member of the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] **sysadmin** fixed server role. By default, **SQLdiag** connects to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] by using Windows Authentication, but it also supports [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Authentication.  
  
## Performance Considerations  
 The performance effects of running **SQLdiag** depend on the type of diagnostic data you have configured it to collect. For example, if you have configured **SQLdiag** to collect [!INCLUDE[ssSqlProfiler](../includes/sssqlprofiler-md.md)] tracing information, the more event classes you choose to trace, the more your server performance is affected.  
  
 The performance impact of running **SQLdiag** is approximately equivalent to the sum of the costs of collecting the configured diagnostics separately. For example, collecting a trace with **SQLdiag** incurs the same performance cost as collecting it with [!INCLUDE[ssSqlProfiler](../includes/sssqlprofiler-md.md)]. The performance impact of using **SQLdiag** is negligible.  
  
## Required Disk Space  
 Because **SQLdiag** can collect different types of diagnostic information, the free disk space that is required to run **SQLdiag** varies. The amount of diagnostic information collected depends on the nature and volume of the workload that the server is processing and may range from a few megabytes to several gigabytes.  
  
## Configuration Files  
 On startup, **SQLdiag** reads the configuration file and the command line arguments that have been specified. You specify the types of diagnostic information that **SQLdiag** collects in the configuration file. By default, **SQLdiag** uses the SQLDiag.Xml configuration file, which is extracted each time the tool runs and is located in the **SQLdiag** utility startup folder. The configuration file uses the XML schema, SQLDiag_schema.xsd, which is also extracted into the utility startup directory from the executable file each time **SQLdiag** runs.  
  
### Editing the Configuration Files  
 You can copy and edit SQLDiag.Xml to change the types of diagnostic data that **SQLdiag** collects. When editing the configuration file always use an XML editor that can validate the configuration file against its XML schema, such as [!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)]. You should not edit SQLDiag.Xml directly. Instead, make a copy of SQLDiag.Xml and rename it to a new file name in the same folder. Then edit the new file, and use the **/I** argument to pass it to **SQLdiag**.  
  
#### Editing the Configuration File When SQLdiag Runs as a Service  
 If you have already run **SQLdiag** as a service and need to edit the configuration file, unregister the SQLDIAG service by specifying the **/U** command line argument and then re-register the service by using the **/R** command line argument. Unregistering and re-registering the service removes old configuration information that was cached in the Windows registry.  
  
## Output Folder  
 If you do not specify an output folder with the **/O** argument, **SQLdiag** creates a subfolder named SQLDIAG under the **SQLdiag** startup folder. For diagnostic information collection that involves high volume tracing, such as [!INCLUDE[ssSqlProfiler](../includes/sssqlprofiler-md.md)] , make sure that the output folder is on a local drive with enough space to store the requested diagnostic output.  
  
 When **SQLdiag** is restarted, it overwrites the contents of the output folder. To avoid this, specify **/N 2** on the command line.  
  
## Data Collection Process  
 When **SQLdiag** starts, it performs the initialization checks necessary to collect the diagnostic data that have been specified in SQLDiag.Xml. This process may take several seconds. After **SQLdiag** has started collecting diagnostic data when it is run as a console application, a message displays informing you that **SQLdiag** collection has started and that you can press CTRL+C to stop it. When **SQLdiag** is run as a service, a similar message is written to the Windows event log.  
  
 If you are using **SQLdiag** to diagnose a problem that you can reproduce, wait until you receive this message before you reproduce the problem on your server.  
  
 **SQLdiag** collects most diagnostic data in parallel. All diagnostic information is collected by connecting to tools, such as the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] **sqlcmd** utility or the Windows command processor, except when information is collected from Windows performance logs and event logs. **SQLdiag** uses one worker thread per computer to monitor the diagnostic data collection of these other tools, often simultaneously waiting for several tools to complete. During the collection process, **SQLdiag** routes the output from each diagnostic to the output folder.  
  
## Stopping Data Collection  
 After **SQLdiag** starts collecting diagnostic data, it continues to do so unless you stop it or it is configured to stop at a specified time. You can configure **SQLdiag** to stop at a specified time by using the **/E** argument, which allows you to specify a stop time, or by using the **/X** argument, which causes **SQLdiag** to run in snapshot mode.  
  
 When **SQLdiag** stops, it stops all diagnostics it has started. For example, it stops [!INCLUDE[ssSqlProfiler](../includes/sssqlprofiler-md.md)] traces it was collecting, it stops executing [!INCLUDE[tsql](../includes/tsql-md.md)] scripts it was running, and it stops any sub processes it has spawned during data collection. After diagnostic data collection has completed, **SQLdiag** exits.  
  
> [!NOTE]  
>  Pausing the **SQLdiag** service is not supported. If you attempt to pause the **SQLdiag** service, it stops after it finishes collecting the diagnostics that it was collecting when you paused it. If you restart **SQLdiag** after stopping it, the application restarts and overwrites the output folder. To avoid overwriting the output folder, specify **/N 2** on the command line.  
  
 **To stop SQLdiag when running as a console application**  
  
 If you are running **SQLdiag** as a console application, press CTRL+C in the console window where **SQLdiag** is running to stop it. After you press CTRL+C, a message displays in the console window informing you that **SQLDiag** data collection is ending, and that you should wait until the process shuts down, which may take several minutes.  
  
 Press Ctrl+C twice to terminate all child diagnostic processes and immediately terminate the application.  
  
 **To stop SQLdiag when running as a service**  
  
 If you are running **SQLdiag** as a service, run **SQLDiag STOP** in the **SQLdiag** startup folder to stop it.  
  
 If you are running multiple instances of **SQLdiag** on the same computer, you can also pass the **SQLdiag** instance name to on the command line when you stop the service. For example, to stop a **SQLdiag** instance named Instance1, use the following syntax:  
  
```  
SQLDIAG STOP /A Instance1  
```  
  
> [!NOTE]  
>  **/A** is the only command-line argument that can be used with **START**, **STOP**, or **STOP_ABORT**. If you need to specify a named instance of **SQLdiag** with one of the service control verbs, specify **/A** after the control verb on the command line as shown in the previous syntax example. When control verbs are used, they must be the first argument on the command line.  
  
 To stop the service as quickly as possible, run **SQLDIAG STOP_ABORT** in the utility startup folder. This command aborts any diagnostics collecting currently being performed without waiting for them to finish.  
  
> [!NOTE]  
>  Use **SQLDiag STOP** or **SQLDIAG STOP_ABORT** to stop the **SQLdiag** service. Do not use the Windows Services Console to stop **SQLdiag** or other [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] services.  
  
## Automatically Starting and Stopping SQLdiag  
 To automatically start and stop diagnostic data collection at a specified time, use the **/B**_start\_time_ and **/E**_stop\_time_ arguments, using 24-hour notation. For example, if you are troubleshooting a problem that consistently appears at approximately 02:00:00, you can configure **SQLdiag** to automatically start collecting diagnostic data at 01:00 and automatically stop at 03:00:00. Use the **/B** and **/E** arguments to specify the start and stop time. Use 24-hour notation to specify an exact start and stop date and time with the format YYYYMMDD_HH:MM:SS. To specify a relative start or stop time, prefix the start and stop time with **+** and omit the date portion (YYYYMMDD_) as shown in the following example, which causes **SQLdiag** to wait 1 hour before it starts collecting information, then it collects information for 3 hours before it stops and exits:  
  
```  
sqldiag /B +01:00:00 /E +03:00:00  
```  
  
 When a relative *start_time* is specified, **SQLdiag** starts at a time that is relative to the current date and time. When a relative *end_time* is specified, **SQLdiag** ends at a time that is relative to the specified *start_time*. If the start or end date and time that you have specified is in the past, **SQLdiag** forcibly changes the start date so that the start date and time are in the future.  
  
 This has important implications on the start and end dates you choose. Consider the following example:  
  
```  
sqldiag /B +01:00:00 /E 08:30:00  
```  
  
 If the current time is 08:00, the end time passes before diagnostic collection actually begins. Because **SQLDiag** automatically adjusts start and end dates to the next day when they occur in the past, in this example diagnostic collection starts at 09:00 today (a relative start time has been specified with **+**) and continues collecting until 08:30 the following morning.  
  
### Stopping and Restarting SQLdiag to Collect Daily Diagnostics  
 To collect a specified set of diagnostics on a daily basis without having to manually start and stop **SQLdiag**, use the **/L** argument. The **/L** argument causes **SQLdiag** to run continuously by automatically restarting itself after a scheduled shutdown. When **/L** is specified, and **SQLdiag** stops because it has reached the end time specified with the **/E** argument, or it stops because it is being run in snapshot mode by using the **/X** argument, **SQLdiag** restarts instead of exiting.  
  
 The following example specifies that **SQLdiag** run in continuous mode to automatically restart after diagnostic data collecting occurs between 03:00:00 and 05:00:00.  
  
```  
sqldiag /B 03:00:00 /E 05:00:00 /L  
```  
  
 The following example specifies that **SQLdiag** run in continuous mode to automatically restart after taking a diagnostic data snapshot at 03:00:00.  
  
```  
sqldiag /B 03:00:00 /X /L  
```  
  
## Running SQLdiag as a Service  
 When you want to use **SQLdiag** to collect diagnostic data for long periods of time during which you might need to log out of the computer on which **SQLdiag** is running, you can run it as a service.  
  
 **To register SQLDiag to run as a service**  
  
 You can register **SQLdiag** to run as a service by specifying the **/R** argument at the command line. This registers **SQLdiag** to run as a service. The **SQLdiag** service name is SQLDIAG. Any other arguments you specify on the command line when you register **SQLDiag** as a service are preserved and reused when the service is started.  
  
 To change the default SQLDIAG service name, use the **/A** command-line argument to specify another name. **SQLdiag** automatically prefixes DIAG$ to any **SQLdiag** instance name specified with **/A** to create sensible service names.  
  
 **To unregister the SQLDIAG service**  
  
 To unregister the service, specify the **/U** argument. Unregistering **SQLdiag** as a service also deletes the Windows registry keys of the service.  
  
 **To start or restart the SQLDIAG service**  
  
 To start or restart the SQLDIAG service, run **SQLDiag START** from the command line.  
  
 If you are running multiple instances of **SQLdiag** by using the **/A** argument, you can also pass the **SQLdiag** instance name on the command line when you start the service. For example, to start a **SQLdiag** instance named Instance1, use the following syntax:  
  
```  
SQLDIAG START /A Instance1  
```  
  
 You can also use the **net start** command to start the SQLDIAG service.  
  
 When you restart **SQLdiag**, it overwrites the contents in the current output folder. To avoid this, specify **/N 2** on the command line to rename the output folder when the utility starts.  
  
 Pausing the **SQLdiag** service is not supported.  
  
## Running Multiple Instances of SQLdiag  
 Run multiple instances of **SQLdiag** on the same computer by specifying **/A**_SQLdiag\_application\_name_ on the command line. This is useful for collecting different sets of diagnostics simultaneously from the same [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] instance. For example, you can configure a named instance of **SQLdiag** to continuously perform lightweight data collection. Then, if a specific problem occurs on [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], you can run the default **SQLdiag** instance to collect diagnostics for that problem, or to gather a set of diagnostics that [!INCLUDE[msCoName](../includes/msconame-md.md)] Customer Support Services has asked you to gather to diagnose a problem.  
  
## Collecting Diagnostic Data from Clustered SQL Server Instances  
 **SQLdiag** supports collecting diagnostic data from clustered [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] instances. To gather diagnostics from clustered [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] instances, make sure that **"."** is specified for the **name** attribute of the **\<Machine>** element in the configuration file SQLDiag.Xml and do not specify the **/G** argument on the command line. By default, **"."** is specified for the **name** attribute in the configuration file and the **/G** argument is turned off. Typically, you do not need to edit the configuration file or change the command line arguments when collecting from a clustered [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] instance.  
  
 When **"."** is specified as the machine name, **SQLdiag** detects that it is running on a cluster, and simultaneously retrieves diagnostic information from all virtual instances of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] that are installed on the cluster. If you want to collect diagnostic information from only one virtual instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] that is running on a computer, specify that virtual [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] for the **name** attribute of the **\<Machine>** element in SQLDiag.Xml.  
  
> [!NOTE]  
>  To collect [!INCLUDE[ssSqlProfiler](../includes/sssqlprofiler-md.md)] trace information from clustered [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] instances, administrative shares (ADMIN$) must be enabled on the cluster.  
  
## See Also  
 [Command Prompt Utility Reference &#40;Database Engine&#41;](../tools/command-prompt-utility-reference-database-engine.md)  
  
  
