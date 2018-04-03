---
title: "Attribute Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
api_name: 
  - "Attribute Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Attribute"
  - "microsoft.xml.analysis.attribute"
  - "urn:schemas-microsoft-com:xml-analysis#Attribute"
helpviewer_keywords: 
  - "Attribute element"
ms.assetid: 0df9cf44-dc5f-4234-8a5a-daac8aabc0d6
caps.latest.revision: 17
author: "mgblythe"
ms.author: "mblythe"
manager: "mblythe"
---
# Attribute Element (XMLA)
  Defines or filters a member in an attribute on which a parent [Insert](../../../2014/analysis-services/dev-guide/insert-element-xmla.md), [Update](../../../2014/analysis-services/dev-guide/update-element-xmla.md), or [Drop](../../../2014/analysis-services/dev-guide/drop-element-xmla.md) command performs.  
  
## Syntax  
  
```xml  
  
<Attributes>  
   ...  
   <Attribute>  
      <AttributeName>...</AttributeName>  
      <Keys>...</Keys>  
      <!-- The following elements are included only for attributes contained in the Attributes element of a parent Insert or Update command -->  
      <Name>...</Name>  
      <Translations>...</Translations>  
      <CustomRollup>...</CustomRollup>  
      <CustomRollupProperties>...</CustomRollupProperties>  
      <UnaryOperator>...</UnaryOperator>  
      <SkippedLevels>...</SkippedLevels>  
   </Attribute>  
   ...  
</Attributes>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|1-1: Required element that occurs once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Attributes](../../../2014/analysis-services/dev-guide/attributes-element-xmla.md)|  
  
 **Child Elements**  
  
|||  
|-|-|  
|**Ancestor or Parent**|**Child Element**|  
|[Drop](../../../2014/analysis-services/dev-guide/drop-element-xmla.md), [Where](../../../2014/analysis-services/dev-guide/where-element-xmla.md)|[AttributeName](../../../2014/analysis-services/dev-guide/attributename-element-xmla.md), [Keys](../../../2014/analysis-services/dev-guide/keys-element-xmla.md)|  
|[Insert](../../../2014/analysis-services/dev-guide/insert-element-xmla.md), [Update](../../../2014/analysis-services/dev-guide/update-element-xmla.md)|[AttributeName](../../../2014/analysis-services/dev-guide/attributename-element-xmla.md), [CustomRollup](../../../2014/analysis-services/dev-guide/customrollup-element-xmla.md), [CustomRollupProperties](../../../2014/analysis-services/dev-guide/customrollupproperties-element-xmla.md), [Keys](../../../2014/analysis-services/dev-guide/keys-element-xmla.md), [Name](../../../2014/analysis-services/dev-guide/name-element-xmla.md), [SkippedLevels](../../../2014/analysis-services/dev-guide/skippedlevels-element-xmla.md), [Translations](../../../2014/analysis-services/dev-guide/translations-element-xmla.md), [UnaryOperator](../../../2014/analysis-services/dev-guide/unaryoperator-element-xmla.md)|  
  
## Remarks  
 The `Attribute` element defines the attribute member that is inserted, updated, or deleted, respectively, by the `Insert`, `Update`, or `Drop` command. As these commands can operate only on one attribute member at a time, the [Attributes](../../../2014/analysis-services/dev-guide/attributes-element-xmla.md) collection of the `Insert`, `Update`, and `Drop` commands can contain only one `Attribute` element. However, the `Attributes` collection of the `Where` element for the `Drop` and `Update` commands can contain more than one `Attribute` element, so that you can filter the attributes to be dropped or updated in a write-enabled dimension.  
  
## See Also  
 [Properties &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/properties-xmla.md)   
 [Write-Enabled Dimensions](../../../2014/analysis-services/dev-guide/write-enabled-dimensions.md)  
  
  