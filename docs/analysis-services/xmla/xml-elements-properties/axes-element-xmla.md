---
title: "Axes Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "Axes Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "Axes"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Axes"
  - "microsoft.xml.analysis.axes"
  - "urn:schemas-microsoft-com:xml-analysis#Axes"
helpviewer_keywords: 
  - "Axes element"
ms.assetid: 2005d06a-f8a2-4b4f-8c0d-2f7f73eb6f5c
caps.latest.revision: 21
author: "jeannt"
ms.author: "jeannt"
manager: "erikre"
---
# Axes Element (XMLA)
  Contains a collection of [Axis](../../../analysis-services/xmla/xml-elements-properties/axis-element-xmla.md) elements representing axis data contained by a [root](../../../analysis-services/xmla/xml-elements-properties/root-element-xmla.md) element that uses the [MDDataSet](../../../analysis-services/xmla/xml-data-types/mddataset-data-type-xmla.md) data type.  
  
## Syntax  
  
```xml  
  
<root xmlns="urn:schemas-microsoft-com:xml-analysis:mddataset">  
   ...  
   <Axes>  
      <Axis>...</Axis>  
   </Axes>  
   ...  
</root>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|Any|  
|Default value|None|  
|Cardinality|1-1: Required element that occurs once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[root](../../../analysis-services/xmla/xml-elements-properties/root-element-xmla.md)|  
|Child elements|[Axis](../../../analysis-services/xmla/xml-elements-properties/axis-element-xmla.md)|  
  
## Remarks  
 Under the **Axes** element, the **Axis** elements are listed in the order that they occur in the dataset, starting at zero. The **AxisFormat** XMLA property setting determines how **Axis** elements are formatted. For more information about the **AxisFormat** property, see [Supported XMLA Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/propertylist-element-supported-xmla-properties.md).  
  
 An axis represents a set of tuples, in which all tuples in the set have the same dimensionality. A set can be represented in different ways with different advantages. For example, the following set of four tuples can be represented as a collection of two-dimensional tuples or a Cartesian product of two one-dimensional sets.  
  
|1999|1999|2000|2000|  
|----------|----------|----------|----------|  
|Actual|Budget|Actual|Budget|  
  
 This set of tuples can be represented either as a collection of two-dimensional tuples:  
  
```  
{ ( 1999, Actual ), ( 1999, Budget ), ( 2000, Actual ), ( 2000, Budget ) }  
```  
  
 This set can also be represented as a Cartesian product of two one-dimensional sets:  
  
```  
{ 1999, 2000 } x { Actual, Budget }  
```  
  
 The first representation, two-dimensional tuples, is simpler for client tools to use. The second representation, a Cartesian product of one-dimensional sets, uses less space and preserves the multidimensional nature of the set.  
  
 The following table lists operations that can be used to define and characterize the structure and members of an axis.  
  
|Operation|Description|  
|---------------|-----------------|  
|Member|The smallest unit of an axis representing the member of a dimension hierarchy.|  
|Members|A collection of **Member** objects from the same dimension hierarchy.|  
|Tuple|A collection of members from different dimension hierarchies.|  
|Tuples|A collection of **Tuple** objects with the same dimensionality.|  
|Union|A union of sets.|  
|CrossJoin|A Cartesian product of sets.|  
  
 These operations translate the two-dimensional tuples and the Cartesian product of one-dimensional sets as follows.  
  
## Two-dimensional tuples  
  
```  
Tuples (  
   Tuple( Member(1999), Member(Actual) ),  
   Tuple( Member(1999), Member(Budget) ),  
   Tuple( Member(2000), Member(Actual) ),  
   Tuple( Member(2000), Member(Budget) )  
```  
  
## Cartesian product of one-dimensional sets  
  
```  
CrossProduct (  
   Members( Member(1999), Member(2000) ),  
   Members( Member(Actual), Member(Budget) )  
```  
  
 A client can use the **AxisFormat** property to request a specific representation.  
  
## See Also  
 [MDDataSet Data Type &#40;XMLA&#41;](../../../analysis-services/xmla/xml-data-types/mddataset-data-type-xmla.md)   
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  