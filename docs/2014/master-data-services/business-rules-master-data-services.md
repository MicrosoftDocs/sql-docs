---
title: "Business Rules (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "business rules [Master Data Services], about business rules"
  - "business rules [Master Data Services]"
ms.assetid: a9f9e41a-2461-4845-b947-58b3a205543f
author: leolimsft
ms.author: lle
manager: craigg
---
# Business Rules (Master Data Services)
  In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], a business rule is a rule that you use to ensure the quality and accuracy of your master data. You can use a business rule to automatically update data, to send email, or to start a business process or workflow.  
  
## Create and Publish Business Rules  
 Business rules are `If/Then` statements that you create in [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)]. If an attribute value meets a specified condition, then an action is taken. Possible actions include setting a default value or changing a value. These actions can be combined with sending an email notification.  
  
 Business rules can be based on specific attribute values (for example, take action if Color=Blue), or when attribute values change (for example, take action if the value of the Color attribute changes). For more information about tracking non-specific changes, see [Change Tracking &#40;Master Data Services&#41;](change-tracking-master-data-services.md).  
  
 To use business rules you must first create and publish your rules, then apply the published rules to data. You can apply rules to subsets of data or to all data for a version by validating the version. A version cannot be committed until all attributes pass business rule validation.  
  
 If a user attempts to add an attribute value that doesn't pass business rule validation, the value can still be saved. You can review and correct validation issues, which are displayed in [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)].  
  
 When you create a model deployment package, if you want to include business rules you must include data from the version in the package.  
  
 If you create a business rule that uses the **OR** operator, you should create a separate rule for each conditional statement that can be evaluated independently. You can then exclude rules as needed, providing more flexibility and easier troubleshooting.  
  
## How Business Rules Are Applied  
 You can set priority order for rules to run in. However, before priority is taken into account, business rules are applied based on the type of action the rule takes. The order is as follows:  
  
1.  **Default Value**  
  
2.  **Change Value**  
  
3.  **Validation**  
  
4.  **External Action**  
  
 Within these groups, actions are applied in priority order, from lowest to highest. So for example, four separate rules might have **Default Value** actions. The **Default Value** action that occurs first depends on the priority order specified in the web UI.  
  
 Other important notes about applying rules:  
  
-   If a business rule is excluded or is not published with a status of **Active**, the rule is still available but is not included when business rules are applied.  
  
-   Business rules apply to the attribute values for all leaf or all consolidated members, not both.  
  
-   Business rules can be applied to any version of a model that is **Open** or **Locked**.  
  
-   Changes made to data when business rules are applied are not logged as transactions.  
  
-   A business rule cannot contain more than one **start workflow** action.  
  
## System Settings  
 There are two settings in [!INCLUDE[ssMDScfgmgr](../includes/ssmdscfgmgr-md.md)] that affect business rules. You can adjust these settings in [!INCLUDE[ssMDScfgmgr](../includes/ssmdscfgmgr-md.md)] or directly in the System Settings table. For more information, see [System Settings &#40;Master Data Services&#41;](../../2014/master-data-services/system-settings-master-data-services.md).  
  
## Related Tasks  
  
|Task Description|Topic|  
|----------------------|-----------|  
|Create and publish a new business rule.|[Create and Publish a Business Rule &#40;Master Data Services&#41;](../../2014/master-data-services/create-and-publish-a-business-rule-master-data-services.md)|  
|Add multiple conditions to a business rule.|[Add Multiple Conditions to a Business Rule &#40;Master Data Services&#41;](../../2014/master-data-services/add-multiple-conditions-to-a-business-rule-master-data-services.md)|  
|Create a business rule to require that attributes have values.|[Require Attribute Values &#40;Master Data Services&#41;](../../2014/master-data-services/require-attribute-values-master-data-services.md)|  
|Create a business rule to take an action based on changes to attribute values.|[Initiate Actions Based on Attribute Value Changes &#40;Master Data Services&#41;](../../2014/master-data-services/initiate-actions-based-on-attribute-value-changes-master-data-services.md)|  
|Change the name of an existing business rule.|[Change a Business Rule Name &#40;Master Data Services&#41;](../../2014/master-data-services/change-a-business-rule-name-master-data-services.md)|  
|Configure [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] to send notifications when business rules are applied.|[Configure Business Rules to Send Notifications &#40;Master Data Services&#41;](../../2014/master-data-services/configure-business-rules-to-send-notifications-master-data-services.md)|  
|Apply business rules to specific members.|[Validate Specific Members against Business Rules &#40;Master Data Services&#41;](../../2014/master-data-services/validate-specific-members-against-business-rules-master-data-services.md)|  
|Exclude a business rule so it is not used.|[Exclude a Business Rule &#40;Master Data Services&#41;](../../2014/master-data-services/exclude-a-business-rule-master-data-services.md)|  
|Delete an existing business rule.|[Delete a Business Rule &#40;Master Data Services&#41;](../../2014/master-data-services/delete-a-business-rule-master-data-services.md)|  
  
## Related Content  
  
-   [Master Data Services Overview](master-data-services-overview-mds.md)  
  
-   [Versions &#40;Master Data Services&#41;](../../2014/master-data-services/versions-master-data-services.md)  
  
-   [Validation &#40;Master Data Services&#41;](../../2014/master-data-services/validation-master-data-services.md)  
  
-   [Change Tracking &#40;Master Data Services&#41;](change-tracking-master-data-services.md)  
  
  
