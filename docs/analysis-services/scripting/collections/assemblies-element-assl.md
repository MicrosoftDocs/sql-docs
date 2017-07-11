---
title: "Assemblies Element (ASSL) | Microsoft Docs"
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
  - "Assemblies Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "Assemblies"
helpviewer_keywords: 
  - "Assemblies element"
ms.assetid: 8c9be991-0717-4fcf-97d9-13df0f27da05
caps.latest.revision: 39
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Assemblies Element (ASSL)
  Contains the collection of [Assembly](../../../analysis-services/scripting/objects/assembly-element-assl.md) elements associated with a [Server](../../../analysis-services/scripting/objects/server-element-assl.md) or [Database](../../../analysis-services/scripting/objects/database-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<Server> <!-- or Database -->  
   ...  
   <Assemblies>  
      <Assembly xsi:type="ClrAssembly">...</Assembly>  
            <Assembly xsi:type="ComAssembly">...</Assembly>  
   </Assemblies>  
   ...  
</Server>  
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
|Parent elements|[Database](../../../analysis-services/scripting/objects/database-element-assl.md), [Server](../../../analysis-services/scripting/objects/server-element-assl.md)|  
|Child elements|[Assembly](../../../analysis-services/scripting/objects/assembly-element-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.AssemblyCollection>.  
  
## See Also  
 [Collections &#40;ASSL&#41;](../../../analysis-services/scripting/collections/collections-assl.md)  
  
  