---
title: Connect to a SQL Database With Java and Maven
description: Build a Java application with Maven, connect to SQL Server, Azure SQL Database, or SQL database in Fabric, and verify a parameterized query result.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: 09/18/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: quickstart
ms.custom: intro-get-started
ai-usage: ai-assisted
---

# Quickstart: Connect to SQL Server with Java and Maven

<a id="getting-started-with-the-jdbc-driver"></a>

Use the Microsoft Java Database Connectivity (JDBC) Driver for SQL Server to connect a Java application to SQL Server, Azure SQL Database, or SQL database in Fabric. This quickstart uses Maven to download dependencies, reads connection settings from environment variables, and verifies a parameterized query result. You don't need AdventureWorks or any sample tables.

For Azure SQL Database or SQL database in Fabric, use Microsoft Entra ID authentication without storing a password in your application. During local development, sign in through a browser. For an application hosted in Azure, use a managed identity.

<a id="getting-started"></a>

## Prerequisites

- [Java Development Kit (JDK) 21](/java/openjdk/download#openjdk-21). Check your installation with `java -version`.
- [Apache Maven](https://maven.apache.org/install.html). Run `mvn -version` and confirm that Maven uses JDK 21.
- A database and permission to connect to it. Choose a hosted database or SQL Server container in [Choose your database](#choose-your-database). The query reads a calculated value and doesn't create or modify database objects.
- Network access to your database endpoint. For Azure SQL, [configure network access](/azure/azure-sql/database/network-access-controls-overview). For Fabric, follow [Connect to your SQL database](/fabric/database/sql/connect). For SQL Server, enable Transmission Control Protocol/Internet Protocol (TCP/IP) and use the configured port.
- A server certificate that your Java runtime trusts. The application requires encryption and validates the server certificate. For a private certificate authority, follow [Trust a private certificate authority](#trust-a-private-certificate-authority).

### Choose your database

Use an existing database or create one by using the following guides. An empty database is sufficient.

| Database | Setup and access |
| --- | --- |
| SQL database in Fabric | [Create a SQL database](/fabric/database/sql/create), and then follow [Fabric authentication and access requirements](/fabric/database/sql/authentication). Copy the database's server and database names from **Settings** > **Connection strings**. Use the SQL database connection, not its SQL analytics endpoint. |
| Azure SQL Database | [Create a single database](/azure/azure-sql/database/single-database-create-quickstart). Ask your administrator to [configure a Microsoft Entra administrator and create a database user for your identity](/azure/azure-sql/database/authentication-aad-configure). Azure subscription access alone doesn't grant database access. |
| SQL Server container | Create a container by using [Docker](../../linux/install-upgrade/quickstart-install-docker.md), [sqlcmd](../../tools/sqlcmd/quickstart-sqlcmd-create-container.md), or the [MSSQL extension for Visual Studio Code](../../tools/visual-studio-code-extensions/mssql/mssql-local-container.md). Use the container host's DNS name and published TCP port, and a SQL login with permission to connect to your database. |
| Existing SQL Server | Use the instance's DNS name, TCP port, and an existing database. Ask your administrator for a SQL login with permission to connect. |

SQL Server Linux container images require a supported x86-64 host. On an ARM64 development computer, run this Java application against a hosted database or a container on a supported remote host instead of relying on container emulation.

A new container might use a certificate that your JDK doesn't trust. Before running this sample, [configure a server certificate](../../linux/security/encrypted-connections.md) and, for a private issuer, [configure the Java trust store](#trust-a-private-certificate-authority). Keep certificate validation enabled for container connections too.

## Create the Maven project

Create a directory named `jdbc-quickstart`. In that directory, create `pom.xml` with the following contents. Maven manages the class path; you don't need to download Java archive (JAR) files manually.

```xml
<project xmlns="http://maven.apache.org/POM/4.0.0"
         xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
         xsi:schemaLocation="http://maven.apache.org/POM/4.0.0 https://maven.apache.org/xsd/maven-4.0.0.xsd">
    <modelVersion>4.0.0</modelVersion>
    <groupId>com.example</groupId>
    <artifactId>jdbc-quickstart</artifactId>
    <version>1.0.0</version>

    <properties>
        <maven.compiler.release>21</maven.compiler.release>
        <project.build.sourceEncoding>UTF-8</project.build.sourceEncoding>
    </properties>

    <dependencies>
        <dependency>
            <groupId>com.microsoft.sqlserver</groupId>
            <artifactId>mssql-jdbc</artifactId>
            <version>13.6.0.jre11</version>
        </dependency>
        <dependency>
            <groupId>com.azure</groupId>
            <artifactId>azure-core-http-netty</artifactId>
            <version>1.16.6</version>
        </dependency>
        <dependency>
            <groupId>com.azure</groupId>
            <artifactId>azure-identity</artifactId>
            <version>1.18.4</version>
        </dependency>
    </dependencies>

    <build>
        <plugins>
            <plugin>
                <groupId>org.apache.maven.plugins</groupId>
                <artifactId>maven-compiler-plugin</artifactId>
                <version>3.14.0</version>
            </plugin>
            <plugin>
                <groupId>org.codehaus.mojo</groupId>
                <artifactId>exec-maven-plugin</artifactId>
                <version>3.5.0</version>
                <configuration>
                    <executable>${java.home}/bin/java</executable>
                    <arguments>
                        <argument>-classpath</argument>
                        <classpath />
                        <argument>Quickstart</argument>
                    </arguments>
                </configuration>
            </plugin>
        </plugins>
    </build>
</project>
```

The `jre11` driver artifact works with JDK 21. The explicit `azure-identity` dependency supplies the authentication libraries needed for the Microsoft Entra modes in this quickstart; those libraries are optional dependencies of the driver and aren't automatically added to your application.

These versions match the [published JDBC 13.6.0 Maven project object model (POM)](https://repo.maven.apache.org/maven2/com/microsoft/sqlserver/mssql-jdbc/13.6.0.jre11/mssql-jdbc-13.6.0.jre11.pom). The explicit `azure-core-http-netty` dependency retains the transport version selected by that driver release.

For driver updates, check the [download page](download-microsoft-jdbc-driver-for-sql-server.md), [published Maven versions](https://central.sonatype.com/artifact/com.microsoft.sqlserver/mssql-jdbc), and [Java support matrix](microsoft-jdbc-driver-for-sql-server-support-matrix.md#java-and-jdbc-specification-support). When you update the driver, match the authentication dependencies to the driver's published POM. See [Feature dependencies](feature-dependencies-of-microsoft-jdbc-driver-for-sql-server.md#run-time). Use explicit release versions rather than Maven version ranges or `LATEST`.

## Configure the connection

Set the environment variables in the same terminal where you run Maven. Replace `<server>` and `<database>` with your endpoint and database name. Set `SQL_SERVER_NAME` to the host name only, without a JDBC URL or port.

If the copied server value is `tcp:<server>,1433`, use only `<server>` for `SQL_SERVER_NAME` and set `SQL_PORT` to `1433`. Don't include the `tcp:` prefix or `,1433` suffix in the host name.

<a id="azure-sql-with-microsoft-entra-authentication"></a>

### Azure SQL or Fabric with Microsoft Entra authentication

For local development, use `ActiveDirectoryInteractive`. The driver opens a browser for sign-in and supports multifactor authentication. Sign in with the identity that has access to your database. Don't set `SQL_USER` or `SQL_PASSWORD` for this mode.

For Azure SQL Database, replace `<server>` with the complete host name, such as `contoso.database.windows.net`. For Fabric, copy the host name from the SQL database's connection settings; don't construct it from the database name or append an Azure SQL suffix. Fabric requires Microsoft Entra authentication; don't use the SQL authentication example for Fabric.

# [PowerShell](#tab/powershell)

```powershell
$env:SQL_SERVER_NAME = "<server>"
$env:SQL_DATABASE_NAME = "<database>"
$env:SQL_PORT = "1433"
$env:SQL_AUTHENTICATION = "ActiveDirectoryInteractive"
```

# [Bash](#tab/bash)

```bash
export SQL_SERVER_NAME="<server>"
export SQL_DATABASE_NAME="<database>"
export SQL_PORT="1433"
export SQL_AUTHENTICATION="ActiveDirectoryInteractive"
```

---

For a headless application hosted on an Azure resource with a system-assigned managed identity:

1. Enable the resource's managed identity.
1. Ask your administrator to grant that identity access to the database. For Azure SQL, [create a database user for the identity](/azure/azure-sql/database/authentication-aad-configure). For Fabric, follow [Fabric authentication and access requirements](/fabric/database/sql/authentication), including the applicable tenant settings for service principals.
1. Set `SQL_AUTHENTICATION` to `ActiveDirectoryManagedIdentity` in the host's application configuration. Keep the server, database, and port settings. The same Java application runs without a browser or password.

Managed identity requires a supported Azure host; it isn't a replacement for interactive sign-in on your workstation. For user-assigned identities and other authentication modes, see [Connect using Microsoft Entra authentication](connecting-using-azure-active-directory-authentication.md).

### SQL Server with SQL authentication

For a SQL Server container or existing instance configured for SQL authentication, set `SQL_AUTHENTICATION` to `SqlPassword` and supply a SQL login with access to your database. Don't use a server administrator account for this sample. The PowerShell password prompt requires PowerShell 7.1 or later.

# [PowerShell](#tab/powershell)

```powershell
$env:SQL_SERVER_NAME = "<server>"
$env:SQL_DATABASE_NAME = "<database>"
$env:SQL_PORT = "1433"
$env:SQL_AUTHENTICATION = "SqlPassword"
$env:SQL_USER = "<user_id>"
$env:SQL_PASSWORD = Read-Host "SQL password" -MaskInput
```

# [Bash](#tab/bash)

```bash
export SQL_SERVER_NAME="<server>"
export SQL_DATABASE_NAME="<database>"
export SQL_PORT="1433"
export SQL_AUTHENTICATION="SqlPassword"
export SQL_USER="<user_id>"
read -r -s -p "SQL password: " SQL_PASSWORD
printf '\n'
export SQL_PASSWORD
```

---

Use your instance's TCP port if it isn't 1433. Environment variables keep credentials out of source code, but they aren't a secret store. Don't log them or commit them to source control. Don't enable Maven debug logging (`-X`) while password variables are set: the execution plugin logs the child process's environment values. For deployed applications, use your platform's secret management and [secure connection settings](securing-connection-strings.md).

### Trust a private certificate authority

Skip this section if your JDK already trusts the server's certificate issuer. Otherwise, get the public certificate and SHA-256 fingerprint of the issuing certificate authority (CA) from your administrator through a trusted channel. Don't trust a certificate just because the server presented it during a failed connection.

Set `SQL_SERVER_NAME` to a DNS name covered by the server certificate and resolvable from your computer. The application doesn't override `hostNameInCertificate`, so the driver validates the configured server name. Adding a trusted CA doesn't fix a certificate-name mismatch.

Use the `keytool` from the same JDK 21 installation that Maven uses. Replace `<ca-certificate-file>` with the verified CA certificate's path, `<jdk-home>` with that JDK's directory, and `<trust-store-file>` with a new absolute path outside your project. Keep the trust store in a directory that other unprivileged users can't modify. Stop if any command fails.

1. Inspect the CA certificate and compare its SHA-256 fingerprint with the value your administrator supplied.

   ```console
   keytool -printcert -file "<ca-certificate-file>"
   ```

1. Create a dedicated PKCS12 trust store by copying the JDK's public roots. Don't modify the installed JDK's `cacerts` file or overwrite an existing trust store.

   ```console
   keytool -importkeystore -srckeystore "<jdk-home>/lib/security/cacerts" -destkeystore "<trust-store-file>" -deststoretype PKCS12
   ```

   Enter a new destination-store password at the prompts. For the source-store password, use the value supplied by your JDK provider or administrator. Copying the public roots retains trust for servers that use public CAs.

1. Add the verified CA to the dedicated store.

   ```console
   keytool -importcert -alias sql-server-ca -file "<ca-certificate-file>" -keystore "<trust-store-file>" -storetype PKCS12
   ```

   Enter the destination-store password. Before confirming the import, check that the displayed fingerprint matches the verified value.

Set these two optional variables in the terminal where you run Maven. Use the destination-store password you just created. The PowerShell password prompt requires PowerShell 7.1 or later.

# [PowerShell](#tab/powershell)

```powershell
$env:SQL_TRUST_STORE = "<trust-store-file>"
$env:SQL_TRUST_STORE_PASSWORD = Read-Host "Trust-store password" -MaskInput
```

# [Bash](#tab/bash)

```bash
export SQL_TRUST_STORE="<trust-store-file>"
read -r -s -p "Trust-store password: " SQL_TRUST_STORE_PASSWORD
printf '\n'
export SQL_TRUST_STORE_PASSWORD
```

---

The Java application reads these variables and configures its JDBC trust store explicitly. Omit both variables to use the Java Virtual Machine's (JVM's) default trust configuration. Keep `encrypt=true` and `trustServerCertificate=false`.

Maven starts a separate Java process for this application. Setting a Java Secure Socket Extension (JSSE) property such as `javax.net.ssl.trustStore` with `mvn -D...` or `MAVEN_OPTS` doesn't configure that child process. `JAVA_TOOL_OPTIONS` and `JDK_JAVA_OPTIONS` reach it, but the JVM echoes those options at startup. Don't put passwords in either variable. Use the application variables in this section instead. For more about trust configuration, see [Configure the client for encryption](configuring-the-client-for-ssl-encryption.md).

## Add the Java application

From the `jdbc-quickstart` directory, create the Java source directory.

# [PowerShell](#tab/powershell)

```powershell
New-Item -ItemType Directory -Path src\main\java -Force | Out-Null
```

# [Bash](#tab/bash)

```bash
mkdir -p src/main/java
```

---

Save the following complete application as `Quickstart.java` in `src/main/java`.

The Transact-SQL (T-SQL) query `SELECT CAST(? AS int) + 1 AS answer` accepts one parameter. `setInt` binds the value `41` separately from the SQL text. The expected result is exactly one row containing `42`.

```java
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;

import com.microsoft.sqlserver.jdbc.SQLServerDataSource;

public class Quickstart {
    public static void main(String[] args) throws SQLException {
        SQLServerDataSource dataSource = new SQLServerDataSource();
        dataSource.setServerName(required("SQL_SERVER_NAME"));
        dataSource.setDatabaseName(required("SQL_DATABASE_NAME"));
        dataSource.setPortNumber(Integer.parseInt(required("SQL_PORT")));
        dataSource.setEncrypt("true");
        dataSource.setTrustServerCertificate(false);
        dataSource.setLoginTimeout(30);

        String authentication = required("SQL_AUTHENTICATION");
        switch (authentication) {
            case "ActiveDirectoryInteractive":
            case "ActiveDirectoryManagedIdentity":
                break;
            case "SqlPassword":
                dataSource.setUser(required("SQL_USER"));
                dataSource.setPassword(required("SQL_PASSWORD"));
                break;
            default:
                throw new IllegalArgumentException(
                        "SQL_AUTHENTICATION must be ActiveDirectoryInteractive, "
                        + "ActiveDirectoryManagedIdentity, or SqlPassword.");
        }
        dataSource.setAuthentication(authentication);

        if (System.getenv("SQL_TRUST_STORE") != null
                || System.getenv("SQL_TRUST_STORE_PASSWORD") != null) {
            dataSource.setTrustStore(required("SQL_TRUST_STORE"));
            dataSource.setTrustStorePassword(required("SQL_TRUST_STORE_PASSWORD"));
            dataSource.setTrustStoreType("PKCS12");
        }

        String sql = "SELECT CAST(? AS int) + 1 AS answer";
        try (Connection connection = dataSource.getConnection();
             PreparedStatement statement = connection.prepareStatement(sql)) {
            statement.setInt(1, 41);
            statement.setQueryTimeout(30);
            try (ResultSet results = statement.executeQuery()) {
                if (!results.next() || results.getInt("answer") != 42 || results.next()) {
                    throw new SQLException("Expected exactly one row with answer = 42.");
                }
            }
        }
        System.out.println("Verified result: 42");
    }

    private static String required(String name) {
        String value = System.getenv(name);
        if (value == null || value.isBlank()) {
            throw new IllegalArgumentException("Set environment variable " + name + ".");
        }
        return value;
    }
}
```

The application sets the login and query timeouts to 30 seconds. Interactive authentication has a separate token wait limit described in [Troubleshoot the first run](#troubleshoot-the-first-run). The application closes the result set, prepared statement, and connection by using try-with-resources, including when an exception occurs. Errors propagate and cause the process to exit with a nonzero status.

## Run and verify

From the `jdbc-quickstart` directory, run:

```console
mvn -q compile exec:exec
```

Maven downloads the dependencies, compiles the application, and starts a separate Java process. For `ActiveDirectoryInteractive`, complete the browser sign-in and return to your terminal.

After the query succeeds and the application closes the resources, it prints:

```output
Verified result: 42
```

A browser message that authentication is complete doesn't prove the database connection succeeded. Check for the application's result and a zero exit status: `$LASTEXITCODE` in PowerShell or `$?` in Bash. The query doesn't depend on any existing tables or leave data to clean up.

If you used SQL authentication, remove `SQL_PASSWORD` from the terminal environment when you're finished. In PowerShell, run `Remove-Item Env:\SQL_PASSWORD`. In Bash, run `unset SQL_PASSWORD`.

If you configured a private-CA trust store, also remove its environment variables. In PowerShell, run `Remove-Item Env:\SQL_TRUST_STORE, Env:\SQL_TRUST_STORE_PASSWORD`. In Bash, run `unset SQL_TRUST_STORE SQL_TRUST_STORE_PASSWORD`. Retain the trust store only as long as you need it.

## Troubleshoot the first run

| Symptom | Action |
| --- | --- |
| `Set environment variable ...` | Set the named variable in the terminal running Maven. An integrated development environment (IDE) might need its own run configuration. |
| Java compilation or class version error | Run `mvn -version` and check that Maven uses JDK 21. Check `JAVA_HOME` if Maven and `java -version` report different runtimes. |
| Missing authentication library | Keep the `azure-identity` dependency in `pom.xml` and run the application through Maven so it includes transitive dependencies. |
| Login timeout or connection refused | Check the server host name, TCP port, database availability, and network access. |
| Interactive sign-in times out | The driver waits at most 20 seconds for the interactive token request. Complete sign-in promptly. If the request times out, run the command again. Increasing `loginTimeout` doesn't extend this token wait limit. |
| Microsoft Entra sign-in succeeds but database access fails | Confirm that you signed in to the correct tenant and that your identity has access to the selected database. Check the Azure SQL database user or [Fabric permissions](/fabric/database/sql/authentication), as applicable. |
| Certificate validation fails, including `PKIX path building failed` | Follow [Trust a private certificate authority](#trust-a-private-certificate-authority) for a private issuer. Connect using a DNS name covered by the server certificate. Don't bypass validation with `trustServerCertificate=true`. |

For additional diagnostics, see [Troubleshooting connectivity](troubleshooting-connectivity.md).

## Related content

- [Connect using Microsoft Entra authentication](connecting-using-azure-active-directory-authentication.md)
- [Using a SQL statement with parameters](using-an-sql-statement-with-parameters.md)
- [Using auto-generated keys](using-auto-generated-keys.md)
- [Connection resiliency](connection-resiliency.md)
- [Sample JDBC driver applications](sample-jdbc-driver-applications.md)
