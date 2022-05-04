---
title: Install on Linux
titleSuffix: SQL Server Machine Learning Services
description: 'Learn how to install SQL Server Machine Learning Services on Linux: Red Hat, Ubuntu, and SUSE.'
author: WilliamDAssafMSFT
ms.author: wiassaf
manager: rothja
ms.date: 05/24/2022
ms.topic: how-to
ms.prod: sql
ms.technology: machine-learning-services
monikerRange: ">=sql-server-ver16||>=sql-server-linux-ver16"
ms.custom:
  - intro-installation
---
# Install SQL Server Machine Learning Services (Python and R) on Linux

[!INCLUDE [SQL Server 2022 - Linux](../includes/applies-to-version/sqlserver2022-linux.md)]

This article guides you in the installation of [SQL Server Machine Learning Services](../machine-learning//sql-server-machine-learning-services.md) on Linux. Python and R scripts can be executed in-database using Machine Learning Services.

You can install Machine Learning Services on Red Hat Enterprise Linux (RHEL), SUSE Linux Enterprise Server (SLES), and Ubuntu. For more information, see [the Supported platforms section in the Installation guidance for SQL Server on Linux](sql-server-linux-setup.md#supportedplatforms).

::: moniker range=">=sql-server-linux-ver15"
> [!IMPORTANT]
> This article refers to [!INCLUDE[sssql22-md](../includes/sssql22-md.md)]. For [!INCLUDE[sssql22-md](../includes/sssql19-md.md)], see to [Install SQL Server 2019 Machine Learning Services (Python and R) on Linux](sql-server-linux-setup-machine-learning-sql-2022.md).
::: moniker-end

## Pre-install checklist

* [Install SQL Server on Linux](sql-server-linux-setup.md) and verify the installation.

* Check the SQL Server Linux repositories for the Python and R extensions. 
  If you already configured source repositories for the database engine install, you can run the **mssql-mlservices** package install commands using the same repo registration.

* You should have a tool for running T-SQL commands. 

  * You can use [Azure Data Studio](../azure-data-studio/download-azure-data-studio.md), a free database tool that runs on Linux, Windows, and macOS.

## Package list

On an internet-connected device, packages are downloaded and installed independently of the database engine using the package installer for each operating system. The following table describes all available packages, but for R and Python, you specify packages that provide either the full feature installation or the minimum feature installation.

Available installation packages for SQL Server 2022 on Linux:

| Package name | Applies-to | Description |
|--------------|----------|-------------|
|mssql-server-extensibility  | All | Extensibility framework used to run Python and R. |


<a name="RHEL"></a>

## Install on RHEL TODO

```bash
sudo yum install mssql-server-extensibility
```

TODO 


<a name="ubuntu"></a>

## Install on Ubuntu

Refer to [Quickstart: Install SQL Server and create a database on Ubuntu](quickstart-install-connect-ubuntu.md) for SQL Server 2022 on Linux installation. Then, use the following steps to install SQL Server Machine Learning Services on Ubuntu:

 - [Install R on Ubuntu](#install-r-on-ubuntu)
 - [Install Python on Ubuntu](#install-python-on-ubuntu)

### Install R on Ubuntu

The following commands register the repository providing the R language platform. 

> [!Tip]
> If possible, run `apt-get update` to refresh packages on the system prior to installation. 

1. Begin installation as root.

    ```bash
    sudo su
    ```

2. Optionally, if your system does not have the `https apt transport` option:

    ```bash
    apt-get install apt-transport-https
    ```

3. Set the location of the package repo the "prod" directory containing the distribution. This example specifies 20.04. Replace with 16.04 or 14.04 if you want those versions.

    ```bash
    wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb
    ```

4. Register the repository.

    ```bash
    dpkg -i packages-microsoft-prod.deb
    ```

5. Update packages on your system (required).

    ```bash
    apt-get update
    ```

6. Install the SQL Server extensibility feature.

    ```bash
    apt-get install mssql-server-extensibility
    ```

7. Download and install the version of R that is desired. Choose a version of R 4.2 or higher, [available for download directly from cran.r-project.org](https://cran.r-project.org/). Follow the instructions for the desired runtime.

8. Prepare RevoScaleR dependencies by retrieving the installation key, installing it, and then removing the public key. 

    ```bash
    cd /tmp
    # Get the key
    wget https://apt.repos.intel.com/intel-gpg-keys/GPG-PUB-KEY-INTEL-SW-PRODUCTS-2019.PUB
    # Install key
    apt-key add GPG-PUB-KEY-INTEL-SW-PRODUCTS-2019.PUB
    # Remove the public key file 
    rm GPG-PUB-KEY-INTEL-SW-PRODUCTS-2019.PUB
    # exit the root shell
    ```

9. Install the RevoScaleR dependencies.

    ```bash
    sh -c 'echo deb https://apt.repos.intel.com/mkl all main > /etc/apt/sources.list.d/intel-mkl.list'
    apt-get update
    
    apt-get install -y intel-mkl-2019.2-057
    ```

10. Install CompatibilityAPI and RevoScale R dependencies. There are two options to perform this, from the R terminal of the version you have installed, or shell.

    ```r
    # R Terminal
    install.packages("iterators")
    install.packages("foreach")
    install.packages("R6")
    install.packages("jsonlite")
    ```

    ```bash
    which R
    cd /io/runtime/R4.2/lib/R/bin
    ./R -e "install.packages('iterators',repos = 'http://cran.us.r-project.org')"
    ./R -e "install.packages('foreach',repos = 'http://cran.us.r-project.org')"
    ./R -e "install.packages('R6',repos = 'http://cran.us.r-project.org')"
    ./R -e "install.packages('jsonlite',repos = 'http://cran.us.r-project.org')"
    ```

11. Download the [CompatibilityAPI for Linux](https://go.microsoft.com/fwlink/?LinkID=2193925).

12. Install the CompatibilityAPI for Linux, using the following shell script.

    ```bash
    ./R -e "install.packages('CompatibilityAPI', repos = NULL, type='source')"
    ```

13. Download [RevoScaleR Linux](https://go.microsoft.com/fwlink/?LinkID=2193829).

14. Install RevoScaleR Linux.

    ```bash
    ./R -e "install.packages('RevoScaleR', repos = NULL, type='source')"
    ```

15. Provision permissions of the BxlServer.
 
    ```bash
    chmod +x /io/runtime/R4.2/lib/R/library/RevoScaleR/rxLibs/x64/BxlServer
    ```

16. Copy necessary libraries.
    
    ```bash
    sudo cp /opt/intel/compilers_and_libraries_2019.2.187/linux/mkl/lib/intel64_lin/libmkl_gnu_thread.so /io/runtime/R4.2/lib/R/library/RevoScaleR/rxLibs/x64/libmkl_gnu_thread.so
    sudo cp /opt/intel/compilers_and_libraries_2019.2.187/linux/mkl/lib/intel64_lin/libmkl_gf_lp64.so /io/runtime/R4.2/lib/R/library/RevoScaleR/rxLibs/x64/libmkl_gf_lp64.so
    sudo cp /opt/intel/compilers_and_libraries_2019.2.187/linux/mkl/lib/intel64_lin/libmkl_core.so /io/runtime/R4.2/lib/R/library/RevoScaleR/rxLibs/x64/libmkl_core.so
    sudo cp /opt/intel/compilers_and_libraries_2019.2.187/linux/mkl/lib/intel64_lin/libmkl_vml_mc3.so /io/runtime/R4.2/lib/R/library/RevoScaleR/rxLibs/x64/libmkl_vml_mc3.so
    sudo cp /opt/intel/compilers_and_libraries_2019.2.187/linux/mkl/lib/intel64_lin/libmkl_vml_def.so /io/runtime/R4.2/lib/R/library/RevoScaleR/rxLibs/x64/libmkl_vml_def.so
    
    sudo cp /opt/intel/compilers_and_libraries_2019.2.187/linux/mkl/lib/intel64_lin/libmkl_def.so /io/runtime/R4.2/lib/R/library/RevoScaleR/rxLibs/x64/libmkl_def.so
    sudo cp /opt/intel/compilers_and_libraries_2019.2.187/linux/mkl/lib/intel64_lin/libmkl_avx2.so /io/runtime/R4.2/lib/R/library/RevoScaleR/rxLibs/x64/libmkl_avx2.so
    ```

17. Verify RevoScaleR installation from the R terminal.

    ```r
    library("RevoScaleR")
    ```

18. Configure the installed R runtime with SQL Server for Linux.

    ```bash
    sudo /opt/mssql/bin/mssql-conf set extensibility rbinpath /io/runtime/R4.2/lib/R/bin/R
    sudo /opt/mssql/bin/mssql-conf set extensibility datadirectories /io/runtime/R4.2/
    
    systemctl restart mssql-launchpadd.service
    ```

19. Configure SQL Server for Linux to allow external scripts using the `sp_configure` system stored procedure.

    ```sql
    EXEC sp_configure 'external scripts enabled', 1;
    GO
    RECONFIGURE
    GO
    ```

20. Verify the installation by executing a simple T-SQL command to return the version of R:

    ```sql
    EXEC sp_execute_external_script @script=N'print(R.version)',@language=N'R';
    GO
    ```

### Install Python on Ubuntu

1. Begin installation as root.

    ```bash
    sudo su
    ```
2. Update packages on your system (required).

    ```bash
    apt-get update
    ```

3. Install the SQL Server extensibility feature.

    ```bash
    apt-get install mssql-server-extensibility
    ```

4. To install Python 3.10, first install the dependency packages necessary to configure Python.
    
    ```bash
    apt-get install -y make build-essential libssl-dev zlib1g-dev libbz2-dev libreadline-dev libsqlite3-dev wget curl llvm libncurses5-dev libncursesw5-dev xz-utils tk-dev
    apt-get install -y build-essential checkinstall
    ```

5. There are two options to install python, either from the source or using `apt install`:

    To install from the source:
    
    ```bash
    wget https://www.python.org/ftp/python/3.10.0/Python-3.10.0.tgz
    tar -xf Python-3.10.0.tgz
    
    cd Python-3.10.0
    ./configure --enable-shared --libdir=/usr/lib && make && make altinstall
    ```
    
    To install using `apt install`:
    
    ```bash
    apt install -y software-properties-common 
    add-apt-repository ppa:deadsnakes/ppa -y
    apt install  -y python3.10 libpython3.10
    apt install  -y python3.10-distutils
    curl -sS  https://bootstrap.pypa.io/get-pip.py | python3.10
    python3.10 -m pip install /home/<your user>/temp/revoscalepy-10.0.0-py3-none-any.whl
    ```

6. Prepare revoscalepy dependencies by retrieving the installation key, installing it, and then removing the public key. 

    ```bash
    cd /tmp
    # now get the key:
    wget https://apt.repos.intel.com/intel-gpg-keys/GPG-PUB-KEY-INTEL-SW-PRODUCTS-2019.PUB
    # now install that key
    apt-key add GPG-PUB-KEY-INTEL-SW-PRODUCTS-2019.PUB
    # now remove the public key file exit the root shell
    rm GPG-PUB-KEY-INTEL-SW-PRODUCTS-2019.PUB
    ```

7. Install revoscalepy dependencies.

    ```bash
    sh -c 'echo deb https://apt.repos.intel.com/mkl all main > /etc/apt/sources.list.d/intel-mkl.list'
    apt-get update
    apt-get install -y intel-mkl-2019.2-057
    ```

8. Download [revoscalepy for Linux](https://go.microsoft.com/fwlink/?LinkID=2193830).

9. Install revoscalepy for the root user.

    TODO WHAT IF NOT IN A CONTAINER?
    
    ```bash
    # From host copy to docker container
    docker cp drop_build_main_linux.zip <container id>:/
    
    #Back on docker container
    unzip drop_build_main_linux.zip
    
    sudo -H pip3.10 install /drop_build_main_linux/revoscalepy/Linux/revoscalepy-10.0.0-py3-none-any.whl
    ```

10. Verify the revoscalepy installation from the python terminal. Verify the library can be imported.

    ```python
    import revoscalepy
    ```

11. Configure the installed python runtime with SQL Server. This step will differ based on what option you chose in step 2.

    To verify the installation from source:

    ```bash
    sudo /opt/mssql/bin/mssql-conf set extensibility pythonbinpath /usr/local/bin/python3.10
    sudo /opt/mssql/bin/mssql-conf set extensibility datadirectories /usr/local/
    ```

    To verify the installation from `apt install`:

    ```bash
    sudo /opt/mssql/bin/mssql-conf set extensibility pythonbinpath /usr/bin/python3.10
    sudo /opt/mssql/bin/mssql-conf set extensibility datadirectories /usr/local/:/usr/local/lib/python3.10/dist-packages
    ```

12. Restart the Launchpad service.

    ```bash
    systemctl restart mssql-launchpadd.service
    ```
    
13. Configure SQL Server for Linux to allow external scripts using the `sp_configure` system stored procedure.

    ```sql
    EXEC sp_configure 'external scripts enabled', 1;
    GO
    RECONFIGURE
    GO
    ```

14. Verify the installation by executing a simple T-SQL command to return the version of python:

    ```sql
    EXEC sp_execute_external_script @script=N'import sys;print(sys.version)',@language=N'Python';
    GO
    ```


<a name="SLES"></a>

## Install Java

To install the Java language extension, see [Install SQL Server Java Language Extension on Linux](sql-server-linux-setup-language-extensions-java.md).


## Verify installation

To validate installation:

* Run a T-SQL script that executes a system stored procedure invoking Python or R using a query tool. 

* Execute the following SQL command to test R execution in SQL Server. Errors? Try a service restart, `sudo systemctl restart mssql-server.service`.
  ```sql
  EXEC sp_execute_external_script   
  @language =N'R', 
  @script=N' 
  OutputDataSet <- InputDataSet', 
  @input_data_1 =N'SELECT 1 AS hello' 
  WITH RESULT SETS (([hello] int not null)); 
  GO 
  ```
 
* Execute the following SQL command to test Python execution in SQL Server. 
 
  ```sql
  EXEC sp_execute_external_script  
  @language =N'Python', 
  @script=N' 
  OutputDataSet = InputDataSet; 
  ', 
  @input_data_1 =N'SELECT 1 AS hello' 
  WITH RESULT SETS (([hello] int not null)); 
  GO 
  ```

<a name="install-all"></a>

## Unattended installation

Using the [unattended install](sql-server-linux-setup.md#unattended) for the Database Engine, add the packages for mssql-mlservices and EULAs.

 Use one of the mlservices-specific EULA parameters for the open-source R and Python distributions:

```bash
sudo /opt/mssql/bin/mssql-conf setup accept-eula-ml
```

The complete EULA is documented at [Configure SQL Server on Linux with the mssql-conf tool](sql-server-linux-configure-mssql-conf.md#mlservices-eula).

## Offline installation

Follow the [Offline installation](sql-server-linux-setup.md#offline) instructions for steps on installing the packages. Find your download site, and then download specific packages using the package list below.

> [!Tip]
> Several of the package management tools provide commands that can help you determine package dependencies. For yum, use `sudo yum deplist [package]`. For Ubuntu, use `sudo apt-get install --reinstall --download-only [package name]` followed by `dpkg -I [package name].deb`.

 
### Download site TODO

Download packages from [https://packages.microsoft.com/](https://packages.microsoft.com/). All of the mlservices packages for Python and R are colocated with database engine package. Base version for the mlservices packages is 9.4.6.  

### RHEL/8 paths

|Package|Download location|
|--|----|
| mssql/mlservices packages | [https://packages.microsoft.com/rhel/8/mssql-server-2019/](https://packages.microsoft.com/rhel/8/mssql-server-2019/) |

### Ubuntu/20.04 paths

|Package|Download location|
|--|----|
| mssql/mlservices packages | [https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/) |

Select extensions you want to use and download the packages necessary for a specific language. The filenames include platform information in the suffix.

## Next steps

Python developers can learn how to use Python with SQL Server by following these tutorials:

+ [Python tutorial: Predict ski rental with linear regression in SQL Server Machine Learning Services](../machine-learning/tutorials/python-ski-rental-linear-regression-deploy-model.md)
+ [Python tutorial: Categorizing customers using k-means clustering with SQL Server Machine Learning Services](../machine-learning/tutorials/python-clustering-model.md)

R developers can get started with some simple examples, and learn the basics of how R works with SQL Server. For your next step, see the following links:

+ [Quickstart: Run R in T-SQL](../machine-learning/tutorials/quickstart-r-create-script.md)
+ [Tutorial: In-database analytics for R developers](../machine-learning/tutorials/r-taxi-classification-introduction.md)
