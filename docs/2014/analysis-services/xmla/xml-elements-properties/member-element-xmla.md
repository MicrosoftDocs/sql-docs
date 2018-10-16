---
title: "Member Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Member Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "urn:schemas-microsoft-com:xml-analysis#Member"
  - "microsoft.xml.analysis.member"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Member"
helpviewer_keywords: 
  - "Member element"
ms.assetid: 5cc33a1f-192e-4821-a4ef-9a5f2bb7a9f0
author: minewiskan
ms.author: owend
manager: craigg
---
# Member Element (XMLA)
  Represents a single member in a parent [Members](members-element-xmla.md) or [Tuple](tuple-element-xmla.md) element.  
  
## Syntax  
  
```xml  
  
<Members>  
   ...  
   <Member>  
      <UName>...</UName>  
      <Caption>...</Caption>  
      <LName>...</LName>  
      <LNum>...</LNum>  
      <DisplayInfo>...</DisplayInfo>  
   </Member>  
   ...  
</Members>  
<!-- or -->  
<Tuple>  
   ...  
   <Member Hierarchy="string">  
      <UName>...</UName>  
      <Caption>...</Caption>  
      <LName>...</LName>  
      <LNum>...</LNum>  
      <DisplayInfo>...</DisplayInfo>  
   </Member>  
   ...  
</Tuple>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-n: Optional element that can occur more than once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Members](members-element-xmla.md), [Tuple](tuple-element-xmla.md)|  
|Child elements|[Caption](caption-element-xmla.md), [DisplayInfo](displayinfo-element-xmla.md), [LName](name-element-xmla.md), [LNum](lnum-element-xmla.md), [UName](uname-element-xmla.md)|  
  
## Attributes  
  
|Attribute|Description|  
|---------------|-----------------|  
|Hierarchy|Required `String` attribute (for parent `Tuple` elements only). The name of the hierarchy to which the member represented by the `Member` element belongs.|  
  
## Remarks  
 The `Member` element contains the information needed to identify and display a member within a given hierarchy. For parent `Members` elements, the hierarchy is already specified by the `Hierarchy` attribute of the parent element. For parent `Tuple` elements, the hierarchy is specified using the `Hierarchy` attribute of the `Member` element.  
  
## See Also  
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
