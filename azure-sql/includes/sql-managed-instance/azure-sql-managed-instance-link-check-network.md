---
author: bluefooted
ms.author: pamela
ms.reviewer: vanto
ms.date: 12/06/2024
ms.service: azure-sql-managed-instance
ms.topic: include
---

Bidirectional network connectivity between SQL Server and SQL Managed Instance is necessary for the link to work. After you open ports on the SQL Server side and configure an NSG rule on the SQL Managed Instance side, test connectivity by using either SQL Server Management Studio (SSMS) or Transact-SQL. 

Test the network by creating a temporary SQL Agent job on both SQL Server and SQL Managed Instance to check the connection between the two instances. When you use **Network Checker** in SSMS, the job is automatically created for you, and deleted after the test completes. You need to manually delete the SQL Agent job if you test your network by using T-SQL. 

> [!NOTE]
> Executing PowerShell scripts by the SQL Server Agent on SQL Server on Linux is not currently supported, so it's not currently possible to execute `Test-NetConnection` from the SQL Server Agent job on SQL Server on Linux.

To use the SQL Agent to test network connectivity, you need the following requirements: 
- The user doing the test must have [permissions to create a job](/sql/ssms/agent/configure-a-user-to-create-and-manage-sql-server-agent-jobs) (either as a **sysadmin** or belongs to the SQLAgentOperator role for `msdb`) for both SQL Server and SQL Managed Instance. 
- The SQL Server Agent service must be [running](/sql/ssms/agent/start-stop-or-pause-the-sql-server-agent-service) on SQL Server. Since the Agent is on by default on SQL Managed Instance, no additional action is necessary.

Consider the following:
- To avoid false negatives, all firewalls along the network path must allow Internet Control Message Protocol (ICMP) traffic.
- To avoid false positives, all firewalls along the network path must allow traffic on the proprietary SQL Server UCS protocol. Blocking the protocol can lead to a successful connection test, but the link fails to create.
- Advanced firewall setups with packet-level guardrails in place need to be properly configured to properly allow traffic between SQL Server and SQL Managed Instance.


### [SSMS](#tab/ssms)

To test network connectivity between SQL Server and SQL Managed Instance in SSMS, follow these steps: 

1. Connect to the instance that will be the primary replica in SSMS. 
1. In **Object Explorer**, expand databases, and right-click the database you intend to link with the secondary. Select **Tasks** > **Azure SQL Managed Instance link** > **Test Connection** to open the **Network Checker** wizard: 

   :::image type="content" source="../../managed-instance/media/managed-instance-link-preparation/test-connection-in-ssms.png" alt-text="Screenshot of object explorer in SSMS, with test connection selected in the database link right-click menu.":::

1. Select **Next** on the **Introduction** page of the **Network Checker** wizard.
1. If all requirements are met on the **Prerequisites** page, select **Next**. Otherwise resolve any unmet prerequisites, and then select **Re-run Validation**. 
1. On the **Login** page, select **Login** to connect to the other instance that will be the secondary replica. Select **Next**.
1. Check details on the **Specify Network Options** page and provide an IP address, if necessary. Select **Next**.
1. On the **Summary** page, review the actions the wizard takes and then select **Finish** to test the connection between the two replicas. 
1. Review the **Results** page to validate connectivity exists between the two replicas, and then select **Close** to finish. 

### [T-SQL](#tab/tsql)

To use T-SQL to test connectivity, you have to check the connection in both directions. First, test the connection from SQL Server to SQL Managed Instance, and then test the connection from SQL Managed Instance to SQL Server.

### Test connection from SQL Server to SQL Managed Instance

Use SQL Server Agent on SQL Server to run connectivity tests from SQL Server to SQL Managed Instance.

