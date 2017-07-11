---
title: "DataSourcePermissions Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "DataSourcePermissions Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "DataSourcePermissions element"
ms.assetid: 6e1cfbec-65b9-4942-a628-f7f7c891e35f
caps.latest.revision: 12
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# DataSourcePermissions Element (ASSL)
  Contains the collection of [DataSourcePermission](../../../analysis-services/scripting/objects/datasourcepermission-element-assl.md) elements associated with a [DataSource](../../../analysis-services/scripting/data-type/datasource-data-type-assl.md) data type.  
  
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
|Parent elements|[DataSource](../../../analysis-services/scripting/data-type/datasource-data-type-assl.md)|  
|Child elements|[DataSourcePermission](../../../analysis-services/scripting/objects/datasourcepermission-element-assl.md)|  
  
## Remarks  
  
## See Also  
 [Permission Data Type &#40;ASSL&#41;](../../../analysis-services/scripting/data-type/permission-data-type-assl.md)   
 [Collections &#40;ASSL&#41;](../../../analysis-services/scripting/collections/collections-assl.md)  
  
  