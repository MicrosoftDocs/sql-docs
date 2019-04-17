---
title: "Using the SOAP API in a Windows Application | Microsoft Docs"
ms.date: 03/14/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-native"
ms.technology: application-integration


ms.topic: reference
helpviewer_keywords: 
  - "rendered reports [Reporting Services]"
  - "Windows applications [Reporting Services]"
  - "Windows Forms [Reporting Services]"
  - "SOAP [Reporting Services], Windows applications"
ms.assetid: e4804792-20cd-4df2-9257-fb958ff447b4
author: markingmyname
ms.author: maghan
---
# Integrating Reporting Services Using SOAP - Windows Application
  You can access the full functionality of the report server through the Reporting Services SOAP API. The SOAP API is a Web service and, as such, can be easily accessed to provide enterprise reporting features to your custom business applications. You can access the Web service in a Windows application simply by writing code that makes calls to the service. Using the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)], you can generate a proxy class that exposes the properties and methods of the Web service and enables you to use a familiar infrastructure and tools to build business applications built on [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] technology.  
  
## Integrating Report Management Functionality Using Windows Forms  
 Unlike URL access, the SOAP API exposes the complete set of management functions that are available through the report server. This means that the entire administrative functionality of Report Manager is available to developers through SOAP. As such, you can develop a complete management and administration tool using Windows Forms. For example, in your Windows application, you might want to enable your users to retrieve the contents of the report server namespace. To do this, you could use the Web service <xref:ReportService2010.ReportingService2010.ListChildren%2A> method to list all the items in the report server database and then use a Listview, Treeview, or Combobox control to display those items to your users. The following Web service code might be used to retrieve the current list of available reports in a user's My Reports folder when a user clicks a button on a form:  
  
```vb  
' Button click event that retrieves a list of reports from  
' the My Reports folder and displays them in a combo box  
Private Sub listReportsButton_Click(sender As Object, e As System.EventArgs)  
   ' Create a new Web service object and set credentials  
   ' to Windows Authentication  
   Dim rs As New ReportingService2010()  
   rs.Credentials = System.Net.CredentialCache.DefaultCredentials  
  
   ' Return the list of items in My Reports  
   Dim items As CatalogItem() = rs.ListChildren("/Adventureworks 2008 Sample Reports", False)  
  
   Dim ci As CatalogItem  
   For Each ci In  items  
      ' If the item is a report, add it to   
      ' a combo box  
      If ci.TypeName = "Report" Then  
         catalogComboBox.Items.Add(ci.Name)  
      End If  
   Next ci  
End Sub 'listReportsButton_Click  
```  
  
```csharp  
// Button click event that retrieves a list of reports from  
// the My Reports folder and displays them in a combo box  
private void listReportsButton_Click(object sender, System.EventArgs e)  
{  
   // Create a new Web service object and set credentials  
   // to Windows Authentication  
   ReportingService2010 rs = new ReportingService2010();  
   rs.Credentials = System.Net.CredentialCache.DefaultCredentials;  
  
   // Return the list of items in My Reports  
   CatalogItem[] items = rs.ListChildren("/Adventureworks 2008 Sample Reports", false);  
  
   foreach (CatalogItem ci in items)  
   {  
      // If the item is a report, add it to   
      // a combo box  
      if (ci.TypeName == "Report")  
         catalogComboBox.Items.Add(ci.Name);  
   }  
}  
```  
  
 From there, you might enable users to select the report from the Combo box and preview the report on the form either using a Web browser control or an image control.  
  
## Enabling Report Viewing and Navigation Using Windows Forms  
 There are two methods available for integrating reports into your Windows Forms applications.  
  
 You can use the SOAP API to render reports to any of the supported rendering formats using the <xref:ReportExecution2005.ReportExecutionService.Render%2A> method. There are slight disadvantages to enabling report viewing and navigation through SOAP:  
  
-   You cannot take advantage of the built-in functionality of the report toolbar that is included with the HTML Viewer through URL access.  
  
-   If you render to HTML, you must separately render any images or resources as additional streams using the <xref:ReportExecution2005.ReportExecutionService.RenderStream%2A> method.  
  
-   There is a slight performance advantage to rendering reports using URL access over using the SOAP API.  
  
 However, the <xref:ReportExecution2005.ReportExecutionService.Render%2A> method of the SOAP API can be used to render reports and save them to various output formats programmatically. This is an advantage over URL access, which requires user interaction. When you render a report using the SOAP API <xref:ReportExecution2005.ReportExecutionService.Render%2A> method, you can render to any of the supported output formats.  
  
 You can also use the freely distributable Report Viewer controls that are included with [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[vsOrcas](../../includes/vsorcas-md.md)]. The Report Viewer controls make it easy to embed [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] functionality into custom applications. The Report Viewer controls are intended for developers who want to provide predesigned, fully authored reports as part of an application feature set (for example, a Web site management application might include reports that show click-stream analysis on company Web sites). Embedding the controls in an application provides a streamlined alternative to including the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] server components in your application deployment. The controls provide report functionality, but without the additional report authoring, publication, or distribution and delivery support that you find in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].  
  
 There are two versions of the Report Viewer controls, one for rich Windows client applications and one for [!INCLUDE[vstecasp](../../includes/vstecasp-md.md)] applications. The controls support both local processing and remote processing modes. In local processing mode, your application provides the report definition and datasets and triggers report processing. In remote processing mode, data retrieval and report processing happen on the report server and the control is used for display and report navigation. This model allows you to build rich applications that can be scaled from desktop to the enterprise.  
  
 Report Viewer controls are documented in [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)] online Help. For more information, see the [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)] product documentation.  
  
## See Also  
 [Building Applications Using the Web Service and the .NET Framework](../../reporting-services/report-server-web-service/net-framework/building-applications-using-the-web-service-and-the-net-framework.md)   
 [Integrating Reporting Services into Applications](../../reporting-services/application-integration/integrating-reporting-services-into-applications.md)   
 [Using the SOAP API in a Web Application](../../reporting-services/application-integration/integrating-reporting-services-using-soap-web-application.md)  
  
  
