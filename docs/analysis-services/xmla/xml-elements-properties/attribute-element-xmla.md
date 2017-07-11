---
title: "Attribute Element (XMLA) | Microsoft Docs"
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
  - "Attribute Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Attribute"
  - "microsoft.xml.analysis.attribute"
  - "urn:schemas-microsoft-com:xml-analysis#Attribute"
helpviewer_keywords: 
  - "Attribute element"
ms.assetid: 0df9cf44-dc5f-4234-8a5a-daac8aabc0d6
caps.latest.revision: 17
author: "jeannt"
ms.author: "jeannt"
manager: "erikre"
---
# Attribute Element (XMLA)
  Defines or filters a member in an attribute on which a parent [Insert](../../../analysis-services/xmla/xml-elements-commands/insert-element-xmla.md), [Update](../../../analysis-services/xmla/xml-elements-commands/update-element-xmla.md), or [Drop](../../../analysis-services/xmla/xml-elements-commands/drop-element-xmla.md) command performs.  
  
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
|Parent elements|[Attributes](../../../analysis-services/xmla/xml-elements-properties/attributes-element-xmla.md)|  
|Child elements|See the table below.|  
  
|Ancestor or Parent|Child Element|  
|------------------------|-------------------|  
|[Drop](../../../analysis-services/xmla/xml-elements-commands/drop-element-xmla.md), [Where](../../../analysis-services/xmla/xml-elements-properties/where-element-xmla.md)|[AttributeName](../../../analysis-services/xmla/xml-elements-properties/attributename-element-xmla.md), [Keys](../../../analysis-services/xmla/xml-elements-properties/keys-element-xmla.md)|  
|[Insert](../../../analysis-services/xmla/xml-elements-commands/insert-element-xmla.md), [Update](../../../analysis-services/xmla/xml-elements-commands/update-element-xmla.md)|[AttributeName](../../../analysis-services/xmla/xml-elements-properties/attributename-element-xmla.md), [CustomRollup](../../../analysis-services/xmla/xml-elements-properties/customrollup-element-xmla.md), [CustomRollupProperties](../../../analysis-services/xmla/xml-elements-properties/customrollupproperties-element-xmla.md), [Keys](../../../analysis-services/xmla/xml-elements-properties/keys-element-xmla.md), [Name](../../../analysis-services/xmla/xml-elements-properties/name-element-xmla.md), [SkippedLevels](../../../analysis-services/xmla/xml-elements-properties/skippedlevels-element-xmla.md), [Translations](../../../analysis-services/xmla/xml-elements-properties/translations-element-xmla.md), [UnaryOperator](../../../analysis-services/xmla/xml-elements-properties/unaryoperator-element-xmla.md)|  
  
## Remarks  
 The **Attribute** element defines the attribute member that is inserted, updated, or deleted, respectively, by the **Insert**, **Update**, or **Drop** command. As these commands can operate only on one attribute member at a time, the [Attributes](../../../analysis-services/xmla/xml-elements-properties/attributes-element-xmla.md) collection of the **Insert**, **Update**, and **Drop** commands can contain only one **Attribute** element. However, the **Attributes** collection of the **Where** element for the **Drop** and **Update** commands can contain more than one **Attribute** element, so that you can filter the attributes to be dropped or updated in a write-enabled dimension.  
  
## See Also  
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)   
 [Write-Enabled Dimensions](../../../analysis-services/multidimensional-models-olap-logical-dimension-objects/write-enabled-dimensions.md)  
  
  