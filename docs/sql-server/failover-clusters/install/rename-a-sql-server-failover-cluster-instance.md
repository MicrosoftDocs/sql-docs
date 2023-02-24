---
title: "Rename Failover Cluster Instance"
description: This article describes how to rename a SQL Server instance that is part of a failover cluster, which differs from renaming a stand-alone instance.
ms.custom: "seo-lt-2019"
ms.date: "12/13/2019"
ms.service: sql
ms.reviewer: ""
ms.subservice: failover-cluster-instance
ms.topic: how-to
helpviewer_keywords: 
  - "clusters [SQL Server], virtual servers"
  - "renaming virtual servers"
  - "virtual servers [SQL Server], failover clustering"
  - "failover clustering [SQL Server], virtual servers"
ms.assetid: 2a49d417-25fb-4760-8ae5-5871bfb1e6f3
author: MashaMSFT
ms.author: mathoma
---
# Rename a SQL Server Failover Cluster Instance
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  When a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance is part of a failover cluster, the process of renaming the virtual server differs from that of renaming a stand-alone instance. For more information, see [Rename a Computer that Hosts a Stand-Alone Instance of SQL Server](../../../database-engine/install-windows/rename-a-computer-that-hosts-a-stand-alone-instance-of-sql-server.md).  
  
 The name of the virtual server is always the same as the name of the SQL Network Name (the SQL Virtual Server Network Name). Although you can change the name of the virtual server, you cannot change the instance name. For example, you can change a virtual server named VS1\instance1 to some other name, such as SQL35\instance1, but the instance portion of the name, instance1, will remain unchanged.  
  
 Before you begin the renaming process, review the items below.  
  
-   [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] does not support renaming servers involved in replication, except in the case of using log shipping with replication. The secondary server in log shipping can be renamed if the primary server is permanently lost. For more information, see [Log Shipping and Replication &#40;SQL Server&#41;](../../../database-engine/log-shipping/log-shipping-and-replication-sql-server.md).  
  
-   When renaming a virtual server that is configured to use database mirroring, you must turn off database mirroring before the renaming operation, and then re-establish database mirroring with the new virtual server name. Metadata for database mirroring will not be updated automatically to reflect the new virtual server name.  
  
### To rename a virtual server  
  
1.  Using Cluster Administrator, change the SQL Network Name to the new name.  
  
2.  Take the network name resource offline. This takes the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] resource and other dependent resources offline as well.  
  
3.  Bring the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] resource back online.  
  
## Verify the Renaming Operation  
 After a virtual server has been renamed, any connections that used the old name must now connect using the new name.  
  
 To verify that the renaming operation has completed, select information from either **@@servername** or **sys.servers**. The **@@servername** function will return the new virtual server name, and the **sys.servers** table will show the new virtual server name. To verify that the failover process is working correctly with the new name, the user should also try to fail the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] resource over to the other nodes.  
  
 For connections from any node in the cluster, the new name can be used almost immediately. However, for connections using the new name from a client computer, the new name cannot be used to connect to the server until the new name is visible to that client computer. The length of time required for the new name to be propagated across a network can be a few seconds, or as long as 3 to 5 minutes, depending on the network configuration; additional time may be required before the old virtual server name is no longer visible on the network.  
  
 To minimize network propagation delay of a virtual server renaming operation, use the following steps:  
  
#### To minimize network propagation delay  
  
1.  Issue the following commands from a command prompt on the server node:  
  
    ```  
    ipconfig /flushdns  
    ipconfig /registerdns  
    nbtstat -RR  
    ```  
  
## Additional considerations after the Renaming Operation  
 After we rename the network name of failover cluster we need to verify and perform the following instructions to enable all scenarios in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Agent and [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)].  
  
 **[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Agent Service:** Verify and perform the below additional actions for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Agent Service:  
  
-   Fix the registry settings if SQL Agent is configured for event forwarding. For more information, see [Designate an Events Forwarding Server &#40;SQL Server Management Studio&#41;](../../../ssms/agent/designate-an-events-forwarding-server-sql-server-management-studio.md).  
  
-   Fix the master server (MSX) and target servers (TSX) instance names when machines / cluster network name is renamed. For more information, see the following topics:  
  
    -   [Defect Multiple Target Servers from a Master Server](../../../ssms/agent/defect-multiple-target-servers-from-a-master-server.md)  
  
    -   [Create a Multiserver Environment](../../../ssms/agent/create-a-multiserver-environment.md)  
  
-   Reconfigure the Log Shipping so that updated server name is used to backup and restore logs. For more information, see the following topics:  
  
    -   [Configure Log Shipping &#40;SQL Server&#41;](../../../database-engine/log-shipping/configure-log-shipping-sql-server.md)  
  
    -   [Remove Log Shipping &#40;SQL Server&#41;](../../../database-engine/log-shipping/remove-log-shipping-sql-server.md)  
  
-   Update the Jobsteps that depend on server name. For more information, see [Manage Job Steps](../../../ssms/agent/manage-job-steps.md).  
  
## See Also  
 [Rename a Computer that Hosts a Stand-Alone Instance of SQL Server](../../../database-engine/install-windows/rename-a-computer-that-hosts-a-stand-alone-instance-of-sql-server.md)  
  
