---
author: rwestMSFT
ms.author: randolphwest
ms.date: 10/08/2026
ms.service: sql
ms.topic: include
ms.custom:
  - linux-related-content
---
<a id="RHEL"></a>

Use the following steps to install the **mssql-tools18** on Red Hat Enterprise Linux.

- [!INCLUDE [rhel-10](rhel-10.md)]
- [!INCLUDE [rhel-9](rhel-9.md)]
- [!INCLUDE [rhel-8](rhel-8.md)]

<!--SQL Server 2017 and 2019 on Linux-->
::: moniker range="<=sql-server-linux-ver15 || <=sql-server-ver15"

Download the Microsoft Red Hat repository configuration file.

```bash
curl -fsSL https://packages.microsoft.com/config/rhel/8/prod.repo | sudo tee /etc/yum.repos.d/mssql-release.repo
```

::: moniker-end
<!--SQL Server 2022 on Linux-->
::: moniker range="=sql-server-linux-ver16 || =sql-server-ver16"

Download the Microsoft Red Hat repository configuration file.

```bash
curl -fsSL https://packages.microsoft.com/config/rhel/9/prod.repo | sudo tee /etc/yum.repos.d/mssql-release.repo
```

::: moniker-end
<!--SQL Server 2025 on Linux-->
::: moniker range=">=sql-server-linux-ver17 || >=sql-server-ver17"

Download the Microsoft Red Hat repository configuration file from the RHEL 9 repo. The same versions of tools also work for RHEL 10.

```bash
curl -fsSL https://packages.microsoft.com/config/rhel/9/prod.repo | sudo tee /etc/yum.repos.d/mssql-release.repo
```

::: moniker-end

1. If you had a previous version of **mssql-tools** installed, remove any older unixODBC packages.

   ```bash
   sudo yum remove mssql-tools unixODBC-utf16 unixODBC-utf16-devel
   ```

1. Run the following commands to install **mssql-tools18** with the unixODBC developer package.

   ```bash
   sudo yum install -y mssql-tools18 unixODBC-devel
   ```

   To update to the latest version of **mssql-tools18**, run the following commands:

   ```bash
   sudo yum check-update
   sudo yum update mssql-tools18
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
