---
description: "Data Flow Performance Features"
title: "Data Flow Performance Features | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "Aggregate transformation [Integration Services]"
  - "Integration Services packages, performance"
  - "performance [Integration Services]"
  - "data flow [Integration Services], troubleshooting"
  - "SQL Server Integration Services packages, performance"
  - "loading data"
  - "control flow [Integration Services], troubleshooting"
  - "SSIS packages, performance"
  - "packages [Integration Services], performance"
  - "queries [Integration Services], troubleshooting"
  - "sorting data [Integration Services]"
  - "aggregations [Integration Services]"
ms.assetid: c4bbefa6-172b-4547-99a1-a0b38e3e2b05
author: chugugrace
ms.author: chugu
---
# Data Flow Performance Features

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


  This topic provides suggestions about how to design [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] packages to avoid common performance issues. This topic also provides information about features and tools that you can use to troubleshoot the performance of packages.  
  
## Configuring the Data Flow  
 To configure the Data Flow task for better performance, you can configure the task's properties, adjust buffer size, and configure the package for parallel execution.  
  
### Configure the Properties of the Data Flow Task  
  
> [!NOTE]  
>  The properties discussed in this section must be set separately for each Data Flow task in a package.  
  
 You can configure the following properties of the Data Flow task, all of which affect performance:  
  
-   Specify the locations for temporary storage of buffer data (BufferTempStoragePath property) and of columns that contain binary large object (BLOB) data (BLOBTempStoragePath property). By default, these properties contain the values of the TEMP and TMP environment variables. You might want to specify other folders to put the temporary files on a different or faster hard disk drive, or to spread them across multiple drives. You can specify multiple directories by delimiting the directory names with semicolons.  
  
-   Define the default size of the buffer that the task uses, by setting the DefaultBufferSize property, and define the maximum number of rows in each buffer, by setting the DefaultBufferMaxRows property. Set the AutoAdjustBufferSize property to indicate whether the default size of the buffer is calculated automatically from the value of the  DefaultBufferMaxRows property. The default buffer size is 10 megabytes, with a maximum buffer size of 2^31-1 bytes. The default maximum number of rows is 10,000.  
  
-   Set the number of threads that the task can use during execution, by setting the EngineThreads property. This property provides a suggestion to the data flow engine about the number of threads to use. The default is 10, with a minimum value of 3. However, the engine will not use more threads than it needs, regardless of the value of this property. The engine may also use more threads than specified in this property, if necessary to avoid concurrency issues.  
  
-   Indicate whether the Data Flow task runs in optimized mode (RunInOptimizedMode property). Optimized mode improves performance by removing unused columns, outputs, and components from the data flow.  
  
    > [!NOTE]  
    >  A property with the same name, RunInOptimizedMode, can be set at the project level in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] to indicate that the Data Flow task runs in optimized mode during debugging. This project property overrides the RunInOptimizedMode property of Data Flow tasks at design time.  
  
### Adjust the Sizing of Buffers  
 The data flow engine begins the task of sizing its buffers by calculating the estimated size of a single row of data. Then it multiplies the estimated size of a row by the value of DefaultBufferMaxRows to obtain a preliminary working value for the buffer size.  
  
-   If AutoAdjustBufferSize is set to true, the engine data flow engine uses the calculated value as the buffer size, and the value of DefaultBufferSize is ignored.  
  
-   If AutoAdjustBufferSize is set to false, the engine data flow engine uses the following rules to determine the buffer size.  
  
    -   If the result is more than the value of DefaultBufferSize, the engine reduces the number of rows.  
  
    -   If the result is less than the internally-calculated minimum buffer size, the engine increases the number of rows.  
  
    -   If the result falls between the minimum buffer size and the value of DefaultBufferSize, the engine sizes the buffer as close as possible to the estimated row size times the value of DefaultBufferMaxRows.  
  
 When you begin testing the performance of your data flow tasks, use the default values for DefaultBufferSize and DefaultBufferMaxRows. Enable logging on the data flow task, and select the BufferSizeTuning event to see how many rows are contained in each buffer.  
  
 Before you begin adjusting the sizing of the buffers, the most important improvement that you can make is to reduce the size of each row of data by removing unneeded columns and by configuring data types appropriately.  
  
 To determine the optimum number of buffers and their size, experiment with the values of DefaultBufferSize and DefaultBufferMaxRows while monitoring performance and the information reported by the BufferSizeTuning event.  
  
 Do not increase buffer size to the point where paging to disk starts to occur. Paging to disk hinders performance more than a buffer size that has not been optimized. To determine whether paging is occurring, monitor the "Buffers spooled" performance counter in the Performance snap-in of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Management Console (MMC).  
  
