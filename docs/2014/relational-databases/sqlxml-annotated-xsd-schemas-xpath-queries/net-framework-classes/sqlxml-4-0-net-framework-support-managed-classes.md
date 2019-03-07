---
title: "SQLXML Managed Classes | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: xml
ms.topic: "reference"
helpviewer_keywords: 
  - ".NET Framework [SQLXML], Managed Classes"
  - "SQL Server .NET Data Provider"
  - "Managed Classes [SQLXML], about managed classes"
  - "providers [SQLXML], SQL Server .NET Data Provider"
  - "data providers [SQLXML], SQL Server .NET Data Provider"
  - "Managed Classes [SQLXML]"
  - "XML [SQLXML]"
  - "SQLXML Managed Classes"
  - "providers [SQLXML], SQLXML Managed Classes"
  - "data providers [SQLXML], SQLXML Managed Classes"
  - "SQLXML, Managed Classes"
ms.assetid: 73a5faeb-dabf-4895-acb5-a9651b646065
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# SQLXML Managed Classes
  [!INCLUDE[msCoName](../../../includes/msconame-md.md)] SQLXML Managed Classes exposes the functionality of SQLXML 4.0 inside the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] .NET Framework. With SQLXML Managed Classes, you can write a C# application to access XML data from an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], bring the data into the .NET Framework environment, process the data, and send the updates back to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] as a DiffGram to apply the updates. You must use a mapping schema when applying updates to a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database using SQLXML Managed Classes. For a working sample, see [Accessing SQLXML Functionality in the .NET Environment](accessing-sqlxml-functionality-in-the-net-environment.md).  
  
 To use the SQLXML Managed Classes with SQLXML 4.0, you must install Microsoft Visual Studio.  
  
> [!NOTE]  
>  The .NET Framework includes the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] .NET Data Provider. This provider can be used to access [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] from the .NET environment; however, it can handle only traditional SQL queries (that is, relational database queries with the exception of FOR XML queries). You cannot execute XML templates or the server-side XPath queries in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
## In This Section  
 [SQLXML Managed Classes Object Model](../../../database-engine/dev-guide/sqlxml-managed-classes-object-model.md)  
 Documents SQLXML Managed Classes and their properties and methods.  
  
 [Using the SQLXML Managed Classes](sqlxml-4-0-net-framework-support-managed-classes.md)  
 Provides examples of using SQLXML Managed Classes.  
  
  
