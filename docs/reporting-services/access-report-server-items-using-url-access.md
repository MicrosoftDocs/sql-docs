---
title: "Access report server items using URL access"
description: "Learn how to access catalog items of different types in a report server database or in a SharePoint site using rs:Command=Value."
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: reporting-services
ms.topic: concept-article
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "referencing URL items for report server access"
  - "URL access [Reporting Services], report servers"
---
# Access report server items by using URL access
  This topic describes how to access catalog items of different types in a report server database or in a SharePoint site using *rs:Command*=*Value*. It is not necessary to actually add this parameter string. If you omit it, the report server evaluates the item type and selects the appropriate parameter value automatically. However, using the *rs:Command*=*Value* string in the URL improves the performance of the report server.  
  
 Note the `_vti_bin` proxy syntax in the examples below. For more information about using the proxy syntax, see [URL access parameter reference](../reporting-services/url-access-parameter-reference.md).  

> [!NOTE]
> Reporting Services integration with SharePoint is no longer available after SQL Server 2016.
  
## Access a report  
 To view a report in the browser, use the *rs:Command*=*Render* parameter. For example:  
  
 - **Native** `https://myrshost/reportserver?/Sales/YearlySalesByCategory&rs:Command=Render`  

## Access a resource  
 To access a resource, use the *rs:Command*=*GetResourceContents* parameter. If the resource is compatible with the browser, such as an image, it is opened in the browser. Otherwise, you are prompted to open or save the file or resource to disk.  
  
 **Native** `https://myrshost/reportserver?/Sales/StorePicture&rs:Command=GetResourceContents`  

## Access a data source  
 To access a data source, use the *rs:Command*=*GetDataSourceContents* parameter. If your browser supports XML, the data source definition is displayed if you are an authenticated user with **Read Contents** permission on the data source. For example:  
  
 **Native** `https://myrshost/reportserver?/Sales/AdventureWorks2022&rs:Command=GetDataSourceContents`  

```  
<DataSourceDefinition>  
   <Extension>SQL</Extension>  
   <ConnectString>Provider=SQLOLEDB.1;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=AdventureWorks2022;Data Source=MYSERVER1;</ConnectString>  
   <CredentialRetrieval>Integrated</CredentialRetrieval>  
   <WindowsCredentials>False</WindowsCredentials>  
   <ImpersonateUser>False</ImpersonateUser>  
   <Prompt />  
   <Enabled>True</Enabled>  
</DataSourceDefinition>  
```  
  
 The connection string is returned based on the **SecureConnectionLevel** setting of the report server. For more information about the **SecureConnectionLevel** setting, see [Use secure web service methods](../reporting-services/report-server-web-service/net-framework/using-secure-web-service-methods.md).  
  
## Access the contents of a folder  
 To access the contents of a folder, use the *rs:Command*=*GetChildren* parameter. A generic folder-navigation page is returned that contains links to the subfolders, reports, data sources, and resources in the requested folder. For example:  
  
 **Native** `https://myrshost/reportserver?/Sales&rs:Command=GetChildren`  

 The user interface you see is similar to the directory browsing mode used by [!INCLUDE[msCoName](../includes/msconame-md.md)] Internet Information Server (IIS). The version number, including the build number, of the report server is also displayed below the folder listing.  
  
## Related content

- [URL access &#40;SSRS&#41;](../reporting-services/url-access-ssrs.md)
- [URL access parameter reference](../reporting-services/url-access-parameter-reference.md)
