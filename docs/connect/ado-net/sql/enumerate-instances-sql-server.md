---
title: "Enumerating instances of SQL Server (ADO.NET)"
description: "Describes how to enumerate active instances of SQL Server."
author: David-Engel
ms.author: v-davidengel
ms.reviewer: v-kaywon
ms.date: "08/15/2019"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
dev_langs:
  - "csharp"
---
# Enumerating instances of SQL Server (ADO.NET)

[!INCLUDE[Driver_ADONET_Download](../../../includes/driver_adonet_download.md)]

SQL Server permits applications to find SQL Server instances within the current network. The <xref:System.Data.Sql.SqlDataSourceEnumerator> class exposes this information to the application developer, providing a <xref:System.Data.DataTable> containing information about all the visible servers. This returned table contains a list of server instances available on the network that matches the list provided when a user attempts to create a new connection, and expands the drop-down list containing all the available servers on the **Connection Properties** dialog box. The results displayed are not always complete.  
  
> [!NOTE]
>  As with most Windows services, it is best to run the SQL Browser service with the least possible privileges. See SQL Server Books Online for more information on the SQL Browser service, and how to manage its behavior.  
  
## Retrieving an enumerator instance  
In order to retrieve the table containing information about the available SQL Server instances, you must first retrieve an enumerator, using the shared/static <xref:System.Data.Sql.SqlDataSourceEnumerator.Instance%2A> property:  
  
```csharp  
System.Data.Sql.SqlDataSourceEnumerator instance =   
   System.Data.Sql.SqlDataSourceEnumerator.Instance  
```  
  
Once you have retrieved the static instance, you can call the <xref:System.Data.Sql.SqlDataSourceEnumerator.GetDataSources%2A> method, which returns a <xref:System.Data.DataTable> containing information about the available servers:  
  
```csharp  
System.Data.DataTable dataTable = instance.GetDataSources();  
```  
  
The table returned from the method call contains the following columns, all of which contain `string` values:  
  
|Column|Description|  
|------------|-----------------|  
|**ServerName**|Name of the server.|  
|**InstanceName**|Name of the server instance. Blank if the server is running as the default instance.|  
|**IsClustered**|Indicates whether the server is part of a cluster.|  
|**Version**|Version of the server. For example:<br /><br /> -   9.00.x (SQL Server 2005)<br />-   10.0.xx (SQL Server 2008)<br />-   10.50.x (SQL Server 2008 R2)<br />-   11.0.xx (SQL Server 2012)|  
  
## Enumeration limitations  
All of the available servers may or may not be listed. The list can vary depending on factors such as timeouts and network traffic. This can cause the list to be different on two consecutive calls. Only servers on the same network will be listed. Broadcast packets typically won't traverse routers, which is why you may not see a server listed, but it will be stable across calls.  
  
Listed servers may or may not have additional information such as `IsClustered` and version. This is dependent on how the list was obtained. Servers listed through the SQL Server browser service will have more details than those found through the Windows infrastructure, which will list only the name.  
  
> [!NOTE]
>  Server enumeration is only available when running in full-trust. Assemblies running in a partially-trusted environment will not be able to use it, even if they have the <xref:Microsoft.Data.SqlClient.SqlClientPermission> Code Access Security (CAS) permission.  
  
SQL Server provides information for the <xref:System.Data.Sql.SqlDataSourceEnumerator> through the use of an external Windows service named SQL Browser. This service is enabled by default, but administrators may turn it off or disable it, making the server instance invisible to this class.  
  
## Example  
The following console application retrieves information about all of the visible SQL Server instances and displays the information in the console window.  
  
```csharp  
using System.Data.Sql;  
  
class Program  
{  
  static void Main()  
  {  
    // Retrieve the enumerator instance and then the data.  
    SqlDataSourceEnumerator instance =  
      SqlDataSourceEnumerator.Instance;  
    System.Data.DataTable table = instance.GetDataSources();  
  
    // Display the contents of the table.  
    DisplayData(table);  
  
    Console.WriteLine("Press any key to continue.");  
    Console.ReadKey();  
  }  
  
  private static void DisplayData(System.Data.DataTable table)  
  {  
    foreach (System.Data.DataRow row in table.Rows)  
    {  
      foreach (System.Data.DataColumn col in table.Columns)  
      {  
        Console.WriteLine("{0} = {1}", col.ColumnName, row[col]);  
      }  
      Console.WriteLine("============================");  
    }  
  }  
}  
```  
  
## Next steps
- [SQL Server and ADO.NET](index.md)
