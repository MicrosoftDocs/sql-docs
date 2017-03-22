---
title: "Soft-NUMA (SQL Server) | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "11/16/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "NUMA"
  - "soft-NUMA"
helpviewer_keywords: 
  - "NUMA"
  - "non-uniform memory access"
  - "soft-NUMA"
ms.assetid: 1af22188-e08b-4c80-a27e-4ae6ed9ff969
caps.latest.revision: 53
author: "CarlRabeler"
ms.author: "carlrab"
manager: "jhubbard"
---
# Soft-NUMA (SQL Server)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Modern processors have multiple to many cores per socket. Each socket is represented, usually, as a single NUMA node. The SQL Server database engine partitions various internal structures and partitions service threads per NUMA node.  With processors containing 10 or more cores per socket, using software NUMA to split hardware NUMA nodes generally increases scalability and performance. Prior to SQL Server 2014 SP2, software-based NUMA (soft-NUMA) required you to edit the registry to add a node configuration affinity mask and was configured per computer rather than per instance.  With SQL Server 2014 SP2 and SQL Server 2016, soft-NUMA is configured automatically at the database-instance level when the SQL Server service starts.  
  
> [!NOTE]  
>  Hot-add processors are not supported by soft-NUMA.  
  
## Automatic Soft-NUMA  
 With SQL Server 2016, whenever the database engine server detects more than 8 physical cores per NUMA node or socket at startup, soft-NUMA nodes are created automatically by default. Hyper-threaded processor cores are not differentiated when counting physical cores in a node.  When the number of physical cores detected is more than 8 per socket, the database engine service will create soft-NUMA nodes that ideally contain 8 cores, but can go down to 5 or up to 9 logical cores per node. The size of the hardware node can be limited by a CPU affinity mask. See   
            [ALTER SERVER CONFIGURATION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-server-configuration-transact-sql.md). The number of NUMA nodes will never exceed the maximum number of supported NUMA nodes.  
  
 You can disable or re-enable soft-NUMA using the [ALTER SERVER CONFIGURATION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-server-configuration-transact-sql.md) statement with the SET SOFTNUMA argument. Changing the value of this setting requires a restart of the database engine to take effect.  
  
 The figure below shows the type of information regarding soft-NUMA that you will see in the SQL Server error log when SQL Server detects hardware NUMA nodes with greater than 8 physical cores in each node or socket.  


``2016-11-14 13:39:43.17 Server      SQL Server detected 2 sockets with 12 cores per socket and 24 logical processors per socket, 48 total logical processors; using 48 logical processors based on SQL Server licensing. This is an informational message; no user action is required.``  
  **``2016-11-14 13:39:43.35 Server      Automatic soft-NUMA was enabled because SQL Server has detected hardware NUMA nodes with greater than 8 physical cores.``**  
  ``2016-11-14 13:39:43.63 Server      Node configuration: node 0: CPU mask: 0x0000000000555555:0 Active CPU mask: 0x0000000000555555:0. This message provides a description of the NUMA configuration for this computer. This is an informational message only. No user action is required.``  
  ``2016-11-14 13:39:43.63 Server      Node configuration: node 1: CPU mask: 0x0000000000aaaaaa:0 Active CPU mask: 0x0000000000aaaaaa:0. This message provides a description of the NUMA configuration for this computer. This is an informational message only. No user action is required.``  
  ``2016-11-14 13:39:43.63 Server      Node configuration: node 2: CPU mask: 0x0000555555000000:0 Active CPU mask: 0x0000555555000000:0. This message provides a description of the NUMA configuration for this computer. This is an informational message only. No user action is required.``  
  ``2016-11-14 13:39:43.63 Server      Node configuration: node 3: CPU mask: 0x0000aaaaaa000000:0 Active CPU mask: 0x0000aaaaaa000000:0. This message provides a description of the NUMA configuration for this computer. This is an informational message only. No user action is required.``

  
## Manual Soft-NUMA  
 To manually configure [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to use soft-NUMA by disabling automatic soft_NUMA and editing the registry to add a node configuration affinity mask. When using this method, the soft-NUMA mask can be stated as a binary, DWORD (hexadecimal or decimal), or QWORD (hexadecimal or decimal) registry entry. To configure more than the first 32 CPUs use QWORD or BINARY registry values. (QWORD values cannot be used prior to [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)].) After modifying the registry, you must restart the  [!INCLUDE[ssDE](../../includes/ssde-md.md)] for the soft-NUMA configuration to take  effect.  
  
> [!TIP]
> CPUs are numbered starting with 0.  

