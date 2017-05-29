---
title: "Installing the Microsoft ODBC Driver for SQL Server on Linux and macOS | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "driver, installing"
ms.assetid: f78b81ed-5214-43ec-a600-9bfe51c5745a
caps.latest.revision: 69
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# Installing the Microsoft ODBC Driver for SQL Server on Linux and macOS
[!INCLUDE[Driver_ODBC_Download](../../../includes/driver_odbc_download.md)]

This topic explains how to install the [!INCLUDE[msCoName](../../../includes/msconame_md.md)] ODBC Driver for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion_md.md)] on Linux and macOS, as well as the optional Command Line Tools for SQL Server (`bcp` and `sqlcmd`) and the unixODBC Development Headers.

## Microsoft ODBC Driver 13.1 for SQL Server 

### Debian 8
```
sudo su 
curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add -
curl https://packages.microsoft.com/config/debian/8/prod.list > /etc/apt/sources.list.d/mssql-release.list
exit
sudo apt-get update
sudo ACCEPT_EULA=Y apt-get install msodbcsql
# optional: for bcp and sqlcmd
sudo ACCEPT_EULA=Y apt-get install mssql-tools
echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bash_profile
echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc
source ~/.bashrc
# optional: for unixODBC development headers
sudo apt-get install unixodbc-dev
```

### RedHat Enterprise Server 6
```
sudo su
curl https://packages.microsoft.com/config/rhel/6/prod.repo > /etc/yum.repos.d/mssql-release.repo
exit
sudo yum remove unixODBC-utf16 unixODBC-utf16-devel #to avoid conflicts
sudo ACCEPT_EULA=Y yum install msodbcsql
# optional: for bcp and sqlcmd
sudo ACCEPT_EULA=Y yum install mssql-tools
echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bash_profile
echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc
source ~/.bashrc
# optional: for unixODBC development headers
sudo yum install unixODBC-devel
```

### RedHat Enterprise Server 7
```
sudo su
curl https://packages.microsoft.com/config/rhel/7/prod.repo > /etc/yum.repos.d/mssql-release.repo
exit
sudo yum remove unixODBC-utf16 unixODBC-utf16-devel #to avoid conflicts
sudo ACCEPT_EULA=Y yum install msodbcsql
# optional: for bcp and sqlcmd
sudo ACCEPT_EULA=Y yum install mssql-tools
echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bash_profile
echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc
source ~/.bashrc
# optional: for unixODBC development headers
sudo yum install unixODBC-devel
```

### SUSE Linux Enterprise Server 12

```
sudo su
zypper ar https://packages.microsoft.com/config/sles/12/prod.repo
exit
sudo ACCEPT_EULA=Y zypper install msodbcsql
# optional: for bcp and sqlcmd
sudo ACCEPT_EULA=Y zypper install mssql-tools
echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bash_profile
echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc
source ~/.bashrc
# optional: for unixODBC development headers
sudo zypper install unixODBC-devel
``` 

### Ubuntu 15.10
```
sudo su 
curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add -
curl https://packages.microsoft.com/config/ubuntu/15.10/prod.list > /etc/apt/sources.list.d/mssql-release.list
exit
sudo apt-get update
sudo ACCEPT_EULA=Y apt-get install msodbcsql
# optional: for bcp and sqlcmd
sudo ACCEPT_EULA=Y apt-get install mssql-tools
echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bash_profile
echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc
source ~/.bashrc
# optional: for unixODBC development headers
sudo apt-get install unixodbc-dev
```

### Ubuntu 16.04
```
sudo su 
curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add -
curl https://packages.microsoft.com/config/ubuntu/16.04/prod.list > /etc/apt/sources.list.d/mssql-release.list
exit
sudo apt-get update
sudo ACCEPT_EULA=Y apt-get install msodbcsql
# optional: for bcp and sqlcmd
sudo ACCEPT_EULA=Y apt-get install mssql-tools
echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bash_profile
echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc
source ~/.bashrc
# optional: for unixODBC development headers
sudo apt-get install unixodbc-dev
```

### Ubuntu 16.10
```
sudo su 
curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add -
curl https://packages.microsoft.com/config/ubuntu/16.10/prod.list > /etc/apt/sources.list.d/mssql-release.list
exit
sudo apt-get update
sudo ACCEPT_EULA=Y apt-get install msodbcsql
# optional: for bcp and sqlcmd
sudo ACCEPT_EULA=Y apt-get install mssql-tools
echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bash_profile
echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc
source ~/.bashrc
# optional: for unixODBC development headers
sudo apt-get install unixodbc-dev
```

