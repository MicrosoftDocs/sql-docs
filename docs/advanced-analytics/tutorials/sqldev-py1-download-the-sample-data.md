---
title: "Step 1: Download the sample data | Microsoft Docs"
ms.custom: ""
ms.date: "10/17/2017"
ms.reviewer: 
ms.suite: sql
ms.prod: machine-learning-services
ms.prod_service: machine-learning-services
ms.component: 
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "tutorial"
applies_to: 
  - "SQL Server 2017"
dev_langs: 
  - "Python"
  - "TSQL"
ms.assetid: 
caps.latest.revision: 2
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Step 1: Download the sample data

This article is part of a tutorial, [In-database Python analytics for SQL developers](sqldev-in-database-python-for-sql-developers.md). 

Both the data and the scripts for this tutorial are shared on Github. In this step, you use a PowerShell script to download the data and script files to a local directory of your choosing.

## Run the script

1. Open a Windows PowerShell command console.

    Use the option, **Run as Administrator**, if administrative privileges are needed to create the destination directory or to write files to the specified destination.

2. Run the following PowerShell commands, changing the value of the parameter *DestDir* to any local directory.  The default we've used here is `C:\temp\pysql`.

    ```ps
    $source = 'https://raw.githubusercontent.com/Azure/Azure-MachineLearning-DataScience/master/Misc/PythonSQL/Download_Scripts_SQL_Walkthrough.ps1'
    $ps1_dest = "$pwd\Download_Scripts_SQL_Walkthrough.ps1"
    $wc = New-Object System.Net.WebClient
    $wc.DownloadFile($source, $ps1_dest)
    .\Download_Scripts_SQL_Walkthrough.ps1 –DestDir 'C:\temp\pysql'
    ```
    
    If the folder you specify in *DestDir* does not exist, it will be created by the PowerShell script.
    
    If you get an error, temporarily set the policy for execution of PowerShell scripts to **unrestricted** for this walkthrough, by using the **Bypass** argument and scoping the changes to the current session. Running this command does not result in a configuration change.
    
    ```ps
    Set-ExecutionPolicy Bypass -Scope Process
    ```

3. Depending on your internet connection, the download might take a while. 

## View the results

When all files have been downloaded, the PowerShell script opens to the folder specified by  *DestDir*. 

+ In the PowerShell command prompt, run the following command, to list the files that were downloaded.

    ```ps
    ls
    ```

![list of files downloaded by PowerShell script](media/sqldev-python-filelist.png "list of files downloaded by PowerShell script")

## Next step

[Step 2: Import data to SQL Server using PowerShell](sqldev-py2-import-data-to-sql-server-using-powershell.md)

## Previous Step

[In-Database Python Analytics for the SQL Developer](sqldev-in-database-python-for-sql-developers.md)

## See Also

[Machine Learning Services with Python](../python/sql-server-python-services.md)