1. Connect to SQL Managed Instance, and run the following script to generate parameters you'll need later:

   ```sql
   SELECT 'DECLARE @serverName NVARCHAR(512) = N''' + value + ''''
   FROM sys.dm_hadr_fabric_config_parameters
   WHERE parameter_name = 'DnsRecordName'
   
   UNION
   
   SELECT 'DECLARE @node NVARCHAR(512) = N''' + NodeName + '.' + Cluster + ''''
   FROM (
       SELECT SUBSTRING(replica_address, 0, CHARINDEX('\', replica_address)) AS NodeName,
           RIGHT(service_name, CHARINDEX('/', REVERSE(service_name)) - 1) AppName,
           JoinCol = 1
       FROM sys.dm_hadr_fabric_partitions fp
       INNER JOIN sys.dm_hadr_fabric_replicas fr
           ON fp.partition_id = fr.partition_id
       INNER JOIN sys.dm_hadr_fabric_nodes fn
           ON fr.node_name = fn.node_name
       WHERE service_name LIKE '%ManagedServer%'
           AND replica_role = 2
   ) t1
   LEFT JOIN (
       SELECT value AS Cluster,
           JoinCol = 1
       FROM sys.dm_hadr_fabric_config_parameters
       WHERE parameter_name = 'ClusterName'
       ) t2
       ON (t1.JoinCol = t2.JoinCol)
   INNER JOIN (
       SELECT [value] AS AppName
       FROM sys.dm_hadr_fabric_config_parameters
       WHERE section_name = 'SQL'
           AND parameter_name = 'InstanceName'
       ) t3
       ON (t1.AppName = t3.AppName)
   
   UNION
   
   SELECT 'DECLARE @port NVARCHAR(512) = N''' + value + ''''
   FROM sys.dm_hadr_fabric_config_parameters
   WHERE parameter_name = 'HadrPort';
   ```

   Results should look like the following sample: 

   ```output
    DECLARE @node NVARCHAR(512) = N'DB123.tr123456.west-us.worker.database.windows.net'
    DECLARE @port NVARCHAR(512) = N'11002'
    DECLARE @serverName NVARCHAR(512) = N'contoso-instance.12345678.database.windows.net'
   ```

   Save the results to use the next steps. Since these parameters can change after any failover, be sure to generate them again, if necessary.

1. Connect to your SQL Server instance. 

1. Open a new query window and paste the following script:

   ```sql
   --START
   -- Parameters section
   DECLARE @node NVARCHAR(512) = N''
   DECLARE @port NVARCHAR(512) = N''
   DECLARE @serverName NVARCHAR(512) = N''
   
   --Script section
   IF EXISTS (
           SELECT job_id
           FROM msdb.dbo.sysjobs_view
           WHERE name = N'TestMILinkConnection'
           )
       EXEC msdb.dbo.sp_delete_job @job_name = N'TestMILinkConnection',
           @delete_unused_schedule = 1
   
   DECLARE @jobId BINARY (16),
       @cmd NVARCHAR(MAX)
   
   EXEC msdb.dbo.sp_add_job @job_name = N'TestMILinkConnection',
       @enabled = 1,
       @job_id = @jobId OUTPUT
   
   SET @cmd = (N'tnc ' + @serverName + N' -port 5022 | select ComputerName, RemoteAddress, TcpTestSucceeded | Format-List')
   
   EXEC msdb.dbo.sp_add_jobstep @job_id = @jobId,
       @step_name = N'Test Port 5022',
       @step_id = 1,
       @cmdexec_success_code = 0,
       @on_success_action = 3,
       @on_fail_action = 3,
       @subsystem = N'PowerShell',
       @command = @cmd,
       @database_name = N'master'
   
   SET @cmd = (N'tnc ' + @node + N' -port ' + @port + ' | select ComputerName, RemoteAddress, TcpTestSucceeded | Format-List')
   
   EXEC msdb.dbo.sp_add_jobstep @job_id = @jobId,
       @step_name = N'Test HADR Port',
       @step_id = 2,
       @cmdexec_success_code = 0,
       @subsystem = N'PowerShell',
       @command = @cmd,
       @database_name = N'master'
   
   EXEC msdb.dbo.sp_add_jobserver @job_id = @jobId,
       @server_name = N'(local)'
   GO
   
   EXEC msdb.dbo.sp_start_job @job_name = N'TestMILinkConnection'
   GO
   
   --Check status every 5 seconds
   DECLARE @RunStatus INT
   
   SET @RunStatus = 10
   
   WHILE (@RunStatus >= 4)
   BEGIN
       SELECT DISTINCT @RunStatus = run_status
       FROM [msdb].[dbo].[sysjobhistory] JH
       INNER JOIN [msdb].[dbo].[sysjobs] J
           ON JH.job_id = J.job_id
       WHERE J.name = N'TestMILinkConnection'
           AND step_id = 0
   
       WAITFOR DELAY '00:00:05';
   END
   
   --Get logs once job completes
   SELECT [step_name],
       SUBSTRING([message], CHARINDEX('TcpTestSucceeded', [message]), CHARINDEX('Process Exit', [message]) - CHARINDEX('TcpTestSucceeded', [message])) AS    TcpTestResult,
       SUBSTRING([message], CHARINDEX('RemoteAddress', [message]), CHARINDEX('TcpTestSucceeded', [message]) - CHARINDEX('RemoteAddress', [message])) AS    RemoteAddressResult,
       [run_status],
       [run_duration],
       [message]
   FROM [msdb].[dbo].[sysjobhistory] JH
   INNER JOIN [msdb].[dbo].[sysjobs] J
       ON JH.job_id = J.job_id
   WHERE J.name = N'TestMILinkConnection'
       AND step_id <> 0
       --END
   ```

