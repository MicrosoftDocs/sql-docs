---
title: "Processing Analysis Services Objects | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "OLAP objects [Analysis Services], processing"
  - "OLAP objects [Analysis Services]"
ms.assetid: c7e1f66f-16ca-43da-b8c7-4d3e1fa8b58d
author: minewiskan
ms.author: owend
manager: craigg
---
# Processing Analysis Services Objects
  Processing affects the following [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] object types: [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] databases, cubes, dimensions, measure groups, partitions, and data mining structures and models. For each object, you can specify the level of processing for the object, or you can specify the Process Default option to enable [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] to automatically select the optimal level of processing. For more information about the different levels of processing for each object, see [Processing Options and Settings &#40;Analysis Services&#41;](processing-options-and-settings-analysis-services.md).  
  
 You should be aware of the consequences of processing behavior in order to reduce the occurrence of negative repercussions. For example, fully processing a dimension automatically sets all partitions dependent on that dimension to an unprocessed state. This causes affected cubes to become unavailable for query until the dependent partitions are processed.  
  
 This topic includes the following sections:  
  
 [Processing a Database](#bkmk_procdb)  
  
 [Processing a Dimension](#bkmk_procdim)  
  
 [Processing a Cube](#bkmk_proccube)  
  
 [Processing a Measure Group](#bkmk_procmeasure)  
  
 [Processing a Partition](#bkmk_procpartition)  
  
 [Processing Data Mining Structures and Models](#bkmk_procdm)  
  
##  <a name="bkmk_procdb"></a> Processing a Database  
 In [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], a database contains objects but not data. When you process a database, you direct the server to recursively process those objects that store data in the model, such as dimensions, partitions, mining structures, and mining models.  
  
 When you process a database, some or all partitions, dimensions, and mining models that the database contains are processed. The actual processing type varies depending on the state of each object and the processing option that you select. For more information, see [Processing Options and Settings &#40;Analysis Services&#41;](processing-options-and-settings-analysis-services.md).  
  
##  <a name="bkmk_proccube"></a> Processing a Cube  
 A cube can be thought of as a wrapper object for measure groups and partitions. A cube is made of dimensions in addition to one or more measures, which are stored in partitions. Dimensions define how data is laid out in the cube. When you process a cube, an SQL query is issued to retrieve values from the fact table to populate each member in the cube with appropriate measure values. For any specific path to a node in the cube, there is a value or a calculable value.  
  
 When you process a cube, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] processes any unprocessed dimensions in the cube, and some or all partitions within the measure groups in the cube. The specifics depend on the state of the objects when processing starts and the processing option that you select. For more information about processing options, see [Processing Options and Settings &#40;Analysis Services&#41;](processing-options-and-settings-analysis-services.md).  
  
 Processing a cube creates machine-readable files that store relevant fact data. If there are aggregations created, they are stored in aggregation data files. The cube is then available for browsing from the Object Explorer in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] or Solution Explorer in [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)]  
  
##  <a name="bkmk_procdim"></a> Processing a Dimension  
 When you process a dimension, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] formulates and runs queries against dimension tables to return information that is required for processing.  
  
|Country|Sales Region|State|  
|-------------|------------------|-----------|  
|United States|West|California|  
|United States|West|Oregon|  
|United States|West|Washington|  
  
 The processing itself turns the tabular data into usable hierarchies. These hierarchies are fully articulated member names that are internally represented by unique numeric paths. The following example is a text representation of a hierarchy.  
  
||  
|-|  
|[United States]|  
|[United States].[West]|  
|[United States].[West].[California]|  
|[United States].[West].[Oregon]|  
|[United States].[West].[Washington]|  
  
 Dimension processing does not create or update calculated members, which are defined at the cube level. Calculated members are affected when the cube definition is updated. Also, dimension processing does not create or update aggregations. However, dimension processing can cause aggregations to be dropped. Aggregations are created or updated only during partition processing.  
  
 When you process a dimension, be aware that the dimension might be used in several cubes. When you process the dimension, those cubes are marked as unprocessed and become unavailable for queries. To process both the dimension and the related cubes at the same time, use the batch processing settings. For more information, see [Batch Processing &#40;Analysis Services&#41;](batch-processing-analysis-services.md).  
  
##  <a name="bkmk_procmeasure"></a> Processing a Measure Group  
 When you process a measure group, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] processes some or all partitions within the measure group, and any unprocessed dimensions that participate in the measure group. Specifics of the processing job depend on the processing option that you select. You can process one or more measure groups in [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] without affecting other measure groups in a cube.  
  
> [!NOTE]  
>  You can process individual measure groups programmatically, or by using [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)]. You cannot process individual measure groups in [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)]; however, you can process by partition.  
  
##  <a name="bkmk_procpartition"></a> Processing a Partition  
 Effective administration of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] involves the practice of partitioning data. Partition processing is unique because it involves consideration of hard disk use and space constraints, combined with data structure limitations imposed by [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. To keep query response times fast and processing throughput high, you have to regularly create, process, and merge partitions. It is very important to recognize and manage against the chance of integrating redundant data during partition merging. For more information, see [Merge Partitions in Analysis Services &#40;SSAS - Multidimensional&#41;](merge-partitions-in-analysis-services-ssas-multidimensional.md).  
  
 When you process a partition, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] processes the partition and any unprocessed dimensions that exist in the partition, depending on the processing option that you select. Using partitions offers several advantages for processing. You can process a partition without affecting other partitions in a cube. Partitions are useful for storing data that is subject to cell writeback. Writeback is a feature that enables the user to perform what-if analysis by writing new data back into the partition to see the effect of projected changes. A writeback partition is required if you use the cell writeback feature of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. Processing partitions in parallel is useful because [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] uses the processing power more efficiently and can significantly reduce total processing time. You can also process partitions sequentially.  
  
##  <a name="bkmk_procdm"></a> Processing Data Mining Structures and Models  
 A mining structure defines the data domain from which data-mining models will be built. One mining structure can contain more than one mining model. You can process a mining structure separately from its associated mining models. When you process a mining structure separately, it is populated with the training data from your data source.  
  
 When a data mining model is processed, the training data passes through the mining model algorithms, trains the model using the data mining algorithm, and builds the content. For more information about the data mining model object, see [Mining Structures &#40;Analysis Services - Data Mining&#41;](../data-mining/mining-structures-analysis-services-data-mining.md).  
  
 For more information about processing mining structures and models, see [Processing Requirements and Considerations &#40;Data Mining&#41;](../data-mining/processing-requirements-and-considerations-data-mining.md).  
  
## See Also  
 [Tools and Approaches for Processing &#40;Analysis Services&#41;](tools-and-approaches-for-processing-analysis-services.md)   
 [Batch Processing &#40;Analysis Services&#41;](batch-processing-analysis-services.md)   
 [Multidimensional Model Object Processing](processing-a-multidimensional-model-analysis-services.md)  
  
  
