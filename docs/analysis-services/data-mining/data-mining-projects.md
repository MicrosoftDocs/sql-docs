---
title: "Data Mining Projects | Microsoft Docs"
ms.date: 05/01/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: data-mining
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Data Mining Projects
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  A data mining project is part of an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] solution. During the design process, the objects that you create in this project are available for testing and querying as part of a workspace database. When you want users to be able to query or browse the objects in the project, you must deploy the project to an instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] running in multidimensional mode.  
  
 This topic provides you with the basic information needed to understand and create data mining projects.  
  
 [Creating Data Mining Projects](#bkmk_Overview)  
  
 [Objects in Data Mining Projects](#bkmk_Objects)  
  
-   [Data sources](#bkmk_DataSources)  
  
-   [Data source views](#bkmk_DSV)  
  
-   [Mining structures](#bkmk_Structures)  
  
-   [Mining models](#bkmk_Models)  
  
 [Using the Completed Data Mining Project](#bkmk_Complete)  
  
-   [View and explore models](#bkmk_ViewExplore)  
  
-   [Test and validate models](#bkmk_Validate)  
  
-   [Create predictions](#bkmk_Predict)  
  
 [Programmatic Access to Data Mining Projects](#bkmk_API)  
  
##  <a name="bkmk_Overview"></a> Creating Data Mining Projects  
 In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], you build data mining projects using the template, **OLAP and Data Mining Project**. You can also create data mining projects programmatically, by using AMO. Individual data mining objects can be scripted using the Analysis Services Scripting language (ASSL). For more information, see [Multidimensional Model Data Access &#40;Analysis Services - Multidimensional Data&#41;](../../analysis-services/multidimensional-models/mdx/multidimensional-model-data-access-analysis-services-multidimensional-data.md).  
  
 If you create a data mining project within an existing solution, by default the data mining objects will be deployed to an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database with the same name as the solution file. You can change this name and the target server by using the **Project Properties** dialog box. For more information, see [Configure Analysis Services Project Properties &#40;SSDT&#41;](../../analysis-services/multidimensional-models/configure-analysis-services-project-properties-ssdt.md).  
  
> [!WARNING]  
>  To successfully build and deploy your project, you must have access to an instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] that is running in OLAP/Data Mining mode. You cannot develop or deploy data mining solutions on an instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] that supports tabular models, nor can you use data directly from a [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] workbook or from a tabular model that uses the in-memory data store. To determine whether the instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] that you have can support data mining, see [Determine the Server Mode of an Analysis Services Instance](../../analysis-services/instances/determine-the-server-mode-of-an-analysis-services-instance.md).  
  
 Within each data mining project that you create, you will follow these steps:  
  
1.  Choose a *data source*, such as a cube, database, or even Excel or text files, which contains the raw data you will use for building models.  
  
2.  Define a subset of the data in the data source to use for analysis, and save it as a *data source view*.  
  
3.  Define a *mining structure* to support modeling.  
  
4.  Add *mining models* to the mining structure, by choosing an *algorithm* and specifying how the algorithm will handle the data.  
  
5.  Train models by populating them with the selected data, or a filtered subset of the data.  
  
6.  Explore, test, and rebuild models.  
  
 When the project is complete, you can deploy it for users to browse or query, or provide programmatic access to the mining models in an application, to support predictions and analysis.  
  
  
##  <a name="bkmk_Objects"></a> Objects in Data Mining Projects  
 All data mining projects contain the following four types of objects. You can have multiple objects of all types.  
  
-   Data sources  
  
-   Data source views  
  
-   Mining structures  
  
-   Mining models  
  
 For example, a single data mining project can contain a reference to multiple data sources, with each data source supporting multiple data source views. In turn, each data source view can support multiple mining structures, each with many related mining models.  
  
 Additionally, your project might include plug-in algorithms, custom assemblies, or custom stored procedures; however, these objects are not described here. For more information, see [Analysis Services Developer Documentation](../../analysis-services/analysis-services-developer-documentation.md).  
  
  
###  <a name="bkmk_DataSources"></a> Data Sources  
 The data source defines the connection string and authentication information that the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] server will use to connect to the data source. The data source can contain multiple tables or views; it can be as simple as a single Excel workbook or text file, or as complex as an Online Analytical Processing (OLAP) database or large relational database.  
  
 A single data mining project can reference multiple data sources. Even though a mining model can use only one data source at a time, the project could have multiple models drawing on different data sources.  
  
 [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] supports data from many external providers, and SQL Server Data Mining can use both relational and cube data as a data source. However, if you develop both types of projects-models based on relational sources and models based on OLAP cubes-you might wish to develop and manage these in separate projects.  
  
-   Typically models that are based on an OLAP cube should be developed within the OLAP design solution. One reason is that models based on a cube must process the cube to update data. Generally, you should use cube data only when that is the principal means of data storage and access, or when you require the aggregations, dimensions, and attributes created by the multidimensional project.  
  
-   If your project uses relational data only, you should create the relational models within a separate project, so that you do not unnecessarily reprocess other objects. In many cases, the staging database or the data warehouse used to support cube creation already contains the views that are needed to perform data mining, and you can use those views for data mining rather than use the aggregations and dimensions in the cube.  
  
-   You cannot use in-memory or [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] data directly to build data mining models.  
  
 The data source only identifies the server or provider and the general type of data. If you need to change data formatting and aggregations, use the data source view object.  
  
 To control the way that data from the data source is handled, you can add derived columns or calculation, modify aggregates, or rename columns in the data in the data source view. (You can also work with data downstream, by modifying mining structure columns, or by using modeling flags and filters at the level of the mining model column.)  
  
 If data cleansing is required, or the data in the data warehouse must be modified to create additional variables, change data types, or create alternate aggregation, you might need to create additional project types in support of data mining. For more information about these related projects, see [Related Projects for Data Mining Solutions](../../analysis-services/data-mining/related-projects-for-data-mining-solutions.md).  
  
  
###  <a name="bkmk_DSV"></a> Data Source Views  
 After you have defined this connection to a data source, you create a view that identifies the specific data that is relevant to your model.  
  
 The data source view also enables you to customize the way that the data in the data source is supplied to the mining model. You can modify the structure of the data to make it more relevant to your project, or choose only certain kinds of data.  
  
 For example, by using the Data Source View editor, you can:  
  
-   Create derived columns, such as dateparts, substrings, etc.  
  
-   Aggregate values using [!INCLUDE[tsql](../../includes/tsql-md.md)] statements such as GROUP BY  
  
-   Restrict data temporarily, or sample data  
  
 For more information about how you can modify data within a data source view, see [Data Source Views in Multidimensional Models](../../analysis-services/multidimensional-models/data-source-views-in-multidimensional-models.md).  
  
> [!WARNING]  
>  If you want to filter the data, you can do so in the data source view, but you can also create filters on the data at the level of the mining model. Because the filter definition is stored with the mining model, using model filters makes it easier to determine the data that was used for training the model. Moreover, you can create multiple related models, with different filter criteria. For more information, see [Filters for Mining Models &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/filters-for-mining-models-analysis-services-data-mining.md).  
  
 Note that the data source view that you create can contain additional data that is not directly used for analysis. For example, you might add to your data source view data that is used for testing, predictions, or for drillthrough. For more information about these uses, see [Testing and Validation &#40;Data Mining&#41;](../../analysis-services/data-mining/testing-and-validation-data-mining.md) and [Drillthrough](../../analysis-services/data-mining/drillthrough-queries-data-mining.md).  
  
  
###  <a name="bkmk_Structures"></a> Mining Structures  
 Once you have created your data source and data source view, you must select the columns of data that are most relevant to your business problem, by defining *mining structures* within the project. A mining structure tells the project which columns of data from the data source view should actually be used in modeling, training, and testing.  
  
 To add a new mining structure, you start the Data Mining Wizard. The wizard automatically defines a mining structure, walks you through the process of choosing the data, and optionally lets you add an initial mining model to the structure. Within the mining structure, you choose tables and columns from the data source view or from an OLAP cube, and define relationships among tables, if your data includes nested tables.  
  
 Your choice of data will look very different in the Data Mining Wizard, depending on whether you use relational or online analytical processing (OLAP) data sources.  
  
-   When you choose data from a relational data source, setting up a mining structure is easy: you choose columns from the data in the data source view, and set additional customizations such as aliases, or define how values in the column should be grouped or binned. For more information, see [Create a Relational Mining Structure](../../analysis-services/data-mining/create-a-relational-mining-structure.md).  
  
-   When you use data from an OLAP cube, the mining structure must be in the same database as the OLAP solution.  To create a mining structure, you select attributes from the dimensions and related measures in your OLAP solution. Numeric values are typically found in measures, and categorical variables in dimensions. For more information, see [Create an OLAP Mining Structure](../../analysis-services/data-mining/create-an-olap-mining-structure.md).  
  
-   You can also define mining structures by using DMX. For more information, see [Data Mining Extensions &#40;DMX&#41; Data Definition Statements](../../dmx/dmx-statements-data-definition.md).  
  
 After you have created the initial mining structure, you can copy, modify, and alias the structure columns.  
  
 Each mining structure can contain multiple mining models. Therefore, after you are done, you can open the mining structure again, and use [Data Mining Designer](../../analysis-services/data-mining/data-mining-designer.md) to add more mining models to the structure.  
  
 You also have the option to separate your data into a training data set, used for building models, and a holdout data set to use in testing or validating your mining models.  
  
> [!WARNING]  
>  Some model types, such as time series models, do not support the creation of holdout data sets because they require a continuous series of data for training. For more information, see [Training and Testing Data Sets](../../analysis-services/data-mining/training-and-testing-data-sets.md).  
  
  
###  <a name="bkmk_Models"></a> Mining Models  
 The mining model defines the algorithm, or the method of analysis that you will use on the data. To each mining structure, you add one or more mining models.  
  
 Depending on your needs, you can combine many models in a single project, or create separate projects for each type of model or analytical task.  
  
 After you have created a structure and model, you *process* each model by running the data from the data source view through the algorithm, which generates a mathematical model of the data. This process is also known as *training the model*. For more information, see [Processing Requirements and Considerations &#40;Data Mining&#41;](../../analysis-services/data-mining/processing-requirements-and-considerations-data-mining.md).  
  
 After the model has been processed, you can then visually explore the mining model and create prediction queries against it. If the data from the training process has been cached, you can use *drillthrough* queries to return detailed information about the cases used in the model.  
  
 When you want to use a model for production (for example, for use in making predictions, or for exploration by general users) you can deploy the model to a different server. If you need to reprocess the model in future, you must also export the definition of the underlying mining structure (and, necessarily, the definition of the data source and data source view) at the same time.  
  
 When you deploy a model, you must also ensure that the correct processing options are set on the structure and model, and that potential users have the permissions they need to perform queries, view models, or drillthrough to structure o model data. For more information, see [Security Overview &#40;Data Mining&#41;](../../analysis-services/data-mining/security-overview-data-mining.md).  
  
  
##  <a name="bkmk_Complete"></a> Using the Completed Data Mining Project  
 This section summarizes the ways that you can use the completed data mining project. You can create accuracy charts, explore and validate the data, and make the data mining patterns available to users.  
  
> [!WARNING]  
>  The charts, queries, and visualizations that you use with data mining models are not saved as part of the data mining project, and cannot be deployed. If you need to persist these objects, you must either save the content that is presented or script it as described for each object.  
  
  
###  <a name="bkmk_ViewExplore"></a> View and Explore Models  
 After you have created a model, you can use visual tools and queries to explore the patterns in the model and learn more about the underlying patterns and statistics. On the **Mining Model Viewer** tab in Data Mining Designer, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] provides viewers for each mining model type, which you can use to explore the mining models.  
  
 These visualizations are temporary, and are closed without saving when you exit the session with [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. Therefore, if you need to export these visualizations to another application for presentation or further analysis, use the **Copy** commands provided in each tab or pane of the viewer interface.  
  
 The Data Mining Add-ins for Excel also provides a Visio template that you can use to represent your models in a Visio diagram and annotate and modify the diagram using Visio tools. For more information, see [Microsoft SQL Server 2008 SP2 Data Mining Add-ins for Microsoft Office 2007](http://go.microsoft.com/fwlink/?LinkID=123146).  
  
  
###  <a name="bkmk_Validate"></a> Test and Validate Models  
 After you have created a model, you can investigate the results and make decisions about which models perform the best.  
  
 [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] provides several charts  that you can use to provides tools that you can use to directly compare mining models and choose the most accurate or useful mining model. These tools include a lift chart, profit chart, and a classification matrix. You can generate these charts by using the **Mining Accuracy Chart** tab of Data Mining Designer.  
  
 You can also use the cross-validation report to perform iterative subsampling of your data to determine whether the model is biased to a particular set of data. The statistics that the report provides can be used to objectively compare models and assess the quality of your training data.  
  
 Note that these reports and charts are not stored with the project or in the ssASnoversion database, so if you need to preserve or duplicate the results, you should either save the results, or script the objects by using DMX or AMO. You can also use stored procedures for cross-validation.  
  
 For more information, see [Testing and Validation &#40;Data Mining&#41;](../../analysis-services/data-mining/testing-and-validation-data-mining.md).  
  
  
###  <a name="bkmk_Predict"></a> Create Predictions  
 [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] provides a query language called Data Mining Extensions (DMX) that is the basis for creating predictions and is easily scriptable. To help you build DMX prediction queries, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides a query builder, available in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. There are also many DMX templates for the query editor in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].If you are new to prediction queries, we recommend that you use the query builder that is provided in both Data Mining Designer and [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. For more information, see [Data Mining Tools](../../analysis-services/data-mining/data-mining-tools.md).  
  
 The predictions that you create in either [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] or [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] are not persisted, so if your queries are complex, or you need to reproduce the results, we recommend that you save your prediction queries to DMX query files, script them, or embed the queries as part of an Integration Services package.  
  
  
##  <a name="bkmk_API"></a> Programmatic Access to Data Mining Objects  
 [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] provides several tools that you can use to programmatically work with data mining projects and the objects in them. The DMX language provides statements that you can use to create data sources and data source views, and to create, train, and use data mining structure and models. For more information, see [Data Mining Extensions &#40;DMX&#41; Reference](../../dmx/data-mining-extensions-dmx-reference.md).  
  
 You can also perform these tasks by using the Analysis Services Scripting Language (ASSL), or by using Analysis Management Objects (AMO). For more information, see [Developing with XMLA in Analysis Services](../../analysis-services/multidimensional-models-scripting-language-assl-xmla/developing-with-xmla-in-analysis-services.md).  
  
  
## Related Tasks  
 The following topics describe use of the Data Mining Wizard to create a data mining project and associated objects.  
  
|Tasks|Topics|  
|-----------|------------|  
|Describes how to work with mining structure columns|[Create a Relational Mining Structure](../../analysis-services/data-mining/create-a-relational-mining-structure.md)|  
|Provides more information about how to add new mining models, and process a structure and models|[Add Mining Models to a Structure &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/add-mining-models-to-a-structure-analysis-services-data-mining.md)|  
|Provides links to resources that help you customize the algorithms that build mining models|[Customize Mining Models and Structure](../../analysis-services/data-mining/customize-mining-models-and-structure.md)|  
|Provides links to information about each of the mining model viewers|[Data Mining Model Viewers](../../analysis-services/data-mining/data-mining-model-viewers.md)|  
|Learn how to create a lift chart, profit chart, or classification matrix, or test a mining structure|[Testing and Validation &#40;Data Mining&#41;](../../analysis-services/data-mining/testing-and-validation-data-mining.md)|  
|Learn about processing options and permissions|[Processing Data Mining Objects](../../analysis-services/data-mining/processing-data-mining-objects.md)|  
|Provides more information about Analysis Services|[Multidimensional Model Databases ](../../analysis-services/multidimensional-models/multidimensional-model-databases-ssas.md)|  
  
## See Also  
 [Data Mining Designer](../../analysis-services/data-mining/data-mining-designer.md)   
 [Creating Multidimensional Models Using SQL Server Data Tools &#40;SSDT&#41;](../../analysis-services/multidimensional-models/creating-multidimensional-models-using-sql-server-data-tools-ssdt.md)   
 [Workspace Database](../../analysis-services/tabular-models/workspace-database-ssas-tabular.md)  
  
  
