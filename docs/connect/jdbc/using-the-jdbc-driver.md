---
title: Install the JDBC Driver
description: Install Microsoft JDBC Driver for SQL Server with Maven, Gradle, or downloaded JAR files, and verify that Java can discover the driver.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: 11/06/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: install-set-up-deploy
ai-usage: ai-assisted
---

# Install the JDBC driver

<a id="using-the-jdbc-driver"></a>

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

Install the Microsoft JDBC Driver for SQL Server in the same place your Java application resolves its other libraries. For most applications, use Maven or Gradle. Use a downloaded JAR when your build system can't restore packages from Maven Central or when an application server requires you to copy libraries into a specific folder.

This article covers installation and driver discovery. To build and run a complete app that connects to a database, use the [Java and Maven quickstart](getting-started-with-the-jdbc-driver.md).

## Choose the right JAR file

Microsoft publishes two current artifacts for version 13.6.2:

| Java runtime | Maven version | Downloaded JAR | JDBC API level |
| --- | --- | --- | --- |
| Java 8 | `13.6.2.jre8` | `mssql-jdbc-13.6.2.jre8.jar` | JDBC 4.2. |
| Java 11 and later versions | `13.6.2.jre11` | `mssql-jdbc-13.6.2.jre11.jar` | JDBC 4.3 partial compatibility. |

