---
title: "Using Kerberos Integrated Authentication to Connect to SQL Server | Microsoft Docs"
ms.custom: ""
ms.date: "07/11/2018"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 687802dc-042a-4363-89aa-741685d165b3
author: MightyPen
ms.author: genemi
manager: craigg
---

# Using Kerberos Integrated Authentication to Connect to SQL Server

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

Beginning in [!INCLUDE[jdbc_40](../../includes/jdbc_40_md.md)], an application can use the **authenticationScheme** connection property to indicate that it wants to connect to a database using type 4 Kerberos integrated authentication. See [Setting the Connection Properties](../../connect/jdbc/setting-the-connection-properties.md) for more information on connection properties. For more information on Kerberos, see [Microsoft Kerberos](https://go.microsoft.com/fwlink/?LinkID=100758).

When using integrated authentication with the Java **Krb5LoginModule**, you can configure the module using [Class Krb5LoginModule](https://docs.oracle.com/javase/8/docs/jre/api/security/jaas/spec/com/sun/security/auth/module/Krb5LoginModule.html).

The [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] sets the following properties for IBM Java VMs:

- **useDefaultCcache = true**
- **moduleBanner = false**

The [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] sets the following properties for all other Java VMs:

- **useTicketCache = true**
- **doNotPrompt = true**

## Remarks

Prior to [!INCLUDE[jdbc_40](../../includes/jdbc_40_md.md)], applications could specify integrated authentication (using Kerberos or NTLM, depending on which is available) by using the **integratedSecurity** connection property and by referencing **sqljdbc_auth.dll**, as described in [Building the Connection URL](../../connect/jdbc/building-the-connection-url.md).

Beginning in [!INCLUDE[jdbc_40](../../includes/jdbc_40_md.md)], an application can use the **authenticationScheme** connection property to indicate that it wants to connect to a database using Kerberos integrated authentication using the pure Java Kerberos implementation:

- If you want integrated authentication using **Krb5LoginModule**, you must still specify the **integratedSecurity=true** connection property. You would then also specify the **authenticationScheme=JavaKerberos** connection property.

- To continue using integrated authentication with **sqljdbc_auth.dll**, just specify **integratedSecurity=true** connection property (and optionally **authenticationScheme=NativeAuthentication**).

- If you specify **authenticationScheme=JavaKerberos** but do not also specify **integratedSecurity=true**, the driver will ignore the **authenticationScheme** connection property and it will expect to find user name and password credentials in the connection string.

When using a datasource to create connections, you can programmatically set the authentication scheme using **setAuthenticationScheme** and (optionally) set the SPN for Kerberos connections using **setServerSpn**.

A new logger has been added to support Kerberos authentication: com.microsoft.sqlserver.jdbc.internals.KerbAuthentication. For more information, see [Tracing Driver Operation](../../connect/jdbc/tracing-driver-operation.md).

The following guidelines will help you to configure Kerberos:

1. Set **AllowTgtSessionKey** to 1 in the registry for Windows. For more information, see [Kerberos protocol registry entries and KDC configuration keys in Windows Server 2003](https://support.microsoft.com/kb/837361).
2. Make sure that the Kerberos configuration (krb5.conf in UNIX environments), points to the correct realm and KDC for your environment.
3. Initialize the TGT cache by using kinit or logging into the domain.
4. When an application that uses **authenticationScheme=JavaKerberos** runs on the Windows Vista or Windows 7 operating systems, you should use a standard user account. However if you run the application under an administrator's account, the application must run with administrator privileges.

> [!NOTE]  
> The serverSpn connection attribute is only supported by Microsoft JDBC Drivers 4.2 and higher.

## Service Principal Names

A service principal name (SPN) is the name by which a client uniquely identifies an instance of a service.

You can specify the SPN using the **serverSpn** connection property, or simply let the driver build it for you (the default). This property is in the form of: "MSSQLSvc/fqdn:port\@REALM" where fqdn is the fully-qualified domain name, port is the port number, and REALM is the Kerberos realm of the SQL Server in upper-case letters. The realm portion of this property is optional if your Kerberos configuration's default realm is the same realm as that of the Server and is not included by default. If you wish to support a cross-realm authentication scenario where the default realm in the Kerberos configuration is different than the realm of the Server, then you must set the SPN with the serverSpn property.

For example, your SPN might look like: "MSSQLSvc/some-server.zzz.corp.contoso.com:1433\@ZZZZ.CORP.CONTOSO.COM"

For more information about service principal names (SPNs), see:

- [How to use Kerberos authentication in SQL Server](https://support.microsoft.com/kb/319723)

- [Using Kerberos with SQL Server](https://go.microsoft.com/fwlink/?LinkId=207814)

> [!NOTE]  
> Before 6.2 release of JDBC driver, for proper use of Cross Realm Kerberos, you would need to explicitly set the **serverSpn**.
>
> As of the 6.2 release, the driver will be able to build the **serverSpn** by default, even when using Cross Realm Kerberos. Although one can use **serverSpn** explicitly too.

## Creating a Login Module Configuration File

You can optionally specify a Kerberos configuration file. If a configuration file is not specified, the following settings are in effect:

Sun JVM  
 com.sun.security.auth.module.Krb5LoginModule required useTicketCache=true;

IBM JVM  
 com.ibm.security.auth.module.Krb5LoginModule required useDefaultCcache = true;

If you decide to create a login module configuration file, the file must follow this format:

```java
<name> {  
    <LoginModule> <flag> <LoginModule options>;  
    <optional_additional_LoginModules, flags_and_options>;  
};  
```

A login configuration file consists of one or more entries, each specifying which underlying authentication technology should be used for a particular application or applications. For example,

```java
SQLJDBCDriver {  
   com.sun.security.auth.module.Krb5LoginModule required useTicketCache=true;  
};  
```

So, each login module configuration file entry consists of a name followed by one or more LoginModule-specific entries, where each LoginModule-specific entry is terminated by a semicolon and the entire group of LoginModule-specific entries is enclosed in braces. Each configuration file entry is terminated by a semicolon.

In addition to allowing the driver to acquire Kerberos credentials using the settings specified in the login module configuration file, the driver can use existing credentials. This can be useful when your application needs to create connections using more than one user's credentials.

The driver will attempt to use existing credentials if they are available, before attempting to login using the specified login module. Thus, when using the `Subject.doAs` method for executing code under a specific context, a connection will be created with the credentials passed to the `Subject.doAs` call.

For more information, see [JAAS Login Configuration File](https://docs.oracle.com/javase/8/docs/technotes/guides/security/jgss/tutorials/LoginConfigFile.html) and [Class Krb5LoginModule](https://docs.oracle.com/javase/8/docs/jre/api/security/jaas/spec/com/sun/security/auth/module/Krb5LoginModule.html).

Beginning in Microsoft JDBC Driver 6.2, name of login module configuration file can optionally be passed using connection property `jaasConfigurationName`, this allows each connection to have its own login configuration.

## Creating a Kerberos Configuration File

For more information about Kerberos configuration files, see [Kerberos Requirements](https://docs.oracle.com/javase/8/docs/technotes/guides/security/jgss/tutorials/KerberosReq.html).

This is a sample domain configuration file, where YYYY and ZZZZ are the domain names.

```ini
[libdefaults]  
default_realm = YYYY.CORP.CONTOSO.COM  
dns_lookup_realm = false  
dns_lookup_kdc = true  
ticket_lifetime = 24h  
forwardable = yes  

[domain_realm]  
.yyyy.corp.contoso.com = YYYY.CORP.CONTOSO.COM  
.zzzz.corp.contoso.com = ZZZZ.CORP.CONTOSO.COM  

[realms]  
        YYYY.CORP.CONTOSO.COM = {  
  kdc = krbtgt/YYYY.CORP. CONTOSO.COM @ YYYY.CORP. CONTOSO.COM  
  default_domain = YYYY.CORP. CONTOSO.COM  
}  

        ZZZZ.CORP. CONTOSO.COM = {  
  kdc = krbtgt/ZZZZ.CORP. CONTOSO.COM @ ZZZZ.CORP. CONTOSO.COM  
  default_domain = ZZZZ.CORP. CONTOSO.COM  
}  

```

## Enabling the Domain Configuration File and the Login Module Configuration File

You can enable a domain configuration file with -Djava.security.krb5.conf. You can enable a login module configuration file with **-Djava.security.auth.login.config**.

For example, the following command can be used to start the application:

```bash
Java.exe -Djava.security.auth.login.config=SQLJDBCDriver.conf -Djava.security.krb5.conf=krb5.ini <APPLICATION_NAME>  

```

## Verifying that SQL Server Can be Accessed via Kerberos

Run the following query in SQL Server Management Studio:

```sql
select auth_scheme from sys.dm_exec_connections where session_id=\@\@spid
```

Make sure that you have the necessary permission to run this query.

## Constrained Delegation

Beginning in Microsoft JDBC Driver 6.2, the driver supports Kerberos Constrained Delegation. The delegated credential can be passed in as org.ietf.jgss.GSSCredential object, these credentials are used by driver to establish connection.

```java
Properties driverProperties = new Properties();
GSSCredential impersonatedUserCredential = [userCredential]
driverProperties.setProperty("integratedSecurity", "true");
driverProperties.setProperty("authenticationScheme", "JavaKerberos");
driverProperties.put("gsscredential", impersonatedUserCredential);
Connection conn = DriverManager.getConnection(CONNECTION_URI, driverProperties);
```

## Kerberos Connection using Principal Names and Password

Beginning in Microsoft JDBC Driver 6.2, the driver can establish Kerberos connection using the Principal Name and Password passed in connection string.

```java
jdbc:sqlserver://servername=server_name;integratedSecurity=true;authenticationScheme=JavaKerberos;userName=user@REALM;password=****
```

The username property does not require REALM if user belongs to the default_realm set in krb5.conf file. When `userName` and `password` is set along with `integratedSecurity=true;` and `authenticationScheme=JavaKerberos;` property, the connection is established with value of userName as Kerberos Principal along with the password supplied.

## Using Kerberos authentication from Unix Machines on the same Domain

This guide assumes a working Kerberos setup already exists. Run the following code on a Windows machine with working Kerberos authentication to verify if the aforementioned is true. The code will print "Authentication Scheme: KERBEROS" to console if successful. No additional run-time flags, dependencies, or driver settings are required outside of the ones provided. The same block of code can be run on Linux to verify successful connections.

```java
SQLServerDataSource ds = new SQLServerDataSource();
ds.setServerName("/*SQL Server Name*/");
ds.setPortNumber(1433); //change if necessary
ds.setIntegratedSecurity(true);
ds.setAuthenticationScheme("JavaKerberos");
ds.setDatabaseName("master"); //change if necessary

try (Connection c = ds.getConnection(); Statement s = c.createStatement()) {
	try (ResultSet rs = s.executeQuery("select auth_scheme from sys.dm_exec_connections where session_id=@@spid")) {
		while (rs.next()) {
			System.out.println("Authentication Scheme: " + rs.getString(1));
		}
	}
}
```

1. Domain join the Unix agent to the same domain as the server.
2. (Optional) Set the default Kerberos ticket location, this is most conveniently done by setting the `KRB5CCNAME` environment variable.
3. Get the Kerberos ticket, either by generating a new one or placing an existing one in the default Kerberos ticket location. To generate a ticket, simply use a terminal and initialize the ticket via `kinit USER@DOMAIN.AD` where "USER" and "DOMAIN.AD" is the principal and domain respectively. E.g: `kinit SQL_SERVER_USER03@MICROSOFT.COM`. The ticket will be generated in the default ticket location or in the KERB5CCNAME path if set.
4. The terminal will prompt for a password, enter the password.
5. Verify the credentials in the ticket via `klist` and confirm the credentials are the ones you intend to use for authentication.
6. Run the above sample code and confirm that Kerberos Authentication was successful.

## See Also

[Connecting to SQL Server with the JDBC Driver](../../connect/jdbc/connecting-to-sql-server-with-the-jdbc-driver.md)
