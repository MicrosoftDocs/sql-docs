---
title: "RestrictionList Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "RestrictionList Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "urn:schemas-microsoft-com:xml-analysis#RestrictionList"
  - "microsoft.xml.analysis.restrictionlist"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#RestrictionList"
helpviewer_keywords: 
  - "RestrictionList element"
ms.assetid: 2297c005-381e-49a4-a207-826f7f9ea93a
author: minewiskan
ms.author: owend
manager: craigg
---
# RestrictionList Element (XMLA)
  Contains a collection of restriction columns and values used by the [Discover](../xml-elements-methods-discover.md) method.  
  
## Syntax  
  
```  
  
<Restrictions>  
   <RestrictionList>  
      <!-- Zero or more restriction columns and values -->  
   </RestrictionList>  
</Restrictions>  
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
|Parent elements|[Restrictions](restrictions-element-xmla.md)|  
|Child elements|Restriction columns and values (see Remarks).|  
  
## Remarks  
 The `RestrictionList` element contains a collection of restriction columns on which the data returned by the `Discover` method can be filtered. Each restriction column in the `RestrictionList` element is defined by a separate XML element. The value of the restriction column is the data contained by the XML element, and the name of the restriction column corresponds to the name of the XML element.  
  
## See Also  
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
