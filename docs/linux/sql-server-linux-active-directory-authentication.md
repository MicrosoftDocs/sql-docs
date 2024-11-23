---
title: "Tutorial: Use Active Directory Authentication for SQL Server on Linux"
titleSuffix: SQL Server
description: This tutorial provides the configuration steps for Active Directory authentication for SQL Server on Linux.
author: amitkh-msft
ms.author: amitkh
ms.reviewer: vanto, randolphwest
ms.date: 11/18/2024
ms.service: sql
ms.subservice: linux
ms.topic: tutorial
ms.custom:
  - linux-related-content
helpviewer_keywords:
  - "Linux, AD authentication"
  - "Linux, Active Directory authentication"
---
# Tutorial: Use Active Directory authentication with SQL Server on Linux

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

This tutorial explains how to configure [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] on Linux to support Active Directory authentication, also known as integrated authentication. For an overview, see [Active Directory authentication for SQL Server on Linux](sql-server-linux-active-directory-auth-overview.md).

This tutorial consists of the following tasks:

> [!div class="checklist"]
> - Join [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] host to Active Directory domain
> - Create Active Directory user for [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] and set SPN
> - Configure [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] service keytab
> - Secure the keytab file
> - Configure [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] to use the keytab file for Kerberos authentication
> - Create Active Directory-based logins in Transact-SQL
> - Connect to [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] using Active Directory Authentication

## Prerequisites

Before you configure Active Directory Authentication, you need to:

- Set up an Active Directory Domain Controller (Windows) on your network
- Install [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)]
  - [Quickstart: Install SQL Server and create a database on Red Hat](quickstart-install-connect-red-hat.md)
  - [Quickstart: Install SQL Server and create a database on SUSE Linux Enterprise Server](quickstart-install-connect-suse.md)
  - [Quickstart: Install SQL Server and create a database on Ubuntu](quickstart-install-connect-ubuntu.md)

<a id="join"></a>

## Join SQL Server host to Active Directory domain

