---
title: "Administer Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Administer Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "Administer"
helpviewer_keywords: 
  - "Administer element"
ms.assetid: 52924cd6-6176-47c8-ab17-4ee0e0ce42b1
author: minewiskan
ms.author: owend
manager: craigg
---
# Administer Element (ASSL)
  Indicates whether the associated permission includes the right to administer a [Database](../objects/database-element-assl.md) element.  
  
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
|Parent elements|[DatabasePermission](../objects/databasepermission-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The `Administer` element indicates whether a user can perform administrative functions only on the specified database. The server administrator role can perform administrative functions on all databases contained by the instance.  
  
 The element that corresponds to the parent of `Administer` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.DatabasePermission>.  
  
## See Also  
 [Permission Data Type &#40;ASSL&#41;](../data-type/permission-data-type-assl.md)   
 [Role Element &#40;ASSL&#41;](../objects/role-element-assl.md)   
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
