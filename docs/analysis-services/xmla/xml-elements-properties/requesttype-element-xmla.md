---
title: "RequestType Element (XMLA) | Microsoft Docs"
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
# RequestType Element (XMLA)
[!INCLUDE[ssas-appliesto-sqlas-aas](../../../includes/ssas-appliesto-sqlas-aas.md)]
  Determines the type of metadata returned by the [Discover](../../../analysis-services/xmla/xml-elements-methods-discover.md) method.  
  
## Syntax  
  
```xml  
  
<Discover>  
   ...  
   <RequestType>...</RequestType>  
   ...  
</Discover>  
```  
  
## Element characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|None|  
|Cardinality|1-1: Required element that occurs once and only once.|  
  
## Element relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Discover](../../../analysis-services/xmla/xml-elements-methods-discover.md)|  
|Child elements|None|  
  
## Remarks  
 The **RequestType** element determines the schema rowset from which the **Discover** method returns data. This enumeration is limited to the names of the schema rowsets supported by Analysis Services. For more information about schema rowsets, see [Analysis Services Schema Rowsets](../../../analysis-services/schema-rowsets/analysis-services-schema-rowsets.md).  
  
> [!NOTE]  
>  The **RequestType** element enumerates only schema rowset names. An error occurs if the schema rowset GUID is used.  
  
## See also
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  
