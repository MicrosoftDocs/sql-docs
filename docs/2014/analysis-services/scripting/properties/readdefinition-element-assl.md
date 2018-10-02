---
title: "ReadDefinition Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "ReadDefinition Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "ReadDefinition"
helpviewer_keywords: 
  - "ReadDefinition element"
ms.assetid: 1f250129-13b2-41b9-b083-b5aacddf0060
author: minewiskan
ms.author: owend
manager: craigg
---
# ReadDefinition Element (ASSL)
  Determines whether members can read the definition of the database or the definition of objects in the database.  
  
## Syntax  
  
```xml  
  
<Permission> <!-- or one of the elements listed below in the Element Relationships table -->  
   ...  
   <ReadDefinition>...</ReadDefinition>  
   ...  
</Permission>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|*None*|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[CubePermission](../objects/cubepermission-element-assl.md), [DatabasePermission](../objects/databasepermission-element-assl.md), [DimensionPermission](../objects/dimensionpermission-element-assl.md), [MiningModelPermission](../objects/miningmodelpermission-element-assl.md), [MiningStructurePermission](../objects/miningstructurepermission-element-assl.md), [Permission](../data-type/permission-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of this element is limited to one of the strings listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*None*|Access to the object definition is not allowed.|  
|*Basic*|Basic access to the object definition is allowed. **Note:**  This permission is required for creating local cubes, linking measure groups, and linking dimensions.|  
|*Allowed*|Full access to the object definition is allowed. **Note:**  This permission is required for executing a [Discover](../../xmla/xml-elements-methods-discover.md) XML for Analysis (XMLA) call using the DISCOVER_XML_METADATA parameter.|  
  
 The elements that correspond to the parents of `ReadDefinition` in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.CubePermission>, <xref:Microsoft.AnalysisServices.DatabasePermission>, <xref:Microsoft.AnalysisServices.DimensionPermission>, <xref:Microsoft.AnalysisServices.MiningModelPermission>, <xref:Microsoft.AnalysisServices.MiningStructurePermission>, and <xref:Microsoft.AnalysisServices.Permission>.  
  
## See Also  
 [Role Element &#40;ASSL&#41;](../objects/role-element-assl.md)   
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
