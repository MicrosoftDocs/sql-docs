---
author: rwestMSFT
ms.author: randolphwest
ms.date: 11/29/2023
ms.service: sql
ms.topic: include
ms.custom:
  - linux-related-content
---
<a id="ubuntu"></a>

Use the following steps to install the **mssql-tools18** on Ubuntu.

> [!NOTE]  
>  
> - Ubuntu 18.04 is supported starting with SQL Server 2019 CU 3.
> - Ubuntu 20.04 is supported starting with SQL Server 2019 CU 10.
> - Ubuntu 22.04 is supported starting with SQL Server 2022 CU 10.

1. Enter superuser mode.

   ```bash
   sudo su
   ```

1. Import the public repository GPG keys.

   ```bash
   curl https://packages.microsoft.com/keys/microsoft.asc | sudo tee /etc/apt/trusted.gpg.d/microsoft.asc
   ```

1. Register the Microsoft Ubuntu repository.

   - For Ubuntu 22.04, use the following command:

     ```bash
     curl https://packages.microsoft.com/config/ubuntu/22.04/prod.list > /etc/apt/sources.list.d/mssql-release.list
     ```

   - For Ubuntu 20.04, use the following command:

     ```bash
     curl https://packages.microsoft.com/config/ubuntu/20.04/prod.list > /etc/apt/sources.list.d/mssql-release.list
     ```

   - For Ubuntu 18.04, use the following command:

     ```bash
     curl https://packages.microsoft.com/config/ubuntu/18.04/prod.list > /etc/apt/sources.list.d/mssql-release.list
     ```

   - For Ubuntu 16.04, use the following command:

     ```bash
     curl https://packages.microsoft.com/config/ubuntu/16.04/prod.list > /etc/apt/sources.list.d/mssql-release.list
     ```

1. Exit superuser mode.

   ```bash
   exit
   ```

1. Update the sources list and run the installation command with the unixODBC developer package.

   ```bash
   sudo apt-get update
   sudo apt-get install mssql-tools18 unixodbc-dev
   ```

   > [!NOTE]  
   > To update to the latest version of **mssql-tools**, run the following commands:
   >  
   > ```bash
   > sudo apt-get update  
   > sudo apt-get install mssql-tools18
   > ```

1. **Optional**: Add `/opt/mssql-tools18/bin/` to your `PATH` environment variable in a bash shell.

   To make **sqlcmd** and **bcp** accessible from the bash shell for login sessions, modify your `PATH` in the `~/.bash_profile` file with the following command:

   ```bash
   echo 'export PATH="$PATH:/opt/mssql-tools18/bin"' >> ~/.bash_profile
   ```

   To make **sqlcmd** and **bcp** accessible from the bash shell for interactive/non-login sessions, modify the `PATH` in the `~/.bashrc` file with the following command:

   ```bash
   echo 'export PATH="$PATH:/opt/mssql-tools18/bin"' >> ~/.bashrc
   source ~/.bashrc
   ```
