---
title: "Group Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "Group Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "GROUP"
helpviewer_keywords: 
  - "Group element"
ms.assetid: 7df4ba90-b39f-4d8a-8db1-b73639a522a6
caps.latest.revision: 34
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
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
|Parent elements|[Groups](../../../analysis-services/scripting/collections/groups-element-assl.md)|  
|Child elements|[Members](../../../analysis-services/scripting/collections/members-element-assl.md), [Name](../../../analysis-services/scripting/properties/name-element-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Group>.  
  
## See Also  
 [UserDefinedGroupBinding Data Type &#40;ASSL&#41;](../../../analysis-services/scripting/data-type/userdefinedgroupbinding-data-type-assl.md)   
 [Objects &#40;ASSL&#41;](../../../analysis-services/scripting/objects/objects-assl.md)  
  
  