---
title: "Members Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Members Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Members"
  - "urn:schemas-microsoft-com:xml-analysis#Members"
  - "microsoft.xml.analysis.members"
helpviewer_keywords: 
  - "Members element"
ms.assetid: 55f9ec3a-5a41-4b3a-acd6-c07598868c46
author: minewiskan
ms.author: owend
manager: craigg
---
# Members Element (XMLA)
  Contains a collection of [Member](member-element-xmla.md) elements contained by the parent [CrossProduct](crossproduct-element-xmla.md) element.  
  
## Syntax  
  
```xml  
  
<CrossProduct>  
   <Members Hierarchy="string">  
      <Member>...</Member>  
   </Members>  
   ...  
</CrossProduct>  
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
|Parent elements|[CrossProduct](crossproduct-element-xmla.md)|  
|Child elements|[Member](member-element-xmla.md)|  
  
## Attributes  
  
|Attribute|Description|  
|---------------|-----------------|  
|Hierarchy|Required `String` attribute. The name of the hierarchy to which the members contained by the `Members` element belong.|  
  
## Remarks  
 When a client application sets the `AxisFormat` property to *ClusterFormat*, the members on each axis are divided into clusters in which each cluster represents a cross-product between ordered sets of members from each hierarchy. Each `Axis` element consists of one or more `CrossProduct` elements. Every `CrossProduct` element contains a `Members` element for each hierarchy on the axis. The `Members` element, in turn, contains one `Member` element for each member of the specified hierarchy included in the cross-product.  
  
## Example  
 The following example illustrates the structure of the `Members` element when a client specifies *ClusterFormat* for the `AxisFormat` XMLA property, given the following members for the axis:  
  
||||||  
|-|-|-|-|-|  
|`Time` hierarchy|1999|1999|2000|2001|  
|`Category` hierarchy|Actual|Budget|Budget|Budget|  
|Clusters|Cluster 1|Cluster 1|Cluster 1|Cluster 2|  
  
```  
<Axes>  
   <Axis name="Axis0">  
      <CrossProduct Size = "4">  
         <Members Hierarchy="Time">  
            <Member>  
               <UName>[Time].[1999]</UName>  
               ...  
            </Member>  
            <Member>  
               <UName>[Time].[2000]</UName>  
               ...  
            </Member>  
         </Members>  
         <Members Hierarchy="Category">  
            <Member>  
               <UName>[Scenario].[Actual]</UName>  
               ...  
            </Member>  
            <Member>  
               <UName>[Scenario].[Budget]</UName>  
               ...  
            </Member>  
         </Members>  
      </CrossProduct>  
      <CrossProduct Size = "1">  
         <Members Hierarchy="Time">  
            <Member>  
               <UName>[Time].[2001]</UName>  
               ...  
            </Member>  
         </Members>  
         <Members Hierarchy="Category">  
            <Member>  
               <UName>[Scenario].[Budget]</UName>  
               ...  
            </Member>  
         </Members>  
      </CrossProduct>  
   </Axis>  
   ...  
</Axes>  
```  
  
## See Also  
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
