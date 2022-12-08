---
title: Configure Active Directory authentication with SQL Server on Linux-based containers using adutil
description: Step by step on how to configure Active Directory authentication with SQL Server on Linux containers using adutil
author: amitkh-msft
ms.author: amitkh
ms.reviewer: randolphwest
ms.date: 03/07/2022
ms.topic: tutorial
ms.service: sql
ms.subservice: linux
moniker: ">= sql-server-linux-2017 || >= sql-server-2017 || =sqlallproducts-allversions"
---

# Tutorial: Configure Active Directory authentication with SQL Server on Linux containers

This tutorial explains how to configure SQL Server on Linux containers to support Active Directory authentication, also known as integrated authentication. For an overview, see [Active Directory authentication for SQL Server on Linux](sql-server-linux-active-directory-auth-overview.md).

This tutorial consists of the following tasks:

> [!div class="checklist"]
> - Install adutil
> - Join Linux host to Active Directory domain
> - Create an Active Directory user for SQL Server and set the ServicePrincipalName (SPN) using the adutil tool
> - Create the SQL Server service keytab file
> - Create the mssql.conf and krb5.conf files to be used by the SQL Server container
> - Mount the config files and deploy the SQL Server container
> - Create Active Directory-based SQL Server logins using Transact-SQL
> - Connect to SQL Server using Active Directory authentication

## Prerequisites

The following are required before configuring Active Directory authentication:

- Have an Active Directory Domain Controller (Windows) in your network.
- Install the adutil tool on a Linux host machine, which is joined to a domain. Follow the [Install adutil](#install-adutil) section below for details.

## Container deployment and preparation

To set up your container, you'll need to know in advance the port that will be used by the container on the host. The default port, 1433, might be mapped differently on your container host. For this tutorial, port 5433 on the host will be mapped to port 1433 of the container. For more information, see our quickstart, [Run SQL Server container images with Docker](quickstart-install-connect-docker.md)

When registering Service Principal Names (SPN), you can use the hostname of the machine or the name of the container, but you should set it up according to what you'd like to see when you connect to the container externally.

Make sure there's a forwarding host (A) entry added in Active Directory for the Linux host IP address, mapping to the name of the SQL Server container. In this tutorial, the IP address of `myubuntu` host machine is `10.0.0.10`, and my SQL Server container name is `sql1`. We add the forwarding host entry in Active Directory as shown below. The entry ensures that when users connect to `sql1.contoso.com`, it reaches the right host.

:::image type="content" source="media/sql-server-linux-containers-ad-auth-adutil-tutorial/host-a-record.png" alt-text="add host record":::

For this tutorial, we're using an environment in Azure with three VMs. One VM acting as the Windows domain controller (DC), with the domain name `contoso.com`. The Domain Controller is named `adVM.contoso.com`. The second machine is a Windows machine called `winbox`, running Windows 10 desktop, which is used as a client box and has SQL Server Management Studio (SSMS) installed. The third machine is an Ubuntu 18.04 LTS machine named `myubuntu`, which hosts the SQL Server containers. All machines have been joined to the `contoso.com` domain. For more information, see [Join SQL Server on a Linux host to an Active Directory domain](sql-server-linux-active-directory-join-domain.md).

> [!NOTE]
> Joining the host container machine to the domain is not mandatory, as you can see later in this article.

## Install adutil

To install adutil tool, follow the steps explained in:[Introduction to adutil - Active Directory utility](sql-server-linux-ad-auth-adutil-introduction.md) utility on a host machine that is domain joined.

## Creating the Active Directory user, SPNs, and SQL Server service keytab

If you don't want the SQL Server on Linux container host to be part of the domain, and haven't followed the steps to join the machine to the domain, then on another Linux machine that is already part of the Active Directory domain, follow the below steps:

 1. Create an Active Directory user for SQL Server and set the SPN using the adutil tool.

 2. Create and configure the SQL Server service keytab file.

Copy the mssql.keytab file that was created to the host machine that will run the SQL Server container, and configure the container to use the copied mssql.keytab. Optionally, you can also join your Linux host that will run the SQL Server container to the Active Directory domain and follow the below steps on the same machine.

### Create an Active Directory user for SQL Server and set the ServicePrincipalName using the adutil tool

Enabling Active Directory authentication on SQL Server on Linux containers requires steps 1-3 mentioned below to be run on a Linux machine that is part of the Active Directory domain.

1. Obtain or renew the Kerberos TGT (ticket-granting ticket) using the `kinit` command. Use a privileged account for the `kinit` command. The account needs to have permission to connect to the domain, and also should be able to create accounts and SPNs in the domain.

    In this example script, a privileged user called `privilegeduser@CONTOSO.COM` has already been created on the domain controller.

    ```bash
    kinit privilegeduser@CONTOSO.COM
    ```

2. Using the adutil tool, create the new user that will be used as the privileged Active Directory account by SQL Server.

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

3. Register SPNs to the user created above. You can use the host machine name instead of the container name if desired, depending on how you'd like the connection to look externally. In this tutorial, port 5433 is used instead of 1433. This is the port mapping for the container. Your port number could be different.

    ```bash
    adutil spn addauto -n sqluser -s MSSQLSvc -H sql1.contoso.com -p 5433
    ```

    > [!NOTE]
    >
    > - `addauto` will create the SPNs automatically, provided sufficient privileges are present for the **kinit** account.
    > - `-n`: Name of the account the SPNs will be assigned to.
    > - `-s`: The service name to use for generating SPNs. In this case, it is for SQL Server service, and hence the service name is MSSQLSvc.
    > - `-H`: The hostname to use for generating SPNs. If not specified, the local host's FQDN will be used. Please provide the FQDN for the container name as well. In this case, the container name is `sql1` and the FQDN is `sql1.contoso.com`.
    > - `-p`: The port to use for generating SPNs. If not specified, SPNs will be generated without a port. SQL connections will only work in this case when the SQL Server is listening to the default port, 1433.

### Create the SQL Server service keytab file

Create the keytab file that contains entries for each of the 4 SPNs created previously, and one for the user. The keytab file will be mounted to the container, so it can be created at any location on the host. You can safely change this path, as long as the resulting keytab is mounted correctly when using docker/podman to deploy the container.

To create the keytab for all the SPNs, we can use the `createauto` option:

```bash
adutil keytab createauto -k /container/sql1/secrets/mssql.keytab -p 5433 -H sql1.contoso.com --password 'P@ssw0rd' -s MSSQLSvc
```

> [!NOTE]
>
> - `-k`: Path where you would like the `mssql.keytab` file to be created. In the above example the directory "/container/sql1/secrets" should already exist on the host.
> - `-p`: The port to use for generating SPNs. If not specified, SPNs will be generated without a port.
> - `-H`: The hostname to use for generating SPNs. If not specified, the local host's FQDN will be used. Please provide the FQDN for the container name as well. In this case, the container name is `sql1` and the FQDN is `sql1.contoso.com`.
> - `-s`: The service name to use for generating SPNs. In this case, it is for SQL Server service, and hence the service name is MSSQLSvc.
> - `--password`: This is the password of the privileged Active Directory user account that was created earlier.
> - `-e` or `--enctype`: Encryption types for the keytab entry. Use a comma-separated list of values. If not specified, an interactive prompt will be presented.

When given a choice to choose the encryption types, you can choose more than one. For this example, we chose `aes256-cts-hmac-sha1-96` and `arcfour-hmac`. Ensure you choose the encryption type that is supported by the host and domain.

If you'd like to non-interactively choose the encryption type, you can specify your choice of encryption type with the -e argument in the above command. For additional help on the adutil commands, run the command below.

```bash
adutil keytab createauto --help
```

> [!NOTE]
> `arcfour-hmac` is a weak encryption and not a recommended encryption type to be used in production environment.

To create the keytab for the user, the command is:

```bash
adutil keytab create -k /container/sql1/secrets/mssql.keytab -p sqluser --password 'P@ssw0rd'
```

> [!NOTE]
>
> - `-k`: Path where you would like the `mssql.keytab` file to be created. In the above example the directory "/container/sql1/secrets" should already exist on the host.
> - `-p`: Principal to add to the keytab.

The adutil keytab create/autocreate doesn't overwrite the previous files, it just appends to the file if already present.

Ensure the keytab created has the right permissions set when deploying the container.

```bash
chmod 440 /container/sql1/secrets/mssql.keytab
```

> [!NOTE]
> At this point, you can copy the mssql.keytab from the current Linux host to the Linux host where you would deploy the SQL Server container, and follow the rest of the steps on the Linux host which will run the SQL Server Container. If the above steps were performed on the same Linux host where the SQL Containers will be deployed, then follow the next steps as well on the same host.

## Create the config files to be used by the SQL Server container

1. Create an `mssql.conf` file with the settings for Active Directory. This file can be created anywhere on the host and needs to be mounted correctly during the docker run command. In this example, we placed this file `mssql.conf` under `/container/sql1`, which is our container directory. The content of the `mssql.conf` is as shown below:

    ```ini
    [network]
    privilegedadaccount = sqluser
    kerberoskeytabfile = /var/opt/mssql/secrets/mssql.keytab
    ```

    > [!NOTE]
    >
    > - `privilegedadaccount`: Privileged Active Directory user to use for Active Directory authentication.
    > - `kerberoskeytabfile`: The path in the container where the mssql.keytab file will be located.

1. Create a krb5.conf file. Here's a sample shown below. The casing matters on these files.

    ```ini
    [libdefaults]
    default_realm = CONTOSO.COM
    
    [realms]
    CONTOSO.COM = {
        kdc = adVM.contoso.com
        admin_server = adVM.contoso.com
        default_domain = CONTOSO.COM
    }
    
    [domain_realm]
    .contoso.com = CONTOSO.COM
    contoso.com = CONTOSO.COM
    ```

1. Copy all files, `mssql.conf`, `krb5.conf`, `mssql.keytab` to a location that will be mounted to the SQL Server container. In this example, these files are placed on the host at the following locations: `mssql.conf` and `krb5.conf` at `/container/sql1/`. `mssql.keytab` is placed at the location `/container/sql1/secrets/`.

1. Make sure there's enough permission on these folders for the user running the docker/podman command. When the container starts, the user needs access to the folder path created. In this example, we provided the below permissions given to the folder path:

    ```bash
    sudo chmod 755 /container/sql1/
    ```

## Mount the config files and deploy the SQL Server container

Run your SQL Server container, and mount the correct Active Directory configuration files that were previously created as shown below:

> [!IMPORTANT]  
> The `SA_PASSWORD` environment variable is deprecated. Please use `MSSQL_SA_PASSWORD` instead.

```bash
sudo docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=\<YourStrong@Passw0rd\>" \
-p 5433:1433 --name sql1 \
-v /container/sql1:/var/opt/mssql \
-v /container/sql1/krb5.conf:/etc/krb5.conf \
-d mcr.microsoft.com/mssql/server:2019-latest
```

> [!NOTE]
> When running container on LSM (Linux Security Module) like SELinux enabled hosts, you need to mount the volumes using the `Z` option, which tells docker to label the content with a private unshared label. For more information, see [configure the SE Linux label](https://docs.docker.com/storage/bind-mounts/#configure-the-selinux-label).

Our example would contain the following commands:

```bash
sudo docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=P@ssw0rd" -p 5433:1433 --name sql1 \
-v /container/sql1:/var/opt/mssql/ \
-v /container/sql1/krb5.conf:/etc/krb5.conf \
--dns-search contoso.com \
--dns 10.0.0.4 \
--add-host adVM.contoso.com:10.0.0.4 \
--add-host contoso.com:10.0.0.4 \
--add-host contoso:10.0.0.4 \
-d mcr.microsoft.com/mssql/server:2019-latest
```

> [!NOTE]
>
> - The files `mssql.conf` and `krb5.conf` are located at the host file path `/container/sql1`.
> - The `mssql.keytab` that was created is located on the host file path `/container/sql1/secrets`.
> - Because our host machine is on Azure, the Active Directory details in the same order needs to be appended to the docker run command. In our example, the domain controller `adVM` is in the domain `contoso.com`, with an IP address of `10.0.0.4`. The domain controller runs DNS and KDC.

## Create Active Directory-based SQL Server logins in Transact-SQL

Connect to SQL container and run the following commands to create the login, and confirm that it's listed. You can run this command from a client machine (Windows or Linux) running SSMS, Azure Data Studio (ADS), or any other command-line interface (CLI) tool.

```sql
create login [contoso\amvin] From Windows
SELECT name FROM sys.server_principals;
```

## Connect to SQL Server using Active Directory authentication

To connect using [SSMS](../ssms/download-sql-server-management-studio-ssms.md) or [ADS](../azure-data-studio/download-azure-data-studio.md), sign in to the SQL Server with Windows credentials using the SQL Server name and port number (name could be the container name or the host name). For our example, the server name would be `sql1.contoso.com, 5433`.

You can also use a tool like [sqlcmd](../tools/sqlcmd-utility.md) to connect to the SQL Server in your container.

```bash
sqlcmd -E -S 'sql1.contoso.com, 5433'
```

## Resources

- [Understanding Active Directory authentication for SQL Server on Linux and containers](sql-server-linux-ad-auth-understanding.md)
- [Troubleshooting Active Directory authentication for SQL Server on Linux and containers](sql-server-linux-ad-auth-troubleshooting.md)

## Next steps

- [Quickstart: Run SQL Server container images with Docker](quickstart-install-connect-docker.md)
- [Join SQL Server on a Linux host to an Active Directory domain](sql-server-linux-active-directory-auth-overview.md)
- [Rotate SQL Server on Linux keytabs](sql-server-linux-ad-auth-rotate-keytabs.md)
