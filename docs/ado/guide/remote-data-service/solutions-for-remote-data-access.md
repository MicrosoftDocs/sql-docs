---
title: "Solutions for Remote Data Access"
description: "Solutions for Remote Data Access"
author: rothja
ms.author: jroth
ms.date: 11/09/2018
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "RDS [ADO]"
---
# Solutions for Remote Data Access
## The Issue  
 ADO enables your application to directly gain access to and modify data sources (sometimes called a two-tier system). For example, if your connection is to the data source that contains your data, that is a direct connection in a two-tier system.  
  
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](/dotnet/framework/wcf/).  
  
 However, you may want to access data sources indirectly through an intermediary such as Microsoft® Internet Information Services (IIS). This arrangement is sometimes called a three-tier system. IIS is a client/server system that provides an efficient way for a local, or client, application to invoke a remote, or server, program across the Internet or an intranet. The server program gains access to the data source and optionally processes the acquired data.  
  
 For example, your intranet Web page contains an application written in Microsoft® Visual Basic Scripting Edition (VBScript), which connects to IIS. IIS in turn connects to the actual data source, retrieves the data, processes it in some way, and then returns the processed information to your application.  
  
 In this example, your application never directly connected to the data source; IIS did. And IIS accessed the data by means of ADO.  
  
> [!NOTE]
>  The client/server application does not have to be based on the Internet or an intranet (that is, Web-based) - it could consist solely of compiled programs on a local area network. However, the typical case is a Web-based application.  
  
 Because some visual control, such as a grid, check box, or list, may use the returned information, the returned information must be easily used by a visual control.  
  
 You want a simple and efficient application-programming interface that supports three-tier systems, and returns information as easily as if it had been retrieved on a two-tier system. Remote Data Service (RDS) is this interface.  
  
## The Solution  
 RDS defines a programming model - the sequence of activities necessary to gain access to and update a data source - to gain access to data through an intermediary, such as Internet Information Services (IIS). The programming model summarizes the entire functionality of RDS.  
  
## See Also  
 [Basic RDS Programming Model](./basic-rds-programming-model.md)   
 [RDS Scenario](./rds-scenario.md)   
 [RDS Tutorial](./rds-tutorial.md)   
 [RDS Usage and Security](./rds-usage-and-security.md)