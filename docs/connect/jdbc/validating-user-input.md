---
title: Validating user input
description: Learn why validating user input is critical to securing your application from SQL injection attacks.
author: David-Engel
ms.author: v-davidengel
ms.date: 03/31/2021
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---

# Validating user input

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

When you construct an application that accesses data, you should assume all user input to be malicious until proven otherwise. Failure to do so can leave your application vulnerable to attack. One type of attack that can occur is called SQL injection. This attack is where malicious code is added to strings that are passed to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to be parsed and run. To avoid this type of attack, you should use stored procedures with parameters where possible, and always validate user input.

Validating user input in client code is important so that you don't waste round trips to the server. It's equally important to validate parameters to stored procedures on the server. That way input is caught that bypasses client-side validation.

For more information about SQL injection and how to avoid it, see [SQL injection](../../relational-databases/security/sql-injection.md). For more information about validating stored procedure parameters, see [Stored procedures](../../relational-databases/stored-procedures/stored-procedures-database-engine.md) and related articles.

## See also

[Securing JDBC driver applications](securing-jdbc-driver-applications.md)
