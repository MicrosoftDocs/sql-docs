---
title: "CrossProduct Element (XMLA) | Microsoft Docs"
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
  - "CrossProduct Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine#CrossProduct"
  - "microsoft.xml.analysis.crossproduct"
  - "urn:schemas-microsoft-com:xml-analysis#CrossProduct"
helpviewer_keywords: 
  - "CrossProduct element"
ms.assetid: a9a1584e-d2dd-45db-a918-d694c20d8189
caps.latest.revision: 12
author: "jeannt"
ms.author: "jeannt"
manager: "erikre"
---
# CrossProduct Element (XMLA)
  Contains a cross-product between ordered sets of members from each hierarchy for an [Axis](../../../analysis-services/xmla/xml-elements-properties/axis-element-xmla.md) element that uses the [MDDataSet](../../../analysis-services/xmla/xml-data-types/mddataset-data-type-xmla.md) data type, returned by the [Execute](../../../analysis-services/xmla/xml-elements-methods-execute.md) method.  
  
## Syntax  
  
```xml  
  
<Axis>  
   ...  
   <CrossProduct Size="integer">  
      <Members>...</Members>  
   </CrossProduct>  
   ...  
</Axis>  
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
|Parent elements|[Axis](../../../analysis-services/xmla/xml-elements-properties/axis-element-xmla.md)|  
|Child elements|[Members](../../../analysis-services/xmla/xml-elements-properties/members-element-xmla.md)|  
  
## Attributes  
  
|Attribute|Description|  
|---------------|-----------------|  
|Size|Required **Integer** attribute. Indicates the number of tuples contained in the cross-product represented by the **CrossProduct** element.|  
  
## Remarks  
 When a client application sets the **AxisFormat** property to *ClusterFormat*, the members on each axis are divided into clusters in which each cluster represents a cross-product between ordered sets of members from each hierarchy. Each cluster is represented by a **CrossProduct** element. Every **CrossProduct** element contains a **Members** element for each hierarchy on the axis. A **CrossProduct** element can contain members from a single hierarchy.  
  
## Example  
 The following example illustrates the structure of the **CrossProduct** element when a client specifies *ClusterFormat* for the **AxisFormat** XMLA property, given the following members for the axis:  
  
||||||  
|-|-|-|-|-|  
|**Time** hierarchy|1999|1999|2000|2001|  
|**Category** hierarchy|Actual|Budget|Budget|Budget|  
|Clusters|Cluster 1|Cluster 1|Cluster 1|Cluster 2|  
  
```  
<Axes>  
   <Axis name="Axis0">  
      <CrossProduct Size="4">  
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
      <CrossProduct Size="1">  
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
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  