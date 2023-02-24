---
ms.service: sql
ms.subservice: machine-learning-services
ms.date: 04/07/2021
ms.topic: include
author: WilliamDAssafMSFT
ms.author: wiassaf
---

## Prerequisites

Before installing an R custom runtime, install the following:

+ If you use an existing SQL Server instance, install [Cumulative Update (CU) 3 or later](../../../database-engine/install-windows/latest-updates-for-microsoft-sql-server.md) for SQL Server 2019.

## Install Language Extensions

> [!NOTE]
> If you have [Machine Learning Services](../../sql-server-machine-learning-services.md) installed on SQL Server 2019, Language Extensions is already installed and you can skip this step.

Follow the steps below to install [SQL Server Language Extensions](../../../language-extensions/language-extensions-overview.md), which is used for the R custom runtime.

1. Start the setup wizard for SQL Server 2019.
  
1. On the **Installation** tab, select **New SQL Server stand-alone installation or add features to an existing installation**.

1. On the **Feature Selection** page, select these options:
  
    + **Database Engine Services**
  
        To use Language Extensions with SQL Server, you must install an instance of the database engine. You can use either a new or an existing instance.
  
    + **Machine Learning Services and Language Extensions**

        Select **Machine Learning Services and Language Extensions**. Do not select R, as you will be installing the custom R runtime later.

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

1. Note the path where R is installed. For example, in this article it is `C:\Program Files\R\R-4.0.3`.

    :::image type="content" source="../media/r-setup-path.png" alt-text="R setup":::

## Update system environment variable

Follow these steps to modify the **PATH** system environment variables.

1. In the Windows search box, search for **Edit the system environment variables** and open it.

1. Under **Advanced**, select **Environment Variables**.

1. Modify the **PATH** system environment variable.

    Select **PATH** and click **Edit**.

    Select **New** and add the path to the `\bin\x64` folder in your R installation path. For example, `C:\Program Files\R\R-4.0.3\bin\x64`.

## Install Rcpp package

Follow these steps to install the **Rcpp** package.

1. Start an *elevated* command prompt (run as Administrator).

1. Start R from the command prompt. Run `\bin\R.exe` in the folder in your R installation path. For example, `C:\Program Files\R\R-4.0.3\bin\R.exe`.

    ```CMD
    "C:\Program Files\R\R-4.0.3\bin\R.exe"
    ```

1. Run the following script to install the Rcpp package in the `\library` folder in your R installation path. For example, `C:\Program Files\R\R-4.0.3\library`.

    ```R
    install.packages("Rcpp", lib="C:\\Program Files\\R\\R-4.0.3\\library");
    ```

## Grant access to R folder

> [!NOTE]
> If you have installed R in the default location of `C:\Program Files\R\R-version` (for example, `C:\Program Files\R\R-4.0.3`), you can skip this step.

Run the following **icacls** commands from a new *elevated* command prompt to grant **READ & EXECUTE** access to the **SQL Server Launchpad Service user name** and SID **S-1-15-2-1** (**ALL APPLICATION PACKAGES**). The launchpad service user name is of the form `NT Service\MSSQLLAUNCHPAD$INSTANCENAME` where `INSTANCENAME` is the instance name of your SQL Server.

The commands will recursively grant access to all files and folders under the given directory path.

1. Give permissions to **SQL Server Launchpad Service user name** to your R installation path. For example, `C:\Program Files\R\R-4.0.3`.

    ```cmd
    icacls "C:\Program Files\R\R-4.0.3" /grant "NT Service\MSSQLLAUNCHPAD":(OI)(CI)RX /T
    ```

    For named instance, the command will be `icacls "C:\Program Files\R\R-4.0.3" /grant "NT Service\MSSQLLAUNCHPAD$SQL01":(OI)(CI)RX /T` for an instance called **SQL01**.

2. Give permissions to **SID S-1-15-2-1** to your R installation path. For example, `C:\Program Files\R\R-4.0.3`.

    ```cmd
    icacls "C:\Program Files\R\R-4.0.3" /grant *S-1-15-2-1:(OI)(CI)RX /T
    ```

    The preceding command grants permissions to the computer **SID S-1-15-2-1**, which is equivalent to **ALL APPLICATION PACKAGES** on an English version of Windows. Alternatively, you can use `icacls "C:\Program Files\R\R-4.0.3" /grant "ALL APPLICATION PACKAGES":(OI)(CI)RX /T` on an English version of Windows.

## Restart SQL Server Launchpad

Follow these steps to restart the SQL Server Launchpad service.

1. Open [SQL Server Configuration Manager](../../../relational-databases/sql-server-configuration-manager.md).

1. Under **SQL Server Services**, right click on **SQL Server Launchpad (MSSQLSERVER)** and select **Restart**. If you using a named instance, the instance name will be shown instead of **(MSSQLSERVER)**.

## Register language extension

Follow these steps to download and register the R language extension, which is used for the R custom runtime.

1. Download the **R-lang-extension-windows-release.zip** file from the [SQL Server Language Extensions GitHub repo](https://github.com/microsoft/sql-server-language-extensions/releases).

    Alternatively, you can use the debug version (**R-lang-extension-windows-debug.zip**) in a development or test environment. The debug version provides verbose logging information to investigate any errors, and is not recommended for production environments.

1. Use [Azure Data Studio](../../../azure-data-studio/what-is-azure-data-studio.md) to connect to your SQL Server instance and run the following T-SQL command to register the R language extension with [CREATE EXTERNAL LANGUAGE](../../../t-sql/statements/create-external-language-transact-sql.md).

    Modify the path in this statement to reflect the location of the downloaded language extension zip file (**R-lang-extension-windows-release.zip**) and the location your R installation (`C:\\Program Files\\R\\R-4.0.3`).

    ```sql
    CREATE EXTERNAL LANGUAGE [myR]
    FROM (CONTENT = N'C:\path\to\R-lang-extension-windows-release.zip', 
        FILE_NAME = 'libRExtension.dll',
        ENVIRONMENT_VARIABLES = N'{"R_HOME": "C:\\Program Files\\R\\R-4.0.3"}');
    GO
    ```

    Execute the statement for each database you want to use the R language extension in.

    > [!NOTE]
    > **R** is a reserved word and can't be used as the name for a new external language name. Use a different name instead. For example, the statement above uses **myR**.
