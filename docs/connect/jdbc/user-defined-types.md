---
title: "User Defined Types | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 19a71b27-b788-43a3-a76d-fe3001a6f016
author: MightyPen
ms.author: genemi
manager: craigg
---

# User Defined Types

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

User-defined types (UDTs) were introduced in [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] to allow a developer to extend the server's scalar type system by storing common language runtime (CLR) objects in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database. UDTs can contain multiple elements and can have behaviors, unlike the traditional alias data types, that consist of a single [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] system data type. Previously, UDTs were restricted to a maximum size of 8 kilobytes. In [!INCLUDE[ssKatmai](../../includes/sskatmai_md.md)], support was added for UDTs larger than 64 kilobytes. Beginning in version 3.0, the JDBC Driver also supports UDTs larger than 64 kilobytes when you specify the UserDefined format.

There is no behavior change for UDTs that are less than or equal to 8,000 bytes, but larger UDTs are supported and report their size as "unlimited".

## See Also

[Understanding the JDBC Driver Data Types](../../connect/jdbc/understanding-the-jdbc-driver-data-types.md)
