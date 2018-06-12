---
title: "XML for Analysis (XMLA) Reference | Microsoft Docs"
ms.date: 05/30/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: xmla
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# XML for Analysis  (XMLA) Reference
[!INCLUDE[ssas-appliesto-sqlas-aas](../../includes/ssas-appliesto-sqlas-aas.md)]   

Azure Analysis Services and SQL Server Analysis Services use XML for Analysis (XMLA) protocol for communications between client applications and an Analysis Services instance. At their most basic level, other client libraries such as ADOMD.NET and AMO construct requests and decode responses in XMLA, serving as an intermediary to an Analysis Services instance, which uses XMLA exclusively.  
  
 To support the discovery and manipulation of data in both tabular and multidimensional modes, the XMLA specification defines two generally accessible methods, [Discover](../../analysis-services/xmla/xml-elements-methods-discover.md) and [Execute](../../analysis-services/xmla/xml-elements-methods-execute.md), and a collection of XML elements and data types. Because XML allows for a loosely coupled client and server architecture, both methods handle incoming and outgoing information in XML format. 

Analysis Services is compliant with the XMLA 1.1. specification, but also extends it to include data definition and manipulation capability, implemented as annotations on the **Discover** and **Execute** methods. The extended XML syntaxes are Tabular Model Scripting Language (TMSL) and Analysis Services Scripting Language (ASSL). 

TMSL is the command and object model definition syntax for tabular model databases at compatibility level 1200 and higher. TMSL communicates with Analysis Services through the XMLA protocol, where the `XMLA.Execute` method accepts both JSON-based statement scripts in TMSL as well as the traditional XML-based scripts in Analysis Services Scripting Language (ASSL for XMLA). To learn more, see [Tabular Model Scripting Language (TMSL) Reference](../tabular-model-scripting-language-tmsl-reference.md).

ASSL is the command and object model definition syntax for  multidimensional model databases and tabular mode databases at compatibility level 1103 or lower. This definition builds on the XMLA specification without breaking it. Interoperability based on XMLA is ensured whether you use just XMLA, or XMLA and ASSL together. To learn more, see [Analysis Services Scripting Language (ASSL for XMLA)](../scripting/analysis-services-scripting-language-assl-for-xmla.md).
  
 As a developer, you can use XMLA as an interface if solution requirements specify standard protocols such as XML, SOAP, and HTTP. Developers and administrators can also use XMLA on an ad-hoc basis to retrieve information from the server or run commands. 
  
## In this section  
  
|Topic|Description|  
|-----------|-----------------|  
|[XML Data Types (XMLA)](../../analysis-services/xmla/xml-data-types/xml-data-types-xmla.md)|Describes data types in the XMLA specification.|  
|[XML Elements - Commands (XMLA)](../../analysis-services/xmla/xml-elements-commands/xml-elements-commands.md)|Elements that can be used within the Command element during an Execute method call.|  
|[XML Elements - Commands (XMLA)](../../analysis-services/xmla/xml-elements-commands/xml-elements-commands.md)|Elements that can be used within the Command element during an Execute method call.|  
|[XML Elements - Headers (XMLA)](../../analysis-services/xmla/xml-elements-headers/xml-elements-headers.md)| Header elements implemented by Microsoft  Analysis Services.|  
|[XML Elements - Properties (XMLA)](../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)| Elements to represent property information and values for XMLA headers, methods, objects, commands, and data types.|  
|[XML Elements - Methods - Discover (XMLA)](../../analysis-services/xmla/xml-elements-methods-discover.md)| Retrieves information, such as the list of available databases or details about a specific object, from an instance of Analysis Services.|  
|[XML Elements - Methods - Execute (XMLA)](../../analysis-services/xmla/xml-elements-methods-execute.md)| Sends XML for Analysis (XMLA) commands to an instance of Analysis Services.|  
|[XML Elements - Objects - DiscoverResponse (XMLA)](../../analysis-services/xmla/xml-elements-objects-discoverresponse.md)| Contains the information returned by an instance of Analysis Services in response to a Discover method call.|  
|[XML Elements - Objects - ExecuteResponse (XMLA)](../../analysis-services/xmla/xml-elements-objects-executeresponse.md)| Contains the information returned by an instance of Analysis Services in response to an Execute method call.|  
|[XML Elements - Objects (XMLA)](../../analysis-services/xmla/xml-elements-objects.md)| Objects implemented by Analysis Services.|  
|[XML for Analysis Compliance (XMLA)](../../analysis-services/xmla/xml-for-analysis-compliance-xmla.md)|Describes the level of compliance with the XMLA 1.1 specification.|  
  
## See also
 [XML for Analysis Schema Rowsets](../../analysis-services/schema-rowsets/xml/xml-for-analysis-schema-rowsets.md)  
 [Developing with ADOMD.NET](../../analysis-services/multidimensional-models/adomd-net/developing-with-adomd-net.md)  
 [Developing with Analysis Management Objects &#40;AMO&#41;](../../analysis-services/multidimensional-models/analysis-management-objects/developing-with-analysis-management-objects-amo.md)  
