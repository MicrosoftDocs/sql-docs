---
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/22/2024
ms.service: sql
ms.topic: include
ms.custom:
  - linux-related-content
---
<a id="SLES"></a>

Use the following steps to install the **mssql-tools18** on SUSE Linux Enterprise Server.

1. Import the Microsoft package signing key.

   ```bash
   curl -O https://packages.microsoft.com/keys/microsoft.asc
   sudo rpm --import microsoft.asc
   ```

1. Add the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] repository to Zypper.

   - For SLES 15, use the following command:

     ```bash
     sudo zypper ar https://packages.microsoft.com/config/sles/15/prod.repo
     ```

   - For SLES 12, use the following command:

     ```bash
     sudo zypper ar https://packages.microsoft.com/config/sles/12/prod.repo
     ```

1. Install **mssql-tools18** with the unixODBC developer package.

   - For SLES 15, use the following command:

   ```bash
   sudo zypper install -y mssql-tools18 unixODBC-devel glibc-locale-base
   ```

   - For SLES 12, use the following command:

   ```bash
   sudo zypper install -y mssql-tools18 unixODBC-devel
   ```

   To update to the latest version of **mssql-tools18**, run the following commands:

   ```bash
   sudo zypper refresh
   sudo zypper update mssql-tools18
   ```

1. **Optional**: Add `/opt/mssql-tools18/bin/` to your `PATH` environment variable in a bash shell.

   To make **sqlcmd** and **bcp** accessible from the bash shell for login sessions, modify your `PATH` in the `~/.bash_profile` file with the following command:

   ```bash
   echo 'export PATH="$PATH:/opt/mssql-tools18/bin"' >> ~/.bash_profile
   ```

   To make **sqlcmd** or **bcp** accessible from the bash shell for interactive/non-login sessions, modify the `PATH` in the `~/.bashrc` file with the following command:

   ```bash
   echo 'export PATH="$PATH:/opt/mssql-tools18/bin"' >> ~/.bashrc
   source ~/.bashrc
   ```
