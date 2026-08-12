---
title: Troubleshoot Active Directory Authentication for SQL Server on Linux and Containers
description: Troubleshoot Active Directory authentication issues with SQL Server on Linux and containers, configuration tips, common errors. Includes Kerberos, keytabs, and DNS.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: amitkh, atsingh
ms.date: 08/11/2026
ms.service: sql
ms.subservice: linux
ms.topic: troubleshooting-general
ms.custom:
  - linux-related-content
  - build-2025
monikerRange: ">=sql-server-linux-2017 || >=sql-server-2017 || =sqlallproducts-allversions"
---

# Troubleshoot Active Directory authentication for SQL Server on Linux and containers

[!INCLUDE [SQL Server - Linux](../../../includes/applies-to-version/sql-linux.md)]

This article helps you troubleshoot Active Directory Domain Services authentication issues with [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] on Linux and containers. It includes prerequisite checks and tips for a successful Active Directory configuration, and a list of common errors and troubleshooting steps.

## Validate current configuration

Before you begin troubleshooting, validate the current user, `mssql.conf`, Service Principal Name (SPN), and realm settings.

1. Obtain or renew the Kerberos TGT (ticket-granting ticket) with **`kinit`**:

   ```bash
   kinit privilegeduser@CONTOSO.COM
   ```

1. Run the following command, and make sure the user running it has access to the `mssql.keytab`:

   ```bash
   /opt/mssql/bin/mssql-conf validate-ad-config /var/opt/mssql/secrets/mssql.keytab
   ```

   For more information about the `validate-ad-config` command, run `/opt/mssql/bin/mssql-conf validate-ad-config --help`.

## DNS and reverse DNS lookups

1. DNS lookups on the domain name and NetBIOS name should return the same IP address, which normally matches the IP address for the domain controller (DC). Run these commands from the [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] host machine.

   ```bash
   nslookup contoso
   nslookup contoso.com
   ```

   If the IP addresses don't match, see [Join SQL Server on a Linux host to an Active Directory domain](active-directory-join-domain.md) to fix DNS lookups and communication with the DC.

