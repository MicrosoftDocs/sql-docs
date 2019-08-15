---
title: "CLR Integration Security in SQL Server"
ms.date: "08/15/2019"
ms.assetid: 489fe096-fd1d-42de-8438-bf7aed46aea2
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.topic: conceptual
author: MightyPen
ms.author: genemi
---
# CLR Integration Security in SQL Server

![Download-DownArrow-Circled](../../ssdt/media/download.png)[Download ADO.NET](../sql-connection-libraries.md#anchor-20-drivers-relational-access)

Microsoft SQL Server provides the integration of the common language runtime (CLR) component of the .NET Framework. CLR integration allows you to write stored procedures, triggers, user-defined types, user-defined functions, user-defined aggregates, and streaming table-valued functions, using any .NET Framework language, such as Microsoft Visual Basic .NET or Microsoft Visual C#.  
  
 The CLR supports a security model called code access security (CAS) for managed code. In this model, permissions are granted to assemblies based on evidence supplied by the code in metadata. SQL Server integrates the user-based security model of SQL Server with the code access-based security model of the CLR.  
  
## External Resources  
 For more information on CLR integration with SQL Server, see the following resources.  
  
|Resource|Description|  
|--------------|-----------------|  
|[Code Access Security](https://docs.microsoft.com/dotnet/framework/misc/code-access-security)|Contains topics describing CAS in the .NET Framework.|  
|[CLR Integration Security](/sql/relational-databases/clr-integration/security/clr-integration-security)|Discusses the security model for managed code executing inside of SQL Server.|  
  
## See also

- [Application Security Scenarios in SQL Server](../../connect/ado-net/application-security-scenarios-in-sql-server.md)
- [SQL Server Common Language Runtime Integration](../../connect/ado-net/sql-server-common-language-runtime-integration.md)
