---
title: Active Directory Authentication with SQL Server on Linux | Microsoft Docs
description: Configuration steps for AAD authentication for SQL Server on Linux
author: tmullaney
ms.date: 08/23/2017
ms.author: rickbyh 
manager: jhubbard
ms.topic: article
ms.prod: sql-linux
ms.technology: database-engine
helpviewer_keywords: 
  - "Linux, AAD authentication"
---
# Active Directory Authentication with SQL Server on Linux

[!INCLUDE[tsql-appliesto-sslinux-only](../includes/tsql-appliesto-sslinux-only.md)]

This tutorial explains how to configure [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] on Linux to support Active Directory (AD) authentication, also known as integrated authentication. AD Authentication enables domain-joined clients on either Windows or Linux to authenticate to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] using their domain credentials and the Kerberos protocol.

AD Authentication has the following advantages over [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Authentication:

* Users authenticate via single sign-on, without being prompted for a password.   
* By creating logins for AD groups, you can manage access and permissions in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] using AD group memberships.  
* Each user has a single identity across your organization, so you don’t have to keep track of which [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] logins correspond to which people.   
* AD enables you to enforce a centralized password policy across your organization.   


> [!div class="checklist"]
> * Join [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] host to AD domain
> * Create AD user for [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] and set SPN
> * Configure [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] service keytab
> * Create AD-based logins in Transact-SQL
> * Connect to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] using AD Authentication

## Prerequisites

Before you configure AD Authentication, you need to:

* Set up an AD Domain Controller (Windows) on your network  
* Install [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]
  * [Red Hat Enterprise Linux](quickstart-install-connect-red-hat.md)
  * [SUSE Linux Enterprise Server](quickstart-install-connect-suse.md)
  * [Ubuntu](quickstart-install-connect-ubuntu.md)

> [!IMPORTANT]
> At this time, the only authentication method supported for database mirroring endpoint is CERTIFICATE. WINDOWS authentication method will be enabled in a future release.

## Join [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] host to AD domain

