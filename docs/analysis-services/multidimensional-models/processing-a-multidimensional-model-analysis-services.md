---
title: "Processing an Analysis Services  multidimensional model | Microsoft Docs"
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
# Processing a multidimensional model (Analysis Services)
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  Processing is the step, or series of steps, in which Analysis Services loads data from a relational data source into a multidimensional model. For objects that use MOLAP storage, data is saved on disk in the database file folder. For ROLAP storage, processing occurs on demand, in response to an MDX query on an object. For objects that use ROLAP storage, processing refers to updating the cache before returning query results.  
  
 By default, processing occurs when you deploy a solution to the server. You can also process all or part of a solution, either ad hoc using tools such as [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] or [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)], or on a schedule using [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] and SQL Server Agent. When making a structural change to the model, such as removing a dimension or changing its compatibility level, you will need to process again to synchronize the physical and logical aspects of the model.  
  
 This topic includes the following sections:  
  
 [Prerequisites](#bkmk_prereq)  
  
 [Choosing a tool or approach](#bkmk_tool)  
  
 [Processing Objects](#bkmk_proc)  
  
 [Reprocessing Objects](#bkmk_reproc)  
  
##  <a name="bkmk_prereq"></a> Prerequisites  
  
-   Processing requires administrative permissions on the Analysis Services instance. If you are processing interactively from [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)] or [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], you must be a member of the server administrator role on the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance. For processing that runs unattended, for example using an SSIS package that you schedule through SQL Server Agent, the account used to run the package must be a member of the server administrator role. For more information about setting administrator permissions, see [Grant server admin rights to an  Analysis Services instance](../../analysis-services/instances/grant-server-admin-rights-to-an-analysis-services-instance.md).  
  
-   The account used to retrieve data is specified in the data source object, either as an impersonation option if you are using Windows authentication, or as the user name on the connection string if using database authentication. The account must have read permissions on relational data sources used by the model.  
  
-   The project or solution must be deployed before you can process any objects.  
  
     Initially, during the early stages of model development, deployment and processing occur together. However, you can set options to process the model later, after you deploy the solution. For more information about deployment, see [Deploy Analysis Services Projects &#40;SSDT&#41;](../../analysis-services/multidimensional-models/deploy-analysis-services-projects-ssdt.md).  
  
##  <a name="bkmk_tool"></a> Choosing a tool or approach  
 You can process objects interactively using a client application such as [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)] or [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], or a scripted operation that run as a SQL Server Agent job or [!INCLUDE[ssIS](../../includes/ssis-md.md)] package.  
  
 How you process a database varies considerably depending on whether the model is in active development or in production. Once a model is deployed to a production server, processing must be tightly controlled to ensure the integrity and availability of multidimensional data. Because objects are interdependent, processing typically has a cascading effect across the model as other objects are also processed or unprocessed in tandem. If some objects are left in an unprocessed state, queries for that data will not resolve, breaking any reports or applications that use it. When developing a strategy for processing a production database, consider using script or [!INCLUDE[ssIS](../../includes/ssis-md.md)] packages that you have debugged and tested to avoid operator error or overlooked steps.  
  
 For more information, see [Tools and Approaches for Processing &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/tools-and-approaches-for-processing-analysis-services.md).  
  
##  <a name="bkmk_proc"></a> Processing Objects  
 Processing affects the following [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] objects: measure groups, partitions, dimensions, cubes, mining models, mining structures, and databases. When an object contains one or more objects, processing the highest-level object causes a cascade of processing all the lower-level objects. For example, a cube typically contains one or more measure groups (each of which contains one or more partitions) and dimensions. Processing a cube causes processing of all the measure groups within the cube and the constituent dimensions that are currently in an unprocessed state. For more information about processing [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] objects, see [Processing Analysis Services Objects](../../analysis-services/multidimensional-models/processing-analysis-services-objects.md).  
  
 While the processing job is working, the affected [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] objects can be accessed for querying. The processing job works inside a transaction and the transaction can be committed or rolled back. If the processing job fails, the transaction is rolled back. If the processing job succeeds, an exclusive lock is put on the object when changes are being committed, which means the object is temporarily unavailable for query or processing. During the commit phase of the transaction, queries can still be sent to the object, but they will be queued until the commit is completed.  
  
 During a processing job, whether an object is processed, and how it will be processed, depends on the processing option that is set for that object. For more information about the specific processing options that can be applied to each object, see [Processing Options and Settings &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/processing-options-and-settings-analysis-services.md).  
  
##  <a name="bkmk_reproc"></a> Reprocessing Objects  
 Cubes that contain unprocessed elements have to be reprocessed before they can be browsed. Cubes in [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] contain measure groups and partitions that must be processed before the cube can be queried. Processing a cube causes [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] to process constituent dimensions of the cube if those dimensions are in an unprocessed state. After an object has been processed the first time, it must be reprocessed either partially or in full whenever one of the following situations occurs:  
  
-   The structure of the object changes, such as dropping a column in a fact table.  
  
-   The aggregation design for the object changes.  
  
-   The data in the object needs to be updated.  
  
 When you process objects in [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], you can select a processing option, or you can enable [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] to determine the appropriate type of processing. The processing methods made available differ from one object to another, and are based on the type of object. Additionally, the methods available are based on what changes have occurred to the object since it was last processed. If you enable [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] to automatically select a processing method, it will use the method that returns the object to a fully processed state in the least time. For more information, see [Processing Options and Settings &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/processing-options-and-settings-analysis-services.md).  
  
## See Also  
 [Logical Architecture &#40;Analysis Services - Multidimensional Data&#41;](../../analysis-services/multidimensional-models/olap-logical/understanding-microsoft-olap-logical-architecture.md)   
 [Database Objects &#40;Analysis Services - Multidimensional Data&#41;](../../analysis-services/multidimensional-models/olap-logical/database-objects-analysis-services-multidimensional-data.md)  
  
  
