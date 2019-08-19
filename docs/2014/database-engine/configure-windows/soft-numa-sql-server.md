---
title: "Configure SQL Server to Use Soft-NUMA (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "07/12/2016"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "NUMA"
  - "non-uniform memory access"
ms.assetid: 1af22188-e08b-4c80-a27e-4ae6ed9ff969
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# Configure SQL Server to Use Soft-NUMA (SQL Server)
Modern processors have multiple to many cores per socket. Each socket is represented, usually, as a single NUMA node. The SQL Server database engine partitions various internal structures and partitions service threads per NUMA node. With processors containing 10 or more cores per socket, using software NUMA (soft-NUMA) to split hardware NUMA nodes generally increases scalability and performance.   

> [!NOTE]
> Hot-add processors are not supported by soft-NUMA.
  
## Automatic Soft-NUMA
Starting with SQL Server 2014 Service Pack 2, whenever the database engine server detects more than 8 physical processors at startup, soft-NUMA nodes are created automatically if trace flag 8079 is enabled as a startup parameter. Hyper-threaded processor cores are not accounted for when counting physical processors. When the number of physical processors detected is more than 8 per socket, the database engine service will create soft-NUMA nodes that ideally contain 8 cores, but can go down to 5 or up to 9 logical processors per node. The size of the hardware node can be limited by a CPU affinity mask. The number of NUMA nodes will never exceed the maximum number of supported NUMA nodes.

Without the trace flag, soft-NUMA is disabled by default. You can enable soft-NUMA using trace flag 8079. Changing the value of this setting requires a restart of the database engine to take effect.

The figure below shows the type of information regarding soft-NUMA that you will see in the SQL Server error log when SQL Server detects hardware NUMA nodes with greater than 8 logical processors and if trace flag 8079 is enabled.

![Soft-NUMA](./media/soft-numa-sql-server/soft-numa.PNG)

> [!NOTE]
> Starting with SQL Server 2016 this behavior is controlled by the engine and trace flag 8079 has no effect.

## Manual Soft-NUMA
  
To configure [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to use soft-NUMA manually, you must edit the registry to add a node configuration affinity mask. The soft-NUMA mask can be stated as a binary, DWORD (hexadecimal or decimal), or QWORD (hexadecimal or decimal) registry entry. To configure more than the first 32 CPUs use QWORD or BINARY registry values. (QWORD values cannot be used prior to [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)].) You must restart the [!INCLUDE[ssDE](../../includes/ssde-md.md)] to configure soft-NUMA.  
  
