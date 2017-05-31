---
# required metadata

title: "Active Directory Authentication with SQL Server on Linux | Microsoft Docs"
description: "Configuration steps for AAD authentication for SQL Server on Linux"
author: "tmullaney" 
ms.date: "06/14/2017"
ms.author: "thmullan;rickbyh" 
manager: "jhubbard"
ms.topic: "article"
ms.prod: "sql-linux"
ms.technology: "database-engine"
ms.assetid: 
helpviewer_keywords: 
  - "Linux, AAD authentication"

---
# Active Directory Authentication with SQL Server on Linux  
[!INCLUDE[tsql-appliesto-sslinx-only_md](../../docs/includes/tsql-appliesto-sslinx-only_md.md)]


This document explains how to configure [!INCLUDE[ssNoVersion](../../docs/includes/ssnoversion-md.md)] on Linux to support Active Directory (AD) authentication, also known as integrated authentication. AD Authentication enables domain-joined clients on either Windows or Linux to authenticate to [!INCLUDE[ssNoVersion](../../docs/includes/ssnoversion-md.md)] using their domain credentials and the Kerberos protocol. 
AD Authentication has the following advantages over [!INCLUDE[ssNoVersion](../../docs/includes/ssnoversion-md.md)] Authentication:  
•	Users authenticate via single sign-on, without being prompted for a password.   
•	By creating logins for AD groups, you can manage access and permissions in [!INCLUDE[ssNoVersion](../../docs/includes/ssnoversion-md.md)] using AD group memberships.  
•	Each user has a single identity across your organization, so you don’t have to keep track of which [!INCLUDE[ssNoVersion](../../docs/includes/ssnoversion-md.md)] logins correspond to which people.   
•	AD enables you to enforce a centralized password policy across your organization.   

## Prerequisites
Before you configure AD Authentication, you need to:  
- Set up an AD Domain Controller (Windows) on your network  
- Install [!INCLUDE[ssNoVersion](../../docs/includes/ssnoversion-md.md)]  
  - [Red Hat Enterprise Linux](sql-server-linux-setup-red-hat.md)  
  - [SUSE Linux Enterprise Server](sql-server-linux-setup-suse-linux-enterprise-server.md)  
  - [Ubuntu](sql-server-linux-setup-ubuntu.md)  

