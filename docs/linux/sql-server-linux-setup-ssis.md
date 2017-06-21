# Installing SQL Server Integration Services on Linux


The following steps install SQL Server Integration Services (mssql-server-is) on Linux. The information on the features supported for this release of the Integration Services, see the [Release Notes](sql-server-linux-release-notes.md).


Install the SQL Server Integration Services for your platform:

- [Ubuntu](#ubuntu)


## <a name="ubuntu">Install on Ubuntu</a>
To install the mssql-server-is Package on Ubuntu, follow these steps:


1.    Import the public repository GPG keys.

    ```bash
    curl httpspackages.microsoft.comkeysmicrosoft.asc  sudo apt-key add –
    ```


2.    Register the Microsoft SQL Server Ubuntu repository.

    ```bash
    curl httpspackages.microsoft.comconfigubuntu16.04mssql-server.list sudo tee etcaptsources.list.dmssql-server.list
    ```


3.    Run the following commands to install SQL Server Integration Services.

    ```bash
    sudo apt-get update
    sudo apt-get install -y mssql-server-is
    ```


4.    After installation, please run ssis-conf.

    ```bash
    sudo optssisbinssis-conf
    ```


5.    Once the configuration is done, set path.

    ```bash
    export PATH=optssisbin$PATH
    ```


6.    If your user is not in SSIS group, please add current user to SSIS group. 

    ```bash
    sudo gpasswd -a “current user” ssis
    ```

      Use “ID” command to make sure current user is in SSIS group.

    ```bash
    id
    ```


7.    Copy your SSIS package to your Linux machine and run.

    ```bash
    dtexec F “your package” DE “protection password”
    ```


      If you already have mssql-server-is installed, you can update to the latest version with the following command:

    ```bash
    sudo apt-get install mssql-server-is
    ```


## Next steps

For more information on how to use SSIS on Linux to extract, transform and load data for SQL Server on Linux, see [Extract, transform, and load data for SQL Server on Linux with SSIS](sql-server-linux-migrate-ssis.md).