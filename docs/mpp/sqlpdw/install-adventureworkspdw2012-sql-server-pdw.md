---
title: "Install AdventureWorksPDW2012 (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: aac73ccd-4ca9-42c0-a207-4f2efc4ce38a
caps.latest.revision: 6
author: BarbKess
---
# Install AdventureWorksPDW2012 (SQL Server PDW)
This topic describes how to install the **AdventureWorksPDW2012** sample database onto SQL Server PDW.  
  
Parent Topic: [Client Tools and Applications &#40;SQL Server PDW&#41;](../sqlpdw/client-tools-and-applications-sql-server-pdw.md)  
  
## Contents  
  
-   [Before You Begin](#BeforeBegin)  
  
-   [Step 1: Determine Capacity Requirements](#Step1)  
  
-   [Step 2: Acquire the Server](#Step2)  
  
-   [Step 3: Connect the Server to the InfiniBand Networks](#Step3)  
  
## <a name="BeforeBegin"></a>Before You Begin  
Install the **dwloader** loading tool, if it is not already installed, onto the computer that will run the **AdventureWorksPDW2012** installation scripts. For installation instructions, see [Install dwloader Command-Line Loader &#40;SQL Server PDW&#41;](../sqlpdw/install-dwloader-command-line-loader-sql-server-pdw.md).  
  
## <a name="Step1"></a>Step 1: Obtain the Installation File  
Obtain the AdventureWorksPDW2012.zip file from your appliance administrator, or see [Client Tools and Applications &#40;SQL Server PDW&#41;](../sqlpdw/client-tools-and-applications-sql-server-pdw.md) for location information. This file contains the scripts and data that you need to install the **AdventureWorksPDW2012** sample database onto SQL Server PDW.  
  
## <a name="Step2"></a>Step 2: Install the Sample Database  
To install the **AdventureWorksPDW2012** sample database:  
  
1.  Open Windows Explorer and navigate to the folder that contains AdventureWorksPDW2012.zip.  
  
2.  Right-click on the AdventureWorksPDW2012.zip file, and choose Extract All. This will extract the files to an **AdventureWorksPDW2012** folder.  
  
3.  Edit aw_create.bat and set the following values:  
  
    -   server = IP address of the Control node cluster  
  
    -   user = SQL Server PDW login that has permission to load data and create a database.  
  
    -   password = User password.  
  
4.  Save your changes to aw_create.bat.  
  
5.  Right-click the aw_create.bat file, and select Open. Alternatively, to see the command-line activity, you can run the aw_create.bat file from a command-prompt.  
  
## <a name="Step3"></a>Step 3: Verify the database is installed  
To verify the installation is complete, open SQL Server Data Tools and view the databases on your appliance.  You should see **AdventureWorksPDW2012** in the list of databases.  
  
![AdventureWorksPDW2012](../sqlpdw/media/SQL_Server_PDW_AdventureWorks.png "SQL_Server_PDW_AdventureWorks")  
  
