---
title: "AllowBrowsing Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "AllowBrowsing Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "AllowBrowsing"
helpviewer_keywords: 
  - "AllowBrowsing element"
ms.assetid: e5d09f8c-080b-4013-8c6a-0c9775e6ab25
caps.latest.revision: 38
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# AllowBrowsing Element (ASSL)
  Defines whether the members of a [Role](../../../analysis-services/scripting/objects/role-element-assl.md) element have browse permission on a [MiningModel](../../../analysis-services/scripting/objects/miningmodel-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<MiningModelPermission>  
      ...  
   <AllowBrowsing>...</AllowBrowsing>  
   ...  
</MiningModelPermission>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|Boolean|  
|Default value|**True**|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[MiningModelPermission](../../../analysis-services/scripting/objects/miningmodelpermission-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The element that corresponds to the parent of **AllowBrowsing** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.MiningModelPermission>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  