1. Replace the `@node`, `@port`, and `@serverName` parameters with the values you got from the first step. 

1. Run the script and check the results. You should see results such as the following example:

   :::image type="content" source="../../managed-instance/media/managed-instance-link-preparation/test-connectivity-results.png" alt-text="Screenshot that shows the output with the test results in SSMS.":::

1. Verify the results:

   - The outcome of each test at TcpTestSucceeded should be `TcpTestSucceeded : True`.
   - The RemoteAddresses should belong to the IP range for the SQL Managed Instance subnet.

   If the response is unsuccessful, verify the following network settings:
   - There are rules in both the network firewall *and* the SQL Server host OS (Windows/Linux) firewall that allows traffic to the entire *subnet IP range* of SQL Managed Instance.
   - There's an NSG rule that allows communication on port 5022 for the virtual network that hosts SQL Managed Instance.

### Test connection from SQL Managed Instance to SQL Server

To check that SQL Managed Instance can reach SQL Server, first create a test endpoint. Then you use the SQL Server Agent to run a PowerShell script with the `tnc` command pinging SQL Server on port 5022 from the SQL managed instance.

To create a test endpoint, connect to SQL Server and run the following T-SQL script:

```sql
-- Run on SQL Server
-- Create the certificate needed for the test endpoint
USE MASTER
CREATE CERTIFICATE TEST_CERT
WITH SUBJECT = N'Certificate for SQL Server',
EXPIRY_DATE = N'3/30/2051'
GO

-- Create the test endpoint on SQL Server
USE MASTER
CREATE ENDPOINT TEST_ENDPOINT
    STATE=STARTED
    AS TCP (LISTENER_PORT=5022, LISTENER_IP = ALL)
    FOR DATABASE_MIRRORING (
        ROLE=ALL,
        AUTHENTICATION = CERTIFICATE TEST_CERT,
        ENCRYPTION = REQUIRED ALGORITHM AES
    )
```

To verify that the SQL Server endpoint is receiving connections on port 5022, run the following PowerShell command on the host operating system of your SQL Server instance:

```powershell
tnc localhost -port 5022
```

A successful test shows `TcpTestSucceeded : True`. You can then proceed to create a SQL Server Agent job on the SQL managed instance to try testing the SQL Server test endpoint on port 5022 from the SQL managed instance.

Next, create a SQL Server Agent job on the SQL managed instance called `NetHelper` by running the following T-SQL script on the SQL managed instance. Replace:

- `<SQL_SERVER_IP_ADDRESS>` with the IP address of SQL Server that can be accessed from SQL managed instance.

```sql
-- Run on SQL managed instance
-- SQL_SERVER_IP_ADDRESS should be an IP address that could be accessed from the SQL Managed Instance host machine.
DECLARE @SQLServerIpAddress NVARCHAR(MAX) = '<SQL_SERVER_IP_ADDRESS>'; -- insert your SQL Server IP address in here
DECLARE @tncCommand NVARCHAR(MAX) = 'tnc ' + @SQLServerIpAddress + ' -port 5022 -InformationLevel Quiet';
DECLARE @jobId BINARY(16);

IF EXISTS (
        SELECT *
        FROM msdb.dbo.sysjobs
        WHERE name = 'NetHelper'
        ) THROW 70000,
    'Agent job NetHelper already exists. Please rename the job, or drop the existing job before creating it again.',
    1
    -- To delete NetHelper job run: EXEC msdb.dbo.sp_delete_job @job_name=N'NetHelper'
    EXEC msdb.dbo.sp_add_job @job_name = N'NetHelper',
        @enabled = 1,
        @description = N'Test SQL Managed Instance to SQL Server network connectivity on port 5022.',
        @category_name = N'[Uncategorized (Local)]',
        @owner_login_name = N'sa',
        @job_id = @jobId OUTPUT;

EXEC msdb.dbo.sp_add_jobstep @job_id = @jobId,
    @step_name = N'TNC network probe from SQL MI to SQL Server',
    @step_id = 1,
    @os_run_priority = 0,
    @subsystem = N'PowerShell',
    @command = @tncCommand,
    @database_name = N'master',
    @flags = 40;

EXEC msdb.dbo.sp_update_job @job_id = @jobId,
    @start_step_id = 1;

EXEC msdb.dbo.sp_add_jobserver @job_id = @jobId,
    @server_name = N'(local)';
```

