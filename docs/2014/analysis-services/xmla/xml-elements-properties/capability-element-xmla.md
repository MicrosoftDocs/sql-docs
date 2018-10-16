---
title: "Capability Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Capability Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "urn:schemas-microsoft-com:xml-analysis#Capability"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Capability"
  - "microsoft.xml.analysis.capability"
helpviewer_keywords: 
  - "Capability element"
ms.assetid: 544a733e-77fc-48a0-8f92-9cd1fdbcf824
author: minewiskan
ms.author: owend
manager: craigg
---
# Capability Element (XMLA)
  Indicates support for a protocol capability in the parent [ProtocolCapabilities](../xml-elements-headers/protocolcapabilities-element-xmla.md) header element.  
  
## Syntax  
  
```xml  
  
<ProtocolCapabilities>  
   ...  
   <Capability>...</Capability>  
   ...  
</ProtocolCapabilities>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality|0-n: Optional element that can occur more than once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[ProtocolCapabilities](../xml-elements-headers/protocolcapabilities-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
 The `Capability` element indicates that a particular capability, such as binary or compression, is supported by either the application that included the `ProtocolCapabilities` header element in the SOAP header of the SOAP request, or by the instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] that included the `ProtocolCapabilities` header element in the SOAP header of the SOAP response. The value of the `Capability` element is the name of the capability to be supported.  
  
 [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] supports the capabilities listed in the following table.  
  
|Capability name|Description|  
|---------------------|-----------------|  
|sx|Binary XML support|  
|xpress|Compression support|  
  
## See Also  
 [Managing Connections and Sessions &#40;XMLA&#41;](../../multidimensional-models-scripting-language-assl-xmla/managing-connections-and-sessions-xmla.md)   
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
