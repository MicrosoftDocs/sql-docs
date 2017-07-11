---
title: "NamingTemplate Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "NamingTemplate Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "NamingTemplate"
helpviewer_keywords: 
  - "NamingTemplate element"
ms.assetid: d68d765c-f012-40c1-acd4-32741ee2eadf
caps.latest.revision: 34
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# NamingTemplate Element (ASSL)
  Defines how levels are named in a parent-child hierarchy constructed from the [DimensionAttribute](../../../analysis-services/scripting/data-type/dimensionattribute-data-type-assl.md) parent element.  
  
## Syntax  
  
```xml  
  
<DimensionAttribute>  
   ...  
      <NamingTemplate>...</NamingTemplate>  
   ...  
</DimensionAttribute>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[DimensionAttribute](../../../analysis-services/scripting/data-type/dimensionattribute-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of the **NamingTemplate** element is used only by parent attributes (in other words, the value of the [Usage](../../../analysis-services/scripting/properties/usage-element-dimensionattribute-assl.md) element of the **DimensionAttribute** parent element is set to *Parent*).  
  
 When a parent attribute is used to construct a hierarchy, the levels of the hierarchy are determined by the parent-child relationships between members contained by the parent attribute. Therefore, unlike other dimensions, the level names cannot be drawn from the attribute names used for the hierarchy.  
  
 Instead, a naming template is used to generate level names for parent-child hierarchies. The **NamingTemplate** element, defined in the parent attribute, contains a string expression used to define level names. There are two ways to define a naming template for a parent attribute. You can either design a naming pattern, or you can specify a list of names.  
  
 A naming pattern contains an asterisk (`*`) as a placeholder character for a counter that is incremented and inserted into the name of each new and deeper level. For example, using `Level *` results in the level names `Level 01`, `Level 02`, `Level 03`, and so on, if no (All) level is defined. If a naming pattern does not contain the placeholder character, it is first used as is, and then subsequent level names are formed by appending a space and a number to the end of the pattern. For example, using `Level` results in the level names `Level`, `Level 01`, `Level 02`, and so on.  
  
 To use a specific set of names for the naming, the value of the **NamingTemplate** element is set to a semicolon-delimited list of level names. Each name in the list is used for a subsequent level name. If the number of levels exceeds the number of names in the list, the last name in the list is used as a template for any additional level names, with a space and ordinal number appended to the last name as described earlier. For example, using `Division;Group;Unit` results in the level names `Division`, `Group`, `Unit`, `Unit 01`, `Unit 02`, and so on. By contrast, using `Division;Group;Unit *` results in the level names `Division`, `Group`, `Unit 03`, `Unit 04`, and so on.  
  
 Each name in the list is treated as a template to ensure uniqueness of level names. For example, using `Manager;Team Lead;Manager;Team Lead;Worker *` results in the level names `Manager`, `Team Lead`, `Manager 01`, `Team Lead 01`, `Worker 05`, `Worker 06`.  
  
 Use two asterisks (**) to include the asterisk (\*) character in a level name as part of a naming template.  
  
 The element that corresponds to the parent of **NamingTemplate** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.DimensionAttribute>.  
  
## See Also  
 [NamingTemplateTranslations Element &#40;ASSL&#41;](../../../analysis-services/scripting/collections/namingtemplatetranslations-element-assl.md)   
 [DimensionAttribute Data Type &#40;ASSL&#41;](../../../analysis-services/scripting/data-type/dimensionattribute-data-type-assl.md)   
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  