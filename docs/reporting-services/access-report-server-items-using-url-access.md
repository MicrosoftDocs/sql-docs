---
title: "Access Report Server Items Using URL Access | Microsoft Docs"
ms.date: 03/14/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: reporting-services


ms.topic: conceptual
helpviewer_keywords: 
  - "referencing URL items for report server access"
  - "URL access [Reporting Services], report servers"
ms.assetid: a58b4ca6-129d-45e9-95c7-e9169fe5bba4
author: markingmyname
ms.author: maghan
---
# Access Report Server Items Using URL Access
  This topic describes how to access catalog items of different types in a report server data base or in a SharePoint site using *rs:Command*=*Value*. It is not necessary to actually add this parameter string. If you omit it, the report server evaluates the item type and selects the appropriate parameter value automatically. However, using the *rs:Command*=*Value* string in the URL improves the performance of the report server.  
  
 Note the `_vti_bin` proxy syntax in the examples below. For more information about using the proxy syntax, see [URL Access Parameter Reference](../reporting-services/url-access-parameter-reference.md).  
  
## Access a Report  
 To view a report in the browser, use the *rs:Command*=*Render* parameter. For example:  
  
 - **Native** `https://myrshost/reportserver?/Sales/YearlySalesByCategory&rs:Command=Render`  
 - **SharePoint** `https://myspsite/subsite/_vti_bin/reportserver?https://myspsite/subsite/Sales/YearlySalesByCategory&rs:Command=Render`  
  
> [!TIP]  
>  It is important the URL include the `_vti_bin` proxy syntax to route the request through SharePoint and the [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] HTTP proxy. The proxy adds some context to the HTTP request, context that is required to ensure proper execution of the report for SharePoint mode report servers.  
  
## Access a Resource  
 To access a resource, use the *rs:Command*=*GetResourceContents* parameter.If the resource is compatible with the browser, such as an image, it is opened in the browser. Otherwise, you are prompted to open or save the file or resource to disk.  
  
 **Native** `https://myrshost/reportserver?/Sales/StorePicture&rs:Command=GetResourceContents`  
  
 **SharePoint** `https://myspsite/subsite/_vti_bin/reportserver?https://myspsite/subsite/Sales/StorePicture.jpg&rs:Command=GetResourceContents`  
  
## Access a Data Source  
 To access a data source, use the *rs:Command*=*GetDataSourceContents* parameter. If your browser supports XML, the data source definition is displayed if you are an authenticated user with **Read Contents** permission on the data source. For example:  
  
 **Native** `https://myrshost/reportserver?/Sales/AdventureWorks2012&rs:Command=GetDataSourceContents`  
  
 **SharePoint** `https://myspsite/subsite/_vti_bin/reportserver?https://myspsite/subsite/Sales/AdventureWorks2012&rs:Command=GetDataSourceContents`  
  
 The XML structure might look similar to the following example:  
  
```  
<DataSourceDefinition>  
   <Extension>SQL</Extension>  
   <ConnectString>Provider=SQLOLEDB.1;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=AdventureWorks2012;Data Source=MYSERVER1;</ConnectString>  
   <CredentialRetrieval>Integrated</CredentialRetrieval>  
   <WindowsCredentials>False</WindowsCredentials>  
   <ImpersonateUser>False</ImpersonateUser>  
   <Prompt />  
   <Enabled>True</Enabled>  
</DataSourceDefinition>  
```  
  
 The connection string is returned based on the **SecureConnectionLevel** setting of the report server. For more information about the **SecureConnectionLevel** setting, see [Using Secure Web Service Methods](../reporting-services/report-server-web-service/net-framework/using-secure-web-service-methods.md).  
  
## Access the Contents of a Folder  
 To access the contents of a folder, use the *rs:Command*=*GetChildren* parameter. A generic folder-navigation page is returned that contains links to the subfolders, reports, data sources, and resources in the requested folder. For example:  
  
 **Native** `https://myrshost/reportserver?/Sales&rs:Command=GetChildren`  
  
 **SharePoint** `https://myspsite/subsite/_vti_bin/reportserver?https://myspsite/subsite/Sales&rs:Command=GetChildren`  
  
 The user interface you see is similar to the directory browsing mode used by [!INCLUDE[msCoName](../includes/msconame-md.md)] Internet Information Server (IIS). The version number, including the build number, of the report server is also displayed below the folder listing.  
  
## See Also  
 [URL Access &#40;SSRS&#41;](../reporting-services/url-access-ssrs.md)   
 [URL Access Parameter Reference](../reporting-services/url-access-parameter-reference.md) 
