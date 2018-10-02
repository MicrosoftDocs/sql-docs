---
title: "RequiresRestart Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/08/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "RequiresRestart Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "RequiresRestart"
helpviewer_keywords: 
  - "RequiresRestart element"
ms.assetid: 9e98f956-c41e-4e15-a7bd-e17c10ee6fc6
author: minewiskan
ms.author: owend
manager: craigg
---
# RequiresRestart Element (ASSL)
  Contains a read-only value associated with a [ServerProperty](../objects/serverproperty-element-assl.md) element that determines whether changing the value of the server property requires that the instance be restarted for the change to take effect.  
  
## Syntax  
  
```xml  
  
<ServerProperty>  
   ...  
   <RequiresRestart>...</RequiresRestart>  
   ...  
</ServerProperty>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|Boolean|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[ServerProperty](../objects/serverproperty-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The element that corresponds to the parent of `RequiresRestart` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ServerProperty>.  
  
## See Also  
 [ServerProperties Element &#40;ASSL&#41;](../collections/serverproperties-element-assl.md)   
 [Server Element &#40;ASSL&#41;](../objects/server-element-assl.md)   
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
