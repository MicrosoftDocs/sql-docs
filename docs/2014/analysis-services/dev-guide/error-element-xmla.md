---
title: "Error Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
api_name: 
  - "Error Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "microsoft.xml.analysis.error"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Error"
  - "urn:schemas-microsoft-com:xml-analysis#Error"
helpviewer_keywords: 
  - "Error element"
ms.assetid: add670cb-cab2-42be-91a3-d0c385f29d16
caps.latest.revision: 13
author: "mgblythe"
ms.author: "mblythe"
manager: "mblythe"
---
# Error Element (XMLA)
  Contains information about an error returned by an instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)].  
  
## Syntax  
  
```xml  
  
<Message>  
   <Error   
      ErrorCode="unsignedint"   
      Severity="string"   
      Description="string"  
      Source="string"  
      HelpFile="string"  
   />  
</Message>  
<!-- or -->  
<Cell><!-- or row -->  
   <!-- A child element -->  
      <Error xmlns="urn:schemas-microsoft-com:xml-analysis:exception"  
         < ErrorCode>...</ErrorCode>  
         < Description>...</Description>  
         < Source>...</Source>  
         < HelpFile>...</HelpFile>  
      </Error>  
   <!-- /A child element -- >  
</Cell>  
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
|Parent elements|[Message](../../../2014/analysis-services/dev-guide/message-element-xmla.md)|  
  
## Child Elements  
  
|Ancestor|Child elements|  
|--------------|--------------------|  
|[Message](../../../2014/analysis-services/dev-guide/message-element-xmla.md)|None|  
|[Cell](../../../2014/analysis-services/dev-guide/cell-element-mddataset-xmla.md), [row](../../../2014/analysis-services/dev-guide/message-element-xmla.md)|[Description](../../../2014/analysis-services/dev-guide/description-element-xmla.md), [ErrorCode](../../../2014/analysis-services/dev-guide/errorcode-element-xmla.md), [HelpFile](../../../2014/analysis-services/dev-guide/helpfile-element-xmla.md), [Source](../../../2014/analysis-services/dev-guide/source-element-error-xmla.md)|  
  
## Attributes  
  
|Attribute|Description|  
|---------------|-----------------|  
|ErrorCode|Required `UnsignedInt` attribute (only when `Message` is the parent element.) Contains the numeric return code of the error.|  
|Severity|Optional `String` attribute (only when `Message` is the parent element.) Contains the severity of the error.|  
|Description|Optional `String` attribute (only when `Message` is the parent element.) Contains the descriptive text of the error.|  
|Source|Optional `String` attribute (only when `Message` is the parent element.) Contains the name of the component that generated the error.|  
|HelpFile|Optional `String` attribute (only when `Message` is the parent element.) Contains the path or URL to the Help file or topic that describes the error.|  
  
## Remarks  
  
## See Also  
 [Warning Element &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/warning-element-xmla.md)   
 [Properties &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/properties-xmla.md)  
  
  