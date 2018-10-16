---
title: "Attribute Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
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
author: minewiskan
ms.author: owend
manager: craigg
---
# Attribute Element (XMLA)
  Defines or filters a member in an attribute on which a parent [Insert](../xml-elements-commands/insert-element-xmla.md), [Update](../xml-elements-commands/update-element-xmla.md), or [Drop](../xml-elements-commands/drop-element-xmla.md) command performs.  
  
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
|Parent elements|[Attributes](attributes-element-xmla.md)|  
  
 **Child Elements**  
  
|||  
|-|-|  
|**Ancestor or Parent**|**Child Element**|  
|[Drop](../xml-elements-commands/drop-element-xmla.md), [Where](name-element-xmla.md), [Keys](keys-element-xmla.md)|  
|[Insert](../xml-elements-commands/insert-element-xmla.md), [Update](../xml-elements-commands/update-element-xmla.md)|[AttributeName](name-element-xmla.md), [CustomRollup](customrollup-element-xmla.md), [CustomRollupProperties](properties-element-xmla.md), [Keys](keys-element-xmla.md), [Name](name-element-xmla.md), [SkippedLevels](skippedlevels-element-xmla.md), [Translations](translations-element-xmla.md), [UnaryOperator](unaryoperator-element-xmla.md)|  
  
## Remarks  
 The `Attribute` element defines the attribute member that is inserted, updated, or deleted, respectively, by the `Insert`, `Update`, or `Drop` command. As these commands can operate only on one attribute member at a time, the [Attributes](attributes-element-xmla.md) collection of the `Insert`, `Update`, and `Drop` commands can contain only one `Attribute` element. However, the `Attributes` collection of the `Where` element for the `Drop` and `Update` commands can contain more than one `Attribute` element, so that you can filter the attributes to be dropped or updated in a write-enabled dimension.  
  
## See Also  
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)   
 [Write-Enabled Dimensions](../../multidimensional-models-olap-logical-dimension-objects/write-enabled-dimensions.md)  
  
  
