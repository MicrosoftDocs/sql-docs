---
title: "Install Microsoft R Server from the Command Line | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: fb4446ba-e9ce-4b93-9854-5e8a58507da0
caps.latest.revision: 4
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Install Microsoft R Server from the Command Line
    
You can install Microsoft R Server from the command line by using the scripting facilities provided with SQL Server setup. 

- **Unattended** installation requires that you specify the location of the setup utility, and use arguments to indicate which features to install. 
- For a **quiet** installation, provide the same arguments and add the **/q** switch. No prompts will be provided and no interaction is required. However, setup will fail if any required arguments are omitted.

For more information, see [Install SQL Server 2016 from the Command Prompt](../../database-engine/install-windows/install-sql-server-2016-from-the-command-prompt.md).

## Perform a Command-Line Install of Microsoft R Server (Standalone)

 The following examples shows the arguments used when performing a command line install of Microsoft R Server using SQL Server setup.


### Example of unattended installation of R Server Standalone

Run the following command from an elevated command prompt to install only Microsoft R Server (Standalone) and its prerequisites. 

```  
Setup.exe /q /ACTION=Install /FEATURES=SQL_SHARED_MR /IACCEPTROPENLICENSETERMS /IACCEPTSQLSERVERLICENSETERMS 
```  

To view progress and prompts, remove the _/q_ argument.

Note that all of the following arguments are required:
  - **FEATURES = SQL_SHARED_MR** gets only the Microsoft R Server components, including Microsoft R Open and any prerequisites.
  - **IACCEPTROPENLICENSETERMS** indicates you have accepted the license terms for using the open source R components.
  - **IACCEPTSQLSERVERLICENSETERMS** is required to run the setup wizard.


### Offline installation

If you install Microsoft R Server (Standalone) on a computer that has no Internet access, you must download the required R components in advance, and copy them to a local folder. For download locations, see [Installing R Components without Internet Access](../../advanced-analytics/r-services/installing-ml-components-without-internet-access.md).   


## Advanced Installation Tips

After setup is complete, you can review the configuration file created by SQL Server setup, along with a summary of setup actions.

By default, all setup logs and summaries for SQL Server and related features are created in `C:\Program Files\Microsoft SQL Server\140\Setup Bootstrap\Log`.

A separate subfolder is created  for each feature installed. For Microsoft R Server, look for: 
- `RSetup.log`
- `Settings.xml`
- `Summmary<instance_GUID>.txt`

To set up another instance of Microsoft R Server with the same parameters, you can also re-use the configuration file created during installation. For more information about [Install SQL Server Using a Configuration File](https://msdn.microsoft.com/library/dd239405.aspx)



### Customizing Your R Environment

After installation, you can install additional R packages. For more information, see [Installing and Managing R Packages](../../advanced-analytics/r-services/installing-and-managing-r-packages.md).

> [!IMPORTANT]
> If you intend to run your R code on SQL Server, make sure that you install the same packages on both the client computer running Microsoft R Server, and the SQL Server instance that is running R Services. 



## See Also  

[Microsoft R Server](../../advanced-analytics/r-services/getting-started-with-microsoft-r-server-standalone.md)
  
