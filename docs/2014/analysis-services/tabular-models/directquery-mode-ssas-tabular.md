---
title: "DirectQuery Mode (SSAS Tabular) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.bidtoolset.realtime.f1"
ms.assetid: 45ad2965-05ec-4fb1-a164-d8060b562ea5
author: minewiskan
ms.author: owend
manager: craigg
---
# DirectQuery Mode (SSAS Tabular)
  Analysis Services lets you retrieve data and create reports from a tabular model by retrieving data and aggregates directly from a relational database system, using *DirectQuery mode*. This topic introduces the differences between standard tabular models that reside only in memory and tabular models that can query a relational data source, and explains how you can author and deploy a model for use in DirectQuery mode.  
  
 Sections in this topic:  
  
-   [Benefits of DirectQuery Mode](#bkmk_Benefits)  
  
-   [Authoring Models for Use with DirectQuery Mode](#bkmk_Design)  
  
    -   [Data Sources for DirectQuery Models](directquery-mode-ssas-tabular.md#bkmk_datasources)  
  
    -   [Validation and Design Restrictions for DirectQuery Mode](#bkmk_Validation)  
  
    -   [Formula Compatibility for DirectQuery Models](#bkmk_FormulaCompat)  
  
    -   [Security in DirectQuery Mode](#bkmk_Security)  
  
    -   [Security in DirectQuery Mode](#bkmk_Security)  
  
-   [DirectQuery Properties](#bkmk_PropertyList)  
  
-   [Related Topics and Tasks](#bkmk_related_tasks)  
  
##  <a name="bkmk_Benefits"></a> Benefits of DirectQuery Mode  
 By default, tabular models use an in-memory cache to store and query data. Because tabular models use data that resides in memory, even complex queries can be incredibly fast. However, there are some drawbacks to using cached data:  
  
-   Data is not refreshed when the source data changes. You must process the model to get updates to the data.  
  
-   When you turn off the computer that hosts the model, the cache is saved to disk and must be reopened when you load the model or open the PowerPivot file. The save and load operations can be time-consuming.  
  
 In contrast, DirectQuery mode uses data that is stored in a SQL Server database.  Generally, you import all or a small sample of the data into the cache while authoring the model, and when you deploy the model, you specify that the data source for queries against the model should be SQL Server, not the cached data. Any DAX queries on the data are translated by [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] into equivalent SQL statements against the specified relational data source.  
  
 There are many advantages to deploying a model using DirectQuery mode:  
  
-   It is possible to have a model over data sets that are too large to fit in memory on the Analysis Services server.  
  
-   The data is guaranteed to be up-to-date, and there is no extra management overhead of having to maintain a separate copy of the data. Changes to the underlying source data can be immediately reflected in queries against the data model.  
  
-   DirectQuery can take advantage of provider-side query acceleration, such as that provided by xVelocity memory optimized column indexes.  
  
-   Any security enforced by the back-end database is guaranteed to be enforced, using row-level security. In contrast, if you are using cached data, it can be difficult to ensure that the cache is secured exactly as on the server.  
  
-   If the model contains complex formulas that might require multiple queries, Analysis Services can perform optimization to ensure that the query plan for the query executed against the back-end database will be as efficient as possible.  
  
##  <a name="bkmk_Design"></a> Authoring Models for Use with DirectQuery Mode  
 Tabular models are authored by using the model designer [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]. The model designer creates all models in memory, which means that when you are modeling, if your data is too large to fit into memory, you should import only a subset of data into the cache used by the workspace database.  
  
 When you are ready to switch to DirectQuery mode, you can change a property that enables DirectQuery mode. For more information, see [Enable DirectQuery Design Mode &#40;SSAS Tabular&#41;](enable-directquery-mode-in-ssdt.md).  
  
 When you do this, the model designer automatically configures the workspace database to run in a hybrid mode that lets you continue to work with the cached data. The model designer will also notify you of any features in your model that are incompatible with DirectQuery mode. The following list summarizes the main requirements to keep in mind:  
  
-   **Data sources:** DirectQuery models can only use data from a single SQL Server data source. When DirectQuery mode has been turned on for a model, you can use no other types of data in the model designer, including tables added by copy-paste operations. All other import options are disabled. Any tables included in a query must be part of the SQL Server data source. See [Data Sources for DirectQuery Models](directquery-mode-ssas-tabular.md#bkmk_datasources)for more information.  
  
-   **Support for calculated columns:** Calculated columns are not supported for DirectQuery models. However, you can create measures and KPIs, which operate over sets of data. See the section on [validation](#bkmk_Validation) for more information.  
  
-   **Limited use of DAX functions:** Some DAX functions cannot be used in DirectQuery mode, so you must replace them with other functions, or create the values using derived columns in the data source. The model designer provides design-time validation for any errors that arise when you create formulas that are incompatible with DirectQuery mode. See the following sections for more information: [Validation](#bkmk_Validation).  
  
-   **Formula compatibility:** In certain known cases, the same formula can return different results in a cached or hybrid model compared to a DirectQuery model that uses only the relational data store. These differences are a consequence of the semantic differences between the xVelocity in-memory analytics (VertiPaq) engine and SQL Server. For more information about these differences, see this section: [Formula Compatibility](#bkmk_FormulaCompat).  
  
-   **Security:** You can use different methods to secure models depending on how they are deployed. Cached data for tabular models is secured by using the security model of the Analysis Services instance. DirectQuery models can be secured by using roles, but you can also use security defined in the relational data store. The model can be configured so that users who open a report based on a DirectQuery-only model can see only the data that is allowed to them under their permissions in SQL Server. See this section for more information: [Security](#bkmk_Security).  
  
-   **Client restrictions:** When a model is in DirectQuery mode, it can only be queried by using DAX. You cannot use MDX to create queries. This means that you cannot use the Excel Pivot Client, because Excel uses MDX.  
  
     However, you can create queries against a DirectQuery model in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] if you use a DAX table query as part of an XMLA Execute statement, For more information, see [DAX Query Syntax Reference](https://msdn.microsoft.com/library/ee634217.aspx).  
  
 When you have resolved all the design issues and tested your model, you are ready for deployment. At this point, you can set the preferred method for answering queries against the model. Do you want users to have access to the cache, or always use only the relational data source?  
  
 If you deploy the model in a *hybrid mode*, the cache is still available and can be used for queries. A hybrid mode provides you with many options:  
  
-   When both the cache and the relational data source are available, you can set the preferred connection method, but ultimately the client controls which source is used, using the DirectQueryMode connection string property.  
  
-   You can also configure partitions on the cache in such a way that the primary partition used for DirectQuery mode is never processed and must always reference the relational source. There are many ways to use partitions to optimize the model design and reporting experience. For more information, see [Partitions and DirectQuery Mode &#40;SSAS Tabular&#41;](define-partitions-in-directquery-models-ssas-tabular.md).  
  
-   After the model has been deployed, you can change the preferred connection method. For example, you might use a hybrid mode for testing, and switch the model over to **DirectQuery only** mode only after thoroughly testing any reports or queries that use the model. For more information, see [Set or Change the Preferred Connection Method for DirectQuery](../set-or-change-the-preferred-connection-method-for-directquery.md).  
  
###  <a name="bkmk_DataSources"></a> Data Sources for DirectQuery Models  
 As soon as you change the design environment to enable DirectQuery mode, the data sources for the workspace database are validated to ensure that they come from a single SQL Server data source. Data from other sources, including copy-pasted data, is not allowed in DirectQuery models.  
  
 If you intend to use the model in DirectQuery mode, you must ensure that all the data you need for reporting is stored in the specified SQL Server database. If the data you need for modeling is not available in that source, consider use of Integration Services or other data warehousing tools to import the data into a SQL Server database that serves as the DirectQuery data source.  
  
###  <a name="bkmk_Validation"></a> Validation and Design Restrictions for DirectQuery Mode  
 When you author a model for use in DirectQuery mode, you must initially load some portion of the data into the cache. If the data you will eventually use is too large to fit into memory, you can use the **Preview & Filter** option in the Table Import wizard to select a subset of data, or write a SQL script to get the data that you want.  
  
> [!WARNING]  
>  Because DirectQuery mode does not support the use of calculated columns, if there are columns that you wish to combine or perform other operations on, you should plan ahead and create the column definition as part of your data import query or script.  
  
 To view and resolve validation errors, open the **Error List** in [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)]. Critical errors that prevent use of DirectQuery mode are displayed on the **Errors** tab. You must fix these errors before changing to DirectQuery mode. The validation errors that are more difficult to resolve typically are related to formulas that are not supported in DirectQuery mode. See the section, [Formula Compatibility](#bkmk_FormulaCompat), for an overview of errors related to formulas and calculated columns.  
  
 The following list describes other considerations to keep in mind when authoring a model for DirectQuery access:  
  
-   When in *DirectQuery only* mode, the results in a report can vary depending on the security context of the user who is viewing the results. You should test models with different credentials to ensure that users get the expected results.  
  
-   If you configure a model to operate in hybrid mode, which allows the use of either the cache or data from SQL Server, you should be aware of the possibility that clients connecting to each source might see different results, depending on the mode specified in the connection string. If you need to ensure that your report users see only data from SQL Server, you must clear the cache or change the model to DirectQueryOnly.  
  
###  <a name="bkmk_FormulaCompat"></a> Formula Compatibility for DirectQuery Models  
 Some models might contain formulas that are not supported in DirectQuery mode, and the model must be redesigned to prevent validation errors. Restrictions on formulas that are supported in DirectQuery mode include the following:  
  
-   Calculated columns are not supported in any tabular model that has DirectQuery mode enabled, not even hybrid models. If you need calculated columns for a model, consider converting them to derived columns by using Transact-SQL in your import definition.  
  
-   DirectQuery models do support the use of DAX formulas for use in measures, which are converted to set-based operations against the relational data store. All measures that you create by using implicit measures are supported.  
  
-   Not all functions are supported. Because [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] converts all DAX formulas and measure definitions into SQL statements when querying a DirectQuery model, any formula containing elements that cannot be converted into Transact-SQL will trigger validation errors on the model. For example, time intelligence functions are not supported. Even functions that are supported might behave differently, such as statistical functions. For a complete list of compatibility issues, see [Formula Compatibility in DirectQuery Mode](../dax-formula-compatibility-in-directquery-mode-ssas-2014.md).  
  
-   Some formulas in the model might validate when you switch the model to DirectQuery mode, but return different results when executed against the cache vs. the relational data store. This is because calculations against the cache use the semantics of the xVelocity in-memory analytics (VertiPaq) engine, which contains many features meant to emulate the behavior of Excel, whereas queries against data stored in the relational data store necessarily use the semantics of SQL Server. For a list of DAX functions that might return different results when the model is deployed to real-time, see [Formula Compatibility in DirectQuery Mode](../dax-formula-compatibility-in-directquery-mode-ssas-2014.md).  
  
###  <a name="bkmk_Connecting"></a> Connecting to DirectQuery Models  
 Clients that use MDX as the query language cannot connect to models that use DirectQuery mode. For example, if you attempt to create an MDX query against a DirectQuery model, you will get an error indicating that the cube cannot be found, or has not been processed. You can create queries against DirectQuery models by using [!INCLUDE[ssCrescent](../../includes/sscrescent-md.md)], DAX formulas, or XMLA queries. For more information about how you can perform ad hoc queries against tabular models, see [Tabular Model Data Access](tabular-model-data-access.md).  
  
 If you are using a hybrid model, you can specify whether users connect to the cache or use DirectQuery data by specifying the connection string property, DirectQueryMode.  
  
###  <a name="bkmk_Security"></a> Security in DirectQuery Mode  
 During model authoring, you specify the permissions that are used to retrieve the source data. This will often be your own credentials, or an account used for development. However, when you switch the model to use DirectQuery mode, the security context is more complex:  
  
-   Consider whether users have the necessary level of access to the data in the relational data store.  
  
-   Users who view the same model or report might see different data, depending on the user's security context.  
  
-   If the model cache has been preserved, the cache is secured using the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] security model (roles). The cache might contain data that the model designer is privileged to see but the user is not. Model and report designers should either clear the cache, or secure this data by controlling access via roles.  
  
-   A model that answers queries from the cache cannot impersonate the current user when connecting to the data source. If you want to impersonate the current user when connecting to the data source, you must use DirectQuery mode.  
  
-   If your report model requires security, you have two options: you can either use [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] roles, or you can set row-level permissions on the data source. Security in the relational data source is used to control access to tables, and column-level security is not supported. Therefore, if users in one region do not have permission to view sales figures from different regions, a report that includes a measure based on the Sales table would return blanks or an error.  
  
 The impersonation settings property specifies the credentials used when you are connecting to a model using DirectQuery, either for a DirectQuery only model or for a hybrid model answering queries using DirectQuery. The property has the following values:  
  
 **Default**  
 Uses the credentials specified in the import wizard to connect to the data source. This can be a specific Windows user or the service account.  
  
 `ImpersonateCurrentUser`  
 Uses the credentials of the current user to connect to the data source.  
  
 For information on how to set these properties, see [DirectQuery Deployment Scenarios &#40;SSAS Tabular&#41;](../directquery-deployment-scenarios-ssas-tabular.md).  
  
##  <a name="bkmk_PropertyList"></a> DirectQuery Properties  
 The following table lists the properties that you can set in [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)], and in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], to enable DirectQuery and to control the source of data used for queries against the model.  
  
|Property name|Description|  
|-------------------|-----------------|  
|**DirectQueryMode property**|This property enables use of DirectQuery mode in the model designer. You must set this property to `On` to change any of the other DirectQuery properties.<br /><br /> For more information, see [Enable DirectQuery Design Mode &#40;SSAS Tabular&#41;](enable-directquery-mode-in-ssdt.md).|  
|**QueryMode property**|This property specifies the default query method for a DirectQuery model, You set this property in the model designer when you deploy the model, but you can override it later. The property has these values:<br /><br /> **DirectQuery** - This setting specifies all queries to the model should use the relational data source only.<br /><br /> **DirectQuery with In-Memory** - This setting specifies, by default, queries should be answered by using the relational source, unless otherwise specified in the connection string from the client.<br /><br /> **In-Memory** - This setting specifies  queries should be answered by using the cache only.<br /><br /> **In-Memory with DirectQuery** - This setting specifies, by default. queries should be answered by using the cache, unless otherwise specified in the connection string from the client.<br /><br /> <br /><br /> For more information, see [Set or Change the Preferred Connection Method for DirectQuery](../set-or-change-the-preferred-connection-method-for-directquery.md).|  
|**DirectQueryMode property**|After the model has been deployed, you can change the preferred query data source for a DirectQuery model, by changing this property in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]<br /><br /> Like the previous property, this property specifies the default data source for the model, and has these values:<br /><br /> **InMemory**: Queries can use the cache only.<br /><br /> **DirectQuerywithInMemory**: Queries use the relational data source by default, unless otherwise specified in the connection string from the client.<br /><br /> **InMemorywithDirectQuery**: Queries use the cache by default, unless otherwise specified in the connection string from the client.<br /><br /> (**DirectQuery**: Queries use the relational data source only.<br /><br /> <br /><br /> For more information, see [Set or Change the Preferred Connection Method for DirectQuery](../set-or-change-the-preferred-connection-method-for-directquery.md).|  
|**Impersonation Settings property**|This property defines the credentials used to connect to the SQL Server data source at query time. You can set this property in the model designer, and you can change the value later, after the model has been deployed.<br /><br /> Note that these credentials are used only for answering queries against the relational data store; they are not the same as the credentials used for processing the cache of a hybrid model.<br /><br /> Impersonation cannot be used when the model is used within memory only. The setting `ImpersonateCurrentUser`, is invalid unless the model is using DirectQuery mode.|  
  
 Additionally, if your model includes partitions, you must choose one partition to use as the source for queries in DirectQuery mode. For more information, see [Partitions and DirectQuery Mode &#40;SSAS Tabular&#41;](define-partitions-in-directquery-models-ssas-tabular.md).  
  
##  <a name="bkmk_related_tasks"></a> Related Topics and Tasks  
  
|Topic|Description|  
|-----------|-----------------|  
|[Partitions and DirectQuery Mode &#40;SSAS Tabular&#41;](define-partitions-in-directquery-models-ssas-tabular.md)|Describes how partitions are used in models configured for DirectQuery mode.|  
|[DAX Formula Compatibility in DirectQuery Mode](../dax-formula-compatibility-in-directquery-mode-ssas-2014.md)|Describes restrictions and compatibility requirements on the formulas that you can use in models configured for DirectQuery mode.|  
|[Enable DirectQuery Design Mode &#40;SSAS Tabular&#41;](enable-directquery-mode-in-ssdt.md)|Describes how you can change the design-time environment so that it supports using DirectQuery mode|  
|[Change the DirectQuery Partition &#40;SSAS Tabular&#41;](../change-the-directquery-partition-ssas-tabular.md)|Describes how to change the DirectQuery partition.|  
|[Set or Change the Preferred Connection Method for DirectQuery](../set-or-change-the-preferred-connection-method-for-directquery.md)|Describes how to set or change the connection method for models configured for DirectQuery.|  
|[DirectQuery Deployment Scenarios &#40;SSAS Tabular&#41;](../directquery-deployment-scenarios-ssas-tabular.md)|Describes DirectQuery deployment scenarios.|  
|[Configure In-Memory or DirectQuery Access for a Tabular Model Database](enable-directquery-mode-in-ssms.md)|Understand DirectQuery configurations|  
|[Clear the Analysis Services Caches](../instances/clear-the-analysis-services-caches.md)|Clear the cache of the tabular model|  
  
## See Also  
 [Partitions &#40;SSAS Tabular&#41;](partitions-ssas-tabular.md)   
 [Tabular Model Projects &#40;SSAS Tabular&#41;](tabular-model-projects-ssas-tabular.md)   
 [Analyze in Excel &#40;SSAS Tabular&#41;](analyze-in-excel-ssas-tabular.md)  
  
  
