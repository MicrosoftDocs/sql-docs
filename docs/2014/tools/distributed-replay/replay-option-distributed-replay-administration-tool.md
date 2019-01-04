---
title: "Replay Option (Distributed Replay Administration Tool) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: tools-other
ms.topic: conceptual
ms.assetid: d7bce6a5-d414-488d-a3cd-50c1c62019c4
author: stevestein
ms.author: sstein
manager: craigg
---
# Replay Option (Distributed Replay Administration Tool)
  The [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Distributed Replay administration tool, `DReplay.exe`, is a command-line tool that you can use to communicate with the distributed replay controller. This topic describes the **replay** command-line option and corresponding syntax.  
  
 The **replay** option initiates the event replay stage, in which the controller dispatches replay data to the specified clients, launches the distributed replay and synchronizes the clients. Optionally, each client participating in the replay can record the replay activity and save a result trace file locally.  
  
 ![Topic link icon](../../database-engine/media/topic-link.gif "Topic link icon") For more information about the syntax conventions that are used with the administration tool syntax, see [Transact-SQL Syntax Conventions &#40;Transact-SQL&#41;](/sql/t-sql/language-elements/transact-sql-syntax-conventions-transact-sql).  
  
## Syntax  
  
```  
  
      dreplay replay [-mcontroller] -dcontroller_working_dir [-o]  
    [-starget_server] -wclients [-cconfig_file]  
    [-fstatus_interval]  
```  
  
#### Parameters  
 **-m** *controller*  
 Specifies the computer name of the controller. You can use "`localhost`" or "`.`" to refer to the local computer.  
  
 If the **-m** parameter is not specified, the local computer is used.  
  
 **-d** *controller_working_dir*  
 Specifies the directory on the controller where the intermediate file will be stored. The **-d** parameter is required.  
  
 The following requirements apply:  
  
-   The directory must reside on the controller.  
  
-   You must specify the full path, starting with a drive letter (for example, `c:\WorkingDir`).  
  
-   The path must not end with a backslash "`\`".  
  
-   UNC paths are not supported.  
  
 **-o**  
 Captures the clients' replay activity and saves it to a result trace file in the path specified by the `<ResultDirectory>` element in the client configuration file, `DReplayClient.xml`.  
  
 When the **-o** parameter is not specified, the result trace file is not generated. The console output returns summary information at the end of replay, but no other replay statistics are available.  
  
 **-s** *target_server*  
 Specifies the target instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] that the distributed workload should be replayed against. You must specify this parameter in the format **server_name[\instance name]**.  
  
 You cannot use "`localhost`" or "`.`" as the target server.  
  
 The **-s** parameter is not required if the `<Server>` element is specified in the `<ReplayOptions>` section of the replay configuration file, `DReplay.exe.replay.config`.  
  
 If the **-s** parameter is used, the `<Server>` element in the `<ReplayOptions>` section of the replay configuration file will be ignored.  
  
 **-w** *clients*  
 This required parameter is a comma-separated list (without spaces) that specifies the computer names of clients that should participate in the distributed replay. IP addresses are not allowed. Be aware that the clients must already be registered with the controller.  
  
> [!NOTE]  
>  Each client registers with the controller that is specified in the client configuration file when the client service starts.  
  
 **-c** *config_file*  
 Is the full path of the replay configuration file; used to specify the location when it is stored in a different location.  
  
 The **-c** parameter is not required if you want to use the default values of the replay configuration file, `DReplay.exe.replay.config`.  
  
 **-f** *status_interval*  
 Specifies the frequency (in seconds) at which to display the status.  
  
 If **-f** is not specified, the default interval is 30 seconds.  
  
## Examples  
 In this example, the distributed replay derives much of its behavior from a modified replay configuration file, `DReplay.exe.replay.config`.  
  
-   The **-m** parameter specifies that a computer named `controller1` acts as the controller. The computer name must be specified when the controller service is running on a different computer.  
  
-   The **-d** parameter specifies the location of the intermediate file on the controller, `c:\WorkingDir`.  
  
-   The **-o** parameter specifies that each specified client capture the replay activity and save it to a result trace file. Note: The `<ResultTrace>` element in the configuration file can be used to specify if row count and result set be recorded.  
  
-   The **-w** parameter specifies that computers `client1` through `client4` participate as clients in the distributed replay.  
  
-   The **-c** parameter is used to point to the modified configuration file, `DReplay.exe.replay.config`.  
  
-   The **-s** parameter is not required because the `<Server>` element is specified in the `<ReplayOptions>` element of the replay configuration file, `DReplay.exe.replay.config`.  
  
 The event replay stage is initiated with the following syntax, when the administration tool is run from a different computer than the controller:  
  
```  
dreplay replay -m controller1 -d c:\WorkingDir -o -w client1,client2,client3,client4 -c c:\DReplay.exe.replay.config  
```  
  
 To specify a synchronous sequencing mode, the `<SequencingMode>` element of the `DReplay.exe.replay.config` file is set equal to the value `synchronization`. The `<ResultTrace>` section of the replay configuration file is modified to specify that row count be recorded. These changes are shown in the following XML example:  
  
```  
<?xml version='1.0'?>  
<Options>  
    <ReplayOptions>  
        <Server>server_name\replay_target_instance</Server>  
        <SequencingMode>synchronization</SequencingMode>  
        <ConnectTimeScale></ConnectTimeScale>  
        <ThinkTimeScale></ThinkTimeScale>  
        <HealthmonInterval>60</HealthmonInterval>  
        <QueryTimeout>3600</QueryTimeout>  
        <ThreadsPerClient></ThreadsPerClient>  
    </ReplayOptions>  
    <OutputOptions>  
        <ResultTrace>  
            <RecordRowCount>Yes</RecordRowCount>  
            <RecordResultSet>No</RecordResultSet>  
        </ResultTrace>  
    </OutputOptions>  
</Options>  
```  
  
 To specify a stress sequencing mode, the `<SequencingMode>` element of the `DReplay.exe.replay.config` file is set equal to the value `stress`. The `<ConnectTimeScale>` and `<ThinkTimeScale>` elements are set to the value `50` (to specify 50 percent). For more information about connect time and think time, see [Configure Distributed Replay](configure-distributed-replay.md). These changes are shown in the following XML example:  
  
```  
<?xml version='1.0'?>  
<Options>  
    <ReplayOptions>  
        <Server>server_name\replay_target_instance_name</Server>  
        <SequencingMode>stress</SequencingMode>  
        <ConnectTimeScale>50</ConnectTimeScale>  
        <ThinkTimeScale>50</ThinkTimeScale>  
        <HealthmonInterval>60</HealthmonInterval>  
        <QueryTimeout>3600</QueryTimeout>  
        <ThreadsPerClient></ThreadsPerClient>  
    </ReplayOptions>  
    <OutputOptions>  
        <ResultTrace>  
            <RecordRowCount>Yes</RecordRowCount>  
            <RecordResultSet>No</RecordResultSet>  
        </ResultTrace>  
    </OutputOptions>  
</Options>  
```  
  
## Permissions  
 You must run the administration tool as an interactive user, as either a local user or a domain user account. To use a local user account, the administration tool and controller must be running on the same computer.  
  
 For more information, see [Distributed Replay Security](distributed-replay-security.md).  
  
## See Also  
 [Replay Trace Data](replay-trace-data.md)   
 [Review the Replay Results](review-the-replay-results.md)   
 [SQL Server Distributed Replay](sql-server-distributed-replay.md)   
 [Configure Distributed Replay](configure-distributed-replay.md)   
 [SQL Server Distributed Replay Forum](https://social.technet.microsoft.com/Forums/sl/sqldru/)   
 [Using Distributed Replay to Load Test Your SQL Server - Part 2](https://blogs.msdn.com/b/mspfe/archive/2012/11/14/using-distributed-replay-to-load-test-your-sql-server-part-2.aspx)   
 [Using Distributed Replay to Load Test Your SQL Server - Part 1](https://blogs.msdn.com/b/mspfe/archive/2012/11/08/using-distributed-replay-to-load-test-your-sql-server-part-1.aspx)  
  
  
