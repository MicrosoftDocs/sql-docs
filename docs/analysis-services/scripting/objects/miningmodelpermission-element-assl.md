---
title: "MiningModelPermission Element (ASSL) | Microsoft Docs"
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
  - "MiningModelPermission Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "MiningModelPermission"
helpviewer_keywords: 
  - "MiningModelPermission element"
ms.assetid: 4bd2f7e7-ff0d-404e-96fb-7e2c4eeb91e9
caps.latest.revision: 39
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# MiningModelPermission Element (ASSL)
  Defines the permissions members of a [Role](../../../analysis-services/scripting/objects/role-element-assl.md) element have on an individual [MiningModel](../../../analysis-services/scripting/objects/miningmodel-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<MiningModelPermissions>  
   <MiningModelPermission xsi:type="Permission">  
      <!-- The following elements extend Permission -->  
      <AllowDrillThrough>...</AllowDrillThrough>  
      <AllowBrowsing>...</AllowBrowsing>  
   </MiningModelPermission>  
</MiningModelPermissions>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|[Permission](../../../analysis-services/scripting/data-type/permission-data-type-assl.md)|  
|Default value|None|  
|Cardinality|0-n: Optional element that can occur more than once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[MiningModelPermissions](../../../analysis-services/scripting/collections/miningmodelpermissions-element-assl.md)|  
|Child elements|[AllowBrowsing](../../../analysis-services/scripting/properties/allowbrowsing-element-assl.md), [AllowDrillThrough](../../../analysis-services/scripting/properties/allowdrillthrough-element-assl.md)|  
  
## Remarks  
 In [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)], you can enable drillthrough on mining structures by adding the **AllowDrillthrough** permission to the [MiningStructurePermissions](../../../analysis-services/scripting/collections/miningstructurepermissions-element-assl.md) collection. If **AllowDrillthrough** is enabled on both the mining structure and the mining model, any member of a role that has [AllowDrillThrough Element &#40;ASSL&#41;](../../../analysis-services/scripting/properties/allowdrillthrough-element-assl.md) permissions on the model can query the data mining model, and return structure columns that were not included in the model, by using the following syntax:  
  
```  
SELECT StructureColumn('<column name>') FROM <model>.CASES  
```  
  
 Therefore, to protect sensitive data or personal information, you should grant **AllowDrillthrough** permission on a mining model only when necessary. For more information, see [AllowDrillThrough Element &#40;ASSL&#41;](../../../analysis-services/scripting/properties/allowdrillthrough-element-assl.md).  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.MiningModelPermission>.  
  
## See Also  
 [Objects &#40;ASSL&#41;](../../../analysis-services/scripting/objects/objects-assl.md)  
  
  