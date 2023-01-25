---
ms.service: sql
ms.subservice: machine-learning-services
ms.date: 02/08/2021
ms.topic: include
author: WilliamDAssafMSFT
ms.author: wiassaf
---
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
    Environment="PYTHONHOME=<path to the python3.7 lib>"
    ```

1. Save the file and close the editor.

Next, make sure `libpython3.7m.so.1.0` can be loaded.

1. Create a custom-python.conf file in `/etc/ld.so.conf.d`.

    ```bash
    sudo vi /etc/ld.so.conf.d/custom-python.conf
    ```

1. In the file that opens, add the path to **libpython3.7m.so.1.0** from the custom Python installation.

    ```
    <path to the python3.7 lib>
    ```

1. Save the new file and close the editor.

1. Run `ldconfig` and verify `libpython3.7m.so.1.0` can be loaded by running the following commands and checking that all the dependent libraries can be found.

    ```bash
    sudo ldconfig
    ldd <path to the python3.7 lib>/libpython3.7m.so.1.0
    ```

### Grant access to Python folder

Set the `datadirectories` option in the extensibility section of `/var/opt/mssql/mssql.conf` file to the custom python installation.

```bash
sudo /opt/mssql/bin/mssql-conf set extensibility.datadirectories <path to python3.7>
```

### Restart mssql-launchpadd

Run the following command to restart **mssql-launchpadd**.

```bash
sudo systemctl restart mssql-launchpadd
```

<a name="register-language-extension-linux"></a>

## Register language extension

Follow these steps to download and register the Python language extension, which is used for the Python custom runtime.

1. Download the **python-lang-extension-linux-release.zip** file from the [SQL Server Language Extensions GitHub repo](https://github.com/microsoft/sql-server-language-extensions/releases).

    Alternatively, you can use the debug version (**python-lang-extension-linux-debug.zip**) in a development or test environment. The debug version provides verbose logging information to investigate any errors, and is not recommended for production environments.

1. Use [Azure Data Studio](../../../azure-data-studio/what-is-azure-data-studio.md) to connect to your SQL Server instance and run the following T-SQL command to register the Python language extension with [CREATE EXTERNAL LANGUAGE](../../../t-sql/statements/create-external-language-transact-sql.md). 

    Modify the path in this statement to reflect the location of the downloaded language extension zip file (**python-lang-extension-linux-release.zip**).

    ```sql
    CREATE EXTERNAL LANGUAGE [myPython]
    FROM (CONTENT = N'/path/to/python-lang-extension-linux-release.zip', FILE_NAME = 'libPythonExtension.so.1.1');
    GO
    ```

    Execute the statement for each database you want to use the Python language extension in.

    > [!NOTE]
    > **Python** is a reserved word and can't be used as the name for a new external language name. Use a different name instead. For example, the statement above uses **myPython**.
