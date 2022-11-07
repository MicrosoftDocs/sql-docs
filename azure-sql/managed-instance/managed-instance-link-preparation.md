---
title: Prepare environment for Azure SQL Managed Instance link
titleSuffix: Azure SQL Managed Instance
description: Learn how to prepare your environment for using a Managed Instance link to replicate and fail over your database to SQL Managed Instance.
author: sasapopo
ms.author: sasapopo
ms.reviewer: mathoma, danil
ms.date: 11/16/2022
ms.service: sql-managed-instance
ms.subservice: data-movement
ms.topic: how-to
---

# Prepare your environment for a link - Azure SQL Managed Instance
[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This article teaches you how to prepare your environment for a [SQL Managed Instance link](managed-instance-link-feature-overview.md) so that you can replicate databases from SQL Server to Azure SQL Managed Instance.

> [!NOTE]
> Some functionality of the link is generally available, while some is currently in preview. Review the [requirements](managed-instance-link-feature-overview.md#requirements) to learn more. 

## Prerequisites 

To use the link with Azure SQL Managed Instance, you need the following prerequisites: 

- An active Azure subscription. If you don't have one, [create a free account](https://azure.microsoft.com/free/).
- [Supported version of SQL Server](managed-instance-link-feature-overview.md#requirements) with required service update installed.
- Azure SQL Managed Instance. [Get started](instance-create-quickstart.md) if you don't have it. 

## Prepare your SQL Server instance

To prepare your SQL Server instance, you need to validate that:

- You're on the minimum supported version.
- You've enabled the availability groups feature.
- You've added the proper trace flags at startup.
- Your databases are in full recovery mode and backed up.

You'll need to restart SQL Server for these changes to take effect.

### Install service updates

To check your SQL Server version, run the following Transact-SQL (T-SQL) script on SQL Server: 

```sql
-- Run on SQL Server
-- Shows the version and CU of the SQL Server
SELECT @@VERSION as 'SQL Server version'
```

Ensure that your SQL Server version has the appropriate servicing update installed, as listed below. You must restart your SQL Server instance during the update. 

| SQL Server Version  |  Operating system (OS) | Servicing update requirement |
|---------|---------|---------|
|[!INCLUDE [sssql22-md](../../docs/includes/sssql22-md.md)] | Windows Server & Linux | SQL Server 2022 RTM | 
|[!INCLUDE [sssql19-md](../../docs/includes/sssql19-md.md)] | Windows Server |  [SQL Server 2019 CU15 (KB5008996)](https://support.microsoft.com/en-us/topic/kb5008996-cumulative-update-15-for-sql-server-2019-4b6a8ee9-1c61-482d-914f-36e429901fb6), or above for Enterprise and Developer editions, and [CU17 (KB5016394)](https://support.microsoft.com/topic/kb5016394-cumulative-update-17-for-sql-server-2019-3033f654-b09d-41aa-8e49-e9d0c353c5f7), or above, for Standard editions. |
|[!INCLUDE [sssql17-md](../../docs/includes/sssql17-md.md)] | N/A | Not supported | 
|[!INCLUDE [sssql16-md](../../docs/includes/sssql16-md.md)] | Windows Server |[SQL Server 2016 SP3 (KB 5003279)](https://support.microsoft.com/help/5003279) and [SQL Server 2016 Azure Connect pack (KB 5014242)](https://support.microsoft.com/help/5014242) |

### Create a database master key in the master database

Create database master key in the master database, if not already present. Insert your password in place of `<strong_password>` in the script below, and keep it in a confidential and secure place. Run this T-SQL script on SQL Server:

```sql
-- Run on SQL Server
-- Create a master key
USE MASTER
CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<strong_password>'
```

To make sure that you have the database master key, use the following T-SQL script on SQL Server:

```sql
-- Run on SQL Server
SELECT * FROM sys.symmetric_keys WHERE name LIKE '%DatabaseMasterKey%'
```

### Enable availability groups

The link feature for SQL Managed Instance relies on the Always On availability groups feature, which isn't enabled by default. To learn more, review [Enable the Always On availability groups feature](/sql/database-engine/availability-groups/windows/enable-and-disable-always-on-availability-groups-sql-server). 

To confirm that the Always On availability groups feature is enabled, run the following T-SQL script on SQL Server: 

```sql
-- Run on SQL Server
-- Is Always On enabled on this SQL Server
DECLARE @IsHadrEnabled sql_variant = (select SERVERPROPERTY('IsHadrEnabled'))
SELECT
    @IsHadrEnabled as 'Is HADR enabled',
    CASE @IsHadrEnabled
        WHEN 0 THEN 'Always On availability groups is DISABLED.'
        WHEN 1 THEN 'Always On availability groups is ENABLED.'
        ELSE 'Unknown status.'
    END
	as 'HADR status'
```

The above query will display if Always On availability group is enabled, or not, on your SQL Server.

>[!IMPORTANT]
> For SQL Server 2016, if you need to enable Always On availability group, you will need to complete extra steps documented in [prepare SQL Server 2016 prerequisites](managed-instance-link-preparation-wsfc.md). These extra steps are not required for all higher SQL Server versions (2019-2022) supported by the link.

If the availability groups feature isn't enabled, follow these steps to enable it, or otherwise skip to the next section: 

1. Open SQL Server Configuration Manager. 
1. Select **SQL Server Services** from the left pane. 
1. Right-click the SQL Server service, and then select **Properties**. 
 
   :::image type="content" source="./media/managed-instance-link-preparation/sql-server-configuration-manager-sql-server-properties.png" alt-text="Screenshot that shows SQL Server Configuration Manager, with selections for opening properties for the service.":::

1. Go to the **Always On Availability Groups** tab. 
1. Select the **Enable Always On Availability Groups** checkbox, and then select **OK**. 

   :::image type="content" source="./media/managed-instance-link-preparation/always-on-availability-groups-properties.png" alt-text="Screenshot that shows the properties for Always On availability groups.":::

    - If using **SQL Server 2016**, and if Enable Always On Availability Groups option is disabled with message `This computer is not a node in a failover cluster.`, follow extra steps described in [prepare SQL Server 2016 prerequisites](managed-instance-link-preparation-wsfc.md). Once you've completed these other steps, come back and retry this step again.

1. Select **OK** in the dialog
1. Restart the SQL Server service.

### Enable startup trace flags

To optimize the performance of your SQL Managed Instance link, we recommend enabling the following trace flags at startup: 

- `-T1800`: This trace flag optimizes performance when the log files for the primary and secondary replicas in an availability group are hosted on disks with different sector sizes, such as 512 bytes and 4K. If both primary and secondary replicas have a disk sector size of 4K, this trace flag isn't required. To learn more, review [KB3009974](https://support.microsoft.com/topic/kb3009974-fix-slow-synchronization-when-disks-have-different-sector-sizes-for-primary-and-secondary-replica-log-files-in-sql-server-ag-and-logshipping-environments-ed181bf3-ce80-b6d0-f268-34135711043c).
- `-T9567`: This trace flag enables compression of the data stream for availability groups during automatic seeding. The compression increases the load on the processor but can significantly reduce transfer time during seeding.

To enable these trace flags at startup, use the following steps: 

1. Open SQL Server Configuration Manager. 
1. Select **SQL Server Services** from the left pane. 
1. Right-click the SQL Server service, and then select **Properties**. 

   :::image type="content" source="./media/managed-instance-link-preparation/sql-server-configuration-manager-sql-server-properties.png" alt-text="Screenshot that shows SQL Server Configuration Manager.":::

1. Go to the **Startup Parameters** tab. In **Specify a startup parameter**, enter `-T1800` and select **Add** to add the startup parameter. Then enter `-T9567` and select **Add** to add the other trace flag. Select **Apply** to save your changes. 

   :::image type="content" source="./media/managed-instance-link-preparation/startup-parameters-properties.png" alt-text="Screenshot that shows startup parameter properties.":::

1. Select **OK** to close the **Properties** window.

To learn more, review the [syntax for enabling trace flags](/sql/t-sql/database-console-commands/dbcc-traceon-transact-sql). 

### Restart SQL Server and validate the configuration

After you've ensured that you're on a supported version of SQL Server, enabled the Always On availability groups feature, and added your startup trace flags, restart your SQL Server instance to apply all of these changes:

1. Open **SQL Server Configuration Manager**. 
1. Select **SQL Server Services** from the left pane. 
1. Right-click the SQL Server service, and then select **Restart**. 

    :::image type="content" source="./media/managed-instance-link-preparation/sql-server-configuration-manager-sql-server-restart.png" alt-text="Screenshot that shows the SQL Server restart command call.":::

After the restart, run the following T-SQL script on SQL Server to validate the configuration of your SQL Server instance: 

```sql
-- Run on SQL Server
-- Shows the version and CU of SQL Server
SELECT @@VERSION as 'SQL Server version'

-- Shows if the Always On availability groups feature is enabled 
SELECT SERVERPROPERTY ('IsHadrEnabled') as 'Is Always On enabled? (1 true, 0 false)'

-- Lists all trace flags enabled on SQL Server
DBCC TRACESTATUS
```

Your SQL Server version should be one of the supported versions with service updates applied, the Always On availability groups feature should be enabled, and you should have the trace flags `-T1800` and `-T9567` enabled. The following screenshot is an example of the expected outcome for a SQL Server instance that has been properly configured: 

:::image type="content" source="./media/managed-instance-link-preparation/ssms-results-expected-outcome.png" alt-text="Screenshot that shows the expected outcome in S S M S.":::

## Configure network connectivity

For the link to work, you must have network connectivity between SQL Server and SQL Managed Instance. The network option that you choose depends on where your SQL Server instance resides - whether it's on-premises or on a virtual machine (VM). 

### SQL Server on Azure Virtual Machines 

Deploying SQL Server on Azure Virtual Machines in the same Azure virtual network that hosts SQL Managed Instance is the simplest method, because network connectivity will automatically exist between the two instances. To learn more, see the detailed tutorial [Deploy and configure an Azure VM to connect to Azure SQL Managed Instance](./connect-vm-instance-configure.md). 

If your SQL Server on Azure Virtual Machines instance is in a different virtual network from your managed instance, you need to make a connection between both virtual networks. The virtual networks don't have to be in the same subscription for this scenario to work.

There are two options for connecting virtual networks:

- [Azure VNet peering](/azure/virtual-network/virtual-network-peering-overview)
- VNet-to-VNet VPN gateway ([Azure portal](/azure/vpn-gateway/vpn-gateway-howto-vnet-vnet-resource-manager-portal), [PowerShell](/azure/vpn-gateway/vpn-gateway-vnet-vnet-rm-ps), [Azure CLI](/azure/vpn-gateway/vpn-gateway-howto-vnet-vnet-cli))

Peering is preferable because it uses the Microsoft backbone network, so from the connectivity perspective, there is no noticeable difference in latency between virtual machines in a peered virtual network and in the same virtual network. Virtual network peering is supported between the networks in the same region. Global virtual network peering is [supported for instances hosted in subnets created starting 9/22/2020](frequently-asked-questions-faq.yml#does-sql-managed-instance-support-global-vnet-peering).

### SQL Server outside Azure 

If your SQL Server instance is hosted outside Azure, establish a VPN connection between SQL Server and SQL Managed Instance by using either of these options: 

- [Site-to-site VPN connection](/office365/enterprise/connect-an-on-premises-network-to-a-microsoft-azure-virtual-network)
- [Azure ExpressRoute connection](/azure/expressroute/expressroute-introduction)

> [!TIP]
> We recommend ExpressRoute for the best network performance when you're replicating data. Provision a gateway with enough bandwidth for your use case. 

### Network ports between the environments

Regardless of the connectivity mechanism, there are requirements that must be met for the network traffic to flow between the  environments:

The Network Security Group (NSG) rules on the subnet hosting managed instance needs to allow:
- Inbound traffic on port 5022 and port range 11000-11999 from the network hosting SQL Server

Firewall on the network hosting SQL Server, and the host OS needs to allow:
- Inbound traffic on port 5022 from the entire subnet range hosting SQL Managed Instance

:::image type="content" source="./media/managed-instance-link-preparation/link-networking-requirements.png" alt-text="Diagram showing network requirements to set up the link between SQL Server and managed instance.":::

Port numbers can't be changed or customized. IP address ranges of subnets hosting managed instance, and SQL Server must not overlap.

The following table describes port actions for each environment: 

|Environment|What to do|
|:---|:-----|
|SQL Server (in Azure) | Open both inbound and outbound traffic on port 5022 for the network firewall to the entire subnet IP range of SQL Managed Instance. If necessary, do the same on the SQL Server host OS (Windows/Linux) firewall. Create a network security group (NSG) rule in the virtual network that hosts the VM to allow communication on port 5022. |
|SQL Server (outside Azure) | Open both inbound and outbound traffic on port 5022 for the network firewall to the entire subnet IP range of SQL Managed Instance. If necessary, do the same on the SQL Server host OS (Windows/Linux) firewall. |
|SQL Managed Instance |[Create an NSG rule](/azure/virtual-network/manage-network-security-group#create-a-security-rule) in Azure portal to allow inbound and outbound traffic from the IP address and the networking hosting SQL Server on port 5022 and port range 11000-11999. |

Use the following PowerShell script on the Windows host OS of the SQL Server instance to open ports in the Windows firewall: 

```powershell
New-NetFirewallRule -DisplayName "Allow TCP port 5022 inbound" -Direction inbound -Profile Any -Action Allow -LocalPort 5022 -Protocol TCP
New-NetFirewallRule -DisplayName "Allow TCP port 5022 outbound" -Direction outbound -Profile Any -Action Allow -LocalPort 5022 -Protocol TCP
```

## Test bidirectional network connectivity

Bidirectional network connectivity between SQL Server and SQL Managed Instance is necessary for the link to work. After you open ports on the SQL Server side and configure an NSG rule on the SQL Managed Instance side, test connectivity. 

### Test the connection from SQL Server to SQL Managed Instance 

We will use SQL Agent on SQL Server to run connectivity tests from SQL Server to SQL Managed Instance.

1. Connect to SQL Managed Instance and run the following script to generate some parameters we will need later:

   ```sql
   SELECT 'DECLARE @serverName NVARCHAR(512) = N'''+ value + ''''
   FROM sys.dm_hadr_fabric_config_parameters
   WHERE parameter_name = 'DnsRecordName'
   UNION
   SELECT 'DECLARE @node NVARCHAR(512) = N'''+ NodeName + '.' + Cluster + ''''
   FROM 
   (SELECT SUBSTRING(replica_address,0, CHARINDEX('\', replica_address)) as NodeName
   , RIGHT(service_name,CHARINDEX('/', REVERSE(service_name))-1) AppName, JoinCol = 1
   FROM sys.dm_hadr_fabric_partitions fp
   JOIN sys.dm_hadr_fabric_replicas fr ON fp.partition_id = fr.partition_id
   JOIN sys.dm_hadr_fabric_nodes fn ON fr.node_name = fn.node_name
   WHERE service_name like '%ManagedServer%' and replica_role = 2) t1
   LEFT JOIN
   (SELECT value as Cluster, JoinCol = 1
   FROM sys.dm_hadr_fabric_config_parameters
   WHERE parameter_name  = 'ClusterName') t2
   ON (t1.JoinCol = t2.JoinCol)
   INNER JOIN
   (SELECT [value] AS AppName
   FROM sys.dm_hadr_fabric_config_parameters
   WHERE section_name = 'SQL' and parameter_name = 'InstanceName') t3 
   ON (t1.AppName = t3.AppName)
   UNION
   SELECT 'DECLARE @port NVARCHAR(512) = N'''+ value + ''''
   FROM sys.dm_hadr_fabric_config_parameters
   WHERE parameter_name = 'HadrPort';
   ```

   You will get something like:

   :::image type="content" source="./media/managed-instance-link-preparation/test-connectivity-parameters-output.png" alt-text="Screenshot that shows the output of the script that generates parameter values for testing connectivity in SSMS.":::

   Save the result to be used in the next steps. Note that the parameters we just generated may change after any failover on SQL Managed Instance, be sure to generate them again if needed.

1. Connect to SQL Server

1. Open a new query window and paste the following:

   ```sql
   --START
   -- Parameters section
   DECLARE @node NVARCHAR(512) = N''
   DECLARE @port NVARCHAR(512) = N''
   DECLARE @serverName NVARCHAR(512) = N''
   
   --Script section
   IF EXISTS (SELECT job_id FROM msdb.dbo.sysjobs_view WHERE name = N'TestMILinkConnection')
   EXEC msdb.dbo.sp_delete_job @job_name = N'TestMILinkConnection', @delete_unused_schedule=1
   
   DECLARE @jobId BINARY(16), @cmd NVARCHAR(MAX)
   
   EXEC  msdb.dbo.sp_add_job @job_name=N'TestMILinkConnection', @enabled=1, @job_id = @jobId OUTPUT
   
   SET @cmd = (N'tnc ' + @serverName + N' -port 5022 | select ComputerName, RemoteAddress, TcpTestSucceeded | Format-List')
   EXEC msdb.dbo.sp_add_jobstep @job_id = @jobId, @step_name = N'Test Port 5022'
   , @step_id = 1, @cmdexec_success_code = 0, @on_success_action = 3, @on_fail_action = 3
   , @subsystem = N'PowerShell', @command = @cmd, @database_name = N'master'
   
   SET @cmd = (N'tnc ' + @node + N' -port ' + @port +' | select ComputerName, RemoteAddress, TcpTestSucceeded | Format-List')
   EXEC msdb.dbo.sp_add_jobstep @job_id = @jobId, @step_name = N'Test HADR Port'
   , @step_id = 2, @cmdexec_success_code = 0, @subsystem = N'PowerShell', @command = @cmd, @database_name = N'master'
   
   EXEC msdb.dbo.sp_add_jobserver @job_id = @jobId, @server_name = N'(local)'
   GO
   EXEC msdb.dbo.sp_start_job @job_name = N'TestMILinkConnection'
   GO
   --Check status every 5 seconds
   DECLARE @RunStatus INT 
   SET @RunStatus=10
   WHILE ( @RunStatus >= 4)
   BEGIN
   SELECT distinct @RunStatus = run_status
   FROM [msdb].[dbo].[sysjobhistory] JH JOIN [msdb].[dbo].[sysjobs] J ON JH.job_id = J.job_id 
   WHERE J.name=N'TestMILinkConnection' and step_id = 0
   WAITFOR DELAY '00:00:05'; 
   END
   
   --Get logs once job completes
   SELECT [step_name]
   ,SUBSTRING([message], CHARINDEX('TcpTestSucceeded',[message]), CHARINDEX('Process Exit', [message])-CHARINDEX('TcpTestSucceeded',[message])) as TcpTestResult
   ,SUBSTRING([message], CHARINDEX('RemoteAddress',[message]), CHARINDEX ('TcpTestSucceeded',[message])-CHARINDEX('RemoteAddress',[message])) as RemoteAddressResult
   ,[run_status] ,[run_duration], [message]
   FROM [msdb].[dbo].[sysjobhistory] JH JOIN [msdb].[dbo].[sysjobs] J ON JH.job_id= J.job_id
   WHERE J.name = N'TestMILinkConnection' and step_id <> 0
   --END
   ```

1. Replace the *@node*, *@port* and *@serverName* parameters with the values you got from Step 1.

1. Run the script and check the results, you will get something like:

   :::image type="content" source="./media/managed-instance-link-preparation/test-connectivity-results.png" alt-text="Screenshot that shows the output with the test results in SSMS.":::

1. Verify the results:

   - The outcome of each test at TcpTestSucceeded should be `TcpTestSucceeded : True`.
   - The RemoteAddresses should belong to the IP range for the SQL Managed Instance subnet.

   If the response is unsuccessful, verify the following network settings:
   - There are rules in both the network firewall *and* the SQL Server host OS (Windows/Linux) firewall that allows traffic to the entire *subnet IP range* of SQL Managed Instance. 
   - There's an NSG rule that allows communication on port 5022 for the virtual network that hosts SQL Managed Instance. 

### Test the connection from SQL Managed Instance to SQL Server

To check that SQL Managed Instance can reach SQL Server, you first create a test endpoint. Then you use the SQL Agent to run a PowerShell script with the `tnc` command pinging SQL Server on port 5022 from the managed instance.

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

A successful test shows `TcpTestSucceeded : True`. You can then proceed to creating a SQL Agent job on the managed instance to try testing the SQL Server test endpoint on port 5022 from the managed instance.

Next, create a SQL Agent job on the managed instance called `NetHelper` by running the following T-SQL script on the managed instance. Replace:
- `<SQL_SERVER_IP_ADDRESS>` with the IP address of SQL Server that can be accessed from managed instance.

```sql
-- Run on managed instance
-- SQL_SERVER_IP_ADDRESS should be an IP address that could be accessed from the SQL Managed Instance host machine.
DECLARE @SQLServerIpAddress NVARCHAR(MAX) = '<SQL_SERVER_IP_ADDRESS>' -- insert your SQL Server IP address in here
DECLARE @tncCommand NVARCHAR(MAX) = 'tnc ' + @SQLServerIpAddress + ' -port 5022 -InformationLevel Quiet'
DECLARE @jobId BINARY(16)

IF EXISTS(select * from msdb.dbo.sysjobs where name = 'NetHelper') THROW 70000, 'Agent job NetHelper already exists. Please rename the job, or drop the existing job before creating it again.', 1
-- To delete NetHelper job run: EXEC msdb.dbo.sp_delete_job @job_name=N'NetHelper'

EXEC msdb.dbo.sp_add_job @job_name=N'NetHelper',
    @enabled=1,
    @description=N'Test Managed Instance to SQL Server network connectivity on port 5022.',
    @category_name=N'[Uncategorized (Local)]',
    @owner_login_name=N'sa', @job_id = @jobId OUTPUT

EXEC msdb.dbo.sp_add_jobstep @job_id=@jobId, @step_name=N'TNC network probe from MI to SQL Server',
    @step_id=1,
    @os_run_priority=0, @subsystem=N'PowerShell',
    @command = @tncCommand,
    @database_name=N'master',
    @flags=40

EXEC msdb.dbo.sp_update_job @job_id = @jobId, @start_step_id = 1

EXEC msdb.dbo.sp_add_jobserver @job_id = @jobId, @server_name = N'(local)'

```

>[!TIP]
> In case that you need to modify the IP address of your SQL Server for the connectivity probe from managed instance, delete NetHelper job by running `EXEC msdb.dbo.sp_delete_job @job_name=N'NetHelper'`, and re-create NetHelper job using the script above.

Then, create a stored procedure `ExecuteNetHelper` that will help run the job and obtain results from the network probe. Run the following T-SQL script on managed instance:

```sql
-- Run on managed instance
IF EXISTS(SELECT * FROM sys.objects WHERE name = 'ExecuteNetHelper') 
	THROW 70001, 'Stored procedure ExecuteNetHelper already exists. Rename or drop the existing procedure before creating it again.', 1
GO
CREATE PROCEDURE ExecuteNetHelper AS
-- To delete the procedure run: DROP PROCEDURE ExecuteNetHelper
BEGIN
  -- Start the job.
  DECLARE @NetHelperstartTimeUtc datetime = getutcdate()
  DECLARE @stop_exec_date datetime = null
  EXEC msdb.dbo.sp_start_job @job_name = N'NetHelper'
  
  -- Wait for job to complete and then see the outcome.
  WHILE (@stop_exec_date is null)
  BEGIN
  
    -- Wait and see if the job has completed.
    WAITFOR DELAY '00:00:01'
    SELECT @stop_exec_date = sja.stop_execution_date
    FROM msdb.dbo.sysjobs sj JOIN msdb.dbo.sysjobactivity sja ON sj.job_id = sja.job_id
    WHERE sj.name = 'NetHelper'
  
    -- If job has completed, get the outcome of the network test.
    IF (@stop_exec_date is not null)
    BEGIN
      SELECT 
        sj.name JobName, sjsl.date_modified as 'Date executed', sjs.step_name as 'Step executed', sjsl.log as 'Connectivity status'
      FROM
        msdb.dbo.sysjobs sj
        LEFT OUTER JOIN msdb.dbo.sysjobsteps sjs ON sj.job_id = sjs.job_id
        LEFT OUTER JOIN msdb.dbo.sysjobstepslogs sjsl ON sjs.step_uid = sjsl.step_uid
      WHERE
        sj.name = 'NetHelper'
    END
  
    -- In case of operation timeout (90 seconds), print timeout message.
    IF (datediff(second, @NetHelperstartTimeUtc, getutcdate()) > 90)
    BEGIN
  	SELECT 'NetHelper timed out during the network check. Please investigate SQL Agent logs for more information.'
      BREAK;
    END
  END
END
```

Run the following query on managed instance to execute the stored procedure that will execute the NetHelper agent job and show the resulting log:

```sql
-- Run on managed instance
EXEC ExecuteNetHelper

```

If the connection was successful, the log will show `True`. If the connection was unsuccessful, the log will show `False`. 

:::image type="content" source="./media/managed-instance-link-preparation/ssms-output-tnchelper.png" alt-text="Screenshot that shows the expected output of the NetHelper SQL Agent job.":::

If the connection was unsuccessful, verify the following items: 

- The firewall on the host SQL Server instance allows inbound and outbound communication on port 5022. 
- An NSG rule for the virtual network that hosts SQL Managed Instance allows communication on port 5022. 
- If your SQL Server instance is on an Azure VM, an NSG rule allows communication on port 5022 on the virtual network that hosts the VM.
- SQL Server is running.
- There exists test endpoint on SQL Server.

After resolving issues, rerun NetHelper network probe again by running `EXEC ExecuteNetHelper` on managed instance.

Finally, after the network test has been successful, drop the test endpoint and certificate on SQL Server by using the following T-SQL commands: 

```sql
-- Run on SQL Server
DROP ENDPOINT TEST_ENDPOINT
GO
DROP CERTIFICATE TEST_CERT
GO
```

> [!CAUTION]
> Proceed with the next steps only if you've validated network connectivity between your source and target environments. Otherwise, troubleshoot network connectivity issues before proceeding.

## Migrate a certificate of a TDE-protected database (optional)

If you're migrating a SQL Server database protected by Transparent Data Encryption (TDE) to a managed instance, you must migrate the corresponding encryption certificate from the on-premises or Azure VM SQL Server instance to the managed instance before using the link. For detailed steps, see [Migrate a TDE certificate to a managed instance](tde-certificate-migrate.md).

## Install SSMS

SQL Server Management Studio (SSMS) is the easiest way to use the Managed Instance link. [Download SSMS version 19.0, or later](/sql/ssms/download-sql-server-management-studio-ssms) and install it to your client machine. 

After installation finishes, open SSMS and connect to your supported SQL Server instance. Right-click a user database and validate that the **Azure SQL Managed Instance link** option appears on the menu. 

:::image type="content" source="./media/managed-instance-link-preparation/ssms-database-context-menu-managed-instance-link.png" alt-text="Screenshot that shows the Azure SQL Managed Instance link option on the context menu.":::

## Configure SSMS for government cloud 

If you want to deploy your SQL Managed Instance to a government cloud, you'll need to modify your SQL Server Management Studio (SSMS) settings to use the correct cloud. If you're not deploying your SQL Managed Instance to a government cloud, skip this step. 

To update your SSMS settings, follow these steps: 

1. Open SSMS. 
1. From the menu, select **Tools** and then choose **Options**. 
1. Expand **Azure Services** and select **Azure Cloud**. 
1. Under **Select an Azure Cloud**, use the drop-down to choose **AzureUSGovernment**, or another government cloud: 

  :::image type="content" source="media/managed-instance-link-preparation/ssms-for-government-cloud.png" alt-text="Screenshot of SSMS UI, options page, Azure services, with Azure cloud highlighted. ":::



## Next steps

- After you've prepared your environment, you're ready to start [replicating your database](managed-instance-link-use-ssms-to-replicate-database.md). To learn more, review [Link feature for Azure SQL Managed Instance](managed-instance-link-feature-overview.md). 
