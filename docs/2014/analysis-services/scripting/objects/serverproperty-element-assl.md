---
title: "ServerProperty Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "ServerProperty Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "SERVERPROPERTY"
helpviewer_keywords: 
  - "ServerProperty element"
ms.assetid: f152a1b5-0972-40d8-907f-f131c2a108bb
author: minewiskan
ms.author: owend
manager: craigg
---
# ServerProperty Element (ASSL)
  Defines a server property associated with a [Server](server-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<ServerProperties>  
   <ServerProperty>  
      <Name>...</Name>  
      <Value>...</Value>  
            <RequiresRestart>...</RequiresRestart>  
            <PendingValue>...</PendingValue>  
      <DefaultValue>...</DefaultValue>  
      <DisplayFlag>...</DisplayFlag>  
   </ServerProperty>  
</ServerProperties>  
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
|Parent elements|[ServerProperties](../collections/serverproperties-element-assl.md)|  
|Child elements|[DefaultValue](../properties/value-element-assl.md), [DisplayFlag](../properties/displayflag-element-assl.md), [Name](../properties/name-element-assl.md), [PendingValue](../properties/pendingvalue-element-assl.md), [RequiresRestart](../properties/requiresrestart-element-assl.md), [Value](../properties/value-element-assl.md)|  
  
## Remarks  
 The `ServerProperty` element describes the data and metadata for a server property associated with an instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)]. Unlike elements contained by other collections in the Analysis Services Scripting Language (ASSL), the `ServerProperty` element uses name/value pairs instead of explicitly named elements to describe server properties. The name/value pairs provide for flexibility and extensibility.  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ServerProperty>.  
  
## See Also  
 [Server Element &#40;ASSL&#41;](server-element-assl.md)   
 [Objects &#40;ASSL&#41;](objects-assl.md)  
  
  
