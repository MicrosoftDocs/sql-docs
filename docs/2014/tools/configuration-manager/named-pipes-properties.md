---
title: "Named Pipes Properties | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "pipes [SQL Server]"
  - "listening [SQL Server], pipes"
  - "pipes [SQL Server], listening on pipes"
  - "Named Pipes [SQL Server], listening on pipes"
ms.assetid: a5fd5b8e-f889-485b-89e3-d4010ec4c6ec
author: stevestein
ms.author: sstein
manager: craigg
---
# Named Pipes Properties
  Use the **Protocol**page on the **Named Pipes Properties** dialog box to view or change the named pipe that [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] listens to, when using the Named Pipes protocol.  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] must be restarted to enable or disable the protocol, or change the named pipe.  
  
## Options  
 **Enabled**  
 Possible values are **Yes** and **No**.  
  
 **Pipe Name**  
 Specifies the named pipe on which [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] listens. By default, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] listens on: `\\.\pipe\sql\query` for the default instance and `\\.\pipe\MSSQL$<instancename>\sql\query` for a named instance. This field is limited to 2047 characters.  
  
## Creating an Alternate Named Pipe  
 To change the named pipe, type the new pipe name in the **Pipe Name** box and then stop and restart [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Since **sql\query** is well known as the named pipe used by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], changing the pipe can help reduce the risk of attack by malicious programs.  
  
### Example  
 Type **\\\\.\pipe\unit\app** to listen on the **unit\app** pipe.  
  
 Type **\\\\.\pipe\acct** to listen on the **acct** pipe.  
  
## See Also  
 [Enable or Disable a Server Network Protocol](../../database-engine/configure-windows/enable-or-disable-a-server-network-protocol.md)   
 [Choosing a Network Protocol](../../../2014/tools/configuration-manager/choosing-a-network-protocol.md)   
 [Creating a Valid Connection String Using Named Pipes](../../../2014/tools/configuration-manager/creating-a-valid-connection-string-using-named-pipes.md)  
  
  
