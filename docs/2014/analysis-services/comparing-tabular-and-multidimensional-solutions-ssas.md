---
title: "Comparing Tabular and Multidimensional Solutions (SSAS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 76ee5e96-6a04-49af-a88e-cb5fe29f2e9a
author: minewiskan
ms.author: owend
manager: craigg
---
# Comparing Tabular and Multidimensional Solutions (SSAS)
  [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] provides two distinct approaches for data modeling: tabular and multidimensional. While there is significant overlap between them, there are also important differences that will inform your decision about how to move forward. In this topic, we offer feature comparisons and explain how each approach addresses common project requirements. For example, if support of a specific data source is a top consideration, the section on data sources can help guide your decision on which modeling approach to use.  
  
 This topic includes the following sections:  
  
-   [Overview of modeling in Analysis Services](#bkmk_overview)  
  
-   [Data Source Support by Solution Type](#bkmk_ds)  
  
-   [Model Features](#bkmk_models)  
  
-   [Model Size](#bkmk_modelsize)  
  
-   [Programmability and Developer Experience](#bkmk_ext)  
  
-   [Query and Scripting Language Support](#bkmk_lang)  
  
-   [Security Feature Support](#bkmk_sec)  
  
-   [Design Tools](#bkmk_designer)  
  
-   [Client and Reporting Applications](#bkmk_client)  
  
-   [Hosting Platforms](#bkmk_sharePoint)  
  
-   [Server Deployment Modes for Multidimensional and Tabular Solutions](#bkmk_deploymentmode)  
  
-   [Next Step: Build a Solution](#bkmk_Next)  
  
 Additional information can be found in this technical article on MSDN: [Choosing a Tabular or Multidimensional Modeling Experience in SQL Server 2012 Analysis Services](https://go.microsoft.com/fwlink/?LinkId=251588).  
  
##  <a name="bkmk_overview"></a> Overview of modeling in Analysis Services  
 Analysis Services provides a model development experience, as well as model deployment via database hosting on an Analysis Services instance. Model types include tabular and multidimensional. As you might expect, database hosting supports the tabular and multidimensional solutions that you create, but database hosting also includes PowerPivot for SharePoint.  
  
 PowerPivot for SharePoint is *Analysis Services in SharePoint mode*, where Analysis Services operates as an adjunct service to SharePoint, helping to host and manage Excel Data Models that were previously created in Excel and then saved to SharePoint. The role of Analysis Services in this context is to load the data model into memory, refresh data from external data sources, and execute queries against the model. In this configuration, Analysis Services operates behind the scenes. All connections and requests to Analysis Services are made by SharePoint, and only when an Excel workbook contains a data model (data models are optional in Excel workbooks). If building a data model in Excel, and hosting it in SharePoint, aligns with your project requirements, see [Power Pivot: Powerful data analysis and data modeling in Excel](https://support.office.com/en-ie/article/Power-Pivot-Powerful-data-analysis-and-data-modeling-in-Excel-d7b119ed-1b3b-4f23-b634-445ab141b59b) and [PowerPivot for SharePoint &#40;SSAS&#41;](power-pivot-sharepoint/power-pivot-for-sharepoint-ssas.md) for more information.  
  
> [!NOTE]  
>  Excel Data Models and tabular models are architecturally similar. You can import an Excel Data Model into a tabular model if you need to support larger amounts of data or use other model features not available in Excel.  
  
 Tabular and multidimensional solutions are built using [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)] and are intended for corporate BI projects that run on a standalone [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] instance. Both solutions yield high performance analytical databases that integrate easily with Excel, Reporting Services reports, and other BI applications from Microsoft and third-party applications. Both solutions result in standalone databases that can be used by any client application that supports Analysis Services.  
  
 At a high level, differences between tabular and multidimensional models can be characterized as follows:  
  
-   Multidimensional and data mining solutions use OLAP modeling constructs (cubes and dimensions) and MOLAP, ROLAP, or HOLAP storage that use disk as the primary data storage for pre-aggregated data.  
  
-   Tabular solutions use relational modeling constructs such as tables and relationships for modeling data, and the in-memory analytics engine for storing and calculating data. Most, if not all, of the model is stored in RAM and is often much faster than its multidimensional counterpart.  
  
 For new projects, consider the tabular approach first. It will be faster to design, test, and deploy; and it will work better with the latest self-service BI applications from Microsoft.  
  
##  <a name="bkmk_ds"></a> Data Source Support by Solution Type  
 Multidimensional and tabular models use imported data from external sources. Most developers use a data warehouse, designed to support reporting data structures, as the primary data source behind a model. The data warehouse is often based on a star or snowflake schema, and SSIS is used to load data from OLTP solutions into the data warehouse. Modeling is simpler when you use a data warehouse as the backend data source.  
  
|**Link**|**Summary of supported options**|  
|--------------|--------------------------------------|  
|[Data Sources Supported &#40;SSAS Multidimensional&#41;](multidimensional-models/supported-data-sources-ssas-multidimensional.md)|Multidimensional models use data from relational data sources.|  
|[Data Sources Supported &#40;SSAS Tabular&#41;](tabular-models/data-sources-supported-ssas-tabular.md)|Tabular models support a broader range of data sources, including flat files, data feeds, and data sources that are accessed via ODBC data providers.|  
  
 Both modeling approaches can use data from multiple data sources in the same model.  
  
 If your solution calls for storing model data outside the model in the relational database (a technique used when data size requirements are especially large), the data source type must be a SQL Server relational database. Both ROLAP storage for multidimensional models and DirectQuery for tabular models have this requirement.  
  
 **Data Size**  
  
 Both tabular and multidimensional solutions use data compression that reduces the size of the Analysis Services database relative to the data warehouse from which you are importing data. Because actual compression will vary based on the characteristics of the underlying data, there is no way to know precisely how much disk and memory will be required by a solution after data is processed and used in queries. An estimate used by many Analysis Services developers is that primary storage of a multidimensional database will be about one third size of the original data.  
  
 Tabular databases can sometimes get greater amounts of compression, about one tenth the size, especially if most of the data is imported from fact tables. For tabular, memory requirements will be larger than the size of data on disk due to additional data structures that are created when the tabular database is loaded into memory. Under load, both disk and memory requirements for either solution type can be expected to increase as Analysis Services caches, stores, scans, and queries data.  
  
 For some projects, data requirements might be so large as to become a factor in choosing between the model types. If the data you need to load is many terabytes in size, a tabular solution might not meet your requirements if available memory cannot accommodate the data. There is a paging option that swaps in-memory data to disk, but very large amounts of data are better accommodated in multidimensional solutions. The largest [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] databases in production today are multidimensional. For more information about memory paging options for tabular solutions, see [Memory Properties](server-properties/memory-properties.md). For more information about scaling a multidimensional solution, see [Scale-Out Querying for Analysis Services with Read-Only Databases](https://go.microsoft.com/fwlink/?LinkId=251711).  
  
##  <a name="bkmk_models"></a> Model Features  
 The following table summarizes feature availability at the model level. If you already installed Analysis Services, you can use this information to understand the capabilities of the server mode you installed. If you are already familiar with model features in Analysis Services and your business requirements include one or more of these features, you can review this list to ensure that the feature you want to use is available in the type of model you plan to build.  
  
 For more information about how features compare by modeling approach, see the [Choosing a Tabular or Multidimensional Modeling Experience in SQL Server 2012 Analysis Services](https://go.microsoft.com/fwlink/?LinkId=251588) technical article on MSDN.  
  
> [!NOTE]  
>  Tabular modeling is supported in specific editions of SQL Server. For more information, see [Features Supported by the Editions of SQL Server 2014](../../2014/getting-started/features-supported-by-the-editions-of-sql-server-2014.md).  
  
||||  
|-|-|-|  
||**Multidimensional**|**Tabular**|  
|Actions|[Yes](multidimensional-models/actions-in-multidimensional-models.md)|No|  
|Aggregation objects|[Yes](multidimensional-models/designing-aggregations-analysis-services-multidimensional.md)|No|  
|Calculated Measures|[Yes](multidimensional-models/create-calculated-members.md)|Yes|  
|Custom Assemblies|[Yes](multidimensional-models/multidimensional-model-assemblies-management.md)|No|  
|Custom Rollups|Yes|No|  
|Distinct Count|[Yes](multidimensional-models/use-aggregate-functions.md)|Yes (via DAX)*|  
|Drillthrough|[Yes](multidimensional-models/actions-in-multidimensional-models.md)|Yes|  
|Hierarchies|[Yes](multidimensional-models/user-defined-hierarchies-create.md)|Yes|  
|KPIs|[Yes](multidimensional-models/key-performance-indicators-kpis-in-multidimensional-models.md)|Yes|  
|Linked measure groups|[Yes](multidimensional-models/linked-measure-groups.md)|No|  
|Many-to-many relationships|[Yes](multidimensional-models/define-a-many-to-many-relationship-and-many-to-many-relationship-properties.md)|No|  
|Parent-child Hierarchies|[Yes](multidimensional-models/parent-child-dimension.md)|Yes (via DAX)|  
|Partitions|[Yes](tabular-models/partitions-ssas-tabular.md)|  
|Perspectives|[Yes](multidimensional-models/perspectives-in-multidimensional-models.md)|[Yes](tabular-models/partitions-ssas-tabular.md)|  
|Semi-additive Measures|[Yes](multidimensional-models/define-semiadditive-behavior.md)|Yes (via DAX)|  
|Translations|[Yes](multidimensional-models/translations-in-multidimensional-models-analysis-services.md)|No|  
|User-defined Hierarchies|[Yes](multidimensional-models/user-defined-hierarchies-create.md)|Yes|  
|Writeback|[Yes](multidimensional-models/set-partition-writeback.md)|No|  
  
 *If your solution must support a very large number of distinct counts (such as many millions of customer IDs), consider Tabular first. It tends to be more performant in this scenario. See the section about distinct counts in the whitepaper, [Analysis Services Case Study: Using Tabular Models in Large-scale Commercial Solutions](https://msdn.microsoft.com/library/dn751533.aspx).  
  
##  <a name="bkmk_modelsize"></a> Model Size  
 The size of the model, in terms of total number of objects, does not vary by solution type. However, the design tools used to build each solution vary in how well they accommodate working with a large number of objects. A larger model is somewhat easier to build in [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)] because it provides more facilities for diagramming and listing objects by type in Object Explorer and Solution Explorer.  
  
 Very large models that consist of many hundreds of tables or dimensions are often built programmatically in Visual Studio, and not in the design tools. For more information about the maximum number of objects in a model, see [Maximum Capacity Specifications &#40;Analysis Services&#41;](multidimensional-models/olap-physical/maximum-capacity-specifications-analysis-services.md).  
  
##  <a name="bkmk_ext"></a> Programmability and Developer Experience  
 For tabular and multidimensional models, there is one object model shared for both modalities. AMO and ADOMD.NET support both modes. Neither client library was revised for tabular constructs so you will need to understand how multidimensional and tabular constructs and naming conventions relate to each other. As a first step, review the AMO-to-tabular programming sample to learn AMO programming against a tabular model. For more information, download the sample from the [codeplex web site](https://go.microsoft.com/fwlink/?LinkID=221036).  
  
 Tabular solutions only support one model.bim file per solution, which means that all work must be done in a single file. Development teams that are accustomed to working with multiple projects in a single solution might need to revise how they work when building a shared tabular solution.  
  
##  <a name="bkmk_lang"></a> Query and Scripting Language Support  
 Analysis Services includes MDX, DMX, DAX, XML/A, and ASSL. Support for these languages varies slightly by model type. If query and scripting language requirements are a consideration, review the following list.  
  
-   Tabular model databases support DAX calculations, DAX queries, and MDX queries.  
  
-   Multidimensional model databases support MDX calculations and MDX queries as well as ASSL.  
  
-   Data mining models support DMX and ASSL.  
  
-   Analysis Services PowerShell is supported for server and database administration. Model type (or server mode) is not a factor in use of the PowerShell cmdlets.  
  
 All databases support XML/A.  
  
##  <a name="bkmk_sec"></a> Security Feature Support  
 All Analysis Services solutions can be secured at the database level. More granular security options vary by mode. If granular security settings are requirement for your solution, review the following list to ensure the level of security you want is supported in the type of solution you want to build:  
  
-   Tabular model databases can use row-level security, using role-based permissions in Analysis Services.  
  
-   Multidimensional model databases can use dimension and cell-level security, using role-based permissions in Analysis Services.  
  
 Excel Data Models can be restored to a tabular mode server. Once the file is restored, it is decoupled from SharePoint (assuming you restored it from a SharePoint location), allowing you to use almost all of the tabular modeling features, including row-level security. The one tabular modeling feature that you cannot use on a restored workbook is linked tables.  
  
##  <a name="bkmk_designer"></a> Design Tools  
 Data modeling skills and technical expertise can vary widely among users who are tasked with building analytical models. If tool familiarity or user expertise is a consideration for your solution, compare the following experiences for model creation.  
  
|**Modeling Tool**|**How Used**|  
|-----------------------|------------------|  
|[!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)]|Use to create Tabular, Multidimensional, and Data Mining solutions. This authoring environment uses the Visual Studio shell to provide workspaces, property panes, and object navigation. Technical users who already use Visual Studio will most likely prefer this tool for building business intelligence applications. See [Tools and applications used in Analysis Services](tools-and-applications-used-in-analysis-services.md) for details.|  
|Excel 2013 and later, with the Power Pivot for Excel add-in|Power Pivot for Excel is a tool used to edit and enhance an Excel Data Model. It has a separate application workspace that opens over Excel, but uses the same visual metaphors (tabbed pages, grid layout, and formula bar) as Excel. Users who are proficient in Excel typically prefer this tool over [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)]. See [Power Pivot: Powerful data analysis and data modeling in Excel](https://support.office.com/en-ie/article/Power-Pivot-Powerful-data-analysis-and-data-modeling-in-Excel-d7b119ed-1b3b-4f23-b634-445ab141b59b).|  
  
##  <a name="bkmk_client"></a> Client and Reporting Applications  
 In previous releases, your choice of model type had an impact on which client applications you could use, but that distinction has diminished over time. Tabular and multidimensional provide mostly equivalent support with regards to client applications that connect to Analysis Services data. The following table is a list of Microsoft client applications that can be used with Analysis Services data models.  
  
|**Application**|**Description**|  
|---------------------|---------------------|  
|Excel PivotTable reports|Excel functionality is the same for both tabular and multidimensional models, although Writeback (an Analysis Services capability that Excel implements) is only supported for multidimensional.|  
|Reporting Services RDL reports|RDL reports, created in either Report Builder or Report Designer, can use any Analysis Services model, as well as Excel Data Models hosted on PowerPivot for SharePoint.|  
|PerformancePoint dashboards|In SharePoint, PerformancePoint dashboards can connect to all Analysis Services databases, including Excel Data Models. For more information, see [Create Data Connections (PerformancePoint Services)](https://go.microsoft.com/fwlink/?linkdID=218155).|  
|[!INCLUDE[ssCrescent](../includes/sscrescent-md.md)] in Office 365 or Power BI sites|Tabular models only.|  
|[!INCLUDE[ssCrescent](../includes/sscrescent-md.md)] in SharePoint on-premises|[!INCLUDE[ssCrescent](../includes/sscrescent-md.md)],as a ClickOnce application from SharePoint, can use either an Analysis Services cube or tabular model.|  
  
##  <a name="bkmk_deploymentmode"></a> Server Deployment Modes for Multidimensional and Tabular Solutions  
 An Analysis Services instance is installed in one of three modes that set the operational context of the server. The server mode you install will determine the type of solutions that can be deployed to that server. Storage and memory architecture are the primary differences among the modes, but additional differences apply. The three server modes are briefly described in the following table. For more information, see [Determine the Server Mode of an Analysis Services Instance](instances/determine-the-server-mode-of-an-analysis-services-instance.md).  
  
|Deployment mode|Description|  
|---------------------|-----------------|  
|0 - Multidimensional and Data Mining|Runs multidimensional and data mining solutions that you deploy to a default instance of Analysis Services. Deployment mode 0 is the default for an Analysis Services installation. For more information, see [Install Analysis Services in Multidimensional and Data Mining Mode](../../2014/sql-server/install/install-analysis-services-in-multidimensional-and-data-mining-mode.md).|  
|1 - PowerPivot for SharePoint|For Excel Data Model access, Analysis Services is an internal component of SharePoint. Analysis Services is installed in deployment mode 1 and accepts requests only from Excel Services in a SharePoint environment. For more information, see [PowerPivot for SharePoint 2010 Installation](../../2014/sql-server/install/powerpivot-for-sharepoint-2010-installation.md).|  
|2 - Tabular|Runs tabular solutions on a standalone instance of Analysis Services configured for deployment mode 2. For more information, see [Install Analysis Services in Tabular Mode](instances/install-windows/install-analysis-services.md).|  
  
 Note that server models are not interchangeable. At installation, you will choose a mode for server operation. You should install multiple instances, one for each server mode, to support all workloads.  
  
##  <a name="bkmk_sharePoint"></a> Hosting Platforms  
 Microsoft has several methodologies for hosting data, applications, reports, and collaboration. In this section, we cover Analysis Services interoperability with regards to each hosting platform.  
  
|**Platform**|**Description**|  
|------------------|---------------------|  
|Microsoft Azure|You can run any supported version and edition of Analysis Services on an Azure Virtual Machine. In contrast with Azure SQL Database, which is a service on Azure that provides much of the same functionality as an on-premises relational database engine, there is no Analysis Services equivalent on Azure. Installing, configuring, and running Analysis Services in an Azure VM is our only Azure based option.|  
|Office 365|Excel Online in Office 365 supports remote connections to tabular and multidimensional models that run on premises.|  
|Power BI sites in Office 365|In a Power BI site, Power View reports can connect to tabular data models that run on-premises.|  
|On Premises Servers (SharePoint and SQL Server instances)|An on premises database server (that is, a SQL Server instance that has Analysis Services installed) is still the primary means for making Analysis Services data available to reports and client applications. Tabular, multidimensional, and data mining solutions run on Analysis Services instances on a network, with no SharePoint dependency.<br /><br /> SQL Server integrates with SharePoint by adding support for PowerPivot data access and tabular data access. Investment in SharePoint and SQL Server integration grows when you maximize the number of features used from each product. If you have SharePoint, you can install SQL Server PowerPivot for SharePoint to enable PowerPivot data access and get the PowerPivot .bism connection files used to access tabular databases running on an external Analysis Services instance on a network server.<br /><br /> If you have both SharePoint and SQL Server, you can support the following combination of services and applications:<br /><br /> Analysis Services models (either tabular or multidimensional)<br /><br /> Middle tier SharePoint services (Excel Services, Reporting Services in SharePoint, or PerformancePoint services)<br /><br /> Browser clients or rich clients (Excel) for deeper data analysis and exploration.|  
  
##  <a name="bkmk_Next"></a> Next Step: Build a Solution  
 Now that you have a basic understanding of how the solutions compare, try out the following tutorials to learn the steps for creating each one. The following links take you to tutorials that explain the steps.  
  
-   Build a tabular model using the [Tabular Modeling &#40;Adventure Works Tutorial&#41;](tabular-modeling-adventure-works-tutorial.md).  
  
-   Build a multidimensional model using the [Multidimensional Modeling &#40;Adventure Works Tutorial&#41;](multidimensional-modeling-adventure-works-tutorial.md).  
  
-   Build a data mining model using the [Basic Data Mining Tutorial](../../2014/tutorials/basic-data-mining-tutorial.md).  
  
-   Build a PowerPivot model using the [PowerPivot for Excel Tutorial](https://go.microsoft.com/fwlink/?LinkId=251135).  
  
## See Also  
 [Analysis Services Instance Management](instances/analysis-services-instance-management.md)   
 [What's New in Analysis Services and Business Intelligence](what-s-new-in-analysis-services.md)   
 [What's New &#40;Reporting Services&#41;](../../2014/reporting-services/what-s-new-reporting-services.md)   
 [What's New in PowerPivot](https://go.microsoft.com/fwlink/?LinkId=238141)   
 [PowerPivot Help for SQL Server 2012](https://go.microsoft.com/fwlink/?LinkID=220946)   
 [PowerPivot BI Semantic Model Connection &#40;.bism&#41;](power-pivot-sharepoint/power-pivot-bi-semantic-model-connection-bism.md)   
 [Create and Manage Shared Data Sources &#40;Reporting Services in SharePoint Integrated Mode&#41;](../../2014/reporting-services/create-manage-shared-data-sources-reporting-services-sharepoint-integrated-mode.md)  
  
  
