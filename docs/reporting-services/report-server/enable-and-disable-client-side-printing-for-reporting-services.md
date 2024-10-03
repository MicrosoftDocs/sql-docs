---
title: "Enable and disable client-side printing for Reporting Services"
description: Learn how to enable or disable client-side printing for Reporting Services reports viewed in a browser. Client-side printing uses PDF and is enabled by default.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-server
ms.topic: conceptual
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "pdf"
  - "viewer"
  - "reportviewer"
  - "toolbar"
---

# Enable and disable client-side printing for Reporting Services

  The print button on the report viewer toolbar uses the Portable Document Format (PDF) format for client-side printing of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] reports viewed in a browser. The new remote printing experience uses the PDF rendering extension that is included with [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], to render the report in PDF format. You can download a PDF form of the report. Or, if you have an application installed for viewing PDF files, the print button displays a print dialog for page common configuration items. These include configuration items such as page size an orientation and a preview of the PDF file. Although client-side printing is enabled by default, you can disable the feature to prevent it from being used.  
  
 Previous versions of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] used an ActiveX control that required downloading to the client computer from the report server. If you upgrade your report server to SQL Server 2016 or later, the print control isn't removed from the report server or client computers.  

##  <a name="bkmk_clientside_printexpereince"></a> The print experience  
 When you select the print :::image type="icon" source="../../reporting-services/report-server/media/htmlviewer-print.png" alt-text="Screenshot of the print button."::: button on the report viewer toolbar, the experience varies depending on what PDF viewing applications you installed on the client computer and what browser you're using.   You can download the PDF file or configure print options from a dialog, or both, depending on the client computer.  
   
 :::image type="content" source="../../reporting-services/media/ssrs-htmlviewer-toolbar.png" alt-text="Screenshot of the Report toolbar.":::

  
|Interaction|User interface|  
|-|-|  
|The first dialog is the same for all browsers and allows you to change basic layout properties such as orientation. When you select **Print**, the experience is slightly different depending on the browser you're using.| :::image type="content" source="../../reporting-services/report-server/media/ssrs-pdfprint-chrome1.png" alt-text="Screenshot of the first print properties screen.":::|  
|In Chrome, a detailed browser print screen opens. You can change the print configuration, print, and open the operating systems print dialog.|:::image type="content" source="../../reporting-services/report-server/media/ssrs-pdfprint-chrome2.png" alt-text="Screenshot of the detailed print properties."::: :::image type="content" source="../../reporting-services/report-server/media/ssrs-pdfprint-chrome3-png.png" alt-text="Screenshot of the print configuration settings.":::|  
|If you have a PDF reader application installed, the print button opens a preview window of the PDF file and you can save or print.| |  
|If you don't have a PDF reader application installed, there are two user experiences:<br /><br /> The report automatically renders and uses your browsers download process to download the PDF file. **Note:** The more complicated the report is, the longer the delay between the time you select **Print** and when you see your browser's download notification. You can also force the download again by selecting **Click here to view the PDF of your report.**.<br /><br /> Force the PDF download by selecting **Click here to view the PDF of your report.**.|:::image type="content" source="../../reporting-services/report-server/media/ssrs-pdfprint-firefox2.png" alt-text="Screenshot of the PDF ready notification.":::|  
  
##  <a name="bkmk_troubleshoot_clientsideprinting"></a> Troubleshoot client-side printing  
 If the print button in the report viewer toolbar is disabled, verify the following statements:  
  
-   Client-side printing is disabled for the report server in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)]. See the section [Enable and disable client-side printing](#bkmk_enable) in this article.  
  
-   The [!INCLUDE[ssRSCurrent](../../includes/ssrscurrent-md.md)] PDF rendering extension is disabled. Review the `<Extension Name="PDF"` section of the `rsreportserver.config` file.  
  
-   You're viewing the reporting in comparability mode, which uses the old [!INCLUDE[ssRSCurrent](../../includes/ssrscurrent-md.md)] HTML4 rendering engine. The PDF printing experience requires the HTML 5 rendering engine.  Select **Try Preview** on the toolbar.  
  
:::image type="content" source="../../reporting-services/report-server/media/ssrs-html5-switch2html5.png" alt-text="Screenshot of the Try Preview button.":::

  
##  <a name="bkmk_enable"></a> Enable and disable client-side printing  
 Report server administrators have the option of disabling the remote print feature by setting the report server system property `EnableClientPrinting` to **False**. This setting disables client-side printing for all reports managed by that server. By default, `EnableClientPrinting` is set to **True**. You can disable client-side printing in the following ways:  
  
-   For a **native mode report server**:  
  
    1.  Start [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] with administrative privileges.  
  
    2.  Connect to a report server instance in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)].  
  
    3.  Right-click the report server node, and then select **Properties**. If the **Properties** option is disabled, verify that you started [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] with administrative privileges.  
  
    4.  Select **Advanced**.  
  
    5.  Select **EnableClientPrinting**.  
  
    6.  Set to **True** or **False**, and then select **OK**.  
  
          :::image type="content" source="../../reporting-services/report-server/media/ssrs-ssmsproperties-clientprinting.png" alt-text="Screenshot of the EnableClientPrinting option.":::
 
  
-   For a **SharePoint mode report server**:  
  
    1.  In SharePoint Central Administration, select **Application Management**.  
  
    2.  Select **Manage service applications**.  
  
    3.  Choose the name of your [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service application, and then select **Manage** in the SharePoint ribbon.  
  
    4.  Select **System Settings**.  
  
    5.  Select **Enable Client Printing**. The **Enable Client Printing** option is near the bottom of the page.  
  
    6.  Select **OK**.  
  
-   Write script or code to set the report server system property `EnableClientPrinting` to **False.**  
  
 The following sample script illustrates one approach for disabling client-side printing. Compile and then run the following [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[visual-basic](../../includes/visual-basic-md.md)] code to set the **EnableClientPrinting** property to **False**. After you run the code, restart IIS.  
  
### Sample script  
  
```  
Imports System  
Imports System.Web.Services.Protocols  
Class Sample  
   Public Shared Sub Main()  
Dim rs As New ReportingService()  
      rs.Credentials = System.Net.CredentialCache.DefaultCredentials  
        Dim props(0) As [Property]  
        Dim setProp As New [Property]  
        setProp.Name = "EnableClientPrinting"  
        setProp.Value = "False"   
        props(0) = setProp  
        Try  
            rs.SetSystemProperties(props)  
        Catch ex As System.Web.Services.Protocols.SoapException  
            Console.Write(ex.Detail.InnerXml)  
        Catch e as Exception  
            Console.Write(e.Message)  
        End Try  
    End Sub 'Main  
End Class 'Sample  
```

More questions? [Try asking the Reporting Services forum](https://go.microsoft.com/fwlink/?LinkId=620231)
