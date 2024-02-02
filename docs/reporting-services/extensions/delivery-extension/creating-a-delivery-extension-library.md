---
title: "Create a delivery extension library"
description: Find out how to assign a delivery extension that you create in Reporting Services to a unique namespace and build it into a library or assembly file.
author: maggiesMSFT
ms.author: maggies
ms.date: 03/14/2017
ms.service: reporting-services
ms.subservice: extensions
ms.topic: reference
ms.custom: updatefrequency5
helpviewer_keywords:
  - "delivery extensions [Reporting Services], namespace assignments"
  - "library [Reporting Services]"
  - "assigning namespaces to extensions"
---
# Create a delivery extension library
  Each [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] delivery extension you create should be assigned to a unique namespace and built into a library or assembly file. The exact name of the namespace isn't important, but it must be unique and not shared with any other extension. You should create your own unique namespaces for your company's delivery extensions.  
  
 The following example shows the code to begin a [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] delivery extension, which uses the namespaces that contain the delivery interfaces and any utility classes.  
  
```vb  
Imports System  
Imports Microsoft.ReportingServices.Interfaces  
  
Namespace CompanyName.ExtensionName  
   ...  
```  
  
```csharp  
using System;  
using Microsoft.ReportingServices.Interfaces;  
  
namespace CompanyName.ExtensionName  
{  
   ...  
```  
  
 When compiling a [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] delivery extension, you must supply to the compiler a reference to Microsoft.ReportingServices.Interfaces.dll, because the delivery extension interfaces and classes are contained there. The <xref:Microsoft.ReportingServices.Interfaces> namespace is needed to implement the <xref:Microsoft.ReportingServices.Interfaces.IExtension> interface, the <xref:Microsoft.ReportingServices.Interfaces.IDeliveryExtension> interface, and more. For example, if all the files containing the code to implement a [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] delivery extension written in C# were in a single directory with the extension .cs, the following command would be issued from that directory to compile the files stored in CompanyName.ExtensionName.dll.  
  
```csharp  
csc /t:library /out:CompanyName.ExtensionName.dll *.cs /r:System.dll   
/r:Microsoft.ReportingServices.Interfaces.dll  
```  
  
 The following code example shows the command that would be used for [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[visual-basic](../../../includes/visual-basic-md.md)] files with the extension .vb.  
  
```vb  
vbc /t:library /out:CompanyName.ExtensionName.dll *.vb /r:System.dll   
/r:Microsoft.ReportingServices.Interfaces.dll  
```  
  
> [!NOTE]  
>  You can also design, develop, and build your delivery extension using [!INCLUDE[vsprvs](../../../includes/vsprvs-md.md)]. For more information about developing assemblies in [!INCLUDE[vsprvs](../../../includes/vsprvs-md.md)], see your [!INCLUDE[vsprvs](../../../includes/vsprvs-md.md)] documentation.  
  
## Related content

- [Reporting Services extensions](../../../reporting-services/extensions/reporting-services-extensions.md)   
- [Implement a delivery extension](../../../reporting-services/extensions/delivery-extension/implementing-a-delivery-extension.md)   
- [Reporting Services extension library](../../../reporting-services/extensions/reporting-services-extension-library.md)  
  
  
