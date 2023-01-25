---
title: "Client Protocols - Named Pipes Properties (Protocol Tab)"
description: Find out how to view or modify the description of the default pipe in SQL Server Configuration Manager. Learn how to connect to a different pipe.
ms.service: sql
ms.reviewer: ""
ms.subservice: tools-other
ms.topic: conceptual
helpviewer_keywords: 
  - "pipes [SQL Server], connecting to"
  - "Named Pipes [SQL Server], default pipe"
  - "client protocols [SQL Server]"
author: markingmyname
ms.author: maghan
ms.custom: seo-lt-2019
ms.date: "03/14/2017"
monikerRange: ">=sql-server-2016"
---

# Client Protocols - Named Pipes Properties (Protocol Tab)

[!INCLUDE [SQL Server - Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

In [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager use the **Protocol** tab on the **Named Pipes Properties** dialog box to view or modify the description of default pipe. To connect to a different pipe, type the pipe in the **Default Pipe** box. For more information about connection strings, see [Creating a Valid Connection String Using Named Pipes](/previous-versions/sql/sql-server-2016/ms189307(v=sql.130)).  
  
## Options  

**Default Pipe**  

Specifies the default pipe the Named Pipes Net-library will use to attempt to connect to the target instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. By default, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] listens on: `\\.\pipe\sql\query`  
  
To connect to the default pipe, enter `sql\query`  
  
**Enabled**  
Possible values are **Yes** and **No**.  
  
## See Also

- [Choosing a Network Protocol](/previous-versions/sql/sql-server-2016/ms187892(v=sql.130))
