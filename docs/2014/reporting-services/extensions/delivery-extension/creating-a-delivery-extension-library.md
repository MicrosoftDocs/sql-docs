---
title: "Creating a Delivery Extension Library | Microsoft Docs"
ms.custom: ""
ms.date: "04/27/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: reporting-services
ms.topic: "reference"
helpviewer_keywords: 
  - "delivery extensions [Reporting Services], namespace assignments"
  - "library [Reporting Services]"
  - "assigning namespaces to extensions"
ms.assetid: 63b32f93-4bab-4b07-bd72-39a47aca1cda
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Creating a Delivery Extension Library
  Each [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] delivery extension you create should be assigned to a unique namespace and built into a library or assembly file. The exact name of the namespace is not important, but it must be unique and not shared with any other extension. You should create your own unique namespaces for your company's delivery extensions.  
  
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
  
 The following code example shows the command that would be used for [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[vbprvb](../../../includes/vbprvb-md.md)] files with the extension .vb.  
  
```vb  
vbc /t:library /out:CompanyName.ExtensionName.dll *.vb /r:System.dll   
/r:Microsoft.ReportingServices.Interfaces.dll  
```  
  
> [!NOTE]  
>  You can also design, develop, and build your delivery extension using [!INCLUDE[vsprvs](../../../includes/vsprvs-md.md)]. For more information about developing assemblies in [!INCLUDE[vsprvs](../../../includes/vsprvs-md.md)], see your [!INCLUDE[vsprvs](../../../includes/vsprvs-md.md)] documentation.  
  
## See Also  
 [Reporting Services Extensions](../reporting-services-extensions.md)   
 [Implementing a Delivery Extension](implementing-a-delivery-extension.md)   
 [Reporting Services Extension Library](../reporting-services-extension-library.md)  
  
  
