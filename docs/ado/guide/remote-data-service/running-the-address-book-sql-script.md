---
title: "Running the Address Book SQL Script"
description: "Running the Address Book SQL Script"
author: rothja
ms.author: jroth
ms.date: 11/09/2018
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "address book application scenario [ADO]"
  - "RDS scenarios [ADO]"
---
# Running the Address Book SQL Script
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](/dotnet/framework/wcf/).  
  
 You must use either the ISQL/Query Analyzer command-line utility or the SQL Server Enterprise Manager to run the included SQL script (Sampleemp.sql) that:  
  
-   Creates a new database, AddrBookDB, on the default device.  
  
-   Connects to the database, AddrBookDB.  
  
-   Creates an Employee table.  
  
-   Populates the table with sample data.  
  
-   Runs a simple SELECT statement to verify the population of the database table.  
  
-   Sets up a user account called "adcdemo" with a password of "adcdemo."  
  
#### To run the Sampleemp.sql script in Microsoft SQL Server 6.5  
  
1.  Click **Start**, point to **Programs**, and then point to **Microsoft SQL Server 6.5**. Click **SQL Enterprise Manager**.  
  
2.  From the **Tools** menu, click **SQL Query Tool**.  
  
3.  Click **Load SQL Script** and browse to c:\Platform SDK\Samples\DataAccess\RDS\AddressBook.  
  
4.  Select the file Sampleemp.sql. Click **Open**.  
  
5.  Click the **Execute Query** button (the green arrow on the toolbar).  
  
6.  After it executes, close the **Query** and **Enterprise Manager** windows.  
  
#### To run the Sampleemp.sql script in Microsoft SQL Server 7.0  
  
1.  Click **Start**, point to **Programs**, and then point to **Microsoft SQL Server 7.0**. Click **Enterprise Manager**.  
  
2.  Be sure that the SQL Server that you want to use is selected from your list of registered servers in Enterprise Manager.  
  
3.  From the **Tools** menu, click **SQL Server Query Analyzer**.  
  
4.  Click the **Load SQL Script** button (the open folder on the toolbar) and browse to c:\Platform SDK\Samples\DataAccess\RDS\AddressBook.  
  
5.  Select the file Sampleemp.sql. Click **Open**.  
  
6.  Click the **Execute Query** button (the green arrow on the toolbar) or **F5**.  
  
7.  After it executes, close the **Query**, **Query Analyzer**, and **Enterprise Manager** windows.  
  
## See Also  
 [Running the Address Book Sample Application](./running-the-address-book-sample-application.md)