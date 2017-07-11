---
title: "ServerProperty Element (ASSL) | Microsoft Docs"
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
  - "ServerProperty Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "SERVERPROPERTY"
helpviewer_keywords: 
  - "ServerProperty element"
ms.assetid: f152a1b5-0972-40d8-907f-f131c2a108bb
caps.latest.revision: 36
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# ServerProperty Element (ASSL)
  Defines a server property associated with a [Server](../../../analysis-services/scripting/objects/server-element-assl.md) element.  
  
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
|Parent elements|[ServerProperties](../../../analysis-services/scripting/collections/serverproperties-element-assl.md)|  
|Child elements|[DefaultValue](../../../analysis-services/scripting/properties/defaultvalue-element-assl.md), [DisplayFlag](../../../analysis-services/scripting/properties/displayflag-element-assl.md), [Name](../../../analysis-services/scripting/properties/name-element-assl.md), [PendingValue](../../../analysis-services/scripting/properties/pendingvalue-element-assl.md), [RequiresRestart](../../../analysis-services/scripting/properties/requiresrestart-element-assl.md), [Value](../../../analysis-services/scripting/properties/value-element-assl.md)|  
  
## Remarks  
 The **ServerProperty** element describes the data and metadata for a server property associated with an instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)]. Unlike elements contained by other collections in the Analysis Services Scripting Language (ASSL), the **ServerProperty** element uses name/value pairs instead of explicitly named elements to describe server properties. The name/value pairs provide for flexibility and extensibility.  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ServerProperty>.  
  
## See Also  
 [Server Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/server-element-assl.md)   
 [Objects &#40;ASSL&#41;](../../../analysis-services/scripting/objects/objects-assl.md)  
  
  