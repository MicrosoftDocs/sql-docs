---
title: "Performance Counters (SSAS) | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom:
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Performance Counters (SSAS)
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  Using Performance Monitor, you can monitor the performance of a Microsoft SQL Server Analysis Services (SSAS) instance by using performance counters.  
  
 Performance Monitor is a [!INCLUDE[msCoName](../../includes/msconame-md.md)] Management Control (MMC) snap-in that tracks resource usage. You can start this MMC snap-in by typing in **PerfMon** at the command prompt or from Control Panel by clicking **Administrative Tools**, then **Performance Monitor**. Performance Monitor lets you track server and process performance and activity by using predefined objects and counters, and monitor events by using user-defined counters. Performance Monitor collects counts instead of data about the events, for example, memory usage, number of active transactions, or CPU activity. You can also set thresholds on specific counters to generate alerts that notify operators.  
  
 Performance Monitor can monitor remote and local instances of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [Using Performance Monitor](http://technet.microsoft.com/library/cc749115.aspx).  
  
 To see the description of any counter that can be used with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], in Performance Monitor, open the **Add Counters** dialog box, select a performance object, and then click **Show Description**. The most important counters are CPU usage, memory usage, disk IO rate. It is recommended you start with these important counters, and move to more detailed counters when you have a better idea of what else could be improved through monitoring. For more information about which counters to include, see the [SQL Server 2008 R2 Operations Guide](http://go.microsoft.com/fwlink/?LinkID=225539).  
  
 Counters are grouped so you can more easily find related counters.  
  
## Counters by Groups  
  
|Group|Description|  
|-----------|-----------------|  
|[Cache](#bkmk_Cache)|Statistics related to the Analysis Services aggregation cache.|  
|[Connection](#bkmk_Connection)|Statistics related to Microsoft Analysis Services connections.|  
|[Data Mining Prediction](#bkmk_DataMiningPrediction)|Statistics related to processing data mining models processing.|  
|[Data Mining Model Processing](#bkmk_DataMiningModelProcessing)|Statistics related to creating predictions from data mining models.|  
|[Locks](#bkmk_Locks)|Statistics related to Microsoft Analysis Services internal server locks.|  
|[MDX](#bkmk_MDX)|Statistics related to Microsoft Analysis Services MDX calculations.|  
|[Memory](#bkmk_Memory)|Statistics related to Microsoft Analysis Services internal server memory.|  
|[Proactive Caching](#bkmk_ProactiveCaching)|Statistics related to Microsoft Analysis Services Proactive Caching.|  
|[Processing Aggregations](#bkmk_ProcAggregations)|Statistics related to processing of aggregations in MOLAP data files.|  
|[Processing Indexes](#bkmk_ProcIndexes)|Statistics related to processing of indexes for MOLAP data files.|  
|[Processing](#bkmk_Processing)|Statistics related to processing of data.|  
|[Storage Engine Query](#bkmk_StorageEngineQuery)|Statistics related to Microsoft Analysis Services storage engine queries.|  
|[Threads](#bkmk_Threads)|Statistics related to Microsoft Analysis Services threads.|  
  
###  <a name="bkmk_Cache"></a> Cache  
 Statistics related to the Microsoft Analysis Services aggregation cache.  
  
|Counter|Description|  
|-------------|-----------------|  
|Current KB|Current memory used by the aggregation cache, in KB.|  
|KB added/sec|Rate of memory added to the cache, KB/sec.|  
|Current entries|Current number of cache entries.|  
|Inserts/sec|Rate of insertions into the cache.  The rate is tracked per partition per cube per database.|  
|Evictions/sec|Rate of evictions from the cache.  This is per partition per cube per database.  Evictions are typically due to background cleaner.|  
|Total inserts|Insertions into the cache.  The rate is tracked per partition per cube per database.|  
|Total evictions|Evictions from the cache.  Evictions are tracked per partition per cube per database.  Evictions are typically due to background cleaner.|  
|Direct hits/sec|Rate of cache direct hits. A cache hit indicates that queries were answered from an existing cache entry.|  
|Misses/sec|Rate of cache misses.|  
|Lookups/sec|Rate of cache lookups.|  
|Total direct hits|Total count of direct cache hits.  A direct cache hit indicates that queries were answered from existing cache entries.|  
|Total misses|Total count of cache misses.|  
|Total lookups|Total number of lookups into the cache.|  
|Direct hit ratio|Ratio of cache direct hits to cache lookups, for the period between counter values.|  
|Total filtered iterator cache hits|Total number of cache hits that returned an indexed iterator over the filtered results.|  
|Total filtered iterator cache misses|Total number of cache hits that were unable to build an indexed iterator over the filtered results and had to build a new cache with the filtered results.|  
  
###  <a name="bkmk_Connection"></a> Connection  
 Statistics related to Microsoft Analysis Services connections.  
  
|Counter|Description|  
|-------------|-----------------|  
|Current connections|Current number of client connections established.|  
|Requests/sec|Rate of connection requests.  These are arrivals.|  
|Total requests|Total connection requests.  These are arrivals.|  
|Successes/sec|Rate of successful connection completions.|  
|Total successes|Total successful connections.|  
|Failures/sec|Rate of connection failures.|  
|Total failures|Total failed connection attempts.|  
|Current user sessions|Current number of user sessions established.|  
  
###  <a name="bkmk_DataMiningModelProcessing"></a> Data Mining Model Processing  
 Statistics related to Microsoft Analysis Services Data Mining model processing.  
  
|Counter|Description|  
|-------------|-----------------|  
|Cases/sec|Rate at which cases are processed.|  
|Current models processing|Current number of models being processed.|  
  
###  <a name="bkmk_DataMiningPrediction"></a> Data Mining Prediction  
 Statistics related to Microsoft Analysis Services Data Mining prediction.  
  
|Counter|Description|  
|-------------|-----------------|  
|Concurrent DM queries|Current number of data mining queries being actively worked on.|  
|Predictions/sec|Number of predictions generated in data mining queries|  
|Rows/sec|Number of rows handled during a data mining prediction query.|  
|Queries/sec|Number of data mining queries that were handled.|  
|Total Queries|Total data mining queries received by the server.|  
|Total Rows|Total rows returned by data mining queries.|  
|Total Predictions|Total data mining prediction queries received by the server.|  
  
###  <a name="bkmk_Locks"></a> Locks  
 Statistics related to Microsoft Analysis Services internal server locks.  
  
|Counter|Description|  
|-------------|-----------------|  
|Current latch waits|Current number of threads waiting for a latch.  These are latch requests that could not be given immediate grants and are in a wait state.|  
|Latch waits/sec|Rate of latch requests that could not be granted immediately and had to wait before being granted.|  
|Current locks|Current number of locked objects.|  
|Current lock waits|Current number of clients waiting for a lock.|  
|Lock requests/sec|Number of lock requests per second.|  
|Lock grants/sec|Number of lock grants per second.|  
|Lock waits/sec|Number of lock waits per second.  These are lock requests that could not be given immediate lock grants and were put in a wait state.|  
|Lock denials/sec|Rate of lock denials.|  
|Unlock requests/sec|Number of unlock requests per second.|  
|Total deadlocks detected|Total number of deadlocks detected.|  
  
###  <a name="bkmk_MDX"></a> MDX  
 Statistics related to Microsoft Analysis Services MDX Calculations.  
  
|Counter|Description|  
|-------------|-----------------|  
|Number of calculation covers|Total number of evaluation nodes built by MDX execution plans, including active and cached.|  
|Current number of evaluation nodes|Current (approximate) number of evaluation nodes built by MDX execution plans, including active and cached.|  
|Number of Storage Engine evaluation nodes|Total number of Storage Engine evaluation nodes built by MDX execution plans.|  
|Number of cell-by-cell evaluation nodes|Total number of cell-by-cell evaluation nodes built by MDX execution plans.|  
|Number of bulk-mode evaluation nodes|Total number of bulk-mode evaluation nodes built by MDX execution plans.|  
|Number of evaluation nodes that covered a single cell|Total number of evaluation nodes built by MDX execution plans that covered only one cell.|  
|Number of evaluation nodes with calculations at the same granularity|Total number of evaluation nodes built by MDX execution plans for which the calculations were at the same granularity as the evaluation node.|  
|Current number of cached evaluation nodes|Current (approximate) number of cached evaluation nodes built by MDX execution plans.|  
|Number of cached Storage Engine evaluation nodes|Total number of cached Storage Engine evaluation nodes built by MDX execution plans|  
|Number of cached bulk-mode evaluation nodes|Total number of cached bulk-mode evaluation nodes built by MDX execution plans.|  
|Number of cached 'other' evaluation nodes|Total number of cached evaluation nodes built by MDX execution plans that are neither Storage Engine nor Bulk-mode.|  
|Number of evictions of evaluation nodes|Total number of cache evictions of evaluation nodes due to collisions.|  
|Number of hash index hits in the cache of evaluation nodes|Total number of hits in the cache of evaluation nodes that were satisfied by the hash index.|  
|Number of cell-by-cell hits in the cache of evaluation nodes|Total number of cell-by-cell hits in the cache of evaluation nodes.|  
|Number of cell-by-cell misses in the cache of evaluation nodes|Total number of cell-by-cell misses in the cache of evaluation nodes.|  
|Number of subcube hits in the cache of evaluation nodes|Total number of subcube hits in the cache of evaluation nodes.|  
|Number of subcube misses in the cache of evaluation nodes|Total number of subcube misses in the cache of evaluation nodes.|  
|Total Sonar subcubes|Total number of subcubes that the query optimizer generated.|  
|Total cells calculated|Total number of cell properties calculated.|  
|Total recomputes|Total number of cells recomputed due to error.|  
|Total flat cache inserts|Total number of cell values inserted into flat calculation cache.|  
|Total calculation cache registered|Total number of calculation caches registered.|  
|Total NON EMPTY|Total number of times a NON EMPTY algorithm was used.|  
|Total NON EMPTY unoptimized|Total number of times an unoptimized NON EMPTY algorithm was used.|  
|Total NON EMPTY for calculated members|Total number of times a NON EMPTY algorithm looped over calculated members.|  
|Total Autoexist|Total number of times Autoexist was performed.|  
|Total EXISTING|Total number of times an EXISTING set operation was performed.|  
  
###  <a name="bkmk_Memory"></a> Memory  
 Statistics related to Microsoft Analysis Services internal server memory.  
  
|Counter|Description|  
|-------------|-----------------|  
|Page Pool 64 Alloc KB|Memory borrowed from system, in KB.  This memory is given away to other parts of the server.|  
|Page Pool 64 Lookaside KB|Current memory in 64KB lookaside list, in KB.  (Memory pages ready to be used.)|  
|Page Pool 8 Alloc KB|Memory borrowed from 64KB page pool, in KB.  This memory is given away to other parts of the server.|  
|Page Pool 8 Lookaside KB|Current memory in 8KB lookaside list, in KB.  (Memory pages ready to be used.)|  
|Page Pool 1 Alloc KB|Memory borrowed from 64KB page pool, in KB.  This memory is given away to other parts of the server.|  
|Page Pool 1 Lookaside KB|Current memory in 8KB lookaside list, in KB.  (Memory pages ready to be used.)|  
|Cleaner Current Price|Current price of memory, $/byte/time, normalized to 1000.|  
|Cleaner Balance/sec|Rate of balance+shrink operations.|  
|Cleaner Memory shrunk KB/sec|Rate of shrinking, in KB/sec.|  
|Cleaner Memory shrinkable KB|Amount of memory, in KB, subject to purging by the background cleaner.|  
|Cleaner Memory nonshrinkable KB|Amount of memory, in KB, not subject to purging by the background cleaner.|  
|Cleaner Memory KB|Amount of memory, in KB, known to the background cleaner.  (Cleaner memory shrinkable + Cleaner memory nonshrinkable.)|  
|Memory Usage KB|Memory usage of the server process as used in calculating cleaner memory price.  Equal to counter Process\PrivateBytes plus the size of memory-mapped data, ignoring any memory which was mapped or allocated by the xVelocity in-memory analytics engine (VertiPaq) in excess of the xVelocity engine Memory Limit.|  
|Memory Limit Low KB|Low memory limit, from configuration file.|  
|Memory Limit High KB|High memory limit, from configuration file.|  
|AggCacheKB|Current memory allocated to aggregation cache, in KB.|  
|Quota KB|Current memory quota, in KB.  Memory quota is also known as a memory grant or memory reservation.|  
|Quota Blocked|Current number of quota requests that are blocked until other memory quotas are freed.|  
|Filestore KB|Current memory allocated to filestore (file cache), in KB.|  
|Filestore Page Faults/sec|Filestore page fault rate.|  
|Filestore Reads/sec|Filestore pages read/sec.|  
|Filestore KB Reads/sec|Filestore KB read/sec.|  
|Filestore Writes/sec|Filestore pages written/sec.  The writes are asynchronous.|  
|Filestore KB Write/sec|Filestore KB written/sec.  The writes are asynchronous.|  
|Filestore IO Errors/sec|Filestore IO Error rate.|  
|Filestore IO Errors|Filestore IO Errors total.|  
|Filestore Clock Pages Examined/sec|Rate of background cleaner examining pages for eviction consideration.|  
|Filestore Clock Pages HaveRef/sec|Rate of background cleaner examining pages that have a current reference count (are currently in use).|  
|Filestore Clock Pages Valid/sec|Rate of background cleaner examining pages that are valid candidates for eviction.|  
|Filestore Memory Pinned KB|Current filestore memory pinned, in KB.|  
|In-memory Dimension Property File KB|Current size of in-memory dimension property file, in KB.|  
|In-memory Dimension Property File KB/sec|Rate of writes to in-memory dimension property file, in KB.|  
|Potential In-memory Dimension Property File KB|Potential size of in-memory dimension property file, in KB.|  
|Dimension Property Files|Number of dimension property files.|  
|In-memory Dimension Index (Hash) File KB|Size of current in-memory dimension index (hash) file, in KB.|  
|In-memory Dimension Index (Hash) File KB/sec|Rate of writes to in-memory dimension index (hash) file, in KB.|  
|Potential In-memory Dimension Index (Hash) File KB|Potential size of in-memory dimension index (hash) file, in KB.|  
|Dimension Index (Hash) Files|Number of dimension index (hash) files.|  
|In-memory Dimension String File KB|Current size of in-memory dimension string file, in KB.|  
|In-memory Dimension String File KB/sec|Rate of writes to in-memory dimension string file, in KB.|  
|Potential In-memory Dimension String File KB|Potential size of in-memory dimension string file, in KB.|  
|Dimension String Files|Number of dimension string files.|  
|In-memory Map File KB|Current size of in-memory map file, in KB.|  
|In-memory Map File KB/sec|Rate of writes to in-memory map file, in KB.|  
|Potential In-memory Map File KB|Potential size of in-memory map file, in KB.|  
|Map Files|Number of map files.|  
|In-memory Aggregation Map File KB|Current size of in-memory aggregation map file, in KB.|  
|In-memory Aggregation Map File KB/sec|Rate of writes to in-memory aggregation map file, in KB.|  
|Potential In-memory Aggregation Map File KB|Size of potential in-memory aggregation map file, in KB.|  
|Aggregation Map Files|Number of aggregation map files.|  
|In-memory Fact Data File KB|Size of current in-memory fact data file, in KB.|  
|In-memory Fact Data File KB/sec|Rates of writes to in-memory fact data file KB rate.|  
|Potential In-memory Fact Data File KB|Size of potential in-memory fact data file, in KB.|  
|Fact Data Files|Number of fact data files.|  
|In-memory Fact String File KB|Size of current in-memory fact string file, in KB.|  
|In-memory Fact String File KB/sec|Rate of writes to in-memory fact string file, in KB.|  
|Potential In-memory Fact String File KB|Size of potential in-memory fact string file, in KB.|  
|Fact String Files|Number of fact string files.|  
|In-memory Fact Aggregation File KB|Current size of in-memory fact aggregation file, in KB.|  
|In-memory Fact Aggregation File KB/sec|Rate of writes to in-memory fact aggregation file, in KB.|  
|Potential In-memory Fact Aggregation File KB|Size of potential in-memory fact aggregation file, in KB.|  
|Fact Aggregation Files|Number of fact aggregation files.|  
|In-memory Other File KB|Size of current in-memory other file, in KB.|  
|In-memory Other File KB/sec|Rate of writes to in-memory other file, in KB.|  
|Potential In-memory Other File KB|Size of potential in-memory other file, in KB.|  
|Other Files|Number of other files.|  
|VertiPaq Paged KB|Kilobytes of paged memory in use for in-memory data.|  
|VertiPaq Nonpaged KB|Kilobytes of memory locked in the working set for use by the in-memory engine.|  
|VertiPaq Memory-Mapped KB|Kilobytes of pageable memory in use for in-memory data.|  
|Memory Limit Hard KB|Hard memory limit, from configuration file.|  
|Memory Limit VertiPaq KB|In-memory limit, from configuration file.|  
  
###  <a name="bkmk_ProactiveCaching"></a> Proactive Caching  
 Statistics related to Microsoft Analysis Services Proactive Caching.  
  
|Counter|Description|  
|-------------|-----------------|  
|Notifications/sec|Rate of notifications from relational database.|  
|Processing Cancellations/sec|Rate of processing cancellations caused by notifications.|  
|Proactive Caching Begin/sec|Rate of proactive caching begin.|  
|Proactive Caching Completion/sec|Rate of proactive caching completion.|  
  
###  <a name="bkmk_ProcAggregations"></a> Processing Aggregations  
 Statistics related to Microsoft Analysis Services processing of aggregations in MOLAP data files.  
  
|Counter|Description|  
|-------------|-----------------|  
|Current partitions|Current number of partitions being processed.|  
|Total partitions|Total number of partitions processed (successfully or otherwise).|  
|Memory size rows|Size of current aggregations in memory.  This count is an estimate.|  
|Memory size bytes|Size of current aggregations in memory.  This count is an estimate.|  
|Rows merged/sec|Rate of rows merged or inserted into an aggregation.|  
|Rows created/sec|Rate of aggregation rows created.|  
|Temp file rows written/sec|Rate of writing rows to a temporary file.  Temporary files are written when aggregations exceed memory limits.|  
|Temp file bytes written/sec|Rate of writing bytes to a temporary file.  Temporary files are written when aggregations exceed memory limits.|  
  
###  <a name="bkmk_ProcIndexes"></a> Processing Indexes  
 Statistics related to Microsoft Analysis Services processing of indexes for MOLAP data files.  
  
|Counter|Description|  
|-------------|-----------------|  
|Current partitions|Current number of partitions being processed.|  
|Total partitions|Total number of partitions processed (successfully or otherwise).|  
|Rows/sec|Rate of rows from MOLAP files used to create indexes.|  
|Total rows|Total rows from MOLAP files used to create indexes.|  
  
###  <a name="bkmk_Processing"></a> Processing  
 Statistics related to Microsoft Analysis Services processing of data.  
  
|Counter|Description|  
|-------------|-----------------|  
|Rows read/sec|Rate of rows read from all relational databases.|  
|Total rows read|Count of rows read from all relational databases.|  
|Rows converted/sec|Rate of rows converted during processing.|  
|Total rows converted|Count of rows converted during processing.|  
|Rows written/sec|Rate of rows written during processing.|  
|Total rows written|Count of rows written during processing.|  
  
###  <a name="bkmk_StorageEngineQuery"></a> Storage Engine Query  
 Statistics related to Microsoft Analysis Services storage engine queries.  
  
|Counter|Description|  
|-------------|-----------------|  
|Current measure group queries|Current number of measure group queries being actively worked on.|  
|Measure group queries/sec|Rate of measure group queries|  
|Total measure group queries|Total number of queries to measure group.|  
|Current dimension queries|Current number of dimension queries being actively worked on.|  
|Dimension queries/sec|Rate of dimension queries|  
|Total dimension queries.|Total number of dimension queries.|  
|Queries answered/sec|Rate of queries answered.|  
|Total queries answered|Total number of queries answered.|  
|Bytes sent/sec|Rate of bytes sent by server to clients, in response to queries.|  
|Total bytes sent|Total bytes sent by server to clients, in response to queries.|  
|Rows sent/sec|Rate of rows sent by server to clients.|  
|Total rows sent|Total rows sent by server to clients.|  
|Queries from cache direct/sec|Rate of queries answered from cache directly.|  
|Queries from cache filtered/sec|Rate of queries answered by filtering existing cache entry.|  
|Queries from file/sec|Rate of queries answered from files.|  
|Total queries from cache direct|Total number of queries derived directly from cache.  Note that this is per partition.|  
|Total queries from cache filtered|Total queries answered by filtering existing cache entries.|  
|Total queries from file|Total number of queries answered from files.|  
|Map reads/sec|Number of logical read operations using the Map file.|  
|Map bytes/sec|Bytes read from the Map file.|  
|Data reads/sec|Number of logical read operations using the Data file.|  
|Data bytes/sec|Bytes read from the Data file.|  
|Avg time/query|Average time per query, in milliseconds.  Response time based on queries answered since the last counter measurement.|  
|Network round trips/sec|Rate of network round trips.  This includes all client/server communication.|  
|Total network round trips|Total network round trips.  This includes all client/server communication.|  
|Flat cache lookups/sec|Rate of flat cache lookups.  This includes global, session, and query scope flat caches.|  
|Flat cache hits/sec|Rate of flat cache hits.  This includes global, session, and query scope flat caches.|  
|Calculation cache lookups/sec|Rate of calculation cache lookups.  This includes global, session, and query scope calculation caches.|  
|Calculation cache hits/sec|Rate of calculation cache hits.  This includes global, session, and query scope calculation caches.|  
|Persisted cache lookups/sec|Rate of persisted cache lookups.  Persisted caches are created by the MDX script CACHE statement.|  
|Persisted cache hits/sec|Rate of persisted cache hits.  Persisted caches are created by the MDX script CACHE statement.|  
|Dimension cache lookups/sec|Rate of dimension cache lookups.|  
|Dimension cache hits/sec|Rate of dimension cache hits.|  
|Measure group cache lookups/sec|Rate of measure group cache lookups.|  
|Measure group cache hits/sec|Rate of measure group cache hits.|  
|Aggregation lookups/sec|Rate of aggregation lookups.|  
|Aggregation hits/sec|Rate of aggregation hits.|  
  
###  <a name="bkmk_Threads"></a> Threads  
 Statistics related to Microsoft Analysis Services threads.  
  
|Counter|Description|  
|-------------|-----------------|  
|Short parsing idle threads|Number of idle threads in the short parsing thread pool.|  
|Short parsing busy threads|Number of busy threads in the short parsing thread pool.|  
|Short parsing job queue length|Number of jobs in the queue of the short parsing thread pool.|  
|Short parsing job rate|Rate of jobs through the short parsing thread pool.|  
|Long parsing idle threads|Number of idle threads in the long parsing thread pool.|  
|Long parsing busy threads|Number of busy threads in the long parsing thread pool.|  
|Long parsing job queue length|Number of jobs in the queue of the long parsing thread pool.|  
|Long parsing job rate|Rate of jobs through the long parsing thread pool.|  
|Query pool idle threads|Number of idle threads in the query thread pool.|  
|Query pool busy threads|Number of busy threads in the query thread pool.|  
|Query pool job queue length|Number of jobs in the queue of the query thread pool.|  
|Query pool job rate|Rate of jobs through the query thread pool.|  
|Processing pool idle non-I/O threads|Number of idle threads in the processing thread pool dedicated to non-I/O jobs.|  
|Processing pool busy non-I/O threads|Number of threads running non-I/O jobs in the processing thread pool.|  
|Processing pool job queue length|Number of non-I/O jobs in the queue of the processing thread pool.|  
|Processing pool job rate|Rate of non-I/O jobs through the processing thread pool.|  
|Processing pool idle I/O job threads|Number of idle threads for I/O jobs in the processing thread pool.|  
|Processing pool busy I/O job threads|Number of threads running I/O jobs in the processing thread pool.|  
|Processing pool I/O job queue length|Number of I/O jobs in the queue of the processing thread pool.|  
|Processing pool I/O job completion rate|Rate of I/O jobs through the processing thread pool.|  
  
  
