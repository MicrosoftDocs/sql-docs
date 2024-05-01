---
title: "MSSQLSERVER_19407"
description: "MSSQLSERVER_19407"
author: pijocoder
ms.author: jopilov
ms.reviewer: randolphwest
ms.date: 04/03/2024
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "19407 (Database Engine error)"
---
# MSSQLSERVER_19407

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

## Details

| Attribute | Value |
| :--- | :--- |
| Product Name | SQL Server |
| Event ID | 19407 |
| Event Source | MSSQLSERVER |
| Component | SQLEngine |
| Symbolic Name | HADR_AG_LEASE_EXPIRED |
| Message Text | The lease between availability group '%.*ls' and the Windows Server Failover Cluster has expired. A connectivity issue occurred between the instance of SQL Server and the Windows Server Failover Cluster. To determine whether the availability group is failing over correctly, check the corresponding availability group resource in the Windows Server Failover Cluster. |

## Explanation

Error 19407 is raised in the SQL Server error log when the communication between SQL Server and the Windows Server Failover cluster is lost. Typically a corrective action occurs - a failover to another Always On node.

A lease is a time-based communication mechanism that takes place between the SQL Server and the Windows Server Failover Cluster (WSFC) process, specifically the RHS.EXE process. The two processes communicate with each other periodically to ensure the other process is running and responding. This communication takes place using Windows [Event objects](/windows/win32/sync/event-objects) and ensures that a failover of the AG resource doesn't occur without the knowledge of the WSFC. If one of the processes doesn't respond to the lease communication based on a predefined lease period, a lease timeout occurs. For detailed information, see [Mechanics and guidelines of lease, cluster, and health check timeouts for Always On availability groups](../../database-engine/availability-groups/windows/availability-group-lease-healthcheck-timeout.md). Also see [How It Works: SQL Server AlwaysOn Lease Timeout](https://techcommunity.microsoft.com/t5/sql-server-support-blog/how-it-works-sql-server-alwayson-lease-timeout/ba-p/317268)

### Causes

Since Windows Events are light-weight synchronization objects, there's relatively small number of external factors that affect them negatively. Typical issues that can lead to lease timeout involve system-wide problems. Here's a list of possibilities that can cause lease expiration and cause a restart or failover:

- High CPU usage on the system (close to 100%).

- Out-of-memory conditions - low virtual memory and/or one of the processes is being paged out.

- WSFC going offline due to quorum loss. To troubleshoot quorum loss issues, see [Configure and manage quorum](/windows-server/failover-clustering/manage-cluster-quorum), and [WSFC Disaster Recovery through Forced Quorum (SQL Server)](../../sql-server/failover-clusters/windows/wsfc-disaster-recovery-through-forced-quorum-sql-server.md).

- [VM throttling](/azure/virtual-machines/premium-storage-performance#throttling) affecting performance and causing lease expiration.

- The SQL Server process doesn't respond while generating a large memory dump. For more information about stack dump generation, see [Impact of dump generation](/troubleshoot/sql/tools/use-sqldumper-generate-dump-file#impact-of-dump-generation). Stack dump generation can occur for some of the following reasons:

  - Nonyielding scheduler
  - Latch timeout
  - Deadlocked scheduler
  - Unresolved deadlock

## User action

### Troubleshoot high CPU issues

1. Open Task Manager.

1. Go to the **Performance** tab and see if CPUs are close to or at 100% utilization.

1. Go to the **Processes** tab and sort processes by the CPU column in descending order, by selecting on the **CPU** column.

1. Identify the process that uses most CPU, and work on understanding and resolving the reason for it causing the high CPU.

1. If the process is SQL Server, see [Troubleshoot high-CPU-usage issues in SQL Server](/troubleshoot/sql/performance/troubleshoot-high-cpu-usage-issues).

1. You can use the following PowerShell script to check for CPU utilization on the system.

   ```powershell
   Get-Counter -Counter "\Processor(_Total)\% Processor Time" -SampleInterval 5 -MaxSamples 30 |
       Select-Object -ExpandProperty CounterSamples | Select-Object TimeStamp, Path, CookedValue
   ```

### Troubleshoot low memory issues

If there are occurrences of low virtual or physical memory on the system, the SQL Server or the cluster resource host service (`RHS.exe`) process could be paged out. If the process is paged out to disk, it doesn't execute actively, and the lease timeout might be reached by the time memory is available and the process virtual bytes are paged back into physical memory. Low virtual memory can be caused by applications, drivers, or OS consuming the entire memory on the system. Use the following methods to troubleshoot this issue:

1. Check the Application or System event log for errors like `Your system is low on virtual memory`. You might even see this error displayed on screen if you're signed in to the server.

1. Open Task manager, select Performance -> Memory to check if close to 100% percent of memory is being consumed. Use the Details tab to identify any applications that might be the largest memory consumers.

1. You can alternatively use Performance Monitor and monitor these counters over time:

   - **Process\Working Set** - to check individual processes memory usage
   - **Memory\Available MBytes** - to check overall memory usage on the system

   You can use the following PowerShell script to identify overall memory usage across all process and the available memory on the system. If you would like to get individual processes memory usage, change it `"\Process(_Total)\Working Set"` to `"\Process(*)\Working Set"`.

   ```powershell
   $serverName = $env:COMPUTERNAME
   $Counters = @(
     ("\\$serverName" + "\Process(_Total)\Working Set") , ("\\$serverName" + "\Memory\Available Bytes")
   )

   Get-Counter -Counter $Counters -MaxSamples 30 | ForEach-Object {
       $_.CounterSamples | ForEach-Object {
           [pscustomobject]@{
               TimeStamp = $_.TimeStamp
               Path      = $_.Path
               Value_MB  = ([Math]::Round($_.CookedValue, 3)) / 1024 / 1024
           }
           Start-Sleep -s 5
       }
   }
   ```

1. If you identify specific applications that are consuming large amounts of memory, consider stopping or moving those applications on another system or control their memory usage.

1. If SQL Server is consuming large amounts of memory, you might consider using `sp_configure 'max server memory'` to lower its memory usage.

### Performance Monitor data collection for CPU, memory, and disk

This PowerShell script facilitates the collection of performance monitor (PerfMon) data with respect to CPU, memory, and disk. The script is designed to be flexible, allowing customization for both default and named instances of SQL Server.

```powershell
#Replace with your instance name if need to collect PerfMon data for named instance
$InstanceName = 'MSSQLSERVER'

# Replace with your desired location
$Location = "D:\PerfMonLogs"

# Function to create performance counter log
function Create-PerfCounterLog {
    param
    (
        [string]$InstanceName,
        [string]$Location
    )
    $counters = @(
        '\Memory\*',
        '\PhysicalDisk(*)\*',
        '\LogicalDisk(*)\*',
        '\Server\*',
        '\System\*',
        '\Process(*)\*',
        '\Processor(*)\*',
        '\SQLServer:Databases(*)\*',
        '"\SQLServer:Buffer Manager\*"',
        '"\SQLServer:SQL Statistics\*"',
        '"\SQLServer:Transactions\*"',
        '"\SQLServer:Database Mirroring\*"',
        '"\SQLServer:Latches\*"',
        '"\SQLServer:General Statistics\*"',
        '"\SQLServer:Availability Replica(*)\*"',
        '"\SQLServer:Plan Cache(*)\*"'
    )
    if ($InstanceName -eq 'MSSQLSERVER') {
        # This is for the default SQL Server instance
        $logmanCommand = "logman create counter MS_perf_log -f bin -c $counters"

    }
    else {
        # This is for a named SQL Server instance
        $InstanceName = "MSSQL`$$InstanceName"
        $counters = $counters -replace 'SQLServer', $InstanceName
        $logmanCommand = "logman create counter MS_perf_log -f bin -c $counters"

    }

    $Location = $Location + '\MS_perf_log.blg'
    $logmanCommand += " -si 00:00:01 -max 500 -o $Location"

    Start-Process -FilePath "cmd.exe" -ArgumentList "/c $logmanCommand" -Verb RunAs -Wait

}

