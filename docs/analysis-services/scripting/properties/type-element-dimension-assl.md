---
title: "Type Element (Dimension) (ASSL) | Microsoft Docs"
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
  - "Type Element (Dimension)"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "TYPE"
helpviewer_keywords: 
  - "Type element"
ms.assetid: 6a2798b1-26c7-49d8-b556-e681c69d9574
caps.latest.revision: 37
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Type Element (Dimension) (ASSL)
  Provides information about the contents of the dimension.  
  
## Syntax  
  
```xml  
  
<Dimension>  
      ...  
   <Type>...</Type>  
   ...  
</Dimension>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|*Regular*|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[Dimension](../../../analysis-services/scripting/objects/dimension-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 Some values for **Type**, for example *Accounts*, determine specific behavior.  
  
 The value of this element is limited to one of the strings listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*Regular*|The dimension is a regular dimension.|  
|*Time*|The dimension is a time dimension.<br /><br /> Note: This value indicates that the dimension supports functionality specific to time dimensions.|  
|*Geography*|The dimension contains geographical attributes.|  
|*Organization*|The dimension contains organizational attributes.|  
|*BillOfMaterials*|The dimension contains bill of materials attributes.|  
|*Accounts*|The dimension contains account-related attributes.<br /><br /> Note: This value indicates that the dimension supports functionality specific to account dimensions.|  
|*Customers*|The dimension contains customer-related attributes.|  
|*Products*|The dimension contains product-related attributes.|  
|*Scenario*|The dimension contains scenario-related attributes.|  
|*Quantitative*|The dimension contains quantitative attributes.|  
|*Utility*|The dimension contains utility attributes.|  
|*Currency*|The dimension contains currency attributes.|  
|*Rates*|The dimension contains exchange rate attributes.|  
|*Channel*|The dimension contains channel attributes.|  
|*Promotion*|The dimension contains promotion-related attributes.|  
  
 The enumeration that corresponds to the allowed values for **Type** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.DimensionType>.  
  
 The element that corresponds to the parent of **Type** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Dimension>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  