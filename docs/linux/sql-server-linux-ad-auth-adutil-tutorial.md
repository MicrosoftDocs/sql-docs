---
title: Configure Active Directory authentication with SQL Server on Linux using adutil
description: Step by step on how to configure Active Directory authentication with SQL Server on Linux using adutil
author: amitkh-msft
ms.author: amitkh
ms.reviewer: vanto, randolphwest
ms.date: 07/15/2024
ms.service: sql
ms.subservice: linux
ms.topic: tutorial
ms.custom:
  - linux-related-content
monikerRange: ">=sql-server-linux-2017 || >=sql-server-2017 || =sqlallproducts-allversions"
---

# Tutorial: Use adutil to configure Active Directory authentication with SQL Server on Linux

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

This tutorial explains how to configure Windows Active Directory authentication for SQL Server on Linux using [**adutil**](sql-server-linux-ad-auth-adutil-introduction.md). For another method of configuring Active Directory authentication using **ktpass**, see [Tutorial: Use Active Directory authentication with SQL Server on Linux](sql-server-linux-active-directory-authentication.md).

This tutorial consists of the following tasks:

> [!div class="checklist"]
> - Install **adutil**
> - Join Linux machine to your Active Directory domain
> - Create an Active Directory user for SQL Server and set the Service Principal Name (SPN) using **adutil**
> - Create the SQL Server service keytab (key table) file
> - Configure SQL Server to use the keytab file
> - Create Active Directory-based SQL Server logins using Transact-SQL
> - Connect to SQL Server using Active Directory authentication

## Prerequisites

Before configuring Active Directory authentication, you need:

- A Windows Domain Controller running Active Directory Domain Services in your network.
- The **adutil** tool installed on a domain-joined host machine.

## Domain machine preparation

Make sure there's a forwarding host (A) entry added in Active Directory for the Linux host IP address. In this tutorial, the IP address of `sql1` host machine is `10.0.0.10`. We add the forwarding host entry in Active Directory in the following example. The entry ensures that when users connect to `sql1.contoso.com`, it reaches the right host.

:::image type="content" source="media/sql-server-linux-ad-auth-adutil-tutorial/host-a-record.png" alt-text="Screenshot of add host record.":::

For this tutorial, we're using an environment in Azure with three virtual machines (VMs). One VM is a Windows Server computer named `adVM.contoso.com`, running as a Domain Controller (DC) with the domain name `contoso.com`. The second VM is a client machine running Windows 10 named `winbox`, which has SQL Server Management Studio (SSMS) installed. The third machine is an Ubuntu 18.04 LTS machine named `sql1`, which hosts SQL Server.

## Join the Linux host machine to your Active Directory domain

To join `sql1` to the Active Directory domain, see [Join SQL Server on a Linux host to an Active Directory domain](sql-server-linux-active-directory-join-domain.md).

## Install adutil

To install **adutil**, follow the steps explained in the article [Introduction to adutil - Active Directory utility](sql-server-linux-ad-auth-adutil-introduction.md) on the host machine that you added to the domain in the previous step.

## <a id="adutil-spn"></a> Use adutil to create an Active Directory user for SQL Server and set the Service Principal Name (SPN)

1. Obtain or renew the Kerberos TGT (ticket-granting ticket) using the `kinit` command. You must use a privileged account for the `kinit` command, and the host machine should already be part of the domain. The account needs permission to connect to the domain, and create accounts and SPNs in the domain.

   In this example script, a privileged user called `privilegeduser@CONTOSO.COM` is already created on the domain controller.

   ```bash
   kinit privilegeduser@CONTOSO.COM
   ```

1. Using **adutil**, create the new user that will be the privileged Active Directory account by SQL Server.

   Passwords can be specified in three different ways. If you use more than one of these methods, they take precedence in the following order:

   - Using the password flag: `--password <password>`
   - In an environment variable: `ADUTIL_ACCOUNT_PWD`
   - Interactive input at a command line prompt

   The environment variable or interactive input methods are more secure than the password flag.

   ```bash
   adutil user create --name sqluser --distname CN=sqluser,CN=Users,DC=CONTOSO,DC=COM --password 'P@ssw0rd'
   ```

   You can specify the name of the account using the distinguished name (`--distname`) as shown previously, or you can use the Organizational Unit (OU) name. The OU name (`--ou`) takes precedence over distinguished name in case you specify both. You can run the following command for more details:

   ```bash
   adutil user create --help
   ```

