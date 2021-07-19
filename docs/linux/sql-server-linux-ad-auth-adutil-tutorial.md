---
title: Configure Active Directory authentication with SQL Server on Linux using adutil
description: Step by step on how to configure Active Directory authentication with SQL Server on Linux using adutil
author: amvin87
ms.author: amitkh
ms.reviewer: vanto
ms.date: 12/10/2020
ms.topic: tutorial
ms.prod: sql
ms.technology: linux
moniker: ">= sql-server-linux-2017 || >= sql-server-2017 || =sqlallproducts-allversions"
---

# Tutorial: Configure Active Directory authentication with SQL Server on Linux using adutil

> [!NOTE]
> **adutil** is currently in **public preview**

This tutorial explains how to configure Active Directory (AD) authentication for SQL Server on Linux using adutil. For another method of configuring AD authentication using ktpass, see [Tutorial: Use Active Directory authentication with SQL Server on Linux](sql-server-linux-active-directory-authentication.md).

This tutorial consists of the following tasks:

> [!div class="checklist"]
> - Install adutil-preview
> - Join Linux machine to your AD domain
> - Create an AD user for SQL Server and set the ServicePrincipalName (SPN) using the adutil tool
> - Create the SQL Server service keytab file
> - Configure SQL Server to use the keytab file
> - Create AD-based SQL Server logins using Transact-SQL
> - Connect to SQL Server using AD authentication

## Prerequisites

The following are required before configuring AD authentication:

- Have an AD Domain Controller (Windows) in your network.
- Install the adutil-preview tool on a Linux host machine. Follow the section below based on the Linux distribution that you're running to install adutil-preview.

## Install adutil-preview

On the Linux host machine, use the following commands to install adutil-preview.