Join your [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Linux host with an Active Directory domain controller. For information on how to join an active directory domain, see [Join SQL Server on a Linux host to an Active Directory domain](sql-server-linux-active-directory-join-domain.md).

<a id="createuser"></a>

## Create Active Directory user for SQL Server and set SPN

> [!NOTE]  
> The following steps use your [fully qualified domain name](https://en.wikipedia.org/wiki/Fully_qualified_domain_name) (FQDN). If you're on Azure, you must **[create a FQDN](/azure/virtual-machines/linux/portal-create-fqdn)** before you proceed.

1. On your domain controller, run the [New-ADUser](/previous-versions/windows/it-pro/windows-server-2008-R2-and-2008/ee617253(v=technet.10)) PowerShell command to create a new Active Directory user with a password that never expires. The following example names the account `sqlsvc`, but the account name can be anything you like. You'll be prompted to enter a new password for the account.

   ```powershell
   Import-Module ActiveDirectory

   New-ADUser sqlsvc -AccountPassword (Read-Host -AsSecureString "Enter Password") -PasswordNeverExpires $true -Enabled $true
   ```

   > [!NOTE]  
   > It's a security best practice to have a dedicated Active Directory account for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], so that the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] instance credentials aren't shared with other services using the same account. However, you can optionally reuse an existing Active Directory account if you know the account's password (which is required to generate a keytab file in the next step). Additionally, the account should be enabled to support 128-bit and 256-bit Kerberos AES encryption (`msDS-SupportedEncryptionTypes` attribute) on the user account. To validate the account is enabled for AES encryption, locate the account in **Active Directory Users and Computers** utility, and select **Properties**. Locate the **Accounts** tab in **Properties**, and validate the two following checkboxes are selected.  
   >  
   > 1. **This account supports Kerberos AES 128 bit encryption**
   >  
   > 2. **This account supports Kerberos AES 256 bit encryption**

1. Set the ServicePrincipalName (SPN) for this account using the **setspn.exe** tool. The SPN must be formatted exactly as specified in the following example. You can find the fully qualified domain name of the [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] host machine by running `hostname --all-fqdns` on the [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] host. The TCP port should be 1433 unless you have configured [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] to use a different port number.

   ```powershell
   setspn -A MSSQLSvc/<fully qualified domain name of host machine>:<tcp port> sqlsvc
   setspn -A MSSQLSvc/<netbios name of the host machine>:<tcp port> sqlsvc
   ```

   > [!NOTE]  
   > If you receive an error, `Insufficient access rights`, check with your domain administrator that you have sufficient permissions to set an SPN on this account. The account that is used to register an SPN will need the `Write servicePrincipalName` permissions. For more information, see [Register a Service Principal Name for Kerberos connections](../database-engine/configure-windows/register-a-service-principal-name-for-kerberos-connections.md).
   >  
   > If you change the TCP port in the future, you must run the **setspn** command again with the new port number. You also need to add the new SPN to the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] service keytab by following the steps in the next section.

For more information, see [Register a Service Principal Name for Kerberos connections](../database-engine/configure-windows/register-a-service-principal-name-for-kerberos-connections.md).

<a id="configurekeytab"></a>

## Configure SQL Server service keytab

Configuring Active Directory authentication for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux requires an Active Directory user account and the SPN created in the previous section.

> [!IMPORTANT]  
> If the password for the Active Directory account is changed or the password for the account that the SPNs are assigned to is changed, you must update the keytab with the new password and Key Version Number (KVNO). Some services might also rotate the passwords automatically. Review any password rotation policies for the accounts in question and align them with scheduled maintenance activities to avoid unexpected downtime.

<a id="spn"></a>

### SPN keytab entries

1. Check the Key Version Number (KVNO) for the Active Directory account created in the previous step. Usually it's 2, but it could be another integer if you changed the account's password multiple times. On the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] host machine, run the following commands:

    - The below examples assume the `user` is in the `@CONTOSO.COM` domain. Modify the user and domain name to your user and domain name.

   ```bash
   kinit user@CONTOSO.COM
   kvno user@CONTOSO.COM
   kvno MSSQLSvc/<fully qualified domain name of host machine>:<tcp port>@CONTOSO.COM
   ```

   > [!NOTE]  
   > SPNs can take several minutes to propagate through your domain, especially if the domain is large. If you receive the error, `kvno: Server not found in Kerberos database while getting credentials for MSSQLSvc/<fully qualified domain name of host machine>:<tcp port>@CONTOSO.COM`, please wait a few minutes and try again.</br></br> The above commands will only work if the server has been joined to an Active Directory domain, which was covered in the previous section.

1. Using **[ktpass](/windows-server/administration/windows-commands/ktpass)**, add keytab entries for each SPN using the following commands on a Windows machine Command Prompt:

    - `<DomainName>\<UserName>` - Active Directory user account
    - `@CONTOSO.COM` - Use your domain name
    - `/kvno <#>` - Replace `<#>` with the KVNO obtained in an earlier step
    - `<password>` - [!INCLUDE [password-complexity](includes/password-complexity.md)]

   ```cmd
   ktpass /princ MSSQLSvc/<fully qualified domain name of host machine>:<tcp port>@CONTOSO.COM /ptype KRB5_NT_PRINCIPAL /crypto aes256-sha1 /mapuser <DomainName>\<UserName> /out mssql.keytab -setpass -setupn /kvno <#> /pass <password>

   ktpass /princ MSSQLSvc/<fully qualified domain name of host machine>:<tcp port>@CONTOSO.COM /ptype KRB5_NT_PRINCIPAL /crypto rc4-hmac-nt /mapuser <DomainName>\<UserName> /in mssql.keytab /out mssql.keytab -setpass -setupn /kvno <#> /pass <password>

   ktpass /princ MSSQLSvc/<netbios name of the host machine>:<tcp port>@CONTOSO.COM /ptype KRB5_NT_PRINCIPAL /crypto aes256-sha1 /mapuser <DomainName>\<UserName> /in mssql.keytab /out mssql.keytab -setpass -setupn /kvno <#> /pass <password>

   ktpass /princ MSSQLSvc/<netbios name of the host machine>:<tcp port>@CONTOSO.COM /ptype KRB5_NT_PRINCIPAL /crypto rc4-hmac-nt /mapuser <DomainName>\<UserName> /in mssql.keytab /out mssql.keytab -setpass -setupn /kvno <#> /pass <password>

   ktpass /princ <UserName>@CONTOSO.COM /ptype KRB5_NT_PRINCIPAL /crypto aes256-sha1 /mapuser <DomainName>\<UserName> /in mssql.keytab /out mssql.keytab -setpass -setupn /kvno <#> /pass <password>

   ktpass /princ <UserName>@CONTOSO.COM /ptype KRB5_NT_PRINCIPAL /crypto rc4-hmac-nt /mapuser <DomainName>\<UserName> /in mssql.keytab /out mssql.keytab -setpass -setupn /kvno <#> /pass <password>
   ```

   The previous commands allow both AES and RC4 encryption ciphers for Active Directory authentication. RC4 is an older encryption cipher and if a higher degree of security is required, you can choose to create the keytab entries with only the AES encryption cipher.

   > [!NOTE]  
   > The last two `UserName` entries must be in lowercase, or the permission authentication can fail.

