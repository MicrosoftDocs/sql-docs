---
title: "DataSourcePermission Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
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
author: minewiskan
ms.author: owend
manager: craigg
---
# DataSourcePermission Element (ASSL)
  Defines the default permissions in a [DataSource](../data-type/datasource-data-type-assl.md) data type for a specific [Role](role-element-assl.md) element.  
  
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
|Data type and length|[Permission](../data-type/permission-data-type-assl.md)|  
|Default value|None|  
|Cardinality|0-n: Optional element that can occur once or more than once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[DataSourcePermissions](../collections/datasourcepermissions-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 `DataSourcePermission` objects can exist only for roles owned by the database, and only one `DataSourcePermission` object can exist for any role.  
  
## See Also  
 [Role Element &#40;ASSL&#41;](role-element-assl.md)   
 [Objects &#40;ASSL&#41;](objects-assl.md)  
  
  
