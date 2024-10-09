---
title: Prepare environment for link
titleSuffix: Azure SQL Managed Instance
description: Learn how to prepare your environment to create a link between SQL Server and Azure SQL Managed Instance
author: sasapopo
ms.author: sasapopo
ms.reviewer: mathoma, danil, randolphwest
ms.date: 10/03/2024
ms.service: azure-sql-managed-instance
ms.subservice: data-movement
ms.custom: ignite-2023
ms.topic: how-to
---

# Prepare your environment for a link - Azure SQL Managed Instance

[!INCLUDE [appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This article teaches you how to prepare your environment for a [Managed Instance link](managed-instance-link-feature-overview.md) so that you can replicate between SQL Server installed to Windows or Linux and Azure SQL Managed Instance. 

> [!NOTE]  
> You can automate preparing your environment for the Managed Instance link by using a downloadable script. For more information, see the [Automating link setup blog](https://techcommunity.microsoft.com/t5/modernization-best-practices-and/automating-the-setup-of-azure-sql-managed-instance-link/ba-p/3696961).

## Prerequisites

To create a link between SQL Server and Azure SQL Managed Instance, you need the following prerequisites:

- An active Azure subscription. If you don't have one, [create a free account](https://azure.microsoft.com/free/).
- [Supported version of SQL Server](managed-instance-link-feature-overview.md#prerequisites) with the required service update.
- Azure SQL Managed Instance. [Get started](instance-create-quickstart.md) if you don't have it. 
- Decide which server you intend to be the initial primary to determine where you should create the link from. 
    - Configuring a link *from* SQL Managed Instance primary to SQL Server secondary is only supported starting with [SQL Server 2022 CU10](/troubleshoot/sql/releases/sqlserver-2022/cumulativeupdate10) and by instances configured with the [SQL Server 2022 update policy](update-policy.md#sql-server-2022-update-policy). 

> [!CAUTION]  
> When you create your SQL managed instance to use with the link feature, take into account the memory requirements for any In-Memory OLTP features SQL Server uses. For more information, see [Overview of Azure SQL Managed Instance resource limits](resource-limits.md).

## Permissions

For SQL Server, you should have **sysadmin** permissions.

For Azure SQL Managed Instance, you should be a member of the [SQL Managed Instance Contributor](/azure/role-based-access-control/built-in-roles#sql-managed-instance-contributor), or have the following permissions for a custom role:

| Microsoft.Sql/ resource | Necessary permissions |
| --- | --- |
| Microsoft.Sql/managedInstances | /read, /write |
| Microsoft.Sql/managedInstances/hybridCertificate | /action |
| Microsoft.Sql/managedInstances/databases | /read, /delete, /write, /completeRestore/action, /readBackups/action, /restoreDetails/read |
| Microsoft.Sql/managedInstances/distributedAvailabilityGroups | /read, /write, /delete, /setRole/action |
| Microsoft.Sql/managedInstances/endpointCertificates | /read |
| Microsoft.Sql/managedInstances/hybridLink | /read, /write, /delete |
| Microsoft.Sql/managedInstances/serverTrustCertificates | /write, /delete, /read |

## Prepare your SQL Server instance

To prepare your SQL Server instance, you need to validate that:

- You're on the minimum supported version.
- You've enabled the availability groups feature.
- You've added the proper trace flags at startup.
- Your databases are in the full recovery model and backed up.

You need to restart SQL Server for these changes to take effect.

### Install service updates

Ensure that your SQL Server version has the appropriate servicing update installed, as listed in the [version supportability table](managed-instance-link-feature-overview.md#prerequisites). If you need to install any updates, you must restart your SQL Server instance during the update.

To check your SQL Server version, run the following Transact-SQL (T-SQL) script on SQL Server:

```sql
-- Run on SQL Server
-- Shows the version and CU of the SQL Server
USE master;
GO
SELECT @@VERSION as 'SQL Server version';
```

### Create a database master key in the `master` database

Create database master key in the `master` database, if one isn't already present. Insert your password in place of `<strong_password>` in the following script, and keep it in a confidential and secure place. Run this T-SQL script on SQL Server:

```sql
-- Run on SQL Server
-- Create a master key
USE master;
GO
CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<strong_password>';
```

To make sure that you have the database master key, use the following T-SQL script on SQL Server:

```sql
-- Run on SQL Server
USE master;
GO
SELECT * FROM sys.symmetric_keys WHERE name LIKE '%DatabaseMasterKey%';
```

### Enable availability groups

The link feature relies on the Always On availability groups feature, which is disabled by default. For more information, see [Enable the Always On availability groups feature](/sql/database-engine/availability-groups/windows/enable-and-disable-always-on-availability-groups-sql-server).

> [!NOTE]
> For SQL Server on Linux, see [Enable Always On availability groups](/sql/linux/sql-server-linux-create-availability-group#enable-the-availability-groups-feature).

To confirm the availability groups feature is enabled, run the following T-SQL script on SQL Server:

```sql
-- Run on SQL Server
-- Is the availability groups feature enabled on this SQL Server
DECLARE @IsHadrEnabled sql_variant = (select SERVERPROPERTY('IsHadrEnabled'))
SELECT
    @IsHadrEnabled as 'Is HADR enabled',
    CASE @IsHadrEnabled
        WHEN 0 THEN 'Availability groups DISABLED.'
        WHEN 1 THEN 'Availability groups ENABLED.'
        ELSE 'Unknown status.'
    END
    as 'HADR status'
```

> [!IMPORTANT]  
> For [!INCLUDE [sssql16-md](../../docs/includes/sssql16-md.md)], if you need to enable the availability groups feature, you will need to complete extra steps documented in [Prepare SQL Server 2016 prerequisites - Azure SQL Managed Instance link](managed-instance-link-preparation-wsfc.md). These extra steps are not required for [!INCLUDE [sssql19-md](../../docs/includes/sssql19-md.md)] and later versions supported by the link.

If the availability groups feature isn't enabled, follow these steps to enable it:

1. Open SQL Server Configuration Manager.
1. Select **SQL Server Services** from the left pane.
1. Right-click the SQL Server service, and then select **Properties**.

   :::image type="content" source="media/managed-instance-link-preparation/sql-server-configuration-manager-sql-server-properties.png" alt-text="Screenshot that shows SQL Server Configuration Manager, with selections for opening properties for the service.":::

1. Go to the **Always On Availability Groups** tab.
1. Select the **Enable Always On Availability Groups** checkbox, and then select **OK**.

   :::image type="content" source="media/managed-instance-link-preparation/always-on-availability-groups-properties.png" alt-text="Screenshot that shows the properties for Always On availability groups.":::

   - If using [!INCLUDE [sssql16-md](../../docs/includes/sssql16-md.md)], and if **Enable Always On Availability Groups** option is disabled with message `This computer is not a node in a failover cluster.`, follow extra steps described in [Prepare SQL Server 2016 prerequisites - Azure SQL Managed Instance link](managed-instance-link-preparation-wsfc.md). Once you've completed these other steps, come back and retry this step again.

1. Select **OK** in the dialog.
1. Restart the SQL Server service.

### Enable startup trace flags

To optimize the performance of your link, we recommend enabling the following trace flags at startup:

- `-T1800`: This trace flag optimizes performance when the log files for the primary and secondary replicas in an availability group are hosted on disks with different sector sizes, such as 512 bytes and 4 KB. If both primary and secondary replicas have a disk sector size of 4 KB, this trace flag isn't required. For more information, see [KB3009974](https://support.microsoft.com/help/3009974).
- `-T9567`: This trace flag enables compression of the data stream for availability groups during automatic seeding. The compression increases the load on the processor but can significantly reduce transfer time during seeding.


> [!NOTE]
> For SQL Server on Linux, see [Enable trace flags](/sql/linux/sql-server-linux-configure-mssql-conf#traceflags).

To enable these trace flags at startup, use the following steps:

1. Open SQL Server Configuration Manager.
1. Select **SQL Server Services** from the left pane.
1. Right-click the SQL Server service, and then select **Properties**.

   :::image type="content" source="media/managed-instance-link-preparation/sql-server-configuration-manager-sql-server-properties.png" alt-text="Screenshot that shows SQL Server Configuration Manager.":::

1. Go to the **Startup Parameters** tab. In **Specify a startup parameter**, enter `-T1800` and select **Add** to add the startup parameter. Then enter `-T9567` and select **Add** to add the other trace flag. Select **Apply** to save your changes.

   :::image type="content" source="media/managed-instance-link-preparation/startup-parameters-properties.png" alt-text="Screenshot that shows startup parameter properties.":::

1. Select **OK** to close the **Properties** window.

For more information, see the [syntax to enable trace flags](/sql/t-sql/database-console-commands/dbcc-traceon-transact-sql).

### Restart SQL Server and validate the configuration

After you've ensured that you're on a supported version of SQL Server, enabled the Always On availability groups feature, and added your startup trace flags, restart your SQL Server instance to apply all of these changes:

1. Open **SQL Server Configuration Manager**.
1. Select **SQL Server Services** from the left pane.
1. Right-click the SQL Server service, and then select **Restart**.

    :::image type="content" source="media/managed-instance-link-preparation/sql-server-configuration-manager-sql-server-restart.png" alt-text="Screenshot that shows the SQL Server restart command call.":::

After the restart, run the following T-SQL script on SQL Server to validate the configuration of your SQL Server instance:

```sql
-- Run on SQL Server
-- Shows the version and CU of SQL Server
USE master;
GO
SELECT @@VERSION as 'SQL Server version';
GO
-- Shows if the Always On availability groups feature is enabled
SELECT SERVERPROPERTY ('IsHadrEnabled') as 'Is Always On enabled? (1 true, 0 false)';
GO
-- Lists all trace flags enabled on SQL Server
DBCC TRACESTATUS;
```

Your SQL Server version should be one of the supported versions applied with the appropriate service updates, the Always On availability groups feature should be enabled, and you should have the trace flags `-T1800` and `-T9567` enabled. The following screenshot is an example of the expected outcome for a SQL Server instance that's been properly configured:

:::image type="content" source="media/managed-instance-link-preparation/ssms-results-expected-outcome.png" alt-text="Screenshot that shows the expected outcome in S S M S.":::

## Configure network connectivity

For the link to work, you must have network connectivity between SQL Server and SQL Managed Instance. The network option that you choose depends on whether or not your SQL Server instance is on an Azure network. 

### SQL Server on Azure Virtual Machines

Deploying SQL Server on Azure Virtual Machines in the same Azure virtual network that hosts SQL Managed Instance is the simplest method, because network connectivity will automatically exist between the two instances. For more information, see [Quickstart: Configure an Azure VM to connect to Azure SQL Managed Instance](connect-vm-instance-configure.md).

If your SQL Server on Azure Virtual Machines instance is in a different virtual network from your managed instance, you need to make a connection between both virtual networks. The virtual networks don't have to be in the same subscription for this scenario to work.

There are two options for connecting virtual networks:

- [Azure virtual network peering](/azure/virtual-network/virtual-network-peering-overview)
- VNet-to-VNet VPN gateway ([Azure portal](/azure/vpn-gateway/vpn-gateway-howto-vnet-vnet-resource-manager-portal), [PowerShell](/azure/vpn-gateway/vpn-gateway-vnet-vnet-rm-ps), [Azure CLI](/azure/vpn-gateway/vpn-gateway-howto-vnet-vnet-cli))

Peering is preferable because it uses the Microsoft backbone network, so from the connectivity perspective, there's no noticeable difference in latency between virtual machines in a peered virtual network and in the same virtual network. Virtual network peering is supported between the networks in the same region. Global virtual network peering is supported for instances hosted in subnets created after September 22, 2020. For more information, see [Frequently asked questions (FAQ)](frequently-asked-questions-faq.yml#does-sql-managed-instance-support-global-vnet-peering).

### SQL Server outside Azure

If your SQL Server instance is hosted outside Azure, establish a VPN connection between SQL Server and SQL Managed Instance by using either of these options:

- [Site-to-site VPN connection](/office365/enterprise/connect-an-on-premises-network-to-a-microsoft-azure-virtual-network)
- [Azure ExpressRoute connection](/azure/expressroute/expressroute-introduction)

> [!TIP]  
> We recommend ExpressRoute for the best network performance when you're replicating data. Provision a gateway with enough bandwidth for your use case.

### Network ports between the environments

Regardless of the connectivity mechanism, there are requirements that must be met for the network traffic to flow between the environments:

The Network Security Group (NSG) rules on the subnet hosting managed instance needs to allow:

- Inbound port 5022 and port range 11000-11999 to receive traffic from the source SQL Server IP
- Outbound port 5022 to send traffic to the destination SQL Server IP

All firewalls on the network hosting SQL Server, and the host OS needs to allow:

- Inbound port 5022 opened to receive traffic from the source IP range of the MI subnet /24 (for example 10.0.0.0/24)
- Outbound ports 5022, and the port range 11000-11999 opened to send traffic to the destination IP range of MI subnet (example 10.0.0.0/24)

:::image type="content" source="media/managed-instance-link-preparation/link-networking-requirements.png" alt-text="Diagram showing network requirements to set up the link between SQL Server and managed instance.":::

The following table describes port actions for each environment:

| Environment | What to do |
| :--- | :--- |
| SQL Server (in Azure) | Open both inbound and outbound traffic on port 5022 for the network firewall to the entire subnet IP range of SQL Managed Instance. If necessary, do the same on the SQL Server host OS (Windows/Linux) firewall. To allow communication on port 5022, create a network security group (NSG) rule in the virtual network that hosts the VM. |
| SQL Server (outside Azure) | Open both inbound and outbound traffic on port 5022 for the network firewall to the entire subnet IP range of SQL Managed Instance. If necessary, do the same on the SQL Server host OS (Windows/Linux) firewall. |
| SQL Managed Instance | [Create an NSG rule](/azure/virtual-network/manage-network-security-group#create-a-security-rule) in Azure portal to allow inbound and outbound traffic from the IP address and the networking hosting SQL Server on port 5022 and port range 11000-11999. |

Use the following PowerShell script on the Windows host OS of the SQL Server instance, to open ports in Windows Firewall:

```powershell
New-NetFirewallRule -DisplayName "Allow TCP port 5022 inbound" -Direction inbound -Profile Any -Action Allow -LocalPort 5022 -Protocol TCP
New-NetFirewallRule -DisplayName "Allow TCP port 5022 outbound" -Direction outbound -Profile Any -Action Allow -LocalPort 5022 -Protocol TCP
```

The following diagram shows an example of an on-premises network environment, indicating that *all firewalls in the environment need to have open ports*, including the OS firewall hosting the SQL Server, and any corporate firewalls and/or gateways:

:::image type="content" source="media/managed-instance-link-preparation/link-networking-infrastructure.png" alt-text="Diagram showing network infrastructure to set up the link between SQL Server and managed instance.":::

> [!IMPORTANT]  
> - Ports need to be open in every firewall in the networking environment, including the host server, as well as any corporate firewalls or gateways on the network. In corporate environments, you might need to show your network administrator the information in this section to help open additional ports in the corporate networking layer.
> - While you can choose to customize the endpoint on the SQL Server side, port numbers for SQL Managed Instance can't be changed or customized.
> - IP address ranges of subnets hosting managed instances, and SQL Server must not overlap.

### Add URLs to allowlist

Depending on your network security settings, it might be necessary to add URLs for the SQL Managed Instance FQDN and some of the Resource Management endpoints used by Azure to your allowlist. 

The following lists the resources that should be added to your allowlist: 

- The fully qualified domain name (FQDN) of your SQL Managed Instance. For example: *managedinstance1.6d710bcf372b.database.windows.net*.
- Microsoft Entra Authority
- Microsoft Entra Endpoint Resource ID
- Resource Manager Endpoint
- Service Endpoint 

Follow the steps in the [Configure SSMS for government clouds](#configure-ssms-for-government-clouds) section to access the **Tools** interface in SQL Server Management Studio (SSMS) and identify the specific URLs for the resources within your cloud you need to add to your allowlist.


## Test network connectivity

Bidirectional network connectivity between SQL Server and SQL Managed Instance is necessary for the link to work. After you open ports on the SQL Server side and configure an NSG rule on the SQL Managed Instance side, test connectivity by using either SQL Server Management Studio (SSMS) or Transact-SQL. 

Test the network by creating a temporary SQL Agent job on both SQL Server and SQL Managed Instance to check the connection between the two instances. When you use **Network Checker** in SSMS, the job is automatically created for you, and deleted after the test completes. You need to manually delete the SQL Agent job if you test your network by using T-SQL. 

> [!NOTE]
> Executing PowerShell scripts by the SQL Server Agent on SQL Server on Linux is not currently supported, so it's not currently possible to execute `Test-NetConnection` from the SQL Server Agent job on SQL Server on Linux.

To use the SQL Agent to test network connectivity, you need the following requirements: 
- The user doing the test must have [permissions to create a job](/sql/ssms/agent/configure-a-user-to-create-and-manage-sql-server-agent-jobs) (either as a **sysadmin** or belongs to the SQLAgentOperator role for `msdb`) for both SQL Server and SQL Managed Instance. 
- The SQL Server Agent service must be [running](/sql/ssms/agent/start-stop-or-pause-the-sql-server-agent-service) on SQL Server. Since the Agent is on by default on SQL Managed Instance, no additional action is necessary.


### [SSMS](#tab/ssms)

To test network connectivity between SQL Server and SQL Managed Instance in SSMS, follow these steps: 

1. Connect to the instance that will be the primary replica in SSMS. 
1. In **Object Explorer**, expand databases, and right-click the database you intend to link with the secondary. Select **Tasks** > **Azure SQL Managed Instance link** > **Test Connection** to open the **Network Checker** wizard: 

   :::image type="content" source="media/managed-instance-link-preparation/test-connection-in-ssms.png" alt-text="Screenshot of object explorer in S S M S, with test connection selected in the database link right-click menu.":::

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

   :::image type="content" source="media/managed-instance-link-preparation/test-connectivity-parameters-output.png" alt-text="Screenshot that shows the output of the script that generates parameter values for testing connectivity in SSMS.":::

   Save the results to use the next steps. Since these parameters can change after any failover, be sure to generate them again, if necessary.

1. Connect to SQL Server. 

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

   :::image type="content" source="media/managed-instance-link-preparation/test-connectivity-results.png" alt-text="Screenshot that shows the output with the test results in S S M S.":::

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

:::image type="content" source="media/managed-instance-link-preparation/ssms-output-tnchelper.png" alt-text="Screenshot that shows the expected output of the NetHelper SQL Agent job.":::

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

## Migrate a certificate of a TDE-protected database (optional)

If you're linking a SQL Server database protected by Transparent Data Encryption (TDE) to a managed instance, you must migrate the corresponding encryption certificate from the on-premises or Azure VM SQL Server instance to the managed instance before using the link. For detailed steps, see [Migrate a certificate of a TDE-protected database to Azure SQL Managed Instance](tde-certificate-migrate.md).

SQL Managed Instance databases that are encrypted with service-managed TDE keys can't be linked to SQL Server. You can link an encrypted database to SQL Server only if it was encrypted with a customer-managed key and the destination server has access to the same key that's used to encrypt the database. For more information, see [Set up SQL Server TDE with Azure Key Vault](/sql/relational-databases/security/encryption/setup-steps-for-extensible-key-management-using-the-azure-key-vault). 

> [!NOTE]
> Azure Key Vault is supported by SQL Server on Linux starting with [SQL Server 2022 CU 14](/troubleshoot/sql/releases/sqlserver-2022/cumulativeupdate14). 

## Install SSMS

SQL Server Management Studio (SSMS) is the easiest way to use the Managed Instance link. [Download SSMS version 19.0, or later](/sql/ssms/download-sql-server-management-studio-ssms) and install it to your client machine.

After installation finishes, open SSMS and connect to your supported SQL Server instance. Right-click a user database and validate that the **Azure SQL Managed Instance link** option appears on the menu.

:::image type="content" source="media/managed-instance-link-preparation/ssms-database-context-menu-managed-instance-link.png" alt-text="Screenshot that shows the Azure SQL Managed Instance link option on the context menu.":::

## Configure SSMS for government clouds

If you want to deploy your SQL Managed Instance to a government cloud, you need to modify your SQL Server Management Studio (SSMS) settings to use the correct cloud. If you're not deploying your SQL Managed Instance to a government cloud, skip this step.

To update your SSMS settings, follow these steps:

1. Open SSMS.
1. From the menu, select **Tools** and then choose **Options**.
1. Expand **Azure Services** and select **Azure Cloud**.
1. Under **Select an Azure Cloud**, use the dropdown list to choose **AzureUSGovernment**, or another government cloud, such as **AzureChinaCloud**:

:::image type="content" source="media/managed-instance-link-preparation/ssms-for-government-cloud.png" alt-text="Screenshot of SSMS UI, options page, Azure services, with Azure cloud highlighted. ":::

If you want to go back to the public cloud, choose **AzureCloud** from the dropdown list.

## Related content 

- [Overview of the Managed Instance link feature](managed-instance-link-feature-overview.md)
- [Configure link between SQL Server and SQL Managed instance with SSMS](managed-instance-link-configure-how-to-ssms.md)
- [Configure link between SQL Server and SQL Managed instance with scripts](managed-instance-link-configure-how-to-scripts.md)
