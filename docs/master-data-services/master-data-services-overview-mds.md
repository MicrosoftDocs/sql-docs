---
title: Overview
description: Learn about the key data organization and management features of Master Data Services. Master Data Services enables you to manage a master set of your data.
ms.custom: ""
ms.date: "02/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: master-data-services
ms.topic: conceptual
keywords: 
  - "what is master data"
helpviewer_keywords: 
  - "Master Data Services, overview"
  - "Master Data Services"
ms.assetid: 8a4c28b1-6061-4850-80b6-132438b8c156
author: CordeliaGrey
ms.author: jiwang6
---
# Master Data Services Overview (MDS)

[!INCLUDE [SQL Server - Windows only  ](../includes/applies-to-version/sql-windows-only.md)]

This topic describes the key data organization and management features of [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)]. 
  
 [!INCLUDE[ssMDSshort_md](../includes/ssmdsshort-md.md)] enables you to manage a master set of your organization's data. You can organize the data into models, create rules for updating the data, and control who updates the data. With Excel, you can share the master data set with other people in your organization. 
  
 >  For a description of the [!INCLUDE[ssMDSshort_md](../includes/ssmdsshort-md.md)] architecture, see the [Master Data Services -- The Basics](https://www.simple-talk.com/sql/database-delivery/master-data-services-basics) article on simple-talk.com. For information about the new features  in [!INCLUDE[ssnoversion](../includes/ssnoversion-md.md)], see [What's New in Master Data Services &#40;MDS&#41;](../master-data-services/what-s-new-in-master-data-services-mds.md)  
   **For instructions on how to install [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], set up the database and Website, and deploy the sample models, see** [Master Data Services Installation and Configuration](../master-data-services/master-data-services-installation-and-configuration.md).  
  
 In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], the model is the highest level container in the structure of your master data. You create a model to manage groups of similar data, for example to manage online product data. A model contains one or more entities, and entities contain members that are the data records. An entity is similar to a table.  
  
 For example, your online product model may contain entities such as product, color, and style. The  color entity may contain  members for the colors red, silver, and black.  
  
 ![Color entity](../master-data-services/media/mds-productmodel-colorentity-composite.png "Color entity")  
  
 Models also contain attributes that are defined within entities. An attribute contains values that help describe the entity members. There are free-form attributes and domain-based attributes.  A domain-based attribute contains values that are populated by members from an entity and can be used as attribute values for other entities.  
  
 For example, the product entity might have free-form attributes for cost and weight. And, there is a domain-based attribute for color ![Number 1](../master-data-services/media/mds-number1.png "Number 1") that contains values that are populated by the color entity members. This  master list of colors is used as attribute values for the Product entity ![Number 2](../master-data-services/media/mds-number2.png "Number 2").  
  
 ![Domain-based attribute for color](../master-data-services/media/mds-productentity-color-domainattribute.png "Domain-based attribute for color")  
  
 Derived hierarchies come from the relationships between entities in a model. These are domain-based attribute relationships. In the product model for example, you can have a color derived hierarchy ![Number 1](../master-data-services/media/mds-number1.png "Number 1") that comes from the relationship between the color ![Number 2](../master-data-services/media/mds-number2.png "Number 2") and product ![Number 3](../master-data-services/media/mds-number3.png "Number 3") entities.  
  
 ![Color derived hierarchy](../master-data-services/media/mds-derivedhierarchy.png "Color derived hierarchy")  
  
 Once you've defined  a basic structure for your data, you can start adding data records (members) by using the import feature. You load data into staging tables,  validate the data using business rules, and load the data into  MDS tables.  You can also use business rules to set attribute values.  
  
 The following table outlines the key [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] tasks. Unless otherwise noted, all of the following procedures require you to be a model administrator. For more information, see [Administrators &#40;Master Data Services&#41;](../master-data-services/administrators-master-data-services.md).  
  
> [!NOTE]  
>  You might want to complete the following tasks in a test environment and use the sample data provided when you install [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)]. For more information, see [Deploying Models &#40;Master Data Services&#41;](../master-data-services/deploying-models-master-data-services.md).  
  
|Action|Details|Related Topics|  
|------------|-------------|--------------------|  
|Create a model|When you create a model, it is considered VERSION_1.|[Models &#40;Master Data Services&#41;](../master-data-services/models-master-data-services.md)<br /><br /> [Create a Model &#40;Master Data Services&#41;](../master-data-services/create-a-model-master-data-services.md)|  
|Create entities|Create as many entities as you need to contain your members.|[Entities &#40;Master Data Services&#41;](../master-data-services/entities-master-data-services.md)<br /><br /> [Create an Entity &#40;Master Data Services&#41;](../master-data-services/create-an-entity-master-data-services.md)|  
|Create entities to use as domain-based attributes|To create a domain-based attribute, first create the entity to populate the attribute value list.|[Domain-Based Attributes &#40;Master Data Services&#41;](../master-data-services/domain-based-attributes-master-data-services.md)<br /><br /> [Create a Domain-Based Attribute &#40;Master Data Services&#41;](../master-data-services/create-a-domain-based-attribute-master-data-services.md)|  
|Create attributes for your entities|Create attributes to describe members. A Name and Code attribute are automatically included in each entity and cannot be removed. You might want to create other free-form attributes to contain text, dates, numbers, or files.|[Attributes &#40;Master Data Services&#41;](../master-data-services/attributes-master-data-services.md)<br /><br /> [Create a Text Attribute &#40;Master Data Services&#41;](../master-data-services/create-a-text-attribute-master-data-services.md)<br /><br /> [Create a Numeric Attribute &#40;Master Data Services&#41;](../master-data-services/create-a-numeric-attribute-master-data-services.md)<br /><br /> [Create a Date Attribute &#40;Master Data Services&#41;](../master-data-services/create-a-date-attribute-master-data-services.md)<br /><br /> [Create a Link Attribute &#40;Master Data Services&#41;](../master-data-services/create-a-link-attribute-master-data-services.md)<br /><br /> [Create a File Attribute &#40;Master Data Services&#41;](../master-data-services/create-a-file-attribute-master-data-services.md)|  
|Create attribute groups|If you have more than four or five attributes for an entity, you might want to create attribute groups. These groups are the tabs that are displayed above the grid in **Explorer** and they help ease navigation by grouping attributes together on individual tabs.|[Attribute Groups &#40;Master Data Services&#41;](../master-data-services/attribute-groups-master-data-services.md)<br /><br /> [Create an Attribute Group &#40;Master Data Services&#41;](../master-data-services/create-an-attribute-group-master-data-services.md)|  
|Import members for your supporting entities|Import the data for your supporting entities by using the staging process. For the Product model, this might mean importing colors or sizes. You can also create members manually.<br /><br /> <br /><br /> Note: Users can create members in [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] if they have a minimum of **Update** permission to an entity's leaf model object and access to the **Explorer** functional area.|[Overview: Importing Data from Tables &#40;Master Data Services&#41;](../master-data-services/overview-importing-data-from-tables-master-data-services.md)<br /><br /> [Create a Leaf Member &#40;Master Data Services&#41;](../master-data-services/create-a-leaf-member-master-data-services.md)|  
|Create and apply business rules to ensure data quality|Create and publish business rules to ensure the accuracy of your data. You can use business rules to:<br /><br /> Set default attribute values.<br /><br /> Change attribute values.<br /><br /> Send email notifications when data doesn't pass business rule validation.|[Business Rules &#40;Master Data Services&#41;](../master-data-services/business-rules-master-data-services.md)<br /><br /> [Create and Publish a Business Rule &#40;Master Data Services&#41;](../master-data-services/create-and-publish-a-business-rule-master-data-services.md)<br /><br /> [Validate Specific Members against Business Rules (Master Data Services)](../master-data-services/validate-specific-members-against-business-rules-master-data-services.md)<br /><br /> [Configure Email Notifications &#40;Master Data Services&#41;](../master-data-services/configure-email-notifications-master-data-services.md)<br /><br /> [Configure Business Rules to Send Notifications &#40;Master Data Services&#41;](../master-data-services/configure-business-rules-to-send-notifications-master-data-services.md)|  
|Import members for your primary entities and apply business rules|Import the members for your primary entities by using the staging process. When done, validate the version, which applies business rules to all members in the model version.<br /><br /> You can then work to correct any business rule validation issues.|[Validation &#40;Master Data Services&#41;](../master-data-services/validation-master-data-services.md)<br /><br /> [Validate a Version against Business Rules &#40;Master Data Services&#41;](../master-data-services/validate-a-version-against-business-rules-master-data-services.md)<br /><br /> [Validation Stored Procedure &#40;Master Data Services&#41;](../master-data-services/validation-stored-procedure-master-data-services.md)|  
|Create derived hierarchies|Derived hierarchies can be updated as your business needs change and ensure that all members are accounted for at the appropriate level.|[Derived Hierarchies &#40;Master Data Services&#41;](../master-data-services/derived-hierarchies-master-data-services.md)<br /><br /> [Create a Derived Hierarchy &#40;Master Data Services&#41;](../master-data-services/create-a-derived-hierarchy-master-data-services.md)|  
|If needed, create explicit hierarchies|If you want to create hierarchies that are not level-based and that include members from a single entity, you can create explicit hierarchies.|[Explicit Hierarchies &#40;Master Data Services&#41;](../master-data-services/explicit-hierarchies-master-data-services.md)<br /><br /> [Create an Explicit Hierarchy &#40;Master Data Services&#41;](../master-data-services/create-an-explicit-hierarchy-master-data-services.md)|  
|If needed, create collections|If you want to view different groupings of members for reporting or analysis and do not need a complete hierarchy, create a collection.<br /><br /> <br /><br /> Note: Users can create collections in [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] if they have a minimum of **Update** permission to the collection model object and access to the **Explorer** functional area.|[Collections &#40;Master Data Services&#41;](../master-data-services/collections-master-data-services.md)<br /><br /> [Create a Collection &#40;Master Data Services&#41;](../master-data-services/create-a-collection-master-data-services.md)|  
|Create user-defined metadata|To describe your model objects, add user-defined metadata to your model. The metadata might include the owner of an object or the source the data comes from.||  
|Lock a version of your model and assign a version flag|Lock a version of your model to prevent changes to the members, except by administrators. When the version's data has validated successfully against business rules, you can commit the version, which prevents changes to members by all users.<br /><br /> Create and assign a version flag to the model. Flags help users and subscribing systems identify which version of a model to use.|[Versions &#40;Master Data Services&#41;](../master-data-services/versions-master-data-services.md)<br /><br /> [Lock a Version &#40;Master Data Services&#41;](../master-data-services/lock-a-version-master-data-services.md)<br /><br /> [Create a Version Flag &#40;Master Data Services&#41;](../master-data-services/create-a-version-flag-master-data-services.md)|  
|Create subscription views|For your subscribing systems to consume your master data, create subscription views, which create standard views in the [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] database.|[Overview: Exporting Data &#40;Master Data Services&#41;](../master-data-services/overview-exporting-data-master-data-services.md)<br /><br /> [Create a Subscription View to Export Data &#40;Master Data Services&#41;](../master-data-services/create-a-subscription-view-to-export-data-master-data-services.md)|  
|Configure user and group permissions|You cannot copy user and group permissions from a test to a production environment. However, you can use your test environment to determine the security you want to use eventually in production.|[Security &#40;Master Data Services&#41;](../master-data-services/security-master-data-services.md)<br /><br /> [Add a Group &#40;Master Data Services&#41;](../master-data-services/add-a-group-master-data-services.md)<br /><br /> [Add a User &#40;Master Data Services&#41;](../master-data-services/add-a-user-master-data-services.md)|  
  
 When ready, you can deploy your model, with or without its data, to your production environment. For more information, see [Deploying Models &#40;Master Data Services&#41;](../master-data-services/deploying-models-master-data-services.md).  
  
  

