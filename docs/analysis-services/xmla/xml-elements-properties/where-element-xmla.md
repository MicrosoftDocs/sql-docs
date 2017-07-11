---
title: "Where Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "Where Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "urn:schemas-microsoft-com:xml-analysis#Where"
  - "microsoft.xml.analysis.where"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Where"
helpviewer_keywords: 
  - "Where element"
ms.assetid: 81fb4190-3379-4ddf-8795-a0772f3b92bb
caps.latest.revision: 12
author: "jeannt"
ms.author: "jeannt"
manager: "erikre"
---
# Where Element (XMLA)
  Defines a filter condition used by the parent [Drop](../../../analysis-services/xmla/xml-elements-commands/drop-element-xmla.md) or [Update](../../../analysis-services/xmla/xml-elements-commands/update-element-xmla.md) command.  
  
## Syntax  
  
```xml  
  
<Drop> <!-- or Update -->  
   ...  
   <Where>  
      <Attributes>...</Attributes>  
   </Where>  
   ...  
</Insert>  
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
|Parent elements|[Drop](../../../analysis-services/xmla/xml-elements-commands/drop-element-xmla.md), [Update](../../../analysis-services/xmla/xml-elements-commands/update-element-xmla.md)|  
|Child elements|[Attributes](../../../analysis-services/xmla/xml-elements-properties/attributes-element-xmla.md)|  
  
## Remarks  
 For **Drop** commands, the **Where** element, combined with the [DeleteWithDescendants](../../../analysis-services/xmla/xml-elements-properties/deletewithdescendants-element-xmla.md) element, identifies the scope of attribute members to be dropped.  
  
 For **Update** commands, the **Where** element identifies the scope of attribute members to be updated. Multiple attribute members can be updated by using a combination of attributes included in the **Attributes** collection of the parent **Update** command and the **Attributes** collection of the **Where** element.  
  
 For more information about deleting and updating attribute members, see [Inserting, Updating, and Dropping Members &#40;XMLA&#41;](../../../analysis-services/multidimensional-models-scripting-language-assl-xmla/inserting-updating-and-dropping-members-xmla.md).  
  
## See Also  
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  