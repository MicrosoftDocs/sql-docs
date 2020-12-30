---
title: Install Python custom runtime
description: Learn how to install a Python custom runtime for SQL Server using Language Extensions. The Python custom runtime can be used for machine learning.
ms.prod: sql
ms.technology: machine-learning-services
ms.date: 11/30/2020
ms.topic: how-to
author: dphansen
ms.author: davidph
ms.custom: seo-lt-2019
zone_pivot_groups: python-custom-runtime-platform
monikerRange: ">=sql-server-ver15||>=sql-server-linux-ver15"
---
# Install a Python custom runtime for SQL Server
[!INCLUDE [SQL Server 2019 and later](../../includes/applies-to-version/sqlserver2019.md)]

This article describes how to install a Python custom runtime for running external Python scripts with SQL Server. The custom runtime uses the [SQL Server Language Extensions](../../language-extensions/language-extensions-overview.md) and can be used for executing machine learning scripts.

The Python custom runtime allows you to use your own version of the Python runtime with SQL Server, instead of the default runtime version installed with [SQL Server Machine Learning Services](../sql-server-machine-learning-services.md).

::: zone pivot="python-custom-runtime-windows"

## Prerequisites

Before installing a Python custom runtime, install the following:

+ Install the [Cumulative Update (CU) 3 or later](../../database-engine/install-windows/latest-updates-for-microsoft-sql-server.md) for SQL Server 2019.

