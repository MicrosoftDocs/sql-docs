---
title: "DeleteWithDescendants Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "DeleteWithDescendants Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine#DeleteWithDescendants"
  - "microsoft.xml.analysis.deletewithdescendants"
  - "urn:schemas-microsoft-com:xml-analysis#DeleteWithDescendants"
helpviewer_keywords: 
  - "DeleteWithDescendants element"
ms.assetid: adfc9437-aaa7-4364-bcdb-128fcc9a410d
author: minewiskan
ms.author: owend
manager: craigg
---
# DeleteWithDescendants Element (XMLA)
  Indicates whether the descendants of attribute members are also deleted by the parent [Drop](../xml-elements-commands/drop-element-xmla.md) command.  
  
## Syntax  
  
```xml  
  
<Drop>  
   ...  
   <DeleteWithDescendants>...</DeleteWithDescendants>  
   ...  
</Drop>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|Boolean|  
|Default value|False|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Drop](../xml-elements-commands/drop-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
 The `DeleteWithDescendants` element determines whether the `Drop` command should delete the attribute members identified by the [Where](where-element-xmla.md) element, but also that the descendants of those attribute members are to be dropped as well.  
  
> [!NOTE]  
>  This element applies only to attribute members in parent-child hierarchies.  
  
 For more information about deleting and updating attribute members, see [Inserting, Updating, and Dropping Members &#40;XMLA&#41;](../../multidimensional-models-scripting-language-assl-xmla/inserting-updating-and-dropping-members-xmla.md).  
  
## See Also  
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
