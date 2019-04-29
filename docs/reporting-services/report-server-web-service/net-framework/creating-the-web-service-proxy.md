---
title: "Creating the Web Service Proxy | Microsoft Docs"
ms.date: 03/14/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-native"
ms.technology: report-server-web-service


ms.topic: reference
helpviewer_keywords: 
  - "Report Server Web service, proxies"
  - "proxies [Reporting Services]"
  - "XML Web service [Reporting Services], proxies"
  - "Web service [Reporting Services], proxies"
  - "Web references [Reporting Services]"
ms.assetid: b1217843-8d3d-49f3-a0d2-d35b0db5b2df
author: maggiesMSFT
ms.author: maggies
---
# Creating the Web Service Proxy
  A client and a Web service can communicate using SOAP messages, which encapsulate the input and output parameters as XML. A proxy class maps parameters to XML elements and then sends the SOAP messages over a network. In this way, the proxy class frees you from having to communicate with the Web service at the SOAP level and allows you to invoke Web service methods in any development environment that supports SOAP and Web service proxies.  
  
 There are two ways to add a proxy class to your development project using the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../../includes/dnprdnshort-md.md)]: with the WSDL tool in the [!INCLUDE[dnprdnshort](../../../includes/dnprdnshort-md.md)], and by adding a Web reference in [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[vsprvs](../../../includes/vsprvs-md.md)]. The following sections discuss this subject in further detail.  
  
## Adding the Proxy Using the WSDL Tool  
 The [!INCLUDE[dnprdnshort](../../../includes/dnprdnshort-md.md)] SDK includes the Web Services Description Language tool (Wsdl.exe), which enables you to generate a Web service proxy for use in the [!INCLUDE[dnprdnshort](../../../includes/dnprdnshort-md.md)] development environment. The most common way to create a client proxy in languages that support Web services (currently C# and [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[vbprvb](../../../includes/vbprvb-md.md)]) is to use the WSDL tool.  
  
 **To add a proxy class to your project using Wsdl.exe**  
  
1.  From a command prompt, use Wsdl.exe to create a proxy class, specifying (at a minimum) the URL to the Report Server Web service.  
  
     For example, the following command prompt statement specifies a URL for the management endpoint of the Report Server Web service:  
  
    ```  
    wsdl /language:CS /n:"Microsoft.SqlServer.ReportingServices2010" https://<Server Name>/reportserver/reportservice2010.asmx?wsdl  
    ```  
  
     The WSDL tool accepts a number of command-prompt arguments for generating a proxy. The preceding example specifies the language C#, a suggested namespace to use in the proxy (to prevent name collision if using more than one Web service endpoint), and generates a C# file called ReportingService2010.cs. If the example had specified [!INCLUDE[vbprvb](../../../includes/vbprvb-md.md)], the example would have generated a proxy file with the name ReportingService2010.vb. This file is created in the directory from which you run the command.  
  
2.  Compile the proxy class into an assembly file (with the extension .dll) and reference it in your project, or add the class as a project item.  
  
    > [!NOTE]  
    >  When you add a proxy class to your project manually, you need to add a reference to System.Web.Services.dll. If you add the proxy using a Web reference in Visual Studio .NET, the reference is automatically created for you. For more information, see "Adding the Proxy Using a Web Reference in Visual Studio" later in this topic.  
  
     After you add the proxy class as an item to your project, the associated file appears in Solution Explorer.  
  
3.  To call the service programmatically, create an instance of the proxy class.  
  
     The following code example shows the syntax for creating an instance of the <xref:ReportService2010.ReportingService2010> proxy class in a project:  
  
```vb  
Dim service As New ReportingService2010()  
```  
  
```csharp  
ReportingService2010 service = new ReportingService2010();  
  
```  
  
 For more information about the Wsdl.exe tool, including its full syntax, see "Web Services Description Language Tool" in the [!INCLUDE[dnprdnshort](../../../includes/dnprdnshort-md.md)] SDK documentation. For a full explanation of Web service proxies, see "Creating an XML Web Service Proxy" in the [!INCLUDE[dnprdnshort](../../../includes/dnprdnshort-md.md)] SDK documentation.  
  
## Adding the Proxy Using a Web Reference in Visual Studio  
 A Web reference enables a project to consume one or more Web services. [!INCLUDE[vsprvs](../../../includes/vsprvs-md.md)] enables users to add Web service references to projects by following a few simple steps.  
  
 **To add a Web reference to a project**  
  
1.  In **Solution Explorer**, select the project that will consume the Web service.  
  
2.  On the **Project** menu, click **Add Web Reference**.  
  
     The **Add Web Reference** dialog box opens.  
  
3.  In the **URL** field, enter the complete path to the Report Server Web service.  
  
     A simplified URL for the report execution endpoint of the Report Server Web service might look like this:  
  
    ```  
    https://<Server Name>/reportserver/reportexecution2005.asmx  
    ```  
  
     The URL contains the domain in which the Report Server Web service is deployed, the name of the folder containing the service, and the name of the discovery file for the service. For a complete description of the different URL elements, see [Accessing the SOAP API](../../../reporting-services/report-server-web-service/accessing-the-soap-api.md).  
  
     A description of the methods and properties provided by the Web service appears in the Browser pane on the left.  
  
    > [!NOTE]  
    >  For more information about the items associated with the Report Server Web service, see [Report Server Web Service Methods](../../../reporting-services/report-server-web-service/methods/report-server-web-service-methods.md).  
  
4.  Verify that your project can use the Report Server Web service, and that you have appropriate permission to access the report server.  
  
5.  In the **Web reference name** field, enter a name that you will use in your code to access the Report Server Web service programmatically.  
  
6.  Select the **Add Reference** button to create a reference in your application to the Web service.  
  
     The new reference appears in **Solution Explorer** under the Web References node for the active project, named as specified in the **Web reference name** field.  
  
7.  In **Solution Explorer**, expand the Web References folder to note the namespace for the Web reference classes that are available to the items in your project.  
  
     After adding a Web reference to your project, the associated files are displayed in a folder within the Web References folder of **Solution Explorer**.  
  
 After you add the Web reference, use the following syntax to create an instance of the proxy class:  
  
```vb  
Dim rs As New myNamespace.myReferenceName.ReportExecutionService()  
rs.Url = "https://<Server Name>/reportserver/reportexecution2005.asmx?wsdl"  
rs.Credentials = System.Net.CredentialCache.DefaultCredentials  
```  
  
```csharp  
myNamespace.myReferenceName.ReportExecutionService rs = new myNamespace.myReferenceName.ReportExecutionService();  
rs.Url = "https://<Server Name>/reportserver/reportexecution2005.asmx?wsdl";  
rs.Credentials = System.Net.CredentialCache.DefaultCredentials;  
  
```  
  
 You can also add a **using** (**Import** in [!INCLUDE[vbprvb](../../../includes/vbprvb-md.md)]) directive to the Report Server Web service reference. If you use this directive, you do not need to fully qualify the types in the namespace. To do this, add the following code to your file:  
  
```vb  
Import myNamespace.myReferenceName  
```  
  
```csharp  
using myNamespace.myReferenceName;  
```  
  
## See Also  
 [Report Server Web Service](../../../reporting-services/report-server-web-service/report-server-web-service.md)   
 [Building Applications Using the Web Service and the .NET Framework](../../../reporting-services/report-server-web-service/net-framework/building-applications-using-the-web-service-and-the-net-framework.md)   
 [Technical Reference &#40;SSRS&#41;](../../../reporting-services/technical-reference-ssrs.md)  
  
  
