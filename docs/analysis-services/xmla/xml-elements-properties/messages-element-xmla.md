---
title: "Messages Element (XMLA) | Microsoft Docs"
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
apiname: 
  - "Messages Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Messages"
  - "microsoft.xml.analysis.messages"
  - "urn:schemas-microsoft-com:xml-analysis#Messages"
helpviewer_keywords: 
  - "Messages element"
ms.assetid: 719d15ff-f18b-4c56-80ba-a9114c0b7d8a
caps.latest.revision: 11
author: "jeannt"
ms.author: "jeannt"
manager: "erikre"
---
# Messages Element (XMLA)
  Contains a collection of [Message](../../../analysis-services/xmla/xml-elements-properties/message-element-xmla.md) elements returned from an instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] by a [Discover](../../../analysis-services/xmla/xml-elements-methods-discover.md) or [Execute](../../../analysis-services/xmla/xml-elements-methods-execute.md) method call.  
  
## Syntax  
  
```xml  
  
<Resultset>  
   <Messages>  
      <Message>  
   </Messages>  
</Resultset>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Resultset](../../../analysis-services/xmla/xml-data-types/resultset-data-type-xmla.md)|  
|Child elements|[Message](../../../analysis-services/xmla/xml-elements-properties/message-element-xmla.md)|  
  
## Remarks  
 This element is used in cases where a **Discover** method call or a single XMLA command within an **Execute** method call completes successfully, but with errors or warnings. In such cases, a **Messages** element is added to the [root](../../../analysis-services/xmla/xml-elements-properties/root-element-xmla.md) element after all other elements, which in turn contains one or more **Message** elements. Each **Message** element represents a single message, either an error or a warning, returned by the [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance.  
  
## See Also  
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  