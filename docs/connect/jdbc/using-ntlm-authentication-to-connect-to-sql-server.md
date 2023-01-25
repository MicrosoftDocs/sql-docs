---
title: Using NTLM authentication to connect to SQL Server
description: Learn how to establish a SQL database connection using NTLM authentication with the JDBC driver.
author: lilgreenbird
ms.author: "v-susanh"
ms.reviewer: v-davidengel
ms.date: 03/31/2021
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---

# Using NTLM Authentication to connect to SQL Server

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

The [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] allows an application to use the **authenticationScheme** connection property to indicate that it wants to connect to a database using NTLM v2 Authentication.

The following properties are also used for NTLM Authentication:

- **domain = domainName** (optional)
- **user = userName**
- **password = password**
- **integratedSecurity = true**

Other than **domain**, the other properties are mandatory, the driver will throw an error if any are missing when the **NTLM** authenticationScheme property is used.

For more information on connection properties, see [Setting the connection properties](setting-the-connection-properties.md). For more information on the Microsoft NTLM authentication protocol, see [Microsoft NTLM](/windows/desktop/SecAuthN/microsoft-ntlm).

## Remarks

See [Network security: LAN Manager authentication level](/windows/security/threat-protection/security-policy-settings/network-security-lan-manager-authentication-level) for description of the SQL server settings, which control the behavior of NTLM authentication.

## Logging

A new logger has been added to support NTLM authentication: com.microsoft.sqlserver.jdbc.internals.NTLMAuthentication. For more information, see [Tracing Driver Operation](tracing-driver-operation.md).

## DataSource

When using a datasource to create connections, the NTLM properties can be programmatically set using **setAuthenticationScheme**, **setDomain**, and (optionally) **setServerSpn**.

```java
SQLServerDataSource ds = new SQLServerDataSource();
ds.setServerName("<server>");
ds.setPortNumber(1433); // change if necessary
ds.setIntegratedSecurity(true);
ds.setAuthenticationScheme("NTLM");
ds.setDomain("<domainName>");
ds.setUser("<userName>");
ds.setPassword("<password>");
ds.setDatabaseName("<database>");
ds.setServerSpn("<serverSpn");

try (Connection c = ds.getConnection(); Statement s = c.createStatement();
        ResultSet rs = s.executeQuery("select auth_scheme from sys.dm_exec_connections where session_id=@@spid")) {
    while (rs.next()) {
        System.out.println("Authentication Scheme: " + rs.getString(1));
    }
}
```

## Service principal names

A service principal name (SPN) is the name by which a client uniquely identifies an instance of a service.

You can specify the SPN using the **serverSpn** connection property, or let the driver build it for you (the default). This property is in the form of: "MSSQLSvc/fqdn:port\@REALM" where fqdn is the fully qualified domain name, port is the port number, and REALM is the realm of the SQL Server in upper-case letters. The realm portion of this property is optional since the default realm is the same as the realm of the Server.

For example, your SPN might look like: "MSSQLSvc/some-server.zzz.corp.contoso.com:1433"

For more information about service principal names (SPNs), see:

- [Service Principal Name (SPN) Support in Client Connections](../../relational-databases/native-client/features/service-principal-name-spn-support-in-client-connections.md)

> [!NOTE]
> The serverSpn connection attribute is only supported by Microsoft JDBC Drivers 4.2 and higher.

> Before 6.2 release of JDBC driver, you would need to explicitly set the **serverSpn**. As of the 6.2 release, the driver will be able to build the **serverSpn** by default, although one can use **serverSpn** explicitly too.

## Security risks

The NTLM protocol is an old authentication protocol with various vulnerabilities, which pose a security risk. It's based on a weak cryptographic scheme and is vulnerable to attacks. NTLM has been replaced by Kerberos, which is much more secure and recommended. NTLM authentication should only be used in a secure, trusted environment or when Kerberos can't be used.

The [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] only supports NTLM v2, which has some security improvements over the original v1 protocol. It's also recommended to enable Extended Protection, or use TLS Encryption for increased security.

For more information on how to enable Extended Protection and, see:

- [Connect to the Database Engine Using Extended Protection](../../database-engine/configure-windows/connect-to-the-database-engine-using-extended-protection.md)

For more information on connecting with encryption, see:

- [Connecting with encryption](connecting-with-ssl-encryption.md)

> [!NOTE]
> For the 7.4 release, enabling **both** Extended Protection and Encryption is not supported.

## See also

[Connecting to SQL Server with the JDBC driver](connecting-to-sql-server-with-the-jdbc-driver.md)