1. After executing the previous commands, you should have a keytab file named `mssql.keytab`. Copy the file over to the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] machine under the folder `/var/opt/mssql/secrets`.

1. Secure the keytab file.

   Anyone with access to this keytab file can impersonate [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on the domain, so make sure you restrict access to the file such that only the mssql account has read access:

   ```bash
   sudo chown mssql:mssql /var/opt/mssql/secrets/mssql.keytab
   sudo chmod 400 /var/opt/mssql/secrets/mssql.keytab
   ```

1. The following configuration option needs to be set with the **mssql-conf** tool to specify the account to be used while accessing the keytab file.

   ```bash
   sudo mssql-conf set network.privilegedadaccount <username>
   ```

   > [!NOTE]  
   > Only include the username and not domainname\username or username@domain. [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] internally adds domain name as required along with this username when used.

1. Use the following steps to configure [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] to start using the keytab file for Kerberos authentication.

   ```bash
   sudo mssql-conf set network.kerberoskeytabfile /var/opt/mssql/secrets/mssql.keytab
   sudo systemctl restart mssql-server
   ```

   > [!TIP]  
   > Optionally, disable UDP connections to the domain controller to improve performance. In many cases, UDP connections consistently fail when connecting to a domain controller, so you can set config options in `/etc/krb5.conf` to skip UDP calls. Edit `/etc/krb5.conf` and set the following options:
   >  
   > ```bash
   > /etc/krb5.conf
   > [libdefaults]
   > udp_preference_limit=0
   > ```

At this point, you're ready to use Active Directory-based logins in [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)].

<a id="createsqllogins"></a>

## Create Active Directory-based logins in Transact-SQL

1. Connect to [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] and create a new, Active Directory-based login:

   ```sql
   CREATE LOGIN [CONTOSO\user]
       FROM WINDOWS;
   ```

1. Verify that the login is now listed in the [sys.server_principals](../relational-databases/system-catalog-views/sys-server-principals-transact-sql.md) system catalog view:

   ```sql
   SELECT name
   FROM sys.server_principals;
   ```

<a id="connect"></a>

## Connect to SQL Server using Active Directory authentication

Sign in to a client machine using your domain credentials. Now you can connect to [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] without reentering your password by using Active Directory authentication. If you create a login for an Active Directory group, any Active Directory user who is a member of that group can connect in the same way.

The specific connection string parameter for clients to use Active Directory authentication depends on which driver you're using. Consider the examples in the following sections.

### sqlcmd on a domain-joined Linux client

Sign in to a domain-joined Linux client using **ssh** and your domain credentials:

```bash
ssh -l user@contoso.com client.contoso.com
```

Make sure you've installed the [mssql-tools](sql-server-linux-setup-tools.md) package, then connect using **sqlcmd** without specifying any credentials:

```bash
sqlcmd -S mssql-host.contoso.com
```

Different from SQL Windows, Kerberos authentication works for local connection in SQL Linux. However, you still need to provide the FQDN of the SQL Linux host, and Active Directory authentication won't work if you attempt to connect to `.`, `localhost`, `127.0.0.1`, etc.

### SSMS on a domain-joined Windows client

Sign in to a domain-joined Windows client using your domain credentials. Make sure [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Management Studio is installed, then connect to your [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] instance (for example, `mssql-host.contoso.com`) by specifying **Windows Authentication** in the **Connect to Server** dialog.

### Active Directory authentication using other client drivers

The following table describes recommendations for other client drivers:

| Client driver | Recommendation |
| --- | --- |
| **JDBC** | Use Kerberos Integrated Authentication to Connect [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]. |
| **ODBC** | Use Integrated Authentication. |
| **ADO.NET** | Connection String Syntax. |

<a id="additionalconfig"></a>

## Additional configuration options

If you're using third-party utilities such as [PBIS](https://www.beyondtrust.com/), [VAS](https://www.oneidentity.com/products/one-identity-safeguard-authentication-services), or [Centrify](https://delinea.com/centrify) to join the Linux host to Active Directory domain and you would like to force [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] to use the OpenLDAP library directly, you can configure the `disablesssd` option with **mssql-conf** as follows:

```bash
sudo mssql-conf set network.disablesssd true
systemctl restart mssql-server
```

> [!NOTE]  
> There are utilities such as **realmd** which set up SSSD, while other tools such as PBIS, VAS and Centrify don't setup SSSD. If the utility used to join Active Directory domain doesn't setup SSSD, you should configure `disablesssd` option to `true`. While it's not required as [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] will attempt to use SSSD for Active Directory before falling back to OpenLDAP mechanism, it would be more performant to configure it so [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] makes OpenLDAP calls directly bypassing the SSSD mechanism.

If your domain controller supports LDAPS, you can force all connections from [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] to the domain controllers to be over LDAPS. To check your client can contact the domain controller over LDAPS, run the following bash command, `ldapsearch -H ldaps://contoso.com:3269`. To set [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] to only use LDAPS, run the following:

```bash
sudo mssql-conf set network.forcesecureldap true
systemctl restart mssql-server
```

This will use LDAPS over SSSD if Active Directory domain join on host was done via SSSD package and `disablesssd` isn't set to true. If `disablesssd` is set to true along with `forcesecureldap` being set to true, then it will use LDAPS protocol over OpenLDAP library calls made by [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)].

### Post SQL Server 2017 CU 14

Starting with [!INCLUDE [sssql17-md](../includes/sssql17-md.md)] CU 14, if [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] was joined to an Active Directory domain controller using third-party providers and is configured to use OpenLDAP calls for general Active Directory lookup by setting `disablesssd` to true, you can also use `enablekdcfromkrb5` option to force [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] to use krb5 library for KDC lookup instead of reverse DNS lookup for KDC server.

This might be useful for the scenario where you want to manually configure the domain controllers that [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] attempts to communicate with. And you use the OpenLDAP library mechanism by using the KDC list in `krb5.conf`.

First, set `disablesssd` and `enablekdcfromkrb5conf` to true and then restart [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]:

```bash
sudo mssql-conf set network.disablesssd true
sudo mssql-conf set network.enablekdcfromkrb5conf true
systemctl restart mssql-server
```

Then configure the KDC list in `/etc/krb5.conf` as follows:

```ini
[realms]
CONTOSO.COM = {
  kdc = dcWithGC1.contoso.com
  kdc = dcWithGC2.contoso.com
}
```

While it's not recommended, it's possible to use utilities, such as **realmd**, that set up SSSD while joining the Linux host to the domain, while configuring `disablesssd` to true so that [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] uses OpenLDAP calls instead of SSSD for Active Directory related calls.

> [!NOTE]  
> [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] login by using an FQDN (for example, `CONTOSO.COM\Username`) isn't supported. Use the `CONTOSO\Username` format.
>  
> [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] logins from Domain Local groups aren't supported. Use Global Security Domain groups instead.

## Related content

- [Encrypt connections to SQL Server on Linux](sql-server-linux-encrypted-connections.md)
- [Understand Active Directory authentication for SQL Server on Linux and containers](sql-server-linux-ad-auth-understanding.md)
- [Troubleshoot Active Directory authentication for SQL Server on Linux and containers](sql-server-linux-ad-auth-troubleshooting.md)

[!INCLUDE [contribute-to-content](../includes/paragraph-content/contribute-to-content.md)]
