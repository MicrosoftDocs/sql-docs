---
author: bluefooted
ms.author: pamela
ms.date: 01/23/2026
ms.service: azure-vm-sql-server
ms.topic: include
---
When using an Azure Load Balancer to support a Virtual Network Name (VNN) resource, you must configure the cluster to reply to the health probe requests. If the health probe fails to get a response from a backend instance, then no new connections are sent to that backend instance until the health probe succeeds again.

To set the probe port parameter in PowerShell, use the following script once per applicable IP address resource:

```powershell
$ClusterNetworkName = "<MyClusterNetworkName>" # The cluster network name. Use Get-ClusterNetwork on Windows Server 2012 or later to find the name.
$IPResourceName = "<IPResourceName>" # The IP address resource name.
[int]$ProbePort = <nnnnn> # The probe port that you configured in the health probe of the load balancer for a given Frontend IP Address. Any unused TCP port is valid.

Import-Module FailoverClusters

Get-ClusterResource $IPResourceName | Set-ClusterParameter -Multiple @{"Address"="$IPResourceName";"ProbePort"=$ProbePort;"SubnetMask"="255.255.255.255";"Network"="$ClusterNetworkName";"EnableDhcp"=0}
```

The changes you make don't take effect until the IP address resource is taken offline and brought online again. Perform a failover of the resource for this change to take effect.

After you set the cluster probe, use the following PowerShell script to check cluster parameters:

```powershell
Get-ClusterResource $IPResourceName | Get-ClusterParameter
```

## Exclude ports from the dynamic port range

When using a health probe port between 49,152 and 65,535 (the [default dynamic port range for TCP/IP](/windows/client-management/troubleshoot-tcpip-port-exhaust#default-dynamic-port-range-for-tcpip)), add an exclusion for each health probe port on every VM.

Configuring port exclusion prevents other system processes from being dynamically assigned the same port on the VM

To set a port exclusion, use the following PowerShell script:
- for each health probe port
- on every VM

```powershell
[int]$ProbePort = <nnnnn> # The probe port that you configured in the health probe of the load balancer. Any unused TCP port is valid.

netsh int ipv4 add excludedportrange tcp startport=$ProbePort numberofports=1 store=persistent
```

To confirm that exclusions have been configured correctly, use the following command:

```powershell
netsh int ipv4 show excludedportrange tcp
```
