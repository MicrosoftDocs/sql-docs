---
title: "RequestType Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "RequestType Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine#RequestType"
  - "urn:schemas-microsoft-com:xml-analysis#RequestType"
  - "microsoft.xml.analysis.requesttype"
helpviewer_keywords: 
  - "RequestType element"
ms.assetid: 54270a57-e327-4233-b4b2-d85b44652ac5
author: minewiskan
ms.author: owend
manager: craigg
---
# RequestType Element (XMLA)
  Determines the type of metadata returned by the [Discover](../xml-elements-methods-discover.md) method.  
  
## Syntax  
  
```xml  
  
<Discover>  
   ...  
   <RequestType>...</RequestType>  
   ...  
</Discover>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|None|  
|Cardinality|1-1: Required element that occurs once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Discover](../xml-elements-methods-discover.md)|  
|Child elements|None|  
  
## Remarks  
 The `RequestType` element determines the schema rowset from which the `Discover` method returns data. This enumeration is limited to the names of the schema rowsets supported by [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)]. For more information about schema rowsets, see [Analysis Services Schema Rowsets](../../schema-rowsets/analysis-services-schema-rowsets.md).  
  
> [!NOTE]  
>  The `RequestType` element enumerates only schema rowset names. An error occurs if the schema rowset GUID is used.  
  
## See Also  
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
