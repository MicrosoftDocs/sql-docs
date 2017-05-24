---
title: "Data Mining Programming | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "analysis-services/data-mining"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "article"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "data mining [Analysis Services], developer's guide"
ms.assetid: 9fd77b16-0b89-44ce-bcf1-7c04b62499da
caps.latest.revision: 20
author: "jeannt"
ms.author: "jeannt"
manager: "erikre"
---
# Data Mining Programming
  If you find that the built-in tools and viewers in [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] do not meet your requirements, you can extend the power of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] by coding your own extensions. In this approach, you have two options:  
  
-   **XMLA**  
  
     [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] supports XML for Analysis (XMLA) as a protocol for communication with client applications. Additional commands are supported by [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] that extend the XML for Analysis specification.  
  
     Because [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] uses XMLA for data definition, data manipulation, and data control support, you can create mining structures and mining models by using the visual tools provided by [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], and then extend the data mining objects that you have created by using Data Mining Extensions (DMX) and Analysis Services Scripting Language (ASSL) scripts.  
  
     You can create and modify data mining objects entirely in XMLA scripts, and run prediction queries against the models programmatically from your own applications.  
  
-   **Analysis Management Objects (AMO)**  
  
     [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] also provides a complete framework that enables third-party data mining providers to integrate the data mining objects into [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)].  
  
     You can create mining structures and mining models by using AMO. See the following samples in CodePlex:  
  
    -   AMO Browser  
  
         Connects to the SSAS instance you specify and lists all server objects and their properties, including mining structure and mining models.  
  
    -   AMO Simple Sample  
  
         The AS Simple Sample covers programmatic access to most major objects, and demonstrates metadata browsing, and access to the values in objects.  
  
         The sample also demonstrates how to create and process a data mining structure and model, as well as browse an existing data mining model.  
  
-   **DMX**  
  
     You can use DMX to encapsulate command statements, prediction queries, and metadata queries and return results in a tabular format, assuming you have created a connection to an [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] server.  
  
## In This Section  
 [OLE DB for Data Mining](../analysis-services/data-mining-programming-ole-db.md)  
 Describes additions to the specification to support data mining and multidimensional data: new schema rowsets and columns, Data Mining Extensions (DMX) language for creating and managing mining structures.  
  
## Related Reference  
 [Developing with ADOMD.NET](../analysis-services/multidimensional-models/adomd-net/developing-with-adomd-net.md)  
 Introduces ADOMD.NET client and server programming objects.  
  
 [Developing with Analysis Management Objects &#40;AMO&#41;](../analysis-services/multidimensional-models/analysis-management-objects/developing-with-analysis-management-objects-amo.md)  
 Introduces the AMO programming library.  
  
 [Developing with Analysis Services Scripting Language &#40;ASSL&#41;](../analysis-services/multidimensional-models/scripting-language-assl/developing-with-analysis-services-scripting-language-assl.md)  
 Introduces XML for Analysis (XMLA) and its extensions.  
  
## See Also  
 [Analysis Services Developer Documentation](../analysis-services/analysis-services-developer-documentation.md)   
 [Data Mining Extensions &#40;DMX&#41; Reference](../dmx/data-mining-extensions-dmx-reference.md)  
  
  