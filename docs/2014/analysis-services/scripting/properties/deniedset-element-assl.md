---
title: "DeniedSet Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "DeniedSet Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "DeniedSet"
helpviewer_keywords: 
  - "DeniedSet element"
ms.assetid: 898deefb-822d-458b-96d8-880da287b687
author: minewiskan
ms.author: owend
manager: craigg
---
# DeniedSet Element (ASSL)
  Contains a set expression that defines the list of permissions that are denied on the associated attribute.  
  
## Syntax  
  
```xml  
  
<AttributePermission>  
      ...  
   <DeniedSet>...</DeniedSet>  
      ...  
</AttributePermission>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[AttributePermission](../objects/attributepermission-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The element that corresponds to the parent of `DeniedSet` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.AttributePermission>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
