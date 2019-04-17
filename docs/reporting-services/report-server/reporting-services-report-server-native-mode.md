---
title: "Reporting Services Report Server (Native Mode) | Microsoft Docs"
ms.date: 03/15/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-server


ms.topic: conceptual
helpviewer_keywords: 
  - "administering Reporting Services"
  - "administering [Reporting Services]"
  - "Reporting Services, administration"
ms.assetid: fa0d84e2-4c21-432c-aa7c-23517da75253
author: markingmyname
ms.author: maghan
---
# Reporting Services Report Server (Native Mode)
  A report server configured for native mode runs as an application server that provides all processing and management capability exclusively through [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]components.  
  
 You can use either [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] or Report Manager to manage [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] reports. Use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] configuration manager to manage a report server in native mode.  
  
 If the report server is configured for SharePoint mode, you must use the content management pages on the SharePoint site to manage reports, shared data sources, and other report server items.  
  
 This topic contains the following information:  
  
-   [Summary of Native Mode](#bkmk_sum)  
  
-   [Managing content](#bkmk_managecontent)  
  
-   [Securing and Managing a Resource](#bkmk_manageresources)  
  
-   [Referencing an Image Resource from a Report](#bkmk_referenceimage)  
  
##  <a name="bkmk_sum"></a> Summary of Native Mode  
 A [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] native mode installation consists of several server-side features that you need to manage and maintain. The server features include the following:  
  
-   The Report Server Web service, which runs within the Report Server service.  
  
-   The background processing applications, which handle scheduled operations and report delivery.  
  
-   The report server database.  
  
 To fully administer a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] installation, you must have the following permissions:  
  
-   Membership in the local Administrator group on the report server computer. If your installation includes server features that run on remote computers, you must have administrator permissions on those computers if you want to manage those servers over a remote connection.  
  
-   Database administrator permissions for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance that hosts the database.  
  
-   If you are installing [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] on a domain controller, you must be a domain administrator.  
  
##  <a name="bkmk_managecontent"></a> Managing content  
 In [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], content management refers to the management of reports, models, folders, resources, and shared data sources. All these items can be managed independently of each other through properties and security settings. Any item can be moved to a different location in the report server folder namespace. To manage items effectively, you need to know which tasks a content manager performs.  
  
> [!NOTE]  
>  Content management is different from report server administration. For more information about how to manage the environment in which a report server runs, see [Configuration and Administration of a Report Server &#40;Reporting Services SharePoint Mode&#41;](../../reporting-services/report-server-sharepoint/configuration-and-administration-of-a-report-server.md).  
  
 Content management includes the following tasks:  
  
-   Securing the report server site and items by applying the role-based security provided with [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].  
  
-   Structuring the report server folder hierarchy by adding, modifying, and deleting folders.  
  
-   Setting defaults and properties that apply to items managed by the report server. For example, you can set baseline maximum values that determine report history storage policies.  
  
-   Creating shared data source items that can be used in place of report-specific data source connections. A publisher or content manager can select a data source that is different from the one originally defined for a report; for example, to replace a reference to a test database with a reference to a production database.  
  
-   Creating shared schedules that can be used in place of report-specific and subscription-specific schedules, making it easier to maintain schedule information over time.  
  
-   Creating data-driven subscriptions that generate recipient lists by retrieving data from a data store.  
  
-   Balancing report-processing demands that are placed on the server by scheduling report processing and specifying which ones can be run on demand and which ones are loaded from cache.  
  
 Permission to perform management tasks are provided through two predefined roles: **System Administrator** and **Content Manager**. Effective management of report server content requires that you are assigned to both roles. For more information about these predefined roles, see [Roles and Permissions &#40;Reporting Services&#41;](../../reporting-services/security/roles-and-permissions-reporting-services.md).  
  
 Tools for managing report server content include [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] or Report Manager. [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] allows you to set defaults and enable features. Report Manager is used to grant user access to report server items and operations, view and use reports and other content types, and view and use all shared items and report distribution features.  
  
##  <a name="bkmk_manageresources"></a> Securing and Managing a Resource  
 A resource is a managed item that is stored on a report server, but is not processed by a report server. Typically, a resource provides external content to report users. Examples include an image in a .jpg file or an HTML file that describes the business rules used in a report. The JPG or HTML file is stored on the report server, but the report server passes the file directly to the browser rather than processing it first.  
  
 To add a resource to a report server, you upload or publish a file:  
  
|Operation|File type|  
|---------------|---------------|  
|Upload|All files are uploaded as resources except report definition (.rdl) and report model (.smdl) files.<br /><br /> To upload a resource, you must use Report Manager if the report server runs in native mode or an application page on a SharePoint site if the server runs in SharePoint integrated mode. For more information, see [Upload a File or Report &#40;Report Manager&#41;](../../reporting-services/reports/upload-a-file-or-report-report-manager.md) or [Upload Documents to a SharePoint Library &#40;Reporting Services in SharePoint Mode&#41;](../../reporting-services/report-server-sharepoint/upload-documents-to-a-sharepoint-library-reporting-services-in-sharepoint-mode.md).|  
|Publish|All files in a project are uploaded as resources except for .rdl, .smdl, and .rds data source files. To publish a resource, add an existing item to a project in Report Designer and then publish the project to a report server.|  
  
 All resources originate as files on a file system, which are subsequently uploaded to a report server. Except for the 4 megabyte default file size limitations imposed by ASP.NET, there are no restrictions on the kinds of files you can upload. However, when published to a report server as a resource, file types that have equivalent MIME types are more optimal than others. For example, resources that are based on HTML and JPG files will open in a browser window when the user clicks the resource, rendering the HTML as a Web page and the JPG as an image that the user can see. In contrast, resources that do not have equivalent MIME types, such as desktop application files, for example, may not be rendered in the browser window.  
  
 Whether a resource can be viewed by report users depends on the viewing capabilities of the browser. Because resources are not processed by the report server, the browser must provide the viewing capability to render a specific MIME type. If the browser cannot render the content, users who view the resource see only the general properties of the resource.  
  
 Resources exist alongside reports, shared data sources, shared schedules, and folders as named items in the report server folder hierarchy. You can search for, view, secure, and set properties on resources just as you would any item stored on a report server. To view or manage a resource, you must have the View resources or Manage resources tasks in your role assignment.  
  
##  <a name="bkmk_referenceimage"></a> Referencing an Image Resource from a Report  
 Resources can contain an image that you reference in a report. If report requirements include the use of external images, consider the following advantages to storing the image as a resource:  
  
-   Centralized storage in the report server database. If you move the report server database and its contents to another computer, the external image stays with the report. You do not have to keep track of image files that are stored on disk on different computers.  
  
-   Secured through role assignments rather than file system security. The same permissions used to view a report can be applied to the resource. In contrast, if you store the image on disk, you must ensure that either the Anonymous user account or the unattended execution account have permission to access the file.  
  
 To use an image resource in a report, add the image file to the project and publish it along with the report. Once the image is published, you can update the image reference in the report so that it points to the resource on the report server, and then republish just the report to save your changes. You can now subsequently update the image independently of the report by republishing the resource. The report uses the most current version of the image available on the report server.  
  
## See Also  
 [Configure and Administer a Report Server &#40;SSRS Native Mode&#41;](../../reporting-services/report-server/configure-and-administer-a-report-server-ssrs-native-mode.md)   
 [Troubleshoot a Reporting Services Installation](../../reporting-services/install-windows/troubleshoot-a-reporting-services-installation.md)  
  
  
