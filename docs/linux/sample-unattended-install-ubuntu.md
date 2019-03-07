---
title: Unattended install for SQL Server on Ubuntu
titleSuffix: SQL Server
description: SQL Server Script Sample - Unattended Install on Ubuntu
author: rothja
ms.author: jroth
manager: craigg
ms.date: 10/02/2017
ms.topic: conceptual
ms.prod: sql
ms.custom: "sql-linux, seodec18"
ms.technology: linux
---
# Sample: Unattended SQL Server installation script for Ubuntu

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-linuxonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-linuxonly.md)]

This sample Bash script installs SQL Server 2017 on Ubuntu 16.04 without interactive input. It provides examples of installing the database engine, the SQL Server command-line tools, SQL Server Agent, and performs post-install steps. You can optionally install full-text search and create an administrative user.

> [!TIP]
> If you do not need an unattended installation script, the fastest way to install SQL Server is to follow the [quickstart for Ubuntu](quickstart-install-connect-ubuntu.md). For other setup information, see [Installation guidance for SQL Server on Linux](sql-server-linux-setup.md).

## Prerequisites

- You need at least 2 GB of memory to run SQL Server on Linux.
- The file system must be **XFS** or **EXT4**. Other file systems, such as **BTRFS**, are unsupported.
- For other system requirements, see [System requirements for SQL Server on Linux](sql-server-linux-setup.md#system).

## Sample script

```bash
#!/bin/bash -e

# Use the following variables to control your install:

# Password for the SA user (required)
MSSQL_SA_PASSWORD='<YourStrong!Passw0rd>'

# Product ID of the version of SQL server you're installing
# Must be evaluation, developer, express, web, standard, enterprise, or your 25 digit product key
# Defaults to developer
MSSQL_PID='evaluation'

# Install SQL Server Agent (recommended)
SQL_INSTALL_AGENT='y'

# Install SQL Server Full Text Search (optional)
# SQL_INSTALL_FULLTEXT='y'

# Create an additional user with sysadmin privileges (optional)
# SQL_INSTALL_USER='<Username>'
# SQL_INSTALL_USER_PASSWORD='<YourStrong!Passw0rd>'

if [ -z $MSSQL_SA_PASSWORD ]
then
  echo Environment variable MSSQL_SA_PASSWORD must be set for unattended install
  exit 1
fi

echo Adding Microsoft repositories...
sudo curl https://packages.microsoft.com/keys/microsoft.asc | sudo apt-key add -
repoargs="$(curl https://packages.microsoft.com/config/ubuntu/16.04/mssql-server-2017.list)"
sudo add-apt-repository "${repoargs}"
repoargs="$(curl https://packages.microsoft.com/config/ubuntu/16.04/prod.list)"
sudo add-apt-repository "${repoargs}"

echo Running apt-get update -y...
sudo apt-get update -y

echo Installing SQL Server...
sudo apt-get install -y mssql-server

echo Running mssql-conf setup...
sudo MSSQL_SA_PASSWORD=$MSSQL_SA_PASSWORD \
     MSSQL_PID=$MSSQL_PID \
     /opt/mssql/bin/mssql-conf -n setup accept-eula

echo Installing mssql-tools and unixODBC developer...
sudo ACCEPT_EULA=Y apt-get install -y mssql-tools unixodbc-dev

# Add SQL Server tools to the path by default:
echo Adding SQL Server tools to your path...
echo PATH="$PATH:/opt/mssql-tools/bin" >> ~/.bash_profile
echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc
source ~/.bashrc

# Optional SQL Server Agent installation:
if [ ! -z $SQL_INSTALL_AGENT ]
then
  echo Installing SQL Server Agent...
  sudo apt-get install -y mssql-server-agent
fi

# Optional SQL Server Full Text Search installation:
if [ ! -z $SQL_INSTALL_FULLTEXT ]
then
    echo Installing SQL Server Full-Text Search...
    sudo apt-get install -y mssql-server-fts
fi

# Configure firewall to allow TCP port 1433:
echo Configuring UFW to allow traffic on port 1433...
sudo ufw allow 1433/tcp
sudo ufw reload

# Optional example of post-installation configuration.
# Trace flags 1204 and 1222 are for deadlock tracing.
# echo Setting trace flags...
# sudo /opt/mssql/bin/mssql-conf traceflag 1204 1222 on

# Restart SQL Server after installing:
echo Restarting SQL Server...
sudo systemctl restart mssql-server

# Connect to server and get the version:
counter=1
errstatus=1
while [ $counter -le 5 ] && [ $errstatus = 1 ]
do
  echo Waiting for SQL Server to start...
  sleep 3s
  /opt/mssql-tools/bin/sqlcmd \
    -S localhost \
    -U SA \
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
if [ ! -z $SQL_INSTALL_USER ] && [ ! -z $SQL_INSTALL_USER_PASSWORD ]
then
  echo Creating user $SQL_INSTALL_USER
  /opt/mssql-tools/bin/sqlcmd \
    -S localhost \
    -U SA \
    -P $MSSQL_SA_PASSWORD \
    -Q "CREATE LOGIN [$SQL_INSTALL_USER] WITH PASSWORD=N'$SQL_INSTALL_USER_PASSWORD', DEFAULT_DATABASE=[master], CHECK_EXPIRATION=ON, CHECK_POLICY=ON; ALTER SERVER ROLE [sysadmin] ADD MEMBER [$SQL_INSTALL_USER]"
fi

echo Done!
```

### Running the script

To run the script

1. Paste the sample into your favorite text editor and save it with a memorable name, like `install_sql.sh`.

1. Customize `MSSQL_SA_PASSWORD`, `MSSQL_PID`, and any of the other variables you'd like to change.

1. Mark the script as executable

   ```bash
   chmod +x install_sql.sh
   ```

1. Run the script

   ```bash
   ./install_sql.sh
   ```

### Understanding the script
The first thing the Bash script does is set a few variables. These can be either scripting variables, like the sample, or environment variables. The variable ``` MSSQL_SA_PASSWORD ``` is **required** by SQL Server installation, the others are custom variables created for the script. The sample script performs the following steps:

1. Import the public Microsoft GPG keys.

1. Register the Microsoft repositories for SQL Server and the command-line tools.

1. Update the local repositories

1. Install SQL Server

1. Configure SQL Server with the ```MSSQL_SA_PASSWORD``` and automatically accept the End-User License Agreement.

1. Automatically accept the End-User License Agreement for the SQL Server command-line tools, install them, and install the unixodbc-dev package.

1. Add the SQL Server command-line tools to the path for ease of use.

1. Install the SQL Server Agent if the scripting variable ```SQL_INSTALL_AGENT``` is set, on by default.

1. Optionally install SQL Server Full-Text search, if the variable ```SQL_INSTALL_FULLTEXT``` is set.

1. Unblock port 1433 for TCP on the system firewall, necessary to connect to SQL Server from another system.

1. Optionally set trace flags for deadlock tracing. (requires uncommenting the lines)

1. SQL Server is now installed, to make it operational, restart the process.

1. Verify that SQL Server is installed correctly, while hiding any error messages.

1. Create a new server administrator user if ```SQL_INSTALL_USER``` and ```SQL_INSTALL_USER_PASSWORD``` are both set.

## Next steps

Simplify multiple unattended installs and create a stand-alone Bash script that sets the proper environment variables. You can remove any of the variables the sample script uses and put them in their own Bash script.

```bash
#!/bin/bash
export MSSQL_SA_PASSWORD='<YourStrong!Passw0rd>'
export MSSQL_PID='evaluation'
export SQL_INSTALL_AGENT='y'
export SQL_INSTALL_USER='<Username>'
export SQL_INSTALL_USER_PASSWORD='<YourStrong!Passw0rd>'
export SQL_INSTALL_AGENT='y'
```

Then run the Bash script as follows:
```bash
. ./my_script_name.sh
```

For more information about SQL Server on Linux, see [SQL Server on Linux overview](sql-server-linux-overview.md).
