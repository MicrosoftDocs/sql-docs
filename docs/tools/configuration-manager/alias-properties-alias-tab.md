---
title: Alias Properties (Alias Tab)
description: Use the Alias tab of the Properties dialog box to configure an alias so that you can use an alternate name when connecting to an instance of SQL Server.
ms.service: sql
ms.reviewer: ""
ms.subservice: tools-other
ms.topic: conceptual
ms.assetid: 2d1498e2-129c-4ce7-88e5-408e4037243c
author: markingmyname
ms.author: maghan
ms.custom: seo-lt-2019
ms.date: 03/14/2017
monikerRange: ">=sql-server-2016"
---

# &lt;Alias&gt; Properties (Alias Tab)

[!INCLUDE [SQL Server Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

An alias is an alternate name that can be used to make a connection. The alias encapsulates the required elements of a connection string, and exposes them with a name chosen by the user. Use the **Alias** page on the **\<**Alias name**> Properties** dialog box to view or specify the elements of connection string of an Alias.

## Options

**Alias Name**

The name (alias) that you want to use to refer to this connection.  

**Pipe Name** / **Port No**  

Additional elements of the connection string. The name of this box varies with the selected protocol. See the topics listed below for examples.  

**Protocol**

The protocol used for the connection.

**Server**

The name of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance being connected to.  

## See Also

- [Creating a Valid Connection String Using Shared Memory Protocol](../../tools/configuration-manager/creating-a-valid-connection-string-using-shared-memory-protocol.md)
- [Creating a Valid Connection String Using TCP IP](../../tools/configuration-manager/creating-a-valid-connection-string-using-tcp-ip.md)
- [Creating a Valid Connection String Using Named Pipes](/previous-versions/sql/sql-server-2016/ms189307(v=sql.130))