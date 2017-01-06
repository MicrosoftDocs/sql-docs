---
title: "Connect With PowerShell (SQL Server PDW)"
ms.custom: na
ms.date: 05/19/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 53560473-1f15-4373-a46d-ba526dea5fee
caps.latest.revision: 4
author: BarbKess
---
# Connect With PowerShell (SQL Server PDW)
Windows PowerShell exposes the hierarchy of SQL Server objects in paths similar to file system paths. You can use the paths to locate an object, and then use methods to perform actions on the objects. For more information about using SQL Server and PowerShell, see [SQL Server PowerShell Provider](http://msdn.microsoft.com/en-us/library/cc281947.aspx).  
  
## Examples  
In all examples, replace `xxx.xxx.xxx.xxx` with the IP address of your SQL Server PDW. Replace `[login]` with the login you use to connect to SQL Server PDW. Replace `[*********]` with the password for the login. Use TCP port 17000 for SQL Server 2008 PDW. Use TCP port 17001 for SQL Server 2012 PDW.  
  
To start PowerShell:  
  
1.  Open a command prompt.  
  
2.  To start PowerShell execute `PowerShell.exe`.  
  
### Example A: Simple Example  
The following example:  
  
1.  Connects to SQL Server PDW.  
  
2.  Defines and executes a SQL statement.  
  
3.  Closes the connection  
  
4.  Returns the statement results to the screen.  
  
```  
$conn = New-Object System.Data.SqlClient.SqlConnection  
$ConnectionString = "Server=xxx.xxx.xxx.xxx,17001;Integrated Security=False;user id=[login];password=[*********]"  
$conn.ConnectionString=$ConnectionString  
$conn.Open()  
  
$Query = "SELECT database_id, name FROM sys.databases;"  
$cmd=new-object system.Data.SqlClient.SqlCommand($Query,$conn)  
$ds=New-Object system.Data.DataSet  
$da=New-Object system.Data.SqlClient.SqlDataAdapter($cmd)  
  
[void]$da.fill($ds)  
$conn.Close()  
  
$ds.Tables  
```  
  
### Example B: Simple Example with Variables  
The following example: connects to SQL Server PDW, creates a connection, defines and executes a SQL statement, closes the connection, and returns the statement results to the screen.  
  
1.  Defines variables.  
  
2.  Connects to SQL Server PDW using some of the variables.  
  
3.  Builds and executes a SQL statement using some of the variables.  
  
4.  Closes the connection  
  
5.  Returns the statement results to the screen.  
  
```  
$Server="xxx.xxx.xxx.xxx,17001"  
$User="[login]"  
$Pwd="[*********]"  
$Database="AdventureWorksPDW2012"  
$Table="DimCustomer"  
  
$conn = New-Object System.Data.SqlClient.SqlConnection  
$ConnectionString = "Server=$Server;Integrated Security=False;user id=$User;password=$Pwd"  
$conn.ConnectionString=$ConnectionString  
$conn.Open()  
  
$Query = "SELECT COUNT(*) AS NumberOfCustomers FROM $Database.dbo.$Table;"  
$cmd=new-object system.Data.SqlClient.SqlCommand($Query,$conn)  
$ds=New-Object system.Data.DataSet  
$da=New-Object system.Data.SqlClient.SqlDataAdapter($cmd)  
  
[void]$da.fill($ds)  
$conn.Close()  
  
$ds.Tables  
```  
  
### Example C: Executing a SQL Command in a PowerShell Script  
Save the following text in a PowerShell script file named `TestScript.ps1` in a folder named `C:\PDWScripts`. The `Server`, `Port`, `User`, and `Pwd` variables are passed to PowerShell from the command line.  
  
```  
#Set the input variables  
param($Server, $Port, $User, $Pwd)  
  
#Open a connection  
$conn = New-Object System.Data.SqlClient.SqlConnection  
$ConnectionString = "Server=$Server,$Port;Integrated Security=False;user id=$User;password=$Pwd"  
$conn.ConnectionString=$ConnectionString  
$conn.Open()  
  
#Create and execute the SQL script  
$Query = "SELECT database_id, name FROM sys.databases;"  
$cmd=new-object system.Data.SqlClient.SqlCommand($Query,$conn)  
$ds=New-Object system.Data.DataSet  
$da=New-Object system.Data.SqlClient.SqlDataAdapter($cmd)  
  
#Clean up  
[void]$da.fill($ds)  
$conn.Close()  
  
#Return results to the screen  
$ds.Tables  
```  
  
Execute the following command at a command prompt. The `Server`, `Port`, `User`, and `Pwd` variables are passed to PowerShell from the command line.  
  
```  
PowerShell.exe C:\PDWScripts\TestScript.ps1 -Server xxx.xxx.xxx.xxx –Port [n] -User [login] –Pwd [*********]  
```  
  
