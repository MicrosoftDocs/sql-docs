---
title: "Management of Data Mining Solutions and Objects | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "data mining [Analysis Services], managing"
  - "managing mining models"
ms.assetid: 06fc61dd-925c-4347-8677-7046ee5d2f6f
author: minewiskan
ms.author: owend
manager: craigg
---
# Management of Data Mining Solutions and Objects
  [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] provides client tools that you can use to manage existing mining structures and mining models. This section describes the management operations that you can perform using each environment.  
  
 In addition to these tools, you can manage data mining objects programmatically, by using AMO, or use other clients that connect to an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database, such as the Data Mining Add-ins for [!INCLUDE[ofprexcel](../../includes/ofprexcel-md.md)] 2007.  
  
## In this Section  
 [Moving Data Mining Objects](moving-data-mining-objects.md)  
  
 [Processing Requirements and Considerations &#40;Data Mining&#41;](processing-requirements-and-considerations-data-mining.md)  
  
 [Using SQL Server Profiler to Monitor Data Mining &#40;Analysis Services - Data Mining&#41;](using-sql-server-profiler-to-monitor-data-mining-analysis-services-data-mining.md)  
  
## Location of Data Mining Objects  
 Mining structures and models that have been processed are stored in an instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].  
  
 If you create a connection to an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database in `Immediate` mode when developing your data mining objects, any objects that you create are immediately added to the server as you work. However, if you design data mining objects in **Offline** mode, which is the default when you work in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], the mining objects that you create are only metadata containers until you deploy them to an instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. Therefore, any time that you make a change to an object, you must redeploy the object to the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] server. For more information about data mining architecture, see [Physical Architecture &#40;Analysis Services - Data Mining&#41;](physical-architecture-analysis-services-data-mining.md).  
  
> [!NOTE]  
>  Some clients, such as the Data Mining Add-ins for [!INCLUDE[ofprexcel](../../includes/ofprexcel-md.md)] 2007, also let you create session mining models and mining structures, which use a connection to an instance but store the mining structure and models on the server only for the duration of the session. You can still manage these models through the client, the same as you would structures and models stored in an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database, but the objects are not persisted after you disconnect from the instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].  
  
## Managing Data Mining Objects in SQL Server Data Tools  
 [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] offers features that make it easy to create, browse, and edit data mining objects.  
  
 The following links provide information on how you can modify data mining objects by using [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]:  
  
-   [Edit the Data Source View used for a Mining Structure](edit-the-data-source-view-used-for-a-mining-structure.md)  
  
-   [Change the Properties of a Mining Structure](change-the-properties-of-a-mining-structure.md)  
  
-   [Change the Properties of a Mining Model](change-the-properties-of-a-mining-model.md)  
  
-   [View or Change Modeling Flags &#40;Data Mining&#41;](modeling-flags-data-mining.md)  
  
-   [View or Change Algorithm Parameters](view-or-change-algorithm-parameters.md)  
  
 Typically you will use [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] as a tool for developing new projects and adding to existing projects, and then manage projects and objects that have been deployed by using tools such as [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
 However, you can directly modify objects that are already deployed to an instance of ssASnoversion by using the `Immediate` option and connecting to the server in Online mode. For more information, see [Connect in Online Mode to an Analysis Services Database](../multidimensional-models/connect-in-online-mode-to-an-analysis-services-database.md).  
  
> [!WARNING]  
>  All changes to a mining structure or mining model, including changes to metadata such as a name or description, require that the structure or model be reprocessed.  
  
 If you do not have the solution file that was used to create the data mining project or objects, you can import the existing project from the server using the Analysis Services Import wizard, make modifications to the objects, and then redeploy using the `Incremental` option. For more information, see [Import a Data Mining Project using the Analysis Services Import Wizard](import-a-data-mining-project-using-the-analysis-services-import-wizard.md).  
  
## Managing Data Mining Objects in SQL Server Management Studio  
 In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], you can script, process, or delete mining structures and mining models. You can view only a limited set of properties by using Object Explorer; however, you can view additional metadata about mining models by opening a **DMX Query** window and selecting a mining structure.  
  
-   [Create a DMX Query in SQL Server Management Studio](create-a-dmx-query-in-sql-server-management-studio.md)  
  
## Managing Data Mining Objects Programmatically  
 You can create, alter, process, and delete data mining objects by using the following programming languages. Each language is designed for different tasks and as a result, there might be restrictions on the type of operations that you can perform. For example, some properties of data mining objects cannot be changed by using Data Mining Extensions (DMX); you must use XMLA or AMO.  
  
### Analysis Management Objects (AMO)  
 Analysis Management Objects (AMO) is an object model built on top of XMLA that gives you full control over data mining objects. By using AMO, you can create, deploy, and monitor mining structures and mining models  
  
-   [AMO Concepts and Object Model](https://docs.microsoft.com/bi-reference/amo/amo-concepts-and-object-model)  
  
-   <xref:Microsoft.AnalysisServices>  
  
 **Restrictions:** None.  
  
### Data Mining Extensions (DMX)  
 Data Mining Extensions (DMX) can be used with other command interfaces such as [!INCLUDE[vstecado](../../includes/vstecado-md.md)] or ADOMD.Net to create, delete, and query mining structures and mining models.  
  
-   [Data Mining Extensions &#40;DMX&#41; Data Definition Statements](/sql/dmx/dmx-statements-data-definition)  
  
 **Restrictions:** Some properties cannot be changed by using DMX.  
  
### XML for Analysis (XMLA)  
 XML for Analysis (XMLA) is the data definition language for all of Analysis Services. XMLA gives you control over most data mining objects and server operations. All management operations between the client and the server can be performed by using XMLA. For convenience, you can use the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] Scripting Language (ASSL) to wrap the XML.  
  
 **Restrictions:** [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] generates some XMLA statements that are supported for internal use only, and cannot be used in XML DDL scripts.  
  
## See Also  
 [Developer's Guide &#40;Analysis Services&#41;](../analysis-services-developer-documentation.md)  
  
  
