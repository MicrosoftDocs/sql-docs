---
title: "Data Mining Query Interfaces | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "predictions [Analysis Services], DMX prediction queries"
  - "predictions [DMX]"
  - "DMX [Analysis Services], prediction queries"
  - "prediction queries [DMX]"
  - "predictions [Analysis Services]"
  - "queries [DMX], prediction queries"
  - "mining models [Analysis Services], DMX"
ms.assetid: a8952427-fd8c-4300-8f62-25f57ac1be0c
author: minewiskan
ms.author: owend
manager: craigg
---
# Data Mining Query Interfaces
  Data mining queries are based on the Data Mining Extensions (DMX) language. You use DMX for all prediction and modeling tasks, including classification, risk analysis, generation of recommendations, and linear regression. You can also retrieve the patterns and statistics that were generated when you processed the model.  
  
 The syntax for a prediction query using DMX is similar to the syntax for a query in [!INCLUDE[tsql](../../includes/tsql-md.md)]. Both [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] and [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] provide tools that help you build DMX prediction queries.  
  
 This topic describes the interfaces that you can use to create and execute data mining queries using DMX.  
  
 [Query Tools](#bkmk_Tools)  
  
-   [Prediction Query Builder](#bkmk_Builder)  
  
-   [Query Editor](#bkmk_QueryEditor)  
  
-   [DMX Templates](#bkmk_Templates)  
  
-   [Integration Services](#bkmk_SSIS)  
  
 [Application Programming Interfaces](#bkmk_API)  
  
##  <a name="bkmk_Tools"></a> Data Mining Query Tools  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides the following tools that you can use to build prediction queries, content queries, and data definition queries against data mining objects:  
  
-   Prediction Query Builder  
  
-   Query Editor  
  
-   DMX templates  
  
-   Integration Services data mining components  
  
###  <a name="bkmk_Builder"></a> Prediction Query Builder  
 Prediction Query Builder is included in the **Mining Model Prediction** tab of Data Mining Designer, which is available in both [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]and [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)].  
  
 When you use the query builder, you can use graphical tools to select a mining model, add new case data, and add prediction functions. The Prediction Query Builder includes a text editor that you can use to modify the query manually, and a simple **Results** pane to view the results of the query.  
  
###  <a name="bkmk_QueryEditor"></a> Query Editor  
 The Query Editor in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] provides tools that you can use to build and run DMX queries. You can connect to an instance of SQL Server Analysis Services, and then select a database, mining structure columns, and a mining model. The **Metadata Explorer** contains a list of prediction functions that you can browse.  
  
###  <a name="bkmk_Templates"></a> DMX Templates  
 [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] provides interactive DMX query templates that you can use to build DMX queries. If you do not see the list of templates, click **View** on the toolbar, and select **Template Explorer**. To see all Analysis Services templates, including templates for DMX, MDX, and XMLA, click the cube icon.  
  
 To build a query using a template, you can drag the template into an open query window, or you can double-click the template to open a new connection and a new query pane.  
  
 For an example of how to create a prediction query from a template, see [Create a Singleton Prediction Query from a Template](create-a-singleton-prediction-query-from-a-template.md).  
  
> [!WARNING]  
>  The Data Mining Add-in for Microsoft Office Excel also contains a number of templates, along with an interactive query builder which can help you compose complex DMX statements. To use the templates, click **Query**, and click **Advanced** in the Data Mining Client.  
  
###  <a name="bkmk_SSIS"></a> Integration Services Data Mining Components  
 You can also include prediction queries as part of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package. The following tasks and transformations in [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] support the creation and execution of DMX prediction queries and DMX statements.  
  
|Component|Description|  
|---------------|-----------------|  
|Data Mining Query task|Executes DMX queries and other DMX statements as part of a control flow.<br /><br /> The task editor provides the Prediction Query Builder, and a text box for modifying the DMX query manually. However, the task editor cannot validate the query against objects in an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] solution. Therefore, it is best to create a query within [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)] or [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] and then paste the text of the statement or query into the task editor.|  
|Data Mining Query transformation|Executes a prediction query within a data flow, using data supplied by a data flow source.<br /><br /> The task editor provides the Prediction Query Builder, and a text box for modifying the DMX query manually.<br /><br /> The transformation can only be used for creating queries that use data in the data flow; that is, queries that use the PREDICTION JOIN syntax. This component cannot be used for executing content queries or other kinds of DMX statements.|  
  
##  <a name="bkmk_API"></a> Application Programming Interfaces  
 You can create custom applications that execute queries against data mining models by using a variety of programming languages, in combination with server protocols such as OLE DB or Analysis Services ADOMD client. For more information, see [Data Mining Programming](../dev-guide/data-mining-programming.md).  
  
 However, XMLA constitutes the underlying message format for all interactions with an Analysis Service server. Within an XMLA message, queries are represented differently depending on whether you are sending a prediction query based on DMX, a content query, or a query that retrieves model metadata using the data mining schema rowsets.  
  
-   The text of **prediction queries** (and all other DMX statements) is sent in XMLA by using the [Execute Method &#40;XMLA&#41;](https://docs.microsoft.com/bi-reference/xmla/xml-elements-methods-execute) method, with the DMX query placed as text within the [Statement Element &#40;XMLA&#41;](https://docs.microsoft.com/bi-reference/xmla/xml-elements-commands/statement-element-xmla) element of the XMLA [Command Element &#40;XMLA&#41;](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/command-element-xmla) element.  
  
-   To retrieve **model content** and **model metadata**, such as the number of clusters, the attributes used in decision trees, the date the model was last processed, and the algorithm parameters used when creating the model, you can use the [Discover Method &#40;XMLA&#41;](https://docs.microsoft.com/bi-reference/xmla/xml-elements-methods-discover) method and specify one of the data mining schema rowsets in the [RequestType Element &#40;XMLA&#41;](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/type-element-xmla) header. To narrow the scope of the query, enter criteria as restrictions within the [RestrictionList Element &#40;XMLA&#41;](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/restrictionlist-element-xmla) element.  
  
## See Also  
 [Data Mining Extensions &#40;DMX&#41; Reference](/sql/dmx/data-mining-extensions-dmx-reference)   
 [Data Mining Solutions](data-mining-solutions.md)   
 [Understanding the DMX Select Statement](/sql/dmx/understanding-the-dmx-select-statement)   
 [Structure and Usage of DMX Prediction Queries](/sql/dmx/structure-and-usage-of-dmx-prediction-queries)   
 [Create a Prediction Query Using the Prediction Query Builder](create-a-prediction-query-using-the-prediction-query-builder.md)   
 [Create a DMX Query in SQL Server Management Studio](create-a-dmx-query-in-sql-server-management-studio.md)  
  
  
