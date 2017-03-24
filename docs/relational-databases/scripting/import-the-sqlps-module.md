---
title: "Import the SQLPS Module | Microsoft Docs"
ms.custom: ""
ms.date: "08/01/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: a972c56e-b2af-4fe6-abbd-817406e2c93a
caps.latest.revision: 12
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# Import the SQLPS Module
  The recommended way to manage [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from PowerShell is to import the **sqlps** module into a Windows PowerShell environment. The module loads and registers the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] snap-ins and manageability assemblies.  Beginning in Windows PowerShell 3.0, modules are imported automatically when any cmdlet or function in the module is used in a command. This feature works on any module in a directory that this included in the value of the PSModulePath environment variable.  For additional information, see [Importing a PowerShell Module](https://msdn.microsoft.com/library/dd878284(v=vs.85).aspx)
  
1.  **Before You Begin:**  [Security](#Security)  
  
2.  **To load the module:**  [Load the sqlps Module](#LoadSqlps)  
  
## Before You Begin  
 After importing the **sqlps** module into Windows PowerShell, you can then:  
  
-   Interactively run Windows PowerShell commands.  
  
-   Run Windows PowerShell script files.  
  
-   Run [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] cmdlets.  
  
-   Use the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provider paths to navigate through the hierarchy of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] objects.  
  
-   Use the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] manageability object models (such as Microsoft.SqlServer.Management.Smo) to manage [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] objects.  
  
> [!NOTE]  
>  The verbs used in the names of two SQL Server cmdlets (**Encode-Sqlname** and **Decode-Sqlname**) do not match the approved verbs for Windows PowerShell. This has no effect on their operation, but Windows PowerShell raises a warning when the **sqlps** module is imported to a session.  
  
###  <a name="Security"></a> Security  
 By default, Windows PowerShell runs with the scripting execution policy set to **Restricted**, which prevents running any Windows PowerShell scripts. To load the **sqlps** module, you can use the **Set-ExecutionPolicy** cmdlet to enable running signed scripts, or any scripts. Only run scripts from trusted sources, and secure all input and output files using the appropriate NTFS permissions. For more information about enabling Windows PowerShell scripts, see [Running Windows PowerShell Scripts](http://www.microsoft.com/technet/scriptcenter/topics/winpsh/manual/run.mspx).  
  
##  <a name="LoadSqlps"></a> Load the sqlps Module  
 **To load the sqlps module in Windows PowerShell**  
  
1.  Use the **Set-ExecutionPolicy** cmdlet to set the appropriate script execution policy.  
  
2.  Use the **Import-Module** cmdlet to import the sqlps module. Specify the **DisableNameChecking** parameter if you want to suppress the warning about **Encode-Sqlname** and **Decode-Sqlname**.  
  
### Example  
 This example loads the **sqlps** module with name checking turned off.  
  
```powershell 
# Import the SQL Server Module.    
Import-Module Sqlps -DisableNameChecking;

# To check whether the module is installed.
Get-Module -ListAvailable -Name Sqlps;
```  
  
> [!NOTE]  
>  If the **sqlps** module is not in your path, change to the location of the module or use the full path in the script (using double-quotes of folders in your path have spaces). The **sqlps** module is located in the Tools\Powershell folder for your SQL Server instance.  
  
 ![Arrow icon used with Back to Top link](../../analysis-services/instances/media/uparrow16x16.gif "Arrow icon used with Back to Top link") [&#91;Top&#93;]()  
  
## See Also  
 [SQL Server PowerShell](../../relational-databases/scripting/sql-server-powershell.md)   
 [SQL Server PowerShell Provider](../../relational-databases/scripting/sql-server-powershell-provider.md)   
 [Use the Database Engine cmdlets](../../relational-databases/scripting/use-the-database-engine-cmdlets.md)  
 [Installing a PowerShell Module](https://msdn.microsoft.com/library/dd878350(v=vs.85).aspx)  
 [Import-Module](https://technet.microsoft.com/library/hh849725.aspx)
  
  
