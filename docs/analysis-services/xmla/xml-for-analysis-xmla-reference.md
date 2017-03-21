---
title: "XML for Analysis  (XMLA) Reference | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "XML for Analysis, reference"
  - "XMLA, reference"
ms.assetid: 88045e05-ce47-4e28-999b-7f9c74af9faf
caps.latest.revision: 33
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# XML for Analysis  (XMLA) Reference
  [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] uses the XML for Analysis (XMLA) protocol to handle all communication between client applications and an Analysis Services instance. At their most basic level, other client libraries such as ADOMD.NET and AMO construct requests and decode responses in XMLA, serving as an intermediary to an Analysis Services instance, which uses XMLA exclusively.  
  
 To support the discovery and manipulation of data in both multidimensional and tabular formats, the XMLA specification defines two generally accessible methods, [Discover](../../analysis-services/xmla/xml-elements-methods-discover.md) and [Execute](../../analysis-services/xmla/xml-elements-methods-execute.md), and a collection of XML elements and data types. Because XML allows for a loosely coupled client and server architecture, both methods handle incoming and outgoing information in XML format. Analysis Services is compliant with the XMLA 1.1. specification, but also extends it to include data definition and manipulation capability, implemented as annotations on the **Discover** and **Execute** methods. The extended XML syntax is referred to as Analysis Services Scripting Language (ASSL). ASSL builds on the XMLA specification without breaking it. Interoperability based on XMLA is ensured whether you use just XMLA, or XMLA and ASSL together.  
  
 As a programmer, you can use XMLA as a programmatic interface if solution requirements specify standard protocols, such as XML, SOAP, and HTTP. Programmers and administrators can also use XMLA on an ad hoc basis to retrieve information from the server or run commands.  
  
## In This Section  
  
|Topic|Description|  
|-----------|-----------------|  
|[XML Elements &#40;XMLA&#41;](http://msdn.microsoft.com/library/40ab2360-efb6-4ba6-bf23-e84964e51008)|Describes elements in the XMLA specification.|  
|[XML Data Types &#40;XMLA&#41;](../../analysis-services/xmla/xml-data-types/xml-data-types-xmla.md)|Describes data types in the XMLA specification.|  
|[XML for Analysis Compliance &#40;XMLA&#41;](../../analysis-services/xmla/xml-for-analysis-compliance-xmla.md)|Describes the level of compliance with the XMLA 1.1 specification.|  
  
## Related Sections  
 [Developing with Analysis Services Scripting Language &#40;ASSL&#41;](../../analysis-services/multidimensional-models/scripting-language-assl/developing-with-analysis-services-scripting-language-assl.md)  
  
 [XML for Analysis Schema Rowsets](../../analysis-services/schema-rowsets/xml/xml-for-analysis-schema-rowsets.md)  
  
 [Developing with ADOMD.NET](../../analysis-services/multidimensional-models/adomd-net/developing-with-adomd-net.md)  
  
 [Developing with Analysis Management Objects &#40;AMO&#41;](../../analysis-services/multidimensional-models/analysis-management-objects/developing-with-analysis-management-objects-amo.md)  
  
## See Also  
 [Understanding Microsoft OLAP Architecture](../../analysis-services/multidimensional-models/olap-physical/understanding-microsoft-olap-architecture.md)  
  
  