---
title: "Find Errors with Transactional Replication"
description: Describes how to locate and identify errors with Transactional Replication, as well as the troubleshooting methodology for addressing issues with replication.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 06/12/2025
ms.service: sql
ms.subservice: replication
ms.topic: how-to
helpviewer_keywords:
  - "replication [SQL Server], tutorials"
monikerRange: "=azuresqldb-mi-current || >=sql-server-2017"
ms.custom:
  - updatefrequency5
  - sfi-image-nochange
---
# Troubleshooter: Find errors with SQL Server transactional replication

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Troubleshooting replication errors can be frustrating without a basic understanding of how transactional replication works. The first step in creating a publication is having the Snapshot Agent create the snapshot and save it to the snapshot folder. Next, the Distribution Agent applies the snapshot to the subscriber.

This process creates the publication and puts it in the *synchronizing* state. Synchronization works in three phases:

1. Transactions occur on objects that are replicated, and are marked "for replication" in the transaction log.

1. The Log Reader Agent scans through the transaction log and looks for transactions that are marked "for replication." These transactions are then saved to the distribution database.

1. The Distribution Agent scans through the distribution database by using the reader thread. Then, by using the writer thread, this agent connects to the subscriber to apply those changes to the subscriber.

Errors can occur in any step of this process. Finding those errors can be the most challenging aspect of troubleshooting synchronization issues. Thankfully, the use of Replication Monitor makes this process easy.

> [!NOTE]  
> The purpose of this troubleshooting guide is to teach troubleshooting methodology. It's designed not to solve your specific error, but to provide general guidance in finding errors with replication. Some specific examples are provided, but the resolution to them can vary depending on the environment. The example errors are based on the [Tutorial: Configure replication between two fully connected servers (transactional)](tutorial-replicating-data-between-continuously-connected-servers.md) tutorial.

## Troubleshooting methodology

### Questions to ask

1. Where in the synchronization process is replication failing?
1. Which agent is experiencing an error?
1. When was the last time replication worked successfully? Has anything changed since then?

### Steps to take

1. Use Replication Monitor to identify at which point replication is encountering the error (which agent?):

   - If errors are occurring in the **Publisher to Distributor** section, the issue is with the Log Reader Agent.
   - If errors are occurring in the **Distributor to Subscriber** section, the issue is with the Distribution Agent.