> [!WARNING]
> [!INCLUDE[ssNoteRegistry](../../includes/ssnoteregistry-md.md)]  
  
 Consider the following example. A computer with eight CPUs does not have hardware NUMA. Three soft-NUMA nodes are configured.   
            [!INCLUDE[ssDE](../../includes/ssde-md.md)] instance A is configured to use CPUs 0 through 3. A second instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] is installed and configured to use CPUs 4 through 7. The example can be visually represented as:  
  
 `CPUs          0  1  2  3  4  5  6  7`  
  
 `Soft-NUMA   <-N0--><-N1-><----N2---->`  
  
 `SQL Server  <instance A ><instance B>`  
  
 Instance A, which experiences significant I/O, now has two I/O threads and one lazy writer thread, while instance B, which performs processor-intensive operations, has only one I/O thread and one lazy writer thread. Differing amounts of memory can be assigned to the instances, but unlike hardware NUMA, they both receive memory from the same operating system memory block and there is no memory-to-processor affinity.  
  
 The lazy writer thread is tied to the SQL OS view of the physical NUMA memory nodes. Therefore, whatever the hardware presents as physical NUMA nodes will equate to the number of lazy writer threads that are created. For more information, see   
            [How It Works: Soft NUMA, I/O Completion Thread, Lazy Writer Workers and Memory Nodes](http://blogs.msdn.com/b/psssql/archive/2010/04/02/how-it-works-soft-numa-i-o-completion-thread-lazy-writer-workers-and-memory-nodes.aspx).  
  
> [!NOTE]
> The **Soft-NUMA** registry keys are not copied when you upgrade an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
### Set the CPU affinity mask  
 Run the following statement on instance A to configure it to use CPUs 0, 1, 2, and 3 by setting the CPU affinity mask:  
  
```  
ALTER SERVER CONFIGURATION   
SET PROCESS AFFINITY CPU=0 TO 3;  
```  
  
 Run the following statement on instance B to configure it to use CPUs 4, 5, 6, and 7 by setting the CPU affinity mask:  
  
```  
ALTER SERVER CONFIGURATION   
SET PROCESS AFFINITY CPU=4 TO 7;  
```  
  
### Map soft-NUMA nodes to CPUs  
 Using the Registry Editor program (regedit.exe), add the following registry keys to map soft-NUMA node 0 to CPUs 0 and 1, soft-NUMA node 1 to CPUs 2 and 3, and soft-NUMA node 2 to CPUs 4. 5, 6, and 7.  
  
> [!TIP]
> To specify CPUs 60 through 63, use a QWORD value of F000000000000000 or a BINARY value of 1111000000000000000000000000000000000000000000000000000000000000.  
  
 In the following example, assume you have a DL580 G9 server, with 18 cores per socket (in 4 sockets), and each socket is in its own K-group. A soft-numa configuration that you might create would look something like following. (6 cores per Node, 3 nodes per group, 4 groups).  
  
|Example for a [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] server with multiple K-Groups|Type|Value name|Value data|  
|-----------------------------------------------------------------------------------------------------------------|----------|----------------|----------------|  
|HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\130\NodeConfiguration\Node0|DWORD|CPUMask|0x3F|  
|HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\130\NodeConfiguration\Node0|DWORD|Group|0|  
|HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\130\NodeConfiguration\Node1|DWORD|CPUMask|0x0fc0|  
|HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\130\NodeConfiguration\Node1|DWORD|Group|0|  
|HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\130\NodeConfiguration\Node2|DWORD|CPUMask|0x3f000|  
|HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\130\NodeConfiguration\Node2|DWORD|Group|0|  
|HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\130\NodeConfiguration\Node3|DWORD|CPUMask|0x3F|  
|HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\130\NodeConfiguration\Node3|DWORD|Group|1|  
|HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\130\NodeConfiguration\Node4|DWORD|CPUMask|0x0fc0|  
|HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\130\NodeConfiguration\Node4|DWORD|Group|1|  
|HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\130\NodeConfiguration\Node5|DWORD|CPUMask|0x3f000|  
|HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\130\NodeConfiguration\Node5|DWORD|Group|1|  
|HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\130\NodeConfiguration\Node6|DWORD|CPUMask|0x3F|  
|HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\130\NodeConfiguration\Node6|DWORD|Group|2|  
|HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\130\NodeConfiguration\Node7|DWORD|CPUMask|0x0fc0|  
|HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\130\NodeConfiguration\Node7|DWORD|Group|2|  
|HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\130\NodeConfiguration\Node8|DWORD|CPUMask|0x3f000|  
|HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\130\NodeConfiguration\Node8|DWORD|Group|2|  
|HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\130\NodeConfiguration\Node9|DWORD|CPUMask|0x3F|  
|HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\130\NodeConfiguration\Node9|DWORD|Group|3|  
|HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\130\NodeConfiguration\Node10|DWORD|CPUMask|0x0fc0|  
|HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\130\NodeConfiguration\Node10|DWORD|Group|3|  
|HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\130\NodeConfiguration\Node11|DWORD|CPUMask|0x3f000|  
|HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\130\NodeConfiguration\Node11|DWORD|Group|3|  
  
## Metadata  
 You can use the following DMVs to view the current state and configuration of soft_NUMA.  
  
-   [sp_configure &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md): Displays the current value (0 or 1) for SOFTNUMA  
  
-   [sys.dm_os_nodes &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-nodes-transact-sql.md): The node_state_desc column for each NUMA node will show whether the node was created by soft-NUMA or not.  
  
-   [sys.dm_os_sys_info &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-sys-info-transact-sql.md): The softnuma and softnuma_desc columns will show the configuration values.  
  
> [!NOTE]
> While you can view the running value for automatic soft-NUMA using [sp_configure &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md), you cannot change its value using **sp_configure**. You must use the [ALTER SERVER CONFIGURATION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-server-configuration-transact-sql.md) statement with the SET SOFTNUMA argument.  
  
## See Also  
 [Map TCP IP Ports to NUMA Nodes &#40;SQL Server&#41;](../../database-engine/configure-windows/map-tcp-ip-ports-to-numa-nodes-sql-server.md)   
 [affinity mask Server Configuration Option](../../database-engine/configure-windows/affinity-mask-server-configuration-option.md)   
 [ALTER SERVER CONFIGURATION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-server-configuration-transact-sql.md)  
  
  

