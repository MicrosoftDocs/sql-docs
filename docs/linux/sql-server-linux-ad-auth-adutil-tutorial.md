---
title: Configure Active Directory authentication with SQL Server on Linux using adutil
description: Step by step on how to configure Active Directory authentication with SQL Server on Linux using adutil
author: amitkh-msft
ms.author: amitkh
ms.reviewer: vanto, randolphwest
ms.date: 09/27/2022
ms.service: sql
ms.subservice: linux
ms.topic: tutorial
monikerRange: ">= sql-server-linux-2017 || >= sql-server-2017 || =sqlallproducts-allversions"
---

# Tutorial: Use adutil to configure Active Directory authentication with SQL Server on Linux

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

This tutorial explains how to configure Windows Active Directory authentication for SQL Server on Linux using [**adutil**](sql-server-linux-ad-auth-adutil-introduction.md). For another method of configuring Active Directory authentication using **ktpass**, see [Tutorial: Use Active Directory authentication with SQL Server on Linux](sql-server-linux-active-directory-authentication.md).

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

Before configuring Active Directory authentication, you'll need:

- A Windows Domain Controller running Active Directory Domain Services in your network.
- The **adutil** tool installed on a domain-joined host machine.

## Domain machine preparation

Make sure there's a forwarding host (A) entry added in Active Directory for the Linux host IP address. In this tutorial, the IP address of `myubuntu` host machine is `10.0.0.10`. We add the forwarding host entry in Active Directory as shown below. The entry ensures that when users connect to `myubuntu.contoso.com`, it reaches the right host.

:::image type="content" source="media/sql-server-linux-ad-auth-adutil-tutorial/host-a-record.png" alt-text="add host record":::

For this tutorial, we're using an environment in Azure with three virtual machines (VMs). One VM is a Windows Server computer named `adVM.contoso.com`, running as a Domain Controller (DC) with the domain name `contoso.com`. The second VM is a client machine running Windows 10 named `winbox`, which has SQL Server Management Studio (SSMS) installed. The third machine is an Ubuntu 18.04 LTS machine named `myubuntu`, which hosts SQL Server.

## Join the Linux host machine to your Active Directory domain

To join `myubuntu` to the Active Directory domain, see [Join SQL Server on a Linux host to an Active Directory domain](sql-server-linux-active-directory-join-domain.md).

## Install adutil

To install **adutil**, follow the steps explained in the article [Introduction to adutil - Active Directory utility](sql-server-linux-ad-auth-adutil-introduction.md) on the host machine that you added to the domain in the previous step.

## <a id="adutil-spn"></a> Use adutil to create an Active Directory user for SQL Server and set the Service Principal Name (SPN)

1. Obtain or renew the Kerberos TGT (ticket-granting ticket) using the `kinit` command. You must use a privileged account for the `kinit` command, and the host machine should already be part of the domain. The account needs permission to connect to the domain, and create accounts and SPNs in the domain.

    In this example script, a privileged user called `privilegeduser@CONTOSO.COM` has already been created on the domain controller.

    ```bash
    kinit privilegeduser@CONTOSO.COM
    ```

1. Using **adutil**, create the new user that will be used as the privileged Active Directory account by SQL Server.

    Passwords can be specified in three different ways. If you use more than one of these methods, they take precedence in the following order:

    - Using the password flag: `--password <password>`
    - In an environment variable: `ADUTIL_ACCOUNT_PWD`
    - Interactive input at a command line prompt

    The environment variable or interactive input methods are more secure than the password flag.

   ```bash
   adutil user create --name sqluser --distname CN=sqluser,CN=Users,DC=CONTOSO,DC=COM --password 'P@ssw0rd'
   ```

    You can specify the name of the account using the distinguished name (`--distname`) as shown above, or you can use the Organizational Unit (OU) name. The OU name (`--ou`) takes precedence over distinguished name in case you specify both. You can run the below command for more details:

    ```bash
    adutil user create --help
    ```

1. Register SPNs to the principal created above. You must use the machine's fully qualified domain name (FQDN). In this tutorial, we're using SQL Server's default port, 1433. Your port number could be different.

    ```bash
    adutil spn addauto -n sqluser -s MSSQLSvc -H myubuntu.contoso.com -p 1433
    ```

    - `addauto` will create the SPNs automatically, as long as there are sufficient privileges for the `kinit` account.
    - `-n`: The name of the account the SPNs will be assigned to.
    - `-s`: The service name to use for generating SPNs. In this case it is for the SQL Server service, which is why the service name is `MSSQLSvc`.
    - `-H`: The hostname to use for generating SPNs. If not specified, the local host's FQDN will be used. In this case, the host name is `myubuntu` and the FQDN is `myubuntu.contoso.com`.
    - `-p`: The port to use for generating SPNs. If not specified, SPNs will be generated without a port. SQL connections will only work in this case when the SQL Server is listening to the default port, 1433.

