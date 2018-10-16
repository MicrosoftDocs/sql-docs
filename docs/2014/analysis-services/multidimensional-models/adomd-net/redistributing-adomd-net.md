---
title: "Redistributing ADOMD.NET | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
helpviewer_keywords: 
  - "ADOMD.NET, redistributing"
  - "redistributing ADOMD.NET"
ms.assetid: f8db3c99-0243-4b92-b486-0d8786c264f4
author: minewiskan
ms.author: owend
manager: craigg
---
# Redistributing ADOMD.NET
  When you write applications that use ADOMD.NET, you must redistribute the appropriate version of ADOMD.NET along with your application. To redistribute ADOMD.NET, include the ADOMD.NET setup program in your application's setup program.  
  
 The ADOMD.NET setup program, as well as the latest version of ADOMD.NET, can be found on the Microsoft Download Center as part of the SQL Server Feature Pack.  
  
 The ADOMD.NET setup program installs the ADOMD.NET files in \<*system drive*>:\Program Files\Microsoft.NET\ADOMD.NET\\*version number*.  
  
 After you include the ADOMD.NET setup program, have your application's setup program launch the ADOMD.NET setup program and install ADOMD.NET. Additionally, depending on your environment, you might need to ensure that related assemblies are trusted by SQL Server.  
  
 For more information:  
  
 [Feature Pack for Microsoft SQL Server](http://go.microsoft.com/fwlink/?LinkId=389949)  
  
 [Microsoft Connect: ADOMD.NET Dependencies](http://go.microsoft.com/fwlink/?LinkId=389950)  
  
## See Also  
 [ADOMD.NET Client Programming](../../multidimensional-models-adomd-net-client/adomd-net-client-programming.md)   
 [ADOMD.NET Server Programming](../../multidimensional-models-adomd-net-server/adomd-net-server-programming.md)  
  
  