### OS X 10.11 (El Capitan) and macOS 10.12 (Sierra)

```
/usr/bin/ruby -e "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/master/install)"
brew tap microsoft/mssql-release https://github.com/Microsoft/homebrew-mssql-release
brew update
brew install msodbcsql mssql-tools
```

## Microsoft ODBC Driver 13 for SQL Server

### RedHat Enterprise Server 6
```
sudo su
curl https://packages.microsoft.com/config/rhel/6/prod.repo > /etc/yum.repos.d/mssql-release.repo
exit
sudo yum update
sudo yum remove unixODBC #to avoid conflicts
sudo ACCEPT_EULA=Y yum install msodbcsql-13.0.1.0-1 mssql-tools-14.0.2.0-1
sudo yum install unixODBC-utf16-devel #this step is optional but recommended*
#Create symlinks for tools
ln -sfn /opt/mssql-tools/bin/sqlcmd-13.0.1.0 /usr/bin/sqlcmd 
ln -sfn /opt/mssql-tools/bin/bcp-13.0.1.0 /usr/bin/bcp
```

### RedHat Enterprise Server 7
```
sudo su
curl https://packages.microsoft.com/config/rhel/7/prod.repo > /etc/yum.repos.d/mssql-release.repo
exit
sudo yum update
sudo yum remove unixODBC #to avoid conflicts
sudo ACCEPT_EULA=Y yum install msodbcsql-13.0.1.0-1 mssql-tools-14.0.2.0-1
sudo yum install unixODBC-utf16-devel #this step is optional but recommended*
#Create symlinks for tools
ln -sfn /opt/mssql-tools/bin/sqlcmd-13.0.1.0 /usr/bin/sqlcmd 
ln -sfn /opt/mssql-tools/bin/bcp-13.0.1.0 /usr/bin/bcp
```

### Ubuntu 15.10
```
sudo su 
curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add -
curl https://packages.microsoft.com/config/ubuntu/15.10/prod.list > /etc/apt/sources.list.d/mssql-release.list
exit
sudo apt-get update
sudo ACCEPT_EULA=Y apt-get install msodbcsql=13.0.1.0-1 mssql-tools-14.0.2.0-1
sudo apt-get install unixodbc-dev-utf16 #this step is optional but recommended*
#Create symlinks for tools
ln -sfn /opt/mssql-tools/bin/sqlcmd-13.0.1.0 /usr/bin/sqlcmd 
ln -sfn /opt/mssql-tools/bin/bcp-13.0.1.0 /usr/bin/bcp
```

### Ubuntu 16.04
```
sudo su 
curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add -
curl https://packages.microsoft.com/config/ubuntu/16.04/prod.list > /etc/apt/sources.list.d/mssql-release.list
exit
sudo apt-get update
sudo ACCEPT_EULA=Y apt-get install msodbcsql=13.0.1.0-1 mssql-tools-14.0.2.0-1
sudo apt-get install unixodbc-dev-utf16 #this step is optional but recommended*
#Create symlinks for tools
ln -sfn /opt/mssql-tools/bin/sqlcmd-13.0.1.0 /usr/bin/sqlcmd 
ln -sfn /opt/mssql-tools/bin/bcp-13.0.1.0 /usr/bin/bcp
```

### SUSE Linux Enterprise Server 12

```
sudo su 
zypper ar https://packages.microsoft.com/config/sles/12/prod.repo 
zypper update 
sudo ACCEPT_EULA=Y zypper install msodbcsql-13.0.1.0-1 mssql-tools-14.0.2.0-1
zypper install unixODBC-utf16-devel
#Create symlinks for tools
ln -sfn /opt/mssql-tools/bin/sqlcmd-13.0.1.0 /usr/bin/sqlcmd 
ln -sfn /opt/mssql-tools/bin/bcp-13.0.1.0 /usr/bin/bcp
```

### Offline installation
If you prefer/require the [!INCLUDE[msCoName](../../../includes/msconame_md.md)] ODBC Driver 13 to be installed on a computer with no internet connection, you will need to resolve package dependencies manually. The [!INCLUDE[msCoName](../../../includes/msconame_md.md)] ODBC Driver 13 has the following direct dependencies:
- Ubuntu: libc6 (>= 2.21), libstdc++6 (>= 4.9), libkrb5-3, libcurl3, openssl, debconf (>= 0.5), unixodbc (>= 2.3.1-1)
- Red Hat: glibc, e2fsprogs, krb5-libs, openssl, unixODBC
- SuSE: glibc, libuuid1, krb5, openssl, unixODBC

