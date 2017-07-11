---
title: "DatabasePermission Element (ASSL) | Microsoft Docs"
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
  - "DatabasePermission Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "DatabasePermission"
helpviewer_keywords: 
  - "DatabasePermission element"
ms.assetid: 6dcb9136-a40d-42e3-ad3b-b8ce8c7ca78c
caps.latest.revision: 39
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# DatabasePermission Element (ASSL)
  Defines the default permissions in a [Database](../../../analysis-services/scripting/objects/database-element-assl.md) element for a specific [Role](../../../analysis-services/scripting/objects/role-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<DatabasePermissions>  
      <DatabasePermission xsi:type="Permission">  
      <!-- The following elements extend Permission -->  
      <Administer>...</Administer>  
...</DatabasePermission>  
</DatabasePermissions>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|[Permission](../../../analysis-services/scripting/data-type/permission-data-type-assl.md)|  
|Default value|False|  
|Cardinality|0-n: Optional element that can occur more than once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[DatabasePermissions](../../../analysis-services/scripting/collections/databasepermissions-element-assl.md)|  
|Child elements|[Administer](../../../analysis-services/scripting/properties/administer-element-assl.md)|  
  
## Remarks  
 **DatabasePermission** objects can exist only for roles owned by the database, and only one **DatabasePermission** object can exist for any role.  
  
 This element has the following validations under DeploymentMode value 2 (Tabular Models).  
  
-   *Administer* attribute default value is set to **False**, except when the user has administrative privileges. For users with administrative privileges the attribute value is set to **True**.  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.DatabasePermission>.  
  
## See Also  
 [Role Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/role-element-assl.md)   
 [Objects &#40;ASSL&#41;](../../../analysis-services/scripting/objects/objects-assl.md)  
  
  