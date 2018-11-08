---
title: "MSSQLSERVER_17194 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
f1_keywords: 
  - "17194"
helpviewer_keywords: 
  - "17194 (Database Engine error)"
ms.assetid: 0d03eb20-28a7-4ceb-8903-7f9420a620f7
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_17194
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|17194|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name||  
|Message Text|The server was unable to load the SSL provider library needed to log in; the connection has been closed. SSL is used to encrypt either the login sequence or all communications, depending on how the administrator has configured the server. See Books Online for information on this error message:  0xXXXX. [CLIENT: 11.11.11.11]|  
  
## Explanation  
 This error indicates that the client has closed the connection. This error could occur because the connection time-out has expired. The error message displays a value from the operating system that describes the underlying problem.  
  
## User Action  
 If the error code in the message is 0x2746 (decimal value 10054), this means that the connection was reset by the client, typically because of a time-out. To resolve the error, increase the connection time-out on the client or calling program.  
  
 To determine a possible solution for other error message values, use the operating system command **net helpmsg** and specify the decimal value of the error code.  
  
 For more information about how to connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Server Network Configuration](../../database-engine/configure-windows/server-network-configuration.md) and [Client Network Configuration](../../database-engine/configure-windows/client-network-configuration.md).  
  
## Internal-Only  
  
