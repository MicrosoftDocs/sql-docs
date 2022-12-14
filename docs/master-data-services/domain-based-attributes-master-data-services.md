---
title: Domain-Based Attributes
description: Learn about domain-based attributes in Master Data Services, which have values populated from another entity. Users must pick a value from a list.
ms.custom: ""
ms.date: "03/15/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "domain-based attributes [Master Data Services], about domain-based attributes"
  - "domain-based attributes [Master Data Services]"
  - "attributes [Master Data Services], domain-based attributes"
ms.assetid: df6f33ff-97f6-466c-af74-9780b2247473
author: CordeliaGrey
ms.author: jiwang6
---
# Domain-Based Attributes (Master Data Services)

[!INCLUDE [SQL Server - Windows only ASDBMI  ](../includes/applies-to-version/sql-windows-only-asdbmi.md)]

  In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], a domain-based attribute is an attribute with values that are populated by members from another entity. You can think of a domain-based attribute as a constrained list; domain-based attributes prevent users from entering attribute values that are not valid. To select an attribute value, the user must pick from a list.  
  
## Domain-Based Attribute Example  
 In the following image, the Product entity has a domain-based attribute called Subcategory. The Subcategory attribute is populated by values from the Subcategory entity.  
  
 The Subcategory entity has a domain-based attribute called Category. The Category attribute is populated by values from the Category entity.  
  
 ![Domain-Based Attributes in an Entity](../master-data-services/media/mds-conc-domain-based-attribute-conceptual.gif "Domain-Based Attributes in an Entity")  
  
## Use Same Entity for Multiple Domain-Based Attributes  
 You can use the same entity as a domain-based attribute of multiple entities. For example, you can create an entity called YesNoIndicator with the members: Yes, No, and Maybe. You can create a domain-based attribute named InStock and use the YesNoIndicator entity as the source. You can also create another domain-based attribute named Approved and use the YesNoIndicator entity as a source. Any time you want users to choose from a list of the YesNoIndicator entity's members, you can use the entity as a domain-based attribute.  
  
## Domain-Based Attributes Form Derived Hierarchies  
 Domain-based attribute relationships are the basis for derived hierarchies. For more information, see [Derived Hierarchies &#40;Master Data Services&#41;](../master-data-services/derived-hierarchies-master-data-services.md).  
  
## Related Tasks  
  
|Task Description|Topic|  
|----------------------|-----------|  
|Create a new domain-based attribute that is sourced from an existing entity.|[Create a Domain-Based Attribute &#40;Master Data Services&#41;](../master-data-services/create-a-domain-based-attribute-master-data-services.md)|  
|Create a new entity.|[Create an Entity &#40;Master Data Services&#41;](../master-data-services/create-an-entity-master-data-services.md)|  
  
## Related Content  
  
-   [Derived Hierarchies &#40;Master Data Services&#41;](../master-data-services/derived-hierarchies-master-data-services.md)  
  
-   [Attributes &#40;Master Data Services&#41;](../master-data-services/attributes-master-data-services.md)  
  
-   [Entities &#40;Master Data Services&#41;](../master-data-services/entities-master-data-services.md)  
  
  
