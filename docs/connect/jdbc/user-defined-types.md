---
title: "User defined types"
description: "User defined types"
author: David-Engel
ms.author: v-davidengel
ms.date: "08/12/2019"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---

# User defined types

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

User-defined types (UDTs) were introduced in [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] to allow a developer to extend the server's scalar type system by storing common language runtime (CLR) objects in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database. UDTs can contain multiple elements and can have behaviors, unlike the traditional alias data types, that consist of a single [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] system data type. Previously, UDTs were restricted to a maximum size of 8 kilobytes. In [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], support was added for UDTs larger than 64 kilobytes. Beginning in version 3.0, the JDBC Driver also supports UDTs larger than 64 kilobytes when you specify the UserDefined format.

There is no behavior change for UDTs that are less than or equal to 8,000 bytes, but larger UDTs are supported and report their size as "unlimited".

## See also

[Understanding the JDBC driver data types](../../connect/jdbc/understanding-the-jdbc-driver-data-types.md)
