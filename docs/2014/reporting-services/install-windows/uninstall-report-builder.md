---
title: "Uninstall the Stand-Alone Version of Report Builder (Report Builder) | Microsoft Docs"
ms.custom: ""
ms.date: "12/29/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 009538c6-4941-4393-b14b-9144cffdbdaf
author: maggiesMSFT
ms.author: maggies
manager: craigg
---
# Uninstall the Stand-Alone Version of Report Builder (Report Builder)
  You can uninstall the stand-alone version of Report Builder from the control panel or the command line. You cannot uninstall the [!INCLUDE[ndptecclick](../../includes/ndptecclick-md.md)] version of Report Builder without uninstalling [!INCLUDE[ssRSCurrent](../../includes/ssrscurrent-md.md)].  
  
 Uninstalling Report Builder from the command line uses syntax that is identical to the syntax you use to install Report Builder, except you use the /x option instead of the /i option. Command lines for uninstalling can also include the /quiet option and other standard options. If the Report Builder Windows Installer Package (ReportBuilder3_x86.msi) has been removed, you cannot use the command line easily to uninstall Report Builder. To learn more about how you might be able to remove Report Builder by using its GUID, see the documentation for the msiexec program in the msdn library.  
  
 If folders used by Report Builder include custom files, the folders and the files are preserved when Report Builder is removed. Only the Report Builder files are removed.  
  
### To uninstall Report Builder from the control panel  
  
1.  On the **Start** menu, click **Control Panel**.  
  
2.  In the Control Panel, click **Programs and Features**.  
  
3.  Locate [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] Report Builder in the **Name** list and click it.  
  
4.  Click **Uninstall**.  
  
5.  If prompted to confirm the uninstall of Report Builder, click **Yes**.  
  
### To uninstall Report Builder from the command line  
  
1.  On the **Start** menu, click **Run**.  
  
2.  In the **Open** text box, type `cmd.`  
  
3.  In the command prompt window, navigate to the folder with ReportBuilder3_x86.msi.  
  
4.  Type a basic command line such as the following:  
  
 `msiexec /x ReportBuilder3_x86.msi /quiet /l*v install.log`  
  
 If you can to include logging, use a command line such as the following:  
  
 `msiexec /x ReportBuilder3_x86.msi /quiet /l*v c:\junk\install.log`  
  
1.  Press **Enter**.  
  
## See Also  
 [Install, Uninstall, and Report Builder Support](../install-uninstall-and-report-builder-support.md)   
 [Install the Stand-Alone Version of Report Builder &#40;Report Builder&#41;](install-report-builder.md)  
  
  
