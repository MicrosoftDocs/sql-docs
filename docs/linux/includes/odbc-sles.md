---
author: rwestMSFT
ms.author: randolphwest
ms.date: 10/08/2026
ms.service: sql
ms.topic: include
ms.custom:
  - linux-related-content
---
<a id="SLES"></a>

Use the following steps to install **mssql-tools18** on SUSE Linux Enterprise Server.

> [!NOTE]  
> Starting in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)], SUSE Linux Enterprise Server (SLES) isn't supported.

Import the Microsoft package signing key.

```bash
sudo rpm --import https://packages.microsoft.com/keys/microsoft.asc
```

<!--SQL Server 2017 on Linux-->
::: moniker range="=sql-server-linux-2017 || =sql-server-2017"

Add the Microsoft repository to Zypper.

```bash
sudo zypper ar https://packages.microsoft.com/config/sles/12/prod.repo
```

Install **mssql-tools18** with the unixODBC developer package.

```bash
sudo zypper install -y mssql-tools18 unixODBC-devel
```

::: moniker-end
<!--SQL Server 2019 and later versions on Linux-->
::: moniker range=">=sql-server-linux-ver15 || >=sql-server-ver15"

Add the Microsoft repository to Zypper.

```bash
sudo zypper ar https://packages.microsoft.com/config/sles/15/prod.repo
```

Install **mssql-tools18** with the unixODBC developer package.

```bash
sudo zypper install -y mssql-tools18 unixODBC-devel glibc-locale-base
```

::: moniker-end

1. To update to the latest version of **mssql-tools18**, run the following commands:

   ```bash
   sudo zypper refresh
   sudo zypper update mssql-tools18
   ```

1. **Optional**: Add `/opt/mssql-tools18/bin/` to your `PATH` environment variable in a Bash shell.

   To make **`sqlcmd`** and **`bcp`** accessible from the Bash shell for login sessions, modify your `PATH` in the `~/.bash_profile` file with the following command:

   ```bash
   echo 'export PATH="$PATH:/opt/mssql-tools18/bin"' >> ~/.bash_profile
   source ~/.bash_profile
   ```

   To make **`sqlcmd`** and **`bcp`** accessible from the Bash shell for interactive and non-login sessions, modify the `PATH` in the `~/.bashrc` file with the following command:

   ```bash
   echo 'export PATH="$PATH:/opt/mssql-tools18/bin"' >> ~/.bashrc
   source ~/.bashrc
   ```
