---
title: "Grant permissions to users and alerting administrators"
description: Learn how to grant permissions to users and alerting administrators in SQL Server Reporting Services (SSRS).
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: reporting-services
ms.topic: conceptual
ms.custom:
  - updatefrequency5
monikerRange: ">=sql-server-2016 <=sql-server-2016"
---
# Grant permissions to users and alerting administrators

[!INCLUDE[ssrs-appliesto](../includes/ssrs-appliesto.md)] [!INCLUDE[ssrs-appliesto-2016](../includes/ssrs-appliesto-2016.md)] [!INCLUDE[ssrs-appliesto-not-pbirsi](../includes/ssrs-appliesto-not-pbirs.md)] [!INCLUDE[ssrs-appliesto-sharepoint-2013-2016i](../includes/ssrs-appliesto-sharepoint-2013-2016.md)]

Before users and alerting administrators can create, edit, delete, and view data alerts they must be granted SharePoint permissions. There are no special permissions to use with the [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] data alerting feature. You use the built-in SharePoint permissions.

> [!NOTE]
> Reporting Services integration with SharePoint is no longer available after SQL Server 2016.

**Information workers**-Permissions must include the Create Alert and View Items SharePoint permissions. The built-in SharePoint permission levels named Design, Contribute, Read, and View Only include the Create Alert and View Items SharePoint permissions. You can also create a custom permission level with the permissions required to support users that create, edit, run, and view data alerts.

**Alerting administrators**-Permissions must include the Manage Alert SharePoint permission. By default only the Full Control permission level includes this permission for sites created with the Team Site site template. If you use other site templates, you see different lists of default SharePoint groups. You can add the Manage Alert permission to one of the built-in permission levels. You can also create a custom permission level with the permission required to support alerting administrators that view and delete data alerts.

To learn more about SharePoint permissions, see [User permissions and permission levels (SharePoint Server 2010)](/SharePoint/sites/user-permissions-and-permission-levels).

## Grant permissions
  
1.  Go to the SharePoint site to which you want to grant permissions.  
  
2.  On the toolbar, select **Site Actions** and then choose **Site Permissions**.  
  
     If you don't see this option, you don't have sufficient permission to grant permissions to others.  
  
3.  Select **Grant Permissions**.  
  
4.  In **Users/Groups**, type the user names, group names, or e-mail addresses you want grant permission to.  
  
5.  Select the **Add users to a SharePoint group** or **Grant users permission directly** option. Depending on whether you selected **Add users to a SharePoint group** or **Grant users permissions directly** do one of the following:  
  
    -   If you selected **Add users to a SharePoint group**, select a permission level in the drop-down list.  
  
    -   If you selected **Grant users permissions directly**, select a permission level.  
  
6.  Select **OK**.

## Related content

- [Set permissions for report server items on a SharePoint site &#40;Reporting Services in SharePoint Integrated Mode&#41;](../reporting-services/security/set-permissions-for-report-server-items-on-a-sharepoint-site.md)
- [Reporting Services data alerts](../reporting-services/reporting-services-data-alerts.md)
- [Try asking the Reporting Services forum](https://go.microsoft.com/fwlink/?LinkId=620231)