1. Perform a reverse DNS (rDNS) lookup for each IP address from the previous results. Include IPv4 and IPv6 addresses where applicable.

   ```bash
   nslookup <IPs returned from the above commands>
   ```

   All should return `<hostname>.contoso.com`. Otherwise, check the PTR (pointer) records in Active Directory.

   You might have to work with your domain administrator to get rDNS working. If you can't add PTR entries for all the returned IP addresses, you can also [limit SQL Server to a subset of domain controllers](#validate-realm-information-in-krb5conf). This change affects any other services using `krb5.conf` on the host.

   For more information about reverse DNS, see [What is reverse DNS?](/azure/dns/dns-reverse-dns-overview#what-is-reverse-dns)

## Check keytab file and permissions

1. Check that you created the keytab (key table) file, and that you configured **`mssql-conf`** to use the correct file with appropriate permissions. The keytab must be accessible to the `mssql` user account. For more information, see [Use adutil to configure Active Directory authentication with SQL Server on Linux](adutil-tutorial.md#create-the-sql-server-service-keytab-file-using-mssql-conf).

1. Make sure that you can list the contents of the keytab, and that you added the correct SPNs, port, encryption type, and user account. If you don't type the passwords correctly when creating the SPNs and keytab entries, you encounter errors when you try to sign in with Active Directory authentication.

   ```bash
   klist -kte /var/opt/mssql/secrets/mssql.keytab
   ```

   An example of a working keytab follows. The example uses two encryption types, but you can use just one or more depending on the encryption types supported in your environment. In the example, `sqluser@CONTOSO.COM` is the privileged account (which matches the `network.privilegedadaccount` setting in **`mssql-conf`**), and the host name for [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] is `sqllinux.contoso.com` listening on the default port `1433`.

   ```bash
   $ kinit privilegeduser@CONTOSO.COM
   Password for privilegeduser@CONTOSO.COM:

   $ klist

   Ticket cache: FILE:/tmp/krb5cc_1000
   Default principal: privilegeduser@CONTOSO.COM
   Valid starting     Expires            Service principal
   01/26/22 20:42:02  01/27/22 06:42:02  krbtgt/CONTOSO.COM@CONTOSO.COM
       renew until 01/27/22 20:41:57

   $ klist -kte /var/opt/mssql/secrets/mssql.keytab

   Keytab name: FILE:/var/opt/mssql/secrets/mssql.keytab
   KVNO Timestamp         Principal
   ---- ----------------- --------------------------------------------------------
      2 01/13/22 13:19:47 MSSQLSvc/sqllinux@CONTOSO.COM (aes256-cts-hmac-sha1-96)
      2 01/13/22 13:19:47 MSSQLSvc/sqllinux@CONTOSO.COM (aes128-cts-hmac-sha1-96)
      2 01/13/22 13:19:47 MSSQLSvc/sqllinux.contoso.com@CONTOSO.COM (aes256-cts-hmac-sha1-96)
      2 01/13/22 13:19:47 MSSQLSvc/sqllinux.contoso.com@CONTOSO.COM (aes128-cts-hmac-sha1-96)
      2 01/13/22 13:19:47 MSSQLSvc/sqllinux:1433@CONTOSO.COM (aes256-cts-hmac-sha1-96)
      2 01/13/22 13:19:47 MSSQLSvc/sqllinux:1433@CONTOSO.COM (aes128-cts-hmac-sha1-96)
      2 01/13/22 13:19:47 MSSQLSvc/sqllinux.contoso.com:5533@CONTOSO.COM (aes256-cts-hmac-sha1-96)
      2 01/13/22 13:19:47 MSSQLSvc/sqllinux.contoso.com:5533@CONTOSO.COM (aes128-cts-hmac-sha1-96)
      2 01/13/22 13:19:55 sqluser@CONTOSO.COM (aes256-cts-hmac-sha1-96)
      2 01/13/22 13:19:55 sqluser@CONTOSO.COM (aes128-cts-hmac-sha1-96)
    ```

## Validate realm information in `krb5.conf`

1. In `krb5.conf` (located at `/etc/krb5.conf`), check that you provide values for the default realm, realm information, and domain to realm mapping. Review the following sample `krb5.conf` file. For more information, see [Understand Active Directory authentication for SQL Server on Linux and containers](understand-active-directory.md).

   ```ini
   [libdefaults]
   default_realm = CONTOSO.COM
   default_keytab_name = /var/opt/mssql/secrets/mssql.keytab
   default_ccache_name = ""

   [realms]
   CONTOSO.COM = {
       kdc = adVM.contoso.com
       admin_server = adVM.contoso.com
       default_domain= contoso.com
   }

   [domain_realm]
   .contoso.com = CONTOSO.COM
   contoso.com = CONTOSO.COM
   ```

1. You can restrict [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] to contact a subset of domain controllers, which is useful if your DNS configuration returns more domain controllers than [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] needs to contact. [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] on Linux lets you specify a list of domain controllers that [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] contacts in a round-robin fashion when performing a Lightweight Directory Access Protocol (LDAP) lookup.

   Complete these two steps. First, modify `krb5.conf` by adding the domain controllers you need, prefixed with `kdc =`.

   ```ini
   [realms]
   CONTOSO.COM = {
     kdc = kdc1.contoso.com
     kdc = kdc2.contoso.com
     ..
     ..
   }
   ```

   The `krb5.conf` file is a common Kerberos client configuration file, so any changes you make in this file affect other services in addition to [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)]. Before making any changes, consult your domain administrator.

   Enable the `network.enablekdcfromkrb5conf` setting with **`mssql-conf`**, and then restart [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)]:

   ```bash
   sudo /opt/mssql/bin/mssql-conf set network.enablekdcfromkrb5conf true
   sudo systemctl restart mssql-server
   ```

## Troubleshoot Kerberos

The following details help you troubleshoot Active Directory authentication issues and identify specific error messages.

