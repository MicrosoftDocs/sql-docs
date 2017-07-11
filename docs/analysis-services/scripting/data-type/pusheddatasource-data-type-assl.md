---
title: "PushedDataSource Data Type (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "PushedDataSource Data Type"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "PushedDataSource"
helpviewer_keywords: 
  - "PushedDataSource data type"
ms.assetid: b319ee87-7c0a-41ec-a8af-cc7089aeb6ad
caps.latest.revision: 36
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# PushedDataSource Data Type (ASSL)
  Defines a primitive data type that represents a data source (such as a [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] package) used for "pushing" data into a [Cube](../../../analysis-services/scripting/objects/cube-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<PushedDataSource>  
   <Root>...</Root>  
   <EndOfData>...</EndOfData>  
</PushedDataSource>  
```  
  
## Data Type Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Base data types|None|  
|Derived data types|None|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|[EndOfData](../../../analysis-services/scripting/properties/endofdata-element-assl.md), [Root](../../../analysis-services/scripting/properties/root-element-assl.md)|  
|Derived elements|None|  
  
## Remarks  
 **PushedDataSource** is used only within a processing command as an out-of-line data source. Persisted data sources are never of this type.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../analysis-services/scripting/data-type/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  