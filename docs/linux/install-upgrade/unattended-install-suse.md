---
title: Unattended Install for SQL Server on SUSE Linux Enterprise Server
titleSuffix: SQL Server
description: Use a sample bash script to install SQL Server on SUSE Linux Enterprise Server (SLES) without interactive input.
author: rwestMSFT
ms.author: randolphwest
ms.date: 01/02/2026
ms.service: sql
ms.subservice: linux
ms.topic: install-set-up-deploy
ms.custom:
  - linux-related-content
  - build-2025
  - sfi-ropc-blocked
---
# Sample: Unattended SQL Server installation script for SUSE Linux Enterprise Server

[!INCLUDE [SQL Server - Linux](../../includes/applies-to-version/sql-linux.md)]

This sample bash script installs [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] on SUSE Linux Enterprise Server (SLES) without interactive input. It provides examples of installing the [!INCLUDE [ssde-md](../../includes/ssde-md.md)], the SQL Server command-line tools, and SQL Server Agent, and performing post-installation steps. You can optionally install full-text search and create an administrative user.

> [!NOTE]  
> Starting in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)], SUSE Linux Enterprise Server (SLES) isn't supported.

If you don't need an unattended installation script, follow the [quickstart for SLES](quickstart-install-suse.md). For other setup information, see [Installation guidance for SQL Server on Linux](setup.md).

## Prerequisites

- You need at least 2 GB of memory to run SQL Server on Linux.
- The file system must be **XFS** or **ext4**. Other file systems, such as **BTRFS**, aren't supported.
- For other system requirements, see [System requirements for SQL Server on Linux](setup.md#system).

> [!IMPORTANT]  
> [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] requires `libsss_nss_idmap0`, which the default SLES repositories don't provide. You can install this package from the SLES SDK.

## Sample script

This example installs [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] on SLES v15 SP6. If you want to install a different version of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] or SLES, change the Microsoft repository paths accordingly.

Save the sample script to a file and then customize it. Replace the variable values in the script. You can also set any of the scripting variables as environment variables, as long as you remove them from the script file.

> [!IMPORTANT]  
> The `SA_PASSWORD` environment variable is deprecated. Use `MSSQL_SA_PASSWORD` instead.

[!INCLUDE [password-complexity](../includes/password-complexity.md)]

```bash
#!/bin/bash -e

# Use the following variables to control your install:

# Password for the SA user (required)
MSSQL_SA_PASSWORD='<password>'

# Product ID of the version of SQL Server you're installing
# Must be evaluation, developer, express, web, standard, enterprise, or your 25-digit product key
# Defaults to developer
MSSQL_PID='evaluation'

# Enable SQL Server Agent (recommended)
SQL_ENABLE_AGENT='y'

# Install SQL Server Full-Text Search (optional)
# SQL_INSTALL_FULLTEXT='y'

# Create an additional user with sysadmin privileges (optional)
# SQL_INSTALL_USER='<Username>'
# SQL_INSTALL_USER_PASSWORD='<password>'

if [ -z "$MSSQL_SA_PASSWORD" ]
then
  echo Environment variable MSSQL_SA_PASSWORD must be set for unattended install
  exit 1
fi

echo Adding Microsoft repositories...
sudo zypper addrepo -fc https://packages.microsoft.com/config/sles/15/mssql-server-2022.repo
sudo zypper addrepo -fc https://packages.microsoft.com/config/sles/15/prod.repo
sudo zypper --gpg-auto-import-keys refresh

# Add the SLES v15 SP6 SDK to obtain libsss_nss_idmap0
sudo SUSEConnect -p sle-sdk/15.3/x86_64

echo Installing SQL Server...
sudo zypper install -y mssql-server

echo Running mssql-conf setup...
sudo MSSQL_SA_PASSWORD=$MSSQL_SA_PASSWORD \
     MSSQL_PID=$MSSQL_PID \
     /opt/mssql/bin/mssql-conf -n setup accept-eula

echo Installing mssql-tools and unixODBC developer...
sudo ACCEPT_EULA=Y zypper install -y mssql-tools unixODBC-devel

# Add SQL Server tools to the path by default:
echo Adding SQL Server tools to your path...
echo PATH="$PATH:/opt/mssql-tools/bin" >> ~/.bash_profile
echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc
source ~/.bashrc

# Optionally enable SQL Server Agent:
if [ -n "$SQL_ENABLE_AGENT" ]
then
  echo Enable SQL Server Agent...
  sudo /opt/mssql/bin/mssql-conf set sqlagent.enabled true
fi

# Optional SQL Server Full-Text Search installation:
if [ -n "$SQL_INSTALL_FULLTEXT" ]
then
  echo Installing SQL Server full-text search...
    sudo zypper install -y mssql-server-fts
fi

# Configure firewall to allow TCP port 1433:
echo Configuring SuSEfirewall2 to allow traffic on port 1433...
sudo SuSEfirewall2 open INT TCP 1433
sudo SuSEfirewall2 stop
sudo SuSEfirewall2 start

# Example of setting post-installation configuration options
# Set trace flags 1204 and 1222 for deadlock tracing:
# echo Setting trace flags...
# sudo /opt/mssql/bin/mssql-conf trace flag 1204 1222 on

# Restart SQL Server after making configuration changes:
echo Restarting SQL Server...
sudo systemctl restart mssql-server

# Connect to server and get the version:
counter=1
errstatus=1
while [ $counter -le 5 ] && [ $errstatus = 1 ]
do
  echo Waiting for SQL Server to start...
  sleep 5s
  /opt/mssql-tools/bin/sqlcmd \
    -S localhost \
    -U sa \
    -P $MSSQL_SA_PASSWORD \
    -Q "SELECT @@VERSION" 2>/dev/null
  errstatus=$?
  ((counter++))
done

# Display error if connection failed:
if [ $errstatus = 1 ]
then
  echo Cannot connect to SQL Server, installation aborted
  exit $errstatus
fi

# Optional new user creation:
if [ -n "$SQL_INSTALL_USER" ] && [ -n "$SQL_INSTALL_USER_PASSWORD" ]
then
  echo Creating user $SQL_INSTALL_USER
  /opt/mssql-tools/bin/sqlcmd \
    -S localhost \
    -U sa \
    -P $MSSQL_SA_PASSWORD \
    -Q "CREATE LOGIN [$SQL_INSTALL_USER] WITH PASSWORD=N'$SQL_INSTALL_USER_PASSWORD', DEFAULT_DATABASE=[master], CHECK_EXPIRATION=ON, CHECK_POLICY=ON; ALTER SERVER ROLE [sysadmin] ADD MEMBER [$SQL_INSTALL_USER]"
fi

echo Done!
```