### Trace Kerberos

After you create the user, SPNs, and keytabs, and configure **`mssql-conf`**, verify the Active Directory configuration.

To verify the configuration for [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] on Linux, use the privileged account to get or renew the Kerberos TGT. Run this command to show the Kerberos trace messages in the console (`stdout`):

```bash
root@sqllinux mssql# KRB5_TRACE=/dev/stdout kinit -kt /var/opt/mssql/secrets/mssql.keytab sqluser
```

If there aren't any problems, you should see output similar to the following sample. If not, the trace provides context about which steps to review.

```output
3791545 1640722276.100275: Getting initial credentials for sqluser@CONTOSO.COM
3791545 1640722276.100276: Looked up etypes in keytab: aes256-cts, aes128-cts
3791545 1640722276.100278: Sending unauthenticated request
3791545 1640722276.100279: Sending request (202 bytes) to CONTOSO.COM
3791545 1640722276.100280: Initiating TCP connection to stream 10.0.0.4:88
3791545 1640722276.100281: Sending TCP request to stream 10.0.0.4:88
3791545 1640722276.100282: Received answer (185 bytes) from stream 10.0.0.4:88
3791545 1640722276.100283: Terminating TCP connection to stream 10.0.0.4:88
3791545 1640722276.100284: Response was from master KDC
3791545 1640722276.100285: Received error from KDC: -1765328359/Additional pre-authentication required
3791545 1640722276.100288: Preauthenticating using KDC method data
3791545 1640722276.100289: Processing preauth types: PA-PK-AS-REQ (16), PA-PK-AS-REP_OLD (15), PA-ETYPE-INFO2 (19), PA-ENC-TIMESTAMP (2)
3791545 1640722276.100290: Selected etype info: etype aes256-cts, salt "CONTOSO.COMsqluser", params ""
3791545 1640722276.100291: Retrieving sqluser@CONTOSO.COM from /var/opt/mssql/secrets/mssql.keytab (vno 0, enctype aes256-cts) with result: 0/Success
3791545 1640722276.100292: AS key obtained for encrypted timestamp: aes256-cts/E84B
3791545 1640722276.100294: Encrypted timestamp (for 1640722276.700930): plain 301AA011180F32303231313XXXXXXXXXXXXXXXXXXXXXXXXXXXXX, encrypted 333109B95898D1B4FC1837DAE3E4CBD33AF8XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
3791545 1640722276.100295: Preauth module encrypted_timestamp (2) (real) returned: 0/Success
3791545 1640722276.100296: Produced preauth for next request: PA-ENC-TIMESTAMP (2)
3791545 1640722276.100297: Sending request (282 bytes) to CONTOSO.COM
3791545 1640722276.100298: Initiating TCP connection to stream 10.0.0.4:88
3791545 1640722276.100299: Sending TCP request to stream 10.0.0.4:88
3791545 1640722276.100300: Received answer (1604 bytes) from stream 10.0.0.4:88
3791545 1640722276.100301: Terminating TCP connection to stream 10.0.0.4:88
3791545 1640722276.100302: Response was from master KDC
3791545 1640722276.100303: Processing preauth types: PA-ETYPE-INFO2 (19)
3791545 1640722276.100304: Selected etype info: etype aes256-cts, salt "CONTOSO.COMsqluser", params ""
3791545 1640722276.100305: Produced preauth for next request: (empty)
3791545 1640722276.100306: AS key determined by preauth: aes256-cts/E84B
3791545 1640722276.100307: Decrypted AS reply; session key is: aes256-cts/05C0
3791545 1640722276.100308: FAST negotiation: unavailable
3791545 1640722276.100309: Initializing KCM:0:37337 with default princ sqluser@CONTOSO.COM
3791545 1640722276.100310: Storing sqluser@CONTOSO.COM -> krbtgt/CONTOSO.COM@CONTOSO.COM in KCM:0:37337
3791545 1640722276.100311: Storing config in KCM:0:37337 for krbtgt/CONTOSO.COM@CONTOSO.COM: pa_type: 2
3791545 1640722276.100312: Storing sqluser@CONTOSO.COM -> krb5_ccache_conf_data/pa_type/krbtgt/CONTOSO.COM@CONTOSO.COM@X-CACHECONF: in KCM:0:37337

$ sudo klist
Ticket cache: KCM:0:37337
Default principal: sqluser@CONTOSO.COM
Valid starting Expires Service principal
12/28/2021 20:11:16 12/29/2021 06:11:16 krbtgt/CONTOSO.COM@CONTOSO.COM
renew until 01/04/2022 20:11:16
```

