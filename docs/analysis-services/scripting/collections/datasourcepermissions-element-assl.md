---
title: "DataSourcePermissions Element (ASSL) | Microsoft Docs"
ms.date: 05/03/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: assl
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# DataSourcePermissions Element (ASSL)
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
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
  
  
