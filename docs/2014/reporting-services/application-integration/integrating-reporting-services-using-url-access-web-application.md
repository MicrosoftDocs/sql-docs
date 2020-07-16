---
title: "Using URL Access in a Web Application | Microsoft Docs"
ms.custom: ""
ms.date: "03/08/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: reporting-services
ms.topic: "reference"
helpviewer_keywords: 
  - "links [Reporting Services], URL access"
  - "URL access [Reporting Services], Web applications"
  - "POST requests"
  - "direct addressing [Reporting Services]"
  - "Web applications [Reporting Services]"
  - "hyperlinks [Reporting Services]"
ms.assetid: 39e7918c-ad2d-4ca6-b099-2dd4dbdb83dc
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Using URL Access in a Web Application
  URL access in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] is specifically designed to enable access to individual reports over a network. This type of access is best for integrating report viewing and navigation into a custom Web application. To use URL access in Web applications, you can:  
  
-   Address a URL to a specific report server from a Web site or portal.  
  
-   Use a form POST method and pass query string parameters to a report server URL using form fields.  
  
## URL Access Through Direct Addressing  
 To access a report server or report server database item using a URL, simply provide the URL address from within a Web browser or application. You can also supply parameters to the URL that can affect the appearance of the report or resource that is being accessed. A URL can target a report server through the address bar of a Web browser, or a URL can be the source of an **IFrame** that is part of a larger Web application or portal. You can include hyperlinks to reports on various Web pages of your portal, as well as target a specific frame for the report or open a new browser window in the process.  
  
 In the following example, the hyperlink targets a frame named "main", which might be different from the one that includes the hyperlink. The hyperlink might be part of Web portal.  
  
```  
<a href="http://server/reportserver?/SampleReports/Territory Sales   
Drilldown&rs:Command=Render&rc:LinkTarget=main" target="main" >  
   Click here for the Territory Sales Drilldown sample report  
</a>  
```  
  
 In the previous example, the device information setting **LinkTarget** is passed with a value of "main" in the query string of the URL. This ensures that any drillthrough hyperlinks in the report also target the frame named "main".  
  
 For more information about device information settings, see [Passing Device Information Settings to Rendering Extensions](../report-server-web-service/net-framework/passing-device-information-settings-to-rendering-extensions.md).  
  
 Note that many servers and browsers limit the number of characters allowed in a URL. In some cases, a 256-character limit is imposed. To get around this limitation, you can use POST requests using form submission.  
  
> [!NOTE]  
>  Internet Explorer has a maximum URL length of 2,083 characters. This limit applies to both POST and GET request URLs. POST, however, is not limited by the size of the URL for submitting name/value pairs as part of a form, because they are transferred in the header and not the URL.  
  
## URL Access Through a Form POST Method  
 When a user requests data from a report server using URL access, the HTTP request uses the GET method. This is equivalent to a form submission where METHOD="GET". URL requests or form submissions that use METHOD="GET" are limited by the maximum number of characters that a server or Web browser can process.  
  
 With POST requests (METHOD="POST" and input fields), the name/value pairs are transferred in the header and not the URL. Therefore, the name/value pairs of the query string are not part of the URL, thus enabling you to provide much longer and more complex parameter lists.  
  
 Using direct access, a user can see the URL for the report server, and may be able to modify the  query string or note the particular URL request and report server parameters for later use.  
  
 The following sample HTML demonstrates the use of a form that you can use to target a report server with a specific URL and pass query string parameters as part of the form's input fields.  
  
```  
<FORM id="frmRender" action="http://server/reportserver?/SampleReports/  
   Territory Sales Drilldown" method="post" target="_self">  
   <INPUT type="hidden" name="rs:Command" value="Render">   
   <INPUT type="hidden" name="rc:LinkTarget" value="main">  
   <INPUT type="hidden" name="rs:Format" value="HTML4.0">  
   <INPUT type="submit" value="Button">  
</FORM>  
```  
  
 In the previous example, if a user clicks the button on the form, the report server returns an HTML-rendered report targeted at the current frame. A comparable URL access string might look like the following:  
  
```  
http://server/reportserver?/SampleReports/Territory Sales   
Drilldown&rs:Command=Render&rc:LinkTarget=main&rs:Format=HTML4.0  
```  
  
## See Also  
 [Integrating Reporting Services into Applications](../application-integration/integrating-reporting-services-into-applications.md)   
 [Integrating Reporting Services Using URL Access](integrating-reporting-services-using-url-access.md)   
 [Using URL Access in a Windows Application](integrating-reporting-services-using-url-access-windows-application.md)   
 [URL Access &#40;SSRS&#41;](../url-access-ssrs.md)  
  
  