### Enable Kerberos and security-based PAL logging

To identify specific error messages in the PAL (Platform Abstraction Layer), enable `security.kerberos` and `security.ldap` logging. Create a `logger.ini` file with the following content at `/var/opt/mssql/`, and then restart [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] to capture any initialization errors. Reproduce the failure. The PAL logs Active Directory error and debug messages to `/var/opt/mssql/log/security.log`.

```ini
[Output:security]
Type = File
Filename = /var/opt/mssql/log/security.log
[Logger]
Level = Silent
[Logger:security.kerberos]
Level = Debug
Outputs = security
[Logger:security.ldap]
Level = Debug
Outputs = security
```

[!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] picks up logger changes from `logger.ini` without a restart, but failures during Active Directory service initialization at [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] startup otherwise go unnoticed. Restarting [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] captures all the error messages.

The security log continues to write to the drive until you remove the changes in `logger.ini`. Disable `security.kerberos` and `security.ldap` logging once you identify and resolve the problem, to prevent running out of space on the drive.

The PAL logger generates log files in the following format:

```output
<DATETIME> <Log level> [<logger>] <<process/thread identifier>> <message>
```

For example, a sample line from the log follows:

```output
12/28/2021 13:56:31.609453055 Error [security.kerberos] <0003753757/0x00000324> Request ticket server MSSQLSvc/sql.contoso.com:1433@CONTOSO.COM kvno 3 enctype aes256-cts found in keytab but cannot decrypt ticket
```

Once you enable PAL logging and reproduce the issue, look for the first message with a log-level of `Error`. Use the following table to find the error and follow the guidance and recommendation to troubleshoot and resolve the issue.

## Common error messages

### Error message: "Login failed. The login is from an untrusted domain and cannot be used with Integrated authentication"

#### Possible cause

You encounter this error when you try to sign in with an Active Directory account after you configure Active Directory authentication.

#### Guidance

