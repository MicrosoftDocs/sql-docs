---
title: "Tuple Element (XMLA) | Microsoft Docs"
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
# Tuple Element (XMLA)
[!INCLUDE[ssas-appliesto-sqlas-aas](../../../includes/ssas-appliesto-sqlas-aas.md)]
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
  
## Element characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-n: Optional element that can occur more than once.|  
  
## Element relationships  
  
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
  
## See also
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  
