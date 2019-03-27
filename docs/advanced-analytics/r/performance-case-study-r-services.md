---
title: Performance for SQL Server R Services - results and resources - SQL Server Machine Learning Services
ms.prod: sql
ms.technology: machine-learning

ms.date: 04/15/2018  
ms.topic: conceptual
ms.author: dphansen
ms.author: davidph
manager: cgronlun
---
# Performance for R Services: results and resources
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This article is the fourth and final in a series that describes performance optimization for R Services. This article summarizes the methods, findings, and conclusions of two case studies that tested various optimization methods.

The two case studies had different goals:

+ The first case study, by the R Services development team, sought to measure the impact of specific optimization techniques
+ The second case study, by a data scientist team, experimented with multiple methods to determine the best optimizations for a specific high-volume scoring scenario.

This topic lists the detailed results of the first case study. For the second case study, a summary describes the overall findings. At the end of this topic are links to all scripts and sample data, and resources used by the original authors.

## Performance case study: Airline dataset

This case study by the SQL Server R Services development team tested the effects of various optimizations. A single rxLogit model was created and scoring performed on the Airline data set. Optimizations were applied during the training and scoring processes to assess individual impacts.

- Github: [Sample data and scripts](https://github.com/Microsoft/SQL-Server-R-Services-Samples/tree/master/PerfTuning) for SQL Server optimizations study

### Test methods

1. The Airline dataset consists a single table of 10M rows. It was downloaded and bulk loaded into SQL Server.
2. Six copies of the table were made.
3. Various modifications were applied to the copies of the table, to test SQL Server features such as page compression, row compression, indexing, columnar data store, etc.
4. Performance was measured before and after each optimization was applied.

| Table name| Description|
|------|------|
| *airline* | Data converted from original xdf file using `rxDataStep`|                          |
| *airlineWithIntCol*   | *DayOfWeek* converted to an integer rather than a string. Also adds a *rowNum* column.|
| *airlineWithIndex*    | The same data as the *airlineWithIntCol* table, but with a single clustered index using the *rowNum* column.|
| *airlineWithPageComp* | The same data as the *airlineWithIndex* table, but with page compression enabled. Also adds two columns, *CRSDepHour* and *Late*, which are computed from *CRSDepTime* and *ArrDelay*. |
| *airlineWithRowComp*  | The same data as the *airlineWithIndex* table, but with row compression enabled. Also adds two columns, *CRSDepHour* and *Late*, which are computed from *CRSDepTime* and *ArrDelay*. |
| *airlineColumnar*     | A columnar store with a single clustered index. This table is populated with data from a cleaned up csv file.|

Each test consisted of these steps:

1. R garbage collection was induced before each test.
2. A logistic regression model was created based on the table data. The value of *rowsPerRead* for each test was set to 500000.
3. Scores were generated using the trained model
4. Each test was run six times. The time of the first run (the "cold run") was dropped. To allow for the occasional outlier, the **maximum** time among the remaining five runs was also dropped. The average of the four remaining runs was taken to compute the average elapsed runtime of each test.
5. The tests were run using the *reportProgress* parameter with the value 3 (= report timings and progress). Each output file contains information regarding the time spent in IO, transition time, and compute time. These times are useful for troubleshooting and diagnosis.
6. The console output was also directed to a file in the output directory.
7. The test scripts processed the times in these files to compute the average time over runs.

For example, the following results are the times from a single test. The main timings of interest are **Total read time** (IO time) and **Transition time** (overhead in setting up processes for computation).

**Sample timings**

```text
Running IntCol Test. Using airlineWithIntCol table.
run 1 took 3.66 seconds
run 2 took 3.44 seconds
run 3 took 3.44 seconds
run 4 took 3.44 seconds
run 5 took 3.45 seconds
run 6 took 3.75 seconds
  
Average Time: 3.4425
metric time pct
1 Total time 3.4425 100.00
2 Overall compute time 2.8512 82.82
3 Total read time 2.5378 73.72
4 Transition time 0.5913 17.18
5 Total non IO time 0.3134 9.10
```

We recommend that you download and modify the test scripts to help you troubleshoot issues with R Services or with RevoScaleR functions.

### Test results (all)

This section compares before and after results for each of the tests.

#### Data size with compression and a columnar table store

The first test compared the use of compression and a columnar table to reduce the size of the data.

| Table name            | Rows     | Reserved   | Data       | index_size | Unused  | % Saving (reserved) |
|-----------------------|----------|------------|------------|------------|---------|---------------------|
| *airlineWithIndex*    | 10000000 | 2978816 KB | 2972160 KB | 6128 KB    | 528 KB  | 0                   |
| *airlineWithPageComp* | 10000000 | 625784 KB  | 623744 KB  | 1352 KB    | 688 KB  | 79%                 |
| *airlineWithRowComp*  | 10000000 | 1262520 KB | 1258880 KB | 2552 KB    | 1088 KB | 58%                 |
| *airlineColumnar*     | 9999999  | 201992 KB  | 201624 KB  | n/a        | 368 KB  | 93%                 |

**Conclusions**

The greatest reduction in data size was achieved by applying a columnstore index, followed by page compression.

#### Effects of compression

This test compared the benefits of row compression, page compression, and no compression. A model was trained using `rxLinMod` on data from three different data tables. The same formula and query was used for all tables.

| Table name            | Test name       | numTasks | Average time |
|-----------------------|-----------------|----------|--------------|
| *airlineWithIndex*    | NoCompression   | 1        | 5.6775       |
|                       | NoCompression - parallel| 4        | 5.1775       |
| *airlineWithPageComp* | PageCompression | 1        | 6.7875       |
|                       | PageCompression - parallel | 4        | 5.3225       |
| *airlineWithRowComp*  | RowCompression  | 1        | 6.1325       |
|                       | RowCompression - parallel  | 4        | 5.2375       |

**Conclusions**

Compression alone does not seem to help. In this example, the increase in CPU to handle compression compensates for the decrease in IO time.

However, when the test is run in parallel by setting *numTasks* to 4, the average time decreases.

For larger data sets, the effect of compression may be more noticeable. Compression depends on the data set and values, so experimentation may be needed to determine the effect compression has on your data set.

### Effect of Windows power plan options

In this experiment, `rxLinMod` was used with the *airlineWithIntCol* table. The Windows Power Plan was set to either **Balanced** or **High performance**. For all tests, *numTasks* was set to 1. The test was run six times, and was performed twice under both power options to investigate the variability of results.

**High performance** power option:

| Test name | Run \# | Elapsed time | Average time |
|-----------|--------|--------------|--------------|
| IntCol    | 1      | 3.57 seconds |              |
|           | 2      | 3.45 seconds |              |
|           | 3      | 3.45 seconds |              |
|           | 4      | 3.55 seconds |              |
|           | 5      | 3.55 seconds |              |
|           | 6      | 3.45 seconds |              |
|           |        |              | 3.475        |
|           | 1      | 3.45 seconds |              |
|           | 2      | 3.53 seconds |              |
|           | 3      | 3.63 seconds |              |
|           | 4      | 3.49 seconds |              |
|           | 5      | 3.54 seconds |              |
|           | 6      | 3.47 seconds |              |
|           |        |              | 3.5075       |

**Balanced** power option:

| Test name | Run \# | Elapsed time | Average time |
|-----------|--------|--------------|--------------|
| IntCol    | 1      | 3.89 seconds |              |
|           | 2      | 4.15 seconds |              |
|           | 3      | 3.77 seconds |              |
|           | 4      | 5 seconds    |              |
|           | 5      | 3.92 seconds |              |
|           | 6      | 3.8 seconds  |              |
|           |        |              | 3.91         |
|           | 1      | 3.82 seconds |              |
|           | 2      | 3.84 seconds |              |
|           | 3      | 3.86 seconds |              |
|           | 4      | 4.07 seconds |              |
|           | 5      | 4.86 seconds |              |
|           | 6      | 3.75 seconds |              |
|           |        |              | 3.88         |

**Conclusions**

The execution time is more consistent and faster when using the Windows **High performance** power plan.

#### Using integer vs. strings in formulas

This test assessed the impact of modifying the R code to avoid a common problem with string factors. Specifically, a model was trained using `rxLinMod` using two tables: in the first, factors are stored as strings; in the second table, factors are stored as integers.

+ For the *airline* table, the [DayOfWeek] column contains strings. The _colInfo_ parameter was used to specify the factor levels (Monday, Tuesday, ...)

+  For the *airlineWithIndex* table, [DayOfWeek] is an integer. The _colInfo_ parameter was not specified.

+ In both cases, the same formula was used: `ArrDelay ~ CRSDepTime + DayOfWeek`.

| Table name          | Test name   | Average time |
|---------------------|-------------|--------------|
| *Airline*           | *FactorCol* | 10.72        |
| *airlineWithIntCol* | *IntCol*    | 3.4475       |

**Conclusions**

There is a clear benefit when using integers rather than strings for factors.

### Avoiding transformation functions

In this test, a model was trained using `rxLinMod`, but the code was changed between the two runs:

+ In the first run, a transformation function was applied as part of model building. 
+ In the second run, the feature values were precomputed and available, so that no transformation function was required.

| Test name             | Average time |
|-----------------------|--------------|
| WithTransformation    | 5.1675       |
| WithoutTransformation | 4.7          |

**Conclusions**

Training time was shorter when **not** using a transformation function. In other words, the model was trained faster when using columns that are pre-computed and persisted in the table.

The savings is expected to be greater if there were many more transformations and the data set were larger (\> 100M).

### Using columnar store

This test assessed the performance benefits of using a columnar data store and index. The same model was trained using `rxLinMod` and no data transformations.

+ In the first run, the data table used a standard row store.
+ In the second run, a column store was used.

| Table name         | Test name | Average time |
|--------------------|-----------|--------------|
| *airlineWithIndex* | RowStore  | 4.67         |
| *airlineColumnar*  | ColStore  | 4.555        |

**Conclusions**

Performance is better with the columnar store than with the standard row store. A significant difference in performance can be expected on larger data sets (\> 100 M).

### Effect of using the cube parameter

The purpose of this test was to determine whether the option in RevoScaleR for using the precomputed **cube** parameter can improve performance. A model was trained using `rxLinMod`, using this formula:

```R
ArrDelay ~ Origin:DayOfWeek + Month + DayofMonth + CRSDepTime
```

In the table, the factors *DayOfWeek* is stored as a string.

| Test name     | Cube parameter | numTasks | Average time | Single-row predict (ArrDelay_Pred) |
|---------------|----------------|----------|--------------|---------------------------------|
| CubeArgEffect | `cube = F`     | 1        | 91.0725      | 9.959204                        |
|               |                | 4        | 44.09        | 9.959204                        |
|               | `cube = T`     | 1        | 21.1125      | 9.959204                        |
|               |                | 4        | 8.08         | 9.959204                        |

**Conclusions**

The use of the cube parameter argument clearly improves performance.

### Effect of changing maxDepth for rxDTree models

In this experiment, the `rxDTree` algorithm was used to create a model on the *airlineColumnar* table. For this test *numTasks* was set to 4. Several different values for *maxDepth* were used to demonstrate how altering tree depth affects run time.

| Test name       | maxDepth | Average time |
|-----------------|----------|--------------|
| TreeDepthEffect | 1        | 10.1975      |
|                 | 2        | 13.2575      |
|                 | 4        | 19.27        |
|                 | 8        | 45.5775      |
|                 | 16       | 339.54       |

**Conclusions**

As the depth of the tree increases, the total number of nodes increases exponentially. The elapsed time for creating the model also increased significantly.

### Prediction on a stored model

The purpose of this test was to determine the performance impacts on scoring when the trained model is saved to a SQL Server table rather than generated as part of the currently executing code. For scoring, the stored model is loaded from the database and predictions are created using a one-row data frame in memory (local compute context).

The test results show the time to save the model, and the time taken to load the model and predict.

| Table name | Test name | Average time (to train model) | Time to save/load model|
|------------|------------|------------|------------|
| airline    | SaveModel| 21.59| 2.08|
| airline    | LoadModelAndPredict | | 2.09 (includes time to predict) |

**Conclusions**

Loading a trained model from a table is clearly a faster way to do prediction. We recommend that you avoid creating the model and performing scoring all in the same script.

## Case study: Optimization for the resume-matching task

The resume-matching model was developed by Microsoft data scientist Ke Huang to test the performance of R code in SQL Server, and by doing so help data scientists create scalable, enterprise-level solutions.

### Methods

Both the RevoScaleR and MicrosoftML packages were used to train a predictive model in a complex R solution involving large datasets. SQL queries and R code were identical in all tests. Tests were conducted on a single Azure VM with SQL Server installed. The author then compared scoring times with and without the following optimizations provided by SQL Server:

- In-memory tables
- Soft-NUMA
- Resource Governor

To assess the effect of soft-NUMA on R script execution, the data science team tested the solution on an Azure virtual machine with 20 physical cores. On these physical cores, four soft-NUMA nodes were created automatically, such that each node contained five cores.

CPU affinitization was enforced in the resume-matching scenario, to assess the impact on R jobs. Four **SQL resource pools** and four **external resource pools** were created, and CPU affinity was specified to ensure that the same set of CPUs would be used in each node.

Each of the resource pools was assigned to a different workload group, to optimize hardware utilization. The reason is that Soft-NUMA and CPU affinity cannot divide physical memory in the physical NUMA nodes; therefore, by definition all soft NUMA nodes that are based on the same physical NUMA node must use memory in the same OS memory block. In other words, there is no
memory-to-processor affinity.

The following process was used to create this configuration:

1. Reduce the amount of memory allocated by default to SQL Server.

2. Create four new pools for running the R jobs in parallel.

3. Create four workload groups such that each workload group is associated with a resource pool.

4. Restart Resource Governor with the new workload groups and assignments.

5. Create a user-defined classifier function (UDF) to assign different tasks on different workload groups.

6. Update the Resource Governor configuration to use the function for appropriate workload groups.

### Results

The configuration that had the best performance in the resume-matching study was as follows:

-   Four internal resource pools (for SQL Server)

-   Four external resource pools (for external script jobs)

-   Each resource pool is associated with a specific workload group

-   Each resource pool is assigned to different CPUs

-   Maximum internal memory usage (for SQL Server) = 30%

-   Maximum memory for use by R sessions = 70%

For the resume-matching model, external script use was heavy and there were no other database engine services running. Therefore, the resources allocated to external scripts were increased to 70%, which proved the best configuration for script performance.

This configuration was arrived at by experimenting with different values. If you use different hardware or a different solution, the optimum configuration might be different. Always experiment to find the best configuration for your case!

In the optimized solution, 1.1 million rows of data (with 100 features) were scored in under 8.5 seconds on a 20-core computer. Optimizations significantly improved the performance in terms of scoring time.

The results also suggested that the **number of features** had a significant impact on the scoring time. The improvement was even more prominent when more features were used in the prediction model.

We recommend that you read this blog article and the accompanying tutorial for a detailed discussion.

-   [Optimization tips and tricks for machine learning in SQL Server](https://azure.microsoft.com/blog/optimization-tips-and-tricks-on-azure-sql-server-for-machine-learning-services/)

Many users have noted that there is a small pause as the R (or Python) runtime is loaded for the first time. For this reason, as described in these tests, the time for the first run is often measured but later discarded. Subsequent caching might result in notable performance differences between the first and second runs. There is also some overhead when data is moved between SQL Server and the external runtime, particularly if data is passed over the network rather than being loaded directly from SQL Server.

For all these reasons, there is no single solution for mitigating this initial loading time, as the performance impact varies significantly depending on the task. For example, caching is performed for single-row scoring in batches; hence, successive scoring operations are much faster and neither the model nor the R runtime is reloaded. You can also use [native scoring](../sql-native-scoring.md) to avoid loading the R runtime entirely.

For training large models, or scoring in large batches, the overhead might be minimal in comparison to the gains from avoiding data movement or from streaming and parallel processing. See these recent blogs and samples for additional performance guidance:

+ [Loan classification using SQL Server 2016 R Services](https://blogs.msdn.microsoft.com/microsoftrservertigerteam/2016/09/27/loan-classification-using-sql-server-2016-r-services/)
+ [Early customer experiences with R Services](https://blogs.msdn.microsoft.com/sqlcat/2016/06/16/early-customer-experiences-with-sql-server-r-services/)
+ [Using R to detect fraud at 1 million transactions per second](https://blog.revolutionanalytics.com/2016/09/fraud-detection.html/)

## Resources

The following are links to information, tools, and scripts used in the development of these tests.

+ Performance testing scripts and links to the data: [Sample data and scripts for SQL Server optimizations study](https://github.com/Microsoft/SQL-Server-R-Services-Samples/tree/master/PerfTuning)

+ Article describing the resume-matching solution: [Optimization tip and tricks for SQL Server R Services](https://azure.microsoft.com/blog/optimization-tips-and-tricks-on-azure-sql-server-for-machine-learning-services/)

+ Scripts used in SQL optimization for resume-matching solution: [GitHub repository](https://github.com/Microsoft/SQL-Server-R-Services-Samples/tree/master/SQLOptimizationTips)

### Learn about Windows server management

+ [How to determine the appropriate page file size for 64-bit versions of Windows](https://support.microsoft.com/kb/2860880)

+ [Understanding NUMA](https://technet.microsoft.com/library/ms178144.aspx)

+ [How SQL Server supports NUMA](https://technet.microsoft.com/library/ms180954.aspx)

+ [Soft NUMA](https://docs.microsoft.com/sql/database-engine/configure-windows/soft-numa-sql-server)

### Learn about SQL Server optimizations

+ [Reorganize and Rebuild Indexes](../../relational-databases/indexes/reorganize-and-rebuild-indexes.md)

+ [Introduction to memory-optimized tables](https://docs.microsoft.com/sql/relational-databases/in-memory-oltp/introduction-to-memory-optimized-tables)

+ [Demonstration: Performance improvement of in-memory OLTP](https://docs.microsoft.com/sql/relational-databases/in-memory-oltp/demonstration-performance-improvement-of-in-memory-oltp)

+ [Data compression](../../relational-databases/data-compression/data-compression.md)

+ [Enable Compression on a Table or Index](../../relational-databases/data-compression/enable-compression-on-a-table-or-index.md)

+ [Disable Compression on a Table or Index](../../relational-databases/data-compression/disable-compression-on-a-table-or-index.md)

### Learn about managing SQL Server

+ [Monitor and Tune for Performance](../../relational-databases/performance/monitor-and-tune-for-performance.md)

+ [Resource Governor](../../relational-databases/resource-governor/resource-governor.md)

+ [Introducing Resource Governor](https://technet.microsoft.com/library/bb895232.aspx)

+ [Resource Governance for R Services](resource-governance-for-r-services.md)

+ [How to Create a resource pool for R](how-to-create-a-resource-pool-for-r.md)

+ [An example of configuring Resource Governor](https://blog.sqlauthority.com/2012/06/04/sql-server-simple-example-to-configure-resource-governor-introduction-to-resource-governor/)

### Tools

+ [DISKSPD storage load generator/performance test tool](https://github.com/microsoft/diskspd)

+ [FSUtil utility reference](https://technet.microsoft.com/library/cc753059.aspx)


## Other articles in this series

[Performance tuning for R - introduction](sql-server-r-services-performance-tuning.md)

[Performance tuning for R - SQL Server configuration](sql-server-configuration-r-services.md)

[Performance tuning for R - R code and data optimization](r-and-data-optimization-r-services.md)

[Performance Tuning - case study results](performance-case-study-r-services.md)
