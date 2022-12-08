---
title: Members
description: In Master Data Services, members are the physical master data, such as a Road-150 bike in a Product entity or a specific customer in a Customer entity.
ms.custom: ""
ms.date: "03/17/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "leaf members [Master Data Services]"
  - "consolidated members [Master Data Services]"
  - "consolidated members [Master Data Services], about consolidated members"
  - "members [Master Data Services], about members"
  - "leaf members [Master Data Services], about leaf members"
  - "members [Master Data Services]"
ms.assetid: 0fda32b9-677d-4ba2-bb28-f76f2383a30f
author: CordeliaGrey
ms.author: jiwang6
---
# Members (Master Data Services)

[!INCLUDE [SQL Server - Windows only ASDBMI  ](../includes/applies-to-version/sql-windows-only-asdbmi.md)]

  In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], members are the physical master data. For example, a member can be a Road-150 bike in a Product entity or a specific customer in a Customer entity.  
  
## How Members Relate to Other Model Objects  
 You can think of members as rows in a table. Related members are contained in an entity, and each member is defined by attribute values.  
  
 In this example, the table represents an entity, the rows in the table represent members, and the columns in the table represent attributes. Each cell represents an attribute value for a specific member.  
  
 ![Master Data Services Entity Represented as Table](../master-data-services/media/mds-conc-entity-table.gif "Master Data Services Entity Represented as Table")  
  
## Member Types  
 There are three types of members: leaf members, consolidated members, and collection members.  
  
 Leaf members are the default members in an entity.  
  
-   In derived hierarchies, leaf members are the only type of member. Leaf members from one entity are used as parents of leaf members from another entity.  
  
-   In explicit hierarchies, leaf members are the lowest level and cannot have children.  
  
 Consolidated members exist only when explicit hierarchies and collections are enabled for the entity.  
  
-   Derived hierarchies do not contain consolidated members.  
  
-   In explicit hierarchies, consolidated members can be parents of other members within the hierarchy, or they can be children.  
  
## Use Hierarchies and Collections to Organize Members  
 Hierarchies and collections can be used to group members for reporting or analysis. For more information, see [Hierarchies &#40;Master Data Services&#41;](../master-data-services/hierarchies-master-data-services.md) and [Collections &#40;Master Data Services&#41;](../master-data-services/collections-master-data-services.md).  
  
## Member Example  
 In the following example, each member is made up of a Name, Code, Subcategory, StandardCost, ListPrice, and FilePhoto attribute value.  
  
 ![Bike Product Entity Table](../master-data-services/media/mds-conc-entity-table-w-data.gif "Bike Product Entity Table")  
  
## Related Tasks  
  
|Task Description|Topic|  
|----------------------|-----------|  
|Create a new leaf member.|[Create a Leaf Member &#40;Master Data Services&#41;](../master-data-services/create-a-leaf-member-master-data-services.md)|  
|Create a new consolidated member.|[Create a Consolidated Member &#40;Master Data Services&#41;](../master-data-services/create-a-consolidated-member-master-data-services.md)|  
|Delete an existing member or collection.|[Delete a Member or Collection &#40;Master Data Services&#41;](../master-data-services/delete-a-member-or-collection-master-data-services.md)|  
|Reactivate a deleted member or collection.|[Reactivate a Member or Collection &#40;Master Data Services&#41;](../master-data-services/reactivate-a-member-or-collection-master-data-services.md)|  
|Update the attribute values of a member.|[Change the Attribute Type &#40;MDS Add-in for Excel&#41;](../master-data-services/microsoft-excel-add-in/change-the-attribute-type-mds-add-in-for-excel.md)|  

  
## Related Content  
  
-   [Master Data Services Overview &#40;MDS&#41;](../master-data-services/master-data-services-overview-mds.md)  
  
-   [Entities &#40;Master Data Services&#41;](../master-data-services/entities-master-data-services.md)  
  
-   [Attributes &#40;Master Data Services&#41;](../master-data-services/attributes-master-data-services.md)  
  
-   [Hierarchies &#40;Master Data Services&#41;](../master-data-services/hierarchies-master-data-services.md)  
  
-   [Collections &#40;Master Data Services&#41;](../master-data-services/collections-master-data-services.md)  
  
-   [Leaf Permissions &#40;Master Data Services&#41;](../master-data-services/leaf-permissions-master-data-services.md)  
  
 
-   [Filter Operators &#40;Master Data Services&#41;](../master-data-services/filter-operators-master-data-services.md)  
  
  
