---
title: Introduction to adutil - Active Directory Utility
description: Overview of adutil, a utility for configuring and managing Active Directory domains for SQL Server on Linux and containers
author: amitkh-msft
ms.author: amitkh
ms.reviewer: vanto, randolphwest
ms.date: 09/27/2022
ms.service: sql
ms.subservice: linux
ms.topic: conceptual
monikerRange: ">= sql-server-linux-2017 || >= sql-server-2017 || =sqlallproducts-allversions"
---

# Introduction to adutil - Active Directory utility

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

The **adutil** tool is a command-line interface (CLI) utility for configuring and managing Windows Active Directory domains for SQL Server on Linux and containers, without switching between Windows and Linux machines to manage Active Directory.

Support for **adutil** is limited for SQL Server use cases only.

You don't need to use **adutil** to enable Active Directory authentication for SQL Server on Linux or containers. You can also use utilities like **ktpass**, as explained in [Tutorial: Use Active Directory authentication with SQL Server on Linux](sql-server-linux-active-directory-authentication.md).

The **adutil** tool is designed as a series of commands and subcommands, with additional flags that you specify as further input. Each top level command represents a category of administrative functions. Within that category, each subcommand is an operation. This article will show you how you can download and get started with **adutil**.

## Configure adutil for LDAP over Secure Sockets Layer (SSL)

