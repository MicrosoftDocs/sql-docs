---
title: "MemberRef Element (CSDLBI) | Microsoft Docs"
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
applies_to: 
  - "SQL Server 2016 Preview"
ms.assetid: 399aaa34-896c-48e7-aacb-18564f31b568
caps.latest.revision: 5
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# MemberRef Element (CSDLBI)
  The MemberRef element identifies the name of a property that is the target of a reference.  
  
## Elements and Attributes  
 The following table lists the elements and attributes that define the MemberRef element.  
  
|Name|Is Required|Description|  
|----------|-----------------|-----------------|  
|Name|Yes|he name of the property contained in a MemberRef element.|  
  
## MemberRefs Element  
 MemberRefs is a complex type that defines a collection of members in which each member is contained in a MemberRef element.  
  
 The following table lists the elements and attributes of the MemberRefs type.  
  
|Name|Is Required|Description|  
|----------|-----------------|-----------------|  
|MemberRef|Yes|A string containing the member reference.|  
  
## Example  
 **Tabular**  
  
 The following sample, in CSDLBI version 1.1, represents a portion of the AdventureWorks sample data model that defines the Products table. Here the MemberRef element is used for each column that is included in the default field set for the model.  
  
```  
  
<bi:EntityType>  
   <bi:DefaultDetails>  
     <bi:MemberRef Name="Color" />  
     <bi:MemberRef Name="DealerPrice" />  
     <bi:MemberRef Name="Status" />  
   </bi:DefaultDetails>  
  
```  
  
## Example  
 **Multidimensional**  
  
 The following sample, in CSDLBI version 1.1, represents a portion of the Contoso Operations cube that defines the Bikes table. In this sample, a MemberRef element is used to specify the name of the column that is used as the default field set in reports, and another MemberRef element is used to reference the column that provides the sort order.  
  
```  
  
<bi:DefaultDetails>  
   <bi:MemberRef Name="Color" />  
</bi:DefaultDetails>  
  
<bi:SortMembers>  
   <bi:MemberRef Name="Color" />  
</bi:SortMembers>  
  
```  
  
## See Also  
 [Technical Reference for BI Annotations to CSDL](../../../analysis-services/tabular-model-programming-compatibility-levels-1050-1103/conceptual-schema-definition-language-csdl/technical-reference-for-bi-annotations-to-csdl.md)  
  
  