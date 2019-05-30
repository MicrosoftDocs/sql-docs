---
title: "Lesson 1: Create the RDL Schema Visual Studio Project | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: reporting-services-native
ms.topic: conceptual
ms.assetid: f420509c-51aa-4170-8c25-64c2954f4bb9
author: markingmyname
ms.author: maghan
manager: kfile
---
# Lesson 1: Create the RDL Schema Visual Studio Project
  For this tutorial, you will create a simple console application. This tutorial assumes you are developing in [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[vs_dev10_long](../includes/vs-dev10-long-md.md)].  
  
> [!NOTE]  
>  When accessing the Report Server Web service running on [!INCLUDE[ssExpress](../includes/ssexpress-md.md)] with Advanced Services, you must append "_SQLExpress" to the "ReportServer" path. For example:  
>   
>  `http://myserver/reportserver_sqlexpress/reportservice2010.asmx"`  
  
### To create the web service proxy  
  
1.  From the **Start** menu, select **All Programs**, then Microsoft Visual Studio, then **Visual Studio Tools**, and then **Visual Studio 2010 Command Prompt**.  
  
2.  In the command prompt window, run the following command if you are using C#:  
  
    ```  
    wsdl /language:CS /n:"ReportService2010" http://<Server Name>/reportserver/reportservice2010.asmx?wsdl  
    ```  
  
     If you are using Visual Basic, then run the following command:  
  
    ```  
    wsdl /language:VB /n:"ReportService2010" http://<Server Name>/reportserver/reportservice2010.asmx?wsdl  
    ```  
  
     This command generates a .cs or .vb file. You will add this file to your application.  
  
### To create a console application  
  
1.  On the **File** menu, point to **New**, and then click **Project** to open the **New Project** dialog box.  
  
2.  In the left pane, under **Installed Templates**, click either **Visual Basic** or the **Visual C#** node, and select a category of project types from the expanded list.  
  
3.  Choose the **Console Application** project type.  
  
4.  In the **Name** box, enter a name for your project. Type the name `SampleRDLSchema`.  
  
5.  In the **Location** box, type the path where you want to save your project, or click **Browse** to navigate to the folder.  
  
6.  [!INCLUDE[clickOK](../includes/clickok-md.md)] A collapsed view of your project appears in Solution Explorer.  
  
7.  On the **Project** menu, click **Add Existing Item**.  
  
8.  Navigate to the location for the .cs or .vb file you generated, then select the file, and then click **Add**.  
  
     You will also need to add a reference to the <xref:System.Web.Services> namespace for your Web reference to work.  
  
9. On the Project menu, click **Add Reference**.  
  
     In the **Add Reference** dialog box, in the **.NET** tab, select **System.Web.Services**, then click **OK**.  
  
     For more information about how to connect to the Report Server Web service, see [Building Applications Using the Web Service and the .NET Framework](../reporting-services/report-server-web-service/net-framework/building-applications-using-the-web-service-and-the-net-framework.md).  
  
10. In Solution Explorer, expand the project node. You will see a code file with the default name of Program.cs (Module1.vb for [!INCLUDE[vbprvb](../includes/vbprvb-md.md)]) has been added to your project.  
  
11. Open the Program.cs (Module1.vb for [!INCLUDE[vbprvb](../includes/vbprvb-md.md)]) file and replace the code with the following:  
  
     The following code provides the method stubs we will use to implement the Load, Update and Save functionality.  
  
    ```csharp  
    using System;  
    using System.Collections.Generic;  
    using System.IO;  
    using System.Text;  
    using System.Xml;  
    using System.Xml.Serialization;  
    using ReportService2010;  
  
    namespace SampleRDLSchema  
    {  
        class ReportUpdater  
        {  
            ReportingService2010 _reportService;  
  
            static void Main(string[] args)  
            {  
                ReportUpdater reportUpdater = new ReportUpdater();  
                reportUpdater.UpdateReport();  
            }  
  
            private void UpdateReport()  
            {  
                try  
                {  
                    // Set up the Report Service connection  
                    _reportService = new ReportingService2010();  
                    _reportService.Credentials =  
                        System.Net.CredentialCache.DefaultCredentials;  
                    _reportService.Url =  
                       "http://<Server Name>/reportserver/" +  
                                       "reportservice2010.asmx";  
  
                    // Call the methods to update a report definition  
                    LoadReportDefinition();  
                    UpdateReportDefinition();  
                    PublishReportDefinition();  
                }  
                catch (Exception ex)  
                {  
                    System.Console.WriteLine(ex.Message);  
                }  
            }  
  
            private void LoadReportDefinition()  
            {  
                //Lesson 3: Load a report definition from the report   
                //          catalog  
            }  
  
            private void UpdateReportDefinition()  
            {  
                //Lesson 4: Update the report definition using the    
                //          classes generated from the RDL Schema  
            }  
  
            private void PublishReportDefinition()  
            {  
                //Lesson 5: Publish the updated report definition back   
                //          to the report catalog  
            }  
        }  
    }  
    ```  
  
    ```vb  
    Imports System  
    Imports System.Collections.Generic  
    Imports System.IO  
    Imports System.Text  
    Imports System.Xml  
    Imports System.Xml.Serialization  
    Imports ReportService2010  
  
    Namespace SampleRDLSchema  
  
        Module ReportUpdater  
  
            Private m_reportService As ReportingService2010  
  
            Public Sub Main(ByVal args As String())  
  
                Try  
                    'Set up the Report Service connection  
                    m_reportService = New ReportingService2010  
                    m_reportService.Credentials = _  
                        System.Net.CredentialCache.DefaultCredentials  
                    m_reportService.Url = _  
                        "http:// <Server Name>/reportserver/" & _  
                               "reportservice2010.asmx"  
  
                    'Call the methods to update a report definition  
                    LoadReportDefinition()  
                    UpdateReportDefinition()  
                    PublishReportDefinition()  
                Catch ex As Exception  
                    System.Console.WriteLine(ex.Message)  
                End Try  
  
            End Sub  
  
            Private Sub LoadReportDefinition()  
                'Lesson 3: Load a report definition from the report   
                '          catalog  
            End Sub  
  
            Private Sub UpdateReportDefinition()  
                'Lesson 4: Update the report definition using the   
                '          classes generated from the RDL Schema  
            End Sub  
  
            Private Sub PublishReportDefinition()  
                'Lesson 5: Publish the updated report definition back   
                '          to the report catalog  
            End Sub  
  
        End Module  
  
    End Namespace   
    ```  
  
## Next Lesson  
 In the next lesson, you will use the XML Schema Definition Tool (Xsd.exe) to generate classes from the RDL schema and include them in your project. See [Lesson 2: Generate Classes from the RDL Schema using the xsd Tool](../../2014/tutorials/lesson-2-generate-classes-from-the-rdl-schema-using-the-xsd-tool.md).  
  
## See Also  
 [Updating Reports Using Classes Generated from the RDL Schema &#40;SSRS Tutorial&#41;](../../2014/tutorials/updating-reports-using-classes-generated-from-the-rdl-schema-ssrs-tutorial.md)   
 [Report Definition Language &#40;SSRS&#41;](../reporting-services/reports/report-definition-language-ssrs.md)  
  
  