Use `13.6.2.jre11` unless your application must run on Java 8. The `jre11` artifact works on the Java versions listed for driver 13.6 in the [support matrix](microsoft-jdbc-driver-for-sql-server-support-matrix.md#java-and-jdbc-specification-support). Use `13.6.2.jre8` only for Java 8 applications.

Don't place more than one `mssql-jdbc` JAR on the same application classpath. Multiple driver versions can cause class loading conflicts and make troubleshooting harder.

## Install with Maven

Add the driver dependency to your `pom.xml` file.

```xml
<dependency>
    <groupId>com.microsoft.sqlserver</groupId>
    <artifactId>mssql-jdbc</artifactId>
    <version>13.6.2.jre11</version>
</dependency>
```

For Java 8 applications, use `13.6.2.jre8` instead:

```xml
<dependency>
    <groupId>com.microsoft.sqlserver</groupId>
    <artifactId>mssql-jdbc</artifactId>
    <version>13.6.2.jre8</version>
</dependency>
```

The base Maven dependency resolves only the driver JAR. A base application doesn't include optional feature libraries, such as Microsoft Entra authentication and Azure Key Vault support for Always Encrypted, unless you add them. For feature-specific dependencies, see [Feature dependencies](feature-dependencies-of-microsoft-jdbc-driver-for-sql-server.md).

## Install with Gradle

For Gradle builds, add the dependency that matches your Java runtime.

```gradle
implementation 'com.microsoft.sqlserver:mssql-jdbc:13.6.2.jre11'
```

For Java 8 applications, use `13.6.2.jre8` instead:

```gradle
implementation 'com.microsoft.sqlserver:mssql-jdbc:13.6.2.jre8'
```

The Gradle notation uses the same Maven Central coordinates as Maven. Run your Gradle build after you add the dependency to verify dependency resolution in your environment.

<a id="setting-the-classpath"></a>

## Install from a downloaded package

Download the ZIP or TAR.GZ package from [Download Microsoft JDBC Driver for SQL Server](download-microsoft-jdbc-driver-for-sql-server.md). Extract the package, then copy the JAR that matches your Java runtime into your application or application server library folder.

These full packages contain version 13.6.0. To install the latest servicing release manually, download the JAR that matches your Java runtime from the [13.6.2 GitHub release](https://github.com/microsoft/mssql-jdbc/releases/tag/v13.6.2) instead.

The extracted package includes paths like the following examples:

```console
<installation directory>\sqljdbc_13.6\enu\jars\mssql-jdbc-13.6.0.jre8.jar
<installation directory>\sqljdbc_13.6\enu\jars\mssql-jdbc-13.6.0.jre11.jar
```

On Linux or macOS, the directory separator is `/`:

```console
<installation directory>/sqljdbc_13.6/enu/jars/mssql-jdbc-13.6.0.jre11.jar
```

If you use a manual classpath, include exactly one driver JAR.

```powershell
$env:CLASSPATH = ".;C:\Program Files\Microsoft JDBC Driver 13.6 for SQL Server\sqljdbc_13.6\enu\jars\mssql-jdbc-13.6.0.jre11.jar"
```

```bash
export CLASSPATH=".:/opt/mssql-jdbc/sqljdbc_13.6/enu/jars/mssql-jdbc-13.6.0.jre11.jar"
```

If you run from an integrated development environment (IDE), add the driver JAR as a project library or dependency instead of setting a global classpath. If you deploy to a servlet container, Jakarta EE server, or another application server, follow that server's library-loading guidance. Don't copy the driver into multiple classloader locations for the same application.

## Verify driver discovery

Current driver JARs include `META-INF/services/java.sql.Driver`, so Java can discover `com.microsoft.sqlserver.jdbc.SQLServerDriver` through the standard JDBC service-provider mechanism. You don't need `Class.forName` for normal driver loading.

Use this no-network check to confirm that your application sees the intended driver JAR:

```java
import java.sql.Driver;
import java.sql.DriverManager;
import java.util.Enumeration;

public class DriverDiscoveryCheck {
    public static void main(String[] args) throws Exception {
        Enumeration<Driver> drivers = DriverManager.getDrivers();
        boolean found = false;

        while (drivers.hasMoreElements()) {
            Driver driver = drivers.nextElement();
            if (driver.getClass().getName().equals("com.microsoft.sqlserver.jdbc.SQLServerDriver")) {
                System.out.println(driver.getClass().getName());
                System.out.println(driver.getMajorVersion() + "." + driver.getMinorVersion());
                System.out.println(driver.getClass().getProtectionDomain().getCodeSource().getLocation());
                found = true;
            }
        }

        if (!found) {
            throw new IllegalStateException("Microsoft JDBC Driver not discovered");
        }
    }
}
```

Compile and run it with the driver on the classpath:

```console
javac DriverDiscoveryCheck.java
java -cp ".;<path-to-driver-jar>" DriverDiscoveryCheck
```

On Linux and macOS, use `:` instead of `;` as the classpath separator:

```console
java -cp ".:<path-to-driver-jar>" DriverDiscoveryCheck
```

This check verifies driver discovery only. It doesn't verify your connection URL, credentials, Transport Layer Security (TLS), firewall rules, or database permissions.

## Diagnose classpath problems

If Java can't load the driver, check these common causes:

- The driver JAR isn't on the application classpath or module path.
- The application has more than one `mssql-jdbc` JAR.
- The application uses the Java 11 artifact on Java 8.
- The application server loads a different copy of the driver from a shared library folder.
- A feature-specific dependency is missing. The base driver can load, but a feature such as Microsoft Entra authentication or Azure Key Vault support fails when that code path runs.

If you see `ClassNotFoundException` for `com.microsoft.sqlserver.jdbc.SQLServerDriver`, Java can't find the driver JAR. If you see `NoClassDefFoundError` for an Azure, Bouncy Castle, ANTLR, OSGi, or Gson type, check [Feature dependencies](feature-dependencies-of-microsoft-jdbc-driver-for-sql-server.md).

<a id="make-a-simple-connection-to-a-database"></a>

## Connect to a database

Use the [Java and Maven quickstart](getting-started-with-the-jdbc-driver.md) to build a complete application and run a parameterized query. The quickstart includes database setup choices, Microsoft Entra authentication for Azure SQL and Fabric, SQL authentication for local development, expected output, and cleanup.

For full connection-string syntax and secure defaults, see [Build the connection URL](building-the-connection-url.md) and [Set the connection properties](setting-the-connection-properties.md).

## Historical classpath anchors

The following anchors previously pointed to per-version classpath sections. Current setup guidance is version-independent: choose the artifact for your Java runtime, place one driver JAR on the application classpath, and keep optional feature libraries with the application that uses them. For older driver version details, use the [support matrix](microsoft-jdbc-driver-for-sql-server-support-matrix.md) and [release notes](release-notes-for-the-jdbc-driver.md).

<a id="for-microsoft-jdbc-driver-136"></a>
<a id="for-microsoft-jdbc-driver-134"></a>
<a id="for-microsoft-jdbc-driver-132"></a>
<a id="for-microsoft-jdbc-driver-1210"></a>
<a id="for-microsoft-jdbc-driver-128"></a>
<a id="for-microsoft-jdbc-driver-126"></a>
<a id="for-microsoft-jdbc-driver-124"></a>
<a id="for-microsoft-jdbc-driver-122"></a>
<a id="for-microsoft-jdbc-driver-112"></a>
<a id="for-microsoft-jdbc-driver-102"></a>
<a id="for-microsoft-jdbc-driver-94"></a>
<a id="for-microsoft-jdbc-driver-92"></a>
<a id="for-microsoft-jdbc-driver-84"></a>
<a id="for-microsoft-jdbc-driver-82"></a>
<a id="for-microsoft-jdbc-driver-74"></a>
<a id="for-microsoft-jdbc-driver-72"></a>
<a id="for-microsoft-jdbc-driver-70"></a>
<a id="for-microsoft-jdbc-driver-64"></a>
<a id="for-microsoft-jdbc-driver-62"></a>
<a id="for-microsoft-jdbc-driver-41-42-and-60"></a>
<a id="applications-that-are-run-directly-at-the-command-prompt"></a>
<a id="applications-that-run-in-an-ide"></a>
<a id="servlets-and-jsps"></a>
<a id="enterprise-java-beans"></a>

## Related content

- [Download Microsoft JDBC Driver for SQL Server](download-microsoft-jdbc-driver-for-sql-server.md)
- [System requirements for the JDBC driver](system-requirements-for-the-jdbc-driver.md)
- [Support matrix](microsoft-jdbc-driver-for-sql-server-support-matrix.md)
- [Feature dependencies](feature-dependencies-of-microsoft-jdbc-driver-for-sql-server.md)
- [Java and Maven quickstart](getting-started-with-the-jdbc-driver.md)
