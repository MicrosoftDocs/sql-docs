---
title: "Methods (XMLA) | Microsoft Docs"
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
f1_keywords: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine#"
helpviewer_keywords: 
  - "methods [XML for Analysis]"
  - "XML for Analysis, methods"
  - "XMLA, methods"
ms.assetid: c6768dd4-ca06-4a85-93b7-5fd5700886ad
caps.latest.revision: 30
author: "jeannt"
ms.author: "jeannt"
manager: "erikre"
---
# XML Elements - Methods
  The XML for Analysis (XMLA) protocol uses two methods, **Discover** and **Execute**, to offer a standard way for applications to access information on an instance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. Because these methods are invoked by using Simple Object Access Protocol (SOAP), they accept input and deliver output in XML. [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] implements both methods, in compliance with the XML for Analysis 1.1 specification.  
  
## In This Section  
 The following topics describe the XMLA methods implemented by [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].  
  
|Method|Description|  
|------------|-----------------|  
|[Discover Method &#40;XMLA&#41;](../../analysis-services/xmla/xml-elements-methods-discover.md)|Retrieves information, such as the list of available databases or details about a specific object, from an instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. The data retrieved with the **Discover** method depends on the values of the parameters passed to it.|  
|[Execute Method &#40;XMLA&#41;](../../analysis-services/xmla/xml-elements-methods-execute.md)|Sends XMLA commands to an instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. This includes requests involving data transfer, such as retrieving or updating data on the server.|  
  
## See Also  
 [XML Elements &#40;XMLA&#41;](http://msdn.microsoft.com/library/40ab2360-efb6-4ba6-bf23-e84964e51008)   
 [XML Data Types &#40;XMLA&#41;](../../analysis-services/xmla/xml-data-types/xml-data-types-xmla.md)   
 [XML Elements &#40;XMLA&#41;](http://msdn.microsoft.com/library/40ab2360-efb6-4ba6-bf23-e84964e51008)  
  
  