### Configure the Package for Parallel Execution  
 Parallel execution improves performance on computers that have multiple physical or logical processors. To support parallel execution of different tasks in the package, [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] uses two properties: **MaxConcurrentExecutables** and **EngineThreads**.  
  
#### The MaxConcurrentExcecutables Property  
 The **MaxConcurrentExecutables** property is a property of the package itself. This property defines how many tasks can run simultaneously. The default value is -1, which means the number of physical or logical processors plus 2.  
  
 To understand how this property works, consider a sample package that has three Data Flow tasks. If you set **MaxConcurrentExecutables** to 3, all three Data Flow tasks can run simultaneously. However, assume that each Data Flow task has 10 source-to-destination execution trees. Setting **MaxConcurrentExecutables** to 3 does not ensure that the execution trees inside each Data Flow task run in parallel.  
  
#### The EngineThreads Property  
 The **EngineThreads** property is a property of each Data Flow task. This property defines how many threads the data flow engine can create and run in parallel. The **EngineThreads** property applies equally to both the source threads that the data flow engine creates for sources and the worker threads that the engine creates for transformations and destinations. Therefore, setting **EngineThreads** to 10 means that the engine can create up to ten source threads and up to ten worker threads.  
  
 To understand how this property works, consider the sample package with three Data Flow tasks. Each of Data Flow task contains ten source-to-destination execution trees. If you set EngineThreads to 10 on each Data Flow task, all 30 execution trees can potentially run simultaneously.  
  
> [!NOTE]  
>  A discussion of threading is beyond the scope of this topic. However, the general rule is not to run more threads in parallel than the number of available processors. Running more threads than the number of available processors can hinder performance because of the frequent context-switching between threads.  
  
## Configuring Individual Data Flow Components  
 To configure individual data flow components for better performance, there are some general guidelines that you can follow. There are also specific guidelines for each type of data flow component: source, transformation, and destination.  
  
### General Guidelines  
 Regardless of the data flow component, there are two general guidelines that you should follow to improve performance: optimize queries and avoid unnecessary strings.  
  
#### Optimize Queries  
 A number of data flow components use queries, either when they extract data from sources, or in lookup operations to create reference tables. The default query uses the SELECT * FROM \<tableName> syntax. This type of query returns all the columns in the source table. Having all the columns available at design time makes it possible to choose any column as a lookup, pass-through, or source column. However, after you have selected the columns to be used, you should revise the query to include only those selected columns. Removing superfluous columns makes the data flow in a package more efficient because fewer columns create a smaller row. A smaller row means that more rows can fit into one buffer, and the less work it is to process all the rows in the dataset.  
  
 To construct a query, you can type the query or use Query Builder.  
  
