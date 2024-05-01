---
title: "Create clustered DTC resource for an availability group"
description: "This article walks you through a complete configuration of a clustered DTC resource for a SQL Server Always On availability group."
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 01/15/2024
ms.service: sql
ms.subservice: availability-groups
ms.topic: how-to
monikerRange: ">=sql-server-2016"
---
# Create clustered DTC resource for an Always On availability group

[!INCLUDE [sql windows only](../../../includes/applies-to-version/sql-windows-only.md)]

This article walks you through a complete configuration of a clustered DTC resource for a [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] Always On availability group (AG). The complete configuration can take up to an hour to complete.

The walkthrough creates a clustered DTC resource and the [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] AGs to align with the requirements at [How to cluster the DTC service for an Always On availability group](cluster-dtc-for-sql-server-2016-availability-groups.md).

The walkthrough uses PowerShell and Transact-SQL (T-SQL) scripts. Many of the T-SQL scripts require **SQLCMD Mode** to be enabled. For more information on **SQLCMD Mode**, see [Edit SQLCMD Scripts with Query Editor](../../../tools/sqlcmd/edit-sqlcmd-scripts-query-editor.md). The PowerShell module `FailoverClusters` must be imported. For more information about importing a PowerShell module, see [Importing a PowerShell Module](/powershell/scripting/developer/module/importing-a-powershell-module). This walkthrough is based on the following configuration options:

- All requirements from [Prerequisites, restrictions, and recommendations for Always On availability groups](prereqs-restrictions-recommendations-always-on-availability.md) are met.
- The domain is `contoso.lab`.
- The user has the Create Computer objects permission in the OU where the DTC Network Name resource will be created.
- The user is a domain user with administrator rights on all nodes in the cluster.
- A file share called `sqlbackups` has been created for backups.
- The default instances of [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] are named `SQLNODE1` and `SQLNODE2`.
- The same service account is used on all instances of [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)].
- The user is a member of the fixed [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] role sysadmin on all instances of [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)].
- The default outcome of transactions that DTC can't resolve will be set to `presume commit`.
- The mirroring endpoint will use port `5022`.
- No other AGs or clustered DTC resources exist.
- Cluster details (Existing):
  - Name: `Cluster`
  - Network Name: `Cluster Network 1`
  - Nodes: `SQLNODE1, SQLNODE2`
  - Shared storage: `Cluster Disk 3` (Owned by `SQLNODE1`)
- Cluster details (To be created):
  - Network Name resource: `DTCnet1`
  - DTC Network Name resource:  `DTC1`
  - DTC Physical Disk resource: `DTCDisk1`
  - DTC IP and subnet resource: `192.168.2.54`, `255.255.255.0`
  - DTC IP resource: `DTCIP1`

## 1. Check operating system

