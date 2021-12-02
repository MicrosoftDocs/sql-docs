---
title: Introduction to adutil - Active Directory Utility 
description: Overview of adutil and using it to configure Active Directory for SQL Server on Linux
author: amvin87
ms.author: amitkh
ms.reviewer: vanto
ms.date: 09/30/2021
ms.topic: conceptual
ms.prod: sql
ms.technology: linux
moniker: ">= sql-server-linux-2017 || >= sql-server-2017 || =sqlallproducts-allversions"
---

# Introduction to adutil - Active Directory utility

Adutil is a command-line interface (CLI) utility for interacting and managing Active Directory domains. You can use this tool to simplify Active Directory (AD) authentication configuration for both SQL Server on Linux and Linux-based SQL containers. Adutil eliminates the need to switch between Windows and Linux machines to manage Windows Active Directory when enabling AD authentication for SQL Server on Linux and containers.

Having adutil isn't a prerequisite for enabling AD authentication for SQL Server on Linux. You can use utilities like ktpass, as explained in [Tutorial: Use Active Directory authentication with SQL Server on Linux](sql-server-linux-active-directory-authentication.md), to enable AD authentication for SQL Server on Linux or containers.

Adutil is designed as a series of commands and subcommands, with additional flags that can be specified as further input. A category of administrative functions is represented by each top-level command. Within that category, each subcommand is an operation. We'll show you how you can download and get started with adutil.

> [!NOTE]
> adutil is a tool developed with SQL Server as the application. Hence, support for adutil will be limited for SQL Server use cases only.

> [!NOTE]
> For controlled and secure environments where Lightweight Directory Access Protocol (LDAP) over Secure Sockets Layer (SSL) aka LDAPS is preferred over Lightweight Directory Access Protocol (LDAP). 
> Please enable or set the “useLdaps“ option to ‘true’ in adutil.json configuration file which is usually located at: “/var/opt/mssql/.adutil/adutil.json” when run under the mssql user.  Here is a sample json with setting:
```bash
  {
    "useLdaps": "true"
  }
```
> This setting useLDAPS is set to "false" by default. When configuring this setting and using mssql-conf to setup the keytab, you need to ensure that mssql-conf command is running under the user mssql.  You can follow the below steps to setup the keytab using mssql-conf option:
    1.	Run as mssql user, you can run the command: “sudo su mssql” 
    2.	Now you can run the command to setup the keytab using the command: “mssql-conf setup-ad-keytab”
> If you are using regular LDAP we would like to clarify that passwords are not exchanged in plain text.  For more information on how to configure LDAPS and how it differs from LDAP. Please refer the article [LDAP over SSL (LDAPS) Certificate](ttps://social.technet.microsoft.com/wiki/contents/articles/2980.ldap-over-ssl-ldaps-certificate.aspx#Enabling_LDAPS_for_Client_Authentication)

## Installing adutil

> [!NOTE]
> If you do not accept the EULA during the time of install, when you run the adutil command for the first time, you will have to run it with the flag `--accept-eula`. This is true for all distributions.

# [RHEL](#tab/rhel)

1. Download the Microsoft Red Hat repository configuration file.

    ```bash
    sudo curl -o /etc/yum.repos.d/msprod.repo https://packages.microsoft.com/config/rhel/8/prod.repo
    ```

1. If you had a previous preview version of adutil installed, remove any older adutil packages using the below command.

    ```bash
    sudo yum remove adutil-preview
    ```

1. Run the following commands to install **adutil**. `ACCEPT_EULA=Y` accepts the EULA for adutil. The EULA is placed at the path `/usr/share/adutil/`.

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

1. If you had a previous preview version of adutil installed, remove any older adutil packages using the below command.

    ```bash
    sudo apt-get remove adutil-preview
    ```

1. Run the following command to install **adutil**. `ACCEPT_EULA=Y` accepts the EULA for adutil. The EULA is placed at the path `/usr/share/adutil/`.

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

1. If you had a previous preview version of adutil installed, remove any older adutil packages using the below command.

    ```bash
    sudo zypper remove adutil-preview
    ```

1. Run the following command to install **adutil**. `ACCEPT_EULA=Y` accepts the EULA for adutil. The EULA is placed at the path `/usr/share/adutil/`.

    ```bash
    sudo ACCEPT_EULA=Y zypper install -y adutil
    ```

---

## Manage Windows AD using adutil

Ensure that you download adutil to a host that is domain joined. You also need to obtain or renew the kerberos TGT (ticket-granting ticket) using the `kinit` command. Ensure that the account you use to `kinit` with has the permissions to execute the actions you intend to run on Windows AD through the adutil tool. For example, if you intend to create accounts and service principal names (SPN) using the adutil tool, then you should `kinit` with the account that has privileges to create SPNs and users on your AD.

Here are some examples of actions that you can perform using adutil. To see a list of top-level commands, type `adutil --help`. This command will show you the top-level commands that you can use to manage and interact with AD.

```bash
 $adutil --help 
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

Each command is documented so that you can get started right away. Here are some of the typical activities that adutil is used for when configuring or administering AD authentication for SQL Server on Linux and containers:

- Creating an account in AD: 

    ```bash
    adutil user create --name sqluser --distname CN=sqluser,CN=Users,DC=CONTOSO,DC=COM 
    ```

- Creating SPNs associated with an account or service:

    ```bash
    adutil spn addauto -n sqluser -s MSSQLSvc -H mymachine.contoso.com -p 1433 
    ```

- Creating keytabs using adutil:

    ```bash 
    adutil keytab createauto -k /var/opt/mssql/secrets/mssql.keytab -p 1433 -H mymachine.contoso.com --password 'P@ssw0rd' -s MSSQLSvc 
    ```

You can refer to the reference manual page of adutil using the command `man adutil`.

## Next steps

- [Configure Active Directory authentication with SQL Server on Linux using adutil](sql-server-linux-ad-auth-adutil-tutorial.md)
- [Configure Active Directory authentication with SQL Server on Linux containers](sql-server-linux-containers-ad-auth-adutil-tutorial.md)
- [Rotate SQL Server on Linux keytabs](sql-server-linux-ad-auth-rotate-keytabs.md)
