---
title: "RootMemberIf Element (ASSL) | Microsoft Docs"
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
  - "RootMemberIf Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "RootMemberIf"
helpviewer_keywords: 
  - "RootMemberIf element"
ms.assetid: b695e271-c748-4abc-a09f-acb1014f768f
caps.latest.revision: 34
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# RootMemberIf Element (ASSL)
  Determines how the root member or members of a parent attribute are identified.  
  
## Syntax  
  
```xml  
  
<DimensionAttribute>  
   ...  
   <RootMemberIf>...</RootMemberIf>  
   ...  
</DimensionAttribute>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|*ParentIsBlankSelfOrMissing*|  
|Cardinality|0-1: Optional element that can occur once and only once|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[DimensionAttribute](../../../analysis-services/scripting/data-type/dimensionattribute-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of the **RootMemberIf** element is used only by parent attributes (in other words, the value of the [Usage](../../../analysis-services/scripting/properties/usage-element-dimensionattribute-assl.md) element of the **DimensionAttribute** parent element is set to *Parent*) to determine the root (topmost) members of a parent-child hierarchy.  
  
 The value of this element is limited to one of the strings listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*ParentIsBlankSelfOrMissing*|Only members that meet one or more of the conditions described for *ParentIsBlank*, *ParentIsSelf*, or *ParentIsMissing* are treated as root members.|  
|*ParentIsBlank*|Only members with a null, a zero, or an empty string in the key columns represented by the [KeyColumns](../../../analysis-services/scripting/collections/keycolumns-element-assl.md) collection of **DimensionAttribute** are treated as root members.|  
|*ParentIsSelf*|Only members with themselves as parents are treated as root members.|  
|*ParentIsMissing*|Only members with parents that cannot be found are treated as root members.|  
  
 The enumeration that corresponds to the allowed values for **RootMemberIf** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.RootIfValue>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  