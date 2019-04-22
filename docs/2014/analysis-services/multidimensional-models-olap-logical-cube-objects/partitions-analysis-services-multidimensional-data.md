---
title: "Partitions (Analysis Services - Multidimensional Data) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: "analysis-services"
ms.topic: "reference"
helpviewer_keywords: 
  - "storage [Analysis Services], partitions"
  - "incremental updates [Analysis Services]"
  - "data sources [Analysis Services], partitions"
  - "data storage [Analysis Services]"
  - "aggregations [Analysis Services], partitions"
  - "OLAP objects [Analysis Services], partitions"
  - "storing data [Analysis Services], partitions"
  - "partitions [Analysis Services], about partitions"
  - "cubes [Analysis Services], partitions"
  - "partitions [Analysis Services]"
  - "remote partitions [Analysis Services]"
  - "measure groups [Analysis Services], partitions"
ms.assetid: cd10ad00-468c-4d49-9f8d-873494d04b4f
author: minewiskan
ms.author: owend
manager: craigg
---
# Partitions (Analysis Services - Multidimensional Data)
  A partition is a container for a portion of the measure group data. Partitions are not seen from MDX queries; all queries reflect the whole content of the measure group, regardless of how many partitions are defined for the measure group. The data content of a partition is defined by the query bindings of the partition, and by the slicing expression.  
  
 A simple <xref:Microsoft.AnalysisServices.Partition> object is composed of: basic information, slicing definition, aggregation design, and others. Basic information includes the name of the partition, the storage mode, the processing mode, and others. The slicing definition is an MDX expression specifying a tuple or a set. The slicing definition has the same restrictions as the StrToSet MDX function. Together with the CONSTRAINED parameter, the slicing definition can use dimension, hierarchy, level and member names, keys, unique names, or other named objects in the cube, but cannot use MDX functions. The aggregation design is a collection of aggregation definitions that can be shared across multiple partitions. The default is taken from the parent cube's aggregation design.  
  
 Partitions are used by [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] to manage and store data and aggregations for a measure group in a cube. Every measure group has at least one partition; this partition is created when the measure group is defined. When you create a new partition for a measure group, the new partition is added to the set of partitions that already exist for the measure group. The measure group reflects the combined data that is contained in all its partitions. This means that you must ensure that the data for a partition in a measure group is exclusive of the data for any other partition in the measure group to ensure that data is not reflected in the measure group more than once. The original partition for a measure group is based on a single fact table in the data source view of the cube. When there are multiple partitions for a measure group, each partition can reference a different table in either the data source view or in the underlying relational data source for the cube. More than one partition in a measure group can reference the same table, if each partition is restricted to different rows in the table.  
  
 Partitions are a powerful and flexible means of managing cubes, especially large cubes. For example, a cube that contains sales information can contain a partition for the data of each past year and also partitions for each quarter of the current year. Only the current quarter partition needs to be processed when current information is added to the cube; processing a smaller amount of data will improve processing performance by decreasing processing time. At the end of the year the four quarterly partitions can be merged into a single partition for the year and a new partition created for the first quarter of the new year. Further, this new partition creation process can be automated as part of your data warehouse loading and cube processing procedures.  
  
 Partitions are not visible to business users of the cube. However, administrators can configure, add, or drop partitions. Each partition is stored in a separate set of files. The aggregate data of each partition can be stored on the instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] where the partition is defined, on another instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], or in the data source that is used to supply the partition's source data. Partitions allow the source data and aggregate data of a cube to be distributed across multiple hard drives and among multiple server computers. For a cube of moderate to large size, partitions can greatly improve query performance, load performance, and ease of cube maintenance. For more information about remote partitions, see [Remote Partitions](partitions-remote-partitions.md).  
  
 The storage mode of each partition can be configured independently of other partitions in the measure group. Partitions can be stored by using any combination of options for source data location, storage mode, proactive caching, and aggregation design. Options for real-time OLAP and proactive caching let you balance query speed against latency when you design a partition. Storage options can also be applied to related dimensions and to facts in a measure group. This flexibility lets you design cube storage strategies appropriate to your needs. For more information, see [Partition Storage Modes and Processing](partitions-partition-storage-modes-and-processing.md), [Aggregations and Aggregation Designs](aggregations-and-aggregation-designs.md) and [Proactive Caching &#40;Partitions&#41;](partitions-proactive-caching.md).  
  
## Partition Structure  
 The structure of a partition must match the structure of its measure group, which means that the measures that define the measure group must also be defined in the partition, along with all related dimensions. Therefore, when a partition is created, it automatically inherits the same set of measures and related dimensions that were defined for the measure group.  
  
 However, each partition in a measure group can have a different fact table, and these fact tables can be from different data sources. When different partitions in a measure group have different fact tables, the tables must be sufficiently similar to maintain the structure of the measure group, which means that the processing query returns the same columns and same data types for all fact tables for all partitions.  
  
 When fact tables for different partitions are from different data sources, the source tables for any related dimensions, and also any intermediate fact tables, must also be present in all data sources and must have the same structure in all the databases. Also, all dimension table columns that are used to define attributes for cube dimensions related to the measure group must be present in all of the data sources. There is no need to define all the joins between the source table of a partition and a related dimension table if the partition source table has the identical structure as the source table for the measure group.  
  
 Columns that are not used to define measures in the measure group can be present in some fact tables but absent in others. Similarly, columns that are not used to define attributes in related dimension tables can be present in some databases but absent in others. Tables that are not used for either fact tables or related dimension tables can be present in some databases but absent in others.  
  
## Data Sources and Partition Storage  
 A partition is based either on a table or view in a data source, or on a table or named query in a data source view. The location where partition data is stored is defined by the data source binding. Typically, you can partition a measure group horizontally or vertically:  
  
-   In a horizontally partitioned measure group, each partition in a measure group is based on a separate table. This kind of partitioning is appropriate when data is separated into multiple tables. For example, some relational databases have a separate table for each month's data.  
  
-   In a vertically partitioned measure group, a measure group is based on a single table, and each partition is based on a source system query that filters the data for the partition. For example, if a single table contains several months data, the measure group could still be partitioned by month by applying a Transact-SQL WHERE clause that returns a separate month's data for each partition.  
  
 Each partition has storage settings that determine whether the data and aggregations for the partition are stored in the local instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] or in a remote partition using another instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. The storage settings can also specify the storage mode and whether proactive caching is used to control latency for a partition. For more information, see [Partition Storage Modes and Processing](partitions-partition-storage-modes-and-processing.md), [Proactive Caching &#40;Partitions&#41;](partitions-proactive-caching.md), and [Remote Partitions](partitions-remote-partitions.md).  
  
## Incremental Updates  
 When you create and manage partitions in multiple-partition measure groups, you must take special precautions to guarantee that cube data is accurate. Although these precautions do not usually apply to single-partition measure groups, they do apply when you incrementally update partitions. When you incrementally update a partition, a new temporary partition is created that has a structure identical to that of the source partition. The temporary partition is processed and then merged with the source partition. Therefore, you must ensure that the processing query that populates the temporary partition does not duplicate any data already present in an existing partition.  
  
## See Also  
 [Configure Measure Properties](../multidimensional-models/configure-measure-properties.md)   
 [Cubes in Multidimensional Models](../multidimensional-models/cubes-in-multidimensional-models.md)  
  
  
