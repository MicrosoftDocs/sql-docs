---
title: "Enable and Disable Client-Side Printing for Reporting Services | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 0e709c96-7517-4547-8ef6-5632f8118524
author: markingmyname
ms.author: maghan
manager: kfile
---
# Enable and Disable Client-Side Printing for Reporting Services
  The [!INCLUDE[msCoName](../../includes/msconame-md.md)] ActiveX control, **RSClientPrint**, provides client-side printing for reports viewed in a browser. The control displays a custom print dialog box that supports features common to other print dialog boxes. The features include print preview, page selections for specifying specific pages and ranges, page margins, and orientation. Although client-side printing is enabled by default, you can disable the feature to prevent it from being used.  
  
> [!NOTE]  
>  Downloading ActiveX controls requires administrator permissions.  
  
## Downloading the ActiveX Control  
 Each user who wants to use the print feature must download and install the ActiveX control that provides client print functionality. The first time a user clicks the **Printer** icon on the report toolbar, the Microsoft ActiveX control is downloaded to the computer. After the control is downloaded, the **Print** dialog box displays whenever the user clicks the **Printer** icon.  
  
 Depending on browser settings, the user may be prompted to install the control, be prevented from installing the control, or have the control install transparently in the background.  
  
 For [!INCLUDE[msCoName](../../includes/msconame-md.md)] Internet Explorer, settings that affect ActiveX control download and installation are specified through the **ActiveX controls and plug-ins** node in the **Security Settings** page for the Web content zone. The following settings determine whether users can download and run the print control, based on Web zone security preferences:  
  
-   Download signed ActiveX controls.  
  
-   Script ActiveX controls marked safe for scripting.  
  
-   Run ActiveX controls and plug-ins.  
  
 Users who want to use **RSClientPrint** to perform client-side printing must enable the following:  
  
-   **Download signed ActiveX controls** and **Script ActiveX control marked safe for scripting** for installation purposes.  
  
-   **Run ActiveX controls and plug-ins** for ongoing print operations.  
  
 The **RSClientPrint** ActiveX control is signed, meaning it contains a valid digital certificate from [!INCLUDE[msCoName](../../includes/msconame-md.md)].  
  
## Enabling and Disabling Client-Side Printing  
 Report server administrators have the option of disabling the print feature by setting the report server system property **EnableClientPrinting** to `false`. This will disable client-side printing for all reports managed by that server. By default, **EnableClientPrinting** is set to `true`. You can disable client-side printing in the following ways:  
  
-   For a **Native mode report server**:  
  
    1.  Start [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] with administrative privileges.  
  
    2.  Connect to a report server instance in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)].  
  
    3.  Right-click the report server node, and then click **Properties**. If the **Properties** option is disabled, verify you started [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] with administrative privileges.  
  
    4.  Select **Enable download for the ActiveX client print control**.  
  
    5.  Click **OK**.  
  
-   For a **SharePoint mode report server**:  
  
    1.  In SharePoint Central Administration, click **Application Management**.  
  
    2.  Click **Manage service applications**.  
  
    3.  Click the name of your [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service application, and then click **Manage** in the SharePoint ribbon.  
  
    4.  Click **System Settings**.  
  
    5.  Select **Enable Client Printing**. The **Enable Client Printing** option is near the bottom of the page.  
  
    6.  Click **OK**.  
  
-   Write script or code to set the report server system property **EnableClientPrinting** to `false.`  
  
 The following sample script illustrates one approach for disabling client-side printing. Compile and then run the following [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[vbprvb](../../includes/vbprvb-md.md)] code to set the **EnableClientPrinting** property to **False**. After you run the code, restart IIS.  
  
### Sample Script  
  
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
  
  
