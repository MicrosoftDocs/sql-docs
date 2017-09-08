---
title: "Lesson 1: Download the sample data| Microsoft Docs"
ms.custom: ""
ms.date: "07/26/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
applies_to: 
  - "SQL Server 2016"
dev_langs: 
  - "R"
  - "TSQL"
ms.assetid: 32a5d5ad-c58a-4669-a90d-ef296b48fcd8
caps.latest.revision: 10
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Lesson 1: Download the sample data

This article is part of a tutorial for SQL developers on how to use R in SQL Server.

In this step, you'll download the sample dataset and the [!INCLUDE[tsql](../../includes/tsql-md.md)] script files that are used in this tutorial. Both the data and the script files are shared on GitHub, but the PowerShell script will download the data and script files to a local directory of your choosing.

## Download the data and scripts

1.  Open a Windows PowerShell command console.
  
    Use the option, **Run as Administrator**, if administrative privileges are needed  to create the destination directory or to write files to the specified destination.
  
2.  Run the following PowerShell commands, changing the value of the parameter *DestDir* to any local directory.  The default we've used here is **TempRSQL**.
  
    ```ps
    $source = ‘https://raw.githubusercontent.com/Azure/Azure-MachineLearning-DataScience/master/Misc/RSQL/Download_Scripts_SQL_Walkthrough.ps1’  
    $ps1_dest = “$pwd\Download_Scripts_SQL_Walkthrough.ps1”
    $wc = New-Object System.Net.WebClient
    $wc.DownloadFile($source, $ps1_dest)
    .\Download_Scripts_SQL_Walkthrough.ps1 –DestDir ‘C:\tempRSQL’
    ```
  
    If the folder you specify in *DestDir* does not exist, it will be created by the PowerShell script.
  
    > [!TIP]
    > If you get an error, you can temporarily set the policy for  execution of PowerShell scripts to **unrestricted** only for this walkthrough, by using the Bypass argument and scoping the changes to the current session.
    >   
    >````
    > Set\-ExecutionPolicy Bypass \-Scope Process
    >````
    > Running this command does not result in a configuration change.
  
    Depending on your Internet connection, the download might take a while.
  
3.  When all files have been downloaded, the PowerShell script opens to the folder specified by  *DestDir*. In the PowerShell command prompt, run the following command and review the files that have been downloaded.
  
    ```
    ls
    ```
  
    **Results:**
  
    ![list of files downloaded by PowerShell script](media/rsql-devtut-filelist.png "list of files downloaded by PowerShell script")
  
## Next lesson

[Lesson 2: Import data to SQL Server using PowerShell](../r/sqldev-import-data-to-sql-server-using-powershell.md)

## Previous lesson

[In-database R analytics for SQL developers](../tutorials/sqldev-in-database-r-for-sql-developers.md)
