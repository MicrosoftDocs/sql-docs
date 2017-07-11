---
title: "DataSourceImpersonationInfo Element (ASSL) | Microsoft Docs"
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
  - "DataSourceImpersonationInfo Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "DataSourceImpersonationInfo element"
ms.assetid: a153044b-2d6c-406b-aeb3-15bf096931f4
caps.latest.revision: 12
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# DataSourceImpersonationInfo Element (ASSL)
  Contains the information used to determine impersonation behavior when connecting to the data source for a [Database](../../../analysis-services/scripting/objects/database-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<Database>  
   <DataSourceImpersonationInfo>  
      <!-- Child elements are only those inherited from ImpersonationInfo -->  
   </DataSourceImpersonationInfo>  
</Database>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|[ImpersonationInfo Data Type &#40;ASSL&#41;](../../../analysis-services/scripting/data-type/impersonationinfo-data-type-assl.md)|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Database](../../../analysis-services/scripting/objects/database-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  