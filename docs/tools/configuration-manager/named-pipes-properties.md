---
title: "Named Pipes Properties"
description: Use the Protocol page on the Named Pipes Properties dialog box to view or change the named pipe that SQL Server listens to when using the Named Pipes protocol.
ms.custom: seo-lt-2019
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: tools-other
ms.topic: conceptual
helpviewer_keywords: 
  - "pipes [SQL Server]"
  - "listening [SQL Server], pipes"
  - "pipes [SQL Server], listening on pipes"
  - "Named Pipes [SQL Server], listening on pipes"
ms.assetid: a5fd5b8e-f889-485b-89e3-d4010ec4c6ec
author: markingmyname
ms.author: maghan
monikerRange: ">=sql-server-2016"
---
# Named Pipes Properties
[!INCLUDE [SQL Server Windows Only](../../includes/applies-to-version/sql-windows-only.md)]
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
 [Choosing a Network Protocol](/previous-versions/sql/sql-server-2016/ms187892(v=sql.130))   
 [Creating a Valid Connection String Using Named Pipes](/previous-versions/sql/sql-server-2016/ms189307(v=sql.130))  
  
