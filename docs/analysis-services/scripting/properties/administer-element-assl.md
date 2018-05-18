---
title: "Administer Element (ASSL) | Microsoft Docs"
ms.date: 05/08/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: assl
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Administer Element (ASSL)
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
  Indicates whether the associated permission includes the right to administer a [Database](../../../analysis-services/scripting/objects/database-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<DatabasePermission>  
      ...  
      <Administer>...</Administer>  
   ...  
</DatabasePermission>  
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
|Parent elements|[DatabasePermission](../../../analysis-services/scripting/objects/databasepermission-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The **Administer** element indicates whether a user can perform administrative functions only on the specified database. The server administrator role can perform administrative functions on all databases contained by the instance.  
  
 The element that corresponds to the parent of **Administer** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.DatabasePermission>.  
  
## See Also  
 [Permission Data Type &#40;ASSL&#41;](../../../analysis-services/scripting/data-type/permission-data-type-assl.md)   
 [Role Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/role-element-assl.md)   
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  
