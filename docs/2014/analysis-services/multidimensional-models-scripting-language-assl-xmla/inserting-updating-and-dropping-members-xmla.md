---
title: "Inserting, Updating, and Dropping Members (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: "analysis-services"
ms.topic: "reference"
helpviewer_keywords: 
  - "inserting dimension members"
  - "XML for Analysis, members"
  - "removing dimension members"
  - "dropping dimension members"
  - "write-enabled dimensions [Analysis Services]"
  - "XMLA, members"
  - "deleting dimension members"
  - "dimensions [Analysis Services], XML for Analysis"
ms.assetid: bba922b5-8b88-4051-9506-ff055248182a
author: minewiskan
ms.author: owend
manager: craigg
---
# Inserting, Updating, and Dropping Members (XMLA)
  You can use the [Insert](https://docs.microsoft.com/bi-reference/xmla/xml-elements-commands/insert-element-xmla), [Update](https://docs.microsoft.com/bi-reference/xmla/xml-elements-commands/update-element-xmla), and [Drop](https://docs.microsoft.com/bi-reference/xmla/xml-elements-commands/drop-element-xmla) commands in XML for Analysis (XMLA) to respectively insert, update, or delete members from a write-enabled dimension. For more information about write-enabled dimensions, see [Write-Enabled Dimensions](../multidimensional-models-olap-logical-dimension-objects/write-enabled-dimensions.md).  
  
## Inserting New Members  
 The `Insert` command inserts new members into specified attributes in a write-enabled dimension.  
  
 Before constructing the `Insert` command, you should have the following information available for the new members to be inserted:  
  
-   The dimension in which to insert the new members.  
  
-   The dimension attribute in which to insert the new members.  
  
-   The names of the new members, including any applicable translations for the name.  
  
-   The keys of the new members. If an attribute uses a composite key, the key may require multiple values.  
  
-   Values for any applicable attribute properties that are not implemented as other attributes within the dimension. Such attribute properties include unary operations, translations, custom rollups, custom rollup properties, and skipped levels.  
  
 The `Insert` command takes only two properties:  
  
-   The [Object](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/object-element-xmla) property, which contains an object reference for the dimension in which the members are to be inserted. The object reference contains the database identifier, cube identifier, and dimension identifier for the dimension.  
  
-   The [Attributes](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/attributes-element-xmla) property, which contains one or more [Attribute](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/attribute-element-xmla) elements to identify the attributes in which members are to be inserted. Each `Attribute` element identifies an attribute and provides the name, value, translations, unary operator, custom rollup, custom rollup properties, and skipped levels for a single member to be added to the identified attribute.  
  
    > [!NOTE]  
    >  All properties for the `Attribute` element must be included. Otherwise, an error may occur.  
  
## Updating Existing Members  
 The `Update` command updates existing members in specified attributes, based on relationships with other members in other attributes, in a write-enabled dimension. The `Update` command can move members to other levels in hierarchies contained by the dimension, and can be used to restructure parent-child hierarchies defined by parent attributes.  
  
 Before constructing the `Update` command, you should have the following information available for the members to be updated:  
  
-   The dimension in which to update existing members.  
  
-   The dimension attributes in which to update existing members.  
  
-   The keys of the existing members. If an attribute uses a composite key, the key may require multiple values.  
  
-   Values for any applicable attribute properties that are not implemented as other attributes within the dimension. Such attribute properties include unary operations, translations, custom rollups, custom rollup properties, and skipped levels.  
  
 The `Update` command takes only three required properties:  
  
-   The `Object` property, which contains an object reference for the dimension in which the members are to be updated. The object reference contains the database identifier, cube identifier, and dimension identifier for the dimension.  
  
-   The `Attributes` property, which contains one or more `Attribute` elements to identify the attributes in which members are to be updated. The `Attribute` element identifies an attribute and provides the name, value, translations, unary operator, custom rollup, custom rollup properties, and skipped levels for a single member updated for the identified attribute.  
  
    > [!NOTE]  
    >  All properties for the `Attribute` element must be included. Otherwise, an error may occur.  
  
-   The [Where](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/where-element-xmla) property, which contains one or more `Attribute` elements that constrain the attributes in which members are to be updated. The `Where` property is crucial to limiting an `Update` command to specific instances of a member. If the `Where` property is not specified, all instances of a given member are updated. For example, there are three customers for whom you want to change the city name from Redmond to Bellevue. To change the city name, you must provide a `Where` property that identifies the three members in the Customer attribute for which the members in the City attribute should be changed. If you do not provide this `Where` property, every customer whose city name is currently Redmond would have the city name of Bellevue after the `Update` command runs.  
  
    > [!NOTE]  
    >  With the exception of new members, the `Update` command can only update attribute key values for attributes not included in the `Where` clause. For example, the city name cannot be updated when a customer is updated; otherwise, the city name is changed for all customers.  
  
### Updating Members in Parent Attributes  
 To support parent attributes, the `Update` command the optional [MoveWithDescendants](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/movewithdescendants-element-xmla)MovewithDescedants properties. Setting the `MoveWithDescendants` property to true indicates that the descendants of the parent member should also be moved with the parent member when the identifier of that parent member changes. If this value is set to false, moving a parent member causes the immediate descendants of that parent member to be promoted to the level in which the parent member formerly resided.  
  
 When updating members in a parent attribute, the `Update` command cannot update members in other attributes.  
  
## Dropping Existing Members  
 Before constructing the `Drop` command, you should have the following information available for the members to be dropped:  
  
-   The dimension in which to drop existing members.  
  
-   The dimension attributes in which to drop existing members.  
  
-   The keys of the existing members to be dropped. If an attribute uses a composite key, the key may require multiple values.  
  
 The `Drop` command takes only two required properties:  
  
-   The `Object` property, which contains an object reference for the dimension in which the members are to be dropped. The object reference contains the database identifier, cube identifier, and dimension identifier for the dimension.  
  
-   The `Where` property, which contains one or more `Attribute` elements to constrain the attributes in which members are to be deleted. The `Where` property is crucial to limiting a `Drop` command to specific instances of a member. If the `Where` command is not specified, all instances of a given member are dropped. For example, there are three customers that you want to drop from Redmond. To drop these customers, you must provide a `Where` property that identifies the three members in the Customer attribute to be removed and the Redmond member of the City attribute from which the three customers are to be removed. If the `Where` property only specifies the Redmond member of the City attribute, every customer associated with Redmond would be dropped by the `Drop` command. If the `Where` property only specifies the three members in the Customer attribute, the three customers would be deleted entirely by the `Drop` command.  
  
    > [!NOTE]  
    >  The `Attribute` elements included in a `Drop` command must contain only the `AttributeName` and `Keys` properties. Otherwise, an error may occur.  
  
### Dropping Members in Parent Attributes  
 Setting the [DeleteWithDescendants](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/deletewithdescendants-element-xmla) property indicates that the descendants of a parent member should also be deleted with the parent member. If this value is set to false, the immediate descendants of the parent member are instead promoted to the level in which the parent member formerly resided.  
  
> [!IMPORTANT]  
>  A user needs only to have delete permissions for the parent member to delete both the parent member and its descendants. A user does not need delete permissions on the descendants.  
  
## See Also  
 [Drop Element &#40;XMLA&#41;](https://docs.microsoft.com/bi-reference/xmla/xml-elements-commands/drop-element-xmla)   
 [Insert Element &#40;XMLA&#41;](https://docs.microsoft.com/bi-reference/xmla/xml-elements-commands/insert-element-xmla)   
 [Update Element &#40;XMLA&#41;](https://docs.microsoft.com/bi-reference/xmla/xml-elements-commands/update-element-xmla)   
 [Defining and Identifying Objects &#40;XMLA&#41;](https://docs.microsoft.com/bi-reference/xmla/xml-elements-objects)   
 [Developing with XMLA in Analysis Services](developing-with-xmla-in-analysis-services.md)  
  
  
