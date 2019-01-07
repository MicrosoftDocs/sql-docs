---
title: "Analysis Services Thread Pool Properties | Microsoft Docs"
ms.date: 06/07/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: 
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Thread Pool Properties
[!INCLUDE[ssas-appliesto-sqlas-all-aas](../../includes/ssas-appliesto-sqlas-all-aas.md)]

  [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] uses multi-threading for many operations, improving overall server performance by running multiple jobs in parallel. To manage threads more efficiently, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] uses thread pools to preallocate threads and facilitate thread availability for the next job.  
  
 Each instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] maintains its own set of thread pools. There are differences in how tabular and multidimensional instances use thread pools. For example, only multidimensional instances use the **IOProcess** thread pool. As such, the **PerNumaNode** property, described in this article, is not meaningful for Tabular instances. In the [Property Reference](#bkmk_propref) section below, mode requirements are called out for each property.
  
> [!NOTE]  
>  Tabular deployment on NUMA systems is out of scope for this topic. Although tabular solutions can be successfully deployed on NUMA systems, the performance characteristics of the in-memory database technology used by tabular models may show limited benefits on a highly scaled up architectures. For more information, see [Analysis Services Case Study: Using Tabular Models in Large-scale Commercial Solutions](http://msdn.microsoft.com/library/dn751533.aspx) and [Hardware Sizing a Tabular Solution](http://go.microsoft.com/fwlink/?LinkId=330359).  
  
##  <a name="bkmk_threadarch"></a> Thread Management in Analysis Services  
 [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] uses multi-threading to take advantage of the available CPU resources by increasing the number of tasks executing in parallel. The storage engine is multi-threaded. Examples of multi-threaded jobs that execute within the storage engine include processing objects in parallel or handling discrete queries that have been pushed to the storage engine, or returning data values requested by a query. The formula engine, due to the serial nature of the calculations it evaluates, is single threaded. Each query executes primarily on a single thread, requesting and often waiting for data returned by the storage engine. Query threads have longer executions, and are released only after the entire query is completed.  
  
 By default, on versions [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] will use all available logical processors, up to 640 on systems running higher editions of Windows and SQL Server. Upon start up, the msmdsrv.exe process will be assigned to a specific processor group, but over time threads can be scheduled on any logical processor, in any processor group.  
  
 One side-effect of using a large number of processors is that you can sometimes experience performance degradation as query and processing loads are spread out across a large number of processors and contention for shared data structures increase. This can occur particularly on high-end systems that use NUMA architecture, but also on non-NUMA systems running multiple data intensive applications on the same hardware.  
  
 To alleviate this problem, you can set affinity between types of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] operations and a specific set of logical processors. The **GroupAffinity** property lets you create custom affinity masks that specify which system resource to use for each of the thread pool types managed by [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].
 
We recommend SQL Server 2016 Cumulative Update 1 (CU1) or later for setting **GroupAffinity** in tabular instances. 
  
 **GroupAffinity** is a property that can be set on any of the thread pools used for various [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] workloads:  
  
-   **ThreadPool \ Parsing \ Short**  is a parsing pool for short requests. Requests that fit within a single network message are considered short. 
  
-   **ThreadPool \ Parsing \ Long**  is a parsing pool for all other requests that do not fit within a single network message. 
  
    > [!NOTE]  
    >  A thread from either parsing pool can be used to execute a query. Queries that execute quickly, such as a fast Discover or Cancel request, are sometimes executed immediately rather than queued to the Query thread pool. 
  
-   **ThreadPool \ Query** is the thread pool that executes all requests that are not handled by the parsing thread pool. Threads in this thread pool will execute all types of operations, such as Discovers, MDX, DAX, DMX, and DDL commands. A
  
-   **ThreadPool \ IOProcess** is used for IO jobs associated with storage engine queries in the multidimensional engine. The work done by these threads is expected to not have dependencies on other threads. These threads will typically be scanning a single segment of a partition and performing filtering and aggregation on the segment data. **IOProcess** threads are particularly sensitive to NUMA hardware configurations. As such, this thread pool has the **PerNumaNode** configuration property which can be used to tune the performance if needed. 
  
-   **ThreadPool \ Process** is for longer duration storage engine jobs, including aggregations, indexing, and commit operations. ROLAP storage mode also uses threads from the Processing thread pool.  

- **VertiPaq \ ThreadPool** is the thread pool for executing table scans in a tabular model.
  
 To service requests, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] may exceed the maximum thread pool limit, requesting additional threads if they are necessary to perform the work. However, when a thread finishes executing its task, if the current count of threads is greater than the maximum limit, the thread is simply ended, rather than returned to the thread pool.  
  
