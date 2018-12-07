---
title: "Generate and analyze the CLUSTER.LOG for an availability group"
description: "Describes how to generate and analyze the cluster log for an Always On availability group. "
ms.custom: "ag-guide, seodec18"
ms.date: "06/14/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
ms.assetid: 01a9e3c1-2a5f-4b98-a424-0ffc15d312cf
author: rothja
ms.author: jroth
manager: craigg
---
# Generate and analyze the CLUSTER.LOG for an Always On availability group
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  As a failover cluster resource, there are external interactions between SQL Server, the Windows Server Failover Cluster service (WSFC) cluster, and the SQL Server resource DLL (hadrres.dll), that cannot be monitored within SQL Server. The WSFC log, CLUSTER.LOG, can diagnose issues in the WSFC cluster or in the SQL Server resource DLL.  
  
 The following diagram demonstrates the relationship between applications like SQL Server and Windows Cluster Manager that initiate availability group resource creation, destruction, or state changes.  
  
## Generate cluster log  
 You can generate the cluster logs in two ways:  
  
1.  Use the `cluster /log /g` command at the command prompt. This command generates the cluster logs to the \windows\cluster\reports directory on each WSFC node. The advantage of this method is that you can specify the level of detail in the generated logs by using the `/level` option. The disadvantage is that you cannot specify the destination directory for the generated cluster logs. For more information, see [How to create the cluster.log in Windows Server 2008 Failover Clustering](https://blogs.msdn.com/b/clustering/archive/2008/09/24/8962934.aspx).  
  
2.  Use the [Get-ClusterLog](https://technet.microsoft.com/library/ee461045.aspx) PowerShell cmdlet. The advantage of this method is that you can generate the cluster log from all nodes to one destination directory on the node that you run the cmdlet. The disadvantage is that you cannot specify the level of detail in the generated logs.  
  
 The following PowerShell commands generate the cluster logs from all cluster nodes from the last 15 minutes and place them in the current directory. Run the commands in a PowerShell window with Administrative privileges.  
  
```powershell  
Import-Module FailoverClusters   
Get-ClusterLog -TimeSpan 15 -Destination .  
```  
  
## Always On log verbosity  
 You can increase the verbosity of the logs in CLUSTER.LOG for an availability group. To modify the verbosity, follow the steps below:  
  
1.  From the **Start** menu, open the **Failover Cluster Manager**.  
  
2.  Expand your cluster and the **Services and applications** node, then click the availability group name.  
  
3.  In the detail pane, right-click the availability group resource and click **Properties**.  
  
4.  Click the **Properties** tab.  
  
5.  Modify the **VerboseLogging** property. By default, **VerboseLogging** is set to `0` which reports information, warnings and errors. **VerboseLogging** can be set from `0` to `2`.  
  
6.  Click **OK**.  
  
7.  Right-click the availability group resource again and click **Take this resource offline**.  
  
8.  Right-click the availability group resource again and click **Bring this resource online**.  
  
## Availability group resource events  
 The table below shows the different kinds of events you can see in CLUSTER.LOG that pertain to the availability group resource. For more information on the Resource Hosting Subsystem (RHS) and Resource Control Monitor (RCM) in WSFC, see [Resource Hosting Subsystem (RHS) In Windows Server 2008 Failover Clusters](https://blogs.technet.com/b/askcore/archive/2009/11/23/resource-hosting-subsystem-rhs-in-windows-server-2008-failover-clusters.aspx).  
  
|Identifier|Source|Example from CLUSTER.LOG|  
|----------------|------------|------------------------------|  
|Messages prefixed with `[RES]` and `[hadrag]`|hadrres.dll (Always On Resource DLL)|00002cc4.00001264::2011/08/05-13:47:42.543 INFO  [RES] SQL Server Availability Group \<ag>: `[hadrag]` Offline request.<br /><br /> 00002cc4.00003384::2011/08/05-13:47:42.558 ERR   [RES] SQL Server Availability Group \<ag>: `[hadrag]` Lease Thread terminated<br /><br /> 00002cc4.00003384::2011/08/05-13:47:42.605 INFO  [RES] SQL Server Availability Group \<ag>: `[hadrag]` Free SQL statement<br /><br /> 00002cc4.00003384::2011/08/05-13:47:42.902 INFO  [RES] SQL Server Availability Group \<ag>: `[hadrag]` Disconnect from SQL Server|  
|Messages prefixed with `[RHS]`|RHS.EXE (Resource Hosting Subsystem, host process of hadrres.dll)|00000c40.00000a34::2011/08/10-18:42:29.498 INFO  [RHS] Resource ag has come offline. RHS is about to report resource status to RCM.|  
|Messages prefixed with `[RCM]`|Resource Control Monitor (Cluster Service)|000011d0.00000f80::2011/08/05-13:47:42.480 INFO  [RCM] rcm::RcmGroup::Move: Bringing group 'ag' offline first...<br /><br /> 000011d0.00000f80::2011/08/05-13:47:42.496 INFO  [RCM] TransitionToState(ag) Online-->OfflineCallIssued.|  
|RcmApi/ClusAPI|An API call, which mostly means SQL Server is requesting the action|000011d0.00000f80::2011/08/05-13:47:42.465 INFO  [RCM] rcm::RcmApi::MoveGroup: (ag, 2)|  
  
## Debug Always On resource DLL in isolation  
 It is a debugging best practice to configure your cluster to run the Always On resource DLL (hadrres.dll) in isolation from other resource DLLs. By default, the WSFC cluster runs all resource DLLs in a single instance of rhs.exe. This causes all resources within the cluster to share the same rhs.exe instance. When you attempt to debug hadrres.dll with a debugger, pausing at a break point may cause other resources that share the rhs.exe instance to be paused as well. Also, when you run multiple availability groups in the same cluster, the same configuration can cause all availability groups to be paused when you pause at a break point to debug one availability group.  
  
 To isolate an availability group from the other cluster resource DLLs, including other availability groups, do the following to run hadrres.dll inside a separate rhs.exe process:  
  
1.  Open **Registry Editor** and navigate to the following key: HKEY_LOCAL_MACHINE\Cluster\Resources. This key contains the keys for all the resources, each with a different GUID.  
  
2.  Find the resource key that contains a **Name** value that matches your availability group name.  
  
3.  Change the **SeparateMonitor** value to **1**.  
  
4.  Restart the clustered service for your availability group in the WSFC cluster.  
  
  
