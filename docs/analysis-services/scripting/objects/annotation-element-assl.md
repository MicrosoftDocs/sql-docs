---
title: "Annotation Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "Annotation Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "Annotation"
helpviewer_keywords: 
  - "Annotation element"
ms.assetid: 7d75291a-47b4-498a-8ba4-3d093b8513b2
caps.latest.revision: 43
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Annotation Element (ASSL)
  Contains elements that are used to extend the Analysis Services Scripting Language (ASSL) schema.  
  
## Syntax  
  
```xml  
  
<Annotations>  
   <Annotation>  
      <Name>...</Name>  
      <Visibility>...</Visibility>  
      <Value>...</Value>  
   </Annotation>  
</Annotations >  
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
|Parent elements|[Annotations](../../../analysis-services/scripting/collections/annotations-element-assl.md)|  
|Child elements|[Name](../../../analysis-services/scripting/properties/name-element-assl.md), [Value](../../../analysis-services/scripting/properties/value-element-assl.md), [Visibility](../../../analysis-services/scripting/properties/visibility-element-assl.md)|  
  
## Remarks  
 The **Annotation** element provides extensibility of the ASSL schema for all objects other than those used solely to define a complex data type. The **Value** element of the **Annotation** element can contain valid XML from any XML namespace other than ASSL, subject to the following rules:  
  
-   The XML can contain only elements.  
  
-   Each element must have a unique name. It is recommended that the value of the **Name** element of the **Annotation** element reference the target namespace.  
  
 These rules are imposed to allow the contents of the **Annotation** element to be exposed as a set of name/value pairs through other interfaces, such as Decision Support Objects (DSO).  
  
 Comments and white space within the **Annotation** element that are not enclosed within a child element cannot be preserved. In addition, all elements must be read-write; read-only elements will be ignored.  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Annotation>.  
  
## See Also  
 [Objects &#40;ASSL&#41;](../../../analysis-services/scripting/objects/objects-assl.md)  
  
  