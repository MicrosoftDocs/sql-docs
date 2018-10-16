---
title: "Level Element (CSDLBI) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
ms.assetid: fdf75c47-77dc-4bdb-9931-8eca198fdb88
author: minewiskan
ms.author: owend
manager: craigg
---
# Level Element (CSDLBI)
  The Level element is a complex type that defines a single level in a hierarchy  
  
## Elements and Attributes  
 The following table lists the elements and attributes that define the Level element.  
  
|Name|Is Required|Description|  
|----------|-----------------|-----------------|  
|Source|Yes|A container for the property reference.|  
|PropertyRef|Yes|A reference to an instance property. Other attributes of the level, such as captions, name, and reference name, can be taken from the referenced instance property. If so, you do not need to specify these in the Level element.|  
  
## Remarks  
 For more information about hierarchies in tabular models, see [Hierarchy Element &#40;CSDLBI&#41;](hierarchy-element-csdlbi.md).  
  
## Example  
 **Tabular**  
  
 The following example, in CSDLBI version 1.1, shows the definition of multiple levels in a hierarchy from the AdventureWorks tabular model sample.  
  
```  
  
<bi:Hierarchy Name="Category">  
   <bi:Level Name="CategoryName">  
     <bi:Source>  
       <bi:PropertyRef Name="CategoryName" />  
     </bi:Source>  
   </bi:Level>  
   <bi:Level Name="ProductName">  
     <bi:Source>  
       <bi:PropertyRef Name="ProductName" />  
     </bi:Source>  
   </bi:Level>  
</bi:Hierarchy>  
```  
  
## Example  
 **Multidimensional**  
  
 The following example, in CSDLBI version 1.1, shows a hierarchy with multiple levels, from the Contoso Operations cube.  
  
```  
<bi:Hierarchy   
   Name="Product_Hierarchy"   
   Caption="Product Hierarchy"   
   ReferenceName="Product Hierarchy">  
     <bi:Documentation>  
        <bi:Summary>DESCRIPTION_ProductModelCateg_Hierarchies</bi:Summary>  
     </bi:Documentation>  
  
     <bi:Level Name="ProductLine">  
        <bi:Source>  
        <bi:PropertyRef Name="ProductLine" />  
        </bi:Source>  
     </bi:Level>  
  
     <bi:Level Name="ModelName">  
        <bi:Source>  
        <bi:PropertyRef Name="ModelName" />  
        </bi:Source>  
     </bi:Level>  
</bi:Hierarchy>  
```  
  
## See Also  
 [Understanding the Tabular Object Model](../representation/understanding-tabular-object-model-at-levels-1050-through-1103.md)   
 [Understanding Functions for Parent-Child Hierarchies in DAX](https://msdn.microsoft.com/library/gg492192(v=sql.120).aspx)   
 [Configure the &#40;All&#41; Level for Attribute Hierarchies](../../multidimensional-models/database-dimensions-configure-the-all-level-for-attribute-hierarchies.md)  
  
  
