---
title: "PolyBase scale-out groups | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "05/24/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine-polybase"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "PolyBase"
  - "PolyBase, scale-out groups"
  - "scale-out PolyBase"
ms.assetid: c7810135-4d63-4161-93ab-0e75e9d10ab5
caps.latest.revision: 20
author: "barbkess"
ms.author: "barbkess"
manager: "jhubbard"
---
# PolyBase scale-out groups
[!INCLUDE[tsql-appliesto-ss2016-xxxx-xxxx-xxx_md](../../includes/tsql-appliesto-ss2016-xxxx-xxxx-xxx-md.md)]

  A standalone SQL Server instance with PolyBase can become a performance bottleneck when dealing with massive data sets in Hadoop or Azure Blob Storage. The PolyBase Group feature allows you to create a cluster of SQL Server instances to process large data sets from external data sources, such as Hadoop or Azure Blob Storage, in a scale-out fashion for better query performance.  
  
 See [Get started with PolyBase](../../relational-databases/polybase/get-started-with-polybase.md) and [PolyBase Guide](../../relational-databases/polybase/polybase-guide.md).  
  
 ![PolyBase scale-out groups](../../relational-databases/polybase/media/polybase-scale-out-groups.png "PolyBase scale-out groups")  
  
## Overview  
  
### Head node  
 The head node contains the SQL Server instance to which PolyBase queries are submitted. Each PolyBase group can have only one head node. A head node is a logical group of SQL Database Engine, PolyBase Engine and PolyBase Data Movement Service on the SQL Server instance.  
  
### Compute node  
 A compute node contains the SQL Server instance that assists with scale-out query processing on external data. A compute node is a logical group of SQL Server and the PolyBase data movement service on the SQL Server instance. A PolyBase group can have multiple compute nodes.  
  
### Distributed query processing  
 PolyBase queries are submitted to the SQL Server on the head node. The part of the query that refers to external tables is handed-off to the PolyBase engine.  
  
 The PolyBase engine is the key component  behind PolyBase queries. It parses the query on external data, generates the query plan and distributes the work to the data movement service on the compute nodes for execution. After completion of the work, it receives the results from the compute nodes and submits them to SQL Server for processing and returning to the client.  
  
 The PolyBase data movement service receives instructions from the PolyBase engine and transfers data between HDFS and SQL Server, and between SQL Server instances on the head and compute nodes.  
  
### Editions availability  
 After setup of SQL Server, the instance can be designated as either a head node or a compute node.  The choice depends on which version of SQL Server PolyBase is running on. On an Enterprise edition installation, the instance can be designated either as head node or a compute node. On a Standard edition, the instance can only be designated as a compute node.  
  
## To configure a PolyBase group  
  
### Prerequisites  
  
-   N machines in the same domain  
  
-   A domain user account to run PolyBase services  
  
### Steps  
  
1.  Install SQL Server with PolyBase on N machines.  
  
2.  Select one SQL Server instance as the head node. A head node can only be designated on an instance running SQL Server Enterprise.  
  
3.  Add remaining SQL Server instances as compute nodes using [sp_polybase_join_group](../../relational-databases/system-stored-procedures/polybase-stored-procedures-sp-polybase-join-group.md).  
  
4.  Monitor nodes in the group using [sys.dm_exec_compute_nodes &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-compute-nodes-transact-sql.md).  
  
5.  Optional. Remove a compute node from  using [sp_polybase_leave_group &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/polybase-stored-procedures-sp-polybase-leave-group.md).  
  
## Example walk-through  
 This walks through the steps of configuring a PolyBase Group using:  
  
1.  Two machines in the domain *PQTH4A* The machine names are:  
  
    -   PQTH4A-CMP01  
  
    -   PQTH4A-CMP02  
  
2.  Domain account: *PQTH4A\PolybaseUse*r  
  
#### Step 1: Install SQL Server with PolyBase on all machines  
  
1.  Run setup.exe.  
  
2.  On the the Feature Selection page, select **PolyBase Query Service for External Data**.  
  
3.  On the the Server Configuration page, use the **domain account** PQTH4A\PolybaseUser for SQL Server PolyBase Engine and SQL Server PolyBase Data Movement Service.  
  
4.  On the PolyBase Configuration page, select the option **Use the SQL Server instance as part of a PolyBase scale-out group**. This opens  the firewall  to allow incoming connections to the PolyBase services.  
  
5.  After setup is complete, run **services.msc**. Verify that SQL Server, PolyBase Engine and PolyBase Data Movement Service are running.  
  
     ![PolyBase services](../../relational-databases/polybase/media/polybase-services.png "PolyBase services")  
  
#### Step 2: Select one SQL Server as head node  
  
-   After setup is complete, both machines can function as PolyBase Group head nodes. In this example, we will choose “MSSQLSERVER” on PQTH4A-CMP01 as the head node.  
  
#### Step 3: Add other SQL Server instances as compute nodes  
  
1.  Connect to SQL Server on PQTH4A-CMP02.  
  
2.  Run the stored procedure [sp_polybase_join_group](../../relational-databases/system-stored-procedures/polybase-stored-procedures-sp-polybase-join-group.md).  
  
    ```  
    -- Enter head node details:   
    -- head node machine name, head node dms control channel port, head node sql server name  
    EXEC sp_polybase_join_group 'PQTH4A-CMP01', 16450, 'MSSQLSERVER';  
  
    ```  
  
3.  Run services.msc on the compute node (PQTH4A-CMP02).  
  
4.  Shutdown the PolyBase engine and restart the PolyBase data movement service.  
  
#### Optional: Remove a compute node  
  
1.  Connect to the compute node SQL Server (PQTH4A-CMP02).  
  
2.  Run the stored procedure sp_polybase_leave_group.  
  
    ```  
    EXEC sp_polybase_leave_group;  
    ```  
  
3.  Run services.msc on the compute node that is being removed (PQTH4A-CMP02).  
  
4.  Start PolyBase Engine. Restart PolyBase data movement service.  
  
5.  Verify that the node has been removed by running the DMV sys.dm_exec_compute_nodes on PQTH4A-CMP01. Now, PQTH4A-CMP02 will function as a standalone head node  
  
## Next steps  
 For troubleshooting, see [PolyBase troubleshooting with dynamic management views](http://msdn.microsoft.com/library/ce9078b7-a750-4f47-b23e-90b83b783d80).  
  
## See Also  
 [Get started with PolyBase](../../relational-databases/polybase/get-started-with-polybase.md)   
 [PolyBase Guide](../../relational-databases/polybase/polybase-guide.md)   
 [PolyBase Connectivity Configuration &#40;Transact-SQL&#41;](../../database-engine/configure-windows/polybase-connectivity-configuration-transact-sql.md)  
  
  
