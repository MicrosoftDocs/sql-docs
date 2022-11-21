---
title: Using database mirroring (JDBC)
description: Learn about using database mirroring with the JDBC Driver for SQL Server and things you need to consider when failover happens.
author: David-Engel
ms.author: v-davidengel
ms.date: "08/12/2019"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---

# Using database mirroring (JDBC)

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

Database mirroring is primarily a software solution for increasing database availability and data redundancy. The [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] provides implicit support for database mirroring, so that the developer doesn't need to write any code or take any other action when it has been configured for the database.

Database mirroring, which is implemented on a per-database basis, keeps a copy of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] production database on a standby server. This server is either a hot or warm standby server, depending on the configuration and state of the database mirroring session. A hot standby server supports rapid failover without a loss of committed transactions. A warm standby server supports forcing service (with possible data loss).

The production database is called the _principal_ database, and the standby copy is called the _mirror_ database. The principal database and mirror database must be on separate instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (server instances). They should reside on separate computers, if possible.

The production server instance (the principal server) communicates with the standby server instance (the mirror server). The principal and mirror servers act as partners within a database mirroring session. If the principal server fails, the mirror server can make its database the principal database through a process called _failover_. For example, Partner_A and Partner_B are two partner servers, with the principal database initially on Partner_A as principal server, and the mirror database residing on Partner_B as the mirror server. If Partner_A goes offline, the database on Partner_B can fail over to become the current principal database. When Partner_A rejoins the mirroring session, it becomes the mirror server and its database becomes the mirror database.

If Partner_A server is irreparably damaged, a Partner_C server can be brought online to act as the mirror server for Partner_B, which is now the principal server. However, in this scenario, the client application must include programming logic to ensure that the connection string properties are updated with the new server names used in the database mirroring configuration. Otherwise, the connection to the servers may fail.

Alternative database mirroring configurations offer different levels of performance and data safety, and support different forms of failover. For more information, see "Overview of Database Mirroring" in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.

## Programming considerations

When the principal database server fails, the client application receives errors in response to API calls, which indicate that the connection to the database has been lost. When these errors occur, any uncommitted changes to the database are lost and the current transaction is rolled back. In this scenario, the application should close the connection (or release the data source object) and try to reopen it. On connection, the new connection is transparently redirected to the mirror database, which now acts as the principal server, without the client having to modify the connection string or data source object.

When a connection is initially established, the principal server sends the identity of its failover partner to the client that will be used when failover occurs. When an application tries to establish an initial connection with a failed principal server, the client doesn't know the identity of the failover partner. To allow clients the opportunity to cope with this scenario, the failoverPartner connection string property, and optionally the [setFailoverPartner](../../connect/jdbc/reference/setfailoverpartner-method-sqlserverdatasource.md) data source method, allows the client to specify the identity of the failover partner on its own. The client property is used only in this scenario; if the principal server is available, it isn't used.

> [!NOTE]
> When a failoverPartner is specified in either the connection string or with a data source object, the databaseName property must also be set or else an exception will be thrown. If the failoverPartner and databaseName are not specified explicitly, the application will not attempt to failover when the principal database server fails. In other words, the transparent redirection only works for connections that explicitly specify the failoverPartner and databaseName. For more information about failoverPartner and other connection string properties, see [Setting the connection properties](../../connect/jdbc/setting-the-connection-properties.md).

If the failover partner server supplied by the client doesn't refer to a server acting as a failover partner for the specified database, and if the server/database referred to is in a mirrored arrangement, the connection is refused by the server. Although the [SQLServerDataSource](../../connect/jdbc/reference/sqlserverdatasource-class.md) class provides the [getFailoverPartner](../../connect/jdbc/reference/getfailoverpartner-method-sqlserverdatasource.md) method, this method only returns the name of the failover partner specified in the connection string or the setFailoverPartner method. To retrieve the name of the actual failover partner that is currently being used, use the following [!INCLUDE[tsql](../../includes/tsql-md.md)] statement:

```sql
SELECT m.mirroring_role_DESC, m.mirroring_state_DESC,
m.mirroring_partner_instance FROM sys.databases as db,
sys.database_mirroring AS m WHERE db.name = 'MirroringDBName'
AND db.database_id = m.database_id
```

> [!NOTE]
> You will need to change this statement to use the name of your mirroring database.

Consider caching the partner information to update the connection string or devise a retry strategy in case the first attempt at making a connection fails.

## Example

In the following example, an attempt is first made to connect to the principle server. If that fails and an exception is thrown, an attempt is made to connect to the mirror server, which may have been promoted to the new principle server. Note the use of the failoverPartner property in the connection string.

```java
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;
import java.sql.Statement;

public class ClientFailover {
    public static void main(String[] args) {

        String connectionUrl = "jdbc:sqlserver://serverA:1433;"
                + "encrypt=true;databaseName=AdventureWorks;integratedSecurity=true;"
                + "failoverPartner=serverB";

        // Establish the connection to the principal server.
        try (Connection con = DriverManager.getConnection(connectionUrl);
                Statement stmt = con.createStatement();) {
            System.out.println("Connected to the principal server.");

            // Note that if a failover of serverA occurs here, then an
            // exception will be thrown and the failover partner will
            // be used in the first catch block below.

            // Execute a SQL statement that inserts some data.

            // Note that the following statement assumes that the
            // TestTable table has been created in the AdventureWorks
            // sample database.
            stmt.executeUpdate("INSERT INTO TestTable (Col2, Col3) VALUES ('a', 10)");
        }
        catch (SQLException se) {
            System.out.println("Connection to principal server failed, " + "trying the mirror server.");
            // The connection to the principal server failed,
            // try the mirror server which may now be the new
            // principal server.
            try (Connection con = DriverManager.getConnection(connectionUrl);
                    Statement stmt = con.createStatement();) {
                System.out.println("Connected to the new principal server.");
                stmt.executeUpdate("INSERT INTO TestTable (Col2, Col3) VALUES ('a', 10)");
            }
            // Handle any errors that may have occurred.
            catch (SQLException e) {
                e.printStackTrace();
            }
        }
    }
}
```

## See also

[Connecting to SQL Server with the JDBC driver](../../connect/jdbc/connecting-to-sql-server-with-the-jdbc-driver.md)
