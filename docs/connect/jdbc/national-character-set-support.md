---
title: National character set support
description: Learn how to support national character set conversions in the Microsoft JDBC Driver for SQL Server.
author: David-Engel
ms.author: v-davidengel
ms.date: "08/12/2019"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# National character set support

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

The JDBC driver provides support for the JDBC 4.0 API, which includes new national character set conversion API methods. This support includes new setter, getter, and updater methods for **NCHAR**, **NVARCHAR**, **LONGNVARCHAR**, and **NCLOB** JDBC types.

The following list shows new getter, setter, and updater methods to support the national character set conversion:

- [SQLServerPreparedStatement](reference/sqlserverpreparedstatement-class.md): [setNString](reference/setnstring-method-int-java-lang-string.md), [setNCharacterStream](reference/setncharacterstream-method-sqlserverpreparedstatement.md), [setNClob](reference/setnclob-method-sqlserverpreparedstatement.md).

- [SQLServerCallableStatement](reference/sqlservercallablestatement-class.md): [getNClob](reference/getnclob-method-sqlservercallablestatement.md), [getNString](reference/getnstring-method-sqlservercallablestatement.md), [getNCharacterStream](reference/getncharacterstream-method-sqlservercallablestatement.md), [setNString](reference/setnstring-method-sqlservercallablestatement.md), [setNCharacterStream](reference/setncharacterstream-method-sqlservercallablestatement.md), [setNClob](reference/setnclob-method-sqlservercallablestatement.md).

- [SQLServerResultSet](reference/sqlserverresultset-class.md): [getNClob](reference/getnclob-method-sqlserverresultset.md), [getNString](reference/getnstring-method-sqlserverresultset.md), [getNCharacterStream](reference/getncharacterstream-method-sqlserverresultset.md), [updateNClob](reference/updatenclob-method-sqlserverresultset.md), [updateNString](reference/updatenstring-method-sqlserverresultset.md), [updateNCharacterStream](reference/updatencharacterstream-method-sqlserverresultset.md).

> [!NOTE]
> You must set the classpath to include the sqljdbc4.jar file to use these methods in your application.

To send String parameters to the server in Unicode format, the applications should either use the new JDBC 4.0 national character methods; or set the **sendStringParametersAsUnicode** connection property to "**true**" when using the non-national character methods. The recommended way is to use the new JDBC 4.0 national character methods where possible. For more information about the **sendStringParametersAsUnicode** connection property, see [Setting the Connection Properties](setting-the-connection-properties.md).

## See also

[Understanding the JDBC driver data types](understanding-the-jdbc-driver-data-types.md)
