---
title: "Tuples Element (XMLA) | Microsoft Docs"
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
  - "Tuples Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Tuples"
  - "microsoft.xml.analysis.tuples"
  - "urn:schemas-microsoft-com:xml-analysis#Tuples"
helpviewer_keywords: 
  - "Tuples element"
ms.assetid: 5494bbaa-c1aa-43fa-b3e0-83befb2bccdd
caps.latest.revision: 11
author: "jeannt"
ms.author: "jeannt"
manager: "erikre"
---
# Tuples Element (XMLA)
  Contains a set of [Tuple](../../../analysis-services/xmla/xml-elements-properties/tuple-element-xmla.md) objects for an [Axis](../../../analysis-services/xmla/xml-elements-properties/axis-element-xmla.md) element that uses the [MDDataSet](../../../analysis-services/xmla/xml-data-types/mddataset-data-type-xmla.md) data type, returned by the [Execute](../../../analysis-services/xmla/xml-elements-methods-execute.md) method.  
  
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
|Parent elements|[Axis](../../../analysis-services/xmla/xml-elements-properties/axis-element-xmla.md)|  
|Child elements|[Tuple](../../../analysis-services/xmla/xml-elements-properties/tuple-element-xmla.md)|  
  
## Remarks  
 When a client application sets the **AxisFormat** property to *TupleFormat*, an axis is represented as a set of tuples. Each **Axis** element contains a **Tuples** element representing the set of tuples on that axis. Each tuple is represented by using a **Tuple** element that contains [Member](../../../analysis-services/xmla/xml-elements-properties/member-element-xmla.md) elements from every hierarchy on the axis.  
  
## Example  
 The following example illustrates the structure of the **Tuples** element when a client specifies *TupleFormat* or *CustomFormat* for the **AxisFormat** XML for Analysis (XMLA) property, given the following members for the axis:  
  
|||||  
|-|-|-|-|  
|**Time** hierarchy|1999|1999|2000|  
|**Category** hierarchy|Actual|Budget|Budget|  
  
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
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  