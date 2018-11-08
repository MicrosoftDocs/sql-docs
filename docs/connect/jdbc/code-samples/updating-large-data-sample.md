---
title: "Updating Large Data Sample | Microsoft Docs"
ms.custom: ""
ms.date: "07/31/2018"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 76ecc05f-a77d-40a2-bab9-91a7fcf17347
author: MightyPen
ms.author: genemi
manager: craigg
---

# Updating Large Data Sample

[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

This [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)] sample application demonstrates how to update a large column in a database.

The code file for this sample is named UpdateLargeData.java, and can be found in the following location:

```bash
\<installation directory>\sqljdbc_<version>\<language>\samples\adaptive
```

## Requirements

To run this sample application, you'll need access to the [!INCLUDE[ssSampleDBnormal](../../../includes/sssampledbnormal_md.md)] sample database. Also set the classpath to include the sqljdbc4.jar file. If the classpath is missing an entry for sqljdbc4.jar, the sample application will throw the common "Class not found" exception. For more information about how to set the classpath, see [Using the JDBC Driver](../../../connect/jdbc/using-the-jdbc-driver.md).

> [!NOTE]  
> The [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)] provides sqljdbc.jar, sqljdbc4.jar, sqljdbc41.jar, or sqljdbc42.jar class library files to be used depending on your preferred Java Runtime Environment (JRE) settings. This sample uses the [isWrapperFor](../../../connect/jdbc/reference/iswrapperfor-method-sqlserverstatement.md) and [unwrap](../../../connect/jdbc/reference/unwrap-method-sqlserverstatement.md) methods, which are introduced in the JDBC 4.0 API, to access the driver-specific response buffering methods. In order to compile and run this sample, you will need sqljdbc4.jar class library, which provides support for JDBC 4.0. For more information about which JAR file to choose, see [System Requirements for the JDBC Driver](../../../connect/jdbc/system-requirements-for-the-jdbc-driver.md).

## Example

In the following example, the sample code makes a connection to the [!INCLUDE[ssSampleDBnormal](../../../includes/sssampledbnormal_md.md)] database. Then, the sample code creates a Statement object and uses the [isWrapperFor](../../../connect/jdbc/reference/iswrapperfor-method-sqlserverstatement.md) method to check whether the Statement object is a wrapper for the specified [SQLServerStatement](../../../connect/jdbc/reference/sqlserverstatement-class.md) class. The [unwrap](../../../connect/jdbc/reference/unwrap-method-sqlserverstatement.md) method is used to access the driver-specific response buffering methods.

Next, the sample code sets the response buffering mode as "**adaptive**" by using the [setResponseBuffering](../../../connect/jdbc/reference/setresponsebuffering-method-sqlserverstatement.md) method of the [SQLServerStatement](../../../connect/jdbc/reference/sqlserverstatement-class.md) class and also demonstrates how to get the adaptive buffering mode.

Then, it runs the SQL statement, and places the data that it returns into an updatable [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object.

Finally, the sample code iterates through the rows of data that are in the result set. If it finds an empty document summary, it uses the combination of [updateString](../../../connect/jdbc/reference/updatestring-method-sqlserverresultset.md) and [updateRow](../../../connect/jdbc/reference/updaterow-method-sqlserverresultset.md) methods to update the row of data and again persist it to the database. If there's already data, it uses the [getString](../../../connect/jdbc/reference/getstring-method-sqlserverresultset.md) method to display some of the data.

The default behavior of the driver is "**adaptive.**" However, for the forward-only updatable result sets and when the data in the result set is larger than the application memory, the application has to set the adaptive buffering mode explicitly by using the [setResponseBuffering](../../../connect/jdbc/reference/setresponsebuffering-method-sqlserverstatement.md) method of the [SQLServerStatement](../../../connect/jdbc/reference/sqlserverstatement-class.md) class.

[!code[JDBC#UsingAdaptiveBuffering3](../../../connect/jdbc/codesnippet/Java/updating-large-data-sample_1.java)]

## See Also

[Working with Large Data](../../../connect/jdbc/code-samples/working-with-large-data.md)
