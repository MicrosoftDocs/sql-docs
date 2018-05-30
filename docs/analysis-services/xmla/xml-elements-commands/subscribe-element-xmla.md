---
title: "Subscribe Element (XMLA) | Microsoft Docs"
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
# Subscribe Element (XMLA)
[!INCLUDE[ssas-appliesto-sqlas-aas](../../../includes/ssas-appliesto-sqlas-aas.md)]
  Subscribes to a trace and returns a rowset that contains the trace events from a Analysis Services instance.  
  
## Syntax  
  
```xml  
  
<Command>  
   <Subscribe>  
      <Object>...</Object>  
   </Subscribe>  
</Command>  
```  
  
## Element characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-n: Optional element that can occur more than once.|  
  
## Element relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Command](../../../analysis-services/xmla/xml-elements-properties/command-element-xmla.md)|  
|Child elements|[Object](../../../analysis-services/xmla/xml-elements-properties/object-element-xmla.md)|  
  
## Remarks  
 The **Subscribe** command subscribes to and streams back a rowset from a specified trace on an [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance. If an object other than a trace is specified in the **Object** element, an error occurs.  
  
 If the **Object** element is not specified, a session trace is defined and subscribed to on the [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance. The session trace returns a fixed set of trace events from the current session.  
  
 The rowset stream returned by this command is terminated if the client application closes the connection to the [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance, or if the session on which the **Subscribe** command is executed is terminated.  
  
## See also
 [Commands &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-commands/xml-elements-commands.md)  
  
  