1. Use **[realmd](https://www.freedesktop.org/software/realmd/docs/guide-active-directory-join.html)** to join your host machine to your AD Domain. If you haven't already, install both the realmd and Kerberos client packages on the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] host machine using your Linux distribution's package manager:

   ```bash
   # RHEL
   sudo yum install realmd krb5-workstation

   # SUSE
   sudo zypper install realmd krb5-client

   # Ubuntu
   sudo apt-get install realmd krb5-user software-properties-common python-software-properties packagekit
   ```

2. If the Kerberos client package installation prompts you for a realm name, enter your domain name in uppercase.

   > [!NOTE]
   > This walkthrough uses "contoso.com" and "CONTOSO.COM" as example domain and realm names, respectively. You should replace these with your own values. These commands are case-sensitive, so make sure you use uppercase wherever it is used in this walkthrough.

3. Configure your [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] host machine to use your AD domain controller's IP address as a DNS nameserver. 

  3.1 Ubuntu

  Edit the `/etc/network/interfaces` file so that your AD domain controller's IP address is listed as a dns-nameserver. For example: 

   ```/etc/network/interfaces
   <...>
   # The primary network interface
   auth eth0
   iface eth0 inet dhcp
   dns-nameservers **<AD domain controller IP address>**
   dns-search **<AD domain name>**
   ```

  > [!NOTE]
  > The network interface (eth0) might differ for differnet machines. To find out which one you are using, run ifconfig and copy the interface that has an IP address and transmitted and received bytes.

  After editing this file, restart the network service:

  ```bash
  sudo ifdown eth0 && sudo ifup eth0
  ```

  Now check that your `/etc/resolv.conf` file contains a line like the following:  

  ```Code
  nameserver **<AD domain controller IP address>**
  ```

    3.2 RHEL

  Edit the `/etc/sysconfig/network-scripts/ifcfg-eth0` file (or other interface config file as appropriate) so that your AD domain controller's IP address is listed as a DNS server:

  ```/etc/sysconfig/network-scripts/ifcfg-eth0
  <...>
  PEERDNS=no
  DNS1=**<AD domain controller IP address>**
  ```

  After editing this file, restart the network service:

  ```bash
  sudo systemctl restart network
  ```

  Now check that your `/etc/resolv.conf` file contains a line like the following:  

  ```Code
  nameserver **<AD domain controller IP address>**
  ```

5. Join the domain

  Once you've confirmed that your DNS is configured properly, join the domain by running the command below. You'll need to authenticate using an AD account that has sufficient privileges in AD to join a new machine to the domain.

  Specifically, this command will create a new computer account in AD, create the `/etc/krb5.keytab` host keytab file, and configure the domain in `/etc/sssd/sssd.conf`:

  ```bash
  sudo realm join contoso.com -U 'user@CONTOSO.COM' -v
  <...>
   * Successfully enrolled machine in realm
  ```

  > [!NOTE]
  > If you see an error, "Necessary packages are not installed," then you should install those packages using your Linux distribution's package manager before running the `realm join` command again.
  >
  > If you receive an error, "Insufficient permissions to join the domain," then you will need to check with a domain administrator that you have sufficient permissions to join Linux machines to your domain.

6. Verify that you can now gather information about a user from the domain, and that you can acquire a Kerberos ticket as that user.

  We will use **id**, **[kinit](https://web.mit.edu/kerberos/krb5-1.12/doc/user/user_commands/kinit.html)** and **[klist](https://web.mit.edu/kerberos/krb5-1.12/doc/user/user_commands/klist.html)** commands for this.

  ```bash
  id user@contoso.com
  uid=1348601103(user@contoso.com) gid=1348600513(domain group@contoso.com) groups=1348600513(domain group@contoso.com)

  kinit user@CONTOSO.COM
  Password for user@CONTOSO.COM:

  klist
  Ticket cache: FILE:/tmp/krb5cc_1000
  Default principal: user@CONTOSO.COM
  <...>
  ```

  > [!NOTE]
  > If `id user@contoso.com` returns, "No such user," make sure that the SSSD service started successfully by running the command `sudo systemctl status sssd`. If the service is running and you still see the "No such user" error, try enabling verbose logging for SSSD. For more information, see the Red Hat documentation for [Troubleshooting SSSD](https://access.redhat.com/documentation/Red_Hat_Enterprise_Linux/7/html/System-Level_Authentication_Guide/trouble.html#SSSD-Troubleshooting).
  >
  > If `kinit user@CONTOSO.COM` returns, "KDC reply did not match expectations while getting initial credentials," make sure you specified the realm in uppercase.

  For more information, see the Red Hat documentation for [Discovering and Joining Identity Domains](https://access.redhat.com/documentation/Red_Hat_Enterprise_Linux/7/html/Windows_Integration_Guide/realmd-domain.html). 

## Create AD user for [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] and set SPN

> [!NOTE]
> In the next steps we will use your [fully qualified domain name](https://en.wikipedia.org/wiki/Fully_qualified_domain_name). If you are on **Azure**, you will have to **[create one](https://docs.microsoft.com/en-us/azure/virtual-machines/linux/portal-create-fqdn)** before you proceed.

1. On your domain controller, run the [New-ADUser](https://technet.microsoft.com/library/ee617253.aspx) PowerShell command to create a new AD user with a password that never expires. This example names the account "mssql," but the account name can be anything you like. You will be prompted to enter a new password for the account:

  ```PowerShell
  Import-Module ActiveDirectory

  New-ADUser mssql -AccountPassword (Read-Host -AsSecureString "Enter Password") -PasswordNeverExpires $true -Enabled $true
  ```

  > [!NOTE]
  > It is a security best practice to have a dedicated AD account for SQL Server, so that SQL Server's credentials aren't shared with other services using the same account. However, you can reuse an existing AD account if you prefer, if you know the account's password (required to generate a keytab file in the next step).

2. Set the ServicePrincipalName (SPN) for this account using the `setspn.exe` tool. The SPN must be formatted exactly as specified in the following example: You can find the fully qualified domain name of the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] host machine by running `hostname --all-fqdns` on the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] host, and the TCP port should be 1433 unless you have configured [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] to use a different port number.

  ```PowerShell
  setspn -A MSSQLSvc/**<fully qualified domain name of host machine>**:**<tcp port>** mssql
  ```

  > [!NOTE]
  > If you receive an error, "Insufficient access rights," then you need to check with a domain administrator that you have sufficient permissions to set an SPN on this account.
  >
  > If you change the TCP port in the future, then you will need to run the setspn command again with the new port number. You will also need to add the new SPN to the SQL Server service keytab by following the steps in the next section.

3. For more information, see [Register a Service Principal Name for Kerberos Connections](../database-engine/configure-windows/register-a-service-principal-name-for-kerberos-connections.md).

## Configure [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] service keytab

1. Check the Key Version Number (kvno) for the AD account created in the previous step. Usually it will be 2, but it could be another integer if you changed the account's password multiple times. On the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] host machine, run the following:

  ```bash
  kinit user@CONTOSO.COM

  kvno MSSQLSvc/**<fully qualified domain name of host machine>**:**<tcp port>**
  ```

2. Create a keytab file for the AD user you created in the previous step. To do so we will use **[ktutil](https://web.mit.edu/kerberos/krb5-1.12/doc/admin/admin_commands/ktutil.html)**. When prompted, enter the password for that AD account.

  ```bash
  sudo ktutil

  ktutil: addent -password -p MSSQLSvc/**<fully qualified domain name of host machine>**:**<tcp port>**@CONTOSO.COM -k **<kvno from above>** -e aes256-cts-hmac-sha1-96

  ktutil: addent -password -p MSSQLSvc/**<fully qualified domain name of host machine>**:**<tcp port>**@CONTOSO.COM -k **<kvno from above>** -e rc4-hmac

  ktutil: wkt /var/opt/mssql/secrets/mssql.keytab

  quit
  ```

  > [!NOTE]
  > The ktutil tool does not validate the password, so make sure you enter it correctly.

3. Anyone with access to this `keytab` file can impersonate [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] on the domain, so make sure you restrict access to the file such that only the `mssql` account has read access:

  ```bash
  sudo chown mssql:mssql /var/opt/mssql/secrets/mssql.keytab
  sudo chmod 400 /var/opt/mssql/secrets/mssql.keytab
  ```

4. Configure [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] to use this `keytab` file for Kerberos authentication:

  ```bash
  sudo /opt/mssql/bin/mssql-conf set network.kerberoskeytabfile /var/opt/mssql/secrets/mssql.keytab
  sudo systemctl restart mssql-server
  ```

## Create AD-based logins in Transact-SQL

1. Connect to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] and create a new, AD-based login:

  ```sql
  CREATE LOGIN [CONTOSO\user] FROM WINDOWS;
  ```

2. Verify that the login is now listed in the [sys.server_principals](../relational-databases/system-catalog-views/sys-server-principals-transact-sql.md) system catalog view:

  ```sql
  SELECT name FROM sys.server_principals;
  ```

## Connect to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] using AD Authentication

Log in to a client machine using your domain credentials. Now you can connect to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] without reentering your password, by using AD Authentication. If you create a login for an AD group, any AD user who is a member of that group can connect in the same way.

The specific connection string parameter for clients to use AD Authentication depends on which driver you are using. A few examples are below.

1. `sqlcmd` on a domain-joined Linux client

  Log in to a domain-joined Linux client using `ssh` and your domain credentials:

  ```bash
  ssh -l user@contoso.com client.contoso.com
  ```

  Make sure you've installed the [mssql-tools](sql-server-linux-setup-tools.md) package, then connect using `sqlcmd` without specifying any credentials:

  ```bash
  sqlcmd -S mssql.contoso.com
  ```

2. SSMS on a domain-joined Windows client

  Log in to a domain-joined Windows client using your domain credentials. Make sure [!INCLUDE[ssmanstudiofull-md](../includes/ssmanstudiofull-md.md)] is installed, then connect to your [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] instance by specifying **Windows Authentication** in the **Connect to Server** dialog.

3. AD Authentication using other client drivers

  * JDBC: [Using Kerberos Integrated Authentication to Connect SQL Server](https://docs.microsoft.com/sql/connect/jdbc/using-kerberos-integrated-authentication-to-connect-to-sql-server)
  * ODBC: [Using Integrated Authentication](https://docs.microsoft.com/sql/connect/odbc/linux/using-integrated-authentication)
  * ADO.NET: [Connection String Syntax](https://msdn.microsoft.com/en-us/library/system.data.sqlclient.sqlauthenticationmethod(v=vs.110).aspx)
