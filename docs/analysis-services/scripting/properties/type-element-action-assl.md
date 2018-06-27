---
title: "Type Element (Action) (ASSL) | Microsoft Docs"
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
# Type Element (Action) (ASSL)
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
  Contains the type of the [Action](../../../analysis-services/scripting/objects/action-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<Action>  
   ...  
   <Type>...</Type>  
   ...  
</Action>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|None|  
|Cardinality|1-1: Required element that occurs once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[Action](../../../analysis-services/scripting/objects/action-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of this element is limited to one of the strings listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*Url*|Displays a variable page in an Internet browser.|  
|*Html*|Executes an HTML script in an Internet browser.|  
|*Statement*|Executes an OLE DB command.|  
|*DrillThrough*|Retrieves a rowset for drillthrough.<br /><br /> This value is identical to *Rowset* and identifies drillthrough actions. It can only be used on actions whose [TargetType](../../../analysis-services/scripting/properties/targettype-element-assl.md) value is set to *Cells*.|  
|*DataSet*|Retrieves a dataset.|  
|*Rowset*|Retrieves a rowset.|  
|*CommandLine*|Executes a command at a command prompt.|  
|*Proprietary*|Performs an operation using an interface different than those listed earlier in this table.|  
|*Report*|Displays a variable page in an Internet browser.<br /><br /> This value is identical to *Url* and identifies report actions.|  
  
 The element that corresponds to the parent of **Type** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Action>.  
  
## See Also  
 [DrillThroughAction Data Type &#40;ASSL&#41;](../../../analysis-services/scripting/data-type/drillthroughaction-data-type-assl.md)   
 [ReportAction Data Type &#40;ASSL&#41;](../../../analysis-services/scripting/data-type/reportaction-data-type-assl.md)   
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  
