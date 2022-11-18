---
title: "Creating a Data Processing Extension Library | Microsoft Docs"
description: Learn how to create a Reporting Services data processing extension. View sample code, and see which namespace and library file requirements you need to meet.
ms.date: 03/14/2017
ms.prod: reporting-services
ms.technology: extensions


ms.topic: reference
helpviewer_keywords: 
  - "data processing extensions [Reporting Services], namespace assignments"
  - "library [Reporting Services]"
  - "assigning namespaces to extensions"
ms.assetid: 82f4b71b-dd39-467d-8d8c-6771eb2b12de
author: maggiesMSFT
ms.author: maggies
---
# Creating a Data Processing Extension Library
  Each [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] data processing extension you create should be assigned to a unique namespace and built into a library or assembly file. The exact name of the namespace is not important, but it must be unique and not shared with any other extension. [!INCLUDE[msCoName](../../../includes/msconame-md.md)] uses the namespace <xref:Microsoft.ReportingServices.DataProcessing> for the data processing extensions that ship with [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)]. You should create your own unique namespaces for your company's data processing extensions.  
  
 The following example shows the code to begin a [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] data processing extension, which uses the namespaces that contain the data processing interfaces and any utility classes.  
  
```vb  
Imports System  
Imports Microsoft.ReportingServices.DataProcessing  
Imports Microsoft.ReportingServices.Interfaces  
  
Namespace CompanyName.ExtensionName  
   ...  
```  
  
```csharp  
using System;  
using Microsoft.ReportingServices.DataProcessing;  
using Microsoft.ReportingServices.Interfaces;  
  
namespace CompanyName.ExtensionName  
{  
   ...  
```  
  
 When compiling a [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] data processing extension, you must supply to the compiler a reference to Microsoft.ReportingServices.Interfaces.dll, because the data processing extension interfaces are contained there. The <xref:Microsoft.ReportingServices.DataProcessing> namespace is needed to implement the data processing extension interfaces, and the <xref:Microsoft.ReportingServices.Interfaces> namespace is needed to implement the <xref:Microsoft.ReportingServices.Interfaces.IExtension> interface. For example, if all the files containing the code to implement a [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] data processing extension written in C# were in a single directory with the extension .cs, the following command would be issued from that directory to compile the files stored in CompanyName.ExtensionName.dll.  
  
```csharp  
csc /t:library /out:CompanyName.ExtensionName.dll *.cs /r:System.dll /r:Microsoft.ReportingServices.Interfaces.dll  
```  
  
 The following code example shows the command that would be used for [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[vbprvb](../../../includes/vbprvb-md.md)] files with the extension .vb.  
  
```vb  
vbc /t:library /out:CompanyName.ExtensionName.dll *.vb /r:System.dll /r:Microsoft.ReportingServices.Interfaces.dll  
```  
  
> [!NOTE]  
>  You can also design, develop, and build your data processing extension using [!INCLUDE[vsprvs](../../../includes/vsprvs-md.md)]. For more information about developing assemblies in [!INCLUDE[vsprvs](../../../includes/vsprvs-md.md)], see your [!INCLUDE[vsprvs](../../../includes/vsprvs-md.md)] documentation.  
  
## See Also  
 [Reporting Services Extensions](../../../reporting-services/extensions/reporting-services-extensions.md)   
 [Implementing a Data Processing Extension](../../../reporting-services/extensions/data-processing/implementing-a-data-processing-extension.md)   
 [Reporting Services Extension Library](../../../reporting-services/extensions/reporting-services-extension-library.md)  
  
  