> [!NOTE]  
>  When you run a package in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], the Progress tab of [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer lists warnings. These warnings include identifying any data column that a source makes available to the data flow, but is not subsequently used by downstream data flow components. You can use the **RunInOptimizedMode** property to remove these columns automatically.  
  
#### Avoid Unnecessary Sorting  
 Sorting is inherently a slow operation, and avoiding unnecessary sorting can enhance the performance of the package data flow.  
  
 Sometimes the source data has already been sorted before being used by a downstream component. Such pre-sorting can occur when the SELECT query used an ORDER BY clause or when the data was inserted into the source in sorted order. For such pre-sorted source data, you can provide a hint that the data is sorted, and thereby avoid the use of a Sort transformation to satisfy the sorting requirements of certain downstream transformations. (For example, the Merge and Merge Join transformations require sorted inputs.) To provide a hint that the data is sorted, you have to do the following tasks:  
  
-   Set the **IsSorted** property on the output of an upstream data flow component to **True**.  
  
-   Specify the sort key columns on which the data is sorted.  
  
 For more information, see [Sort Data for the Merge and Merge Join Transformations](../../integration-services/data-flow/transformations/sort-data-for-the-merge-and-merge-join-transformations.md).  
  
 If you have to sort the data in the data flow, you can improve performance by designing the data flow to use as few sort operations as possible. For example, the data flow uses a Multicast transformation to copy the dataset. Sort the dataset once before the Multicast transformation runs, instead of sorting multiple outputs after the transformation.  
  
 For more information, see [Sort Transformation](../../integration-services/data-flow/transformations/sort-transformation.md), [Merge Transformation](../../integration-services/data-flow/transformations/merge-transformation.md), [Merge Join Transformation](../../integration-services/data-flow/transformations/merge-join-transformation.md), and [Multicast Transformation](../../integration-services/data-flow/transformations/multicast-transformation.md).  
  
### Sources  
  
#### OLE DB Source  
 When you use an OLE DB source to retrieve data from a view, select "SQL command" as the data access mode and enter a SELECT statement. Accessing data by using a SELECT statement performs better than selecting "Table or view" as the data access mode.  
  
### Transformations  
 Use the suggestions in this section to improve the performance of the Aggregate, Fuzzy Lookup, Fuzzy Grouping, Lookup, Merge Join, and Slowly Changing Dimension transformations.  
  
#### Aggregate Transformation  
 The Aggregate transformation includes the **Keys**, **KeysScale**, **CountDistinctKeys**, and **CountDistinctScale** properties. These properties improve performance by enabling the transformation to preallocate the amount of memory that the transformation needs for the data that the transformation caches. If you know the exact or approximate number of groups that are expected to result from a **Group by** operation, set the **Keys** and **KeysScale** properties, respectively. If you know the exact or approximate number of distinct values that are expected to result from a **Distinct count** operation, set the **CountDistinctKeys** and **CountDistinctScale** properties, respectively.  
  
 If you have to create multiple aggregations in a data flow, consider creating multiple aggregations that use one Aggregate transformation instead of creating multiple transformations. This approach improves performance when one aggregation is a subset of another aggregation because the transformation can optimize internal storage and scan incoming data only once. For example, if an aggregation uses a GROUP BY clause and an AVG aggregation, combining them into one transformation can improve performance. However, performing multiple aggregations within one Aggregate transformation serializes the aggregation operations, and therefore might not improve performance when multiple aggregations must be computed independently.  
  
#### Fuzzy Lookup and Fuzzy Grouping Transformations  
 For information about optimizing the performance of the Fuzzy Lookup and Fuzzy Grouping transformations, see the white paper, [Fuzzy Lookup and Fuzzy Grouping in SQL Server Integration Services 2005](/previous-versions/sql/sql-server-2005/administrator/ms345128(v=sql.90)).  
  
#### Lookup Transformation  
 Minimize the size of the reference data in memory by entering a SELECT statement that looks up only the columns that you need. This option performs better than selecting an entire table or view, which returns a large amount of unnecessary data.  
  
#### Merge Join Transformation  
 You no longer have to configure the value of the **MaxBuffersPerInput** property because Microsoft has made changes that reduce the risk that the Merge Join transformation will consume excessive memory. This problem sometimes occurred when the multiple inputs of the Merge Join produced data at uneven rates.  
  
#### Slowly Changing Dimension Transformation  
 The Slowly Changing Dimension Wizard and the Slowly Changing Dimension transformation are general-purpose tools that meet the needs of most users. However, the data flow that the wizard generates is not optimized for performance.  
  
 Typically, the slowest components in the Slowly Changing Dimension transformation are the OLE DB Command transformations that perform UPDATEs against a single row at a time. Therefore, the most effective way to improve the performance of the Slowly Changing Dimension transformation is to replace the OLE DB Command transformations. You can replace these transformations with destination components that save all rows to be updated to a staging table. Then, you can add an Execute SQL task that performs a single set-based Transact-SQL UPDATE against all rows at the same time.  
  
 Advanced users can design a custom data flow for slowly changing dimension processing that is optimized for large dimensions. For a discussion and example of this approach, see the section, "Unique dimension scenario," in the white paper, [Project REAL: Business Intelligence ETL Design Practices](https://www.microsoft.com/download/details.aspx?id=14582).  
  
### Destinations  
 To achieve better performance with destinations, consider using a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] destination and testing the destination's performance.  
  
#### SQL Server Destination  
 When a package loads data to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on the same computer, use a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] destination. This destination is optimized for high-speed bulk loads.  
  