## Run the script

To run the script:

1. Paste the sample into your favorite text editor and save it with a memorable name, like `install_sql.sh`.

1. Customize `MSSQL_SA_PASSWORD`, `MSSQL_PID`, and any of the other variables you'd like to change.

1. Mark the script as executable:

   ```bash
   chmod +x install_sql.sh
   ```

1. Run the script.

   ```bash
   ./install_sql.sh
   ```

## Understand the script

The bash script starts by setting a few variables. These variables can be either scripting variables, like the sample, or environment variables. The script requires the `MSSQL_SA_PASSWORD` variable for installing SQL Server. The script uses custom variables for the other variables.

The sample script performs the following steps:

1. Register the Microsoft repositories for SQL Server and the command-line tools.

1. Update the local repositories and import the public Microsoft GPG keys.

1. Install SQL Server.

1. Configure SQL Server with the `MSSQL_SA_PASSWORD` and automatically accept the End-User License Agreement.

1. Automatically accept the End-User License Agreement for the SQL Server command-line tools, install them, and install the `unixODBC-devel` package.

1. Add the SQL Server command-line tools to the path for ease of use.

1. Enable SQL Server Agent if the scripting variable `SQL_ENABLE_AGENT` is set. The sample sets it by default.

1. Optionally install SQL Server Full-Text Search if the variable `SQL_INSTALL_FULLTEXT` is set.

1. Unblock port 1433 for TCP on the system firewall, necessary to connect to SQL Server from another system.

1. Optionally set trace flags for deadlock tracing (requires uncommenting the lines).

1. Restart SQL Server after making configuration changes.

1. Verify that SQL Server is installed correctly, while hiding any error messages.

1. Create a new server administrator user if `SQL_INSTALL_USER` and `SQL_INSTALL_USER_PASSWORD` are both set.

## Unattended install

Simplify multiple unattended installs by creating a stand-alone bash script that sets the proper environment variables. You can remove any of the variables the sample script uses and put them in your own bash script.

```bash
#!/bin/bash
export MSSQL_SA_PASSWORD='<password>'
export MSSQL_PID='evaluation'
export SQL_ENABLE_AGENT='y'
export SQL_INSTALL_USER='<Username>'
export SQL_INSTALL_USER_PASSWORD='<password>'
```

> [!CAUTION]  
> [!INCLUDE [password-complexity](../includes/password-complexity.md)]

Then, run the bash script as follows:

```bash
. ./my_script_name.sh
```

## Related content

- [What is SQL Server on Linux?](../sql-server-linux-overview.md)