+ Install [Python 3.7](https://www.python.org/downloads/) on the server.

    The Python language extension used for the custom Python runtime currently supports Python 3.7 only. If you would like to use a different version of Python, follow the instruction in the [Python Language Extension GitHub repo](https://github.com/microsoft/sql-server-language-extensions/tree/master/language-extensions/python) to modify and rebuild the extension.

    > [!IMPORTANT]
    > During the installation of Python, check **Add Python 3.7 to PATH**.

## Install Language Extensions

> [!NOTE]
> If you have [Machine Learning Services](../sql-server-machine-learning-services.md) installed on SQL Server 2019, Language Extensions is already installed and you can skip this step.

Follow the steps below to install [SQL Server Language Extensions](../../language-extensions/language-extensions-overview.md), which is used for the Python custom runtime.

1. Start the setup wizard for SQL Server 2019.
  
1. On the **Installation** tab, select **New SQL Server stand-alone installation or add features to an existing installation**.

1. On the **Feature Selection** page, select these options:
  
    + **Database Engine Services**
  
        To use Language Extensions with SQL Server, you must install an instance of the database engine. You can use either a new or an existing instance.
  
    + **Machine Learning Services and Language Extensions**

        Select **Machine Learning Services and Language Extensions**. Do not select Python, as you will be installing the custom Python runtime later.

        :::image type="content" source="media/2019-setup-language-extensions.png" alt-text="SQL Server 2019 Language Extensions setup.":::

1. On the **Ready to Install** page, verify that these selections are included, and select **Install**.
  
    + Database Engine Services
    + Machine Learning Services and Language Extensions

1. After the setup is complete, restart the machine if you're asked to do so.

> [!IMPORTANT]
> If you install a new instance of SQL Server 2019 with Language Extensions, then install the [Cumulative Update (CU) 3 or later](../../database-engine/install-windows/latest-updates-for-microsoft-sql-server.md) before you continue to the next step.

## Install pandas

Install the [pandas](https://pandas.pydata.org/) package for Python from an *elevated* command prompt:

```bash
python.exe -m pip install pandas
```

## Add environment variable

Add or modify the system environment variable **PYTHONHOME**.

1. In the Windows search box, type *environment* and select **Edit the system environment variables**.
1. In the **Advanced** tab, select **Environment Variables**.
1. Under **System variables**, select **New** to create **PYTHONHOME** to point to your Python 3.7 installation location. If PYTHONHOME already exists, select **Edit** to point it to the Python 3.7 installation location.
1. Select **OK** to close all the windows.

    :::image type="content" source="media/pythonhome-env-variable.png" alt-text="PYTHONHOME environment variable.":::

## Grant access to Python folder

Run the following **icacls** commands from a new *elevated* command prompt to grant **READ & EXECUTE** access to **PYTHONHOME** to **SQL Server Launchpad Service** and SID **S-1-15-2-1** (**ALL_APPLICATION_PACKAGES**).

1. Give permissions to **SQL Server Launchpad Service user name**.

    ```cmd
    icacls "%PYTHONHOME%" /grant "NT Service\MSSQLLAUNCHPAD":(OI)(CI)RX /T
    ```

    For named instance, the command will be `icacls "%PYTHONHOME%" /grant "NT Service\MSSQLLAUNCHPAD$SQL01":(OI)(CI)RX /T` for an instance called **SQL01**.

2. Give permissions to **SID S-1-15-2-1**.

    ```cmd
    icacls "%PYTHONHOME%" /grant *S-1-15-2-1:(OI)(CI)RX /T
    ```

    The preceding command grants permissions to the computer **SID S-1-15-2-1**, which is equivalent to **ALL APPLICATION PACKAGES** on an English version of Windows. Alternatively, you can use `icacls "%R_HOME%" /grant "ALL APPLICATION PACKAGES":(OI)(CI)RX /T` on an English version of Windows.

## Restart SQL Server Launchpad

Follow these steps to restart the SQL Server Launchpad service.

1. Open [SQL Server Configuration Manager](../../relational-databases/sql-server-configuration-manager.md).

1. Under **SQL Server Services**, right click on **SQL Server Launchpad (MSSQLSERVER)** and select **Restart**. If you using a named instance, the instance name will be shown instead of **(MSSQLSERVER)**.

## Register language extension

Follow these steps to download and register the Python language extension, which is used for the Python custom runtime.

1. Download the **python-lang-extension-windows.zip** file from the [SQL Server Language Extensions GitHub repo](https://github.com/microsoft/sql-server-language-extensions/releases).

    Alternatively, you can use the debug version (**python-lang-extension-windows-debug.zip**) in a development or test environment. The debug version provides verbose logging information to investigate any errors, and is not recommended for production environments.

1. Use [Azure Data Studio](../../azure-data-studio/what-is-azure-data-studio.md) to connect to your SQL Server instance and run the following T-SQL command to register the Python language extension with [CREATE EXTERNAL LANGUAGE](../../t-sql/statements/create-external-language-transact-sql.md). 

    Modify the path in this statement to reflect the location of the downloaded language extension zip file (**python-lang-extension-windows.zip**).

    ```sql
    CREATE EXTERNAL LANGUAGE [myPython]
    FROM (CONTENT = N'/path/to/python-lang-extension-windows.zip', FILE_NAME = 'pythonextension.dll');
    GO
    ```

    Execute the statement for each database you want to use the Python language extension in.

    > [!NOTE]
    > **Python** is a reserved word and can't be used as the name for a new external language name. Use a different name instead. For example, the statement above uses **myPython**.

::: zone-end

::: zone pivot="python-custom-runtime-linux"

## Prerequisites

Before installing a Python custom runtime, install the following:

+ Install SQL Server 2019 for Linux. You can install SQL Server on Red Hat Enterprise Linux (RHEL), SUSE Linux Enterprise Server (SLES), and Ubuntu. For more information, see [the Installation guidance for SQL Server on Linux](../../linux/sql-server-linux-setup.md).

+ Upgrade to Cumulative Update (CU) 3 or later for SQL Server 2019. Follow these steps:
    1. Configure the repositories for Cumulative Updates. For more information, see [Configure repositories for installing and upgrading SQL Server on Linux](../../linux/sql-server-linux-change-repo.md).

    1. Update the **mssql-server** package to the latest Cumulative Update. For more information, see [the Update or Upgrade SQL Server section in the installation guidance for SQL Server on Linux](../../linux/sql-server-linux-setup.md#upgrade).

+ Install [Python 3.7](https://www.python.org/downloads/) on the server.

    The Python language extension used for the custom Python runtime currently supports Python 3.7 only. If you would like to use a different version of Python, follow the instruction in the [Python Language Extension GitHub repo](https://github.com/microsoft/sql-server-language-extensions/tree/master/language-extensions/python) to modify and rebuild the extension.

## Install Language Extensions

> [!NOTE]
> If you have [Machine Learning Services](../sql-server-machine-learning-services.md) installed on SQL Server 2019, the **mssql-server-extensibility** package for Language Extensions is already installed and you can skip this step.

Follow the commands below to install [SQL Server Language Extensions](../../language-extensions/language-extensions-overview.md) on Linux, which is used for the Python custom runtime.

#### [Ubuntu](#tab/ubuntu)

1. If possible, run this command to refresh the packages on the system prior to the installation.

    ```bash
    # Install as root or sudo
    sudo apt-get update
    ```

1. Ubuntu might not have the https apt transport option. To install it, run this command.

    ```bash
    # Install as root or sudo
    apt-get install apt-transport-https
    ```

1. Install **mssql-server-extensibility** with this command.

    ```bash
    # Install as root or sudo
    sudo apt-get install mssql-server-extensibility
    ```

#### [Red Hat Enterprise Linux (RHEL)](#tab/rhel)

```bash
# Install as root or sudo
sudo yum install mssql-server-extensibility
```

#### [SUSE Linux Enterprise Server (SLES)](#tab/sles)

```bash
# Install as root or sudo
sudo zypper install mssql-server-extensibility
```

---

## Install Python 3.7 and pandas

Install Python 3.7, the libpython3.7 library, and the pandas package. 

The following are example commands for Ubuntu:

```bash
# Install python3.7 and the corresponding library:
sudo add-apt-repository ppa:deadsnakes/ppa
sudo apt-get update
sudo apt-get install python3.7 python3-pip libpython3.7

# Install pandas to /usr/lib:
sudo python3.7 -m pip install pandas -t /usr/lib/python3.7/dist-packages
```

## Custom installation of Python

> [!NOTE]
> If you have installed Python 3.7 in the default location of `/usr/lib/python3.7`, you can skip this section and move on to the [Register language extension](#register-language-extension-linux) section.

If you built your own version of Python 3.7, use the following commands to let SQL Server know your custom installation.

### Add environment variable

First, edit the **mssql-launchpadd** service to add the **PYTHONHOME** environment variable to the file `/etc/systemd/system/mssql-launchpadd.service.d/override.conf`

1. Open the file with systemctl

    ```bash
    sudo systemctl edit mssql-launchpadd
    ```

1. Insert the following text in the `/etc/systemd/system/mssql-launchpadd.service.d/override.conf` file that opens. Set value of **PYTHONHOME** to the custom Python installation path.

    ```
    [Service]
    Environment="PYTHONHOME=/path/to/installation/of/python3.7"
    ```

1. Save the file and close the editor.

Next, make sure `libpython3.7m.so.1.0` can be loaded.

1. Create a custom-python.conf file in `/etc/ld.so.conf.d`.

    ```bash
    sudo vi /etc/ld.so.conf.d/custom-python.conf
    ```

1. In the file that opens, add the path to **libpython3.7m.so.1.0** from the custom Python installation.

    ```
    /path/to/installation/of/python3.7/lib
    ```

1. Save the new file and close the editor.

1. Run `ldconfig` and verify `libpython3.7m.so.1.0` can be loaded by running the following commands and checking that all the dependent libraries can be found.

    ```bash
    sudo ldconfig
    ldd /path/to/installation/of/python3.7/lib/libpython3.7m.so.1.0
    ```

### Grant access to Python folder

Set the `datadirectories` option in the extensibility section of /var/opt/mssql/mssql.conf file to the custom python installation.

```bash
sudo /opt/mssql/bin/mssql-conf set extensibility.datadirectories /path/to/installation/of/python3.7
```

### Restart mssql-launchpadd

```bash
sudo systemctl restart mssql-launchpadd
```

<a name="register-language-extension-linux"></a>

## Register language extension

Follow these steps to download and register the Python language extension, which is used for the Python custom runtime.

1. Download the **python-lang-extension-linux.zip** file from the [SQL Server Language Extensions GitHub repo](https://github.com/microsoft/sql-server-language-extensions/releases).

    Alternatively, you can use the debug version (**python-lang-extension-linux-debug.zip**) in a development or test environment. The debug version provides verbose logging information to investigate any errors, and is not recommended for production environments.

1. Use [Azure Data Studio](../../azure-data-studio/what-is-azure-data-studio.md) to connect to your SQL Server instance and run the following T-SQL command to register the Python language extension with [CREATE EXTERNAL LANGUAGE](../../t-sql/statements/create-external-language-transact-sql.md). 

    Modify the path in this statement to reflect the location of the downloaded language extension zip file (**python-lang-extension-linux.zip**).

    ```sql
    CREATE EXTERNAL LANGUAGE [myPython]
    FROM (CONTENT = N'/path/to/python-lang-extension-linux.zip', FILE_NAME = 'libPythonExtension.so.1.0');
    GO
    ```

    Execute the statement for each database you want to use the Python language extension in.

    > [!NOTE]
    > **Python** is a reserved word and can't be used as the name for a new external language name. Use a different name instead. For example, the statement above uses **myPython**.

::: zone-end

## Enable external script

You can execute a Python external script with the stored procedure [sp_execute_external script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md).

To enable external scripts, use [Azure Data Studio](../../azure-data-studio/what-is-azure-data-studio.md) to execute the statement below.

```sql
sp_configure 'external scripts enabled', 1;
RECONFIGURE WITH OVERRIDE;  
```

## Verify installation

Use the following SQL script to verify the installation and functionality of the Python custom runtime.

```sql
EXEC sp_execute_external_script
@language =N'myPython',
@script=N'
import sys
print(sys.path)
print(sys.version)
print(sys.executable)'
```

## Next steps

+ [Install an R custom runtime for SQL Server](custom-runtime-r.md)
+ [Extensibility framework in SQL Server](../concepts/extensibility-framework.md)
+ [Language Extensions Overview](../../language-extensions/language-extensions-overview.md)
