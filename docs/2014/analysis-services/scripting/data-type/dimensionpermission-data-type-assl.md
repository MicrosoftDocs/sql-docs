---
title: "DimensionPermission Data Type (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "DimensionPermission Data Type"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "DimensionPermission data type"
ms.assetid: 066405ff-903f-467a-b0d5-e58653952c52
author: minewiskan
ms.author: owend
manager: craigg
---
# DimensionPermission Data Type (ASSL)
  Defines a derived data type that represents the permissions assigned to a database dimension.  
  
## Syntax  
  
```xml  
  
<DimensionPermission>  
   <!-- The following elements extend Permission -->  
   <AttributePermissions>...</AttributePermissions>  
   <AllowedRowsExpression>...</AllowedRowsExpression>  
</DimensionPermission>  
```  
  
## Data Type Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Base data types|[Permission](permission-data-type-assl.md)|  
|Derived data types|None|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|[AttributePermissions](../collections/attributepermissions-element-assl.md), [AllowedRowsExpression](../collections/attributepermissions-element-assl.md)|  
|Derived elements|[DimensionPermission](../objects/dimensionpermission-element-assl.md)|  
  
## Remarks  
 This element has the following validations under DeploymentMode value 2 (tabular server mode).  
  
-   *AttributePermission* attribute must be empty or an error occurs.  
  
 This element has the following validations under DeploymentMode value 0 (OLAP).  
  
-   *AllowedRowsExpression* attribute must be empty or an error occurs.  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.DimensionPermission>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](analysis-services-scripting-language-xml-data-types-assl.md)  
  
  
