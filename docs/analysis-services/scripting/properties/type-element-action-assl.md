---
title: "Type Element (Action) (ASSL) | Microsoft Docs"
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
  - "Type Element (Action)"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "TYPE"
helpviewer_keywords: 
  - "Type element"
ms.assetid: 534cdf99-1edf-4490-9eaa-61f189a19434
caps.latest.revision: 35
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Type Element (Action) (ASSL)
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
  
  