This generic error message requires you to [enable PAL logging](#enable-kerberos-and-security-based-pal-logging) to identify the specific error.

See the following list of common errors to identify the possible cause for each error, and then follow the troubleshooting guidance to resolve the issue.

| Error messages |
| --- |
| [Windows NT user or group 'CONTOSO\user' not found](#error-windows-nt) |
| [Could not look up short domain name due to error](#error-short-domain-name) |
| [Could not perform rDNS lookup for host \<hostname\> due to error](#error-rdns-lookup) |
| [FQDN not returned by rDNS lookup](#error-fqdn-not-returned) |
| [Failed to bind to LDAP server](#error-failed-to-bind-to-ldap-server) |
| [Key table entry not found](#error-key-table-entry-not-found) |
| [No key table entry found for \<principal\>](#error-no-key-table-entry-found) |
| [Request ticket server \<principal\> not found in keytab (ticket kvno \<KVNO\>)](#error-request-ticket-server-not-found-kvno) |
| [Request ticket server \<principal\> kvno \<KVNO\> found in keytab but not with enctype \<encryption type\>](#error-request-ticket-server-not-found-enctype) |
| [Request ticket server \<principal\> kvno \<KVNO\> enctype \<encryption type\> found in keytab but cannot decrypt ticket](#error-request-ticket-server-not-found-decrypt) |

<a id="error-windows-nt"></a>

### Error message: Windows NT user or group 'CONTOSO\user' not found

#### Possible cause

You might encounter this error when trying to create the Windows login, or during [group refresh](understand-active-directory.md#sql-server-group-refresh).

#### Guidance

To validate the issue, follow the guidance for "Login failed. The login is from an untrusted domain and cannot be used with Integrated authentication. (Microsoft SQL Server, Error: 18452)" and [enable PAL logging](#enable-kerberos-and-security-based-pal-logging) to identify the specific error, and troubleshoot accordingly.

<a id="error-short-domain-name"></a>

### Error message: "Could not look up short domain name due to error"

#### Possible cause

The Transact-SQL syntax to create an Active Directory login is:

```sql
CREATE LOGIN [CONTOSO\user]
    FROM WINDOWS;
```

The NetBIOS name (`CONTOSO`) is required in the command, but the FQDN of the domain (`contoso.com`) must be provided in the backend when performing an LDAP connection. To do this conversion, a DNS lookup is performed on `CONTOSO` to resolve to the IP of a domain controller, which can then be bound to for LDAP queries.

#### Guidance

The error message "Could not look up short domain name due to error" suggests that `nslookup` for `contoso` doesn't resolve to the IP address of the domain controller. Review [DNS and reverse DNS lookups](#dns-and-reverse-dns-lookups) to confirm that `nslookup` for both the NetBIOS and domain name match.

<a id="error-rdns-lookup"></a>

<a id="error-fqdn-not-returned"></a>

### Error messages: "Could not perform rDNS lookup for host \<hostname\> due to error" or "FQDN not returned by rDNS lookup"

#### Possible cause

These error messages usually indicate that the reverse DNS records (PTR records) don't exist for all domain controllers.

#### Guidance

Check the [DNS and reverse DNS lookups](#dns-and-reverse-dns-lookups). After you identify the domain controllers that don't have rDNS entries, you have two options:

- **Add rDNS entries for all domain controllers**

  This setting isn't a [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] setting, and you must configure it at the domain level. You might have to work with your domain administration team to create the required PTR records for all the domain controllers that `nslookup` returns for the domain name.

- **Restrict [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] to a subset of domain controllers**

  If you can't add PTR records for all returned domain controllers, you can [limit SQL Server to a subset of domain controllers](#validate-realm-information-in-krb5conf).

<a id="error-failed-to-bind-to-ldap-server"></a>

### Error message: "Failed to bind to LDAP server ldap://CONTOSO.COM:3268: Local Error"

#### Possible cause

This generic error from OpenLDAP normally means one of two things:

- No credentials
- rDNS problems

Here's an example of the error message:

```output
12/09/2021 14:32:11.319933684 Error [security.ldap] <0000000142/0x000001c0> Failed to bind to LDAP server ldap://[CONTOSO.COM:3268]: Local error
```

#### Guidance

- **No credentials**

  Other error messages appear first if credentials don't load for LDAP connections. [Enable PAL logging](#enable-kerberos-and-security-based-pal-logging) and check the error log for error messages before this one. If there aren't any other errors, it's most likely not a credentials issue. If you find an error, fix it before moving on. In most cases, it's one of the error messages this article covers.

- **rDNS problems**

  Check the [DNS and reverse DNS lookups](#dns-and-reverse-dns-lookups).

  When the OpenLDAP library connects to a domain controller, it provides either the fully qualified domain name (FQDN), which in this example is `contoso.com`, or the DC's FQDN (`kdc1.contoso.com`). After establishing the connection (but before returning success to the caller), the OpenLDAP library checks the IP of the server it connected to. It then performs a reverse DNS lookup and checks that the name of the server it connected to (`kdc1.contoso.com`) matches the requested domain (`contoso.com`). If it doesn't match, the OpenLDAP library fails the connection as a security feature. This mismatch is part of why the rDNS settings are important for [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] on Linux, and are the focus of this article.

<a id="error-key-table-entry-not-found"></a>

### Error message: "Key table entry not found"

#### Possible cause

This error indicates access issues with the keytab file or missing entries in the keytab.

#### Guidance

Make sure the keytab file has the correct access level and permissions. The default location and name for the keytab file is `/var/opt/mssql/secrets/mssql.keytab`. To view the current permissions on all files under the secrets folder, run this command:

```bash
sudo ls -lrt /var/opt/mssql/secrets
```

Use these commands to set the permissions and access level on the keytab file:

```bash
sudo chown mssql /var/opt/mssql/secrets/mssql.keytab
sudo chmod 440 /var/opt/mssql/secrets/mssql.keytab
```

For more information on listing the keytab entries and setting the correct permissions, see the previous [Check keytab file and permissions](#check-keytab-file-and-permissions) section. If you don't meet any of the conditions in that section, you see this error or an equivalent error: `"Key table entry not found"`.

<a id="error-no-key-table-entry-found"></a>

### Error message: "No key table entry found for \<principal\>"

#### Possible cause

When you try to retrieve the credentials for `<principal>` from the keytab, you find no applicable entries.

#### Guidance

To list all entries in the keytab, follow the [Check keytab file and permissions](#check-keytab-file-and-permissions) section of this article. Make sure that `<principal>` is present. In this case, the principal account is usually the `network.privilegedadaccount` to which you register the SPNs. If it isn't, add it with the **`adutil`** command. For more information, see [Use adutil to configure Active Directory authentication with SQL Server on Linux](adutil-tutorial.md#create-the-sql-server-service-keytab-file-using-mssql-conf).

<a id="error-request-ticket-server-not-found-kvno"></a>

### Error message: "Request ticket server \<principal\> not found in keytab (ticket kvno \<KVNO\>)"

#### Possible cause

This error indicates that [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] can't find a keytab entry for the requested ticket with the specified Key Version Number (KVNO).

#### Guidance

To list all entries in the keytab, follow the [Check keytab file and permissions](#check-keytab-file-and-permissions) section of this article. If you can't find an error message that matches the `<principal>` and KVNO, update the keytab file to add this entry, following the steps in that section.

You can also run the following command to get the latest KVNO from the DC. Before you run this command, obtain or renew the Kerberos TGT with the **`kinit`** command. For more information, see [Use adutil to create an Active Directory user for SQL Server and set the Service Principal Name (SPN)](adutil-tutorial.md#adutil-spn).

```bash
kvno MSSQLSvc/<hostname>
```

<a id="error-request-ticket-server-not-found-enctype"></a>

### Error message: "Request ticket server \<principal\> kvno \<KVNO\> found in keytab but not with enctype \<encryption type\>"

#### Possible cause

This error means that [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)]'s keytab doesn't contain the encryption type that the client requests.

#### Guidance

To validate, follow the [Check keytab file and permissions](#check-keytab-file-and-permissions) section of this article to list all entries in the keytab. If you can't find an error message that matches the principal, KVNO, and encryption type, update the keytab file to add this entry, following the steps in that section.

<a id="error-request-ticket-server-not-found-decrypt"></a>

### Error message: "Request ticket server \<principal\> kvno \<KVNO\> enctype \<encryption type\> found in keytab but cannot decrypt ticket"

#### Possible cause

This error message indicates that [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] can't use a credential from the keytab file to decrypt the incoming authentication request. An incorrect password often causes this error.

#### Guidance

Recreate the keytab with the correct password. If you use **`adutil`**, create the keytab with the right password and follow the steps in [Tutorial: Use adutil to configure Active Directory authentication with SQL Server on Linux](adutil-tutorial.md).

## Common ports

This table shows the common ports that [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] on Linux uses to configure and administer Active Directory authentication.

| Active Directory Service | Port |
| --- | --- |
| DNS | 53 |
| LDAP | 389 |
| LDAPS | 636 |
| Kerberos | 88 |

## Related content

- [Understand Active Directory authentication for SQL Server on Linux and containers](understand-active-directory.md)
- [Tutorial: Use adutil to configure Active Directory authentication with SQL Server on Linux](adutil-tutorial.md)
