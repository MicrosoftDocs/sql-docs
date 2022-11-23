---
ms.service: sql
ms.subservice: machine-learning-services
ms.date: 02/18/2021
ms.topic: include
author: WilliamDAssafMSFT
ms.author: wiassaf
---
## Custom installation of R

::: zone pivot="platform-linux-ubuntu"
> [!NOTE]
> If you have installed R in the default location of **/usr/lib/R**, you can skip this section and move on to the [Install Rcpp package](#install-rcpp-package-linux) section.
::: zone-end

### Update the environment variables

First, edit the **mssql-launchpadd** service to add the **R_HOME** environment variable to the file `/etc/systemd/system/mssql-launchpadd.service.d/override.conf`

1. Open the file with systemctl

    ```bash
    sudo systemctl edit mssql-launchpadd
    ```

1. Insert the following text in the `/etc/systemd/system/mssql-launchpadd.service.d/override.conf` file that opens. Set value of **R_HOME** to the custom R installation path.

    ```text
    [Service]
    Environment="R_HOME=<path to R>"
    ```

1. Save and close.

Next, make sure `libR.so` can be loaded.

1. Create a custom-r.conf file in **/etc/ld.so.conf.d**.

    ```bash
    sudo vi /etc/ld.so.conf.d/custom-r.conf
    ```

1. In the file that opens, add path to **libR.so** from the custom R installation.

    ```
    <path to the R lib>
    ```

1. Save the new file and close the editor.

1. Run `ldconfig` and verify **libR.so** can be loaded by running the following command and checking that all dependent libraries can be found.

    ```bash
    sudo ldconfig
    ldd <path to the R lib>/libR.so
    ```

### Grant access to the custom R installation folder

Set the `datadirectories` option in the extensibility section of `/var/opt/mssql/mssql.conf` file to the custom R installation.

```bash
sudo /opt/mssql/bin/mssql-conf set extensibility.datadirectories <path to R>
```

### Restart mssql-launchpadd service

Run the following command to restart **mssql-launchpadd**.

```bash
sudo systemctl restart mssql-launchpadd
```

<a name="install-rcpp-package-linux"></a>

## Install Rcpp package

Follow these steps to install the **Rcpp** package.

1. Start R from a shell:

    ```bash
    sudo ${R_HOME}/bin/R
    ```

1. Run the following script to install the Rcpp package in the ${R_HOME}\library folder.

  ```R
  install.packages("Rcpp", lib = "${R_HOME}/library");
  ```

## Register language extension

Follow these steps to download and register the R language extension, which is used for the R custom runtime.

1. Download the **R-lang-extension-linux-release.zip** file from the [SQL Server Language Extensions GitHub repo](https://github.com/microsoft/sql-server-language-extensions/releases).

    Alternatively, you can use the debug version (**R-lang-extension-linux-debug.zip**) in a development or test environment. The debug version provides verbose logging information to investigate any errors, and is not recommended for production environments.

1. Use [Azure Data Studio](../../../azure-data-studio/what-is-azure-data-studio.md) to connect to your SQL Server instance and run the following T-SQL command to register the R language extension with [CREATE EXTERNAL LANGUAGE](../../../t-sql/statements/create-external-language-transact-sql.md). 

    Modify the path in this statement to reflect the location of the downloaded language extension zip file (**R-lang-extension-linux-release.zip**).

    ```sql
    CREATE EXTERNAL LANGUAGE [myR]
    FROM (CONTENT = N'/path/to/R-lang-extension-linux-release.zip', FILE_NAME = 'libRExtension.so.1.1');
    GO
    ```

    Execute the statement for each database you want to use the R language extension in.

    > [!NOTE]
    > **R** is a reserved word and can't be used as the name for a new external language name. Use a different name instead. For example, the statement above uses **myR**.
