---
title: "Type Element (Dimension) (ASSL) | Microsoft Docs"
ms.date: 5/8/2018
ms.prod: sql
ms.custom: assl
ms.reviewer: owend
ms.technology: analysis-services
ms.topic: reference
author: minewiskan
ms.author: owend
manager: kfile
---
# Type Element (Dimension) (ASSL)
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
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
  
  
