---
description: "Uninstall Report Builder"
title: "Uninstall Report Builder | Microsoft Docs"
ms.date: 05/30/2017
ms.service: reporting-services


ms.topic: conceptual
ms.assetid: 009538c6-4941-4393-b14b-9144cffdbdaf
author: maggiesMSFT
ms.author: maggies
---

# Uninstall Report Builder

You can uninstall the stand-alone version of Report Builder from the control panel or the command line.

Uninstalling Report Builder from the command line uses syntax that is identical to the syntax you use to install Report Builder, except you use the /x option instead of the /i option. Command lines for uninstalling can also include the /quiet option and other standard options. If the Report Builder Windows Installer Package (ReportBuilder3_x86.msi) has been removed, you cannot use the command line easily to uninstall Report Builder. To learn more about how you might be able to remove Report Builder by using its GUID, see the documentation for the msiexec program in [Command-Line Options](/windows/desktop/Msi/command-line-options).  

If folders used by Report Builder include custom files, the folders and the files are preserved when Report Builder is removed. Only the Report Builder files are removed.  

### To uninstall Report Builder from the control panel

1.  On the **Start** menu, click **Control Panel**.  
  
2.  In the Control Panel, click **Programs and Features**.  
  
3.  Locate [!INCLUDE[msCoName](../../includes/msconame-md.md)] SQL Server Report Builder in the **Name** list and click it.  
  
4.  Click **Uninstall**.  
  
5.  If prompted to confirm the uninstall of Report Builder, click **Yes**.  
  
### To uninstall Report Builder from the command line  
  
1.  On the **Start** menu, click **Run**.  
  
2.  In the **Open** text box, type **cmd.**  
  
3.  In the command prompt window, navigate to the folder with ReportBuilder3_x86.msi.  
  
4.  Type a basic command line such as the following:  
  
 `msiexec /x ReportBuilder3_x86.msi /quiet /l*v install.log`  
  
 If you can to include logging, use a command line such as the following:  
  
 `msiexec /x ReportBuilder3_x86.msi /quiet /l*v c:\junk\install.log`  
  
5.  Press **Enter**.  

## Next steps

[Install Report Builder](../../reporting-services/install-windows/install-report-builder.md)  

More questions? [Try asking the Reporting Services forum](https://go.microsoft.com/fwlink/?LinkId=620231)
