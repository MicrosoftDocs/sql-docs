---
title: "Soft-NUMA (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "02/13/2018"
ms.prod: sql
ms.prod_service: high-availability
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
f1_keywords: 
  - "NUMA"
  - "soft-NUMA"
helpviewer_keywords: 
  - "NUMA"
  - "non-uniform memory access"
  - "soft-NUMA"
ms.assetid: 1af22188-e08b-4c80-a27e-4ae6ed9ff969
author: "CarlRabeler"
ms.author: "carlrab"
manager: craigg
---
# Soft-NUMA (SQL Server)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

Modern processors have multiple cores per socket. Each socket is represented, usually, as a single NUMA node. The SQL Server database engine partitions various internal structures and partitions service threads per NUMA node.  With processors containing 10 or more cores per socket, using software NUMA to split hardware NUMA nodes generally increases scalability and performance. Prior to [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] SP2, software-based NUMA (soft-NUMA) required you to edit the registry to add a node configuration affinity mask, and was configured at the host level, rather than per instance. Starting with [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] SP2 and [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)], soft-NUMA is configured automatically at the database-instance level when the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] service starts.  
  
> [!NOTE]  
> Hot-add processors are not supported by soft-NUMA.  
  
## Automatic Soft-NUMA  
With [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)], whenever the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]  detects more than eight physical cores per NUMA node or socket at startup, soft-NUMA nodes are created automatically by default. Hyper-threaded processor cores are not differentiated when counting physical cores in a node.  When the detected number of physical cores is more than eight per socket, the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] creates soft-NUMA nodes that ideally contain eight cores, but can go down to five or up to nine logical cores per node. The size of the hardware node can be limited by a CPU affinity mask. The number of NUMA nodes never exceeds the maximum number of supported NUMA nodes.  
  
You can disable or re-enable soft-NUMA using the [ALTER SERVER CONFIGURATION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-server-configuration-transact-sql.md) statement with the `SET SOFTNUMA` argument. Changing the value of this setting requires a restart of the database engine to take effect.  
  
The figure below shows the type of information regarding soft-NUMA that you see in the SQL Server error log, when [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] detects hardware NUMA nodes with greater than eight physical cores per each node or socket.  


```
2016-11-14 13:39:43.17 Server      SQL Server detected 2 sockets with 12 cores per socket and 24 logical processors per socket, 48 total logical processors; using 48 logical processors based on SQL Server licensing. This is an informational message; no user action is required.     
2016-11-14 13:39:43.35 Server      Automatic soft-NUMA was enabled because SQL Server has detected hardware NUMA nodes with greater than 8 physical cores.     
2016-11-14 13:39:43.63 Server      Node configuration: node 0: CPU mask: 0x0000000000555555:0 Active CPU mask: 0x0000000000555555:0. This message provides a description of the NUMA configuration for this computer. This is an informational message only. No user action is required.    
2016-11-14 13:39:43.63 Server      Node configuration: node 1: CPU mask: 0x0000000000aaaaaa:0 Active CPU mask: 0x0000000000aaaaaa:0. This message provides a description of the NUMA configuration for this computer. This is an informational message only. No user action is required.    
2016-11-14 13:39:43.63 Server      Node configuration: node 2: CPU mask: 0x0000555555000000:0 Active CPU mask: 0x0000555555000000:0. This message provides a description of the NUMA configuration for this computer. This is an informational message only. No user action is required.     
2016-11-14 13:39:43.63 Server      Node configuration: node 3: CPU mask: 0x0000aaaaaa000000:0 Active CPU mask: 0x0000aaaaaa000000:0. This message provides a description of the NUMA configuration for this computer. This is an informational message only. No user action is required.   
```   

