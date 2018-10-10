---
title: "Process Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Process Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "Process"
helpviewer_keywords: 
  - "Process element"
ms.assetid: 4aa08718-be44-4781-92cf-7b32b20f862c
author: minewiskan
ms.author: owend
manager: craigg
---
# Process Element (ASSL)
  Determines whether a user can process the owner of the parent element.  
  
## Syntax  
  
```xml  
  
<Permission> <-- or one of the other elements contained in the Element Relationships table -->  
   ...  
   <Process>...</Process>  
   ...  
</Permission>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|Boolean|  
|Default value|False|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[CubePermission](../objects/cubepermission-element-assl.md), [DatabasePermission](../objects/databasepermission-element-assl.md), [DimensionPermission](../objects/dimensionpermission-element-assl.md), [MiningModelPermission](../objects/miningmodelpermission-element-assl.md), [MiningStructurePermission](../objects/miningstructurepermission-element-assl.md), [Permission](../data-type/permission-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The elements that correspond to the parents of `Process` in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.CubePermission>, <xref:Microsoft.AnalysisServices.DatabasePermission>, <xref:Microsoft.AnalysisServices.DimensionPermission>, <xref:Microsoft.AnalysisServices.MiningModelPermission>, <xref:Microsoft.AnalysisServices.MiningStructurePermission>, and <xref:Microsoft.AnalysisServices.Permission>.  
  
## See Also  
 [Role Element &#40;ASSL&#41;](../objects/role-element-assl.md)   
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
