---
title: "Query Design Tools in Report Designer SQL Server Data Tools (SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "graphical query designer [Reporting Services]"
  - "MDX query designer [Reporting Services]"
  - "text-based query designer [Reporting Services]"
  - "DMX [Reporting Services]"
  - "query designers [Reporting Services]"
  - "generic query designer See text-based query designer"
  - "Reporting Services, query designers"
  - "semantic queries [Reporting Services]"
  - "Report Model Query Designer"
ms.assetid: a8139a9d-4aeb-4e64-96f3-564edf60479f
author: markingmyname
ms.author: maghan
manager: craigg
---
# Query Design Tools in Report Designer SQL Server Data Tools (SSRS)
  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] provides a variety of query design tools that you can use to create dataset queries in Report Designer. The type of data source that you are working with determines the availability of a particular query designer. In addition, some query designers provide alternate modes so that you can choose whether to work in visual mode or directly in the query language. This topic introduces each tool and describes the type of data source each one supports. The following tools are described in this topic:  
  
-   [Text-based Query Designer](#Textbased)  
  
-   [Graphical Query Designer](#Graphical)  
  
-   [Report Model Query Designer](#Model)  
  
-   [MDX Query Designer](#MDX)  
  
-   [DMX Query Designer](#DMX)  
  
-   [SapNetWeaver BI Query Designer](#SAPBW)  
  
-   [Hyperion Essbase Query Designer](#Hyperion)  
  
 All of the query design tools run in the data design environment of [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] when you work with a Report Server project template or Report Server Wizard project template. For more information about working with the query designers, see [Reporting Services Query Designers](../reporting-services-query-designers.md).  
  
##  <a name="Textbased"></a> Text-based Query Designer  
 The text-based query designer is the default query building tool for most supported relational data sources, including [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], Oracle, Teradata, OLE DB, XML, and ODBC. In contrast with the graphical query designer, this query design tool does not validate query syntax during query design. The following image provides an illustration of the text-based query designer.  
  
 ![Generic query designer, for relational data query](../../analysis-services/media/rsqd-dsaw-sql-generic.gif "Generic query designer, for relational data query")  
  
 The text-based query designer is recommended for creating complex queries, using stored procedures, querying XML data, and for writing dynamic queries. Depending on the data source, you may be able to toggle the **Edit As Text** button on the toolbar to switch between the graphical query designer and the text-based query designer. For more information, see [Text-based Query Designer User Interface](../text-based-query-designer-user-interface.md).  
  
##  <a name="Graphical"></a> Graphical Query Designer  
 The graphical query designer is used to create or modify [!INCLUDE[tsql](../../includes/tsql-md.md)] queries that run against a relational database. This query design tool is used in several [!INCLUDE[msCoName](../../../includes/msconame-md.md)] products and in other [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] components. Depending on the data source type, it supports Text, StoredProcedure, and TableDirect modes. The following image provides an illustration of the graphical query designer.  
  
 ![Graphical query designer for sql query](../media/rsqd-dsaw-sql.gif "Graphical query designer for sql query")  
  
 You can toggle the **Edit As Text** button on the toolbar to switch between the graphical query designer and the text-based query designer. For more information, see [Graphical Query Designer User Interface](graphical-query-designer-user-interface.md).  
  
##  <a name="Model"></a> Report Model Query Designer  
 The Report Model query designer is used to create or modify queries that run against a SMDL report model that has been published to a report server. Reports that run against models support clickthrough data exploration. The query determines the path of data exploration at run time. The following image provides an illustration of the Report Model query designer.  
  
 ![Semantic Model Query Designer UI](../media/rsqd-dsawmodel-smql.gif "Semantic Model Query Designer UI")  
  
 To use the Report Model query designer, you must define a data source that points to a published model. When you define a dataset for the data source, you can open the dataset query in the Report Model query designer. The Report Model query designer can be used in graphical or text-based modes. You can toggle the **Edit As Text** button on the toolbar to switch between the graphical query designer and the text-based query designer. For more information, see [Report Model Query Designer User Interface](report-model-query-designer-user-interface.md).  
  
##  <a name="MDX"></a> MDX Query Designer  
 The Multidimensional Expression (MDX) query designer is used to create or modify queries that run against an [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] data source with multidimensional cubes. The following image provides an illustration of the MDX query designer after the query and filter are defined.  
  
 ![Analysis Services MDX query designer, design view](../../analysis-services/media/rsqd-dsawas-mdx-designmode.gif "Analysis Services MDX query designer, design view")  
  
 To use the MDX query designer, you must define a data source that has an Analysis Services cube available that is valid and has been processed. When you define a dataset for the data source, you can open the query in the MDX query designer. If necessary, use the MDX and DMX buttons on the toolbar to switch between MDX and DMX modes. For more information, see [Analysis Services MDX Query Designer User Interface](analysis-services-mdx-query-designer-user-interface.md).  
  
##  <a name="DMX"></a> DMX Query Designer  
 The Data Mining Prediction Expression (DMX) query designer is used to create or modify queries that run against an [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] data source with mining models. The following image provides an illustration of the DMX query designer after the model and input tables are selected.  
  
 ![Analysis Services DMX query designer, design view](../media/rsqd-dsawas-dmx-designmode.gif "Analysis Services DMX query designer, design view")  
  
 To use the DMX query designer, you must define a data source that has a valid, data mining model available. When you define a dataset for the data source, you can open the query in the DMX query designer. If necessary, use the MDX and DMX buttons on the toolbar to switch between MDX and DMX modes. After you select the model, you can create data mining prediction queries that provide data to a report. For more information, see [Analysis Services DMX Query Designer User Interface](analysis-services-dmx-query-designer-user-interface.md).  
  
##  <a name="SAPBW"></a> Sap NetWeaver BI Query Designer  
 The [!INCLUDE[SAP_DPE_BW_1](../../../includes/sap-dpe-bw-1-md.md)] query designer is used to retrieve data from a [!INCLUDE[SAP_DPE_BW_1](../../../includes/sap-dpe-bw-1-md.md)] database. To use this query designer, you must have an [!INCLUDE[SAP_DPE_BW_1](../../../includes/sap-dpe-bw-1-md.md)] data source that has at least one InfoCube, MultiProvider, or Web-enabled query defined. The following image provides an illustration of the [!INCLUDE[SAP_DPE_BW_1](../../../includes/sap-dpe-bw-1-md.md)] query designer.  
  
 ![Query Designer using MDX in Design Mode](../media/rsqd-dssapbw-mdx-designmode.gif "Query Designer using MDX in Design Mode")  
  
##  <a name="Hyperion"></a> Hyperion Essbase Query Designer  
 The [!INCLUDE[extEssbase](../../../includes/extessbase-md.md)] query designer is used to retrieve data from [!INCLUDE[extEssbase](../../../includes/extessbase-md.md)] databases and applications. The following image provides an illustration of the [!INCLUDE[extEssbase](../../../includes/extessbase-md.md)] query designer.  
  
 ![Query Designer for Hyperion Essbase data source](../media/rsqd-dshyperionessbase-mdx-designmode.gif "Query Designer for Hyperion Essbase data source")  
  
 To use this query designer, you must have a [!INCLUDE[extEssbase](../../../includes/extessbase-md.md)] data source that has at least one database. For more information, see [SAP NetWeaver BI Query Designer User Interface](sap-netweaver-bi-query-designer-user-interface.md).  
  
## See Also  
 [Reporting Services Tools](../tools/reporting-services-tools.md)   
 [Add Data to a Report &#40;Report Builder and SSRS&#41;](report-datasets-ssrs.md)   
 [Data Connections, Data Sources, and Connection Strings in Reporting Services](../data-connections-data-sources-and-connection-strings-in-reporting-services.md)   
 [Reporting Services Tutorials &#40;SSRS&#41;](../reporting-services-tutorials-ssrs.md)   
 [Data Sources Supported by Reporting Services &#40;SSRS&#41;](../create-deploy-and-manage-mobile-and-paginated-reports.md)   
 [Create an Embedded or Shared Data Source &#40;SSRS&#41;](../create-an-embedded-or-shared-data-source-ssrs.md)  
  
  