Each of these packages in turn has their own dependencies which may or may not be present on the system. For a general solution to this issue, refer to your distribution's package manager documentation: [Redhat](https://wiki.centos.org/HowTos/CreateLocalRepos), [Ubuntu](http://unix.stackexchange.com/questions/87130/how-to-quickly-create-a-local-apt-repository-for-random-packages-using-a-debian), and [SUSE](https://en.opensuse.org/Portal:Zypper)

It is also common to manually download all the dependent packages and place them together on the installation computer, then manually install each package in turn, finishing with the [!INCLUDE[msCoName](../../../includes/msconame_md.md)] ODBC Driver 13 package.

#### Redhat Linux Enterprise Server 7
  - Download the latest msodbcsql .rpm from here: http://packages.microsoft.com/rhel/7/prod/
  - Install dependencies and the driver
  
```
yum install glibc e2fsprogs krb5-libs openssl unixODBC unixODBC-devel #install dependencies
sudo rpm -i  msodbcsql-13.1.X.X-X.x86_64.rpm #install the Driver
```

#### Ubuntu 16.04
- Download the latest msodbcsql .deb from here: http://packages.microsoft.com/ubuntu/16.04/prod/pool/main/m/msodbcsql/ 
- Install dependencies and the driver 

```
sudo apt-get install libc6 libstdc++6 libkrb5-3 libcurl3 openssl debconf unixodbc unixodbc-dev #install dependencies
sudo dpkg -i msodbcsql_13.1.X.X-X_amd64.deb #install the Driver
```

#### SUSE Linux Enterprise Server 12
- Download the latest msodbcsql .rpm from here: http://packages.microsoft.com/sles/12/prod/
- Install the dependencies and the driver

```
zypper install glibc, libuuid1, krb5, openssl, unixODBC unixODBC-devel #install dependencies
sudo rpm -i  msodbcsql-13.1.X.X-X.x86_64.rpm #install the Driver
```

Once you have completed the package installation, you can verify that the [!INCLUDE[msCoName](../../../includes/msconame_md.md)] ODBC Driver 13 can find all its dependencies by running ldd and inspecting its output for missing libraries:
```
ldd /opt/microsoft/msodbcsql/lib64/libmsodbcsql-*
```
  
## Microsoft ODBC Driver 11 for SQL Server on Linux

Before you can use the driver, install the unixODBC driver manager. See [Installing the Driver Manager](../../../connect/odbc/linux-mac/installing-the-driver-manager.md) for more information.  

**Installation Steps**  

> [!IMPORTANT]  
> These instructions refer to `msodbcsql-11.0.2270.0.tar.gz`, which is installation file for Red Hat Linux. If you are installing the Preview for SUSE Linux, the file name is `msodbcsql-11.0.2260.0.tar.gz`.  
  
To install the driver:

1.  Make sure that you have root permission.  

2.  Change to the directory where the download placed the file `msodbcsql-11.0.2270.0.tar.gz`. Make sure that you have the \*.tar.gz file that matches your version of Linux. To extract the files, execute the following command, **tar xvzf msodbcsql-11.0.2270.0.tar.gz**.  
  
3.  Change to the `msodbcsql-11.0.2270.0` directory and there you should see a file called **install.sh**.  
  
4.  To see a list of the available installation options, execute the following command: **./install.sh**.  
  
5.  Make a backup of **odbcinst.ini**. The driver installation updates **odbcinst.ini**. odbcinst.ini contains the list of drivers that are registered with the unixODBC Driver Manager. To discover the location of odbcinst.ini on your computer, execute the following command: **odbc_config --odbcinstini**.  
  
6.  Before you install the driver, execute the following command: **./install.sh verify**. The output of **./install.sh verify** reports if your computer has the required software to support the ODBC driver on Linux.  
  
7.  When you are ready to install the ODBC driver on Linux, execute the command: **./install.sh install**. If you need to specify an install command (**bin-dir** or **lib-dir**), specify the command after the **install** option.  
  
8.  After reviewing the license agreement, type **YES** to continue with the installation.  
  
