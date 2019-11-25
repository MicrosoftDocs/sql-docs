---
title: Run Database Experimentation Assistant at a command prompt
description: Run Database Experimentation Assistant at a command prompt
ms.custom: "seo-lt-2019"
ms.date: 11/22/2019
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

## Start a new workload capture by using the DEA command

To start a new workload capture, at a command prompt, run the following command:

`Deacmd.exe -o startcapturetrace -s <SQLServerInstance> -e <encryptconnection> -u <trustservercertificate> -d <database name> -p <trace file path> -f <trace file name> -t <Max duration>`

**Example**

`Deacmd.exe -o startcapturetrace -s localhost -e -d adventureworks -p c:\test -f sql2008capture -t 60`

## Replay a workload capture

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

## Analyze traces using the DEA command

To start a new trace analysis, at a command prompt, run the following command:

`Deacmd.exe -o analysis -a <Target1 trace filepath> -b <Target2 trace filepath> -r reportname -s <SQLserverInstance> -e <encryptconnection> -u <trustservercertificate>`

**Example**

`Deacmd.exe -o analysis -a C:\Trace\SQL2008Source\Trace.trc -b C:\ Trace\SQL2014Trace\Trace.trc -r upgrade20082014 -s localhost -e`

## See also

- For more information about using DEA, see [Overview of Database Experimentation Assistant](database-experimentation-assistant-overview.md).
