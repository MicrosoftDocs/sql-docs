---
title: "Partitions and DirectQuery Mode (SSAS Tabular) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 5f179ba9-6efb-46ae-90e5-945bbfddb719
author: minewiskan
ms.author: owend
manager: craigg
---
# Partitions and DirectQuery Mode (SSAS Tabular)
  This section explains how partitions are used in DirectQuery models. For more general information about partitions in tabular models, see [Partitions &#40;SSAS Tabular&#41;](partitions-ssas-tabular.md).  
  
 For instructions on how to change the partition that is used, or view information about the partition, see [Change the DirectQuery Partition &#40;SSAS Tabular&#41;](../change-the-directquery-partition-ssas-tabular.md).  
  
## Using Partitions in DirectQuery Mode  
 For each table, you must specify a single partition to use as the DirectQuery data source.  If there are multiple partitions, when you switch the model to enable DirectQuery mode, by default the first partition that was created in the table is flagged as the DirectQuery partition. You can change this later by using the Partition Manager in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)].  
  
 Why allow only a single partition in DirectQuery mode?  
  
 In tabular models (as in OLAP models), the partitions of a table are defined by SQL queries. The developer who creates the partition definition is responsible for ensuring that partitions do not overlap. Analysis Services does not check whether records belong in one or multiple partitions.  
  
 Partitions in a cached tabular model behave the same way. If you are using an in-memory model, while the cache is being accessed, DAX formulas are evaluated for each partition, and the results are combined. However, when a tabular model uses DirectQuery mode, it would be impossible to evaluate multiple partitions, combine the results, and convert that into a SQL statement for sending to the relational data store. Doing so could lead to unacceptable loss of performance, as well as potential inaccuracies as the results are aggregated.  
  
 Therefore, for queries answered in DirectQuery mode, the server uses a single partition that has been marked as the primary partition for DirectQuery access, called the *DirectQuery partition*.  The SQL query specified in the definition of this partition defines the complete set of data that can be used to answer queries in DirectQuery mode.  
  
 If you do not explicitly define a partition, the engine simply issues a SQL query to the entire relational data source, performs any set-based operations dictated by the DAX formula, and returns the query results.  
  
 If you have multiple partitions in a table, and select one partition as the DirectQuery partition, by default all other partitions are marked as being for in-memory use only.  
  
## Partitions in Cached Models and in DirectQuery Models  
 When you configure a DirectQuery partition, you must specify processing options for the partition.  
  
 There are two processing options for the DirectQuery partition. To set this property, use the **Partition Manager** in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], or [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], and select the **Processing Option** property. The following table lists the values of this property, and describes the effects of each value when combined with the DirectQueryUsage property on the connection string:  
  
|**DirectQueryUsage** property|**Processing Option** property|Notes|  
|-----------------------------------|------------------------------------|-----------|  
|DirectQuery|Never process this partition|When the model is using DirectQuery only, processing is never necessary.<br /><br /> In hybrid models, you can configure the DirectQuery partition to never be processed. For example, if you are operating over a very large data set and do not want the full results added to the cache, you can specify that the DirectQuery partition include the union of results for all other partitions in the table, and then never process the union. Queries that go to the relational source will not be affected, and queries against cached data will combine data from the other partitions|  
|InMemory With DirectQuery|Allow partition to be processed|If the model is using hybrid mode, you should use the same partition for queries against the in-memory and queries against the DirectQuery data source.|  
  
## See Also  
 [Partitions &#40;SSAS Tabular&#41;](partitions-ssas-tabular.md)  
  
  
