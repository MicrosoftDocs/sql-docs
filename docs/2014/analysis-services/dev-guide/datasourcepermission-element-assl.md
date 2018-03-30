---
title: "DataSourcePermission Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
api_name: 
  - "DataSourcePermission Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "DataSourcePermission element"
ms.assetid: 6dc6fb13-034e-479a-902e-27f3fb78c33f
caps.latest.revision: 14
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# DataSourcePermission Element (ASSL)
  Defines the default permissions in a [DataSource](../../../2014/analysis-services/dev-guide/datasource-data-type-assl.md) data type for a specific [Role](../../../2014/analysis-services/dev-guide/role-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<DataSourcePermissions>  
   <DataSourcePermission xsi:type="Permission">  
      <!-- No child elements other than those from Permission are defined -->  
...</DataSourcePermission>  
</DataSourcePermissions>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|[Permission](../../../2014/analysis-services/dev-guide/permission-data-type-assl.md)|  
|Default value|None|  
|Cardinality|0-n: Optional element that can occur once or more than once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[DataSourcePermissions](../../../2014/analysis-services/dev-guide/datasourcepermissions-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 `DataSourcePermission` objects can exist only for roles owned by the database, and only one `DataSourcePermission` object can exist for any role.  
  
## See Also  
 [Role Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/role-element-assl.md)   
 [Objects &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/objects-assl.md)  
  
  