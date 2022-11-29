---
title: Understanding Active Directory authentication for SQL Server on Linux and containers
description: Understand Active Directory authentication with SQL Server on Linux and containers. Includes LDAP, Kerberos, keytabs and DNS.
author: amitkh-msft
ms.author: amitkh
ms.reviewer: randolphwest
ms.date: 09/27/2022
ms.service: sql
ms.subservice: linux
ms.topic: conceptual
monikerRange: ">= sql-server-linux-2017 || >= sql-server-2017 || =sqlallproducts-allversions"
---
# Understand Active Directory authentication for SQL Server on Linux and containers

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

This article provides you details on how Active Directory authentication works for [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] deployed on Linux or containers.

## Concepts

### <a id="ldap"></a> Lightweight Directory Access Protocol (LDAP)

LDAP is an application protocol for working with various directory services, including Active Directory. Directory services store user and account information, and security information such as passwords. That information is encrypted and then shared with other devices on the network.

For more information on how Lightweight Directory Access Protocol over SSL (LDAPS) differs from LDAP, see [LDAP over SSL (LDAPS) Certificate](https://social.technet.microsoft.com/wiki/contents/articles/2980.ldap-over-ssl-ldaps-certificate.aspx#Enabling_LDAPS_for_Client_Authentication).

To find out more about securing LDAP, see [How to enable LDAP signing in Windows Server](/troubleshoot/windows-server/identity/enable-ldap-signing-in-windows-server).

### Kerberos

Kerberos is an authentication protocol used to verify the identity of a user or host computer. You can think of it as a way to verify the client and server.

When you work in a heterogeneous (mixed) environment where you have Windows and non-Windows servers and clients, there are two kinds of files you need to work with Active Directory-based directory services:

- Keytab files (short for "key tables")
- Kerberos configuration files (`krb5.conf` or `krb5.ini`)

## What is a keytab file?

Server processes on Linux or Unix systems can't be configured to run processes with a Windows service account. When you want a Linux or Unix system to automatically log into Active Directory on startup, you must use a *keytab* file.

A keytab is a cryptographic file containing a representation of a Kerberos-protected service and its long-term *key* of its associated service principal name in the Key Distribution Center (KDC). The key is not the password itself.

Keytabs are used to either:

- authenticate the service itself to another service on the network, or
- decrypt the Kerberos service ticket of an inbound directory user to the service.

For more information, see [Active Directory: Using Kerberos Keytabs to integrate non-Windows systems](https://social.technet.microsoft.com/wiki/contents/articles/36470.active-directory-using-kerberos-keytabs-to-integrate-non-windows-systems.aspx).

## What is a `krb5.conf` file?

The `/etc/krb5.conf` file (which may also be called `krb5.ini`) provides configuration inputs for the Kerberos v5 (KRB5) and GNU Simple Authentication and Security Layer API (GSSAPI) libraries.

This information includes the default domain, properties of each domain (such as Key Distribution Centers), and default Kerberos ticket lifetime.

This file is necessary for Active Directory authentication to work. `krb5.conf` is an INI file, but each value in the key-value pair can be a subgroup enclosed by `{` and `}`.

For more information on the `krb5.conf` file, refer to the [MIT Kerberos Consortium documentation](http://web.mit.edu/kerberos/krb5-1.16/doc/admin/conf_files/krb5_conf.html).

## Configure Kerberos for [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] on Linux

These are the values you need on the host server running [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] on Linux. If you have other (non-[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]) services running on the same host, your `krb5.conf` file might need several more entries.

Here is a sample `krb5.conf` file for reference:

```ini
[libdefaults]
default_realm = CONTOSO.COM

[realms]
CONTOSO.COM = {
  kdc = adVM.contoso.com
  admin_server = adVM.contoso.com
  default_domain = contoso.com
}

[domain_realm]
.contoso.com = CONTOSO.COM
contoso.com = CONTOSO.COM
```

- `libdefaults` - the `default_realm` value must be present. This value specifies the domain to which the host machine belongs.

- `realms` (optional) - For each realm, the `kdc` value may be set to specify which Key Distribution Centers the machine should contact when looking up Active Directory accounts. If you have set more than one KDC, the KDC for each connection will be selected by round-robin.

- `domain_realm` (optional) - Mappings for each realm may be provided. If not, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] on Linux assumes that the domain `contoso.com` maps to the realm `CONTOSO.COM`.

### The Kerberos authentication process

As with Kerberos authentication on Windows, the first two steps to obtain a ticket-granting ticket (TGT) are the same:

- A client begins the login process by sending their username and password (encrypted) to the domain controller (DC).

- After checking the username and password against its internal storage, the DC returns a TGT for the user to the client.

[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] on Linux uses the keytab file to read the password for the Service Principal Name (SPN) and then decrypts the encrypted blob, which it uses to authorize the connection. The next steps outline this process.

- Once the user has the TGT, the client starts a connection to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] by specifying the hostname and port of the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] instance.

