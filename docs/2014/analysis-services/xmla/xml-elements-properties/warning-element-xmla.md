---
title: "Warning Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Warning Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "urn:schemas-microsoft-com:xml-analysis#Warning"
  - "microsoft.xml.analysis.warning"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Warning"
helpviewer_keywords: 
  - "Warning element"
ms.assetid: a34a6caa-4b68-486b-8f50-cdc124c65888
author: minewiskan
ms.author: owend
manager: craigg
---
# Warning Element (XMLA)
  Contains information about a warning returned by an instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)].  
  
## Syntax  
  
```xml  
  
<Message>  
   <Warning   
      ErrorCode="unsignedint"   
      Severity="string"   
      Description="string"  
      Source="string"  
      HelpFile="string"  
   />  
</Message>  
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
|Parent elements|[Message](message-element-xmla.md)|  
|Child elements|None|  
  
## Attributes  
  
|Attribute|Description|  
|---------------|-----------------|  
|ErrorCode|Required `UnsignedInt` attribute. Contains the numeric return code of the warning.|  
|Severity|Optional `String` attribute. Contains the severity of the warning.|  
|Description|Optional `String` attribute. Contains the descriptive text of the warning.|  
|Source|Optional `String` attribute. Contains the name of the component that generated the warning.|  
|HelpFile|Optional `String` attribute. Contains the path or URL to the Help file or topic that describes the warning.|  
  
## Remarks  
  
## See Also  
 [Error Element &#40;XMLA&#41;](error-element-xmla.md)   
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
