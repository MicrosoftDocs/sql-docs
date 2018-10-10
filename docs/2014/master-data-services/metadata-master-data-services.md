---
title: "Metadata (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "master-data-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "user-defined metadata [Master Data Services], about user-defined metadata"
  - "metadata [Master Data Services], about metadata"
  - "metadata [Master Data Services]"
  - "user-defined metadata [Master Data Services]"
ms.assetid: ac1aabe3-d8d4-4d7a-8954-50ee3c185d81
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Metadata (Master Data Services)
  In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], user-defined metadata is information that you use to describe model objects. For example, you may want to track the owners of a particular model or entity, or track the source systems that supply data to an entity.  
  
 User-defined metadata is managed by a model called **Metadata**. This model is automatically included when [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] is installed, and it is similar to all other MDS models, except that you cannot create versions of it.  
  
 After you populate the Metadata model with user-defined metadata, you can include it in subscription views, so it can be consumed by subscribing systems.  
  
## Metadata Entities  
 The Metadata model includes five entities, each of which represents a type of master data model object that supports user-defined metadata. For example, the **Model Metadata Definition** entity contains members that represent models, and the **Attribute Metadata Definition** entity has members that represent all attributes in all models.  
  
 To define metadata for a model object, you populate one of these member's attributes. For example, in the **Entity Metadata Definition** entity, you can populate the Price member's Description attribute with the text: **The product price when sold to a customer**.  
  
 The members in the Metadata model are automatically updated whenever model objects that support user-defined metadata are added or deleted.  
  
 The Metadata model cannot be versioned, have version flags added or changed, or be saved as a model deployment package. However, it has all the same other functionality available to other master data models. For example, you might implement a set of business rules on the Metadata model to enforce data policies.  
  
## Customizing Your Metadata Model  
 Each metadata definition entity includes Name, Code, and Description attributes. You may want to create additional attributes to further describe your model objects.  
  
 For example, you might create:  
  
-   A domain-based attribute named Owner, which you use to track the owner of each model object.  
  
-   A free-form attribute named Last Review Date, which you use to track the date that an object was last reviewed by the owner.  
  
-   A domain-based attribute named Sources, which you use to track and manage the source systems that interact with the [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] instance.  
  
## Related Tasks  
  
|Task Description|Topic|  
|----------------------|-----------|  
|Add metadata to a model object.|[Add Metadata &#40;Master Data Services&#41;](add-metadata-master-data-services.md)
)|  
  
## Related Content  
  
-   [Exporting Data &#40;Master Data Services&#41;](overview-exporting-data-master-data-services.md)  
  
  
