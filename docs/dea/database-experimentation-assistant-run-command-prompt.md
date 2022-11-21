---
title: Run Database Experimentation Assistant at a command prompt
description: Learn how to capture a trace in Database Experimentation Assistant (DEA) and then analyze the results, all from a command prompt.
author: pochiraju
ms.author: rajpo
ms.reviewer: mathoma
ms.date: 05/18/2022
ms.service: sql
ms.subservice: dea
ms.topic: how-to
ms.custom:
  - "seo-lt-2019"
  - kr2b-contr-experiment
---

# Run Database Experimentation Assistant at a command prompt

This article describes how to capture a trace in Database Experimentation Assistant (DEA) and then analyze the results, all from a command prompt.

> [!NOTE]
> To learn more about each DEA operation, run the following command:
>
> `Deacmd.exe -o <operation> --help`
>
> An operation name is required. Valid operations are *Analysis*, *StartCapture*, and *StopCapture*.

## Start a new workload capture by using the DEA command

To start a new workload capture, at a command prompt, run the following command:

```cmd
Deacmd.exe -o StartCapture -n <Trace FileName> -x <Trace Format> -h <SQLServerInstance> -f <database name> -e <Encrypt Connection> -m <Authetication Mode> -u <user name> -p <password> -l <Location of Output Folder> -d <duration>
```

For example:

```cmd
Deacmd.exe -o StartCapture -n sql2008capture -x 0 -h localhost -f adventureworks -e --trust -m 0 -l c:\test  -d 60
```

When you start a new workload capture with the `Deacmd.exe` command, you can use the following options:

| Option| Description |  
| --- | --- |
| `-n`, `--name` | Required. Trace file name. |
| `-x`, `--format` | Required. Format of the trace (0 = Trace, 1 = XEvents). |
| `-d`, `--duration` | Required. Maximum duration for the capture, in minutes. |
| `-l`, `--location` | Required. Location of output folder for storing trace or XEvent files on the host computer. |
| `-t`, `--type` | Default: 0. Type of the SQL Server (0 = SqlServer, 1 = AzureSQLDB, 2 = Azure SQL Managed Instance). |
| `-h`, `--host` | Required. SQL Server host name or instance name to start the capture. |
| `-e`, `--encrypt` | Default: True. Encrypt connection to SQL Server instance. |
| `--trust` | Default: False. Trust server certificate while connecting to SQL Server instance. |
| `-f`, `--databasename` | Name of the database to filter your traces, if not specified the capture starts on all the databases. |
| `-m`, `--authmode` | Default: 0. Authentication mode (0 = Windows, 1 = Sql Authentication). |
| `-u`, `--username` | User name for connecting to the SQL Server. |
| `-p`, `--password` | Password for connecting to the SQL Server. |

## Replay a workload capture

If you're using Distributed Replay, perform the following steps.

1. Sign in to the Distributed Replay controller computer.
2. To convert the workload trace that you captured using the DEA command to an IRF file, run the following command:

   ```cmd
   DReplay preprocess -m "dreplaycontroller" -i "Path to first trace file" -d "<Folder path on controller>\IrfFolder"
   ```

3. Start a trace capture on the target computer running SQL Server using *StartReplayCaptureTrace.sql*.

    a.  In SQL Server Management Studio (SSMS), open *<Dea_InstallPath\>\Scripts\StartReplayCaptureTrace.sql*.

    b.  Run `Set @durationInMins=0` so that the trace capture doesn't stop automatically after a specified time.

    c.  To set the maximum file size per trace file, run `Set @maxfilesize`. The recommended size is 200 MB.

    d.  Edit `@Tracefile` to set a unique name for your trace file.

    e.  Edit `@dbname` to specify a database name if the workload must be captured only on a specific database. By default, the workload on the entire server is captured.

4. To replay the IRF file against the target SQL Server instance, run the following command:

    ```cmd
    DReplay replay -m "dreplaycontroller" -d "<Folder Path on Dreplay Controller>\IrfFolder" -o -s "SQL2016Target" -w "dreplaychild1,dreplaychild2,dreplaycild3,dreplaychild4"
    ```

    a.  To monitor the status, run the following command:

    ```cmd
    DReplay status -f 1
    ```

    b. To stop the replay, for example if you see that the pass percentage is lower than expected, run the following command:

    ```cmd
    DReplay cancel
    ```

5. Stop the trace capture on the target SQL Server instance.
6. In SSMS, open *\<Dea_InstallPath>\Scripts\StopCaptureTrace.sql*.
7. Edit `@Tracefile` to match the trace file path on the target computer running SQL Server.
8. Run the script against the target computer running SQL Server.

## Using InBuilt Replay

If you're using InBuilt Replay, you won't have to set up Distributed Replay. The ability to use InBuilt Replay at the command prompt is on the way. Currently, you can use our GUI to run replay using InBuilt Replay.

## Analyze traces using the DEA command

To start a new trace analysis, run the following command:

```cmd
Deacmd.exe -o analysis -a <Target1 trace filepath> -b <Target2 trace filepath> -r reportname -h <SQLserverInstance> -e <encryptconnection> -u <username>
```

For example:

```cmd
Deacmd.exe -o analysis -a C:\Trace\SQL2008Source\Trace.trc -b C:\ Trace\SQL2014Trace\Trace.trc -r upgrade20082014 -h localhost -e
```

To view the analysis reports of these trace files, you need to use the GUI to view charts and organized metrics. However, the analysis database is written to the SQL Server instance specified, so you can also  query the generated analysis tables directly.

When analyzing traces using the DEA command, you can use the following options:

| Option| Description |  
| --- | --- |
| `-a`, `--traceA` | Required. File path to the event file for the A instance. Example: *C:\traces\Sql2008trace.trc*.  If there's a batch of files, select the first file and DEA checks for rollover files automatically. If files are in blob, provide the folder path where you want the event files stored locally.  Example: *C:\traces\\* |
| `-b`, `--traceB` | Required. File path to the event file for the B instance. Example: *C:\traces\Sql2014trace.trc*. If there's a batch of files, select the first file and DEA checks for rollover files automatically. If files are in blob, provide the folder path where you want the event files stored locally.  Example: *C:\traces\\* |
| `-r`, `--ReportName` | Required. Name for current analysis. The analysis report that gets generated is identified by this name. |
| `-t`, `--type` | Default: 0. Type of the SQL Server (0 = SqlServer, 1 = AzureSQLDB, 2 = Azure SQL Managed Instance). |
| `-h`, `--host` | Required. SQL Server host name or instance name. |
| `-e`, `--encrypt` | Default: True. Encrypt connection to SQL Server instance.|
| `--trust` | Default: False. Trust server certificate while connecting to SQL Server instance. |
| `-m`, `--authmode` | Default: 0. Authentication mode (0 = Windows, 1 = Sql Authentication). |
| `-u`, `--username` | User name for connecting to the SQL Server. |
| `--p` | Password for connecting to the SQL Server. |
| `--ab` | Default: False. Storage location of trace A is in blob. If used, must also specify `--abu (Trace A Blob Url)` |
| `--bb` | Default: False. Storage location of trace B is in blob. If used, must also specify `--bbu (Trace B Blob Url)` |
| `--abu` | Blob URL for A instance with SAS key. |
| `--bbu` | Blob URL for B instance with SAS key. |

## See also

- For more information about using DEA, see [Overview of Database Experimentation Assistant](database-experimentation-assistant-overview.md).
