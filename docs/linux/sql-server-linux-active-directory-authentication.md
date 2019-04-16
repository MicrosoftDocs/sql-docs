---
title: "Tutorial: Use AD authentication for SQL Server on Linux"
titleSuffix: SQL Server
description: This tutorial provides the configuration steps for AD authentication for SQL Server on Linux.
author: Dylan-MSFT
ms.author: Dylan.Gray
ms.reviewer: rothja
ms.date: 04/01/2019
manager: craigg
ms.topic: tutorial
ms.prod: sql
ms.custom: "seodec18"
ms.technology: linux
helpviewer_keywords: 
  - "Linux, AAD authentication"
---

# Tutorial: Use Active Directory authentication with SQL Server on Linux

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-linuxonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-linuxonly.md)]

This tutorial explains how to configure [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] on Linux to support Active Directory (AD) authentication, also known as integrated authentication. For an overview, see [Active Directory authentication for SQL Server on Linux](sql-server-linux-active-directory-auth-overview.md).

This tutorial consists of the following tasks:

> [!div class="checklist"]
> * Join [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] host to AD domain
> * Create AD user for [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] and set SPN
> * Configure [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] service keytab
> * Secure the keytab file
> * Configure SQL Server to use the keytab file for Kerberos authentication
> * Create AD-based logins in Transact-SQL
> * Connect to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] using AD Authentication

## Prerequisites

Before you configure AD Authentication, you need to:

* Set up an AD Domain Controller (Windows) on your network  
* Install [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]
  * [Red Hat Enterprise Linux (RHEL)](quickstart-install-connect-red-hat.md)
  * [SUSE Linux Enterprise Server (SLES)](quickstart-install-connect-suse.md)
  * [Ubuntu](quickstart-install-connect-ubuntu.md)

## <a id="join"></a> Join [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] host to AD domain

You must join your SQL Server Linux host with an Active Directory domain controller. For information on how to join an active directory domain, see [Join SQL Server on a Linux host to an Active Directory domain](sql-server-linux-active-directory-join-domain.md).

## <a id="createuser"></a> Create AD user (or MSA) for [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] and set SPN

> [!NOTE]
> The following steps use your [fully qualified domain name](https://en.wikipedia.org/wiki/Fully_qualified_domain_name). If you are on **Azure**, you must **[create one](https://docs.microsoft.com/azure/virtual-machines/linux/portal-create-fqdn)** before you proceed.

1. On your domain controller, run the [New-ADUser](https://technet.microsoft.com/library/ee617253.aspx) PowerShell command to create a new AD user with a password that never expires. The following example names the account `mssql`, but the account name can be anything you like. You will be prompted to enter a new password for the account.

   ```PowerShell
   Import-Module ActiveDirectory

   New-ADUser mssql -AccountPassword (Read-Host -AsSecureString "Enter Password") -PasswordNeverExpires $true -Enabled $true
   ```

   > [!NOTE]
   > It is a security best practice to have a dedicated AD account for SQL Server, so that SQL Server's credentials aren't shared with other services using the same account. However, you can optionally reuse an existing AD account if you know the account's password (which is required to generate a keytab file in the next step).

2. Set the ServicePrincipalName (SPN) for this account using the **setspn.exe** tool. The SPN must be formatted exactly as specified in the following example. You can find the fully qualified domain name of the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] host machine by running `hostname --all-fqdns` on the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] host. The TCP port should be 1433 unless you have configured [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] to use a different port number.

   ```PowerShell
   setspn -A MSSQLSvc/**<fully qualified domain name of host machine>**:**<tcp port>** mssql
   setspn -A MSSQLSvc/**<netbios name of the host machine>**:**<tcp port>** mssql
   ```

   > [!NOTE]
   > If you receive an error, `Insufficient access rights`, check with your domain administrator that you have sufficient permissions to set an SPN on this account.
   >
   > If you change the TCP port in the future, you must run the **setspn** command again with the new port number. You also need to add the new SPN to the SQL Server service keytab by following the steps in the next section.

