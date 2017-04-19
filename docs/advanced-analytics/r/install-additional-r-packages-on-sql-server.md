---
title: "Install Additional R Packages on SQL Server | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "11/08/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 21456462-e58a-44c3-9d3a-68b4263575d7
caps.latest.revision: 16
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Install Additional R Packages on SQL Server
This topic describes how to install new R packages to an instance of [!INCLUDE[rsql_productname_md](../../includes/rsql-productname-md.md)] on a computer that has access to the Internet.

## 1. Locate the Windows binaries in ZIP file format

R packages are supported on many platforms. You must ensure that the package you  want to install has a binary format for the Windows platform. Otherwise the downloaded package will not work.

For example, to obtain the [FISHalyseR](http://bioconductor.org/packages/release/bioc/html/FISHalyseR.html) package from Bioconductor:  
  
1.  In the **Package Archives** list, find the **Windows binary** version.  
  
2.  Right-click the link to the .ZIP file, and select  **Save target as**.  
  
3.  Navigate to  the local folder where zipped packages are stored, and click **Save**.  
  
 This process creates a local copy of the package. You can then install the package, or copy the zipped package to a server that does not have Internet access.  
  
  
## 2. Open the default R package library for SQL Server R Services 

Navigate to the folder on the server where the R packages associated with [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] have been installed. It is important that you install packages to the default library that is associated with the current instance. 

For instructions on how to locate this library, see [Installing and Managing R Packages](../../advanced-analytics/r-services/installing-and-managing-r-packages.md).

   For each instance where you will run a package, you must install a separate copy of the packages. Currently packages cannot be shared across instances.
     
  
## 3. Open an administrative command prompt 

Open R as an administrator.  You can do this by using the Windows command p,ropt, or by using one of the R utilities.
  
### Using the Windows command prompt 

1. Open a Windows command prompt as administrator, and navigate to the directory where the RTerm.Exe or RGui.exe files are located.  
  
    In a default install, this is the R **\bin** directory. For example, in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], the R tools are located here: 

    **Default instance**

     `C:\Program Files\MSSQL13.MSSQLSERVER\R_SERVICES\bin` 
 
     **Named instance**
   
     `C:\Program files\MSSQL13.<instanceName>\R_SERVICES\bin\x64`  
  
2. Run **R.Exe**.  
  
### Using the R command-line utilities 
  
1. Use Windows Explorer to navigate to the directory containing the R tools.  
  
2. Right-click **RGui.exe** or **RTerm.exe**, and select **Run as administrator**.  
## 4. Install the package

The R command to install the package depends on whether you are getting the package from the Internet or from a local zipped file.  
  
### Install package from Internet  
  
1.  In general, you use the following command to install a new package from CRAN or one of the mirror sites.  
  
    ```  
    install.packages("target_package_name")  
    ```
    
    Note that double quotation marks are always required for the package name.

2.  To specify the library where the package should be installed, use a command like this one to set the library location:
    
    ```  
    lib.SQL <- "C:\\Program Files\\Microsoft SQL Server\\MSSQL13.MSSQLSERVER\\R_SERVICES\\library"    
    ```

    Note that for [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)], currently only one package library is allowed. Do not install packages to a user library, or you will not be able to run the package from [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)].   
     
3.  Having defined the library location, the following statement installs the popular e1070 package into the package library used by R Services.  
  
    ```  
    install.packages("e1071", lib = lib.SQL)  
    ```  
  
4.  You will be asked for a mirror site from which to obtain the package. Select any mirror site that is convenient for your location.  
  
    For a list of current CRAN mirrors, see [this site](https://cran.r-project.org/mirrors.html).  
  
    > [!TIP]  
    >  To avoid having to select a mirror site each time you add a new package, you can configure your R development environment to always use the same repository.  
    >   
    >  To do this, edit the global R settings file, .Rprofile, and add the following line:  
    >   
    >  `options(repos=structure(c(CRAN="<mirror site URL>")))`  
    >   
    >  For detailed information about preferences and other files loaded when the R runtime starts, run this command from an R console:  
    >   
    >  `?Startup`  
  
5.  If the target package depends on additional packages, the R installer will automatically download the dependencies and install them for you.  
  
### Manual package installation, or installing on computer with no Internet access 

1. If the package that you intend to install has dependencies, get the required packages ahead of time and add them to the folder with other package zipped files.

    > [!TIP]
    > 
    > We recommend that you set up a local repository using [miniCRAN](https://mran.revolutionanalytics.com/package/miniCRAN/) if you need to support frequent offline installation of R packages.  
  
2.  At the R command prompt, type the following command to specify the path and name of the package to install:  
   
    ```  
    install.packages("C:\\Temp\\Downloaded packages\\mynewpackage.zip", repos=NULL)  
    ``` 
     
    This command extracts the R package from its local zipped file, assuming you saved the copy in the directory `C:\Temp\Downloaded packages`, and installs the package (with its dependencies) into the R library on the local computer.  
  
3.  If you have previously modified the R environment on the computer, you should ensure that the R environment variable `.libPath` uses just one path, which points to the R_SERVICES folder for the instance.  
  
> [!NOTE]
> If you have installed Microsoft R Server (Standalone) in addition to SQL Server R Services, your computer will have a separate installation of R with all the R tools and libraries. Packages that are installed to the R_SERVER library are used only by Microsoft R Server and cannot be accessed by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
> 
>  Be sure to use the R_SERVICES library when installing packages that you want to use in SQL Server.

  
## See Also  
 [Set up SQL Server R Services &#40;In-Database&#41;](../../advanced-analytics/r-services/set-up-sql-server-r-services-in-database.md)  
  
  
