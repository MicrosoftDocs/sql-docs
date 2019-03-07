---
title: "Multithreaded Applications | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "threads [SQL Server], multithreaded applications"
  - "ODBC applications, multithreaded applications"
  - "asynchronous operations [SQL Server Native Client]"
  - "SQL Server Native Client ODBC driver, multithreaded applications"
  - "multithreaded applications [SQL Server Native Client]"
ms.assetid: d352c91a-6e08-4e50-9f3e-a37892d9c2cc
author: MightyPen
ms.author: genemi
manager: craigg
---
# Multithreaded Applications
  The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver is a multithreaded driver. Writing a multithreaded application is an alternative to using asynchronous calls to process multiple ODBC calls. A thread can make a synchronous ODBC call, and other threads can process while the first thread is blocked waiting for the response to its call. This model is more efficient than making asynchronous calls because it eliminates overhead such as network traffic and making repeated ODBC function calls testing for SQL_STILL_EXECUTING.  
  
 Asynchronous mode is still an effective method of processing. The performance improvements of a multithreaded model are not enough to justify rewriting asynchronous applications. If users are converting DB-Library applications that use the DB-Library asynchronous model, it is easier to convert them to the ODBC asynchronous model.  
  
## See Also  
 [Creating a SQL Server Native Client ODBC Driver Application](creating-a-driver-application.md)  
  
  
