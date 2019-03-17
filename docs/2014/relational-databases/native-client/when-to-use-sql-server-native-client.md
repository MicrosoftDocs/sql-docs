---
title: "When to Use SQL Server Native Client | Microsoft Docs"
ms.custom: ""
ms.date: "03/09/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "SQL Server Native Client ODBC driver, about SQL Server Native Client ODBC driver"
  - "SQLNCLI, about SQL Server Native Client"
  - "data access [SQL Server Native Client], about SQL Server Native Client"
ms.assetid: 08f18b36-209d-4cf7-9623-ebc61859a91d
author: MightyPen
ms.author: genemi
manager: craigg
---
# When to Use SQL Server Native Client
  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client is one technology that you can use to access data in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database.  For a discussion of the different data-access technologies, see [Data Access Technologies Road Map](https://go.microsoft.com/fwlink/?LinkID=179186)  
  
 When deciding whether to use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client as the data access technology of your application, you should consider several factors.  
  
 For new applications, if you're using a managed programming language such as Microsoft Visual C# or Visual Basic, and you need to access the new features in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you should use the .NET Framework Data Provider for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], which is part of the .NET Framework.  
  
 If you are developing a COM-based application and need to access the new features introduced in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you should use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client. If you don't need access to the new features of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you can continue to use Windows Data Access Components (WDAC).  
  
 For existing OLE DB and ODBC applications, the primary issue is whether you need to access the new features of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. If you have a mature application that does not need the new features of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you can continue to use WDAC. But if you do need to access those new features, such as the [xml data type](/sql/t-sql/xml/xml-transact-sql), you should use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client.  
  
 Both [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client and MDAC support read committed transaction isolation using row versioning, but only [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client supports snapshot transaction isolation. (In programming terms, read committed transaction isolation with row versioning is the same as Read-Committed transaction.)  
  
 For information about the differences between [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client and MDAC, see [Updating an Application to SQL Server Native Client from MDAC](../../relational-databases/native-client/applications/updating-an-application-to-sql-server-native-client-from-mdac.md).  
  
## See Also  
 [SQL Server Native Client Programming](../../relational-databases/native-client/sql-server-native-client-programming.md)   
 [ODBC How-to Topics](../native-client-odbc-how-to/odbc-how-to-topics.md)   
 [OLE DB How-to Topics](../native-client-ole-db-how-to/ole-db-how-to-topics.md)  
  
  
