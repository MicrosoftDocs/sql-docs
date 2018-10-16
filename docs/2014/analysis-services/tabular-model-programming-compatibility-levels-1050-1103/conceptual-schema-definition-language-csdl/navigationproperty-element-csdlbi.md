---
title: "NavigationProperty Element (CSDLBI) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
ms.assetid: a36b4d3b-6a6c-489b-8a46-2e6b925b568f
author: minewiskan
ms.author: owend
manager: craigg
---
# NavigationProperty Element (CSDLBI)
  The NavigationProperty element is a complex type that extends the CSDL Member type, to support navigation in business intelligence data models.  
  
> [!WARNING]  
>  This element is for reporting and cannot be modified or manipulated.  
  
## Elements and Attributes  
 The following table lists the elements and attributes that define the NavigationProperty element.  
  
|Name|Is Required|Description|  
|----------|-----------------|-----------------|  
|CollectionCaption|No|Plural name for referring to a set of instances of the navigation property.<br /><br /> If this attribute is omitted, the Caption attribute of the base Member is used.|  
  
## Example  
 **Tabular**  
  
 The following example shows a navigation property in CSDLBI version 1.1 that describes the link between the Product SubCategory table and the Product table in a tabular model.  
  
```  
  
<NavigationProperty   
    Name="Product_Sub_Category_ProductSubcategoryKey"      
    Relationship="Sandbox.Product_Product_Sub_Category_Product_Sub_Category_ProductSubcategoryKey"  
     FromRole="Product_ProductSubcategoryKey"   
    ToRole="Product_Sub_Category_ProductSubcategoryKey">  
<bi:NavigationProperty   
     ReferenceName="Product Sub-Category_ProductSubcategoryKey" />  
</NavigationProperty>  
```  
  
## Example  
 **Multidimensional**  
  
 The following example shows a navigation property in CSDLBI version 1.1 that describes the relationship between two tables in the Contoso Operations cube. The relationship connects the Bike Category table and the Product Subcategory table.  
  
```  
  
<NavigationProperty   
     Name="BikeSubcategory_ProductSubcategoryKey"   
     Relationship="Sandbox.Bike_BikeSubcategory_BikeSubcategory_ProductSubcategoryKey"   
     FromRole="Bike_ProductSubcategoryKey"   
     ToRole="BikeSubcategory_ProductSubcategoryKey">  
   <bi:NavigationProperty />  
</NavigationProperty>  
```  
  
## See Also  
 [Understanding the Tabular Object Model](../representation/understanding-tabular-object-model-at-levels-1050-through-1103.md)  
  
  
