---
title: "Tabular Model Data Access | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 6ae74a8b-0025-450d-94a5-4e601831d420
author: minewiskan
ms.author: owend
manager: craigg
---
# Tabular Model Data Access
  Tabular model databases in Analysis Services can be accessed by most of the same clients, interfaces, and languages that you use to retrieve data or metadata from a multidimensional model. For more information, see [Multidimensional Model Data Access &#40;Analysis Services - Multidimensional Data&#41;](../multidimensional-models/mdx/multidimensional-model-data-access-analysis-services-multidimensional-data.md).  
  
 This topic describes the clients, query languages, and programmatic interfaces that work with tabular models.  
  
## Clients  
 The following Microsoft client applications support native connections to [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] tabular model databases.  
  
### Excel  
 You can connect to tabular model databases from Excel, using the data visualization and analysis capabilities in Excel to work with your data. To access the data, you define an Analysis Services data connection, specify a server that runs in tabular server mode, and then choose the database you want to use. For more information, see [Connect to or import data from SQL Server Analysis Services](https://go.microsoft.com/fwlink/?linkID=215150).  
  
 Excel is also the recommended application for browsing tabular models in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]. The tool includes an **Analyze in Excel** option that starts a new instance of Excel, creates an Excel workbook, and opens a data connection from the workbook to the model workspace database. When browsing tabular model data in Excel, be aware that Excel issues queries against the model using the Excel PivotTable client. Accordingly, operations within the Excel workbook result in MDX queries being sent to the workspace database, not DAX queries. If you are using SQL Profiler or another monitoring tool to monitor queries, you can expect to see MDX and not DAX in the profiler trace. For more information about the Analyze in Excel feature, see [Analyze in Excel &#40;SSAS Tabular&#41;](analyze-in-excel-ssas-tabular.md).  
  
### Power View  
 [!INCLUDE[ssCrescent](../../includes/sscrescent-md.md)] is a Reporting Services reporting client application that runs in a SharePoint 2010 environment. It combines data exploration, query design, and presentation layout into an integrated ad-hoc reporting experience. [!INCLUDE[ssCrescent](../../includes/sscrescent-md.md)] can use tabular models as data sources, regardless of whether the model is hosted on an instance of Analysis Services running in tabular mode, or retrieved from a relational data store by using DirectQuery mode. To connect to a tabular model in [!INCLUDE[ssCrescent](../../includes/sscrescent-md.md)], you must create a connection file that contains the server location and database name. You can create a Reporting Services shared data source or a BI semantic model connection file in SharePoint. For more information about BI semantic model connections, see [PowerPivot BI Semantic Model Connection &#40;.bism&#41;](../power-pivot-sharepoint/power-pivot-bi-semantic-model-connection-bism.md).  
  
 The [!INCLUDE[ssCrescent](../../includes/sscrescent-md.md)] client determines the structure of the specified model by sending a request to the specified data source, which returns a schema that can be used by the client to create queries against the model as a data source and perform operations based on the data. Subsequent operations in the [!INCLUDE[ssCrescent](../../includes/sscrescent-md.md)] user interface to filter data, perform calculations or aggregations, and display associated data are controlled by the client and cannot be programmatically manipulated.  
  
 The queries that are sent by the [!INCLUDE[ssCrescent](../../includes/sscrescent-md.md)] client to the model are issued as DAX statements, which you can monitor by setting a trace on the model.  The client also issues a request to the server for the initial schema definition, which is presented according to the Conceptual Schema Definition Language (CSDL). For more information, see [CSDL Annotations for Business Intelligence &#40;CSDLBI&#41;](https://docs.microsoft.com/bi-reference/csdl/csdl-annotations-for-business-intelligence-csdlbi)  
  
### SQL Server Management Studio  
 You can use [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to manage instances that host tabular models, and to query the metadata and data in them. You can process models or the objects in a model, create and manage partitions, and set security that can be used for managing data access. For more information, see the following topics:  
  
-   [Determine the Server Mode of an Analysis Services Instance](../instances/determine-the-server-mode-of-an-analysis-services-instance.md)  
  
-   [Connect to Analysis Services](../instances/connect-to-analysis-services.md)  
  
-   [Monitor an Analysis Services Instance](../instances/monitor-an-analysis-services-instance.md)  
  
 You can use both the MDX and XMLA query windows in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to retrieve data and metadata from a tabular model database. However, note the following restrictions:  
  
-   Statements using MDX and DMX are not supported for models that have been deployed in DirectQuery mode; therefore, if you need to create a query against a tabular model in DirectQuery mode, you should use an **XMLA Query** window instead.  
  
-   You cannot change the database context of the XMLA Query window after you have opened the **Query** window. Therefore, if you need to send a query to a different database or to a different instance, you must open that database or instance using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] and open a new **XMLA Query** window within that context.  
  
 You can create traces against an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] tabular model as you would on a multidimensional solution. In this release, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] provides many new events that can be used to track memory usage, query and processing operations, and file usage. For more information, see [Analysis Services Trace Events](https://docs.microsoft.com/bi-reference/trace-events/analysis-services-trace-events).  
  
> [!WARNING]  
>  If you put a trace on a tabular model database, you might see some events that are categorized as DMX queries. However, data mining is not supported on tabular model data, and the DMX queries executed on the database are limited to SELECT statements on the model metadata. The events are categorized as DMX only because the same parser framework is used for MDX.  
  
## Query Languages  
 Analysis Services tabular models support most of the same query languages that are provided for access to multidimensional models. The exception is tabular models that have been deployed in DirectQuery mode, which do not retrieve data from an Analysis Services data store, but retrieve data directly from a SQL Server data source. You cannot query these models using MDX, but must use a client that supports conversion of DAX expressions to Transact-SQL statements, such as the [!INCLUDE[ssCrescent](../../includes/sscrescent-md.md)] client.  
  
### DAX  
 You can use DAX for creating expressions and formulas in all kinds of tabular models, regardless of whether the model is stored on SharePoint as a PowerPivot-enabled Excel workbook, or on an instance of Analysis Services.  
  
 Additionally, you can use DAX expressions within the context of an XMLA EXECUTE command statement to send queries to a tabular model that has been deployed in DirectQuery mode.  
  
 For examples of queries on a tabular model using DAX, see [DAX Query Syntax Reference](https://msdn.microsoft.com/library/ee634217.aspx).  
  
### MDX  
 You can use MDX to create queries against tabular models that use the in-memory cache as the preferred query method (that is, models that have not been deployed in DirectQuery mode). Although clients such as [!INCLUDE[ssCrescent](../../includes/sscrescent-md.md)] use DAX both for creating aggregations and for querying the model as a data source, if you are familiar with MDX it can be a shortcut to create sample queries in MDX, see [Building Measures in MDX](../multidimensional-models/mdx/mdx-building-measures.md).  
  
### CSDL  
 The Conceptual Schema Definition Language is not a query language, per se, but it can be used to retrieve information about the model and model metadata, that can later be used to create reports or create queries against the model.  
  
 For information about how CSDL is used in tabular models, see [CSDL Annotations for Business Intelligence &#40;CSDLBI&#41;](https://docs.microsoft.com/bi-reference/csdl/csdl-annotations-for-business-intelligence-csdlbi).  
  
## Programmatic Interfaces  
 The principal interfaces that are used for interacting with [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] tabular models are the schema rowsets, XMLA, and the query clients and query tools provided by [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] and [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)].  
  
### Data and Metadata  
 You can retrieve data and metadata from tabular models in managed applications using ADOMD.NET. For examples of applications that create and modify objects in a tabular model, see the following resources:  
  
-   Tabular Model AMO Sample on Codeplex  
  
-   [Use Dynamic Management Views &#40;DMVs&#41; to Monitor Analysis Services](../instances/use-dynamic-management-views-dmvs-to-monitor-analysis-services.md)  
  
 You can use the Analysis Services 9.0 OLE DB provider in unmanaged client applications to support OLE DB access to tabular models. An updated version of the Analysis Services OLE DB provider is required to enable tabular model access. For more information about providers used with tabular models, see [Install the Analysis Services OLE DB Provider on SharePoint Servers](../../sql-server/install/install-the-analysis-services-ole-db-provider-on-sharepoint-servers.md) .  
  
 You can also retrieve data directly from an Analysis Services instance in an XML-based format. You can retrieve the schema of the tabular model by using the DISCOVER_CSDL_METADATA rowset, or you can use an EXECUTE or DISCOVER command with existing ASSL elements, objects, or properties. For more information, see the following resources:  
  
-   [CSDL Annotations for Business Intelligence &#40;CSDLBI&#41;](https://docs.microsoft.com/bi-reference/csdl/csdl-annotations-for-business-intelligence-csdlbi)  
  
### Manipulate Analysis Services Objects  
 You can create, modify, delete, and process tabular models and objects in them, including tables, columns, perspectives, measures, and partitions, using XMLA commands, or by using AMO. Both AMO and XMLA have been updated to support additional properties that are used in tabular models for enhanced reporting and modeling.  
  
 For examples of how tabular objects can be scripted using AMO and XMLA, see the following resources:  
  
-   Tabular Model AMO Sample on Codeplex  
  
-   AdventureWorks Samples on CodePlex  
  
 You can use PowerShell to manage and monitor instances of Analysis Services, as well as for creating and monitoring security used for tabular model access. For more information, see [Analysis Services PowerShell](../analysis-services-powershell.md).  
  
### Schema Rowsets  
 Client applications can use the schema rowsets to examine the metadata of tabular models and to retrieve support and monitoring information from the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] server. In this release of SQL Server new schema rowsets have been added and existing schema rowsets extended to support features related to tabular models and to enhance monitoring and performance analysis across [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].  
  
-   [DISCOVER_CALC_DEPENDENCY Rowset](https://docs.microsoft.com/bi-reference/schema-rowsets/xml/discover-calc-dependency-rowset)  
  
     New schema rowset for tracking dependencies among the columns and references in a tabular model  
  
-   [DISCOVER_CSDL_METADATA Rowset](https://docs.microsoft.com/bi-reference/schema-rowsets/xml/discover-csdl-metadata-rowset)  
  
     New schema rowset for obtaining the CSDL representation of a tabular model  
  
-   [DISCOVER_XEVENT_TRACE_DEFINITION Rowset](../dev-guide/discover-xevent-trace-definition-rowset.md)  
  
     New schema rowset for monitoring SQL Server Extended Events. For more information, see [Use SQL Server Extended Events &#40;XEvents&#41; to Monitor Analysis Services](../instances/monitor-analysis-services-with-sql-server-extended-events.md).  
  
-   [DISCOVER_TRACES Rowset](https://docs.microsoft.com/bi-reference/schema-rowsets/xml/discover-traces-rowset)  
  
     New `Type` column lets you filter traces by category. For more information, see [Create Profiler Traces for Replay &#40;Analysis Services&#41;](../instances/create-profiler-traces-for-replay-analysis-services.md).  
  
-   [MDSCHEMA_HIERARCHIES Rowset](https://docs.microsoft.com/bi-reference/schema-rowsets/ole-db-olap/mdschema-hierarchies-rowset)  
  
     New `STRUCTURE_TYPE` enumeration supports identification of user-defined hierarchies created in tabular models. For more information, see [Hierarchies &#40;SSAS Tabular&#41;](hierarchies-ssas-tabular.md).  
  
 There are no updates to the OLE DB for Data Mining schema rowsets in this release.  
  
> [!WARNING]  
>  You cannot use MDX or DMX queries in a database that has been deployed in DirectQuery mode; therefore, if you need to execute a query against a DirectQuery model using the schema rowsets, you should use XMLA, and not the associated DMV. For DMVs that return results for the server as a whole, such as SELECT * from $system.DBSCHEMA_CATALOGS or DISCOVER_TRACES, you can execute the query in the content of a database that is deployed in a cached mode.  
  
## See Also  
 [Connect to a Tabular Model Database &#40;SSAS&#41;](connect-to-a-tabular-model-database-ssas.md)   
 [PowerPivot Data Access](../power-pivot-sharepoint/power-pivot-data-access.md)   
 [Connect to Analysis Services](../instances/connect-to-analysis-services.md)  
  
  
