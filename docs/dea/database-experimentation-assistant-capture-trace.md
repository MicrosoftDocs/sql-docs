---
title: Capture a trace in Database Experimentation Assistant for SQL Server upgrades
description: Capture a trace in Database Experimentation Assistant
ms.custom: ""
ms.date: 10/22/2018
ms.prod: sql
ms.prod_service: dea
ms.suite: sql
ms.technology: dea
ms.tgt_pltfrm: ""
ms.topic: conceptual
author: HJToland3
ms.author: ajaykar
ms.reviewer: douglasl
manager: craigg
---

# Capture a trace in Database Experimentation Assistant

You can use a trace capture in Database Experimentation Assistant (DEA) to create a trace file that has a log of captured server events. A captured server event is an event that occurs on a specific server during a specific time period. A trace capture must be run once per server.

Before you start a trace capture, make sure that you back up all target databases.

Query caching in SQL Server might affect evaluation results. We recommend that you restart the SQL Server service (MSSQLSERVER) in the services application to improve the consistency of evaluation results.

## Create a trace capture

1. In DEA, select the menu icon in the left menu. In the expanded menu, select **Capture Traces** next to the camera icon.

    ![Select Capture Traces in the menu](./media/database-experimentation-assistant-capture-trace/dea-capture-trace-capture.png)

1. Under **New Capture**, enter or select the following information:

    - **SQL Server instance name**: Enter a name for the computer running SQL Server on which you want to capture a server trace.
    - **Database name**: Enter a name for a database on which to start a database trace. If you don't specify a database, trace is captured on all the databases on the server.
    - **Trace file name**: Enter a name for the trace file for your capture.
    - **Max file size (MB)**: Select the rollover size for files. A new file is created as needed at the file size you select. The recommended rollover size is 200 MB.
    - **Duration (in min)**: Select the length of time (in minutes) that you want the trace capture to run.
    - **Path to store output trace file**: Select the destination path for the trace file. 

    > [!NOTE]
    > The file path to the trace file must be on the computer that's running SQL Server. If the SQL Server service isn't set for a specific account, the service might need write permissions to the specified folder for the trace file to be written.
    >
    >

    ![New Capture page](./media/database-experimentation-assistant-capture-trace/dea-capture-trace-inputs.png)

## Start the trace capture

After you enter or select the required information, select **Start** to start capturing traces. If the information you entered is valid, the trace capture process begins. Otherwise, the text boxes that have invalid entries are highlighted with red. 

Make sure that the values you've selected or entered are correct, and then select **Start**.

When the trace capture is finished running, locate your new trace file in the file location that you specified in **Path to store output trace file**. Select the bell icon at the bottom of the left menu to monitor the progress of the capture.

![Capture Traces progress](./media/database-experimentation-assistant-capture-trace/dea-capture-trace-progress.png)

### Trace file

The trace capture writes out a .trc file in the specified location. The trace file includes trace results of the activity of a SQL Server database. TRC files are designed to provide more information about errors that are detected and reported by SQL Server.

## Frequently asked questions about trace capture

Following are some frequently asked questions about trace capture in DEA.

### What events are captured when I run a trace capture on a production database?

The following table provides the list of events and the corresponding column data that we collect for traces:
  
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

### Is there a performance effect on my production server when trace capture is running?
    
Yes, there's a minimal performance effect during trace collection. In our tests, we found about a 3% memory pressure.
    
### What kind of permissions are required for capturing traces on a production workload?
    
- The Windows user that runs the trace operation in the DEA application must have sysadmin rights on the computer that's running SQL Server.
- The service account used on the computer running SQL Server must have write access to the specified trace file path.

### Can I capture traces for the entire server or only on a single database?
    
You can use DEA to capture traces for all databases in the server or for a single database.
    
### I have a linked server configured in my production environment. Do those queries show up in the traces?
    
If you're running a trace capture for the entire server, the trace captures all queries, including the linked server queries. To run a trace capture for the entire server, leave the **Database name** box under **New Capture** empty.
    
### What's the minimum recommended time for production workload traces?
    
We recommend that you choose a time that best represents the entirety of your workload. That way, the analysis runs on all the queries in your workload.
    
### How important is to take a database backup right before I start a trace capture?
    
Before you start a trace capture, make sure that you back up all your target databases. The captured trace in Target 1 and Target 2 is replayed. If the database state isn't the same, the results of the experimentation are skewed.

### Can I collect XEvents instead of traces, and can I replay XEvents?
    
Yes. DEA supports XEvents. Download the latest version of DEA and give it a try.

## Troubleshoot trace captures

If you see an error when you run a trace capture, review the following prerequisites:

- Confirm that the name of the computer running SQL Server is valid. To confirm, try to connect to the computer running SQL Server by using SQL Server Management Studio (SSMS).
- Confirm that your firewall configuration doesn't block connections to the computer running SQL Server.
- Confirm that the user has the permissions that are listed in the blog posting [Replay FAQ](https://blogs.msdn.microsoft.com/datamigration/2017/03/24/dea-2-0-replay-faq/).
- Confirm that the trace name doesn't follow the standard rollover convention (Capture\_1). Instead, try trace names like Capture\_1A or Capture1.

Following are some possible errors you might see and solutions for resolving them:

|Possible errors|Solution|  
|---|---|  
|Unable to start the trace on the target SQL Server, check whether you have the required permissions and that the SQL Server account has write access to the specified trace file path Sql Error Code (53)|The user running the DEA tool must have access to the computer running SQL Server. The user must be assigned the sysadmin role.|  
|Unable to start the trace on the target SQL Server, check whether you have the required permissions and that the SQL Server account has write access to the specified trace file path Sql Error Code (19062)|The trace path specified might not exist or the folder doesn't have write permissions for the account under which SQL Server services are running (for example, NETWORK SERVICE). The path must exist, and it must have the required permissions for the trace to start.|  
|A DEA trace currently is running on the target server.|An active trace is already running on the target server. You can't start a new trace when a server-wide trace is already running.|  
|Can't open the requested database for capturing trace. This error might be caused by an incorrect database name.|The specified database doesn't exist, or it's not accessible to the current user. Use the correct database name.|  

If you see any other errors labeled *Sql Error Code*, see [System error messages](https://docs.microsoft.com/previous-versions/sql/sql-server-2008-r2/cc645603(v=sql.105)) for detailed descriptions and resolutions.
    
## Next steps

- To learn how to configure the Distributed Replay tools in SQL Server before you replay a captured trace, see [Configure replay](database-experimentation-assistant-configure-replay.md).

- For a 19-minute introduction to DEA and demonstration, watch the following video:

  > [!VIDEO https://channel9.msdn.com/Shows/Data-Exposed/Introducing-the-Database-Experimentation-Assistant/player]
