---
title: Run Database Experimentation Assistant at a command prompt
description: Learn how to capture a trace in Database Experimentation Assistant (DEA) and then analyze the results, all from a command prompt.
ms.custom: "seo-lt-2019"
ms.date: 02/25/2020
ms.prod: sql
ms.prod_service: dea
ms.suite: sql
ms.technology: dea
ms.tgt_pltfrm: ""
ms.topic: conceptual
author: HJToland3
ms.author: jtoland
ms.reviewer: mathoma
---

# Run Database Experimentation Assistant at a command prompt

This article describes how to capture a trace in Database Experimentation Assistant (DEA) and then analyze the results, all from a command prompt.

   > [!NOTE]
   > To learn more about each DEA operation, try running the following command:
   >
   > `Deacmd.exe -o <operation> --help`
   >
   > An operation name is required; valid operations are **Analysis**, **StartCapture**, and **StopCapture**.

## Start a new workload capture by using the DEA command

To start a new workload capture, at a command prompt, run the following command:

`Deacmd.exe -o StartCapture -n <Trace FileName> -x <Trace Format> -h <SQLServerInstance> -f <database name> -e <Encrypt Connection> -m <Authetication Mode> -u <user name> -p <password> -l <Location of Output Folder> -d <duration>`

**Example**

`Deacmd.exe -o StartCapture -n sql2008capture -x 0 -h localhost -f adventureworks -e --trust -m 0 -l c:\test  -d 60`

**Additional options**

When starting a new workload capture with the `Deacmd.exe` command, you can use the following additional options:

| Option| Description |  
| --- | --- |
| -n, --name | Required; trace file name |
| -x, --format | Required; format of the trace (Trace = 0, XEvents = 1) |
| -d, --duration | Required; maximum duration for the capture, in minutes |
| -l, --location | Required; location of output folder for storing trace / xevent files on the host computer |
| -t, --type | (Default: 0) Type / Edition of the Sql Server (SqlServer = 0, AzureSQLDB = 1, Azure SQL Managed Instance = 2) |
| -h, --host | Required; SQL Server host name and/or instance name to start the capture |
| -e, --encrypt | (Default: True) Encrypt connection to SQL Server Instance. Default is true |
| --trust | (Default: False) Trust server certificate while connecting to SQL Server Instance. Default is false |
| -f, --databasename | (Default: ) Name of the database to filter your traces, if not specified the capture will start on all the databases |
| -m, --authmode | (Default: 0) Authentication Mode (Windows = 0, Sql Authentication = 1) |
| -u, --username | User name for connecting to the SQL Server |
| -p, --password | Password for connecting to the SQL Server |

## Replay a workload capture

**Using Distributed Replay**

If you're using Distributed Replay, perform the following steps.

1. Log in to the Distributed Replay controller computer.
2. To convert the workload trace that you captured using the DEA command to an IRF file, at a command prompt, run the following command:

    `DReplay preprocess -m "dreplaycontroller" -i "Path to first trace file" -d "<Folder path on controller>\IrfFolder"`

3. Start a trace capture on the target computer running SQL Server using StartReplayCaptureTrace.sql.

    a.  In SQL Server Management Studio (SSMS), open <Dea_InstallPath\>\Scripts\StartReplayCaptureTrace.sql.

    b.  Run `Set @durationInMins=0` so that the trace capture doesn't stop automatically after a specified time.

    c.  To set the max file size per trace file, run `Set @maxfilesize`. The recommended size is 200 (in MB).

    d.  Edit `@Tracefile` to set a unique name for your trace file.

    e.  Edit `@dbname` to specify a database name if the workload must be captured only on a specific database. By default, the workload on the entire server is captured.

4. To replay the IRF file against the target SQL Server instance, at a command prompt, run the following command:

    `DReplay replay -m "dreplaycontroller" -d "<Folder Path on Dreplay Controller>\IrfFolder" -o -s "SQL2016Target" -w "dreplaychild1,dreplaychild2,dreplaycild3,dreplaychild4"`

    a.  To monitor the status, at a command prompt, run `DReplay status -f 1`.

    b.  To stop the replay, for example if you see that the pass % is lower than expected, at a command prompt, run `DReplay cancel`.

5. Stop the trace capture on the target SQL Server instance.
6. In SSMS, open `<Dea_InstallPath>\Scripts\StopCaptureTrace.sql`.
7. Edit `@Tracefile` to match the trace file path on the target computer running SQL Server.
8. Run the script against the target computer running SQL Server.

**Using InBuilt Replay**

If you are using  InBuilt Replay, you won't have to set up Distributed Replay. The ability to use InBuilt Replay via the command line is on the way, but in the interim, you can use our GUI to run replay using InBuilt Replay.

## Analyze traces using the DEA command

To start a new trace analysis, at a command prompt, run the following command:

`Deacmd.exe -o analysis -a <Target1 trace filepath> -b <Target2 trace filepath> -r reportname -h <SQLserverInstance> -e <encryptconnection> -u <username>`

**Example**

`Deacmd.exe -o analysis -a C:\Trace\SQL2008Source\Trace.trc -b C:\ Trace\SQL2014Trace\Trace.trc -r upgrade20082014 -h localhost -e`

To view the analysis reports of these trace files, you need to use the GUI to view charts and organized metrics.  However, the analysis database is written to the SQL Server instance specified, so you can also  query the generated analysis tables directly.

**Additional options**

When analyzing traces using the DEA command, you can use the following additional options:

| Option| Description |  
| --- | --- |
| -a, --traceA | Required; file path to the event file for A instance. Example C:\traces\Sql2008trace.trc.  If there's a batch of files, select the first file and DEA will check for rollover files automatically. If files are in blob, provide the folder path where you want the event files stored locally.  Example C:\traces\ |
| -b, --traceB | Required; file path to the event file for B instance. Example C:\traces\Sql2014trace.trc. If there's a batch of files, select the first file and DEA will check for rollover files automatically. If files are in blob, provide the folder path where you want the event files stored locally.  Example C:\traces\ |
| -r, --ReportName | Required; name for current analysis. The analysis report that gets generated will be identified by this name. |
| -t, --type | (Default: 0) Type / Edition of the Sql Server (SqlServer = 0, AzureSQLDB = 1, Azure SQL Managed Instance = 2) |
| -h, --host | Required; SQL Server host name and/or instance name |
| -e, --encrypt | (Default: True) Encrypt connection to SQL Server Instance. Default is true |
| --trust | (Default: False) Trust server certificate while connecting to SQL Server Instance. Default is false |
| -m, --authmode | (Default: 0) Authentication Mode (Windows = 0, Sql Authentication = 1) |
| -u, --username | User name for connecting to the SQL Server |
| --p | Password for connecting to the SQL Server |
| --ab | (Default: False) Storage location of trace A is in blob. If used, must also specify --abu (Trace A Blob Url) |
| --bb | (Default: False) Storage location of trace B is in blob. If used, must also specify --bbu (Trace B Blob Url) |
| --abu | Blob URL for A instance with SAS key |
| --bbu | Blob URL for B instance with SAS key |

## See also

- For more information about using DEA, see [Overview of Database Experimentation Assistant](database-experimentation-assistant-overview.md).
