---
title: Data Access From CLR Database Objects
description: CLR routines can access data from within a CLR database object by using the .NET Framework Data Provider for SQL Server, also referred to as SqlClient.
author: uc-msft
ms.author: umajay
ms.date: 12/27/2024
ms.service: sql
ms.subservice: clr
ms.topic: "reference"
helpviewer_keywords:
  - "common language runtime [SQL Server], data access"
  - "routines [CLR integration]"
  - "data access [CLR integration]"
  - "ADO.NET [CLR integration]"
  - "internal data access [CLR integration]"
  - "common language runtime [SQL Server], ADO.NET"
  - "database objects [CLR integration], data access"
  - "managed code [SQL Server], database objects"
  - ".NET Data Access Provider for SQL Server [CLR integration]"
  - "managed code [SQL Server], data access"
  - "SqlClient provider"
  - "in-process data access providers [CLR integration]"
---
# Data access from CLR database objects

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

A common language runtime (CLR) routine might easily access data stored in the instance of [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] in which it runs, and data stored in remote instances. The user context in which the code runs, determines the particular data the routine can access. Access data from within a CLR database object by using the .NET Framework Data Provider for [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)], also referred to as `SqlClient`. This is the same provider used by developers accessing [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] data from managed client and middle-tier applications. Because of this, you can use your knowledge of ADO.NET and `SqlClient` in client and middle-tier applications.

User-defined type methods and user-defined functions aren't allowed to perform data access by default. You must set the `DataAccess` property of `SqlMethodAttribute` or `SqlFunctionAttribute` to `DataAccessKind.Read` to enable read-only data access from user-defined type (UDT) methods or user-defined functions. Data modification operations aren't allowed from UDTs or user-defined functions, and throw exceptions at execution time if attempted.

This section discusses only the specific functional and behavioral differences when accessing data from within a CLR database object. For more information about the features and functionality of ADO.NET, see the ADO.NET documentation included in the .NET Framework SDK.

The following table lists the articles in this section.

| Article | Description |
| --- | --- |
| [Context connection](context-connection.md) | Describes the context connection to SQL Server. |
| [Impersonation and credentials for connections](impersonation-and-credentials-for-connections.md) | Describes impersonating connections and connection credentials. |
| [SQL Server in-process specific extensions to ADO.NET](../../clr-integration-data-access-in-process-ado-net/sql-server-in-process-specific-extensions-to-ado-net.md) | Discusses the in-process specific `SqlPipe`, `SqlContext`, `SqlTriggerContext`, and `SqlDataRecord` objects. |
| [CLR integration and transactions](../../clr-integration-data-access-transactions/clr-integration-and-transactions.md) | Describes how the new transaction framework provided in the System.Transactions namespace integrates with ADO.NET and [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] CLR integration. |
| [XML Serialization from CLR Database Objects](/dotnet/standard/serialization/introducing-xml-serialization) | Explains how to enable XML serialization scenarios of CLR database objects inside [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)]. |
