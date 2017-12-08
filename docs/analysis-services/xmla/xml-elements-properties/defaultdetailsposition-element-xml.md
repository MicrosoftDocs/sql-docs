---
title: "DefaultDetailsPosition Element (XML) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: "analysis-services"
ms.prod_service: "analysis-services, azure-analysis-services"
ms.service: ""
ms.component: ""
ms.reviewer: ""
ms.suite: "pro-bi"
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
applies_to: 
  - "SQL Server 2016 Preview"
ms.assetid: 851ad331-aefd-4277-a5e5-e32a8f5c5e22
caps.latest.revision: 6
author: "Minewiskan"
ms.author: "owend"
manager: "kfile"
ms.workload: "Inactive"
---
# DefaultDetailsPosition Element (XML)
[!INCLUDE[ssas-appliesto-sqlas-aas](../../../includes/ssas-appliesto-sqlas-aas.md)]
  Contains information about the position of the element in a collection of elements.  
  
## Syntax  
  
```xml  
  
<RelationshipEndVisualizationProperties>  
   ...  
   <DefaultDetailsPosition>...</DefaultDetailsPosition>  
   ...  
</RelationshipEndVisualizationProperties>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|Integer|  
|Default value|-1|  
|Cardinality|0-1: Optional element that occurs once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[RelationshipEndVisualizationProperties](../../../analysis-services/scripting/data-type/relationshipendvisualizationproperties-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 For **RelationshipEndVisualizationProperties** elements, the **DefaultDetailsPosition** element contains the position of the default detail element in a collection of details. The default value of **false** indicates there is no default detail to be used.  
  
  
