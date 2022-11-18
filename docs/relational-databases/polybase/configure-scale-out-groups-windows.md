---
title: "Configure PolyBase scale-out groups on Windows"
description: Set up a PolyBase scale-out group to create a cluster of SQL Server instances. This improves query performance for large data sets from external sources.
ms.date: 02/22/2022
ms.service: sql
ms.subservice: polybase
ms.topic: "tutorial"
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: 
monikerRange: ">= sql-server-2016"
---
# Configure PolyBase scale-out groups on Windows

[!INCLUDE [SQL Server Windows Only - ASDBMI ](../../includes/applies-to-version/sql-windows-only-asdbmi.md)]

This article describes how to set up a [PolyBase scale-out group](polybase-scale-out-groups.md) on Windows. This creates a cluster of SQL Server instances to process large data sets from external data sources, such as Hadoop or Azure Blob Storage, in a scale-out fashion for better query performance.

[!INCLUDE[polybase-scaleout-banner-retirement](../../includes/polybase-scaleout-banner-retirement.md)]

## Prerequisites
  
- More than one machine in the same domain.  
  
- A domain user account to run PolyBase services. A group managed service account (gMSA) is recommended. For more information, see [Group Managed Service Accounts Overview](/windows-server/security/group-managed-service-accounts/group-managed-service-accounts-overview).
  
## Process overview

The following steps summarize the process for creating a PolyBase scale-out group. The next section provides a more detailed walk-through of each step.
  
1. Install the same version of SQL Server with PolyBase on N machines.
  
2. Select one SQL Server instance as the head node. 
  
3. Add remaining SQL Server instances as compute nodes using [sp_polybase_join_group](../../relational-databases/system-stored-procedures/polybase-stored-procedures-sp-polybase-join-group.md).

4. Monitor nodes in the group using [sys.dm_exec_compute_nodes &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-compute-nodes-transact-sql.md).

5. Optional. Remove a compute node from  using [sp_polybase_leave_group &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/polybase-stored-procedures-sp-polybase-leave-group.md).

## Example walk-through

This walks through the steps of configuring a PolyBase Group using:  
  
1. Two machines in the domain *PQTH4A* The machine names are:  
  
   - PQTH4A-CMP01  
  
   - PQTH4A-CMP02  
  
2. Domain account: *PQTH4A\PolyBaseUse*r  

## Install SQL Server with PolyBase on all machines

1. Run setup.exe.
  
2. On the Feature Selection page, select **PolyBase Query Service for External Data**.
  
3. On the Server Configuration page, use the **domain account** PQTH4A\PolyBaseUser for SQL Server PolyBase Engine and SQL Server PolyBase Data Movement Service.
  
4. On the PolyBase Configuration page, select the option **Use the SQL Server instance as part of a PolyBase scale-out group**. This opens the firewall to allow incoming connections to the PolyBase services. SQL Server installation wizard automatically exposes the following TCP ports in the Windows Server Firewall: 1433,16450-16453, and 17001. If the head node is a SQL Server named instance, you must also manually add the SQL Server port to the Windows firewall on the head node and also start the SQL Browser on the head node. Ports should be allowed only on the firewalls of servers in the PolyBase scale-out group.
  
5. After setup is complete, run **services.msc**. Verify that SQL Server, PolyBase Engine and PolyBase Data Movement Service are running.
  
   ![PolyBase services](../../relational-databases/polybase/media/polybase-services.png "PolyBase services")  
  
## Select one SQL Server as head node  
  
After setup is complete, both machines can function as PolyBase Group head nodes. In this example, we will choose "MSSQLSERVER" on PQTH4A-CMP01 as the head node.
  
## Add other SQL Server instances as compute nodes  
  
1. Connect to SQL Server on PQTH4A-CMP02.
  
2. Run the stored procedure [sp_polybase_join_group](../../relational-databases/system-stored-procedures/polybase-stored-procedures-sp-polybase-join-group.md).

   ```sql
   -- Enter head node details:
   -- head node machine name, head node dms control channel port, head node sql server name  
    EXEC sp_polybase_join_group 'PQTH4A-CMP01', 16450, 'MSSQLSERVER';
   ```  

3. Run services.msc on the compute node (PQTH4A-CMP02).
  
4. Shutdown the PolyBase engine and restart the PolyBase data movement service.

> [!NOTE] 
> When the PolyBase Engine service gets restarted or stopped in the head node, the Data Movement Service (DMS) services gets stopped as soon as the communication channel is closed between DMS and PolyBase Engine Service (DW). If the DW engine gets restarted more than two times, the DMS goes to a quiet period for 90 minutes and it must wait 90 minutes for the next auto start attempt. In such situation, you should start this service manually on all nodes.

## Optional: Remove a compute node  
  
1. Connect to the compute node SQL Server (PQTH4A-CMP02).
  
2. Run the stored procedure sp_polybase_leave_group.
  
    ```sql  
    EXEC sp_polybase_leave_group;  
    ```  
  
3. Run services.msc on the compute node that is being removed (PQTH4A-CMP02).
  
4. Start PolyBase Engine. Restart PolyBase data movement service.
  
5. Verify that the node has been removed by running the DMV sys.dm_exec_compute_nodes on PQTH4A-CMP01. Now, PQTH4A-CMP02 will function as a standalone head node  

## Known limitations
If you have a default SQL Server instance that is configured to listen on TCP port other than 1433, you cannot use it as a head node in a PolyBase scale-out group. When executing `sp_polybase_join_group`, if you pass 'MSSQLSERVER' as the instance name, SQL Server will assume port 1433 is the listener port, so the Data Movement service will be unable to connect to the head node when starting.

## Next steps  

For troubleshooting, see [PolyBase troubleshooting with dynamic management views](/previous-versions/sql/sql-server-2016/mt146389(v=sql.130)).
  
For more information about PolyBase, see the [PolyBase overview](../../relational-databases/polybase/polybase-guide.md).