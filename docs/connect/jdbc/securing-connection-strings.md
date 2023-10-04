---
title: Securing connection strings
description: Learn how to secure connection string information when using the JDBC Driver for SQL Server.
author: David-Engel
ms.author: v-davidengel
ms.date: 03/31/2021
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---

# Securing connection strings

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

Protecting access to your data source is one of the most important goals of helping to secure an application. To limit access to your data source, you must take precautions to help secure connection information such as a user ID, password, and data source name. Storing a user ID and password in plain text, such as in your source code, presents a serious security issue. Even if you supply a compiled version of code that contains user ID and password information in an external source, your compiled code can potentially be disassembled and the user ID and password exposed. As a result, it's imperative that critical information such as a user ID and password not exist in your code.

## Recommendations

It's recommended to not store the password together with the connection URL in application source code. Instead, consider storing the password in a separate file that has restricted access. The access to that file can be granted to the context under which the application is running.

Another approach is to store the encrypted password in a file. Make sure you use an encryption API that doesn't require the key to be stored somewhere and isn't derived from the password of a user. For example, you might consider using certificate-based public/private key pairs, or use an approach where two parties use a key agreement protocol (Diffie-Hellman algorithm) to generate identical secret keys for encryption without ever having to transmit the secret key.

If you take connection string information from an external source, such as a user supplying a user ID and password, you must validate any input from the source to ensure that it follows the correct format and doesn't contain extra parameters that affect your connection.

## See also

[Securing JDBC driver applications](securing-jdbc-driver-applications.md)
