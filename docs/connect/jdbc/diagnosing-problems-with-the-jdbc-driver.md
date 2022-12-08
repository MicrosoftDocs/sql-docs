---
title: Diagnosing problems with the JDBC driver
description: Learn how to diagnose and troubleshoot problems like error handling, checking the driver version and tracing.
author: David-Engel
ms.author: v-davidengel
ms.date: 08/12/2019
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Diagnosing problems with the JDBC driver

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

No matter how well an application is designed and developed, problems will inevitably occur. When they do, it's important to have some techniques for diagnosing those problems. When using the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)], some common problems that can occur aren't having the right driver version or being unable to connect to a database.

The articles in this section discuss many techniques for diagnosing these and other problems including error handling, checking the driver version, tracing, and troubleshooting connectivity issues.

## In this section

|Article|Description|
|-----------|-----------------|
|[Handling errors](handling-errors.md)|Describes how to handle errors that are returned from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|
|[Getting the driver version](getting-the-driver-version.md)|Describes how to determine which version of the JDBC driver is installed.|
|[Tracing driver operation](tracing-driver-operation.md)|Describes how to enable tracing when using the JDBC driver.|
|[Troubleshooting connectivity](troubleshooting-connectivity.md)|Describes how to troubleshoot database connectivity.|
|[Accessing diagnostic information in the extended events log](accessing-diagnostic-information-in-the-extended-events-log.md)|Describes how to use information in the server's extended events log to understand connection failures.|

## See also

[Overview of the JDBC driver](overview-of-the-jdbc-driver.md)
