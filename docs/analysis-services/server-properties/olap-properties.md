---
title: "Analysis Services OLAP Properties | Microsoft Docs"
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
# OLAP Properties
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]

  [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] supports the OLAP server properties listed in the following tables. For more information about additional server properties and how to set them, see [Server Properties in Analysis Services](../../analysis-services/server-properties/server-properties-in-analysis-services.md).  
  
 **Applies to:** Multidimensional server mode only  
  
## Memory  
 **DefaultPageSizeForData**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **DefaultPageSizeForDataHeader**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **DefaultPageSizeForIndex**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **DefaultPageSizeForIndexHeader**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **DefaultPageSizeForString**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **DefaultPageSizeForHash**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **DefaultPageSizeForProp**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
## LazyProcessing  
 **Enabled**  
 A Boolean property that specifies whether lazy aggregation processing is enabled.  
  
 **SleepIntervalSecs**  
 A signed 32-bit integer property that defines the interval, in seconds, that the server checks whether there are lazy processing jobs pending.  
  
 **MaxCPUUsage**  
 A signed 64-bit double-precision floating-point number property that defines maximum CPU usage for lazy processing, expressed as a percentage. The server monitors average CPU use based on snapshots. It is normal behavior for the CPU to spike above this threshold.  
  
 The default value for this property is 0.5, indicating a maximum of 50% of the CPU will be devoted to lazy processing.  
  
 **MaxObjectsInParallel**  
 A signed 32-bit integer property that specifies the maximum number of partitions that can be lazily processed in parallel.  
  
 **MaxRetries**  
 A signed 32-bit integer property that defines the number of retries in the event that lazy processing fails before an error is raised.  
  
## ProcessPlan  
 **CacheRowsetRows**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **CacheRowsetToDisk**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **DistinctBuffer**  
 A signed 32-bit integer property that defines the size of an internal buffer used for distinct counts. Increase this value to speed up distinct count processing at the cost of memory use.  
  
 **EnableRolapDimQueryTableGrouping**  
 A Boolean property that specifies whether table grouping is enabled for ROLAP dimensions. If True, when querying ROLAP dimensions at runtime, entire ROLAP dimension tables are queried at once, as opposed to separate queries for each attribute.  
  
 **EnableTableGrouping**  
 A Boolean property that specifies whether table grouping is enabled. If True, when processing dimensions, entire dimension tables are queried at once, as opposed to separate queries for each attribute.  
  
 **ForceMultiPass**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **MaxTableDepth**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **MemoryAdjustConst**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **MemoryAdjustFactor**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **MemoryLimit**  
 A signed 64-bit double-precision floating-point number property that defines the maximum amount of memory to be devoted to processing, expressed as a percentage of physical memory.  
  
 The default value for this property is 65, indicating that 65% of physical memory may be devoted to cube and dimension processing.  
  
 **MemoryLimitErrorEnabled**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **OptimizeSchema**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
## ProactiveCaching  
 **DefaultRefreshInterval**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **DimensionLatencyAccuracy**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **PartitionLatencyAccuracy**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
