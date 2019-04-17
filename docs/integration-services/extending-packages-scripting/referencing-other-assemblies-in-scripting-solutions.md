---
title: "Referencing Other Assemblies in Scripting Solutions | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "reference"
dev_langs: 
  - "VB"
helpviewer_keywords: 
  - "SSIS Script task, .NET Framework"
  - "Script task [Integration Services], adding references"
  - "referencing custom assemblies"
  - "SSIS Script task, VisualBasic namespace"
  - "assemblies [Integration Services]"
  - "VisualBasic namespace"
  - "Script task [Integration Services], VisualBasic namespace"
  - "Microsoft.VisualBasic namespace"
  - "Script task [Integration Services], .NET Framework"
  - ".NET Framework [Integration Services]"
  - "referencing Web services"
ms.assetid: 9b655bcd-19f6-43d8-9f89-1b4d299c6380
author: janinezhang
ms.author: janinez
manager: craigg
---
# Referencing Other Assemblies in Scripting Solutions
  The [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] class library provides the script developer with a powerful set of tools for implementing custom functionality in [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] packages. The Script task and the Script component can also use custom managed assemblies.  
  
> [!NOTE]
>  To enable your packages to use the objects and methods from a Web service, use the **Add Web Reference** command available in [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[vsprvs](../../includes/vsprvs-md.md)] Tools for Applications (VSTA). In earlier versions of [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)], you had to generate a proxy class to use a Web service.  
  
## Using a Managed Assembly  
 For [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] to find a managed assembly at design time, you must do the following steps:  
  
1.  Store the managed assembly in any folder on your computer.  
  
    > [!NOTE]  
    >  In earlier versions of [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)], you could only add a reference to a managed assembly that was stored in the %windir%\Microsoft.NET\Framework\vx.x.xxxxx folder or the %ProgramFiles%\Microsoft SQL Server\100\SDK\Assemblies folder.  
  
2.  Add a reference to the managed assembly.  
  
     To add the reference, in VSTA, in the **Add Reference** dialog box, on the **Browse** tab, locate and add the managed assembly.  
  
 For [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] to find the managed assembly at run time, you must do the following steps:  
  
1.  Sign the managed assembly with a strong name.  
  
2.  Install the assembly in the global assembly cache on the computer on which the package is run.  
  
     For more information, see [Building, Deploying, and Debugging Custom Objects](../../integration-services/extending-packages-custom-objects/building-deploying-and-debugging-custom-objects.md).  
  
## Using the Microsoft .NET Framework Class Library  
 The Script task and the Script component can take advantage of all the other objects and functionality exposed by the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] class library. For example, by using the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)], you can retrieve information about your environment and interact with the computer that is running the package.  
  
 This list describes several of the more frequently used [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] classes:  
  
-   **System.Data** Contains the ADO.NET architecture.  
  
-   **System.IO** Provides an interface to the file system and streams.  
  
-   **System.Windows.Forms** Provides form creation.  
  
-   **System.Text.RegularExpressions** Provides classes for working with regular expressions.  
  
-   **System.Environment** Returns information about the local computer, the current user, and computer and user settings.  
  
-   **System.Net** Provides network communications.  
  
-   **System.DirectoryServices** Exposes Active Directory.  
  
-   **System.Drawing** Provides extensive image manipulation libraries.  
  
-   **System.Threading** Enables multithreaded programming.  
  
 For more information about the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)], see the MSDN Library.  
  
## See Also  
 [Extending Packages with Scripting](../../integration-services/extending-packages-scripting/extending-packages-with-scripting.md)  
  
  
