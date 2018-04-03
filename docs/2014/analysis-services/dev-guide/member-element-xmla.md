---
title: "Member Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
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
caps.latest.revision: 11
author: "mgblythe"
ms.author: "mblythe"
manager: "mblythe"
---
# Member Element (XMLA)
  Represents a single member in a parent [Members](../../../2014/analysis-services/dev-guide/members-element-xmla.md) or [Tuple](../../../2014/analysis-services/dev-guide/tuple-element-xmla.md) element.  
  
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
|Parent elements|[Members](../../../2014/analysis-services/dev-guide/members-element-xmla.md), [Tuple](../../../2014/analysis-services/dev-guide/tuple-element-xmla.md)|  
|Child elements|[Caption](../../../2014/analysis-services/dev-guide/caption-element-xmla.md), [DisplayInfo](../../../2014/analysis-services/dev-guide/displayinfo-element-xmla.md), [LName](../../../2014/analysis-services/dev-guide/lname-element-xmla.md), [LNum](../../../2014/analysis-services/dev-guide/lnum-element-xmla.md), [UName](../../../2014/analysis-services/dev-guide/uname-element-xmla.md)|  
  
## Attributes  
  
|Attribute|Description|  
|---------------|-----------------|  
|Hierarchy|Required `String` attribute (for parent `Tuple` elements only). The name of the hierarchy to which the member represented by the `Member` element belongs.|  
  
## Remarks  
 The `Member` element contains the information needed to identify and display a member within a given hierarchy. For parent `Members` elements, the hierarchy is already specified by the `Hierarchy` attribute of the parent element. For parent `Tuple` elements, the hierarchy is specified using the `Hierarchy` attribute of the `Member` element.  
  
## See Also  
 [Properties &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/properties-xmla.md)  
  
  