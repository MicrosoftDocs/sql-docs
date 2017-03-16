---
title: "return Element (XMLA) | Microsoft Docs"
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
  - "return Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "microsoft.xml.analysis.return"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#return"
  - "urn:schemas-microsoft-com:xml-analysis#return"
helpviewer_keywords: 
  - "return element"
ms.assetid: 3cfe8b74-fec3-4987-a74a-5f731444e024
caps.latest.revision: 14
author: "jeannt"
ms.author: "jeannt"
manager: "erikre"
---
# return Element (XMLA)
  Contains information returned by a [DiscoverResponse](../../../analysis-services/xmla/xml-elements-objects-discoverresponse.md) element in response to a [Discover](../../../analysis-services/xmla/xml-elements-methods-discover.md) method call or an [ExecuteResponse](../../../analysis-services/xmla/xml-elements-objects-executeresponse.md) element in response to an [Execute](../../../analysis-services/xmla/xml-elements-methods-execute.md) method call.  
  
## Syntax  
  
```xml  
  
<DiscoverResponse> <!-- or ExecuteResponse -->  
   <return>  
      <root>...</root>  
      <!-- or -->  
      <results>...</results> <!-- ExecuteResponse only -->  
   </return>  
</DiscoverResponse>  
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
|Parent elements|[DiscoverResponse](../../../analysis-services/xmla/xml-elements-objects-discoverresponse.md), [ExecuteResponse](../../../analysis-services/xmla/xml-elements-objects-executeresponse.md)|  
|Child elements|See the table below.|  
  
|Ancestor|Child elements|  
|--------------|--------------------|  
|[DiscoverResponse](../../../analysis-services/xmla/xml-elements-objects-discoverresponse.md)|[root](../../../analysis-services/xmla/xml-elements-properties/root-element-xmla.md)|  
|[ExecuteResponse](../../../analysis-services/xmla/xml-elements-objects-executeresponse.md)|[root](../../../analysis-services/xmla/xml-elements-properties/root-element-xmla.md) or [results](../../../analysis-services/xmla/xml-elements-properties/results-element-xmla.md)|  
  
## Remarks  
 The **return** element contains the data returned by the **Discover** and **Execute** methods. Typically, the **return** element contains a single **root** element that contains either the data returned by a successful **Discover** or **Execute** method call or an XML for Analysis (XMLA) exception returned by an unsuccessful method call. If the **Execute** method contains a **Batch** command that performs multiple operations, the **return** element contains a **results** element which, in turn, contains one **root** element for each command executed successfully or unsuccessfully by the **Batch** command.  
  
## See Also  
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  