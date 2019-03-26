---
title: "Discontinued Master Data Services Features in SQL Server 2014 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
ms.assetid: 3236cce0-cfd9-43f8-8be3-e8c8dff8f162
author: leolimsft
ms.author: lle
manager: craigg
---
# Discontinued Master Data Services Features in SQL Server 2014
  This topic describes [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] features that are no longer available in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
  
## [!INCLUDE[ssSQL14](../includes/sssql14-md.md)] Discontinued Features  
 There are no discontinued features in this release.  
  
## [!INCLUDE[ssSQL11](../includes/sssql11-md.md)] Discontinued Features  
  
### Security  
 To make assigning security easier, you can no longer assign model object permissions to the Derived Hierarchy, Explicit Hierarchy, and Attribute Group objects.  
  
-   Derived hierarchy permissions are now based on the model. For example, if you want a user to have permission to a derived hierarchy, you must assign **Update** to the model object. Then you can assign **Deny** access to any entities you don't want the user to have access to.  
  
-   Explicit hierarchy permissions are now based on the entity. For example, if the user has **Update** permissions to an Account entity, then all explicit hierarchies for the entity will be updateable.  
  
-   Attribute group permissions can no longer be assigned in the **User and Group Permissions** functional area. Instead, in the **System Administration** functional area where attribute groups are created, users and groups can be given **Update** permission to attribute groups. **Read-only** permission to attribute groups is no longer available.  
  
### Staging Process  
 You cannot use the new staging process to:  
  
-   Create or delete collections.  
  
-   Add members to or remove members from collections.  
  
-   Reactivate members and collections.  
  
 You can use the [!INCLUDE[ssKilimanjaro](../includes/sskilimanjaro-md.md)] staging process to work with collections.  
  
### Model Deployment Wizard  
 Packages that contain data can no longer be created and deployed by using the wizard in the [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] web application. A new command line utility can be used instead. For more information, see [Deploying Models &#40;Master Data Services&#41;](deploying-models-master-data-services.md).  
  
 The wizard can still be used to create and deploy packages that do not contain data.  
  
 In addition, packages can be deployed to the edition of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] they were created in only. This means that packages created in [!INCLUDE[ssKilimanjaro](../includes/sskilimanjaro-md.md)] cannot be deployed to [!INCLUDE[ssSQL11](../includes/sssql11-md.md)]. You must deploy the package to a [!INCLUDE[ssKilimanjaro](../includes/sskilimanjaro-md.md)] environment and then upgrade the database to [!INCLUDE[ssSQL11](../includes/sssql11-md.md)].  
  
### Code Generation Business Rules  
 Business rules that automatically generate values for the Code attribute are now administered differently. Previously, to generate values for the Code attribute, you used the **Default attribute to a generated value** action in the **System Administration** functional area under **Business Rules**. Now, in **System Administration**, you must edit the entity to enable automatically-generated Code values. For more information, see [Automatic Code Creation &#40;Master Data Services&#41;](automatic-code-creation-master-data-services.md).  
  
 If you have a [!INCLUDE[ssKilimanjaro](../includes/sskilimanjaro-md.md)] model deployment package that contains a rule of this type, when you upgrade the database to [!INCLUDE[ssSQL11](../includes/sssql11-md.md)], the business rule will be excluded.  
  
### Bulk Updates and Exporting  
 In the [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] web application, you can no longer update attribute values for multiple members in bulk. To do bulk updates, use the staging process or the [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)][!INCLUDE[ssMDSXLS](../includes/ssmdsxls-md.md)].  
  
 In the [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] web application, you can no longer export members to Excel. To work with members in Excel, use the [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)][!INCLUDE[ssMDSXLS](../includes/ssmdsxls-md.md)].  
  
### Transactions  
 In the **Explorer** functional area, users can no longer revert their own transactions. Previously, users could revert changes they made to data in **Explorer**. Administrators can still revert transactions for all users in the **Version Management** functional area.  
  
 Annotations are now permanent and cannot be deleted. Previously, annotations were considered transactions and could be deleted by reverting the transaction.  
  
### Web Service  
 The [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] web service is now enabled automatically, as required by Silverlight. Previously, the web service had to be enabled manually.  
  
### PowerShell Cmdlets  
 MDS no longer includes PowerShell cmdlets.  
  
## See Also  
 [Deprecated Master Data Services Features in SQL Server 2014](deprecated-master-data-services-features.md)  
  
  
