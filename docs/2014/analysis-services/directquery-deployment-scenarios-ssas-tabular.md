---
title: "DirectQuery Deployment Scenarios (SSAS Tabular) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 2aaf5cb8-294b-4031-94b3-fe605d7fc4c7
author: minewiskan
ms.author: owend
manager: craigg
---
# DirectQuery Deployment Scenarios (SSAS Tabular)
  This topic provides a walkthrough of the design and deployment process for DirectQuery models. You can configure DirectQuery to use relational data only (DirectQuery only), or you can configure the model to switch between using cached data only or relational data only (hybrid mode). This topic explains the implementation process for both modes, and describes possible differences in query results depending on the mode and the security configuration.  
  
 [Design and Deployment Steps](#bkmk_DQProcedure)  
  
 [Comparing DirectQuery Configurations](#bkmk_Configurations)  
  
##  <a name="bkmk_DQProcedure"></a> Design and Deployment Steps  
 **Step 1. Create the solution**  
  
 Regardless of which mode you will use, you must review the information that describes limitations on the data that can be used in DirectQuery models. For example, all the data used in your model and reports must come from a single SQL Server database. For more information, see [DirectQuery Mode &#40;SSAS Tabular&#41;](tabular-models/directquery-mode-ssas-tabular.md).  
  
 Also, review the limitations on measures and calculated columns, and determine whether the formulas you intend to use are compatible with DirectQuery mode. You might need to remove or modify the following elements:  
  
-   Calculated columns are not supported.  
  
-   Copy-pasted data cannot be used. If you import a PowerPivot model to jumpstart your solution, be sure to delete linked tables before importing the solution, as this data cannot be deleted and will block DirectQuery validation.  
  
 **Step 2. Enable DirectQuery mode in the model designer**  
  
 By default, DirectQuery is disabled. Therefore, you must configure the design environment to support DirectQuery mode.  
  
 Right-click the **Model.bim** node in Solution Explorer and set the property, **DirectQuery Mode**, to `On`.  
  
 You can turn on DirectQuery at any time; however, to ensure that you do not create columns or formulas that are incompatible with DirectQuery mode, we recommend that you enable DirectQuery mode right from the beginning.  
  
 Initially, even DirectQuery models are always created in memory. The default query mode for the workspace database is also set to **DirectQuery with In-Memory**. This hybrid working mode lets you use the cache of imported data for improved performance during the model design process, while validating the model against DirectQuery requirements.  
  
 **Step 3. Resolve validation errors**  
  
 If you get validation errors when you turn DirectQuery on, or when you add new data or formulas, open the Visual Studio **Error List**, and then take the required actions.  
  
-   Change any required property settings for DirectQuery mode, as described in the error messages.  
  
-   Remove calculated columns. If you require a calculated column for a particular measure, you can always create the column by using the [Relational Query Designer &#40;SSAS&#41;](relational-query-designer-ssas.md) provided in the Table Import wizard.  
  
-   Modify or remove formulas that are incompatible with DirectQuery mode. If you require a particular function for a calculation, consider ways that you could provide an equivalent by using Transact-SQL.  
  
-   Add data as needed.  If your model previously used copy-paste data or data from providers other than SQL Server, you can create new views and derived columns within the existing connection, or use distributed queries.  All data used in a DirectQuery model must be accessible via a single SQL Server data source.  
  
 **Step 4. Set the preferred method for answering queries on the model**  
  
|||  
|-|-|  
|**DirectQuery only**|Set the property to **DirectQuery**.|  
|**Hybrid mode**|Set the property to **In-Memory With DirectQuery** or **DirectQuery With In-Memory**.<br /><br /> You can change this value later to use a different preference.<br /><br /> Note that clients can override the preferred method in the connection string.|  
  
 **Step 5. Specify the DirectQuery partition**  
  
|||  
|-|-|  
|**DirectQuery only**|Optional. A DirectQuery only model has no need for a partition.<br /><br /> However, if you created partitions in the model during the design phase, remember that only one partition can be used as the data source. By default the first partition you created will be used as the DirectQuery partition.<br /><br /> To ensure that all the data required by the model is available from the DirectQuery partition, choose a DirectQuery partition and edit the SQL statement to get the entire data set.|  
|**Hybrid mode**|If any table in your model has multiple partitions, you must choose a single partition as the *DirectQuery partition*. If you do not assign a partition, by default, the first partition that was created will be used as the DirectQuery partition.<br /><br /> Set processing options on all partitions except the DirectQuery. Typically the DirectQuery partition is never processed, because the data is passed through from the relational source.<br /><br /> For more information, see [Partitions and DirectQuery Mode &#40;SSAS Tabular&#41;](tabular-models/define-partitions-in-directquery-models-ssas-tabular.md).|  
  
 **Step 6. Configure impersonation**  
  
 Impersonation is supported only for DirectQuery models. The impersonation option, **Impersonation Settings**, defines the credentials that are used when viewing data from the specified SQL Server data source.  
  
|||  
|-|-|  
|**DirectQuery only**|For the  **Impersonation Settings** property, specify the account that will be used to connect to the SQL Server data source.<br /><br /> If you use the value, **ImpersonateCurrentUser**, the instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] that hosts the model will pass the credentials of the current user of the model to the SQL Server database.|  
|**Hybrid mode**|For the **Impersonation Settings** property, specify the account that will be used to access the data in the SQL Server data source.<br /><br /> This setting does not affect the credentials that are used to process the cache used by the model.|  
  
 **Step 7. Deploy the model**  
  
 When you are ready to deploy the model, open the **Project** menu of Visual Studio, and select **Properties**. Set the **QueryMode** property to one of the values described in the following table:  
  
 For more information, see [Deploy From SQL Server Data Tools &#40;SSAS Tabular&#41;](tabular-models/deploy-from-sql-server-data-tools-ssas-tabular.md).  
  
|||  
|-|-|  
|**DirectQuery only**|**DirectQueryOnly**<br /><br /> Because you have specified Direct Query only, the metadata of the model is deployed to the server, but the model is not processed.<br /><br /> Note that the cache that was used by the workspace database is not automatically deleted. If you want to ensure that users are not able to see the cached data, you might wish to clear the design-time cache. For more information, see [Clear the Analysis Services Caches](instances/clear-the-analysis-services-caches.md).|  
|**Hybrid mode**|**DirectQuery with In-Memory**<br /><br /> **In-Memory with DirectQuery**<br /><br /> Both of these values allow you to use either the cache or the relational data source as necessary. The order defines which data source is used by default when answering queries against the model.<br /><br /> In a hybrid mode, the cache must be processed at the same time that the model metadata is deployed to the server.<br /><br /> You can change this setting after deployment.|  
  
 **Step 8. Verify deployed model**  
  
 In [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)], open the instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] where you deployed the model. Right-click the name of the database and select **Properties**.  
  
-   The property, **DirectQueryMode**, was set when you defined the deployment properties.  
  
-   The property, **Data Source Impersonation Info**, was set when you defined the user impersonation options. For more information, see [Set Impersonation Options &#40;SSAS - Multidimensional&#41;](multidimensional-models/set-impersonation-options-ssas-multidimensional.md).  
  
-   You can change these properties any time after the model has been deployed.  
  
##  <a name="bkmk_Configurations"></a> Comparing DirectQuery Options  
 **DirectQuery Only**  
 This option is preferred when you want to guarantee a single source of data, or when your data is too large to fit in memory. If you are working with a very large relational data source, during design time you can create the model by using some subset of the data. When you deploy the model in DirectQuery only mode, you can edit the data source definition to include all the required data.  
  
 This option is also preferred if you want to use the security provided by the relational data source to control user access to data. With cached tabular models, you can also use [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] roles to control data access, but the data stored in the cache must also be secured. You should always use this option if your security context requires that data should never be cached.  
  
 The following table describes the possible deployment outcomes for DirectQuery only mode:  
  
|||  
|-|-|  
|**DirectQuery with no cache**|No data is loaded into the cache. The model can never be processed.<br /><br /> The model can only be queried by using clients that support DAX queries. Query results are always returned from the original data source.<br /><br /> **DirectQueryMode** = `On`<br /><br /> **QueryMode** = **DirectQuery**|  
|**DirectQuery with queries against cache only**|Deployment fails. This configuration is not supported.<br /><br /> **DirectQueryMode** = `On`<br /><br /> **QueryMode** = **In-Memory**|  
  
 **Hybrid mode**  
 Deploying your model in a hybrid mode has many advantages: you can get up-to-date data from the SQL Server data source if needed, but preserving the cache gives you the ability to work with data in memory for faster performance while designing reports or testing the model.  
  
 A DirectQuery hybrid mode is also useful if your model is very large. Rather than have users get stale data or have the model be unavailable while the cache is processed, you can switch the model to DirectQuery mode while processing is in progress. Users might experience slightly slower performance but they would be able to get data directly from the relational store, ensuring that results were up-to-date.  
  
 The following table compares the deployment outcome in each of the combinations of DirectQuery options.  
  
|||  
|-|-|  
|**Hybrid mode with cache preferred**|The model can be processed and data can be loaded into the cache. Queries use the cache by default.  If a client wants to use the DirectQuery source, a parameter must be inserted in the connection string.<br /><br /> **DirectQueryMode** = `On`<br /><br /> **QueryMode** = **In-Memory with DirectQuery**|  
|**Hybrid mode with DirectQuery preferred**|The model is processed and data can be loaded into the cache. However, queries use DirectQuery by default. If a client wants to use the cached data, a parameter must be inserted in the connection string. If the tables in the model are partitioned, the principal partition of the cache is also set to **In-Memory with DirectQuery**.<br /><br /> **DirectQueryMode** = `On`<br /><br /> **QueryMode** = **DirectQuery with In-Memory**|  
  
## See Also  
 [DirectQuery Mode &#40;SSAS Tabular&#41;](tabular-models/directquery-mode-ssas-tabular.md)   
 [Tabular Model Data Access](tabular-models/tabular-model-data-access.md)  
  
  
