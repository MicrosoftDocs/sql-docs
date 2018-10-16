---
title: "Restrictions Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Restrictions Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "urn:schemas-microsoft-com:xml-analysis#Restrictions"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Restrictions"
  - "microsoft.xml.analysis.restrictions"
helpviewer_keywords: 
  - "Restrictions element"
ms.assetid: e745ce13-b468-4372-a6f0-0da3d772dda3
author: minewiskan
ms.author: owend
manager: craigg
---
# Restrictions Element (XMLA)
  Contains restriction columns and data used by the [Discover](../xml-elements-methods-discover.md) method.  
  
## Syntax  
  
```  
  
<Discover>  
...  
   <Restrictions>  
      <RestrictionList>...</RestrictionList>  
   </Restrictions>  
...  
</Discover>  
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
|Parent elements|[Discover](../xml-elements-methods-discover.md)|  
|Child elements|[RestrictionList](restrictionlist-element-xmla.md)|  
  
## Remarks  
 The `Restrictions` element represents restriction columns and data used to restrict the information retrieved by the `Discover` method.  
  
## Example  
  
```  
<Discover xmlns="urn:schemas-microsoft-com:xml-analysis">  
   <RequestType>DISCOVER_PROPERTIES</RequestType>  
   <Restrictions>  
      <RestrictionList xmlns="urn:schemas-microsoft-com:xml-analysis">  
         <PropertyName>Catalog</PropertyName>  
      </RestrictionList>  
   </Restrictions>  
   <Properties />  
</Discover>  
```  
  
## See Also  
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
