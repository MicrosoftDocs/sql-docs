---
title: "Processing Requirements and Considerations (Data Mining) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "data mining [Analysis Services], objects"
  - "mining structures [Analysis Services], processing"
  - "mining models [Analysis Services], processing"
ms.assetid: f7331261-6f1c-4986-b2c7-740f4b92ca44
author: minewiskan
ms.author: owend
manager: craigg
---
# Processing Requirements and Considerations (Data Mining)
  This topic describes some technical considerations to keep in mind when processing data mining objects. For a general explanation of what processing is, and how it applies to data mining, see [Processing Data Mining Objects](processing-data-mining-objects.md).  
  
 [Queries on Relational Store](#bkmk_QueryReqs)  
  
 [Processing Mining Structures](#bkmk_ProcessStructures)  
  
 [Processing Mining Models](#bkmk_ProcessModels)  
  
##  <a name="bkmk_QueryReqs"></a> Queries on the Relational Store during Processing  
 For data mining, there are three phases to processing: querying the source data, determining raw statistics, and using the model definition and algorithm to train the mining model.  
  
 The [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] server issues queries to the database that provides the raw data. This database might be an instance of [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] or an earlier version of the SQL Server database engine. When you process a data mining structure, the data in the source is transferred to the mining structure and persisted on disk in a new, compressed format. Not every column in the data source is processed: only the columns that are included in the mining structure, as defined by the bindings.  
  
 Using this data, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] builds an index of all data and discretized columns, and creates a separate index for continuous columns. One query is issued for each nested table to create the index, and an additional query per nested table is generated to process relationships between each pair of a nested table and case table. The reason for creating multiple queries is to process a special internal multidimensional data store. You can limit the number of queries that [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] sends to the relational store by setting the server property, `DatabaseConnectionPoolMax`. For more information, see [OLAP Properties](../server-properties/olap-properties.md).  
  
 When you process the model, the model does not reread the data from the data source, but instead gets the summary of the data from the mining structure. Using the cube that was created, together with the cached index and case data has been cached, the server creates independent threads to train the models.  
  
 For more information about the editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that support Parallel Model Processing, see [Features Supported by the Editions of SQL Server 2012](https://go.microsoft.com/fwlink/?linkid=232473) (https://go.microsoft.com/fwlink/?linkid=232473).  
  
##  <a name="bkmk_ProcessStructures"></a> Processing Mining Structures  
 A mining structure can be processed together with all dependent models, or separately. Processing a mining structure separately from models can be useful when some models are expected to take a long time to process and you want to defer that operation.  
  
 For more information, see [Process a Mining Structure](process-a-mining-structure.md).  
  
 If you are concerned about conserving hard disk space, note that [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] retains mining structure caches locally. That is, it writes out all the training data to your local hard disk. If you do not want the data cached, you can change the default by setting the <xref:Microsoft.AnalysisServices.MiningStructureCacheMode> property on the mining structure to `ClearAfterProcessing`. This will destroy the cache after models are processed; however, it will also disable drillthrough on the mining structure. For more information, see [Drillthrough Queries &#40;Data Mining&#41;](drillthrough-queries-data-mining.md).  
  
 Also, if you clear the cache, you will not be able to use the holdout test set, if you defined one, and the definition of the test set partition will be lost. For more information about holdout test sets, see [Training and Testing Data Sets](training-and-testing-data-sets.md).  
  
##  <a name="bkmk_ProcessModels"></a> Processing Mining Models  
 You can process a mining model separately from its associated mining structure, or you can process all models that are based on the structure, together with the structure.  
  
 For more information, see [Process a Mining Model](process-a-mining-model.md).  
  
 However, in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] and [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], you cannot multiselect mining models to process with the structure. If you need to control which models are processed, you must select them individually, or use XMLA or DMX to process models serially.  
  
## When Reprocessing is Required  
 You must process the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] models that you define before you can start to work with them. You must also reprocess the mining models whenever you change the mining model structure, update the training data, change an existing mining model, or add a new mining model to the structure.  
  
 Mining models are also processed in these scenarios:  
  
 **Deployment of a project**: Depending on the project settings and the current state of the project, the mining models in the project are typically processed in full when the project is deployed.  
  
 When you initiate deployment, processing starts automatically, unless there is a previously processed version on the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] server and there have been no structural changes. You can deploy a project by selecting **Deploy solution** from the drop-down list or by pressing the F5 key. You can  
  
 For more information about how to set [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] deployment properties that control how mining models are deployed, see [Deployment of Data Mining Solutions](deployment-of-data-mining-solutions.md).  
  
 **Moving a mining model**: When you move a mining model by using the EXPORT command, only the definition of the model is exported, which includes the name of the mining structure that is expected to provide data to the model.  
  
 Reprocessing requirements for the following scenarios using the EXPORT and IMPORT commands:  
  
-   The mining structure exists on the target instance and the mining structure is in an unprocessed state.  
  
     Both the structure and model must be reprocessed.  
  
-   The mining structure exists on the target instance and the mining structure has been processed. Only the mining model was exported.  
  
     The model can be used without processing.  
  
-   The mining structure definition was also exported by using the WITH DEENDENCIES keyword.  
  
     Both the structure and model must be reprocessed.  
  
 For more information, see [Export and Import Data Mining Objects](export-and-import-data-mining-objects.md).  
  
## See Also  
 [Mining Structures &#40;Analysis Services - Data Mining&#41;](mining-structures-analysis-services-data-mining.md)   
 [Mining Structures &#40;Analysis Services - Data Mining&#41;](mining-structures-analysis-services-data-mining.md)   
 [Multidimensional Model Object Processing](../multidimensional-models/processing-a-multidimensional-model-analysis-services.md)  
  
  
