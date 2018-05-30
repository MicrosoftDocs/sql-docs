---
title: "Capability Element (XMLA) | Microsoft Docs"
ms.date: 05/08/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: xmla
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile"
---
# Capability Element (XMLA)
[!INCLUDE[ssas-appliesto-sqlas-aas](../../../includes/ssas-appliesto-sqlas-aas.md)]
  Indicates support for a protocol capability in the parent [ProtocolCapabilities](../../../analysis-services/xmla/xml-elements-headers/protocolcapabilities-element-xmla.md) header element.  
  
## Syntax  
  
```xml  
  
<ProtocolCapabilities>  
   ...  
   <Capability>...</Capability>  
   ...  
</ProtocolCapabilities>  
```  
  
## Element characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality|0-n: Optional element that can occur more than once.|  
  
## Element relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[ProtocolCapabilities](../../../analysis-services/xmla/xml-elements-headers/protocolcapabilities-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
 The **Capability** element indicates that a particular capability, such as binary or compression, is supported by either the application that included the **ProtocolCapabilities** header element in the SOAP header of the SOAP request, or by the instance of Analysis Services that included the **ProtocolCapabilities** header element in the SOAP header of the SOAP response. The value of the **Capability** element is the name of the capability to be supported.  
  
 [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] supports the capabilities listed in the following table.  
  
|Capability name|Description|  
|---------------------|-----------------|  
|sx|Binary XML support|  
|xpress|Compression support|  
  
## See also
 [Managing Connections and Sessions &#40;XMLA&#41;](../../../analysis-services/multidimensional-models-scripting-language-assl-xmla/managing-connections-and-sessions-xmla.md)   
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  