You should use Lightweight Directory Access Protocol over SSL (LDAPS) instead of Lightweight Directory Access Protocol (LDAP). If you want to learn more about LDAP, see [Lightweight Directory Access Protocol (LDAP)](sql-server-linux-ad-auth-understanding.md#ldap). For more information on how to configure LDAPS and how it differs from LDAP, see [Enabling LDAPS for Client Authentication](https://social.technet.microsoft.com/wiki/contents/articles/2980.ldap-over-ssl-ldaps-certificate.aspx#Enabling_LDAPS_for_Client_Authentication).

You can set the `useLdaps` option to `true` in the `adutil.json` configuration file, which is located at: `/var/opt/mssql/.adutil/adutil.json` when run under the `mssql` user. This JSON code sample shows how to configure the setting:

```json
{
    "useLdaps": "true"
}
```

By default, the `useLDAPS` setting is set to `false`. When configuring this setting and using **mssql-conf** to create the keytab (key table), make sure you run **mssql-conf** as the user `mssql`. You can do this by running the following command:

```bash
sudo su mssql
```

To set up the keytab file, see [Create the SQL Server service keytab file](sql-server-linux-ad-auth-adutil-tutorial.md#create-the-sql-server-service-keytab-file).

## Install adutil

If you don't accept the EULA during the time of install, when you run the **adutil** command for the first time, you'll have to run it with the flag `--accept-eula`. This is true for all distributions.

# [RHEL](#tab/rhel)

1. Download the Microsoft Red Hat repository configuration file.

    ```bash
    sudo curl -o /etc/yum.repos.d/msprod.repo https://packages.microsoft.com/config/rhel/8/prod.repo
    ```

1. If you had a previous preview version of **adutil** installed, remove any older **adutil** packages using the below command.

    ```bash
    sudo yum remove adutil-preview
    ```

1. Run the following commands to install **adutil**. `ACCEPT_EULA=Y` accepts the EULA for **adutil**. The EULA is placed at the path `/usr/share/adutil/`.

    ```bash
    sudo ACCEPT_EULA=Y yum install -y adutil
    ```

# [Ubuntu](#tab/ubuntu)

1. Import the public repository GPG keys and then register the Microsoft Ubuntu repository.

    ### Ubuntu 18.04

    ```bash
    curl https://packages.microsoft.com/keys/microsoft.asc | sudo apt-key add -
    sudo curl https://packages.microsoft.com/config/ubuntu/18.04/prod.list | sudo tee /etc/apt/sources.list.d/msprod.list
    ```

    ### Ubuntu 20.04

    ```bash
    sudo wget -qO /etc/apt/trusted.gpg.d/microsoft.asc https://packages.microsoft.com/keys/microsoft.asc
    sudo curl https://packages.microsoft.com/config/ubuntu/20.04/prod.list | sudo tee /etc/apt/sources.list.d/msprod.list
    ```

1. If you had a previous preview version of **adutil** installed, remove any older **adutil** packages using the below command.

    ```bash
    sudo apt-get remove adutil-preview
    ```

1. Run the following command to install **adutil**. `ACCEPT_EULA=Y` accepts the EULA for **adutil**. The EULA is placed at the path `/usr/share/adutil/`.

    ```bash
    sudo apt-get update
    sudo ACCEPT_EULA=Y apt-get install -y adutil
    ```

# [SLES](#tab/sles)

1. Add the Microsoft SQL Server repository to Zypper.

    ### SLES 12

    ```bash
    sudo rpm --import https://packages.microsoft.com/keys/microsoft.asc
    sudo zypper addrepo -fc https://packages.microsoft.com/config/sles/12/prod.repo
    ```

    ### SLES 15

    ```bash
    sudo rpm --import https://packages.microsoft.com/keys/microsoft.asc
    sudo zypper addrepo -fc https://packages.microsoft.com/config/sles/15/prod.repo
    ```

1. If you had a previous preview version of **adutil** installed, remove any older **adutil** packages using the below command.

    ```bash
    sudo zypper remove adutil-preview
    ```

1. Run the following command to install **adutil**. `ACCEPT_EULA=Y` accepts the EULA for **adutil**. The EULA is placed at the path `/usr/share/adutil/`.

    ```bash
    sudo ACCEPT_EULA=Y zypper install -y adutil
    ```

---

## Use adutil to manage Windows Active Directory

Make sure that you download **adutil** to a host that is already joined to an Active Directory domain. You also need to obtain or renew the Kerberos TGT (ticket-granting ticket), using the **kinit** command and a privileged domain account. The account you use must have permission to create accounts and Service Principal Names (SPNs) on the domain.

Here are some examples of actions that you can perform using **adutil**. To see a list of top-level commands, type `adutil --help`. This command will show you the top-level commands that you can use to manage and interact with Active Directory.

```bash
$ adutil --help
adutil - A general AD utility
  Usage:
    adutil [account|delegation|group|keytab|machine|ou|spn|user|config]
  Subcommands:
    account      Functions for generic account operations
    delegation   Functions for configuring delegation permissions
    group        Functions for group management
    keytab       Functions for keytab management
    machine      Functions for managing machine accounts
    ou           Functions for managing organizational units
    spn          Functions for service principal name (SPN) management
    user         Functions for user account management
    config       Functions for modifying adutil configuration
  Flags:
       --version       Displays the program version string.
    -h --help          Displays help with available flag, subcommand, and positional value parameters.
    -d --debug         Display additional debugging information when making LDAP/Kerberos calls.
       --accept-eula   Accepts the current EULA for adutil. This has no effect if the EULA has already been accepted.
```

To seek help with the next level of commands, you can further run the help option as shown below:

```bash
$ adutil spn --help
spn - Functions for service principal name (SPN) management
  Usage:
    spn [add|addauto|delete|search|show]
  Subcommands:
    add       Adds the provided SPNs to an account
    addauto   Automatically generate SPNs based on SPN component inputs and add them to an account
    delete    Deletes the provided SPNs from an account
    search    Search for an SPN by name or list all SPNs in the directory
    show      Get the list of SPNs assigned to an account
  Flags:
    --version       Displays the program version string.
    -h --help          Displays help with available flag, subcommand, and positional value parameters.
    -d --debug         Display additional debugging information when making LDAP/Kerberos calls.
       --accept-eula   Accepts the current EULA for adutil. This has no effect if the EULA has already been accepted.
```

```bash
$ adutil spn search --help
search - Search for an SPN by name or list all SPNs in the directory
  Usage:
     search [name]
  Positional Variables:
    name   OPTIONAL: Name of the SPN to search for in the directory. * can be used as a wildcard
  Flags:
    --version       Displays the program version string.
    -h --help          Displays help with available flag, subcommand, and positional value parameters.
    -n --name          OPTIONAL: Name of the SPN to search for in the directory. * can be used as a wildcard
    -f --filter        OPTIONAL: Filter for the search (User,Machine,Group)
    -o --ouname        OPTIONAL: Distinguished name of OU in which SPNs should be searched. If omitted, the entire directory will be searched.
    -d --debug         Display additional debugging information when making LDAP/Kerberos calls.
       --accept-eula   Accepts the current EULA for adutil. This has no effect if the EULA has already been accepted.
```

## Samples

Each command is documented so you can get started right away. Here are some of the typical activities that **adutil** is used for when configuring or administering Active Directory authentication for SQL Server on Linux and containers:

- Create an account in Active Directory:

  ```bash
  adutil user create --name sqluser --distname CN=sqluser,CN=Users,DC=CONTOSO,DC=COM
  ```

- Create SPNs associated with an account or service:

  ```bash
  adutil spn addauto -n sqluser -s MSSQLSvc -H mymachine.contoso.com -p 1433
  ```

- Create keytabs using **adutil**:

  ```bash
  adutil keytab createauto -k /var/opt/mssql/secrets/mssql.keytab -p 1433 -H mymachine.contoso.com --password 'P@ssw0rd' -s MSSQLSvc
  ```

You can refer to the reference manual page of **adutil** using the command `man adutil`.

## Next steps

- [Configure Active Directory authentication with SQL Server on Linux using adutil](sql-server-linux-ad-auth-adutil-tutorial.md)
- [Configure Active Directory authentication with SQL Server on Linux containers](sql-server-linux-containers-ad-auth-adutil-tutorial.md)
- [Rotate SQL Server on Linux keytabs](sql-server-linux-ad-auth-rotate-keytabs.md)