## Process  
 **AggregationMemoryLimitMax**  
 A signed 64-bit double-precision floating-point number property that defines the maximum amount of memory that can be devoted to aggregation processing, expressed as a percentage of physical memory.  
  
 The default value for this property is 80, indicating that 80% of physical memory may be devoted to aggregation processing.  
  
 **AggregationMemoryLimitMin**  
 A signed 64-bit double-precision floating-point number property that defines the minimum amount of memory that can be devoted to aggregation processing, expressed as a percentage of physical memory. A larger value may speed up aggregation processing at the cost of memory usage.  
  
 The default value for this property is 10, indicating that minimally 10% of physical memory will be devoted to aggregation processing.  
  
 **AggregationNewAlgo**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **AggregationPerfLog2**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **AggregationsBuildEnabled**  
 A Boolean property that specifies whether aggregation building is enabled. This is a mechanism to benchmark aggregation building without changing aggregation design.  
  
 **BufferMemoryLimit**  
 A signed 64-bit double-precision floating-point number property that defines the processing buffer memory limit, expressed as a percent of physical memory.  
  
 The default value for this property is 60, which indicates that up to 60% of physical memory can be used for buffer memory.  
  
 **BufferRecordLimit**  
 A signed 32-bit integer property that defines the number of records that can be buffered during processing.  
  
 The default value for this property is 1048576 (records).  
  
 **CacheRecordLimit**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **CheckDistinctRecordSortOrder**  
 A Boolean property that defines if the sort order for the results of a distinct count query are meaningful when processing partitions. True indicates the sort order is not meaningful and must be "checked" by the server. When processing partitions with distinct count measure, query sent to SQL with order-by. Set to false to speed up processing.  
  
 The default value for this property is True, which indicates that the sort order is not meaningful and must be checked.  
  
 **DatabaseConnectionPoolConnectTimeout**  
 A signed 32-bit integer property that specifies timeout when opening a new connection in seconds.  
  
 **DatabaseConnectionPoolGeneralTimeout**  
 A signed 32-bit integer property that specifies database connection timeout for use with external OLEDB connections in seconds.  
  
 **DatabaseConnectionPoolMax**  
 A signed 32-bit integer property that specifies the maximum number of pooled database connections.  
  
 The default value for this property is 50 (connections).  
  
 **DatabaseConnectionPoolTimeout**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **DataFileInitEnabled**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **DataPlacementOptimization**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **DataSliceInitEnabled**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **DeepCompressValue**  
 A Boolean property applying to measures with Double data type that specifies whether numbers can be compressed, causing a loss in numeric precision. A value of False indicates no compression and no precision loss.  
  
 The default value for this property is True, which indicates that compression is enabled and precision will be lost.  
  
 **DimensionPropertyKeyCache**  
 A Boolean property that specifies whether dimension property keys are cached. Must be set to True if keys are non-unique.  
  
 **IndexBuildEnabled**  
 A Boolean property that specifies whether indexes are built upon processing. This property is for benchmarking and informational purposes.  
  
 **IndexBuildThreshold**  
 A signed 32-bit integer property that specifies a row count threshold below which indexes will not be built for partitions.  
  
 The default value for this property is 4096 (rows).  
  
 **IndexFileInitEnabled**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **MapFormatMask**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **RecordsReportGranularity**  
 A signed 32-bit integer property that specifies how often the server records Trace events during processing, in rows.  
  
 The default value for this property is 1000, which indicates that a Trace event is logged once every 1000 rows.  
  
 **ROLAPDimensionProcessingEffort**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
## Query  
 **AggregationsUseEnabled**  
 A Boolean property that defines whether stored aggregations are used at runtime. This property allows aggregations to be disabled without changing the aggregation design or re-processing, for informational and benchmarking purposes.  
  
 The default value for this property is True, indicating that aggregations are enabled.  
  
 **AllowSEFiltering**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **CalculationCacheRegistryMaxIterations**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **CalculationEvaluationPolicy**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **ConvertDeletedToUnknown**  
 A Boolean property that specifies whether deleted dimension members are converted to Unknown member.  
  
 **CopyLinkedDataCacheAndRegistry**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **DataCacheRegistryMaxIterations**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **DefaultDrillthroughMaxRows**  
 A signed 32-bit integer property that specifies the maximum number of rows that will return from a drill-through query.  
  
 The default value for this property is 10000 (rows).  
  
 **DimensionPropertyCacheSize**  
 A signed 32-bit integer property that specifies the amount of memory (in bytes) used to cache dimension members used in a query.  
  
 The default is 4,000,000 bytes (or 4 MB) per attribute hierarchy, per active query. The default value provides a well-balanced cache size for solutions having typical hierarchies. However, dimensions with a very large number of members (in the millions) or deep hierarchies perform better if you increase this value.  
  
 Implications of increasing cache size:  
  
