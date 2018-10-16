---
title: "EntitySet Element (CSDLBI) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
ms.assetid: d4703c9e-5594-472e-a85b-0f5bd0d73d6f
author: minewiskan
ms.author: owend
manager: craigg
---
# EntitySet Element (CSDLBI)
  The EntitySet element defines a collection of entities of a particular type in a CSDLBI data model.  
  
 The EntitySet must specify each of the entity types that are included in the data model. Information about these model entities is specified by listing child entities of the type, Entity element. For more information, see [EntityType Element &#40;CSDLBI&#41;](entitytype-element-csdlbi.md).  
  
 Properties such as collation and language are defined at the level of the EntityContainer, not on individual objects. However, columns and text attributes within the model can have captions or translations in other languages.  
  
## Elements and Attributes  
 The table below lists the elements and attributes that define an EntitySet.  
  
|Attribute Name|Is Required|Description|  
|--------------------|-----------------|-----------------|  
|Caption|No|A user-friendly description of the entity set.|  
|CollectionCaption|No|A string that contains the plural name of the entity.|  
|ReferenceName|No|Contains the unmerged and fully qualified name of the entity. In a multidimensional model, this corresponds to the CubeDimension name..|  
|Hidden|No|Indicates whether the entity is hidden. By default, entities are not hidden.|  
  
## Example  
 **Tabular**  
  
 The following sample, in CSDLBI version 1.1, shows the definitions of the Date and Geography tables, from the AdventureWorks tabular model.  
  
```  
  
<EntitySet   
   Name="Date"   
   EntityType="Sandbox. Date">  
<bi:EntitySet />  
</EntitySet>  
  
<EntitySet   
   Name="Geography"   
   EntityType="Sandbox.Geography">  
<bi:EntitySet />  
</EntitySet>  
```  
  
## Example  
 **Multidimensional**  
  
 The following sample, in CSDLBI version 1.1 shows several EntitySet elements from the Contoso Retail Operations cube.  
  
```  
  
<EntitySet Name="Outage" EntityType="Sandbox.Outage">  
       <bi:EntitySet />  
</EntitySet>  
  
<EntitySet Name="Store" EntityType="Sandbox.Store">  
     <bi:EntitySet />  
</EntitySet>  
```  
  
## See Also  
 [Technical Reference for BI Annotations to CSDL](technical-reference-for-bi-annotations-to-csdl.md)  
  
  
