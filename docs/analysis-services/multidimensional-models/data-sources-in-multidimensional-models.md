---
title: "Data Sources in Multidimensional Models | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: multidimensional-models
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Data Sources in Multidimensional Models
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  All data that you import or load into a multidimensional model originates from an external data source. Typically, source data is from a data warehouse designed for reporting purposes, but it could come from any relational database that is accessed directly or indirectly through an intermediary, such as an [!INCLUDE[ssIS](../../includes/ssis-md.md)] package.  
  
 A **data source** object in [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] specifies a direct connection to an external data source. In addition to physical location, a data source object specifies the connection string, data provider, credentials, and other properties that control connection behavior.  
  
 Information provided by the data source object is used during the following operations:  
  
-   Get schema information and other metadata used to generate data source views into a model.  
  
-   Query or load data into a model during processing.  
  
-   Run queries against multidimensional or data mining models that use ROLAP storage mode.  
  
-   Read or write to remote partitions.  
  
-   Connect to linked objects, as well as synchronize from target to source.  
  
-   Perform write back operations that update fact table data stored in a relational database.  
  
 When building a multidimensional model from the bottom up, you start by creating the data source object, and then use it to generate the next object, a **data source view**. A data source view is the data abstraction layer in the model. It is typically created after the data source object, using the schema of the source database as the basis. However, you can choose other ways to build a model, including starting with cubes and dimensions, and generating the schema that best supports your design.  
  
 Regardless of how you build it, each model requires at least one data source object that specifies a connection to source data. You can create multiple data source objects in a single model to use data from different sources or vary connection properties for specific objects.  
  
 Data source objects can be managed independently of other objects in your model. After you create a data source, you can change its properties later, and then preprocess the model to ensure the data is retrieved correctly.  
  
## Related Topics and Tasks  
  
|Topic|Description|  
|-----------|-----------------|  
|[Supported Data Sources &#40;SSAS - Multidimensional&#41;](../../analysis-services/multidimensional-models/supported-data-sources-ssas-multidimensional.md)|Describes the types of data sources that can be used in a multidimensional model.|  
|[Create a Data Source &#40;SSAS Multidimensional&#41;](../../analysis-services/multidimensional-models/create-a-data-source-ssas-multidimensional.md)|Explains how to add a data source object to a multidimensional model.|  
|[Delete a Data Source in Solution Explorer &#40;SSAS Multidimensional&#41;](../../analysis-services/multidimensional-models/delete-a-data-source-in-solution-explorer-ssas-multidimensional.md)|Use this procedure to delete a data source object from a multidimensional model.|  
|[Set Data Source Properties &#40;SSAS Multidimensional&#41;](../../analysis-services/multidimensional-models/set-data-source-properties-ssas-multidimensional.md)|Describes each property and explains how to set each one.|  
|[Set Impersonation Options &#40;SSAS - Multidimensional&#41;](../../analysis-services/multidimensional-models/set-impersonation-options-ssas-multidimensional.md)|Explains how to configure options in the Impersonation Information dialog box.|  
  
## See Also  
 [Database Objects &#40;Analysis Services - Multidimensional Data&#41;](../../analysis-services/multidimensional-models/olap-logical/database-objects-analysis-services-multidimensional-data.md)   
 [Logical Architecture &#40;Analysis Services - Multidimensional Data&#41;](../../analysis-services/multidimensional-models/olap-logical/understanding-microsoft-olap-logical-architecture.md)   
 [Data Source Views in Multidimensional Models](../../analysis-services/multidimensional-models/data-source-views-in-multidimensional-models.md)   
 [Data Sources and Bindings &#40;SSAS Multidimensional&#41;](../../analysis-services/multidimensional-models/data-sources-and-bindings-ssas-multidimensional.md)  
  
  
