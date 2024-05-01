---
title: "URL examples for items on a report server - SharePoint mode"
description: View examples of how to use URLs that specify locations in a SharePoint site Web hierarchy on a report server.
author: maggiesMSFT
ms.author: maggies
ms.date: 03/14/2017
ms.service: reporting-services
ms.subservice: tools
ms.topic: conceptual
ms.custom: updatefrequency5
---
# URL examples for items on a report server - SharePoint mode
  To publish reports and related items to a SharePoint library, you can publish the content by using the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] authoring tools such as Report Designer. You can also upload the content by using SharePoint site actions.  
  
 SharePoint sites use different Web addresses than a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report server in native mode. A SharePoint site Web hierarchy includes the SharePoint Web application, a top-level site, optional subsites, and libraries. You must know how to create a URL address that specifies the SharePoint server and the location in the SharePoint site hierarchy where you want to publish a report or related items.  
  
 Items related to a report include shared data sources, subreports, drillthrough reports, and resources such as Web-based image files. A report that is published to a SharePoint library must specify these related items by their location in the SharePoint library.  
  
 Use the examples in this article to help create URLs to reports and related items in your reporting solutions.  
  
## Site hierarchy  
 When you configure a report server to run in SharePoint integrated mode, the SharePoint Web hierarchy is used to address items that are processed and managed on a report server.  
  
 The following elements of the Web hierarchy can be used to access and secure report server content. Other objects such as lists and pages aren't used to access report server content and therefore aren't described in the following table.  
  
|Object|Description|  
|------------|-----------------|  
|SharePoint Web application|A SharePoint Web application can be installed as a stand-alone server or under a farm that contains a collection of virtual servers. A Web application has a URL (for example, `http:*//servername*`) and can contain multiple sites.|  
|Site|A site is either a parent site for a Web application or a subsite.|  
|SharePoint library|A library contains documents or folders. A library or folder in a library is the only site object that can store reports, report models, shared data sources, and external images.|  
|Item|Report server items that you can reference in a URL include a report definition for a report or subreport, a report model, a shared data source, or an external image.|  
  
## URL syntax and rules  
 You can identify each report server item in a library by its fully qualified URL. The URL includes a protocol prefix, server name, site, library, file name, and file name extension for the file type.  
  
### URL for a SharePoint server  
 You must use a URL to the SharePoint server when you deploy a Report Server or Report Model project from [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] to the report server.  
  
 To find the name of the server to use, open a browser and locate the SharePoint library where you want to publish a report. The server name appears immediately after the protocol prefix, for example, `http:*//servername*`.  
  
[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] URL proxy endpoint use isn't supported. A proxy endpoint includes a port number, for example, `http:*//servername:8080/reportserver*`.  
  
### URL for a SharePoint server site or subsite  
 When you deploy a report or report data source, you must use a URL to a SharePoint site and subsite, if there's one. In the URL, the site name appears immediately after the server name, for example, `https://*servername/site*` or `https://*servername/site/subsite*`.  
  
 On a [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[offSPServ](../../includes/offspserv-md.md)] 2007 or [!INCLUDE[SPS2010](../../includes/sps2010-md.md)] Web application, the site and subsite frequently correspond to the tabs on the main site. To find the site name or subsite name, select **Home**, and then **All Site Content**. Scroll to the bottom and look for **Sites and Workspaces**. The list of sites appears in this section.  
  
### URL for a SharePoint library  
 When you deploy a report or related item to a SharePoint library, you must use a URL to the SharePoint library. The URL to use for a library differs depending on the version of SharePoint that you use.  
  
 On [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[winSPServ](../../includes/winspserv-md.md)] 3.0 or [!INCLUDE[SPF2010](../../includes/spf2010-md.md)], the library appears after the server name, for example, `https://*servername/*Shared Documents`.  
  
 On [!INCLUDE[offSPServ](../../includes/offspserv-md.md)] 2007 or [!INCLUDE[SPS2010](../../includes/sps2010-md.md)], the library appears after the site and subsite. For example, `https://*servername/site/*Documents`.  
  
 To find the path information for a new SharePoint library or for an unfamiliar site, open a browser and locate the SharePoint library where you want to publish your reports. If the library is empty, upload any file. Right-click the file and select **Properties** to open the **Properties** window. The address of the file contains the URL values that you need for a publish operation.  
  
### Fully qualified URLs for items on a SharePoint site  
 Items that are stored in a SharePoint library are always addressed through a fully qualified URL that starts with the Web application (`https://*server*`) as the root node, and concludes with the name of the file that you're referencing.  
  
 File names in the URL must include a file name extension.  
  
 You can't use relative URLs for dependent items in reports that you publish to a SharePoint site. For example, you can't use a relative URL to reference a shared data source, report model, or subreport. You must always specify the fully qualified URL to a SharePoint library for each item. There's no way to predict where a dependent file might be located. You can't predict it because there's no predefined hierarchy to the sites that you can use to parse a URL format.  
  
 When you publish or upload a report that contains dependent items, you must set the references to the dependent items after the report is published. References that worked correctly in Preview mode in Report Designer aren't guaranteed to work after the report is published. For more information, see [Publishing from an Authoring Tool to a SharePoint Library](#publishingToDocLib) in this article.  
  
### URLs for external images  
 A report definition can include an image file that is stored as an external file. You can reference that file in the report definition by setting a fully qualified URL to the image file. It can be stored on a SharePoint site or on a remote computer.  
  
> [!IMPORTANT]  
>  If the external URL is for an image on a SharePoint site, the broken image icon will appear when you preview the report in Report Builder. When you upload the report to the SharePoint site, and render the report in connected mode, the broken image icon will appear if you have only **View Items** permissions.  
  
 Regardless of the report server mode, references to an external image file in a report must be a fully qualified URL. Also, referencing an external image file typically requires that you configure the unattended report processing account.  
  
