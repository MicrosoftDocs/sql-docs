---
title: SQL Server Configuration (R Services) - SQL Server Machine Learning Services
ms.prod: sql
ms.technology: machine-learning

ms.date: 03/29/2019
ms.topic: conceptual
author: dphansen
ms.author: davidph
manager: cgronlun
---
# SQL Server configuration for use with R
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This article is the second in a series that describes performance optimization for R Services based on two case studies.  This article provides guidance about the hardware and network configuration of the computer that is used to run SQL Server R Services. It also contains information about ways to configure the SQL Server instance, database, or tables used in a solution. Because use of NUMA in SQL Server blurs the line between hardware and database optimizations, a third section discusses CPU affinitization and resource governance in detail.

> [!TIP]
> If you are new to SQL Server, we highly recommend that you also review the SQL Server performance tuning guide: [Monitor and tune for performance](https://docs.microsoft.com/sql/relational-databases/performance/monitor-and-tune-for-performance).

## Hardware optimization

Optimization of the server computer is important for making sure that you have the resources to run external scripts. When resources are limited, you might see symptoms such as these:

- Job execution is deferred or canceled to prioritize other database operations
- Error "quota exceeded" causing R script to terminate without completion
- Data loaded into R memory truncated, for incomplete results

### Memory

The amount of memory available on the computer can have a large impact on the performance of advanced analytic algorithms. Insufficient memory might affect the degree of parallelism when using the SQL compute context. It can also affect the chunk size (rows per read operation) that can be processed, and the number of simultaneous sessions that can be supported.

A minimum of 32 GB is highly recommended. If you have more than 32 GB available, you can configure the SQL data source to use more rows in every read operation to improve performance.

You can also manage memory used by the instance. By default, SQL Server is prioritized over external script processes when memory is allocated. In a default installation of R Services, only 20% of available memory is allocated to R.

Typically this is not enough for data science tasks, but neither do you want to starve SQL server of memory. You should experiment and fine-tune memory allocation between the database engine, related services, and external scripts, with the understanding that the optimum configuration varies case by case.

For the resume-matching model, external script use was heavy and there were no other database engine services running; therefore, resources allocated to external scripts were increased to 70%, which was the best configuration for script performance.

### Power options

On the Windows operating system, the **High performance** power option should be used. Using a different power setting results in decreased or inconsistent performance when using SQL Server.

### Disk IO

Training and prediction jobs using R Services are inherently IO bound, and depend on the speed of the disk(s) that the database is stored on. Faster drives, such as solid-state drives (SSD) may help.

Disk IO is also affected by other applications accessing the disk: for example, read operations against a database by other clients. Disk IO performance can also be affected by settings on the file system in use, such as the block size used by the file system.

If multiple drives are available, store the databases on a different drive than SQL Server so that requests for the database engine are not hitting the same disk as requests for data stored in the database.

Disk IO can also greatly impact performance when running RevoScaleR analytic functions that use multiple iterations during training. For example, `rxLogit`, `rxDTree`, `rxDForest`, and `rxBTrees` all use multiple iterations. When the data source is SQL Server, these algorithms use temporary files that are optimized to capture the data. These files are automatically cleaned up after the session completes. Having a high-performance disk for read/write operations can significantly improve the overall elapsed time for these algorithms.

> [!NOTE]
> Early versions of R Services required 8.3 filename support on Windows operating systems. This restriction was lifted after Service Pack 1. However, you can use fsutil.exe to determine whether a drive supports 8.3 filenames, or to enable support if it does not.

### Paging file

The Windows operating system uses a paging file to manage crash dumps and for storing virtual memory pages. If you notice excessive paging, consider increasing the physical memory on the machine. Although having more physical memory does not eliminate paging, it does reduce the need for paging.

The speed of the disk that the page file is stored on can also affect performance. Storing the page file on an SSD, or using multiple page files across multiple SSDs, can improve performance.

For information on sizing the page file, see [How to determine the appropriate page file size for 64-bit versions of Windows](https://support.microsoft.com/kb/2860880).

## Optimizations at instance or database level

Optimization of the SQL Server instance is the key to efficient execution of external scripts.

> [!NOTE]
> The optimal settings differ depending on the size and type of your data, the number of columns you are using for scoring or training a model.
> 
> You can review the results of specific optimizations in the final article: [Performance Tuning - case study results](../../advanced-analytics/r/performance-case-study-r-services.md)
> 
> For sample scripts, see the separate [GitHub repository](https://github.com/Microsoft/SQL-Server-R-Services-Samples/tree/master/PerfTuning).

### Table compression

IO performance can often be improved by using either compression or a columnar data store. Generally, data is often repeated in several columns within a table, so using a columnstore takes advantage of these repetitions when compressing the data.

A columnstore might not be as efficient if there are numerous insertions into the table, but is a good choice if the data is static or only changes infrequently. If a columnar store is not appropriate, enabling compression on a row major table can be used to improve IO.

For more information, see the following documents:

+ [Data compression](../../relational-databases/data-compression/data-compression.md)

+ [Enable compression on a table or index](../../relational-databases/data-compression/enable-compression-on-a-table-or-index.md)

+ [Columnstore indexes guide](../../relational-databases/indexes/columnstore-indexes-overview.md)

### Memory-optimized tables

Nowadays, memory is no longer a problem for modern computers. As hardware specifications continue to improve, it is relatively easy to get RAM at good values. However, at the same time, data is being produced more quickly than ever before, and the data must be processed with low latency.

Memory-optimized tables represent one solution, in that they leverage the large memory available in advanced computers to tackle the problem of big data. Memory-optimized tables mainly reside in memory, so that data is read from and written to memory. For durability, a second copy of the table is maintained on disk and data is only read from disk during database recovery.

If you need to read from and write to tables frequently, memory-optimized tables can help with high scalability and low latency.  In the resume-matching scenario, use of memory-optimized tables allowed us to read all the resume features from the database and store them in main memory, to match with new job openings. This significantly reduced disk IO.

Additional performance gains were achieved by using memory-optimized table in the process of writing predictions back to the database from multiple concurrent batches. The use of memory-optimized tables on SQL Server enabled low latency on table reads and writes.

The experience was also seamless during development. Durable memory-optimized tables were created at the same time that the database was created. Therefore, development used the same workflow regardless of where the data was stored.

### Processor

SQL Server can perform tasks in parallel by using the available cores on the machine; the more cores that are available, the better the performance. While increasing the number of cores might not help for IO bound operations, CPU bound algorithms benefit from faster CPUs with many cores.

Because the server is normally used by multiple users simultaneously, the database administrator must
determine the ideal number of cores that are needed to support peak workload computations.

### Resource governance

In editions that support Resource Governor, you can use resource pools to specify that certain workloads are allocated some number of CPUs. You can also manage the amount of memory allocated to specific workloads.

Resource governance in SQL Server lets you centralize monitoring and control of the various resources used by SQL Server and by R. For example, you might allocate half the available memory for the database engine, to ensure that core services can always run in spite of transient heavier workloads.

The default value for memory consumption by external scripts is limited to 20% of the total memory available for SQL Server itself. This limit is applied by default to ensure that all tasks that rely on the database server are not severely affected by long running R jobs. However, these limits can be changed by the database administrator. In many cases, the 20% limit is not adequate to support serious machine learning workloads.

The configuration options supported are **MAX_CPU_PERCENT**, **MAX_MEMORY_PERCENT**, and **MAX_PROCESSES**. To view the current settings, use this statement: `SELECT * FROM sys.resource_governor_external_resource_pools`

-  If the server is primarily used for R Services, it might be helpful to increase MAX_CPU_PERCENT to 40% or 60%.

-  If many R sessions must use the same server at the same time, all three settings should be increased.

To change the allocated resource values, use T-SQL statements.

+ This statement sets the memory usage to 40%: `ALTER EXTERNAL RESOURCE POOL [default] WITH (MAX_MEMORY_PERCENT = 40)`

+ This statement sets all three configurable values: `ALTER EXTERNAL RESOURCE POOL [default] WITH (MAX_CPU_PERCENT = 40, MAX_MEMORY_PERCENT = 50, MAX_PROCESSES = 20)`

+ If you change a memory, CPU, or max process setting, and then want to apply the settings immediately, run this statement: `ALTER RESOURCE GOVERNOR RECONFIGURE`

## Soft-NUMA, hardware NUMA, and CPU affinity

When using SQL Server as the compute context, you can sometimes achieve better performance by tuning settings related to NUMA and processor affinity. 

Systems with _hardware NUMA_ have more than one system bus, each serving a small set of processors. Each CPU can access memory associated with other groups in a coherent way. Each group is called a NUMA node. If you have hardware NUMA, it may be configured to use interleaved memory instead of NUMA. In that case, Windows and therefore SQL Server will not recognize it as NUMA. 

You can run the following query to find the number of memory nodes available to SQL Server:

```sql
SELECT DISTINCT memory_node_id
FROM sys.dm_os_memory_clerks
```

If the query returns a single memory node (node 0), either you do not have hardware NUMA, or the hardware is configured as interleaved (non-NUMA). SQL Server also ignores hardware NUMA when there four or fewer CPUs, or if at least one node has only one CPU.

If your computer has multiple processors but does not have hardware-NUMA, you can also use [Soft-NUMA](https://docs.microsoft.com/sql/database-engine/configure-windows/soft-numa-sql-server) to subdivide CPUs into smaller groups.  In both SQL Server 2016 and SQL Server 2017, the Soft-NUMA feature is automatically enabled when starting the SQL Server service.

When Soft-NUMA is enabled, SQL Server automatically manages the nodes for you; however, to optimize for specific workloads, you can disable _soft affinity_ and manually configure CPU affinity for the soft NUMA nodes. This can give you more control over which workloads are assigned to which nodes, particularly if you are using an edition of SQL Server that supports resource governance. By specifying CPU affinity and aligning resource pools with groups of CPUs, you can reduce latency, and ensure that related processes are performed within the same NUMA node.

The overall process for configuring soft-NUMA and CPU affinity to support R workloads is as follows:

1. Enable soft-NUMA, if available
2. Define processor affinity
3. Create resource pools for external processes, using [Resource Governor](../r/resource-governance-for-r-services.md)
4. Assign the [workload groups](../../relational-databases/resource-governor/resource-governor-workload-group.md) to specific affinity groups

For details, including sample code, see this tutorial: [SQL Optimization Tips and Tricks (Ke Huang)](https://gallery.cortanaintelligence.com/Tutorial/SQL-Server-Optimization-Tips-and-Tricks-for-Analytics-Services)

**Other resources:**

+ [Soft-NUMA in SQL Server](https://docs.microsoft.com/sql/database-engine/configure-windows/soft-numa-sql-server)
    
    How to map soft-NUMA nodes to CPUs

## Task-specific optimizations

This section summarizes methods adopted in these case studies, and in other tests, for optimizing specific machine learning workloads. Common workloads include model training, feature extraction and feature engineering, and various scenarios for scoring: single-row, small batch, and large batch.

### Feature engineering

One pain point with R is that it is usually processed on a single CPU. This is a major performance bottleneck for many tasks, especially feature engineering. In the resume-matching solution, the feature engineering task alone created 2,500 cross-product features that had to be combined with the original 100 features. This task would take a significant amount of time if everything was done on a single CPU.

There are multiple ways to improve the performance of feature engineering. You can either optimize your R code and keep feature extraction inside the modeling process, or move the feature engineering process into SQL.

- Using R. You define a function and then pass it as the argument to [rxTransform](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxtransform) during training. If the model supports parallel processing, the feature engineering task can be processed using multiple CPUs. Using this approach, the data science team observed a 16% performance improvement in terms of scoring time. However, this approach requires a model that supports parallelization and a query that can be executed using a parallel plan.

- Use R with a SQL compute context. In a multiprocessor environment with isolated resources available for execution of separate batches, you can achieve greater efficiency by isolating the SQL queries used for each batch, to extract data from tables and constrain the data on the same workload group. Methods used to isolate the batches include partitioning, and use of PowerShell to execute separate queries in parallel.

- Ad hoc parallel execution: In a SQL Server compute context, you can rely on the SQL database engine to enforce parallel execution if possible and if that option is found to be more efficient.

- Use T-SQL in a separate featurization process. Precomputing the feature data using SQL is generally faster.

### Prediction (scoring) in parallel

One of the benefits of SQL Server is its ability to handle a large volume of rows in parallel. Nowhere is this advantage so marked as in scoring. Generally the model does not need access to all the data for scoring, so you can partition the input data, with each workload group processing one task.

You can also send the input data as a single query, and SQL Server then analyzes the query. If a parallel query plan can be created for the input data, it automatically partitions data assigned to the nodes and performs required joins and aggregations in parallel as well.

If you are interested in the details of how to define a stored procedure for use in scoring, see the sample project on [GitHub](https://github.com/Microsoft/SQL-Server-R-Services-Samples/tree/master/SQLOptimizationTips/SQLR) and look for the file "step5_score_for_matching.sql". The sample script also tracks query start and end times and writes the time to the SQL console so that you can assess performance.

### Concurrent scoring using resource groups

To scale up the scoring problem, a good practice is to adopt the map-reduce approach in which millions of items are divided into multiple batches. Then, multiple scoring jobs are executed concurrently. In this framework, the batches are processed on different CPU sets, and the results are collected and written back to the database.

This is the approach used in the resume-matching scenario; however, resource governance in SQL Server is essential for implementing this approach. By setting up workload groups for external script jobs, you can route R scoring jobs to different processor groups and achieve faster throughput.

Resource governance can also help allocate divide the available resources on the server (CPU and memory) to minimize workload competition. You can set up classifier functions to distinguish between different types of R jobs: for example, you might decide that scoring called from an application always takes priority, while retraining jobs have low priority. This resource isolation can potentially improve execution time and provide more predictable performance.

### Concurrent scoring using PowerShell

If you decide to partition the data yourself, you can use PowerShell scripts to execute multiple concurrent scoring tasks. To do this, use the Invoke-SqlCmd cmdlet, and initiate the scoring tasks in parallel.

In the resume-matching scenario, concurrency was designed as follows:

- 20 processors divided into four groups of five CPUs each. Each group of CPUs is located on the same NUMA node.

- Maximum number of concurrent batches was set to eight.

- Each workload group must handle two scoring tasks. As soon as one task finished reading data and starts scoring, the other task can start reading data from the database.

To see the PowerShell scripts for this scenario, open the file experiment.ps1 in the [Github project](https://github.com/Microsoft/SQL-Server-R-Services-Samples/tree/master/SQLOptimizationTips).

### Storing models for prediction

When training and evaluation finishes and you have selected a best model, we recommend storing the model in the database so that it is available for predictions. Loading the pre-computed model from the database for the prediction is efficient, because SQL Server machine learning uses special serialization algorithms to store and load models when moving between R and the database.

> [!TIP]
> In SQL Server 2017, you can use the PREDICT function to perform scoring even if R is not installed on the server. Limited models types are supported, from the RevoScaleR package.

However, depending on the algorithm you use, some models can be quite large, especially when trained on a large data set. For example, algorithms such as **lm** or **glm** generate a lot of summary data along with rules. Because there are limits on the size of a model that can be stored in a varbinary column, we recommend that you eliminate unnecessary artifacts from the model before storing the model in the database for production.

## Articles in this series

[Performance tuning for R - introduction](../r/sql-server-r-services-performance-tuning.md)

[Performance tuning for R - SQL Server configuration](../r/sql-server-configuration-r-services.md)

[Performance tuning for R - R code and data optimization](../r/r-and-data-optimization-r-services.md)

[Performance Tuning - case study results](../r/performance-case-study-r-services.md)
