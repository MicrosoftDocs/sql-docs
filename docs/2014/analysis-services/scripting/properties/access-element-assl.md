---
title: "Access Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Access Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "Access"
helpviewer_keywords: 
  - "Access element"
ms.assetid: 6ad99010-fac5-48e9-a099-ecbca380e127
author: minewiskan
ms.author: owend
manager: craigg
---
# Access Element (ASSL)
  Indicates the level of access given to a [CellPermission](../objects/cellpermission-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<CellPermission>  
   ...  
   <Access>...</Access>  
   ...  
</CellPermission>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[CellPermission](../objects/cellpermission-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value for this element is limited to one of the strings listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*Read*|Read-only access is allowed.|  
|*ReadContingent*|Read-contingent access is allowed.|  
|*ReadWrite*|Read-write access is allowed.|  
  
 The enumeration that corresponds to the allowed values for `Access` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.CellPermissionAccess>.  
  
## See Also  
 [Role Element &#40;ASSL&#41;](../objects/role-element-assl.md)   
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