### Specify subreports and drillthrough reports  
 Subreports must reside in the same folder as the main report. You can't specify a relative folder.  
  
 To specify drillthrough reports, include the URL in an expression. For example, specify the report that is named SalesDetails as a drillthrough report. In the Action for the text box or placeholder text, set ReportName to the following expression:  
  
```  
="https://site/subsite/documentlibrary/SalesDetails.rdl"  
```  
  
### Reserved names on SharePoint sites  
 If you're creating or constructing a URL to an item that is located on a SharePoint site, know that the words **Personal** and **Sites** are both reserved names under the default site.  
  
## Examples of URLs  
 When publishing items to a SharePoint library, you must specify fully qualified URLs to the target library. A fully qualified SharePoint URL includes the SharePoint Web application, site, library, folder (optional), file, and file name extension. The following examples provide several illustrations of the syntax you should use.  
  
|Target|Example URL|  
|------------|-----------------|  
|A SharePoint server.|`https://TestServer`|  
|A SharePoint server site or subsite.|`https://TestServer/toplevelsite/subsite`|  
|The Company Sales sample report in **Shared Documents** on a [!INCLUDE[winSPServ](../../includes/winspserv-md.md)] or [!INCLUDE[SPF2010](../../includes/spf2010-md.md)] deployment.|`https://TestServer/TestSite/Shared%20Documents/Company%20Sales.rdl`|  
|The Company Sales sample report in **Documents/Doc** folder on a [!INCLUDE[offSPServ](../../includes/offspserv-md.md)] or [!INCLUDE[SPS2010](../../includes/sps2010-md.md)] instance.|`https://TestServer/TestSite/Documents/Doc/Company%20Sales.rdl`|  
|The Company Sales sample report in **Report Center** on an [!INCLUDE[offSPServ](../../includes/offspserv-md.md)] or [!INCLUDE[SPS2010](../../includes/sps2010-md.md)] instance.|`https://TestServer/TestSite/Reports/Doc/Company%20Sales.rdl`|  
  
##  <a name="publishingToDocLib"></a> Publish from an authoring tool to a SharePoint library  
 When you use a report authoring tool to publish reports and related files to a library, the files are validated before they're added. If you upload reports and related files by using the **Upload** action on a SharePoint library, no validation check occurs. You don't know whether the file is valid until you access the report by managing, editing, or running it.  
  
> [!NOTE]  
>  In order to publish reports to a SharePoint site from [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], you might need to add the SharePoint site to your list of trusted locations in the Internet Explorer browser.  
  
### Shared data sources  
 When you publish a shared data source from a report authoring tool, you set the project property **TargetDataSourceFolder**. The target data source folder must be a URL to a SharePoint library. Unlike in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] native mode, you can't specify a relative folder; relative paths aren't valid. If a folder in the Document Library path doesn't exist, one is created.  
  
 When you publish a shared data source (.rds) file to a SharePoint site, this action changes the data source file to an .rsds file name extension. The .rsds file can't be saved locally from a SharePoint site and imported into an existing [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] project. Shared data sources with file name extensions .rds and .rsds aren't interchangeable.  
  
#### Shared data sources from Report Designer  
 If you're publishing shared data sources from a Report Designer project, you can either use a URL that specifies the target library or you can leave the property blank. Unlike in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] native mode, you can't specify a relative folder; relative paths aren't valid. If a folder in the Document Library path doesn't exist, one is created. If you leave the target data source folder blank, the data source is published in the target report folder.  
  
### File names  
 File names in a URL for report items must include a file name extension. The file name extension determines the file type. When you publish report items from a report authoring tool, the file name extension is included automatically. If you upload a report item to a SharePoint library, you must include a file name extension.  
  
 If you don't specify a file name extension for items that you upload to a SharePoint site, the **rsInvalidDataSourceReference** error occurs. File names might not include characters that aren't recognized as valid file name characters by SharePoint applications. Don't include the following characters: `# % & * : < > ? / { | }`.  
  
## Differences between uploading and publishing  
 When you use Report Designer or Report Builder to publish reports and related files to a library, the system validates the files before adding them. If you upload reports and related files by using the **Upload** action on a SharePoint library, no validation check occurs. You don't know whether the file is valid until you access the report by managing, editing, or running it.  
  
## Update a published item  
 After you publish or upload an item to a SharePoint library, you should check the item out of the library before updating it. While the report is checked out to you, you're the only user who has permission to change the report. When you're finished, check it back in.  
  
 You might upload or publish a report without checking out the document first, for example, by uploading an item that has the same name as an existing item. If you do, the report server checks it out for you, adds the updated report as a new version of the existing item, and then checks the document back in.  
  
## External images as resources  
 A report server that runs in native mode supports the concept of a resource. The concept of a resource is defined as any file that is stored and secured on the report server, but the report server doesn't process it. In native mode, it can be any kind of file.  
  
 When a report server runs in SharePoint integrated mode, the concept of a resource has a narrower definition. The report server retains the concept of a resource for storing reports that reference an external image. This concept applies if the report is a snapshot or a copy that is kept for internal use.  
  
## Related content 
 [Publish a report to a SharePoint library](../../reporting-services/reports/publish-a-report-to-a-sharepoint-library.md)   
 [Publish a shared data source to a SharePoint library](../../reporting-services/reports/publish-a-shared-data-source-to-a-sharepoint-library.md)   
 [Project Property Pages dialog box](../../reporting-services/tools/project-property-pages-dialog-box.md)  
  
  
