---
title: "Methods (XMLA) | Microsoft Docs"
ms.date: 05/08/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: xmla
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# XML Elements - Methods
[!INCLUDE[ssas-appliesto-sqlas-aas](../../includes/ssas-appliesto-sqlas-aas.md)]
  The XML for Analysis (XMLA) protocol uses two methods, **Discover** and **Execute**, to offer a standard way for applications to access information on an instance of Analysis Services. Because these methods are invoked by using Simple Object Access Protocol (SOAP), they accept input and deliver output in XML. Analysis Services implements both methods, in compliance with the XML for Analysis 1.1 specification.  
  
## In this section  
 The following topics describe the XMLA methods implemented by Analysis Services.  
  
|Method|Description|  
|------------|-----------------|  
|[Discover Method &#40;XMLA&#41;](../../analysis-services/xmla/xml-elements-methods-discover.md)|Retrieves information, such as the list of available databases or details about a specific object, from an instance of Analysis Services. The data retrieved with the **Discover** method depends on the values of the parameters passed to it.|  
|[Execute Method &#40;XMLA&#41;](../../analysis-services/xmla/xml-elements-methods-execute.md)|Sends XMLA commands to an instance of Analysis Services. This includes requests involving data transfer, such as retrieving or updating data on the server.|  
  
## See also
 [XML Elements &#40;XMLA&#41;](http://msdn.microsoft.com/library/40ab2360-efb6-4ba6-bf23-e84964e51008)   
 [XML Data Types &#40;XMLA&#41;](../../analysis-services/xmla/xml-data-types/xml-data-types-xmla.md)   
 [XML Elements &#40;XMLA&#41;](http://msdn.microsoft.com/library/40ab2360-efb6-4ba6-bf23-e84964e51008)  
  
  
