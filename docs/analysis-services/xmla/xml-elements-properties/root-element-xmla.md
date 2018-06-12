---
title: "root Element (XMLA) | Microsoft Docs"
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
# root Element (XMLA)
[!INCLUDE[ssas-appliesto-sqlas-aas](../../../includes/ssas-appliesto-sqlas-aas.md)]
  Contains a result returned by the [Discover](../../../analysis-services/xmla/xml-elements-methods-discover.md) method or an XML for Analysis (XMLA) command executed using the [Execute](../../../analysis-services/xmla/xml-elements-methods-execute.md) method.  
  
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
  
## Element characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|See the table below.|  
|Default value|None|  
|Cardinality|1-n: Required element that can occur more than once.|  
  
|Ancestor|Data type|  
|--------------|---------------|  
|[DiscoverResponse](../../../analysis-services/xmla/xml-elements-objects-discoverresponse.md)|[Rowset](../../../analysis-services/xmla/xml-data-types/rowset-data-type-xmla.md), [olapxmla_EmptyResult](../../../analysis-services/xmla/xml-data-types/emptyresult-data-type-xmla.md)|  
|[ExecuteResponse](../../../analysis-services/xmla/xml-elements-objects-executeresponse.md)|[MDDataSet](../../../analysis-services/xmla/xml-data-types/mddataset-data-type-xmla.md), [Rowset](../../../analysis-services/xmla/xml-data-types/rowset-data-type-xmla.md), [olapxmla_EmptyResult](../../../analysis-services/xmla/xml-data-types/emptyresult-data-type-xmla.md)|  
  
## Element relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[results](../../../analysis-services/xmla/xml-elements-properties/results-element-xmla.md), [return](../../../analysis-services/xmla/xml-elements-properties/return-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
 The **root** element contains the information returned in either the [DiscoverResponse](../../../analysis-services/xmla/xml-elements-objects-discoverresponse.md) element returned by a single **Discover** method call, or in the [ExecuteResponse](../../../analysis-services/xmla/xml-elements-objects-executeresponse.md) element returned by a single XMLA command executed by a single **Execute** method call.  
  
## See also
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  
