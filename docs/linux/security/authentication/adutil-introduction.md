---
title: Introduction to adutil - Active Directory Utility
description: Overview of adutil, a utility for configuring and managing Active Directory domains for SQL Server on Linux and containers
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: amitkh, atsingh
ms.date: 05/07/2026
ms.service: sql
ms.subservice: linux
ms.topic: concept-article
ms.custom:
  - linux-related-content
monikerRange: ">=sql-server-linux-2017 || >=sql-server-2017 || =sqlallproducts-allversions"
---

# Introduction to `adutil` - Active Directory utility

[!INCLUDE [SQL Server - Linux](../../../includes/applies-to-version/sql-linux.md)]

The **`adutil`** tool is a command-line interface (CLI) utility for configuring and managing Windows Active Directory domains for SQL Server on Linux and containers. It eliminates the need to switch between Windows and Linux machines to manage Active Directory.

> [!NOTE]  
> Support for **`adutil`** is limited to SQL Server use cases only. You can also use other utilities like **`ktpass`** to enable Active Directory authentication, as explained in [Tutorial: Use Active Directory authentication with SQL Server on Linux](active-directory-tutorial.md).

Before you get started, make sure you install **`adutil`** on a host that is already joined to an Active Directory domain.

The **`adutil`** tool is designed as a series of commands and subcommands, with extra flags that you specify as further input. Each top-level command represents a category of administrative functions. Within that category, each subcommand is an operation. This article shows you how to install and get started with **`adutil`**.

## Configure `adutil` for LDAP over Secure Sockets Layer (SSL)

