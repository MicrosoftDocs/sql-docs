---
title: "Enabling a DLL to Run on DCOM | Microsoft Docs"
ms.prod: "sql-non-specified"
ms.technology:
  - "drivers"
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.suite: ""
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "DLL on DCOM in RDS [ADO]"
  - "DCOM in RDS [ADO]"
  - "business objects in RDS [ADO]"
ms.assetid: 5f1c2205-191c-4fb4-9bd9-84c878ea46ed
caps.latest.revision: 15
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# Enabling a DLL to Run on DCOM
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/en-us/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](http://go.microsoft.com/fwlink/?LinkId=199565).  
  
 The following steps outline how to enable a business object .dll to use both DCOM and Microsoft Internet Information Services (HTTP) via Component Services.  
  
1.  Create a new empty package in the Component Services MMC snap-in.  
  
     You will use the Component Services MMC snap-in to create a package and add the DLL into this package. This makes the .dll accessible through DCOM, but it removes the accessibility through IIS. (If you check in the registry for the .dll, the **Inproc** key is now empty; setting the Activation attribute, explained later in this topic, adds a value in the **Inproc** key.)  
  
2.  Install a business object into the package.  
  
     -or-  
  
     Import the [RDSServer.DataFactory](../../../ado/reference/rds-api/datafactory-object-rdsserver.md) object into the package.  
  
3.  Set the Activation attribute for the package to **In the creator's process** (Library application).  
  
     To make the .dll accessible through DCOM and IIS on the same computer, you must set the component's Activation attribute in the Component Services MMC snap-in. After you set the attribute to **In the creator's process**, you will notice that an **Inproc** server key in the registry has been added that points to a Component Services surrogate .dll.  
  
 For more information about Component Services (or Microsoft Transaction Service, if you are using Windows NT) and how to perform these steps, visit the Microsoft Transaction Server Web site.


