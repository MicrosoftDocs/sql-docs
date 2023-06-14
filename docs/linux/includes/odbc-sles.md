---
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/11/2023
ms.service: sql
ms.topic: include
---
<a id="SLES"></a>

Use the following steps to install the **mssql-tools18** on SUSE Linux Enterprise Server.

1. Enter superuser mode.

   ```bash
   sudo su
   ```

1. Import the Microsoft package signing key.

   ```bash
   curl -O https://packages.microsoft.com/keys/microsoft.asc
   rpm --import microsoft.asc
   ```

1. Add the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] repository to Zypper.

   - For SLES 15, use the following command:

     ```bash
     zypper ar https://packages.microsoft.com/config/sles/15/prod.repo
     ```

   - For SLES 12, use the following command:

     ```bash
     zypper ar https://packages.microsoft.com/config/sles/12/prod.repo
     ```

1. Exit superuser mode.

   ```bash
   exit
   ```

1. Install **mssql-tools18** with the unixODBC developer package.

   ```bash
   sudo zypper install -y mssql-tools18 unixodbc-dev
   ```

   > [!NOTE]  
   > To update to the latest version of **mssql-tools18**, run the following commands:
   >  
   > ```bash
   > sudo zypper refresh
   > sudo zypper update mssql-tools18
   > ```

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
