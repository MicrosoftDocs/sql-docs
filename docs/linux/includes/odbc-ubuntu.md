---
author: rwestMSFT
ms.author: randolphwest
ms.date: 01/27/2026
ms.service: sql
ms.topic: include
ms.custom:
  - linux-related-content
---
<a id="ubuntu"></a>

Use the following steps to install the **mssql-tools18** on Ubuntu.

- Ubuntu 24.04 is supported starting with [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] CU 1.
- Ubuntu 22.04 is supported starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] CU 10.
- Ubuntu 20.04 is supported starting with [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] CU 10.
- Ubuntu 18.04 is supported starting with [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] CU 3.

### [Ubuntu 18.04](#tab/odbc-ubuntu-1804)

1. Enter superuser mode.

   ```bash
   sudo su
   ```

1. Import the public repository GPG keys.

   ```bash
   curl https://packages.microsoft.com/keys/microsoft.asc | sudo tee /etc/apt/trusted.gpg.d/microsoft.asc
   ```

1. Register the Microsoft Ubuntu repository.

   ```bash
   curl https://packages.microsoft.com/config/ubuntu/18.04/prod.list | tee /etc/apt/sources.list.d/mssql-release.list
   ```

1. Exit superuser mode.

   ```bash
   exit
   ```

### [Ubuntu 20.04](#tab/odbc-ubuntu-2004)

1. Enter superuser mode.

   ```bash
   sudo su
   ```

1. Import the public repository GPG keys.

   ```bash
   curl https://packages.microsoft.com/keys/microsoft.asc | sudo tee /etc/apt/trusted.gpg.d/microsoft.asc
   ```

1. Register the Microsoft Ubuntu repository.

   ```bash
   curl https://packages.microsoft.com/config/ubuntu/20.04/prod.list | tee /etc/apt/sources.list.d/mssql-release.list
   ```

1. Exit superuser mode.

   ```bash
   exit
   ```

### [Ubuntu 22.04](#tab/odbc-ubuntu-2204)

1. Enter superuser mode.

   ```bash
   sudo su
   ```

1. Import the public repository GPG keys.

   ```bash
   curl https://packages.microsoft.com/keys/microsoft.asc | sudo tee /etc/apt/trusted.gpg.d/microsoft.asc
   ```

1. Register the Microsoft Ubuntu repository.

   ```bash
   curl https://packages.microsoft.com/config/ubuntu/22.04/prod.list | tee /etc/apt/sources.list.d/mssql-release.list
   ```

1. Exit superuser mode.

   ```bash
   exit
   ```

### [Ubuntu 24.04](#tab/odbc-ubuntu-2404)

Use the following steps to install the **mssql-tools18** for [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] on Ubuntu 24.04.

1. Enter superuser mode.

   ```bash
   sudo su
   ```

1. Register the Microsoft repository for Ubuntu 24.04.

   ```bash
   curl -sSL -O https://packages.microsoft.com/config/ubuntu/24.04/packages-microsoft-prod.deb
   ```

1. Install the repository package:

   ```bash
   sudo dpkg -i packages-microsoft-prod.deb
   ```

1. Exit superuser mode.

   ```bash
   exit
   ```

---

1. Update the sources list and run the installation command with the unixODBC developer package.

   ```bash
   sudo apt-get update
   sudo apt-get install mssql-tools18 unixodbc-dev
   ```

   To update to the latest version of **mssql-tools**, run the following commands:

   ```bash
   sudo apt-get update
   sudo apt-get install mssql-tools18
   ```

1. **Optional**: Add `/opt/mssql-tools18/bin/` to your `PATH` environment variable in a bash shell.

   To make **sqlcmd** and **bcp** accessible from the bash shell for login sessions, modify your `PATH` in the `~/.bash_profile` file with the following command:

   ```bash
   echo 'export PATH="$PATH:/opt/mssql-tools18/bin"' >> ~/.bash_profile
   source ~/.bash_profile
   ```

   To make **sqlcmd** and **bcp** accessible from the bash shell for interactive/non-login sessions, modify the `PATH` in the `~/.bashrc` file with the following command:

   ```bash
   echo 'export PATH="$PATH:/opt/mssql-tools18/bin"' >> ~/.bashrc
   source ~/.bashrc
   ```
