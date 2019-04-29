---
title: "Grant Permissions to Users and Alerting Administrators | Microsoft Docs"
ms.date: 08/17/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: reporting-services


ms.topic: conceptual
ms.assetid: 166808e1-ada7-48d2-bda8-8f7c017fb3aa
author: maggiesMSFT
ms.author: maggies
monikerRange: ">=sql-server-2016 <=sql-server-2016||=sqlallproducts-allversions"
---
# Grant Permissions to Users and Alerting Administrators

[!INCLUDE[ssrs-appliesto](../includes/ssrs-appliesto.md)] [!INCLUDE[ssrs-appliesto-2016](../includes/ssrs-appliesto-2016.md)] [!INCLUDE[ssrs-appliesto-not-pbirsi](../includes/ssrs-appliesto-not-pbirs.md)] [!INCLUDE[ssrs-appliesto-sharepoint-2013-2016i](../includes/ssrs-appliesto-sharepoint-2013-2016.md)]

Before users and alerting administrators can create, edit, delete, and view data alerts they must be granted SharePoint permissions. There are no special permissions to use with the [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] data alerting feature, you use the built-in SharePoint permissions.

> [!NOTE]
> Reporting Services integration with SharePoint is no longer available after SQL Server 2016.

**Information workers**-Permissions must include the Create Alert and View Items SharePoint permissions. The built-in SharePoint permission levels named Design, Contribute, Read, and View Only include the Create Alert and View Items SharePoint permissions. You can also create a custom permission level with the permissions required to support users that create, edit, run, and view data alerts.

**Alerting administrators**-Permissions must include the Manage Alert SharePoint permission. By default only the Full Control permission level includes this permission for sites created with the Team Site site template. If you use other site templates, you will see different lists of default SharePoint groups. You can add the Manage Alert permission to one of the built-in permission levels or create a custom permission level with the permission required to support alerting administrators that view and delete data alerts.

To learn more about SharePoint permissions, see [User permissions and permission levels (SharePoint Server 2010)](https://technet.microsoft.com/library/cc721640.aspx).

## Grant permissions
  
1.  Go to the SharePoint site to which you want to grant permissions.  
  
2.  On the toolbar, click **Site Actions** and then click **Site Permissions**.  
  
     If you do not see this option, you do not sufficient permission to grant permissions to others.  
  
3.  Click **Grant Permissions**.  
  
4.  In **Users/Groups**, type the user names, group names, or e-mail addresses you want grant permission to.  
  
5.  Select the **Add users to a SharePoint group** or **Grant users permission directly** option. Depending on whether you selected **Add users to a SharePoint group** or **Grant users permissions directly** do one of the following:  
  
    -   If you selected **Add users to a SharePoint group**, select a permission level in the drop-down list.  
  
    -   If you selected **Grant users permissions directly**, select a permission level.  
  
6.  [!INCLUDE[clickOK](../includes/clickok-md.md)]  

## See Also

[Set Permissions for Report Server Items on a SharePoint Site &#40;Reporting Services in SharePoint Integrated Mode&#41;](../reporting-services/security/set-permissions-for-report-server-items-on-a-sharepoint-site.md)   
[Reporting Services Data Alerts](../reporting-services/reporting-services-data-alerts.md)  

More questions? [Try asking the Reporting Services forum](https://go.microsoft.com/fwlink/?LinkId=620231)
