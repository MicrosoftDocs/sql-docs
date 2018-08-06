---
title: "Warning Element (XMLA) | Microsoft Docs"
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
# Warning Element (XMLA)
[!INCLUDE[ssas-appliesto-sqlas-aas](../../../includes/ssas-appliesto-sqlas-aas.md)]
  Contains information about a warning returned by an instance of Analysis Services.  
  
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
  
## Element characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Message](../../../analysis-services/xmla/xml-elements-properties/message-element-xmla.md)|  
|Child elements|None|  
  
## Attributes  
  
|Attribute|Description|  
|---------------|-----------------|  
|ErrorCode|Required **UnsignedInt** attribute. Contains the numeric return code of the warning.|  
|Severity|Optional **String** attribute. Contains the severity of the warning.|  
|Description|Optional **String** attribute. Contains the descriptive text of the warning.|  
|Source|Optional **String** attribute. Contains the name of the component that generated the warning.|  
|HelpFile|Optional **String** attribute. Contains the path or URL to the Help file or topic that describes the warning.|  
  
## Remarks  
  
## See also
 [Error Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/error-element-xmla.md)   
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  
