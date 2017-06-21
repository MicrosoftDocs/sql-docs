---
title: "Comparing Tabular and Multidimensional Solutions (SSAS) | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "06/15/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "analysis-services/multidimensional-tabular"
  - "analysis-services/data-mining"
ms.tgt_pltfrm: ""
ms.topic: "get-started-article"
ms.assetid: 76ee5e96-6a04-49af-a88e-cb5fe29f2e9a
caps.latest.revision: 49
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Comparing tabular and multidimensional solutions
  SQL Server Analysis Services provides several approaches for creating a business intelligence semantic model: Tabular, Multidimensional, and Power Pivot for SharePoint.
  
 Having more than one approach enables a modeling experience tailored to different business and user requirements. Multidimensional is a mature technology built on open standards, embraced by numerous vendors of BI software, but can be hard to master. Tabular offers a relational modeling approach that many developers find more intuitive. Power Pivot is even simpler, offering visual data modeling in Excel, with server support provided via SharePoint.  
  
 All models are deployed as databases that run on an Analysis Services instance, accessed by client tools using a single set of data providers, and visualized in interactive and static reports via Excel, Reporting Services, Power BI, and BI tools from other vendors.  
  
 Tabular and multidimensional solutions are built using SSDT and are intended for corporate BI projects that run on a standalone [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] instance on-premises, and for tabular models, an [Azure Analysis Services](https://azure.microsoft.com/services/analysis-services/) server in the cloud. Both solutions yield high performance analytical databases that integrate easily with BI clients. Yet each solution differs in how they are created, used, and deployed. The bulk of this topic compares these two types so that you can identify the right approach for you.  
  
 For new projects, we generally recommend tabular models. Tabular models are faster to design, test, and deploy; and will work better with the latest self-service BI applications and cloud services from Microsoft.  
  
##  <a name="bkmk_overview"></a> Overview of modeling types  
 New to Analysis Services? The following table enumerates the different models, summarizes the approach, and identifies the initial release vehicle.  
 
 > [!NOTE]  
>  **Azure Analysis Services** supports tabular models at the 1200 and higher compatibility levels. However, not all tabular modeling functionality described in this topic is supported in Azure Analysis Services. While creating and deploying tabular models to Azure Analysis Services is much the same as it is for on-premises, it's important to understand the differences. To learn more , see [What is Azure Analysis Services?](https://docs.microsoft.com/azure/analysis-services/analysis-services-overview)
  
||||  
|-|-|-|  
|**Type**|**Modeling description**|**Released**|  
|Tabular|Relational modeling constructs (model, tables, columns). Internally, metadata is inherited from OLAP modeling constructs (cubes, dimensions, measures). Code and script use OLAP metadata.|SQL Server 2012 and later (compatibility levels 1050 - 1103) <sup>1</sup>|  
|Tabular in SQL Server 2016|Relational modeling constructs (model, tables, columns), articulated in tabular metadata object definitions in [Tabular Model Scripting Language (TMSL)](../analysis-services/tabular-model-scripting-language-tmsl-reference.md) and [Tabular Object Model (TOM)](../analysis-services/tabular-model-programming-compatibility-level-1200/introduction-to-the-tabular-object-model-tom-in-analysis-services-amo.md) code.|SQL Server 2016 (compatibility level 1200)| 
|Tabular in SQL Server 2017|Relational modeling constructs (model, tables, columns), articulated in tabular metadata object definitions in [Tabular Model Scripting Language (TMSL)](../analysis-services/tabular-model-scripting-language-tmsl-reference.md) and [Tabular Object Model (TOM)](../analysis-services/tabular-model-programming-compatibility-level-1200/introduction-to-the-tabular-object-model-tom-in-analysis-services-amo.md) code.|SQL Server 2017 (compatibility level 1400)| 
|Multidimensional|OLAP modeling constructs (cubes, dimensions, measures).|SQL Server 2000 and later|  
|Power Pivot|Originally an add-in, but now fully integrated into Excel. Visual modeling only, over an internal Tabular infrastructure. You can import a Power Pivot model into SSDT to create a new Tabular model that runs on an Analysis Services instance.|via Excel and Power Pivot BI Desktop|  
  
 <sup>1</sup> Compatibility levels are significant in the current release due to tabular metadata engine and support for scenario-enabling  features available only at the higher level. Later versions support earlier compatibility levels, but it is recommended you create new models or upgrade existing models to the highest compatibility level supported by the server version.
  
##  <a name="bkmk_models"></a> Model Features  
  The following table summarizes feature availability at the model level. Review this list to ensure that the feature you want to use is available in the type of model you plan to build.  
  
|||||  
|-|-|-|-|  
||Multidimensional|Tabular|Power Pivot|  
|Actions|Yes|No|No|  
|Aggregations|Yes|No|No|  
|Calculated Column|No|Yes|Yes|  
|Calculated Measures|Yes|Yes|Yes|  
|Calculated Tables|No|Yes <sup>1</sup>|No|  
|Custom Assemblies|Yes|No|No|  
|Custom Rollups|Yes|No|No|  
|Default Member|Yes|No|No|  
|Display folders|Yes|Yes <sup>1</sup>|No|  
|Distinct Count|Yes|Yes (via DAX)|Yes (via DAX)|  
|Drillthrough|Yes|Yes (detail opens in separate worksheet)|Yes|  
|Hierarchies|Yes|Yes|Yes|  
|KPIs|Yes|Yes|Yes|  
|Linked objects|Yes|Yes (linked tables)|No|  
|Many-to-many relationships|Yes|No (but there is [bi-directional cross filters](../analysis-services/tabular-models/bi-directional-cross-filters-tabular-models-analysis-services.md) at 1200 and higher compatibility levels)|No|  
|Named sets|Yes|No|No|  
|Ragged Hierarchies|Yes|Yes|No|  
|Parent-child Hierarchies|Yes|Yes (via DAX)|Yes (via DAX)|  
|Partitions|Yes|Yes|Yes|  
|Perspectives|Yes|Yes|Yes| 
|Row-level Security|Yes|Yes|No| 
|Semi-additive Measures|Yes|Yes|Yes|  
|Translations|[Yes](../analysis-services/multidimensional-models/translations-in-multidimensional-models-analysis-services.md)|Yes|[Yes](../analysis-services/tabular-models/translations-in-tabular-models-analysis-services.md)|  
|User-defined Hierarchies|Yes|Yes|Yes|  
|Writeback|Yes|No|No|  
  
 <sup>1</sup> See [Compatibility Level for Tabular models in Analysis Services](../analysis-services/tabular-models/compatibility-level-for-tabular-models-in-analysis-services.md) for information about functional differences between compatibility levels.  
  
##  <a name="bkmk_ds"></a> Data Considerations  
 Tabular and multidimensional models use imported data from external sources. The amount and type of data you need to import can be a primary consideration when deciding which model type best fits your data.  
  
 **Compression**  
  
 Both tabular and multidimensional solutions use data compression that reduces the size of the Analysis Services database relative to the data warehouse from which you are importing data. Because actual compression will vary based on the characteristics of the underlying data, there is no way to know precisely how much disk and memory will be required by a solution after data is processed and used in queries.  
  
 An estimate used by many Analysis Services developers is that primary storage of a multidimensional database will be about one third size of the original data. Tabular databases can sometimes get greater amounts of compression, about one tenth the size, especially if most of the data is imported from fact tables.  
  
 **Size of the model and resource bias (in-memory or disk)**  
  
 The size of an Analysis Services database is constrained only by the resources available to run it. The model type and storage mode will also play a role in how large the database can grow.  
  
 Tabular databases run either in-memory or in DirectQuery mode that offloads query execution to an external database. For tabular in-memory analytics, the database is stored entirely in memory, which means you must have sufficient memory to not only load all the data, but also  additional data structures created to support queries.  
  
 DirectQuery, revamped in SQL Server 2016, has fewer restrictions than before, and better performance. Leveraging the backend relational database for storage and query execution makes building a large scale Tabular model more feasible than was previously possible.  
  
 Historically, the largest databases in production are multidimensional, with processing and query workloads running independently on dedicated hardware, each one optimized for its respective use.  Tabular databases are catching up quickly, and new advancements in DirectQuery will help close the gap even further.  
  
 For multidimensional offloading data storage and query execution is available via ROLAP.   On a query server, rowsets can be cached, and stale ones  paged out. Efficient and balanced use of memory and disk resources often guide customers to multidimensional solutions.  
  
 Under load, both disk and memory requirements for either solution type can be expected to increase as Analysis Services caches, stores, scans, and queries data. For more information about memory paging options, see [Memory Properties](../analysis-services/server-properties/memory-properties.md). To learn more about scale, see [High availability and Scalability in Analysis Services](../analysis-services/instances/high-availability-and-scalability-in-analysis-services.md).  
  
 Power Pivot for Excel has an artificial file size limit of 2 gigabytes, which is imposed so that workbooks created in Power Pivot for Excel can be uploaded to SharePoint, which sets maximum limits on file upload size. One of the main reasons for migrating a Power Pivot workbook to a tabular solution on a standalone Analysis Services instance is to get around the file size limitation. For more information about configuring maximum file upload size, see [Configure Maximum File Upload Size &#40;Power Pivot for SharePoint&#41;](../analysis-services/power-pivot-sharepoint/configure-maximum-file-upload-size-power-pivot-for-sharepoint.md).  
  
 **Supported Data Sources**  
  
 Tabular models can import data from relational data sources, data feeds, and some document formats. You can also use OLE DB for ODBC providers with tabular models. Tabular models at the 1400 compatibility level offer a signifcant increase in the variety of data sources from which you can import from. This is due to the introduction of the modern Get Data data query and import features in SSDT.   

  Multidimensional solutions can import data from relational data sources using OLE DB native and managed providers.  
  
 To view the list of external data sources that you can import to each model, see the following topics:  
  
-   [Data Sources Supported &#40;SSAS Tabular&#41;](../analysis-services/tabular-models/data-sources-supported-ssas-tabular.md)  

-   [Supported Data Sources &#40;SSAS - Multidimensional&#41;](../analysis-services/multidimensional-models/supported-data-sources-ssas-multidimensional.md)  
  

  
##  <a name="bkmk_lang"></a> Query and Scripting Language Support  
 Analysis Services includes MDX, DMX, DAX, XML/A, ASSL, and TMSL. Support for these languages can vary by model type. If query and scripting language requirements are a consideration, review the following list.  

-   Tabular model databases support DAX calculations, DAX queries, and MDX queries. This is true at all compatibilities levels. Script languages are ASSL (over XMLA) for compatibility levels 1050-1103, and TMSL (over XMLA) for compatibility level 1200 and higher. 

-   Power Pivot workbooks use DAX for calculations, and DAX or MDX for queries.  
  
-   Multidimensional model databases support MDX calculations, MDX queries, DAX queries, and ASSL. 
  
-   Data mining models support DMX and ASSL.  
  
-   Analysis Services PowerShell is supported for Tabular and Multidimensional models and databases.  
  
 All databases support XML/A. See [Query and Expression Language Reference &#40;Analysis Services&#41;](http://msdn.microsoft.com/library/9597533d-35f4-4742-9d8c-7af392163527) and [Analysis Services Developer Documentation](../analysis-services/analysis-services-developer-documentation.md) for more information.  
  
##  <a name="bkmk_sec"></a> Security Features  
 All Analysis Services solutions can be secured at the database level. More granular security options vary by mode. If granular security settings are requirement for your solution, review the following list to ensure the level of security you want is supported in the type of solution you want to build:  

  
-   Tabular model databases can use row-level security, using role-based permissions.  
  
-   Multidimensional model databases can use dimension and cell-level security, using role-based permissions.  

-   [!INCLUDE[ssGemini](../includes/ssgemini-md.md)] workbooks are secured at the file level, using SharePoint permissions.  
  
 [!INCLUDE[ssGemini](../includes/ssgemini-md.md)] workbooks can be restored to a Tabular mode server. Once the file is restored, it is decoupled from SharePoint, allowing you to use all tabular modeling features, including row-level security.  
  
##  <a name="bkmk_designer"></a> Design Tools  
 Data modeling skills and technical expertise can vary widely among users who are tasked with building analytical models. If tool familiarity or user expertise is a consideration for your solution, compare the following experiences for model creation.  
  
|Modeling Tool|How Used|  
|-------------------|--------------|  
|[!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)]|Use to create tabular, multidimensional, and data mining solutions. This authoring environment uses the Visual Studio shell to provide workspaces, property panes, and object navigation. Technical users who already use Visual Studio will most likely prefer this tool for building business intelligence applications.|  
|[!INCLUDE[ssGemini](../includes/ssgemini-md.md)] for Excel|Use to create a [!INCLUDE[ssGemini](../includes/ssgemini-md.md)] workbook that you later deploy to a SharePoint farm that has an installation of [!INCLUDE[ssGemini](../includes/ssgemini-md.md)] for SharePoint. [!INCLUDE[ssGemini](../includes/ssgemini-md.md)] for Excel has a separate application workspace that opens over Excel. It uses the same visual metaphors (tabbed pages, grid layout, and formula bar) as Excel. Users who are proficient in Excel will prefer this tool over [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)].|  
  
##  <a name="bkmk_client"></a> Client Application Support  
 In-general, tabular and multidimensional solutions support client applications using one or more of the Analysis Services client libraries (MSOLAP, AMOMD, ADOMD). For example, Excel, Power BI Desktop, and custom applications.   
 
 If you are using Reporting Services, report feature availability varies across editions and server modes. For this reason, the type of report that you want to build might influence which server mode you choose to install.  
  
 [!INCLUDE[ssCrescent](../includes/sscrescent-md.md)], a Reporting Services authoring tool that runs in SharePoint, is available on a report server that is deployed in a SharePoint 2010 farm. The only type of data source that can be used with this report is an Analysis Services tabular model database or a [!INCLUDE[ssGemini](../includes/ssgemini-md.md)] workbook. This means that you must have a tabular mode server or a [!INCLUDE[ssGemini](../includes/ssgemini-md.md)] for SharePoint server to host the data source used by this type of report. You cannot use a multidimensional model as a data source for a [!INCLUDE[ssCrescent](../includes/sscrescent-md.md)] report. You must create a [!INCLUDE[ssGemini](../includes/ssgemini-md.md)] BI Semantic Model connection or a Reporting Services shared data source to use as the data source for a [!INCLUDE[ssCrescent](../includes/sscrescent-md.md)] report.  
  
 Report Builder and Report Designer can use any Analysis Services database, including [!INCLUDE[ssGemini](../includes/ssgemini-md.md)] workbooks that are hosted on [!INCLUDE[ssGemini](../includes/ssgemini-md.md)] for SharePoint.  
  
 Excel PivotTable reports are supported by all Analysis Services databases. Excel functionality is the same whether you use a tabular .database, multidimensional database, or [!INCLUDE[ssGemini](../includes/ssgemini-md.md)] workbook, although Writeback is only supported for multidimensional databases.  
 
  
  
## See Also  
 [Analysis Services Instance Management](../analysis-services/instances/analysis-services-instance-management.md)   
 [What's New in Analysis Services](../analysis-services/what-s-new-in-analysis-services.md)     

  
  