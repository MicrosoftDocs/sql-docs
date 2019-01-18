---
title: "Data Mining Query Tools | Microsoft Docs"
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
# Data Mining Query Tools
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  All data mining queries use the Data Mining Extensions (DMX) language. DMX can be used to create models for all kinds of machine learning tasks, including classification, risk analysis, generation of recommendations, and linear regression. You can also write DMX queries to get information about the patterns and statistics that were generated when you processed the model.  
  
 You can write your own DMX, or you can build basic DMX using a tool such as the **Prediction Query Builder** and then modify it. Both [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] and [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] provide tools that help you build DMX prediction queries. This topic describes how to create and execute data mining queries using these tools.  
  
-   [Prediction Query Builder](#bkmk_Builder)  
  
-   [Query Editor](#bkmk_QueryEditor)  
  
-   [DMX Templates](#bkmk_Templates)  
  
-   [Integration Services](#bkmk_SSIS)  
  
-   [Application Programming Interfaces](#bkmk_API)  
  
##  <a name="bkmk_Builder"></a> Prediction Query Builder  
 Prediction Query Builder is included in the **Mining Model Prediction** tab of Data Mining Designer, which is available in both [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]and [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)].  
  
 When you use the query builder, you select a mining model, add new case data, and add prediction functions. You can then switch to the text editor to modify the query manually, or switch to the **Results** pane to view the results of the query.  
  
##  <a name="bkmk_QueryEditor"></a> Query Editor  
 The Query Editor in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] also lets you build and run DMX queries. You can connect to an instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], and then select a database, mining structure columns, and a mining model. The **Metadata Explorer** contains a list of prediction functions that you can browse.  
  
##  <a name="bkmk_Templates"></a> DMX Templates  
 [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] provides interactive DMX query templates that you can use to build DMX queries. If you do not see the list of templates, click **View** on the toolbar, and select **Template Explorer**. To see all [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] templates, including templates for DMX, MDX, and XMLA, click the cube icon.  
  
 To build a query using a template, you can drag the template into an open query window, or you can double-click the template to open a new connection and a new query pane.  
  
 For an example of how to create a prediction query from a template, see [Create a Singleton Prediction Query from a Template](../../analysis-services/data-mining/create-a-singleton-prediction-query-from-a-template.md).  
  
> [!WARNING]  
>  The Data Mining Add-in for Microsoft Office Excel also contains a number of templates, along with an interactive query builder which can help you compose complex DMX statements. To use the templates, click **Query**, and click **Advanced** in the Data Mining Client.  
  
##  <a name="bkmk_SSIS"></a> Integration Services Data Mining Components  
 You can also include prediction queries as part of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package. The following tasks and transformations in [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] support the creation and execution of DMX prediction queries and DMX statements.  
  
|Component|Description|  
|---------------|-----------------|  
|Data Mining Query task|Executes DMX queries and other DMX statements as part of a control flow.<br /><br /> The task editor provides the Prediction Query Builder, and a text box for modifying the DMX query manually. However, the task editor cannot validate the query against objects in an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] solution. Therefore, it is best to create a query within [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)] or [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] and then paste the text of the statement or query into the task editor.|  
|Data Mining Query transformation|Executes a prediction query within a data flow, using data supplied by a data flow source.<br /><br /> The task editor provides the Prediction Query Builder, and a text box for modifying the DMX query manually.<br /><br /> The transformation can only be used for creating queries that use data in the data flow; that is, queries that use the PREDICTION JOIN syntax. This component cannot be used for executing content queries or other kinds of DMX statements.|  
  
##  <a name="bkmk_API"></a> Application Programming Interfaces  
 You can create custom applications that execute queries against data mining models by using a variety of programming languages, in combination with server protocols such as OLE DB or Analysis Services ADOMD client. For more information, see [Data Mining Programming](../../analysis-services/data-mining-programming.md).  
  
 However, XMLA constitutes the underlying message format for all interactions with an Analysis Service server. Within an XMLA message, queries are represented differently depending on whether you are sending a prediction query based on DMX, a content query, or a query that retrieves model metadata using the data mining schema rowsets.  
  
-   The text of **prediction queries** (and all other DMX statements) is sent in XMLA by using the [Execute Method &#40;XMLA&#41;](https://docs.microsoft.com/bi-reference/xmla/xml-elements-methods-execute) method, with the DMX query placed as text within the [Statement Element &#40;XMLA&#41;](https://docs.microsoft.com/bi-reference/xmla/xml-elements-commands/statement-element-xmla) element of the XMLA [Command Element &#40;XMLA&#41;](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/command-element-xmla) element.  
  
-   To retrieve **model content** and **model metadata**, such as the number of clusters, the attributes used in decision trees, the date the model was last processed, and the algorithm parameters used when creating the model, you can use the [Discover Method &#40;XMLA&#41;](https://docs.microsoft.com/bi-reference/xmla/xml-elements-methods-discover) method and specify one of the data mining schema rowsets in the [RequestType Element &#40;XMLA&#41;](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/requesttype-element-xmla) header. To narrow the scope of the query, enter criteria as restrictions within the [RestrictionList Element &#40;XMLA&#41;](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/restrictionlist-element-xmla) element.  
  
## See Also  
 [Data Mining Extensions &#40;DMX&#41; Reference](../../dmx/data-mining-extensions-dmx-reference.md)   
 [Data Mining Solutions](../../analysis-services/data-mining/data-mining-solutions.md)   
 [Understanding the DMX Select Statement](../../dmx/understanding-the-dmx-select-statement.md)   
 [Structure and Usage of DMX Prediction Queries](../../dmx/structure-and-usage-of-dmx-prediction-queries.md)   
 [Create a Prediction Query Using the Prediction Query Builder](../../analysis-services/data-mining/create-a-prediction-query-using-the-prediction-query-builder.md)   
 [Create a DMX Query in SQL Server Management Studio](../../analysis-services/data-mining/create-a-dmx-query-in-sql-server-management-studio.md)  
  
  