- The SQL client internally creates a Service Principal Name in the format `MSSQLSvc/<host>:<port>`. This is a hardcoded format in most [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] clients.

- The client starts the Kerberos handshake by requesting a session key from the DC for that SPN. Both the TGT and the SPN are sent to the DC.

:::image type="content" source="media/sql-server-linux-ad-auth-understanding/ad-auth-explained-tgt-spn.svg" alt-text="Active Directory authentication for SQL Server on Linux - Ticket-Granting Ticket and Service Principal Name sent to Domain Controller":::

- After the DC validates the TGT and SPN, it sends the session key to the client, for connecting to the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] SPN.

:::image type="content" source="media/sql-server-linux-ad-auth-understanding/ad-auth-explained-session-key-received.svg" alt-text="Active Directory authentication for SQL Server on Linux - session key returned to client by DC":::

- The encrypted blob from the session key is sent to the server.

:::image type="content" source="media/sql-server-linux-ad-auth-understanding/ad-auth-explained-session-key-sent.svg" alt-text="Active Directory authentication for SQL Server on Linux - session key sent to server":::

- [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] reads the password for the SPN from its keytab (`mssql.keytab`), which is a file on disk containing encrypted (SPN, password) tuples.

- [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] decrypts the encrypted blob from the client with the password it just looked up, to get the client's username.

- [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] looks up the client in the `sys.syslogins` table to check if the client is authorized to connect.

- The connection is either accepted or denied.

:::image type="content" source="media/sql-server-linux-ad-auth-understanding/ad-auth-explained-approved-or-denied.svg" alt-text="Active Directory authentication for SQL Server on Linux - connection accepted or denied":::

## Configure Kerberos for SQL Server containers

Active Directory authentication for [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] in containers is essentially the same as [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] on Linux. The only difference is the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] host SPN. In the previous scenario, the SPN was `MSSQLSvc/<host>:<port>` because we were connecting via the name of the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] host. Now however, we need to connect to the container.

For [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] containers, you can create the `krb5.conf` file inside the container. The host node running the container doesn't need to be part of the domain, but should be able to reach to the domain controller to which the container will try to connect.

Because we are connecting to a container, the server name in the client connection may be different than just the hostname. It could be the hostname, the container name, or another alias. In addition, there is a good chance that the exposed port for [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] won't be the default **1433**.

You will need to use the SPN that is stored in `mssql.keytab` to connect to the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] container. For example, if the SPN in `mssql.keytab` is `MSSQLSvc/sqlcontainer.domain.com:8000`, you would use `sqlcontainer.domain.com,8000` as your connection string in the client (including **sqlcmd**, SQL Server Management Studio, and Azure Data Studio).

:::image type="content" source="media/sql-server-linux-ad-auth-understanding/ad-auth-explained-container.svg" alt-text="Active Directory authentication for SQL Server Containers":::

## SQL Server group refresh

You may be wondering why there is a user account in the keytab if you only need a Service Principal Name to authenticate.

Imagine you have a user *adUser*, which is a member of a group *adGroup*. If *adGroup* is added as a login to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], that means *adUser* has permission to sign in to the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] instance as well. While *adUser* is still connected to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], a domain admin might remove *adUser* from *adGroup*. Now *adUser* should no longer have permission to sign in to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], but they have already passed the Kerberos authentication process and are connected.

We periodically run a process called *group refresh* to protect against a scenario where a connected user is no longer allowed to perform a privileged action (such as creating a login or altering a database).

[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] has a privileged Active Directory account which it uses for group refresh. This account is either configured using **mssql-conf** with the **network.privilegedadaccount** setting, or defaults to the machine account of the host machine (`<hostname>$`).

The credentials for the privileged account in `mssql.keytab` are used to impersonate the client (*adUser* in this example). [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] does a Kerberos handshake with itself to identify the group membership information, and compares it with `sys.syslogins` to check if *adUser* still has the permissions necessary to connect and execute the requested Transact-SQL commands. If *adUser* has been removed from *adGroup*, the connection is terminated by [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].

## Next steps

- [Troubleshooting Active Directory authentication for SQL Server on Linux and containers](sql-server-linux-ad-auth-troubleshooting.md)
- [Rotating Keytabs for SQL Server on Linux](sql-server-linux-ad-auth-rotate-keytabs.md)
- [Tutorial: Use adutil to configure Active Directory authentication with SQL Server on Linux](sql-server-linux-ad-auth-adutil-tutorial.md)
