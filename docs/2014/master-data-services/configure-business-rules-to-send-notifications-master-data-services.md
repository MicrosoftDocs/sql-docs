---
title: "Configure Business Rules to Send Notifications (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "business rules [Master Data Services], configuring notifications"
  - "e-mail [Master Data Services], configuring business rules"
  - "notifications [Master Data Services], configuring business rules"
ms.assetid: b24f7b11-ab53-4642-999c-e17b543b3558
author: leolimsft
ms.author: lle
manager: craigg
---
# Configure Business Rules to Send Notifications (Master Data Services)
  In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], configure business rules to send notifications when you want to notify users about attribute value changes.  
  
## Prerequisites  
 To perform this procedure:  
  
-   You must have permission to access the **System Administration** and **User and Group Permissions** functional areas. If you do not have permission to the **User and Group Permissions** functional area, you cannot view the list of users and groups to send notifications to.  
  
-   You must be a model administrator. For more information, see [Administrators &#40;Master Data Services&#41;](administrators-master-data-services.md).  
  
-   A business rule that uses a validation action must already exist. For more information, see [Create and Publish a Business Rule &#40;Master Data Services&#41;](../../2014/master-data-services/create-and-publish-a-business-rule-master-data-services.md).  
  
-   The user or group that receives the notification must have at least **Read-only** permission to the attribute that fails validation. Users or groups that are explicitly or implicitly denied permission to the attribute will receive the email but will not be able to access the attribute in [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)].  
  
-   If mail is sent to a group, only members of the group that have accessed [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] will get the email.  
  
### To configure business rules to send notifications  
  
1.  In [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)], click **System Administration**.  
  
2.  From the menu bar, point to **Manage** and click **Business Rules**.  
  
3.  On the **Business Rule Maintenance** page, from the **Model** list, select a model.  
  
4.  From the **Entity** list, select an entity.  
  
5.  From the **Member Type** list, select a type of member.  
  
6.  From the **Attribute** list, select an attribute or leave the default of **All**.  
  
7.  In the grid, in the row for the business rule, double-click the **Notification** field.  
  
8.  From the sub-menu, click a user or group to send the email notification to.  
  
## Next Steps  
  
-   Apply business rules to data by following one of these procedures:  
  
    -   [Validate Specific Members against Business Rules &#40;Master Data Services&#41;](../../2014/master-data-services/validate-specific-members-against-business-rules-master-data-services.md)  
  
    -   [Validate a Version against Business Rules &#40;Master Data Services&#41;](../../2014/master-data-services/validate-a-version-against-business-rules-master-data-services.md)  
  
-   Configure the email protocol as follows:  
  
    -   [Configure Email Notifications &#40;Master Data Services&#41;](../../2014/master-data-services/configure-email-notifications-master-data-services.md)  
  
## See Also  
 [Notifications &#40;Master Data Services&#41;](../../2014/master-data-services/notifications-master-data-services.md)   
 [Configure Email Notifications &#40;Master Data Services&#41;](../../2014/master-data-services/configure-email-notifications-master-data-services.md)  
  
  
