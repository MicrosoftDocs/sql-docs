---
title: "Data Access from CLR Database Objects | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: clr
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
ms.assetid: 9a0f4dee-71c1-42e9-a85e-52382807010f
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# Data Access from CLR Database Objects
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  A common language runtime (CLR) routine may easily access data stored in the instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] in which it runs, as well as data stored in remote instances. Which particular data the routine can access is determined by the user context in which the code is running. Access data from within a CLR database object by using the .NET Framework Data Provider for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], also referred to as **SqlClient**. This is the same provider used by developers accessing [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data from managed client and middle-tier applications. Because of this, you can leverage your knowledge of ADO.NET and **SqlClient** in client and middle-tier applications.  
  
> [!NOTE]  
>  User-defined type methods and user-defined functions are not allowed to perform data access by default. You must set the **DataAccess** property of **SqlMethodAttribute** or **SqlFunctionAttribute** to **DataAccessKind.Read** to enable read-only data access from user-defined type (UDT) methods or user-defined functions. Data modification operations are not allowed from UDTs or user-defined functions, and throws exceptions at execution time if attempted.  
  
 This section discusses only the specific functional and behavioral differences when accessing data from within a CLR database object. For more information about the features and functionality of ADO.NET, see the ADO.NET documentation included in the .NET Framework SDK.  
  
 The following table lists the topics in this section.  
  
 [Context Connection](../../../relational-databases/clr-integration/data-access/context-connection.md)  
 Describes the context connection to SQL Server.  
  
 [Impersonation and Credentials for Connections](../../../relational-databases/clr-integration/data-access/impersonation-and-credentials-for-connections.md)  
 Describes impersonating connections and connection credentials.  
  
 [SQL Server In-Process Specific Extensions to ADO.NET](../../../relational-databases/clr-integration-data-access-in-process-ado-net/sql-server-in-process-specific-extensions-to-ado-net.md)  
 Discusses the in-process specific **SqlPipe**, **SqlContext**, **SqlTriggerContext**, and **SqlDataRecord** objects.  
  
 [CLR Integration and Transactions](../../../relational-databases/clr-integration-data-access-transactions/clr-integration-and-transactions.md)  
 Describes how the new transaction framework provided in the System.Transactions namespace integrates with ADO.NET and [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] CLR integration.  
  
 [XML Serialization from CLR Database Objects](https://msdn.microsoft.com/library/ac84339b-9384-4710-bebc-01607864a344)  
 Explains how to enable XML serialization scenarios of CLR database objects inside [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
  
