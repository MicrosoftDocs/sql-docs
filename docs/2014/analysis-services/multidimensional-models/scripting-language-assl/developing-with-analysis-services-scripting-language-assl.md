---
title: "Developing with Analysis Services Scripting Language (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
helpviewer_keywords: 
  - "Analysis Services Scripting Language"
  - "ASSL"
ms.assetid: ce9aca4d-b7ad-451e-bb7f-20c2b0c03f29
author: minewiskan
ms.author: owend
manager: craigg
---
# Developing with Analysis Services Scripting Language (ASSL)
  Analysis Services Scripting Language (ASSL) is an extension to XMLA that adds an object definition language and command language for creating and managing Analysis Services structures directly on the server. You can use ASSL in custom application to communicate with Analysis Services over the XMLA protocol. ASSL is made up of two parts:  
  
-   A Data Definition Language (DDL), or object definition language, defines and describes an instance of [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)], as well as the databases and database objects that the instance contains.  
  
-   A command language that sends action commands, such as `Create`, `Alter`, or `Process`, to an instance of Analysis Services. This command language is discussed in the [XML for Analysis  &#40;XMLA&#41; Reference](https://docs.microsoft.com/bi-reference/xmla/xml-for-analysis-xmla-reference).  
  
 To view the ASSL that describes a multidimensional solution in [!INCLUDE[ssBIDevStudio](../../../includes/ssbidevstudio-md.md)], you can use the View Code command at the project level. You can also create or edit ASSL script in [!INCLUDE[ssManStudio](../../../includes/ssmanstudio-md.md)] using the XMLA query editor. The scripts you build can be used to manage objects or run commands on the server.  
  
## See Also  
 [ASSL Objects and Object Characteristics](assl-objects-and-object-characteristics.md)   
 [ASSL XML Conventions](assl-xml-conventions.md)   
 [Data Sources and Bindings &#40;SSAS Multidimensional&#41;](../data-sources-and-bindings-ssas-multidimensional.md)  
  
  
