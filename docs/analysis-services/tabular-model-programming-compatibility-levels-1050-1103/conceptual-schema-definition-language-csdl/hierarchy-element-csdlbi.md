---
title: "Hierarchy Element (CSDLBI) | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
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
ms.assetid: 9debb638-1b28-401b-9499-8f63943863e9
caps.latest.revision: 13
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Hierarchy Element (CSDLBI)
  The Hierarchy element is a logical container for fields in a table that can be linked to each other to form a hierarchy. The Hierarchy element is derived from the CSDL Member element and has been extended to support the hierarchies created in business intelligence data models.  
  
## Elements and Attributes  
 The following table lists the elements and attributes that define the Hierarchy element  
  
|Name|Is Required|Description|  
|----------|-----------------|-----------------|  
|Documentation|No|A description of the hierarchy.|  
|Level|Yes|One or more Level elements that define the columns used in the hierarchy.<br /><br /> See [Level Element &#40;CSDLBI&#41;](../../../analysis-services/tabular-model-programming-compatibility-levels-1050-1103/conceptual-schema-definition-language-csdl/level-element-csdlbi.md).|  
  
## Remarks  
 In tabular models, hierarchies are created by specifying parent-child relationships among columns in the same table. For more information, see [Hierarchies &#40;SSAS Tabular&#41;](../../../analysis-services/tabular-models/hierarchies-ssas-tabular.md).  
  
## Example  
 **Tabular**  
  
 The following example, in CSDLBI version 1.0, shows a hierarchy in the AdventureWorks sample model that has been added to the Products table.  
  
```  
  
<bi:Hierarchy Name="Categoryy">  
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
  
 The following example, in CSDLBI version 1.1, shows a hierarchy from the Contoso Retail Operations cube.  
  
```  
  
<bi:Hierarchy Name="Product_Hierarchy" Caption="Product Hierarchy" ReferenceName="Product Hierarchy">  
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
 [Understanding the Tabular Object Model at Compatibility Levels 1050 through 1103](../../../analysis-services/tabular-model-programming-compatibility-levels-1050-1103/representation/understanding-tabular-object-model-at-levels-1050-through-1103.md)   
 [Understanding Functions for Parent-Child Hierarchies in DAX](http://msdn.microsoft.com/en-us/b11f0cff-cee4-4ae7-a5b3-ebe288fc42d3)   
 [Configure the &#40;All&#41; Level for Attribute Hierarchies](../../../analysis-services/multidimensional-models/database-dimensions-configure-the-all-level-for-attribute-hierarchies.md)  
  
  