For more information, see [Register a Service Principal Name for Kerberos Connections](../database-engine/configure-windows/register-a-service-principal-name-for-kerberos-connections.md).

## <a id="configurekeytab"></a> Configure [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] service keytab

There are two different ways to configure the SQL Server service keytab files. The first option is to use a machine account (UPN), while second option uses a Managed Service Account (MSA) in the keytab configuration. Both mechanisms are equally functional, and you can choose the method that works best for your environment.

In both cases, the SPN created in the earlier step is required, and the SPN must be registered in the keytab.

To configure the SQL Server service keytab file:

1. Configure the [SPN keytab entries](#spn) in the next section.

1. Then either [add UPN](#upn) (option 1) or [MSA](#msa) (option 2) entries in the keytab file by following the steps in their respective sections.

> [!IMPORTANT]
> If the password for the UPN/MSA is changed or the password for the account that the SPNs are assigned to is changed, you must update the keytab with the new password and Key Version Number (KVNO). Some services might also rotate the passwords automatically. Review any password rotation policies for the accounts in question and align them with scheduled maintenance activities to avoid unexpected downtime.

### <a id="spn"></a> SPN keytab entries

1. Check the Key Version Number (KVNO) for the AD account created in the previous step. Usually it is 2, but it could be another integer if you changed the account's password multiple times. On the SQL Server host machine, run the following commands:

   ```bash
   kinit user@CONTOSO.COM
   kvno MSSQLSvc/**<fully qualified domain name of host machine>**:**<tcp port>**
   ```

   > [!NOTE]
   > SPNs can take several minutes to propagate through your domain, especially if the domain is large. If you receive the error, `kvno: Server not found in Kerberos database while getting credentials for MSSQLSvc/**<fully qualified domain name of host machine>**:**<tcp port>**@CONTOSO.COM`, please wait a few minutes and try again.  

1. Start **ktutil**:

   ```bash
   sudo ktutil
   ```

1. Add keytab entries for each SPN using the following commands:

   ```bash
   addent -password -p MSSQLSvc/**<fully qualified domain name of host machine>**:**<tcp port>**@CONTOSO.COM -k **<kvno from above>** -e aes256-cts-hmac -sha1-96
   addent -password -p MSSQLSvc/**<fully qualified domain name of host machine>**:**<tcp port>**@CONTOSO.COM -k **<kvno from above>** -e rc4-hmac
   addent -password -p MSSQLSvc/**<netbios name of the host machine>**:**<tcp port>**@CONTOSO.COM -k **<kvno from above>** -e aes256-cts-hmac -sha1-96
   addent -password -p MSSQLSvc/**<netbios name of the host machine>**:**<tcp port>**@CONTOSO.COM -k **<kvno from above>** -e rc4-hmac
   ```

1. Write the keytab to a file and then quit ktutil:

   ```bash
   wkt /var/opt/mssql/secrets/mssql.keytab
   quit
   ```

   > [!NOTE]
   > The **ktutil** tool does not validate the password, so make sure you enter it correctly when prompted.

### <a id="upn"></a> Option 1: Using UPN to configure the keytab

Add the machine account to your keytab with **ktutil**. The machine account (also called a UPN) is present in **/etc/krb5.keytab** in the form `<hostname>$@<realm.com>` (for example, `sqlhost$@CONTOSO.COM`). Copy these entries from **/etc/krb5.keytab** to **mssql.keytab**.

1. Start **ktuil** with the following command:

   ```bash
   sudo ktutil
   ```

1. Use the **rkt** command to read all of the entries from **/etc/krb5.keytab**.

   ```bash
   rkt /etc/krb5.keytab
   ```

1. Next, list out the entries.

   ```bash
   list
   ```

1. Delete all the entries by their slot number that are not the UPN. Do this one at a time by repeating the following command:

   ```bash
   delent <slot num>
   ```

   > [!IMPORTANT]
   > When an entry is deleted, such as slot 1, all values slide up by one to take its place. This means the entry in slot 2 moves to slot 1 when slot 1's entry is deleted.

1. List out the entries again until only UPN entries are left.

   ```bash
   list
   ```

1. When only UPN entries are left, append these values to **mssql.keytab**:

   ```bash
   wkt /var/opt/mssql/secrets/mssql.keytab
   ```

1. Quit **ktutil**.

   ```bash
   quit
   ```

### <a id="msa"></a> Option 2:  Using MSA to configure the keytab

For the MSA option, you must create SQL Server's Kerberos keytab. It should contain all of the [SPNs registered in the first step](#spn) and the credentials for the MSA to which the SPNs are registered. 

1. After the SPN keytab entries are created, run the following commands from a Linux machine that is domain joined:

   ```bash
   kinit <AD user>
   kvno <any SPN registered in step 1>
      <spn>@CONTOSO.COM: kvno = <KVNO>
   ```

   This step displays the KVNO for the user account assigned the SPN ownership. For this step to work, the SPN must have been assigned to the MSA account during its creation. If the SPN was not assigned to MSA, the KVNO displayed will be of current SPN owner account and be incorrect to use for configuration.  

1. Start **ktutil**:

   ```bash
   sudo ktutil
   ```

1. Add the MSA with the following two commands:

   ```bash
   addent -password -p <MSA> -k <kvno from command above> -e aes256-cts-hmac-sha1-96
   addent -password -p <MSA> -k <kvno from command above> -e rc4-hmac
   ```

1. Write the keytab to a file and then quit ktutil:

   ```bash
   wkt /var/opt/mssql/secrets/mssql.keytab
   quit
   ```

1. When using the MSA approach, a configuration option needs to be set with the **mssql-conf** tool to specify the MSA to be used while accessing the keytab file. Ensure the values below are in **/var/opt/mssql/mssql.conf**.

   ```bash
   sudo mssql-conf set network.privilegedadaccount <MSA_Name>
   ```

   > [!NOTE]
   > Only include the MSA name and not the domain\account name.

## <a id="securekeytab"></a> Secure the keytab file

Anyone with access to this keytab file can impersonate SQL Server on the domain, so make sure you restrict access to the file such that only the mssql account has read access:

```bash
sudo chown mssql:mssql /var/opt/mssql/secrets/mssql.keytab
sudo chmod 400 /var/opt/mssql/secrets/mssql.keytab
```

## <a id="keytabkerberos"></a> Configure SQL Server to use the keytab file for Kerberos authentication

Use following steps to configure the SQL Server to start using the keytab file for Kerberos authentication.

```bash
sudo mssql-conf set network.kerberoskeytabfile /var/opt/mssql/secrets/mssql.keytab
sudo systemctl restart mssql-server
```

Optionally disable UDP connections to the domain controller to improve performance. In many cases, UDP connections consistently fail when connecting to a domain controller, so you can set config options in **/etc/krb5.conf** to skip UDP calls. Edit **/etc/krb5.conf** and set the following options:

```/etc/krb5.conf
[libdefaults]
udp_preference_limit=0
```

At this point, you are ready to use AD-based logins in SQL Server as follows.

## <a id="createsqllogins"></a> Create AD-based logins in Transact-SQL

1. Connect to SQL Server and create a new, AD-based login:

   ```sql
   CREATE LOGIN [CONTOSO\user] FROM WINDOWS;
   ```

1. Verify that the login is now listed in the [sys.server_principals](../relational-databases/system-catalog-views/sys-server-principals-transact-sql.md) system catalog view:

   ```sql
   SELECT name FROM sys.server_principals;
   ```

## <a id="connect"></a> Connect to SQL Server using AD Authentication

Log in to a client machine using your domain credentials. Now you can connect to SQL Server without reentering your password by using AD Authentication. If you create a login for an AD group, any AD user who is a member of that group can connect in the same way.

The specific connection string parameter for clients to use AD Authentication depends on which driver you are using. Consider the examples in the following sections.

### sqlcmd on a domain-joined Linux client

Log in to a domain-joined Linux client using **ssh** and your domain credentials:

```bash
ssh -l user@contoso.com client.contoso.com
```

Make sure you've installed the [mssql-tools](sql-server-linux-setup-tools.md) package, then connect using **sqlcmd** without specifying any credentials:

```bash
sqlcmd -S mssql-host.contoso.com
```

### SSMS on a domain-joined Windows client

Log in to a domain-joined Windows client using your domain credentials. Make sure SQL Server Management Studio is installed, then connect to your SQL Server instance (for example, `mssql-host.contoso.com`) by specifying **Windows Authentication** in the **Connect to Server** dialog.

### AD Authentication using other client drivers

The following table describes recommendations for other client drivers:

| Client driver | Recommendation |
|---|---|
| **JDBC** | Use Kerberos Integrated Authentication to Connect SQL Server. |
| **ODBC** | Use Integrated Authentication. |
| **ADO.NET** | Connection String Syntax. |

## <a id="additionalconfig"></a> Additional configuration options

If you are using third-party utilities such as [PBIS](https://www.beyondtrust.com/), [VAS](https://www.oneidentity.com/products/authentication-services/), or [Centrify](https://www.centrify.com/) to join the Linux host to AD domain and you would like to force SQL server in using the openldap library directly, you can configure the **disablesssd** option with **mssql-conf** as follows:

```bash
sudo mssql-conf set network.disablesssd true
systemctl restart mssql-server
```

> [!NOTE]
> There are utilities such as **realmd** which set up SSSD, while other tools such as PBIS, VAS and Centrify do not setup SSSD. If the utility used to join AD domain does not setup SSSD, it is recommended to configure **disablesssd** option to `true`. While it is not required as SQL Server will attempt to use SSSD for AD before falling back to openldap mechanism, it would be more performant to configure it so SQL Server makes openldap calls directly bypassing the SSSD mechanism.

If your domain controller supports LDAPS, you can force all connections from SQL Server to the domain controllers to be over LDAPS. To check your client can contact the domain controller over ldaps, run the following bash command, `ldapsearch -H ldaps://contoso.com:3269`. To set SQL Server to only use LDAPS, run the following:

```bash
sudo mssql-conf set network.forcesecureldap true
systemctl restart mssql-server
```

This will use LDAPS over SSSD if AD domain join on host was done via SSSD package and **disablesssd** is not set to true. If **disablesssd** is set to true along with **forcesecureldap** being set to true, then it will use LDAPS protocol over openldap library calls made by SQL Server.

### Post SQL Server 2017 CU14

Starting with SQL Server 2017 CU14, if SQL Server was joined to an AD domain controller using third-party providers and is configured to use openldap calls for general AD lookup by setting **disablesssd** to true, you can also use **enablekdcfromkrb5** option to force SQL Server to use krb5 library for KDC lookup instead of reverse DNS lookup for KDC server.

This may be useful for the scenario where you want to manually configure the domain controllers that SQL Server attempts to communicate with. And you use the openldap library mechanism by using the KDC list in **krb5.conf**.

First, set **disablessd** and **enablekdcfromkrb5conf** to true and then restart SQL Server:

```bash
sudo mssql-conf set network.disablesssd true
sudo mssql-conf set network.enablekdcfromkrb5conf true
systemctl restart mssql-server
```

Then configure the KDC list in **/etc/krb5.conf** as follows:

```/etc/krb5.conf
[realms]
CONTOSO.COM = {
  kdc = dcWithGC1.contoso.com
  kdc = dcWithGC2.contoso.com
}
```

> [!NOTE]
> While it is not recommended, it is possible to use utilities, such as **realmd**, that set up SSSD while joining the Linux host to the domain, while configuring **disablesssd** to true so that SQL Server uses openldap calls instead of SSSD for Active Directory related calls.

## Next steps

In this tutorial, we walked through how to set up Active Directory authentication with SQL Server on Linux. You learned how to:
> [!div class="checklist"]
> * Join [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] host to AD domain
> * Create AD user for [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] and set SPN
> * Configure [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] service keytab
> * Create AD-based logins in Transact-SQL
> * Connect to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] using AD Authentication

Next, explore other security scenarios for SQL Server on Linux.

> [!div class="nextstepaction"]
>[Encrypting Connections to SQL Server on Linux](sql-server-linux-encrypted-connections.md)