#### Testing the Performance of Destinations  
 You may find that saving data to destinations takes more time than expected. To identify whether the slowness is caused by the inability of the destination to process data quickly enough, you can temporarily replace the destination with a Row Count transformation. If the throughput improves significantly, it is likely that the destination that is loading the data is causing the slowdown.  
  
### Review the Information on the Progress Tab  
 [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer provides information about both control flow and data flow when you run a package in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]. The **Progress** tab lists tasks and containers in order of execution and includes start and finish times, warnings, and error messages for each task and container, including the package itself. It also lists data flow components in order of execution and includes information about progress, displayed as percentage complete, and the number of rows processed.  
  
 To enable or disable the display of messages on the **Progress** tab, toggle the **Debug Progress Reporting** option on the **SSIS** menu. Disabling progress reporting can help improve performance while running a complex package in [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)].  
  
## Related Tasks  
  
-   [Sort Data for the Merge and Merge Join Transformations](../../integration-services/data-flow/transformations/sort-data-for-the-merge-and-merge-join-transformations.md)  
  
## Related Content  
 **Articles and Blog Posts**  
  
-   Technical article, [SQL Server 2005 Integration Services: A Strategy for Performance](/previous-versions/sql/sql-server-2005/administrator/cc966530(v=technet.10)), on technet.microsoft.com  
  
-   Technical article, [Integration Services: Performance Tuning Techniques](/previous-versions/sql/sql-server-2005/administrator/cc966529(v=technet.10)), on technet.microsoft.com  
  
-   Technical article, [Increasing Throughput of Pipelines by Splitting Synchronous Transformations into Multiple Tasks](https://download.microsoft.com/download/0/F/B/0FBFAA46-2BFD-478F-8E56-7BF3C672DF9D/SQLCAT's%20Guide%20to%20BI%20and%20Analytics.pdf), in _SQLCAT's Guide to BI and Analytics_
  
-   Technical article, [The Data Loading Performance Guide](/previous-versions/sql/sql-server-2008/dd425070(v=sql.100)), on msdn.microsoft.com.  
  
-   Technical article, [We Loaded 1TB in 30 Minutes with SSIS, and So Can You](/previous-versions/sql/sql-server-2008/dd537533(v=sql.100)), on msdn.microsoft.com.  
  
-   Technical article, [Top 10 SQL Server Integration Services Best Practices](https://techcommunity.microsoft.com/t5/datacat/top-10-sql-server-integration-services-best-practices/ba-p/305163), on sqlcat.com.  
  
-   Technical article and sample, [The "Balanced Data Distributor" for SSIS](https://www.sqlshack.com/ssis-balanced-data-distributor-overview/), on sqlcat.com.  
  
-   Blog post, [Troubleshooting SSIS Package Performance Issues](https://techcommunity.microsoft.com/t5/sql-server-integration-services/api-sample-oledb-source-and-oledb-destination/ba-p/387553), on blogs.msdn.com  
  
 **Videos**  
  
-   Video series, [Designing and Tuning for Performance your SSIS packages in the Enterprise (SQL Video Series)](https://go.microsoft.com/fwlink/?LinkId=400878)  
  
-   Video, [Tuning Your SSIS Package Data Flow in the Enterprise (SQL Server Video)](/previous-versions/ff686901(v=msdn.10)), on technet.microsoft.com  
  
-   Video, [Understanding SSIS Data Flow Buffers (SQL Server Video)](/previous-versions/ff686905(v=msdn.10)), on technet.microsoft.com  
  
-   Presentation, [How Microsoft IT Leverages SQL Server 2008 SSIS Dataflow Engine Enhancements](https://channel9.msdn.com/Shows/TechNet+Radio/TechNet-Radio-How-Microsoft-IT-Leverages-SQL-Server-2008-SSIS-Dataflow-Engine-Enhancements), on sqlcat.com.  
  
-   Video, [Balanced Data Distributor](/previous-versions/dn912438(v=msdn.10)), on technet.microsoft.com.  
  
## See Also  
 [Troubleshooting Tools for Package Development](../../integration-services/troubleshooting/troubleshooting-tools-for-package-development.md)   
 [Troubleshooting Tools for Package Execution](../../integration-services/troubleshooting/troubleshooting-tools-for-package-execution.md)  
  
