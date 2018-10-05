---
title: Replay a trace with Database Experimentation Assistant for SQL Server upgrades
description: Replay a trace with Database Experimentation Assistant
ms.custom: ""
ms.date: 10/05/2018
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

# Replay a trace with Database Experimentation Assistant

Replay trace lets you replay a captured trace file (.trc) against an upgraded test environment. For example, consider a production workload that is run out of SQL Server 2008 R2. The trace file for this workload must be replayed twice: once on an environment with the same SQL Server as production, and again on an environment with the upgrade target SQL Server version, such as SQL Server 2016.

> [!NOTE]
> To run this action, virtual machines or machines must be manually set up to run Distributed Replay traces. Read the blog about [how to set up Distributed Replay controller and clients](https://blogs.msdn.microsoft.com/datamigration/distributed-replay-controller-and-client-setup/).
>
>

## Open Replay Traces
Open the tool and select the menu icon on the left side of screen. This action opens the left side-bar menu. Choose **Replay Traces** next to the play icon.

![Replay1](./media/database-experimentation-assistant-replay-trace/dea-replay-trace-open.png)

## Enter inputs for replaying traces  

> [!NOTE]
> The Distributed Replay controller machine requires permissions to the user account you'll be using to remote.
>
>

### To grant access to the Distributed Replay controller

1. Open command prompt and type **dcomcnfg** to open the **Component Services** interface.
1. In the **Component Services** dialog, expand **Component Services** > **Computers** > **My Computer** > **DCOM Config** > **DReplayController**.
1. Right-click to access the properties of DReplayController.
1. On the **Security** tab, select **Edit** to add the user account.
1. Select **OK**.

### Verify Setup

- **SQL Server install path:** Provide the path to where SQL Server is installed. For example, [C:\\Program](/Program) Files (x86)\\Microsoft SQL Server\\120.
- **Controller machine name:** Provide the name of the machine that has been set up as the controller. This machine is the one running the Windows service named SQL Server Distributed Replay controller. The Distributed Replay controller orchestrates the actions of the Distributed Replay clients. There can only be one controller instance in each Distributed Replay environment.
- **Client machine names:** Provide the name for each client machine, separated by commas, for example client1, client2. You can have up to five client controllers. Clients are one or more machines, physical or virtual, running the Windows service named SQL Server Distributed Replay client. The Distributed Replay clients work together to simulate workloads against an instance of SQL Server. There can be one or more clients in each Distributed Replay environment.
- Select **Next**.

### Select Trace

- **Path to trace file:** Provide the path to the input trace (.trc) file.
- **Path to store replay preprocess output:**  
    \- If you don't already have the IRF file, provide the path to the location in which you want to store the IRF file and other pre-process outputs.  
    \- If you already have the IRF file, provide its path.
- Select **Next**.

### Replay Trace

- **Trace file name:** Provide a trace file name.
- **Max file size (MB):** Provide the trace file roll-over size value. The default is 200 MB. The dropdown is also editable to enter a custom value.
- **Path to store replay trace output:** Provide the path for the output trace (.trc) file.
- **SQL Server instance name:** Provide the name of the server on which to replay traces.
- Select **Start**.

If the inputs are valid, the Distributed Replay process begins. Otherwise, the fields that have invalid inputs are highlighted with red. Make sure that the entered values are correct and select **Start**.

Wait until the replay has finished running to see the location you specified. Select the bell icon at the bottom left of the navigation to monitor the replay progress.

![ReplayProgress](./media/database-experimentation-assistant-replay-trace/dea-replay-trace-progress.png)

## Frequently asked questions about Replay Trace
### What security permissions do I need to start a replay capture on my target server?

- The Windows user running the trace operation in the DEA Application must have sysadmin privileges in the target SQL Server. These permissions are needed to start a trace.
- The service account, under which the target SQL Server is running, requires write access to the specified trace file path.
- The service account, under which the Distributed Replay Client services are running, must have permission to connect and execute queries on the target SQL Server.

### Can I start more than one replay in the same session?

Yes, DEA allows multiple replays to be started and tracked to completion within the same session.

### Can I start more than one replays in parallel?

Yes, but not with the same **Controller plus Clients**. The controller and clients will be busy. Set up a separate set of **Controller plus Client** machines to start a parallel replay.

### How long does a replay typically take to complete?

A replay typically takes the same amount of time as the source trace plus the amount of time that is taken to preprocess the source trace. However, if the client machines that are registered with the controller aren't sufficient to manage the load produced from the replay, the replay can take longer to complete. Distributed Replay allows up to 16 client machines to be registered with the controller.

### How large can the target trace files get?

The target trace files can be anywhere from 5 to 15 times the size of the source trace based on how many queries are run. For instance, query plan blobs can be large and if the statistics change often, there will be more events captured for such queries.

### Why do I need to restore databases?

SQL Server is a stateful relational database management system. To properly run an A/B test, the state of the database must be retained at all times. Otherwise, you might see errors in queries during replay that won't appear in production. To prevent such errors, we recommend taking a backup right before the source capture. Similarly, restoring of the backup on the target SQL Server is required to prevent errors during replay.

### What does Pass % on the Replay screen mean?

Pass % means that only a percentage of queries passed. It lets you diagnose whether the number of errors is expected or not. The errors could be expected or could occur because the database has lost its integrity. If the pass % isn't what you expect, you can stop the trace and look at the trace file in SQL Profiler to see which queries didn't succeed.

### How can I look at the trace events collected during Replay?

Open a target trace file and view it in SQL Profiler. Or, if you wish to make modifications to the replay capture, all the SQL scripts are available at C:\\Program Files (x86)\\Microsoft Corporation\\Database Experimentation Assistant\\Scripts\\StartReplayCapture.sql.

### Which trace events does DEA collect during Replay?

The trace events captured include performance-related information. The capture configuration is present in the StartReplayCaptureTrace.sql script. These events are typical SQL Trace Events that are listed here: https://msdn.microsoft.com/en-us/library/ms186265.aspx.

## Troubleshooting Replay Trace
### I'm unable to connect to the SQL Server.

- Confirm that the SQL Server name is valid. To confirm, try connecting to the server using SSMS.
- Confirm that the firewall configuration isn't blocking connections to SQL Server.
- Confirm user has the required permissions.
- Confirm that the Distributed Replay client's service account has access to the SQL Server.

Further details can be found in the logs at %temp%\\DEA. If the problem persists, contact the product team.

### I'm unable to connect to the Distributed Replay controller.

- Verify that the Distributed Replay controller service is running on the controller machine using the Distributed Replay Management Tools (run command `dreplay.exe status -f 1`).
- If the replay is being started remotely:
  - Confirm that the machine running DEA can ping the controller successfully. Confirm firewall settings to allow connections per instructions in the Configure Replay Environment page.
  - Make sure that the DCOM Remote Launch and Remote Activation are allowed for the user of the Distributed Replay controller.
  - Make sure that the DCOM Remote Access permissions are allowed for the user of Distributed Replay controller.

### The trace file path exists on my machine. Why can't Distributed Replay controller find it?

Distributed Replay can only access local disk resources. You must copy source trace files over to the Distributed Replay controller machine before starting the replay. Also, you must provide the path on the New Replay screen of DEA. 

Also, UNC paths aren't compatible with Distributed Replay. They must be local absolute paths to the first source trace file, including extension.

### Why can't I browse for files on the New Replay screen?

Since we can't browse a remote machine's folders, browsing for files isn't useful. Copy-pasting the absolute paths is more efficient.

### I started replay with a trace, but Distributed Replay didn't replay any events.

This issue can happen because the trace file doesn't have replayable events or doesn't have information on how to replay events. Confirm whether the trace file path provided is a source trace file. The source trace file is created using the configuration provided in the StartCaptureTrace.sql script.

### I'm getting an "Unexpected error occurred!" while trying to preprocess my trace files using SQL Server 2017 Distributed Replay controller.

This issue is known in the RTM version of SQL Server 2017. For more information, see [https://support.microsoft.com/en-us/help/4045678/fix-unexpected-error-when-you-use-the-dreplay-feature-to-replay-a](https://support.microsoft.com/en-us/help/4045678/fix-unexpected-error-when-you-use-the-dreplay-feature-to-replay-a).  
  
The issue has been addressed in the latest Cumulative Update 1 for SQL Server 2017. Download the latest CU from [https://support.microsoft.com/en-us/help/4038634/cumulative-update-1-for-sql-server-2017](https://support.microsoft.com/en-us/help/4038634/cumulative-update-1-for-sql-server-2017).

## Next steps
For a 19-minute introduction and demonstration of this feature, watch the following video:

> [!VIDEO https://channel9.msdn.com/Shows/Data-Exposed/Introducing-the-Database-Experimentation-Assistant/player]
