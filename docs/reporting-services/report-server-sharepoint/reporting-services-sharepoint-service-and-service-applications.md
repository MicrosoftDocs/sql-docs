---
title: "Reporting Services SharePoint service and service applications | Microsoft Docs"
ms.date: 09/25/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-server-sharepoint


ms.topic: conceptual
author: maggiesMSFT
ms.author: maggies
monikerRange: ">=sql-server-2016 <=sql-server-2016||=sqlallproducts-allversions"
---
# Reporting Services SharePoint service and service applications

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE[ssrs-appliesto-2016](../../includes/ssrs-appliesto-2016.md)] [!INCLUDE[ssrs-appliesto-sharepoint-2013-2016i](../../includes/ssrs-appliesto-sharepoint-2013-2016.md)] [!INCLUDE[ssrs-appliesto-not-pbirsi](../../includes/ssrs-appliesto-not-pbirs.md)]

[!INCLUDE [ssrs-previous-versions](../../includes/ssrs-previous-versions.md)]

  Reporting Services SharePoint mode is architected on the SharePoint service architecture and utilizes a SharePoint service and one to many service applications. Creating a service application makes the service available and generates the service application database. You can create multiple Reporting Services service applications but one service application is sufficient for most deployment scenarios.  

> [!NOTE]
> Reporting Services integration with SharePoint is no longer available after SQL Server 2016.
  
## Creating a Reporting Services service application

 You can use SharePoint Central Administration or PowerShell scripts to create the Reporting Services services applications. For more information on using SharePoint Central Administration, see the "Create a Reporting Services Service Application" section in [Install Reporting Services SharePoint Mode for SharePoint 2010](https://msdn.microsoft.com/47efa72e-1735-4387-8485-f8994fb08c8c). See the PowerShell section later in this topic for a sample PowerShell script for creating service applications.  
  
## Modify the associations of the service application with a proxy group

 The New page for creating a services application contains the section **Web Application Association**. The section allows you to associate your service application as you create it. Use the following steps to change the association and assign a customer configuration to the service application. The same general process can also be used to add the proxy to the default group rather than changing the association of the service application to a custom group.  
  
1.  In SharePoint Central Administration, in the Application Management, click **Configure Service Application Associations**.  
  
2.  On the Service application Associations page, change the view to **Service Applications**.  
  
3.  Find and click the name of your new Reporting Services Service application. You could also click the application proxy group name **default** to add the proxy to default group rather than completing the following steps.  
  
4.  Select **Custom** in the selection box **Edit the following group of connections**.  
  
5.  Check the box for your proxy and click **Ok**.  
  
## Edit service application properties

 You can reopen the property page of the service application to modify the properties.  
  
1.  In SharePoint Central Administration, in the Application Management group, click **Manage service applications**.  
  
2.  Select the service application by clicking the type column to select the entire row. If you click the name of the application it, the Management options page for the service opens instead of opening the properties of the service application.  
  
3.  In the Service Applications ribbon, click **Properties**.  
  
## Create a Reporting Services service application using PowerShell

 You can use PowerShell to create the Service application and proxy. The sample below assumes that you know what application pool you want to configure the service application to use.  
  
1.  Add the application pool object of your application pool name to a variable that is passed into the New action.  
  
    ```  
    $appPoolName = get-spserviceapplicationpool "<application pool name>"  
    ```  
  
2.  Create the service application with a name and application pool name you provide.  
  
    ```  
    New-SPRSServiceApplication -Name 'MyServiceApplication' -ApplicationPool $appPoolName -DatabaseName 'MyServiceApplicationDatabase' -DatabaseServer '<Server Name>'  
    ```  
  
3.  Get the new service application object, and pipe the object into the Pipe the new proxy cmdlet.  
  
    ```  
    Get-SPRSServiceApplication -name MyServiceApplication | New-SPRSServiceApplicationProxy "MyServiceApplicationProxy"  
    ```  
  
## Related tasks
  
|Task|Link|  
|----------|----------|  
|Manage the settings of your Service Application.|[Manage a Reporting Services SharePoint Service Application](../../reporting-services/report-server-sharepoint/manage-a-reporting-services-sharepoint-service-application.md)|  
|Backup and restore the service application and related components such as encryption keys and proxy.|[Backup and Restore Reporting Services SharePoint Service Applications](../../reporting-services/report-server-sharepoint/backup-and-restore-reporting-services-sharepoint-service-applications.md)|  

More questions? [Try asking the Reporting Services forum](https://go.microsoft.com/fwlink/?LinkId=620231)