# Function to start the collector
function Start-PerfCounterLog {
    Start-Process -FilePath "cmd.exe" -ArgumentList "/c logman start MS_perf_log" -Verb RunAs -Wait
}

# Function to stop and delete the collector
function Stop-Delete-PerfCounterLog {
    Start-Process -FilePath "cmd.exe" -ArgumentList "/c logman stop MS_perf_log" -Verb RunAs -Wait
    Start-Process -FilePath "cmd.exe" -ArgumentList "/c logman delete MS_perf_log" -Verb RunAs -Wait
}

# Create folder if not exists - update the file path as per your environment
$folderPath = $Location
if (-not (Test-Path $folderPath)) {
    New-Item -Path $folderPath -ItemType Directory
}

# Create performance counter log
Create-PerfCounterLog -InstanceName $InstanceName -Location $Location

# Start the collector
Start-PerfCounterLog

# If the event has occurred again and captured, then stop the collector
# Uncomment below line when you want to stop and delete the collector
# Stop-Delete-PerfCounterLog
```

### Reduce or avoid large memory dumps of the SQL Server or cluster process

In some cases, the SQL Server process might encounter exceptions, asserts, scheduler issues, and so on. In those cases, SQL Server triggers the `SQLDumper.exe` process by default, to generate a minidump with indirect memory. However, if that dump generation takes a long time, the SQL Server process stops responding, which might trigger a lease timeout. Common causes for a memory dump to take a long time include:

- large memory usage by the process
- the I/O subsystem where the dump is written is slow
- the default setting was changed from mini dump to a filtered or full dump

To avoid a lease timeout, use the following steps on AG systems:

- Increase session-timeout, for example, 120 seconds for all replicas
- Change the auto failover of all replicas to manual failover
- Increase the LeaseTimeout to 60,000 ms (60 seconds) and change HealthCheckTimeout to 90,000 ms (90 seconds)

For more information, see [Use the Sqldumper.exe tool to generate a dump file in SQL Server](/troubleshoot/sql/tools/use-sqldumper-generate-dump-file).

### Check virtual machine (VM) configuration for overprovisioning

If you're using a virtual machine, ensure that you aren't overprovisioning or overcommitting CPUs and memory resources. Overprovisioning CPUs or memory might cause the guest OS to run out of resources and show the same problems described earlier - high CPU and low memory. Frequently if you're viewing things inside the guest OS, explaining why you're running out of computing resources is difficult, because things are happening outside of the virtual machine itself. Overcommitting resources can cause temporary halts of processing, which are likely to cause lease timeouts. For more information on how to address overcommitting, see [Troubleshooting ESX/ESXi virtual machine performance issues (2001003)](https://kb.vmware.com/s/article/2001003) and [Virtualization – Overcommitting memory and how to detect it within the VM](https://techcommunity.microsoft.com/t5/running-sap-applications-on-the/virtualization-8211-overcommitting-memory-and-how-to-detect-it/ba-p/367623).

### Check for virtual machine (VM) migration or backup

Hyper-V, VMware, and other VM solutions offer the ability to move VMs between host machines (Hyper-V Live Migration and VMware vMotion). In most cases, these technologies provide an almost instantaneous migration. However, if there are network or host machine bottlenecks, these migrations can be prolonged, which causes the VM to be in a *suspended*, non-operational state. This can lead to the lease timeout expiration between SQL Server and the cluster processes. Resolve any issues with the VM migration first before you address lease timeout issues.

Virtual machine backups solutions can also cause a downtime for VMs. If a VM backup is being taken at host OS, or any similar maintenance is done at the host machine that takes a long time, they can lead to a lease timeout issue. The reason is that while the clock is ticking, the SQL Server and cluster processes aren't able to communicate with each other on the suspended VM. Address any delays caused by VM backups or other maintenance first, before you examine lease timeout issues.
