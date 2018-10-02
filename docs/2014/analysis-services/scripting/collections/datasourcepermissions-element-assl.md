---
title: "DataSourcePermissions Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "DataSourcePermissions Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "DataSourcePermissions element"
ms.assetid: 6e1cfbec-65b9-4942-a628-f7f7c891e35f
author: minewiskan
ms.author: owend
manager: craigg
---
# DataSourcePermissions Element (ASSL)
  Contains the collection of [DataSourcePermission](../objects/datasourcepermission-element-assl.md) elements associated with a [DataSource](../data-type/datasource-data-type-assl.md) data type.  
  
## Syntax  
  
```xml  
  
<DataSource>  
   ...  
   <DataSourcePermissions>  
      <DataSourcePermission>...</DataSourcePermission>  
   </DataSourcePermissions>  
   ...  
</DataSource>  
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
|Parent elements|[DataSource](../data-type/datasource-data-type-assl.md)|  
|Child elements|[DataSourcePermission](../objects/datasourcepermission-element-assl.md)|  
  
## Remarks  
  
## See Also  
 [Permission Data Type &#40;ASSL&#41;](../data-type/permission-data-type-assl.md)   
 [Collections &#40;ASSL&#41;](collections-assl.md)  
  
  
