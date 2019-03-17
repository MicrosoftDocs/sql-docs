---
title: "Backup and Restore Reporting Services SharePoint Service Applications | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: dfb4ed77-90e5-4273-b690-89a945508ed2
author: markingmyname
ms.author: maghan
manager: kfile
---
# Backup and Restore Reporting Services SharePoint Service Applications
  This topic describes how to backup and restore a [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] services application using SharePoint Central Administration or PowerShell. The topic contains:  
  
-   [Limitations and Restrictions](#bkmk_Restrictions)  
  
-   [Backup The Service application](#bkmk_backup)  
  
-   [Restore the Service Application](#bkmk_restore)  
  
##  <a name="bkmk_BeforeYouBegin"></a> Before You Begin  
  
###  <a name="bkmk_Restrictions"></a> Limitations and Restrictions  
  
> [!NOTE]  
>  [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] service applications can partially be backed up and restored using the SharePoint backup and restore functionality. **Additional steps are required** and the steps are documented in this topic. Currently the backup process **does not** backup encryption keys and credentials for unattended execution accounts (UEA) or windows authentication to the [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] database.  
  
###  <a name="bkmk_recommendations"></a> Recommendations  
  
-   Backup the encryption keys before starting the SharePoint backup. If you do not backup the encryption keys, then you will not be able to access your encrypted data, following the restore of the service application. You will need to delete your encrypted data.  
  
-   Verify if your [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] service application is using UEA or Windows authentication for database access. If it is using either, verify what the proper credentials are so you can correctly configure the service application after the restore process.  
  
-   Review the SharePoint backup log is created in the same folder as the backup file. The file is typically named **spbackup.log**  
  
##  <a name="bkmk_backup"></a> Backup The Service application  
 Complete the following steps in order:  
  
1.  Backup the encryption keys  
  
2.  Backup the Service application  
  
3.  Verify if you service application uses an UEA or Windows authentication for database access. If it does, make a note of the credentials so you can use them to configure the service application after it is restored.  
  
### Backup the Encryption Keys using Central Administration  
 For information on backing up the [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] encryption keys, see the "Encryption Keys" section of [Manage a Reporting Services SharePoint Service Application](../../2014/reporting-services/manage-a-reporting-services-sharepoint-service-application.md).  
  
###  <a name="bkmk_centraladmin"></a> Backup the Service application Using SharePoint Central Administration  
 To back up the Service Application, complete the following steps:  
  
1.  In SharePoint Central Administration Click **Perform a backup** in the **Backup and Restore** group.  
  
2.  Under the **Shared Services** node, expand **Shared Service Applications** and select your service application. It will have a type of **SQL Server Reporting Services Service Application**.  
  
3.  Click **Next**.  
  
4.  Type the path for the **Backup location:** and click **Start Backup**  
  
5.  Repeat the process above but instead of selecting the service application, expand the **Shared Services Proxies** node, and select service application proxy. It will have a type of **SQL Server Reporting Services Service Application Proxy**.  
  
 For more information, see the following topics in the SharePoint documentation:  
  
 [Back up a service application (SharePoint Foundation 2010) in the SharePoint documenttation](https://msdn.microsoft.com/library/ee748601.aspx).  
  
 [Back up a service application (SharePoint Server 2010)](https://technet.microsoft.com/library/ee428318.aspx)  
  
### Verify Execution Account and Database Authentication  
 **Execution Account:** To verify if your service application is using an execution account:  
  
1.  In SharePoint Central Administration, click **Manage Service Applications** in the **Application Management** group.  
  
2.  Click the name of your service application and then click **Manage** in the SharePoint Ribbon.  
  
3.  Click **Execution Account**.  
  
4.  If an execution account is configured, you need to know the credentials when it is time to restore the service application backup. Do not proceed with the backup and restore procedure until you know the correct credentials.  
  
 **Database Authentication:** To verify if your service application is using Windows Authentication for the database authentication:  
  
1.  In SharePoint Central Administration, click **Manage Service Applications** in the **Application Management** group.  
  
2.  Click the name of your service application and then click **Properties** in the SharePoint Ribbon.  
  
3.  Review the **Reporting Services (SSRS) Service Database** section.  
  
4.  If Windows Authentication is configured, you need to know the credentials so you can configure the service application after you restore it. Do not proceed with the backup and restore procedure until you know the correct credentials.  
  
##  <a name="bkmk_restore"></a> Restore the Service Application  
 Complete the following steps in order:  
  
1.  Restore the [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] service application.  
  
2.  Restore the encryption keys  
  
3.  If your service application was using an execution account or Windows authentication for database access, configure the credentials.  
  
### Restore the Service application Using SharePoint Central Administration  
  
1.  In SharePoint Central Administration Click **Restore from a backup** in the **Backup and Restore** group.  
  
2.  Type the path to your backup file in **Backup Directory Location** box and click **Refresh**.  
  
3.  Select your service application backup from the **Top Component** list and then click **Next**.  
  
4.  Select your [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] application and then click **Next**.  
  
5.  In the **Login Names and Passwords** section type the password for the login name. The login name box should be populated with the login the service application was using prior to the backup.  
  
6.  Click **Start Restore**.  
  
7.  Repeat the process above but instead of restoring the service application, expand the **Shared Services** node and then expand the **Shared Service Applications** node.  
  
 For more information, see the following topics in the SharePoint documentation:  
  
 [Restore a service application (SharePoint Foundation 2010)](https://msdn.microsoft.com/library/ee748615.aspx).  
  
 [Restore a service application (SharePoint Server 2010)](ttp://technet.microsoft.com/library/ee428305.aspx).  
  
### Restore the Encryption Keys using Central Administration  
 For information on restoring the [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] encryption keys, see the "Encryption Keys" section of [Manage a Reporting Services SharePoint Service Application](../../2014/reporting-services/manage-a-reporting-services-sharepoint-service-application.md).  
  
### Configure the Execution Account and Database Authentication  
 **Execution Account:** If your service application was using an execution account complete the following steps to configure it:  
  
1.  In SharePoint Central Administration, click **Manage Service Applications** in the **Application Management** group.  
  
2.  Click the name of your service application and then click **Manage** in the SharePoint Ribbon.  
  
3.  Click **Execution Account**.  
  
4.  Type the account, password, and select the **Specify and Execution Account** box.  
  
5.  Click **OK**.  
  
 **Database Authentication:** If your service application was using Windows Authentication for the database authentication complete the following steps:  
  
1.  In SharePoint Central Administration click **Manage Service Applications** in the **Application Management** group.  
  
2.  Click the name of your service application and then click **Properties** in the SharePoint Ribbon.  
  
3.  Review the **Reporting Services (SSRS) Service Database** section.  
  
4.  Select **Windows Authentication**.  
  
5.  Type the account and password. Select **Use as Windows Credentials** if appropriate.  
  
6.  Click **Ok**  
  
  
