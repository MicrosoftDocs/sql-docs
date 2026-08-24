---
title: Configure Email Notifications
description: Configure Email Notifications (Master Data Services)
author: meetdeepak
ms.author: dkhare
ms.date: 03/05/2026
ms.service: sql
ms.subservice: master-data-services
ms.topic: how-to
ms.custom:
  - build-2025
helpviewer_keywords:
  - "e-mail [Master Data Services], configuring"
  - "notifications [Master Data Services], configuring notifications"
---
# Configure Email Notifications (Master Data Services)

[!INCLUDE [SQL Server - Windows only ASDBMI](../includes/applies-to-version/sql-windows-only-asdbmi.md)]

[!INCLUDE [support-notice](includes/support-notice.md)]

  Configure notification emails when you want [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] to send email messages automatically.  
  
### To configure notifications  
  
1.  In [!INCLUDE[ssMDScfgmgr](../includes/ssmdscfgmgr-md.md)], on the **Database** page, select your [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] database.  
  
2.  In the **System Settings** section, click **Create Profile**.  
  
3.  Complete all required fields. For more information, see [Create Database Mail Profile and Account Dialog Box &#40;Master Data Services Configuration Manager&#41;](master-data-services-overview-mds.md).  
  
4.  Click **OK**.  
  
    > [!NOTE]  
    >  After you configure notifications, you cannot use [!INCLUDE[ssMDScfgmgr](../includes/ssmdscfgmgr-md.md)] to make changes. You must make changes directly in the [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] database. For more information, see [Database Mail Configuration Objects](../relational-databases/database-mail/database-mail-configuration-objects.md).  
  
## Related content

- [Notifications (Master Data Services)](notifications-master-data-services.md)
- [Configure Business Rules to Send Notifications (Master Data Services)](configure-business-rules-to-send-notifications-master-data-services.md)
- [System Settings (Master Data Services)](system-settings-master-data-services.md)
