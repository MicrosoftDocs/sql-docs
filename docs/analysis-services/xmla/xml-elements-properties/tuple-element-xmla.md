---
title: "Tuple Element (XMLA) | Microsoft Docs"
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
  - "Tuple Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "microsoft.xml.analysis.tuple"
  - "urn:schemas-microsoft-com:xml-analysis#Tuple"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Tuple"
helpviewer_keywords: 
  - "Tuple element"
ms.assetid: d65aba10-55e1-49c1-81bc-0756c39c0da2
caps.latest.revision: 11
author: "jeannt"
ms.author: "jeannt"
manager: "erikre"
---
# Tuple Element (XMLA)
  Contains a collection of [Member](../../../analysis-services/xmla/xml-elements-properties/member-element-xmla.md) elements contained by the parent [Tuples](../../../analysis-services/xmla/xml-elements-properties/tuples-element-xmla.md) element.  
  
## Syntax  
  
```xml  
  
<Tuples>  
   <Tuple>  
      <Member>...</Member>  
   </Tuple>  
   ...  
</Tuples>  
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
|Parent elements|[Tuples](../../../analysis-services/xmla/xml-elements-properties/tuples-element-xmla.md)|  
|Child elements|[Member](../../../analysis-services/xmla/xml-elements-properties/member-element-xmla.md)|  
  
## Remarks  
 When a client application sets the **AxisFormat** property to *TupleFormat*, an axis is represented as a set of tuples. Each **Axis** element contains a **Tuples** element representing the set of tuples on that axis. Each tuple is represented by using a **Tuple** element that contains **Member** elements from every hierarchy on the axis.  
  
## Example  
 The following example illustrates the structure of the **Tuple** element when a client specifies *TupleFormat* or *CustomFormat* for the **AxisFormat** XMLA property, given the following members for the axis:  
  
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
  
  