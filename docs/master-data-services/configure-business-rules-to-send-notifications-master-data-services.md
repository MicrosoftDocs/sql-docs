---
title: "Configure Business Rules to Send Notifications (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "mds"
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

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

  In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], configure business rules to send notifications when you want to notify users about attribute value changes.  
  
## Prerequisites  
 To perform this procedure:  
  
-   You must have permission to access the **System Administration** and **User and Group Permissions** functional areas. If you do not have permission to the **User and Group Permissions** functional area, you cannot view the list of users and groups to send notifications to.  
  
-   You must be a model administrator. For more information, see [Administrators &#40;Master Data Services&#41;](../master-data-services/administrators-master-data-services.md).  
  
-   A business rule that uses a validation action must already exist. For more information, see [Create and Publish a Business Rule &#40;Master Data Services&#41;](../master-data-services/create-and-publish-a-business-rule-master-data-services.md).  
  
-   The user or group that receives the notification must have at least **Read-only** permission to the attribute that fails validation. Users or groups that are explicitly or implicitly denied permission to the attribute will receive the email but will not be able to access the attribute in [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)].  
  
-   If mail is sent to a group, only members of the group that have accessed [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] will get the email.  
  
### To configure business rules to send notifications  
  
1.  In [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)], click **System Administration**.  
  
2.  From the menu bar, point to **Manage** and click **Business Rules**.  
  
3.  On the **Business Rules** page, from the **Model** list, select a model.  
  
4.  From the **Entity** drop-down list, select an entity.  
  
5.  From the **Member Types** drop-down list, select a type of member.  
  
6.  In the grid, select the row for the business rule you want to edit and click **Edit**.  
  
7.  Select the **Send Notifications** check-box and from the drop-down list select a user or group to send the email notification to.  
  
8.  Click **Save**.  
  
9. Click **Publish All**.  
  
10. On the confirmation dialog box, click **OK**. The value in the **Business Rule State** column changed to **Active** and the **Notification** column shows the selected user or group to send notification to.  
  
## Next Steps  
  
-   Apply business rules to data by following one of these procedures:  
  
    -   [Validate Specific Members against Business Rules &#40;Master Data Services&#41;](../master-data-services/validate-specific-members-against-business-rules-master-data-services.md)  
  
    -   [Validate a Version against Business Rules &#40;Master Data Services&#41;](../master-data-services/validate-a-version-against-business-rules-master-data-services.md)  
  
-   Configure the email protocol as follows:  
  
    -   [Configure Email Notifications &#40;Master Data Services&#41;](../master-data-services/configure-email-notifications-master-data-services.md)  
  
## See Also  
 [Notifications &#40;Master Data Services&#41;](../master-data-services/notifications-master-data-services.md)   
 [Configure Email Notifications &#40;Master Data Services&#41;](../master-data-services/configure-email-notifications-master-data-services.md)  
  
  
