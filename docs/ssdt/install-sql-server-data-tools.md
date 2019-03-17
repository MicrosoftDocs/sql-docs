---
title: "Install SQL Server Data Tools | Microsoft Docs"
ms.custom: 
  - "SSDT"
ms.date: "02/09/2017"
ms.prod: "sql"
ms.technology: ssdt
ms.reviewer: ""
ms.topic: conceptual
f1_keywords: 
  - "sql.data.tools.package.stub"
ms.assetid: 6f8616cb-9119-42c3-a9b1-936e088763e7
author: "stevestein"
ms.author: "sstein"
manager: "craigg"
---
# Install SQL Server Data Tools
This topic describes how to install SQL Server Data Tools. Updates to SQL Server Data Tools are available on the SQL Server Data Tools download page ([Install Latest SQL Server Data Tools](https://go.microsoft.com/fwlink/?LinkID=616714)).  
  
Whether you are using Visual Studio 2012 or Visual Studio 2013, install the latest SQL Server Data Tools updates to get the newest features.  
  
## SQL Server Data Tools for Visual Studio 2013  
SQL Server Data Tools ships in Visual Studio 2013 Express for Web, Visual Studio 2013 Express for Desktop, Visual Studio Community 2013, and all paid SKUs including the Professional SKU and higher. After you install Visual Studio 2013, you can go to Tools -> Extensions and Updates -> Updates to learn if there is an update to install.  
  
## SQL Server Data Tools for Visual Studio 2012  
SQL Server Data Tools ships in the Professional SKU, or higher, in Visual Studio 2012. After you install Visual Studio 2012 and install the November 2012, or later, SQL Server Data Tools update, SQL Server Data Tools can report when there is an update to install. For more information, see [Check for Updates Dialog Box](../ssdt/check-for-updates-dialog-box.md).  
  
If Visual Studio 2012 is not installed, SQL Server Data Tools will install the Visual Studio 2012 Integrated Shell and install SQL Server Data Tools.  
  
## Administrative Installation Point  
If you need to install SQL Server Data Tools on multiple computers or computers with no internet access, you can create an administrative installation layout on either a network share or a physical medium and then install from that installation point.  
  
To create an installation layout, download SSDTSetup.EXE and run it with the `/layout`*location* option to create a layout at *location*. Users can then run SSDTSetup.Exe from *location*.  
  
Download SSDTSetup.exe by going to [Install Latest SQL Server Data Tools](https://go.microsoft.com/fwlink/?LinkID=616714), click the link for your version of Visual Studio, and then download SSDTSetup.exe for your language.  
  
