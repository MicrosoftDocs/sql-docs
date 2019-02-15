---
title: "Validating User Input | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 8aa867b0-e6f0-49eb-93d3-817ae2ed8f77
author: MightyPen
ms.author: genemi
manager: craigg
---

# Validating User Input

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

When you construct an application that accesses data, you should assume all user input to be malicious until proven otherwise. Failure to do this can leave your application vulnerable to attack. One type of attack that can occur is called SQL injection, where malicious code is added to strings that are later passed to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to be parsed and run. To avoid this type of attack, you should use stored procedures with parameters where possible, and always validate user input.

Validating user input in client code is important so that you do not waste round trips to the server. It is equally important to validate parameters to stored procedures on the server to catch input that is not valid and that bypasses client-side validation.

For more information about SQL injection and how to avoid it, see "SQL Injection" in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online. For more information about validating stored procedure parameters, see "Stored Procedures ( [!INCLUDE[ssDE](../../includes/ssde_md.md)])" and subordinate topics in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.

## See Also

[Securing JDBC Driver Applications](../../connect/jdbc/securing-jdbc-driver-applications.md)
