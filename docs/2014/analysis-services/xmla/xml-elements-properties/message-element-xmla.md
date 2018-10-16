---
title: "Message Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Message Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "microsoft.xml.analysis.message"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Message"
  - "urn:schemas-microsoft-com:xml-analysis#Message"
helpviewer_keywords: 
  - "Message element"
ms.assetid: 028911e2-9779-43b1-824d-6d7fb2295885
author: minewiskan
ms.author: owend
manager: craigg
---
# Message Element (XMLA)
  Contains a message returned from an instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] by a [Discover](../xml-elements-methods-discover.md) or [Execute](../xml-elements-methods-execute.md) method call.  
  
## Syntax  
  
```xml  
  
<Messages>  
   ...  
   <Message>  
      <Error>...</Error>  
      <!-- or -->  
      <Warning>...</Warning>  
   </Message>  
   ...  
</Messages>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-n: Optional element that can occur more than once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Messages](messages-element-xmla.md)|  
|Child elements|[Error](error-element-xmla.md), [Warning](warning-element-xmla.md)|  
  
## Remarks  
 This element is used in cases where a `Discover` method call or a single XMLA command within an `Execute` method call completes successfully, but with errors or warnings. In such cases, a `Messages` element is added to the root element after all other elements, which in turn contains one or more `Message` elements. Each `Message` element represents a single message, either an error or a warning, returned by the [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance.  
  
## See Also  
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