> [!NOTE]
> Starting with [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] SP2, use trace flag 8079 to allow [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to use Automatic Soft-NUMA. Starting with  [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] this behavior is controlled by the engine and trace flag 8079 has no effect. For more information, see [DBCC TRACEON - Trace Flags](../../sql/t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md).

## Manual Soft-NUMA  
To manually configure [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to use soft-NUMA, disable automatic soft-NUMA, and edit the registry to add a node configuration affinity mask. When using this method, the soft-NUMA mask can be stated as a binary, DWORD (hexadecimal or decimal), or QWORD (hexadecimal or decimal) registry entry. To configure more than the first 32 CPUs use QWORD or BINARY registry values (QWORD values cannot be used prior to [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]). After modifying the registry, you must restart the [!INCLUDE[ssDE](../../includes/ssde-md.md)] for the soft-NUMA configuration to take effect.  
  
> [!TIP]
> CPUs are numbered starting with 0.  

> [!WARNING]
> [!INCLUDE[ssNoteRegistry](../../includes/ssnoteregistry-md.md)]  
  
Consider the example of a computer with eight CPUs, that does not have hardware NUMA. Three soft-NUMA nodes are configured.   
[!INCLUDE[ssDE](../../includes/ssde-md.md)] instance A is configured to use CPUs 0 through 3. A second instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] is installed and configured to use CPUs 4 through 7. The example can be visually represented as:  
  
 `CPUs          0  1  2  3  4  5  6  7`  
  
 `Soft-NUMA   <-N0--><-N1-><----N2---->`  
  
 `SQL Server  <instance A ><instance B>`  
  
 Instance A, which experiences significant I/O, now has two I/O threads and one lazy writer thread. Instance B, which performs processor-intensive operations, has only one I/O thread and one lazy writer thread. Differing amounts of memory can be assigned to the instances, but unlike hardware NUMA, they both receive memory from the same operating system memory block, and there is no memory-to-processor affinity.  
  
 The lazy writer thread is tied to the SQLOS view of the physical NUMA memory nodes. Therefore, whatever the hardware presents as the number of physical NUMA nodes, this will be the number of lazy writer threads that are created. For more information, see [How It Works: Soft NUMA, I/O Completion Thread, Lazy Writer Workers and Memory Nodes](https://blogs.msdn.com/b/psssql/archive/2010/04/02/how-it-works-soft-numa-i-o-completion-thread-lazy-writer-workers-and-memory-nodes.aspx).  
  
> [!NOTE]
> The **Soft-NUMA** registry keys are not copied when you upgrade an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
### Set the CPU affinity mask  
 Run the following statement on instance A to configure it to use CPUs 0, 1, 2, and 3 by setting the CPU affinity mask:  
  
```sql  
ALTER SERVER CONFIGURATION   
SET PROCESS AFFINITY CPU=0 TO 3;  
```  
  
Run the following statement on instance B to configure it to use CPUs 4, 5, 6, and 7 by setting the CPU affinity mask:  
  
```sql  
ALTER SERVER CONFIGURATION   
SET PROCESS AFFINITY CPU=4 TO 7;  
```  
  
### Map soft-NUMA nodes to CPUs  
 Using the Registry Editor program (regedit.exe), add the following registry keys to map soft-NUMA node 0 to CPUs 0 and 1, soft-NUMA node 1 to CPUs 2 and 3, and soft-NUMA node 2 to CPUs 4, 5, 6, and 7.  
  
> [!TIP]
> To specify CPUs 60 through 63, use a QWORD value of F000000000000000 or a BINARY value of 1111000000000000000000000000000000000000000000000000000000000000.  
  
 In the following example, assume you have a DL580 G9 server, with 18 cores per socket (in four sockets), and each socket is in its own K-group. A soft-NUMA configuration that you might create would look something like the following: six cores per Node, three nodes per group, four groups.  
  
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
 You can use the following DMVs to view the current state and configuration of soft-NUMA.  
  
-   [sp_configure &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md): Displays the current value (0 or 1) for SOFTNUMA  
  
-   [sys.dm_os_sys_info &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-sys-info-transact-sql.md): The *softnuma* and *softnuma_desc* columns displays the current configuration values.  
  
> [!NOTE]
> While you can view the running value for automatic soft-NUMA using [sp_configure &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md), you cannot change its value using **sp_configure**. You must use the [ALTER SERVER CONFIGURATION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-server-configuration-transact-sql.md) statement with the `SET SOFTNUMA` argument.  
  
## See Also  
[Map TCP IP Ports to NUMA Nodes &#40;SQL Server&#41;](../../database-engine/configure-windows/map-tcp-ip-ports-to-numa-nodes-sql-server.md)    
[affinity mask Server Configuration Option](../../database-engine/configure-windows/affinity-mask-server-configuration-option.md)    
[ALTER SERVER CONFIGURATION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-server-configuration-transact-sql.md)     
[sys.dm_os_nodes &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-nodes-transact-sql.md)   
  
