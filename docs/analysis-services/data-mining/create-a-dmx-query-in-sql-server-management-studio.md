---
title: "Create a DMX Query in SQL Server Management Studio | Microsoft Docs"
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
# Create a DMX Query in SQL Server Management Studio
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides a set of features to help you create prediction queries, content queries, and data definition queries against mining models and mining structures.  
  
-   The graphical Prediction Query Builder is available in both [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] and [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], to simplify the process of writing prediction queries and mapping data sets to a model.  
  
-   The query templates provided in the Template Explorer jump-start the creation of many kinds of DMX queries, including many types of prediction queries. Templates are provided for content queries, queries used nested data sets, queries that return cases from the mining structure, and even data definition queries.  
  
-   The Metadata Explorer in the MDX and DMX query panes provides a list of available models and structures that you can drag and drop into the query builder, as well as a list of DMX functions. This feature makes it easy to get object names right, without typing.  
  
 This topic describes how to build a DMX query by using the Metadata Explorer and the DMX query editor.  
  
##  <a name="BKMK_Templates"></a> DMX Query Templates  
 Templates for creating basic DMX queries are available in Template Explorer. The **DMX** folder contains data mining templates, which are divided into these categories:  
  
-   **Model Content**  
  
-   **Model Management**  
  
-   **Prediction Queries**  
  
-   **Structure Content**  
  
 You can also create custom templates, for queries or commands that you run frequently.  
  
## XMLA Query Templates  
 [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] also provides templates for XMLA queries.  
  
 There is some overlap between the types of queries that you can perform by using XMLA and DMX. For example, you can create some model content queries by using either DMX or the data mining schema rowsets, but the schema rowsets sometimes contain information that is not exposed in DMX content queries.  
  
 There are also some key differences in the way that operations are handled in DMX and in XMLA. For example, you can use XMLA to perform administrative operations such as backup of an entire [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database, but if you want to back up a single mining model, DMX provides a simple command, [EXPORT &#40;DMX&#41;](../../dmx/export-dmx.md), that is better suited to that purpose.  
  
##  <a name="BKMK_Building_Queries"></a> Build and Run a DMX Query  
  
#### Open a new DMX Query window  
  
1.  Click **New Query** in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], and then select **New Analysis Server DMX Query**.  
  
2.  When the **Connect to Server** dialog box appears, select the instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] that contains the mining models you want to work with.  
  
#### Open Template Explorer  
  
1.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], on the **View** menu, select **Template Explorer**.  
  
2.  Click **Analysis Server** to see a tree view of the templates that apply to [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].  
  
#### Apply a template to build a query  
  
-   Right-click the appropriate query type and select **Open**.  
  
-   Or, drag the template into the query editor.  
  
-   You can also fill in the parameters for the query by using the option, **Specify Values for Parameters**, from the **Query** menu.  
  
 For examples of how to create specific types of queries from templates, see the following topics:  
  
 [Create a Singleton Prediction Query from a Template](../../analysis-services/data-mining/create-a-singleton-prediction-query-from-a-template.md)  
  
 [Create a Content Query on a Mining Model](../../analysis-services/data-mining/create-a-content-query-on-a-mining-model.md)  
  
## See Also  
 [Data Mining Query Tools](../../analysis-services/data-mining/data-mining-query-tools.md)   
 [Data Mining Extensions &#40;DMX&#41; Reference](../../dmx/data-mining-extensions-dmx-reference.md)  
  
  