> [!TIP]  
>  CPUs are numbered starting with 0.  
  
 [!INCLUDE[ssNoteRegistry](../../includes/ssnoteregistry-md.md)]  
  
 Consider the following example. A computer with eight CPUs does not have hardware NUMA. Three soft-NUMA nodes are configured. [!INCLUDE[ssDE](../../includes/ssde-md.md)] instance A is configured to use CPUs 0 through 3. A second instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] is installed and configured to use CPUs 4 through 7. The example can be visually represented as:  
  
 `CPUs          0  1  2  3  4  5  6  7`  
  
 `Soft-NUMA   <-N0--><-N1-><----N2---->`  
  
 `SQL Server  <instance A ><instance B>`  
  
 Instance A, which experiences significant I/O, now has two I/O threads and one lazy writer thread, while instance B, which performs processor-intensive operations, has only one I/O thread and one lazy writer thread. Differing amounts of memory can be assigned to the instances, but unlike hardware NUMA, they both receive memory from the same operating system memory block and there is no memory-to-processor affinity.  
  
 The lazy writer thread is tied to the SQL OS view of the physical NUMA memory nodes. Therefore, whatever the hardware presents as physical NUMA nodes will equate to the number of lazy writer threads that are created. For more information, see [How It Works: Soft NUMA, I/O Completion Thread, Lazy Writer Workers and Memory Nodes](https://blogs.msdn.com/b/psssql/archive/2010/04/02/how-it-works-soft-numa-i-o-completion-thread-lazy-writer-workers-and-memory-nodes.aspx).  
  
> [!NOTE]  
>  The **Soft-NUMA** registry keys are not copied when you upgrade an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
### Set the CPU affinity mask  
  
1.  Run the following statement on instance A to configure it to use CPUs 0, 1, 2, and 3 by setting the CPU affinity mask:  
  
    ```  
    ALTER SERVER CONFIGURATION   
    SET PROCESS AFFINITY CPU=0 TO 3;  
    ```  
  
2.  Run the following statement on instance B to configure it to use CPUs 4, 5, 6, and 7 by setting the CPU affinity mask:  
  
    ```  
    ALTER SERVER CONFIGURATION   
    SET PROCESS AFFINITY CPU=4 TO 7;  
    ```  
  
### Map soft-NUMA nodes to CPUs  
  
-   Using the Registry Editor program (regedit.exe), add the following registry keys to map soft-NUMA node 0 to CPUs 0 and 1, soft-NUMA node 1 to CPUs 2 and 3, and soft-NUMA node 2 to CPUs 4. 5, 6, and 7.  
  
     In the following example, assume you have a DL580 G9 server, with 18 cores per socket (in 4 sockets), and each socket is in its own K-group. A soft-numa configuration that you might create would look something like following. (6 cores per Node, 3 nodes per group, 4 groups).  
  
    |Example for a [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] server with multiple K-Groups|Type|Value name|Value data|  
    |------------------------------------------------------------------------|----------|----------------|----------------|  
    |HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\120\NodeConfiguration\Node0|DWORD|CPUMask|0x3F|  
    |HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\120\NodeConfiguration\Node0|DWORD|Group|0|  
    |HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\120\NodeConfiguration\Node1|DWORD|CPUMask|0x0fc0|  
    |HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\120\NodeConfiguration\Node1|DWORD|Group|0|  
    |HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\120\NodeConfiguration\Node2|DWORD|CPUMask|0x3f000|  
    |HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\120\NodeConfiguration\Node2|DWORD|Group|0|  
    |HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\120\NodeConfiguration\Node3|DWORD|CPUMask|0x3F|  
    |HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\120\NodeConfiguration\Node3|DWORD|Group|1|  
    |HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\120\NodeConfiguration\Node4|DWORD|CPUMask|0x0fc0|  
    |HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\120\NodeConfiguration\Node4|DWORD|Group|1|  
    |HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\120\NodeConfiguration\Node5|DWORD|CPUMask|0x3f000|  
    |HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\120\NodeConfiguration\Node5|DWORD|Group|1|  
    |HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\120\NodeConfiguration\Node6|DWORD|CPUMask|0x3F|  
    |HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\120\NodeConfiguration\Node6|DWORD|Group|2|  
    |HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\120\NodeConfiguration\Node7|DWORD|CPUMask|0x0fc0|  
    |HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\120\NodeConfiguration\Node7|DWORD|Group|2|  
    |HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\120\NodeConfiguration\Node8|DWORD|CPUMask|0x3f000|  
    |HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\120\NodeConfiguration\Node8|DWORD|Group|2|  
    |HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\120\NodeConfiguration\Node9|DWORD|CPUMask|0x3F|  
    |HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\120\NodeConfiguration\Node9|DWORD|Group|3|  
    |HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\120\NodeConfiguration\Node10|DWORD|CPUMask|0x0fc0|  
    |HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\120\NodeConfiguration\Node10|DWORD|Group|3|  
    |HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\120\NodeConfiguration\Node11|DWORD|CPUMask|0x3f000|  
    |HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\120\NodeConfiguration\Node11|DWORD|Group|3|  
  
     Additional examples:  
  
    |[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]|Type|Value name|Value data|  
    |---------------------------|----------|----------------|----------------|  
    |HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\120\NodeConfiguration\Node0|DWORD|CPUMask|0x03|  
    |HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\120\NodeConfiguration\Node0|DWORD|Group|0|  
    |HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\120\NodeConfiguration\Node1|DWORD|CPUMask|0x0c|  
    |HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\120\NodeConfiguration\Node1|DWORD|Group|0|  
    |HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\120\NodeConfiguration\Node2|DWORD|CPUMask|0xf0|  
    |HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\120\NodeConfiguration\Node2|DWORD|Group|0|  
  
    > [!TIP]  
    >  To specify CPUs 60 through 63, use a QWORD value of F000000000000000 or a BINARY value of 1111000000000000000000000000000000000000000000000000000000000000.  
  
    |[!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]|Type|Value name|Value data|  
    |---------------------------|----------|----------------|----------------|  
    |HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\110\NodeConfiguration\Node0|DWORD|CPUMask|0x03|  
    |HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\110\NodeConfiguration\Node0|DWORD|Group|0|  
    |HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\110\NodeConfiguration\Node1|DWORD|CPUMask|0x0c|  
    |HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\110\NodeConfiguration\Node1|DWORD|Group|0|  
    |HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\110\NodeConfiguration\Node2|DWORD|CPUMask|0xf0|  
    |HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\110\NodeConfiguration\Node2|DWORD|Group|0|  
  
    |SQL Server 2008 R2|Type|Value name|Value data|  
    |------------------------|----------|----------------|----------------|  
    |HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\100\NodeConfiguration\Node0|DWORD|CPUMask|0x03|  
    |HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\100\NodeConfiguration\Node0|DWORD|Group|0|  
    |HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\100\NodeConfiguration\Node1|DWORD|CPUMask|0x0c|  
    |HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\100\NodeConfiguration\Node1|DWORD|Group|0|  
    |HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\100\NodeConfiguration\Node2|DWORD|CPUMask|0xf0|  
    |HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\100\NodeConfiguration\Node2|DWORD|Group|0|  
  
    |SQL Server 2008|Type|Value name|Value data|  
    |---------------------|----------|----------------|----------------|  
    |HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\100\NodeConfiguration\Node0|DWORD|CPUMask|0x03|  
    |HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\100\NodeConfiguration\Node1|DWORD|CPUMask|0x0c|  
    |HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\100\NodeConfiguration\Node2|DWORD|CPUMask|0xf0|  
  
    |SQL Server 2005|Type|Value name|Value data|  
    |---------------------|----------|----------------|----------------|  
    |HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\90\NodeConfiguration\Node0|DWORD|CPUMask|0x03|  
    |HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\90\NodeConfiguration\Node1|DWORD|CPUMask|0x0c|  
    |HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\90\NodeConfiguration\Node2|DWORD|CPUMask|0xf0|  
  
## See Also  
 [Map TCP IP Ports to NUMA Nodes &#40;SQL Server&#41;](map-tcp-ip-ports-to-numa-nodes-sql-server.md)   
 [affinity mask Server Configuration Option](affinity-mask-server-configuration-option.md)   
 [ALTER SERVER CONFIGURATION &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-server-configuration-transact-sql)  
  
  
