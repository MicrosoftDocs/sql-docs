---
title: "Install the Stand-Alone Version of Report Builder (Report Builder) | Microsoft Docs"
ms.custom: ""
ms.date: "12/29/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 6b2291bb-1d20-4d08-81cb-a16dd8e01faf
author: markingmyname
ms.author: maghan
manager: kfile
---
# Install the Stand-Alone Version of Report Builder (Report Builder)
  You can install Report Builder from the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] feature pack on the [Microsoft Download Center](https://go.microsoft.com/fwlink/?LinkID=168472) or a location such as public folder to which the ReportBuilder3_x86.msi, the Windows Installer Package for Report Builder, has been downloaded.  
  
 You can also perform a command line installation of Report Builder and provide arguments to customize the installation. In addition to the standard MSI intrinsic parameters, you can use the custom parameters that Report Builder provides: RBINSTALLDIR and REPORTSERVERURL. RBINSTALLDIR specifies the root installation folder for Report Builder. REPORTSERVERURL specifies the default report server that Report Builder uses to save reports on the server.  
  
 If you want a completely silent installation, with no user interface interaction at all, specify the **/quiet** option. By design, the quiet option flag suppresses installation errors. It is therefore recommended that you include the **/l** option, which specifies logging, when you use the quiet option.  
  
> [!IMPORTANT]  
>  Windows Vista and Windows 7 security features require elevated permissions to run command line operations and will prompt for permission to run the command line. The installation is not silent. To make the installation silent, you need to run the command line as an administrator.  
  
### To install Report Builder from the download site  
  
1.  Go to [Microsoft SQL Server 2012 Report Builder](https://go.microsoft.com/fwlink/?LinkID=219138) and locate the Report Builder section of the Web page.  
  
2.  Click **X86 Package**.  
  
3.  In the **File Download** dialog box, click **Run**.  
  
    > [!IMPORTANT]  
    >  Download only files from trusted sources.  
  
4.  In the Internet Explorer dialog box, click **Run**.  
  
    > [!IMPORTANT]  
    >  Run only files from trusted sources.  
  
5.  The Microsoft SQL Server Report Builder Wizard is launched.  
  
6.  On the **Welcome to the Installation Wizard** page, click **Next**.  
  
7.  On the **License Agreement** page, read the agreement and then select the **I accept the terms in the license agreement** option. Click **Next**.  
  
8.  Provide your personal name and company name. Click **Next**.  
  
9. On the **Feature Selection** page, optionally click **Browse** or **Disk Cost**. Click **Next**.  
  
    -   Click **Browse** to see the default location of Report Builder and update it.  
  
        > [!NOTE]  
        >  The default installation folder for Report Builder is \<drive>Program Files\Microsoft SQL Server.  
  
    -   Click **Disk Cost** to learn how much disk space Report Builder consumes.  
  
        > [!NOTE]  
        >  If a volume does not have sufficient amount of free disk space, the volume is highlighted.  
  
10. On the **Default Target Server** page, optionally provide the URL to the target report server if it is different from the default. Click **Next**.  
  
    > [!NOTE]  
    >  If you plan to work with Report Builder when it is connected to a report server, it is convenient to provide the URL to the server at this time. However, you can also do this from the **Options** dialog box when you are working in Report Builder.  
  
11. Click **Install** to complete the installation of Report Builder.  
  
### To install Report Builder from a share  
  
1.  Contact your administrator for the location of ReportBuilder3_x86.msi.msi that you run to install Report Builder on your local computer.  
  
2.  Browse to locate the ReportBuilder3_x86.msi.msi, the Windows Installer Package (MSI) for Report Builder, and click it.  
  
     The Microsoft SQL Server Report Builder Wizard is launched.  
  
3.  On the **Welcome to the Installation Wizard** page, click **Next**.  
  
4.  On the **License Agreement** page, read the agreement and then select the **I accept the terms in the license agreement** option. Click **Next**.  
  
5.  Provide your personal name and company name. Click **Next**.  
  
6.  On the **Feature Selection** page, optionally click **Browse** or **Disk Cost**. Click **Next**.  
  
    -   Click **Browse** to see the default location of Report Builder and update it.  
  
        > [!NOTE]  
        >  The default installation folder for Report Builder is \<drive>Program Files\Microsoft SQL Server.  
  
    -   Click **Disk Cost** to learn how much disk space Report Builder consumes.  
  
        > [!NOTE]  
        >  If a volume does not have sufficient amount of free disk space, the volume is highlighted.  
  
7.  On the **Default Target Server** page, optionally provide the URL to the target report server if it is different from the default. Click **Next**.  
  
    > [!NOTE]  
    >  If you plan to work with Report Builder when it is connected to a report server, it is convenient to provide the URL to the server at this time. However, you can also do this from the **Options** dialog box when you are working in Report Builder.  
  
8.  Click **Install** to complete the installation of Report Builder.  
  
### To install Report Builder from the command line  
  
1.  Go to [Microsoft SQL Server 2012 Report Builder](https://go.microsoft.com/fwlink/?LinkID=219138) and locate the Report Builder section.  
  
2.  Click **X86 Package**.  
  
3.  Click Save.  
  
4.  Optionally, browse to the location to save to, verify the **Save as** option is **Windows Installer Package**, and then click **Save**.  
  
5.  On the **Start** menu, click **Run**.  
  
6.  In the Open text box, type `cmd.`  
  
7.  In the Command Prompt window, navigate to the folder where you saved ReportBuilder3_x86.msi.  
  
8.  Type a command with the following format:  
  
     `msiexec/i ReportBuilder3_.msi /option [value] [/option [value]]`  
  
     The two options specific to installing Report Builder are: RBINSTALLDIR and REPORTSERVERURL. It not required that you include these arguments in the command line. The following is the baseline command:  
  
     `msiexec /i ReportBuilder3_x86.msi /quiet`  
  
9. To run the command, press Enter.  
  
## See Also  
 [Install, Uninstall, and Report Builder Support](../install-uninstall-and-report-builder-support.md)   
 [Uninstall the Stand-Alone Version of Report Builder &#40;Report Builder&#41;](install-report-builder.md)  
  
  
