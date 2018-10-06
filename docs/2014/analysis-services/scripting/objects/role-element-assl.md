---
title: "Role Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Role Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "ROLE"
helpviewer_keywords: 
  - "Role element"
ms.assetid: 56f52462-a7fd-4b51-a7fb-4311134439e9
author: minewiskan
ms.author: owend
manager: craigg
---
# Role Element (ASSL)
  Contains information about a security role.  
  
## Syntax  
  
```xml  
  
<Roles>  
      <Role>  
      <Name>...</Name>  
      <ID>...</ID>  
      <CreatedTimestamp>...</CreateTimestamp>  
      <LastSchemaUpdate>...</LastSchemaUpdate>  
      <Description>...</Description>  
      <Members>...</Members>  
      <Annotations>...</Annotations>  
   </Role>  
</Roles>  
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
|Parent elements|[Roles](../collections/roles-element-assl.md)|  
|Child elements|[Annotations](../collections/annotations-element-assl.md), [CreatedTimestamp](../properties/createdtimestamp-element-assl.md), [Description](../properties/description-element-assl.md), [ID](../properties/id-element-assl.md), [LastSchemaUpdate](../properties/lastschemaupdate-element-assl.md), [Members](../collections/members-element-assl.md), [Name](../properties/name-element-assl.md)|  
  
## Remarks  
 The definition of the role includes the users that are members of the role.  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Role>.  
  
## See Also  
 [Database Element &#40;ASSL&#41;](database-element-assl.md)   
 [Server Element &#40;ASSL&#41;](server-element-assl.md)   
 [Objects &#40;ASSL&#41;](objects-assl.md)  
  
  
