---
title: "Improve PolyBase scale-out groups on Windows | Microsoft Docs"
ms.date: 09/24/2018
ms.prod: sql
ms.reviewer: ""
ms.custom: ""
ms.technology: polybase
ms.topic: "tutorial"
author: rothja
ms.author: jroth
manager: craigg
---
# Improve PolyBase scale-out groups on Windows

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This article describes how to setup a [PolyBase scale-out group](polybase-scale-out-groups.md) on Windows. This creates a cluster of SQL Server instances to process large data sets from external data sources, such as Hadoop or Azure Blob Storage, in a scale-out fashion for better query performance.

## Prerequisites
  
- More than one machine in the same domain  
  
- A domain user account to run PolyBase services  
  
## Process overview

The following steps summarize the process for creating a PolyBase scale-out group. The next section provides a more detailed walk-through of each step.
  
1. Install the same version of SQL Server with PolyBase on N machines.
  
2. Select one SQL Server instance as the head node. A head node can only be designated on an instance running SQL Server Enterprise.
  
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
  
4. On the PolyBase Configuration page, select the option **Use the SQL Server instance as part of a PolyBase scale-out group**. This opens  the firewall  to allow incoming connections to the PolyBase services.
  
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
  
## Optional: Remove a compute node  
  
1. Connect to the compute node SQL Server (PQTH4A-CMP02).
  
2. Run the stored procedure sp_polybase_leave_group.
  
    ```sql  
    EXEC sp_polybase_leave_group;  
    ```  
  
3. Run services.msc on the compute node that is being removed (PQTH4A-CMP02).
  
4. Start PolyBase Engine. Restart PolyBase data movement service.
  
5. Verify that the node has been removed by running the DMV sys.dm_exec_compute_nodes on PQTH4A-CMP01. Now, PQTH4A-CMP02 will function as a standalone head node  
  
## Next steps  

For troubleshooting, see [PolyBase troubleshooting with dynamic management views](https://msdn.microsoft.com/library/ce9078b7-a750-4f47-b23e-90b83b783d80).
  
For more information about PolyBase, see the [PolyBase overview](../../relational-databases/polybase/polybase-guide.md).