> [!NOTE]  
>  Exceeding the maximum thread pool count is a protection invoked only when certain deadlock conditions arise. To prevent runaway thread creation beyond the maximum, threads are created gradually (after a short delay) after the maximum limit has been reached. Exceeding maximum thread count can lead to a slowdown in task execution. If the performance counters show the thread counts are regularly beyond the thread pool maximum size, you might consider that as an indicator that thread pool sizes are too small for the degree of concurrency being requested from the system.  
  
 By default, thread pool size is determined by [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], and is based on the number of cores. You can observe the selected default values by examining the msmdsrv.log file after server startup. As a performance tuning exercise, you might choose to increase the size of the thread pool, as well as other properties, to improve query or processing performance.  
  
##  <a name="bkmk_propref"></a> Thread Pool Property Reference  
 This section describes the thread pool properties found in the msmdsrv.ini file of each [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance. A subset of these properties also appears in SQL Server Management Studio.  
  
 Properties are listed in alphabetical order.  
  
|Name|Type|Description|Default|Guidance|  
|----------|----------|-----------------|-------------|--------------|  
|**IOProcess** \ **Concurrency**|double|A double-precision floating point value that determines the algorithm for setting a target on the number of threads that can be queued at one time.|2.0|An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.<br /><br /> Concurrency is used to initialize thread pools, which are implemented using IO Completion Ports in Windows. See [I/O Completion Ports](http://msdn.microsoft.com/library/windows/desktop/aa365198\(v=vs.85\).aspx) for details.<br /><br /> Applies to multidimensional models only.|  
|**IOProcess** \ **GroupAffinity**|string|An array of hexadecimal values that correspond to processor groups on the system, used to set affinity of threads in the IOProcess thread pool to logical processors in each processor group.|none|You can use this property to create custom affinities. The property is empty by default.<br /><br /> See [Set GroupAffinity to affinitize threads to processors in a processor group](#bkmk_groupaffinity) for details.<br /><br /> Applies to multidimensional models only.|  
|**IOProcess** \ **MaxThreads**|int|A signed 32-bit integer that specifies the maximum number of threads to include in the thread pool.|0|0 indicates the server determines the defaults. By default, the server either sets this value to 64, or to 10 times the number of logical processors, whichever is higher. For example, on a 4-core system with hyperthreading, the thread pool maximum is 80 threads.<br /><br /> If you set this value to a negative value, the server multiples that value by the number of logical processors. For example, when set to -10 on a server having 32 logical processors, the maximum is 320 threads.<br /><br /> The maximum value is subject to available processors per any custom affinity masks that you previously defined. For example, if you already set thread pool affinity to use 8 out of 32 processors, and you now set MaxThreads to -10, then the upper bound on the thread pool would 10 times 8, or 80 threads.<br /><br /> The actual values used for this thread pool property are written to the msmdsrv log file upon service start up.<br /><br /> More information about tuning the thread pool settings can be found in the [Analysis Services Operations Guide](http://msdn.microsoft.com/library/hh226085.aspx).<br /><br /> Applies to multidimensional models only.|  
|**IOProcess** \ **MinThreads**|int|A signed 32-bit integer that specifies the minimum number of threads to preallocate for the thread pool.|0|0 indicates the server determines the defaults. By default, the minimum is 1.<br /><br /> If you set this value to a negative value, the server multiples that value by the number of logical processors.<br /><br /> The actual values used for this thread pool property are written to the msmdsrv log file upon service start up.<br /><br /> More information about tuning the thread pool settings can be found in the [Analysis Services Operations Guide](http://msdn.microsoft.com/library/hh226085.aspx).<br /><br /> Applies to multidimensional models only.|  
|**IOProcess** \ **PerNumaNode**|int|A signed 32-bit integer that determines the number of thread pools created for the msmdsrv process.|-1|Valid values are -1, 0, 1, 2<br /><br /> -1 = The server selects a different IO Thread Pool strategy based on the number of NUMA nodes. On systems having fewer than 4 NUMA nodes, the server behavior is the same as 0 (one IOProcess thread pool is created for the system). On systems having 4 or more nodes, the behavior is the same as 1 (IOProcess thread pools are created for each node).<br /><br /> 0 = Disables per NUMA node thread pools so that there is only one IOProcess thread pool used by the msmdsrv.exe process.<br /><br /> 1 = Enables one IOProcess thread pool per NUMA node.<br /><br /> 2 = One IOProcess thread pool per logical processor. Threads in each thread pool are affinitized to the NUMA node of the logical processor, with the ideal processor set to the logical processor.<br /><br /> See [Set PerNumaNode to affinitize IO threads to processors in a NUMA node](#bkmk_pernumanode) for details.<br /><br /> Applies to multidimensional models only.|  
|**IOProcess** \ **PriorityRatio**|int|A signed 32-bit integer that can be used to ensure that lower priority threads are sometimes executed even when a higher priority queue is not empty.|2|An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.<br /><br /> Applies to multidimensional models only.|  
|**IOProcess** \ **StackSizeKB**|int|A signed 32-bit integer that can be used to adjust memory allocation during thread execution.|0|An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.<br /><br /> Applies to multidimensional models only.|  
|**Parsing**  \ **Long** \ **Concurrency**|double|A double-precision floating point value that determines the algorithm for setting a target on the number of threads that can be queued at one time.|2.0|An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.<br /><br /> Concurrency is used to initialize thread pools, which are implemented using IO Completion Ports in Windows. See [I/O Completion Ports](http://msdn.microsoft.com/library/windows/desktop/aa365198\(v=vs.85\).aspx) for details.|  
|**Parsing**  \ **Long** \ **GroupAffinity**|string|An array of hexadecimal values that correspond to processor groups on the system, used to set affinity of parsing threads to logical processors in each processor group.|none|You can use this property to create custom affinities. The property is empty by default.<br /><br /> See [Set GroupAffinity to affinitize threads to processors in a processor group](#bkmk_groupaffinity) for details.|  
|**Parsing**  \ **Long** \ **NumThreads**|int|A signed 32-bit integer property that defines the number of threads that can be created for long commands.|0|0 indicates that the server determines the defaults. The default behavior is to set **NumThreads** to an absolute value of 4, or 2 times the number of logical processors, whichever is higher.<br /><br /> If you set this value to a negative value, the server multiples that value by the number of logical processors. For example, when set to -10 on a server having 32 logical processors, the maximum is 320 threads.<br /><br /> The maximum value is subject to available processors per any custom affinity masks that you previously defined. For example, if you already set thread pool affinity to use 8 out of 32 processors, and you now set NumThreads to -10, then the upper bound on the thread pool would 10 times 8, or 80 threads.<br /><br /> The actual values used for this thread pool property are written to the msmdsrv log file upon service start up.|  
|**Parsing**  \ **Long** \ **PriorityRatio**|int|A signed 32-bit integer that can be used to ensure that lower priority threads are sometimes executed even when a higher priority queue is not empty.|0|An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.|  
|**Parsing**  \ **Long** \ **StackSizeKB**|int|A signed 32-bit integer that can be used to adjust memory allocation during thread execution.|0|An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.|  
|**Parsing**  \ **Short** \ **Concurrency**|double|A double-precision floating point value that determines the algorithm for setting a target on the number of threads that can be queued at one time.|2.0|An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.<br /><br /> Concurrency is used to initialize thread pools, which are implemented using IO Completion Ports in Windows. See [I/O Completion Ports](http://msdn.microsoft.com/library/windows/desktop/aa365198\(v=vs.85\).aspx) for details.|  
|**Parsing**  \ **Short** \ **GroupAffinity**|string|An array of hexadecimal values that correspond to processor groups on the system, used to set affinity of parsing threads to logical processors in each processor group.|none|You can use this property to create custom affinities. The property is empty by default.<br /><br /> See [Set GroupAffinity to affinitize threads to processors in a processor group](#bkmk_groupaffinity) for details.|  
|**Parsing**  \ **Short** \ **NumThreads**|int|A signed 32-bit integer property that defines the number of threads that can be created for short commands.|0|0 indicates that the server determines the defaults. The default behavior is to set **NumThreads** to an absolute value of 4, or 2 times the number of logical processors, whichever is higher.<br /><br /> If you set this value to a negative value, the server multiples that value by the number of logical processors. For example, when set to -10 on a server having 32 logical processors, the maximum is 320 threads.<br /><br /> The maximum value is subject to available processors per any custom affinity masks that you previously defined. For example, if you already set thread pool affinity to use 8 out of 32 processors, and you now set NumThreads to -10, then the upper bound on the thread pool would 10 times 8, or 80 threads.<br /><br /> The actual values used for this thread pool property are written to the msmdsrv log file upon service start up.|  
|**Parsing**  \ **Short** \ **PriorityRatio**|int|A signed 32-bit integer that can be used to ensure that lower priority threads are sometimes executed even when a higher priority queue is not empty.|0|An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.|  
|**Parsing**  \ **Short** \ **StackSizeKB**|int|A signed 32-bit integer that can be used to adjust memory allocation during thread execution.|64 * logical processors|An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.|  
|**Process** \ **Concurrency**|double|A double-precision floating point value that determines the algorithm for setting a target on the number of threads that can be queued at one time.|2.0|An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.<br /><br /> Concurrency is used to initialize thread pools, which are implemented using IO Completion Ports in Windows. See [I/O Completion Ports](http://msdn.microsoft.com/library/windows/desktop/aa365198\(v=vs.85\).aspx) for details.|  
|**Process** \ **GroupAffinity**|string|An array of hexadecimal values that correspond to processor groups on the system, used to set affinity of processing threads to logical processors in each processor group.|none|You can use this property to create custom affinities. The property is empty by default.<br /><br /> See [Set GroupAffinity to affinitize threads to processors in a processor group](#bkmk_groupaffinity) for details.|  
|**Process** \ **MaxThreads**|int|A signed 32-bit integer that specifies the maximum number of threads to include in the thread pool.|0|0 indicates the server determines the defaults. By default, the server either sets this value to an absolute value of 64, or the number of logical processors, whichever is higher. For example, on a 64-core system with hyperthreading enabled (resulting in 128 logical processors), the thread pool maximum is 128 threads.<br /><br /> If you set this value to a negative value, the server multiples that value by the number of logical processors. For example, when set to -10 on a server having 32 logical processors, the maximum is 320 threads.<br /><br /> The maximum value is subject to available processors per any custom affinity masks that you previously defined. For example, if you already set thread pool affinity to use 8 out of 32 processors, and you now set MaxThreads to -10, then the upper bound on the thread pool would 10 times 8, or 80 threads.<br /><br /> The actual values used for this thread pool property are written to the msmdsrv log file upon service start up.<br /><br /> More information about tuning the thread pool settings can be found in the [Analysis Services Operations Guide](http://msdn.microsoft.com/library/hh226085.aspx).|  
|**Process** \ **MinThreads**|int|A signed 32-bit integer that specifies the minimum number of threads to preallocate for the thread pool.|0|0 indicates the server determines the defaults. By default, the minimum is 1.<br /><br /> If you set this value to a negative value, the server multiples that value by the number of logical processors.<br /><br /> The actual values used for this thread pool property are written to the msmdsrv log file upon service start up.<br /><br /> More information about tuning the thread pool settings can be found in the [Analysis Services Operations Guide](http://msdn.microsoft.com/library/hh226085.aspx).|  
|**Process** \ **PriorityRatio**|int|A signed 32-bit integer that can be used to ensure that lower priority threads are sometimes executed even when a higher priority queue is not empty.|2|An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.|  
|**Process** \ **StackSizeKB**|int|A signed 32-bit integer that can be used to adjust memory allocation during thread execution.|0|An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.|  
|**Query**  \ **Concurrency**|double|A double-precision floating point value that determines the algorithm for setting a target on the number of threads that can be queued at one time.|2.0|An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.<br /><br /> Concurrency is used to initialize thread pools, which are implemented using IO Completion Ports in Windows. See [I/O Completion Ports](http://msdn.microsoft.com/library/windows/desktop/aa365198\(v=vs.85\).aspx) for details.|  
|**Query** \ **GroupAffinity**|string|An array of hexadecimal values that correspond to processor groups on the system, used to set affinity of processing threads to logical processors in each processor group.|none|You can use this property to create custom affinities. The property is empty by default.<br /><br /> See [Set GroupAffinity to affinitize threads to processors in a processor group](#bkmk_groupaffinity) for details.|  
|**Query**  \ **MaxThreads**|int|A signed 32-bit integer that specifies the maximum number of threads to include in the thread pool.|0|0 indicates the server determines the defaults. By default, the server either sets this value to an absolute value of 10, or 2 times the number of logical processors, whichever is higher. For example, on a 4-core system with hyperthreading, the maximum thread count is 16.<br /><br /> If you set this value to a negative value, the server multiples that value by the number of logical processors. For example, when set to -10 on a server having 32 logical processors, the maximum is 320 threads.<br /><br /> The maximum value is subject to available processors per any custom affinity masks that you previously defined. For example, if you already set thread pool affinity to use 8 out of 32 processors, and you now set MaxThreads to -10, then the upper bound on the thread pool would 10 times 8, or 80 threads.<br /><br /> The actual values used for this thread pool property are written to the msmdsrv log file upon service start up.<br /><br /> More information about tuning the thread pool settings can be found in the [Analysis Services Operations Guide](http://msdn.microsoft.com/library/hh226085.aspx).|  
|**Query** \ **MinThreads**|int|A signed 32-bit integer that specifies the minimum number of threads to preallocate for the thread pool.|0|0 indicates the server determines the defaults. By default, the minimum is 1.<br /><br /> If you set this value to a negative value, the server multiples that value by the number of logical processors.<br /><br /> The actual values used for this thread pool property are written to the msmdsrv log file upon service start up.<br /><br /> More information about tuning the thread pool settings can be found in the [Analysis Services Operations Guide](http://msdn.microsoft.com/library/hh226085.aspx).|  
|**Query** \ **PriorityRatio**|int|A signed 32-bit integer that can be used to ensure that lower priority threads are sometimes executed even when a higher priority queue is not empty.|2|An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.|  
|**Query**  \ **StackSizeKB**|int|A signed 32-bit integer that can be used to adjust memory allocation during thread execution.|0|An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.|  
|**VertiPaq** \ **CPUs**|int|A signed 32-bit integer that specifies the maximum number of processors to use for tabular queries.|0|0 indicates the server determines the defaults. By default, the server either sets this value to an absolute value of 10, or 2 times the number of logical processors, whichever is higher. For example, on a 4-core system with hyperthreading, the maximum thread count is 16.<br /><br /> If you set this value to a negative value, the server multiples that value by the number of logical processors. For example, when set to -10 on a server having 32 logical processors, the maximum is 320 threads.<br /><br /> The maximum value is subject to available processors per any custom affinity masks that you previously defined. For example, if you already set thread pool affinity to use 8 out of 32 processors, and you now set MaxThreads to -10, then the upper bound on the thread pool would 10 times 8, or 80 threads.<br /><br /> The actual values used for this thread pool property are written to the msmdsrv log file upon service start up.|  
  |**VertiPaq** \ **GroupAffinity**|string|An array of hexadecimal values that correspond to processor groups on the system, used to set affinity of processing threads to logical processors in each processor group.|none|You can use this property to create custom affinities. The property is empty by default.<br /><br /> See [Set GroupAffinity to affinitize threads to processors in a processor group](#bkmk_groupaffinity) for details. Applies to Tabular only.| 
    
##  <a name="bkmk_groupaffinity"></a> Set GroupAffinity to affinitize threads to processors in a processor group  
 **GroupAffinity** is provided for advanced tuning purposes. You can use the **GroupAffinity** property to set affinity between [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] thread pools and specific processors; however, for most installations, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] performs best when it can use all available logical processors. Accordingly, group affinity is unspecified by default.  
  
 Should performance testing indicate a need for CPU optimization, you might consider a higher level approach, such as using Windows Server Resource Manager to set affinity between logical processors and a server process. Such an approach can be simpler to implement and manage than defining custom affinities for individual thread pools.  
  
 If that approach is insufficient, you can achieve greater precision by defining custom affinities for thread pools. Customizing affinity settings is more likely to be recommended on large multi-core systems (either NUMA or non-NUMA) experiencing performance degradation due to thread pools spread out over too-wide a range of processors. Although you can set **GroupAffinity** on systems having fewer than 64 logical processors, the benefit is negligible and might even degrade performance.  
  
> [!NOTE]  
>  **GroupAffinity** is constrained by editions that limit the number of cores used by [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. At startup, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] uses edition information and the **GroupAffinity** properties to compute affinity masks for each thread pool managed by [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. Standard edition can use a maximum of 24 cores. If you install [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] standard edition on a large multi-core system that has more than 24 cores, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] will only use 24 of them. For more information about processor maximums, see the cross-box scale limits in [Features by editions in SQL Server](https://msdn.microsoft.com/library/cc645993.aspx).  
  
### Syntax  
 The value is hexadecimal for each processor group, with the hexadecimal representing the logical processors that [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] attempts to use first when allocating threads for a given thread pool.  
  
 **Bitmask for Logical Processors**  
  
 You can have up to 64 logical processors within a single processor group. The bitmask is 1 (or 0) for each logical processor in the group that is used (or not used) by a thread pool. Once you compute the bitmask, you then calculate the hexadecimal value as the value for **GroupAffinity**.  
  
 **Multiple processor groups**  
  
 Processor groups are determined on system startup. **GroupAffinity** accepts hexadecimal values for each processor group in a comma delimited list. Given multiple processor groups (up to 10 on higher end systems), you can bypass individual groups by specifying 0x0. For example, on a system with four processor groups (0, 1, 2, 3), you could exclude groups 0 and 2 by entering 0x0 for the first and third values.  
  
 `<GroupAffinity>0x0, 0xFF, 0x0, 0xFF</GroupAffinity>`  
  
### Steps for computing the processor affinity mask  
 You can set **GroupAffinity** in msmdsrv.ini or in server property pages in SQL Server Management Studio.  
  
1.  **Determine the number of processors and processor groups**  
  
     You can download [Coreinfo utility from winsysinternals](http://technet.microsoft.com/sysinternals/cc835722.aspx).  
  
     Run **coreinfo** to get this information from the Logical Processor to Group Map section. A separate line is generated for each logical processor.  
  
2.  Sequence the processors, from right to left: `7654 3210`  
  
     The example shows only 8 processors (0 through 7), but a processor group can have a maximum of 64 logical processors, and there can be up to 10 processor groups in an enterprise-class Windows server.  
  
3.  **Compute the bitmask for the processor groups you want to use**  
  
     `7654 3210`  
  
     Replace the number with a 0 or 1, depending on whether you want to exclude or include the logical processor. On a system having eight processors, your calculation might look like this if you wanted to use processors 7, 6, 5, 4, and 1 for Analysis Services:  
  
     `1111 0010`  
  
4.  **Convert the binary number to a Hex value**  
  
     Using a calculator or conversion tool, convert the binary number to its hexadecimal equivalent. In our example, `1111 0010` converts to `0xF2`.  
  
5.  **Enter the hex value in the GroupAffinity property**  
  
     In msmdsrv.ini or in the server property page in Management Studio, set **GroupAffinity** to the value calculated in step 4.  
  
> [!IMPORTANT]  
>  Setting **GroupAffinity** is a manual task encompassing multiple steps. When computing **GroupAffinity**, check your calculations carefully. Although [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] will return an error if the entire mask is invalid, a combination of valid and invalid settings results in [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] ignoring the property. For example, if the bitmask includes extra values, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] ignores the setting, using all processors on the system. There is no error or warning to alert you when this action occurs, but you can check the msmdsrv.log file to learn how the affinities are actually set.  
  
##  <a name="bkmk_pernumanode"></a> Set PerNumaNode to affinitize IO threads to processors in a NUMA node  
 For multidimensional Analysis Services instances, you can set **PerNumaNode** on the **IOProcess** thread pool to further optimize thread scheduling and execution. Whereas **GroupAffinity** identifies which set of logical processors to use for a given thread pool, **PerNumaNode** goes one step further by specifying whether to create multiple thread pools, further affinitized to some subset of the allowed logical processors.  
  
> [!NOTE]  
>  On Windows Server 2012, use Task Manager to view the number of NUMA nodes on the computer. In Task Manager, on the Performance tab, select **CPU** and then right-click the graph area to view NUMA nodes. Alternatively, [download](http://technet.microsoft.com/sysinternals/cc835722.aspx) the Coreinfo utility from Windows Sysinternals and run `coreinfo -n` to return NUMA nodes and logical processors in each node.  
  
 Valid values for **PerNumaNode** are -1, 0, 1, 2, as described in the [Thread Pool Property Reference](#bkmk_propref) section in this topic.  
  
### Default (Recommended)  
 On systems having NUMA nodes, we recommend using the default setting of PerNumaNode=-1, allowing [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] to adjust the number of thread pools and their thread affinity based on node count. If the system has fewer than 4 nodes, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] implements the behaviors described by **PerNumaNode**=0, whereas **PerNumaNode**=1 is used on systems having 4 or more nodes.  
  
### Choosing a value  
 You can also override the default to use another valid value.  
  
 **Setting PerNumaNode=0**  
  
 NUMA nodes are ignored. There will be just one IOProcess thread pool, and all threads in that thread pool will be affinitized to all logical processors. By default (where PerNumaNode=-1), this is the operative setting if the computer has fewer than 4 NUMA nodes.  
  
 ![Numa, processor and thread pool correspondance](../../analysis-services/server-properties/media/ssas-threadpool-numaex0.PNG "Numa, processor and thread pool correspondance")  
  
 **Setting PerNumaNode=1**  
  
 IOProcess thread pools are created for each NUMA node. Having separate thread pools improves coordinated access to local resources, such as local cache on a NUMA node.  
  
 ![Numa, processor and thread pool correspondance](../../analysis-services/server-properties/media/ssas-threadpool-numaex1.PNG "Numa, processor and thread pool correspondance")  
  
 **Setting PerNumaNode=2**  
  
 This setting is for very high-end systems running intensive [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] workloads. This property sets IOProcess thread pool affinity at its most granular level, creating and affinitizing separate thread pools at the logical processor level.  
  
 In the following example, on a system having 4 NUMA nodes and 32 logical processors, setting **PerNumaNode** to 2 would result in 32 IOProcess thread pools. The threads in the first 8 thread pools would be affinitized to all the logical processors in the NUMA node 0, but with the ideal processor set to 0, 1, 2, up to 7. The next 8 thread pools would be affinitized to all the logical processors in NUMA node 1, with the ideal processor set to 8, 9, 10, up to 15, and so on.  
  
 ![Numa, processor and thread pool correspondance](../../analysis-services/server-properties/media/ssas-threadpool-numaex2.PNG "Numa, processor and thread pool correspondance")  
  
 At this level of affinity, the scheduler always attempts to use the ideal logical processor first, within the preferred NUMA node. If the logical processor is unavailable, the scheduler chooses another processor within the same node, or within the same processor group if no other threads are available. For more information and examples, see [Analysis Services 2012 Configuration settings (Wordpress Blog)](http://go.microsoft.com/fwlink/?LinkId=330387).  
  
###  <a name="bkmk_workdistrib"></a> Work distribution among IOProcess threads  
 As you consider whether to set the **PerNumaNode** property, knowing how **IOProcess** threads are used can help you make a more informed decision.  
  
 Recall that **IOProcess** is used for IO jobs associated with storage engine queries in the multidimensional engine.  
  
 When a segment is scanned, the engine identifies the partition to which the segment belongs, and attempts to queue the segment job to the thread pool used by the partition. In general, all segments belonging to a partition will queue their tasks to the same thread pool. On NUMA systems, this behavior is particularly advantageous because all scans for a partition will use memory in the file system cache that is allocated locally to that NUMA node.  
  
 The following scenarios suggest adjustments that can sometimes improve query performance on NUMA systems:  
  
-   For measure groups that are under-partitioned (for example, having a single partition), increase the number of partitions. Using just one partition will cause the engine to always queue tasks to one thread pool (thread pool 0). Adding more partitions allows the engine to use additional thread pools.  
  
     Alternatively, if you cannot create additional partitions, try setting **PerNumaNode**=0 as a way to increase the number of threads available to thread pool 0.  
  
-   For databases in which segment scans are evenly distributed across multiple partitions, setting **PerNumaNode** to 1 or 2 can improve query performance because it increases the overall number of **IOProcess** thread pools used by the system.  
  
-   For solutions that have multiple partitions, but only one is heavily scanned, try setting **PerNumaNode**=0 to see if it improves performance.  
  
 Although both partition and dimension scans use the **IOProcess** thread pool, dimension scans only use thread pool 0. This can result in a slightly uneven load on that thread pool, but the imbalance should be temporary, as dimension scans tend to be very fast and infrequent.  
  
> [!NOTE]  
>  When changing a server property, remember that the configuration option applies to all databases running on the current instance. Choose settings that benefit the most important databases, or the greatest number of databases. You cannot set processor affinity at the database level, nor can you set affinity between individual partitions and specific processors.  
  
 For more information about job architecture, see section 2.2 in [SQL Server Analysis Services Performance Guide](http://www.microsoft.com/download/details.aspx?id=17303).  
  
##  <a name="bkmk_related"></a> Dependent or Related Properties  
 As explained in section 2.4 of the [Analysis Services Operations Guide](http://msdn.microsoft.com/library/hh226085.aspx), if you increase the processing thread pool, you should make sure that the **CoordinatorExecutionMode** settings, as well as the **CoordinatorQueryMaxThreads** settings, have values that enable you to make full use of the increased thread pool size.  
  
 Analysis Services uses a coordinator thread for gathering the data needed to complete a processing or query request. The coordinator first queues up one job for each partition that must be touched. Each of those jobs then continues to queue up more jobs, depending on the total number of segments that must be scanned in the partition.  
  
 The default value for **CoordinatorExecutionMode** is -4, meaning a limit of 4 jobs in parallel per core, which constrains the total number of coordinator jobs that can be executed in parallel by a subcube request in the storage engine.  
  
 The default value for **CoordinatorQueryMaxThreads** is 16, which limits the number of segment jobs that can be executed in parallel for each partition.  
  
##  <a name="bkmk_currentsettings"></a> Determine current thread pool settings  
 At each service startup, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] outputs the current thread pool settings into the msmdsrv.log file, including minimum and maximum threads, processor affinity mask, and concurrency.  
  
 The following example is an excerpt from the log file, showing the default settings for the Query thread pool (MinThread=0, MaxThread=0, Concurrency=2), on a 4-core system with hyper-threading enabled. The affinity mask is 0xFF, indicating 8 logical processors. Notice that leading zeros are prepended to the mask. You can ignore the leading zeros.  
  
 `"10/28/2013 9:20:52 AM) Message: The Query thread pool now has 1 minimum threads, 16 maximum threads, and a concurrency of 16.  Its thread pool affinity mask is 0x00000000000000ff. (Source: \\?\C:\Program Files\Microsoft SQL Server\MSAS11.MSSQLSERVER\OLAP\Log\msmdsrv.log, Type: 1, Category: 289, Event ID: 0x4121000A)"`  
  
 Recall that the algorithm for setting **MinThread** and **MaxThread** incorporates system configuration, particularly the number of processors. The following blog post offers insights into how the values are calculated: [Analysis Services 2012 Configuration settings (Wordpress Blog)](http://go.microsoft.com/fwlink/?LinkId=330387). Note that these settings and behaviors are subject to adjustment in subsequent releases.  
  
 The following list shows examples of other affinity mask settings, for different combinations of processors:  
  
-   Affinity for processors 3-2-1-0 on an 8 core system results in this bitmask: 00001111, and a hexadecimal value: 0xF  
  
-   Affinity for processors 7-6-5-4 on an 8 core system results in this bitmask: 11110000, and a hexadecimal value: 0xF0  
  
-   Affinity for processors 5-4-3-2 on an 8 core system results in this bitmask: 00111100, and a hexadecimal value: 0x3C  
  
-   Affinity for processors 7-6-1-0 on an 8 core system results in this bitmask: 11000011, and a hexadecimal value: 0xC3  
  
 Recall that on systems having multiple processor groups, a separate affinity mask is generated for each group, in a comma separated list.  
  
##  <a name="bkmk_msmdrsrvini"></a> About MSMDSRV.INI  
 The msmdsrv.ini file contains configuration settings for an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance, affecting all databases running on that instance. You cannot use server configuration properties to optimize performance of just one database to the exclusion of all others. However, you can install multiple instances of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] and configure each instance to use properties that benefit databases sharing similar characteristics or workloads.  
  
 All server configuration properties are included in the msmdsrv.ini file. Subsets of the properties more likely to be modified also appear in administration tools, such as SSMS.  
  
 The contents of msmdsrv.ini are identical for both Tabular and Multidimensional instances of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. However, some settings apply to one mode only. Differences in behavior based on server mode are noted in property reference documentation.  
  
> [!NOTE]  
>  For instructions on how to set properties, see [Server Properties in Analysis Services](../../analysis-services/server-properties/server-properties-in-analysis-services.md).  
  
## See Also  
 [About Processes and Threads](/windows/desktop/ProcThread/about-processes-and-threads)   
 [Multiple Processors](/windows/desktop/ProcThread/multiple-processors)   
 [Processor Groups](/windows/desktop/ProcThread/processor-groups)   
 [Analysis Services Thread Pool Changes in SQL Server 2012](http://blogs.msdn.com/b/psssql/archive/2012/01/31/analysis-services-thread-pool-changes-in-sql-server-2012.aspx)   
 [Analysis Services 2012 Configuration settings (Wordpress Blog)](http://go.microsoft.com/fwlink/?LinkId=330387)   
 [Supporting Systems That Have More Than 64 Processors](http://msdn.microsoft.com/library/windows/hardware/gg463349.aspx)   
 [SQL Server Analysis Services Operations Guide](http://go.microsoft.com/fwlink/?LinkID=225539)  
  
  
