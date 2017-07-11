---
title: "Attributes Element (XMLA) | Microsoft Docs"
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
  - "Attributes Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Attributes"
  - "microsoft.xml.analysis.attributes"
  - "urn:schemas-microsoft-com:xml-analysis#Attributes"
helpviewer_keywords: 
  - "Attributes element"
ms.assetid: c0393de8-44e8-46de-af78-1fd66c218521
caps.latest.revision: 15
author: "jeannt"
ms.author: "jeannt"
manager: "erikre"
---
# Attributes Element (XMLA)
  Contains a collection of [Attribute](../../../analysis-services/xmla/xml-elements-properties/attribute-element-xmla.md) elements used by the parent [Insert](../../../analysis-services/xmla/xml-elements-commands/insert-element-xmla.md) or [Update](../../../analysis-services/xmla/xml-elements-commands/update-element-xmla.md) command, or by the parent [Where](../../../analysis-services/xmla/xml-elements-properties/where-element-xmla.md) element.  
  
## Syntax  
  
```xml  
  
<Insert > <!-- or one of the elements listed below in the Element Relationships table -->  
   ...  
   <Attributes>  
      <Attribute>...</Attribute>  
   </Attributes>  
   ...  
</Insert>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Insert](../../../analysis-services/xmla/xml-elements-commands/insert-element-xmla.md), [Update](../../../analysis-services/xmla/xml-elements-commands/update-element-xmla.md), [Where](../../../analysis-services/xmla/xml-elements-properties/where-element-xmla.md)|  
|Child elements|[Attribute](../../../analysis-services/xmla/xml-elements-properties/attribute-element-xmla.md)|  
  
## Remarks  
  
## See Also  
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  