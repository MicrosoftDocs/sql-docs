---
title: "Define partitions in Analysis Services DirectQuery models | Microsoft Docs"
ms.date: 05/07/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: tabular-models
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Define partitions in DirectQuery models
[!INCLUDE[ssas-appliesto-sqlas-aas](../../includes/ssas-appliesto-sqlas-aas.md)]
  This section explains how partitions are used in DirectQuery models. For more general information about partitions in tabular models, see [Partitions](../../analysis-services/tabular-models/partitions-ssas-tabular.md).  
  
> [!NOTE]  
>  Although a table can have multiple partitions, in DirectQuery mode, only one of them can be designated for use in query execution. The single partition requirement applies to DirectQuery models at all compatibility levels.  
  
## Using Partitions in DirectQuery Mode  
 For each table, you must specify a single partition to use as the DirectQuery data source.  If there are multiple partitions, when you switch the model to enable DirectQuery mode, by default the first partition that was created in the table is flagged as the DirectQuery partition. You can change this later by using the Partition Manager in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)].  
  
 Why allow only a single partition in DirectQuery mode?  
  
 In tabular models (as in OLAP models), the partitions of a table are defined by SQL queries. The developer who creates the partition definition is responsible for ensuring that partitions do not overlap. Analysis Services does not check whether records belong in one or multiple partitions.  
  
 Partitions in a cached tabular model behave the same way. If you are using an in-memory model, while the cache is being accessed, DAX formulas are evaluated for each partition, and the results are combined. However, when a tabular model uses DirectQuery mode, it would be impossible to evaluate multiple partitions, combine the results, and convert that into a SQL statement for sending to the relational data store. Doing so could lead to unacceptable loss of performance, as well as potential inaccuracies as the results are aggregated.  
  
 Therefore, for queries answered in DirectQuery mode, the server uses a single partition that has been marked as the primary partition for DirectQuery access, called the *DirectQuery partition*.  The SQL query specified in the definition of this partition defines the complete set of data that can be used to answer queries in DirectQuery mode.  
  
 If you do not explicitly define a partition, the engine simply issues a SQL query to the entire relational data source, performs any set-based operations dictated by the DAX formula, and returns the query results.  
  
  
## Change a DirectQuery Partition  
 Because only one partition in a table can be designated as the DirectQuery partition, by default, Analysis Services uses the first partition that was created in the table. During model project authoring, you can change the DirectQuery partition by using the Partition Manager dialog box in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]. For deployed models, you can change the DirectQuery partition by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
#### Change the DirectQuery partition for a tabular model project  
  
1.  In [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)], in the model designer, click on the table (tab) that contains the partitioned table.  
  
2.  Click on the **Table** menu, and then click **Partitions**.  
  
3.  In **Partition Manager**, the partition that is the current Direct Query partition in indicated by the prefix **(DirectQuery)** on the partition name.  
  
     Select a different partition from the **Partitions** list, and then click **Set as DirectQuery**. The **Set as DirectQuery** button is not enabled when the current DirectQuery partition is selected, and is not visible if the model has not been enabled for Direct Query mode.  
  
4.  If necessary, change the processing options, and then click **OK**.  
  
#### Change the DirectQuery partition for a deployed tabular model  
  
1.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], open the model database in Object Explorer.  
  
2.  Expand the **Tables** node, then right-click the partitioned table, and then select **Partitions**.  
  
     The partition that is designated for use with DirectQuery mode has the prefix (DirectQuery) on the partition name.  
  
3.  To change to a different partition, click the **Direct Query** toolbar icon to open the **Set DirectQuery Partition** dialog box. The DirectQuery toolbar icon is not available on models that have not been enabled for Direct Query.  
  
4.  Choose a different partition from the **Partition Name** dropdown list, and then change processing options on the partition if necessary.  
  
## Partitions in Cached Models and in DirectQuery Models  
 When you configure a DirectQuery partition, you must specify processing options for the partition.  
  
 There are two processing options for the DirectQuery partition. To set this property, use the **Partition Manager** in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], or [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], and select the **Processing Option** property. The following table lists the values of this property, and describes the effects of each value when combined with the DirectQueryUsage property on the connection string:  
  
|**Connection String** property|**Processing Option** property|Notes|  
|------------------------------------|------------------------------------|-----------|  
|DirectQuery|Never process this partition|When the model is using DirectQuery only, processing is never necessary.<br /><br /> In hybrid models, you can configure the DirectQuery partition to never be processed. For example, if you are operating over a very large data set and do not want the full results added to the cache, you can specify that the DirectQuery partition include the union of results for all other partitions in the table, and then never process the union. Queries that go to the relational source will not be affected, and queries against cached data will combine data from the other partitions|  
|DataView=Sample<br /><br /> Applies to Tabular models using sample data views|Allow partition to be processed|If the model is using sample data, you can process the table to return a filtered dataset that provides visual cues during model design.|  
|DirectQueryUsage=InMemory With DirectQuery<br /><br /> Applies to Tabular 1100 or 1103 models  running in a combination of in-memory and DirectQuery mode|Allow partition to be processed|If the model is using hybrid mode, you should use the same partition for queries against the in-memory and DirectQuery data source.|  
  
## See also  
 [Partitions](../../analysis-services/tabular-models/partitions-ssas-tabular.md)  
  
  
