---
ms.service: sql
ms.subservice: machine-learning-services
ms.date: 02/08/2021
ms.topic: include
author: WilliamDAssafMSFT
ms.author: wiassaf
---
## Prerequisites

Before installing a Python custom runtime, install:

+ If you use an existing SQL Server instance, install [Cumulative Update (CU) 3 or later](../../../database-engine/install-windows/latest-updates-for-microsoft-sql-server.md) for SQL Server 2019.

## Install Language Extensions

> [!NOTE]
> If you have [Machine Learning Services](../../sql-server-machine-learning-services.md) installed on SQL Server 2019, Language Extensions is already installed and you can skip this step.

Follow the steps below to install [SQL Server Language Extensions](../../../language-extensions/language-extensions-overview.md), which is used for the Python custom runtime.

1. Start the setup wizard for SQL Server 2019.
  
1. On the **Installation** tab, select **New SQL Server stand-alone installation or add features to an existing installation**.

1. On the **Feature Selection** page, select these options:
  
    + **Database Engine Services**
  
        To use Language Extensions with SQL Server, you must install an instance of the database engine. You can use either a new or an existing instance.
  
    + **Machine Learning Services and Language Extensions**

        Select **Machine Learning Services and Language Extensions**. Do not select Python, as you will be installing the custom Python runtime later.

        :::image type="content" source="../media/2019-setup-language-extensions.png" alt-text="SQL Server 2019 Language Extensions setup.":::

1. On the **Ready to Install** page, verify that these selections are included, and select **Install**.
  
    + Database Engine Services
    + Machine Learning Services and Language Extensions

1. After the setup is complete, restart the machine if you're asked to do so.

> [!IMPORTANT]
> If you install a new instance of SQL Server 2019 with Language Extensions, then install the [Cumulative Update (CU) 3 or later](../../../database-engine/install-windows/latest-updates-for-microsoft-sql-server.md) before you continue to the next step.

## Install Python

The Python language extension used for the custom Python runtime currently supports Python 3.7 only. If you would like to use a different version of Python, follow the instruction in the [Python Language Extension GitHub repo](https://github.com/microsoft/sql-server-language-extensions/tree/master/language-extensions/python) to modify and rebuild the extension.

1. Download [Python 3.7](https://www.python.org/downloads/windows/) for Windows and run the Setup on the server.

1. Select **Add Python 3.7 to PATH** and then select **Customize installation**.

    :::image type="content" source="../media/python-install-add-to-path.png" alt-text="Python 3.7 installation - Add Python 3.7 to PATH":::

1. Under **Optional Features**, leave the defaults and select **Next**.

1. Select **Install for all users** and take note of the installation location.

    :::image type="content" source="../media/python-install-for-all-users.png" alt-text="Python 3.7 installation - Install for all users":::

1. Select **Install**.

## Install pandas

Install the [pandas](https://pandas.pydata.org/) package for Python from an *elevated* command prompt (Run as Administrator):

```bash
python.exe -m pip install pandas
```

## Grant access to Python folder

Run the following **icacls** commands from a new *elevated* command prompt to grant **READ & EXECUTE** access to the Python installation location to **SQL Server Launchpad Service** and SID **S-1-15-2-1** (**ALL_APPLICATION_PACKAGES**).

The examples below use the Python installation location as `C:\Program Files\Python37`. If your location is different, change it in the command.

1. Give permissions to **SQL Server Launchpad Service user name**.

    ```cmd
    icacls "C:\Program Files\Python37" /grant "NT Service\MSSQLLAUNCHPAD":(OI)(CI)RX /T
    ```

    For named instance, the command will be `icacls "C:\Program Files\Python37" /grant "NT Service\MSSQLLAUNCHPAD$SQL01":(OI)(CI)RX /T` for an instance called **SQL01**.

2. Give permissions to **SID S-1-15-2-1**.

    ```cmd
    icacls "C:\Program Files\Python37" /grant *S-1-15-2-1:(OI)(CI)RX /T
    ```

    The preceding command grants permissions to the computer **SID S-1-15-2-1**, which is equivalent to **ALL APPLICATION PACKAGES** on an English version of Windows. Alternatively, you can use `icacls "C:\Program Files\Python37" /grant "ALL APPLICATION PACKAGES":(OI)(CI)RX /T` on an English version of Windows.

## Restart SQL Server Launchpad

Follow these steps to restart the SQL Server Launchpad service.

1. Open [SQL Server Configuration Manager](../../../relational-databases/sql-server-configuration-manager.md).

1. Under **SQL Server Services**, right click on **SQL Server Launchpad (MSSQLSERVER)** and select **Restart**. If you are using a named instance, the instance name will be shown instead of **(MSSQLSERVER)**.

## Register language extension

Follow these steps to download and register the Python language extension, which is used for the Python custom runtime.

1. Download the **python-lang-extension-windows-release.zip** file from the [SQL Server Language Extensions GitHub repo](https://github.com/microsoft/sql-server-language-extensions/releases).

    Alternatively, you can use the debug version (**python-lang-extension-windows-debug.zip**) in a development or test environment. The debug version provides verbose logging information to investigate any errors, and is not recommended for production environments.

1. Use [Azure Data Studio](../../../azure-data-studio/what-is-azure-data-studio.md) to connect to your SQL Server instance and run the following T-SQL command to register the Python language extension with [CREATE EXTERNAL LANGUAGE](../../../t-sql/statements/create-external-language-transact-sql.md).

    Modify the path in this statement to reflect the location of the downloaded language extension zip file (**python-lang-extension-windows-release.zip**) and the location your Python installation (`C:\\Program Files\\Python37`).

    ```sql
    CREATE EXTERNAL LANGUAGE [myPython]
    FROM (CONTENT = N'C:\path\to\python-lang-extension-windows-release.zip', 
        FILE_NAME = 'pythonextension.dll', 
        ENVIRONMENT_VARIABLES = N'{"PYTHONHOME": "C:\\Program Files\\Python37"}');
    GO
    ```

    Execute the statement for each database you want to use the Python language extension in.

    > [!NOTE]
    > **Python** is a reserved word and can't be used as the name for a new external language name. Use a different name instead. For example, the statement above uses **myPython**.
