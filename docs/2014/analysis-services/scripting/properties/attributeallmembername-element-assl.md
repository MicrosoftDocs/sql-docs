---
title: "AttributeAllMemberName Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "AttributeAllMemberName Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "AttributeAllMemberName"
helpviewer_keywords: 
  - "AttributeAllMemberName element"
ms.assetid: 5ede46a7-d8b0-40be-98d7-b01047b27d2e
author: minewiskan
ms.author: owend
manager: craigg
---
# AttributeAllMemberName Element (ASSL)
  Contains the caption, in the default language, for the All member of the dimension.  
  
## Syntax  
  
```xml  
  
<Dimension>  
      ...  
   <AttributeAllMemberName>...</AttributeAllMemberName>  
   ...  
</Dimension>  
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
|Parent element|[Dimension](../objects/dimension-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The element that corresponds to the parent of `AttributeAllMemberName` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Dimension>.  
  
## See Also  
 [Configure the &#40;All&#41; Level for Attribute Hierarchies](../../multidimensional-models/database-dimensions-configure-the-all-level-for-attribute-hierarchies.md)   
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
