---
title: "Roles Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Roles Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "Roles"
helpviewer_keywords: 
  - "Roles element"
ms.assetid: 4191b7ce-bae4-4200-8550-3904420efafd
author: minewiskan
ms.author: owend
manager: craigg
---
# Roles Element (ASSL)
  Contains the collection of [Role](../objects/role-element-assl.md) elements defined under the parent element.  
  
## Syntax  
  
```xml  
  
<Database> <!-- or Server -->  
   ...  
   <Roles>  
      <Role>...</Role>  
   </Roles>  
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
|Parent elements|[Database](../objects/database-element-assl.md), [Server](../objects/server-element-assl.md)|  
|Child elements|[Role](../objects/role-element-assl.md)|  
  
## Remarks  
 The `Roles` element associated with a `Server` element contains only one role, named Administrators, which represents the server administrator role. The server administrator role cannot be changed or deleted, nor can additional roles be added to the collection.  
  
 The `Roles` element associate with a `Database` element contains the roles defined for that database.  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.RoleCollection>.  
  
## See Also  
 [Collections &#40;ASSL&#41;](collections-assl.md)  
  
  