> [!TIP]  
> If you need to modify the IP address of your SQL Server for the connectivity probe from SQL managed instance, delete NetHelper job by running `EXEC msdb.dbo.sp_delete_job @job_name=N'NetHelper'`, and re-create NetHelper job using the previous script.

Then, create a stored procedure `ExecuteNetHelper` that helps run the job, and obtains results from the network probe. Run the following T-SQL script on SQL managed instance:

```sql
-- Run on managed instance
IF EXISTS(SELECT * FROM sys.objects WHERE name = 'ExecuteNetHelper')
    THROW 70001, 'Stored procedure ExecuteNetHelper already exists. Rename or drop the existing procedure before creating it again.', 1
GO
CREATE PROCEDURE ExecuteNetHelper AS
-- To delete the procedure run: DROP PROCEDURE ExecuteNetHelper
BEGIN
    -- Start the job.
    DECLARE @NetHelperstartTimeUtc DATETIME = GETUTCDATE();
    DECLARE @stop_exec_date DATETIME = NULL;

    EXEC msdb.dbo.sp_start_job @job_name = N'NetHelper';

    -- Wait for job to complete and then see the outcome.
    WHILE (@stop_exec_date IS NULL)
    BEGIN
        -- Wait and see if the job has completed.
        WAITFOR DELAY '00:00:01'

        SELECT @stop_exec_date = sja.stop_execution_date
        FROM msdb.dbo.sysjobs sj
        INNER JOIN msdb.dbo.sysjobactivity sja
            ON sj.job_id = sja.job_id
        WHERE sj.name = 'NetHelper'

        -- If job has completed, get the outcome of the network test.
        IF (@stop_exec_date IS NOT NULL)
        BEGIN
            SELECT sj.name JobName,
                sjsl.date_modified AS 'Date executed',
                sjs.step_name AS 'Step executed',
                sjsl.log AS 'Connectivity status'
            FROM msdb.dbo.sysjobs sj
            LEFT JOIN msdb.dbo.sysjobsteps sjs
                ON sj.job_id = sjs.job_id
            LEFT JOIN msdb.dbo.sysjobstepslogs sjsl
                ON sjs.step_uid = sjsl.step_uid
            WHERE sj.name = 'NetHelper'
        END

        -- In case of operation timeout (90 seconds), print timeout message.
        IF (datediff(second, @NetHelperstartTimeUtc, getutcdate()) > 90)
        BEGIN
            SELECT 'NetHelper timed out during the network check. Please investigate SQL Agent logs for more information.'

            BREAK;
        END
    END
END;
```

Run the following query on SQL managed instance to execute the stored procedure that will execute the NetHelper agent job and show the resulting log:

```sql
-- Run on managed instance
EXEC ExecuteNetHelper;
```

If the connection was successful, the log shows `True`. If the connection was unsuccessful, the log shows `False`.

:::image type="content" source="../../managed-instance/media/managed-instance-link-preparation/ssms-output-tnchelper.png" alt-text="Screenshot that shows the expected output of the NetHelper SQL Agent job.":::

If the connection was unsuccessful, verify the following items:

- The firewall on the host SQL Server instance allows inbound and outbound communication on port 5022.
- An NSG rule for the virtual network that hosts SQL Managed Instance allows communication on port 5022.
- If your SQL Server instance is on an Azure VM, an NSG rule allows communication on port 5022 on the virtual network that hosts the VM.
- SQL Server is running.
- There exists test endpoint on SQL Server.

After resolving issues, rerun NetHelper network probe again by running `EXEC ExecuteNetHelper` on managed instance.

Finally, after the network test is successful, drop the test endpoint and certificate on SQL Server by using the following T-SQL commands:

```sql
-- Run on SQL Server
DROP ENDPOINT TEST_ENDPOINT;
GO
DROP CERTIFICATE TEST_CERT;
GO
```

---

> [!CAUTION]  
> Proceed with the next steps only if you've validated network connectivity between your source and target environments. Otherwise, troubleshoot network connectivity issues before proceeding.