## Step 1: Join [!INCLUDE[ssNoVersion](../../docs/includes/ssnoversion-md.md)] host to AD domain
Numerous tools exist to help you join the [!INCLUDE[ssNoVersion](../../docs/includes/ssnoversion-md.md)] host machine to your AD domain. This walkthrough uses [realmd](https://www.freedesktop.org/software/realmd/docs/guide-active-directory-join.html), a popular open source package. If you haven't already, install both the **realmd** and Kerberos client packages on the [!INCLUDE[ssNoVersion](../../docs/includes/ssnoversion-md.md)] host machine using your Linux distribution's package manager:  
```bash  
# RHEL
sudo yum install realmd
sudo yum install krb5-workstation

# SUSE
sudo zypper install realmd
sudo zypper install krb5-client

# Ubuntu
sudo apt-get install realmd
sudo apt-get install krb5-user
```  

If the Kerberos client package installation prompts you for a realm name, enter your domain name in uppercase.  
Run the following command to verify that the [!INCLUDE[ssNoVersion](../../docs/includes/ssnoversion-md.md)] host machine is configured to use the AD domain controller for DNS. For the rest of this walkthrough, you should replace "contoso.com" and "CONTOSO.COM" with your own domain.  
```bash  
sudo realm discover contoso.com -v
```  
If no domains are found, you need to configure your machine to use your AD domain controller as a DNS nameserver. Make sure that your `/etc/resolv.conf` file contains a line like the following:  
```Code  
nameserver **<your AD domain controller IP address>**
```  
Next, join the domain. You'll need to authenticate using an AD account that has sufficient privileges in AD to join a new machine to the domain:  
```bash  									
sudo realm join contoso.com -U 'user@CONTOSO.COM' -v
<snip>
 * Successfully enrolled machine in realm
```    
Specifically, this command creates a new computer account in AD, create the `/etc/krb5.keytab` host keytab file, and configure the domain in `/etc/sssd/sssd.conf`. If you see an error, "Necessary packages are not installed," then you should install those packages using your Linux distro's package manager before running the realm join command again.  
Verify that you can now gather information about a user from the domain, and that you can acquire a Kerberos ticket as that user:  
```bash  
id user@contoso.com
uid=1348601103(user@contoso.com) gid=1348600513(domain group@contoso.com) groups=1348600513(domain group@contoso.com)

kinit user@CONTOSO.COM
Password for user@CONTOSO.COM:

klist
Ticket cache: FILE:/tmp/krb5cc_1000
Default principal: user@CONTOSO.COM
<snip>
```   
For more information, see the Red Hat documentation for [Discovering and Joining Identity Domains](https://access.redhat.com/documentation/Red_Hat_Enterprise_Linux/7/html/Windows_Integration_Guide/realmd-domain.html). 

## Step 2: Create AD user for [!INCLUDE[ssNoVersion](../../docs/includes/ssnoversion-md.md)] and set SPN  
On your domain controller, run the [New-ADUser](https://technet.microsoft.com/library/ee617253.aspx) PowerShell command to create a new AD user with a password that never expires. You will be prompted to enter a new password for the account:  
```PowerShell  	
New-ADUser mssql -AccountPassword (Read-Host -AsSecureString "Enter Password") -PasswordNeverExpires $true -Enabled $true
```   
Now set the ServicePrincipalName (SPN) for this account using the `setspn.exe` tool. The SPN must be formatted exactly as specified in the following example: You can find the fully qualified domain name of the [!INCLUDE[ssNoVersion](../../docs/includes/ssnoversion-md.md)] host machine by running hostname `--fqdn` on the [!INCLUDE[ssNoVersion](../../docs/includes/ssnoversion-md.md)] host, and the TCP port should be 1433 unless you have configured [!INCLUDE[ssNoVersion](../../docs/includes/ssnoversion-md.md)] to use a different port number.  
```PowerShell   
setspn -A MSSQLSvc/**<fully qualified domain name of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] host machine>**:**<tcp port>** mssql
```   
For more information, see [Register a Service Principal Name for Kerberos Connections](/sql/database-engine/configure-windows/register-a-service-principal-name-for-kerberos-connections.md).  

## Step 3: Configure [!INCLUDE[ssNoVersion](../../docs/includes/ssnoversion-md.md)] service keytab  
On the [!INCLUDE[ssNoVersion](../../docs/includes/ssnoversion-md.md)] host machine, create a keytab file for the AD user you just created. The `addent` command prompts you for a password. Enter the same password you used to create the AD user for [!INCLUDE[ssNoVersion](../../docs/includes/ssnoversion-md.md)].   
```bash  
sudo ktutil

ktutil: addent -password -p MSSQLSvc/**<fully qualified domain name of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] host machine>**:**<tcp port>**@CONTOSO.COM -k 2 -e aes256-cts-hmac-sha1-96
ktutil: wkt /var/opt/mssql/secrets/mssql.keytab
quit
```  

>  [!NOTE]  
>  If your domain controller is using Windows Server 2008 R2 or older, you should use `rc4-hmac` instead of `aes256-cts-hmac-sha1-96` as the encryption algorithm in the preceding `addent` command. For more information on which encryption algorithms are supported by different versions of Windows and Active Directory, see Network security: [Configure encryption types allowed for Kerberos Win7 only](https://docs.microsoft.com/windows/device-security/security-policy-settings/network-security-configure-encryption-types-allowed-for-kerberos).  

Anyone with access to this `keytab` file can impersonate [!INCLUDE[ssNoVersion](../../docs/includes/ssnoversion-md.md)] on the domain, so make sure you restrict access to the file such that only the `mssql` account has read access:  
```bash  
sudo chown mssql:mssql /var/opt/mssql/secrets/mssql.keytab
sudo chmod 600 /var/opt/mssql/secrets/mssql.keytab
```  
Next, configure [!INCLUDE[ssNoVersion](../../docs/includes/ssnoversion-md.md)] to use this `keytab` file for Kerberos authentication:  
```bash  
sudo /opt/mssql/bin/mssql-conf set auth.keytab /var/opt/mssql/secrets/mssql.keytab
sudo systemctl restart mssql-server
```  

## Step 4: Create AD-based logins in Transact-SQL  
Connect to [!INCLUDE[ssNoVersion](../../docs/includes/ssnoversion-md.md)] and create a new, AD-based login:  
```Transact-SQL  
CREATE LOGIN [user@contoso.com] FROM EXTERNAL PROVIDER;
```   

>  [!NOTE]  
>  Starting with [!INCLUDE[sssqlv14-md](../../docs/includes/sssqlv14-md.md)], the userPrincipalName (UPN) `login_name@DomainName` is the preferred format for creating AD-based logins. However, the pre-Windows 2000 user logon name format is also supported for compatibility: `CREATE LOGIN [CONTOSO\user] FROM WINDOWS;`  
Verify that the login is now listed in the [sys.server_principals](/sql/relational-databases/system-catalog-views/sys-server-principals-transact-sql.mc) system catalog view:  
```Transact-SQL  
SELECT name FROM sys.server_principals;
```  

## Step 5: Connect to [!INCLUDE[ssNoVersion](../../docs/includes/ssnoversion-md.md)] using AD Authentication  
Log in to a client machine using your domain credentials. Now you can connect to [!INCLUDE[ssNoVersion](../../docs/includes/ssnoversion-md.md)] without reentering your password, by using AD Authentication. If you create a login for an AD group, any AD user who is a member of that group can connect in the same way.  
The specific connection string parameter for clients to use AD Authentication depends on which driver you are using. A few examples are below.  

## Examples  
### Example 1: `sqlcmd` on a domain-joined Linux client  
Log in to a domain-joined Linux client using `ssh` and your domain credentials:  
```bash  
ssh -l user@contoso.com client.contoso.com
```  

Make sure you've installed the [mssql-tools](sql-server-linux-setup-tools.md) package, then connect using `sqlcmd` without specifying any credentials:  
```bash  
sqlcmd -S mssql.contoso.com
```  

### Example 2: SSMS on a domain-joined Windows client  
Log in to a domain-joined Windows client using your domain credentials. Make sure [!INCLUDE[ssmanstudiofull-md](../../docs/includes/ssmanstudiofull-md.md)] is installed, then connect to your [!INCLUDE[ssNoVersion](../../docs/includes/ssnoversion-md.md)] instance by specifying **Windows Authentication** in the **Connect to Server** dialog.  

### AD Authentication using other client drivers  
•	JDBC: [Using Kerberos Integrated Authentication to Connect SQL Server](https://docs.microsoft.com/sql/connect/jdbc/using-kerberos-integrated-authentication-to-connect-to-sql-server]()  
•	ODBC: [Using Integrated Authentication](https://docs.microsoft.com/sql/connect/odbc/linux/using-integrated-authentication)  
•	ADO.NET: [Connection String Syntax](https://msdn.microsoft.com/library/ms254500.aspx)   