Installation puts the driver in `/opt/microsoft/msodbcsql/11.0.2270.0`. The driver and its support files must be in `/opt/microsoft/msodbcsql/11.0.2270.0`.  
  
To verify that the Microsoft ODBC driver on Linux was registered successfully, execute the following command: **odbcinst -q -d -n "ODBC Driver 11 for SQL Server"**.  
  
[Use Existing MSDN C++ ODBC Samples for the ODBC Driver on Linux](http://blogs.msdn.com/b/sqlblog/archive/2012/01/26/use-existing-msdn-c-odbc-samples-for-microsoft-linux-odbc-driver.aspx) shows a code sample that connects to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion_md.md)] using the ODBC driver on Linux.  
  
**Uninstalling**  
  
You can uninstall the ODBC driver 11 on Linux by executing the following commands:  
  
1.  `rm -f /usr/bin/sqlcmd`
  
2.  `rm -f /usr/bin/bcp`  
  
3.  `rm -rf /opt/microsoft/msodbcsql`  
  
4.  `odbcinst -u -d -n "ODBC Driver 11 for SQL Server"`
  
## Troubleshooting Connection Problems  
If you are unable to make a connection to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion_md.md)] using the ODBC driver, use the following information to identify the problem.  
  
The most common connection problem is to have two copies of the UnixODBC Driver Manager installed. Search /usr for libodbc\*.so\*. If you see more than one version of the file, you (possibly) have more than one driver manager installed. Your application might use the wrong version.
  
Enable the connection log by editing your `/etc/odbcinst.ini` file to contain the following section with these items:

```
[ODBC]
Trace = Yes
TraceFile = (path to log file, or /dev/stdout to output directly to the terminal)
```  
  
If you get another connection failure and do not see a log file, there (possibly) are two copies of the driver manager on your computer. Otherwise, the log output should be similar to the following:  
  
```  
[ODBC][28783][1321576347.077780][SQLDriverConnectW.c][290]  
        Entry:  
            Connection = 0x17c858e0  
            Window Hdl = (nil)  
            Str In = [DRIVER={ODBC Driver 13 for SQL Server};SERVER={contoso.com};Trusted_Connection={YES};WSID={mydb.contoso.com};AP...][length = 139 (SQL_NTS)]  
            Str Out = (nil)  
            Str Out Max = 0  
            Str Out Ptr = (nil)  
            Completion = 0  
        UNICODE Using encoding ASCII 'UTF8' and UNICODE 'UTF16LE'  
```  
  
If the ASCII character encoding is not UTF-8, for example: 
  
```  
UNICODE Using encoding ASCII 'ISO8859-1' and UNICODE 'UCS-2LE'  
```  
  
There is more than one Driver Manager installed and your application is using the wrong one, or the Driver Manager was not built correctly.  
  
For more information about resolving connection failures, see:  
  
-   [Steps to troubleshoot SQL connectivity issues](http://blogs.msdn.com/b/sql_protocols/archive/2008/04/30/steps-to-troubleshoot-connectivity-issues.aspx)  
  
-   [SQL Server 2005 Connectivity Issue Troubleshoot - Part I](http://blogs.msdn.com/b/sql_protocols/archive/2005/10/22/sql-server-2005-connectivity-issue-troubleshoot-part-i.aspx)  
  
-   [Connectivity troubleshooting in SQL Server 2008 with the Connectivity Ring Buffer](http://blogs.msdn.com/b/sql_protocols/archive/2008/05/20/connectivity-troubleshooting-in-sql-server-2008-with-the-connectivity-ring-buffer.aspx)  
  
-   [SQL Server Authentication Troubleshooter](http://blogs.msdn.com/b/sqlsecurity/archive/2010/03/29/sql-server-authentication-troubleshooter.aspx)  
  
-   [Error details (http://www.microsoft.com/products/ee/transform.aspx?ProdName=Microsoft+SQL+Server&EvtSrc=MSSQLServer&EvtID=11001)](http://www.microsoft.com/products/ee/transform.aspx?ProdName=Microsoft+SQL+Server&EvtSrc=MSSQLServer&EvtID=001)  
  
    The error number specified in the URL (11001) should be changed to match the error that you see.  
  
## See Also

[Installing the Driver Manager](../../../connect/odbc/linux-mac/installing-the-driver-manager.md)

[Release Notes](../../../connect/odbc/linux-mac/release-notes.md)

[System Requirements](../../../connect/odbc/linux-mac/system-requirements.md)
