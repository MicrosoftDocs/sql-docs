---
title: "MiningStructurePermission Element (ASSL) | Microsoft Docs"
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
  - "MiningStructurePermission Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "MiningStructurePermission"
helpviewer_keywords: 
  - "MiningStructurePermission element"
ms.assetid: 4ba2bfd2-9003-4eed-8049-a74d452894ea
caps.latest.revision: 43
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# MiningStructurePermission Element (ASSL)
  Defines the permissions that members of a [Role](../../../analysis-services/scripting/objects/role-element-assl.md) element have on an individual [MiningStructure](../../../analysis-services/scripting/objects/miningstructure-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<MiningStructurePermissions>  
   <MiningStructurePermission xsi:type="Permission">  
      <AllowDrillthrough>...</AllowDrillthrough>  
  
      <!-- This element also inherits all the child elements listed in Permission -->  
   </MiningStructurePermission>  
</MiningStructurePermissions>  
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
|Parent elements|[MiningStructurePermissions](../../../analysis-services/scripting/collections/miningstructurepermissions-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.MiningStructurePermission>.  
  
 In [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)], the permission **AllowDrillthrough** has been extended to apply to a mining structure. When you assign this permission to a role, any user who is a member of that role can directly query the mining structure by using the following syntax:  
  
```  
SELECT <structure column list> FROM <structure>.CASES  
```  
  
 Moreover, if **AllowDrillthrough** is enabled on both the mining structure and the mining model, users can query structure columns that were not included in the mining model by using the following syntax:  
  
```  
SELECT StructureColumn('<structure column name>' FROM <model>.CASES  
```  
  
 For example, you create a model using only columns for customer key, customer income, and customer purchases. By using drillthrough, a user can return other structure columns that were not included in the mining model, such as customer contact information.  
  
 Therefore, to protect sensitive data or personal information, you should construct your data source view to mask personal information and grant **AllowDrillthrough** permission on a structure only when necessary.  
  
 For more information, see [Drillthrough Queries &#40;Data Mining&#41;](../../../analysis-services/data-mining/drillthrough-queries-data-mining.md).  
  
## See Also  
 <xref:Microsoft.AnalysisServices.MiningModel.AllowDrillThrough%2A>   
 <xref:Microsoft.AnalysisServices.AdomdClient.MiningModel.AllowDrillThrough%2A>   
 [Objects &#40;ASSL&#41;](../../../analysis-services/scripting/objects/objects-assl.md)  
  
  