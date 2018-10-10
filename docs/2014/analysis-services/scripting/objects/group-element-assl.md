---
title: "Group Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Group Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "GROUP"
helpviewer_keywords: 
  - "Group element"
ms.assetid: 7df4ba90-b39f-4d8a-8db1-b73639a522a6
author: minewiskan
ms.author: owend
manager: craigg
---
# Group Element (ASSL)
  Defines a group of members bound to an attribute.  
  
## Syntax  
  
```xml  
  
<Groups>  
   <Group>  
      <Name>...</Name>  
      <Members>...</Members>  
   </Group>  
<Groups/>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|1-n: Required element that can occur more than once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Groups](../collections/groups-element-assl.md)|  
|Child elements|[Members](../collections/members-element-assl.md), [Name](../properties/name-element-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Group>.  
  
## See Also  
 [UserDefinedGroupBinding Data Type &#40;ASSL&#41;](../data-type/binding-data-type-assl.md)   
 [Objects &#40;ASSL&#41;](objects-assl.md)  
  
  
