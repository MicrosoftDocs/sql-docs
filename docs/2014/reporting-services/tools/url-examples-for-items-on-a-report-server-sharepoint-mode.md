---
title: "URL Examples for Published Report Items on a Report Server in SharePoint Mode (SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 54cb861a-8cec-445c-875d-599fb9bd1973
author: markingmyname
ms.author: maghan
manager: craigg
---
# URL Examples for Published Report Items on a Report Server in SharePoint Mode (SSRS)
  To publish reports and related items to a SharePoint library, you can either publish the content using the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] authoring tools such as Report Designer or you can upload the content by using SharePoint site actions.  
  
 SharePoint sites use different Web addresses than a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report server in native mode. A SharePoint site Web hierarchy includes the SharePoint Web application, a top-level site, optional subsites, and libraries. You must know how to create a URL address that specifies the SharePoint server as well as the location in the SharePoint site hierarchy where you want to publish a report or related items.  
  
 Items related to a report include shared data sources, subreports, drillthrough reports, and resources such as Web-based image files. A report that has been published to a SharePoint library must specify these related items by their location in the SharePoint library.  
  
 Use the examples in this topic to help create URLs to reports and related items in your reporting solutions.  
  
## Site Hierarchy  
 When you configure a report server to run in SharePoint integrated mode, the SharePoint Web hierarchy is used to address items that are processed and managed on a report server.  
  
 The following elements of the Web hierarchy can be used to access and secure report server content. Other objects such as lists and pages are not used to access report server content and therefore are not described in the following table.  
  
|Object|Description|  
|------------|-----------------|  
|SharePoint Web application|A SharePoint Web application can be installed as a stand-alone server or under a farm that contains a collection of virtual servers. A Web application has a URL (for example, http:*//servername*) and can contain multiple sites.|  
|Site|A site is either a parent site for a Web application or a subsite.|  
|SharePoint library|A library contains documents or folders. A library or folder in a library is the only site object that can store reports, report models, shared data sources, and external images.|  
|Item|Report server items that you can reference in a URL include a report definition for a report or subreport, a report model, a shared data source, or an external image.|  
  
## URL Syntax and Rules  
 Each report server item in a library is identified by a fully qualified URL that includes a protocol prefix, server name, site, library, file name, and file name extension for the file type.  
  
### URL for a SharePoint Server  
 You must use a URL to the SharePoint server when you deploy a Report Server or Report Model project from [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] to the report server.  
  
 To find the name of the server to use, open a browser and locate the SharePoint library where you want to publish a report. The server name appears immediately after the protocol prefix, for example, http:*//servername*.  
  
 Using the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] URL proxy endpoint is not supported. A proxy endpoint includes a port number, for example, http:*//servername:8080/reportserver*.  
  
### URL for a SharePoint Server Site or Subsite  
 When you deploy a report or report data source, you must use a URL to a SharePoint site and subsite, if there is one. In the URL, the site name appears immediately after the server name., for example, http://*servername/site* or http://*servername/site/subsite*.  
  
 On a [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[offSPServ](../../includes/offspserv-md.md)] 2007 or [!INCLUDE[SPS2010](../../includes/sps2010-md.md)] Web application, the site and subsite frequently correspond to the tabs on the main site. To find the site name or subsite name, click **Home**, and then **All Site Content**. Scroll to the bottom and look for **Sites and Workspaces**. The list of sites appears in this section.  
  
### URL for a SharePoint Library  
 When you deploy a report or related item to a SharePoint library, you must use a URL to the SharePoint library. The URL to use for a library differs depending on the version of SharePoint you are using.  
  
 On [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[winSPServ](../../includes/winspserv-md.md)] 3.0 or [!INCLUDE[SPF2010](../../includes/spf2010-md.md)], the library appears after the server name, for example, http://*servername/*Shared Documents.  
  
 On [!INCLUDE[offSPServ](../../includes/offspserv-md.md)] 2007 or [!INCLUDE[SPS2010](../../includes/sps2010-md.md)], the library appears after the site and subsite. For example, http://*servername/site/*Documents.  
  
 To find the path information for a new SharePoint library or for an unfamiliar site, open a browser and locate the SharePoint library where you want to publish your reports. If the library is empty, upload any file. Right-click the file and select **Properties** to open the **Properties** window. The address of the file contains the URL values that you need for a publish operation.  
  