You should use Lightweight Directory Access Protocol over SSL (LDAPS) instead of Lightweight Directory Access Protocol (LDAP). For more information about LDAP, see [Lightweight Directory Access Protocol (LDAP)](understand-active-directory.md#ldap).

You can set the `useLdaps` option to `true` in the `adutil.json` configuration file. When you run **`adutil`** under the `mssql` user, the configuration file is located at `/var/opt/mssql/.adutil/adutil.json`. This JSON code sample shows how to configure the setting:

```json
{
    "useLdaps": "true"
}
```

By default, `useLdaps` is `false`. When you configure this setting and use **`mssql-conf`** to create the keytab (key table), make sure you run **`mssql-conf`** as the `mssql` user. Run the following command to switch to the `mssql` user:

```bash
sudo su mssql
```

To set up the keytab using **`mssql-conf`**, see [Create the SQL Server service keytab file using mssql-conf](adutil-tutorial.md#create-the-sql-server-service-keytab-file-using-mssql-conf).

## Install `adutil`

If you don't accept the end user license agreement (EULA) during installation, when you run the **`adutil`** command for the first time, you must run it with the `--accept-eula` flag (for all distributions).

# [Red Hat Enterprise Linux (RHEL)](#tab/rhel)

1. Download the Microsoft Red Hat repository configuration file.

   **RHEL 10**

   ```bash
   sudo curl -o /etc/yum.repos.d/msprod.repo https://packages.microsoft.com/config/rhel/10/prod.repo
   ```

   **RHEL 9**

   ```bash
   sudo curl -o /etc/yum.repos.d/msprod.repo https://packages.microsoft.com/config/rhel/9/prod.repo
   ```

   **RHEL 8**

   ```bash
   sudo curl -o /etc/yum.repos.d/msprod.repo https://packages.microsoft.com/config/rhel/8/prod.repo
   ```

1. If you installed a previous preview version of **`adutil`**, remove any older **`adutil`** packages using the following command.

   ```bash
   sudo yum remove adutil-preview
   ```

1. To install **`adutil`**, run the following command. `ACCEPT_EULA=Y` accepts the EULA for **`adutil`**. The EULA is located at `/usr/share/adutil/`.

   ```bash
   sudo ACCEPT_EULA=Y yum install -y adutil
   ```

# [Ubuntu](#tab/ubuntu)

1. Import the public repository GNU Privacy Guard (GPG) keys and then register the Microsoft Ubuntu repository.

   **Ubuntu 24.04**

   ```bash
   curl -fsSL https://packages.microsoft.com/keys/microsoft.asc | sudo gpg --dearmor -o /usr/share/keyrings/microsoft-prod.gpg
   curl https://packages.microsoft.com/config/ubuntu/24.04/prod.list | sudo tee /etc/apt/sources.list.d/msprod.list
   ```

   **Ubuntu 22.04**

   ```bash
   curl -fsSL https://packages.microsoft.com/keys/microsoft.asc | sudo gpg --dearmor -o /usr/share/keyrings/microsoft-prod.gpg
   curl https://packages.microsoft.com/config/ubuntu/22.04/prod.list | sudo tee /etc/apt/sources.list.d/msprod.list
   ```

   **Ubuntu 20.04**

   ```bash
   curl https://packages.microsoft.com/keys/microsoft.asc | sudo tee /etc/apt/trusted.gpg.d/microsoft.asc
   curl https://packages.microsoft.com/config/ubuntu/20.04/prod.list | sudo tee /etc/apt/sources.list.d/msprod.list
   ```

   > [!TIP]  
   > If you experience a key-related issue on Ubuntu 22.04 and later versions, run the following command instead:
   >
   > ```bash
   > curl https://packages.microsoft.com/keys/microsoft.asc | sudo tee /etc/apt/trusted.gpg.d/microsoft.asc
   > ```

1. If you installed a previous preview version of **`adutil`**, remove any older **`adutil`** packages using the following command.

   ```bash
   sudo apt-get remove adutil-preview
   ```

1. To install **`adutil`**, run the following command. `ACCEPT_EULA=Y` accepts the EULA for **`adutil`**. The EULA is located at `/usr/share/adutil/`.

   ```bash
   sudo apt-get update
   sudo ACCEPT_EULA=Y apt-get install -y adutil
   ```

# [SUSE Linux Enterprise Server](#tab/sles)

1. Add the Microsoft SQL Server repository to Zypper.

   **SLES 15**

   ```bash
   sudo rpm --import https://packages.microsoft.com/keys/microsoft.asc
   sudo zypper addrepo -fc https://packages.microsoft.com/config/sles/15/prod.repo
   ```

   **SLES 12**

   ```bash
   sudo rpm --import https://packages.microsoft.com/keys/microsoft.asc
   sudo zypper addrepo -fc https://packages.microsoft.com/config/sles/12/prod.repo
   ```

1. If you installed a previous preview version of **`adutil`**, remove any older **`adutil`** packages using the following command.

   ```bash
   sudo zypper remove adutil-preview
   ```

1. To install **`adutil`**, run the following command. `ACCEPT_EULA=Y` accepts the EULA for **`adutil`**. The EULA is located at `/usr/share/adutil/`.

   ```bash
   sudo zypper refresh
   sudo ACCEPT_EULA=Y zypper install -y adutil
   ```

---

## Use `adutil` to manage Windows Active Directory

Make sure that you install **`adutil`** on a host that is already joined to an Active Directory domain. You also need to obtain or renew the Kerberos ticket-granting ticket (TGT), using the **`kinit`** command and a privileged domain account. The account you use must have permission to create accounts and Service Principal Names (SPNs) on the domain.

Here are some examples of actions that you can perform using **`adutil`**. To see a list of top-level commands, type `adutil --help`. This command shows you the top-level commands that you can use to manage and interact with Active Directory.

```bash
adutil --help
```

You see the following output:

```output
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

To get help with lower-level commands, use the following examples:

- `spn` command:

  ```bash
  adutil spn --help
  ```

- `spn search` command:

  ```bash
  adutil spn search --help
  ```

## Samples

Each command is documented so you can get started right away. Here are some of the typical activities that **`adutil`** is used for when configuring or administering Active Directory authentication for SQL Server on Linux and containers:

- Create an account in Active Directory:

  ```bash
  adutil user create --name sqluser --distname CN=sqluser,CN=Users,DC=CONTOSO,DC=COM
  ```

- Create SPNs associated with an account or service:

  ```bash
  adutil spn addauto -n sqluser -s MSSQLSvc -H mymachine.contoso.com -p 1433
  ```

- Create keytabs using **`adutil`**:

  ```bash
  adutil keytab createauto -k /var/opt/mssql/secrets/mssql.keytab -p 1433 -H mymachine.contoso.com --password '<password>' -s MSSQLSvc
  ```

  > [!CAUTION]  
  > [!INCLUDE [password-complexity](../../includes/password-complexity.md)]

For more information, see the **`adutil`** reference manual page by using `man adutil`.

## Related content

- [Tutorial: Use adutil to configure Active Directory authentication with SQL Server on Linux](adutil-tutorial.md)
- [Tutorial: Configure Active Directory authentication with SQL Server on Linux containers](../../containers/tutorial-adutil.md)
- [Rotate keytabs for SQL Server on Linux](rotate-keytabs.md)
