---
title: "Setup or Configure R Tools | Microsoft Docs"
ms.custom: ""
ms.date: "01/20/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 7c04ae30-d391-4369-9742-d2b275e14c0d
caps.latest.revision: 9
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Setup or Configure R Tools
  Microsoft R Server provides all the base R libraries, the a set of ScaleR packages, and standard R tools that you need to develop and test R code. However, if you want to use a dedicated R development environment, there are several available, including many free tools.  
  
## Basic R Tools  
 Additional tools are not required in an installation of Microsoft R Server, because all the standard R tools that are included in a *base installation* of R are installed by default.

-   **RTerm**: A command-line tool for running R scripts 
  
-   **RGui.exe**:  A simple interactive editor for R. The command-line arguments are the same for RGui.exe and RTerm. 
  
-   **RScript**: A command-line tool for running R scripts in batch mode.  

To locate these tools, find the R library location. This varies depending on whether you installed only SQL Server R Services, or if you also installed R Server (Standalone). For more information, see [What is Installed and Where to Find R Packages](https://msdn.microsoft.com/library/mt695941(sql.130).aspx#Anchor_1)

Then, look in the folder `..\R_SERVER\bin\x64`.  

> [!TIP]  
>  Need help with the R tools? Documentation is included, in the setup folder: `C:\Program Files\Microsoft SQL Server\R_SERVER\doc` and in `C:\Program Files\Microsoft SQL Server\R_SERVER\doc\manual`.  
>   
>  Or, just open **RGui**, click **Help**, and select one of the options.  

## Microsoft R Client

Microsoft R Client is a free download that lets you develop R solutions that can be easily run in Microsoft R Server or in SQL Server R Services. This option is provided to help data scientists who might not have access to R Server (available in Enterprise Edition) develop solutions that use ScaleR. 

If you use a different R development environment, such as R Tools for Visual Studio or RStudio, to use ScaleR you must specify that the Microsoft R Client be used as the R executable. This gives you full access to the RevoScaleR package and other features of Microsoft R Server, although performance will be limited.

You can also use the tools provided in R Client, such as RGui and RTerm, to run scripts or write and execute ad hoc R code.

[Install Microsoft R Client](https://msdn.microsoft.com/microsoft-r/r-client-install)
  
##  <a name="bkmk_RTools"></a> R Tools for Visual Studio  

 For convenience in working with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] databases, consider using [!INCLUDE[rsql_rtvs](../../includes/rsql-rtvs-md.md)] as your development environment. [!INCLUDE[rsql_rtvs](../../includes/rsql-rtvs-md.md)] is a free add-in for Visual Studio that works in all editions of Visual Studio. Visual Studio also provides support for Python and F# integration.  

 For installation instructions, see [How to install R Tools for Visual Studio](https://docs.microsoft.com/visualstudio/rtvs/installation).

> [!TIP]
> Before you install any new packages, check which R runtime is being used by default. Otherwise it can be very easy to install new R packages to a default library location and then not be able to find them from R Server!


## RStudio

If you prefer to use RStudio, some additional steps are required to use the RevoScaleR libraries:
- Install either Microsoft R Server or Microsoft R Client to get the required packages and libraries.
- Update your R path to use the R Server runtime.

For more information, see [Configure Your IDE](https://msdn.microsoft.com/microsoft-r/r-client-get-started#step-2-configure-your-ide).


## See Also  
 [Create a Standalone R Server](../../advanced-analytics/r-services/create-a-standalone-r-server.md)   
 [Getting Started with Microsoft R Server &#40;Standalone&#41;](../../advanced-analytics/r-services/getting-started-with-microsoft-r-server-standalone.md)  
  
  