For supported distributed transactions, [!INCLUDE [ssHADR](../../../includes/sshadr-md.md)] must be running on [!INCLUDE [winserver2012r2-md](../../../includes/winserver2012r2-md.md)] and later versions. For [!INCLUDE [winserver2012r2-md](../../../includes/winserver2012r2-md.md)], you must install the update in [KB3090973](https://support.microsoft.com/help/3090973). This script checks the operating system version, specifies whether you need to install hotfix 3090973. Run the following PowerShell script on `SQLNODE1`.

```powershell
# A few OS checks

<#
Script:
    1) is re-runnable
    2) will work on any node in the cluster, and
    3) will execute against every node in the cluster
#>

$nodes = (Get-ClusterNode).Name;
foreach ($node in $nodes) {

    # At least 2012 R2
    $os = (Get-WmiObject -class Win32_OperatingSystem -ComputerName $node).caption;
    if ($os -like "*2012 R2*" -or $os -like "*2016*" -or $os -like "*2019*" -or $os -like "*2022*") {
        Write-Host "$os is supported on $node.";
    }
    else {
        Write-Host "STOP! $os is not supported on $node.";
    }

    # KB 3090973
    if ($os -like "*2012 R2*") {
        $kb = Get-Hotfix -ComputerName $node | Where { $_.HotFixID -eq 'KB3090973' };
        if ($kb) {
            Write-Host "KB3090973 is installed on $node."
        }
        else {
            Write-Host "HotFixID KB3090973 must be applied on $node. See https://support.microsoft.com/help/3090973 for additional information and to download the hotfix.";
        }
    }
    else {
        Write-Host "KB3090973 does not apply to $os on $node."
    }
}
```

## 2. Configure firewall rules

This script configures the firewall to allow DTC traffic on each [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] hosting an AG replica, and any other server engaging in the distributed transaction. The script also configures the firewall to allow connections for the database mirroring endpoint. Run the following PowerShell script on `SQLNODE1`.

```powershell
# Configure Firewall

<#
Script:
    1) is re-runnable
    2) will work on any node in the cluster, and
    3) will execute against every node in the cluster
#>

$nodes = (Get-ClusterNode).Name;
foreach ($node in $nodes) {
    Get-NetFirewallRule -CimSession $node -DisplayGroup 'Distributed Transaction Coordinator' -Enabled False -ErrorAction SilentlyContinue | Enable-NetFirewallRule;
    New-NetFirewallRule -CimSession $node -DisplayName 'SQL Server Mirroring' -Description 'Port 5022 for SQL Server Mirroring' -Action Allow -Direction Inbound -Protocol TCP -LocalPort 5022 -RemotePort Any -LocalAddress Any -RemoteAddress Any;
};
```

## 3. Configure in-doubt xact resolution

This script configures the **[in-doubt xact resolution](../../configure-windows/in-doubt-xact-resolution-server-configuration-option.md)** server configuration option to "presume commit" for in-doubt transactions.

Run the following T-SQL script in SQL Server Management Studio (SSMS) against `SQLNODE1` in **SQLCMD mode**.

```sql
/*******************************************************************
    Execute script in its entirety on SQLNODE1 in SQLCMD mode
*******************************************************************/
-- Configure in-doubt xact resolution on all SQL Server instances to presume commit
IF (
    SELECT CAST(value_in_use AS BIT)
    FROM sys.configurations WITH (NOLOCK)
    WHERE [name] = N'show advanced options'
) = 0
BEGIN
    EXEC sp_configure 'show advanced options', 1;
    RECONFIGURE;
END

-- Configure the server to presume commit for in-doubt transactions.
IF (
    SELECT CAST(value AS BIT)
    FROM sys.configurations WITH (NOLOCK)
    WHERE [name] = N'in-doubt xact resolution'
) <> 1
BEGIN
    EXEC sp_configure 'in-doubt xact resolution', 1;
    RECONFIGURE;
END
GO

-----------------------------------------------------------------------------
:connect SQLNODE2

IF (
    SELECT CAST(value_in_use AS BIT)
    FROM sys.configurations WITH (NOLOCK)
    WHERE [name] = N'show advanced options'
) = 0
BEGIN
    EXEC sp_configure 'show advanced options', 1;
    RECONFIGURE;
END

-- Configure the server to presume commit for in-doubt transactions.
IF (
    SELECT CAST(value AS BIT)
    FROM sys.configurations WITH (NOLOCK)
    WHERE [name] = N'in-doubt xact resolution'
) <> 1
BEGIN
    EXEC sp_configure 'in-doubt xact resolution', 1;
    RECONFIGURE;
END
GO
```

## 4. Create test databases

The script creates a database named `AG1` on `SQLNODE1` and a database named `dtcDemoAG1` on `SQLNODE2`. Run the following T-SQL script in SSMS against `SQLNODE1` in **SQLCMD mode**.

```sql
/*******************************************************************
    Execute script in its entirety on SQLNODE1 in SQLCMD mode
*******************************************************************/
-- On SQLNODE1
USE master;
GO

SET NOCOUNT ON;

IF EXISTS (
    SELECT *
    FROM sys.databases
    WHERE name = N'AG1'
)
BEGIN
    DROP DATABASE AG1;
END
GO

CREATE DATABASE AG1;
GO

ALTER DATABASE AG1
SET RECOVERY FULL WITH NO_WAIT;

ALTER AUTHORIZATION ON DATABASE::AG1 TO sa;
GO

USE AG1;

CREATE TABLE [dbo].[Names] (
    [Name] [varchar](64) NULL,
    [EditDate] DATETIME
);

INSERT [dbo].[Names]
VALUES ('AG1', GETDATE());
GO

-- Against SQNODE2
:connect SQLNODE2

USE master;
GO

SET NOCOUNT ON;

IF EXISTS (
    SELECT *
    FROM sys.databases
    WHERE name = N'dtcDemoAG1'
)
BEGIN
    DROP DATABASE dtcDemoAG1;
END
GO

CREATE DATABASE dtcDemoAG1;
GO

ALTER DATABASE dtcDemoAG1
SET RECOVERY SIMPLE WITH NO_WAIT;

ALTER AUTHORIZATION ON DATABASE::dtcDemoAG1 TO sa;
GO

USE dtcDemoAG1;
GO

CREATE TABLE [dbo].[Names] (
    [Name] [varchar](64) NULL,
    [EditDate] DATETIME
);
GO
```

## 5. Create endpoints

This script creates an endpoint called `AG1_endpoint` that listens on TCP port `5022`. Run the following T-SQL script in SSMS against `SQLNODE1` in **SQLCMD mode**.

```sql
/**********************************************
Execute on SQLNODE1 in SQLCMD mode
**********************************************/
-- Create endpoint on the server instance that hosts the primary replica:
IF NOT EXISTS (SELECT * FROM sys.database_mirroring_endpoints)
BEGIN
    CREATE ENDPOINT AG1_endpoint AUTHORIZATION [sa]
        STATE = STARTED
        AS TCP (LISTENER_PORT = 5022)
    FOR DATABASE_MIRRORING(ROLE = ALL);
END
GO

-----------------------------------------------------------------------------
:connect SQLNODE2

IF NOT EXISTS (SELECT *FROM sys.database_mirroring_endpoints)
BEGIN
    CREATE ENDPOINT AG1_endpoint AUTHORIZATION [sa]
        STATE = STARTED
        AS TCP (LISTENER_PORT = 5022)
    FOR DATABASE_MIRRORING(ROLE = ALL);
END
GO
```

## 6. Prepare databases for availability group

The script backs up `AG1` on `SQLNODE1` and restores it to `SQLNODE2`. Run the following T-SQL script in SSMS against `SQLNODE1` in **SQLCMD mode**.

```sql
/*******************************************************************
    Execute script in its entirety on SQLNODE1 in SQLCMD mode
*******************************************************************/
-- Backup database
BACKUP DATABASE AG1 TO DISK = N'\\sqlnode1\sqlbackups\AG1.bak'
WITH FORMAT, STATS = 10;

-- Backup transaction log
BACKUP LOG AG1 TO DISK = N'\\sqlnode1\sqlbackups\AG1_Log.bak'
WITH FORMAT, STATS = 10;
GO

-- Restore database and logs on secondary WITH NORECOVERY
:connect SQLNODE2

USE [master];
GO

RESTORE DATABASE AG1
FROM DISK = N'\\sqlnode1\sqlbackups\AG1.bak'
WITH NORECOVERY, STATS = 10;

RESTORE LOG AG1
FROM DISK = N'\\sqlnode1\sqlbackups\AG1_Log.bak'
WITH NORECOVERY, STATS = 10;
GO
```

## 7. Create availability group

[!INCLUDE [ssHADR](../../../includes/sshadr-md.md)] must be created with the `CREATE AVAILABILITY GROUP` command and the `WITH DTC_SUPPORT = PER_DB` clause. You can't currently alter an existing AG. The New Availability Group wizard doesn't allow you to enable DTC support for a new AG. The following script creates the new AG and joins the secondary. Run the following T-SQL script in SSMS against `SQLNODE1` in **SQLCMD mode**.

```sql
/*******************************************************************
    Execute the script in its entirety on SQLNODE1 in SQLCMD mode
*******************************************************************/
--  Create availability group on SQLNODE1
USE master;
GO

CREATE AVAILABILITY GROUP DTCAG1
WITH (DTC_SUPPORT = PER_DB)
FOR DATABASE AG1 REPLICA ON
'SQLNODE1' WITH (
    ENDPOINT_URL = 'TCP://SQLNODE1.contoso.lab:5022',
    AVAILABILITY_MODE = SYNCHRONOUS_COMMIT,
    FAILOVER_MODE = MANUAL
),
'SQLNODE2' WITH (
    ENDPOINT_URL = 'TCP://SQLNODE2.contoso.lab:5022',
    AVAILABILITY_MODE = SYNCHRONOUS_COMMIT,
    FAILOVER_MODE = MANUAL
);
GO

-- SQLNODE2
-- Join a secondary replica to the availability group
:connect SQLNODE2

ALTER AVAILABILITY GROUP DTCag1 JOIN;

-- Join the database to the availability group
ALTER DATABASE AG1
SET HADR AVAILABILITY GROUP = DTCAG1;
GO
```

Starting with [!INCLUDE [sssql16-md](../../../includes/sssql16-md.md)] Service Pack 2 you can alter an AG for distributed transactions. For [!INCLUDE [sssql16-md](../../../includes/sssql16-md.md)] versions before Service Pack 2, you can't enable DTC on an existing [!INCLUDE [ssHADR](../../../includes/sshadr-md.md)]. [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] accepts the following syntax for an existing AG:  

```sql
USE master;
GO
ALTER AVAILABILITY GROUP <availability_group>
SET (DTC_Support = Per_DB);
```

However, no configuration change is made. You can confirm the **dtc_support** configuration with the following T-SQL query:  

```sql
SELECT name, dtc_support FROM sys.availability_groups; 
```

The only way to enable DTC support on an AG for versions before [!INCLUDE [sssql16-md](../../../includes/sssql16-md.md)] versions before Service Pack 2, is by creating an AG using Transact-SQL.

## <a id="ClusterDTC"></a> 8. Prepare cluster resources

This script prepares the DTC dependent resources: Disk and IP. The shared storage is added to the Windows Cluster. Network resources are created, and then the DTC is created and made as a resource to the AG. Run the following PowerShell script on `SQLNODE1`. This script is based on the sample located at [How to Properly Configure DTC for Clustered Instances of SQL Server (Revised)](https://sqlha.com/how-to-properly-configure-dtc-for-clustered-instances-of-sql-server-with-windows-server-2008-r2/).

```powershell
# Create a clustered Microsoft Distributed Transaction Coordinator properly in the resource group with SQL Server

<#----------------------------------- Begin User Input -----------------------------------#>
$AGgrp = "DTCag1"; # Name of the WSFC resource group that will contain the DTC resource

$WSFC = (Get-Cluster).Name; # Windows Failover Cluster name
$DTCnetwk = "Cluster Network 1" # WSFC Network to use for the DTC IP address

$ClusterAvailableDisk = "Cluster Disk 3"; # Designated disk that can support failover clustering and is visible to all nodes but not yet part of the set of clustered disks
$DTCdisk = "DTCDisk1"; # Name of the disk to be used with DTC

$DTCipresnm = "DTCIP1"; # WSFC Friendly Name of the DTC's IP resource
$DTCipaddr = "192.168.2.54"; # IP address of the DTC resource
$DTCsubnet = "255.255.255.0"; # Subnet for the DTC IP address
$DTCnetnm = "DTCNet1"; # WSFC Friendly Name of the Network Name resource
$DTCresnm = "DTC1"; # Name of the WSFC DTC Network Name resource; Name must be unique in AD
<#------------------------------------ End User Input ------------------------------------#>

# Make a new disk available for use in a failover cluster.
Get-ClusterAvailableDisk | Where-Object { $_.Name -eq $ClusterAvailableDisk } | Add-ClusterDisk;

# Rename disk
$resource = Get-ClusterResource $ClusterAvailableDisk; $resource.Name = $DTCdisk;

# Create the IP resource
Add-ClusterResource -Name $DTCipresnm -ResourceType "IP Address" -Group $AGgrp;

# Set the network to use, IP address, and subnet
# All three have to be configured at the same time
$DTCIPres = Get-ClusterResource $DTCipresnm;
$ntwk = New-Object Microsoft.FailoverClusters.PowerShell.ClusterParameter $DTCipres, Network, $DTCnetwk;
$ipaddr = New-Object Microsoft.FailoverClusters.PowerShell.ClusterParameter $DTCipres, Address, $DTCipaddr;
$subnet = New-Object Microsoft.FailoverClusters.PowerShell.ClusterParameter $DTCipres, SubnetMask, $dtcsubnet;

$setdtcipparams = $ntwk, $ipaddr, $subnet;
$setdtcipparams | Set-ClusterParameter;

# Create the Network Name resource
Add-ClusterResource $DTCnetnm -ResourceType "Network Name" -Group $AGgrp;

# Set the value for the Network Name resource
Get-ClusterResource $DTCnetnm | Set-ClusterParameter DnsName $DTCresnm;

# Add the IP address as a depenency of the Network Name resource
Add-ClusterResourceDependency $DTCnetnm $DTCipresnm;

# Create the Distributed Transaction Coordinator resource
Add-ClusterResource $DTCresnm -ResourceType "Distributed Transaction Coordinator" -Group $AGgrp;

# Add the Network Name as a dependency of the DTC resource
Add-ClusterResourceDependency $DTCresnm $DTCnetnm;

# Move the disk into the resource group with SQL Server
Move-ClusterResource -Name $DTCdisk -Group $AGgrp;

# Add the disk as a dependency of the DTC resource
Add-ClusterResourceDependency $DTCresnm $DTCdisk;

# Bring the IP resource online
Start-ClusterResource $DTCipresnm;

# Bring the Network Name resource online
Start-ClusterResource $DTCnetnm;

# Bring the DTC resource online
Start-ClusterResource $DTCresnm;
```

## 9. Enable Network DTC access

The following script enables Network DTC Access for the clustered DTC service to allow remote computers to enlist in distributed transactions over the network. Run the following PowerShell script on `SQLNODE1`.

```powershell
# Enable Network DTC access for the clustered DTC service

<#
Script:
    1) is re-runnable
    2) will work on any node in the cluster, and
    3) will execute against every node in the cluster
#>

# Enter the Name of DTC resource

$DtcName = "DTC1";
<# ------- End of User Input ------- #>

[bool]$restart = 0;
$node = (Get-ClusterResource -Name $DtcName).OwnerNode.Name;
$DtcSettings = Get-DtcNetworkSetting -DtcName $DtcName;

if ($DtcSettings.InboundTransactionsEnabled -eq $false) {
    Set-DtcNetworkSetting -CimSession $node -DtcName $DtcName -AuthenticationLevel "Mutual" -InboundTransactionsEnabled $true -Confirm:$false;
    $restart = 1;
}

if ($DtcSettings.OutboundTransactionsEnabled -eq $false) {
    Set-DtcNetworkSetting -CimSession $node -DtcName $DtcName -AuthenticationLevel "Mutual" -OutboundTransactionsEnabled $true -Confirm:$false;
    $restart = 1;
}

if ($restart -eq 1) {
    Stop-Dtc -CimSession $node -DtcName $DTCname -Confirm:$false;
    Start-Dtc -CimSession $node -DtcName $DTCname;
}
```

## 10. Disable and stop the local DTC service on each node

Disable the local DTC on both nodes to guarantee that distributed transactions use the clustered DTC resource. The following script disables and stops the local DTC service on each node. Run the following PowerShell script on `SQLNODE1`.

```powershell
# Disable local DTC service

<#
Script:
    1) is re-runnable
    2) will work on any node in the cluster, and
    3) will execute against every node in the cluster
#>

$DTCname = 'Local';
$nodes = (Get-ClusterNode).Name;

foreach ($node in $nodes) {

    $service = Get-WmiObject -class Win32_Service -computername $node -Filter "Name='MSDTC'";
    if ($service.StartMode -ne 'Disabled') {
        $service.ChangeStartMode('Disabled');
    }

    if ($service.State -ne 'Stopped') {
        $service.StopService();
    }
}
```

## 11. Cycle the [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] service for each instance

With the clustered DTC service completely configured, you need to stop and restart each instance of [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] in the AG in order to make sure [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] is enrolled to use this DTC service.

The first time [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] service requires a distributed transaction. It enrolls with a DTC service. The [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] service continues to use that DTC service until it restarts. If a clustered DTC service is available, [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] enrolls with the clustered DTC service. If a clustered DTC service isn't available, [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] enrolls with the local DTC service. To verify that [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] enrolls with the clustered DTC service, stop and restart each instance of [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)].

Follow the steps contained in the following T-SQL script:

```sql
/*
Gracefully cycle the SQL Server service and failover the availability group
    a.    On SQLNODE2, cycle the SQL Server service from SQL Server Configuration Manager

    b.    On SQLNODE2 failover, the availability group to SQLNODE2
        Execute the T-SQL script below on SQLNODE2 (Use Results to Text)

    c.    On SQLNODE1, cycle the SQL Server service from SQL Server Configuration Manager

    d.    On SQLNODE1, failover the availability group to SQLNODE1 once the databases are back in sync.
        Execute the T-SQL script below on SQLNODE1 (Use Results to Text)
*/
SET NOCOUNT ON;

-- Ensure replica is secondary
IF (
    SELECT rs.is_primary_replica
    FROM sys.availability_groups ag
    INNER JOIN sys.dm_hadr_database_replica_states rs
        ON ag.group_id = rs.group_id
    WHERE ag.name = N'DTCag1' AND rs.is_local = 1
) = 0
BEGIN
    -- Wait for SYNCHRONIZED state
    DECLARE @ctr TINYINT = 0;
    DECLARE @msg VARCHAR(128);

    WHILE (
        SELECT synchronization_state
        FROM sys.availability_groups ag
        INNER JOIN sys.dm_hadr_database_replica_states rs
            ON ag.group_id = rs.group_id
        WHERE ag.name = N'DTCag1'
            AND rs.is_primary_replica = 0
            AND rs.is_local = 1
    ) <> 2
    BEGIN
        WAITFOR DELAY '00:00:01';

        SET @ctr += 1;
        SET @msg = 'Waiting for databases to become synchronized. Duration in seconds: ' + cast(@ctr AS VARCHAR(3));

        RAISERROR (@msg, 0, 1) WITH NOWAIT;
    END;

    ALTER AVAILABILITY GROUP DTCAG1 FAILOVER;
    SELECT 'Failover complete' AS [Sucess];
END;
ELSE
BEGIN
    SELECT 'This instance is the primary replica. Connect to the secondary replica and try again.' AS [Error];
END;
```

## 12. Test configuration

This test uses a linked server from `SQLNODE1` to `SQLNODE2` to create a distributed transaction. Ensure the AG primary replica is on `SQLNODE1`. To test the configuration, you will:

- Create linked servers
- Execute a distributed transaction

### Create linked servers

The following script creates two linked servers on `SQLNODE1`. Run the following T-SQL script in SSMS against `SQLNODE1`.

```sql
-- SQLNODE1
IF NOT EXISTS (SELECT * FROM sys.servers WHERE name = N'SQLNODE1')
BEGIN
    EXEC master.dbo.sp_addlinkedserver @server = N'SQLNODE1';
END

IF NOT EXISTS (SELECT * FROM sys.servers WHERE name = N'SQLNODE2')
BEGIN
    EXEC master.dbo.sp_addlinkedserver @server = N'SQLNODE2';
END
 ```

### Execute a distributed transaction

This script first returns the current DTC transaction statistics. Then the script executes a distributed transaction utilizing databases on `SQLNODE1` and `SQLNODE2`. Then the script returns the DTC transaction statics, which should now increase the count. Physically connect to `SQLNODE1` and run the following T-SQL script in SSSMS against `SQLNODE1` in **SQLCMD mode**.

```sql
/*******************************************************************
    Execute the script in its entirety on SQLNODE1 in SQLCMD mode
    Must be physically connected to SQLNODE1
*******************************************************************/
USE AG1;
GO
SET NOCOUNT ON;

-- Get Baseline
!! Powershell; $DtcNameC = Get-DtcClusterDefault; Get-DtcTransactionsStatistics -DtcName $DtcNameC;

SET XACT_ABORT ON
BEGIN DISTRIBUTED TRANSACTION
    INSERT INTO SQLNODE1.[AG1].[dbo].[Names] VALUES ('TestValue1', GETDATE());
    INSERT INTO SQLNODE2.[dtcDemoAG1].[dbo].[Names] VALUES ('TestValue2', GETDATE());
COMMIT TRAN
GO

-- Review DTC Transaction Statistics
!! Powershell; $DtcNameC = Get-DtcClusterDefault; Get-DtcTransactionsStatistics -DtcName $DtcNameC;
```

> [!IMPORTANT]  
> The `USE AG1` statement must be executed to ensure the database context is set to `AG1`. Otherwise, you will receive the following error message: "Transaction context in use by another session."
