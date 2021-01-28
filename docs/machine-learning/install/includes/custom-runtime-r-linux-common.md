---
ms.prod: sql
ms.technology: machine-learning-services
ms.date: 01/14/2021
ms.topic: include
author: dphansen
ms.author: davidph
---
## Install Rcpp package

In the following instructions, ${R_HOME} is the path to your R installation. 

+ Locate the R binary in ${R_HOME}/bin. By default, it is in **/usr/lib/R/bin**.

+ Start R

  ```bash
  sudo ${R_HOME}/bin/R
  ```

+ In this *elevated* R prompt (${R_HOME}/bin/R), run the following script to install the **Rcpp** package in the ${R_HOME}/library folder.

  ```R
  install.packages("Rcpp", lib = "${R_HOME}/library");
  ```

## Using a custom installation of R

> [!NOTE]
>If you have installed R in the default location of **/usr/lib/R**, you can skip this section.

### Update the environment variables

1. Edit the mssql-launchpadd service to add the R_HOME environment variable to the file `/etc/systemd/system/mssql-launchpadd.service.d/override.conf`

    ```bash
    sudo systemctl edit mssql-launchpadd
    ```

    + Insert the following text in the `/etc/systemd/system/mssql-launchpadd.service.d/override.conf` file that opens. Set value of R_HOME to the custom R installation path.

    ```text
    [Service]
    Environment="R_HOME=/path/to/installation/of/R"
    ```

    + Save and close.

2. Make sure **libR.so** can be loaded.

    + Create a custom-r.conf file in **/etc/ld.so.conf.d**.

    ```bash
    sudo vi /etc/ld.so.conf.d/custom-r.conf
    ```

    + In the file that opens, add path to **libR.so** from the custom R installation.

    ```vi editor
    /path/to/installation/of/R/lib
    ```

    + Save and close the new file.

    + Run `ldconfig` and verify **libR.so** can be loaded by running the following command and checking that all dependent libraries can be found.

    ```bash
    sudo ldconfig
    ldd /path/to/installation/of/R/lib/libR.so
    ```

### Grant access to the custom R installation folder

Set the `datadirectories` option in the extensibility section of /var/opt/mssql/mssql.conf file to the custom R installation.

```bash
sudo /opt/mssql/bin/mssql-conf set extensibility.datadirectories /path/to/installation/of/R
```

### Restart mssql-launchpadd service

> [!NOTE]
>If you have installed R in the default location of **/usr/lib/R**, you can skip this step.

```bash
sudo systemctl restart mssql-launchpadd
```

## Download R language extension

Download the [zip file containing the R language extension for Linux](https://github.com/microsoft/sql-server-language-extensions/releases). Recommended to use the release version in production. Use the debug version in development or test since it provides verbose logging information to investigate any errors.

## Register external language

Register this R language extension with [CREATE EXTERNAL LANGUAGE](../../t-sql/statements/create-external-language-transact-sql.md) for each database you want to use it in. Use [Azure Data Studio](https://docs.microsoft.com/sql/azure-data-studio/download-azure-data-studio) to connect to SQL Server and run the following T-SQL command. 
Modify the path in this statement to reflect the location of the downloaded language extension zip file (r-lang-extension.zip).


> [!NOTE]
>**R** is a reserved word. Use a different name for the external language, for example, "myR".

```sql
CREATE EXTERNAL LANGUAGE [myR]
FROM (CONTENT = N'/path/to/R-lang-extension.zip', FILE_NAME = 'libRExtension.so.1.0');
GO
```