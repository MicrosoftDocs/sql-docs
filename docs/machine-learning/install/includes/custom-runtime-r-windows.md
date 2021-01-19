---
ms.prod: sql
ms.technology: machine-learning-services
ms.date: 01/14/2021
ms.topic: include
author: dphansen
ms.author: davidph
---

## Prerequisites

Before installing an R custom runtime, install the following:

+ Install the [Cumulative Update (CU) 3 or later](../../database-engine/install-windows/latest-updates-for-microsoft-sql-server.md) for SQL Server 2019.

+ Install [R version 3.3 or later](https://cran.r-project.org/bin/windows/base/) for Windows on the server.

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

## Install R

Download and install the version of R you will use as the custom runtime. R version 3.3 or later are supported.

1. Download [R version 3.3 or later](https://cran.r-project.org/bin/windows/base/).

1. Run the R setup.

1. Note the path where R is installed. This path is your **R_HOME**. For example, **R_HOME** below is `C:\Program Files\R\R-4.0.3`.

    :::image type="content" source="../media/r-setup-path.png" alt-text="R setup":::

## Update system environment variables

Follow the steps below to add or modify the **R_HOME** and modify the **PATH** system environment variables.

1. In the Windows search box, search for **Edit the system environment variables** and open it.

1. Under **Advanced**, select **Environment Variables**.

1. Create or modify the **R_HOME** system environment variable.

    In **System variables**, select **New** to create the **R_HOME** system environment variable. If you already have an **R_HOME** system environment variable, select **Edit** to modify it instead.

    Under **Variable name** type `R_HOME` under. Under **Variable value** type the path for your R installation. For example, `C:\Program Files\R\R-4.0.3`.

    :::image type="content" source="../media/r_home_environment_variable.png" alt-text="R_HOME environment variable":::

    Click **OK**.

1. Modify the **PATH** system environment variable.

    Select **PATH** and click **Edit**.

    Select **New** and add `%R_HOME%\bin\x64`.

    :::image type="content" source="../media/r_path_environment_variable.png" alt-text="R_HOME environment variable":::

1. Add or modify **R_HOME** as a system environment variable.
    + In the Windows search box, type "environment" and select **Edit the system environment variables**.
    + In the **Advanced** tab, select **Environment Variables**.

    + Under **System variables**, select **New** to create R_HOME.
    To modify, select **Edit** to change it. Modify its value to point to the custom R installation path.

    ![Create R_HOME system environment variable.](../install/media/sys-env-r-home.png)

2. Update the **PATH** environment variable.
    Append the path to **R.dll** to the system **PATH** environment variable. Select **PATH** then **Edit** and add `%R_HOME%\bin\x64` to the list of paths.

    ![Append to PATH system environment variable.](../install/media/sys-env-path-r.png)

3. Select **OK** to close remaining windows.

    As an alternative, to set these environment variables from an *elevated* command prompt, run the following commands. Make sure to use the custom R installation path.

```CMD
setx /m R_HOME "path\to\installation\of\R"
setx /m PATH "path\to\installation\of\R\bin\x64;%PATH%"
```


    In the following instructions, **%R_HOME%** is the environment variable with the value to the path of your R installation and should be replaced with this path.


## Install Rcpp package

Follow these steps to install the **Rcpp** package.

1. Locate the R executable in %R_HOME%\bin. For example, `C:\Program Files\R\R-4.0.3\bin`.

1. Start R from an *elevated* command prompt:

    ```CMD
    %R_HOME%\bin\R.exe
    ```

1. In this *elevated* R prompt (%R_HOME%\bin\R.exe), run the following script to install the Rcpp package in the %R_HOME%\library folder.

    ```R
    install.packages("Rcpp", lib="%R_HOME%/library");
    ```

## Update the system environment variables

1. Add or modify **R_HOME** as a system environment variable.
    + In the Windows search box, type "environment" and select **Edit the system environment variables**.
    + In the **Advanced** tab, select **Environment Variables**.

    + Under **System variables**, select **New** to create R_HOME.
    To modify, select **Edit** to change it. Modify its value to point to the custom R installation path.

    ![Create R_HOME system environment variable.](../install/media/sys-env-r-home.png)

2. Update the **PATH** environment variable.
    Append the path to **R.dll** to the system **PATH** environment variable. Select **PATH** then **Edit** and add `%R_HOME%\bin\x64` to the list of paths.

    ![Append to PATH system environment variable.](../install/media/sys-env-path-r.png)

3. Select **OK** to close remaining windows.

    As an alternative, to set these environment variables from an *elevated* command prompt, run the following commands. Make sure to use the custom R installation path.

```CMD
setx /m R_HOME "path\to\installation\of\R"
setx /m PATH "path\to\installation\of\R\bin\x64;%PATH%"
```









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

    :::image type="content" source="../media/pythonhome-env-variable.png" alt-text="PYTHONHOME environment variable.":::

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

1. Open [SQL Server Configuration Manager](../../../relational-databases/sql-server-configuration-manager.md).

1. Under **SQL Server Services**, right click on **SQL Server Launchpad (MSSQLSERVER)** and select **Restart**. If you using a named instance, the instance name will be shown instead of **(MSSQLSERVER)**.

## Register language extension

Follow these steps to download and register the Python language extension, which is used for the Python custom runtime.

1. Download the **python-lang-extension-windows.zip** file from the [SQL Server Language Extensions GitHub repo](https://github.com/microsoft/sql-server-language-extensions/releases).

    Alternatively, you can use the debug version (**python-lang-extension-windows-debug.zip**) in a development or test environment. The debug version provides verbose logging information to investigate any errors, and is not recommended for production environments.

1. Use [Azure Data Studio](../../../azure-data-studio/what-is-azure-data-studio.md) to connect to your SQL Server instance and run the following T-SQL command to register the Python language extension with [CREATE EXTERNAL LANGUAGE](../../../t-sql/statements/create-external-language-transact-sql.md). 

    Modify the path in this statement to reflect the location of the downloaded language extension zip file (**python-lang-extension-windows.zip**).

    ```sql
    CREATE EXTERNAL LANGUAGE [myPython]
    FROM (CONTENT = N'/path/to/python-lang-extension-windows.zip', FILE_NAME = 'pythonextension.dll');
    GO
    ```

    Execute the statement for each database you want to use the Python language extension in.

    > [!NOTE]
    > **Python** is a reserved word and can't be used as the name for a new external language name. Use a different name instead. For example, the statement above uses **myPython**.
