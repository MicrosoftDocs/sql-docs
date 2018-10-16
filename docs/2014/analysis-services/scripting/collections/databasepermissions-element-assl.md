---
title: "DatabasePermissions Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "DatabasePermissions Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "DatabasePermissions"
helpviewer_keywords: 
  - "DatabasePermissions element"
ms.assetid: c4ce0da3-f7ba-4f11-8cd8-236c32992aaf
author: minewiskan
ms.author: owend
manager: craigg
---
# DatabasePermissions Element (ASSL)
  Contains the collection of [DatabasePermission](../objects/databasepermission-element-assl.md) elements associated with a [Database](../objects/database-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<Database>  
   ...  
   <DatabasePermissions>  
      <DatabasePermission>...</DatabasePermission>  
      </DatabasePermissions>  
      ...  
</Database>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Database](../objects/database-element-assl.md)|  
|Child elements|[DatabasePermission](../objects/databasepermission-element-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.DatabasePermissionCollection>.  
  
## See Also  
 [Permission Data Type &#40;ASSL&#41;](../data-type/permission-data-type-assl.md)   
 [Collections &#40;ASSL&#41;](collections-assl.md)  
  
  