-   Memory utilization costs increase when you allow more memory to be used by the dimension cache. Actual usage depends on query execution. Not all queries will use the allowable maximum.  
  
     Note that the memory used by these caches is considered nonshrinkable and will be included when accounting against the **TotalMemoryLimit**.  
  
-   Affects all databases on the server. **DimensionPropertyCachesize** is a server-wide property. Changing this property affects all databases running on the current instance.  
  
Approach for estimating dimension cache requirements:  
  
1.  Start by increasing the size by a large number to determine whether there is a benefit to increasing the dimension cache size. For example, you might want to double the default value as an initial step.  
  
2.  If a performance improvement is evident, incrementally reduce the value until you reach a balance between performance and memory utilization.  


 **ExpressNonEmptyUseEnabled**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **IgnoreNullRolapRows**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **IndexUseEnabled**  
 A Boolean property that defines whether indexes are used at runtime. This property is for informational and benchmarking purposes.  
  
 **MapHandleAlgorithm**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **MaxRolapOrConditions**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **UseCalculationCacheRegistry**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **UseDataCacheFreeLastPageMemory**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **UseDataCacheRegistry**  
 A Boolean property that specifies whether to enable the data cache registry, where query results are cached (though not calculated results).  
  
 **UseDataCacheRegistryHashTable**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **UseDataCacheRegistryMultiplyKey**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **UseDataSlice**  
 A Boolean property that defines whether to use partition data slices at runtime for query optimization. This property is for benchmarking and informational purposes.  
  
 **UseMaterializedIterators**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **UseSinglePassForDimSecurityAutoExist**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **UseVBANet**  
 A Boolean property that defines whether to use the VBA .net assembly for user-defined functions.  
  
 **CalculationPrefetchLocality\ ApplyIntersect**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **CalculationPrefetchLocality\ ApplySubtract**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **CalculationPrefetchLocality\ PrefetchLowerGranularities**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **DataCache\  CachedPageAlloc\ Income**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **DataCache\  CachedPageAlloc\ InitialBonus**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **DataCache\  CachedPageAlloc\ MaximumBalance**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **DataCache\  CachedPageAlloc\ MinimumBalance**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **DataCache\  CachedPageAlloc\ Tax**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **DataCache\CellStore\ Income**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **DataCache\CellStore\ InitialBonus**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **DataCache\CellStore\ MaximumBalance**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **DataCache\CellStore\ MinimumBalance**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **DataCache\CellStore\ Tax**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **DataCache\ MemoryModel \ Income**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **DataCache\ MemoryModel \ InitialBonus**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **DataCache\ MemoryModel \ MaximumBalance**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **DataCache\ MemoryModel \ MinimumBalance**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **DataCache\ MemoryModel\ Tax**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
## Jobs  
 **ProcessAggregation\ MemoryModel\ Income**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **ProcessAggregation\ MemoryModel\ InitialBonus**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **ProcessAggregation\ MemoryModel\ MaximumBalance**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **ProcessAggregation\ MemoryModel\ MinimumBalance**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **ProcessAggregation\ MemoryModel\ Tax**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **ProcessAggregation\ ProcessPartition\ Income**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **ProcessAggregation\ ProcessPartition \ InitialBonus**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **ProcessAggregation\ ProcessPartition \ MaximumBalance**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **ProcessAggregation\ ProcessPartition \ MinimumBalance**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **ProcessAggregation\ ProcessPartition \ Tax**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **ProcessAggregation\ ProcessProperty\ Income**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **ProcessAggregation\ ProcessProperty\ InitialBonus**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **ProcessAggregation\ ProcessProperty\ MaximumBalance**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **ProcessAggregation\ ProcessProperty\ MinimumBalance**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 **ProcessAggregation\ ProcessProperty\ Tax**  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
## See Also  
 [Server Properties in Analysis Services](../../analysis-services/server-properties/server-properties-in-analysis-services.md)   
 [Determine the Server Mode of an Analysis Services Instance](../../analysis-services/instances/determine-the-server-mode-of-an-analysis-services-instance.md)  
  
  
