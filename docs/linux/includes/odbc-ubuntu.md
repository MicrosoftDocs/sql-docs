---
author: rwestMSFT
ms.author: randolphwest
ms.date: 10/08/2026
ms.service: sql
ms.topic: include
ms.custom:
  - linux-related-content
---
<a id="ubuntu"></a>

Use the following steps to install the **mssql-tools18** on Ubuntu.

- [!INCLUDE [ubuntu-2404](ubuntu-2404.md)]
- [!INCLUDE [ubuntu-2204](ubuntu-2204.md)]
- [!INCLUDE [ubuntu-2004](ubuntu-2004.md)]
- [!INCLUDE [ubuntu-1804](ubuntu-1804.md)]

<!--SQL Server 2017 on Linux-->
::: moniker range="=sql-server-linux-2017 || =sql-server-2017"

Import the public repository GPG keys.

```bash
curl -fsSL https://packages.microsoft.com/keys/microsoft.asc | sudo tee /etc/apt/trusted.gpg.d/microsoft.asc
```

Register the Microsoft Ubuntu repository.

```bash
curl -fsSL https://packages.microsoft.com/config/ubuntu/18.04/prod.list | sudo tee /etc/apt/sources.list.d/mssql-release.list
```

::: moniker-end
<!--SQL Server 2019 on Linux-->
::: moniker range="=sql-server-linux-ver15 || =sql-server-ver15"

Import the public repository GPG keys.

```bash
curl -fsSL https://packages.microsoft.com/keys/microsoft.asc | sudo tee /etc/apt/trusted.gpg.d/microsoft.asc
```

Register the Microsoft Ubuntu repository.

```bash
curl -fsSL https://packages.microsoft.com/config/ubuntu/20.04/prod.list | sudo tee /etc/apt/sources.list.d/mssql-release.list
```

::: moniker-end
<!--SQL Server 2022 on Linux-->
::: moniker range="=sql-server-linux-ver16 || =sql-server-ver16"

Import the public repository GPG keys.

```bash
curl -fsSL https://packages.microsoft.com/keys/microsoft.asc | sudo tee /etc/apt/trusted.gpg.d/microsoft.asc
```

Register the Microsoft Ubuntu repository.

```bash
curl -fsSL https://packages.microsoft.com/config/ubuntu/22.04/prod.list | sudo tee /etc/apt/sources.list.d/mssql-release.list
```

::: moniker-end
<!--SQL Server 2025 on Linux-->
::: moniker range=">=sql-server-linux-ver17 || >=sql-server-ver17"

Download the public key, convert it from ASCII to GPG format, and write it to the required location.

```bash
curl -fsSL https://packages.microsoft.com/keys/microsoft.asc | sudo gpg --batch --yes --dearmor -o /usr/share/keyrings/microsoft-prod.gpg
```

Register the Microsoft Ubuntu repository.

```bash
curl -fsSL https://packages.microsoft.com/config/ubuntu/24.04/prod.list | sudo tee /etc/apt/sources.list.d/mssql-release.list
```

::: moniker-end

1. Update the sources list and run the installation command with the unixODBC developer package.

   ```bash
   sudo apt-get update
   sudo apt-get install mssql-tools18 unixodbc-dev
   ```

   To update to the latest version of **mssql-tools18**, run the following commands:

   ```bash
   sudo apt-get update
   sudo apt-get install mssql-tools18
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