1. Look through that agent's job history in Job Activity Monitor to identify details of the error. If the job history isn't showing enough details, you can [enable verbose logging](#enable-verbose-logging-on-any-agent) on that specific agent.

1. Try to determine a solution for the error.

## Find errors with the Snapshot Agent

The Snapshot Agent generates the snapshot and writes it to the specified snapshot folder.

1. View the status of your Snapshot Agent:

   1. In Object Explorer, expand the **Local Publication** node under **Replication**.

   1. Right-click your publication **AdvWorksProductTrans** > **View Snapshot Agent Status**.

   :::image type="content" source="media/troubleshooting-tran-repl-errors/view-snapshot-agent-status.png" alt-text="Screenshot of View Snapshot Agent Status command on the shortcut menu.":::

1. If an error is reported in the Snapshot Agent status, you can find more details in the Snapshot Agent job history:

   1. Expand **SQL Server Agent** in Object Explorer and open Job Activity Monitor.

   1. Sort by **Category** and identify the Snapshot Agent by the category **REPL-Snapshot**.

   1. Right-click the Snapshot Agent and then select **View History**.

   :::image type="content" source="media/troubleshooting-tran-repl-errors/snapshot-agent-history.png" alt-text="Screenshot of Selections for opening the Snapshot Agent history." lightbox="media/troubleshooting-tran-repl-errors/snapshot-agent-history.png":::

1. In the Snapshot Agent history, select the relevant log entry. This is usually a line or two *before* the entry that's reporting the error. (A red X indicates errors.) Review the message text in the box below the logs:

   :::image type="content" source="media/troubleshooting-tran-repl-errors/snapshot-access-denied.png" alt-text="Screenshot of Snapshot Agent error for denied access." lightbox="media/troubleshooting-tran-repl-errors/snapshot-access-denied.png":::

   ```output
   The replication agent had encountered an exception.
   Exception Message: Access to path '\\node1\repldata.....' is denied.
   ```

If your Windows permissions aren't configured correctly for your snapshot folder, you see an "access is denied" error for the Snapshot Agent. You need to verify permissions to the folder where your snapshot is stored, and make sure that the account used to run the Snapshot Agent has permissions to access the share.

## Find errors with the Log Reader Agent

The Log Reader Agent connects to your publisher database and scans the transaction log for any transactions that are marked "for replication." It then adds those transactions to the distribution database.

1. Connect to the publisher in [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. Expand the server node, right-click the **Replication** folder, and then select **Launch Replication Monitor**:

   :::image type="content" source="media/troubleshooting-tran-repl-errors/launch-repl-monitor.png" alt-text="Screenshot of 'Launch Replication Monitor' command in the shortcut menu.":::

   Replication Monitor opens:

   :::image type="content" source="media/troubleshooting-tran-repl-errors/repl-monitor.png" alt-text="Screenshot of Replication Monitor." lightbox="media/troubleshooting-tran-repl-errors/repl-monitor.png":::

1. The red X indicates that the publication isn't synchronizing. Expand **My Publishers** on the left side, and then expand the relevant publisher server.

1. Select the **AdvWorksProductTrans** publication on the left, and then look for the red X on one of the tabs to identify where the issue is. In this case, the red X is on the **Agents** tab, so one of the agents is encountering an error:

   :::image type="content" source="media/troubleshooting-tran-repl-errors/agent-error.png" alt-text="Screenshot of Red X on the 'Agents' tab of Replication Monitor." lightbox="media/troubleshooting-tran-repl-errors/agent-error.png":::

1. Select the **Agents** tab to identify which agent is encountering the error:

   :::image type="content" source="media/troubleshooting-tran-repl-errors/log-reader-agent-failure.png" alt-text="Screenshot of Red X on the failing Log Reader Agent in Replication Monitor." lightbox="media/troubleshooting-tran-repl-errors/log-reader-agent-failure.png":::

1. This view shows you two agents, the Snapshot Agent and the Log Reader Agent. The one that's encountering an error has the red X. In this case, it's the Log Reader Agent.

   Double-click the line that's reporting the error to open the agent history for the Log Reader Agent. This history provides more information about the error:

   :::image type="content" source="media/troubleshooting-tran-repl-errors/log-reader-error.png" alt-text="Screenshot of Error details for the Log Reader Agent." lightbox="media/troubleshooting-tran-repl-errors/log-reader-error.png":::

   ```output
   Status: 0, code: 20011, text: 'The process could not execute 'sp_replcmds' on 'NODE1\SQL2016'.'.
   The process could not execute 'sp_replcmds' on 'NODE1\SQL2016'.
   Status: 0, code: 15517, text: 'Cannot execute as the database principal because the principal "dbo" does not exist, this type of principal cannot be impersonated, or you do not have permission.'.
   Status: 0, code: 22037, text: 'The process could not execute 'sp_replcmds' on 'NODE1\SQL2016'.'.
   ```

1. The error typically occurs when the owner of the publisher database isn't set correctly. This can happen when a database is restored. To verify this:

   1. Expand **Databases** in Object Explorer.

   1. Right-click **[!INCLUDE [sssampledbnormal-md](../../includes/sssampledbnormal-md.md)]** > **Properties**.

   1. Verify that an owner exists under the **Files** page. If this box is blank, this is the likely cause of your issue.

   :::image type="content" source="media/troubleshooting-tran-repl-errors/db-properties.png" alt-text="Screenshot of the 'Files' page in the database properties, with a blank 'Owner' box." lightbox="media/troubleshooting-tran-repl-errors/db-properties.png":::

1. If the owner is blank on the **Files** page, open a **New Query** window within the context of the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database. Run the following T-SQL code:

   ```sql
   -- set the owner of the database to 'sa' or a specific user account, without the brackets.
   EXECUTE sp_changedbowner '<useraccount>';
   -- example for sa: exec sp_changedbowner 'sa'
   -- example for user account: exec sp_changedbowner 'sqlrepro\administrator'
   ```

1. You might need to restart the Log Reader Agent:

   1. Expand the **SQL Server Agent** node in Object Explorer and open Job Activity Monitor.

   1. Sort by **Category** and identify the Log Reader Agent by the **REPL-LogReader** category.

   1. Right-click the **Log Reader Agent** job and select **Start Job at Step**.

   :::image type="content" source="media/troubleshooting-tran-repl-errors/start-job-at-step.png" alt-text="Screenshot of Selections to restart the Log Reader Agent." lightbox="media/troubleshooting-tran-repl-errors/start-job-at-step.png":::

1. Validate that your publication is now synchronizing by opening Replication Monitor again. If it's not already open, you can find it by right-clicking **Replication** in Object Explorer.

1. Select the **AdvWorksProductTrans** publication, select the **Agents** tab, and double-click the Log Reader Agent to open the agent history. You should now see that the Log Reader Agent is running and either is replicating commands or has "no replicated transactions":

   :::image type="content" source="media/troubleshooting-tran-repl-errors/log-reader-running.png" alt-text="Screenshot of Log Reader Agent running with no replicated transactions.":::

## Find errors with the Distribution Agent

The Distribution Agent finds data in the distribution database and then applies it to the subscriber.

1. Connect to the publisher in [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. Expand the server node, right-click the **Replication** folder, and then select **Launch Replication Monitor**.

1. In **Replication Monitor**, select the **AdvWorksProductTrans** publication, and select the **All Subscriptions** tab. Right-click the subscription and select **View Details**:

   :::image type="content" source="media/troubleshooting-tran-repl-errors/view-details.png" alt-text="Screenshot of 'View Details' command in the shortcut menu.":::

1. The **Distributor to Subscriber History** dialog box opens and clarifies what error the agent is encountering:

   :::image type="content" source="media/troubleshooting-tran-repl-errors/dist-history-error.png" alt-text="Screenshot of Error details for the Distribution Agent.":::

   ```output
   Error messages:
   Agent 'NODE1\SQL2016-AdventureWorks2022-AdvWorksProductTrans-NODE2\SQL2016-7' is retrying after an error. 89 retries attempted. See agent job history in the Jobs folder for more details.
   ```

1. The error indicates that the Distribution Agent is retrying. To find more information, check the job history for the Distribution Agent:

   1. Expand **SQL Server Agent** in Object Explorer > **Job Activity Monitor**.

   1. Sort the jobs by **Category**.

   1. Identify the Distribution Agent by the category **REPL-Distribution**. Right-click the agent and select **View History**.

   :::image type="content" source="media/troubleshooting-tran-repl-errors/view-dist-agent-history.png" alt-text="Screenshot of Selections for viewing the Distribution Agent history." lightbox="media/troubleshooting-tran-repl-errors/view-dist-agent-history.png":::

1. Select one of the error entries and view the error text at the bottom of the window:

   :::image type="content" source="media/troubleshooting-tran-repl-errors/dist-pw-wrong.png" alt-text="Screenshot of Error text that indicates a wrong password for the distribution agent." lightbox="media/troubleshooting-tran-repl-errors/dist-pw-wrong.png":::

   ```output
   Message:
   Unable to start execution of step 2 (reason: Error authenticating proxy NODE1\repl_distribution, system error: The user name or password is incorrect.)
   ```

1. This error indicates that the password that the Distribution Agent used is incorrect. To resolve it:

   1. Expand the **Replication** node in Object Explorer.

   1. Right-click the subscription > **Properties**.

   1. Select the ellipsis (...) next to **Agent Process Account** and modify the password.

   :::image type="content" source="media/troubleshooting-tran-repl-errors/dist-agent-pw-change.png" alt-text="Screenshot of Selections for modifying the password for the Distribution Agent." lightbox="media/troubleshooting-tran-repl-errors/dist-agent-pw-change.png":::

1. Check Replication Monitor again, by right-clicking **Replication** in Object Explorer. A red X under **All Subscriptions** indicates that the Distribution Agent is still encountering an error.

   Open the **Distribution to Subscriber** history by right-clicking the subscription in **Replication Monitor** > **View Details**. Here, the error is now different:

   :::image type="content" source="media/troubleshooting-tran-repl-errors/dist-agent-cant-connect.png" alt-text="Screenshot of Error that indicates the Distribution Agent can't connect.":::

   ```output
   Connecting to Subscriber 'NODE2\SQL2016'
   Agent message code 20084. The process could not connect to Subscriber 'NODE2\SQL2016'.
   Number:  18456
   Message: Login failed for user 'NODE2\repl_distribution'.
   ```

1. This error indicates that the Distribution Agent couldn't connect to the subscriber, because the login failed for user **NODE2\repl_distribution**. To investigate further, connect to the subscriber and open the *current* SQL Server error log under the **Management** node in Object Explorer:

   :::image type="content" source="media/troubleshooting-tran-repl-errors/login-failed.png" alt-text="Screenshot of Error that indicates the login failed for the subscriber." lightbox="media/troubleshooting-tran-repl-errors/login-failed.png":::

   If you're seeing this error, the login is missing on the subscriber. To resolve this error, see [Security Role Requirements for Replication](security/security-role-requirements-for-replication.md).

1. After the login error is resolved, check Replication Monitor again. If all issues have been addressed, you should see a green arrow next to **Publication Name** and a status of **Running** under **All Subscriptions**.

   Right-click the subscription to open the **Distributor To Subscriber** history once more to verify success. If this is the first time you're running the Distribution Agent, you see that the snapshot has been bulk copied to the subscriber:

   :::image type="content" source="media/troubleshooting-tran-repl-errors/dist-agent-success.png" alt-text="Screenshot of Distribution Agent with a 'Running' status and a message about bulk copy." lightbox="media/troubleshooting-tran-repl-errors/dist-agent-success.png":::

## Find errors with the Merge Agent

The merge agent can take a long time to replicate changes. To determine which step of the merge replication synchronization process takes the most time, use [trace flag 101](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md#tf101) together with merge agent logging. To do this, use the following parameters for the merge agent parameters, and then restart the agent:

```console
-T 101
-output
-outputverboselevel
```

> [!NOTE]  
> If you have to write stats to the `<distribution-server>..msmerge_history` table, use trace flag 102.

A sample output of the merge agent after merge replication synchronization finishes is as follows:

```output
**************************************************************
CONNECTION TIMES --> time took to establish the connection to the servers. Publisher (all connections) 156 msec   Subscriber (all connections) 32 msec Distributor 93 msec
**************************************************************
UPLOAD COUNTERS  --> upload phase (changes from the Sub to the Pub) stats MakeGeneration Time = 343 msec. InsertGenHistory Time = 31 msec. UpdateGenHistory Time = 0 msec. ProxiedMetadata Time = 0 msec.
**************************************************************
DOWNLOAD COUNTERS  --> download phase (changes from the Pub to the Sub) stats MakeGeneration Time = 219 msec. InsertGenHistory Time = 0 msec. UpdateGenHistory Time = 0 msec.
**************************************************************
RETENTION-BASED CLEANUP STATISTICS --> sp_mergemetadataretentioncleanup proc stats Publisher: Cleanup Time 281 msec MSmerge_genhistory rows cleaned up 0 MSmerge_contents rows cleaned up 0 MSmerge_tombstone rows cleaned up 0 Subscriber: Cleanup Time 187 msec MSmerge_genhistory rows cleaned up 0 MSmerge_contents rows cleaned up 0 MSmerge_rowtrack rows cleaned up 0 MSmerge_tombstone rows cleaned up 0
**************************************************************
RETRY STATISTICS Retry Time (Upload) 0 msec. Retry Time (Download) 0 msec. Total changes retried 0 Number of Iterations through rows needing retry 0 Total number of changes that failed despite retry 0
**************************************************************
PROXY METADATA QUEUE COUNTERS Queue Full: Number of Waits: 0, Total Wait Time: 0 msec
**************************************************************
Distributor-side History Logging Time = 219 msec. Number of Distributor-side History Messages Logged = 11 Subscriber-side History Logging Time = 295 msec. Number of Subscriber-side History Messages Logged = 11
**************************************************************
2013-05-28 17:24:11.820 OLE DB Subscriber '<SQL Server name>\sql2008r2': DBCC SQLPERF (NETSTATS)  2013-05-28 17:24:11.822 OLE DB Publisher '<SQL Server name>\SQL2008R2':  DBCC SQLPERF (NETSTATS)  2013-05-28 17:24:11.824 OLE DB Distributor '<SQL Server name>\SQL2008R2':  DBCC SQLPERF (NETSTATS)  NETWORK STATISTICS Server  Reads  Writes  Bytes Read Bytes Written Publisher 74  74  19112  37526 Subscriber 73  73  19032  36931 Distributor 75  75  19192  38121
**************************************************************
NETWORK STATUS Network Connection: The computer has one or more LAN cards that are active. Network link speed: Destination Incoming  Outgoing Publisher Unreachable  Unreachable Subscriber Unreachable  Unreachable Distributor Unreachable  Unreachable
**************************************************************
```

## Enable verbose logging on any agent

You can use verbose logging to see more detailed information about errors occurring with any agent in the replication topology. The steps are the same for each agent. Just make sure that you're selecting the correct agent in Job Activity Monitor.

> [!NOTE]  
> The agents can be on either the publisher or the subscriber, based on whether it's a pull or push subscription. If the agent isn't available on the server you're investigating, check the other server.

1. Decide where you want the verbose logging to be saved, and ensure that the folder exists. This example uses c:\temp.

1. Expand the **SQL Server Agent** node in Object Explorer and open Job Activity Monitor.

   :::image type="content" source="media/troubleshooting-tran-repl-errors/job-activity-monitor.png" alt-text="Screenshot of 'View Job Activity' command on the shortcut menu for Job Activity Monitor.":::

1. Sort by **Category** and identify the agent of interest. This example uses the Log Reader Agent. Right-click the agent of interest > **Properties**.

   :::image type="content" source="media/troubleshooting-tran-repl-errors/log-agent-properties.png" alt-text="Screenshot of Selections to open agent properties." lightbox="media/troubleshooting-tran-repl-errors/log-agent-properties.png":::

1. Select the **Steps** page, and then highlight the **Run agent** step. Select **Edit**.

   :::image type="content" source="media/troubleshooting-tran-repl-errors/edit-steps.png" alt-text="Screenshot of Selections for editing the 'Run agent' step.":::

1. In the **Command** box, start a new line, enter the following text, and select **OK**:

   ```console
   -Output C:\Temp\OUTPUTFILE.txt -Outputverboselevel 3
   ```

   You can modify the location and verbosity level according to your preference.

   :::image type="content" source="media/troubleshooting-tran-repl-errors/verbose.png" alt-text="Screenshot of Verbose output in the properties for the job step.":::

   When adding the verbose output parameter, the following issues can cause your agent to fail, or for the outfile file to be missing:

   - There's a formatting issue where the dash became a hyphen.

   - The location doesn't exist on disk, or the account that's running the agent lacks permission to write to the specified location.

   - There's a space missing between the last parameter and the `-Output` parameter.

   - Different agents support different levels of verbosity. If you enable verbose logging but your agent fails to start, try decreasing the specified verbosity level by 1.

1. Restart the Log Reader Agent by right-clicking the agent > **Stop Job at Step**. Refresh by selecting the **Refresh** icon from the toolbar. Right-click the agent > **Start Job at Step**.

1. Review the output on disk.

   :::image type="content" source="media/troubleshooting-tran-repl-errors/output.png" alt-text="Screenshot of Output text file." lightbox="media/troubleshooting-tran-repl-errors/output.png":::

1. To disable verbose logging, follow the same previous steps to remove the entire `-Output` line that you added earlier.

## Related content

- [Transactional replication](transactional/transactional-replication.md)
- [Replication tutorials](replication-tutorials.md)

[!INCLUDE [get-help-options](../../includes/paragraph-content/get-help-options.md)]
