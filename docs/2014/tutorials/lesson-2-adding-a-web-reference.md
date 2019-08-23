---
title: "Lesson 2: Adding a Web Reference | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: reporting-services-native
ms.topic: conceptual
ms.assetid: a2c2b8b8-6b13-45ca-ab3b-1582447b6807
author: VanMSFT
ms.author: vanto
manager: craigg
---
# Lesson 2: Adding a Web Reference
  Web service discovery is the process by which a client locates a Web service and obtains its service description. The process of Web service discovery in [!INCLUDE[vsprvs](../includes/vsprvs-md.md)] involves interrogating a Web site following a predetermined algorithm. The goal of the process is to locate the service description, which is an XML document that uses the Web Services Description Language (WSDL).  
  
 The service description describes what services are available and how to interact with those services. Without a service description, it is impossible to programmatically interact with a Web service.  
  
 Your application must have a means to communicate with the Web service and to locate it at run time. Adding a Web reference to your project for the Web service does this by generating a proxy class that interfaces with the Web service and provides a local representation of the Web service. For more information, see "How to: Generate an XML Web Service Proxy" in your [!INCLUDE[vsprvs](../includes/vsprvs-md.md)] documentation.  
  
### To add a Web reference  
  
1.  On the **Project** menu, click **Add Service Reference**.  
  
2.  In the **Add Service Reference** dialog box, click **Advanced**.  
  
3.  In the **Service Reference Settings** dialog box, click **Add Web Reference**.  
  
4.  In the **URL** box of the **Add Web Reference** dialog box, type the URL to obtain the service description of the Report Server Web service, such as http://localhost/reportserver/reportservice2010.asmx. Then click the **Go** button to retrieve information about the Web service.  
  
     \- or -  
  
     If the Report Server Web service exists on the local machine, click the **Web services on the local machine** link in the browser pane. Then click the link for the ReportService2010 Web service from the list provided.  
  
5.  In the **Web reference name** box, rename the Web reference to ReportService2010, which is the namespace you will use for this Web reference.  
  
6.  Click **Add Reference** to add a Web reference for the target Web service.  
  
     [!INCLUDE[vsprvs](../includes/vsprvs-md.md)] downloads the service description and generates a proxy class to interface between your application and the Report Server Web service. You will also need to add a reference to the <xref:System.Web.Services> namespace for your Web reference to work.  
  
7.  On the Project menu, click **Add Reference**.  
  
8.  In the **Add Reference** dialog box, in the **.NET** tab, select **System.Web.Services**, then click **OK**.  
  
 For more information, see [Accessing the SOAP API](../reporting-services/report-server-web-service/accessing-the-soap-api.md).  
  
## See Also  
 [Report Server Web Service](../reporting-services/report-server-web-service/report-server-web-service.md)   
 [Lesson 3: Accessing the Web Service](../../2014/tutorials/lesson-3-accessing-the-web-service.md)   
 [Accessing the Report Server Web Service Using Visual Basic or Visual C&#35; &#40;SSRS Tutorial&#41;](../../2014/tutorials/access-report-server-web-service-vb-vcsharp-ssrs-tutorial.md)  
  
  
