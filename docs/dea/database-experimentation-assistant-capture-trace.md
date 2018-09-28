---
title: Capture a trace with Database Experimentation Assistant for SQL Server upgrades
description: Capture a trace with Database Experimentation Assistant
ms.custom: ""
ms.date: 09/25/2018
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

# Capture a trace with Database Experimentation Assistant

Capture trace lets you easily produce a trace file (.trc) containing a log of events that occurred on a specified server within a given period of time. This action must be performed once per server.

- Before starting your trace capture, make sure you back up all your target databases.
- Query caching in SQL can affect evaluation results. We recommend restarting the SQL Server service (MSSQLSERVER) in the services application to improve consistency in evaluation results.

## Open Capture Traces
Open the tool and select the menu icon on the left side of the screen. This opens the left side-bar menu. Choose **Capture Traces** next to the camera icon to open Capture Traces.

![Capture1](./media/database-experimentation-assistant-capture-trace/dea-capture-trace-capture.png)

## Enter inputs for capturing traces  
Enter the following information in the input fields before starting the capture trace operation.

![CaptureTraceInputs](./media/database-experimentation-assistant-capture-trace/dea-capture-trace-inputs.png)

- **Server name:** Provide a SQL server name where the server trace you want captured is running.
- **Database name:** Provide a Database name to start the trace on it. If you do not specify a database, trace will be captured on all the databases on the server.
- **Name for capture:** Name the trace file (.trc) for your capture.
- **Max file size:** Specify the rollover size for files; a new file will be created, as needed, at the specified file size. Recommended rollover size is 200 MB.
- **Duration:** Select the length of time (in minutes) you want the capture trace to run.
- **Path to store output trace file:** Specify the destination path for the trace file.  
    Note: File path to trace file must be on the machine the SQL instance is on. If SQL Service is not set for a specific account, it may need write permissions to the specified folder in order for the trace file to be written.

## Begin capturing traces
After you have entered all input information, select **Start** to begin capturing traces. If the inputs are valid, the capture trace process begins. Otherwise the fields that contain invalid inputs will be highlighted with red. 

Make sure the entered values are accurate, and again select **Start**.

After the capture trace has finished running, locate your new trace file in the file location you specified. You can monitor the progress of the capture by selecting the bell icon at the bottom left of the navigation pane.

![ProgressOnCapture](./media/database-experimentation-assistant-capture-trace/dea-capture-trace-progress.png)

**Trace File (.trc)** – Capture Trace writes out a .trc file to the location specified. This file contains trace results of the activity of a SQL database. TRC files are designed to provide more information about errors that are detected and reported by SQL Server.

## Frequently asked questions about Capture Trace
Following are some frequently asked questions about Capture Trace.

### What events are captured while running trace on a production database?

The following table provides the list of events and the corresponding column data that we collect for the traces:
  
|Event Name|Text Data (1)|Binary Data (2)|Database ID (3)|Host Name (8)|Application Name (10)|Login Name (11)|SPID (12)|Start Time (14)|End Time (15)|Database Name (35)|Event Sequence (51)|IsSystem (60)|  
|---|---|---|---|---|---|---|---|---|---|---|---|---|  
|**RPC:Completed (10)**||*|*|*|*|*|*|*|*|*|*|*|  
|**RPC:Starting (11)**||*|*|*|*|*|*|*||*|*|*|  
|**RPC Output Parameter (100)**|*||*|*|*|*|*|*||*|*|*|  
|**SQL:BatchCompleted (12)**|*||*|*|*|*|*|*|*|*|*|*|  
|**SQL:BatchStarting (13)**|*||*|*|*|*|*|*||*|*|*|  
|**Audit Login (14)**|*|*|*|*|*|*|*|*||*|*|*|  
|**Audit Logout (15)**|*||*|*|*|*|*|*|*|*|*|*|  
|**ExistingConnection (17)**|*|*|*|*|*|*|*|*||*|*|*|  
|**CursorOpen (53)**|*||*|*|*|*|*|*||*|*|*|  
|**CursorPrepare (70)**|*||*|*|*|*|*|*||*|*|*|  
|**Prepare SQL (71)**|||*|*|*|*|*|*||*|*|*|  
|**Exec Prepared SQL (72)**|||*|*|*|*|*|*||*|*|*|  
|**CursorExecute (74)**|*||*|*|*|*|*|*||*|*|*|  
|**CursorUnprepare (77)**|*||*|*|*|*|*|*||*|*|*|  
|**CursorClose (78)**|*||*|*|*|*|*|*||*|*|*|  

### Will there be any performance impact on my production server while capturing traces?
    
Yes, there will be minimal performance impact during the period of trace collection. Based on our tests we have found about 3% memory pressure.
    
### What kind of permissions are required for capturing traces from production workload?
    
- The Windows user performing the trace operation in the DEA Application must have sysadmin privileges in the target SQL Server.
- The service account under which the SQL Server is running must have write access to the specified trace file path.

### Can I capture trace for the entire server vs one single database?
    
Yes, DEA lets you capture traces for both the server level (for all the databases in the server) or one single database.
    
### I have a linked server configured in my production environment. Do those queries show up in the traces?
    
If you are running capture for the entire server (by not providing any value in the Database name field in the capture screen), the trace will capture all the queries including the linked server queries.
    
### What is the minimum recommended time for production workload traces?
    
The general recommendation we offer is to choose a time that best represents the entirety of your workload so that we can run the analysis on all the queries in your workload.
    
### How important is to take database backup right before the start of the trace capture?
    
It is important to take backup right before you start the capture. We will be replaying the captured trace in Target 1 and Target 2 and if the database state is not the same, the results of the experimentation will be skewed.

### Can we collect XEvents instead of traces and replay the same?
    
Yes. XEvents support is available in DEA. Download the latest version and give it a try.

## Troubleshooting Capture Trace
If you receive an error while running Capture Trace, first review the following prerequsites to see if it resolves the error:

- Confirm the SQL Server name is valid. To confirm, try connecting to the server using SSMS.
- Confirm your firewall configuration is not blocking connections to SQL Server.
- Confirm user has the permissions listed in the permissions blog.
- Confirm that trace name does not follow the standard rollover convention (that is, 'Capture\_1'). You can instead try trace names like 'Capture\_1A' or 'Capture1'.

Following are some possible errors you might encounter and solutions for resolving them:

|Possible Errors|Solution|  
|---|---|  
|Unable to start the trace on the target SQL Server, check if you have required permissions and SQL Server account has write access to the specified trace file path Sql Error Code (53)|The user running the DEA Tool should have access to SQL Server and have 'sysadmin' role assigned to the user|  
|Unable to start the trace on the target SQL Server, check if you have required permissions and SQL Server account has write access to the specified trace file path Sql Error Code (19062)|The trace path specified might not exist or the folder doesn't have write permissions for the account under which SQL Server services are running (for example, NETWORK SERVICE). The path should exist with right permissions in order for the trace to start.|  
|A DEA trace is currently running on the target server.|An active trace is already running on the target server, DEA will not allow to multiple traces when a server-wide trace is already running.|  
|Cannot open database requested for capturing trace. This error may be due to incorrect database name|The database specified doesn’t exists or it’s not accessible to the current user. Correct the database name|  

If you see any other errors with Sql Error Code, see [System Error Messages](https://docs.microsoft.com/en-us/previous-versions/sql/sql-server-2008-r2/cc645603(v=sql.105)) for detailed description and resolution.
    
