---
title: "return Element (XMLA) | Microsoft Docs"
ms.date: 05/08/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: xmla
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# return Element (XMLA)
[!INCLUDE[ssas-appliesto-sqlas-aas](../../../includes/ssas-appliesto-sqlas-aas.md)]
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
  
## Element characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|1-1: Required element that occurs once and only once.|  
  
## Element relationships  
  
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
  
## See also
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  
