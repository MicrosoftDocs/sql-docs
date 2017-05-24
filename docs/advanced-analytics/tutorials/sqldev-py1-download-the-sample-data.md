---
title: "Step 1: Download the Sample Data| Microsoft Docs"
ms.custom: ""
ms.date: "04/28/2017"
ms.prod: "sql-server-2017"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
applies_to: 
  - "SQL Server 2017"
dev_langs: 
  - "Python"
  - "TSQL"
ms.assetid: 
caps.latest.revision: 1
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Step 1: Download the Sample Data

In this step, you'll download the sample dataset and the scripts that are used in this walkthrough. Both the data and the script files are shared on Github, but the PowerShell script will download the data and script files to a local directory of your choosing.

## Download the Data and Scripts

1. Open a Windows PowerShell command console.

    Use the option, **Run as Administrator**, if administrative privileges are needed to create the destination directory or to write files to the specified destination.

2. Run the following PowerShell commands, changing the value of the parameter *DestDir* to any local directory.  The default we've used here is **TempPythonSQL**.

    ```
    $source = 'https://raw.githubusercontent.com/Azure/Azure-MachineLearning-DataScience/master/Misc/PythonSQL/Download_Scripts_SQL_Walkthrough.ps1'
    $ps1_dest = "$pwd\Download_Scripts_SQL_Walkthrough.ps1"
    $wc = New-Object System.Net.WebClient
    $wc.DownloadFile($source, $ps1_dest)
    .\Download_Scripts_SQL_Walkthrough.ps1 –DestDir 'C:\tempPythonSQL'
    ```
    
    If the folder you specify in *DestDir* does not exist, it will be created by the PowerShell script.
    
    If you get an error, you can temporarily set the policy for execution of PowerShell scripts to **unrestricted** only for this walkthrough, by using the Bypass argument and scoping the changes to the current session. Running this command does not result in a configuration change.
    
    `Set\-ExecutionPolicy Bypass \-Scope Process`

3. Depending on your Internet connection, the download might take a while. When all files have been downloaded, the PowerShell script opens to the folder specified by  *DestDir*. In the PowerShell command prompt, run the following command and review the files that have been downloaded.

    ```
    ls
    ```
**Results:**

![list of files downloaded by PowerShell script](media/sqldev-python-filelist.png "list of files downloaded by PowerShell script")

## Next Step

[Step 2: Import Data to SQL Server using PowerShell](sqldev-py2-import-data-to-sql-server-using-powershell.md)

## Previous Step

[In-Database Python Analytics for the SQL Developer](sqldev-in-database-python-for-sql-developers.md)

## See Also

[Machine Learning Services with Python](../python/sql-server-python-services.md)


