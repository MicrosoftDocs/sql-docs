---
title: "Invocation Element (ASSL) | Microsoft Docs"
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
  - "Invocation Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "Invocation"
helpviewer_keywords: 
  - "Invocation element"
ms.assetid: f6bf64ad-ae57-4d46-bf92-1d07a65378bb
caps.latest.revision: 35
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Invocation Element (ASSL)
  Specifies how an [Action](../../../analysis-services/scripting/objects/action-element-assl.md) should be invoked.  
  
## Syntax  
  
```xml  
  
<Action>  
   ...  
   <Invocation>...</Invocation>  
   ...  
</Action>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|*Interactive*|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[Action](../../../analysis-services/scripting/objects/action-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The invocation of an action depends on the client application. The **Invocation** element suggests to a client application how an action should be handled, and does not indicate to the instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] how to invoke an action.  
  
 The value of this element is limited to one of the strings in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*Interactive*|Invoked interactively by the user.|  
|*OnOpen*|Invoked when a client application opens the object.|  
|*Batch*|Invoked by a batch processing command.|  
  
 The enumeration that corresponds to the allowed values for **Invocation** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Action>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  