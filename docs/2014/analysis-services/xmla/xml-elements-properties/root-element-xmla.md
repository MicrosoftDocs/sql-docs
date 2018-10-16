---
title: "root Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Root Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine#root"
  - "urn:schemas-microsoft-com:xml-analysis#root"
  - "microsoft.xml.analysis.root"
helpviewer_keywords: 
  - "root element"
ms.assetid: ecd9d6e8-b16c-4d62-9a87-107c413a0056
author: minewiskan
ms.author: owend
manager: craigg
---
# root Element (XMLA)
  Contains a result returned by the [Discover](../xml-elements-methods-discover.md) method or an XML for Analysis (XMLA) command executed using the [Execute](../xml-elements-methods-execute.md) method.  
  
## Syntax  
  
```xml  
  
<return> <!-- or results-->  
   ...  
   <root xmlns="urn:schemas-microsoft-com:xml-analysis:mddataset">...</root> <!-- for Execute method only -->  
   <!-- or -->  
   <root xmlns="urn:schemas-microsoft-com:xml-analysis:rowset">...</root>  
   <!-- or -->  
   <root xmlns= xmlns="urn:schemas-microsoft-com:xml-analysis:empty">...</root>  
   ...  
</return>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Default value|None|  
|Cardinality|1-n: Required element that can occur more than once.|  
  
## Data Type and Length  
  
|Ancestor|Data type|  
|--------------|---------------|  
|[DiscoverResponse](../xml-data-types/rowset-data-type-xmla.md), [olapxmla_EmptyResult](../xml-data-types/emptyresult-data-type-xmla.md)|  
|[ExecuteResponse](../xml-data-types/mddataset-data-type-xmla.md), [Rowset](../xml-data-types/rowset-data-type-xmla.md), [olapxmla_EmptyResult](../xml-data-types/emptyresult-data-type-xmla.md)|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[results](results-element-xmla.md), [return](return-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
 The `root` element contains the information returned in either the [DiscoverResponse](../xml-elements-objects-discoverresponse.md) element returned by a single `Discover` method call, or in the [ExecuteResponse](../xml-elements-objects-executeresponse.md) element returned by a single XMLA command executed by a single `Execute` method call.  
  
## See Also  
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
