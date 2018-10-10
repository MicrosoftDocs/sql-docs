---
title: Run Database Experimentation Assistant at a command prompt for SQL Server upgrades
description: Run Database Experimentation Assistant at a command prompt
ms.custom: ""
ms.date: 10/12/2018
ms.prod: sql
ms.prod_service: dea
ms.suite: sql
ms.technology: dea
ms.tgt_pltfrm: ""
ms.topic: conceptual
author: HJToland3
ms.author: jtoland
ms.reviewer: douglasl
manager: craigg
---

# Run Database Experimentation Assistant at a command prompt
This article describes how you can use the Command Prompt window to capture trace and then analyze the results. 

> [!NOTE]
> Although the process can't yet be run at a command prompt, for convenience we included the steps to replay the workload.
>
>

## Start a new workload capture using the DEA command

To start a new workload capture, run `Deacmd.exe -o startcapturetrace -s <SQLServerInstance> -e <encryptconnection> -u <trustservercertificate> -d <database name> -p <trace file path> -f <trace file name> -t <Max duration>`

**For example**: `Deacmd.exe -o startcapturetrace -s localhost -e -d adventureworks -p c:\test -f sql2008capture -t 60`

## Replay a workload

1.  Log into the Distributed Replay controller machine.
2.  Convert the workload trace captured using the DEA command to an IRF file.

    `DReplay preprocess -m "dreplaycontroller" -i "Path to first trace file" -d "<Folder path on controller>\IrfFolder"`
3.  Start a trace capture on the target SQL instance using *StartReplayCaptureTrace.sql*.
       
    a.  Open `<Dea_InstallPath>\Scripts\StartReplayCaptureTrace.sql` in SSMS.
    
    b.  `Set @durationInMins=0` so that the trace capture doesn't stop automatically after a specified time.
    
    c.  `Set @maxfilesize` - Max file size per trace file. Recommended size is 200.
    
    d.  `@Tracefile` - unique name for your trace file.
    
    e.  `@dbname` - By default, the workload on the entire server is captured. Specify a database name if the workload must be captured on a specific database only.
4.  Replay the IRF file against the target SQL instance:  
        `DReplay replay -m "dreplaycontroller" -d "<Folder Path on Dreplay Controller>\IrfFolder" -o -s "SQL2016Target" -w "dreplaychild1,dreplaychild2,dreplaycild3,dreplaychild4"`
        
    a.  To monitor status, open a Command Prompt window and run `DReplay status -f 1`.
        
    b.  To stop the replay, such as if you see the pass % is lower than expected, open a Command Prompt window and run `DReplay cancel`.

5.  Stop the trace capture on the target SQL instance.
6.  Open `<Dea_InstallPath>\Scripts\StopCaptureTrace.sql` in SSMS.
7.  Edit `@Tracefile` to match the trace file path on the target SQL instance.
8.  Run the script against the target SQL instance.

## Analyze traces using the DEA command

To start a new workload capture, run  `Deacmd.exe -o analysis -a <Target1 trace filepath> -b <Target2 trace filepath> -r reportname -s <SQLserverInstance> -e <encryptconnection> -u <trustservercertificate>`

**For example**: `Deacmd.exe -o analysis -a C:\Trace\SQL2008Source\Trace.trc -b C:\ Trace\SQL2014Trace\Trace.trc -r upgrade20082014 -s localhost -e`

## Next steps

For a 19-minute introduction and demonstration of DEA, watch the following video:

> [!VIDEO https://channel9.msdn.com/Shows/Data-Exposed/Introducing-the-Database-Experimentation-Assistant/player]
