---
title: "Groups Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Groups Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "Groups"
helpviewer_keywords: 
  - "Groups element"
ms.assetid: 62196435-83a8-4a0a-8be1-7dfc986dc6c5
author: minewiskan
ms.author: owend
manager: craigg
---
# Groups Element (ASSL)
  Contains the collection of groups of members bound to an attribute.  
  
## Syntax  
  
```xml  
  
<Binding xsi:type="UserDefinedGroupBinding">  
   ...  
   <Groups>  
      <Group>...</Group>  
   </Groups>  
   ...  
</Binding>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|1-1: Required element that occurs once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Binding](../data-type/binding-data-type-assl.md) of type [UserDefinedGroupBinding](../data-type/userdefinedgroupbinding-data-type-assl.md)|  
|Child elements|[Group](../objects/group-element-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.GroupCollection>.  
  
## See Also  
 [Data Sources and Bindings &#40;SSAS Multidimensional&#41;](../../multidimensional-models/data-sources-and-bindings-ssas-multidimensional.md)   
 [Collections &#40;ASSL&#41;](collections-assl.md)  
  
  