### Fully qualified URLs for Items on a SharePoint Site  
 Items that are stored in a SharePoint library are always addressed through a fully qualified URL that starts with the Web application (http://*server*) as the root node, and concludes with the name of the file that you are referencing.  
  
 File names in the URL must include a file name extension.  
  
 You cannot use relative URLs for dependent items in reports that you publish to a SharePoint site. For example, you cannot use a relative URL to reference a shared data source, report model, or subreport. You must always specify the fully qualified URL to a SharePoint library for each item. There is no way to predict where a dependent file might be located as there is no predefined hierarchy to the sites that you can use to parse a URL format.  
  
 When you publish or upload a report that contains dependent items, you must set the references to the dependent items after the report is published. References that worked correctly in Preview mode in Report Designer are not guaranteed to work after the report is published. For more information, see [Publishing from an Authoring Tool to a SharePoint Library](#publishingToDocLib) in this topic.  
  
### URLs for External Images  
 A report definition can include an image file that is stored as an external file. You can reference that file in the report definition by setting a fully qualified URL to the image file. It can be stored on a SharePoint site or on a remote computer.  
  
> [!IMPORTANT]  
>  If the external URL is for an image on a SharePoint site, the broken image icon will appear when you preview the report in Report Builder. When you upload the report to the SharePoint site, and render the report in connected mode, the broken image icon will appear if you have only `View Items` permissions.  
  
 Regardless of the report server mode, references to an external image file in a report must be a fully qualified URL. Also, referencing an external image file typically requires that you configure the unattended report processing account.  
  
### Specifying Subreports and Drillthrough Reports  
 Subreports must reside in the same folder as the main report. You cannot specify a relative folder.  
  
 To specify drillthrough reports, include the URL in an expression. For example, to specify the report that is named SalesDetails as a drillthrough report, in the Action for the text box or placeholder text, set ReportName to the following expression:  
  
```  
="http://site/subsite/documentlibrary/SalesDetails.rdl"  
```  
  
### Reserved Names on SharePoint Sites  
 If you are creating or constructing a URL to an item that is located on a SharePoint site, know that the words **Personal** and **Sites** are both reserved names under the default site.  
  
## Examples of URLs  
 When publishing items to a SharePoint library, you must specify fully qualified URLs to the target library. A fully qualified SharePoint URL includes the SharePoint Web application, site, library, folder (optional), file, and file name extension. The following examples provide several illustrations of the syntax you should use.  
  
|Target|Example URL|  
|------------|-----------------|  
|A SharePoint server.|http://TestServer|  
|A SharePoint server site or subsite.|http://TestServer/toplevelsite/subsite|  
|The Company Sales sample report in **Shared Documents** on a [!INCLUDE[winSPServ](../../includes/winspserv-md.md)] or [!INCLUDE[SPF2010](../../includes/spf2010-md.md)] deployment.|http://TestServer/TestSite/Shared%20Documents/Company%20Sales.rdl|  
|The Company Sales sample report in **Documents/Doc** folder on a [!INCLUDE[offSPServ](../../includes/offspserv-md.md)] or [!INCLUDE[SPS2010](../../includes/sps2010-md.md)] instance.|http://TestServer/TestSite/Documents/Doc/Company%20Sales.rdl|  
|The Company Sales sample report in **Report Center** on an [!INCLUDE[offSPServ](../../includes/offspserv-md.md)] or [!INCLUDE[SPS2010](../../includes/sps2010-md.md)] instance.|http://TestServer/TestSite/Reports/Doc/Company%20Sales.rdl|  
  
##  <a name="publishingToDocLib"></a> Publishing from an Authoring Tool to a SharePoint Library  
 When you use a report authoring tool to publish reports and related files to a library, the files are validated before they are added. If you upload reports and related files by using the **Upload** action on a SharePoint library, no validation check occurs. You will not know whether the file is valid until you access the report by managing, editing, or running it.  
  
> [!NOTE]  
>  In order to publish reports to a SharePoint site from [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], you might need to add the SharePoint site to your list of trusted locations in the Internet Explorer browser.  
  
### Shared Data Sources  
 When you publish a shared data source from a report authoring tool, you set the project property `TargetDataSourceFolder`. The target data source folder must be a URL to a SharePoint library. Unlike in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] native mode, you cannot specify a relative folder; relative paths are not valid. If a folder in the Document Library path does not exist, one will be created.  
  
 When you publish a shared data source (.rds) file to a SharePoint site, this changes the data source file to an .rsds file name extension. The .rsds file cannot be saved locally from a SharePoint site and imported into an existing [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] project. Shared data sources with file name extensions .rds and .rsds are not interchangeable.  
  
#### Shared Data Sources from Report Designer  
 If you are publishing shared data sources from a Report Designer project, you can either use a URL that specifies the target library or you can leave the property blank. Unlike in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] native mode, you cannot specify a relative folder; relative paths are not valid. If a folder in the Document Library path does not exist, one will be created. If you leave the target data source folder blank, the data source will be published in the target report folder.  
  
### File Names  
 File names in a URL for report items must include a file name extension. The file name extension determines the file type. When you publish report items from a report authoring tool, the file name extension is included automatically. If you upload a report item to a SharePoint library, you must include a file name extension.  
  
 If you do not specify a file name extension for items that you upload to a SharePoint site, the `rsInvalidDataSourceReference` error will occur. File names may not include characters that are not recognized as valid file name characters by SharePoint applications. Do not include the following characters: # % & * : \< > ? / { | }.  
  
## Differences Between Uploading and Publishing  
 When you use Report Designer or Report Builder to publish reports and related files to a library, the files are validated before they are added. If you upload reports and related files by using the **Upload** action on a SharePoint library, no validation check occurs. You will not know whether the file is valid until you access the report by managing, editing, or running it.  
  
## Updating a Published Item  
 After you have published or uploaded an item to a SharePoint library, you should check the item out of the library before updating it. While the report is checked out to you, you will be the only user who has permission to change the report. When you are finished, check it back in.  
  
 If you upload or publish a report without checking the document out first (for example, by uploading an item that has the same name as an existing item), the report server will check it out for you, add the updated report as a new version of the existing item, and then check the document back in.  
  
## External Images as Resources  
 A report server that runs in native mode supports the concept of a resource, which is defined as any file that is stored and secured on the report server, but is not processed by the report server. In native mode, it can be any kind of file.  
  
 When a report server runs in SharePoint integrated mode, the concept of a resource has a narrower definition. The report server retains the concept of a resource for storing reports that reference an external image. This applies if the report is a snapshot or a copy that is kept for internal use.  
  
## See Also  
 [Publish a Report to a SharePoint Library](../reports/publish-a-report-to-a-sharepoint-library.md)   
 [Publish a Shared Data Source to a SharePoint Library](../reports/publish-a-shared-data-source-to-a-sharepoint-library.md)   
 [Project Property Pages Dialog Box](project-property-pages-dialog-box.md)  
  
  