> [!NOTE]
> For this preview version, we are aware that on certain Linux distributions, if the adutil installation is attempted without the `ACCEPT_EULA` parameter, the installation experience is hindered. Our recommendation below is to install the adutil-preview tool with `ACCEPT_EULA=Y` set. You can read the preview [EULA](https://go.microsoft.com/fwlink/?linkid=2151376) ahead of the installation. We are actively working on this and this should be fixed for the GA release.

### RHEL

1. Download the Microsoft Red Hat repository configuration file.

    ```bash
    sudo curl -o /etc/yum.repos.d/msprod.repo https://packages.microsoft.com/config/rhel/8/prod.repo
    ```

1. If you had a previous version of adutil installed, remove any older adutil packages.

    ```bash
    sudo yum remove adutil
    ```

1. Run the following commands to install adutil-preview. `ACCEPT_EULA=Y` accepts the preview EULA for adutil. The EULA is placed at the path '/usr/share/adutil/'.

    ```bash
    sudo ACCEPT_EULA=Y yum install -y adutil-preview
    ```

### Ubuntu

1. Import the public repository GPG keys and then register the Microsoft Ubuntu repository.

    ```bash
    curl https://packages.microsoft.com/keys/microsoft.asc | sudo apt-key add -
    sudo curl https://packages.microsoft.com/config/ubuntu/18.04/prod.list | sudo tee /etc/apt/sources.list.d/msprod.list
    ```

1. If you had a previous version of adutil installed, remove any older adutil packages using the below commands

    ```bash
    sudo apt-get remove adutil
    ```

1. Run the following command to install adutil-preview. `ACCEPT_EULA=Y` accepts the preview EULA for adutil. The EULA is placed at the path '/usr/share/adutil/'.

    ```bash
    sudo apt-get update
    sudo ACCEPT_EULA=Y apt-get install -y adutil-preview
    ```

### SLES

1. Add the Microsoft SQL Server repository to Zypper.

    ```bash
    sudo zypper addrepo -fc https://packages.microsoft.com/config/sles/12/prod.repo 
    ```

1. If you had a previous version of adutil installed, remove any older adutil packages.

    ```bash
    sudo zypper remove adutil
    ```

1. Run the following command to install adutil-preview. `ACCEPT_EULA=Y` accepts the preview EULA for adutil. The EULA is placed at the path '/usr/share/adutil/'.

    ```bash
    sudo ACCEPT_EULA=Y zypper install -y adutil-preview
    ```

## Domain machine preparation

Make sure there is forwarding host (A) entry added in Active Directory for the Linux host IP address. In this tutorial, the IP address of `myubuntu` host machine is `10.0.0.10`. We add the forwarding host entry in Active Directory as shown below. The entry ensures that when users connect to myubuntu.contoso.com, it reaches the right host.

:::image type="content" source="media/sql-server-linux-ad-auth-adutil-tutorial/host-a-record.png" alt-text="add host record":::

For this tutorial, we're using an environment in Azure with three VMs. One VM acting as the windows domain controller (DC), with the domain name `contoso.com`. The Domain Controller is named `adVM.contoso.com`. The second machine is a Windows machine called `winbox`, running Windows 10 desktop, which is used as a client box and has SQL Server Management Studio (SSMS) installed. The third machine is an Ubuntu 18.04 LTS machine named `myubuntu`, which hosts SQL Server.

## Join the Linux host machine to your AD domain

Join your SQL Server Linux host with an Active Directory domain controller. For information on how to join an active directory domain, see [Join SQL Server on a Linux host to an Active Directory domain](sql-server-linux-active-directory-join-domain.md).

## Create an AD user for SQL Server and set the ServicePrincipalName (SPN) using the adutil tool

1. Obtain or renew the Kerberos TGT (ticket-granting ticket) using the `kinit` command. Use a privileged account for the `kinit` command. The account needs to have permission to connect to the domain, and also should be able to create accounts and SPNs in the domain.

    > [!IMPORTANT]
    > Before you run this command, the host machine should already be part of the domain as shown in the previous step.

    ```bash
    kinit privilegeduser@DOMAIN.COM
    ```

    Example: For the environment described above, my privileged account is `amvin@CONTOSO.COM`

    ```bash
    kinit amvin@CONTOSO.COM
    ```

2. Using the adutil tool, create the new user that will be used as the privileged AD Account by SQL Server.

   ```bash
   adutil user create --name sqluser --distname CN=sqluser,CN=Users,DC=CONTOSO,DC=COM --password 'P@ssw0rd'
   ```

    > [!NOTE]
    > Passwords may be specified in any of the three ways:
    >
    > - Password flag: --password \<password\>
    > - Environment variables - `ADUTIL_ACCOUNT_PWD`
    > - Interactive input
    >
    > The precedence of password entry methods follows the order of options listed above. The recommended options are to provide the password using Environment variables or interactive input, as they more secure compared to the password flag.

    You can specify the name of the account using the distinguished name (`-distname`) as shown above, or you can also use the Organizational Unit (OU) name as well. The OU name (`--ou`) takes precedence over distinguished name in case you specify both. You can run the below command for more details:

    ```bash
    adutil user create --help
    ```

3. Register SPNs to the principal created above. Use the machine FQDN. In this tutorial, we're using SQL Server's default port, 1433. Your port number could be different.

    ```bash
    adutil spn addauto -n sqluser -s MSSQLSvc -H myubuntu.contoso.com -p 1433
    ```

    > [!NOTE]
    >
    > - `addauto` will create the SPNs automatically, provided sufficient privileges are present for the kinit account.
    > - `-n`: Name of the account the SPNs will be assigned to.
    > - `-s`: The service name to use for generating SPNs. In this case, it is for SQL Server service, and hence the service name is `MSSQLSvc`.
    > - `-H`: The hostname to use for generating SPNs. If not specified, the local host's FQDN will be used. In this case, the host name is `myubuntu` and the FQDN is `myubuntu.contoso.com`.
    > - `-p`: The port to use for generating SPNs. If not specified, SPNs will be generated without a port. SQL connections will only work in this case when the SQL Server is listening to the default port, 1433.

## Create the SQL Server service keytab file

Create the keytab file that contains entries for each of the 4 SPNs created previously, and one for the user.

```bash
adutil keytab createauto -k /var/opt/mssql/secrets/mssql.keytab -p 1433 -H myubuntu.contoso.com --password 'P@ssw0rd' -s MSSQLSvc 
```

> [!NOTE]
>
> - `-k`: Path where you would like the `mssql.keytab` file to be created. In the above example the directory `/var/opt/mssql/secrets/` should already exist on the host.
> - `-p`: The port to use for generating SPNs. If not specified, SPNs will be generated without a port.
> - `-H`: The hostname to use for generating SPNs. If not specified, the local host's FQDN will be used. In this case, the host name is `myubuntu` and the FQDN is `myubuntu.contoso.com`.
> - `-s`: The service name to use for generating SPNs. In this case, it is for SQL Server service, and hence the service name is `MSSQLSvc`.
> - `--password`: This is the password of the privileged AD user account that was created earlier.
> - `-e` or `--enctype`: Encryption types for the keytab entry. Use a comma-separated list of values. If not specified, an interactive prompt will be presented.

When given a choice to choose the encryption types, you can choose more than one. For this example, we chose `aes256-cts-hmac-sha1-96` and `arcfour-hmac`. Ensure you choose the encryption type that is supported by the host and domain.

If you’d like to non-interactively choose the encryption type, you can specify your choice of encryption type with the -e argument in the above command. For additional help on the adutil commands, run the command below.

```bash
adutil keytab createauto --help
```

> [!NOTE]
> `arcfour-hmac` is a weak encryption and not a recommended encryption type to be used in production environment.

Add an entry in the keytab for the principal name and its password that will be used by SQL Server to connect to AD:

```bash
adutil keytab create -k /var/opt/mssql/secrets/mssql.keytab -p sqluser --password 'P@ssw0rd!'
```

> [!NOTE]
>
> - `-k`: Path where you would like the `mssql.keytab` file to be created.
> - `-p`: Principal to add to the keytab.

The adutil keytab create/autocreate doesn't overwrite the previous files, it just appends to the file if already present.

Ensure the keytab created is owned by the `mssql` user and only the `mssql` user has read/write access to the file. You can run the `chown` and `chmod` commands as shown below:

```bash
chown mssql. /var/opt/mssql/secrets/mssql.keytab
chmod 440 /var/opt/mssql/secrets/mssql.keytab
```

## Configure SQL Server to use the keytab

Run the below commands to configure SQL Server to use the keytab created in the previous step, and set the privileged AD account as the user created above. In our example, the user name is `sqluser`.

```bash
/opt/mssql/bin/mssql-conf set network.kerberoskeytabfile /var/opt/mssql/secrets/mssql.keytab
/opt/mssql/bin/mssql-conf set network.privilegedadaccount sqluser
```

## Restart SQL Server

Run the below command to restart the SQL Server service:

```bash
sudo systemctl restart mssql-server
```

## Create AD-based SQL Server logins in Transact-SQL

Connect to the SQL Server and run the following commands to create the login, and confirm that it's listed.

```sql
create login [contoso\amvin] From Windows
SELECT name FROM sys.server_principals;
```

## Connect to SQL Server using AD authentication.

To connect using [SSMS](../ssms/download-sql-server-management-studio-ssms.md) or [Azure Data Studio](../azure-data-studio/download-azure-data-studio.md), log into the SQL Server with your Windows credentials.

You can also use a tool like [sqlcmd](../tools/sqlcmd-utility.md) to connect to the SQL Server using Windows Authentication.

```bash
sqlcmd -E -S 'myubuntu.contoso.com'
```

## Next Steps

- [Join SQL Server on a Linux host to an Active Directory domain](sql-server-linux-active-directory-auth-overview.md)
- If you're interested on how to configure AD authentication with SQL Server on Linux containers, see [Configure Active Directory authentication with SQL Server on Linux  containers](sql-server-linux-containers-ad-auth-adutil-tutorial.md)