1. Register SPNs to the principal created previously. You must use the machine's fully qualified domain name (FQDN). In this tutorial, we're using SQL Server's default port, 1433. Your port number could be different.

   ```bash
   adutil spn addauto -n sqluser -s MSSQLSvc -H sql1.contoso.com -p 1433
   ```

   - `addauto` creates the SPNs automatically, as long as there are sufficient privileges for the `kinit` account.
   - `-n`: The name of the account to assign the SPNs.
   - `-s`: The service name to use for generating SPNs. In this case it's for the SQL Server service, which is why the service name is `MSSQLSvc`.
   - `-H`: The hostname to use for generating SPNs. If not specified, the local host's FQDN is used. In this case, the host name is `sql1` and the FQDN is `sql1.contoso.com`.
   - `-p`: The port to use for generating SPNs. If not specified, SPNs are generated without a port. SQL connections only work in this case when the SQL Server is listening to the default port, 1433.

## Create the SQL Server service keytab file using mssql-conf

You can install **adutil** and integrate it with **mssql-conf**, to create and configure the keytab using **mssql-conf** directly. This method is preferred for creating a [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] service keytab file. Otherwise, you can [create the SQL Server service keytab file manually](#create-the-sql-server-service-keytab-file-manually).

### Prerequisites

1. Make sure that the `/var/opt/mssql/mssql.conf` file is owned by `mssql` and not `root`. If this isn't the case, you must run the **mssql-conf** commands using `sudo`.

1. On a domain controller, in the Active Directory settings for the `network.privilegedadaccount` account (in these examples, `sqluser@CONTOSO.COM`), enable the following options under the **Account** tab, in the **Account options** section:

   - **This account supports Kerberos AES 128 bit encryption**
   - **This account supports Kerberos AES 256 bit encryption**

### Create the keytab file

Once you [create the user and SPNs](#adutil-spn), you can create the keytab using the following steps.

1. Switch to the `mssql` user:

   ```bash
   su mssql
   ```

1. Sign in as the Active Directory user using the `kinit` command:

   ```bash
   kinit privilegeduser@CONTOSO.COM
   ```

1. Create the keytab file:

   ```bash
   /opt/mssql/bin/mssql-conf setup-ad-keytab /var/opt/mssql/secrets/mssql.keytab sqluser
   ```

   When prompted to restart the SQL Server service to adopt the new Active Directory configuration, you can do in the next section.

1. Confirm the keytab is created with the right entries:

   ```bash
   klist -kte /var/opt/mssql/secrets/mssql.keytab
   ```

   You should see output similar to this example:

   ```output
   keytab name: FILE:/var/opt/mssql/secrets/mssql.keytab
   KVNO Timestamp           Principal
   ---- ------------------- ------------------------------------------------------
      4 12/30/2021 14:02:08 sqluser@CONTOSO.COM (aes256-cts-hmac-sha1-96)
      4 12/30/2021 14:02:08 MSSQLSvc/sql1.contoso.com:1433@CONTOSO.COM (aes256-cts-hmac-sha1-96)
      4 12/30/2021 14:02:08 MSSQLSvc/sql1.contoso.com@CONTOSO.COM (aes256-cts-hmac-sha1-96)
      4 12/30/2021 14:02:08 MSSQLSvc/sql1:1433@CONTOSO.COM (aes256-cts-hmac-sha1-96)
      4 12/30/2021 14:02:08 MSSQLSvc/sql1@CONTOSO.COM (aes256-cts-hmac-sha1-96)
   ```

   If the `/var/opt/mssql/mssql.conf` file isn't owned by `mssql`, you must configure **mssql-conf** to set the **network.kerberoskeytabfile** and **network.privilegedadaccount** values according to the previous steps. Type the password when prompted.

   ```bash
   /opt/mssql/bin/mssql-conf set network.kerberoskeytabfile /var/opt/mssql/secrets/mssql.keytab
   /opt/mssql/bin/mssql-conf set network.privilegedadaccount sqluser
   ```

1. Validate your configuration to ensure that Active Directory authentication works without any issues.

   ```bash
   /opt/mssql/bin/mssql-conf validate-ad-config /var/opt/mssql/secrets/mssql.keytab
   ```

   You should see output similar to the following example:

   ```output
   Detected Configuration:
   Default Realm: CONTOSO.COM
   Keytab: /var/opt/mssql/secrets/mssql.keytab
   Reverse DNS Result: sql1.contoso.com
   SQL Server Port: 1433
   Detected SPNs (SPN, KVNO):
   (MSSQLSvc/sql1.CONTOSO.COM:1433, 4)
   (MSSQLSvc/sql1.CONTOSO.COM, 4)
   (MSSQLSvc/sql1:1433, 4)
   (MSSQLSvc/sql1, 4)
   Privileged Account (Name, KVNO):
   (sqluser, 4)
   ```

## Create the SQL Server service keytab file manually

If you installed **adutil** and integrated it with **mssql-conf**, you can skip ahead to [Create the SQL Server service keytab file using mssql-conf](#create-the-sql-server-service-keytab-file-using-mssql-conf).

1. Create the keytab file that contains entries for each of the four SPNs created previously, and one for the user.

   ```bash
   adutil keytab createauto -k /var/opt/mssql/secrets/mssql.keytab -p 1433 -H sql1.contoso.com --password 'P@ssw0rd' -s MSSQLSvc
   ```

   The possible command line options are:

   - `-k`: The path where the `mssql.keytab` file is created. In the previous example, the directory `/var/opt/mssql/secrets/` should already exist on the host.
   - `-p`: The port to use for generating SPNs. If not specified, SPNs are generated without a port.
   - `-H`: The hostname to use for generating SPNs. If not specified, the local host's FQDN is used. In this case, the host name is `sql1` and the FQDN is `sql1.contoso.com`.
   - `-s`: The service name to use for generating SPNs. For this example, the SQL Server service name is `MSSQLSvc`.
   - `--password`: The password of the privileged Active Directory user account that was created earlier.
   - `-e` or `--enctype`: Encryption types for the keytab entry. Use a comma-separated list of values. If not specified, an interactive prompt is presented.

   You can choose more than one encryption type, as long as your host and domain support the encryption type. In this example, you might choose `aes256-cts-hmac-sha1-96` and `aes128-cts-hmac-sha1-96`. However, you should avoid `arcfour-hmac` in a production environment because it has weak encryption.

   If you'd like to choose the encryption type without prompting, you can specify your choice of encryption type using the `-e` argument in the previous command. For more help on the `adutil keytab` options, run this command:

   ```bash
   adutil keytab createauto --help
   ```

1. Add an entry in the keytab for the principal name and password that SQL Server uses to connect to Active Directory:

   ```bash
   adutil keytab create -k /var/opt/mssql/secrets/mssql.keytab -p sqluser --password 'P@ssw0rd'
   ```

   - `-k`: Path where you would like to create the `mssql.keytab` file.
   - `-p`: Principal to add to the keytab.

   The `adutil keytab [ create | autocreate ]` doesn't overwrite the previous files; it just appends to the file if already present.

1. Make sure the created keytab is owned by the `mssql` user, and that only the `mssql` user has read/write access to the file. You can run the `chown` and `chmod` commands as follows:

   ```bash
   chown mssql /var/opt/mssql/secrets/mssql.keytab
   chmod 440 /var/opt/mssql/secrets/mssql.keytab
   ```

## Configure SQL Server to use the keytab

Run the below commands to configure SQL Server to use the keytab created in the previous step, and set the privileged Active Directory account as the user created previously. In our example, the user name is `sqluser`.

```bash
/opt/mssql/bin/mssql-conf set network.kerberoskeytabfile /var/opt/mssql/secrets/mssql.keytab
/opt/mssql/bin/mssql-conf set network.privilegedadaccount sqluser
```

## Restart SQL Server

Run the following command to restart the SQL Server service:

```bash
sudo systemctl restart mssql-server
```

## Create Active Directory-based SQL Server logins in Transact-SQL

Connect to the SQL Server and run the following commands to create the login, and confirm that it exists.

```sql
CREATE LOGIN [contoso\privilegeduser] FROM WINDOWS;
SELECT name FROM sys.server_principals;
```

## Connect to SQL Server using Active Directory authentication

To connect using [SSMS](../ssms/download-sql-server-management-studio-ssms.md) or [Azure Data Studio](/azure-data-studio/download-azure-data-studio), log into the SQL Server with your Windows credentials.

You can also use a tool like [**sqlcmd**](../tools/sqlcmd/sqlcmd-utility.md) to connect to the SQL Server using Windows Authentication.

```bash
sqlcmd -E -S 'sql1.contoso.com'
```

## Related content

- [Understand Active Directory authentication for SQL Server on Linux and containers](sql-server-linux-ad-auth-understanding.md)
- [Troubleshoot Active Directory authentication for SQL Server on Linux and containers](sql-server-linux-ad-auth-troubleshooting.md)
- [Active Directory authentication for SQL Server on Linux](sql-server-linux-active-directory-auth-overview.md)
- [Tutorial: Configure Active Directory authentication with SQL Server on Linux containers](sql-server-linux-containers-ad-auth-adutil-tutorial.md)
- [Rotate keytabs for SQL Server on Linux](sql-server-linux-ad-auth-rotate-keytabs.md)
