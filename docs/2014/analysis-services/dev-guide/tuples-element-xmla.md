---
title: "Tuples Element (XMLA) | Microsoft Docs"
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
  - "Tuples Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Tuples"
  - "microsoft.xml.analysis.tuples"
  - "urn:schemas-microsoft-com:xml-analysis#Tuples"
helpviewer_keywords: 
  - "Tuples element"
ms.assetid: 5494bbaa-c1aa-43fa-b3e0-83befb2bccdd
caps.latest.revision: 11
author: "mgblythe"
ms.author: "mblythe"
manager: "mblythe"
---
# Tuples Element (XMLA)
  Contains a set of [Tuple](../../../2014/analysis-services/dev-guide/tuple-element-xmla.md) objects for an [Axis](../../../2014/analysis-services/dev-guide/axis-element-xmla.md) element that uses the [MDDataSet](../../../2014/analysis-services/dev-guide/mddataset-data-type-xmla.md) data type, returned by the [Execute](../../../2014/analysis-services/dev-guide/execute-method-xmla.md) method.  
  
## Syntax  
  
```xml  
  
<Axis>  
   ...  
   <Tuples>  
      <Tuple>...</Tuple>  
   </Tuples>  
   ...  
</Axis>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Axis](../../../2014/analysis-services/dev-guide/axis-element-xmla.md)|  
|Child elements|[Tuple](../../../2014/analysis-services/dev-guide/tuple-element-xmla.md)|  
  
## Remarks  
 When a client application sets the `AxisFormat` property to *TupleFormat*, an axis is represented as a set of tuples. Each `Axis` element contains a `Tuples` element representing the set of tuples on that axis. Each tuple is represented by using a `Tuple` element that contains [Member](../../../2014/analysis-services/dev-guide/member-element-xmla.md) elements from every hierarchy on the axis.  
  
## Example  
 The following example illustrates the structure of the `Tuples` element when a client specifies *TupleFormat* or *CustomFormat* for the `AxisFormat` XML for Analysis (XMLA) property, given the following members for the axis:  
  
|||||  
|-|-|-|-|  
|`Time` hierarchy|1999|1999|2000|  
|`Category` hierarchy|Actual|Budget|Budget|  
  
```  
<Axes>  
   <Axis name="Axis0">  
      <Tuples>  
         <Tuple>  
            <Member Hierarchy="Time">  
               <UName>[Time].[1999]</UName>  
               ...  
            </Member>  
            <Member Hierarchy="Category">  
               <UName>[Scenario].[Actual]</UName>  
               ...  
            </Member>  
         </Tuple>  
         <Tuple>  
            <Member Hierarchy="Time">  
               <UName>[Time].[1999]</UName>  
               ...  
            </Member>  
            <Member Hierarchy="Category">  
               <UName>[Scenario].[Budget]</UName>  
               ...  
            </Member>  
         </Tuple>  
         <Tuple>  
            <Member Hierarchy="Time">  
               <UName>[Time].[2000]</UName>  
               ...  
            </Member>  
            <Member Hierarchy="Category">  
               <UName>[Scenario].[Budget]</UName>  
               ...  
            </Member>  
         </Tuple>  
      </Tuples>  
   </Axis>  
   ...  
</Axes>  
```  
  
## See Also  
 [Properties &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/properties-xmla.md)  
  
  