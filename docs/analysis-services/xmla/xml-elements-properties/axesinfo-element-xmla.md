---
title: "AxesInfo Element (XMLA) | Microsoft Docs"
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
  - "AxesInfo Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "urn:schemas-microsoft-com:xml-analysis#AxesInfo"
  - "microsoft.xml.analysis.axesinfo"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#AxesInfo"
helpviewer_keywords: 
  - "AxesInfo element"
ms.assetid: 15cfa67d-5acd-4737-8a81-2df34b334d3f
caps.latest.revision: 12
author: "jeannt"
ms.author: "jeannt"
manager: "erikre"
---
# AxesInfo Element (XMLA)
  Contains a collection of [AxisInfo](../../../analysis-services/xmla/xml-elements-properties/axisinfo-element-xmla.md) elements, representing the axis metadata contained by the parent [OlapInfo](../../../analysis-services/xmla/xml-elements-properties/olapinfo-element-xmla.md) element.  
  
## Syntax  
  
```xml  
  
<OlapInfo>  
   ...  
   <AxesInfo>  
      <AxisInfo>...</AxisInfo>  
   </AxesInfo>  
   ...  
</OlapInfo>  
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
|Parent elements|[OlapInfo](../../../analysis-services/xmla/xml-elements-properties/olapinfo-element-xmla.md)|  
|Child elements|[AxisInfo](../../../analysis-services/xmla/xml-elements-properties/axisinfo-element-xmla.md)|  
  
## Remarks  
 The **AxesInfo** element contains one **AxisInfo** element for each axis within the multidimensional dataset returned by a **root** element using the **MDDataSet** data type.  
  
## See Also  
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  