## Create the SQL Server service keytab file

1. Create the keytab file that contains entries for each of the four SPNs created previously, and one for the user.

    ```bash
    adutil keytab createauto -k /var/opt/mssql/secrets/mssql.keytab -p 1433 -H myubuntu.contoso.com --password 'P@ssw0rd' -s MSSQLSvc
    ```

    The possible command line options are:

    - `-k`: The path where the `mssql.keytab` file will be created. In the above example, the directory `/var/opt/mssql/secrets/` should already exist on the host.
    - `-p`: The port to use for generating SPNs. If not specified, SPNs will be generated without a port.
    - `-H`: The hostname to use for generating SPNs. If not specified, the local host's FQDN will be used. In this case, the host name is `myubuntu` and the FQDN is `myubuntu.contoso.com`.
    - `-s`: The service name to use for generating SPNs. For this example, the SQL Server service name is `MSSQLSvc`.
    - `--password`: The password of the privileged Active Directory user account that was created earlier.
    - `-e` or `--enctype`: Encryption types for the keytab entry. Use a comma-separated list of values. If not specified, an interactive prompt will be presented.

    You can choose more than one encryption type, as long as your host and domain support the encryption type. In this example, you might choose `aes256-cts-hmac-sha1-96` and `aes128-cts-hmac-sha1-96`. However, you should avoid `arcfour-hmac` in a production environment because it has weak encryption.

    If you'd like to choose the encryption type without being prompted, you can specify your choice of encryption type using the `-e` argument in the above command. For more help on the `adutil keytab` options, run this command:

    ```bash
    adutil keytab createauto --help
    ```

1. Add an entry in the keytab for the principal name and its password that will be used by SQL Server to connect to Active Directory:

    ```bash
    adutil keytab create -k /var/opt/mssql/secrets/mssql.keytab -p sqluser --password 'P@ssw0rd!'
    ```

    - `-k`: Path where you would like the `mssql.keytab` file to be created.
    - `-p`: Principal to add to the keytab.

    The `adutil keytab [ create | autocreate ]` doesn't overwrite the previous files, it just appends to the file if already present.

1. Make sure the created keytab is owned by the `mssql` user, and that only the `mssql` user has read/write access to the file. You can run the `chown` and `chmod` commands as shown below:

    ```bash
    chown mssql /var/opt/mssql/secrets/mssql.keytab
    chmod 440 /var/opt/mssql/secrets/mssql.keytab
    ```

## Configure SQL Server to use the keytab

Run the below commands to configure SQL Server to use the keytab created in the previous step, and set the privileged Active Directory account as the user created above. In our example, the user name is `sqluser`.

```bash
/opt/mssql/bin/mssql-conf set network.kerberoskeytabfile /var/opt/mssql/secrets/mssql.keytab
/opt/mssql/bin/mssql-conf set network.privilegedadaccount sqluser
```

## Restart SQL Server

Run the below command to restart the SQL Server service:

```bash
sudo systemctl restart mssql-server
```

## Create Active Directory-based SQL Server logins in Transact-SQL

Connect to the SQL Server and run the following commands to create the login, and confirm that it's listed.

```sql
CREATE LOGIN [contoso\privilegeduser] FROM WINDOWS;
SELECT name FROM sys.server_principals;
```

## Connect to SQL Server using Active Directory authentication

To connect using [SSMS](../ssms/download-sql-server-management-studio-ssms.md) or [Azure Data Studio](../azure-data-studio/download-azure-data-studio.md), log into the SQL Server with your Windows credentials.

You can also use a tool like [**sqlcmd**](../tools/sqlcmd-utility.md) to connect to the SQL Server using Windows Authentication.

```bash
sqlcmd -E -S 'myubuntu.contoso.com'
```

## See also

- [Understanding Active Directory authentication for SQL Server on Linux and containers](sql-server-linux-ad-auth-understanding.md)
- [Troubleshooting Active Directory authentication for SQL Server on Linux and containers](sql-server-linux-ad-auth-troubleshooting.md)

## Next steps

- [Join SQL Server on a Linux host to an Active Directory domain](sql-server-linux-active-directory-auth-overview.md)
- [Configure Active Directory authentication with SQL Server on Linux containers](sql-server-linux-containers-ad-auth-adutil-tutorial.md)
- [Rotate SQL Server on Linux keytabs](sql-server-linux-ad-auth-rotate-keytabs.md)
